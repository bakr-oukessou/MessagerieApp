﻿using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Collections.Generic;
using MessagerieApp.Models;
using MessagerieApp.Repository.Interfaces;

namespace MessagerieApp.Repositories
{
    public class DemandeRessourceRepository : IDemandeRessourceRepository
    {
        private readonly string _connectionString;

        public DemandeRessourceRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<IEnumerable<DemandeRessource>> GetAllDemandesAsync()
        {
            var demandes = new List<DemandeRessource>();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var command = new SqlCommand("SELECT * FROM RessourceRequests", connection);
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        demandes.Add(new DemandeRessource
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            DepartmentId = reader.GetInt32(reader.GetOrdinal("DepartmentId")),
                            RequestDate = reader.GetDateTime(reader.GetOrdinal("RequestDate")),
                            Status = reader.GetString(reader.GetOrdinal("Status"))
                        });
                    }
                }
            }

            return demandes;
        }

        public async Task<DemandeRessource> GetDemandeByIdAsync(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var command = new SqlCommand("SELECT * FROM RessourceRequests WHERE Id = @Id", connection);
                command.Parameters.AddWithValue("@Id", id);

                using (var reader = await command.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        return new DemandeRessource
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            DepartmentId = reader.GetInt32(reader.GetOrdinal("DepartmentId")),
                            RequestDate = reader.GetDateTime(reader.GetOrdinal("RequestDate")),
                            Status = reader.GetString(reader.GetOrdinal("Status"))
                        };
                    }
                }
            }

            return null;
        }

        public async Task AddDemandeAsync(DemandeRessource demande)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var command = new SqlCommand(
                    "INSERT INTO RessourceRequests (DepartmentId, RequestDate, Status) " +
                    "VALUES (@DepartmentId, @RequestDate, @Status); SELECT SCOPE_IDENTITY();",
                    connection);

                command.Parameters.AddWithValue("@DepartmentId", demande.DepartmentId);
                command.Parameters.AddWithValue("@RequestDate", demande.RequestDate);
                command.Parameters.AddWithValue("@Status", demande.Status);

                demande.Id = Convert.ToInt32(await command.ExecuteScalarAsync());
            }
        }

        public async Task UpdateDemandeAsync(DemandeRessource demande)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var command = new SqlCommand(
                    "UPDATE RessourceRequests SET DepartmentId = @DepartmentId, RequestDate = @RequestDate, Status = @Status WHERE Id = @Id",
                    connection);

                command.Parameters.AddWithValue("@Id", demande.Id);
                command.Parameters.AddWithValue("@DepartmentId", demande.DepartmentId);
                command.Parameters.AddWithValue("@RequestDate", demande.RequestDate);
                command.Parameters.AddWithValue("@Status", demande.Status);

                await command.ExecuteNonQueryAsync();
            }
        }

        public async Task DeleteDemandeAsync(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var command = new SqlCommand("DELETE FROM RessourceRequests WHERE Id = @Id", connection);
                command.Parameters.AddWithValue("@Id", id);

                await command.ExecuteNonQueryAsync();
            }
        }

        public async Task UpdateDemandeStatusAsync(int demandeId, string status)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var command = new SqlCommand(
                    "UPDATE RessourceRequests SET Status = @Status WHERE Id = @Id",
                    connection);

                command.Parameters.AddWithValue("@Id", demandeId);
                command.Parameters.AddWithValue("@Status", status);

                await command.ExecuteNonQueryAsync();
            }
        }

        public async Task<IEnumerable<DemandeRessource>> GetDemandesByDepartmentAsync(int departmentId)
        {
            var demandes = new List<DemandeRessource>();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var command = new SqlCommand("SELECT * FROM RessourceRequests WHERE DepartmentId = @DepartmentId", connection);
                command.Parameters.AddWithValue("@DepartmentId", departmentId);

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        demandes.Add(new DemandeRessource
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            DepartmentId = reader.GetInt32(reader.GetOrdinal("DepartmentId")),
                            RequestDate = reader.GetDateTime(reader.GetOrdinal("RequestDate")),
                            Status = reader.GetString(reader.GetOrdinal("Status"))
                        });
                    }
                }
            }

            return demandes;
        }
        public async Task AddDemandeRessourceItemAsync(DemandeRessourceItem item)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var command = new SqlCommand(
                    "INSERT INTO RessourceRequestItems (ResourceRequestId, Type, Specifications, Quantity, AssignedToUserId) " +
                    "VALUES (@ResourceRequestId, @Type, @Specifications, @Quantity, @AssignedToUserId); SELECT SCOPE_IDENTITY();",
                    connection);

                command.Parameters.AddWithValue("@ResourceRequestId", item.ResourceRequestId);
                command.Parameters.AddWithValue("@Type", item.Type);
                command.Parameters.AddWithValue("@Specifications", item.Specifications);
                command.Parameters.AddWithValue("@Quantity", item.Quantity);
                command.Parameters.AddWithValue("@AssignedToUserId", item.AssignedToUserId ?? (object)DBNull.Value);

                item.Id = Convert.ToInt32(await command.ExecuteScalarAsync());
            }
        }

        public async Task<IEnumerable<DemandeRessourceItem>> GetDemandeRessourceItemsAsync(int demandeId)
        {
            var items = new List<DemandeRessourceItem>();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var command = new SqlCommand("SELECT * FROM RessourceRequestItems WHERE ResourceRequestId = @DemandeId", connection);
                command.Parameters.AddWithValue("@DemandeId", demandeId);

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        items.Add(new DemandeRessourceItem
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            ResourceRequestId = reader.GetInt32(reader.GetOrdinal("ResourceRequestId")),
                            Type = reader.GetString(reader.GetOrdinal("Type")),
                            Specifications = reader.GetString(reader.GetOrdinal("Specifications")),
                            Quantity = reader.GetInt32(reader.GetOrdinal("Quantity")),
                            AssignedToUserId = reader.IsDBNull(reader.GetOrdinal("AssignedToUserId")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("AssignedToUserId"))
                        });
                    }
                }
            }

            return items;
        }

        public async Task AssignResourcesToTeachersAsync(int demandeId, IEnumerable<DemandeRessourceItem> assignments)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var transaction = connection.BeginTransaction();

                try
                {
                    foreach (var assignment in assignments)
                    {
                        var command = new SqlCommand(
                            "UPDATE RessourceRequestItems SET AssignedToUserId = @AssignedToUserId WHERE Id = @Id",
                            connection, transaction);

                        command.Parameters.AddWithValue("@Id", assignment.Id);
                        command.Parameters.AddWithValue("@AssignedToUserId", assignment.AssignedToUserId);

                        await command.ExecuteNonQueryAsync();
                    }

                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }
    }

}