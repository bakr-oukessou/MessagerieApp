using System.Collections.Generic;
using System.Threading.Tasks;
using MessagerieApp.Models.TransactionData;

namespace MessagerieApp.Business.Interfaces.TransactionData
{
	public interface INotificationService
	{
		Task<IEnumerable<Notification>> GetAllNotificationsAsync();
		Task<Notification> GetNotificationByIdAsync(int id);
		Task AddNotificationAsync(Notification notification);
		Task UpdateNotificationAsync(Notification notification);
		Task DeleteNotificationAsync(int id);
		Task MarkNotificationAsReadAsync(int notificationId);
		Task MarkNotificationAsArchivedAsync(int notificationId);
		Task<IEnumerable<Notification>> GetNotificationsByUserAsync(int userId);
		Task<IEnumerable<Notification>> GetNotificationsByTypeAsync(NotificationType type);
		Task<IEnumerable<Notification>> GetNotificationsByStatusAsync(StatutNotification status);
		Task EnvoyerDemandeBesoinsAuxEnseignants(int chefDepartementId, int departementId);
		Task EnvoyerNotificationsAppelOffre(int responsableRessourcesId, int fournisseurSelectionneId, List<int> autresFournisseursId);

		Task<List<Notification>> ObtenirNotificationsParUtilisateurAsync(int userId);
		Task<Notification> ObtenirNotificationParIdAsync(int id);
		Task EnvoyerNotificationAsync(Notification notification);
		Task MarquerCommeLueAsync(int notificationId);
		Task ArchiverNotificationAsync(int notificationId);


		// Workflow de notifications
		Task NotifierNouvelleDemande(DemandeRessource demande);
		Task NotifierValidationChef(DemandeRessource demande);
		Task NotifierValidationResponsable(DemandeRessource demande);
		Task NotifierRejetDemande(DemandeRessource demande, string raison);

		// Gestion des notifications
		Task MarquerCommeLue(int notificationId);
		Task ArchiverNotification(int notificationId);
		Task<IEnumerable<Notification>> ObtenirNotificationsUtilisateur(int userId);
	}
}