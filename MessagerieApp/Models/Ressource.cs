using System.ComponentModel.DataAnnotations;

namespace MessagerieApp.Models
{
    public class Ressource
    {
        public string Id { get; set; }
        public string InventoryNumber { get; set; }
        public string Type { get; set; }  // Computer or Printer
        public string Brand { get; set; }
        public string? DepartmentId { get; set; }
        public string? AssignedToUserId { get; set; }
        public DateTime AcquisitionDate { get; set; }
        public DateTime? WarrantyEndDate { get; set; }
        public string Status { get; set; }  // Active, UnderMaintenance, Disposed
        public virtual Departement? Department { get; set; }
        public virtual User? AssignedToUser { get; set; }
        public virtual ICollection<ConstatMaintenance> MaintenanceHistory { get; set; }
    }
}
