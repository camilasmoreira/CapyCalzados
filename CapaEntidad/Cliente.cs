using System;

namespace CapaEntidad
{
    public class Cliente
    {
        public int IdCliente { get; set; }
        public String Documento { get; set; }
        public String NombreCompleto { get; set; }
        public String Correo { get; set; }
        public String Telefono { get; set; }

        public String Direccion { get; set; }
        public bool Estado { get; set; }
        public String FechaRegistro { get; set; }
    }
}
