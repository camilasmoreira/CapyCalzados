using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace CapaDatos
{
    public class CD_Reporte
    {

        public List<ReporteCompra> Compra(DateTime fechainicio, DateTime fechafin, int idproveedor)
        {
            List<ReporteCompra> lista = new List<ReporteCompra>();

            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    StringBuilder query = new StringBuilder();
                    SqlCommand cmd = new SqlCommand("SP_REPORTECOMPRAS", oconexion);
                    cmd.Parameters.AddWithValue("fechainicio", fechainicio);
                    cmd.Parameters.AddWithValue("fechafin", fechafin);
                    cmd.Parameters.AddWithValue("idproveedor", idproveedor);
                    cmd.CommandType = CommandType.StoredProcedure;

                    oconexion.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new ReporteCompra()
                            {
                                FechaRegistro = dr["FechaRegistro"].ToString(),
                                TipoDocumento = dr["TipoDocumento"].ToString(),
                                NumeroDocumento = dr["NumeroDocumento"].ToString(),
                                MontoTotal = dr["MontoTotal"].ToString(),
                                UsuarioRegistro = dr["UsuarioRegistro"].ToString(),
                                DocumentoProveedor = dr["DocumentoProveedor"].ToString(),
                                RazonSocial = dr["RazonSocial"].ToString(),
                                CodigoProducto = dr["CodigoProducto"].ToString(),
                                NombreProducto = dr["NombreProducto"].ToString(),
                                Categoria = dr["Categoria"].ToString(),
                                PrecioCompra = dr["PrecioCompra"].ToString(),
                                PrecioVenta = dr["PrecioVenta"].ToString(),
                                Cantidad = dr["Cantidad"].ToString(),
                                SubTotal = dr["SubTotal"].ToString()
                            });
                        }
                    }

                }
                catch (Exception ex)
                {
                    lista = new List<ReporteCompra>();
                }
            }
            return lista;
        }

        public List<ReporteVenta> Venta(DateTime fechainicio, DateTime fechafin, int idUsuario = 0)
        {
            List<ReporteVenta> lista = new List<ReporteVenta>();

            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("SELECT CONVERT(CHAR(10), v.FechaRegistro, 103) AS [FechaRegistro], v.TipoDocumento, v.NumeroDocumento,");
                    query.AppendLine("ISNULL(v.FormaPago, 'Efectivo') AS [FormaPago], v.MontoTotal,");
                    query.AppendLine("LTRIM(RTRIM(ISNULL(u.Nombre, '') + ' ' + ISNULL(u.Apellido, ''))) AS [UsuarioRegistro],");
                    query.AppendLine("ISNULL(c.Documento, '') AS [DocumentoCliente],");
                    query.AppendLine("ISNULL(NULLIF(LTRIM(RTRIM(ISNULL(c.Nombre, '') + ' ' + ISNULL(c.Apellido, ''))), ''), 'Consumidor Final') AS [NombreCliente],");
                    query.AppendLine("p.Codigo AS [CodigoProducto], p.Nombre AS [NombreProducto], ca.Descripcion AS [Categoria],");
                    query.AppendLine("dv.PrecioVenta, dv.Cantidad, dv.SubTotal");
                    query.AppendLine("FROM Venta v");
                    query.AppendLine("INNER JOIN Usuario u ON u.IdUsuario = v.IdUsuario");
                    query.AppendLine("LEFT JOIN Cliente c ON c.IdCliente = v.IdCliente");
                    query.AppendLine("INNER JOIN Detalle_Venta dv ON dv.IdVenta = v.IdVenta");
                    query.AppendLine("INNER JOIN Productos p ON p.IdProducto = dv.IdProducto");
                    query.AppendLine("INNER JOIN Categorias ca ON ca.IdCategoria = p.IdCategoria");
                    query.AppendLine("WHERE CONVERT(DATE, v.FechaRegistro) BETWEEN CAST(@fechainicio AS DATE) AND CAST(@fechafin AS DATE)");
                    if (idUsuario > 0)
                    {
                        query.AppendLine("AND v.IdUsuario = @idUsuario");
                    }

                    SqlCommand cmd = new SqlCommand(query.ToString(), oconexion);
                    cmd.Parameters.AddWithValue("@fechainicio", fechainicio.Date);
                    cmd.Parameters.AddWithValue("@fechafin", fechafin.Date);
                    if (idUsuario > 0)
                    {
                        cmd.Parameters.AddWithValue("@idUsuario", idUsuario);
                    }
                    cmd.CommandType = CommandType.Text;

                    oconexion.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new ReporteVenta()
                            {
                                FechaRegistro = dr["FechaRegistro"].ToString(),
                                TipoDocumento = dr["TipoDocumento"].ToString(),
                                FormaPago = HasColumn(dr, "FormaPago") ? dr["FormaPago"].ToString() : "Efectivo",
                                NumeroDocumento = dr["NumeroDocumento"].ToString(),
                                MontoTotal = dr["MontoTotal"].ToString(),
                                UsuarioRegistro = dr["UsuarioRegistro"].ToString(),
                                DocumentoCliente = HasColumn(dr, "DocumentoCliente") ? dr["DocumentoCliente"].ToString() : "",
                                NombreCliente = HasColumn(dr, "NombreCliente") ? dr["NombreCliente"].ToString() : "",
                                CodigoProducto = dr["CodigoProducto"].ToString(),
                                NombreProducto = dr["NombreProducto"].ToString(),
                                Categoria = dr["Categoria"].ToString(),
                                PrecioVenta = dr["PrecioVenta"].ToString(),
                                Cantidad = dr["Cantidad"].ToString(),
                                SubTotal = dr["SubTotal"].ToString()
                            });
                        }
                    }

                }
                catch (Exception ex)
                {
                    lista = new List<ReporteVenta>();
                }
            }
            return lista;
        }

        private bool HasColumn(IDataRecord dr, string columnName)
        {
            for (int i = 0; i < dr.FieldCount; i++)
            {
                if (dr.GetName(i).Equals(columnName, StringComparison.InvariantCultureIgnoreCase))
                    return true;
            }
            return false;
        }

    }
}
