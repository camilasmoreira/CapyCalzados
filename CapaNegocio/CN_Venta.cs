using CapaDatos;
using CapaEntidad;
using System.Collections.Generic;
using System.Data;

namespace CapaNegocio
{
    public class CN_Venta
    {
        private CD_Venta objcd_Venta = new CD_Venta();

        public int obtenerCorrelativo()
        {
            return objcd_Venta.obtenerCorrelativo();
        }


        public bool Registrar(Venta obj, DataTable DetalleVenta, out string Mensaje)
        {

            return objcd_Venta.Registrar(obj, DetalleVenta, out Mensaje);


        }
        public bool SumarStock(int idProd, int cantidad)
        {
            return objcd_Venta.SumarStock(idProd, cantidad);
        }
        public bool RestarStock(int idProd, int cantidad)
        {
            return objcd_Venta.RestarStock(idProd, cantidad);
        }

        public Venta obtenerVenta(string numero)
        {
            Venta oVenta = objcd_Venta.obtenerVenta(numero);

            if (oVenta.IdVenta != 0)
            {
                List<DetalleVenta> oDetalleVenta = objcd_Venta.obtenerDetalleVenta(oVenta.IdVenta);

                oVenta.oDetalleVenta = oDetalleVenta;
            }
            return oVenta;
        }
    }
}
