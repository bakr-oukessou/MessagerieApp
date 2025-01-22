namespace MessagerieApp.Models
{
    public class AppelOffresItem
    {
        public string Id { get; set; }
        public string TenderBidId { get; set; }
        public string Type { get; set; }  // Computer or Printer
        public string Brand { get; set; }
        public string Specifications { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public virtual AppelOffres TenderBid { get; set; }
    }
}
