using System.ComponentModel.DataAnnotations;

namespace EFCoreMVCDemo.Models
{
    public class Department
    {
        [Key]
        public int DeptId { get; set; }
        [Required(ErrorMessage="Name is required*")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Location is required*")]
        public string Location { get; set; }

       // public ICollection<Employee> Employees { get; set; }
    }
}
