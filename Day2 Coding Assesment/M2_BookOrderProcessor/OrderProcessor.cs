using System;
using System.Collections.Generic;

static class OrderProcessor
{
    // params se comma separated isbn string aa rhi h, out se valid list return kar rhe
    public static bool TryProcessOrder(out List<string> validIsbns, params string[] isbnEntries)
    {
        validIsbns = new List<string>();

        foreach (string entry in isbnEntries)
        {
            // agar comma separated single string aayi h to split karna padega
            string[] splitEntries = entry.Split(',');

            foreach (string single in splitEntries)
            {
                string trimmed = single.Trim();

                if (IsbnValidator.TryParseISBN(trimmed, out string cleaned))
                {
                    validIsbns.Add(cleaned);
                }
                // invalid wale skip ho jayenge, koi exception nhi throw hogi
            }
        }

        return validIsbns.Count > 0;
    }
}
