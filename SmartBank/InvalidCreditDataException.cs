using System;

public class InvalidCreditDataException : Exception
{
    public InvalidCreditDataException(string message) : base(message)
    {
    }
}
