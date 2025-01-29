using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MessagerieApp.Pages
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public LoginInputModel Login { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Call the AuthService to validate the user
            // Example: var token = await _authService.LoginAsync(Login.Email, Login.Password);

            // Redirect to dashboard upon success
            return RedirectToPage("/Dashboard");
        }

        public class LoginInputModel
        {
            public string Email { get; set; }
            public string Password { get; set; }
        }
    }
}
