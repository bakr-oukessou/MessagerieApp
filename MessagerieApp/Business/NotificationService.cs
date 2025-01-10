using MessagerieApp.Models;
using MessagerieApp.Repository;

namespace MessagerieApp.Business
{
    public class NotificationService
    {
        private readonly NotificationRepository _notificationRepository;
        private readonly UtilisateurRepository _utilisateurRepository;

        public NotificationService(
            NotificationRepository notificationRepository,
            UtilisateurRepository utilisateurRepository)
        {
            _notificationRepository = notificationRepository;
            _utilisateurRepository = utilisateurRepository;
        }

        // Envoi de notification aux enseignants pour les besoins
        public void EnvoyerDemandeBesoinsAuxEnseignants(int chefDepartementId, int departementId)
        {
            // Récupérer tous les enseignants du département
            var enseignants = _utilisateurRepository.ObtenirEnseignantsDuDepartement(departementId);

            foreach (var enseignant in enseignants)
            {
                var notification = new Notification
                {
                    EmetteurId = chefDepartementId,
                    DestinataireId = enseignant.Id,
                    Titre = "Demande de besoins en ressources",
                    Corps = "Veuillez soumettre vos besoins en ressources matérielles pour le prochain semestre.",
                    Type = NotificationType.DemandeBesoins,
                    Statut = StatutNotification.NonLue,
                    DateCreation = DateTime.Now
                };

                _notificationRepository.CreerNotification(notification);
            }
        }

        // Envoi de notifications pour les appels d'offres
        public void EnvoyerNotificationsAppelOffre(int responsableRessourcesId, int fournisseurSelectionneId, List<int> autresFournisseursId)
        {
            // Notification au fournisseur sélectionné
            var notificationAcceptation = new Notification
            {
                EmetteurId = responsableRessourcesId,
                DestinataireId = fournisseurSelectionneId,
                Titre = "Offre acceptée",
                Corps = "Votre offre a été retenue pour la fourniture de ressources matérielles.",
                Type = NotificationType.Acceptation,
                Statut = StatutNotification.NonLue,
                DateCreation = DateTime.Now
            };
            _notificationRepository.CreerNotification(notificationAcceptation);

            // Notifications de rejet aux autres fournisseurs
            foreach (var fournisseurId in autresFournisseursId)
            {
                var notificationRejet = new Notification
                {
                    EmetteurId = responsableRessourcesId,
                    DestinataireId = fournisseurId,
                    Titre = "Offre rejetée",
                    Corps = "Votre offre n'a pas été retenue pour cette période.",
                    Type = NotificationType.Rejet,
                    Statut = StatutNotification.NonLue,
                    DateCreation
                }
