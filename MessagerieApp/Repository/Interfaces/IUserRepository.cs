using MessagerieApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MessagerieApp.Repository.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<User> GetUserByIdAsync(int id);
        Task<User> GetUserByEmailAsync(string email);
        Task AddUserAsync(User user);
        Task UpdateUserAsync(User user);
        Task DeleteUserAsync(int id);
        Task<IEnumerable<User>> GetUsersByDepartmentAsync(int departmentId);
        //Task<User> GetUserRoleAsync(string username);
        Task<string?> GetUserRoleAsync(string? role);


    }
}