using MessagerieApp.Models.TransversalData;

namespace MessagerieApp.Models.MasterData
{
	public class Departement
	{
		public int Id { get; set; }
		public string Nom { get; set; }
		public string Code { get; set; }
		public int? ResponsableId { get; set; }
		public User Responsable { get; set; }
		public List<User> Membres { get; set; }
		public List<Ressource> Ressources { get; set; }
	}
}
