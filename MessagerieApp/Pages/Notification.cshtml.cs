using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MessagerieApp.Models.TransactionData;
using MessagerieApp.Business.Interfaces.TransactionData;

namespace MessagerieApp.Pages
{
	public class NotificationModel : PageModel
    {
        private readonly INotificationService _notificationService;

        public NotificationModel(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        public List<Notification> Notifications { get; set; } = new();

        [BindProperty]
        public Notification NewNotification { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            Notifications = (List<Notification>)await _notificationService.GetAllNotificationsAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostCreateAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            NewNotification.DateCreation = DateTime.Now;
            NewNotification.Statut = 0; // 0 = NonLue
            await _notificationService.AddNotificationAsync(NewNotification);

            TempData["SuccessMessage"] = "Notification créée avec succès.";
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostMarkAsReadAsync(int id)
        {
            await _notificationService.MarkNotificationAsReadAsync(id);
            TempData["SuccessMessage"] = "Notification marquée comme lue.";
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostMarkAsArchivedAsync(int id)
        {
            await _notificationService.MarkNotificationAsArchivedAsync(id);
            TempData["SuccessMessage"] = "Notification archivée.";
            return RedirectToPage();
        }
    }
}
