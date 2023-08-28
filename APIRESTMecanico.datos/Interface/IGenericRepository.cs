namespace APIRESTMecanico.datos.Interface
{
    public interface IGenericRepository<T> where T : class
    {
        Task<List<T>> Listar();
        Task<bool> Insertar(T modelo);
        Task<List<T>> TraerPorId(int id);
        Task<bool> Actualizar(T modelo);
        Task<bool> Eliminar(int id);
    }
}
