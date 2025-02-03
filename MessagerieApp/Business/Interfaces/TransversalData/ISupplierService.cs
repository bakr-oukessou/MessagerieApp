using System.Collections.Generic;
using System.Threading.Tasks;
using MessagerieApp.Models;

namespace MessagerieApp.Business.Interfaces.TransversalData
{
	public interface ISupplierService
	{
		Task<IEnumerable<Supplier>> GetAllSuppliersAsync();
		Task<Supplier> GetSupplierByIdAsync(int id);
		Task AddSupplierAsync(Supplier supplier);
		Task UpdateSupplierAsync(Supplier supplier);
		Task DeleteSupplierAsync(int id);
		Task BlacklistSupplierAsync(int id, string reason);
		Task RemoveFromBlacklistAsync(int id);
	}
}