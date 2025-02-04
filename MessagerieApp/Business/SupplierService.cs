using MessagerieApp.Repositories;
using MessagerieApp.Models;
using System.Threading.Tasks;

namespace MessagerieApp.Services
{
    public class SupplierService : ISupplierService
    {
        private readonly ISupplierRepository _supplierRepository;

        public SupplierService(ISupplierRepository supplierRepository)
        {
            _supplierRepository = supplierRepository;
        }

        public async Task<IEnumerable<Fournisseur>> GetAllSuppliersAsync()
        {
            return await _supplierRepository.GetAllSuppliersAsync();
        }

        public async Task<Fournisseur> GetSupplierByIdAsync(int id)
        {
            return await _supplierRepository.GetSupplierByIdAsync(id);
        }

        public async Task AddSupplierAsync(Fournisseur supplier)
        {
            await _supplierRepository.AddSupplierAsync(supplier);
        }

        public async Task UpdateSupplierAsync(Fournisseur supplier)
        {
            await _supplierRepository.UpdateSupplierAsync(supplier);
        }

        public async Task DeleteSupplierAsync(int id)
        {
            await _supplierRepository.DeleteSupplierAsync(id);
        }

        public async Task BlacklistSupplierAsync(int id, string reason)
        {
            await _supplierRepository.BlacklistSupplierAsync(id, reason);
        }

        public async Task RemoveFromBlacklistAsync(int id)
        {
            await _supplierRepository.RemoveFromBlacklistAsync(id);
        }
    }
}