namespace MessagerieApp.Models
{
    public class User
    {
        private int id;
        private string username;
        private string password;
        private string nom;
        private string prenom;
        private string date_naissance;
        private string niveau;
        private string filiere;
        public User() { }

        public string Date_naissance { get => date_naissance; set => date_naissance = value; }
        public string Niveau { get => niveau; set => niveau = value; }
        public string Filiere { get => filiere; set => filiere = value; }
        public int Id { get => id; set => id = value; }
        public string Username { get => username; set => username = value; }
        public string Password { get => password; set => password = value; }
        public string Nom { get => nom; set => nom = value; }
        public string Prenom { get => prenom; set => prenom = value; }
    }
}
