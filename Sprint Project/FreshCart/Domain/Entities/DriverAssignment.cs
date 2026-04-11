using System.ComponentModel.DataAnnotations;

namespace FreshCart.Domain.Entities;

public class DriverAssignment
{
    public Guid AssignmentId { get; set; }
    
    public Guid OrderId { get; set; }
    
    public Guid DriverId { get; set; }
    
    public AssignmentStatus Status { get; set; } = AssignmentStatus.Pending;
    
    public DateTime AssignedAt { get; set; } = DateTime.UtcNow;
    
    public DateTime? AcceptedAt { get; set; }
    
    public DateTime? PickedUpAt { get; set; }
    
    public DateTime? DeliveredAt { get; set; }
    
    [MaxLength(500)]
    public string? ProofPhotoUrl { get; set; }
    
    [MaxLength(1000)]
    public string? DriverNotes { get; set; }
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    
    public byte[] RowVersion { get; set; } = Array.Empty<byte>();
    
    // Navigation properties
    public virtual Order Order { get; set; } = null!;
    public virtual User Driver { get; set; } = null!;
    public virtual ICollection<DriverLocation> DriverLocations { get; set; } = new List<DriverLocation>();
}

public class DriverLocation
{
    public Guid LocationId { get; set; }
    
    public Guid DriverId { get; set; }
    
    public Guid OrderId { get; set; }
    
    public double Latitude { get; set; }
    
    public double Longitude { get; set; }
    
    public DateTime RecordedAt { get; set; } = DateTime.UtcNow;
    
    public int ETDMinutes { get; set; }
    
    // Navigation properties
    public virtual User Driver { get; set; } = null!;
    public virtual Order Order { get; set; } = null!;
}

public enum AssignmentStatus
{
    Pending = 0,
    Accepted = 1,
    Declined = 2,
    Completed = 3
}