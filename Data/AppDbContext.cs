using Microsoft.EntityFrameworkCore;
using MessageQueueAPI.Models;

namespace MessageQueueAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Message> Messages { get; set; }
        public DbSet<Queue> Queues { get; set; }
        public DbSet<Subscriber> Subscribers { get; set; }
        public DbSet<QueueSubscriber> QueueSubscribers { get; set; }
        public DbSet<QueueMessage> QueueMessages { get; set; } // 🔹 Agregado

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Clave compuesta para QueueSubscriber
            modelBuilder.Entity<QueueSubscriber>()
                .HasKey(qs => new { qs.QueueId, qs.SubscriberId });

            // Clave compuesta para QueueMessage
            modelBuilder.Entity<QueueMessage>()
                .HasKey(qm => new { qm.QueueID, qm.MessageID });

            // Relación muchos a muchos entre Queue y Message
            modelBuilder.Entity<QueueMessage>()
                .HasOne(qm => qm.Queue)
                .WithMany(q => q.QueueMessages)
                .HasForeignKey(qm => qm.QueueID);

            modelBuilder.Entity<QueueMessage>()
                .HasOne(qm => qm.Message)
                .WithMany(m => m.QueueMessages)
                .HasForeignKey(qm => qm.MessageID);
        }
    }
}
