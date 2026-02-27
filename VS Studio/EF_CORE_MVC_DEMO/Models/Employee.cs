namespace EF_CORE_MVC_DEMO.Models
{
    public class Employee
    {
        [Key]
        public int EmpID { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Address { get; set; }
        public int DeptID { get; set; }
        //Navigation property to the Department
        public virtual Department Department { get; set; }
    }
}
