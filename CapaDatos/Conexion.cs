using System;
using System.Configuration;

namespace CapaDatos
{
    public class Conexion
    {
        public static String cadena = ConfigurationManager.ConnectionStrings["cadena_conexion"] != null
            ? ConfigurationManager.ConnectionStrings["cadena_conexion"].ToString()
            : "Data Source=(local);Initial Catalog=DBSistemaVentas;Integrated Security=True;Encrypt=False";
    }
}
