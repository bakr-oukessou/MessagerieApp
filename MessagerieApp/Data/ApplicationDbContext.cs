using MessagerieApp.Models;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Reflection;

namespace MessagerieApp.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {
        }

        public DbSet<Departement> Departments { get; set; }
        public DbSet<Ressource> Resources { get; set; }
        public DbSet<Computer> Computers { get; set; }
        public DbSet<Printer> Printers { get; set; }
        public DbSet<DemandeRessource> ResourceRequests { get; set; }
        public DbSet<DemandeRessourceItem> ResourceRequestItems { get; set; }
        public DbSet<Offre> Tenders { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<AppelOffres> TenderBids { get; set; }
        public DbSet<AppelOffresItem> TenderBidItems { get; set; }
        public DbSet<ConstatMaintenance> MaintenanceTickets { get; set; }
        public DbSet<MaintenanceDiagnosis> MaintenanceDiagnoses { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Message> Messages { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Configure entity relationships and constraints here
            builder.Entity<Resource>()
                .HasDiscriminator(r => r.Type)
                .HasValue<Computer>("Computer")
                .HasValue<Printer>("Printer");

            // Add other configuration as needed
        }
    }
}
