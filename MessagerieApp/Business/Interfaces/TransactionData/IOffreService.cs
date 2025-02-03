using MessagerieApp.Models.TransactionData;

namespace MessagerieApp.Business.Interfaces.TransactionData
{
	public interface IOffreService
	{
		Task<Offre> GetOffreByIdAsync(int id, bool includeDetails = false);
		Task<IEnumerable<Offre>> GetAllOffresAsync();
		Task<int> CreateOffreAsync(Offre offre);
		Task<bool> UpdateOffreAsync(Offre offre);
		Task<bool> DeleteOffreAsync(int id);
		Task<IEnumerable<AppelOffres>> GetAppelOffresByOffreIdAsync(int offreId);
		Task<AppelOffres> SelectBestAppelOffreAsync(int offreId);
	}
}
