using System.Data.SqlClient;

namespace MessagerieApp.Data
{
    public class DatabaseConnection
    {
        private string connectionString;

        public DatabaseConnection(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public SqlConnection GetConnection()
        {
            return new SqlConnection(connectionString);
        }
    }
}
