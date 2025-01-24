namespace MessagerieApp.Models
{
    public class DemandeRessourceItem
    {
        public int Id { get; set; }
        public int ResourceRequestId { get; set; }
        public string Type { get; set; }  // Computer or Printer
        public string Specifications { get; set; }
        public int Quantity { get; set; }
        public int? AssignedToUserId { get; set; }
        public virtual User? AssignedToUser { get; set; }
    }
}
