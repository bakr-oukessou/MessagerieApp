using System.Collections.Generic;
using System.Threading.Tasks;
using MessagerieApp.Models;

namespace MessagerieApp.Repository.Interfaces.TransversalData
{
	public interface ISupplierRepository
	{
		Task<IEnumerable<Supplier>> GetAllSuppliersAsync();
		Task<Supplier> GetSupplierByIdAsync(int id);
		Task AddSupplierAsync(Supplier supplier);
		Task UpdateSupplierAsync(Supplier supplier);
		Task DeleteSupplierAsync(int id);
		Task BlacklistSupplierAsync(int id, string reason);
		Task RemoveFromBlacklistAsync(int id);

		Task<Supplier> GetByIdAsync(int id);
		Task<IEnumerable<Supplier>> GetAllAsync();
		Task AddAsync(Supplier supplier);
		Task UpdateAsync(Supplier supplier);
		Task DeleteAsync(int id);
		Task<IEnumerable<Supplier>> GetBlacklistedSuppliersAsync();
	}
}