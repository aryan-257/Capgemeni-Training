using System;
using System.Text;

static class LogFormatter
{
    // template + params object array - jitne bhi arguments ho skte h
    public static string FormatLogMessage(string template, params object[] args)
    {
        // local function jo placeholders replace krega
        string ReplacePlaceholders(string msg, object[] values)
        {
            StringBuilder sb = new StringBuilder(msg);

            for (int i = 0; i < values.Length; i++)
            {
                string placeholder = "{" + i + "}";
                string valueStr;

                // agar value string h to TryParse se check kr skte h ki number h ya nhi, warna direct ToString
                if (values[i] is string strVal && int.TryParse(strVal, out int parsedInt))
                {
                    valueStr = parsedInt.ToString();
                }
                else
                {
                    valueStr = values[i]?.ToString() ?? string.Empty;
                }

                sb.Replace(placeholder, valueStr);
            }

            return sb.ToString();
        }

        return ReplacePlaceholders(template, args);
    }
}
