using MessagerieApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MessagerieApp.Pages
{
    public class UsersModel : PageModel
    {
        public List<User> UsersList { get; set; } = new List<User>();

        public void OnGet()
        {
            // Fetch users from the database or service
            // Example:
            // UsersList = _userService.GetAllUsers();
        }

        public void OnPost(string username, string password, string role)
        {
            // Handle creation of a new user
            // Example:
            // var newUser = new User { Username = username, Password = password, Role = role };
            // _userService.AddUser(newUser);
        }
    }
}
