using System;
using FlexibleInventorySystem.Services;
using FlexibleInventorySystem.Models;
using FlexibleInventorySystem.Exceptions;

namespace FlexibleInventorySystem
{
    /// <summary>
    /// Console user interface
    /// </summary>
    class Program
    {
        private static InventoryManager _inventory = new InventoryManager();

        static void Main(string[] args)
        {
            Console.WriteLine("=== Flexible Inventory Management System ===\n");

            // Load sample data
            LoadSampleData();

            while (true)
            {
                DisplayMenu();
                string choice = Console.ReadLine();

                try
                {
                    switch (choice)
                    {
                        case "1":
                            AddProductMenu();
                            break;
                        case "2":
                            RemoveProductMenu();
                            break;
                        case "3":
                            UpdateQuantityMenu();
                            break;
                        case "4":
                            FindProductMenu();
                            break;
                        case "5":
                            ViewAllProducts();
                            break;
                        case "6":
                            GenerateReportsMenu();
                            break;
                        case "7":
                            CheckLowStockMenu();
                            break;
                        case "8":
                            Console.WriteLine("Thank you for using the Inventory System!");
                            return;
                        default:
                            Console.WriteLine("Invalid option. Try again.");
                            break;
                    }
                }
                catch (InventoryException ex)
                {
                    Console.WriteLine($"Inventory Error: {ex.Message}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }

                Console.WriteLine("\nPress any key to continue...");
                Console.ReadKey();
                Console.Clear();
            }
        }

        static void DisplayMenu()
        {
            Console.WriteLine("\n=== MAIN MENU ===");
            Console.WriteLine("1. Add Product");
            Console.WriteLine("2. Remove Product");
            Console.WriteLine("3. Update Quantity");
            Console.WriteLine("4. Find Product");
            Console.WriteLine("5. View All Products");
            Console.WriteLine("6. Generate Reports");
            Console.WriteLine("7. Check Low Stock");
            Console.WriteLine("8. Exit");
            Console.Write("\nEnter your choice: ");
        }

        static void AddProductMenu()
        {
            Console.WriteLine("\n=== Add Product ===");
            Console.WriteLine("Select Product Type:");
            Console.WriteLine("1. Electronic Product");
            Console.WriteLine("2. Grocery Product");
            Console.WriteLine("3. Clothing Product");
            Console.Write("Choice: ");
            
            string typeChoice = Console.ReadLine();

            switch (typeChoice)
            {
                case "1":
                    AddElectronicProduct();
                    break;
                case "2":
                    AddGroceryProduct();
                    break;
                case "3":
                    AddClothingProduct();
                    break;
                default:
                    Console.WriteLine("Invalid choice");
                    break;
            }
        }

        static void AddElectronicProduct()
        {
            Console.Write("Product ID: ");
            string id = Console.ReadLine();
            Console.Write("Name: ");
            string name = Console.ReadLine();
            Console.Write("Price: ");
            decimal price = decimal.Parse(Console.ReadLine());
            Console.Write("Quantity: ");
            int quantity = int.Parse(Console.ReadLine());
            Console.Write("Brand: ");
            string brand = Console.ReadLine();
            Console.Write("Warranty (months): ");
            int warranty = int.Parse(Console.ReadLine());
            Console.Write("Voltage: ");
            string voltage = Console.ReadLine();
            Console.Write("Is Refurbished (true/false): ");
            bool isRefurbished = bool.Parse(Console.ReadLine());

            var product = new ElectronicProduct
            {
                Id = id,
                Name = name,
                Price = price,
                Quantity = quantity,
                Category = "Electronics",
                Brand = brand,
                WarrantyMonths = warranty,
                Voltage = voltage,
                IsRefurbished = isRefurbished
            };

            if (_inventory.AddProduct(product))
            {
                Console.WriteLine("Electronic product added successfully!");
            }
        }

        static void AddGroceryProduct()
        {
            Console.Write("Product ID: ");
            string id = Console.ReadLine();
            Console.Write("Name: ");
            string name = Console.ReadLine();
            Console.Write("Price: ");
            decimal price = decimal.Parse(Console.ReadLine());
            Console.Write("Quantity: ");
            int quantity = int.Parse(Console.ReadLine());
            Console.Write("Expiry Date (yyyy-mm-dd): ");
            DateTime expiryDate = DateTime.Parse(Console.ReadLine());
            Console.Write("Is Perishable (true/false): ");
            bool isPerishable = bool.Parse(Console.ReadLine());
            Console.Write("Weight (kg): ");
            double weight = double.Parse(Console.ReadLine());
            Console.Write("Storage Temperature: ");
            string storage = Console.ReadLine();

            var product = new GroceryProduct
            {
                Id = id,
                Name = name,
                Price = price,
                Quantity = quantity,
                Category = "Groceries",
                ExpiryDate = expiryDate,
                IsPerishable = isPerishable,
                Weight = weight,
                StorageTemperature = storage
            };

            if (_inventory.AddProduct(product))
            {
                Console.WriteLine("Grocery product added successfully!");
            }
        }

        static void AddClothingProduct()
        {
            Console.Write("Product ID: ");
            string id = Console.ReadLine();
            Console.Write("Name: ");
            string name = Console.ReadLine();
            Console.Write("Price: ");
            decimal price = decimal.Parse(Console.ReadLine());
            Console.Write("Quantity: ");
            int quantity = int.Parse(Console.ReadLine());
            Console.Write("Size (XS/S/M/L/XL/XXL): ");
            string size = Console.ReadLine();
            Console.Write("Color: ");
            string color = Console.ReadLine();
            Console.Write("Material: ");
            string material = Console.ReadLine();
            Console.Write("Gender (Men/Women/Unisex): ");
            string gender = Console.ReadLine();
            Console.Write("Season (Summer/Winter/All-season): ");
            string season = Console.ReadLine();

            var product = new ClothingProduct
            {
                Id = id,
                Name = name,
                Price = price,
                Quantity = quantity,
                Category = "Clothing",
                Size = size,
                Color = color,
                Material = material,
                Gender = gender,
                Season = season
            };

            if (_inventory.AddProduct(product))
            {
                Console.WriteLine("Clothing product added successfully!");
            }
        }

        static void RemoveProductMenu()
        {
            Console.Write("\nEnter Product ID to remove: ");
            string id = Console.ReadLine();

            if (_inventory.RemoveProduct(id))
            {
                Console.WriteLine("Product removed successfully!");
            }
            else
            {
                Console.WriteLine("Product not found!");
            }
        }

        static void UpdateQuantityMenu()
        {
            Console.Write("\nEnter Product ID: ");
            string id = Console.ReadLine();
            Console.Write("Enter New Quantity: ");
            int quantity = int.Parse(Console.ReadLine());

            if (_inventory.UpdateQuantity(id, quantity))
            {
                Console.WriteLine("Quantity updated successfully!");
            }
            else
            {
                Console.WriteLine("Failed to update quantity!");
            }
        }

        static void FindProductMenu()
        {
            Console.Write("\nEnter Product ID: ");
            string id = Console.ReadLine();

            var product = _inventory.FindProduct(id);
            if (product != null)
            {
                Console.WriteLine("\n=== Product Found ===");
                Console.WriteLine(product.ToString());
                Console.WriteLine(product.GetProductDetails());
            }
            else
            {
                Console.WriteLine("Product not found!");
            }
        }

        static void ViewAllProducts()
        {
            Console.WriteLine("\n" + _inventory.GenerateInventoryReport());
        }

        static void GenerateReportsMenu()
        {
            Console.WriteLine("\n=== Reports Menu ===");
            Console.WriteLine("1. Inventory Report");
            Console.WriteLine("2. Category Summary");
            Console.WriteLine("3. Value Report");
            Console.WriteLine("4. Expiry Report");
            Console.Write("Choice: ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Console.WriteLine("\n" + _inventory.GenerateInventoryReport());
                    break;
                case "2":
                    Console.WriteLine("\n" + _inventory.GenerateCategorySummary());
                    break;
                case "3":
                    Console.WriteLine("\n" + _inventory.GenerateValueReport());
                    break;
                case "4":
                    Console.Write("Enter days threshold: ");
                    int days = int.Parse(Console.ReadLine());
                    Console.WriteLine("\n" + _inventory.GenerateExpiryReport(days));
                    break;
                default:
                    Console.WriteLine("Invalid choice");
                    break;
            }
        }

        static void CheckLowStockMenu()
        {
            Console.Write("\nEnter threshold quantity: ");
            int threshold = int.Parse(Console.ReadLine());

            var lowStockProducts = _inventory.GetLowStockProducts(threshold);

            if (lowStockProducts.Count == 0)
            {
                Console.WriteLine("No low stock products found.");
            }
            else
            {
                Console.WriteLine("\n=== Low Stock Products ===");
                foreach (var product in lowStockProducts)
                {
                    Console.WriteLine($"{product.Id} - {product.Name} - Quantity: {product.Quantity}");
                }
            }
        }

        static void LoadSampleData()
        {
            try
            {
                var laptop = new ElectronicProduct
                {
                    Id = "E001",
                    Name = "Laptop",
                    Price = 999.99m,
                    Quantity = 10,
                    Category = "Electronics",
                    Brand = "Dell",
                    WarrantyMonths = 24,
                    Voltage = "110-240V",
                    IsRefurbished = false
                };

                var milk = new GroceryProduct
                {
                    Id = "G001",
                    Name = "Milk",
                    Price = 3.49m,
                    Quantity = 50,
                    Category = "Groceries",
                    ExpiryDate = DateTime.Now.AddDays(7),
                    IsPerishable = true,
                    Weight = 1.0,
                    StorageTemperature = "Refrigerated"
                };

                var tshirt = new ClothingProduct
                {
                    Id = "C001",
                    Name = "T-Shirt",
                    Price = 19.99m,
                    Quantity = 100,
                    Category = "Clothing",
                    Size = "L",
                    Color = "Blue",
                    Material = "Cotton",
                    Gender = "Unisex",
                    Season = "All-season"
                };

                _inventory.AddProduct(laptop);
                _inventory.AddProduct(milk);
                _inventory.AddProduct(tshirt);

                Console.WriteLine("Sample data loaded successfully!\n");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading sample data: {ex.Message}");
            }
        }
    }
}
