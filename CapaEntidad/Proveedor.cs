using System;

namespace CapaEntidad
{
    public class Proveedor
    {
        public int IdProveedor { get; set; }
        public String Documento { get; set; }
        public String RazonSocial { get; set; }
        public String Direccion { get; set; }
        public String Correo { get; set; }
        public String Telefono { get; set; }
        public bool Estado { get; set; }
        public String FechaRegistro { get; set; }
    }
}
