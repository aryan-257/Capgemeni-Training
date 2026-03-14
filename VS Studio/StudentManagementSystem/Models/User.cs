using System.ComponentModel.DataAnnotations;

namespace StudentManagementSystem.Models
{
    public class User
    {
        public int UserId { get; set; }
        
        [Required]
        [StringLength(100)]
        public string FullName { get; set; } = string.Empty;
        
        [Required]
        [EmailAddress]
        [StringLength(100)]
        public string Email { get; set; } = string.Empty;
        
        [Required]
        [StringLength(255)]
        public string Password { get; set; } = string.Empty;
        
        [Required]
        [StringLength(20)]
        public string Role { get; set; } = string.Empty; // Teacher or Student
    }
}