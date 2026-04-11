using System.ComponentModel.DataAnnotations;

namespace FreshCart.Domain.Entities;

public class Dispute
{
    public Guid DisputeId { get; set; }
    
    public Guid OrderId { get; set; }
    
    public Guid CustomerId { get; set; }
    
    public DisputeType Type { get; set; }
    
    [Required]
    [MaxLength(2000)]
    public string Description { get; set; } = string.Empty;
    
    public DisputeStatus Status { get; set; } = DisputeStatus.Open;
    
    public DisputeResolutionType? ResolutionType { get; set; }
    
    public decimal RefundAmount { get; set; } = 0;
    
    [MaxLength(2000)]
    public string? AdminNotes { get; set; }
    
    public DateTime? ResolvedAt { get; set; }
    
    public Guid? ResolvedByAdminId { get; set; }
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    
    public byte[] RowVersion { get; set; } = Array.Empty<byte>();
    
    // Navigation properties
    public virtual Order Order { get; set; } = null!;
    public virtual User Customer { get; set; } = null!;
    public virtual User? ResolvedByAdmin { get; set; }
}

public enum DisputeType
{
    MissingItems = 0,
    DamagedItems = 1,
    WrongItems = 2,
    LateDelivery = 3,
    PoorQuality = 4,
    Other = 5
}

public enum DisputeStatus
{
    Open = 0,
    InReview = 1,
    Resolved = 2,
    Closed = 3
}

public enum DisputeResolutionType
{
    FullRefund = 0,
    PartialRefund = 1,
    StoreCredit = 2,
    Redelivery = 3,
    NoAction = 4
}