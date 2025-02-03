namespace MessagerieApp.Models.TransactionData
{
	public class PropositionOffre : Notification
	{
		public int FournisseurId { get; set; }
		public int AppelOffreId { get; set; }
		public decimal MontantPropose { get; set; }
	}

}
