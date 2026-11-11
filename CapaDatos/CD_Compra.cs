using CapaEntidad;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace CapaDatos
{
    public class CD_Compra
    {
        public int obtenerCorrelativo()
        {
            int idCorrelativo = 0;

            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("select Count(*) + 1 from Compra");

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

        public bool Registrar(Compra obj, DataTable DetalleCompra, out string Mensaje)
        {
            bool Resultado = false;
            Mensaje = string.Empty;
            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("SP_REGISTRARCOMPRA", oconexion);
                    cmd.Parameters.AddWithValue("IdUsuario", obj.oUsuario.IdUsuario);
                    cmd.Parameters.AddWithValue("IdProveedor", obj.oProveedor.IdProveedor);
                    cmd.Parameters.AddWithValue("TipoDocumento", obj.TipoDocumento);
                    cmd.Parameters.AddWithValue("NumeroDocumento", obj.NumeroDocumento);
                    cmd.Parameters.AddWithValue("MontoTotal", obj.MontoTotal);
                    cmd.Parameters.AddWithValue("DetalleCompra", DetalleCompra);
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

        public List<Compra> ListarCompras(DateTime fechaInicio, DateTime fechaFin, int idProveedor = 0, string numero = "")
        {
            List<Compra> lista = new List<Compra>();
            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    oconexion.Open();
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("SELECT c.IdCompra, LTRIM(RTRIM(ISNULL(u.Nombre, '') + ' ' + ISNULL(u.Apellido, ''))) AS NombreCompleto, u.Documento,");
                    query.AppendLine("pr.IdProveedor, pr.Documento AS DocumentoProveedor, pr.RazonSocial, pr.Correo,");
                    query.AppendLine("c.TipoDocumento, c.Estado, c.NumeroDocumento, c.MontoTotal,");
                    query.AppendLine("CONVERT(char(10), c.FechaRegistro, 103) AS FechaRegistro");
                    query.AppendLine("FROM Compra c");
                    query.AppendLine("INNER JOIN Usuario u ON u.IdUsuario = c.IdUsuario");
                    query.AppendLine("INNER JOIN Proveedor pr ON pr.IdProveedor = c.IdProveedor");
                    query.AppendLine("WHERE CAST(c.FechaRegistro AS DATE) BETWEEN CAST(@fechaInicio AS DATE) AND CAST(@fechaFin AS DATE)");
                    if (idProveedor > 0)
                    {
                        query.AppendLine("AND c.IdProveedor = @idProveedor");
                    }
                    if (!string.IsNullOrWhiteSpace(numero))
                    {
                        query.AppendLine("AND c.NumeroDocumento LIKE '%' + @numero + '%'");
                    }
                    query.AppendLine("ORDER BY c.FechaRegistro DESC, c.IdCompra DESC");

                    SqlCommand cmd = new SqlCommand(query.ToString(), oconexion);
                    cmd.Parameters.AddWithValue("@fechaInicio", fechaInicio.Date);
                    cmd.Parameters.AddWithValue("@fechaFin", fechaFin.Date);
                    if (idProveedor > 0)
                    {
                        cmd.Parameters.AddWithValue("@idProveedor", idProveedor);
                    }
                    if (!string.IsNullOrWhiteSpace(numero))
                    {
                        cmd.Parameters.AddWithValue("@numero", numero.Trim());
                    }
                    cmd.CommandType = CommandType.Text;

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new Compra()
                            {
                                IdCompra = Convert.ToInt32(dr["IdCompra"]),
                                oUsuario = new Usuario { NombreCompleto = dr["NombreCompleto"].ToString(), Documento = dr["Documento"].ToString() },
                                oProveedor = new Proveedor
                                {
                                    IdProveedor = Convert.ToInt32(dr["IdProveedor"]),
                                    Documento = dr["DocumentoProveedor"].ToString(),
                                    RazonSocial = dr["RazonSocial"].ToString(),
                                    Correo = dr["Correo"].ToString()
                                },
                                TipoDocumento = dr["TipoDocumento"].ToString(),
                                NumeroDocumento = dr["NumeroDocumento"].ToString(),
                                MontoTotal = Convert.ToDecimal(dr["MontoTotal"]),
                                FechaRegistro = dr["FechaRegistro"].ToString(),
                                Estado = Convert.ToBoolean(dr["Estado"])
                            });
                        }
                    }
                }
                catch (Exception ex)
                {
                    lista = new List<Compra>();
                }
            }
            return lista;
        }

        public Compra obtenerCompra(string numero)
        {
            Compra obj = new Compra();
            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("select c.IdCompra,");
                    query.AppendLine("LTRIM(RTRIM(ISNULL(u.Nombre, '') + ' ' + ISNULL(u.Apellido, ''))) AS NombreCompleto,u.Documento,");
                    query.AppendLine("pr.Documento[DocumentoProveedor],pr.RazonSocial,pr.Correo,");
                    query.AppendLine("c.TipoDocumento,c.Estado,c.NumeroDocumento,c.MontoTotal,convert(char(10), c.FechaRegistro, 103)[FechaRegistro]");
                    query.AppendLine("from Compra c");
                    query.AppendLine("inner join Usuario u on u.IdUsuario = c.IdUsuario");
                    query.AppendLine("inner join Proveedor pr on pr.IdProveedor = c.IdProveedor");
                    query.AppendLine("where c.NumeroDocumento = @numero");
                    SqlCommand cmd = new SqlCommand(query.ToString(), oconexion);
                    cmd.Parameters.AddWithValue("@numero", numero);
                    cmd.CommandType = CommandType.Text;

                    oconexion.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            obj = new Compra()
                            {
                                IdCompra = Convert.ToInt32(dr["IdCompra"]),
                                oUsuario = new Usuario { NombreCompleto = dr["NombreCompleto"].ToString(), Documento = dr["Documento"].ToString() },
                                oProveedor = new Proveedor { Documento = dr["DocumentoProveedor"].ToString(), RazonSocial = dr["RazonSocial"].ToString(), Correo = dr["Correo"].ToString() },
                                TipoDocumento = dr["TipoDocumento"].ToString(),
                                NumeroDocumento = dr["NumeroDocumento"].ToString(),
                                MontoTotal = Convert.ToDecimal(dr["MontoTotal"].ToString()),
                                FechaRegistro = dr["FechaRegistro"].ToString(),
                                Estado = Convert.ToBoolean(dr["Estado"])
                            };
                        }
                    }
                }
                catch (Exception ex)
                {
                    obj = new Compra();
                }
            }

            return obj;
        }

        public List<DetalleCompra> obtenerDetalleCompra(int idCompra)
        {
            List<DetalleCompra> oLista = new List<DetalleCompra>();
            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
                {
                    oconexion.Open();
                    StringBuilder query = new StringBuilder();

                    query.AppendLine("select p.Codigo, p.Nombre, p.Descripcion, ISNULL(ca.Descripcion,'') AS Categoria, dc.PrecioCompra, dc.PrecioVenta, dc.Cantidad, dc.Total");
                    query.AppendLine("from Detalle_Compra dc");
                    query.AppendLine("inner join Productos p on p.IdProducto = dc.IdProducto");
                    query.AppendLine("left join Categorias ca on ca.IdCategoria = p.IdCategoria");
                    query.AppendLine("where dc.IdCompra = @IdCompra");

                    SqlCommand cmd = new SqlCommand(query.ToString(), oconexion);
                    cmd.Parameters.AddWithValue("@IdCompra", idCompra);
                    cmd.CommandType = CommandType.Text;

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            oLista.Add(new DetalleCompra()
                            {
                                oProducto = new Producto()
                                {
                                    Codigo = dr["Codigo"].ToString(),
                                    Nombre = dr["Nombre"].ToString(),
                                    Descripcion = dr["Descripcion"].ToString(),
                                    oCategoria = new Categoria() { Descripcion = dr["Categoria"].ToString() }
                                },
                                PrecioCompra = Convert.ToDecimal(dr["PrecioCompra"].ToString()),
                                PrecioVenta = Convert.ToDecimal(dr["PrecioVenta"].ToString()),
                                Cantidad = Convert.ToInt32(dr["Cantidad"].ToString()),
                                MontoTotal = Convert.ToDecimal(dr["Total"].ToString())
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                oLista = new List<DetalleCompra>();
            }

            return oLista;
        }

        public bool ValidarCompra(int idCompra, out string Mensaje)
        {
            bool Resultado = false;
            Mensaje = string.Empty;

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
                {
                    SqlCommand cmd = new SqlCommand("SP_VALIDARCOMPRA", oconexion);
                    cmd.Parameters.AddWithValue("IdCompra", idCompra);
                    cmd.Parameters.Add("Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oconexion.Open();
                    cmd.ExecuteNonQuery();
                    Resultado = Convert.ToBoolean(cmd.Parameters["Resultado"].Value);
                    Mensaje = cmd.Parameters["Mensaje"].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                Resultado = false;
                Mensaje = ex.Message;
            }

            return Resultado;
        }
    }
}
