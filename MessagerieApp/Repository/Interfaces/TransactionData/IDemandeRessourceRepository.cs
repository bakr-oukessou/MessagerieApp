using System.Collections.Generic;
using System.Threading.Tasks;
using MessagerieApp.Models.MasterData;
using MessagerieApp.Models.TransactionData;

namespace MessagerieApp.Repository.Interfaces.TransactionData
{
	public interface IDemandeRessourceRepository
	{
		Task<IEnumerable<DemandeRessource>> GetAllDemandesAsync();
		Task<DemandeRessource> GetDemandeByIdAsync(int id);
		Task AddDemandeAsync(DemandeRessource demande);
		Task UpdateDemandeAsync(DemandeRessource demande);
		Task DeleteDemandeAsync(int id);
		Task UpdateDemandeStatusAsync(int demandeId, string status);
		Task<IEnumerable<DemandeRessource>> GetDemandesByDepartmentAsync(int departmentId);
		Task<IEnumerable<RessourceItem>> GetDemandeRessourceItemsAsync(int demandeId);
		Task AddDemandeRessourceItemAsync(RessourceItem item);

		Task<DemandeRessource> GetByIdAsync(int id);
		Task<IEnumerable<DemandeRessource>> GetAllAsync();
		Task AddAsync(DemandeRessource demande);
		Task UpdateAsync(DemandeRessource demande);
		Task DeleteAsync(int id);
		Task<IEnumerable<DemandeRessource>> GetByDepartmentIdAsync(int departmentId);

		Task<List<DemandeRessource>> GetByReceiverIdAsync(int receiverId);
		Task<List<DemandeRessource>> GetBySenderIdAsync(int senderId);
	}
}