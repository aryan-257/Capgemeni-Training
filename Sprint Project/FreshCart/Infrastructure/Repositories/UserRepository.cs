using Microsoft.EntityFrameworkCore;
using FreshCart.Application.Commands.Auth;
using FreshCart.Domain.Entities;
using FreshCart.Infrastructure.Data;

namespace FreshCart.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly FreshCartDbContext _context;

    public UserRepository(FreshCartDbContext context)
    {
        _context = context;
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        return await _context.Users
            .FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task<User?> GetByIdAsync(Guid userId)
    {
        return await _context.Users
            .FirstOrDefaultAsync(u => u.UserId == userId);
    }

    public async Task<User> CreateAsync(User user)
    {
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        return user;
    }

    public async Task<User> UpdateAsync(User user)
    {
        user.UpdatedAt = DateTime.UtcNow;
        _context.Users.Update(user);
        await _context.SaveChangesAsync();
        return user;
    }
}