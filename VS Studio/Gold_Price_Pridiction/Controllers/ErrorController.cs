using Microsoft.AspNetCore.Mvc;

namespace Gold_Price_Pridiction.Controllers
{
    /// <summary>
    /// Handles HTTP error status codes with custom error pages
    /// </summary>
    public class ErrorController : Controller
    {
        private readonly ILogger<ErrorController> _logger;

        public ErrorController(ILogger<ErrorController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Handles status code errors (404, 500, etc.)
        /// </summary>
        [Route("Error/{statusCode}")]
        public IActionResult HttpStatusCodeHandler(int statusCode)
        {
            _logger.LogWarning($"HTTP Status Code: {statusCode}");

            switch (statusCode)
            {
                case 404:
                    ViewBag.ErrorMessage = "Page Not Found";
                    return View("Error404");

                case 500:
                    ViewBag.ErrorMessage = "Internal Server Error";
                    return View("Error500");

                default:
                    ViewBag.ErrorMessage = $"Error {statusCode}";
                    return View("Error");
            }
        }
    }
}
