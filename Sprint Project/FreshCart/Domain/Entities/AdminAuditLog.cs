using System.ComponentModel.DataAnnotations;

namespace FreshCart.Domain.Entities;

public class AdminAuditLog
{
    public Guid LogId { get; set; }
    
    public Guid AdminId { get; set; }
    
    [Required]
    [MaxLength(100)]
    public string Action { get; set; } = string.Empty;
    
    [Required]
    [MaxLength(50)]
    public string EntityType { get; set; } = string.Empty;
    
    public Guid EntityId { get; set; }
    
    public string? PreviousValue { get; set; } // JSON
    
    public string? NewValue { get; set; } // JSON
    
    [MaxLength(1000)]
    public string? Rationale { get; set; }
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    // Navigation properties
    public virtual User Admin { get; set; } = null!;
}