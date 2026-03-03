using System.ComponentModel.DataAnnotations;

namespace Gold_Price_Pridiction.Models
{
    /// <summary>
    /// Represents the data sent to the Python ML API for price prediction
    /// </summary>
    public class PredictionRequest
    {
        /// <summary>
        /// Type of precious metal (Gold or Silver)
        /// </summary>
        [Required(ErrorMessage = "Please select a metal type")]
        [Display(Name = "Metal Type")]
        public string Metal { get; set; } = string.Empty;

        /// <summary>
        /// Date for which to predict the price
        /// </summary>
        [Required(ErrorMessage = "Please select a date")]
        [Display(Name = "Prediction Date")]
        [DataType(DataType.Date)]
        public DateTime PredictionDate { get; set; }

        /// <summary>
        /// Optional: Number of days to predict ahead (default: 1)
        /// </summary>
        [Display(Name = "Days Ahead")]
        [Range(1, 30, ErrorMessage = "Days ahead must be between 1 and 30")]
        public int DaysAhead { get; set; } = 1;
    }
}
