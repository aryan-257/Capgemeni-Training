using Microsoft.EntityFrameworkCore;

namespace EFCoreMVCDemo.Models
{
    public class LpuTrialDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("server=.\\sqlexpress;Trusted_Connection=True;Database=LpuTrial;TrustServerCertificate=true");
        }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }
    }
}
