using MessagerieApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MessagerieApp.Pages
{
    public class MaintenanceDiagnosisModel : PageModel
    {
        public List<MaintenanceDiagnosis> MaintenanceDiagnosisList { get; set; } = new List<MaintenanceDiagnosis>();

        public void OnGet()
        {
            // Fetch maintenance diagnoses from the database or service
            // Example:
            // MaintenanceDiagnosisList = _maintenanceDiagnosisService.GetAllMaintenanceDiagnoses();
        }

        public void OnPost(int resourceId, string diagnosis, int technicianId)
        {
            // Handle creation of a new maintenance diagnosis
            // Example:
            // var newDiagnosis = new MaintenanceDiagnosis { ResourceId = resourceId, Diagnosis = diagnosis, TechnicianId = technicianId };
            // _maintenanceDiagnosisService.AddMaintenanceDiagnosis(newDiagnosis);
        }
    }
}
