using System;

namespace FlexibleInventorySystem.Models
{
    /// <summary>
    /// Clothing product class
    /// </summary>
    public class ClothingProduct : Product
    {
        public string Size { get; set; }
        public string Color { get; set; }
        public string Material { get; set; }
        public string Gender { get; set; }
        public string Season { get; set; }

        /// <summary>
        /// Override GetProductDetails for clothing items
        /// </summary>
        public override string GetProductDetails()
        {
            return $"Size: {Size}, Color: {Color}, Material: {Material}, Gender: {Gender}, Season: {Season}";
        }

        /// <summary>
        /// Check if size is available
        /// Valid sizes: XS, S, M, L, XL, XXL
        /// </summary>
        public bool IsValidSize()
        {
            string[] validSizes = { "XS", "S", "M", "L", "XL", "XXL" };
            return Array.Exists(validSizes, s => s.Equals(Size, StringComparison.OrdinalIgnoreCase));
        }

        /// <summary>
        /// Override CalculateValue to apply seasonal discount
        /// Apply 15% discount for off-season items
        /// </summary>
        public override decimal CalculateValue()
        {
            decimal baseValue = base.CalculateValue();
            
            // Determine current season (simplified logic)
            int currentMonth = DateTime.Now.Month;
            string currentSeason = (currentMonth >= 6 && currentMonth <= 8) ? "Summer" : "Winter";
            
            // Apply discount if off-season
            if (!Season.Equals("All-season", StringComparison.OrdinalIgnoreCase) && 
                !Season.Equals(currentSeason, StringComparison.OrdinalIgnoreCase))
            {
                return baseValue * 0.85m; // 15% discount
            }
            
            return baseValue;
        }
    }
}
