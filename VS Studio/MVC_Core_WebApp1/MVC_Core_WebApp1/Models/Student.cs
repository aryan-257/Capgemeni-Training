using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace MVC_Core_WebApp1.Models
{
    public class Student
    {
        [Required(ErrorMessage = "Roll Can't be Left Blank")]
        public int RollNo {  get; set; }

        [Required(ErrorMessage ="Name Can't be Left Blank")]
        [StringLength(15, MinimumLength = 2, ErrorMessage = "Name min Length is 2 and max lengh is 15")]
        public string Name {  get; set; }

        [Range(18,60, ErrorMessage ="Age is Invalid")]
        public int Age {  get; set; }


        public string Gender {  get; set; }
        public string Address {  get; set; }
    }
}
