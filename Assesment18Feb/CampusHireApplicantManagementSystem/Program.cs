using System;

namespace CampusHireApplicantManagementSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            ApplicantManager manager = new ApplicantManager();
            bool exit = false;

            Console.WriteLine("=== CampusHire Applicant Management System ===\n");

            while (!exit)
            {
                Console.WriteLine("\n--- Main Menu ---");
                Console.WriteLine("1. Add New Applicant");
                Console.WriteLine("2. Display All Applicants");
                Console.WriteLine("3. Search Applicant by ID");
                Console.WriteLine("4. Update Applicant Details");
                Console.WriteLine("5. Delete Applicant");
                Console.WriteLine("6. Exit");
                Console.Write("\nEnter your choice: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddNewApplicant(manager);
                        break;
                    case "2":
                        manager.DisplayAllApplicants();
                        break;
                    case "3":
                        SearchApplicant(manager);
                        break;
                    case "4":
                        UpdateApplicant(manager);
                        break;
                    case "5":
                        DeleteApplicant(manager);
                        break;
                    case "6":
                        exit = true;
                        Console.WriteLine("Thank you for using CampusHire Applicant Management System!");
                        break;
                    default:
                        Console.WriteLine("Invalid choice! Please try again.");
                        break;
                }
            }
        }

        static void AddNewApplicant(ApplicantManager manager)
        {
            Console.WriteLine("\n=== Add New Applicant ===");

            Console.Write("Applicant ID (8 chars, starts with CH): ");
            string id = Console.ReadLine();

            Console.Write("Applicant Name (4-15 chars): ");
            string name = Console.ReadLine();

            Console.Write("Current Location (Mumbai/Pune/Chennai): ");
            string currentLoc = Console.ReadLine();

            Console.Write("Preferred Job Location (Mumbai/Pune/Chennai/Delhi/Kolkata/Bangalore): ");
            string prefLoc = Console.ReadLine();

            Console.Write("Core Competency (.NET/JAVA/ORACLE/Testing): ");
            string competency = Console.ReadLine();

            Console.Write("Passing Year: ");
            if (int.TryParse(Console.ReadLine(), out int year))
            {
                Applicant applicant = new Applicant(id, name, currentLoc, prefLoc, competency, year);
                manager.AddApplicant(applicant);
            }
            else
            {
                Console.WriteLine("Invalid year format!");
            }
        }

        static void SearchApplicant(ApplicantManager manager)
        {
            Console.Write("\nEnter Applicant ID to search: ");
            string id = Console.ReadLine();
            manager.SearchApplicant(id);
        }

        static void UpdateApplicant(ApplicantManager manager)
        {
            Console.Write("\nEnter Applicant ID to update: ");
            string id = Console.ReadLine();
            manager.UpdateApplicant(id);
        }

        static void DeleteApplicant(ApplicantManager manager)
        {
            Console.Write("\nEnter Applicant ID to delete: ");
            string id = Console.ReadLine();
            manager.DeleteApplicant(id);
        }
    }
}
