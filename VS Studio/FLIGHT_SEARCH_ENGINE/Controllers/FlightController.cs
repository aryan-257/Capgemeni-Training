using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using FLIGHT_SEARCH_ENGINE.Data;
using FLIGHT_SEARCH_ENGINE.Models;

namespace FLIGHT_SEARCH_ENGINE.Controllers
{
    public class FlightController : Controller
    {
        private readonly DatabaseHelper _dbHelper;

        public FlightController(IConfiguration configuration)
        {
            _dbHelper = new DatabaseHelper(configuration);
        }

        // Display search form with dropdowns
        public async Task<IActionResult> Index()
        {
            var model = new SearchViewModel();

            try
            {
                var sources = await _dbHelper.GetSourcesAsync();
                var destinations = await _dbHelper.GetDestinationsAsync();

                model.SourceList = new SelectList(sources);
                model.DestinationList = new SelectList(destinations);
            }
            catch (Exception ex)
            {
                ViewBag.Error = $"Error loading data: {ex.Message}";
            }

            return View(model);
        }

        // Simple test page without JavaScript
        public IActionResult Test()
        {
            return View();
        }

        // Handle flight-only search
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SearchFlights(SearchViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var sources = await _dbHelper.GetSourcesAsync();
                var destinations = await _dbHelper.GetDestinationsAsync();
                model.SourceList = new SelectList(sources);
                model.DestinationList = new SelectList(destinations);
                return View("Index", model);
            }

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
                var results = await _dbHelper.SearchFlightsAsync(model.Source, model.Destination, model.NumberOfPersons);

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

        // Handle flight + hotel package search
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SearchFlightsWithHotels(SearchViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var sources = await _dbHelper.GetSourcesAsync();
                var destinations = await _dbHelper.GetDestinationsAsync();
                model.SourceList = new SelectList(sources);
                model.DestinationList = new SelectList(destinations);
                return View("Index", model);
            }

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
                var results = await _dbHelper.SearchFlightsWithHotelsAsync(model.Source, model.Destination, model.NumberOfPersons);

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
