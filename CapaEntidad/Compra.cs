using System;
using System.Collections.Generic;

namespace CapaEntidad
{
    public class Compra
    {
        public int IdCompra { get; set; }
        public Usuario oUsuario { get; set; }
        public Proveedor oProveedor { get; set; }
        public String TipoDocumento { get; set; }
        public String NumeroDocumento { get; set; }
        public decimal MontoTotal { get; set; }
        public List<DetalleCompra> oDetalleCompra { get; set; }
        public String FechaRegistro { get; set; }

        public bool Estado { get; set; }
    }
}
