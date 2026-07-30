using System;
using System.Collections.Generic;

static class RiskCalculator
{
    private const int MaxDepth = 1000;

    public static int CalculateRiskScore(string transactionId)
    {
        // pehle input validate krte h
        if (!TryParseTransactionId(transactionId, out string cleanId))
        {
            Console.WriteLine("Invalid transaction ID format.");
            return -1;
        }

        int depth = 0;
        var visited = new HashSet<string>(); // circular reference detect krne k liye

        return CalculateRecursive(cleanId, ref depth, visited);
    }

    private static bool TryParseTransactionId(string id, out string cleanId)
    {
        // simple validation - TX se start hona chahiye
        if (!string.IsNullOrEmpty(id) && id.StartsWith("TX"))
        {
            cleanId = id.Trim();
            return true;
        }

        cleanId = string.Empty;
        return false;
    }

    private static int CalculateRecursive(string transactionId, ref int depth, HashSet<string> visited)
    {
        // depth limit cross ho gyi to stack overflow rokne k liye ruk jao
        if (depth >= MaxDepth)
        {
            Console.WriteLine("Warning: Maximum recursion depth exceeded at " + transactionId);
            return -1;
        }

        if (visited.Contains(transactionId))
        {
            // circular reference mil gyi, wapis chla jao
            return 0;
        }

        visited.Add(transactionId);
        depth = depth + 1;

        // yahan real me next transaction fetch hoga, demo k liye simple rakha h
        // agar chain aage badhti to yahi recursive call hoti
        return depth; // dummy score, actual logic project k hisab se badlega
    }
}
