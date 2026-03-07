using EF_CoreApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace EF_CoreApplication.Data
{
    public class LibraryDbContext : DbContext
    {
        public LibraryDbContext(DbContextOptions<LibraryDbContext> options) : base(options)
        {
        }

        public DbSet<Book> Books { get; set; }
    }
}
