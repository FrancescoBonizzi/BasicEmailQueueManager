using BasicEmailQueueManager;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Playground
{
    internal static class Program
    {
        static async Task Main(string[] args)
        {
            var emailQueueManager = new EmailManager(
                emailClient: new FakeEmailClient(),
                emailQueueRepository: null,
                logger: new ConsoleLogger(),
                configuration: new FakeConfiguration());

            Console.WriteLine("Sending emails...");
            await emailQueueManager.RunEmailProcessing(CancellationToken.None);
        }
    }
}
