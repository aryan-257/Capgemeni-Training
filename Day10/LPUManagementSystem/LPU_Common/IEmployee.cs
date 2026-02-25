using System;
using System.Collections.Generic;
using System.Text;

namespace LPU_Common
{
    interface IEmployee<T> : IRepo<T>
    {
        List<T> ShowAllMaleEmployees();
        List<T> ShowAllFemaleEmployees();
        List<T> ShowAllEmployeeWithAgeAbove40();
    }
}
