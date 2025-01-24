using MessagerieApp.Models;
using MessagerieApp.Repository;

namespace MessagerieApp.Business
{
    public class OffreService : IOffreService
    {
        private readonly IOffreRepository _offreRepository;
        private readonly INotificationService _notificationService;

        public OffreService(IOffreRepository offreRepository, INotificationService notificationService)
        {
            _offreRepository = offreRepository;
            _notificationService = notificationService;
        }

        public Task<Offre> GetOffreByIdAsync(int id, bool includeDetails = false)
        {
            return _offreRepository.GetByIdAsync(id, includeDetails);
        }

        public Task<IEnumerable<Offre>> GetAllOffresAsync()
        {
            return _offreRepository.GetAllAsync();
        }

        public Task<int> CreateOffreAsync(Offre offre)
        {
            // Validate offre before creation
            ValidateOffre(offre);
            return _offreRepository.CreateOffreAsync(offre);
        }

        public Task<bool> UpdateOffreAsync(Offre offre)
        {
            // Validate offre before update
            ValidateOffre(offre);
            return _offreRepository.UpdateOffreAsync(offre);
        }

        public Task<bool> DeleteOffreAsync(int id)
        {
            return _offreRepository.DeleteOffreAsync(id);
        }

        public Task<IEnumerable<AppelOffre>> GetAppelOffresByOffreIdAsync(int offreId)
        {
            return _offreRepository.GetAppelOffresByOffreIdAsync(offreId);
        }


        public Task<AppelOffres> SelectBestAppelOffreAsync(int offreId)
        {
            var appelOffres = await GetAppelOffresByOffreIdAsync(offreId);

            // Business logic to select the best AppelOffre
            var bestAppelOffre = appelOffres
                .OrderBy(ao => ao.TotalPrice)
                .ThenBy(ao => ao.ProposedDeliveryDate)
                .FirstOrDefault();

            if (bestAppelOffre == null)
            {
                throw new Exception("No AppelOffres found for the given Offre");
            }

            // Update status of selected and rejected AppelOffres
            foreach (var appelOffre in appelOffres)
            {
                appelOffre.Status = appelOffre.Id == bestAppelOffre.Id
                    ? "Accepted"
                    : "Rejected";

                // Notify suppliers about their bid status
                await _notificationService.CreateNotificationAsync(new Notification
                {
                    Message = $"Your bid for Offre {offreId} has been {appelOffre.Status}",
                    RecipientId = appelOffre.SupplierId,
                    CreatedAt = DateTime.UtcNow,
                    IsRead = false,

                });

            }
            return bestAppelOffre;
        
        }
    }
}
