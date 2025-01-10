using System.ComponentModel.DataAnnotations;

namespace MessagerieApp.Models
{
    public class Ressource
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Nom { get; set; }

        [Required]
        public string Type { get; set; } // Ordinateur, Imprimante

        public string Marque { get; set; }

        public DateTime DateAcquisition { get; set; }

        public int DepartementId { get; set; }
        public Departement Departement { get; set; }
    }
}
