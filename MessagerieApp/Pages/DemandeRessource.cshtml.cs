using Microsoft.AspNetCore.Mvc.RazorPages;
using MessagerieApp.Models;
using MessagerieApp.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace MessagerieApp.Pages
{
    public class DemandeRessourceModel : PageModel
    {
        private readonly IDemandeRessourceService _demandeRessourceService;

        public DemandeRessourceModel(IDemandeRessourceService demandeRessourceService)
        {
            _demandeRessourceService = demandeRessourceService;
        }

        // List of resource requests to display
        public IEnumerable<DemandeRessource> Demandes { get; set; }

        // Properties for creating a new resource request
        [BindProperty]
        public int DepartmentId { get; set; }

        [BindProperty]
        public DateTime RequestDate { get; set; }

        [BindProperty]
        public string Status { get; set; }

        // Load resource requests on page load
        public async Task OnGetAsync()
        {
            Demandes = await _demandeRessourceService.GetAllDemandesAsync();
        }

        // Handle form submission to create a new resource request
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var demande = new DemandeRessource
            {
                DepartmentId = DepartmentId,
                RequestDate = RequestDate,
                Status = Status
            };

            await _demandeRessourceService.AddDemandeAsync(demande);

            return RedirectToPage("/DemandeRessource"); // Refresh the page
        }

        // Update the status of a resource request
        public async Task<IActionResult> OnPostUpdateStatusAsync(int id, string status)
        {
            await _demandeRessourceService.UpdateDemandeStatusAsync(id, status);
            return RedirectToPage("/DemandeRessource");
        }
    }
}