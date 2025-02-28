using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MessageQueueAPI.Models
{
    public class QueueSubscriber
    {
        [Key]
        public int Id { get; set; }  // Agregar esta clave primaria

        [Required]
        public int QueueId { get; set; }
        public Queue Queue { get; set; }

        [Required]
        public int SubscriberId { get; set; }
        public Subscriber Subscriber { get; set; }
    }
}

