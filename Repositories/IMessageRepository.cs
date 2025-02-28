using MessageQueueAPI.Models;

namespace MessageQueueAPI.Repositories
{
    public interface IMessageRepository
    {
        Task<Message> GetByIdAsync(int id);
        Task<IEnumerable<Message>> GetAllAsync();
        Task AddAsync(Message message);
        Task UpdateAsync(Message message);
        Task DeleteAsync(int id);
    }
}
