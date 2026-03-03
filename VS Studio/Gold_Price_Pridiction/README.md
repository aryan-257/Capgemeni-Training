# Gold & Silver Price Prediction System

AI-powered web application for predicting gold and silver prices using ASP.NET Core MVC and Python Machine Learning.

## 🎯 Project Overview

Full-stack application that combines:
- **Frontend**: ASP.NET Core MVC with Bootstrap 5
- **Backend**: Python Flask REST API
- **AI/ML**: LSTM Neural Networks (Demo mode available)
- **Visualization**: Chart.js for interactive charts

## 🚀 Quick Start

### Prerequisites
- .NET 10.0 SDK
- Python 3.14+
- Modern web browser

### Running the Application

1. **Start Python API** (Demo Mode):
```powershell
cd Python_ML_Service
python api_demo.py
```
   The API will start on `http://localhost:5000`

2. **Start ASP.NET Application** (in a new terminal):
```powershell
dotnet run
```
   The application will start on `http://localhost:5246`

3. **Access Application**:
   Open your browser and go to: `http://localhost:5246`

### Testing the Application

1. Navigate to the Prediction page
2. Select a metal (Gold or Silver)
3. Choose a prediction date
4. Click "Predict Price"
5. View the predicted price with confidence score
6. Check the Dashboard for visualizations

## 📁 Project Structure

```
Gold_Price_Pridiction/
├── Controllers/              # MVC Controllers
│   ├── PredictionController.cs
│   ├── ErrorController.cs
│   └── HomeController.cs
├── Models/                   # Data Models
│   ├── PredictionRequest.cs
│   └── PredictionResponse.cs
├── Views/                    # Razor Views
│   ├── Prediction/
│   │   ├── Index.cshtml     # Prediction form
│   │   ├── Result.cshtml    # Results page
│   │   └── Dashboard.cshtml # Analytics
│   └── Shared/
│       ├── _Layout.cshtml
│       ├── Error404.cshtml
│       └── Error500.cshtml
├── Python_ML_Service/        # Python ML API
│   ├── api_demo.py          # Demo API (no TensorFlow)
│   ├── api.py               # Full ML API
│   ├── src/                 # ML modules
│   └── requirements.txt
├── wwwroot/                  # Static files
│   ├── css/
│   ├── js/
│   └── lib/
└── Properties/
    └── launchSettings.json
```

## 🎓 Features

- Real-time price predictions for Gold and Silver
- Interactive dashboard with Chart.js visualizations
- Responsive Bootstrap 5 UI
- RESTful API architecture
- Custom error pages (404, 500)
- CORS enabled for API communication
- Demo mode for quick testing

## 📚 Documentation

- `PROJECT_README.md` - Complete technical documentation and architecture details

## 🔧 Configuration

### ASP.NET Configuration (`appsettings.json`):
```json
{
  "PythonApi": {
    "BaseUrl": "http://localhost:5000",
    "TimeoutSeconds": 30
  }
}
```

### Python Dependencies:
See `Python_ML_Service/requirements.txt`

## 🎯 Demo Mode vs Full ML Mode

### Current: Demo Mode
- Uses simulated predictions
- No TensorFlow required
- Instant startup
- Perfect for testing and presentations

### Upgrade to Full ML:
1. Install TensorFlow: `pip install tensorflow keras`
2. Generate data: `python generate_data.py`
3. Train models: `python train.py --metal both --epochs 50`
4. Use `api.py` instead of `api_demo.py`

## 🛠️ Technologies Used

- ASP.NET Core MVC 10.0
- Python 3.14.3
- Flask (REST API)
- Bootstrap 5
- Chart.js
- jQuery
- LSTM Neural Networks (optional)

## 📊 API Endpoints

### Python API (Port 5000):
- `GET /` - API info
- `GET /health` - Health check
- `GET /models` - Model information
- `POST /predict` - Single prediction
- `POST /predict/batch` - Batch predictions

### ASP.NET MVC (Port 5246):
- `/` - Home page
- `/Prediction` - Prediction form
- `/Prediction/Result` - Results page
- `/Prediction/Dashboard` - Analytics dashboard

## 👨‍💻 Development

### Build Project:
```powershell
dotnet build
```

### Run Tests:
```powershell
dotnet test
```

### Check Diagnostics:
Use IDE diagnostics or `dotnet build` for errors

## 📝 License

Academic project for final year submission.

## 👥 Author

Capgemini Training Project

---

**Status**: ✅ Fully Operational
**Last Updated**: March 2, 2026
