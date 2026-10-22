using CapaEntidad;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace CapaDatos
{
    public class CD_Backup
    {


        public string Backup()
        {

            string resultado = "";
            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {

                try
                {
                    if (!Directory.Exists("C:\\Backup"))
                    {
                        Directory.CreateDirectory("C:\\Backup");
                    }
                    string nombre = "Calzados_Ventas Fecha " + DateTime.Now.ToString("dd_MM_yyyy") + " Hora " + DateTime.Now.ToString("hh_mm_ss") + ".bak";

                    StringBuilder query = new StringBuilder();
                    query.AppendLine("BACKUP DATABASE [DBSistemaVentas] TO  DISK = N'C:\\Backup\\" + nombre + "' WITH NOFORMAT, NOINIT,  NAME = N'DBSistemaVentas-Full Database Backup', SKIP, NOREWIND, NOUNLOAD,  STATS = 10\r\n");


                    query.AppendLine("\r\nUpdate BackupR \r\nset fecha_registro = GETDATE()\r\nwhere idBackup = 1");

                    SqlCommand cmd = new SqlCommand(query.ToString(), oconexion);
                    oconexion.Open();
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();

                    resultado = "Copia realizada exitosamente";

                }
                catch (Exception)
                {
                    resultado = "Por favor intentelo nuevamente";
                }
            }
            return resultado;


           

        }

        public string Restore(string ruta)
        {
            string resultado = "";
            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("USE master;");
                    query.AppendLine("ALTER DATABASE [DBSistemaVentas] SET SINGLE_USER WITH ROLLBACK IMMEDIATE;");
                    query.AppendLine($"RESTORE DATABASE [DBSistemaVentas] FROM DISK = N'{ruta}' WITH REPLACE;");
                    query.AppendLine("ALTER DATABASE [DBSistemaVentas] SET MULTI_USER;");
                   
                    SqlCommand cmd = new SqlCommand(query.ToString(), oconexion);
                    oconexion.Open();
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();

                    resultado = "Restauración exitosa";
                }
                catch (Exception ex)
                {
                    resultado = "Por favor, inténtelo nuevamente. Error: " + ex.Message;
                }
            }
            return resultado;
        }


        public bool Backup(DateTime fecha)
        {
            bool resultado = false;
            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {

                if (LlamarFuncionComparar(fecha) == true)
                {
                    try
                    {
                        if (!Directory.Exists("C:\\Backup"))
                        {
                            Directory.CreateDirectory("C:\\Backup");
                        }


                        string nombre = "Calzados_Ventas (Automatico) Fecha " + fecha.ToString("dd_MM_yyyy") + ".bak";

                        StringBuilder query = new StringBuilder();
                        query.AppendLine("BACKUP DATABASE [DBSistemaVentas] TO  DISK = N'C:\\Backup\\" + nombre + "' WITH NOFORMAT, NOINIT,  NAME = N'DBSistemaVentas-Full Database Backup', SKIP, NOREWIND, NOUNLOAD,  STATS = 10\r\n");
                        query.AppendLine("\r\nUpdate BackupR \r\nset fecha_registro = GETDATE()\r\nwhere idBackup = 1");

                        SqlCommand cmd = new SqlCommand(query.ToString(), oconexion);



                        oconexion.Open();
                        cmd.CommandType = CommandType.Text;
                        cmd.ExecuteNonQuery();

                        resultado = true;

                    }
                    catch (Exception)
                    {
                        resultado = false;
                    }
                }



                
            }
            return resultado;

        }


        private bool LlamarFuncionComparar(DateTime fechaRegistro)
        {
            bool resultado = false;

            // Crear la conexión a la base de datos
            using (SqlConnection connection = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    // Crear el comando para ejecutar la función
                    SqlCommand command = new SqlCommand("SELECT dbo.comparar(@fecha_registro)", connection);
                    command.CommandType = CommandType.Text;

                    // Añadir el parámetro @fecha_registro
                    command.Parameters.AddWithValue("@fecha_registro", fechaRegistro);

                    // Abrir la conexión
                    connection.Open();

                    // Ejecutar el comando y obtener el resultado (esperamos un valor de tipo BIT)
                    object result = command.ExecuteScalar();

                    // Convertir el resultado a un valor booleano
                    if (result != DBNull.Value)
                    {
                        resultado = Convert.ToBoolean(result);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }

            return resultado;
        }

    
    }
}
