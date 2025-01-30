using System.Data.SqlClient;
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
                var command = new SqlCommand("SELECT * FROM DemandeRessources", connection);
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
                var command = new SqlCommand("SELECT * FROM DemandeRessources WHERE Id = @Id", connection);
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
                    "INSERT INTO DemandeRessources (DepartmentId, RequestDate, Status) " +
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
                    "UPDATE DemandeRessources SET DepartmentId = @DepartmentId, RequestDate = @RequestDate, Status = @Status WHERE Id = @Id",
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
                var command = new SqlCommand("DELETE FROM DemandeRessources WHERE Id = @Id", connection);
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
                    "UPDATE DemandeRessources SET Status = @Status WHERE Id = @Id",
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
                var command = new SqlCommand("SELECT * FROM DemandeRessources WHERE DepartmentId = @DepartmentId", connection);
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
    }
}