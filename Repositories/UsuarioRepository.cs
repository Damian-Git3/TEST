using Dapper;
using Microsoft.AspNetCore.Connections;
using TEST.DTOS;
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

        public int Insertar(UsuarioRegisterDTO usuario)
        {
            using var connection = _dbConecction.GetOpenConnection();
            string sql = "sp_RegistrarUsuario";
            var id = connection.QueryFirstOrDefault<int>(
                sql,
                new
                {
                    Nombre = usuario.Nombre,
                    Correo = usuario.Correo,
                    Contraseña = usuario.Contraseña,
                    IdPerfil = usuario.IdPerfil
                },
                commandType: System.Data.CommandType.StoredProcedure
            );
            return id;
        }

    }
}
