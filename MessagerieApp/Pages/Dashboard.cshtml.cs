using MessagerieApp.Business;
using MessagerieApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MessagerieApp.Pages
{
    public class DashboardModel : PageModel
    {
        private readonly OffreService _besoinService;

        public DashboardModel(OffreService besoinService)
        {
            _besoinService = besoinService;
        }

        public List<Offre> Besoins { get; set; }

        public async Task OnGetAsync()
        {
            Besoins = (List<Offre>)await _besoinService.GetAllOffresAsync();
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            await _besoinService.DeleteOffreAsync(id);
            return RedirectToPage();
        }
    }
}
