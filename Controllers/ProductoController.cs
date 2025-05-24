using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TEST.DTOS;
using TEST.Repositories;

namespace TEST.Controllers
{
    [Authorize]
    [ApiController]
    [Route("producto")]
    public class ProductoController : Controller
    {
        private readonly ProductoRepository _repo;

        public ProductoController(ProductoRepository repo)
        {
            _repo = repo;
        }

        [HttpPost("registrar")]
        public IActionResult RegistrarProducto(ProductoInsertDTO producto)
        {
            var IdProducto = _repo.Insertar(producto);
            if (IdProducto == 0)
            {
                return BadRequest("Error al registrar el producto.");
            }
            return Ok(IdProducto);
        }

        [HttpPut("actualizar-existencia")]
        public IActionResult ActualizarExistencia(int idProducto, int nuevaExistencia)
        {
            if (idProducto <= 0 || nuevaExistencia <= 0)
            {
                return BadRequest("El id del producto y la nueva existencia deben ser mayores a cero.");
            }
            _repo.ActualizarExistencia(idProducto, nuevaExistencia);
            return Ok("Existencia actualizada correctamente.");
        }

        [HttpGet("reporte-existencia")]
        public IActionResult ReporteExistencia()
        {
            var productos = _repo.ReporteExistencia();
            if (productos == null || !productos.Any())
            {
                return NotFound("No se encontraron productos.");
            }
            return Ok(productos);
        }
    }
}
