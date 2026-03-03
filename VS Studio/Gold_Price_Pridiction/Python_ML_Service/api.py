"""
Flask REST API for Gold & Silver Price Prediction
Serves predictions from trained LSTM models
"""

from flask import Flask, request, jsonify
from flask_cors import CORS
import sys
from pathlib import Path
import logging
from datetime import datetime
import traceback

# Add src to path
sys.path.insert(0, str(Path(__file__).parent / 'src'))

from data_loader import DataLoader
from predictor import PricePredictor

# Configure logging
logging.basicConfig(
    level=logging.INFO,
    format='%(asctime)s - %(name)s - %(levelname)s - %(message)s'
)
logger = logging.getLogger(__name__)

# Initialize Flask app
app = Flask(__name__)
CORS(app)  # Enable CORS for ASP.NET MVC requests

# Global variables for models and data
predictors = {}
data_loader = None
recent_data_cache = {}


def initialize_models():
    """Load trained models and recent data on startup"""
    global predictors, data_loader, recent_data_cache
    
    logger.info("="*70)
    logger.info("INITIALIZING PREDICTION API")
    logger.info("="*70)
    
    try:
        # Initialize data loader
        data_loader = DataLoader(data_dir='data')
        logger.info("✓ Data loader initialized")
        
        # Load models for both metals
        metals = ['gold', 'silver']
        
        for metal in metals:
            try:
                # Load predictor
                predictors[metal] = PricePredictor(
                    metal=metal,
                    model_dir='models',
                    sequence_length=60
                )
                logger.info(f"✓ {metal.upper()} model loaded successfully")
                
                # Load and cache recent data
                df = data_loader.load_data(metal)
                recent_data_cache[metal] = df.tail(60)
                logger.info(f"✓ {metal.upper()} recent data cached (last 60 days)")
                
            except FileNotFoundError as e:
                logger.warning(f"⚠ {metal.upper()} model not found: {e}")
                logger.warning(f"  Please train the model: python train.py --metal {metal}")
            except Exception as e:
                logger.error(f"✗ Failed to load {metal.upper()} model: {e}")
        
        if not predictors:
            logger.error("✗ No models loaded! Please train models first.")
            logger.error("  Run: python train.py --metal both --epochs 50")
        else:
            logger.info("="*70)
            logger.info(f"✓ API READY - {len(predictors)} model(s) loaded")
            logger.info("="*70)
        
    except Exception as e:
        logger.error(f"✗ Initialization failed: {e}")
        logger.error(traceback.format_exc())


@app.route('/', methods=['GET'])
def home():
    """API home endpoint - shows status"""
    return jsonify({
        'service': 'Gold & Silver Price Prediction API',
        'version': '1.0.0',
        'status': 'running',
        'models_loaded': list(predictors.keys()),
        'endpoints': {
            'predict': 'POST /predict',
            'health': 'GET /health',
            'models': 'GET /models'
        }
    })


@app.route('/health', methods=['GET'])
def health_check():
    """Health check endpoint"""
    return jsonify({
        'status': 'healthy',
        'models_loaded': len(predictors),
        'available_metals': list(predictors.keys()),
        'timestamp': datetime.utcnow().isoformat() + 'Z'
    })


@app.route('/models', methods=['GET'])
def get_models_info():
    """Get information about loaded models"""
    models_info = {}
    
    for metal, predictor in predictors.items():
        models_info[metal] = predictor.get_model_info()
    
    return jsonify({
        'models': models_info,
        'total_models': len(predictors)
    })


