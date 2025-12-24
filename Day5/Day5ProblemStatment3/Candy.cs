using System;

namespace Day5ProblemStatment3;

public class Candy
{
    public string? Flavour { get; set; }
    public int Quantity { get; set; }
    public int PricePerPiece { get; set; }
    public double TotalPriece { get; set; }
    public double Discount { get; set; }

    public bool ValidateCandyFloavour()
    {
        if (Flavour == "Strawberry" || Flavour == "Lemon" || Flavour == "Mint")
        {
            return true;
        }
        else
        {
            return false;
        }
    }

}
