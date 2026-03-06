namespace SevicesDemo.Services
{
    public class BasicCalculatorService : IBasicCalculatorService
    {
        public double Add(double a, double b)
        {
            return a + b;
        }
    }
}
