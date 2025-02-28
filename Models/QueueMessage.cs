namespace MessageQueueAPI.Models
{
    public class QueueMessage
    {
        public int QueueID { get; set; }
        public Queue Queue { get; set; }

        public int MessageID { get; set; }
        public Message Message { get; set; }
    }
}
