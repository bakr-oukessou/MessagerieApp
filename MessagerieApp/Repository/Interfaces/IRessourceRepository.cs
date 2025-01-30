using System.Collections.Generic;
using System.Threading.Tasks;
using MessagerieApp.Models;

namespace MessagerieApp.Repository.Interfaces
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
    }
}