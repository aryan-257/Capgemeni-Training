using System.Data;
using Microsoft.Data.SqlClient;
using FLIGHT_SEARCH_ENGINE.Models;

namespace FLIGHT_SEARCH_ENGINE.Data
{
    // Handles all database operations using stored procedures
    public class DatabaseHelper
    {
        private readonly string _connectionString;

        public DatabaseHelper(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        // Get list of source cities for dropdown
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
                            sources.Add(reader["Source"].ToString());
                        }
                    }
                }
            }

            return sources;
        }

        // Get list of destination cities for dropdown
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
                            destinations.Add(reader["Destination"].ToString());
                        }
                    }
                }
            }

            return destinations;
        }

        // Search for flights based on user input
        public async Task<List<FlightResult>> SearchFlightsAsync(string source, string destination, int persons)
        {
            var flights = new List<FlightResult>();

            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand("sp_SearchFlights", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Source", source);
                    command.Parameters.AddWithValue("@Destination", destination);
                    command.Parameters.AddWithValue("@Persons", persons);

                    await connection.OpenAsync();

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var flight = new FlightResult
                            {
                                FlightId = Convert.ToInt32(reader["FlightId"]),
                                FlightName = reader["FlightName"].ToString(),
                                FlightType = reader["FlightType"].ToString(),
                                Source = reader["Source"].ToString(),
                                Destination = reader["Destination"].ToString(),
                                TotalCost = Convert.ToDecimal(reader["TotalCost"])
                            };
                            flights.Add(flight);
                        }
                    }
                }
            }

            return flights;
        }

        // Search for flight + hotel packages
        public async Task<List<FlightHotelResult>> SearchFlightsWithHotelsAsync(string source, string destination, int persons)
        {
            var packages = new List<FlightHotelResult>();

            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand("sp_SearchFlightsWithHotels", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Source", source);
                    command.Parameters.AddWithValue("@Destination", destination);
                    command.Parameters.AddWithValue("@Persons", persons);

                    await connection.OpenAsync();

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var package = new FlightHotelResult
                            {
                                FlightId = Convert.ToInt32(reader["FlightId"]),
                                FlightName = reader["FlightName"].ToString(),
                                Source = reader["Source"].ToString(),
                                Destination = reader["Destination"].ToString(),
                                HotelName = reader["HotelName"].ToString(),
                                TotalCost = Convert.ToDecimal(reader["TotalCost"])
                            };
                            packages.Add(package);
                        }
                    }
                }
            }

            return packages;
        }
    }
}
