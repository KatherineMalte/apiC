namespace APIRESTMecanico.datos.Conexion
{
    public class Conexiondb
    {
        private string connectionString = string.Empty;
        public Conexiondb(){
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
            connectionString = builder.GetSection("ConnectionStrings:cadenaSQL").Value;
}
        public string cadenaSQL() {
            return connectionString;
        }
    }
}
