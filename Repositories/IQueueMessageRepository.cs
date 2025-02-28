using MessageQueueAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MessageQueueAPI.Repositories
{
    public interface IQueueMessageRepository
    {
        Task<IEnumerable<QueueMessage>> GetAllAsync();
        Task<IEnumerable<Message>> GetMessagesByQueueIdAsync(int queueId);
        Task AddAsync(QueueMessage queueMessage);
        Task RemoveAsync(int queueId, int messageId);
    }
}
