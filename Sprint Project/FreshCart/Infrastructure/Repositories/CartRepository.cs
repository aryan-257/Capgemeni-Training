using Microsoft.EntityFrameworkCore;
using FreshCart.Application.Commands.Cart;
using FreshCart.Domain.Entities;
using FreshCart.Infrastructure.Data;

namespace FreshCart.Infrastructure.Repositories;

public class CartRepository : ICartRepository
{
    private readonly FreshCartDbContext _context;

    public CartRepository(FreshCartDbContext context)
    {
        _context = context;
    }

    public async Task<Cart?> GetActiveCartAsync(Guid customerId, Guid storeId)
    {
        return await _context.Carts
            .Include(c => c.CartItems)
                .ThenInclude(ci => ci.Product)
            .Include(c => c.Store)
            .FirstOrDefaultAsync(c => c.CustomerId == customerId && 
                               c.StoreId == storeId && 
                               c.Status == CartStatus.Active);
    }

    public async Task<Cart?> GetByIdAsync(Guid cartId)
    {
        return await _context.Carts
            .Include(c => c.CartItems)
                .ThenInclude(ci => ci.Product)
            .Include(c => c.Store)
            .FirstOrDefaultAsync(c => c.CartId == cartId);
    }

    public async Task<Cart> CreateAsync(Cart cart)
    {
        _context.Carts.Add(cart);
        await _context.SaveChangesAsync();
        return cart;
    }

    public async Task<Cart> UpdateAsync(Cart cart)
    {
        cart.UpdatedAt = DateTime.UtcNow;
        _context.Carts.Update(cart);
        await _context.SaveChangesAsync();
        return cart;
    }

    public async Task DeleteAsync(Guid cartId)
    {
        var cart = await _context.Carts.FindAsync(cartId);
        if (cart != null)
        {
            _context.Carts.Remove(cart);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<Cart?> GetMostRecentActiveCartAsync(Guid customerId)
    {
        return await _context.Carts
            .Include(c => c.CartItems)
                .ThenInclude(ci => ci.Product)
            .Include(c => c.Store)
            .Where(c => c.CustomerId == customerId && c.Status == CartStatus.Active)
            .OrderByDescending(c => c.UpdatedAt)
            .FirstOrDefaultAsync();
    }
}