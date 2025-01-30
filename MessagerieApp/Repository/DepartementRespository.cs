using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Collections.Generic;
using MessagerieApp.Models;

namespace MessagerieApp.Repositories
{
    public class DepartementRepository : IDepartementRepository
    {
        private readonly string _connectionString;

        public DepartementRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<IEnumerable<Departement>> GetAllDepartementsAsync()
        {
            var departements = new List<Departement>();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var command = new SqlCommand("SELECT * FROM Departements", connection);
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        departements.Add(new Departement
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Nom = reader.GetString(reader.GetOrdinal("Nom")),
                            Code = reader.GetString(reader.GetOrdinal("Code")),
                            ResponsableId = reader.IsDBNull(reader.GetOrdinal("ResponsableId")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("ResponsableId"))
                        });
                    }
                }
            }

            return departements;
        }

        public async Task<Departement> GetDepartementByIdAsync(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var command = new SqlCommand("SELECT * FROM Departements WHERE Id = @Id", connection);
                command.Parameters.AddWithValue("@Id", id);

                using (var reader = await command.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        return new Departement
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Nom = reader.GetString(reader.GetOrdinal("Nom")),
                            Code = reader.GetString(reader.GetOrdinal("Code")),
                            ResponsableId = reader.IsDBNull(reader.GetOrdinal("ResponsableId")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("ResponsableId"))
                        };
                    }
                }
            }

            return null;
        }

        public async Task AddDepartementAsync(Departement departement)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var command = new SqlCommand(
                    "INSERT INTO Departements (Nom, Code, ResponsableId) " +
                    "VALUES (@Nom, @Code, @ResponsableId); SELECT SCOPE_IDENTITY();",
                    connection);

                command.Parameters.AddWithValue("@Nom", departement.Nom);
                command.Parameters.AddWithValue("@Code", departement.Code);
                command.Parameters.AddWithValue("@ResponsableId", departement.ResponsableId ?? (object)DBNull.Value);

                departement.Id = Convert.ToInt32(await command.ExecuteScalarAsync());
            }
        }

        public async Task UpdateDepartementAsync(Departement departement)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var command = new SqlCommand(
                    "UPDATE Departements SET Nom = @Nom, Code = @Code, ResponsableId = @ResponsableId WHERE Id = @Id",
                    connection);

                command.Parameters.AddWithValue("@Id", departement.Id);
                command.Parameters.AddWithValue("@Nom", departement.Nom);
                command.Parameters.AddWithValue("@Code", departement.Code);
                command.Parameters.AddWithValue("@ResponsableId", departement.ResponsableId ?? (object)DBNull.Value);

                await command.ExecuteNonQueryAsync();
            }
        }

        public async Task DeleteDepartementAsync(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var command = new SqlCommand("DELETE FROM Departements WHERE Id = @Id", connection);
                command.Parameters.AddWithValue("@Id", id);

                await command.ExecuteNonQueryAsync();
            }
        }
    }
}