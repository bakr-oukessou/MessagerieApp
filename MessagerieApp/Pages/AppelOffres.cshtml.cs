using MessagerieApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MessagerieApp.Pages
{
    public class AppelOffresModel : PageModel
    {
        public List<AppelOffres> AppelOffresList { get; set; } = new List<AppelOffres>();

        public void OnGet()
        {
            // Fetch AppelOffres from the database or service
            // Example:
            // AppelOffresList = _appelOffreService.GetAllAppelOffres();
        }

        public void OnPost(DateTime startDate, DateTime endDate)
        {
            // Handle creation of a new AppelOffre
            // Example:
            // var newAppelOffre = new AppelOffre { StartDate = startDate, EndDate = endDate };
            // _appelOffreService.AddAppelOffre(newAppelOffre);
        }
    }

   
}
