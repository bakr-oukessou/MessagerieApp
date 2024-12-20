using System.ComponentModel.DataAnnotations;

namespace WebApplicationA1.Models
{
    public class Account
    {
        string login;
        string password;

        public Account()
        {
            login = password = "";
        }
        [Required(ErrorMessage ="le login est obligatoire")]
        public string Login {  get { return login; }  set { login = value; } }
        [Required]
        [StringLength(20, MinimumLength = 3)]
        public string Password { get { return password; } set { password = value; } }
    }
}
