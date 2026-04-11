using System.ComponentModel.DataAnnotations;

namespace FreshCart.Domain.Entities;

public class Store
{
    public Guid StoreId { get; set; }
    
    [Required]
    [MaxLength(200)]
    public string Name { get; set; } = string.Empty;
    
    [MaxLength(1000)]
    public string? Description { get; set; }
    
    [Required]
    [MaxLength(500)]
    public string Address { get; set; } = string.Empty;
    
    public double Latitude { get; set; }
    
    public double Longitude { get; set; }
    
    [MaxLength(500)]
    public string? LogoUrl { get; set; }
    
    [MaxLength(500)]
    public string? BannerUrl { get; set; }
    
    public bool IsActive { get; set; } = true;
    
    public List<Guid> DeliveryZoneIds { get; set; } = new();
    
    public string OperatingHours { get; set; } = "{}"; // JSON
    
    public decimal AverageRating { get; set; } = 0;
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    
    public byte[] RowVersion { get; set; } = Array.Empty<byte>();
    
    // Navigation properties
    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
    public virtual ICollection<Promotion> Promotions { get; set; } = new List<Promotion>();
    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
    public virtual ICollection<DeliverySlot> DeliverySlots { get; set; } = new List<DeliverySlot>();
}