"""
Test Prediction Script
Tests the trained model with sample predictions
"""

import sys
from pathlib import Path

# Add src to path
sys.path.insert(0, str(Path(__file__).parent / 'src'))

from data_loader import DataLoader
from predictor import PricePredictor
import logging

logging.basicConfig(level=logging.INFO)
logger = logging.getLogger(__name__)


def test_single_prediction(metal: str):
    """Test single day prediction"""
    
    print(f"\n{'='*70}")
    print(f"TESTING {metal.upper()} PREDICTION")
    print('='*70)
    
    try:
        # Load recent data
        loader = DataLoader(data_dir='data')
        df = loader.load_data(metal)
        recent_data = df.tail(60)  # Last 60 days
        
        print(f"\nUsing last 60 days of data:")
        print(f"  Date range: {recent_data['Date'].min().date()} to {recent_data['Date'].max().date()}")
        print(f"  Last closing price: ${recent_data['Close'].iloc[-1]:.2f}")
        
        # Make prediction
        predictor = PricePredictor(metal, model_dir='models')
        result = predictor.predict(recent_data)
        
        print(f"\nPrediction Result:")
        print(f"  Predicted Price: ${result['predictedPrice']:.2f}")
        print(f"  Prediction Date: {result['predictionDate']}")
        print(f"  Confidence: {result['confidence']:.2%}")
        print(f"  Timestamp: {result['timestamp']}")
        
        # Calculate change
        last_price = recent_data['Close'].iloc[-1]
        change = result['predictedPrice'] - last_price
        change_pct = (change / last_price) * 100
        
        print(f"\nPrice Change:")
        print(f"  Absolute: ${change:+.2f}")
        print(f"  Percentage: {change_pct:+.2f}%")
        
        return result
        
    except FileNotFoundError as e:
        print(f"\n❌ Error: {e}")
        print(f"   Please train the {metal} model first:")
        print(f"   python train.py --metal {metal}")
        return None
    except Exception as e:
        print(f"\n❌ Unexpected error: {e}")
        return None


def test_multi_day_prediction(metal: str, days: int = 7):
    """Test multiple days prediction"""
    
    print(f"\n{'='*70}")
    print(f"TESTING {metal.upper()} - {days} DAYS AHEAD PREDICTION")
    print('='*70)
    
    try:
        # Load recent data
        loader = DataLoader(data_dir='data')
        df = loader.load_data(metal)
        recent_data = df.tail(60)
        
        # Make predictions
        predictor = PricePredictor(metal, model_dir='models')
        predictions = predictor.predict_multiple_days(recent_data, days_ahead=days)
        
        print(f"\nPredictions for next {days} days:")
        print(f"{'Day':<6} {'Date':<12} {'Price':<12} {'Confidence':<12}")
        print('-' * 50)
        
        for pred in predictions:
            print(f"{pred['daysAhead']:<6} {pred['predictionDate']:<12} "
                  f"${pred['predictedPrice']:<11.2f} {pred['confidence']:<11.2%}")
        
        return predictions
        
    except Exception as e:
        print(f"\n❌ Error: {e}")
        return None


def main():
    """Main test function"""
    
    print("="*70)
    print("PRICE PREDICTION TESTING SUITE")
    print("="*70)
    
    metals = ['gold', 'silver']
    
    # Test single predictions
    for metal in metals:
        test_single_prediction(metal)
    
    # Test multi-day predictions
    print("\n" + "="*70)
    print("MULTI-DAY PREDICTIONS")
    print("="*70)
    
    for metal in metals:
        test_multi_day_prediction(metal, days=7)
    
    print("\n" + "="*70)
    print("✓ Testing completed!")
    print("="*70)


if __name__ == "__main__":
    main()
