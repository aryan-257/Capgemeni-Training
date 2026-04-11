using System.ComponentModel.DataAnnotations;

namespace FreshCart.Domain.Entities;

public class Promotion
{
    public Guid PromotionId { get; set; }
    
    public Guid? StoreId { get; set; } // null = platform-wide
    
    [Required]
    [MaxLength(200)]
    public string Name { get; set; } = string.Empty;
    
    public PromotionType Type { get; set; }
    
    public decimal Value { get; set; }
    
    public PromotionAppliesTo AppliesTo { get; set; }
    
    public Guid? TargetId { get; set; } // CategoryId or ProductId based on AppliesTo
    
    public decimal MinOrderValue { get; set; } = 0;
    
    public DateTime StartAt { get; set; }
    
    public DateTime EndAt { get; set; }
    
    public int? UsageLimit { get; set; }
    
    public int UsageCount { get; set; } = 0;
    
    public bool IsActive { get; set; } = true;
    
    [MaxLength(20)]
    public string? CouponCode { get; set; }
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    
    public byte[] RowVersion { get; set; } = Array.Empty<byte>();
    
    // Navigation properties
    public virtual Store? Store { get; set; }
    public virtual ICollection<PromotionRedemption> PromotionRedemptions { get; set; } = new List<PromotionRedemption>();
}

public class PromotionRedemption
{
    public Guid RedemptionId { get; set; }
    
    public Guid PromotionId { get; set; }
    
    public Guid OrderId { get; set; }
    
    public Guid CustomerId { get; set; }
    
    public decimal DiscountAmount { get; set; }
    
    public DateTime RedeemedAt { get; set; } = DateTime.UtcNow;
    
    // Navigation properties
    public virtual Promotion Promotion { get; set; } = null!;
    public virtual Order Order { get; set; } = null!;
    public virtual User Customer { get; set; } = null!;
}

public enum PromotionType
{
    Percentage = 0,
    FixedAmount = 1,
    BOGO = 2,
    Bundle = 3
}

public enum PromotionAppliesTo
{
    All = 0,
    Category = 1,
    Product = 2
}