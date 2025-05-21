using System.Data;
using Microsoft.Data.SqlClient;


namespace TEST.Shared
{
    public class DbConnection
    {
        private readonly string _connectionString;

        public DbConnection(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IDbConnection GetOpenConnection()
        {
            IDbConnection connection = new SqlConnection(_connectionString);
            connection.Open();
            return connection;
        }
    }
}
