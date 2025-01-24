namespace MessagerieApp.Models
{
    public class DemandeRessource
    {
        public int Id { get; set; }
        public int DepartmentId { get; set; }
        public DateTime RequestDate { get; set; }
        public string Status { get; set; }  // Draft, Submitted, Approved, Rejected
        public virtual Departement Department { get; set; }
        public virtual ICollection<DemandeRessourceItem> Items { get; set; }
    }
}
