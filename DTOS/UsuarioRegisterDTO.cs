using Microsoft.AspNetCore.Mvc;

namespace TEST.DTOS
{
    public class UsuarioRegisterDTO
    {
        public string Nombre { get; set; } = string.Empty;
        public string Correo { get; set; } = string.Empty;
        public string Contraseña { get; set; } = string.Empty;
        public int IdPerfil { get; set; }
    }
}
