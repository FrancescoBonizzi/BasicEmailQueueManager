using System;
using System.Collections.Generic;

namespace BasicEmailQueueManager.Domain
{
    public class Email
    {
        public int? EmailId { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime LastUpdateDate { get; set; }
        public string Body { get; set; }
        public string Subject { get; set; }
        public string From { get; set; }
        public IEnumerable<string> To { get; set; }
        public IEnumerable<string> Cc { get; set; }
    }
}
