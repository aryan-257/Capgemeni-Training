using System;

namespace Day4OOPDemo;

class Employee
{
    #region Properties
    public int empId { get; set; }
    public string name { get; set; }

    #endregion

    public virtual int CalculateSalary(int sal)
    {
        int mySal = 0;
        mySal = sal + 15000 + 3000 + 1500 - 2500;
        return mySal;
    }
}


