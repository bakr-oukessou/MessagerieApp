using MessagerieApp.Models;
using MessagerieApp.Repository.Interfaces.TransactionData;

namespace MessagerieApp.Repository
{
	public class AppelOffresRepository : IAppelOffresRepository
    {
        private readonly List<AppelOffres> _appelOffresList = new List<AppelOffres>();

        public IEnumerable<AppelOffres> GetAll()
        {
            return _appelOffresList;
        }

        public AppelOffres GetById(int id)
        {
            return _appelOffresList.FirstOrDefault(a => a.Id == id);
        }

        public void Add(AppelOffres appelOffres)
        {
            _appelOffresList.Add(appelOffres);
        }

        public void Update(AppelOffres appelOffres)
        {
            var existing = GetById(appelOffres.Id);
            if (existing != null)
            {
                existing.OffreId = appelOffres.OffreId;
                existing.FournisseurId = appelOffres.FournisseurId;
                existing.SubmissionDate = appelOffres.SubmissionDate;
                existing.ProposedDeliveryDate = appelOffres.ProposedDeliveryDate;
                existing.WarrantyMonths = appelOffres.WarrantyMonths;
                existing.TotalPrice = appelOffres.TotalPrice;
                existing.Status = appelOffres.Status;
                existing.Items = appelOffres.Items;
            }
        }

        public void Delete(int id)
        {
            var appelOffres = GetById(id);
            if (appelOffres != null)
            {
                _appelOffresList.Remove(appelOffres);
            }
        }
    }
}
