using MessagerieApp.Models;
using System.Data.SqlClient;

namespace MessagerieApp.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly string _connectionString;

        public UserRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<User> GetUser ByUsernameAsync(string username)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var command = new SqlCommand("SELECT * FROM Utilisateurs WHERE Username = @Username", connection);
                command.Parameters.AddWithValue("@Username", username);

                using (var reader = await command.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        return new User
                        {
                            Id = (int)reader["Id"],
                            Username = reader["Username"].ToString(),
                            Password = reader["Password"].ToString(),
                            Nom = reader["Nom"].ToString(),
                            Prenom = reader["Prenom"].ToString(),
                            Date_naissance = reader["DateNaissance"].ToString(),
                            Niveau = reader["Niveau"].ToString(),
                            Filiere = reader["Filiere"].ToString()
                        };
                    }
                }
            }
            return null;
        }

        public async Task AddUser Async(User user)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var command = new SqlCommand("INSERT INTO Utilisateurs (Username, Password, Nom, Prenom, DateNaissance, Niveau, Filiere) VALUES (@Username, @Password, @Nom, @Prenom, @DateNaissance, @Niveau, @Filiere)", connection);
                command.Parameters.AddWithValue("@Username", user.Username);
                command.Parameters.AddWithValue("@Password", user.Password); // Store hashed password
                command.Parameters.AddWithValue("@Nom", user.Nom);
                command.Parameters.AddWithValue("@Prenom", user.Prenom);
                command.Parameters.AddWithValue("@DateNaissance", user.Date_naissance);
                command.Parameters.AddWithValue("@Niveau", user.Niveau);
                command.Parameters.AddWithValue("@Filiere", user.Filiere);

                await command.ExecuteNonQueryAsync();
            }
        }
    }
}
