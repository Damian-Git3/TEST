using Dapper;
using Microsoft.AspNetCore.Connections;
using TEST.Models;
using TEST.Shared;

namespace TEST.Repositories
{
    public class UsuarioRepository
    {
        private readonly DbConnection _dbConecction;

        public UsuarioRepository(DbConnection dbConecction)
        {
            _dbConecction = dbConecction;
        }

        public Usuario? ObtenerPorCorreo(string correo)
        {
            using var connection = _dbConecction.GetOpenConnection();
            string sql = @"SELECT * FROM Usuarios WHERE Correo = @Correo";
            return connection.QueryFirstOrDefault<Usuario>(sql, new { Correo = correo });
        }

        public void Insertar(Usuario usuario)
        {
            using var connection = _dbConecction.GetOpenConnection();
            string sql = @"
            INSERT INTO Usuarios (Nombre, Correo, Contraseña, IdPerfil)
            VALUES (@Nombre, @Correo, @Contraseña, @IdPerfil)";
            connection.Execute(sql, usuario);
        }
    }
}
