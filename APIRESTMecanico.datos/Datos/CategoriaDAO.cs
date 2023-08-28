using APIRESTMecanico.datos.Conexion;
using APIRESTMecanico.datos.Interface;
using APIRESTMecanico.datos.Modelo;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using System.Reflection.Metadata.Ecma335;
using System.Security.AccessControl;

namespace APIRESTMecanico.datos.Datos
{
    public class CategoriaDAO : IGenericRepository<Categoria>
    {
        Conexiondb cn = new Conexiondb();
   
        public async Task<List<Categoria>> Listar()
        {
            var lista = new List<Categoria>();
            using (var sql = new SqlConnection(cn.cadenaSQL()))
            {
                using (var cmd = new SqlCommand("ListarCategorias", sql))
                {
                    await sql.OpenAsync();
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (var item = await cmd.ExecuteReaderAsync())
                    {
                        while (await item.ReadAsync())
                        {
                            var categoria = new Categoria();
                            categoria.idCategoria = (int)item["IdCategoria"];
                            categoria.categoria = (string)item["categoria"];
                            categoria.codigoCategoria = (string)item["codigoCategoria"];
                            categoria.nombre = (string)item["nombre"];
                            categoria.observacion = (string)item["observacion"];
                            lista
                                .Add(categoria);

                        }
                    }
                }
            }
            return lista;
        }
        
        public async Task<List<Categoria>> TraerPorId(int id)
        {
            List<Categoria> _lista = new List<Categoria>();
            using (SqlConnection con = new SqlConnection(cn.cadenaSQL()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("TraerCategoriaPorId", con);
                cmd.CommandType = CommandType.StoredProcedure; cmd.Parameters.AddWithValue("IdCategoria", id);
                using (var item = await cmd.ExecuteReaderAsync())
                {
                    while (await item.ReadAsync())
                    {
                        _lista.Add(new Categoria
                        {
                        categoria = (string)item["categoria"],
                        codigoCategoria = (string)item["codigoCategoria"],
                        nombre = (string)item["nombre"],
                        observacion = (string)item["observacion"]

                    });
                    }
                }
                return _lista;
            }
        }

        public async Task<bool> Insertar(Categoria modelo)
        {
            using (var sql = new SqlConnection(cn.cadenaSQL()))
            {
                sql.Open();
                using (var cmd = new SqlCommand("InsertarCategoria", sql))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("categoria", modelo.categoria);
                    cmd.Parameters.AddWithValue("nombre", modelo.nombre);
                    cmd.Parameters.AddWithValue("observacion", modelo.observacion);
                    int filas_afectadas = await cmd.ExecuteNonQueryAsync();

                    if (filas_afectadas > 0)
                        return true;
                    else
                        return false;

                } 
              

            }
        }


        public async Task<bool> Actualizar(Categoria modelo)
        {
            using(var sql = new SqlConnection(cn.cadenaSQL()))
            {
                sql.Open();
                using (var cmd = new SqlCommand("ActualizarCategoria",sql))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("categoria", modelo.categoria);
                    cmd.Parameters.AddWithValue("nombre", modelo.nombre);
                    cmd.Parameters.AddWithValue("observacion", modelo.observacion);
                    cmd.Parameters.AddWithValue("idCategoria", modelo.idCategoria);
                    int filas_afectadas = await cmd.ExecuteNonQueryAsync();

                    if (filas_afectadas > 0)
                        return true;
                    else
                        return false;
                }
            }
        }

        public async Task<bool> Eliminar(int  id)
        {
            using (var sql = new SqlConnection(cn.cadenaSQL()))
            {
                sql.Open();
                using (var cmd = new SqlCommand("EliminarCategoria", sql))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("idCategoria",id);
                    int filas_afectadas = await cmd.ExecuteNonQueryAsync();

                    if (filas_afectadas > 0)
                        return true;
                    else
                        return false;
                }
            }
        }

    }
}
