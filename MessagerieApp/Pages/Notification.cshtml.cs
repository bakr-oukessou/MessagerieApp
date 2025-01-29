using MessagerieApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MessagerieApp.Pages
{
    public class NotificationModel : PageModel
    {
        public List<Notification> NotificationsList { get; set; } = new List<Notification>();

        public void OnGet()
        {
            // Fetch notifications from the database or service
            // Example:
            // NotificationsList = _notificationService.GetAllNotifications();
        }

        public void OnPost(int userId, string message)
        {
            // Handle creation of a new notification
            // Example:
            // var newNotification = new Notification { UserId = userId, Message = message };
            // _notificationService.AddNotification(newNotification);
        }
    }

}
