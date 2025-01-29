using MessagerieApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MessagerieApp.Pages
{
    public class ConstatMaintenanceModel : PageModel
    {
        public List<ConstatMaintenance> ConstatMaintenanceList { get; set; } = new List<ConstatMaintenance>();

        public void OnGet()
        {
            // Fetch maintenance reports from the database or service
            // Example:
            // ConstatMaintenanceList = _maintenanceService.GetAllMaintenanceReports();
        }

        public void OnPost(int resourceId, string issueDescription, DateTime issueDate, string frequency, string issueType)
        {
            // Handle creation of a new maintenance report
            // Example:
            // var newReport = new ConstatMaintenance { ResourceId = resourceId, IssueDescription = issueDescription, IssueDate = issueDate, Frequency = frequency, IssueType = issueType };
            // _maintenanceService.AddMaintenanceReport(newReport);
        }
    }
}
