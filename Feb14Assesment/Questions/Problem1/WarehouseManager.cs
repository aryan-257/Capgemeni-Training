using System;
using System.Collections.Generic;
using System.Linq;

namespace Problem1.SmartWarehouse
{
    public class WarehouseManager
    {
        private SortedDictionary<int, List<Product>> inventory = new SortedDictionary<int, List<Product>>();
        private HashSet<string> skuSet = new HashSet<string>();

        public void AddProduct(int priority, Product product)
        {
            if (priority < 1 || priority > 10)
                throw new InvalidProductException("Priority must be between 1 and 10");

            if (skuSet.Contains(product.SKU))
                throw new DuplicateSKUException($"Product with SKU {product.SKU} already exists");

            if (!inventory.ContainsKey(priority))
                inventory[priority] = new List<Product>();

            inventory[priority].Add(product);
            skuSet.Add(product.SKU);
            Console.WriteLine($"Added {product.Name} with priority {priority}");
        }

        public void RemoveProduct(string sku)
        {
            foreach (var kvp in inventory)
            {
                var product = kvp.Value.FirstOrDefault(p => p.SKU == sku);
                if (product != null)
                {
                    kvp.Value.Remove(product);
                    skuSet.Remove(sku);
                    Console.WriteLine($"Removed product {product.Name}");
                    return;
                }
            }
            throw new InvalidProductException($"Product with SKU {sku} not found");
        }

        public void UpdateStock(string sku, int newStock)
        {
            foreach (var kvp in inventory)
            {
                var product = kvp.Value.FirstOrDefault(p => p.SKU == sku);
                if (product != null)
                {
                    product.Stock = newStock;
                    Console.WriteLine($"Updated stock for {product.Name} to {newStock}");
                    return;
                }
            }
            throw new InvalidProductException($"Product with SKU {sku} not found");
        }

        public List<Product> GetHighestPriorityProducts()
        {
            if (inventory.Count == 0)
                return new List<Product>();
            return inventory.First().Value;
        }

        public void DisplayInventory()
        {
            Console.WriteLine("\n=== Warehouse Inventory ===");
            foreach (var kvp in inventory)
            {
                Console.WriteLine($"\nPriority {kvp.Key}:");
                foreach (var product in kvp.Value)
                {
                    Console.WriteLine($"  - {product.Name} (SKU: {product.SKU}, Stock: {product.Stock}, Type: {product.GetProductType()})");
                }
            }
        }
    }
}
