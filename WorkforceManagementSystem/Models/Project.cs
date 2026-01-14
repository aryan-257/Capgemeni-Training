using System;

namespace WorkforceManagementSystem.Models
{
    public class Project
    {
        public int ProjectId { get; set; }
        public int EmployeeId { get; set; }
        public string ProjectName { get; set; }
    }
}
