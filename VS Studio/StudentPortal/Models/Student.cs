using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StudentPortal.Models;

public partial class Student
{
    public int StudentId { get; set; }

    [Required(ErrorMessage = "Full Name is required")]
    [StringLength(120, MinimumLength = 2, ErrorMessage = "Full Name must be between 2 and 120 characters")]
    [Display(Name = "Full Name")]
    public string FullName { get; set; } = null!;

    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid email address")]
    [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", 
        ErrorMessage = "Please enter a valid email address (e.g., user@example.com)")]
    [StringLength(180, ErrorMessage = "Email cannot exceed 180 characters")]
    public string Email { get; set; } = null!;

    [Phone(ErrorMessage = "Invalid phone number")]
    [RegularExpression(@"^[\+]?[(]?[0-9]{1,4}[)]?[-\s\.]?[(]?[0-9]{1,4}[)]?[-\s\.]?[0-9]{1,9}$", 
        ErrorMessage = "Please enter a valid phone number (e.g., +919876543210 or 9876543210)")]
    [StringLength(30, ErrorMessage = "Phone cannot exceed 30 characters")]
    public string? Phone { get; set; }

    [StringLength(20)]
    public string Status { get; set; } = "Active";

    [Required(ErrorMessage = "Join Date is required")]
    [Display(Name = "Join Date")]
    [DataType(DataType.Date)]
    public DateOnly JoinDate { get; set; }

    [Display(Name = "Created At")]
    public DateTime CreatedAt { get; set; }

    public virtual ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();

    public virtual ICollection<TblLog> TblLogs { get; set; } = new List<TblLog>();
}
