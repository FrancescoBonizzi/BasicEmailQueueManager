using System;

namespace BasicEmailQueueManager.Domain
{
    public class Email
    {
        public int EmailId { get; set; }
        public Statuses Status { get; set; }
        public DateTimeOffset CreationDate { get; set; }
        public DateTimeOffset LastUpdateDate { get; set; }
        public string Body { get; set; }
        public string Subject { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public string Cc { get; set; }
    }
}
