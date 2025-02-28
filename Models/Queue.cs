namespace MessageQueueAPI.Models
{
    public class Queue
    {
        public int QueueID { get; set; }
        public string Name { get; set; }

        // Relaci�n muchos a muchos con Message
        public ICollection<QueueMessage> QueueMessages { get; set; }
    }
}
