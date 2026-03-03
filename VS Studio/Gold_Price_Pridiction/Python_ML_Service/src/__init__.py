"""
Python ML Service - Source Package
Gold and Silver Price Prediction using LSTM Neural Networks
"""

__version__ = "1.0.0"
__author__ = "Gold Price Prediction Team"

from .data_loader import DataLoader, download_sample_data
from .preprocessor import DataPreprocessor
from .model_builder import LSTMModelBuilder
from .trainer import ModelTrainer
from .predictor import PricePredictor

__all__ = [
    'DataLoader',
    'download_sample_data',
    'DataPreprocessor',
    'LSTMModelBuilder',
    'ModelTrainer',
    'PricePredictor'
]
