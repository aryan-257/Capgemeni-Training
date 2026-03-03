"""
Predictor Module
Makes price predictions using trained LSTM models
"""

import numpy as np
import pandas as pd
from pathlib import Path
from datetime import datetime, timedelta
import logging
from preprocessor import DataPreprocessor
from model_builder import LSTMModelBuilder

logging.basicConfig(level=logging.INFO)
logger = logging.getLogger(__name__)


class PricePredictor:
    """Makes predictions using trained LSTM models"""
    
    def __init__(self, metal: str, model_dir: str = "models", sequence_length: int = 60):
        """
        Initialize predictor
        
        Args:
            metal: 'gold' or 'silver'
            model_dir: Directory containing trained models
            sequence_length: Number of past days used in training
        """
        self.metal = metal.lower()
        self.sequence_length = sequence_length
        
        # Load model
        self.model_builder = LSTMModelBuilder(sequence_length=sequence_length, n_features=5)
        self.model_builder.load_model(self.metal, model_dir)
        
        # Load scaler
        self.preprocessor = DataPreprocessor(sequence_length=sequence_length)
        self.preprocessor.load_scaler(self.metal, model_dir)
        
        logger.info(f"{self.metal.upper()} predictor initialized")
    
    def predict(self, recent_data: pd.DataFrame) -> dict:
        """
        Predict next day's price
        
        Args:
            recent_data: DataFrame with last sequence_length days of data
            
        Returns:
            Dictionary with prediction results
        """
        if len(recent_data) < self.sequence_length:
            raise ValueError(f"Need at least {self.sequence_length} days of historical data")
        
        # Prepare input
        X = self.preprocessor.prepare_prediction_input(recent_data)
        
        # Make prediction (normalized)
        scaled_prediction = self.model_builder.model.predict(X, verbose=0)[0][0]
        
        # Convert back to original scale
        predicted_price = self.preprocessor.inverse_transform_price(scaled_prediction)
        
        # Calculate confidence score (simplified)
        confidence = self._calculate_confidence(recent_data, predicted_price)
        
        result = {
            'predictedPrice': float(predicted_price),
            'metal': self.metal.capitalize(),
            'predictionDate': (datetime.now() + timedelta(days=1)).strftime('%Y-%m-%d'),
            'confidence': float(confidence),
            'timestamp': datetime.utcnow().isoformat() + 'Z',
            'success': True,
            'errorMessage': None
        }
        
        logger.info(f"Prediction: ${predicted_price:.2f} (confidence: {confidence:.2%})")
        
        return result
    
    def predict_multiple_days(self, recent_data: pd.DataFrame, days_ahead: int = 7) -> list:
        """
        Predict prices for multiple days ahead
        
        Args:
            recent_data: DataFrame with historical data
            days_ahead: Number of days to predict
            
        Returns:
            List of prediction dictionaries
        """
        predictions = []
        current_data = recent_data.copy()
        
        for day in range(1, days_ahead + 1):
            # Predict next day
            result = self.predict(current_data)
            result['predictionDate'] = (datetime.now() + timedelta(days=day)).strftime('%Y-%m-%d')
            result['daysAhead'] = day
            predictions.append(result)
            
            # Update data with prediction for next iteration
            # (In production, you'd want actual data, not predictions)
            last_row = current_data.iloc[-1].copy()
            last_row['Close'] = result['predictedPrice']
            last_row['Date'] = pd.to_datetime(result['predictionDate'])
            
            current_data = pd.concat([current_data, pd.DataFrame([last_row])], ignore_index=True)
            current_data = current_data.tail(self.sequence_length)
        
        return predictions
    
    def _calculate_confidence(self, recent_data: pd.DataFrame, predicted_price: float) -> float:
        """
        Calculate confidence score for prediction
        
        Args:
            recent_data: Recent historical data
            predicted_price: Predicted price
            
        Returns:
            Confidence score (0-1)
        """
        # Simple confidence calculation based on:
        # 1. Price volatility (lower volatility = higher confidence)
        # 2. Prediction within reasonable range
        
        recent_prices = recent_data['Close'].tail(30)
        
        # Calculate volatility (standard deviation / mean)
        volatility = recent_prices.std() / recent_prices.mean()
        
        # Calculate how far prediction is from recent average
        recent_avg = recent_prices.mean()
        price_deviation = abs(predicted_price - recent_avg) / recent_avg
        
        # Base confidence (higher for lower volatility)
        base_confidence = max(0.5, 1.0 - (volatility * 10))
        
        # Reduce confidence if prediction is far from recent average
        if price_deviation > 0.1:  # More than 10% deviation
            base_confidence *= 0.8
        
        # Ensure confidence is between 0.5 and 0.99
        confidence = max(0.5, min(0.99, base_confidence))
        
        return confidence
    
    def get_model_info(self) -> dict:
        """
        Get information about the loaded model
        
        Returns:
            Dictionary with model information
        """
        return {
            'metal': self.metal,
            'sequence_length': self.sequence_length,
            'model_parameters': self.model_builder.model.count_params(),
            'input_shape': str(self.model_builder.model.input_shape),
            'output_shape': str(self.model_builder.model.output_shape)
        }


if __name__ == "__main__":
    # Example usage
    from data_loader import DataLoader
    
    # Load recent data
    loader = DataLoader()
    df = loader.load_data('gold')
    recent_data = df.tail(60)  # Last 60 days
    
    # Make prediction
    predictor = PricePredictor('gold')
    result = predictor.predict(recent_data)
    
    print("\n" + "="*60)
    print("PREDICTION RESULT")
    print("="*60)
    print(f"Metal: {result['metal']}")
    print(f"Predicted Price: ${result['predictedPrice']:.2f}")
    print(f"Prediction Date: {result['predictionDate']}")
    print(f"Confidence: {result['confidence']:.2%}")
    print("="*60)
    
    # Predict multiple days
    print("\nPredicting next 7 days...")
    multi_predictions = predictor.predict_multiple_days(recent_data, days_ahead=7)
    
    for pred in multi_predictions:
        print(f"Day {pred['daysAhead']}: ${pred['predictedPrice']:.2f} ({pred['confidence']:.1%})")
