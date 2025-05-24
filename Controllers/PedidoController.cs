using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TEST.DTOS;

namespace TEST.Controllers
{
    [Authorize]
    [ApiController]
    [Route("pedido")]
    public class PedidoController : Controller
    {

        [HttpPost]
        [Route("insertar")]
        public IActionResult CrearPedido([FromBody] PedidoDTO data)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (data.Detalles.Count == 0)
            {
                return BadRequest("El pedido debe tener al menos un detalle.");
            }

            return Ok("Pedido creado con éxito");
        }


    }
}
