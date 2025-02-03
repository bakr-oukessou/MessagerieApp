using MessagerieApp.Models;
using MessagerieApp.Repository.Interfaces.TransactionData;

namespace MessagerieApp.Business
{
	public class AppelOffresService
    {
        private readonly IAppelOffresRepository _repository;

        public AppelOffresService(IAppelOffresRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<AppelOffres> GetAllAppelOffres()
        {
            return _repository.GetAll();
        }

        public AppelOffres GetAppelOffresById(int id)
        {
            return _repository.GetById(id);
        }

        public void CreateAppelOffres(AppelOffres appelOffres)
        {
            _repository.Add(appelOffres);
        }

        public void UpdateAppelOffres(AppelOffres appelOffres)
        {
            _repository.Update(appelOffres);
        }

        public void DeleteAppelOffres(int id)
        {
            _repository.Delete(id);
        }
    }
}
