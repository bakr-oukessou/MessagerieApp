using System.Reflection;

namespace MessagerieApp.Models
{
    public class AppelOffres
    {
        public string Id { get; set; }
        public string TenderId { get; set; }
        public string SupplierId { get; set; }
        public DateTime SubmissionDate { get; set; }
        public DateTime ProposedDeliveryDate { get; set; }
        public int WarrantyMonths { get; set; }
        public decimal TotalPrice { get; set; }
        public string Status { get; set; }  // Submitted, Accepted, Rejected
        public virtual Offre offre { get; set; }
        public virtual Supplier Supplier { get; set; }
        public virtual ICollection<AppelOffresItem> Items { get; set; }
    }
}
