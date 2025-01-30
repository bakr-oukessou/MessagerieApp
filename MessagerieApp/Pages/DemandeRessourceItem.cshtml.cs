using Microsoft.AspNetCore.Mvc.RazorPages;
using MessagerieApp.Models;
using MessagerieApp.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace MessagerieApp.Pages
{
    public class DemandeRessourceItemModel : PageModel
    {
        private readonly IDemandeRessourceService _demandeRessourceService;

        public DemandeRessourceItemModel(IDemandeRessourceService demandeRessourceService)
        {
            _demandeRessourceService = demandeRessourceService;
        }

        // The resource request being edited
        public DemandeRessource Demande { get; set; }

        // Properties for adding a new item to the request
        [BindProperty]
        public int DemandeRessourceId { get; set; }

        [BindProperty]
        public string ResourceType { get; set; }

        [BindProperty]
        public int Quantity { get; set; }

        [BindProperty]
        public string Specifications { get; set; }

        // Load the resource request and its items on page load
        public async Task OnGetAsync(int id)
        {
            Demande = await _demandeRessourceService.GetDemandeByIdAsync(id);
        }

        // Handle form submission to add a new item to the request
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var item = new DemandeRessourceItem
            {
                ResourceRequestId = DemandeRessourceId,
                Type = ResourceType,
                Quantity = Quantity,
                Specifications = Specifications
            };

            // Add the item to the request (you need to implement this method in the service)
            await _demandeRessourceService.AddItemToDemandeAsync(DemandeRessourceId, item);

            return RedirectToPage("/DemandeRessourceItem", new { id = DemandeRessourceId }); // Refresh the page
        }
    }
}