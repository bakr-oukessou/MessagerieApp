using MessagerieApp.Models;

namespace MessagerieApp.Repository.Interfaces
{
    public interface IAppelOffresRepository
    {
        IEnumerable<AppelOffres> GetAll();
        AppelOffres GetById(int id);
        void Add(AppelOffres appelOffres);
        void Update(AppelOffres appelOffres);
        void Delete(int id);
    }
}

