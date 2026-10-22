using System;

namespace CapaEntidad
{
    public class Permiso
    {
        public int IdPermiso { get; set; }
        public Rol oRol { get; set; }
        public String NombreMenu { get; set; }
        public String FechaRegistro { get; set; }
    }
}
