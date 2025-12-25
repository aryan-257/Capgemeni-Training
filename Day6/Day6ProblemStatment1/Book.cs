using System;

namespace Day6ProblemStatment1;

public class Book
{
    #region fields
    public string title;
    public string author;
    public int numPages;
    public DateTime duedate;
    public DateTime returnDate;
    #endregion

    public Book()
    {
        
    }

    public Book(string title, string author, int numPages, DateTime duedate, DateTime returnDate)
    {
        this.title = title;
        this.author = author;
        this.numPages = numPages;
        this.duedate = duedate;
        this.returnDate = returnDate;
    }

    public double AveragePagesPerDay(int daysToRead)
    {
        return (double) numPages / daysToRead;
    }

    public double CalculateLateFee(double dailyLateFeeRate)
    {
        TimeSpan difference = returnDate - duedate;
        int daysLate =difference.Days;
        if (daysLate <= 0)
        {
            return 0;
        }
        return daysLate * dailyLateFeeRate;
    }
}


    
