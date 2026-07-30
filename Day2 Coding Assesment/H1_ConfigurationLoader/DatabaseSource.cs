using System;

class DatabaseSource : IConfigurationSource
{
    public bool TryLoad(out string configData)
    {
        Console.WriteLine("Trying DatabaseSource...");

        // yahan real me db call hoti but demo k liye direct assign kr rhe
        configData = "{ \"AppName\": \"MyApp\", \"Version\": \"1.0\" }";
        return true;
    }
}
