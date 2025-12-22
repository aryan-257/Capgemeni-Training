using System;

namespace Day4OOPDemo;

class Manager : Employee
{
    public int managerId { get; set; }
    public int incentive { get; set; }
    public override int CalculateSalary(int sal)
    {
        int mySal = 0;
        mySal = sal + 20000 + 5000 + 2500 - 3000;
        return mySal;
    }
}
