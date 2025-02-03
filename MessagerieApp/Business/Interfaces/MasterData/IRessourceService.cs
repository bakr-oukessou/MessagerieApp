using System.Collections.Generic;
using System.Threading.Tasks;
using MessagerieApp.Models.MasterData;

namespace MessagerieApp.Business.Interfaces.MasterData
{
	public interface IRessourceService
	{
		Task<IEnumerable<Ressource>> GetAllRessourcesAsync();
		Task<Ressource> GetRessourceByIdAsync(int id);
		Task AddRessourceAsync(Ressource ressource);
		Task UpdateRessourceAsync(Ressource ressource);
		Task DeleteRessourceAsync(int id);
		Task AssignRessourceToDepartmentAsync(int ressourceId, int departmentId);
		Task AssignRessourceToUserAsync(int ressourceId, int userId);
		Task UpdateRessourceStatusAsync(int ressourceId, string status);
		Task<IEnumerable<Ressource>> GetRessourcesByDepartmentAsync(int departmentId);
		Task<IEnumerable<Ressource>> GetRessourcesByUserAsync(int userId);

		Task AddRessourceFromDemandeAsync(RessourceItem item);


	}
}