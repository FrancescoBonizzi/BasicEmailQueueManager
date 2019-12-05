using System;

namespace BasicEmailQueueManager.Exceptions
{
    public class MissingConfigurationException : Exception
    {
        public MissingConfigurationException(string key)
            : base($"Missing configuration value for key: '{key}'") { }
    }
}
