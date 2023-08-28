using APIRESTMecanico.datos.Conexion;
using System.Data.SqlClient;
using System.Data;
using System;
using APIRESTMecanico.datos.Modelo;
using APIRESTMecanico.datos.Interface;

namespace APIRESTMecanico.datos.Datos
{
    public class ClienteDAO : IGenericRepository<Cliente>
    {
        Conexiondb cn = new Conexiondb();
        public async Task<List<Cliente>> Listar()
        {
            var lista = new List<Cliente>();
            using (var sql = new SqlConnection(cn.cadenaSQL()))
            {
                using (var cmd = new SqlCommand("ListarCliente", sql))
                {
                    await sql.OpenAsync();
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (var item = await cmd.ExecuteReaderAsync())
                    {
                        while (await item.ReadAsync())
                        {
                            var cliente = new Cliente();
                            cliente.idCliente = (int)item["IdCliente"];
                            cliente.nombreCliente = (string)item["NombreCliente"];
                            cliente.apellidoCliente = (string)item["ApellidoCliente"];
                            cliente.direccionCliente = (string)item["DireccionCliente"];
                            cliente.paisCliente = (string)item["PaisCliente"];
                            cliente.fechaNacimiento = (DateTime)item["FechaNacimiento"];
                            cliente.telefonoCliente = (int)item["TelefonoCliente"];
                            cliente.emailCliente = (string)item["NombreCliente"];
                            cliente.numeroID = (int)item["NumeroID"];
                            lista.Add(cliente);

                        }
                    }
                }
            }
            return lista;
        }

        public async Task<List<Cliente>> TraerPorId(int id)
        {
            List<Cliente> _lista = new List<Cliente>();
            using (SqlConnection con = new SqlConnection(cn.cadenaSQL()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("TraerClientePorId", con);
                cmd.CommandType = CommandType.StoredProcedure; cmd.Parameters.AddWithValue("IdCliente", id);
                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    while (await dr.ReadAsync())
                    {
                        _lista.Add(new Cliente
                        {
                            nombreCliente = dr["NombreCliente"].ToString(),
                            apellidoCliente = dr["ApellidoCliente"].ToString(),
                            direccionCliente = dr["DireccionCliente"].ToString(),
                            paisCliente = dr["PaisCliente"].ToString(),
                            fechaNacimiento = Convert.ToDateTime(dr["FechaNacimiento"]),
                            telefonoCliente = Convert.ToInt32(dr["TelefonoCliente"]),
                            emailCliente = dr["EmailCliente"].ToString(),
                            numeroID = Convert.ToInt32(dr["NumeroID"])
                        });
                    }
                }
                return _lista;
            }
        }
        public async Task Insertar(Cliente modelo)
        {
            using (var sql = new SqlConnection(cn.cadenaSQL()))
            {
                using (var cmd = new SqlCommand("InsertarCliente", sql))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("NumeroID", modelo.numeroID);
                    cmd.Parameters.AddWithValue("NombreCliente", modelo.nombreCliente);
                    cmd.Parameters.AddWithValue("ApellidoCliente", modelo.apellidoCliente);
                    cmd.Parameters.AddWithValue("DireccionCliente", modelo.direccionCliente);
                    cmd.Parameters.AddWithValue("PaisCliente", modelo.paisCliente);
                    cmd.Parameters.AddWithValue("FechaNacimiento", modelo.fechaNacimiento);
                    cmd.Parameters.AddWithValue("TelefonoCliente", modelo.telefonoCliente);
                    cmd.Parameters.AddWithValue("EmailCliente", modelo.emailCliente);
                    await sql.OpenAsync();
                    await cmd.ExecuteReaderAsync();
                }
            }
        }

        public async Task Actualizar(Cliente modelo)
        {
            using (var sql = new SqlConnection(cn.cadenaSQL()))
            {
                using (var cmd = new SqlCommand("ActualizarCliente", sql))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("id", modelo.idCliente);
                    cmd.Parameters.AddWithValue("NumeroID", modelo.numeroID);
                    cmd.Parameters.AddWithValue("NombreCliente", modelo.nombreCliente);
                    cmd.Parameters.AddWithValue("ApellidoCliente", modelo.apellidoCliente);
                    cmd.Parameters.AddWithValue("DireccionCliente", modelo.direccionCliente);
                    cmd.Parameters.AddWithValue("PaisCliente", modelo.paisCliente);
                    cmd.Parameters.AddWithValue("FechaNacimiento", modelo.fechaNacimiento);
                    cmd.Parameters.AddWithValue("TelefonoCliente", modelo.telefonoCliente);
                    cmd.Parameters.AddWithValue("EmailCliente", modelo.emailCliente);
                    await sql.OpenAsync();
                    await cmd.ExecuteReaderAsync();
                }
            }
        }

        public async Task Eliminar(Cliente modelo)
        {
            using (var sql = new SqlConnection(cn.cadenaSQL()))
            {
                using (var cmd = new SqlCommand("EliminarCliente", sql))
                {
                    cmd.CommandType = CommandType.StoredProcedure; ;
                    cmd.Parameters.AddWithValue("idCliente", modelo.idCliente);
                    await sql.OpenAsync();
                    await cmd.ExecuteReaderAsync();
                }
            }
        }



        public Task<bool> Eliminar(int id)
        {
            throw new NotImplementedException();
        }

        Task<bool> IGenericRepository<Cliente>.Insertar(Cliente modelo)
        {
            throw new NotImplementedException();
        }

        Task<bool> IGenericRepository<Cliente>.Actualizar(Cliente modelo)
        {
            throw new NotImplementedException();
        }
    }
}
