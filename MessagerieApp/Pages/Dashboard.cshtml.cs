using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;
using MessagerieApp.Business.Interfaces.TransactionData;
using MessagerieApp.Business.Interfaces.MasterData;

namespace MessagerieApp.Pages
{
	public class DashboardModel : PageModel
    {
        private readonly IRessourceService _ressourceService;
        private readonly INotificationService _notificationService;
        private readonly IMaintenanceDiagnosisService _maintenanceDiagnosisService;

        public DashboardModel(
            IRessourceService ressourceService,
            INotificationService notificationService,
            IMaintenanceDiagnosisService maintenanceDiagnosisService)
        {
            _ressourceService = ressourceService;
            _notificationService = notificationService;
            _maintenanceDiagnosisService = maintenanceDiagnosisService;
        }

        // Properties to hold dashboard data
        public int TotalResources { get; set; }
        public int TotalNotifications { get; set; }
        public int TotalMaintenanceDiagnoses { get; set; }
        public int PendingMaintenanceDiagnoses { get; set; }

        // Load data for the dashboard
        public async Task OnGetAsync()
        {
            // Fetch total number of resources
            var resources = await _ressourceService.GetAllRessourcesAsync();
            TotalResources = resources.Count();

            // Fetch total number of notifications
            var notifications = await _notificationService.GetAllNotificationsAsync();
            TotalNotifications = notifications.Count();

            // Fetch total number of maintenance diagnoses
            var diagnoses = await _maintenanceDiagnosisService.GetAllMaintenanceDiagnosesAsync();
            TotalMaintenanceDiagnoses = diagnoses.Count();

            // Fetch pending maintenance diagnoses (example: those requiring replacement)
            PendingMaintenanceDiagnoses = diagnoses.Count(d => d.RequiresReplacement);
        }
    }
}