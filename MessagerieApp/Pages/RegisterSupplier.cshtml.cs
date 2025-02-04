using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MessagerieApp.Models;
using MessagerieApp.Repositories;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using System.Text;

namespace MessagerieApp.Pages
{
	public class RegisterSupplierModel : PageModel
	{
		private readonly SupplierRepository _supplierRepo;
		private readonly UserRepository _userRepo;

		[BindProperty]
		public SupplierRegistrationDto Registration { get; set; }

		public RegisterSupplierModel(
			SupplierRepository supplierRepo,
			UserRepository userRepo)
		{
			_supplierRepo = supplierRepo;
			_userRepo = userRepo;
		}

		public async Task<IActionResult> OnPostAsync()
		{
			if (!ModelState.IsValid) return Page();

			// V�rifier l'email existant
			if (await _userRepo.EmailExists(Registration.Email))
			{
				ModelState.AddModelError("Registration.Email", "Cet email est d�j� utilis�");
				return Page();
			}

			// Cr�er le fournisseur
			var supplier = new Supplier { CompanyName = Registration.CompanyName };
			await _supplierRepo.AddSupplierAsync(supplier);

			// Cr�er l'utilisateur associ�
			using var hmac = new HMACSHA512();
			var user = new User
			{
				UserName = Registration.CompanyName,
				Email = Registration.Email,
				Role = UserRole.Supplier,
				SupplierId = supplier.Id,
				Password = Registration.Password
            };

			await _userRepo.AddUserAsync(user);

			return RedirectToPage("/Login");
		}


		public class SupplierRegistrationDto
		{
			[Required(ErrorMessage = "Le nom de la soci�t� est requis.")]
			public string CompanyName { get; set; }

			[Required(ErrorMessage = "L'email est requis.")]
			[EmailAddress(ErrorMessage = "Format d'email invalide.")]
			public string Email { get; set; }

			[Required(ErrorMessage = "Le mot de passe est requis.")]
			[DataType(DataType.Password)]
			[StringLength(20, MinimumLength = 6, ErrorMessage = "Le mot de passe doit contenir entre 6 et 20 caract�res.")]
			public string Password { get; set; }
		}
	}
}