using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace CapaDatos
{
    public class CD_Venta
    {
        public int obtenerCorrelativo()
        {
            int idCorrelativo = 0;

            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("select Count(*) + 1 from Venta");

                    SqlCommand cmd = new SqlCommand(query.ToString(), oconexion);
                    cmd.CommandType = CommandType.Text;

                    oconexion.Open();
                    idCorrelativo = Convert.ToInt32(cmd.ExecuteScalar());
                }
                catch (Exception ex)
                {
                    idCorrelativo = 0;
                }
            }

            return idCorrelativo;
        }

        public bool RestarStock(int idProd, int cantidad)
        {
            bool respuesta = true;

            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("update Productos set Stock = Stock - @cantidad where IdProducto = @IdProducto");

                    SqlCommand cmd = new SqlCommand(query.ToString(), oconexion);
                    cmd.Parameters.AddWithValue("@cantidad", cantidad);
                    cmd.Parameters.AddWithValue("@IdProducto", idProd);
                    cmd.CommandType = CommandType.Text;

                    oconexion.Open();
                    respuesta = cmd.ExecuteNonQuery() > 0 ? true : false;
                }
                catch (Exception ex)
                {
                    respuesta = false;
                }
            }

            return respuesta;
        }

        public bool SumarStock(int idProd, int cantidad)
        {
            bool respuesta = true;

            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("update Productos set Stock = Stock + @cantidad where IdProducto = @IdProducto");

                    SqlCommand cmd = new SqlCommand(query.ToString(), oconexion);
                    cmd.Parameters.AddWithValue("@cantidad", cantidad);
                    cmd.Parameters.AddWithValue("@IdProducto", idProd);
                    cmd.CommandType = CommandType.Text;

                    oconexion.Open();
                    respuesta = cmd.ExecuteNonQuery() > 0 ? true : false;
                }
                catch (Exception ex)
                {
                    respuesta = false;
                }
            }

            return respuesta;
        }

        public bool Registrar(Venta obj, DataTable DetalleVenta, out string Mensaje)
        {
            bool Resultado = false;
            Mensaje = string.Empty;
            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("SP_REGISTRARVENTA", oconexion);
                    cmd.Parameters.AddWithValue("IdUsuario", obj.oUsuario.IdUsuario);
                    cmd.Parameters.AddWithValue("IdCliente", (obj.oCliente != null && obj.oCliente.IdCliente > 0) ? (object)obj.oCliente.IdCliente : DBNull.Value);
                    cmd.Parameters.AddWithValue("TipoDocumento", obj.TipoDocumento);
                    cmd.Parameters.AddWithValue("NumeroDocumento", obj.NumeroDocumento);
                    cmd.Parameters.AddWithValue("FormaPago", string.IsNullOrWhiteSpace(obj.FormaPago) ? "Efectivo" : obj.FormaPago);
                    cmd.Parameters.AddWithValue("MontoPago", obj.MontoPago);
                    cmd.Parameters.AddWithValue("MontoCambio", obj.MontoCambio);
                    cmd.Parameters.AddWithValue("MontoTotal", obj.MontoTotal);
                    cmd.Parameters.AddWithValue("DetalleVenta", DetalleVenta);
                    cmd.Parameters.Add("Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oconexion.Open();
                    cmd.ExecuteNonQuery();
                    Resultado = Convert.ToBoolean(cmd.Parameters["Resultado"].Value);
                    Mensaje = cmd.Parameters["Mensaje"].Value.ToString();
                }
                catch (Exception ex)
                {
                    Resultado = false;
                    Mensaje = ex.Message;
                }
            }
            return Resultado;
        }

        public List<Venta> ListarVentas(DateTime fechaInicio, DateTime fechaFin, string numero = "")
        {
            List<Venta> lista = new List<Venta>();
            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    oconexion.Open();
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("SELECT v.IdVenta, u.NombreCompleto, u.Documento,");
                    query.AppendLine("v.IdCliente, ISNULL(c.Documento, '') AS DocumentoCliente, ISNULL(c.NombreCompleto, 'Consumidor Final') AS NombreCliente,");
                    query.AppendLine("v.TipoDocumento, ISNULL(v.FormaPago, 'Efectivo') AS FormaPago, v.NumeroDocumento,");
                    query.AppendLine("v.MontoPago, v.MontoCambio, v.MontoTotal,");
                    query.AppendLine("CONVERT(char(10), v.FechaRegistro, 103) AS FechaRegistro");
                    query.AppendLine("FROM Venta v");
                    query.AppendLine("INNER JOIN Usuario u ON u.IdUsuario = v.IdUsuario");
                    query.AppendLine("LEFT JOIN Cliente c ON c.IdCliente = v.IdCliente");
                    query.AppendLine("WHERE CAST(v.FechaRegistro AS DATE) BETWEEN CAST(@fechaInicio AS DATE) AND CAST(@fechaFin AS DATE)");
                    if (!string.IsNullOrWhiteSpace(numero))
                    {
                        query.AppendLine("AND v.NumeroDocumento LIKE '%' + @numero + '%'");
                    }
                    query.AppendLine("ORDER BY v.FechaRegistro DESC, v.IdVenta DESC");

                    SqlCommand cmd = new SqlCommand(query.ToString(), oconexion);
                    cmd.Parameters.AddWithValue("@fechaInicio", fechaInicio.Date);
                    cmd.Parameters.AddWithValue("@fechaFin", fechaFin.Date);
                    if (!string.IsNullOrWhiteSpace(numero))
                    {
                        cmd.Parameters.AddWithValue("@numero", numero.Trim());
                    }
                    cmd.CommandType = CommandType.Text;

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new Venta()
                            {
                                IdVenta = Convert.ToInt32(dr["IdVenta"]),
                                oUsuario = new Usuario { NombreCompleto = dr["NombreCompleto"].ToString(), Documento = dr["Documento"].ToString() },
                                oCliente = new Cliente
                                {
                                    IdCliente = dr["IdCliente"] != DBNull.Value ? Convert.ToInt32(dr["IdCliente"]) : 0,
                                    Documento = dr["DocumentoCliente"].ToString(),
                                    NombreCompleto = dr["NombreCliente"].ToString()
                                },
                                DocumentoCliente = dr["DocumentoCliente"].ToString(),
                                NombreCliente = dr["NombreCliente"].ToString(),
                                TipoDocumento = dr["TipoDocumento"].ToString(),
                                FormaPago = dr["FormaPago"].ToString(),
                                NumeroDocumento = dr["NumeroDocumento"].ToString(),
                                MontoPago = Convert.ToDecimal(dr["MontoPago"]),
                                MontoCambio = Convert.ToDecimal(dr["MontoCambio"]),
                                MontoTotal = Convert.ToDecimal(dr["MontoTotal"]),
                                FechaRegistro = dr["FechaRegistro"].ToString()
                            });
                        }
                    }
                }
                catch (Exception ex)
                {
                    lista = new List<Venta>();
                }
            }
            return lista;
        }

        public Venta obtenerVenta(string numero)
        {
            Venta obj = new Venta();
            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    oconexion.Open();
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("select v.IdVenta,u.NombreCompleto,u.Documento,");
                    query.AppendLine("v.IdCliente,ISNULL(c.Documento, '') AS DocumentoCliente,ISNULL(c.NombreCompleto, 'Consumidor Final') AS NombreCliente,");
                    query.AppendLine("v.TipoDocumento,ISNULL(v.FormaPago, 'Efectivo') AS FormaPago,v.NumeroDocumento,");
                    query.AppendLine("v.MontoPago,v.MontoCambio,v.MontoTotal,");
                    query.AppendLine("convert(char(10), v.FechaRegistro, 103)[FechaRegistro]");
                    query.AppendLine("from Venta v");
                    query.AppendLine("inner join Usuario u on u.IdUsuario = v.IdUsuario");
                    query.AppendLine("left join Cliente c on c.IdCliente = v.IdCliente");
                    query.AppendLine("where v.NumeroDocumento = @numero");
                    SqlCommand cmd = new SqlCommand(query.ToString(), oconexion);
                    cmd.Parameters.AddWithValue("@numero", numero);
                    cmd.CommandType = CommandType.Text;

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            obj = new Venta()
                            {
                                IdVenta = Convert.ToInt32(dr["IdVenta"]),
                                oUsuario = new Usuario { NombreCompleto = dr["NombreCompleto"].ToString(), Documento = dr["Documento"].ToString() },
                                oCliente = new Cliente
                                {
                                    IdCliente = dr["IdCliente"] != DBNull.Value ? Convert.ToInt32(dr["IdCliente"]) : 0,
                                    Documento = dr["DocumentoCliente"].ToString(),
                                    NombreCompleto = dr["NombreCliente"].ToString()
                                },
                                DocumentoCliente = dr["DocumentoCliente"].ToString(),
                                NombreCliente = dr["NombreCliente"].ToString(),
                                TipoDocumento = dr["TipoDocumento"].ToString(),
                                FormaPago = dr["FormaPago"].ToString(),
                                NumeroDocumento = dr["NumeroDocumento"].ToString(),
                                MontoPago = Convert.ToDecimal(dr["MontoPago"].ToString()),
                                MontoCambio = Convert.ToDecimal(dr["MontoCambio"].ToString()),
                                MontoTotal = Convert.ToDecimal(dr["MontoTotal"].ToString()),
                                FechaRegistro = dr["FechaRegistro"].ToString()
                            };
                        }
                    }
                }
                catch (Exception ex)
                {
                    obj = new Venta();
                }
            }

            return obj;
        }

        public List<DetalleVenta> obtenerDetalleVenta(int idVenta)
        {
            List<DetalleVenta> oLista = new List<DetalleVenta>();

            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    oconexion.Open();
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("select p.Codigo, p.Nombre, p.Descripcion, ISNULL(ca.Descripcion,'') AS Categoria, dv.PrecioVenta, dv.Cantidad, dv.SubTotal");
                    query.AppendLine("from Detalle_Venta dv");
                    query.AppendLine("inner join Productos p on p.IdProducto = dv.IdProducto");
                    query.AppendLine("left join Categorias ca on ca.IdCategoria = p.IdCategoria");
                    query.AppendLine("where dv.IdVenta = @IdVenta");

                    SqlCommand cmd = new SqlCommand(query.ToString(), oconexion);
                    cmd.Parameters.AddWithValue("@IdVenta", idVenta);
                    cmd.CommandType = CommandType.Text;

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            oLista.Add(new DetalleVenta()
                            {
                                oProducto = new Producto()
                                {
                                    Codigo = dr["Codigo"].ToString(),
                                    Nombre = dr["Nombre"].ToString(),
                                    Descripcion = dr["Descripcion"].ToString(),
                                    oCategoria = new Categoria() { Descripcion = dr["Categoria"].ToString() }
                                },
                                PrecioVenta = Convert.ToDecimal(dr["PrecioVenta"].ToString()),
                                Cantidad = Convert.ToInt32(dr["Cantidad"].ToString()),
                                SubTotal = Convert.ToDecimal(dr["SubTotal"].ToString())
                            });
                        }
                    }
                }
                catch (Exception ex)
                {
                    oLista = new List<DetalleVenta>();
                }
            }

            return oLista;
        }
    }
}
