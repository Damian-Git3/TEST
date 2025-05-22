using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TEST.Repositories;

namespace TEST.Controllers
{
    [Authorize]
    [ApiController]
    [Route("perfil")]
    public class PerfilController : Controller
    {
        private readonly PerfilRepository _repo;

        public PerfilController(PerfilRepository repo)
        {
            _repo = repo;
        }

        [HttpGet("listar")]
        public IActionResult ListarPerfiles()
        {
            var perfiles = _repo.ListarPerfiles();
            if (perfiles == null)
            {
                return NotFound("No se encontraron perfiles.");
            }
            return Ok(perfiles);
        }
    }
}
