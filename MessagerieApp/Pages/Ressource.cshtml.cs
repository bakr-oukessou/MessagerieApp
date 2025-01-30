using Microsoft.AspNetCore.Mvc.RazorPages;
using MessagerieApp.Models;
using MessagerieApp.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using MessagerieApp.Business.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MessagerieApp.Pages
{
    public class RessourceModel : PageModel
    {
        private readonly IRessourceService _ressourceService;

        public RessourceModel(IRessourceService ressourceService)
        {
            _ressourceService = ressourceService;
        }

        // List of resources to display
        public IEnumerable<Ressource> Ressources { get; set; }

        // Properties for creating a new resource
        [BindProperty]
        public string InventoryNumber { get; set; }

        [BindProperty]
        public string Type { get; set; }

        [BindProperty]
        public string Brand { get; set; }

        [BindProperty]
        public int? DepartmentId { get; set; }

        [BindProperty]
        public int? AssignedToUserId { get; set; }

        [BindProperty]
        public DateTime AcquisitionDate { get; set; }

        [BindProperty]
        public DateTime? WarrantyEndDate { get; set; }

        [BindProperty]
        public string Status { get; set; }

        // Load resources on page load
        public async Task OnGetAsync()
        {
            Ressources = await _ressourceService.GetAllRessourcesAsync();
        }

        // Handle form submission to create a new resource
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var ressource = new Ressource
            {
                InventoryNumber = InventoryNumber,
                Type = Type,
                Brand = Brand,
                DepartmentId = DepartmentId,
                AssignedToUserId = AssignedToUserId,
                AcquisitionDate = AcquisitionDate,
                WarrantyEndDate = WarrantyEndDate,
                Status = Status
            };

            await _ressourceService.AddRessourceAsync(ressource);

            return RedirectToPage("/Ressource"); // Refresh the page
        }
    }
}