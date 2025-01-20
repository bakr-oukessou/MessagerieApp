using MessagerieApp.Models;
using MessagerieApp.Repository;
using System.Collections.Generic;
using System.Text;

namespace MessagerieApp.Business
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUser Repository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task RegisterUser Async(RegisterViewModel model)
        {
            var user = new Utilisateur
            {
                Username = model.Username,
                Password = HashPassword(model.Password),
                Nom = model.Nom,
                Prenom = model.Prenom,
                DateNaissance = model.DateNaissance,
                Niveau = model.Niveau,
                Filiere = model.Filiere
            };

            await _userRepository.AddUser Async(user);
        }

        public async Task<Utilisateur> LoginUser Async(LoginViewModel model)
        {
            var user = await _userRepository.GetUser ByUsernameAsync(model.Username);
            if (user != null && VerifyPassword(model.Password, user.Password))
            {
                return user;
            }
            return null;
        }

        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(bytes);
            }
        }

        private bool VerifyPassword(string password, string storedHash)
        {
            var hash = HashPassword(password);
            return hash == storedHash;
        }
    }
}
