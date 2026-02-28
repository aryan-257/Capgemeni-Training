using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FLIGHT_SEARCH_ENGINE.Models
{
    /// <summary>
    /// ViewModel for handling search form data and dropdown lists
    /// </summary>
    public class SearchViewModel
    {
        [Required(ErrorMessage = "Please select a source location")]
        [Display(Name = "Source Location")]
        public string Source { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please select a destination location")]
        [Display(Name = "Destination Location")]
        public string Destination { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please enter number of persons")]
        [Range(1, 10, ErrorMessage = "Number of persons must be between 1 and 10")]
        [Display(Name = "Number of Persons")]
        public int NumberOfPersons { get; set; }

        // Dropdown data for sources
        public SelectList? SourceList { get; set; }

        // Dropdown data for destinations
        public SelectList? DestinationList { get; set; }
    }
}
