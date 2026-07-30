using System;

static class ConfigurationLoader
{
    // params se multiple sources le rhe, jo pehle success hoga wahi use hoga
    public static bool Load(out string finalConfig, params IConfigurationSource[] sources)
    {
        finalConfig = string.Empty;

        foreach (var src in sources)
        {
            if (src.TryLoad(out string data))
            {
                finalConfig = data;
                return true;
            }
            // agar fail hua to loop next source try krega, koi exception nhi
        }

        return false;
    }
}
