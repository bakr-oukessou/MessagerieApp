using MessagerieApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MessagerieApp.Pages
{
    public class DemandeRessourceModel : PageModel
    {
        public List<DemandeRessource> DemandeRessourceList { get; set; } = new List<DemandeRessource>();

        public void OnGet()
        {
            // Fetch resource requests from the database or service
            // Example:
            // DemandeRessourceList = _demandeRessourceService.GetAllDemandeRessources();
        }

        public void OnPost(int teacherId, string resourceType, int quantity)
        {
            // Handle creation of a new resource request
            // Example:
            // var newDemande = new DemandeRessource { TeacherId = teacherId, ResourceType = resourceType, Quantity = quantity };
            // _demandeRessourceService.AddDemandeRessource(newDemande);
        }
    }

}
