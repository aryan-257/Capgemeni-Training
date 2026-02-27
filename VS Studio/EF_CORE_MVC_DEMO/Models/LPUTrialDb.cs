using Microsoft.EntityFrameworkCore;

namespace EF_CORE_MVC_DEMO.Models
{
    public class LPUTrialDb : DbContext // DbContext is for DataBase and DbSet is for Tables(which are inside database)
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=.\\sqlexpress; Trusted_Connection = True; Database = LPUTrialDb; TrustServerCertificate = true");

        }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }
    }
}
