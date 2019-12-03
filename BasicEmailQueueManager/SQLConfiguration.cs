using BasicEmailQueueManager.Infrastructure;
using System;
using System.Security;

namespace BasicEmailQueueManager
{
    public class SQLConfiguration : IConfiguration
    {
        public bool EnableSsl { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public bool UseDefaultCredentials { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string Host { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int Port { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string UserName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public SecureString Password { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public TimeSpan RunInterval { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
