using BasicEmailQueueManager.Infrastructure;
using System;
using System.Threading.Tasks;

namespace BasicEmailQueueManager
{
    public class EmailManager
    {
        private readonly IEmailClient _emailClient;
        private readonly IEmailRepository _emailQueueRepository;
        private readonly ILogger _logger;

        public EmailManager(
            IEmailClient emailClient,
            IEmailRepository emailQueueRepository,
            ILogger logger)
        {
            _emailClient = emailClient ?? throw new ArgumentNullException(nameof(emailClient));
            _emailQueueRepository = emailQueueRepository ?? throw new ArgumentNullException(nameof(emailQueueRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task ProcessEmails()
        {
            var emailsToSend = await _emailQueueRepository.Dequeue();

            foreach (var email in emailsToSend)
            {
                try
                {
                    _emailClient.Send(email);
                    await _emailQueueRepository.SetSent(email.EmailId.Value);
                }
                catch(Exception ex)
                {
                    await _emailQueueRepository.SetInError(email.EmailId.Value);
                    await _logger.LogError($"Failed to send emailId: {email.EmailId}", ex);
                }
            }
        }
    }
}
