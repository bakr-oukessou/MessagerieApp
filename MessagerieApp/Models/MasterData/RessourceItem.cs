using MessagerieApp.Models.TransversalData;

namespace MessagerieApp.Models.MasterData
{
	public class RessourceItem
	{
		public int Id { get; set; }
		public int ResourceRequestId { get; set; }
		public TypeRessource Type { get; set; }
		public string Specifications { get; set; }
		public int Quantity { get; set; }
		public int? AssignedToUserId { get; set; }
		public virtual User? AssignedToUser { get; set; }
	}

	public enum TypeRessource
	{
		Ordinateur,
		Imprimante,
		Autre
	}
}