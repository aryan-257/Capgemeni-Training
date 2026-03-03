"""
Generate Sample Data Script
Creates realistic sample datasets for Gold and Silver
"""

import sys
from pathlib import Path

# Add src to path
sys.path.insert(0, str(Path(__file__).parent / 'src'))

from data_loader import download_sample_data, DataLoader
import logging

logging.basicConfig(level=logging.INFO)
logger = logging.getLogger(__name__)


def main():
    """Generate sample datasets"""
    
    print("="*70)
    print("GENERATING SAMPLE DATASETS")
    print("="*70)
    
    # Generate Gold data
    print("\n[1/2] Generating Gold price data...")
    download_sample_data('gold', output_dir='data')
    
    # Generate Silver data
    print("\n[2/2] Generating Silver price data...")
    download_sample_data('silver', output_dir='data')
    
    # Verify data
    print("\n" + "="*70)
    print("VERIFYING GENERATED DATA")
    print("="*70)
    
    loader = DataLoader(data_dir='data')
    
    for metal in ['gold', 'silver']:
        info = loader.get_data_info(metal)
        print(f"\n{metal.upper()}:")
        print(f"  Total Records: {info['total_records']}")
        print(f"  Date Range: {info['date_range']['start']} to {info['date_range']['end']}")
        print(f"  Price Range: ${info['price_stats']['min']:.2f} - ${info['price_stats']['max']:.2f}")
        print(f"  Average Price: ${info['price_stats']['mean']:.2f}")
    
    print("\n" + "="*70)
    print("✓ Sample data generated successfully!")
    print("  Files created:")
    print("    - data/gold_prices.csv")
    print("    - data/silver_prices.csv")
    print("="*70)
    print("\nNext step: Train the models")
    print("  python train.py --metal both --epochs 50")
    print("="*70)


if __name__ == "__main__":
    main()
