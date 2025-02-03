namespace MessagerieApp.Models.TransactionData
{
	public abstract class Notification
	{

		public int Id { get; set; }
		public int SenderId { get; set; }    // EmetteurId
		public int ReceiverId { get; set; }  // DestinataireId
		public string Title { get; set; }    // Titre
		public string Content { get; set; }  // Corps
		public NotificationStatus Status { get; set; }
		public DateTime CreatedAt { get; set; }
		public DateTime? ReadAt { get; set; }
	}

	public enum NotificationStatus
{
		DemandeBesoins,
		AppelOffre,
		Maintenance,
		Livraison,
		Rejet,
		Acceptation
	}

	public enum StatutNotification
	{
		NonLue,
		Lue,
		Archivee
	}
}
