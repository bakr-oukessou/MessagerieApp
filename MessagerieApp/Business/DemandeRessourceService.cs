using MessagerieApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using MessagerieApp.Repository.Interfaces;

namespace MessagerieApp.Services
{
    public class DemandeRessourceService : IDemandeRessourceService
    {
        private readonly IDemandeRessourceRepository _demandeRessourceRepository;

        public DemandeRessourceService(IDemandeRessourceRepository demandeRessourceRepository)
        {
            _demandeRessourceRepository = demandeRessourceRepository;
        }

        public async Task<IEnumerable<DemandeRessource>> GetAllDemandesAsync()
        {
            return await _demandeRessourceRepository.GetAllDemandesAsync();
        }

        public async Task<DemandeRessource> GetDemandeByIdAsync(int id)
        {
            return await _demandeRessourceRepository.GetDemandeByIdAsync(id);
        }

        public async Task AddDemandeAsync(DemandeRessource demande)
        {
            await _demandeRessourceRepository.AddDemandeAsync(demande);
        }

        public async Task UpdateDemandeAsync(DemandeRessource demande)
        {
            await _demandeRessourceRepository.UpdateDemandeAsync(demande);
        }

        public async Task DeleteDemandeAsync(int id)
        {
            await _demandeRessourceRepository.DeleteDemandeAsync(id);
        }

        public async Task UpdateDemandeStatusAsync(int demandeId, string status)
        {
            await _demandeRessourceRepository.UpdateDemandeStatusAsync(demandeId, status);
        }

        public async Task<IEnumerable<DemandeRessource>> GetDemandesByDepartmentAsync(int departmentId)
        {
            return await _demandeRessourceRepository.GetDemandesByDepartmentAsync(departmentId);
        }
    }
}