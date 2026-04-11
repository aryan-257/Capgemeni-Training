namespace FreshCart.Application.DTOs;

public class ProductDto
{
    public Guid ProductId { get; set; }
    public Guid StoreId { get; set; }
    public string StoreName { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string? Brand { get; set; }
    public string? SKU { get; set; }
    public string? UPC { get; set; }
    public string? Description { get; set; }
    public Guid CategoryId { get; set; }
    public string CategoryName { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public decimal? CompareAtPrice { get; set; }
    public string Unit { get; set; } = string.Empty;
    public int StockQty { get; set; }
    public bool IsAvailable { get; set; }
    public List<string> DietaryTags { get; set; } = new();
    public List<string> ImageUrls { get; set; } = new();
    public decimal AverageRating { get; set; }
    public int TotalReviews { get; set; }
    public DateTime CreatedAt { get; set; }
}

public class CreateProductDto
{
    public Guid StoreId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Brand { get; set; }
    public string? SKU { get; set; }
    public string? UPC { get; set; }
    public string? Description { get; set; }
    public Guid CategoryId { get; set; }
    public decimal Price { get; set; }
    public decimal? CompareAtPrice { get; set; }
    public string Unit { get; set; } = "piece";
    public int StockQty { get; set; }
    public int LowStockThreshold { get; set; } = 10;
    public List<string> DietaryTags { get; set; } = new();
    public List<string> ImageUrls { get; set; } = new();
}

public class UpdateProductDto
{
    public string? Name { get; set; }
    public string? Brand { get; set; }
    public string? Description { get; set; }
    public decimal? Price { get; set; }
    public decimal? CompareAtPrice { get; set; }
    public int? StockQty { get; set; }
    public int? LowStockThreshold { get; set; }
    public bool? IsAvailable { get; set; }
    public List<string>? DietaryTags { get; set; }
    public List<string>? ImageUrls { get; set; }
}

public class ProductSearchDto
{
    public string? Query { get; set; }
    public Guid? StoreId { get; set; }
    public Guid? CategoryId { get; set; }
    public List<string>? DietaryTags { get; set; }
    public string? Brand { get; set; }
    public decimal? MinPrice { get; set; }
    public decimal? MaxPrice { get; set; }
    public decimal? MinRating { get; set; }
    public bool? InStockOnly { get; set; } = true;
    public string SortBy { get; set; } = "relevance"; // relevance, price_asc, price_desc, rating, popularity
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 24;
}