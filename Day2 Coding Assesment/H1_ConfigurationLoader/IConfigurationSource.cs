using System;

interface IConfigurationSource
{
    // har source ka apna tarika hoga load karne ka
    bool TryLoad(out string configData);
}
