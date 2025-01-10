using MessagerieApp.Data;
using MessagerieApp.Models;
using System.Data.SqlClient;

namespace MessagerieApp.Repository
{
    public class NotificationRepository
    {
        private readonly DatabaseConnection _dbConnection;

        public NotificationRepository(DatabaseConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        // Créer une nouvelle notification
        public int CreerNotification(Notification notification)
        {
            using (SqlConnection connection = _dbConnection.GetConnection())
            {
                string query = @"
                INSERT INTO Notifications 
                (EmetteurId, DestinataireId, Titre, Corps, Type, Statut, DateCreation) 
                VALUES 
                (@EmetteurId, @DestinataireId, @Titre, @Corps, @Type, @Statut, @DateCreation);
                SELECT SCOPE_IDENTITY();";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@EmetteurId", notification.EmetteurId);
                    command.Parameters.AddWithValue("@DestinataireId", notification.DestinataireId);
                    command.Parameters.AddWithValue("@Titre", notification.Titre);
                    command.Parameters.AddWithValue("@Corps", notification.Corps);
                    command.Parameters.AddWithValue("@Type", (int)notification.Type);
                    command.Parameters.AddWithValue("@Statut", (int)notification.Statut);
                    command.Parameters.AddWithValue("@DateCreation", DateTime.Now);

                    connection.Open();
                    return Convert.ToInt32(command.ExecuteScalar());
                }
            }
        }

        // Obtenir les notifications pour un utilisateur
        public List<Notification> ObtenirNotificationsUtilisateur(int utilisateurId)
        {
            List<Notification> notifications = new List<Notification>();

            using (SqlConnection connection = _dbConnection.GetConnection())
            {
                string query = @"
                SELECT * FROM Notifications 
                WHERE DestinataireId = @UtilisateurId 
                ORDER BY DateCreation DESC";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UtilisateurId", utilisateurId);

                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            notifications.Add(new Notification
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                EmetteurId = Convert.ToInt32(reader["EmetteurId"]),
                                DestinataireId = Convert.ToInt32(reader["DestinataireId"]),
                                Titre = reader["Titre"].ToString(),
                                Corps = reader["Corps"].ToString(),
                                Type = (NotificationType)Convert.ToInt32(reader["Type"]),
                                Statut = (StatutNotification)Convert.ToInt32(reader["Statut"]),
                                DateCreation = Convert.ToDateTime(reader["DateCreation"]),
                                DateLecture = reader["DateLecture"] != DBNull.Value
                                    ? Convert.ToDateTime(reader["DateLecture"])
                                    : (DateTime?)null
                            });
                        }
                    }
                }
            }

            return notifications;
        }

        // Marquer une notification comme lue
        public bool MarquerNotificationCommeLue(int notificationId)
        {
            using (SqlConnection connection = _dbConnection.GetConnection())
            {
                string query = @"
                UPDATE Notifications 
                SET Statut = @Statut, 
                    DateLecture = @DateLecture 
                WHERE Id = @Id";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", notificationId);
                    command.Parameters.AddWithValue("@Statut", (int)StatutNotification.Lue);
                    command.Parameters.AddWithValue("@DateLecture", DateTime.Now);

                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }
    }
}
