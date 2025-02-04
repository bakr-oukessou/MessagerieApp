using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MessagerieApp.Models;
using MessagerieApp.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MessagerieApp.Business.Interfaces;
using MessagerieApp.Repositories;

namespace MessagerieApp.Pages
{
    public class NotificationModel : PageModel
    {
        private readonly INotificationService _notificationService;
        private readonly INotificationRepository _notificationRepository;
        private readonly IUserService _userService; // Service to get user role

        public NotificationModel(INotificationRepository notificationRepository, IUserService userService)
        {
            _notificationRepository = notificationRepository;
            _userService = userService;
        }
        public List<Notification> Notifications { get; set; } = new();

        [BindProperty]
        public Notification NewNotification { get; set; }
        public string UserRole { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            UserRole = await _userService.GetUserRoleAsync(User.Identity.Name);

            // Fetch notifications based on the user's role
            Notifications = await GetFilteredNotificationsAsync(UserRole);
            Notifications = (List<Notification>)await _notificationService.GetAllNotificationsAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostCreateAsync()
        {
             if (!ModelState.IsValid)
            {
                return Page();
            }

            // Set the creation date
            NewNotification.DateCreation = DateTime.Now;
            NewNotification.Statut = 0; // 0 = NonLue

            // Add the notification
            await _notificationRepository.AddNotificationAsync(NewNotification);

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
        private async Task<List<Notification>> GetFilteredNotificationsAsync(string userRole)
        {
            var notifications = new List<Notification>();

            switch (userRole)
            {
                case "Enseignant":
                    // Enseignant can only view their own DemandeBesoins
                    notifications = (List<Notification>)await _notificationRepository.GetNotificationsByTypeAndUserAsync(NotificationType.DemandeBesoins, User.Identity.Name);
                    break;

                case "ChefDepartment":
                    // ChefDepartment can view DemandeBesoins and AppelOffre
                    notifications = (List<Notification>)await _notificationRepository.GetNotificationsByTypesAsync(new List<NotificationType> { NotificationType.DemandeBesoins, NotificationType.AppelOffre });
                    break;

                case "ResponsableRessource":
                    // ResponsableRessource can view Maintenance, AppelOffre, and Livraison
                    notifications = (List<Notification>)await _notificationRepository.GetNotificationsByTypesAsync(new List<NotificationType> { NotificationType.Maintenance, NotificationType.AppelOffre, NotificationType.Livraison });
                    break;

                case "Fournisseur":
                    // Fournisseur can view AppelOffre and Livraison
                    notifications = (List<Notification>)await _notificationRepository.GetNotificationsByTypesAsync(new List<NotificationType> { NotificationType.AppelOffre, NotificationType.Livraison });
                    break;

                case "ServiceMaintenance":
                    // ServiceMaintenance can view Maintenance notifications
                    notifications = (List<Notification>)await _notificationRepository.GetNotificationsByTypeAsync(NotificationType.Maintenance);
                    break;

                default:
                    // Default: show all notifications (for admin or undefined roles)
                    notifications = (List<Notification>)await _notificationRepository.GetAllNotificationsAsync();
                    break;
            }

            return notifications;
        }
    }
}
