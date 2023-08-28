namespace APIRESTMecanico.datos.Modelo
{
    public class Producto
    {
        public int idProducto { get; set; }
        public string nombreProducto { get; set; }
        public string precioProducto { get; set; }
        public int stok { get; set; }
        public int idCategoria { get; set; }
        public virtual Categoria Categoria { get; set; }

    }
}
