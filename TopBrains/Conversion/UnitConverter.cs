using System;

namespace ConversionApp
{
    public class UnitConverter
    {
        private const double CM_PER_FOOT = 30.48;

        public double FeetToCentimeters(int feet)
        {
            double cm = feet * CM_PER_FOOT;
            return Math.Round(cm, 2, MidpointRounding.AwayFromZero);
        }
    }
}
