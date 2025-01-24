namespace MessagerieApp.Models
{
    public class Offre
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Status { get; set; }  // Open, Closed, Awarded
        public virtual ICollection<DemandeRessource> ResourceRequests { get; set; }
        public virtual ICollection<AppelOffres> AppelOffres { get; set; }
    }
}
