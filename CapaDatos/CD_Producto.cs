using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace CapaDatos
{
    public class CD_Producto
    {
        public List<Producto> Listar()
        {
            List<Producto> lista = new List<Producto>();

            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("SELECT p.IdProducto, p.Codigo, p.Nombre, p.Descripcion, p.Marca,");
                    query.AppendLine("       c.IdCategoria, c.Descripcion AS [DescripcionCategoria],");
                    query.AppendLine("       p.Stock, p.PrecioCompra, p.PrecioVenta, p.Estado,");
                    query.AppendLine("       ISNULL(prDir.IdProveedor, ISNULL(prComp.IdProveedor, ISNULL(prMarca.IdProveedor, 0))) AS IdProveedor,");
                    query.AppendLine("       ISNULL(prDir.RazonSocial, ISNULL(prComp.RazonSocial, ISNULL(prMarca.RazonSocial, p.Marca))) AS RazonSocialProveedor");
                    query.AppendLine("FROM Productos p");
                    query.AppendLine("INNER JOIN Categorias c ON c.IdCategoria = p.IdCategoria");
                    query.AppendLine("LEFT JOIN Proveedor prDir ON prDir.IdProveedor = p.IdProveedor");
                    query.AppendLine("LEFT JOIN (");
                    query.AppendLine("    SELECT dc.IdProducto, co.IdProveedor,");
                    query.AppendLine("           ROW_NUMBER() OVER (PARTITION BY dc.IdProducto ORDER BY co.FechaRegistro DESC, co.IdCompra DESC) AS rn");
                    query.AppendLine("    FROM Detalle_Compra dc");
                    query.AppendLine("    INNER JOIN Compra co ON co.IdCompra = dc.IdCompra");
                    query.AppendLine(") ult ON ult.IdProducto = p.IdProducto AND ult.rn = 1");
                    query.AppendLine("LEFT JOIN Proveedor prComp ON prComp.IdProveedor = ult.IdProveedor");
                    query.AppendLine("LEFT JOIN Proveedor prMarca ON prMarca.RazonSocial = p.Marca");

                    SqlCommand cmd = new SqlCommand(query.ToString(), oconexion);
                    cmd.CommandType = CommandType.Text;

                    oconexion.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new Producto()
                            {
                                IdProducto = Convert.ToInt32(dr["IdProducto"]),
                                Codigo = dr["Codigo"].ToString(),
                                Nombre = dr["Nombre"].ToString(),
                                Descripcion = dr["Descripcion"].ToString(),
                                Marca = dr["Marca"].ToString(),
                                oCategoria = new Categoria() { IdCategoria = Convert.ToInt32(dr["IdCategoria"]), Descripcion = dr["DescripcionCategoria"].ToString() },
                                Stock = Convert.ToInt32(dr["Stock"]),
                                PrecioCompra = Convert.ToDecimal(dr["PrecioCompra"]),
                                PrecioVenta = Convert.ToDecimal(dr["PrecioVenta"]),
                                Estado = Convert.ToBoolean(dr["Estado"]),
                                oProveedor = new Proveedor()
                                {
                                    IdProveedor = Convert.ToInt32(dr["IdProveedor"]),
                                    RazonSocial = string.IsNullOrWhiteSpace(dr["RazonSocialProveedor"].ToString()) ? "Sin proveedor" : dr["RazonSocialProveedor"].ToString()
                                }
                            });
                        }
                    }

                }
                catch (Exception ex)
                {
                    lista = new List<Producto>();
                }
            }
            return lista;
        }

        public int Registrar(Producto obj, out string Mensaje)
        {
            int IdProductoGenerado = 0;
            Mensaje = string.Empty;

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
                {

                    SqlCommand cmd = new SqlCommand("SP_REGISTRARPRODUCTO", oconexion);

                    cmd.Parameters.AddWithValue("Codigo", obj.Codigo);
                    cmd.Parameters.AddWithValue("Nombre", obj.Nombre);
                    cmd.Parameters.AddWithValue("Descripcion", obj.Descripcion);
                    cmd.Parameters.AddWithValue("Marca", obj.Marca);
                    cmd.Parameters.AddWithValue("IdCategoria", obj.oCategoria.IdCategoria);
                    cmd.Parameters.AddWithValue("Estado", obj.Estado);

                    cmd.Parameters.Add("Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oconexion.Open();

                    cmd.ExecuteNonQuery();

                    IdProductoGenerado = Convert.ToInt32(cmd.Parameters["Resultado"].Value);
                    Mensaje = cmd.Parameters["Mensaje"].Value.ToString();

                    if (IdProductoGenerado != 0 && obj.oProveedor != null && obj.oProveedor.IdProveedor > 0)
                    {
                        using (SqlCommand cmdProv = new SqlCommand("UPDATE Productos SET IdProveedor = @IdProveedor WHERE IdProducto = @IdProducto", oconexion))
                        {
                            cmdProv.Parameters.AddWithValue("@IdProveedor", obj.oProveedor.IdProveedor);
                            cmdProv.Parameters.AddWithValue("@IdProducto", IdProductoGenerado);
                            cmdProv.ExecuteNonQuery();
                        }
                    }

                }

            }
            catch (Exception ex)
            {
                IdProductoGenerado = 0;
                Mensaje = ex.Message;
            }

            return IdProductoGenerado;

        }

        public bool Editar(Producto obj, out string Mensaje)
        {
            bool Respuesta = false;
            Mensaje = string.Empty;

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
                {


                    SqlCommand cmd = new SqlCommand("SP_EDITARPRODUCTO", oconexion);
                    cmd.Parameters.AddWithValue("IdProducto", obj.IdProducto);
                    cmd.Parameters.AddWithValue("Codigo", obj.Codigo);
                    cmd.Parameters.AddWithValue("Nombre", obj.Nombre);
                    cmd.Parameters.AddWithValue("Descripcion", obj.Descripcion);
                    cmd.Parameters.AddWithValue("Marca", obj.Marca);
                    cmd.Parameters.AddWithValue("IdCategoria", obj.oCategoria.IdCategoria);
                    cmd.Parameters.AddWithValue("Estado", obj.Estado);
                    cmd.Parameters.Add("Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oconexion.Open();

                    cmd.ExecuteNonQuery();

                    Respuesta = Convert.ToBoolean(cmd.Parameters["Resultado"].Value);
                    Mensaje = cmd.Parameters["Mensaje"].Value.ToString();

                    if (Respuesta && obj.oProveedor != null && obj.oProveedor.IdProveedor > 0)
                    {
                        using (SqlCommand cmdProv = new SqlCommand("UPDATE Productos SET IdProveedor = @IdProveedor WHERE IdProducto = @IdProducto", oconexion))
                        {
                            cmdProv.Parameters.AddWithValue("@IdProveedor", obj.oProveedor.IdProveedor);
                            cmdProv.Parameters.AddWithValue("@IdProducto", obj.IdProducto);
                            cmdProv.ExecuteNonQuery();
                        }
                    }

                }

            }
            catch (Exception ex)
            {
                Respuesta = false;
                Mensaje = ex.Message;
            }

            return Respuesta;

        }

        public bool Eliminar(Producto obj, out string Mensaje)
        {
            bool Respuesta = false;
            Mensaje = string.Empty;

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
                {


                    SqlCommand cmd = new SqlCommand("SP_ELIMINARPRODUCTO", oconexion);
                    cmd.Parameters.AddWithValue("IdProducto", obj.IdProducto);
                    cmd.Parameters.Add("Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oconexion.Open();

                    cmd.ExecuteNonQuery();

                    Respuesta = Convert.ToBoolean(cmd.Parameters["Resultado"].Value);
                    Mensaje = cmd.Parameters["Mensaje"].Value.ToString();

                }

            }
            catch (Exception ex)
            {
                Respuesta = false;
                Mensaje = ex.Message;
            }

            return Respuesta;

        }
    }
}
