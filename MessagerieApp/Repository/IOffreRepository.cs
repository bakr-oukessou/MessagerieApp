using MessagerieApp.Models;

namespace MessagerieApp.Repository
{
    public interface IOffreRepository
    {
        Task<Offre> GetByIdAsync(int id, bool includeDetails = false);
        Task<IEnumerable<Offre>> GetAllAsync();
        Task<int> CreateOffreAsync(Offre offre);
        Task<bool> UpdateOffreAsync(Offre offre);
        Task<bool> DeleteOffreAsync(int id);
        Task<IEnumerable<AppelOffres>> GetAppelOffresByOffreIdAsync(int offreId);
    }
}
