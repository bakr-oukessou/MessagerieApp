using MessagerieApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MessagerieApp.Pages
{
    public class OffresModel : PageModel
    {
        public List<Offre> OffresList { get; set; } = new List<Offre>();

        public void OnGet()
        {
            // Fetch offers from the database or service
            // Example:
            // OffresList = _offreService.GetAllOffres();
        }

        public void OnPost(int supplierId, int appelOffresId, decimal price, DateTime deliveryDate)
        {
            // Handle creation of a new offer
            // Example:
            // var newOffre = new Offre { SupplierId = supplierId, AppelOffresId = appelOffresId, Price = price, DeliveryDate = deliveryDate };
            // _offreService.AddOffre(newOffre);
        }
    }
}
