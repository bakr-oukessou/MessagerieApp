using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Collections.Generic;
using MessagerieApp.Models;
using MessagerieApp.Repository.Interfaces;

namespace MessagerieApp.Repositories
{
	public class UserRepository : IUserRepository
	{
		private readonly string _connectionString;

		public UserRepository(string connectionString)
		{
			_connectionString = connectionString;
		}

		public async Task<IEnumerable<User>> GetAllUsersAsync()
		{
			var users = new List<User>();

			using (var connection = new SqlConnection(_connectionString))
			{
				await connection.OpenAsync();
				var command = new SqlCommand("SELECT * FROM Users", connection);
				using (var reader = await command.ExecuteReaderAsync())
				{
					while (await reader.ReadAsync())
					{
						users.Add(new User
						{
							Id = reader.GetInt32(reader.GetOrdinal("Id")),
							UserName = reader.GetString(reader.GetOrdinal("UserName")),
							Email = reader.GetString(reader.GetOrdinal("Email")),
							Role = Enum.Parse<UserRole>(reader.GetString(reader.GetOrdinal("Role"))), // Conversion string → enum
							PasswordHash = (byte[])reader["PasswordHash"],
							PasswordSalt = (byte[])reader["PasswordSalt"],
							DepartmentId = reader.IsDBNull(reader.GetOrdinal("DepartmentId")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("DepartmentId")),
							SupplierId = reader.IsDBNull(reader.GetOrdinal("SupplierId")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("SupplierId"))
						});
					}
				}
			}

			return users;
		}

		public async Task<bool> EmailExists(string email)
		{
			using var connection = new SqlConnection(_connectionString);
			await connection.OpenAsync();

			var command = new SqlCommand(
				"SELECT 1 FROM Users WHERE Email = @Email",
				connection);

			command.Parameters.AddWithValue("@Email", email);
			return await command.ExecuteScalarAsync() != null;
		}

		public async Task<User> GetUserByIdAsync(int id)
		{
			using (var connection = new SqlConnection(_connectionString))
			{
				await connection.OpenAsync();
				var command = new SqlCommand("SELECT * FROM Users WHERE Id = @Id", connection);
				command.Parameters.AddWithValue("@Id", id);

				using (var reader = await command.ExecuteReaderAsync())
				{
					if (await reader.ReadAsync())
					{
						return new User
						{
							Id = reader.GetInt32(reader.GetOrdinal("Id")),
							UserName = reader.GetString(reader.GetOrdinal("UserName")),
							Email = reader.GetString(reader.GetOrdinal("Email")),
							Role = Enum.Parse<UserRole>(reader.GetString(reader.GetOrdinal("Role"))),
							PasswordHash = (byte[])reader["PasswordHash"],
							PasswordSalt = (byte[])reader["PasswordSalt"],
							DepartmentId = reader.IsDBNull(reader.GetOrdinal("DepartmentId")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("DepartmentId")),
							SupplierId = reader.IsDBNull(reader.GetOrdinal("SupplierId")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("SupplierId"))
						};
					}
				}
			}

			return null;
		}

		public async Task<User> GetUserByEmailAsync(string email)
		{
			using (var connection = new SqlConnection(_connectionString))
			{
				await connection.OpenAsync();
				var command = new SqlCommand("SELECT * FROM Users WHERE Email = @Email", connection);
				command.Parameters.AddWithValue("@Email", email);

				using (var reader = await command.ExecuteReaderAsync())
				{
					if (await reader.ReadAsync())
					{
						return new User
						{
							Id = reader.GetInt32(reader.GetOrdinal("Id")),
							UserName = reader.GetString(reader.GetOrdinal("UserName")),
							Email = reader.GetString(reader.GetOrdinal("Email")),
							Role = Enum.Parse<UserRole>(reader.GetString(reader.GetOrdinal("Role"))),
							PasswordHash = (byte[])reader["PasswordHash"],
							PasswordSalt = (byte[])reader["PasswordSalt"],
							DepartmentId = reader.IsDBNull(reader.GetOrdinal("DepartmentId")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("DepartmentId")),
							SupplierId = reader.IsDBNull(reader.GetOrdinal("SupplierId")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("SupplierId"))
						};
					}
				}
			}

			return null;
		}

		public async Task AddUserAsync(User user)
		{
			using (var connection = new SqlConnection(_connectionString))
			{
				await connection.OpenAsync();
				var command = new SqlCommand(
					@"INSERT INTO Users 
            (UserName, Email, PasswordHash, PasswordSalt, Role, DepartmentId, SupplierId) 
            VALUES 
            (@UserName, @Email, @PasswordHash, @PasswordSalt, @Role, @DepartmentId, @SupplierId);
            SELECT SCOPE_IDENTITY();",
					connection);

				command.Parameters.AddWithValue("@UserName", user.UserName);
				command.Parameters.AddWithValue("@Email", user.Email);
				command.Parameters.AddWithValue("@PasswordHash", user.PasswordHash);
				command.Parameters.AddWithValue("@PasswordSalt", user.PasswordSalt);
				command.Parameters.AddWithValue("@Role", user.Role.ToString());
				command.Parameters.AddWithValue("@DepartmentId", (object)user.DepartmentId ?? DBNull.Value);
				command.Parameters.AddWithValue("@SupplierId", (object)user.SupplierId ?? DBNull.Value);

				user.Id = Convert.ToInt32(await command.ExecuteScalarAsync());
			}
		}

