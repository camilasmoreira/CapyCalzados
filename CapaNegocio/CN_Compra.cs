using CapaDatos;
using CapaEntidad;
using System.Collections.Generic;
using System.Data;

namespace CapaNegocio
{
    public class CN_Compra
    {
        private CD_Compra objcd_compra = new CD_Compra();

        public int obtenerCorrelativo()
        {
            return objcd_compra.obtenerCorrelativo();
        }

        public bool Registrar(Compra obj, DataTable DetalleCompra, out string Mensaje)
        {

            return objcd_compra.Registrar(obj, DetalleCompra, out Mensaje);


        }

        public Compra obtenerCompra(string numero)
        {
            Compra oCompra = objcd_compra.obtenerCompra(numero);

            if (oCompra.IdCompra != 0)
            {
                List<DetalleCompra> oDetalleCompra = objcd_compra.obtenerDetalleCompra(oCompra.IdCompra);

                oCompra.oDetalleCompra = oDetalleCompra;
            }
            return oCompra;
        }

        public bool ValidarCompra(string numero, out string Mensaje)
        {

            Compra oCompra = objcd_compra.obtenerCompra(numero);


            return objcd_compra.ValidarCompra(oCompra.IdCompra, out Mensaje);
        }
    }
}
