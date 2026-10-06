using System;
using System.Configuration;

namespace CapaDatos
{
    public class Conexion
    {
        public static String cadena = ConfigurationManager.ConnectionStrings["cadena_conexion"].ToString();

    }
}
