using Microsoft.AspNetCore.Mvc.RazorPages;
using MessagerieApp.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace MessagerieApp.Pages
{
    public class AppelOffreModel : PageModel
    {
        private readonly INotificationService _notificationService;

        public AppelOffreModel(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        [BindProperty]
        public int ResponsableRessourcesId { get; set; }

        [BindProperty]
        public int FournisseurSelectionneId { get; set; }

        [BindProperty]
        public List<int> AutresFournisseursId { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Send notifications for call for tenders
            await _notificationService.EnvoyerNotificationsAppelOffre(ResponsableRessourcesId, FournisseurSelectionneId, AutresFournisseursId);

            return RedirectToPage("/Success"); // Redirect to a success page
        }
    }
}