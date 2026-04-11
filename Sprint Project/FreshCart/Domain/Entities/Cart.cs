using System.ComponentModel.DataAnnotations;

namespace FreshCart.Domain.Entities;

public class Cart
{
    public Guid CartId { get; set; }
    
    public Guid CustomerId { get; set; }
    
    public Guid StoreId { get; set; }
    
    public CartStatus Status { get; set; } = CartStatus.Active;
    
    public decimal? BudgetTarget { get; set; }
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    
    public byte[] RowVersion { get; set; } = Array.Empty<byte>();
    
    // Navigation properties
    public virtual User Customer { get; set; } = null!;
    public virtual Store Store { get; set; } = null!;
    public virtual ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();
}

public class CartItem
{
    public Guid CartItemId { get; set; }
    
    public Guid CartId { get; set; }
    
    public Guid ProductId { get; set; }
    
    public int Quantity { get; set; }
    
    public decimal UnitPriceAtAdd { get; set; }
    
    public bool IsSubstituted { get; set; } = false;
    
    [MaxLength(500)]
    public string? SpecialInstructions { get; set; }
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    
    // Navigation properties
    public virtual Cart Cart { get; set; } = null!;
    public virtual Product Product { get; set; } = null!;
}

public enum CartStatus
{
    Active = 0,
    CheckedOut = 1,
    Abandoned = 2
}