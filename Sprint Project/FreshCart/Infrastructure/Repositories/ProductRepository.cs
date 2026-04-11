using Microsoft.EntityFrameworkCore;
using FreshCart.Application.Commands.Cart;
using FreshCart.Application.DTOs;
using FreshCart.Domain.Entities;
using FreshCart.Infrastructure.Data;

namespace FreshCart.Infrastructure.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly FreshCartDbContext _context;

    public ProductRepository(FreshCartDbContext context)
    {
        _context = context;
    }

    public async Task<Product?> GetByIdAsync(Guid productId)
    {
        return await _context.Products
            .Include(p => p.Store)
            .Include(p => p.Category)
            .FirstOrDefaultAsync(p => p.ProductId == productId);
    }

    public async Task<IEnumerable<Product>> SearchAsync(ProductSearchDto searchDto)
    {
        var query = _context.Products
            .Include(p => p.Store)
            .Include(p => p.Category)
            .AsQueryable();

        if (!string.IsNullOrEmpty(searchDto.Query))
        {
            query = query.Where(p => p.Name.Contains(searchDto.Query) || 
                                   p.Brand!.Contains(searchDto.Query) ||
                                   p.Description!.Contains(searchDto.Query));
        }

        if (searchDto.StoreId.HasValue)
        {
            query = query.Where(p => p.StoreId == searchDto.StoreId.Value);
        }

        if (searchDto.CategoryId.HasValue)
        {
            query = query.Where(p => p.CategoryId == searchDto.CategoryId.Value);
        }

        if (searchDto.InStockOnly == true)
        {
            query = query.Where(p => p.StockQty > 0 && p.IsAvailable);
        }

        return await query.ToListAsync();
    }

    public async Task<Product> CreateAsync(Product product)
    {
        _context.Products.Add(product);
        await _context.SaveChangesAsync();
        return product;
    }

    public async Task<Product> UpdateAsync(Product product)
    {
        product.UpdatedAt = DateTime.UtcNow;
        _context.Products.Update(product);
        await _context.SaveChangesAsync();
        return product;
    }
}