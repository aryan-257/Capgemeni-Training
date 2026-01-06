using Day2DemoConsole;

Console.WriteLine("Hello, World!");

Student sObj = new Student(123, "Aryan", "Haryana");

sObj.DisplayDetails(sObj);

sObj = new Student(123,"Aryan","Delhi");

sObj.DisplayDetails(sObj);

string[] cities={"Pune","Chennai","Agra","Amritisar","mumbai"};

int i =0;
while(i<cities.Length)
{
    System.Console.WriteLine(cities[i]);
    i++;
}