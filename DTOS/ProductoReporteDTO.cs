namespace TEST.DTOS
{
    public class ProductoReporteDTO
    {
        public int IdProducto { get; set; }
        public string ClaveProducto { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public int Existencia { get; set; }
        public DateTime FechaRegistro { get; set; }
    }
}
