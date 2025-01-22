namespace MessagerieApp.Models
{
    public class DemandeRessourceItem
    {
        public string Id { get; set; }
        public string ResourceRequestId { get; set; }
        public string Type { get; set; }  // Computer or Printer
        public string Specifications { get; set; }
        public int Quantity { get; set; }
        public string? AssignedToUserId { get; set; }
        public virtual User? AssignedToUser { get; set; }
    }
}
