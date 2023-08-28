using APIRESTMecanico.datos.Datos;
using APIRESTMecanico.datos.Modelo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.TagHelpers.Cache;

namespace APIRESTMecanico.datos.Controllers
{
    [ApiController]
        [Route("api/categoria")]
        public class CategoriaController:ControllerBase
        {
            [HttpGet]
            public async Task<ActionResult<List<Categoria>>> GetCategoria() 
            {
            var funcion = new CategoriaDAO();
                var lista = await funcion.Listar();
                return lista;
            }
        [HttpGet("{id}")]
        public async Task<ActionResult> GetCategoriaId(int id)
        {
            var funcion = new CategoriaDAO();
            List<Categoria> _listar = await funcion.TraerPorId(id);
            await funcion.TraerPorId(id);
            return StatusCode(StatusCodes.Status200OK, _listar); 
        }
        [HttpPost]
        public async Task<ActionResult> PostCategoria([FromBody] Categoria categoria)
        {
            var funcion = new CategoriaDAO();
            await funcion.Insertar(categoria);
            return NoContent();
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> PutCategoria(int id, [FromBody] Categoria categoria)
        {
            var funcion = new CategoriaDAO();
            categoria.idCategoria = id;
            await funcion.Actualizar(categoria);
            return NoContent();
        }
        
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCategoria(int id)
        {
            var funcion = new CategoriaDAO();
            var categoria = new Categoria();
            categoria.idCategoria = id;
            await funcion.Eliminar(id);
            return NoContent();
        }
    }
}
