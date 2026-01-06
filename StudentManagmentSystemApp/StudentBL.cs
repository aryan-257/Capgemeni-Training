using System;

namespace StudentManagmentSystemApp;

public class StudentBL
{
    Student sObj=null;
    public StudentBL()
    {
        sObj = new Student();
    }
    public void AcceptStudentDetails()


    {
        Console.ForegroundColor=ConsoleColor.Green;
        System.Console.WriteLine("Student Managment System: ");
        System.Console.WriteLine("==========================");

        Console.ForegroundColor=ConsoleColor.Yellow;
        try{
            System.Console.WriteLine("Enter Roll No: ");
            sObj.RollNo=Convert.ToInt32(Console.ReadLine());

            System.Console.WriteLine("Enter Name: ");
            sObj.Name=Console.ReadLine();

            System.Console.WriteLine("Enter Address: ");
            sObj.Adress=Console.ReadLine();

            System.Console.WriteLine("Enter Physics Marks: ");
            sObj.Phy=Convert.ToInt32(Console.ReadLine());

            System.Console.WriteLine("Enter Chemistry Marks: ");
            sObj.Chem=Convert.ToInt32(Console.ReadLine());

            System.Console.WriteLine("Enter Maths Marks: ");
            sObj.Math=Convert.ToInt32(Console.ReadLine());
        }
        catch(InvalidMarksException e)
        {
            Console.ForegroundColor=ConsoleColor.Red;
            System.Console.WriteLine(e.Message);
        }
        catch(Exception e)
        {
            Console.ForegroundColor=ConsoleColor.Red;
            System.Console.WriteLine(e.Message);
        }

        Console.ForegroundColor=ConsoleColor.White;


    }

    public int calcTotal()
    {
        sObj.Total=sObj.Phy + sObj.Chem + sObj.Math;
        return sObj.Total;
    }

    public float calcPerc()
    {
        sObj.Perc = (calcTotal() / 300.0f) * 100;
        return sObj.Perc; 
    }

    public void calcResult(out int total, out float percentage)
    {
        total=calcTotal();
        percentage=calcPerc();
    }

}
