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
            // TODO Secondo me ci va messo anche lo stato "invio in corso" così è multithread
            // basta mettere in stato "in progress" quelle di cui faccio dequeue tramite DB in transazione

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
