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
            // basta mettere in stato "in progress" quelle di cui faccio dequeue tramite DB in transazione con 

            // http://rusanu.com/2010/03/26/using-tables-as-queues/

            // Ovviamente FIFO! Quindi basati sull'ordine

            /*
             CREATE CLUSTERED INDEX cdxTable on TABLE(PROCESSED, ID);
             Metti il campo processed e l'id della riga in un clustered index
             */

            // Faccio update where to send e metto in una tabella gli id risultanti OUTPUT
            /*
            WITH CTE AS (
                SELECT TOP(1) COMMAND, PROCESSED
                FROM TABLE WITH (READPAST)
                WHERE PROCESSED = 0)
            UPDATE CTE
            SET PROCESSED = 1
            OUTPUT INSERTED.*;
            */

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
