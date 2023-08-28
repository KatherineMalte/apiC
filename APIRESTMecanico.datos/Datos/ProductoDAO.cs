using APIRESTMecanico.datos.Conexion;
using APIRESTMecanico.datos.Interface;
using APIRESTMecanico.datos.Modelo;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Data.SqlClient;
using System.Reflection.Emit;

namespace APIRESTMecanico.datos.Datos
{
    public class ProductoDAO : IGenericRepository<Producto>
    {
        Conexiondb cn = new Conexiondb();

        public Task<bool> Eliminar(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Producto>> Listar()
        {
            var lista = new List<Producto>();
            using (var sql = new SqlConnection(cn.cadenaSQL()))
            {
                using (var cmd = new SqlCommand("listarProductos", sql))
                {
                    await sql.OpenAsync();
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (var dr = await cmd.ExecuteReaderAsync())
                    {
                        while (await dr.ReadAsync())
                        {
                            lista.Add(new Producto
                            {
                                idProducto = Convert.ToInt32(dr["idProducto"]),
                                nombreProducto = dr["nombreProducto"].ToString(),
                                precioProducto = dr["precioProducto"].ToString(),
                                stok = Convert.ToInt32(dr["stok"]),
                                Categoria = new Categoria()
                                {
                                    categoria = dr["categoria"].ToString()
                                }
                            });

                        }
                    }
                }
            }
            return lista;
        }
        public async Task<List<Producto>> TraerPorId(int id)
        {
            Producto modelo = new Producto();
            List<Producto> _lista = new List<Producto>();
          /*  using (SqlConnection con = new SqlConnection(cn.cadenaSQL()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("TraerProductoPorId", con);
                cmd.CommandType = CommandType.StoredProcedure; cmd.Parameters.AddWithValue("IdCategoria", id);
                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    while (await dr.ReadAsync())
                    {
                        _lista.Add(new Producto
                        {
                            idProducto = Convert.ToInt32(dr["idProducto"]),
                            nombreProducto = dr["nombreProducto"].ToString(),
                            precioProducto = dr["precioProducto"].ToString(),
                            stok = Convert.ToInt32(dr["stok"]),

                        });
                    }
                }*/
                return _lista;
        }
        public async Task<bool> Insertar(Producto modelo)
        {
            using (var sql = new SqlConnection(cn.cadenaSQL()))
            {
                sql.Open();
                using (var cmd = new SqlCommand("InsertarProducto", sql))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("NombreProducto", modelo.nombreProducto);
                    cmd.Parameters.AddWithValue("PrecioProducto", modelo.precioProducto);
                    cmd.Parameters.AddWithValue("Stok", modelo.stok);
                    cmd.Parameters.AddWithValue("IdCategoria", modelo.Categoria.idCategoria);
                    int filas_afectadas = await cmd.ExecuteNonQueryAsync();

                    if (filas_afectadas > 0)
                        return true;
                    else
                        return false;

                }


            }
        }
        public async Task<bool> Actualizar(Producto modelo)
        {
            using (var sql = new SqlConnection(cn.cadenaSQL()))
            {
                sql.Open();
                using (var cmd = new SqlCommand("ActualizarProducto", sql))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("NombreProducto", modelo.nombreProducto);
                    cmd.Parameters.AddWithValue("PrecioProducto", modelo.precioProducto);
                    cmd.Parameters.AddWithValue("Stok", modelo.stok);
                    cmd.Parameters.AddWithValue("IdCategoria", modelo.Categoria.idCategoria);
                    cmd.Parameters.AddWithValue("idProducto", modelo.idProducto);
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
