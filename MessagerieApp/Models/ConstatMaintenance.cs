namespace MessagerieApp.Models
{
    public class ConstatMaintenance
    {
        public int Id { get; set; }
        public int ResourceId { get; set; }
        public int ReportedByUserId { get; set; }
        public int? TechnicianId { get; set; }
        public DateTime ReportDate { get; set; }
        public string IssueDescription { get; set; }
        public string Status { get; set; }  // Reported, InProgress, Diagnosed, Resolved, RequiresReplacement
        public virtual Ressource Resource { get; set; }
        public virtual User ReportedByUser { get; set; }
        public virtual User? Technician { get; set; }
        public virtual MaintenanceDiagnosis? Diagnosis { get; set; }
    }
}
