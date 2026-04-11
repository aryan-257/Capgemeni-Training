using System.ComponentModel.DataAnnotations;

namespace FreshCart.Domain.Entities;

public class User
{
    public Guid UserId { get; set; }
    
    [Required]
    [MaxLength(100)]
    public string FullName { get; set; } = string.Empty;
    
    [Required]
    [EmailAddress]
    [MaxLength(255)]
    public string Email { get; set; } = string.Empty;
    
    [Required]
    public string PasswordHash { get; set; } = string.Empty;
    
    [MaxLength(20)]
    public string? Mobile { get; set; }
    
    public UserRole Role { get; set; } = UserRole.Customer;
    
    public bool IsVerified { get; set; } = false;
    
    public bool IsBanned { get; set; } = false;
    
    public bool IsAvailable { get; set; } = true; // For drivers
    
    public int LoyaltyPoints { get; set; } = 0;
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    
    public byte[] RowVersion { get; set; } = Array.Empty<byte>();
    
    // Navigation properties
    public virtual ICollection<Address> Addresses { get; set; } = new List<Address>();
    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
    public virtual ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();
    public virtual ICollection<RecurringList> RecurringLists { get; set; } = new List<RecurringList>();
    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();
    public virtual ICollection<DriverAssignment> DriverAssignments { get; set; } = new List<DriverAssignment>();
}

public enum UserRole
{
    Customer = 0,
    StoreManager = 1,
    Driver = 2,
    Admin = 3
}