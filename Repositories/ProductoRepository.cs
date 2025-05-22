using Microsoft.AspNetCore.Mvc;
using TEST.Shared;
using TEST.Models;
using Dapper;
using TEST.DTOS;

namespace TEST.Repositories
{
    public class ProductoRepository
    {
        private readonly DbConnection _dbConnection;

        public ProductoRepository(DbConnection dbConecction)
        {
            _dbConnection = dbConecction;
        }

        public int Insertar(ProductoInsertDTO producto)
        {
            using var connection = _dbConnection.GetOpenConnection();
            string sql = @"
                            INSERT INTO Productos (ClaveProducto, Nombre, Existencia)
                            VALUES (@ClaveProducto, @Nombre, @Existencia);
                            SELECT CAST(SCOPE_IDENTITY() AS int);
                         ";

            var id = connection.QueryFirstOrDefault<int>(sql, new
            {
                ClaveProducto = producto.ClaveProducto,
                Nombre = producto.Nombre,
                Existencia = producto.Existencia
            });

            return id;
        }

        public void ActualizarExistencia(int idProducto, int nuevaExistencia)
        {
            using var connection = _dbConnection.GetOpenConnection();
            string sql = @"
                            UPDATE Productos
                            SET Existencia = Existencia + @NuevaExistencia
                            WHERE IdProducto = @IdProducto;
                         ";

            _ = connection.Execute(sql, new
            {
                NuevaExistencia = nuevaExistencia,
                IdProducto = idProducto
            });
        }

        public ProductoReporteDTO[]? ReporteExistencia()
        {
            using var connection = _dbConnection.GetOpenConnection();
            string sql = @"
                            SELECT IdProducto, ClaveProducto, Nombre, Existencia, FechaRegistro
                            FROM Productos;
                         ";
            return connection.Query<ProductoReporteDTO>(sql).ToArray();
        }

    }
}