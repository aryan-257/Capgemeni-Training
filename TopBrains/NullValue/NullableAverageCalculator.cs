using System;
using System.Linq;

namespace NullValueApp
{
    public class NullableAverageCalculator
    {
        // Method to compute average of non-null values
        public double? ComputeAverage(double?[] values)
        {
            var nonNullValues = values.Where(v => v.HasValue).Select(v => v.Value).ToArray();

            if (nonNullValues.Length == 0)
                return null;

            double avg = nonNullValues.Average();

            // Round to 2 decimals using AwayFromZero
            return Math.Round(avg, 2, MidpointRounding.AwayFromZero);
        }
    }
}
