using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TEST.DTOS;
using TEST.Repositories;
using TEST.Shared;

namespace TEST.Controllers
{
    [ApiController]
    [Route("usuario")]
    public class UsuarioController : Controller
    {
        private readonly UsuarioRepository _repo;

        public UsuarioController(UsuarioRepository repo)
        {
            _repo = repo;
        }

        [HttpPost("registrar")]
        public IActionResult RegistrarUsuario(UsuarioRegisterDTO usuario)
        {
            // Encriptar la contraseña antes de guardar
            usuario.Contraseña = BCrypt.Net.BCrypt.HashPassword(usuario.Contraseña);

            var IdUsuario = _repo.Insertar(usuario);

            if (IdUsuario == 0)
            {
                return BadRequest("Error al registrar el usuario.");
            }

            return Ok(IdUsuario);
        }

        [HttpPost("login")]
        public IActionResult Login(LoginDTO data)
        {

            if (string.IsNullOrWhiteSpace(data.Correo) || string.IsNullOrWhiteSpace(data.Contrasenia))
            {
                return BadRequest("Correo y contraseña son obligatorios.");
            }

            var usuario = _repo.ObtenerPorCorreo(data.Correo);

            if (usuario == null)
            {
                return NotFound("Usuario no encontrado.");
            }

            if (!BCrypt.Net.BCrypt.Verify(data.Contrasenia, usuario.Contraseña))
            {
                return Unauthorized("Contraseña incorrecta.");
            }

            var secret = Environment.GetEnvironmentVariable("SECRET_WORD");
            if (string.IsNullOrEmpty(secret))
            {
                return StatusCode(500, "Error de configuración del servidor.");
            }

            var token = JWT.GenerarToken(usuario.IdUsuario.ToString(), Environment.GetEnvironmentVariable("SECRET_WORD"));

            return Ok(new
            {
                token,
                usuario = new
                {
                    usuario.IdUsuario,
                    usuario.Nombre,
                    usuario.Correo,
                    usuario.Perfil
                }
            });

        }

        [Authorize]
        [HttpGet("listar")]
        public IActionResult Listar()
        {
            var usuarios = _repo.Listar();

            if (usuarios == null || !usuarios.Any())
            {
                return NotFound("No se encontraron usuarios.");
            }

            return Ok(usuarios);
        }
    }
}
