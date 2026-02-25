using System;

public class ArithmeticEvaluator
{
    public static string Evaluate(string expression)
    {
        if (string.IsNullOrWhiteSpace(expression))
            return "Error:InvalidExpression";

        string[] parts = expression.Split(' ');

        // Must be exactly: a op b
        if (parts.Length != 3)
            return "Error:InvalidExpression";

        // Parse numbers
        if (!int.TryParse(parts[0], out int a) || !int.TryParse(parts[2], out int b))
            return "Error:InvalidNumber";

        string op = parts[1];

        switch (op)
        {
            case "+":
                return (a + b).ToString();

            case "-":
                return (a - b).ToString();

            case "*":
                return (a * b).ToString();

            case "/":
                if (b == 0)
                    return "Error:DivideByZero";
                return (a / b).ToString();

            default:
                return "Error:UnknownOperator";
        }
    }
}
