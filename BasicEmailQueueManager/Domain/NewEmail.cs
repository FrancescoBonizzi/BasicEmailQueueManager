using System;
using System.Collections.Generic;
using System.Linq;

namespace BasicEmailQueueManager.Domain
{
    public class NewEmail
    {
        public DateTimeOffset CreationDate { get; }
        public DateTimeOffset LastUpdateDate { get; }
        public string Body { get; }
        public string Subject { get; }
        public string From { get; }
        public string To { get; }
        public string Cc { get; }

        public NewEmail(
            DateTimeOffset creationDate,
            DateTimeOffset lastUpdateDate,
            string body,
            string subject,
            string from,
            IEnumerable<string> to,
            IEnumerable<string> cc)
        {
            CreationDate = creationDate;
            LastUpdateDate = lastUpdateDate;
            Body = body;
            Subject = subject;
            From = from;

            if (!to.Any())
                throw new ArgumentException("You cannot create an email without a recipient", "to");

            To = string.Join(',', to);

            if (cc != null)
                Cc = string.Join(',', cc);
        }
    }
}
