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

			// Vérification du mot de passe
			if (!VerifyPasswordHash(LoginInput.Password, user.PasswordHash, user.PasswordSalt))
			{
				ErrorMessage = "Email ou mot de passe incorrect.";
				return Page();
			}

			// Création des claims (données de l'utilisateur)
			var claims = new List<Claim>
			{
				new Claim(ClaimTypes.Name, user.UserName),
				new Claim(ClaimTypes.Email, user.Email),
				new Claim(ClaimTypes.Role, user.Role.ToString())
			};

			var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
			var authProperties = new AuthenticationProperties
			{
				IsPersistent = true // Connexion persistante
			};

			// Authentifier l'utilisateur avec les cookies
			await HttpContext.SignInAsync(
				CookieAuthenticationDefaults.AuthenticationScheme,
				new ClaimsPrincipal(claimsIdentity),
				authProperties);

			return RedirectToPage("/Dashboard");
		}

		private bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
		{
			using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
			{
				var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
				return computedHash.SequenceEqual(storedHash);
			}
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