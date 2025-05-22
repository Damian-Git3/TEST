using Dapper;
using TEST.Models;
using TEST.Shared;

namespace TEST.Repositories
{
    public class PedidoRepository
    {
        private readonly DbConnection _dbConecction;
        public PedidoRepository(DbConnection dbConecction)
        {
            _dbConecction = dbConecction;
        }

        public List<Pedido> ObtenerPedidos()
        {
            using var connection = _dbConecction.GetOpenConnection();
            string sql = @"SELECT * FROM Pedidos";
            return connection.Query<Pedido>(sql).ToList();
        }

        public Pedido? ObtenerPedidoPorId(int id)
        {
            using var connection = _dbConecction.GetOpenConnection();
            string sql = @"SELECT * FROM Pedidos WHERE Id = @Id";
            return connection.QueryFirstOrDefault<Pedido>(sql, new { Id = id });
        }

        public int Insertar(Pedido pedido)
        {
            using var connection = _dbConecction.GetOpenConnection();
            string sql = @"INSERT INTO Pedidos 
                                    (IdVendedor, NombreCliente) 
                           VALUES (@IdVendedor, @IdProducto, @Cantidad, @FechaPedido);
                           SELECT CAST(SCOPE_IDENTITY() as int);";
            return connection.QuerySingle<int>(sql, pedido);
        }

        public int InsertarDetalle(DetallePedido pedidoDetalle)
        {
            using var connection = _dbConecction.GetOpenConnection();
            string sql = @"INSERT INTO DetallePedidos (IdPedido, IdProducto, Cantidad) 
                           VALUES (@IdPedido, @IdProducto, @Cantidad)
                        SELECT CAST(SCOPE_IDENTITY() as int)
                            ";
            connection.Execute(sql, pedidoDetalle);
            return connection.QuerySingle<int>(sql, pedidoDetalle);
        }

    }
}
