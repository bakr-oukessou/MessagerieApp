using MessagerieApp.Repositories;
using MessagerieApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using MessagerieApp.Repository.Interfaces;

namespace MessagerieApp.Services
{
    public class NotificationService : INotificationService
    {
        private readonly INotificationRepository _notificationRepository;
        private readonly IUserRepository _utilisateurRepository;

        public NotificationService(INotificationRepository notificationRepository, IUserRepository utilisateurRepository)
        {
            _notificationRepository = notificationRepository;
            _utilisateurRepository = utilisateurRepository;
        }

        public async Task EnvoyerDemandeBesoinsAuxEnseignants(int chefDepartementId, int departementId)
        {
            // Récupérer tous les enseignants du département
            var enseignants = await _utilisateurRepository.GetUsersByDepartmentAsync(departementId);

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

                await _notificationRepository.AddNotificationAsync(notification);
            }
        }

        public async Task EnvoyerNotificationsAppelOffre(int responsableRessourcesId, int fournisseurSelectionneId, List<int> autresFournisseursId)
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
            await _notificationRepository.AddNotificationAsync(notificationAcceptation);

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
                await _notificationRepository.AddNotificationAsync(notificationRejet);
            }
        }

        public async Task<IEnumerable<Notification>> GetAllNotificationsAsync()
        {
            return await _notificationRepository.GetAllNotificationsAsync();
        }

        public async Task<Notification> GetNotificationByIdAsync(int id)
        {
            return await _notificationRepository.GetNotificationByIdAsync(id);
        }

        public async Task AddNotificationAsync(Notification notification)
        {
            await _notificationRepository.AddNotificationAsync(notification);
        }

        public async Task UpdateNotificationAsync(Notification notification)
        {
            await _notificationRepository.UpdateNotificationAsync(notification);
        }

        public async Task DeleteNotificationAsync(int id)
        {
            await _notificationRepository.DeleteNotificationAsync(id);
        }

        public async Task MarkNotificationAsReadAsync(int notificationId)
        {
            await _notificationRepository.MarkNotificationAsReadAsync(notificationId);
        }

        public async Task MarkNotificationAsArchivedAsync(int notificationId)
        {
            await _notificationRepository.MarkNotificationAsArchivedAsync(notificationId);
        }

        public async Task<IEnumerable<Notification>> GetNotificationsByUserAsync(int userId)
        {
            return await _notificationRepository.GetNotificationsByUserAsync(userId);
        }

        public async Task<IEnumerable<Notification>> GetNotificationsByTypeAsync(NotificationType type)
        {
            return await _notificationRepository.GetNotificationsByTypeAsync(type);
        }

        public async Task<IEnumerable<Notification>> GetNotificationsByStatusAsync(StatutNotification status)
        {
            return await _notificationRepository.GetNotificationsByStatusAsync(status);
        }
    }
}