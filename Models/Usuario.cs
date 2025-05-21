namespace TEST.Models
{
    public class Usuario
    {
        public int IdUsuario { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Correo { get; set; } = string.Empty;
        public string Contraseña { get; set; } = string.Empty;
        public int IdPerfil { get; set; }
        public bool Activo { get; set; } = true;
        public DateTime FechaCreacion { get; set; }

        public Perfil? Perfil { get; set; }
    }
}
