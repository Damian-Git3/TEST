using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TEST.Repositories;

namespace TEST.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsuarioController : Controller
    {
        private readonly UsuarioRepository _repo;

        public UsuarioController(UsuarioRepository repo)
        {
            _repo = repo;
        }

        [HttpPost("correo")]
        public IActionResult ObtenerPorCorreo([FromBody] string correo)
        {
            var usuario = _repo.ObtenerPorCorreo(correo);
            if (usuario == null)
            {
                return NotFound();
            }
            return Ok(usuario);
        }
    }
}
