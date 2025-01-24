using System.Reflection;

namespace MessagerieApp.Models
{
    public class AppelOffres
    {
        public int Id { get; set; }
        public int OffreId { get; set; }
        public int FournisseurId { get; set; }
        public DateTime SubmissionDate { get; set; }
        public DateTime ProposedDeliveryDate { get; set; }
        public int WarrantyMonths { get; set; }
        public decimal TotalPrice { get; set; }
        public string Status { get; set; } // Submitted, Accepted, Rejected
        public Offre Offre { get; set; }
        public Supplier Fournisseur { get; set; }
        public List<AppelOffresItem> Items { get; set; }
    }
}
