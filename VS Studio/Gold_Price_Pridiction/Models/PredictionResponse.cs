namespace Gold_Price_Pridiction.Models
{
    /// <summary>
    /// Represents the prediction result received from the Python ML API
    /// </summary>
    public class PredictionResponse
    {
        /// <summary>
        /// Predicted price of the metal (in USD per ounce)
        /// </summary>
        public decimal PredictedPrice { get; set; }

        /// <summary>
        /// Metal type that was predicted (Gold or Silver)
        /// </summary>
        public string Metal { get; set; } = string.Empty;

        /// <summary>
        /// Date for which the prediction was made
        /// </summary>
        public DateTime PredictionDate { get; set; }

        /// <summary>
        /// Model confidence score (0.0 to 1.0)
        /// Higher values indicate more confident predictions
        /// </summary>
        public double Confidence { get; set; }

        /// <summary>
        /// Timestamp when the prediction was generated
        /// </summary>
        public DateTime Timestamp { get; set; }

        /// <summary>
        /// Indicates if the prediction was successful
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// Error message if prediction failed
        /// </summary>
        public string? ErrorMessage { get; set; }

        /// <summary>
        /// Additional metadata from the ML model (optional)
        /// Can include: model version, training accuracy, etc.
        /// </summary>
        public Dictionary<string, object>? Metadata { get; set; }

        // INR Conversion Properties
        /// <summary>
        /// USD to INR exchange rate used for conversion
        /// </summary>
        public decimal ExchangeRate { get; set; } = 83.12m; // Current USD to INR rate

        /// <summary>
        /// Price in INR per gram (24K gold standard)
        /// </summary>
        public decimal PricePerGramINR { get; set; }

        /// <summary>
        /// Price in INR per 10 grams
        /// </summary>
        public decimal PricePer10GramINR { get; set; }

        /// <summary>
        /// Price in INR per kilogram
        /// </summary>
        public decimal PricePerKgINR { get; set; }

        /// <summary>
        /// Calculate INR prices from USD per ounce
        /// Uses realistic Indian market prices for gold/silver
        /// </summary>
        public void CalculateINRPrices()
        {
            // For demo mode: Use realistic Indian market base prices
            // Gold: ~₹17,000 per gram (10g = ₹1,70,000)
            // Silver: ~₹90-100 per gram
            
            decimal pricePerGram;
            
            if (Metal.Equals("Gold", StringComparison.OrdinalIgnoreCase))
            {
                // Gold: Use realistic Indian market price
                // Base: ₹17,000 per gram (10g = ₹1,70,000) with variation from prediction
                decimal baseGoldPrice = 17000m;
                decimal variation = (PredictedPrice - 2050m) / 2050m; // Variation from base USD price
                pricePerGram = baseGoldPrice * (1 + variation);
            }
            else if (Metal.Equals("Silver", StringComparison.OrdinalIgnoreCase))
            {
                // Silver: Use realistic Indian market price
                // Base: ₹95 per gram with variation from prediction
                decimal baseSilverPrice = 95m;
                decimal variation = (PredictedPrice - 25m) / 25m; // Variation from base USD price
                pricePerGram = baseSilverPrice * (1 + variation);
            }
            else
            {
                // Fallback: Convert from USD per ounce
                const decimal GRAMS_PER_OUNCE = 31.1035m;
                pricePerGram = (PredictedPrice * ExchangeRate) / GRAMS_PER_OUNCE;
            }
            
            PricePerGramINR = Math.Round(pricePerGram, 2);
            PricePer10GramINR = Math.Round(pricePerGram * 10, 2);
            PricePerKgINR = Math.Round(pricePerGram * 1000, 2);
        }
    }
}
