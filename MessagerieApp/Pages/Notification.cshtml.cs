using Microsoft.AspNetCore.Mvc.RazorPages;
using MessagerieApp.Models;
using MessagerieApp.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace MessagerieApp.Pages
{
    public class NotificationModel : PageModel
    {
        private readonly INotificationService _notificationService;

        public NotificationModel(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        // List of notifications to display
        public IEnumerable<Notification> Notifications { get; set; }

        // Properties for creating a new notification
        [BindProperty]
        public int EmetteurId { get; set; }

        [BindProperty]
        public int DestinataireId { get; set; }

        [BindProperty]
        public string Titre { get; set; }

        [BindProperty]
        public string Corps { get; set; }

        [BindProperty]
        public NotificationType Type { get; set; }

        // Load notifications on page load
        public async Task OnGetAsync()
        {
            Notifications = await _notificationService.GetAllNotificationsAsync();
        }

        // Handle form submission to create a new notification
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var notification = new Notification
            {
                EmetteurId = EmetteurId,
                DestinataireId = DestinataireId,
                Titre = Titre,
                Corps = Corps,
                Type = Type,
                Statut = StatutNotification.NonLue,
                DateCreation = DateTime.Now
            };

            await _notificationService.AddNotificationAsync(notification);

            return RedirectToPage("/Notification"); // Refresh the page
        }

        // Mark a notification as read
        public async Task<IActionResult> OnPostMarkAsReadAsync(int id)
        {
            await _notificationService.MarkNotificationAsReadAsync(id);
            return RedirectToPage("/Notification");
        }

        // Mark a notification as archived
        public async Task<IActionResult> OnPostMarkAsArchivedAsync(int id)
        {
            await _notificationService.MarkNotificationAsArchivedAsync(id);
            return RedirectToPage("/Notification");
        }
    }
}