"""
Data Loader Module
Loads and validates historical price datasets for Gold and Silver
"""

import pandas as pd
import numpy as np
from pathlib import Path
from typing import Tuple
import logging

logging.basicConfig(level=logging.INFO)
logger = logging.getLogger(__name__)


class DataLoader:
    """Handles loading and validation of price datasets"""
    
    def __init__(self, data_dir: str = "data"):
        """
        Initialize DataLoader
        
        Args:
            data_dir: Directory containing CSV files
        """
        self.data_dir = Path(data_dir)
        self.required_columns = ['Date', 'Open', 'High', 'Low', 'Close', 'Volume']
    
    def load_data(self, metal: str) -> pd.DataFrame:
        """
        Load price data for specified metal
        
        Args:
            metal: 'gold' or 'silver'
            
        Returns:
            DataFrame with price data
        """
        metal = metal.lower()
        file_path = self.data_dir / f"{metal}_prices.csv"
        
        if not file_path.exists():
            raise FileNotFoundError(f"Dataset not found: {file_path}")
        
        logger.info(f"Loading {metal} data from {file_path}")
        
        # Load CSV
        df = pd.read_csv(file_path)
        
        # Validate columns
        self._validate_columns(df)
        
        # Parse dates
        df['Date'] = pd.to_datetime(df['Date'])
        
        # Sort by date
        df = df.sort_values('Date').reset_index(drop=True)
        
        # Remove duplicates
        df = df.drop_duplicates(subset=['Date'], keep='last')
        
        logger.info(f"Loaded {len(df)} records from {df['Date'].min()} to {df['Date'].max()}")
        
        return df
    
    def _validate_columns(self, df: pd.DataFrame) -> None:
        """
        Validate that DataFrame has required columns
        
        Args:
            df: DataFrame to validate
        """
        missing_cols = set(self.required_columns) - set(df.columns)
        if missing_cols:
            raise ValueError(f"Missing required columns: {missing_cols}")
    
    def get_data_info(self, metal: str) -> dict:
        """
        Get information about the dataset
        
        Args:
            metal: 'gold' or 'silver'
            
        Returns:
            Dictionary with dataset statistics
        """
        df = self.load_data(metal)
        
        return {
            'metal': metal,
            'total_records': len(df),
            'date_range': {
                'start': df['Date'].min().strftime('%Y-%m-%d'),
                'end': df['Date'].max().strftime('%Y-%m-%d')
            },
            'price_stats': {
                'min': float(df['Close'].min()),
                'max': float(df['Close'].max()),
                'mean': float(df['Close'].mean()),
                'std': float(df['Close'].std())
            },
            'missing_values': df.isnull().sum().to_dict()
        }


def download_sample_data(metal: str, output_dir: str = "data") -> None:
    """
    Create sample dataset for testing (when real data is not available)
    
    Args:
        metal: 'gold' or 'silver'
        output_dir: Directory to save CSV file
    """
    logger.info(f"Generating sample {metal} data...")
    
    # Generate 5 years of daily data
    dates = pd.date_range(start='2019-01-01', end='2024-01-01', freq='D')
    
    # Base prices
    base_price = 1800 if metal.lower() == 'gold' else 25
    
    # Generate realistic price movements
    np.random.seed(42)
    returns = np.random.normal(0.0002, 0.015, len(dates))  # Daily returns
    prices = base_price * np.exp(np.cumsum(returns))
    
    # Create OHLC data
    df = pd.DataFrame({
        'Date': dates,
        'Open': prices * (1 + np.random.uniform(-0.01, 0.01, len(dates))),
        'High': prices * (1 + np.random.uniform(0.005, 0.02, len(dates))),
        'Low': prices * (1 + np.random.uniform(-0.02, -0.005, len(dates))),
        'Close': prices,
        'Volume': np.random.randint(100000, 500000, len(dates))
    })
    
    # Ensure High is highest and Low is lowest
    df['High'] = df[['Open', 'High', 'Close']].max(axis=1)
    df['Low'] = df[['Open', 'Low', 'Close']].min(axis=1)
    
    # Save to CSV
    output_path = Path(output_dir) / f"{metal.lower()}_prices.csv"
    output_path.parent.mkdir(parents=True, exist_ok=True)
    df.to_csv(output_path, index=False)
    
    logger.info(f"Sample data saved to {output_path}")
    logger.info(f"Records: {len(df)}, Price range: ${df['Close'].min():.2f} - ${df['Close'].max():.2f}")


if __name__ == "__main__":
    # Example usage
    
    # Generate sample data if needed
    download_sample_data('gold')
    download_sample_data('silver')
    
    # Load and display info
    loader = DataLoader()
    
    for metal in ['gold', 'silver']:
        print(f"\n{'='*50}")
        print(f"{metal.upper()} DATA INFO")
        print('='*50)
        info = loader.get_data_info(metal)
        print(f"Total Records: {info['total_records']}")
        print(f"Date Range: {info['date_range']['start']} to {info['date_range']['end']}")
        print(f"Price Range: ${info['price_stats']['min']:.2f} - ${info['price_stats']['max']:.2f}")
        print(f"Average Price: ${info['price_stats']['mean']:.2f}")
