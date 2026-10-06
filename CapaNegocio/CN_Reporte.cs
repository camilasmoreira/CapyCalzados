using CapaDatos;
using CapaEntidad;
using System;
using System.Collections.Generic;

namespace CapaNegocio
{
    public class CN_Reporte
    {
        private CD_Reporte objcd_reporte = new CD_Reporte();

        public List<ReporteCompra> Compra(DateTime fechainicio, DateTime fechafin, int idProveedor)
        {
            return objcd_reporte.Compra(fechainicio, fechafin, idProveedor);
        }
        public List<ReporteVenta> Venta(DateTime fechainicio, DateTime fechafin)
        {
            return objcd_reporte.Venta(fechainicio, fechafin);
        }
    }
}
