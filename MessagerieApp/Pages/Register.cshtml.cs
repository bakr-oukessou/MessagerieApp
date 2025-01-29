using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MessagerieApp.Pages
{
    public class RegisterModel : PageModel
    {
        [BindProperty]
        public RegisterInputModel Register { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Call the AuthService to register the user
            // Example: await _authService.RegisterAsync(new User { ... }, Register.Password);

            return RedirectToPage("/Login");
        }

        public class RegisterInputModel
        {
            public string Name { get; set; }
            public string Email { get; set; }
            public string Password { get; set; }
            public string ConfirmPassword { get; set; }
        }
    }
}
