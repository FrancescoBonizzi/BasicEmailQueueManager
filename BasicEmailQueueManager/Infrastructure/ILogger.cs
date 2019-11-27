using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace BasicEmailQueueManager.Infrastructure
{
    public interface ILogger
    {
        /// <summary>
        /// Logs an error
        /// </summary>
        /// <param name="message"></param>
        /// <param name="exception"></param>
        /// <param name="caller"></param>
        /// <returns></returns>
        Task LogError(
            string message,
            Exception exception,
            [CallerMemberName] string caller = null);

        /// <summary>
        /// Logs an exception
        /// </summary>
        /// <param name="message"></param>
        /// <param name="exception"></param>
        /// <param name="caller"></param>
        /// <returns></returns>
        Task LogError(
            Exception exception,
            [CallerMemberName] string caller = null);
    }
}
