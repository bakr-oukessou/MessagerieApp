using MessagerieApp.Models.TransversalData;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MessagerieApp.Business.Interfaces.TransversalData
{
	public interface IUserService
	{
		Task<IEnumerable<User>> GetAllUsersAsync();
		Task<User> GetUserByIdAsync(int id);
		Task<User> GetUserByEmailAsync(string email);
		Task AddUserAsync(User user);
		Task UpdateUserAsync(User user);
		Task DeleteUserAsync(int id);

		Task<User> ObtenirUtilisateurCourant();
		Task<bool> EstChefDepartement(int userId);
		Task<bool> EstResponsableRessources(int userId);
		Task<bool> EstEnseignant(int userId);
	}
}