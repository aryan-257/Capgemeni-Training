namespace FreshCart.Application.DTOs;

public class CartDto
{
    public Guid CartId { get; set; }
    public Guid CustomerId { get; set; }
    public Guid StoreId { get; set; }
    public string StoreName { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public decimal? BudgetTarget { get; set; }
    public decimal Subtotal { get; set; }
    public decimal? BudgetRemaining { get; set; }
    public int TotalItems { get; set; }
    public List<CartItemDto> Items { get; set; } = new();
    public DateTime UpdatedAt { get; set; }
}

public class CartItemDto
{
    public Guid CartItemId { get; set; }
    public Guid ProductId { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public string? ProductBrand { get; set; }
    public string ProductImageUrl { get; set; } = string.Empty;
    public decimal UnitPrice { get; set; }
    public decimal UnitPriceAtAdd { get; set; }
    public string Unit { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public decimal LineTotal { get; set; }
    public bool IsSubstituted { get; set; }
    public bool IsOutOfStock { get; set; }
    public string? SpecialInstructions { get; set; }
    public DateTime CreatedAt { get; set; }
}

public class AddToCartDto
{
    public Guid ProductId { get; set; }
    public int Quantity { get; set; } = 1;
    public string? SpecialInstructions { get; set; }
}

public class UpdateCartItemDto
{
    public int Quantity { get; set; }
    public string? SpecialInstructions { get; set; }
}

public class SetBudgetDto
{
    public decimal? BudgetTarget { get; set; }
}

public class SubstituteProductDto
{
    public Guid RemoveItemId { get; set; }
    public Guid AddProductId { get; set; }
    public int Quantity { get; set; } = 1;
}

public class ApplyBundleDto
{
    public Guid BundlePromotionId { get; set; }
    public List<Guid> AdditionalProductIds { get; set; } = new();
}

public class VoiceAddDto
{
    public string Transcript { get; set; } = string.Empty;
}