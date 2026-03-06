namespace SevicesDemo.Models
{
    public class CalculatorViewModel
    {
        public double Number1 { get; set; }
        public double Number2 { get; set; }
        public string Operation { get; set; } = string.Empty;
        public double? Result { get; set; }
        public string? ErrorMessage { get; set; }
    }
}
