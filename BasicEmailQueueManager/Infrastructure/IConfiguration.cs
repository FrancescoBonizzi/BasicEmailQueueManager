using System.Security;

namespace BasicEmailQueueManager.Infrastructure
{
    public interface IConfiguration
    {
        string MyProperty { get; set; }
        bool EnableSsl { get; set; }
        bool UseDefaultCredentials { get; set; }
        string Host { get; set; }
        int Port { get; set; }
        string UserName { get; set; }
        SecureString Password { get; set; }
    }
}
