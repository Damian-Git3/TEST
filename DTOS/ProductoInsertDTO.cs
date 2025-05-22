namespace TEST.DTOS
{
    public class ProductoInsertDTO
    {
        public string ClaveProducto { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public int Existencia { get; set; }
    }
}
