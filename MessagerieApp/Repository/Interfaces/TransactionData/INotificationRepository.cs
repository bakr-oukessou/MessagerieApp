using System.Collections.Generic;
using System.Threading.Tasks;
using MessagerieApp.Models.TransactionData;

namespace MessagerieApp.Repository.Interfaces.TransactionData
{
	public interface INotificationRepository
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

		Task<Notification> GetByIdAsync(int id);
		Task<IEnumerable<Notification>> GetAllAsync();
		Task AddAsync(Notification notification);
		Task UpdateAsync(Notification notification);
		Task DeleteAsync(int id);
		Task<IEnumerable<Notification>> GetByUserIdAsync(int userId);
		Task<IEnumerable<Notification>> GetUnreadNotificationsAsync(int userId);


		Task SendAsync(Notification notification);
		Task MarkAsReadAsync(int notificationId);
		Task ArchiveAsync(int notificationId);
	}
}