using System.ComponentModel.DataAnnotations;

namespace StudentManagementSystem.ViewModels
{
    public class StudentProfileViewModel
    {
        public int StudentId { get; set; }
        
        [Display(Name = "Student Name")]
        public string StudentName { get; set; } = string.Empty;
        
        [Display(Name = "Email")]
        public string Email { get; set; } = string.Empty;
        
        [Required]
        [StringLength(20)]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; } = string.Empty;
        
        [Required]
        [StringLength(500)]
        [Display(Name = "Address")]
        public string Address { get; set; } = string.Empty;
        
        [Display(Name = "Department")]
        public string DepartmentName { get; set; } = string.Empty;
        
        [Display(Name = "Course")]
        public string CourseName { get; set; } = string.Empty;
        
        [Display(Name = "Duration")]
        public string Duration { get; set; } = string.Empty;
        
        [Display(Name = "Fees")]
        public decimal Fees { get; set; }
    }
}