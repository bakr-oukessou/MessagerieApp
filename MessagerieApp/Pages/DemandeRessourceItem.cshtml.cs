using MessagerieApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MessagerieApp.Pages
{
    public class DemandeRessourceItemModel : PageModel
    {
        public List<DemandeRessourceItem> DemandeRessourceItemList { get; set; } = new List<DemandeRessourceItem>();

        public void OnGet()
        {
            // Fetch DemandeRessource items from the database or service
            // Example:
            // DemandeRessourceItemList = _demandeRessourceItemService.GetAllDemandeRessourceItems();
        }

        public void OnPost(int demandeRessourceId, string resourceType, int quantity)
        {
            // Handle creation of a new DemandeRessource item
            // Example:
            // var newItem = new DemandeRessourceItem { DemandeRessourceId = demandeRessourceId, ResourceType = resourceType, Quantity = quantity };
            // _demandeRessourceItemService.AddDemandeRessourceItem(newItem);
        }
    }
}
