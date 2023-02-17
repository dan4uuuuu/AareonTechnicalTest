using Microsoft.EntityFrameworkCore;

namespace AareonTechnicalTest.Models
{
    public class NoteConfig
    {
        public static void Configure(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Ticket>(
                entity =>
                {
                    entity.HasKey(t => t.Id);
                    entity.HasMany(n => n.Notes)
                        .WithOne(t => t.Ticket)
                        .HasForeignKey(t => t.TicketId);
                });
        }
    }
}
