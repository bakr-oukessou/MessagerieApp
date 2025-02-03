namespace MessagerieApp.Models.TransactionData
{
	public class AppelOffresItem
	{
		public int Id { get; set; }
		public int AppelOffreID { get; set; }
		public string Type { get; set; }  // Computer or Printer
		public string Brand { get; set; }
		public string Specifications { get; set; }
		public int Quantity { get; set; }
		public decimal UnitPrice { get; set; }
		public virtual AppelOffres AppelOffre { get; set; }
	}
}
