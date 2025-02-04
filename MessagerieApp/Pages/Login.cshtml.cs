using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MessagerieApp.Models;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using MessagerieApp.Repository.Interfaces;

namespace MessagerieApp.Pages
{
	public class LoginModel : PageModel
	{
		private readonly IUserRepository _userRepository;

		public LoginModel(IUserRepository userRepository)
		{
			_userRepository = userRepository;
		}

		[BindProperty]
		public LoginInputModel LoginInput { get; set; }
		public string ErrorMessage { get; set; }

		public void OnGet() { }

		public async Task<IActionResult> OnPostAsync()
		{
			if (!ModelState.IsValid)
			{
				return Page();
			}

			var user = await _userRepository.GetUserByEmailAsync(LoginInput.Email);

			if (user == null)
			{
				ErrorMessage = "Email ou mot de passe incorrect.";
				return Page();
			}

			// Vérification directe du mot de passe en clair
			if (user.Password != LoginInput.Password) // <- Modification ici
			{
				ErrorMessage = "Email ou mot de passe incorrect.";
				return Page();
			}

			// Création des claims
			var claims = new List<Claim>
			{
				new Claim(ClaimTypes.Name, user.UserName),
				new Claim(ClaimTypes.Email, user.Email),
				new Claim(ClaimTypes.Role, user.Role.ToString())
			};

			var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
			var authProperties = new AuthenticationProperties
			{
				IsPersistent = true
			};

			await HttpContext.SignInAsync(
				CookieAuthenticationDefaults.AuthenticationScheme,
				new ClaimsPrincipal(claimsIdentity),
				authProperties);

			return RedirectToPage("/Dashboard");
		}
	}

	public class LoginInputModel
	{
		[Required(ErrorMessage = "L'email est requis")]
		[EmailAddress(ErrorMessage = "Format d'email invalide")]
		public string Email { get; set; }

		[Required(ErrorMessage = "Le mot de passe est requis")]
		[DataType(DataType.Password)]
		public string Password { get; set; }
	}
}