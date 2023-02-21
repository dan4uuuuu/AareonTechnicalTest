using AareonTechnicalTest.Interfaces;
using AareonTechnicalTest.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace AareonTechnicalTest
{
    public class ApplicationContext : IdentityDbContext<Person, IdentityRole, string>
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
            var envDir = Environment.CurrentDirectory;

            DatabasePath = $"{envDir}{System.IO.Path.DirectorySeparatorChar}Ticketing.db";
        }

        public virtual DbSet<Person> Persons { get; set; }

        public virtual DbSet<Ticket> Tickets { get; set; }

        public virtual DbSet<Note> Notes { get; set; }

        public string DatabasePath { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlite($"Data Source={DatabasePath}");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            PersonConfig.Configure(modelBuilder);
            TicketConfig.Configure(modelBuilder);
            NoteConfig.Configure(modelBuilder);
        }

        public async Task<int> SaveChangesAuditableAsync(Guid userId)
        {
            foreach (EntityEntry<IAuditableEntity> auditableEntity in this.ChangeTracker.Entries<IAuditableEntity>())
            {
                if (auditableEntity.State == EntityState.Added)
                {
                    auditableEntity.Entity.CreatedBy = userId;
                    auditableEntity.Entity.CreatedDate = DateTime.UtcNow;
                    auditableEntity.Entity.UpdatedBy = userId;
                    auditableEntity.Entity.UpdatedDate = DateTime.UtcNow;
                }

                if (auditableEntity.State == EntityState.Modified)
                {
                    auditableEntity.Entity.UpdatedBy = userId;
                    auditableEntity.Entity.UpdatedDate = DateTime.UtcNow;
                }
            }
                return await base.SaveChangesAsync();
        }
    }
}
