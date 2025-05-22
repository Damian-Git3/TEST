using Dapper;
using TEST.Models;
using TEST.Shared;

namespace TEST.Repositories
{
    public class PerfilRepository
    {
        private readonly DbConnection _dbConnection;

        public PerfilRepository(DbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }
        public Perfil[]? ListarPerfiles()
        {
            using var connection = _dbConnection.GetOpenConnection();
            string sql = @"SELECT * FROM Perfiles;";
            return connection.Query<Perfil>(sql).ToArray();
        }

    }
}
