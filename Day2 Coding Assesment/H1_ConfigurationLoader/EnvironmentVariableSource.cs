using System;

class EnvironmentVariableSource : IConfigurationSource
{
    public bool TryLoad(out string configData)
    {
        // yahan env variable exist krta h but data nhi h - fail scenario
        configData = string.Empty;
        Console.WriteLine("Trying EnvironmentVariableSource...");
        return false;
    }
}
