using System.ComponentModel.DataAnnotations;

namespace StudentManagementSystem.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [StringLength(100)]
        [Display(Name = "Full Name")]
        public string FullName { get; set; } = string.Empty;
        
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; } = string.Empty;
        
        [Required]
        [StringLength(100, MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; } = string.Empty;
        
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; } = string.Empty;
        
        [Required]
        [Display(Name = "Role")]
        public string Role { get; set; } = string.Empty;
        
        // Additional fields for Student registration
        [StringLength(20)]
        [Display(Name = "Phone Number")]
        public string? PhoneNumber { get; set; }
        
        [StringLength(500)]
        [Display(Name = "Address")]
        public string? Address { get; set; }
        
        [Display(Name = "Department")]
        public int? DepartmentId { get; set; }
        
        [Display(Name = "Course")]
        public int? CourseId { get; set; }
    }
}