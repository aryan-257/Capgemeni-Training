using System;

namespace StudentManagmentSystemApp;

public class Student
{
    int rollNo;
    int phy;
    int chem;
    int math;
    int total;
    float percentage;  

    //CLR Properties

    public int RollNo
    {
        set
        {
            rollNo=value;
        }
        get
        {
            return rollNo;
        }
    }

    //Auto Implicit Property 

    public string Name{get;set;}

    public string Adress{get;set;}

    public int Total
    {
        get
        {
            return total;
        }
        set
        {
            total=value;
        }

    }
    
    public float Perc{get;set;}

    public int Phy
    {
        
        get
        {
            return phy;
        }

        set
        {
            if(value>=0 && value<=100)
            {
                phy=value;
            }
            else
            {
                throw new InvalidMarksException("Invalid Marks");
            }
        }
    }

    public int Chem
    {
        
        get
        {
            return chem;
        }

        set
        {
            if(value>=0 && value<=100)
            {
                chem=value;
            }
            else
            {
                throw new Exception("Invalid Marks");
            }
        }
    }

    public int Math
    {
        
        get
        {
            return math;
        }

        set
        {
            if(value>=0 && value<=100)
            {
                math=value;
            }
            else
            {
                throw new Exception("Invalid Marks");
            }
        }
    }

}

[Serializable]
internal class InvalidMarksException : Exception
{
    public InvalidMarksException()
    {
    }

    public InvalidMarksException(string? message) : base(message)
    {
    }

    public InvalidMarksException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}