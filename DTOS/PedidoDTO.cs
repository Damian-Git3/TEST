namespace TEST.DTOS
{
    public class PedidoDTO
    {
        public int IdVendedor { get; set; }
        public string NombreCliente { get; set; } = string.Empty;
        public List<DetallePedidoDTO> Detalles { get; set; } = new List<DetallePedidoDTO>();
    }
}
