using System;

class Program
{
    static void Main()
    {
        var builder = new QueryBuilder();

        builder.AddWhereClause("Status = 'Active'");

        builder.AddWhereClause(nested =>
        {
            nested.AddWhereClause("Age > 18");
            nested.AddWhereClause("Age < 65");
        });

        Console.WriteLine("Generated SQL:");
        Console.WriteLine(builder.ToString());
    }
}
