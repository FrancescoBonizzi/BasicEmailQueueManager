﻿using BasicEmailQueueManager.Domain;
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

        public EmailManager(
            IEmailClient emailClient,
            IEmailRepository emailQueueRepository,
            ILogger logger)
        {
            _emailClient = emailClient ?? throw new ArgumentNullException(nameof(emailClient));
            _emailQueueRepository = emailQueueRepository ?? throw new ArgumentNullException(nameof(emailQueueRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Enqueue an email
        /// </summary>
        /// <param name="newEmail"></param>
        /// <returns></returns>
        public Task Enqueue(NewEmail newEmail)
             => _emailQueueRepository.Enqueue(newEmail);

        /// <summary>
        /// Starts a loop that with every <see cref="IConfiguration.RunInterval"/> interval 
        /// checks if there are some email to send and sends them.
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task RunEmailProcessing(
            CancellationToken cancellationToken,
            int emailNumberPerBatch,
            TimeSpan runInterval)
        {
            while (true)
            {
                cancellationToken.ThrowIfCancellationRequested();
                try
                {
                    await RunEmailProcessingStep(cancellationToken, emailNumberPerBatch);
                }
                catch (AggregateException exceptions)
                {
                    await _logger.LogError(exceptions);
                    foreach (var exception in exceptions.InnerExceptions)
                        await _logger.LogError(exception);

                    // Don't rethrow to keep the "service" running
                }

                await Task.Delay(runInterval, cancellationToken);
            }
        }

        /// <summary>
        /// Checks one time if there are some email to send and sends them.
        /// Use this method if you want to manage manually each run.
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task RunEmailProcessingStep(
            CancellationToken cancellationToken,
            int emailNumberPerBatch)
        {
            var emailsToSend = await _emailQueueRepository.Dequeue(emailNumberPerBatch);
            int sentEmailsCount = 0;
            var aggregateExceptions = new List<Exception>();

            foreach (var email in emailsToSend)
            {
                cancellationToken.ThrowIfCancellationRequested();
                try
                {
                    await _emailClient.Send(email);
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
