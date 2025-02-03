using MessagerieApp.Models.TransversalData;
using System.ComponentModel.DataAnnotations;

namespace MessagerieApp.Models.MasterData
{
	public class Ressource
	{
		[Key]
		public int Id { get; set; }

		[Required]
		public string InventoryNumber { get; set; }

		[Required]
		public string Type { get; set; }

		public string Brand { get; set; }

		public int? DepartmentId { get; set; }
		public virtual Departement? Department { get; set; }

		public int? AssignedToUserId { get; set; }
		public virtual User? AssignedToUser { get; set; }

		[Required]
		public DateTime AcquisitionDate { get; set; }

		public DateTime? WarrantyEndDate { get; set; }

		[Required]
		public string Status { get; set; } // Disponible, Affectée, En panne
	}

}
