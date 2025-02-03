using System.Collections.Generic;
using System.Threading.Tasks;
using MessagerieApp.Business.Interfaces.MasterData;
using MessagerieApp.Models.MasterData;
using MessagerieApp.Repository.Interfaces.MasterData;

namespace MessagerieApp.Services
{
	public class RessourceService : IRessourceService
    {
        private readonly IRessourceRepository _ressourceRepository;

        public RessourceService(IRessourceRepository ressourceRepository)
        {
            _ressourceRepository = ressourceRepository;
        }

        public async Task<IEnumerable<Ressource>> GetAllRessourcesAsync()
        {
            return await _ressourceRepository.GetAllRessourcesAsync();
        }

        public async Task<Ressource> GetRessourceByIdAsync(int id)
        {
            return await _ressourceRepository.GetRessourceByIdAsync(id);
        }

        public async Task AddRessourceAsync(Ressource ressource)
        {
            await _ressourceRepository.AddRessourceAsync(ressource);
        }

        public async Task UpdateRessourceAsync(Ressource ressource)
        {
            await _ressourceRepository.UpdateRessourceAsync(ressource);
        }

        public async Task DeleteRessourceAsync(int id)
        {
            await _ressourceRepository.DeleteRessourceAsync(id);
        }

        public async Task AssignRessourceToDepartmentAsync(int ressourceId, int departmentId)
        {
            await _ressourceRepository.AssignRessourceToDepartmentAsync(ressourceId, departmentId);
        }

        public async Task AssignRessourceToUserAsync(int ressourceId, int userId)
        {
            await _ressourceRepository.AssignRessourceToUserAsync(ressourceId, userId);
        }

        public async Task UpdateRessourceStatusAsync(int ressourceId, string status)
        {
            await _ressourceRepository.UpdateRessourceStatusAsync(ressourceId, status);
        }

        public Task<IEnumerable<Ressource>> GetRessourcesByDepartmentAsync(int departmentId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Ressource>> GetRessourcesByUserAsync(int userId)
        {
            throw new NotImplementedException();
        }

        public async Task AddRessourceFromDemandeAsync(RessourceItem item)
        {
            await _ressourceRepository.AddRessourceFromDemandeAsync(item);
        }
    }
}