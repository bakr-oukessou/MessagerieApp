using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Collections.Generic;
using MessagerieApp.Models.TransactionData;
using MessagerieApp.Repository.Interfaces.TransactionData;

namespace MessagerieApp.Repositories
{
	public class NotificationRepository : INotificationRepository
    {
        private readonly string _connectionString;

        public NotificationRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<IEnumerable<Notification>> GetAllNotificationsAsync()
        {
            var notifications = new List<Notification>();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var command = new SqlCommand("SELECT * FROM Notifications", connection);
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        notifications.Add(new Notification
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            DestinataireId = reader.GetInt32(reader.GetOrdinal("DestinataireId")), // Corrigé
                            Titre = reader.GetString(reader.GetOrdinal("Titre")),
                            Corps = reader.GetString(reader.GetOrdinal("Corps")),
                            Type = (NotificationType)reader.GetInt32(reader.GetOrdinal("Type")),
                            Statut = (StatutNotification)reader.GetInt32(reader.GetOrdinal("Statut")),
                            DateCreation = reader.GetDateTime(reader.GetOrdinal("DateCreation")),
                            DateLecture = reader.IsDBNull(reader.GetOrdinal("DateLecture")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("DateLecture")),
                        });
                    }
                }
            }
            return notifications;
        }

        public async Task<Notification> GetNotificationByIdAsync(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var command = new SqlCommand("SELECT * FROM Notifications WHERE Id = @Id", connection);
                command.Parameters.AddWithValue("@Id", id);

                using (var reader = await command.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        return new Notification
                        {
                            EmetteurId = reader.GetInt32(reader.GetOrdinal("Id")),
                            DestinataireId = reader.GetInt32(reader.GetOrdinal("DestinataireId")),
                            Titre = reader.GetString(reader.GetOrdinal("Titre")),
                            Corps = reader.GetString(reader.GetOrdinal("Corps")),
                            Type = (NotificationType)reader.GetInt32(reader.GetOrdinal("Type")),
                            Statut = (StatutNotification)reader.GetInt32(reader.GetOrdinal("Statut")),
                            DateCreation = reader.GetDateTime(reader.GetOrdinal("DateCreation")),
                            DateLecture = reader.IsDBNull(reader.GetOrdinal("DateLecture")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("DateLecture"))
                        };
                    }
                }
            }

            return null;
        }

        public async Task AddNotificationAsync(Notification notification)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var command = new SqlCommand(
                    "INSERT INTO Notifications (Id, DestinataireId, Titre, Corps, Type, Statut, DateCreation) " +
                    "VALUES (@EmetteurId, @DestinataireId, @Titre, @Corps, @Type, @Statut, @DateCreation); SELECT SCOPE_IDENTITY();",
                    connection);

                command.Parameters.AddWithValue("@EmetteurId", notification.EmetteurId);
                command.Parameters.AddWithValue("@DestinataireId", notification.DestinataireId);
                command.Parameters.AddWithValue("@Titre", notification.Titre);
                command.Parameters.AddWithValue("@Corps", notification.Corps);
                command.Parameters.AddWithValue("@Type", (int)notification.Type);
                command.Parameters.AddWithValue("@Statut", (int)notification.Statut);
                command.Parameters.AddWithValue("@DateCreation", notification.DateCreation);

                notification.Id = Convert.ToInt32(await command.ExecuteScalarAsync());
            }
        }

        public async Task UpdateNotificationAsync(Notification notification)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var command = new SqlCommand(
                    "UPDATE Notifications SET Id = @EmetteurId, DestinataireId = @DestinataireId, Titre = @Titre, " +
                    "Corps = @Corps, Type = @Type, Statut = @Statut, DateCreation = @DateCreation, DateLecture = @DateLecture " +
                    "WHERE Id = @Id",
                    connection);

                command.Parameters.AddWithValue("@EmetteurId", notification.Id);
                command.Parameters.AddWithValue("@DestinataireId", notification.DestinataireId);
                command.Parameters.AddWithValue("@Titre", notification.Titre);
                command.Parameters.AddWithValue("@Corps", notification.Corps);
                command.Parameters.AddWithValue("@Type", (int)notification.Type);
                command.Parameters.AddWithValue("@Statut", (int)notification.Statut);
                command.Parameters.AddWithValue("@DateCreation", notification.DateCreation);
                command.Parameters.AddWithValue("@DateLecture", notification.DateLecture ?? (object)DBNull.Value);

                await command.ExecuteNonQueryAsync();
            }
        }

        public async Task DeleteNotificationAsync(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var command = new SqlCommand("DELETE FROM Notifications WHERE Id = @Id", connection);
                command.Parameters.AddWithValue("@Id", id);

                await command.ExecuteNonQueryAsync();
            }
        }

        public async Task MarkNotificationAsReadAsync(int notificationId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var command = new SqlCommand(
                    "UPDATE Notifications SET Statut = @Statut, DateLecture = @DateLecture WHERE Id = @Id",
                    connection);

                command.Parameters.AddWithValue("@Id", notificationId);
                command.Parameters.AddWithValue("@Statut", (int)StatutNotification.Lue);
                command.Parameters.AddWithValue("@DateLecture", DateTime.Now);

                await command.ExecuteNonQueryAsync();
            }
        }

        public async Task MarkNotificationAsArchivedAsync(int notificationId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var command = new SqlCommand(
                    "UPDATE Notifications SET Statut = @Statut WHERE Id = @Id",
                    connection);

                command.Parameters.AddWithValue("@Id", notificationId);
                command.Parameters.AddWithValue("@Statut", (int)StatutNotification.Archivee);

                await command.ExecuteNonQueryAsync();
            }
        }

        public async Task<IEnumerable<Notification>> GetNotificationsByUserAsync(int userId)
        {
            var notifications = new List<Notification>();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var command = new SqlCommand("SELECT * FROM Notifications WHERE DestinataireId = @DestinataireId", connection);
                command.Parameters.AddWithValue("@DestinataireId", userId);

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        notifications.Add(new Notification
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            EmetteurId = reader.GetInt32(reader.GetOrdinal("EmetteurId")),
                            DestinataireId = reader.GetInt32(reader.GetOrdinal("DestinataireId")),
                            Titre = reader.GetString(reader.GetOrdinal("Titre")),
                            Corps = reader.GetString(reader.GetOrdinal("Corps")),
                            Type = (NotificationType)reader.GetInt32(reader.GetOrdinal("Type")),
                            Statut = (StatutNotification)reader.GetInt32(reader.GetOrdinal("Statut")),
                            DateCreation = reader.GetDateTime(reader.GetOrdinal("DateCreation")),
                            DateLecture = reader.IsDBNull(reader.GetOrdinal("DateLecture")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("DateLecture"))
                        });
                    }
                }
            }

            return notifications;
        }

        public async Task<IEnumerable<Notification>> GetNotificationsByTypeAsync(NotificationType type)
        {
            var notifications = new List<Notification>();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var command = new SqlCommand("SELECT * FROM Notifications WHERE Type = @Type", connection);
                command.Parameters.AddWithValue("@Type", (int)type);

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        notifications.Add(new Notification
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            EmetteurId = reader.GetInt32(reader.GetOrdinal("EmetteurId")),
                            DestinataireId = reader.GetInt32(reader.GetOrdinal("DestinataireId")),
                            Titre = reader.GetString(reader.GetOrdinal("Titre")),
                            Corps = reader.GetString(reader.GetOrdinal("Corps")),
                            Type = (NotificationType)reader.GetInt32(reader.GetOrdinal("Type")),
                            Statut = (StatutNotification)reader.GetInt32(reader.GetOrdinal("Statut")),
                            DateCreation = reader.GetDateTime(reader.GetOrdinal("DateCreation")),
                            DateLecture = reader.IsDBNull(reader.GetOrdinal("DateLecture")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("DateLecture"))
                        });
                    }
                }
            }

            return notifications;
        }

        public async Task<IEnumerable<Notification>> GetNotificationsByStatusAsync(StatutNotification status)
        {
            var notifications = new List<Notification>();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var command = new SqlCommand("SELECT * FROM Notifications WHERE Statut = @Statut", connection);
                command.Parameters.AddWithValue("@Statut", (int)status);

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        notifications.Add(new Notification
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            EmetteurId = reader.GetInt32(reader.GetOrdinal("EmetteurId")),
                            DestinataireId = reader.GetInt32(reader.GetOrdinal("DestinataireId")),
                            Titre = reader.GetString(reader.GetOrdinal("Titre")),
                            Corps = reader.GetString(reader.GetOrdinal("Corps")),
                            Type = (NotificationType)reader.GetInt32(reader.GetOrdinal("Type")),
                            Statut = (StatutNotification)reader.GetInt32(reader.GetOrdinal("Statut")),
                            DateCreation = reader.GetDateTime(reader.GetOrdinal("DateCreation")),
                            DateLecture = reader.IsDBNull(reader.GetOrdinal("DateLecture")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("DateLecture"))
                        });
                    }
                }
            }

            return notifications;
        }
    }
}