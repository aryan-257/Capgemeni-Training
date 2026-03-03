"""
LSTM Model Builder Module
Defines the neural network architecture for price prediction
"""

from tensorflow import keras
from keras import layers, models, callbacks
import numpy as np
from typing import Tuple
import logging

logging.basicConfig(level=logging.INFO)
logger = logging.getLogger(__name__)


class LSTMModelBuilder:
    """Builds and configures LSTM neural network"""
    
    def __init__(self, sequence_length: int = 60, n_features: int = 5):
        """
        Initialize model builder
        
        Args:
            sequence_length: Number of time steps in input sequence
            n_features: Number of features per time step
        """
        self.sequence_length = sequence_length
        self.n_features = n_features
        self.model = None
    
    def build_model(self, lstm_units: list = [50, 50, 50], 
                   dropout_rate: float = 0.2) -> models.Sequential:
        """
        Build LSTM model architecture
        
        Args:
            lstm_units: List of units for each LSTM layer
            dropout_rate: Dropout rate for regularization
            
        Returns:
            Compiled Keras model
        """
        logger.info("Building LSTM model...")
        
        model = models.Sequential()
        
        # First LSTM layer
        model.add(layers.LSTM(
            units=lstm_units[0],
            return_sequences=True,
            input_shape=(self.sequence_length, self.n_features)
        ))
        model.add(layers.Dropout(dropout_rate))
        
        # Second LSTM layer
        model.add(layers.LSTM(
            units=lstm_units[1],
            return_sequences=True
        ))
        model.add(layers.Dropout(dropout_rate))
        
        # Third LSTM layer
        model.add(layers.LSTM(
            units=lstm_units[2],
            return_sequences=False  # Last LSTM layer
        ))
        model.add(layers.Dropout(dropout_rate))
        
        # Dense output layer
        model.add(layers.Dense(units=25))
        model.add(layers.Dense(units=1))  # Single output: predicted price
        
        # Compile model
        model.compile(
            optimizer='adam',
            loss='mean_squared_error',
            metrics=['mean_absolute_error']
        )
        
        self.model = model
        
        logger.info("Model architecture:")
        model.summary()
        
        return model
    
    def get_callbacks(self, metal: str, patience: int = 10) -> list:
        """
        Get training callbacks
        
        Args:
            metal: 'gold' or 'silver'
            patience: Epochs to wait before early stopping
            
        Returns:
            List of Keras callbacks
        """
        callback_list = [
            # Early stopping: stop if validation loss doesn't improve
            callbacks.EarlyStopping(
                monitor='val_loss',
                patience=patience,
                restore_best_weights=True,
                verbose=1
            ),
            
            # Reduce learning rate when validation loss plateaus
            callbacks.ReduceLROnPlateau(
                monitor='val_loss',
                factor=0.5,
                patience=5,
                min_lr=0.00001,
                verbose=1
            ),
            
            # Save best model
            callbacks.ModelCheckpoint(
                filepath=f'models/{metal.lower()}_model_best.h5',
                monitor='val_loss',
                save_best_only=True,
                verbose=1
            )
        ]
        
        return callback_list
    
    def train(self, X_train: np.ndarray, y_train: np.ndarray,
             X_val: np.ndarray, y_val: np.ndarray,
             metal: str, epochs: int = 100, batch_size: int = 32) -> keras.callbacks.History:
        """
        Train the LSTM model
        
        Args:
            X_train: Training sequences
            y_train: Training targets
            X_val: Validation sequences
            y_val: Validation targets
            metal: 'gold' or 'silver'
            epochs: Maximum number of training epochs
            batch_size: Batch size for training
            
        Returns:
            Training history
        """
        if self.model is None:
            raise ValueError("Model not built. Call build_model() first.")
        
        logger.info(f"Training {metal} model...")
        logger.info(f"Training samples: {len(X_train)}, Validation samples: {len(X_val)}")
        
        history = self.model.fit(
            X_train, y_train,
            validation_data=(X_val, y_val),
            epochs=epochs,
            batch_size=batch_size,
            callbacks=self.get_callbacks(metal),
            verbose=1
        )
        
        logger.info("Training completed!")
        
        return history
    
    def evaluate(self, X_test: np.ndarray, y_test: np.ndarray) -> dict:
        """
        Evaluate model performance
        
        Args:
            X_test: Test sequences
            y_test: Test targets
            
        Returns:
            Dictionary with evaluation metrics
        """
        if self.model is None:
            raise ValueError("Model not built or trained.")
        
        loss, mae = self.model.evaluate(X_test, y_test, verbose=0)
        
        # Make predictions
        predictions = self.model.predict(X_test, verbose=0)
        
        # Calculate additional metrics
        mse = np.mean((predictions.flatten() - y_test) ** 2)
        rmse = np.sqrt(mse)
        
        metrics = {
            'loss': float(loss),
            'mae': float(mae),
            'mse': float(mse),
            'rmse': float(rmse)
        }
        
        logger.info(f"Evaluation metrics: {metrics}")
        
        return metrics
    
    def save_model(self, metal: str, output_dir: str = "models") -> None:
        """
        Save trained model
        
        Args:
            metal: 'gold' or 'silver'
            output_dir: Directory to save model
        """
        if self.model is None:
            raise ValueError("No model to save.")
        
        from pathlib import Path
        output_path = Path(output_dir) / f"{metal.lower()}_model.h5"
        output_path.parent.mkdir(parents=True, exist_ok=True)
        
        self.model.save(output_path)
        logger.info(f"Model saved to {output_path}")
    
    def load_model(self, metal: str, model_dir: str = "models") -> None:
        """
        Load a trained model
        
        Args:
            metal: 'gold' or 'silver'
            model_dir: Directory containing model
        """
        from pathlib import Path
        model_path = Path(model_dir) / f"{metal.lower()}_model.h5"
        
        if not model_path.exists():
            raise FileNotFoundError(f"Model not found: {model_path}")
        
        self.model = models.load_model(model_path)
        logger.info(f"Model loaded from {model_path}")


if __name__ == "__main__":
    # Example usage
    
    # Build model
    builder = LSTMModelBuilder(sequence_length=60, n_features=5)
    model = builder.build_model(lstm_units=[50, 50, 50], dropout_rate=0.2)
    
    print("\nModel Input Shape:", model.input_shape)
    print("Model Output Shape:", model.output_shape)
    print("Total Parameters:", model.count_params())
