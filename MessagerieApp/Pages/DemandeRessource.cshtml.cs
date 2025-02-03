using Microsoft.AspNetCore.Mvc.RazorPages;
using MessagerieApp.Models;
using MessagerieApp.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MessagerieApp.Business.Interfaces.TransactionData;
using MessagerieApp.Business.Interfaces.TransversalData;
using MessagerieApp.Models.TransactionData;

namespace MessagerieApp.Pages
{
	public class DemandeRessourceModel : PageModel
	{
		private readonly IDemandeRessourceService _demandeRessourceService;
		private readonly IUserService _userService;

		public DemandeRessourceModel(
			IDemandeRessourceService demandeRessourceService,
			IUserService userService)
		{
			_demandeRessourceService = demandeRessourceService;
			_userService = userService;
		}

		[BindProperty]
		public string Instruction { get; set; }

		[BindProperty]
		public DemandeRessource NouvelleDemande { get; set; }

		public bool EstEnseignant { get; set; }
		public bool EstChefDepartement { get; set; }
		public IEnumerable<DemandeRessource> Demandes { get; set; }

		public async Task OnGetAsync()
		{
			var user = await _userService.GetUserByIdAsync(int.Parse(User.FindFirst("sub").Value));

			EstEnseignant = await _userService.EstEnseignant(user.Id);
			EstChefDepartement = await _userService.EstChefDepartement(user.Id);

			if (EstChefDepartement)
			{
				Demandes = await _demandeRessourceService.ObtenirHistoriqueDemandes(user.DepartmentId.Value);
			}
			else if (EstEnseignant)
			{
				Demandes = await _demandeRessourceService.GetDemandesParUtilisateurAsync(user.Id);
			}
		}

		public async Task<IActionResult> OnPostInitierDemandeAsync()
		{
			var user = await _userService.GetUserByIdAsync(int.Parse(User.FindFirst("sub").Value));
			if (!await _userService.EstChefDepartement(user.Id))
				return Forbid();

			await _demandeRessourceService.InitierDemandeAuxEnseignants(user.Id, user.DepartmentId.Value);
			return RedirectToPage();
		}

		public async Task<IActionResult> OnPostCreerDemandeAsync()
		{
			var user = await _userService.GetUserByIdAsync(int.Parse(User.FindFirst("sub").Value));
			if (!await _userService.EstEnseignant(user.Id))
				return Forbid();

			NouvelleDemande.DepartmentId = user.DepartmentId.Value;
			await _demandeRessourceService.CreerDemandeAsync(NouvelleDemande);
			return RedirectToPage();
		}

		public async Task<IActionResult> OnPostValiderDemandeAsync(int id)
		{
			var user = await _userService.GetUserByIdAsync(int.Parse(User.FindFirst("sub").Value));
			if (!await _userService.EstChefDepartement(user.Id))
				return Forbid();

			await _demandeRessourceService.ValiderDemandeAsync(id, user.Id, "Demande approuvée");
			return RedirectToPage();
		}

		public async Task<IActionResult> OnPostRejeterDemandeAsync(int id)
		{
			var user = await _userService.GetUserByIdAsync(int.Parse(User.FindFirst("sub").Value));
			if (!await _userService.EstChefDepartement(user.Id))
				return Forbid();

			await _demandeRessourceService.RejeterDemandeAsync(id, user.Id, "Demande rejetée");
			return RedirectToPage();
		}
	}
}