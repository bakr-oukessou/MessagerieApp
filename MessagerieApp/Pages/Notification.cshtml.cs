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

        [TempData]
        public string SuccessMessage { get; set; }

        public NotificationModel(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }


        [BindProperty]
        public Notification NewNotification { get; set; }


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
            try
            {
                Notifications = await _notificationService.GetAllNotificationsAsync();
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine($"Error: {ex.Message}");
                Notifications = new List<Notification>(); // Fallback to an empty list
            }
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
            SuccessMessage = "La notification a été ajoutée avec succès.";


            return RedirectToPage("/Notification"); // Refresh the page
        }
        public async Task<IActionResult> OnPostCreateAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _notificationService.AddNotificationAsync(NewNotification);

            TempData["SuccessMessage"] = "La notification a été ajoutée avec succès.";
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            await _notificationService.DeleteNotificationAsync(id);

            TempData["SuccessMessage"] = "La notification a été supprimée avec succès.";
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostEditAsync(int id)
        {
            var notification = await _notificationService.GetNotificationByIdAsync(id);
            if (notification == null)
            {
                return NotFound();
            }

            // Update logic here (e.g., bind to a form and save changes)
            await _notificationService.UpdateNotificationAsync(notification);

            TempData["SuccessMessage"] = "La notification a été modifiée avec succès.";
            return RedirectToPage();
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