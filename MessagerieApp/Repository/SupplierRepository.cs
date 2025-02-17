﻿using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Collections.Generic;
using MessagerieApp.Models;
using System.Data;

namespace MessagerieApp.Repositories
{
	public class SupplierRepository : ISupplierRepository
	{
		private readonly string _connectionString;

		public SupplierRepository(string connectionString)
		{
			_connectionString = connectionString;
		}

		public async Task<IEnumerable<Supplier>> GetAllSuppliersAsync()
		{
			var suppliers = new List<Supplier>();

			using (var connection = new SqlConnection(_connectionString))
			{
				await connection.OpenAsync();
				var command = new SqlCommand("SELECT * FROM Fournisseur", connection); // Nom de table en anglais

				using (var reader = await command.ExecuteReaderAsync())
				{
					while (await reader.ReadAsync())
					{
						suppliers.Add(new Supplier
						{
							Id = reader.GetInt32(reader.GetOrdinal("Id")),
							CompanyName = reader.GetString(reader.GetOrdinal("CompanyName")),

							IsBlacklisted = reader.GetBoolean(reader.GetOrdinal("IsBlacklisted")),
							BlacklistReason = reader.IsDBNull(reader.GetOrdinal("BlacklistReason")) ? null : reader.GetString(reader.GetOrdinal("BlacklistReason"))
						});
					}
				}
			}

			return suppliers;
		}

		public async Task<Supplier> GetSupplierByIdAsync(int id)
		{
			using (var connection = new SqlConnection(_connectionString))
			{
				await connection.OpenAsync();
				var command = new SqlCommand("SELECT * FROM Suppliers WHERE Id = @Id", connection);
				command.Parameters.AddWithValue("@Id", id);

				using (var reader = await command.ExecuteReaderAsync())
				{
					if (await reader.ReadAsync())
					{
						return new Supplier
						{
							Id = reader.GetInt32(reader.GetOrdinal("Id")),
							CompanyName = reader.GetString(reader.GetOrdinal("CompanyName")),

							IsBlacklisted = reader.GetBoolean(reader.GetOrdinal("IsBlacklisted")),
							BlacklistReason = reader.IsDBNull(reader.GetOrdinal("BlacklistReason")) ? null : reader.GetString(reader.GetOrdinal("BlacklistReason"))
						};
					}
				}
			}

			return null;
		}

		private (byte[] passwordHash, byte[] passwordSalt) CreatePasswordHash(string password)
		{
			using (var hmac = new System.Security.Cryptography.HMACSHA512())
			{
				return (hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password)), hmac.Key);
			}
		}

		public async Task AddSupplierAsync(Supplier supplier)
		{
			using var connection = new SqlConnection(_connectionString);
			await connection.OpenAsync();

			var command = new SqlCommand(
				@"INSERT INTO Supplier (CompanyName, IsBlacklisted, BlacklistReason) 
        VALUES (@CompanyName, @IsBlacklisted, @BlacklistReason);
        SELECT SCOPE_IDENTITY();",
				connection);

			command.Parameters.AddWithValue("@CompanyName", supplier.CompanyName);
			command.Parameters.AddWithValue("@IsBlacklisted", supplier.IsBlacklisted);
			command.Parameters.AddWithValue("@BlacklistReason",
				(object)supplier.BlacklistReason ?? DBNull.Value);

			supplier.Id = Convert.ToInt32(await command.ExecuteScalarAsync());
		}



		public async Task UpdateSupplierAsync(Supplier supplier)
		{
			using (var connection = new SqlConnection(_connectionString))
			{
				await connection.OpenAsync();
				var command = new SqlCommand(
					@"UPDATE Suppliers 
                    SET CompanyName = @CompanyName, 
                        Location = @Location, 

                        IsBlacklisted = @IsBlacklisted, 
                        BlacklistReason = @BlacklistReason 
                    WHERE Id = @Id",
					connection);

				command.Parameters.AddWithValue("@Id", supplier.Id);
				command.Parameters.AddWithValue("@CompanyName", supplier.CompanyName);

				command.Parameters.AddWithValue("@IsBlacklisted", supplier.IsBlacklisted);
				command.Parameters.AddWithValue("@BlacklistReason", supplier.BlacklistReason ?? (object)DBNull.Value);

				await command.ExecuteNonQueryAsync();
			}
		}

		public async Task DeleteSupplierAsync(int id)
		{
			using (var connection = new SqlConnection(_connectionString))
			{
				await connection.OpenAsync();
				var command = new SqlCommand("DELETE FROM Suppliers WHERE Id = @Id", connection);
				command.Parameters.AddWithValue("@Id", id);

				await command.ExecuteNonQueryAsync();
			}
		}

		public async Task BlacklistSupplierAsync(int id, string reason)
		{
			using (var connection = new SqlConnection(_connectionString))
			{
				await connection.OpenAsync();
				var command = new SqlCommand(
					"UPDATE Suppliers SET IsBlacklisted = 1, BlacklistReason = @BlacklistReason WHERE Id = @Id",
					connection);

				command.Parameters.AddWithValue("@Id", id);
				command.Parameters.AddWithValue("@BlacklistReason", reason);

				await command.ExecuteNonQueryAsync();
			}
		}

		public async Task RemoveFromBlacklistAsync(int id)
		{
			using (var connection = new SqlConnection(_connectionString))
			{
				await connection.OpenAsync();
				var command = new SqlCommand(
					"UPDATE Suppliers SET IsBlacklisted = 0, BlacklistReason = NULL WHERE Id = @Id",
					connection);

				command.Parameters.AddWithValue("@Id", id);

				await command.ExecuteNonQueryAsync();
			}
		}
	}
}