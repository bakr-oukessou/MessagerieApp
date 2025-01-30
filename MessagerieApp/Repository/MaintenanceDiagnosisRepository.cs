using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Collections.Generic;
using MessagerieApp.Models;

namespace MessagerieApp.Repositories
{
    public class MaintenanceDiagnosisRepository : IMaintenanceDiagnosisRepository
    {
        private readonly string _connectionString;

        public MaintenanceDiagnosisRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<IEnumerable<MaintenanceDiagnosis>> GetAllMaintenanceDiagnosesAsync()
        {
            var diagnoses = new List<MaintenanceDiagnosis>();

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var command = new SqlCommand("SELECT * FROM MaintenanceDiagnosis", connection);
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        diagnoses.Add(new MaintenanceDiagnosis
                        {
                            Id = reader.GetString(reader.GetOrdinal("Id")),
                            MaintenanceTicketId = reader.GetString(reader.GetOrdinal("MaintenanceTicketId")),
                            DiagnosisDate = reader.GetDateTime(reader.GetOrdinal("DiagnosisDate")),
                            ProblemDescription = reader.GetString(reader.GetOrdinal("ProblemDescription")),
                            Frequency = reader.GetString(reader.GetOrdinal("Frequency")),
                            IssueType = reader.GetString(reader.GetOrdinal("IssueType")),
                            RequiresReplacement = reader.GetBoolean(reader.GetOrdinal("RequiresReplacement"))
                        });
                    }
                }
            }

            return diagnoses;
        }

        public async Task<MaintenanceDiagnosis> GetMaintenanceDiagnosisByIdAsync(string id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var command = new SqlCommand("SELECT * FROM MaintenanceDiagnosis WHERE Id = @Id", connection);
                command.Parameters.AddWithValue("@Id", id);

                using (var reader = await command.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        return new MaintenanceDiagnosis
                        {
                            Id = reader.GetString(reader.GetOrdinal("Id")),
                            MaintenanceTicketId = reader.GetString(reader.GetOrdinal("MaintenanceTicketId")),
                            DiagnosisDate = reader.GetDateTime(reader.GetOrdinal("DiagnosisDate")),
                            ProblemDescription = reader.GetString(reader.GetOrdinal("ProblemDescription")),
                            Frequency = reader.GetString(reader.GetOrdinal("Frequency")),
                            IssueType = reader.GetString(reader.GetOrdinal("IssueType")),
                            RequiresReplacement = reader.GetBoolean(reader.GetOrdinal("RequiresReplacement"))
                        };
                    }
                }
            }

            return null;
        }

        public async Task AddMaintenanceDiagnosisAsync(MaintenanceDiagnosis diagnosis)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var command = new SqlCommand(
                    "INSERT INTO MaintenanceDiagnosis (Id, MaintenanceTicketId, DiagnosisDate, ProblemDescription, Frequency, IssueType, RequiresReplacement) " +
                    "VALUES (@Id, @MaintenanceTicketId, @DiagnosisDate, @ProblemDescription, @Frequency, @IssueType, @RequiresReplacement);",
                    connection);

                command.Parameters.AddWithValue("@Id", diagnosis.Id);
                command.Parameters.AddWithValue("@MaintenanceTicketId", diagnosis.MaintenanceTicketId);
                command.Parameters.AddWithValue("@DiagnosisDate", diagnosis.DiagnosisDate);
                command.Parameters.AddWithValue("@ProblemDescription", diagnosis.ProblemDescription);
                command.Parameters.AddWithValue("@Frequency", diagnosis.Frequency);
                command.Parameters.AddWithValue("@IssueType", diagnosis.IssueType);
                command.Parameters.AddWithValue("@RequiresReplacement", diagnosis.RequiresReplacement);

                await command.ExecuteNonQueryAsync();
            }
        }

        public async Task UpdateMaintenanceDiagnosisAsync(MaintenanceDiagnosis diagnosis)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var command = new SqlCommand(
                    "UPDATE MaintenanceDiagnosis SET MaintenanceTicketId = @MaintenanceTicketId, DiagnosisDate = @DiagnosisDate, " +
                    "ProblemDescription = @ProblemDescription, Frequency = @Frequency, IssueType = @IssueType, RequiresReplacement = @RequiresReplacement " +
                    "WHERE Id = @Id",
                    connection);

                command.Parameters.AddWithValue("@Id", diagnosis.Id);
                command.Parameters.AddWithValue("@MaintenanceTicketId", diagnosis.MaintenanceTicketId);
                command.Parameters.AddWithValue("@DiagnosisDate", diagnosis.DiagnosisDate);
                command.Parameters.AddWithValue("@ProblemDescription", diagnosis.ProblemDescription);
                command.Parameters.AddWithValue("@Frequency", diagnosis.Frequency);
                command.Parameters.AddWithValue("@IssueType", diagnosis.IssueType);
                command.Parameters.AddWithValue("@RequiresReplacement", diagnosis.RequiresReplacement);

                await command.ExecuteNonQueryAsync();
            }
        }

        public async Task DeleteMaintenanceDiagnosisAsync(string id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var command = new SqlCommand("DELETE FROM MaintenanceDiagnosis WHERE Id = @Id", connection);
                command.Parameters.AddWithValue("@Id", id);

                await command.ExecuteNonQueryAsync();
            }
        }
    }
}