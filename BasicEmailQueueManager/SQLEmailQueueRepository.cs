using BasicEmailQueueManager.Domain;
using BasicEmailQueueManager.Infrastructure;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BasicEmailQueueManager
{
    public class SQLEmailQueueRepository : IEmailRepository
    {
        public Task<IEnumerable<Email>> Dequeue(int? count = null)
        {
            throw new NotImplementedException();
        }

        public Task Enqueue(params Email[] email)
        {
            throw new NotImplementedException();
        }

        public Task SetInError(int emailId)
        {
            throw new NotImplementedException();
        }

        public Task SetSent(int emailId)
        {
            throw new NotImplementedException();
        }
    }
}
