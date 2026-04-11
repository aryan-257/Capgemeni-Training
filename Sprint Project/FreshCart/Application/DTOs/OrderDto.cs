namespace FreshCart.Application.DTOs;

public class OrderDto
{
    public Guid OrderId { get; set; }
    public Guid CustomerId { get; set; }
    public Guid StoreId { get; set; }
    public string StoreName { get; set; } = string.Empty;
    public Guid? AssignedDriverId { get; set; }
    public string? DriverName { get; set; }
    public string Status { get; set; } = string.Empty;
    public decimal Subtotal { get; set; }
    public decimal DeliveryFee { get; set; }
    public decimal Discount { get; set; }
    public decimal Tax { get; set; }
    public decimal Total { get; set; }
    public int LoyaltyPointsAwarded { get; set; }
    public string? SpecialInstructions { get; set; }
    public AddressDto DeliveryAddress { get; set; } = null!;
    public DeliverySlotDto? DeliverySlot { get; set; }
    public List<OrderItemDto> Items { get; set; } = new();
    public DateTime CreatedAt { get; set; }
    public DateTime? CancelledAt { get; set; }
}

public class OrderItemDto
{
    public Guid OrderItemId { get; set; }
    public Guid ProductId { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public string? ProductBrand { get; set; }
    public string ProductImageUrl { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal LineTotal { get; set; }
    public string Unit { get; set; } = string.Empty;
    public bool IsSubstituted { get; set; }
    public string? OriginalProductName { get; set; }
    public string? SpecialInstructions { get; set; }
}

public class CreateOrderDto
{
    public Guid CartId { get; set; }
    public Guid DeliveryAddressId { get; set; }
    public Guid? SlotId { get; set; }
    public string PaymentIntentId { get; set; } = string.Empty;
    public string? CouponCode { get; set; }
    public string? SpecialInstructions { get; set; }
    public bool UseExpressDelivery { get; set; } = false;
}

public class OrderConfirmationDto
{
    public Guid OrderId { get; set; }
    public string OrderNumber { get; set; } = string.Empty;
    public decimal Total { get; set; }
    public string Status { get; set; } = string.Empty;
    public DateTime EstimatedDeliveryStart { get; set; }
    public DateTime EstimatedDeliveryEnd { get; set; }
    public AddressDto DeliveryAddress { get; set; } = null!;
    public List<OrderItemDto> Items { get; set; } = new();
    public string ReceiptUrl { get; set; } = string.Empty;
}

public class OrderTrackingDto
{
    public Guid OrderId { get; set; }
    public string Status { get; set; } = string.Empty;
    public DriverInfoDto? Driver { get; set; }
    public AddressDto StoreAddress { get; set; } = null!;
    public AddressDto DeliveryAddress { get; set; } = null!;
    public DateTime? EstimatedDeliveryTime { get; set; }
    public int? ETDMinutes { get; set; }
    public string? ProofOfDeliveryUrl { get; set; }
    public List<OrderStatusHistoryDto> StatusHistory { get; set; } = new();
}

public class DriverInfoDto
{
    public Guid DriverId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? PhotoUrl { get; set; }
    public string? Vehicle { get; set; }
    public decimal Rating { get; set; }
    public double? CurrentLatitude { get; set; }
    public double? CurrentLongitude { get; set; }
}

public class OrderStatusHistoryDto
{
    public string Status { get; set; } = string.Empty;
    public string? Notes { get; set; }
    public DateTime CreatedAt { get; set; }
}