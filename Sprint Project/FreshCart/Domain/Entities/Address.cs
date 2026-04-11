using System.ComponentModel.DataAnnotations;

namespace FreshCart.Domain.Entities;

public class Address
{
    public Guid AddressId { get; set; }
    
    public Guid UserId { get; set; }
    
    [Required]
    [MaxLength(100)]
    public string Label { get; set; } = string.Empty; // e.g., "Home", "Work"
    
    [Required]
    [MaxLength(200)]
    public string Street { get; set; } = string.Empty;
    
    [Required]
    [MaxLength(100)]
    public string City { get; set; } = string.Empty;
    
    [Required]
    [MaxLength(100)]
    public string State { get; set; } = string.Empty;
    
    [Required]
    [MaxLength(20)]
    public string PostalCode { get; set; } = string.Empty;
    
    [Required]
    [MaxLength(100)]
    public string Country { get; set; } = string.Empty;
    
    public double Latitude { get; set; }
    
    public double Longitude { get; set; }
    
    public bool IsDefault { get; set; } = false;
    
    [MaxLength(500)]
    public string? DeliveryInstructions { get; set; }
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    
    public byte[] RowVersion { get; set; } = Array.Empty<byte>();
    
    // Navigation properties
    public virtual User User { get; set; } = null!;
    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}