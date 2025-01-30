using Microsoft.AspNetCore.Mvc.RazorPages;
using MessagerieApp.Models;
using MessagerieApp.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace MessagerieApp.Pages
{
    public class MaintenanceDiagnosisModel : PageModel
    {
        private readonly IMaintenanceDiagnosisService _maintenanceDiagnosisService;

        public MaintenanceDiagnosisModel(IMaintenanceDiagnosisService maintenanceDiagnosisService)
        {
            _maintenanceDiagnosisService = maintenanceDiagnosisService;
        }

        // List of maintenance diagnoses to display
        public IEnumerable<MaintenanceDiagnosis> MaintenanceDiagnoses { get; set; }

        // Properties for creating/editing a maintenance diagnosis
        [BindProperty]
        public MaintenanceDiagnosis MaintenanceDiagnosis { get; set; }

        // Load maintenance diagnoses on page load
        public async Task OnGetAsync()
        {
            MaintenanceDiagnoses = await _maintenanceDiagnosisService.GetAllMaintenanceDiagnosesAsync();
        }

        // Handle form submission to create a new maintenance diagnosis
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _maintenanceDiagnosisService.AddMaintenanceDiagnosisAsync(MaintenanceDiagnosis);

            return RedirectToPage("/MaintenanceDiagnosis"); // Refresh the page
        }

        // Handle form submission to update a maintenance diagnosis
        public async Task<IActionResult> OnPostUpdateAsync(string id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _maintenanceDiagnosisService.UpdateMaintenanceDiagnosisAsync(MaintenanceDiagnosis);

            return RedirectToPage("/MaintenanceDiagnosis"); // Refresh the page
        }

        // Handle form submission to delete a maintenance diagnosis
        public async Task<IActionResult> OnPostDeleteAsync(string id)
        {
            await _maintenanceDiagnosisService.DeleteMaintenanceDiagnosisAsync(id);

            return RedirectToPage("/MaintenanceDiagnosis"); // Refresh the page
        }
    }
}