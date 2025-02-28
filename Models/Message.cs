namespace MessageQueueAPI.Models
{
    public class Message
    {
        public int MessageID { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Relaci�n muchos a muchos con Queue
        public ICollection<QueueMessage> QueueMessages { get; set; }
    }
}
