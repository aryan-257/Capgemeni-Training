using System.ComponentModel.DataAnnotations;

namespace FreshCart.Domain.Entities;

public class DeliverySlot
{
    public Guid SlotId { get; set; }
    
    public Guid StoreId { get; set; }
    
    public Guid ZoneId { get; set; }
    
    public TimeSpan StartTime { get; set; }
    
    public TimeSpan EndTime { get; set; }
    
    public int MaxCapacity { get; set; }
    
    public int CurrentBookings { get; set; } = 0;
    
    public bool IsExpressSlot { get; set; } = false;
    
    public decimal DeliveryFee { get; set; }
    
    public DateOnly Date { get; set; }
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    
    public byte[] RowVersion { get; set; } = Array.Empty<byte>();
    
    // Navigation properties
    public virtual Store Store { get; set; } = null!;
    public virtual DeliveryZone Zone { get; set; } = null!;
    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}

public class DeliveryZone
{
    public Guid ZoneId { get; set; }
    
    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;
    
    public string PolygonCoordinates { get; set; } = "[]"; // JSON array of coordinates
    
    public decimal BaseDeliveryFee { get; set; }
    
    public decimal ExpressFeeAdd { get; set; }
    
    public bool IsActive { get; set; } = true;
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    
    public byte[] RowVersion { get; set; } = Array.Empty<byte>();
    
    // Navigation properties
    public virtual ICollection<DeliverySlot> DeliverySlots { get; set; } = new List<DeliverySlot>();
}