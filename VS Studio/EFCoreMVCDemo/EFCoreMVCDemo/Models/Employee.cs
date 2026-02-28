using System.ComponentModel.DataAnnotations;

namespace EFCoreMVCDemo.Models
{
    public class Employee
    {
        [Key]
        public int EmpId { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public int DeptId { get; set; }

        //Navigation property to the Department entity
        public virtual Department Department { get; set; }
    }
}
