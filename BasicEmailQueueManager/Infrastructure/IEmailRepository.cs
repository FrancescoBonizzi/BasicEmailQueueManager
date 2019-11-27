using BasicEmailQueueManager.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BasicEmailQueueManager.Infrastructure
{
    public interface IEmailRepository
    {
        /// <summary>
        /// Enqueues a set of emails in a single operation
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        Task Enqueue(params Email[] email);

        /// <summary>
        /// Dequeues a maximum of <paramref name="count"/> emails or all
        /// if the parameter is not set
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        Task<IEnumerable<Email>> Dequeue(int? count = null);

        /// <summary>
        /// Sets the email as sent
        /// </summary>
        /// <param name="emailId"></param>
        /// <returns></returns>
        Task SetSent(int emailId);

        /// <summary>
        /// Sets the email as in error
        /// </summary>
        /// <param name="emailId"></param>
        /// <returns></returns>
        Task SetInError(int emailId);
    }
}
