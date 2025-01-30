using System.Collections.Generic;
using System.Threading.Tasks;
using MessagerieApp.Models;

namespace MessagerieApp.Services
{
    public interface IDepartementService
    {
        Task<IEnumerable<Departement>> GetAllDepartementsAsync();
        Task<Departement> GetDepartementByIdAsync(int id);
        Task AddDepartementAsync(Departement departement);
        Task UpdateDepartementAsync(Departement departement);
        Task DeleteDepartementAsync(int id);
    }
}