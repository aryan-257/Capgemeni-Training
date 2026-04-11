using System.ComponentModel.DataAnnotations;

namespace FreshCart.Domain.Entities;

public class Review
{
    public Guid ReviewId { get; set; }
    
    public Guid OrderId { get; set; }
    
    public Guid CustomerId { get; set; }
    
    public ReviewType ReviewType { get; set; }
    
    public Guid TargetId { get; set; } // ProductId or StoreId
    
    public int Rating { get; set; } // 1-5 stars
    
    [MaxLength(2000)]
    public string? ReviewText { get; set; }
    
    public List<string> PhotoUrls { get; set; } = new();
    
    [MaxLength(1000)]
    public string? ManagerResponse { get; set; }
    
    public DateTime? ManagerResponseAt { get; set; }
    
    public bool IsFlagged { get; set; } = false;
    
    public bool IsModerated { get; set; } = false;
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    
    public byte[] RowVersion { get; set; } = Array.Empty<byte>();
    
    // Navigation properties
    public virtual Order Order { get; set; } = null!;
    public virtual User Customer { get; set; } = null!;
}

public enum ReviewType
{
    Product = 0,
    Order = 1
}