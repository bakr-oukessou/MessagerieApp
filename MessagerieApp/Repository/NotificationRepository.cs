using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using MessagerieApp.Helpers;
using MessagerieApp.Models;
using MessagerieApp.Repository.Interfaces;
using Microsoft.Extensions.Configuration;

namespace MessagerieApp.Repositories
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly DatabaseHelper _databaseHelper;

        public NotificationRepository(string connectionString)
        {
            _databaseHelper = new DatabaseHelper(connectionString);
        }

        public async Task<List<Notification>> GetAllNotificationsAsync()
        {
            var notifications = new List<Notification>();

            using (SqlConnection conn = await _databaseHelper.CreateAndOpenConnectionAsync())
            {
                conn.Open();
                string query = "SELECT * FROM Notifications";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            notifications.Add(new Notification
                            {
                                Id = reader.GetInt32("Id"),
                                EmetteurId = reader.GetInt32("EmetteurId"),
                                DestinataireId = reader.GetInt32("DestinataireId"),
                                Titre = reader.GetString("Titre"),
                                Corps = reader.GetString("Corps"),
                                Type = (NotificationType)reader.GetInt32("Type"),
                                Statut = (StatutNotification)reader.GetInt32("Statut"),
                                DateCreation = reader.GetDateTime("DateCreation"),
                                DateLecture = reader.IsDBNull("DateLecture") ? (DateTime?)null : reader.GetDateTime("DateLecture")
                            });
                        }
                    }
                }
            }
            return notifications;
        }

        public async Task<Notification> GetNotificationByIdAsync(int id)
        {
            Notification notification = null;

            using (SqlConnection conn = await _databaseHelper.CreateAndOpenConnectionAsync())
            {
                conn.Open();
                string query = "SELECT * FROM Notifications WHERE Id = @Id";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            notification = new Notification
                            {
                                Id = reader.GetInt32("Id"),
                                EmetteurId = reader.GetInt32("EmetteurId"),
                                DestinataireId = reader.GetInt32("DestinataireId"),
                                Titre = reader.GetString("Titre"),
                                Corps = reader.GetString("Corps"),
                                Type = (NotificationType)reader.GetInt32("Type"),
                                Statut = (StatutNotification)reader.GetInt32("Statut"),
                                DateCreation = reader.GetDateTime("DateCreation"),
                                DateLecture = reader.IsDBNull("DateLecture") ? (DateTime?)null : reader.GetDateTime("DateLecture")
                            };
                        }
                    }
                }
            }
            return notification;
        }

        public async Task AddNotificationAsync(Notification notification)
        {
            using (SqlConnection conn = await _databaseHelper.CreateAndOpenConnectionAsync())
            {
                conn.Open();
                string query = "INSERT INTO Notifications (EmetteurId, DestinataireId, Titre, Corps, Type, Statut, DateCreation) " +
                               "VALUES (@EmetteurId, @DestinataireId, @Titre, @Corps, @Type, @Statut, @DateCreation)";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@EmetteurId", notification.EmetteurId);
                    cmd.Parameters.AddWithValue("@DestinataireId", notification.DestinataireId);
                    cmd.Parameters.AddWithValue("@Titre", notification.Titre);
                    cmd.Parameters.AddWithValue("@Corps", notification.Corps);
                    cmd.Parameters.AddWithValue("@Type", (int)notification.Type);
                    cmd.Parameters.AddWithValue("@Statut", (int)notification.Statut);
                    cmd.Parameters.AddWithValue("@DateCreation", notification.DateCreation);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public async Task MarkAsReadAsync(int id)
        {
            using (SqlConnection conn = await _databaseHelper.CreateAndOpenConnectionAsync())
            {
                conn.Open();
                string query = "UPDATE Notifications SET Statut = @Statut, DateLecture = @DateLecture WHERE Id = @Id";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    cmd.Parameters.AddWithValue("@Statut", (int)StatutNotification.Lue);
                    cmd.Parameters.AddWithValue("@DateLecture", DateTime.Now);

                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
