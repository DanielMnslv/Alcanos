using Microsoft.AspNetCore.Mvc;
using MessageQueueAPI.Models;
using MessageQueueAPI.Repositories;

namespace MessageQueueAPI.Controllers
{
    [Route("api/messages")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        private readonly IMessageRepository _messageRepository;

        public MessagesController(IMessageRepository messageRepository)
        {
            _messageRepository = messageRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetMessages()
        {
            var messages = await _messageRepository.GetAllAsync();
            return Ok(messages);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMessage(int id)
        {
            var message = await _messageRepository.GetByIdAsync(id);
            if (message == null) return NotFound();
            return Ok(message);
        }

        [HttpPost]
        public async Task<IActionResult> CreateMessage([FromBody] Message message)
        {
            if (message == null) return BadRequest();
            await _messageRepository.AddAsync(message);
            return CreatedAtAction(nameof(GetMessage), new { id = message.MessageID }, message);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMessage(int id, [FromBody] Message message)
        {
            if (id != message.MessageID) return BadRequest();
            await _messageRepository.UpdateAsync(message);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMessage(int id)
        {
            await _messageRepository.DeleteAsync(id);
            return NoContent();
        }
    }
}
