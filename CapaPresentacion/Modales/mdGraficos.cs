using CapaEntidad;
using CapaNegocio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace CapaPresentacion.Modales
{
    public partial class mdGraficos : Form
    {
        public mdGraficos()
        {
            InitializeComponent();

          

            // Configura la opacidad del formulario (50% de transparencia)
            this.Opacity = 1;
            // Establece un color clave de transparencia (el color de fondo del formulario será transparente)
            this.BackColor = Color.FromArgb(36, 35, 58); // El color que quieres transparente
            this.TransparencyKey = this.BackColor;
        }



        private void mdGraficos_Load(object sender, EventArgs e)
        {
            // Agregar puntos a las series
            ctVentas.Series[0].Points.AddY(0);
            ctCompras.Series[0].Points.AddY(0);
        }

        private void rjButton1_Click(object sender, EventArgs e)
        {
            // Crear el objeto que obtiene los datos de fechas
            Grafico obj = new CN_Grafico().DatosFechas(dtDesde.Value, dtHasta.Value);

            // Convertir los valores a decimales
            string totVentasStr = obj.totalVentas;
            string totComprasStr = obj.Compras;

            decimal totVentas;
            decimal totCompras;

            // Intentar convertir totVentas
            if (!decimal.TryParse(totVentasStr, out totVentas))
            {
                totVentas = 0; // Valor por defecto o manejar según sea necesario
            }

            // Intentar convertir totCompras
            if (!decimal.TryParse(totComprasStr, out totCompras))
            {
              
                totCompras = 0; // Valor por defecto o manejar según sea necesario
            }

            // Limpiar las series anteriores (opcional, si no quieres acumular datos)
            ctVentas.Series[0].Points.Clear();
            ctCompras.Series[0].Points.Clear();

            // Agregar puntos a las series
            ctVentas.Series[0].Points.AddY(totVentas);
            ctCompras.Series[0].Points.AddY(totCompras);
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();   
        }

        private void dtDesde_ValueChanged(object sender, EventArgs e)
        {
            // Crear el objeto que obtiene los datos de fechas
            Grafico obj = new CN_Grafico().DatosFechas(dtDesde.Value, dtHasta.Value);

            // Convertir los valores a decimales
            string totVentasStr = obj.totalVentas;
            string totComprasStr = obj.Compras;

            decimal totVentas;
            decimal totCompras;

            // Intentar convertir totVentas
            if (!decimal.TryParse(totVentasStr, out totVentas))
            {
                totVentas = 0; // Valor por defecto o manejar según sea necesario
            }

            // Intentar convertir totCompras
            if (!decimal.TryParse(totComprasStr, out totCompras))
            {

                totCompras = 0; // Valor por defecto o manejar según sea necesario
            }

            // Limpiar las series anteriores (opcional, si no quieres acumular datos)
            ctVentas.Series[0].Points.Clear();
            ctCompras.Series[0].Points.Clear();

            // Agregar puntos a las series
            ctVentas.Series[0].Points.AddY(totVentas);
            ctCompras.Series[0].Points.AddY(totCompras);
        }

        private void dtHasta_ValueChanged(object sender, EventArgs e)
        {
            // Crear el objeto que obtiene los datos de fechas
            Grafico obj = new CN_Grafico().DatosFechas(dtDesde.Value, dtHasta.Value);

            // Convertir los valores a decimales
            string totVentasStr = obj.totalVentas;
            string totComprasStr = obj.Compras;

            decimal totVentas;
            decimal totCompras;

            // Intentar convertir totVentas
            if (!decimal.TryParse(totVentasStr, out totVentas))
            {
                totVentas = 0; // Valor por defecto o manejar según sea necesario
            }

            // Intentar convertir totCompras
            if (!decimal.TryParse(totComprasStr, out totCompras))
            {

                totCompras = 0; // Valor por defecto o manejar según sea necesario
            }

            // Limpiar las series anteriores (opcional, si no quieres acumular datos)
            ctVentas.Series[0].Points.Clear();
            ctCompras.Series[0].Points.Clear();

            // Agregar puntos a las series
            ctVentas.Series[0].Points.AddY(totVentas);
            ctCompras.Series[0].Points.AddY(totCompras);
        }
    }
}
