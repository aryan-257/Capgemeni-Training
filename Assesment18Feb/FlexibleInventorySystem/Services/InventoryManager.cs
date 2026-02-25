using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FlexibleInventorySystem.Interfaces;
using FlexibleInventorySystem.Models;
using FlexibleInventorySystem.Exceptions;

namespace FlexibleInventorySystem.Services
{
    /// <summary>
    /// Main inventory manager class
    /// </summary>
    public class InventoryManager : IInventoryOperations, IReportGenerator
    {
        private List<Product> products;
        private readonly object lockObject = new object();

        public InventoryManager()
        {
            products = new List<Product>();
        }

        // ============ IInventoryOperations Implementation ============

        public bool AddProduct(Product product)
        {
            lock (lockObject)
            {
                if (product == null)
                {
                    throw new ArgumentNullException(nameof(product), "Product cannot be null");
                }

                if (products.Any(p => p.Id == product.Id))
                {
                    throw new InventoryException("Product with this ID already exists", "DUPLICATE_ID");
                }

                if (product.Price <= 0)
                {
                    throw new InventoryException("Price must be positive", "INVALID_PRICE");
                }

                if (product.Quantity < 0)
                {
                    throw new InventoryException("Quantity cannot be negative", "INVALID_QUANTITY");
                }

                product.DateAdded = DateTime.Now;
                products.Add(product);
                return true;
            }
        }

        public bool RemoveProduct(string productId)
        {
            lock (lockObject)
            {
                var product = products.FirstOrDefault(p => p.Id == productId);
                if (product != null)
                {
                    products.Remove(product);
                    return true;
                }
                return false;
            }
        }

        public Product FindProduct(string productId)
        {
            return products.FirstOrDefault(p => p.Id == productId);
        }

        public List<Product> GetProductsByCategory(string category)
        {
            return products.Where(p => p.Category.Equals(category, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        public bool UpdateQuantity(string productId, int newQuantity)
        {
            lock (lockObject)
            {
                if (newQuantity < 0)
                {
                    return false;
                }

                var product = FindProduct(productId);
                if (product == null)
                {
                    return false;
                }

                product.Quantity = newQuantity;
                return true;
            }
        }

        public decimal GetTotalInventoryValue()
        {
            return products.Sum(p => p.CalculateValue());
        }

        public List<Product> GetLowStockProducts(int threshold)
        {
            return products.Where(p => p.Quantity < threshold).ToList();
        }

        // ============ IReportGenerator Implementation ============

        public string GenerateInventoryReport()
        {
            StringBuilder report = new StringBuilder();
            report.AppendLine("================================");
            report.AppendLine("INVENTORY REPORT");
            report.AppendLine("================================");
            report.AppendLine($"Total Products: {products.Count}");
            report.AppendLine($"Total Value: {GetTotalInventoryValue():C}");
            report.AppendLine();
            report.AppendLine("Product List:");

            foreach (var product in products)
            {
                report.AppendLine($"{product.Id} - {product.Name} - {product.Category} - Qty: {product.Quantity} - Value: {product.CalculateValue():C}");
            }

            return report.ToString();
        }

        public string GenerateCategorySummary()
        {
            StringBuilder report = new StringBuilder();
            report.AppendLine("CATEGORY SUMMARY");
            report.AppendLine("================================");

            var categoryGroups = products.GroupBy(p => p.Category);

            foreach (var group in categoryGroups)
            {
                int count = group.Count();
                decimal totalValue = group.Sum(p => p.CalculateValue());
                report.AppendLine($"{group.Key}: {count} items - Total Value: {totalValue:C}");
            }

            return report.ToString();
        }

        public string GenerateValueReport()
        {
            StringBuilder report = new StringBuilder();
            report.AppendLine("VALUE ANALYSIS REPORT");
            report.AppendLine("================================");

            if (products.Count == 0)
            {
                report.AppendLine("No products in inventory");
                return report.ToString();
            }

            var mostValuable = products.OrderByDescending(p => p.CalculateValue()).FirstOrDefault();
            var leastValuable = products.OrderBy(p => p.CalculateValue()).FirstOrDefault();
            decimal averagePrice = products.Average(p => p.Price);
            
            var sortedPrices = products.Select(p => p.Price).OrderBy(p => p).ToList();
            decimal medianPrice = sortedPrices.Count % 2 == 0
                ? (sortedPrices[sortedPrices.Count / 2 - 1] + sortedPrices[sortedPrices.Count / 2]) / 2
                : sortedPrices[sortedPrices.Count / 2];

            report.AppendLine($"Most Valuable Product: {mostValuable?.Name} - {mostValuable?.CalculateValue():C}");
            report.AppendLine($"Least Valuable Product: {leastValuable?.Name} - {leastValuable?.CalculateValue():C}");
            report.AppendLine($"Average Price: {averagePrice:C}");
            report.AppendLine($"Median Price: {medianPrice:C}");
            report.AppendLine();
            report.AppendLine("Products Above Average Price:");

            foreach (var product in products.Where(p => p.Price > averagePrice))
            {
                report.AppendLine($"  {product.Name} - {product.Price:C}");
            }

            return report.ToString();
        }

        public string GenerateExpiryReport(int daysThreshold)
        {
            StringBuilder report = new StringBuilder();
            report.AppendLine($"EXPIRY REPORT (Within {daysThreshold} days)");
            report.AppendLine("================================");

            var expiringProducts = products.OfType<GroceryProduct>()
                .Where(p => p.DaysUntilExpiry() <= daysThreshold && p.DaysUntilExpiry() >= 0)
                .OrderBy(p => p.DaysUntilExpiry());

            if (!expiringProducts.Any())
            {
                report.AppendLine("No products expiring soon");
            }
            else
            {
                foreach (var product in expiringProducts)
                {
                    report.AppendLine($"{product.Name} - Expires in {product.DaysUntilExpiry()} days ({product.ExpiryDate:yyyy-MM-dd})");
                }
            }

            return report.ToString();
        }

        // ============ Additional Methods ============

        public IEnumerable<Product> SearchProducts(Func<Product, bool> predicate)
        {
            return products.Where(predicate);
        }

        public void ApplyCategoryDiscount(string category, decimal discountPercentage)
        {
            lock (lockObject)
            {
                var categoryProducts = GetProductsByCategory(category);
                foreach (var product in categoryProducts)
                {
                    product.Price *= (1 - discountPercentage / 100);
                }
            }
        }

        public int GetTotalProductCount()
        {
            return products.Count;
        }

        public IEnumerable<string> GetCategories()
        {
            return products.Select(p => p.Category).Distinct();
        }
    }
}
