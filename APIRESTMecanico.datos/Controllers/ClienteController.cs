using APIRESTMecanico.datos.Datos;
using APIRESTMecanico.datos.Modelo;
using Microsoft.AspNetCore.Mvc;

namespace APIRESTMecanico.datos.Controllers
{
    [ApiController]
    [Route("api/cliente")]
    public class ClienteController : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<List<Cliente>>> GetCliente()
        {
            var funcion = new ClienteDAO();
            var lista = await funcion.Listar();
            return lista;
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<List<Cliente>>> GetIdCliente(int id)
        { var Cliente = new Cliente();
            var funcion = new ClienteDAO();
            List<Cliente> _listar =  await funcion.TraerPorId(id);
            return StatusCode(StatusCodes.Status200OK, _listar);
            
        }
        [HttpPost]
        public async Task<ActionResult> PostCliente([FromBody] Cliente Cliente)
        {
            var funcion = new ClienteDAO();
            await funcion.Insertar(Cliente);
            return NoContent
                ();
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> PutCliente(int id, [FromBody] Cliente Cliente)
        {
            var funcion = new ClienteDAO();
            Cliente.idCliente = id;
            await funcion.Actualizar(Cliente);
            return NoContent
                ();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCliente(int id)
        {
            var funcion = new ClienteDAO();
            var Cliente = new Cliente();
            Cliente.idCliente = id;
            await funcion.Eliminar(Cliente);
            return NoContent
                ();
        }
    }
}
