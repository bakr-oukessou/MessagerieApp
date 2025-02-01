using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Collections.Generic;
using MessagerieApp.Models;
using MessagerieApp.Repository.Interfaces;

namespace MessagerieApp.Repositories
{
    public class RessourceRepository : IRessourceRepository
    {
        private readonly string _connectionString;

        public RessourceRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<IEnumerable<Ressource>> GetAllRessourcesAsync()
        {
            var ressources = new List<Ressource>();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var command = new SqlCommand("SELECT * FROM Ressources", connection);
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        ressources.Add(new Ressource
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            InventoryNumber = reader.GetString(reader.GetOrdinal("InventoryNumber")),
                            Type = reader.GetString(reader.GetOrdinal("Type")),
                            Brand = reader.GetString(reader.GetOrdinal("Brand")),
                            DepartmentId = reader.IsDBNull(reader.GetOrdinal("DepartmentId")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("DepartmentId")),
                            AssignedToUserId = reader.IsDBNull(reader.GetOrdinal("AssignedToUserId")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("AssignedToUserId")),
                            AcquisitionDate = reader.GetDateTime(reader.GetOrdinal("AcquisitionDate")),
                            WarrantyEndDate = reader.IsDBNull(reader.GetOrdinal("WarrantyEndDate")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("WarrantyEndDate")),
                            Status = reader.GetString(reader.GetOrdinal("Status"))
                        });
                    }
                }
            }

            return ressources;
        }

        public async Task<Ressource> GetRessourceByIdAsync(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var command = new SqlCommand("SELECT * FROM Ressources WHERE Id = @Id", connection);
                command.Parameters.AddWithValue("@Id", id);

                using (var reader = await command.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        return new Ressource
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            InventoryNumber = reader.GetString(reader.GetOrdinal("InventoryNumber")),
                            Type = reader.GetString(reader.GetOrdinal("Type")),
                            Brand = reader.GetString(reader.GetOrdinal("Brand")),
                            DepartmentId = reader.IsDBNull(reader.GetOrdinal("DepartmentId")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("DepartmentId")),
                            AssignedToUserId = reader.IsDBNull(reader.GetOrdinal("AssignedToUserId")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("AssignedToUserId")),
                            AcquisitionDate = reader.GetDateTime(reader.GetOrdinal("AcquisitionDate")),
                            WarrantyEndDate = reader.IsDBNull(reader.GetOrdinal("WarrantyEndDate")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("WarrantyEndDate")),
                            Status = reader.GetString(reader.GetOrdinal("Status"))
                        };
                    }
                }
            }

            return null;
        }

        public async Task AddRessourceAsync(Ressource ressource)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var command = new SqlCommand(
                    "INSERT INTO Ressources (InventoryNumber, Type, Brand, DepartmentId, AssignedToUserId, AcquisitionDate, WarrantyEndDate, Status) " +
                    "VALUES (@InventoryNumber, @Type, @Brand, @DepartmentId, @AssignedToUserId, @AcquisitionDate, @WarrantyEndDate, @Status); SELECT SCOPE_IDENTITY();",
                    connection);

                command.Parameters.AddWithValue("@InventoryNumber", ressource.InventoryNumber);
                command.Parameters.AddWithValue("@Type", ressource.Type);
                command.Parameters.AddWithValue("@Brand", ressource.Brand);
                command.Parameters.AddWithValue("@DepartmentId", ressource.DepartmentId ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@AssignedToUserId", ressource.AssignedToUserId ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@AcquisitionDate", ressource.AcquisitionDate);
                command.Parameters.AddWithValue("@WarrantyEndDate", ressource.WarrantyEndDate ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@Status", ressource.Status);

                ressource.Id = Convert.ToInt32(await command.ExecuteScalarAsync());
            }
        }

        public async Task UpdateRessourceAsync(Ressource ressource)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var command = new SqlCommand(
                    "UPDATE Ressources SET InventoryNumber = @InventoryNumber, Type = @Type, Brand = @Brand, " +
                    "DepartmentId = @DepartmentId, AssignedToUserId = @AssignedToUserId, AcquisitionDate = @AcquisitionDate, " +
                    "WarrantyEndDate = @WarrantyEndDate, Status = @Status WHERE Id = @Id",
                    connection);

                command.Parameters.AddWithValue("@Id", ressource.Id);
                command.Parameters.AddWithValue("@InventoryNumber", ressource.InventoryNumber);
                command.Parameters.AddWithValue("@Type", ressource.Type);
                command.Parameters.AddWithValue("@Brand", ressource.Brand);
                command.Parameters.AddWithValue("@DepartmentId", ressource.DepartmentId ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@AssignedToUserId", ressource.AssignedToUserId ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@AcquisitionDate", ressource.AcquisitionDate);
                command.Parameters.AddWithValue("@WarrantyEndDate", ressource.WarrantyEndDate ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@Status", ressource.Status);

                await command.ExecuteNonQueryAsync();
            }
        }

        public async Task DeleteRessourceAsync(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var command = new SqlCommand("DELETE FROM Ressources WHERE Id = @Id", connection);
                command.Parameters.AddWithValue("@Id", id);

                await command.ExecuteNonQueryAsync();
            }
        }

        public async Task AssignRessourceToDepartmentAsync(int ressourceId, int departmentId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var command = new SqlCommand(
                    "UPDATE Ressources SET DepartmentId = @DepartmentId WHERE Id = @Id",
                    connection);

                command.Parameters.AddWithValue("@Id", ressourceId);
                command.Parameters.AddWithValue("@DepartmentId", departmentId);

                await command.ExecuteNonQueryAsync();
            }
        }

        public async Task AssignRessourceToUserAsync(int ressourceId, int userId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var command = new SqlCommand(
                    "UPDATE Ressources SET AssignedToUserId = @AssignedToUserId WHERE Id = @Id",
                    connection);

                command.Parameters.AddWithValue("@Id", ressourceId);
                command.Parameters.AddWithValue("@AssignedToUserId", userId);

                await command.ExecuteNonQueryAsync();
            }
        }

        public async Task UpdateRessourceStatusAsync(int ressourceId, string status)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var command = new SqlCommand(
                    "UPDATE Ressources SET Status = @Status WHERE Id = @Id",
                    connection);

                command.Parameters.AddWithValue("@Id", ressourceId);
                command.Parameters.AddWithValue("@Status", status);

                await command.ExecuteNonQueryAsync();
            }
        }
        public async Task AddRessourceFromDemandeAsync(DemandeRessourceItem item)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var command = new SqlCommand(
                    "INSERT INTO Ressources (InventoryNumber, Type, Brand, DepartmentId, AssignedToUserId, AcquisitionDate, Status) " +
                    "VALUES (@InventoryNumber, @Type, @Brand, @DepartmentId, @AssignedToUserId, @AcquisitionDate, @Status); SELECT SCOPE_IDENTITY();",
                    connection);

                command.Parameters.AddWithValue("@InventoryNumber", Guid.NewGuid().ToString()); // Generate a unique inventory number
                command.Parameters.AddWithValue("@Type", item.Type);
                command.Parameters.AddWithValue("@Brand", item.Specifications); // Use specifications as brand for simplicity
                command.Parameters.AddWithValue("@DepartmentId", item.ResourceRequestId); // Use the demande ID as department ID
                command.Parameters.AddWithValue("@AssignedToUserId", item.AssignedToUserId ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@AcquisitionDate", DateTime.UtcNow);
                command.Parameters.AddWithValue("@Status", "Active");

                await command.ExecuteNonQueryAsync();
            }
        }

        public Task<IEnumerable<Ressource>> GetRessourcesByDepartmentAsync(int departmentId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Ressource>> GetRessourcesByUserAsync(int userId)
        {
            throw new NotImplementedException();
        }
    }
}