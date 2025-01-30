using System.Collections.Generic;
using System.Threading.Tasks;
using MessagerieApp.Models;

namespace MessagerieApp.Services
{
    public interface IDemandeRessourceService
    {
        Task<IEnumerable<DemandeRessource>> GetAllDemandesAsync();
        Task<DemandeRessource> GetDemandeByIdAsync(int id);
        Task AddDemandeAsync(DemandeRessource demande);
        Task UpdateDemandeAsync(DemandeRessource demande);
        Task DeleteDemandeAsync(int id);
        Task UpdateDemandeStatusAsync(int demandeId, string status);
        Task<IEnumerable<DemandeRessource>> GetDemandesByDepartmentAsync(int departmentId);
    }
}