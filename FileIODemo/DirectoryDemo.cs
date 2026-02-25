using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace FILEIODemo
{
    public class DirectoryDemo
    {
        public void DirectoryDemoFunc(string directoryName)
        {
            if (Directory.Exists(directoryName))
            {
                Console.WriteLine("Folder already exists.");
            }
            else
            {
                Directory.CreateDirectory(directoryName);
                Console.WriteLine("Folder Created . . .");
            }
        }

        public void DriveInfoFunc(string driveName)
        {
            DriveInfo driveInfo = new DriveInfo(driveName);
            Console.WriteLine("Drive Name: "+driveInfo.Name);
            Console.WriteLine("Drive FileSystem: "+driveInfo.DriveFormat);
            Console.WriteLine("Drive Size: "+driveInfo.TotalSize);
            Console.WriteLine("Drive Free Space: "+driveInfo.AvailableFreeSpace);

        }

        public void PathDemoFunc()
        {
            string s = @"D:\Downloads\batman";
            Console.WriteLine(Path.GetFileName(s));
            Console.WriteLine(Path.GetTempPath);
        }
    }
}