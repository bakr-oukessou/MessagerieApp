using MessagerieApp.Models;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace MessagerieApp.Repository
{
    public interface IUserRepository
    {
        Task<User> GetUser ByUsernameAsync(string username);
        Task AddUser Async(User user);
        Task UpdateUser Async(User user);
    }
}
