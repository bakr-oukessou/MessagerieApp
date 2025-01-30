using System.Collections.Generic;
using MessagerieApp.Models;

namespace MessagerieApp.Repository.Interfaces
{
    public interface INotificationRepository
    {
        Task<List<Notification>> GetAllNotificationsAsync();
        Task<Notification> GetNotificationByIdAsync(int id);
        Task AddNotificationAsync(Notification notification);
        Task MarkAsReadAsync(int id);
    }

}
