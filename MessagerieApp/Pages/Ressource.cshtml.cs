using MessagerieApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MessagerieApp.Pages
{
    public class RessourceModel : PageModel
    {
        public List<Ressource> RessourcesList { get; set; } = new List<Ressource>();

        public void OnGet()
        {
            // Fetch resources from the database or service
            // Example:
            // RessourcesList = _ressourceService.GetAllRessources();
        }

        public void OnPost(string resourceType, int resourceId, int departmentId)
        {
            // Handle creation of a new resource
            // Example:
            // var newRessource = new Ressource { ResourceType = resourceType, ResourceId = resourceId, DepartmentId = departmentId };
            // _ressourceService.AddRessource(newRessource);
        }
    }
}
