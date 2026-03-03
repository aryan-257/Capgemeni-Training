"""
Data Preprocessor Module
Cleans, normalizes, and prepares data for LSTM model training
"""

import numpy as np
import pandas as pd
from sklearn.preprocessing import MinMaxScaler
from typing import Tuple
import joblib
from pathlib import Path
import logging

logging.basicConfig(level=logging.INFO)
logger = logging.getLogger(__name__)


class DataPreprocessor:
    """Handles data preprocessing for LSTM model"""
    
    def __init__(self, sequence_length: int = 60):
        """
        Initialize preprocessor
        
        Args:
            sequence_length: Number of past days to use for prediction
        """
        self.sequence_length = sequence_length
        self.scaler = MinMaxScaler(feature_range=(0, 1))
        self.feature_columns = ['Open', 'High', 'Low', 'Close', 'Volume']
    
    def prepare_data(self, df: pd.DataFrame) -> Tuple[np.ndarray, np.ndarray]:
        """
        Prepare data for LSTM training
        
        Args:
            df: DataFrame with price data
            
        Returns:
            Tuple of (X, y) where X is sequences and y is targets
        """
        logger.info("Preparing data for LSTM...")
        
        # Extract features
        data = df[self.feature_columns].values
        
        # Normalize data
        scaled_data = self.scaler.fit_transform(data)
        
        # Create sequences
        X, y = self._create_sequences(scaled_data)
        
        logger.info(f"Created {len(X)} sequences of length {self.sequence_length}")
        
        return X, y
    
    def _create_sequences(self, data: np.ndarray) -> Tuple[np.ndarray, np.ndarray]:
        """
        Create sequences for LSTM
        
        Args:
            data: Normalized price data
            
        Returns:
            Tuple of (X, y) sequences
        """
        X, y = [], []
        
        for i in range(self.sequence_length, len(data)):
            # Use past sequence_length days to predict next day's close price
            X.append(data[i-self.sequence_length:i])
            y.append(data[i, 3])  # Index 3 is 'Close' price
        
        return np.array(X), np.array(y)
    
    def split_data(self, X: np.ndarray, y: np.ndarray, 
                   train_ratio: float = 0.8) -> Tuple[np.ndarray, np.ndarray, np.ndarray, np.ndarray]:
        """
        Split data into training and validation sets
        
        Args:
            X: Input sequences
            y: Target values
            train_ratio: Ratio of training data
            
        Returns:
            Tuple of (X_train, X_val, y_train, y_val)
        """
        split_idx = int(len(X) * train_ratio)
        
        X_train = X[:split_idx]
        X_val = X[split_idx:]
        y_train = y[:split_idx]
        y_val = y[split_idx:]
        
        logger.info(f"Training samples: {len(X_train)}, Validation samples: {len(X_val)}")
        
        return X_train, X_val, y_train, y_val
    
    def inverse_transform_price(self, scaled_price: float) -> float:
        """
        Convert normalized price back to original scale
        
        Args:
            scaled_price: Normalized price (0-1)
            
        Returns:
            Original price value
        """
        # Create dummy array with all features
        dummy = np.zeros((1, len(self.feature_columns)))
        dummy[0, 3] = scaled_price  # Index 3 is 'Close' price
        
        # Inverse transform
        original = self.scaler.inverse_transform(dummy)
        
        return float(original[0, 3])
    
    def save_scaler(self, metal: str, output_dir: str = "models") -> None:
        """
        Save the fitted scaler for later use
        
        Args:
            metal: 'gold' or 'silver'
            output_dir: Directory to save scaler
        """
        output_path = Path(output_dir) / f"{metal.lower()}_scaler.pkl"
        output_path.parent.mkdir(parents=True, exist_ok=True)
        
        joblib.dump(self.scaler, output_path)
        logger.info(f"Scaler saved to {output_path}")
    
    def load_scaler(self, metal: str, model_dir: str = "models") -> None:
        """
        Load a previously saved scaler
        
        Args:
            metal: 'gold' or 'silver'
            model_dir: Directory containing scaler
        """
        scaler_path = Path(model_dir) / f"{metal.lower()}_scaler.pkl"
        
        if not scaler_path.exists():
            raise FileNotFoundError(f"Scaler not found: {scaler_path}")
        
        self.scaler = joblib.load(scaler_path)
        logger.info(f"Scaler loaded from {scaler_path}")
    
    def prepare_prediction_input(self, recent_data: pd.DataFrame) -> np.ndarray:
        """
        Prepare recent data for making a prediction
        
        Args:
            recent_data: DataFrame with last sequence_length days of data
            
        Returns:
            Normalized sequence ready for model input
        """
        if len(recent_data) < self.sequence_length:
            raise ValueError(f"Need at least {self.sequence_length} days of data")
        
        # Get last sequence_length days
        data = recent_data[self.feature_columns].tail(self.sequence_length).values
        
        # Normalize
        scaled_data = self.scaler.transform(data)
        
        # Reshape for model input: (1, sequence_length, features)
        return scaled_data.reshape(1, self.sequence_length, len(self.feature_columns))


if __name__ == "__main__":
    # Example usage
    from data_loader import DataLoader
    
    # Load data
    loader = DataLoader()
    df = loader.load_data('gold')
    
    # Preprocess
    preprocessor = DataPreprocessor(sequence_length=60)
    X, y = preprocessor.prepare_data(df)
    
    print(f"Input shape: {X.shape}")  # (samples, sequence_length, features)
    print(f"Output shape: {y.shape}")  # (samples,)
    
    # Split data
    X_train, X_val, y_train, y_val = preprocessor.split_data(X, y)
    print(f"Training set: {X_train.shape}")
    print(f"Validation set: {X_val.shape}")
    
    # Save scaler
    preprocessor.save_scaler('gold')
