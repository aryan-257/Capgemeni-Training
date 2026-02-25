using System.IO;
public class FileStreamDemo
{
   FileStream fs = null;
   public void CreateFile(string fileName)
   {
       fs = new FileStream(fileName, FileMode.Create, FileAccess.Write);
       StreamWriter sw = new StreamWriter(fs);
       sw.WriteLine("This is a sample text written to the file.");
       sw.Close();
       fs.Close();
   }

   public void ReadFile(string fileName)
   {
       fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
       StreamReader sr = new StreamReader(fs);
       string content = sr.ReadToEnd();
       Console.WriteLine("File Content: ");
       Console.WriteLine(content);
       sr.Close();
       fs.Close();
   }
}