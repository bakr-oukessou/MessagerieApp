using MessagerieApp.Models;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace MessagerieApp.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Notification> Notifications { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuration de l'entité User
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Username).IsRequired().HasMaxLength(50);
                entity.Property(e => e.PasswordHash).IsRequired();
                entity.Property(e => e.Nom).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Prenom).IsRequired().HasMaxLength(100);
                entity.Property(e => e.DateNaissance).IsRequired();
                entity.Property(e => e.Niveau).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Filiere).IsRequired().HasMaxLength(100);
            });

            // Configuration de l'entité Contact
            modelBuilder.Entity<Contact>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasOne(d => d.User)
                    .WithMany()
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.Restrict);
                entity.HasOne(d => d.ContactUser)
                    .WithMany()
                    .HasForeignKey(d => d.ContactUserId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // Configuration de l'entité Message
            modelBuilder.Entity<Message>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasOne(d => d.Sender)
                    .WithMany()
                    .HasForeignKey(d => d.SenderId)
                    .OnDelete(DeleteBehavior.Restrict);

                // Configuration pour la liste des destinataires
                entity.Property(e => e.ReceiverIds)
                    .HasConversion(
                        v => string.Join(',', v),
                        v => v.Split(',', StringSplitOptions.RemoveEmptyEntries)
                            .Select(int.Parse)
                            .ToList()
                    );
            });

            // Configuration de l'entité Notification
            modelBuilder.Entity<Notification>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasOne(d => d.User)
                    .WithMany()
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.Restrict);
                entity.HasOne(d => d.Message)
                    .WithMany()
                    .HasForeignKey(d => d.MessageId)
                    .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}
