"""
Model Trainer Module
Orchestrates the complete training pipeline
"""

import logging
from pathlib import Path
from data_loader import DataLoader
from preprocessor import DataPreprocessor
from model_builder import LSTMModelBuilder
import matplotlib.pyplot as plt

logging.basicConfig(level=logging.INFO)
logger = logging.getLogger(__name__)


class ModelTrainer:
    """Orchestrates the complete model training pipeline"""
    
    def __init__(self, metal: str, sequence_length: int = 60):
        """
        Initialize trainer
        
        Args:
            metal: 'gold' or 'silver'
            sequence_length: Number of past days for prediction
        """
        self.metal = metal.lower()
        self.sequence_length = sequence_length
        
        self.data_loader = DataLoader()
        self.preprocessor = DataPreprocessor(sequence_length=sequence_length)
        self.model_builder = LSTMModelBuilder(sequence_length=sequence_length, n_features=5)
    
    def train_complete_pipeline(self, epochs: int = 100, batch_size: int = 32) -> dict:
        """
        Execute complete training pipeline
        
        Args:
            epochs: Maximum training epochs
            batch_size: Training batch size
            
        Returns:
            Dictionary with training results
        """
        logger.info(f"{'='*60}")
        logger.info(f"Starting training pipeline for {self.metal.upper()}")
        logger.info(f"{'='*60}")
        
        # Step 1: Load data
        logger.info("\n[1/6] Loading data...")
        df = self.data_loader.load_data(self.metal)
        
        # Step 2: Preprocess data
        logger.info("\n[2/6] Preprocessing data...")
        X, y = self.preprocessor.prepare_data(df)
        
        # Step 3: Split data
        logger.info("\n[3/6] Splitting data...")
        X_train, X_val, y_train, y_val = self.preprocessor.split_data(X, y, train_ratio=0.8)
        
        # Step 4: Build model
        logger.info("\n[4/6] Building LSTM model...")
        self.model_builder.build_model(lstm_units=[50, 50, 50], dropout_rate=0.2)
        
        # Step 5: Train model
        logger.info("\n[5/6] Training model...")
        history = self.model_builder.train(
            X_train, y_train,
            X_val, y_val,
            metal=self.metal,
            epochs=epochs,
            batch_size=batch_size
        )
        
        # Step 6: Evaluate and save
        logger.info("\n[6/6] Evaluating and saving...")
        metrics = self.model_builder.evaluate(X_val, y_val)
        
        # Save model and scaler
        self.model_builder.save_model(self.metal)
        self.preprocessor.save_scaler(self.metal)
        
        # Plot training history
        self._plot_training_history(history)
        
        logger.info(f"\n{'='*60}")
        logger.info(f"Training completed for {self.metal.upper()}!")
        logger.info(f"{'='*60}")
        
        return {
            'metal': self.metal,
            'metrics': metrics,
            'history': history.history
        }
    
    def _plot_training_history(self, history) -> None:
        """
        Plot and save training history
        
        Args:
            history: Keras training history
        """
        plt.figure(figsize=(12, 4))
        
        # Plot loss
        plt.subplot(1, 2, 1)
        plt.plot(history.history['loss'], label='Training Loss')
        plt.plot(history.history['val_loss'], label='Validation Loss')
        plt.title(f'{self.metal.upper()} - Model Loss')
        plt.xlabel('Epoch')
        plt.ylabel('Loss (MSE)')
        plt.legend()
        plt.grid(True)
        
        # Plot MAE
        plt.subplot(1, 2, 2)
        plt.plot(history.history['mean_absolute_error'], label='Training MAE')
        plt.plot(history.history['val_mean_absolute_error'], label='Validation MAE')
        plt.title(f'{self.metal.upper()} - Mean Absolute Error')
        plt.xlabel('Epoch')
        plt.ylabel('MAE')
        plt.legend()
        plt.grid(True)
        
        plt.tight_layout()
        
        # Save plot
        output_path = Path('models') / f'{self.metal}_training_history.png'
        plt.savefig(output_path, dpi=300, bbox_inches='tight')
        logger.info(f"Training history plot saved to {output_path}")
        
        plt.close()


if __name__ == "__main__":
    import argparse
    
    parser = argparse.ArgumentParser(description='Train LSTM model for price prediction')
    parser.add_argument('--metal', type=str, required=True, choices=['gold', 'silver'],
                       help='Metal type to train model for')
    parser.add_argument('--epochs', type=int, default=100,
                       help='Maximum number of training epochs')
    parser.add_argument('--batch-size', type=int, default=32,
                       help='Training batch size')
    parser.add_argument('--sequence-length', type=int, default=60,
                       help='Number of past days to use for prediction')
    
    args = parser.parse_args()
    
    # Train model
    trainer = ModelTrainer(metal=args.metal, sequence_length=args.sequence_length)
    results = trainer.train_complete_pipeline(epochs=args.epochs, batch_size=args.batch_size)
    
    print("\n" + "="*60)
    print("TRAINING RESULTS")
    print("="*60)
    print(f"Metal: {results['metal'].upper()}")
    print(f"Final Loss: {results['metrics']['loss']:.6f}")
    print(f"Final MAE: {results['metrics']['mae']:.6f}")
    print(f"Final RMSE: {results['metrics']['rmse']:.6f}")
    print("="*60)
