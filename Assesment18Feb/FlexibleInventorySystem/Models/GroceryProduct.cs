using System;

namespace FlexibleInventorySystem.Models
{
    /// <summary>
    /// Grocery product class
    /// </summary>
    public class GroceryProduct : Product
    {
        public DateTime ExpiryDate { get; set; }
        public bool IsPerishable { get; set; }
        public double Weight { get; set; }
        public string StorageTemperature { get; set; }

        /// <summary>
        /// Override GetProductDetails for grocery items
        /// </summary>
        public override string GetProductDetails()
        {
            return $"Expiry: {ExpiryDate:yyyy-MM-dd}, Perishable: {IsPerishable}, Weight: {Weight}kg, Storage: {StorageTemperature}";
        }

        /// <summary>
        /// Check if product is expired
        /// </summary>
        public bool IsExpired()
        {
            return DateTime.Now > ExpiryDate;
        }

        /// <summary>
        /// Calculate days until expiry
        /// Return negative if expired
        /// </summary>
        public int DaysUntilExpiry()
        {
            return (ExpiryDate - DateTime.Now).Days;
        }

        /// <summary>
        /// Override CalculateValue to apply discount for near-expiry items
        /// Apply 20% discount if within 3 days of expiry
        /// </summary>
        public override decimal CalculateValue()
        {
            decimal baseValue = base.CalculateValue();
            
            if (DaysUntilExpiry() <= 3 && DaysUntilExpiry() >= 0)
            {
                return baseValue * 0.80m; // 20% discount
            }
            
            return baseValue;
        }
    }
}
