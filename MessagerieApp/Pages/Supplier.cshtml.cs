using Microsoft.AspNetCore.Mvc.RazorPages;
using MessagerieApp.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MessagerieApp.Business.Interfaces.TransversalData;

namespace MessagerieApp.Pages
{
	public class SupplierModel : PageModel
    {
        private readonly ISupplierService _supplierService;

        public SupplierModel(ISupplierService supplierService)
        {
            _supplierService = supplierService;
        }

        // List of suppliers to display
        public IEnumerable<Fournisseur> Suppliers { get; set; }

        // Properties for creating/editing a supplier
        [BindProperty]
        public Fournisseur Supplier { get; set; }

        // Load suppliers on page load
        public async Task OnGetAsync()
        {
            Suppliers = await _supplierService.GetAllSuppliersAsync();
        }

        // Handle form submission to create a new supplier
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _supplierService.AddSupplierAsync(Supplier);

            return RedirectToPage("/Supplier"); // Refresh the page
        }

        // Handle form submission to update a supplier
        public async Task<IActionResult> OnPostUpdateAsync(int id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _supplierService.UpdateSupplierAsync(Supplier);

            return RedirectToPage("/Supplier"); // Refresh the page
        }

        // Handle form submission to delete a supplier
        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            await _supplierService.DeleteSupplierAsync(id);

            return RedirectToPage("/Supplier"); // Refresh the page
        }

        // Handle form submission to blacklist a supplier
        public async Task<IActionResult> OnPostBlacklistAsync(int id, string reason)
        {
            await _supplierService.BlacklistSupplierAsync(id, reason);

            return RedirectToPage("/Supplier"); // Refresh the page
        }

        // Handle form submission to remove a supplier from the blacklist
        public async Task<IActionResult> OnPostRemoveFromBlacklistAsync(int id)
        {
            await _supplierService.RemoveFromBlacklistAsync(id);

            return RedirectToPage("/Supplier"); // Refresh the page
        }
    }
}