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
        Task<IEnumerable<Notification>> GetNotificationsByTypesAsync(List<NotificationType> type);
        Task<IEnumerable<Notification>> GetNotificationsByStatusAsync(StatutNotification status);
        Task<IEnumerable<Notification>> GetNotificationsByTypeAndUserAsync(NotificationType type, string userId);
        Task <IEnumerable<Notification>> GetNotificationsByTypeAsync(NotificationType maintenance);
    }
}