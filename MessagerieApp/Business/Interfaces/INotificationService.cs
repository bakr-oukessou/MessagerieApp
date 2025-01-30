using MessagerieApp.Models;

namespace MessagerieApp.Business.Interfaces
{
    public interface INotificationService
    {
        Task<List<Notification>> GetAllNotificationsAsync();
        Task<Notification> GetNotificationByIdAsync(int id);
        Task AddNotificationAsync(Notification notification);
        Task MarkAsReadAsync(int id);
        Task CreateNotificationAsync(Notification notification);

    }
}
