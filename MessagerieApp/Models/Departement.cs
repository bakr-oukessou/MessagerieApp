namespace MessagerieApp.Models
{
    public class Departement
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public string Code { get; set; }
        public int? ResponsableId { get; set; }
        public Utilisateur Responsable { get; set; }
        public List<Utilisateur> Membres { get; set; }
        public List<Ressource> Ressources { get; set; }
    }
}
