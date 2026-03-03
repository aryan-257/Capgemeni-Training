"""
Flask REST API for Gold & Silver Price Prediction - DEMO MODE
Works without TensorFlow for quick testing
"""

from flask import Flask, request, jsonify
from flask_cors import CORS
import logging
from datetime import datetime, timedelta
import random

# Configure logging
logging.basicConfig(
    level=logging.INFO,
    format='%(asctime)s - %(name)s - %(levelname)s - %(message)s'
)
logger = logging.getLogger(__name__)

# Initialize Flask app
app = Flask(__name__)
CORS(app)  # Enable CORS for ASP.NET MVC requests

# Demo base prices
BASE_PRICES = {
    'gold': 2050.00,
    'silver': 24.50
}


@app.route('/', methods=['GET'])
def home():
    """API home endpoint - shows status"""
    return jsonify({
        'service': 'Gold & Silver Price Prediction API - DEMO MODE',
        'version': '1.0.0-demo',
        'status': 'running',
        'mode': 'demo',
        'note': 'Using simulated predictions for testing',
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
        'mode': 'demo',
        'models_loaded': 2,
        'available_metals': ['gold', 'silver'],
        'timestamp': datetime.utcnow().isoformat() + 'Z'
    })


@app.route('/models', methods=['GET'])
def get_models_info():
    """Get information about loaded models"""
    return jsonify({
        'models': {
            'gold': {
                'metal': 'gold',
                'mode': 'demo',
                'base_price': BASE_PRICES['gold']
            },
            'silver': {
                'metal': 'silver',
                'mode': 'demo',
                'base_price': BASE_PRICES['silver']
            }
        },
        'total_models': 2
    })


@app.route('/predict', methods=['POST'])
def predict():
    """
    Main prediction endpoint - DEMO MODE
    
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
        
        if metal not in BASE_PRICES:
            return jsonify({
                'success': False,
                'errorMessage': f'Invalid metal type: {metal}. Use "gold" or "silver"'
            }), 400
        
        # Validate days ahead
        if not isinstance(days_ahead, int) or days_ahead < 1 or days_ahead > 30:
            return jsonify({
                'success': False,
                'errorMessage': 'daysAhead must be an integer between 1 and 30'
            }), 400
        
        # Log request
        logger.info(f"Prediction request: metal={metal}, date={prediction_date}, days_ahead={days_ahead}")
        
        # Generate simulated prediction
        base_price = BASE_PRICES[metal]
        
        # Add realistic variation (+/- 2%)
        variation = random.uniform(-0.02, 0.02)
        predicted_price = base_price * (1 + variation)
        
        # Add trend based on days ahead (slight upward trend)
        trend = days_ahead * 0.001
        predicted_price *= (1 + trend)
        
        # Generate confidence (85-95%)
        confidence = random.uniform(0.85, 0.95)
        
        # Use provided date or calculate
        if not prediction_date:
            prediction_date = (datetime.now() + timedelta(days=days_ahead)).strftime('%Y-%m-%d')
        
        result = {
            'predictedPrice': round(predicted_price, 2),
            'metal': metal.capitalize(),
            'predictionDate': prediction_date,
            'confidence': round(confidence, 2),
            'timestamp': datetime.utcnow().isoformat() + 'Z',
            'success': True,
            'errorMessage': None,
            'mode': 'demo'
        }
        
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
        return jsonify({
            'success': False,
            'errorMessage': f'Internal server error: {str(e)}'
        }), 500


@app.route('/predict/batch', methods=['POST'])
def predict_batch():
    """
    Batch prediction endpoint for multiple days - DEMO MODE
    
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
        if metal not in BASE_PRICES:
            return jsonify({
                'success': False,
                'errorMessage': f'Invalid metal type: {metal}'
            }), 400
        
        if not isinstance(days_ahead, int) or days_ahead < 1 or days_ahead > 30:
            return jsonify({
                'success': False,
                'errorMessage': 'daysAhead must be between 1 and 30'
            }), 400
        
        # Generate predictions for each day
        predictions = []
        base_price = BASE_PRICES[metal]
        
        for day in range(1, days_ahead + 1):
            variation = random.uniform(-0.02, 0.02)
            trend = day * 0.001
            predicted_price = base_price * (1 + variation + trend)
            confidence = random.uniform(0.85, 0.95)
            
            pred = {
                'daysAhead': day,
                'predictedPrice': round(predicted_price, 2),
                'metal': metal.capitalize(),
                'predictionDate': (datetime.now() + timedelta(days=day)).strftime('%Y-%m-%d'),
                'confidence': round(confidence, 2),
                'timestamp': datetime.utcnow().isoformat() + 'Z',
                'success': True,
                'mode': 'demo'
            }
            predictions.append(pred)
        
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
    logger.info("="*70)
    logger.info("GOLD & SILVER PRICE PREDICTION API - DEMO MODE")
    logger.info("="*70)
    logger.info("✓ Demo mode active - using simulated predictions")
    logger.info("✓ No TensorFlow required for testing")
    logger.info("✓ API ready for ASP.NET MVC integration")
    logger.info("="*70)
    logger.info("\nStarting Flask API server...")
    logger.info("API available at: http://localhost:5000")
    logger.info("Press CTRL+C to stop the server\n")
    
    app.run(
        host='0.0.0.0',
        port=5000,
        debug=False,
        threaded=True
    )
