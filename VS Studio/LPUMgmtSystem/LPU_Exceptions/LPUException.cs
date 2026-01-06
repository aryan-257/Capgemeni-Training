using System;
namespace LPU_Exceptions
{
    /// <summary>
    /// Custom Exception Class for LPU Management System
    /// By Aryan on 29/12/2025 at 11:34 AM
    /// </summary>
    public class LPUException : Exception
    {
        public LPUException() : base()
        {
            
        }

        public LPUException(string message) : base(message)
        {
            
        }

        public LPUException(string message, Exception innerException) : base(message, innerException)
        {
            
        }


    }
}