@app.route('/predict', methods=['POST'])
def predict():
    """
    Main prediction endpoint
    
    Request Body:
    {
        "metal": "Gold" or "Silver",
        "predictionDate": "2024-12-15",
        "daysAhead": 1
    }
    
    Response:
    {
        "predictedPrice": 2150.75,
        "metal": "Gold",
        "predictionDate": "2024-12-15",
        "confidence": 0.92,
        "timestamp": "2024-12-14T10:30:00Z",
        "success": true,
        "errorMessage": null
    }
    """
    try:
        # Get request data
        data = request.get_json()
        
        if not data:
            return jsonify({
                'success': False,
                'errorMessage': 'No JSON data provided'
            }), 400
        
        # Extract parameters
        metal = data.get('metal', '').lower()
        prediction_date = data.get('predictionDate')
        days_ahead = data.get('daysAhead', 1)
        
        # Validate metal
        if not metal:
            return jsonify({
                'success': False,
                'errorMessage': 'Metal type is required'
            }), 400
        
        if metal not in predictors:
            available = ', '.join(predictors.keys())
            return jsonify({
                'success': False,
                'errorMessage': f'Model not available for metal: {metal}. Available: {available}'
            }), 404
        
        # Validate days ahead
        if not isinstance(days_ahead, int) or days_ahead < 1 or days_ahead > 30:
            return jsonify({
                'success': False,
                'errorMessage': 'daysAhead must be an integer between 1 and 30'
            }), 400
        
        # Log request
        logger.info(f"Prediction request: metal={metal}, date={prediction_date}, days_ahead={days_ahead}")
        
        # Get predictor and recent data
        predictor = predictors[metal]
        recent_data = recent_data_cache[metal]
        
        # Make prediction
        if days_ahead == 1:
            # Single day prediction
            result = predictor.predict(recent_data)
        else:
            # Multi-day prediction
            predictions = predictor.predict_multiple_days(recent_data, days_ahead=days_ahead)
            result = predictions[-1]  # Return the final prediction
        
        # Override prediction date if provided
        if prediction_date:
            result['predictionDate'] = prediction_date
        
        # Log result
        logger.info(f"Prediction: ${result['predictedPrice']:.2f} (confidence: {result['confidence']:.2%})")
        
        return jsonify(result), 200
        
    except ValueError as e:
        logger.error(f"Validation error: {e}")
        return jsonify({
            'success': False,
            'errorMessage': str(e)
        }), 400
        
    except Exception as e:
        logger.error(f"Prediction error: {e}")
        logger.error(traceback.format_exc())
        return jsonify({
            'success': False,
            'errorMessage': f'Internal server error: {str(e)}'
        }), 500


@app.route('/predict/batch', methods=['POST'])
def predict_batch():
    """
    Batch prediction endpoint for multiple days
    
    Request Body:
    {
        "metal": "Gold",
        "daysAhead": 7
    }
    
    Response:
    {
        "predictions": [
            {"day": 1, "price": 2150.75, "confidence": 0.92, ...},
            {"day": 2, "price": 2155.30, "confidence": 0.90, ...},
            ...
        ],
        "success": true
    }
    """
    try:
        data = request.get_json()
        
        if not data:
            return jsonify({
                'success': False,
                'errorMessage': 'No JSON data provided'
            }), 400
        
        metal = data.get('metal', '').lower()
        days_ahead = data.get('daysAhead', 7)
        
        # Validate
        if metal not in predictors:
            return jsonify({
                'success': False,
                'errorMessage': f'Model not available for metal: {metal}'
            }), 404
        
        if not isinstance(days_ahead, int) or days_ahead < 1 or days_ahead > 30:
            return jsonify({
                'success': False,
                'errorMessage': 'daysAhead must be between 1 and 30'
            }), 400
        
        # Make predictions
        predictor = predictors[metal]
        recent_data = recent_data_cache[metal]
        
        predictions = predictor.predict_multiple_days(recent_data, days_ahead=days_ahead)
        
        logger.info(f"Batch prediction: metal={metal}, days={days_ahead}, results={len(predictions)}")
        
        return jsonify({
            'predictions': predictions,
            'total': len(predictions),
            'success': True
        }), 200
        
    except Exception as e:
        logger.error(f"Batch prediction error: {e}")
        return jsonify({
            'success': False,
            'errorMessage': str(e)
        }), 500


@app.errorhandler(404)
def not_found(error):
    """Handle 404 errors"""
    return jsonify({
        'success': False,
        'errorMessage': 'Endpoint not found'
    }), 404


@app.errorhandler(500)
def internal_error(error):
    """Handle 500 errors"""
    logger.error(f"Internal server error: {error}")
    return jsonify({
        'success': False,
        'errorMessage': 'Internal server error'
    }), 500


if __name__ == '__main__':
    # Initialize models before starting server
    initialize_models()
    
    # Start Flask server
    logger.info("\nStarting Flask API server...")
    logger.info("API will be available at: http://localhost:5000")
    logger.info("Press CTRL+C to stop the server\n")
    
    app.run(
        host='0.0.0.0',  # Listen on all interfaces
        port=5000,
        debug=False,  # Set to True for development
        threaded=True  # Handle multiple requests
    )
