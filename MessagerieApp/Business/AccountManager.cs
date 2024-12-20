using WebApplicationA1.Models;

namespace WebApplicationA1.Business
{
    public class AccountManager : IAccountManager
    {
        static List<Account> liste = new List<Account>();
        public AccountManager() { }
        public void Add(Account account)
        {
            liste.Add(account);
        }

        public void Del(string login)
        {
            Account x = Find(login);
            if (x != null)
            {
                liste.Remove(x);
            }
        }

        public bool Exist(string login)
        {
            foreach (Account account in liste)
            {
                if (account.Login == login)
                {
                    return true;
                }
            }
                return false;
        }

        public Account Find(string login)
        {
            foreach (Account account in liste)
            {
                if (account.Login == login)
                {
                    return account;
                }
            }
            return null;
        }
        public List<Account> GetAccounts()
        {
            return liste;
        }

        public void Update(string login, Account account)
        {
            throw new NotImplementedException();
        }
    }
}
