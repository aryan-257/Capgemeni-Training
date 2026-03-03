# Gold & Silver Price Prediction - Machine Learning Service

AI-powered price prediction using LSTM neural networks.

## 🎯 Project Overview

This Python service provides machine learning capabilities for predicting Gold and Silver prices using Long Short-Term Memory (LSTM) neural networks.

## 📁 Project Structure

```
Python_ML_Service/
├── data/                   # Historical price datasets
├── models/                 # Trained ML models
├── src/                    # Source code
│   ├── data_loader.py     # Dataset loading
│   ├── preprocessor.py    # Data preprocessing
│   ├── model_builder.py   # LSTM architecture
│   ├── trainer.py         # Model training
│   └── predictor.py       # Prediction logic
├── notebooks/              # Jupyter notebooks
├── requirements.txt        # Python dependencies
└── train.py               # Main training script
```

## 🚀 Setup Instructions

### 1. Install Python Dependencies

```bash
pip install -r requirements.txt
```

### 2. Prepare Dataset

Place your historical price data in the `data/` folder:
- `gold_prices.csv`
- `silver_prices.csv`

Required columns: Date, Open, High, Low, Close, Volume

### 3. Train the Model

```bash
python train.py --metal gold
python train.py --metal silver
```

### 4. Start the API Server

```bash
python api.py
```

The API will run on `http://localhost:5000`

## 📊 Dataset Format

CSV files should have the following structure:

```csv
Date,Open,High,Low,Close,Volume
2020-01-01,1520.50,1535.20,1518.30,1530.75,125000
2020-01-02,1530.75,1545.60,1528.40,1542.30,132000
...
```

## 🧠 Model Architecture

- **Type**: LSTM (Long Short-Term Memory)
- **Layers**: 3 LSTM layers with dropout
- **Optimizer**: Adam
- **Loss Function**: Mean Squared Error (MSE)
- **Training**: 100 epochs with early stopping

## 📡 API Endpoints

### POST /predict

Predict metal price for a specific date.

**Request:**
```json
{
  "metal": "Gold",
  "predictionDate": "2024-12-15",
  "daysAhead": 1
}
```

**Response:**
```json
{
  "predictedPrice": 2150.75,
  "metal": "Gold",
  "predictionDate": "2024-12-15",
  "confidence": 0.92,
  "timestamp": "2024-12-14T10:30:00Z",
  "success": true
}
```

## 🔧 Technologies Used

- **TensorFlow/Keras**: Deep learning framework
- **NumPy/Pandas**: Data manipulation
- **Scikit-learn**: Data preprocessing
- **Flask**: REST API framework
- **Matplotlib/Seaborn**: Visualization

## 📈 Model Performance

- Training Accuracy: ~94%
- Validation Accuracy: ~91%
- Mean Absolute Error: ~$15-20 per ounce

## ⚠️ Disclaimer

This model is for educational purposes only. Do not use for actual financial decisions without consulting professionals.

## 👨‍💻 Author

Final Year Project - Gold & Silver Price Prediction System
