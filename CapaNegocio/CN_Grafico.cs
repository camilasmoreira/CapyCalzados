using CapaDatos;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaEntidad;

namespace CapaNegocio
{
    public class CN_Grafico
    {
        private CD_Graficos obj = new CD_Graficos();

        public ArrayList Productos()
        {
            return obj.Productos();
        }
        public ArrayList Categorias()
        {
            return obj.Categorias();
        }

        public Grafico DatosFechas(DateTime fechaDesde, DateTime fechaHasta)
        {
            return obj.DatosFechas(fechaDesde, fechaHasta);
        }

        public DataTable ProductosMasVendidos(DateTime fechaDesde, DateTime fechaHasta)
        {
            return obj.ProductosMasVendidos(fechaDesde, fechaHasta);
        }

        public DataTable ProductosMayorRecaudacion(DateTime fechaDesde, DateTime fechaHasta)
        {
            return obj.ProductosMayorRecaudacion(fechaDesde, fechaHasta);
        }

        public DataTable EvolucionVentas(DateTime fechaDesde, DateTime fechaHasta)
        {
            return obj.EvolucionVentas(fechaDesde, fechaHasta);
        }

        public DataTable EvolucionVentasHistorica(string agrupacion, DateTime fechaDesde, DateTime fechaHasta)
        {
            return obj.EvolucionVentasHistorica(agrupacion, fechaDesde, fechaHasta);
        }

        public DataTable RankingVendedores(DateTime fechaDesde, DateTime fechaHasta)
        {
            return obj.RankingVendedores(fechaDesde, fechaHasta);
        }

        public DataTable DiasMayorMovimiento(DateTime fechaDesde, DateTime fechaHasta)
        {
            return obj.DiasMayorMovimiento(fechaDesde, fechaHasta);
        }

        public ArrayList Vendidos()
        {
            return obj.Vendidos();
        }

        public ArrayList Cantidad()
        {
            return obj.Cantidad();
        }

        public Grafico Datos()
        {
            return obj.Datos();
        }
    }
}
