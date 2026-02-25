using System;
using System.Globalization;
using System.Text;

namespace InventoryCleanupApp
{
    public class InventoryCleaner
    {
        // Method to clean up inventory name
        public string CleanName(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return string.Empty;

            input = input.Trim(); // Remove leading/trailing spaces

            StringBuilder sb = new StringBuilder();
            char? lastChar = null;

            foreach (char c in input)
            {
                if (c != lastChar)
                {
                    sb.Append(c);
                    lastChar = c;
                }
            }

            // Remove extra spaces between words
            string noExtraSpaces = System.Text.RegularExpressions.Regex.Replace(sb.ToString(), @"\s+", " ");

            // Convert to Title Case
            TextInfo textInfo = CultureInfo.CurrentCulture.TextInfo;
            string titleCase = textInfo.ToTitleCase(noExtraSpaces.ToLower());

            return titleCase;
        }
    }
}
