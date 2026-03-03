"""
API Startup Script
Checks prerequisites and starts the Flask API server
"""

import sys
import os
from pathlib import Path
import subprocess


def check_python_version():
    """Check if Python version is compatible"""
    print("Checking Python version...")
    version = sys.version_info
    
    if version.major < 3 or (version.major == 3 and version.minor < 8):
        print(f"❌ Python 3.8+ required. Current version: {version.major}.{version.minor}")
        return False
    
    print(f"✓ Python {version.major}.{version.minor}.{version.micro}")
    return True


def check_dependencies():
    """Check if required packages are installed"""
    print("\nChecking dependencies...")
    
    required_packages = [
        'flask',
        'flask_cors',
        'tensorflow',
        'numpy',
        'pandas',
        'sklearn'
    ]
    
    missing = []
    
    for package in required_packages:
        try:
            __import__(package)
            print(f"✓ {package}")
        except ImportError:
            print(f"❌ {package} - NOT INSTALLED")
            missing.append(package)
    
    if missing:
        print(f"\n❌ Missing packages: {', '.join(missing)}")
        print("\nInstall with: pip install -r requirements.txt")
        return False
    
    return True


def check_models():
    """Check if trained models exist"""
    print("\nChecking trained models...")
    
    models_dir = Path('models')
    required_files = [
        'gold_model.h5',
        'silver_model.h5',
        'gold_scaler.pkl',
        'silver_scaler.pkl'
    ]
    
    missing = []
    
    for file in required_files:
        file_path = models_dir / file
        if file_path.exists():
            print(f"✓ {file}")
        else:
            print(f"⚠ {file} - NOT FOUND")
            missing.append(file)
    
    if missing:
        print("\n⚠ Some models are missing. Train them with:")
        print("  python train.py --metal both --epochs 50")
        print("\nAPI will start but predictions may fail for missing models.")
        
        response = input("\nContinue anyway? (y/n): ")
        return response.lower() == 'y'
    
    return True


def check_data():
    """Check if data files exist"""
    print("\nChecking data files...")
    
    data_dir = Path('data')
    required_files = [
        'gold_prices.csv',
        'silver_prices.csv'
    ]
    
    missing = []
    
    for file in required_files:
        file_path = data_dir / file
        if file_path.exists():
            print(f"✓ {file}")
        else:
            print(f"⚠ {file} - NOT FOUND")
            missing.append(file)
    
    if missing:
        print("\n⚠ Data files missing. Generate them with:")
        print("  python generate_data.py")
        
        response = input("\nContinue anyway? (y/n): ")
        return response.lower() == 'y'
    
    return True


def start_api():
    """Start the Flask API server"""
    print("\n" + "="*70)
    print("  STARTING FLASK API SERVER")
    print("="*70)
    print("\nAPI will be available at: http://localhost:5000")
    print("Press CTRL+C to stop the server\n")
    print("="*70 + "\n")
    
    try:
        # Run api.py
        subprocess.run([sys.executable, 'api.py'])
    except KeyboardInterrupt:
        print("\n\n" + "="*70)
        print("  API SERVER STOPPED")
        print("="*70)
    except Exception as e:
        print(f"\n❌ Error starting API: {e}")


def main():
    """Main startup function"""
    print("="*70)
    print("  FLASK API STARTUP CHECKER")
    print("="*70)
    
    # Run checks
    checks = [
        ("Python Version", check_python_version),
        ("Dependencies", check_dependencies),
        ("Trained Models", check_models),
        ("Data Files", check_data)
    ]
    
    all_passed = True
    
    for check_name, check_func in checks:
        if not check_func():
            all_passed = False
            break
    
    if all_passed:
        print("\n" + "="*70)
        print("✓ All checks passed!")
        print("="*70)
        start_api()
    else:
        print("\n" + "="*70)
        print("❌ Startup checks failed. Please fix the issues above.")
        print("="*70)
        sys.exit(1)


if __name__ == "__main__":
    main()
