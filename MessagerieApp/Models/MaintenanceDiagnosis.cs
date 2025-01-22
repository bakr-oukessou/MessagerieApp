namespace MessagerieApp.Models
{
    public class MaintenanceDiagnosis
    {
        public string Id { get; set; }
        public string MaintenanceTicketId { get; set; }
        public DateTime DiagnosisDate { get; set; }
        public string ProblemDescription { get; set; }
        public string Frequency { get; set; }  // Rare, Frequent, Permanent
        public string IssueType { get; set; }  // Software, Hardware
        public bool RequiresReplacement { get; set; }
        public virtual ConstatMaintenance MaintenanceTicket { get; set; }
    }
}
}
