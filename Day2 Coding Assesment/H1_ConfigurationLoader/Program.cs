using System;

class Program
{
    static void Main()
    {
        var envSource = new EnvironmentVariableSource();
        var jsonSource = new JsonFileSource("config.json"); // ye file exist nhi krti, isliye fail hoga
        var dbSource = new DatabaseSource();

        bool loaded = ConfigurationLoader.Load(out string config, envSource, jsonSource, dbSource);

        if (loaded)
        {
            Console.WriteLine("Successfully loaded configuration: " + config);
        }
        else
        {
            Console.WriteLine("All sources failed to load configuration.");
        }
    }
}
