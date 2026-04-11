using System.ComponentModel.DataAnnotations;

namespace FreshCart.Domain.Entities;

public class RecurringList
{
    public Guid RecurringListId { get; set; }
    
    public Guid CustomerId { get; set; }
    
    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;
    
    public RecurringSchedule Schedule { get; set; }
    
    public DayOfWeek? DayOfWeek { get; set; }
    
    public DateTime NextRunAt { get; set; }
    
    public bool IsActive { get; set; } = true;
    
    public bool AutoCheckout { get; set; } = false;
    
    public decimal? BudgetCap { get; set; }
    
    [MaxLength(50)]
    public string? DefaultSlotPreference { get; set; } // e.g., "Morning", "Afternoon"
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    
    public byte[] RowVersion { get; set; } = Array.Empty<byte>();
    
    // Navigation properties
    public virtual User Customer { get; set; } = null!;
    public virtual ICollection<RecurringListItem> RecurringListItems { get; set; } = new List<RecurringListItem>();
}

public class RecurringListItem
{
    public Guid RecurringListItemId { get; set; }
    
    public Guid RecurringListId { get; set; }
    
    public Guid ProductId { get; set; }
    
    public int Quantity { get; set; }
    
    [MaxLength(500)]
    public string? SpecialInstructions { get; set; }
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    // Navigation properties
    public virtual RecurringList RecurringList { get; set; } = null!;
    public virtual Product Product { get; set; } = null!;
}

public enum RecurringSchedule
{
    Weekly = 0,
    BiWeekly = 1,
    Monthly = 2,
    Custom = 3
}