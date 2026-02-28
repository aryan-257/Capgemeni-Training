using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using FLIGHT_SEARCH_ENGINE.Data;
using FLIGHT_SEARCH_ENGINE.Models;

namespace FLIGHT_SEARCH_ENGINE.Controllers
{
    /// <summary>
    /// Controller for handling flight search operations
    /// </summary>
    public class FlightController : Controller
    {
        private readonly DatabaseHelper _dbHelper;

        /// <summary>
        /// Constructor to initialize DatabaseHelper
        /// </summary>
        /// <param name="configuration">IConfiguration for database connection</param>
        public FlightController(IConfiguration configuration)
        {
            _dbHelper = new DatabaseHelper(configuration);
        }

        /// <summary>
        /// Display search form with populated dropdowns
        /// </summary>
        /// <returns>View with SearchViewModel</returns>
        public async Task<IActionResult> Index()
        {
            var model = new SearchViewModel();

            try
            {
                // Get sources and destinations from database
                var sources = await _dbHelper.GetSourcesAsync();
                var destinations = await _dbHelper.GetDestinationsAsync();

                // Create SelectList for dropdowns
                model.SourceList = new SelectList(sources);
                model.DestinationList = new SelectList(destinations);
            }
            catch (Exception ex)
            {
                ViewBag.Error = $"Error loading data: {ex.Message}";
            }

            return View(model);
        }

        /// <summary>
        /// Test page without JavaScript
        /// </summary>
        public IActionResult Test()
        {
            return View();
        }

        /// <summary>
        /// Process flight-only search and display results
        /// </summary>
        /// <param name="model">SearchViewModel with user input</param>
        /// <returns>View with FlightResult list</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SearchFlights(SearchViewModel model)
        {
            if (!ModelState.IsValid)
            {
                // Reload dropdowns if validation fails
                var sources = await _dbHelper.GetSourcesAsync();
                var destinations = await _dbHelper.GetDestinationsAsync();
                model.SourceList = new SelectList(sources);
                model.DestinationList = new SelectList(destinations);
                return View("Index", model);
            }

            // Validate source and destination are different
            if (model.Source == model.Destination)
            {
                ModelState.AddModelError("", "Source and Destination cannot be the same");
                var sources = await _dbHelper.GetSourcesAsync();
                var destinations = await _dbHelper.GetDestinationsAsync();
                model.SourceList = new SelectList(sources);
                model.DestinationList = new SelectList(destinations);
                return View("Index", model);
            }

            try
            {
                // Search for flights
                var results = await _dbHelper.SearchFlightsAsync(model.Source, model.Destination, model.NumberOfPersons);

                // Store search criteria in ViewBag
                ViewBag.SearchType = "Flights Only";
                ViewBag.Source = model.Source;
                ViewBag.Destination = model.Destination;
                ViewBag.Persons = model.NumberOfPersons;

                return View("Results", results);
            }
            catch (Exception ex)
            {
                ViewBag.Error = $"Error searching flights: {ex.Message}";
                var sources = await _dbHelper.GetSourcesAsync();
                var destinations = await _dbHelper.GetDestinationsAsync();
                model.SourceList = new SelectList(sources);
                model.DestinationList = new SelectList(destinations);
                return View("Index", model);
            }
        }

        /// <summary>
        /// Process flight+hotel search and display results
        /// </summary>
        /// <param name="model">SearchViewModel with user input</param>
        /// <returns>View with FlightHotelResult list</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SearchFlightsWithHotels(SearchViewModel model)
        {
            if (!ModelState.IsValid)
            {
                // Reload dropdowns if validation fails
                var sources = await _dbHelper.GetSourcesAsync();
                var destinations = await _dbHelper.GetDestinationsAsync();
                model.SourceList = new SelectList(sources);
                model.DestinationList = new SelectList(destinations);
                return View("Index", model);
            }

            // Validate source and destination are different
            if (model.Source == model.Destination)
            {
                ModelState.AddModelError("", "Source and Destination cannot be the same");
                var sources = await _dbHelper.GetSourcesAsync();
                var destinations = await _dbHelper.GetDestinationsAsync();
                model.SourceList = new SelectList(sources);
                model.DestinationList = new SelectList(destinations);
                return View("Index", model);
            }

            try
            {
                // Search for flight+hotel packages
                var results = await _dbHelper.SearchFlightsWithHotelsAsync(model.Source, model.Destination, model.NumberOfPersons);

                // Store search criteria in ViewBag
                ViewBag.SearchType = "Flight + Hotel Package";
                ViewBag.Source = model.Source;
                ViewBag.Destination = model.Destination;
                ViewBag.Persons = model.NumberOfPersons;

                return View("Results", results);
            }
            catch (Exception ex)
            {
                ViewBag.Error = $"Error searching packages: {ex.Message}";
                var sources = await _dbHelper.GetSourcesAsync();
                var destinations = await _dbHelper.GetDestinationsAsync();
                model.SourceList = new SelectList(sources);
                model.DestinationList = new SelectList(destinations);
                return View("Index", model);
            }
        }
    }
}
