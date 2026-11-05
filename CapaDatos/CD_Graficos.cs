using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace CapaDatos
{
    public class CD_Graficos
    {
        public Grafico Datos()
        {
            Grafico obj = new Grafico();

            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("SP_DATOSGRAFICOS", oconexion);
                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlParameter total = new SqlParameter("@totVentas", 0); total.Direction = ParameterDirection.Output;
                    SqlParameter totalC = new SqlParameter("@totCompras", 0); totalC.Direction = ParameterDirection.Output;
                    SqlParameter prov = new SqlParameter("@totProveedores", 0); prov.Direction = ParameterDirection.Output;
                    SqlParameter prod = new SqlParameter("@totProd", 0); prod.Direction = ParameterDirection.Output;
                    SqlParameter cat = new SqlParameter("@totCat", 0); cat.Direction = ParameterDirection.Output;
                    SqlParameter user = new SqlParameter("@totUser", 0); user.Direction = ParameterDirection.Output;
                    SqlParameter cliente = new SqlParameter("@totClientes", 0); cliente.Direction = ParameterDirection.Output;

                    cmd.Parameters.Add(total);
                    cmd.Parameters.Add(totalC);
                    cmd.Parameters.Add(prov);
                    cmd.Parameters.Add(prod);
                    cmd.Parameters.Add(cat);
                    cmd.Parameters.Add(user);
                    cmd.Parameters.Add(cliente);
                    oconexion.Open();

                    cmd.ExecuteNonQuery();

                    obj.totalVentas = cmd.Parameters["@totVentas"].Value.ToString();
                    obj.Compras = cmd.Parameters["@totCompras"].Value.ToString();
                    obj.Proveedores = cmd.Parameters["@totProveedores"].Value.ToString();
                    obj.Prod = cmd.Parameters["@totProd"].Value.ToString();
                    obj.Cat = cmd.Parameters["@totCat"].Value.ToString();
                    obj.User = cmd.Parameters["@totUser"].Value.ToString();
                    obj.Clientes = cmd.Parameters["@totClientes"].Value.ToString();
                    oconexion.Close();
                }
                catch (Exception ex)
                {
                    obj = new Grafico();
                }
            }
            return obj;
        }

        public Grafico DatosFechas(DateTime fechaDesde, DateTime fechaHasta)
        {
            Grafico obj = new Grafico();

            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("SP_GRAFICOSVFECHAS", oconexion);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("fechaDesde", fechaDesde.Date);
                    cmd.Parameters.AddWithValue("fechaHasta", fechaHasta.Date);
                    SqlParameter total = new SqlParameter("@totVentas", 0); total.Direction = ParameterDirection.Output;
                    SqlParameter totalC = new SqlParameter("@totCompras", 0); totalC.Direction = ParameterDirection.Output;

                    cmd.Parameters.Add(total);
                    cmd.Parameters.Add(totalC);
                    oconexion.Open();

                    cmd.ExecuteNonQuery();

                    obj.totalVentas = cmd.Parameters["@totVentas"].Value.ToString();
                    obj.Compras = cmd.Parameters["@totCompras"].Value.ToString();
                    oconexion.Close();
                }
                catch (Exception ex)
                {
                    obj = new Grafico();
                }
            }
            return obj;
        }

        // 1. Productos más vendidos (Unidades) con filtro de fecha
        public DataTable ProductosMasVendidos(DateTime fechaDesde, DateTime fechaHasta)
        {
            DataTable dt = new DataTable();
            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    string query = @"
                        SELECT TOP 6 p.Nombre AS Producto, SUM(dv.Cantidad) AS Cantidad
                        FROM Detalle_Venta dv
                        INNER JOIN Venta v ON v.IdVenta = dv.IdVenta
                        INNER JOIN Productos p ON p.IdProducto = dv.IdProducto
                        WHERE CAST(v.FechaRegistro AS DATE) BETWEEN CAST(@desde AS DATE) AND CAST(@hasta AS DATE)
                        GROUP BY p.Nombre
                        ORDER BY SUM(dv.Cantidad) DESC";
                    SqlCommand cmd = new SqlCommand(query, oconexion);
                    cmd.Parameters.AddWithValue("@desde", fechaDesde.Date);
                    cmd.Parameters.AddWithValue("@hasta", fechaHasta.Date);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                }
                catch { }
            }
            return dt;
        }

        // 2. Productos de mayor recaudación ($) con filtro de fecha
        public DataTable ProductosMayorRecaudacion(DateTime fechaDesde, DateTime fechaHasta)
        {
            DataTable dt = new DataTable();
            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    string query = @"
                        SELECT TOP 6 p.Nombre AS Producto, SUM(dv.SubTotal) AS Recaudacion
                        FROM Detalle_Venta dv
                        INNER JOIN Venta v ON v.IdVenta = dv.IdVenta
                        INNER JOIN Productos p ON p.IdProducto = dv.IdProducto
                        WHERE CAST(v.FechaRegistro AS DATE) BETWEEN CAST(@desde AS DATE) AND CAST(@hasta AS DATE)
                        GROUP BY p.Nombre
                        ORDER BY SUM(dv.SubTotal) DESC";
                    SqlCommand cmd = new SqlCommand(query, oconexion);
                    cmd.Parameters.AddWithValue("@desde", fechaDesde.Date);
                    cmd.Parameters.AddWithValue("@hasta", fechaHasta.Date);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                }
                catch { }
            }
            return dt;
        }

        // 3. Evolución de ventas por fecha ($)
        public DataTable EvolucionVentas(DateTime fechaDesde, DateTime fechaHasta)
        {
            DataTable dt = new DataTable();
            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    string query = @"
                        SELECT CONVERT(varchar(10), CAST(v.FechaRegistro AS DATE), 103) AS Fecha,
                               CAST(v.FechaRegistro AS DATE) AS FechaOrden,
                               SUM(v.MontoTotal) AS TotalVentas,
                               COUNT(v.IdVenta) AS CantidadVentas
                        FROM Venta v
                        WHERE CAST(v.FechaRegistro AS DATE) BETWEEN CAST(@desde AS DATE) AND CAST(@hasta AS DATE)
                        GROUP BY CAST(v.FechaRegistro AS DATE)
                        ORDER BY FechaOrden ASC";
                    SqlCommand cmd = new SqlCommand(query, oconexion);
                    cmd.Parameters.AddWithValue("@desde", fechaDesde.Date);
                    cmd.Parameters.AddWithValue("@hasta", fechaHasta.Date);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                }
                catch { }
            }
            return dt;
        }

        // 4. Ranking de vendedores ($ y cantidad de ventas)
        public DataTable RankingVendedores(DateTime fechaDesde, DateTime fechaHasta)
        {
            DataTable dt = new DataTable();
            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    string query = @"
                        SELECT u.NombreCompleto AS Vendedor,
                               COUNT(v.IdVenta) AS CantidadVentas,
                               SUM(v.MontoTotal) AS TotalRecaudado
                        FROM Venta v
                        INNER JOIN Usuario u ON u.IdUsuario = v.IdUsuario
                        WHERE CAST(v.FechaRegistro AS DATE) BETWEEN CAST(@desde AS DATE) AND CAST(@hasta AS DATE)
                        GROUP BY u.NombreCompleto
                        ORDER BY SUM(v.MontoTotal) DESC";
                    SqlCommand cmd = new SqlCommand(query, oconexion);
                    cmd.Parameters.AddWithValue("@desde", fechaDesde.Date);
                    cmd.Parameters.AddWithValue("@hasta", fechaHasta.Date);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                }
                catch { }
            }
            return dt;
        }

        // 5. Días de mayor movimiento (ventas por día de la semana)
        public DataTable DiasMayorMovimiento(DateTime fechaDesde, DateTime fechaHasta)
        {
            DataTable dt = new DataTable();
            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    string query = @"
                        SET DATEFIRST 1;
                        SELECT DATEPART(WEEKDAY, v.FechaRegistro) AS NumDia,
                               CASE DATEPART(WEEKDAY, v.FechaRegistro)
                                   WHEN 1 THEN 'Lunes'
                                   WHEN 2 THEN 'Martes'
                                   WHEN 3 THEN 'Miércoles'
                                   WHEN 4 THEN 'Jueves'
                                   WHEN 5 THEN 'Viernes'
                                   WHEN 6 THEN 'Sábado'
                                   WHEN 7 THEN 'Domingo'
                               END AS DiaSemana,
                               COUNT(v.IdVenta) AS CantidadVentas,
                               SUM(v.MontoTotal) AS TotalVentas
                        FROM Venta v
                        WHERE CAST(v.FechaRegistro AS DATE) BETWEEN CAST(@desde AS DATE) AND CAST(@hasta AS DATE)
                        GROUP BY DATEPART(WEEKDAY, v.FechaRegistro)
                        ORDER BY NumDia ASC";
                    SqlCommand cmd = new SqlCommand(query, oconexion);
                    cmd.Parameters.AddWithValue("@desde", fechaDesde.Date);
                    cmd.Parameters.AddWithValue("@hasta", fechaHasta.Date);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                }
                catch { }
            }
            return dt;
        }

        public ArrayList Categorias()
        {
            ArrayList Categoria = new ArrayList();
            ArrayList Producto = new ArrayList();

            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("SP_PRODCATEGORIAS", oconexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Categoria.Add(dr.GetString(0));
                            Producto.Add(dr.GetInt32(1));
                        }
                    }
                }
                catch (Exception ex) { }
            }
            return Categoria;
        }

        public ArrayList Productos()
        {
            ArrayList Categoria = new ArrayList();
            ArrayList Producto = new ArrayList();

            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("SP_PRODCATEGORIAS", oconexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Categoria.Add(dr.GetString(0));
                            Producto.Add(dr.GetInt32(1));
                        }
                    }
                }
                catch (Exception ex) { }
            }
            return Producto;
        }

        public ArrayList Vendidos()
        {
            ArrayList Vendidos = new ArrayList();
            ArrayList Cantidad = new ArrayList();

            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("SP_PRODPREFERIDOS", oconexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Vendidos.Add(dr.GetString(0));
                            Cantidad.Add(dr.GetInt32(1));
                        }
                    }
                }
                catch (Exception ex) { }
            }
            return Vendidos;
        }

        public ArrayList Cantidad()
        {
            ArrayList Cantidad = new ArrayList();

            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("SP_PRODPREFERIDOS", oconexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Cantidad.Add(dr.GetInt32(1));
                        }
                    }
                }
                catch (Exception ex) { }
            }
            return Cantidad;
        }
    }
}
