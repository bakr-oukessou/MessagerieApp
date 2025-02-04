using MessagerieApp.Business.Interfaces;
using MessagerieApp.Models;
using MessagerieApp.Repositories;
using MessagerieApp.Repository.Interfaces;
using MessagerieApp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MessagerieApp.Pages
{
    public class RessourceModel : PageModel
    {
        private readonly IRessourceService _ressourceService;
        private readonly IDemandeRessourceRepository _demandeRessourceRepository;

        public RessourceModel(IRessourceService ressourceService, IDemandeRessourceRepository demandeRessourceRepository)
        {
            _ressourceService = ressourceService;
            _demandeRessourceRepository = demandeRessourceRepository;
        }

        [BindProperty]
        public DemandeRessource NewDemandeRessource { get; set; } = new DemandeRessource();

        [BindProperty]
        public List<DemandeRessourceItem> NewDemandeRessourceItems { get; set; } = new List<DemandeRessourceItem>();

        public List<Ressource> Demandes { get; set; } = new List<Ressource>();

        public async Task<IActionResult> OnGetAsync()
        {
            Demandes = (List<Ressource>)await _ressourceService.GetAllRessourcesAsync();
            
            //Demandes = (await _demandeRessourceRepository.GetDemandesByDepartmentAsync(departmentId)).ToList();
            return Page();
        }

        public async Task<IActionResult> OnPostCreateDemandeAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            NewDemandeRessource.RequestDate = DateTime.UtcNow;
            NewDemandeRessource.Status = "Draft";
            await _demandeRessourceRepository.AddDemandeAsync(NewDemandeRessource);

            foreach (var item in NewDemandeRessourceItems)
            {
                item.ResourceRequestId = NewDemandeRessource.Id;
                await _demandeRessourceRepository.AddDemandeRessourceItemAsync(item);
            }

            TempData["SuccessMessage"] = "La demande de ressource a été créée avec succès.";
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostAssignResourceAsync(int demandeId, int userId)
        {
            await _ressourceService.AssignRessourceToUserAsync(demandeId, userId);
            return new JsonResult(new { success = true });
        }
    }
}