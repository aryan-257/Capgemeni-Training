using System;

namespace Problem1.SmartWarehouse
{
    public class InventoryException : Exception
    {
        public InventoryException(string message) : base(message) { }
    }

    public class LowStockException : InventoryException
    {
        public LowStockException(string message) : base(message) { }
    }

    public class DuplicateSKUException : InventoryException
    {
        public DuplicateSKUException(string message) : base(message) { }
    }

    public class InvalidProductException : InventoryException
    {
        public InvalidProductException(string message) : base(message) { }
    }
}
