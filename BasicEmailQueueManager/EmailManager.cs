using BasicEmailQueueManager.Infrastructure;
using System;
using System.Collections.Generic;
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
        /// <summary>
        /// Starts a loop that with every <see cref="IConfiguration.RunInterval"/> interval 
        /// checks if there are some email to send and sends them.
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task RunEmailProcessing(
            CancellationToken cancellationToken)
        {
            while (true)
            {
                cancellationToken.ThrowIfCancellationRequested();
                try
                {
                    await RunEmailProcessingStep(cancellationToken);
                }
                catch(AggregateException exceptions)
                {
                    await _logger.LogError(exceptions);
                    foreach (var exception in exceptions.InnerExceptions)
                        await _logger.LogError(exception);
                    
                    // Don't rethrow to keep the "service" running
                }

                await Task.Delay(_configuration.RunInterval, cancellationToken);
            }
        }

        /// <summary>
        /// Checks one time if there are some email to send and sends them.
        /// Use this method if you want to manage manually each run.
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task RunEmailProcessingStep(
            CancellationToken cancellationToken)
        {
            var emailsToSend = await _emailQueueRepository.Dequeue();
            int sentEmailsCount = 0;
            var aggregateExceptions = new List<Exception>();

            foreach (var email in emailsToSend)
            {
                cancellationToken.ThrowIfCancellationRequested();
                try
                {
                    _emailClient.Send(email);
                    await _emailQueueRepository.SetSent(email.EmailId);
                    ++sentEmailsCount;
                }
                catch (Exception ex)
                {
                    await _emailQueueRepository.SetInError(email.EmailId);
                    await _logger.LogError($"Failed to send emailId: {email.EmailId}", ex);
                    aggregateExceptions.Add(ex);
                }
            }

            await _logger.LogInformation($"Sent: {sentEmailsCount} emails");

            if (aggregateExceptions.Count > 0)
            {
                throw new AggregateException(
                    $"Couldn't send {aggregateExceptions.Count} emails",
                    aggregateExceptions);
            }
        }
    }
}
