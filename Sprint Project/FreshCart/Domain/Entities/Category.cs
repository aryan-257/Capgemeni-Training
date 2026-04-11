using System.ComponentModel.DataAnnotations;

namespace FreshCart.Domain.Entities;

public class Category
{
    public Guid CategoryId { get; set; }
    
    public Guid? ParentCategoryId { get; set; }
    
    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;
    
    [Required]
    [MaxLength(100)]
    public string Slug { get; set; } = string.Empty;
    
    public int Level { get; set; } = 0; // 0=Department, 1=Category, 2=SubCategory
    
    [MaxLength(500)]
    public string? IconUrl { get; set; }
    
    public int SortOrder { get; set; } = 0;
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    
    public byte[] RowVersion { get; set; } = Array.Empty<byte>();
    
    // Navigation properties
    public virtual Category? ParentCategory { get; set; }
    public virtual ICollection<Category> SubCategories { get; set; } = new List<Category>();
    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}