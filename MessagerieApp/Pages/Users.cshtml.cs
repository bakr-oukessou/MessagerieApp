using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MessagerieApp.Models;
using MessagerieApp.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using MessagerieApp.Business.Interfaces;
using System.Data;

namespace MessagerieApp.Pages
{
    public class UsersModel : PageModel
    {
        private readonly IUserService _userService;

        public UsersModel(IUserService userService)
        {
            _userService = userService;
        }

        public List<User> UsersList { get; set; } = new();
        // Model for creating a new user
        [BindProperty]
        public User NewUser { get; set; }


        public async Task<IActionResult> OnGetAsync()
        {
            UsersList = (List<User>)await _userService.GetAllUsersAsync();
            return Page();
        }
        public async Task<IActionResult> OnPostCreateAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Hash the password (you should use a proper hashing library like BCrypt)
            NewUser.PasswordHash = System.Text.Encoding.UTF8.GetBytes(NewUser.Password);
            NewUser.PasswordSalt = new byte[0]; // Add proper salt generation

            // Add the new user to the database
            await _userService.AddUserAsync(NewUser);

            return RedirectToPage();
        }
        public async Task<IActionResult> OnPostEditAsync(int id, string email, string username, Departement department, UserRole role)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            // Update user properties
            user.Email = email;
            user.UserName = username;
            user.Department = department ;
            user.Role = role;

            // Save changes to the database
            await _userService.UpdateUserAsync(user);

            return RedirectToPage();

        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            try
            {
                await _userService.DeleteUserAsync(id);
            }
            catch
            {
                TempData["ErrorMessage"] = "Erreur lors de la suppression.";
                return RedirectToPage();
            }

            TempData["SuccessMessage"] = "supprimé avec succès.";
            return RedirectToPage();
        }
    }
}
