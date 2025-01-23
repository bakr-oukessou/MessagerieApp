using System.ComponentModel.DataAnnotations;

namespace MessagerieApp.Models
{
    public class Ressource
    {
        public int Id { get; set; }
        public string InventoryNumber { get; set; }
        public string Type { get; set; }
        public string Brand { get; set; }
        public int? DepartmentId { get; set; }
        public int? AssignedToUserId { get; set; }
        public DateTime AcquisitionDate { get; set; }
        public DateTime? WarrantyEndDate { get; set; }
        public string Status { get; set; }

        public Computer? ComputerDetails { get; set; }
        public Imprimante? ImprimanteDetails { get; set; }
    }
}
