using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using MessagerieApp.Models.TransactionData;
using MessagerieApp.Repository.Interfaces.TransactionData;
using Microsoft.Extensions.Configuration;

namespace MessagerieApp.Repository
{
	public class OffreRepository : IOffreRepository
    {
        private readonly string _connectionString;

        public OffreRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<Offre> GetByIdAsync(int id, bool includeDetails = false)
        {
            Offre offre = null;
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                string query = "SELECT * FROM Offres WHERE Id = @Id";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            offre = new Offre
                            {
                                Id = reader.GetInt32("Id"),
                                StartDate = reader.GetDateTime("StartDate"),
                                EndDate = reader.GetDateTime("EndDate"),
                                Status = reader.GetString("Status")
                            };
                        }
                    }
                }
            }
            return offre;
        }

        public async Task<IEnumerable<Offre>> GetAllAsync()
        {
            List<Offre> offres = new List<Offre>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                string query = "SELECT * FROM Offres";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            offres.Add(new Offre
                            {
                                Id = reader.GetInt32("Id"),
                                StartDate = reader.GetDateTime("StartDate"),
                                EndDate = reader.GetDateTime("EndDate"),
                                Status = reader.GetString("Status")
                            });
                        }
                    }
                }
            }
            return offres;
        }

        public async Task<int> CreateOffreAsync(Offre offre)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                string query = "INSERT INTO Offres (StartDate, EndDate, Status) OUTPUT INSERTED.Id VALUES (@StartDate, @EndDate, @Status)";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@StartDate", offre.StartDate);
                    cmd.Parameters.AddWithValue("@EndDate", offre.EndDate);
                    cmd.Parameters.AddWithValue("@Status", offre.Status);
                    return (int)await cmd.ExecuteScalarAsync();
                }
            }
        }

        public async Task<bool> UpdateOffreAsync(Offre offre)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                string query = "UPDATE Offres SET StartDate = @StartDate, EndDate = @EndDate, Status = @Status WHERE Id = @Id";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", offre.Id);
                    cmd.Parameters.AddWithValue("@StartDate", offre.StartDate);
                    cmd.Parameters.AddWithValue("@EndDate", offre.EndDate);
                    cmd.Parameters.AddWithValue("@Status", offre.Status);
                    return await cmd.ExecuteNonQueryAsync() > 0;
                }
            }
        }

        public async Task<bool> DeleteOffreAsync(int id)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                string query = "DELETE FROM Offres WHERE Id = @Id";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    return await cmd.ExecuteNonQueryAsync() > 0;
                }
            }
        }

        public async Task<IEnumerable<AppelOffres>> GetAppelOffresByOffreIdAsync(int offreId)
        {
            List<AppelOffres> appels = new List<AppelOffres>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                string query = "SELECT * FROM AppelOffres WHERE OffreId = @OffreId";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@OffreId", offreId);
                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            appels.Add(new AppelOffres
                            {
                                Id = reader.GetInt32("Id"),
                                OffreId = reader.GetInt32("OffreId"),
                                FournisseurId = reader.GetInt32("FournisseurId"),
                                SubmissionDate = reader.GetDateTime("SubmissionDate"),
                                ProposedDeliveryDate = reader.GetDateTime("ProposedDeliveryDate"),
                                WarrantyMonths = reader.GetInt32("WarrantyMonths"),
                                TotalPrice = reader.GetDecimal("TotalPrice"),
                                Status = reader.GetString("Status")
                            });
                        }
                    }
                }
            }
            return appels;
        }
    }
}
