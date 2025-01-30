using Microsoft.AspNetCore.Mvc.RazorPages;
using MessagerieApp.Models;
using MessagerieApp.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace MessagerieApp.Pages
{
    public class DepartementModel : PageModel
    {
        private readonly IDepartementService _departementService;

        public DepartementModel(IDepartementService departementService)
        {
            _departementService = departementService;
        }

        // List of departments to display
        public IEnumerable<Departement> Departements { get; set; }

        // Properties for creating/editing a department
        [BindProperty]
        public Departement Departement { get; set; }

        // Load departments on page load
        public async Task OnGetAsync()
        {
            Departements = await _departementService.GetAllDepartementsAsync();
        }

        // Handle form submission to create a new department
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _departementService.AddDepartementAsync(Departement);

            return RedirectToPage("/Departement"); // Refresh the page
        }

        // Handle form submission to update a department
        public async Task<IActionResult> OnPostUpdateAsync(int id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _departementService.UpdateDepartementAsync(Departement);

            return RedirectToPage("/Departement"); // Refresh the page
        }

        // Handle form submission to delete a department
        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            await _departementService.DeleteDepartementAsync(id);

            return RedirectToPage("/Departement"); // Refresh the page
        }
    }
}