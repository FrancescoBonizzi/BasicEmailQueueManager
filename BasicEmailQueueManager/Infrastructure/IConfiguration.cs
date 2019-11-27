using System;
using System.Security;

namespace BasicEmailQueueManager.Infrastructure
{
    public interface IConfiguration
    {
        bool EnableSsl { get; set; }
        bool UseDefaultCredentials { get; set; }
        string Host { get; set; }
        int Port { get; set; }
        string UserName { get; set; }
        SecureString Password { get; set; }

        TimeSpan RunInterval { get; set; }

    }
}
