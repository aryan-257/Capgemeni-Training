namespace FLIGHT_SEARCH_ENGINE.Models
{
    /// <summary>
    /// Model to store flight+hotel package search results
    /// </summary>
    public class FlightHotelResult
    {
        public int FlightId { get; set; }
        public string FlightName { get; set; } = string.Empty;
        public string Source { get; set; } = string.Empty;
        public string Destination { get; set; } = string.Empty;
        public string HotelName { get; set; } = string.Empty;
        public decimal TotalCost { get; set; }
    }
}
