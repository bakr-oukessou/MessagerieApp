using MessagerieApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MessagerieApp.Pages
{
    public class AppelOffresItemModel : PageModel
    {
        public List<AppelOffresItem> AppelOffresItemList { get; set; } = new List<AppelOffresItem>();

        public void OnGet()
        {
            // Fetch AppelOffres items from the database or service
            // Example:
            // AppelOffresItemList = _appelOffresItemService.GetAllAppelOffresItems();
        }

        public void OnPost(int appelOffresId, string resourceType, int quantity)
        {
            // Handle creation of a new AppelOffres item
            // Example:
            // var newItem = new AppelOffresItem { AppelOffresId = appelOffresId, ResourceType = resourceType, Quantity = quantity };
            // _appelOffresItemService.AddAppelOffresItem(newItem);
        }
    }
}
