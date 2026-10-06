using System;
using System.Collections.Generic;

namespace CapaEntidad
{
    public class Venta
    {
        public int IdVenta { get; set; }
        public Usuario oUsuario { get; set; }
        public String TipoDocumento { get; set; }
        public String NumeroDocumento { get; set; }
        public String DocumentoCliente { get; set; }
 
        public int IdCupon { get; set; }
        public String NombreCliente { get; set; }
        public decimal MontoPago { get; set; }
        public decimal MontoCambio { get; set; }
        public decimal MontoTotal { get; set; }
        public List<DetalleVenta> oDetalleVenta { get; set; }
        public String FechaRegistro { get; set; }
    }
}
