using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentManagementSystem.Models
{
    public class Student
    {
        public int StudentId { get; set; }
        
        [Required]
        [StringLength(100)]
        public string StudentName { get; set; } = string.Empty;
        
        [Required]
        [EmailAddress]
        [StringLength(100)]
        public string Email { get; set; } = string.Empty;
        
        [Required]
        [StringLength(20)]
        public string PhoneNumber { get; set; } = string.Empty;
        
        [Required]
        [StringLength(500)]
        public string Address { get; set; } = string.Empty;
        
        [Required]
        public int DepartmentId { get; set; }
        
        [Required]
        public int CourseId { get; set; }
        
        // Navigation properties
        [ForeignKey("DepartmentId")]
        public virtual Department Department { get; set; } = null!;
        
        [ForeignKey("CourseId")]
        public virtual Course Course { get; set; } = null!;
    }
}