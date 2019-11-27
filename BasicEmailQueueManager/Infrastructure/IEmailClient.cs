using BasicEmailQueueManager.Domain;
using System.Threading.Tasks;

namespace BasicEmailQueueManager.Infrastructure
{
    public interface IEmailClient
    {
        void Send(Email email);
    }
}
