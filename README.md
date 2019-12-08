# BasicEmailQueueManager
**BasicEmailQueueManager** a very basic enqueue-dequeue-send email library.

`EmailManager` exposes an Enqueue method, which will add an email to the send queue, and `RunEmailProcessing` method which will start a new *infinite-loop* `Task` that periodically checks for new email to send and sends them. It exists also the *single-step* method to manage manually the periodicity of the operation.

`EmailManager` has these depencies:
- `IEmailClient`: the service that implements the email protocol. I coded for you a `DotNetSMTPEmailClient` that uses the built-in `System.Net.Mail` library
- `IEmailRepository`: the service that communicates with the email queue storage. It could be a file, a `RabbitMQ` queue, or a SQL database. Here I coded a `SQLEmailQueueRepository` based on `Microsoft SQL Server` and `Dapper`.
- `ILogger`: a simple logging service to expose some informations or errors that may occur during the whole process

# Usage example:

```c#
var cs = "Data Source=localhost\\SQLExpress; Integrated Security=SSPI; Initial Catalog=PLAYGROUND;";

var emailQueueManager = new EmailManager(
    emailClient: new FakeEmailClient(),
    emailQueueRepository: new SQLEmailQueueRepository(cs),
    logger: new ConsoleLogger());

// Insert test email
for (int i = 0; i < 100; ++i)
{
    await emailQueueManager.Enqueue(new BasicEmailQueueManager.Domain.NewEmail(
        creationDate: DateTimeOffset.Now,
        lastUpdateDate: DateTimeOffset.Now,
        body: "<body>Test body</body>",
        subject: "Subject",
        from: "from@test.it",
        to: new string[] { "to1@test.it", "to2@test.it " },
        cc: null));
}

// Send test emails
Console.WriteLine("Sending emails...");
await emailQueueManager.RunEmailProcessing(
    cancellationToken: CancellationToken.None,
    emailNumberPerBatch: 100,
    runInterval: TimeSpan.FromSeconds(30));
```

# Building
Simply clone this repository and build the `BasicEmailQueueManager.sln` solution.

# How to contribute
- Report any issues
- Implement some new `IEmailRepository`
- Propose new features / improvements
- Just telling your opinion :-)
- [Offer me an espresso!](https://www.paypal.com/cgi-bin/webscr?cmd=_donations&business=DTT7P8N3TV7N6&currency_code=EUR&source=url) ;-)