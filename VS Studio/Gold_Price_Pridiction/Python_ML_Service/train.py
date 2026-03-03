"""
Main Training Script
Run this to train models for Gold and Silver
"""

import sys
import argparse
from pathlib import Path

# Add src to path
sys.path.insert(0, str(Path(__file__).parent / 'src'))

from data_loader import download_sample_data
from trainer import ModelTrainer
import logging

logging.basicConfig(
    level=logging.INFO,
    format='%(asctime)s - %(name)s - %(levelname)s - %(message)s'
)
logger = logging.getLogger(__name__)


def main():
    """Main training function"""
    
    parser = argparse.ArgumentParser(
        description='Train LSTM model for Gold/Silver price prediction'
    )
    parser.add_argument(
        '--metal',
        type=str,
        required=True,
        choices=['gold', 'silver', 'both'],
        help='Metal type to train model for'
    )
    parser.add_argument(
        '--epochs',
        type=int,
        default=100,
        help='Maximum number of training epochs (default: 100)'
    )
    parser.add_argument(
        '--batch-size',
        type=int,
        default=32,
        help='Training batch size (default: 32)'
    )
    parser.add_argument(
        '--sequence-length',
        type=int,
        default=60,
        help='Number of past days to use for prediction (default: 60)'
    )
    parser.add_argument(
        '--generate-sample-data',
        action='store_true',
        help='Generate sample data if datasets are missing'
    )
    
    args = parser.parse_args()
    
    # Generate sample data if requested
    if args.generate_sample_data:
        logger.info("Generating sample datasets...")
        download_sample_data('gold')
        download_sample_data('silver')
    
    # Determine which metals to train
    metals = ['gold', 'silver'] if args.metal == 'both' else [args.metal]
    
    # Train models
    results = {}
    for metal in metals:
        logger.info(f"\n{'#'*70}")
        logger.info(f"# Training {metal.upper()} Model")
        logger.info(f"{'#'*70}\n")
        
        try:
            trainer = ModelTrainer(
                metal=metal,
                sequence_length=args.sequence_length
            )
            
            result = trainer.train_complete_pipeline(
                epochs=args.epochs,
                batch_size=args.batch_size
            )
            
            results[metal] = result
            
        except Exception as e:
            logger.error(f"Failed to train {metal} model: {e}")
            continue
    
    # Print summary
    print("\n" + "="*70)
    print("TRAINING SUMMARY")
    print("="*70)
    
    for metal, result in results.items():
        print(f"\n{metal.upper()}:")
        print(f"  Loss (MSE): {result['metrics']['loss']:.6f}")
        print(f"  MAE: {result['metrics']['mae']:.6f}")
        print(f"  RMSE: {result['metrics']['rmse']:.6f}")
    
    print("\n" + "="*70)
    print("Training completed! Models saved in 'models/' directory")
    print("="*70)


if __name__ == "__main__":
    main()
