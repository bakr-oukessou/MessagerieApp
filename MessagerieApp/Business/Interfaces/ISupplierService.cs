using System.Collections.Generic;
using System.Threading.Tasks;
using MessagerieApp.Models;

namespace MessagerieApp.Services
{
    public interface ISupplierService
    {
        Task<IEnumerable<Fournisseur>> GetAllSuppliersAsync();
        Task<Fournisseur> GetSupplierByIdAsync(int id);
        Task AddSupplierAsync(Fournisseur supplier);
        Task UpdateSupplierAsync(Fournisseur supplier);
        Task DeleteSupplierAsync(int id);
        Task BlacklistSupplierAsync(int id, string reason);
        Task RemoveFromBlacklistAsync(int id);
    }
}