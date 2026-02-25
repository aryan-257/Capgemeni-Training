using System;

namespace DisplayHeightApp
{
    public class HeightCategory
    {
        // Method to get height category
        public string GetCategory(int heightCm)
        {
            if (heightCm < 0 || heightCm > 300)
            {
                throw new ArgumentOutOfRangeException(nameof(heightCm), "Height must be between 0 and 300.");
            }

            if (heightCm < 150)
                return "Short";
            else if (heightCm < 180)
                return "Average";
            else
                return "Tall";
        }
    }
}
