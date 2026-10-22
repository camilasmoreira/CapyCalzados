using System;

namespace CapaEntidad
{
    public class DetalleCompra
    {
        public int IdCompra { get; set; }
        public Producto oProducto { get; set; }
        public decimal PrecioCompra { get; set; }
        public decimal PrecioVenta { get; set; }
        public int Cantidad { get; set; }
        public decimal MontoTotal { get; set; }
        public String FechaRegistro { get; set; }
    }
}
