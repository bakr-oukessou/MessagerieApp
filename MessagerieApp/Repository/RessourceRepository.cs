using MessagerieApp.Data;
using MessagerieApp.Models;
using System.Data.SqlClient;

namespace MessagerieApp.Repository
{
    public class RessourceRepository
    {
        private readonly DatabaseConnection _dbConnection;

        public RessourceRepository(DatabaseConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        // Méthode pour ajouter une ressource
        public int AjouterRessource(Ressource ressource)
        {
            using (SqlConnection connection = _dbConnection.GetConnection())
            {
                string query = @"
                INSERT INTO Ressources 
                (Nom, Type, Marque, DateAcquisition, DepartementId) 
                VALUES 
                (@Nom, @Type, @Marque, @DateAcquisition, @DepartementId);
                SELECT SCOPE_IDENTITY();";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Nom", ressource.Nom);
                    command.Parameters.AddWithValue("@Type", ressource.Type);
                    command.Parameters.AddWithValue("@Marque", ressource.Marque);
                    command.Parameters.AddWithValue("@DateAcquisition", ressource.DateAcquisition);
                    command.Parameters.AddWithValue("@DepartementId", ressource.DepartementId);

                    connection.Open();
                    return Convert.ToInt32(command.ExecuteScalar());
                }
            }
        }

        // Méthode pour obtenir toutes les ressources
        public List<Ressource> ObtenirToutesLesRessources()
        {
            List<Ressource> ressources = new List<Ressource>();

            using (SqlConnection connection = _dbConnection.GetConnection())
            {
                string query = "SELECT * FROM Ressources";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ressources.Add(new Ressource
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                Nom = reader["Nom"].ToString(),
                                Type = reader["Type"].ToString(),
                                Marque = reader["Marque"].ToString(),
                                DateAcquisition = Convert.ToDateTime(reader["DateAcquisition"]),
                                DepartementId = Convert.ToInt32(reader["DepartementId"])
                            });
                        }
                    }
                }
            }

            return ressources;
        }

        // Méthode pour mettre à jour une ressource
        public bool ModifierRessource(Ressource ressource)
        {
            using (SqlConnection connection = _dbConnection.GetConnection())
            {
                string query = @"
                UPDATE Ressources 
                SET Nom = @Nom, 
                    Type = @Type, 
                    Marque = @Marque, 
                    DateAcquisition = @DateAcquisition, 
                    DepartementId = @DepartementId 
                WHERE Id = @Id";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", ressource.Id);
                    command.Parameters.AddWithValue("@Nom", ressource.Nom);
                    command.Parameters.AddWithValue("@Type", ressource.Type);
                    command.Parameters.AddWithValue("@Marque", ressource.Marque);
                    command.Parameters.AddWithValue("@DateAcquisition", ressource.DateAcquisition);
                    command.Parameters.AddWithValue("@DepartementId", ressource.DepartementId);

                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }

        // Méthode pour supprimer une ressource
        public bool SupprimerRessource(int id)
        {
            using (SqlConnection connection = _dbConnection.GetConnection())
            {
                string query = "DELETE FROM Ressources WHERE Id = @Id";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }

        // Méthode de recherche avec filtres
        public List<Ressource> RechercherRessources(string critere)
        {
            List<Ressource> ressources = new List<Ressource>();

            using (SqlConnection connection = _dbConnection.GetConnection())
            {
                string query = @"
                SELECT * FROM Ressources 
                WHERE Nom LIKE @Critere OR 
                      Type LIKE @Critere OR 
                      Marque LIKE @Critere";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Critere", $"%{critere}%");

                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ressources.Add(new Ressource
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                Nom = reader["Nom"].ToString(),
                                Type = reader["Type"].ToString(),
                                Marque = reader["Marque"].ToString(),
                                DateAcquisition = Convert.ToDateTime(reader["DateAcquisition"]),
                                DepartementId = Convert.ToInt32(reader["DepartementId"])
                            });
                        }
                    }
                }
            }

            return ressources;
        }
    }
}
