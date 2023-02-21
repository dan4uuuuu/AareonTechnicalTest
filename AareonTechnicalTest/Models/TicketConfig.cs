using Microsoft.EntityFrameworkCore;

namespace AareonTechnicalTest.Models
{
    public static class TicketConfig
    {
        public static void Configure(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>(
                entity =>
                {
                    entity.HasKey(e => e.Id);
                });

            modelBuilder.Entity<Note>(
                entity =>
                {
                    entity.HasKey(e => e.Id);
                });
        }
    }
}
