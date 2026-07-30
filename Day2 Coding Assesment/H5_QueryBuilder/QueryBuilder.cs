using System;
using System.Text;

class QueryBuilder
{
    private StringBuilder query = new StringBuilder();
    private bool hasClause = false;

    // overload 1 - simple string clause
    public void AddWhereClause(string clause)
    {
        if (!hasClause)
        {
            query.Append("WHERE " + clause);
            hasClause = true;
        }
        else
        {
            query.Append("\nAND " + clause);
        }
    }

    // overload 2 - params of Action delegates for nested condition
    public void AddWhereClause(params Action<QueryBuilder>[] nestedConditions)
    {
        int indentLevel = 0; // ref se pass hoga formatting k liye

        // local recursive function nested conditions process krne k liye
        void ProcessNested(Action<QueryBuilder> condition, ref int indent)
        {
            var nestedBuilder = new QueryBuilder();
            condition(nestedBuilder);

            string indentation = new string(' ', indent * 2);

            query.Append("\n" + indentation + "AND (");
            query.Append("\n" + indentation + "  " + nestedBuilder.query.ToString().Replace("WHERE ", ""));
            query.Append("\n" + indentation + ")");
        }

        foreach (var cond in nestedConditions)
        {
            ProcessNested(cond, ref indentLevel);
        }
    }

    public override string ToString()
    {
        return query.ToString();
    }
}
