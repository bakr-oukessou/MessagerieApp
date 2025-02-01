using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MessagerieApp.Models;
using MessagerieApp.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using MessagerieApp.Business.Interfaces;

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

        public async Task<IActionResult> OnGetAsync()
        {
            UsersList = (List<User>)await _userService.GetAllUsersAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostEditAsync(int id)
        {
            return RedirectToPage("/EditUser", new { id });
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            await _userService.DeleteUserAsync(id);
            TempData["SuccessMessage"] = "Utilisateur supprimé avec succès.";
            return RedirectToPage();
        }
    }
}
