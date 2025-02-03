using MessagerieApp.Models.TransactionData;

namespace MessagerieApp.Repository.Interfaces.TransactionData
{
	public interface IOffreRepository
	{
		Task<Offre> GetByIdAsync(int id, bool includeDetails = false);
		Task<IEnumerable<Offre>> GetAllAsync();
		Task<int> CreateOffreAsync(Offre offre);
		Task<bool> UpdateOffreAsync(Offre offre);
		Task<bool> DeleteOffreAsync(int id);
		Task<IEnumerable<AppelOffre>> GetAppelOffresByOffreIdAsync(int offreId);

		Task<Offre> GetByIdAsync(int id);
		Task AddAsync(Offre offre);
		Task UpdateAsync(Offre offre);
		Task DeleteAsync(int id);
		Task<IEnumerable<Offre>> GetOpenOffresAsync();
	}
}
