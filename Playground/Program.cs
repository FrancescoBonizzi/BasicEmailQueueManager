using BasicEmailQueueManager;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Playground
{
    internal static class Program
    {
        private static async Task Main(string[] args)
        {
            var cs = "Data Source=localhost\\SQLExpress; Integrated Security=SSPI; Initial Catalog=PLAYGROUND;";

            var emailQueueManager = new EmailManager(
                emailClient: new FakeEmailClient(),
                emailQueueRepository: new SQLEmailQueueRepository(cs),
                logger: new ConsoleLogger(),
                configuration: new SQLConfiguration(cs));

            // Insert test email
            for (int i = 0; i < 100; ++i)
            {
                await emailQueueManager.Enqueue(new BasicEmailQueueManager.Domain.NewEmail(
                    DateTimeOffset.Now,
                    DateTimeOffset.Now,
                    "<body>sdsadsaa</body>",
                    "Subject",
                    "from@test.it",
                    new string[] { "to1@test.it", "to2@test.it " },
                    null));
            }

            Console.WriteLine("Sending emails...");
            await emailQueueManager.RunEmailProcessing(
                CancellationToken.None,
                100);
        }
    }
}
