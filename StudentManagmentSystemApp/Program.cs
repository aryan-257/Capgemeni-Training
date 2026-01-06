// See https://aka.ms/new-console-template for more information
//Console.WriteLine("Hello, World!");

using StudentManagmentSystemApp;

StudentBL sBLObj = new StudentBL();
sBLObj.AcceptStudentDetails();
sBLObj.calcTotal();
sBLObj.calcPerc();
int t1;
float t2;
sBLObj.calcResult(out t1, out t2);

System.Console.WriteLine($"Total :{t1}");
System.Console.WriteLine($"Percentage :{t2}%");