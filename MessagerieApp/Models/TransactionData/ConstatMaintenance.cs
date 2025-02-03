namespace MessagerieApp.Models.TransactionData
{
	public class ConstatMaintenance : Notification
	{
		public int RessourceId { get; set; }
		public int MaintenanceId { get; set; }
		public string Description { get; set; }
	}
}
