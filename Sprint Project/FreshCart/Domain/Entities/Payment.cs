using System.ComponentModel.DataAnnotations;

namespace FreshCart.Domain.Entities;

public class Payment
{
    public Guid PaymentId { get; set; }
    
    public Guid OrderId { get; set; }
    
    public decimal Amount { get; set; }
    
    [MaxLength(3)]
    public string Currency { get; set; } = "USD";
    
    public PaymentStatus Status { get; set; }
    
    [MaxLength(100)]
    public string? StripePaymentIntentId { get; set; }
    
    [MaxLength(100)]
    public string? StripeChargeId { get; set; }
    
    public DateTime? ProcessedAt { get; set; }
    
    public decimal RefundedAmount { get; set; } = 0;
    
    public DateTime? RefundedAt { get; set; }
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    
    public byte[] RowVersion { get; set; } = Array.Empty<byte>();
    
    // Navigation properties
    public virtual Order Order { get; set; } = null!;
}

public enum PaymentStatus
{
    Pending = 0,
    Processing = 1,
    Succeeded = 2,
    Failed = 3,
    Cancelled = 4,
    Refunded = 5,
    PartiallyRefunded = 6
}