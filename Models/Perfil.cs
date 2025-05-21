namespace TEST.Models
{
    public class Perfil
    {
        public int IdPerfil { get; set; }
        public string NombrePerfil { get; set; } = string.Empty;

        public ICollection<Usuario>? Usuarios { get; set; }
    }
}
