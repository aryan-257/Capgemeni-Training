using System.Data;
using Microsoft.Data.SqlClient;
using FLIGHT_SEARCH_ENGINE.Models;

namespace FLIGHT_SEARCH_ENGINE.Data
{
    /// <summary>
    /// Database helper class for executing stored procedures and retrieving data
    /// </summary>
    public class DatabaseHelper
    {
        private readonly string _connectionString;

        /// <summary>
        /// Constructor to initialize database connection
        /// </summary>
        /// <param name="configuration">IConfiguration to read connection string</param>
        public DatabaseHelper(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection") 
                ?? throw new ArgumentNullException(nameof(configuration), "Connection string not found");
        }

        /// <summary>
        /// Retrieve all distinct source locations from Flights table
        /// </summary>
        /// <returns>List of source cities</returns>
        public async Task<List<string>> GetSourcesAsync()
        {
            var sources = new List<string>();

            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand("sp_GetSources", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    await connection.OpenAsync();

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            sources.Add(reader["Source"].ToString() ?? string.Empty);
                        }
                    }
                }
            }

            return sources;
        }

        /// <summary>
        /// Retrieve all distinct destination locations from Flights table
        /// </summary>
        /// <returns>List of destination cities</returns>
        public async Task<List<string>> GetDestinationsAsync()
        {
            var destinations = new List<string>();

            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand("sp_GetDestinations", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    await connection.OpenAsync();

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            destinations.Add(reader["Destination"].ToString() ?? string.Empty);
                        }
                    }
                }
            }

            return destinations;
        }

        /// <summary>
        /// Search for flights matching criteria
        /// </summary>
        /// <param name="source">Source city</param>
        /// <param name="destination">Destination city</param>
        /// <param name="persons">Number of travelers</param>
        /// <returns>List of matching flights with total cost</returns>
        public async Task<List<FlightResult>> SearchFlightsAsync(string source, string destination, int persons)
        {
            var flights = new List<FlightResult>();

            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand("sp_SearchFlights", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Add parameters
                    command.Parameters.AddWithValue("@Source", source);
                    command.Parameters.AddWithValue("@Destination", destination);
                    command.Parameters.AddWithValue("@Persons", persons);

                    await connection.OpenAsync();

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            flights.Add(new FlightResult
                            {
                                FlightId = Convert.ToInt32(reader["FlightId"]),
                                FlightName = reader["FlightName"].ToString() ?? string.Empty,
                                FlightType = reader["FlightType"].ToString() ?? string.Empty,
                                Source = reader["Source"].ToString() ?? string.Empty,
                                Destination = reader["Destination"].ToString() ?? string.Empty,
                                TotalCost = Convert.ToDecimal(reader["TotalCost"])
                            });
                        }
                    }
                }
            }

            return flights;
        }

        /// <summary>
        /// Search for flight+hotel packages matching criteria
        /// </summary>
        /// <param name="source">Source city</param>
        /// <param name="destination">Destination city</param>
        /// <param name="persons">Number of travelers</param>
        /// <returns>List of matching flight+hotel packages with total cost</returns>
        public async Task<List<FlightHotelResult>> SearchFlightsWithHotelsAsync(string source, string destination, int persons)
        {
            var packages = new List<FlightHotelResult>();

            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand("sp_SearchFlightsWithHotels", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Add parameters
                    command.Parameters.AddWithValue("@Source", source);
                    command.Parameters.AddWithValue("@Destination", destination);
                    command.Parameters.AddWithValue("@Persons", persons);

                    await connection.OpenAsync();

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            packages.Add(new FlightHotelResult
                            {
                                FlightId = Convert.ToInt32(reader["FlightId"]),
                                FlightName = reader["FlightName"].ToString() ?? string.Empty,
                                Source = reader["Source"].ToString() ?? string.Empty,
                                Destination = reader["Destination"].ToString() ?? string.Empty,
                                HotelName = reader["HotelName"].ToString() ?? string.Empty,
                                TotalCost = Convert.ToDecimal(reader["TotalCost"])
                            });
                        }
                    }
                }
            }

            return packages;
        }
    }
}
