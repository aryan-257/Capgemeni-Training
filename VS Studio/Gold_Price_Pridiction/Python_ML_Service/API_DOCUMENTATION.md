# Flask API Documentation

Complete API reference for the Gold & Silver Price Prediction service.

## 🚀 Quick Start

### Start the API Server

```bash
# Option 1: Direct start
python api.py

# Option 2: With startup checks
python start_api.py
```

The API will be available at: `http://localhost:5000`

---

## 📡 API Endpoints

### 1. Home / Status

**GET /**

Returns API information and available endpoints.

**Response:**
```json
{
  "service": "Gold & Silver Price Prediction API",
  "version": "1.0.0",
  "status": "running",
  "models_loaded": ["gold", "silver"],
  "endpoints": {
    "predict": "POST /predict",
    "health": "GET /health",
    "models": "GET /models"
  }
}
```

---

### 2. Health Check

**GET /health**

Check if the API is running and models are loaded.

**Response:**
```json
{
  "status": "healthy",
  "models_loaded": 2,
  "available_metals": ["gold", "silver"],
  "timestamp": "2024-12-14T10:30:00Z"
}
```

---

### 3. Models Information

**GET /models**

Get detailed information about loaded models.

**Response:**
```json
{
  "models": {
    "gold": {
      "metal": "gold",
      "sequence_length": 60,
      "model_parameters": 15000,
      "input_shape": "(None, 60, 5)",
      "output_shape": "(None, 1)"
    },
    "silver": {
      "metal": "silver",
      "sequence_length": 60,
      "model_parameters": 15000,
      "input_shape": "(None, 60, 5)",
      "output_shape": "(None, 1)"
    }
  },
  "total_models": 2
}
```

---

### 4. Single Prediction

**POST /predict**

Predict price for a specific metal and date.

**Request Headers:**
```
Content-Type: application/json
```

**Request Body:**
```json
{
  "metal": "Gold",
  "predictionDate": "2024-12-15",
  "daysAhead": 1
}
```

**Parameters:**
- `metal` (string, required): "Gold" or "Silver"
- `predictionDate` (string, optional): Date in YYYY-MM-DD format
- `daysAhead` (integer, optional): Number of days ahead (1-30), default: 1

**Success Response (200):**
```json
{
  "predictedPrice": 2150.75,
  "metal": "Gold",
  "predictionDate": "2024-12-15",
  "confidence": 0.92,
  "timestamp": "2024-12-14T10:30:00Z",
  "success": true,
  "errorMessage": null
}
```

**Error Response (400/404/500):**
```json
{
  "success": false,
  "errorMessage": "Model not available for metal: Platinum"
}
```

---

### 5. Batch Prediction

**POST /predict/batch**

Predict prices for multiple days ahead.

**Request Body:**
```json
{
  "metal": "Gold",
  "daysAhead": 7
}
```

**Parameters:**
- `metal` (string, required): "Gold" or "Silver"
- `daysAhead` (integer, required): Number of days (1-30)

**Success Response (200):**
```json
{
  "predictions": [
    {
      "predictedPrice": 2150.75,
      "metal": "Gold",
      "predictionDate": "2024-12-15",
      "confidence": 0.92,
      "daysAhead": 1,
      "timestamp": "2024-12-14T10:30:00Z",
      "success": true
    },
    {
      "predictedPrice": 2155.30,
      "metal": "Gold",
      "predictionDate": "2024-12-16",
      "confidence": 0.90,
      "daysAhead": 2,
      "timestamp": "2024-12-14T10:30:00Z",
      "success": true
    }
  ],
  "total": 7,
  "success": true
}
```

---

## 🔧 Usage Examples

### cURL Examples

**Health Check:**
```bash
curl http://localhost:5000/health
```

**Single Prediction:**
```bash
curl -X POST http://localhost:5000/predict \
  -H "Content-Type: application/json" \
  -d '{
    "metal": "Gold",
    "predictionDate": "2024-12-15",
    "daysAhead": 1
  }'
```

**Batch Prediction:**
```bash
curl -X POST http://localhost:5000/predict/batch \
  -H "Content-Type: application/json" \
  -d '{
    "metal": "Silver",
    "daysAhead": 7
  }'
```

---

### Python Examples

```python
import requests
import json

API_URL = "http://localhost:5000"

# Single prediction
response = requests.post(
    f"{API_URL}/predict",
    json={
        "metal": "Gold",
        "predictionDate": "2024-12-15",
        "daysAhead": 1
    }
)

result = response.json()
print(f"Predicted Price: ${result['predictedPrice']:.2f}")
print(f"Confidence: {result['confidence']:.2%}")
```

---

### C# (ASP.NET) Example

```csharp
using System.Net.Http;
using System.Text;
using System.Text.Json;

var client = new HttpClient();
var apiUrl = "http://localhost:5000/predict";

var requestData = new
{
    metal = "Gold",
    predictionDate = "2024-12-15",
    daysAhead = 1
};

var json = JsonSerializer.Serialize(requestData);
var content = new StringContent(json, Encoding.UTF8, "application/json");

var response = await client.PostAsync(apiUrl, content);
var responseContent = await response.Content.ReadAsStringAsync();

var result = JsonSerializer.Deserialize<PredictionResponse>(responseContent);
Console.WriteLine($"Predicted Price: ${result.PredictedPrice:F2}");
```

---

## ⚠️ Error Codes

| Status Code | Description |
|-------------|-------------|
| 200 | Success |
| 400 | Bad Request (invalid parameters) |
| 404 | Not Found (model not available) |
| 500 | Internal Server Error |

---

## 🔒 CORS Configuration

The API has CORS enabled to accept requests from any origin. This allows your ASP.NET MVC application to call the API from a different port.

**Allowed:**
- All origins (*)
- All methods (GET, POST, etc.)
- All headers

**For production**, restrict CORS to specific origins:

```python
from flask_cors import CORS

CORS(app, origins=["http://localhost:5001", "https://yourdomain.com"])
```

---

## 📊 Response Fields

### Prediction Response

| Field | Type | Description |
|-------|------|-------------|
| predictedPrice | number | Predicted price in USD |
| metal | string | Metal type (Gold/Silver) |
| predictionDate | string | Date of prediction (YYYY-MM-DD) |
| confidence | number | Confidence score (0.0-1.0) |
| timestamp | string | When prediction was made (ISO 8601) |
| success | boolean | Whether prediction succeeded |
| errorMessage | string/null | Error message if failed |

### Confidence Score Interpretation

| Range | Meaning | Color |
|-------|---------|-------|
| 0.8 - 1.0 | High confidence | Green |
| 0.6 - 0.8 | Moderate confidence | Yellow |
| 0.5 - 0.6 | Low confidence | Red |

---

## 🚦 Rate Limiting

Currently, there is no rate limiting. For production:

```python
from flask_limiter import Limiter

limiter = Limiter(app, default_limits=["100 per hour"])

@app.route('/predict', methods=['POST'])
@limiter.limit("10 per minute")
def predict():
    # ...
```

---

## 🔍 Logging

The API logs all requests and errors. Logs include:
- Request parameters
- Prediction results
- Error messages with stack traces

**Log Format:**
```
2024-12-14 10:30:00 - api - INFO - Prediction request: metal=gold, date=2024-12-15
2024-12-14 10:30:01 - api - INFO - Prediction: $2150.75 (confidence: 92.00%)
```

---

## 🧪 Testing

### Run API Tests

```bash
# Start API in one terminal
python api.py

# Run tests in another terminal
python test_api.py
```

### Manual Testing with Postman

1. Import the following collection:
   - Base URL: `http://localhost:5000`
   - Endpoints: /, /health, /models, /predict, /predict/batch

2. Test each endpoint with sample data

---

## 🐛 Troubleshooting

### API won't start

**Issue:** `ModuleNotFoundError: No module named 'flask'`

**Solution:**
```bash
pip install -r requirements.txt
```

---

### Model not found error

**Issue:** `Model not available for metal: gold`

**Solution:**
```bash
python train.py --metal gold --epochs 50
```

---

### CORS error in browser

**Issue:** `Access-Control-Allow-Origin` error

**Solution:** CORS is already enabled. Check if API is running on correct port (5000).

---

### Slow predictions

**Issue:** Predictions take > 1 second

**Solution:**
- Models are loaded once at startup (fast)
- First prediction may be slower (model initialization)
- Subsequent predictions should be < 100ms

---

## 📈 Performance

**Expected Performance:**
- Startup time: 5-10 seconds (loading models)
- Prediction time: 50-100ms per request
- Throughput: 10-20 requests/second
- Memory usage: ~500MB (models loaded in RAM)

---

## 🔐 Security Considerations

For production deployment:

1. **Add Authentication:**
   ```python
   from flask_httpauth import HTTPBasicAuth
   auth = HTTPBasicAuth()
   
   @app.route('/predict', methods=['POST'])
   @auth.login_required
   def predict():
       # ...
   ```

2. **Use HTTPS:**
   - Deploy behind nginx/Apache with SSL
   - Use Let's Encrypt for free certificates

3. **Input Validation:**
   - Already implemented for metal type and days ahead
   - Add additional validation as needed

4. **Rate Limiting:**
   - Implement to prevent abuse
   - Use Redis for distributed rate limiting

---

## 📦 Deployment

### Local Development
```bash
python api.py
```

### Production (Gunicorn)
```bash
pip install gunicorn
gunicorn -w 4 -b 0.0.0.0:5000 api:app
```

### Docker
```dockerfile
FROM python:3.9
WORKDIR /app
COPY requirements.txt .
RUN pip install -r requirements.txt
COPY . .
CMD ["python", "api.py"]
```

---

## 🆘 Support

For issues or questions:
1. Check logs for error messages
2. Verify models are trained
3. Test with `test_api.py`
4. Review this documentation

---

**API Version:** 1.0.0  
**Last Updated:** December 2024  
**Maintained by:** Gold Price Prediction Team
