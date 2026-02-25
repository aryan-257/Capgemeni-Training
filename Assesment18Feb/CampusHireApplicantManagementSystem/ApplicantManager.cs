using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text.Json;

namespace CampusHireApplicantManagementSystem
{
    public class ApplicantManager
    {
        private List<Applicant> applicants;
        private const string DataFile = "applicants.json";

        public ApplicantManager()
        {
            applicants = new List<Applicant>();
            LoadData();
        }

        // Add new applicant
        public void AddApplicant(Applicant applicant)
        {
            if (ValidateApplicant(applicant))
            {
                // Check for duplicate ID
                if (applicants.Any(a => a.ApplicantId == applicant.ApplicantId))
                {
                    Console.WriteLine("Error: Applicant ID already exists!");
                    return;
                }

                applicants.Add(applicant);
                SaveData();
                Console.WriteLine("Applicant added successfully!");
            }
        }

        // Display all applicants
        public void DisplayAllApplicants()
        {
            if (applicants.Count == 0)
            {
                Console.WriteLine("No applicants found.");
                return;
            }

            Console.WriteLine("\n=== All Applicants ===");
            foreach (var applicant in applicants)
            {
                Console.WriteLine(applicant);
            }
        }

        // Search applicant by ID
        public void SearchApplicant(string applicantId)
        {
            var applicant = applicants.FirstOrDefault(a => a.ApplicantId == applicantId);
            if (applicant != null)
            {
                Console.WriteLine("\n=== Applicant Found ===");
                Console.WriteLine(applicant);
            }
            else
            {
                Console.WriteLine("Applicant not found!");
            }
        }

        // Update applicant details
        public void UpdateApplicant(string applicantId)
        {
            var applicant = applicants.FirstOrDefault(a => a.ApplicantId == applicantId);
            if (applicant == null)
            {
                Console.WriteLine("Applicant not found!");
                return;
            }

            Console.WriteLine("\n=== Update Applicant Details ===");
            Console.WriteLine("Leave blank to keep current value");

            Console.Write($"Name [{applicant.ApplicantName}]: ");
            string name = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(name) && name.Length >= 4 && name.Length <= 15)
                applicant.ApplicantName = name;

            Console.Write($"Current Location [{applicant.CurrentLocation}] (Mumbai/Pune/Chennai): ");
            string currentLoc = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(currentLoc) && IsValidCurrentLocation(currentLoc))
                applicant.CurrentLocation = currentLoc;

            Console.Write($"Preferred Job Location [{applicant.PreferredJobLocation}] (Mumbai/Pune/Chennai/Delhi/Kolkata/Bangalore): ");
            string prefLoc = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(prefLoc) && IsValidPreferredLocation(prefLoc))
                applicant.PreferredJobLocation = prefLoc;

            Console.Write($"Core Competency [{applicant.CoreCompetency}] (.NET/JAVA/ORACLE/Testing): ");
            string competency = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(competency) && IsValidCompetency(competency))
                applicant.CoreCompetency = competency;

            Console.Write($"Passing Year [{applicant.PassingYear}]: ");
            string yearInput = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(yearInput) && int.TryParse(yearInput, out int year) && year <= DateTime.Now.Year)
                applicant.PassingYear = year;

            SaveData();
            Console.WriteLine("Applicant updated successfully!");
        }

        // Delete applicant
        public void DeleteApplicant(string applicantId)
        {
            var applicant = applicants.FirstOrDefault(a => a.ApplicantId == applicantId);
            if (applicant != null)
            {
                applicants.Remove(applicant);
                SaveData();
                Console.WriteLine("Applicant deleted successfully!");
            }
            else
            {
                Console.WriteLine("Applicant not found!");
            }
        }

        // Validation
        private bool ValidateApplicant(Applicant applicant)
        {
            // Check all fields are not empty
            if (string.IsNullOrWhiteSpace(applicant.ApplicantId) ||
                string.IsNullOrWhiteSpace(applicant.ApplicantName) ||
                string.IsNullOrWhiteSpace(applicant.CurrentLocation) ||
                string.IsNullOrWhiteSpace(applicant.PreferredJobLocation) ||
                string.IsNullOrWhiteSpace(applicant.CoreCompetency))
            {
                Console.WriteLine("Error: All fields are mandatory!");
                return false;
            }

            // Validate Applicant ID (8 characters, starts with "CH")
            if (applicant.ApplicantId.Length != 8 || !applicant.ApplicantId.StartsWith("CH"))
            {
                Console.WriteLine("Error: Applicant ID must be exactly 8 characters and start with 'CH'!");
                return false;
            }

            // Validate Applicant Name (4-15 characters)
            if (applicant.ApplicantName.Length < 4 || applicant.ApplicantName.Length > 15)
            {
                Console.WriteLine("Error: Applicant Name must be between 4 and 15 characters!");
                return false;
            }

            // Validate Passing Year (not greater than current year)
            if (applicant.PassingYear > DateTime.Now.Year)
            {
                Console.WriteLine("Error: Passing Year cannot be greater than current year!");
                return false;
            }

            // Validate Current Location
            if (!IsValidCurrentLocation(applicant.CurrentLocation))
            {
                Console.WriteLine("Error: Current Location must be Mumbai, Pune, or Chennai!");
                return false;
            }

            // Validate Preferred Job Location
            if (!IsValidPreferredLocation(applicant.PreferredJobLocation))
            {
                Console.WriteLine("Error: Preferred Job Location must be Mumbai, Pune, Chennai, Delhi, Kolkata, or Bangalore!");
                return false;
            }

            // Validate Core Competency
            if (!IsValidCompetency(applicant.CoreCompetency))
            {
                Console.WriteLine("Error: Core Competency must be .NET, JAVA, ORACLE, or Testing!");
                return false;
            }

            return true;
        }

        private bool IsValidCurrentLocation(string location)
        {
            string[] validLocations = { "Mumbai", "Pune", "Chennai" };
            return validLocations.Contains(location, StringComparer.OrdinalIgnoreCase);
        }

        private bool IsValidPreferredLocation(string location)
        {
            string[] validLocations = { "Mumbai", "Pune", "Chennai", "Delhi", "Kolkata", "Bangalore" };
            return validLocations.Contains(location, StringComparer.OrdinalIgnoreCase);
        }

        private bool IsValidCompetency(string competency)
        {
            string[] validCompetencies = { ".NET", "JAVA", "ORACLE", "Testing" };
            return validCompetencies.Contains(competency, StringComparer.OrdinalIgnoreCase);
        }

        // Save data to file using JSON serialization
        private void SaveData()
        {
            try
            {
                string jsonData = JsonSerializer.Serialize(applicants, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(DataFile, jsonData);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving data: {ex.Message}");
            }
        }

        // Load data from file
        private void LoadData()
        {
            try
            {
                if (File.Exists(DataFile))
                {
                    string jsonData = File.ReadAllText(DataFile);
                    applicants = JsonSerializer.Deserialize<List<Applicant>>(jsonData) ?? new List<Applicant>();
                    Console.WriteLine($"Loaded {applicants.Count} applicant(s) from file.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading data: {ex.Message}");
                applicants = new List<Applicant>();
            }
        }
    }
}
