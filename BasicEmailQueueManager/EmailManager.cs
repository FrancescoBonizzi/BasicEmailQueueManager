using BasicEmailQueueManager.Infrastructure;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BasicEmailQueueManager
{
    public class EmailManager
    {
        private readonly IEmailClient _emailClient;
        private readonly IEmailRepository _emailQueueRepository;
        private readonly ILogger _logger;
        private readonly IConfiguration _configuration;

        public EmailManager(
            IEmailClient emailClient,
            IEmailRepository emailQueueRepository,
            ILogger logger,
            IConfiguration configuration)
        {
            _emailClient = emailClient ?? throw new ArgumentNullException(nameof(emailClient));
            _emailQueueRepository = emailQueueRepository ?? throw new ArgumentNullException(nameof(emailQueueRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public async Task ProcessEmails(
            CancellationToken cancellationToken)
        {
            while (true)
            {
                cancellationToken.ThrowIfCancellationRequested();
                var emailsToSend = await _emailQueueRepository.Dequeue();

                foreach (var email in emailsToSend)
                {
                    cancellationToken.ThrowIfCancellationRequested();

                    try
                    {
                        _emailClient.Send(email);
                        await _emailQueueRepository.SetSent(email.EmailId.Value);
                    }
                    catch (Exception ex)
                    {
                        await _emailQueueRepository.SetInError(email.EmailId.Value);
                        await _logger.LogError($"Failed to send emailId: {email.EmailId}", ex);
                    }
                }

                await Task.Delay(_configuration.RunInterval, cancellationToken);
            }
        }

    }
}
