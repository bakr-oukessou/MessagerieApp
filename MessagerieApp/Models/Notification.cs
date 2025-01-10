namespace MessagerieApp.Models
{
    public class Notification
    {
        public int Id { get; set; }
        public int EmetteurId { get; set; }
        public int DestinataireId { get; set; }
        public string Titre { get; set; }
        public string Corps { get; set; }
        public NotificationType Type { get; set; }
        public StatutNotification Statut { get; set; }
        public DateTime DateCreation { get; set; }
        public DateTime? DateLecture { get; set; }
    }

    public enum NotificationType
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