		public async Task UpdateUserAsync(User user)
		{
			using (var connection = new SqlConnection(_connectionString))
			{
				await connection.OpenAsync();
				var command = new SqlCommand(
					@"UPDATE Users 
                    SET UserName = @UserName, Email = @Email, Role = @Role, 
                        DepartmentId = @DepartmentId, SupplierId = @SupplierId, 
                        PasswordHash = @PasswordHash, PasswordSalt = @PasswordSalt 
                    WHERE Id = @Id",
					connection);

				command.Parameters.AddWithValue("@Id", user.Id);
				command.Parameters.AddWithValue("@UserName", user.UserName);
				command.Parameters.AddWithValue("@Email", user.Email);
				command.Parameters.AddWithValue("@Role", user.Role.ToString());
				command.Parameters.AddWithValue("@DepartmentId", user.DepartmentId ?? (object)DBNull.Value);
				command.Parameters.AddWithValue("@SupplierId", user.SupplierId ?? (object)DBNull.Value);
				command.Parameters.AddWithValue("@PasswordHash", user.PasswordHash);
				command.Parameters.AddWithValue("@PasswordSalt", user.PasswordSalt);

				await command.ExecuteNonQueryAsync();
			}
		}

		public async Task DeleteUserAsync(int id)
		{
			using (var connection = new SqlConnection(_connectionString))
			{
				await connection.OpenAsync();
				var command = new SqlCommand("DELETE FROM Users WHERE Id = @Id", connection);
				command.Parameters.AddWithValue("@Id", id);

				await command.ExecuteNonQueryAsync();
			}
		}

		public async Task<IEnumerable<User>> GetUsersByDepartmentAsync(int departmentId)
		{
			var utilisateurs = new List<User>();

			using (var connection = new SqlConnection(_connectionString))
			{
				await connection.OpenAsync();
				var command = new SqlCommand("SELECT * FROM Users WHERE DepartmentId = @DepartmentId", connection);
				command.Parameters.AddWithValue("@DepartmentId", departmentId);

				using (var reader = await command.ExecuteReaderAsync())
				{
					while (await reader.ReadAsync())
					{
						utilisateurs.Add(new User
						{
							Id = reader.GetInt32(reader.GetOrdinal("Id")),
							UserName = reader.GetString(reader.GetOrdinal("UserName")),
							Email = reader.GetString(reader.GetOrdinal("Email")),
							Role = Enum.Parse<UserRole>(reader.GetString(reader.GetOrdinal("Role"))),
							PasswordHash = (byte[])reader["PasswordHash"],
							PasswordSalt = (byte[])reader["PasswordSalt"],
							DepartmentId = reader.GetInt32(reader.GetOrdinal("DepartmentId")),
							SupplierId = reader.IsDBNull(reader.GetOrdinal("SupplierId")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("SupplierId"))
						});
					}
				}
			}

			return utilisateurs;
		}
        public async Task<string> GetUserRoleAsync(string username)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var command = new SqlCommand(
                    "SELECT Role FROM Users WHERE Username = @Username",
                    connection);

                command.Parameters.AddWithValue("@Username", username);

                var result = await command.ExecuteScalarAsync();

                if (result == null)
                {
                    // Log an error or throw an exception
                    throw new InvalidOperationException($"User '{username}' not found or has no role assigned.");
                }

                return result.ToString();
            }
        }
        //public async Task<User> GetUserRoleAsync(UserRole username)
        //{
        //    using (var connection = new SqlConnection(_connectionString))
        //    {
        //        await connection.OpenAsync();
        //        var command = new SqlCommand("SELECT * FROM Users WHERE UserName = @UserName", connection);
        //        command.Parameters.AddWithValue("@UserName", username);

        //        using (var reader = await command.ExecuteReaderAsync())
        //        {
        //            if (await reader.ReadAsync())
        //            {
        //                return new User
        //                {
        //                    Id = reader.GetInt32(reader.GetOrdinal("Id")),
        //                    UserName = reader.GetString(reader.GetOrdinal("UserName")),
        //                    Email = reader.GetString(reader.GetOrdinal("Email")),
        //                    Role = Enum.Parse<UserRole>(reader.GetString(reader.GetOrdinal("Role"))),
        //                    PasswordHash = (byte[])reader["PasswordHash"],
        //                    PasswordSalt = (byte[])reader["PasswordSalt"],
        //                    DepartmentId = reader.IsDBNull(reader.GetOrdinal("DepartmentId")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("DepartmentId")),
        //                    SupplierId = reader.IsDBNull(reader.GetOrdinal("SupplierId")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("SupplierId"))
        //                };
        //            }
        //        }
        //    }

        //    return null;
        //}

    
    }
}