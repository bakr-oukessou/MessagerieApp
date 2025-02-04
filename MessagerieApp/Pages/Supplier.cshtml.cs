using Microsoft.AspNetCore.Mvc.RazorPages;
using MessagerieApp.Models;
using MessagerieApp.Services;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MessagerieApp.Business.Interfaces;

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
                try
                {
                    await _supplierService.DeleteSupplierAsync(id);
                }
                catch
                {
                    TempData["ErrorMessage"] = "Erreur lors de la suppression.";
                    return RedirectToPage();
                }

                TempData["SuccessMessage"] = "supprimé avec succès.";
                return RedirectToPage();
       
            
        }

        public async Task<IActionResult> OnPostBlacklistAsync(int id, string reason)
        {
            try
            {
            await _supplierService.BlacklistSupplierAsync(id, reason);

            }catch
            {
                TempData["ErrorMessage"] = "Erreur.";
                return RedirectToPage();
            }
            TempData["SuccessMessage"] = "Fournisseur blacklisted";
            return RedirectToPage("/Supplier");
        }

        public async Task<IActionResult> OnPostRemoveFromBlacklistAsync(int id)
        {
            await _supplierService.RemoveFromBlacklistAsync(id);
            return RedirectToPage("/Supplier");
        }
    }
}