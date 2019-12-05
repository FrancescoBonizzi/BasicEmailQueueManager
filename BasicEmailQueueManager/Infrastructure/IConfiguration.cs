using System;

namespace BasicEmailQueueManager.Infrastructure
{
    public interface IConfiguration
    {
        bool EnableSsl { get; }
        bool UseDefaultCredentials { get; }
        string Host { get; }
        int Port { get; }
        string UserName { get; }
        string Password { get; }
        TimeSpan RunInterval { get; }
    }
}
