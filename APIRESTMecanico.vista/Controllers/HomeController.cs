using APIRESTMecanico.datos.Modelo;
using APIRESTMecanico.datos.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using APIRESTMecanico.vista.Models;

namespace APIRESTMecanico.vista.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IGenericRepository<Categoria> _categoriaRepository;
        private readonly IGenericRepository<Producto> _productoRepository;
        private readonly IGenericRepository<Cliente> _clienteRepository;

        public HomeController(ILogger<HomeController> logger,
            IGenericRepository<Categoria> categoriaRepository,
            IGenericRepository<Producto> productoRepository,
            IGenericRepository<Cliente> clienteRepository)
        {
            _logger = logger;
            _categoriaRepository = categoriaRepository;
            _productoRepository = productoRepository;
            _clienteRepository = clienteRepository;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Categoria()
        {
            return View();
        }
        public IActionResult Producto()
        {
            return View();
        }
        public IActionResult Cliente()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetCategoria()
        {
            List<Categoria> _listar = await _categoriaRepository.Listar();
            return StatusCode(StatusCodes.Status200OK, _listar);
        }
        [HttpPost]
        public async Task<IActionResult> insertarCategoria([FromBody] Categoria categoria)
        {
            bool _resultado = await _categoriaRepository.Insertar(categoria);

            if (_resultado)
                return StatusCode(StatusCodes.Status200OK, new { valor = _resultado, msg = "ok" });
            else
                return StatusCode(StatusCodes.Status500InternalServerError, new { valor = _resultado, msg = "error" });
        }
        [HttpPut]
        public async Task<IActionResult> actualizarCategoria([FromBody] Categoria categoria)
        {
            bool _resultado = await _categoriaRepository.Actualizar(categoria);

            if (_resultado)
                return StatusCode(StatusCodes.Status200OK, new { valor = _resultado, msg = "ok" });
            else
                return StatusCode(StatusCodes.Status500InternalServerError, new { valor = _resultado, msg = "error" });
        }
        [HttpGet]
        public async Task<IActionResult> traerCategoriaPorId(int idCliente)
        {
            List<Categoria> _listar = await _categoriaRepository.TraerPorId(idCliente);
            return StatusCode(StatusCodes.Status200OK, _listar);

        }

        [HttpGet]
        public async Task<IActionResult> listarProducto()
        {
            List<Producto> _listar = await _productoRepository.Listar();
            return StatusCode(StatusCodes.Status200OK, _listar);
        }
        [HttpPost]
        public async Task<IActionResult> guardarProducto([FromBody] Producto producto)
        {
            bool _resultado = await _productoRepository.Insertar(producto);

            if (_resultado)
                return StatusCode(StatusCodes.Status200OK, new { valor = _resultado, msg = "ok" });
            else
                return StatusCode(StatusCodes.Status500InternalServerError, new { valor = _resultado, msg = "error" });
        }
        [HttpPut]
        public async Task<IActionResult> editarProducto([FromBody] Producto producto)
        {
            bool _resultado = await _productoRepository.Actualizar(producto);

            if (_resultado)
                return StatusCode(StatusCodes.Status200OK, new { valor = _resultado, msg = "ok" });
            else
                return StatusCode(StatusCodes.Status500InternalServerError, new { valor = _resultado, msg = "error" });
        }

        [HttpGet]
        public async Task<IActionResult> listarCliente()
        {
            List<Cliente> _listar = await _clienteRepository.Listar();
            return StatusCode(StatusCodes.Status200OK, _listar);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}