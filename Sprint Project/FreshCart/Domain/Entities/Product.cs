using System.ComponentModel.DataAnnotations;

namespace FreshCart.Domain.Entities;

public class Product
{
    public Guid ProductId { get; set; }
    
    public Guid StoreId { get; set; }
    
    [Required]
    [MaxLength(200)]
    public string Name { get; set; } = string.Empty;
    
    [MaxLength(100)]
    public string? Brand { get; set; }
    
    [MaxLength(50)]
    public string? SKU { get; set; }
    
    [MaxLength(20)]
    public string? UPC { get; set; }
    
    [MaxLength(2000)]
    public string? Description { get; set; }
    
    public Guid CategoryId { get; set; }
    
    public decimal Price { get; set; }
    
    public decimal? CompareAtPrice { get; set; }
    
    [MaxLength(20)]
    public string Unit { get; set; } = "piece";
    
    public int StockQty { get; set; } = 0;
    
    public int LowStockThreshold { get; set; } = 10;
    
    public bool IsAvailable { get; set; } = true;
    
    public List<string> DietaryTags { get; set; } = new();
    
    public List<string> ImageUrls { get; set; } = new();
    
    public decimal AverageRating { get; set; } = 0;
    
    public int TotalReviews { get; set; } = 0;
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    
    public byte[] RowVersion { get; set; } = Array.Empty<byte>();
    
    // Navigation properties
    public virtual Store Store { get; set; } = null!;
    public virtual Category Category { get; set; } = null!;
    public virtual ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();
    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();
}