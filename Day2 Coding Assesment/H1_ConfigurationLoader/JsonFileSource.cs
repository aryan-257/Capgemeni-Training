using System;
using System.IO;

class JsonFileSource : IConfigurationSource
{
    private string filePath;

    public JsonFileSource(string path)
    {
        filePath = path;
    }

    public bool TryLoad(out string configData)
    {
        Console.WriteLine("Trying JsonFileSource...");

        if (!File.Exists(filePath))
        {
            configData = string.Empty;
            return false;
        }

        configData = File.ReadAllText(filePath);
        return true;
    }
}
