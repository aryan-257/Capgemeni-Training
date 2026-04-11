using System.ComponentModel.DataAnnotations;

namespace FreshCart.Domain.Entities;

public class Order
{
    public Guid OrderId { get; set; }
    
    public Guid CustomerId { get; set; }
    
    public Guid StoreId { get; set; }
    
    public Guid? AssignedDriverId { get; set; }
    
    public Guid DeliveryAddressId { get; set; }
    
    public Guid? SlotId { get; set; }
    
    public OrderStatus Status { get; set; } = OrderStatus.Placed;
    
    public decimal Subtotal { get; set; }
    
    public decimal DeliveryFee { get; set; }
    
    public decimal Discount { get; set; } = 0;
    
    public decimal Tax { get; set; }
    
    public decimal Total { get; set; }
    
    [MaxLength(100)]
    public string? PaymentIntentId { get; set; }
    
    public int LoyaltyPointsAwarded { get; set; } = 0;
    
    [MaxLength(1000)]
    public string? SpecialInstructions { get; set; }
    
    public DateTime? CancelledAt { get; set; }
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    
    public byte[] RowVersion { get; set; } = Array.Empty<byte>();
    
    // Navigation properties
    public virtual User Customer { get; set; } = null!;
    public virtual Store Store { get; set; } = null!;
    public virtual User? AssignedDriver { get; set; }
    public virtual Address DeliveryAddress { get; set; } = null!;
    public virtual DeliverySlot? Slot { get; set; }
    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    public virtual ICollection<OrderStatusHistory> StatusHistory { get; set; } = new List<OrderStatusHistory>();
    public virtual DriverAssignment? DriverAssignment { get; set; }
}

public class OrderItem
{
    public Guid OrderItemId { get; set; }
    
    public Guid OrderId { get; set; }
    
    public Guid ProductId { get; set; }
    
    public int Quantity { get; set; }
    
    public decimal UnitPrice { get; set; }
    
    public decimal LineTotal { get; set; }
    
    public bool IsSubstituted { get; set; } = false;
    
    public Guid? OriginalProductId { get; set; }
    
    [MaxLength(500)]
    public string? SpecialInstructions { get; set; }
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    // Navigation properties
    public virtual Order Order { get; set; } = null!;
    public virtual Product Product { get; set; } = null!;
    public virtual Product? OriginalProduct { get; set; }
}

public class OrderStatusHistory
{
    public Guid StatusHistoryId { get; set; }
    
    public Guid OrderId { get; set; }
    
    public OrderStatus Status { get; set; }
    
    [MaxLength(500)]
    public string? Notes { get; set; }
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    // Navigation properties
    public virtual Order Order { get; set; } = null!;
}

public enum OrderStatus
{
    Placed = 0,
    Confirmed = 1,
    Picking = 2,
    ReadyForPickup = 3,
    OutForDelivery = 4,
    Delivered = 5,
    Cancelled = 6,
    IssueReported = 7
}