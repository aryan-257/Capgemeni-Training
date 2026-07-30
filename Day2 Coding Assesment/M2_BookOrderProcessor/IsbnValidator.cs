using System;

static class IsbnValidator
{
    // simple TryParse jaisa method - ISBN clean karke check karta h 13 char ka h ya nhi
    public static bool TryParseISBN(string rawIsbn, out string cleanedIsbn)
    {
        cleanedIsbn = rawIsbn.Replace("-", "").Trim();

        if (cleanedIsbn.Length == 13)
        {
            return true;
        }

        cleanedIsbn = string.Empty;
        return false;
    }
}
