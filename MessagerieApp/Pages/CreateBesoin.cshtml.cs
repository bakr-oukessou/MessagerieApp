using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MessagerieApp.Business.Interfaces.TransactionData;

namespace MessagerieApp.Pages
{
	public class CreateBesoinModel : PageModel
    {
        private readonly INotificationService _notificationService;

        public CreateBesoinModel(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        [BindProperty]
        public int ChefDepartementId { get; set; }

        [BindProperty]
        public int DepartementId { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Send notifications to teachers
            await _notificationService.EnvoyerDemandeBesoinsAuxEnseignants(ChefDepartementId, DepartementId);

            return RedirectToPage("/Success"); // Redirect to a success page
        }
    }
}