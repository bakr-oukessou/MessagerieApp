using WebApplicationA1.Models;

namespace MessagerieApp.Business.Interfaces
{
    public interface IAccountManager
    {
        void Add(Account account);
        bool Exist(string login);
        Account Find(string login);
        void Del(string login);
        void Update(string login, Account account);
        List<Account> GetAccounts();
    }
}