using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplicationA1.Business;
using WebApplicationA1.Models;

namespace WebApplicationA1.Pages
{
    public class RegisterModel : PageModel
    {
        Account account;
        string msg;
        public Account Compte
        {
            get { return account; }
            set { account = value; }
        }

        public string Msg { get => msg; set => msg = value; }

        public void OnGet()
        {
        
        }
        public IActionResult OnPost(Account Compte)
        {
            if (ModelState.IsValid)
            {
                AccountManager manager = new AccountManager();
                
                if (manager.Exist(Compte.Login)==false)
                {
                    manager.Add(Compte);
                    Msg = "account added with success";
                }

                else
                {
                    Msg = "Account already exist!";
                }
            }
            return Page();
        }
    }
}
