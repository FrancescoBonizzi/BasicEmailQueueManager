﻿using BasicEmailQueueManager.Domain;
using BasicEmailQueueManager.Infrastructure;
using System.Net;
using System.Net.Mail;

namespace BasicEmailQueueManager
{
    public class DotNetSMTPEmailClient : IEmailClient
    {
        private readonly SmtpClient _client;

        public DotNetSMTPEmailClient(IConfiguration configuration)
        {
            _client = new SmtpClient
            {
                UseDefaultCredentials = configuration.UseDefaultCredentials,
                EnableSsl = configuration.EnableSsl,
                Host = configuration.Host,
                Port = configuration.Port
            };

            if (!configuration.UseDefaultCredentials)
            {
                _client.Credentials = new NetworkCredential(
                    userName: configuration.UserName,
                    password: configuration.Password);
            }
        }

        public void Send(Email email)
        {
            var mailMessage = new MailMessage
            {
                From = new MailAddress(email.From),
                Subject = email.Subject,
                Body = email.Body
            };

            foreach (var toAddress in email.To)
            {
                mailMessage.To.Add(toAddress);
            }

            foreach (var ccAddress in email.Cc)
            {
                mailMessage.CC.Add(ccAddress);
            }

            _client.Send(mailMessage);
        }
    }
}