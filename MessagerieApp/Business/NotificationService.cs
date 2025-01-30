using MessagerieApp.Business.Interfaces;
using MessagerieApp.Models;
using MessagerieApp.Repositories;
using MessagerieApp.Repository;

namespace MessagerieApp.Business
{
    public class NotificationService : INotificationService
    {
        private readonly NotificationRepository _notificationRepository;
        private readonly UserRepository _utilisateurRepository;

        public NotificationService(
            NotificationRepository notificationRepository,
            UserRepository utilisateurRepository)
        {
            _notificationRepository = notificationRepository;
            _utilisateurRepository = utilisateurRepository;
        }

        public Task AddNotificationAsync(Notification notification)
        {
            throw new NotImplementedException();
        }

        public Task CreateNotificationAsync(Notification notification)
        {
            throw new NotImplementedException();
        }

        // Envoi de notification aux enseignants pour les besoins
        public void EnvoyerDemandeBesoinsAuxEnseignants(int chefDepartementId, int departementId)
        {
            // Récupérer tous les enseignants du département
            var enseignants = _utilisateurRepository.GetUserByIdAsync(departementId);

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

                _notificationRepository.AddNotificationAsync(notification);
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
            _notificationRepository.AddNotificationAsync(notificationAcceptation);

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
                    DateCreation = DateTime.Now
                };
            }
        }

        public Task<List<Notification>> GetAllNotificationsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Notification> GetNotificationByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task MarkAsReadAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
