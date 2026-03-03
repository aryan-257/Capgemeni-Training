using Gold_Price_Pridiction.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;

namespace Gold_Price_Pridiction.Controllers
{
    /// <summary>
    /// Handles gold and silver price prediction requests
    /// Communicates with Python ML API for predictions
    /// </summary>
    public class PredictionController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private readonly ILogger<PredictionController> _logger;

        // Constructor: Dependency Injection
        // ASP.NET automatically provides these services
        public PredictionController(
            IHttpClientFactory httpClientFactory,
            IConfiguration configuration,
            ILogger<PredictionController> logger)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
            _logger = logger;
        }

        /// <summary>
        /// GET: /Prediction/Index
        /// Displays the prediction input form
        /// </summary>
        [HttpGet]
        public IActionResult Index()
        {
            // Create empty model with default values
            var model = new PredictionRequest
            {
                PredictionDate = DateTime.Today.AddDays(1), // Default to tomorrow
                DaysAhead = 1
            };

            return View(model);
        }

        /// <summary>
        /// POST: /Prediction/Index
        /// Processes the prediction request and calls Python API
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken] // Security: Prevents CSRF attacks
        public async Task<IActionResult> Index(PredictionRequest request)
        {
            // Step 1: Validate the incoming data
            if (!ModelState.IsValid)
            {
                // Return form with validation errors
                return View(request);
            }

            // Calculate DaysAhead from PredictionDate
            var today = DateTime.Today;
            var daysAhead = (request.PredictionDate.Date - today).Days;
            
            // Validate date range (1-30 days)
            if (daysAhead < 1)
            {
                ModelState.AddModelError(nameof(request.PredictionDate), 
                    "Prediction date must be in the future.");
                return View(request);
            }
            
            if (daysAhead > 30)
            {
                ModelState.AddModelError(nameof(request.PredictionDate), 
                    "Prediction date cannot be more than 30 days in the future.");
                return View(request);
            }
            
            // Set the calculated DaysAhead
            request.DaysAhead = daysAhead;

            try
            {
                // Step 2: Call Python API
                var response = await CallPredictionApiAsync(request);

                // Step 3: Check if prediction was successful
                if (response.Success)
                {
                    // Store response in TempData to pass to Result page
                    // TempData survives one redirect
                    TempData["PredictionResult"] = JsonSerializer.Serialize(response);
                    
                    // Redirect to Result page
                    return RedirectToAction(nameof(Result));
                }
                else
                {
                    // API returned error
                    ModelState.AddModelError(string.Empty, 
                        response.ErrorMessage ?? "Prediction failed. Please try again.");
                    return View(request);
                }
            }
            catch (HttpRequestException ex)
            {
                // Network error: Python API not reachable
                _logger.LogError(ex, "Failed to connect to prediction API");
                ModelState.AddModelError(string.Empty, 
                    "Unable to connect to prediction service. Please ensure the Python API is running.");
                return View(request);
            }
            catch (Exception ex)
            {
                // Unexpected error
                _logger.LogError(ex, "Unexpected error during prediction");
                ModelState.AddModelError(string.Empty, 
                    "An unexpected error occurred. Please try again later.");
                return View(request);
            }
        }

        /// <summary>
        /// GET: /Prediction/Result
        /// Displays the prediction result
        /// </summary>
        [HttpGet]
        public IActionResult Result()
        {
            // Retrieve prediction result from TempData
            var resultJson = TempData["PredictionResult"] as string;

            if (string.IsNullOrEmpty(resultJson))
            {
                // No result available, redirect back to form
                return RedirectToAction(nameof(Index));
            }

            // Deserialize JSON back to PredictionResponse object
            var result = JsonSerializer.Deserialize<PredictionResponse>(resultJson);

            return View(result);
        }

        /// <summary>
        /// GET: /Prediction/Dashboard
        /// Displays the analytics dashboard
        /// </summary>
        [HttpGet]
        public IActionResult Dashboard()
        {
            return View();
        }

        /// <summary>
        /// Calls the Python ML API to get price prediction
        /// </summary>
        private async Task<PredictionResponse> CallPredictionApiAsync(PredictionRequest request)
        {
            // Get API URL from configuration (appsettings.json)
            var apiUrl = _configuration["PythonApi:BaseUrl"] ?? "http://localhost:5000";
            var endpoint = $"{apiUrl}/predict";

            // Create HTTP client
            var httpClient = _httpClientFactory.CreateClient();
            httpClient.Timeout = TimeSpan.FromSeconds(30); // 30 second timeout

            // Prepare request payload
            var requestPayload = new
            {
                metal = request.Metal,
                predictionDate = request.PredictionDate.ToString("yyyy-MM-dd"),
                daysAhead = request.DaysAhead
            };

            // Serialize to JSON
            var jsonContent = JsonSerializer.Serialize(requestPayload);
            var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            // Log the request
            _logger.LogInformation("Calling prediction API: {Endpoint} with payload: {Payload}", 
                endpoint, jsonContent);

            // Send POST request to Python API
            var httpResponse = await httpClient.PostAsync(endpoint, httpContent);

            // Read response content
            var responseContent = await httpResponse.Content.ReadAsStringAsync();

            // Check if request was successful
            if (httpResponse.IsSuccessStatusCode)
            {
                // Parse successful response
                var apiResponse = JsonSerializer.Deserialize<PredictionResponse>(responseContent, 
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                if (apiResponse != null)
                {
                    apiResponse.Success = true;
                    apiResponse.Timestamp = DateTime.UtcNow;
                    
                    // Calculate INR prices for different weights
                    apiResponse.CalculateINRPrices();
                    
                    return apiResponse;
                }
            }

            // API returned error status code
            _logger.LogWarning("Prediction API returned error: {StatusCode} - {Content}", 
                httpResponse.StatusCode, responseContent);

            return new PredictionResponse
            {
                Success = false,
                ErrorMessage = $"API Error: {httpResponse.StatusCode}"
            };
        }
    }
}
