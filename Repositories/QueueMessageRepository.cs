using MessageQueueAPI.Data;
using MessageQueueAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MessageQueueAPI.Repositories
{
    public class QueueMessageRepository : IQueueMessageRepository
    {
        private readonly AppDbContext _context;

        public QueueMessageRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<QueueMessage>> GetAllAsync()
        {
            return await _context.QueueMessages
                .Include(qm => qm.Queue)
                .Include(qm => qm.Message)
                .ToListAsync();
        }

        public async Task<IEnumerable<Message>> GetMessagesByQueueIdAsync(int queueId)
        {
            return await _context.QueueMessages
                .Where(qm => qm.QueueID == queueId)
                .Select(qm => qm.Message)
                .ToListAsync();
        }

        public async Task AddAsync(QueueMessage queueMessage)
        {
            await _context.QueueMessages.AddAsync(queueMessage);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveAsync(int queueId, int messageId)
        {
            var queueMessage = await _context.QueueMessages
                .FirstOrDefaultAsync(qm => qm.QueueID == queueId && qm.MessageID == messageId);

            if (queueMessage != null)
            {
                _context.QueueMessages.Remove(queueMessage);
                await _context.SaveChangesAsync();
            }
        }
    }
}
