using MessagerieApp.Models;

namespace MessagerieApp.Business
{
    public interface IUserService
    {
        Task<bool> RegisterUserAsync(RegisterViewModel model);

        Task<User> LoginUserAsync(LoginViewModel model);

        Task<User> GetUserByUsernameAsync(string username);

        Task<bool> UpdateUserProfileAsync(User user);

        Task<bool> ChangePasswordAsync(string username, string oldPassword, string newPassword);
    }
}
