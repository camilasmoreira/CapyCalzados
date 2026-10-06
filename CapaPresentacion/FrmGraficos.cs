using CapaEntidad;
using CapaNegocio;
using CapaPresentacion.Modales;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace CapaPresentacion
{
    // Formulario que permite visualizar gráficos de ventas y compras
    public partial class FrmGraficos : Form
    {
        // Constructor del formulario, inicializa los componentes
        public FrmGraficos()
        {
            InitializeComponent();
        }

        // Evento generado por el diseñador para el ingreso en el GroupBox (sin uso)
        private void groupBox1_Enter(object sender, EventArgs e)
        {
        }

        // Evento que se ejecuta cuando se carga el formulario
        private void FrmGraficos_Load(object sender, EventArgs e)
        {
            // Listas para almacenar los datos de categorías y productos desde la capa de negocio
            ArrayList Categorias = new ArrayList();
            ArrayList Productos = new ArrayList();

            // Obtener las categorías y productos desde la capa de negocio (CN_Grafico)
            Categorias = new CN_Grafico().Categorias();
            Productos = new CN_Grafico().Productos();

            // Listas para almacenar los datos de productos vendidos y la cantidad desde la capa de negocio
            ArrayList Vendidos = new ArrayList();
            ArrayList Cantidad = new ArrayList();

            // Obtener los productos vendidos y la cantidad de ventas desde la capa de negocio (CN_Grafico)
            Vendidos = new CN_Grafico().Vendidos();
            Cantidad = new CN_Grafico().Cantidad();

            // Obtener el número de compras realizadas
            int compra = new CN_Compra().obtenerCorrelativo() - 1;

            // Ciclo para crear series de gráficos basadas en las categorías de productos
            for (int i = 0; i < Categorias.Count; i++)
            {
                // Crear una nueva serie para cada categoría
                Series series1 = new Series(Categorias[i].ToString());
                series1.ChartType = SeriesChartType.Column;  // Tipo de gráfico: columnas

                // Agregar puntos a la serie con los valores de la categoría y productos
                series1.Points.AddXY(Categorias[i], Productos[i]);

                // Agregar la serie al gráfico de ventas
                chVentas.Series.Add(series1);
            }

            // Enlazar los datos de productos vendidos y la cantidad al gráfico de compras
            chCompras.Series[0].Points.DataBindXY(Vendidos, Cantidad);

            // Obtener datos de ventas y compras desde la capa de negocio
            Grafico obj = new CN_Grafico().Datos();

            // Mostrar el total de ventas y compras en los respectivos labels
            Ventas.Text = Ventas.Text + "  $" + obj.totalVentas.ToString();
            Compras.Text = Compras.Text + "  $" + obj.Compras.ToString();
        }

        // Evento que abre un formulario modal al hacer clic en el botón
        private void rjButton1_Click(object sender, EventArgs e)
        {
            // Crear una nueva instancia del modal de gráficos
            mdGraficos modal = new mdGraficos();

            // Mostrar el modal
            modal.Show();
        }
    }
}
