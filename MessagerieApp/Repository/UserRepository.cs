﻿using System.Data.SqlClient;
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
                            Role = reader.GetString(reader.GetOrdinal("Role")),
                            DepartmentId = reader.IsDBNull(reader.GetOrdinal("DepartmentId")) ? (int?)null : reader.GetOrdinal("DepartmentId")
                        });
                    }
                }
            }

            return users;
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
                            Role = reader.GetString(reader.GetOrdinal("Role")),
                            DepartmentId = reader.IsDBNull(reader.GetOrdinal("DepartmentId")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("DepartmentId"))
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
                            Role = reader.GetString(reader.GetOrdinal("Role")),
                            DepartmentId = reader.IsDBNull(reader.GetOrdinal("DepartmentId")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("DepartmentId"))
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
                    "INSERT INTO Users (UserName, Email, Role, DepartmentId) VALUES (@UserName, @Email, @Role, @DepartmentId); SELECT SCOPE_IDENTITY();",
                    connection);

                command.Parameters.AddWithValue("@UserName", user.UserName);
                command.Parameters.AddWithValue("@Email", user.Email);
                command.Parameters.AddWithValue("@Role", user.Role);
                command.Parameters.AddWithValue("@DepartmentId", user.DepartmentId ?? (object)DBNull.Value);

                user.Id = Convert.ToInt32(await command.ExecuteScalarAsync());
            }
        }

        public async Task UpdateUserAsync(User user)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var command = new SqlCommand(
                    "UPDATE Users SET UserName = @UserName, Email = @Email, Role = @Role, DepartmentId = @DepartmentId WHERE Id = @Id",
                    connection);

                command.Parameters.AddWithValue("@Id", user.Id);
                command.Parameters.AddWithValue("@UserName", user.UserName);
                command.Parameters.AddWithValue("@Email", user.Email);
                command.Parameters.AddWithValue("@Role", user.Role);
                command.Parameters.AddWithValue("@DepartmentId", user.DepartmentId ?? (object)DBNull.Value);

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
                            Role = reader.GetString(reader.GetOrdinal("Role")),
                            DepartmentId = reader.GetInt32(reader.GetOrdinal("DepartmentId"))
                        });
                    }
                }
            }

            return utilisateurs;
        }
    }
}