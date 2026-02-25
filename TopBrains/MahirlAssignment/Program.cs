using System;
using System.Collections.Generic;

class ConsonantHelper
{
    public static bool IsVowel(char ch)
    {
        ch = Char.ToLower(ch);

        return ch == 'a' || ch == 'e' || ch == 'i' || ch == 'o' || ch == 'u';
    }
}

class StringProcessor
{
    public static string RemoveCommonConsonants(string first , string second)
    {
        HashSet<char> letters = new HashSet<char>();

        foreach (char c in second)
        {
            letters.Add(Char.ToLower(c));
        }

        string temp = "";

        foreach (char c in first)
        {
            char lower = Char.ToLower(c);

            if (!ConsonantHelper.IsVowel(lower) && letters.Contains(lower))
            {
                continue;
            }

            temp += c;
        }

        return temp;
    }

    public static string RemoveConsecutiveDuplicates(string str)
    {
        if (str.Length == 0)
            return str;

        string result = "";
        result += str[0];

        for (int i = 1; i < str.Length; i++)
        {
            if (str[i] != str[i - 1])
            {
                result += str[i];
            }
        }

        return result;
    }
}

class Program
{
    static void Main()
    {
        string firstWord = Console.ReadLine();
        string secondWord = Console.ReadLine();

        string step1 = StringProcessor.RemoveCommonConsonants(firstWord , secondWord);
        string finalAnswer = StringProcessor.RemoveConsecutiveDuplicates(step1);

        Console.WriteLine(finalAnswer);
    }
}
