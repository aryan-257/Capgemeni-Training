using Microsoft.EntityFrameworkCore;
using StudentManagementSystem.Models;

namespace StudentManagementSystem.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        
        public DbSet<User> Users { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Student> Students { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            // Configure relationships
            modelBuilder.Entity<Course>()
                .HasOne(c => c.Department)
                .WithMany(d => d.Courses)
                .HasForeignKey(c => c.DepartmentId)
                .OnDelete(DeleteBehavior.Restrict);
                
            modelBuilder.Entity<Student>()
                .HasOne(s => s.Department)
                .WithMany(d => d.Students)
                .HasForeignKey(s => s.DepartmentId)
                .OnDelete(DeleteBehavior.Restrict);
                
            modelBuilder.Entity<Student>()
                .HasOne(s => s.Course)
                .WithMany(c => c.Students)
                .HasForeignKey(s => s.CourseId)
                .OnDelete(DeleteBehavior.Restrict);
                
            // Seed data
            modelBuilder.Entity<Department>().HasData(
                new Department { DepartmentId = 1, DepartmentName = "Computer Science", Description = "Computer Science and IT related courses" },
                new Department { DepartmentId = 2, DepartmentName = "Business Administration", Description = "Business and Management courses" }
            );
            
            modelBuilder.Entity<Course>().HasData(
                new Course { CourseId = 1, CourseName = "Bachelor of Computer Science", Duration = "4 Years", Fees = 50000, DepartmentId = 1 },
                new Course { CourseId = 2, CourseName = "Master of Computer Applications", Duration = "2 Years", Fees = 40000, DepartmentId = 1 },
                new Course { CourseId = 3, CourseName = "Bachelor of Business Administration", Duration = "3 Years", Fees = 35000, DepartmentId = 2 }
            );
        }
    }
}