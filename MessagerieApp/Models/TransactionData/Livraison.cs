namespace MessagerieApp.Models.TransactionData
{
	public class Livraison : Notification
	{
		public int FournisseurId { get; set; }
		public int RessourceId { get; set; }
		public DateTime DateLivraison { get; set; }
	}
}
