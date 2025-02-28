using MessageQueueAPI.Models;
using MessageQueueAPI.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MessageQueueAPI.Controllers
{
    [Route("api/queue-messages")]
    [ApiController]
    public class QueueMessagesController : ControllerBase
    {
        private readonly IQueueMessageRepository _queueMessageRepository;

        public QueueMessagesController(IQueueMessageRepository queueMessageRepository)
        {
            _queueMessageRepository = queueMessageRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllQueueMessages()
        {
            var queueMessages = await _queueMessageRepository.GetAllAsync();
            return Ok(queueMessages);
        }

        [HttpGet("queue/{queueId}")]
        public async Task<IActionResult> GetMessagesByQueue(int queueId)
        {
            var messages = await _queueMessageRepository.GetMessagesByQueueIdAsync(queueId);
            return Ok(messages);
        }

        [HttpPost]
        public async Task<IActionResult> AddQueueMessage([FromBody] QueueMessage queueMessage)
        {
            if (queueMessage == null)
                return BadRequest("Datos inválidos");

            await _queueMessageRepository.AddAsync(queueMessage);
            return CreatedAtAction(nameof(GetMessagesByQueue), new { queueId = queueMessage.QueueID }, queueMessage);
        }

        [HttpDelete("{queueId}/{messageId}")]
        public async Task<IActionResult> RemoveQueueMessage(int queueId, int messageId)
        {
            await _queueMessageRepository.RemoveAsync(queueId, messageId);
            return NoContent();
        }
    }
}
