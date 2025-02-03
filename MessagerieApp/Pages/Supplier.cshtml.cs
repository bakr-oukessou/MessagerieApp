using Microsoft.AspNetCore.Mvc.RazorPages;
using MessagerieApp.Models;
using MessagerieApp.Services;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace MessagerieApp.Pages
{
    public class SupplierModel : PageModel
    {
        private readonly ISupplierService _supplierService;

        public SupplierModel(ISupplierService supplierService)
        {
            _supplierService = supplierService;
        }

        public IEnumerable<Supplier> Suppliers { get; set; }

        [BindProperty]
        public Supplier Supplier { get; set; }

        public async Task OnGetAsync()
        {
            Suppliers = await _supplierService.GetAllSuppliersAsync();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _supplierService.AddSupplierAsync(Supplier);
            return RedirectToPage("/Supplier");
        }

        public async Task<IActionResult> OnPostUpdateAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _supplierService.UpdateSupplierAsync(Supplier);
            return RedirectToPage("/Supplier");
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            await _supplierService.DeleteSupplierAsync(id);
            return RedirectToPage("/Supplier");
        }

        public async Task<IActionResult> OnPostBlacklistAsync(int id, string reason)
        {
            await _supplierService.BlacklistSupplierAsync(id, reason);
            return RedirectToPage("/Supplier");
        }

        public async Task<IActionResult> OnPostRemoveFromBlacklistAsync(int id)
        {
            await _supplierService.RemoveFromBlacklistAsync(id);
            return RedirectToPage("/Supplier");
        }
    }
}