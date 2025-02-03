using MessagerieApp.Models.MasterData;

namespace MessagerieApp.Models.TransactionData
{
	public class DemandeRessource : Notification
	{
		public int DepartmentId { get; set; }
		public RequestType Type { get; set; }
		public RequestStatus CurrentStatus { get; set; }
		public List<RessourceItem> Items { get; set; } = new();

		// Historique des validations
		public List<ValidationStep> ValidationSteps { get; set; } = new();
	}

	public enum RequestType
	{
		Initiale,           // Chef → Enseignant
		Soumission,         // Enseignant → Chef
		ValidationChef,     // Chef → Responsable
		ValidationResponsable // Responsable → Final
	}

	public enum RequestStatus
	{
		Brouillon,
		EnAttente,
		Validee,
		Rejetee
	}

	public class ValidationStep
	{
		public int ValidatorId { get; set; }
		public DateTime ValidationDate { get; set; }
		public string Comments { get; set; }
	}

}
