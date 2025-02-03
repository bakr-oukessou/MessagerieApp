using MessagerieApp.Models;
using MessagerieApp.Models.TransactionData;

namespace MessagerieApp.Repository.Interfaces.TransactionData
{
	public interface IAppelOffresRepository
	{
		IEnumerable<AppelOffre> GetAll();
		AppelOffre GetById(int id);
		void Add(AppelOffre appelOffres);
		void Update(AppelOffre appelOffres);
		void Delete(int id);

		Task<AppelOffre> GetByIdAsync(int id);
		Task<IEnumerable<AppelOffre>> GetAllAsync();
		Task AddAsync(AppelOffre appelOffres);
		Task UpdateAsync(AppelOffre appelOffres);
		Task DeleteAsync(int id);
		Task<IEnumerable<AppelOffre>> GetByOffreIdAsync(int offreId);
		Task<IEnumerable<AppelOffre>> GetBySupplierIdAsync(int supplierId);
	}
}

