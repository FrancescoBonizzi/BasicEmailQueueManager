using BasicEmailQueueManager.Domain;
using BasicEmailQueueManager.Infrastructure;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

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

        public async Task Send(Email email)
        {
            var mailMessage = new MailMessage
            {
                From = new MailAddress(email.From),
                Subject = email.Subject,
                Body = email.Body
            };

            foreach (var toAddress in email.To.Split(","))
            {
                mailMessage.To.Add(toAddress);
            }

            if (email.Cc != null)
            {
                foreach (var ccAddress in email.Cc.Split(","))
                {
                    mailMessage.CC.Add(ccAddress);
                }
            }

            await _client.SendMailAsync(mailMessage);
        }
    }
}
