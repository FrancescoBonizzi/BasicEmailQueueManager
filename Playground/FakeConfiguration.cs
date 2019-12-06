using BasicEmailQueueManager.Infrastructure;
using System;
using System.Security;

namespace Playground
{
    public class FakeConfiguration : IConfiguration
    {
        public bool EnableSsl => throw new NotImplementedException();

        public bool UseDefaultCredentials => throw new NotImplementedException();

        public string Host => throw new NotImplementedException();

        public int Port => throw new NotImplementedException();

        public string UserName => throw new NotImplementedException();

        public string Password => throw new NotImplementedException();

        public TimeSpan RunInterval => TimeSpan.FromSeconds(10);
    }
}
