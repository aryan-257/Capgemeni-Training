using System.ComponentModel.DataAnnotations;

namespace EF_CORE_MVC_DEMO.Models
{
    public class Department
    {
        [Key]
        public int DepID { get; set; }
        [Required(ErrorMessage = "Mandatory")]
        public string Name { get; set; }
        [Required]
        public string Location { get; set; }
        public ICollection<Employee> Employees { get; set; }

    }
}
