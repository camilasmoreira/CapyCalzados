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

                    SqlParameter total = new SqlParameter("@totVentas", 0);total.Direction = ParameterDirection.Output;
                    SqlParameter totalC = new SqlParameter("@totCompras", 0); totalC.Direction = ParameterDirection.Output;
                    SqlParameter prov = new SqlParameter("@totProveedores", 0); prov.Direction = ParameterDirection.Output;
                    SqlParameter prod = new SqlParameter("@totProd", 0); prod.Direction = ParameterDirection.Output;
                    SqlParameter cat = new SqlParameter("@totCat", 0);  cat.Direction = ParameterDirection.Output;
                    SqlParameter user = new SqlParameter("@totUser", 0);  user.Direction = ParameterDirection.Output;
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

                    cmd.Parameters.AddWithValue("fechaDesde", fechaDesde);
                    cmd.Parameters.AddWithValue("fechaHasta", fechaHasta);
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
                }catch (Exception ex) {

                }
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
                catch (Exception ex)
                {

                }
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
                catch (Exception ex)
                {

                }
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
                catch (Exception ex)
                {

                }
            }
            return Cantidad;
        }

    }
}
