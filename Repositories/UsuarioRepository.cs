using Dapper;
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
            string sql = @"
                SELECT u.*, p.IdPerfil, p.NombrePerfil
                FROM Usuarios u
                INNER JOIN Perfiles p ON u.IdPerfil = p.IdPerfil
                WHERE u.Correo = @Correo";
            return connection.Query<Usuario, Perfil, Usuario>(
                sql,
                (usuario, perfil) =>
                {
                    usuario.Perfil = perfil;
                    return usuario;
                },
                new { Correo = correo },
                splitOn: "IdPerfil"
            ).FirstOrDefault();
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

        public List<UsuarioDTO> Listar()
        {
            using var connection = _dbConecction.GetOpenConnection();
            string sql = @"SELECT IdUsuario, Nombre, Correo, IdPerfil, Activo, FechaCreacion FROM Usuarios";
            return connection.Query<UsuarioDTO>(sql).ToList();
        }
    }
}
