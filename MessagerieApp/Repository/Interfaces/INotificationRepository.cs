using System.Collections.Generic;
using System.Threading.Tasks;
using MessagerieApp.Models;

namespace MessagerieApp.Repositories
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
    }
}