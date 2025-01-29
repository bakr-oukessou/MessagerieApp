using MessagerieApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MessagerieApp.Pages
{
    public class SupplierModel : PageModel
    {
        public List<Supplier> SuppliersList { get; set; } = new List<Supplier>();

        public void OnGet()
        {
            // Fetch suppliers from the database or service
            // Example:
            // SuppliersList = _supplierService.GetAllSuppliers();
        }

        public void OnPost(string companyName, string address, string website, string manager)
        {
            // Handle creation of a new supplier
            // Example:
            // var newSupplier = new Supplier { CompanyName = companyName, Address = address, Website = website, Manager = manager };
            // _supplierService.AddSupplier(newSupplier);
        }
    }
}
