using MessagerieApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MessagerieApp.Pages
{
    public class ImprimanteModel : PageModel
    {
        public List<Imprimante> ImprimantesList { get; set; } = new List<Imprimante>();

        public void OnGet()
        {
            // Fetch printers from the database or service
            // Example:
            // ImprimantesList = _imprimanteService.GetAllImprimantes();
        }

        public void OnPost(string brand, string printSpeed, string resolution)
        {
            // Handle creation of a new printer
            // Example:
            // var newImprimante = new Imprimante { Brand = brand, PrintSpeed = printSpeed, Resolution = resolution };
            // _imprimanteService.AddImprimante(newImprimante);
        }
    }
}
