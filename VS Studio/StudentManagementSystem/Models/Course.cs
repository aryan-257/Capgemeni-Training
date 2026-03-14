using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentManagementSystem.Models
{
    public class Course
    {
        public int CourseId { get; set; }
        
        [Required]
        [StringLength(100)]
        public string CourseName { get; set; } = string.Empty;
        
        [Required]
        [StringLength(50)]
        public string Duration { get; set; } = string.Empty;
        
        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Fees { get; set; }
        
        [Required]
        public int DepartmentId { get; set; }
        
        // Navigation properties
        [ForeignKey("DepartmentId")]
        public virtual Department Department { get; set; } = null!;
        public virtual ICollection<Student> Students { get; set; } = new List<Student>();
    }
}