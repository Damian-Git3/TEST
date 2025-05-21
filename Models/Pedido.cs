namespace TEST.Models
{
    public class Pedido
    {
        public int IdPedido { get; set; }
        public int IdVendedor { get; set; }
        public string NombreCliente { get; set; } = string.Empty;
        public DateTime FechaPedido { get; set; }

        public Usuario? Vendedor { get; set; }
        public ICollection<DetallePedido>? DetallesPedido { get; set; }
    }
}
