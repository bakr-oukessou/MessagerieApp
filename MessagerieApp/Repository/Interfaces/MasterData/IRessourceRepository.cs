using System.Collections.Generic;
using System.Threading.Tasks;
using MessagerieApp.Models.MasterData;

namespace MessagerieApp.Repository.Interfaces.MasterData
{
	public interface IRessourceRepository
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


		Task<Ressource> GetByIdAsync(int id);
		Task<IEnumerable<Ressource>> GetAllAsync();
		Task AddAsync(Ressource ressource);
		Task UpdateAsync(Ressource ressource);
		Task DeleteAsync(int id);
		Task<IEnumerable<Ressource>> GetByDepartmentIdAsync(int departmentId);
		Task<IEnumerable<Ressource>> GetAvailableResourcesAsync();
	}
}