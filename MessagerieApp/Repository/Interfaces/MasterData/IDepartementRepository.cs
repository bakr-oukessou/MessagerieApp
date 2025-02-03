using System.Collections.Generic;
using System.Threading.Tasks;
using MessagerieApp.Models.MasterData;

namespace MessagerieApp.Repository.Interfaces.MasterData
{
	public interface IDepartementRepository
	{
		Task<IEnumerable<Departement>> GetAllDepartementsAsync();
		Task<Departement> GetDepartementByIdAsync(int id);
		Task AddDepartementAsync(Departement departement);
		Task UpdateDepartementAsync(Departement departement);
		Task DeleteDepartementAsync(int id);
	}
}