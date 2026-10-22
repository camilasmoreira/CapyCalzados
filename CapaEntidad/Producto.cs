using System;

namespace CapaEntidad
{
    public class Producto
    {
        public int IdProducto { get; set; }
        public String Codigo { get; set; }
        public String Nombre { get; set; }
        public String Descripcion { get; set; }

        public String Marca { get; set; }
        public Categoria oCategoria { get; set; }
        public int Stock { get; set; }
        public decimal PrecioCompra { get; set; }
        public decimal PrecioVenta { get; set; }
        public bool Estado { get; set; }
        public String FechaRegistro { get; set; }
    }
}
