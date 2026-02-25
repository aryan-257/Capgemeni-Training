using System.Globalization;

namespace FILEIODemo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            DirectoryDemo dirObj = new DirectoryDemo();

            string directoryName = @"D:\Capgemini\LPU";
            dirObj.DirectoryDemoFunc(directoryName);

            dirObj.DriveInfoFunc("D:\\");

            dirObj.PathDemoFunc();
        }
    }
}