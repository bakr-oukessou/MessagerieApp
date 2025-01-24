namespace MessagerieApp.Models
{
    public class Supplier
    {
        public int Id { get; set; }
        public string CompanyName { get; set; }
        public string Location { get; set; }
        public string Address { get; set; }
        public string Website { get; set; }
        public string ManagerName { get; set; }
        public bool IsBlacklisted { get; set; }
        public string? BlacklistReason { get; set; }
        public virtual ICollection<AppelOffres> Offres { get; set; }
    }
}
