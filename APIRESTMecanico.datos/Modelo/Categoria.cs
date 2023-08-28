using Microsoft.Extensions.Hosting;

namespace APIRESTMecanico.datos.Modelo
{
    public class Categoria
    {
        public int idCategoria { get; set; }
        public string categoria { get; set; }
        public string codigoCategoria { get; set; }
        public string nombre { get; set; }
        public string observacion { get; set; }
    }
}
