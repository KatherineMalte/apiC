namespace APIRESTMecanico.datos.Modelo
{
    public class Factura
    {
        public int idFactura { get; set; }
        public string numeroDocumento { get; set; }
        public string numeroFactura { get; set; }
        public Cliente idCliente { get; set; }
        public string fechaFactura { get; set; }
        public ModoPago idModoPago { get; set; }
    }
}
