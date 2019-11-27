using BasicEmailQueueManager.Domain;
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
            _client.Send(
                from: email.From,
                recipients: email.Recipients,
                subject: email.Subject,
                body: email.Body);
        }
    }
}
