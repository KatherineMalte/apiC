using APIRESTMecanico.datos.Datos;
using APIRESTMecanico.datos.Modelo;
using Microsoft.AspNetCore.Mvc;

namespace APIRESTMecanico.datos.Controllers
{
    [ApiController]
    [Route("api/producto")]
    public class ProductoController : Controller
    {
        [HttpGet]
        public async Task<ActionResult<List<Producto>>> GetProducto()
        {
            var funcion = new ProductoDAO();
            List<Producto> _listar = await funcion.Listar();
            return StatusCode(StatusCodes.Status200OK, _listar);
        }
        [HttpPost]
        public async Task<ActionResult> PostProducto([FromBody] Producto producto)
        {
            var funcion = new ProductoDAO();
            await funcion.Insertar(producto);
            return NoContent();
        }
        /*
        [HttpGet]
        public async Task<ActionResult<List<Producto>>> GetProductoId(int id)
        {
            var funcion = new ProductoDAO();
            List<Producto> _listar = await funcion.TraerPorId(id);
            await funcion.TraerPorId(id);
            return StatusCode(StatusCodes.Status200OK, _listar);
        }
        [HttpPost]
        public async Task<ActionResult> PostProducto([FromBody] Producto producto) 
        {
            var funcion = new ProductoDAO();
            await funcion.Insertar(producto);
            return NoContent();
        }*/
    }
}
