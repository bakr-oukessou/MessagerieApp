using MessagerieApp.Repositories;
using MessagerieApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MessagerieApp.Services
{
    public class DepartementService : IDepartementService
    {
        private readonly IDepartementRepository _departementRepository;

        public DepartementService(IDepartementRepository departementRepository)
        {
            _departementRepository = departementRepository;
        }

        public async Task<IEnumerable<Departement>> GetAllDepartementsAsync()
        {
            return await _departementRepository.GetAllDepartementsAsync();
        }

        public async Task<Departement> GetDepartementByIdAsync(int id)
        {
            return await _departementRepository.GetDepartementByIdAsync(id);
        }

        public async Task AddDepartementAsync(Departement departement)
        {
            await _departementRepository.AddDepartementAsync(departement);
        }

        public async Task UpdateDepartementAsync(Departement departement)
        {
            await _departementRepository.UpdateDepartementAsync(departement);
        }

        public async Task DeleteDepartementAsync(int id)
        {
            await _departementRepository.DeleteDepartementAsync(id);
        }
    }
}