using System;

namespace Problem1.SmartWarehouse
{
    public abstract class Product
    {
        public string SKU { get; set; }
        public string Name { get; set; }
        private int stock;
        public int StockThreshold { get; set; }

        public int Stock
        {
            get => stock;
            set
            {
                if (value < 0)
                    throw new InvalidProductException("Stock cannot be negative");
                stock = value;
                if (stock < StockThreshold)
                    throw new LowStockException($"Low stock alert for {Name}. Current: {stock}, Threshold: {StockThreshold}");
            }
        }

        protected Product(string sku, string name, int stock, int threshold)
        {
            SKU = sku;
            Name = name;
            StockThreshold = threshold;
            this.stock = stock;
        }

        public abstract string GetProductType();
    }

    public class Electronics : Product
    {
        public int WarrantyMonths { get; set; }

        public Electronics(string sku, string name, int stock, int threshold, int warranty)
            : base(sku, name, stock, threshold)
        {
            WarrantyMonths = warranty;
        }

        public override string GetProductType() => "Electronics";
    }

    public class Perishable : Product
    {
        public DateTime ExpiryDate { get; set; }

        public Perishable(string sku, string name, int stock, int threshold, DateTime expiry)
            : base(sku, name, stock, threshold)
        {
            ExpiryDate = expiry;
        }

        public override string GetProductType() => "Perishable";
    }

    public class FragileItem : Product
    {
        public string HandlingInstructions { get; set; }

        public FragileItem(string sku, string name, int stock, int threshold, string instructions)
            : base(sku, name, stock, threshold)
        {
            HandlingInstructions = instructions;
        }

        public override string GetProductType() => "Fragile Item";
    }
}
