namespace TEST.Models
{
    public class Producto
    {
        public int IdProducto { get; set; }
        public string ClaveProducto { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public int Existencia { get; set; }
        public DateTime FechaRegistro { get; set; }

        public ICollection<DetallePedido>? DetallesPedido { get; set; }
    }
}
