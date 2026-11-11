using CapaEntidad;
using CapaNegocio;
using CapaPresentacion.Utilidades;
using CustomControls.RJControls;
using FontAwesome.Sharp;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace CapaPresentacion
{
    /// <summary>
    /// Formulario independiente para la Visión Histórica General de Evolución de Ventas.
    /// Agrupa los datos por períodos mayores (Semana, Mes, Trimestre, Año) para evitar
    /// sobrecarga visual en consultas históricas de largo plazo.
    /// </summary>
    public class FrmGraficosHistorico : Form
    {
        public Inicio InicioRef { get; set; }

        private RJTextBox rjTextBox2;
        private GroupBox groupBox1;
        private ComboBox comboAgrupacion;
        private ComboBox comboHorizonte;
        private RJButton btnActualizar;
        private RJButton btnIrDetallado;
        private RJButton cardTotalHistorico;
        private RJButton cardPromedioPeriodo;

        private Chart chHistoricoVentas;
        private Chart chHistoricoOperaciones;
        private Chart chTop5RecaudacionHist;

        public FrmGraficosHistorico(Inicio inicio = null)
        {
            InicioRef = inicio;
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.ClientSize = new Size(902, 620);
            this.Name = "FrmGraficosHistorico";
            this.Text = "Evolución Histórica General de Ventas";
            this.AutoScaleDimensions = new SizeF(6F, 13F);
            this.AutoScaleMode = AutoScaleMode.Font;

            // Contenedor base estilo Capybara
            rjTextBox2 = new RJTextBox()
            {
                Name = "rjTextBox2",
                Location = new Point(25, 13),
                Size = new Size(874, 595),
                Multiline = true,
                Enabled = false,
                IsReadOnly = true,
                BorderRadius = 15,
                BorderSize = 2
            };

            // Panel superior de configuración de agrupación histórica y KPIs
            groupBox1 = new GroupBox()
            {
                Name = "groupBox1",
                Text = "Visión Histórica General - Evolución Agrupada por Períodos Mayores",
                Location = new Point(45, 20),
                Size = new Size(830, 112),
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                ForeColor = Color.White
            };

            Label lblAgrupacion = new Label()
            {
                AutoSize = true,
                Text = "Agrupar por Período:",
                Location = new Point(16, 24),
                Font = new Font("Segoe UI", 9F, FontStyle.Bold)
            };

            comboAgrupacion = new ComboBox()
            {
                Name = "comboAgrupacion",
                DropDownStyle = ComboBoxStyle.DropDownList,
                FlatStyle = FlatStyle.Flat,
                Location = new Point(16, 46),
                Size = new Size(145, 28)
            };
            comboAgrupacion.Items.Add(new OpcionesCombo() { Valor = "MES", Texto = "Por Mes" });
            comboAgrupacion.Items.Add(new OpcionesCombo() { Valor = "SEMANA", Texto = "Por Semana" });
            comboAgrupacion.Items.Add(new OpcionesCombo() { Valor = "TRIMESTRE", Texto = "Por Trimestre" });
            comboAgrupacion.Items.Add(new OpcionesCombo() { Valor = "ANIO", Texto = "Por Año" });
            comboAgrupacion.DisplayMember = "Texto";
            comboAgrupacion.ValueMember = "Valor";
            comboAgrupacion.SelectedIndex = 0;
            comboAgrupacion.SelectedIndexChanged += (s, e) => CargarGraficosHistoricos();

            Label lblHorizonte = new Label()
            {
                AutoSize = true,
                Text = "Horizonte Histórico:",
                Location = new Point(172, 24),
                Font = new Font("Segoe UI", 9F, FontStyle.Bold)
            };

            comboHorizonte = new ComboBox()
            {
                Name = "comboHorizonte",
                DropDownStyle = ComboBoxStyle.DropDownList,
                FlatStyle = FlatStyle.Flat,
                Location = new Point(172, 46),
                Size = new Size(155, 28)
            };
            comboHorizonte.Items.Add(new OpcionesCombo() { Valor = "TODO", Texto = "Histórico Completo" });
            comboHorizonte.Items.Add(new OpcionesCombo() { Valor = "ANIO", Texto = "Último Año (12 m)" });
            comboHorizonte.Items.Add(new OpcionesCombo() { Valor = "SEMESTRE", Texto = "Últimos 6 Meses" });
            comboHorizonte.Items.Add(new OpcionesCombo() { Valor = "TRIMESTRE", Texto = "Últimos 3 Meses" });
            comboHorizonte.DisplayMember = "Texto";
            comboHorizonte.ValueMember = "Valor";
            comboHorizonte.SelectedIndex = 0;
            comboHorizonte.SelectedIndexChanged += (s, e) => CargarGraficosHistoricos();

            btnActualizar = new RJButton()
            {
                Name = "btnActualizar",
                Text = "  Filtrar",
                Location = new Point(338, 40),
                Size = new Size(95, 36)
            };
            btnActualizar.Click += (s, e) => CargarGraficosHistoricos();

            // Botón para cambiar a la Sección / Form de Visión Detallada con Filtros
            btnIrDetallado = new RJButton()
            {
                Name = "btnIrDetallado",
                Text = "  Visión Detallada (Filtros)",
                Location = new Point(16, 78),
                Size = new Size(417, 28)
            };
            btnIrDetallado.Click += BtnIrDetallado_Click;

            cardTotalHistorico = new RJButton()
            {
                Name = "Ventas",
                Text = "Total Ventas:\n$0.00",
                Location = new Point(445, 22),
                Size = new Size(182, 80),
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleCenter
            };

            cardPromedioPeriodo = new RJButton()
            {
                Name = "Compras",
                Text = "Promedio / Período:\n$0.00",
                Location = new Point(636, 22),
                Size = new Size(182, 80),
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleCenter
            };

            groupBox1.Controls.Add(lblAgrupacion);
            groupBox1.Controls.Add(comboAgrupacion);
            groupBox1.Controls.Add(lblHorizonte);
            groupBox1.Controls.Add(comboHorizonte);
            groupBox1.Controls.Add(btnActualizar);
            groupBox1.Controls.Add(btnIrDetallado);
            groupBox1.Controls.Add(cardTotalHistorico);
            groupBox1.Controls.Add(cardPromedioPeriodo);

            // Gráfico 1 (Superior Ancho): Evolución Histórica General de Ventas ($) por Período Mayor
            chHistoricoVentas = CrearGraficoBase(
                "chHistoricoVentas",
                "1. Evolución Histórica General de Ventas ($) - Agrupada por Mes",
                new Point(45, 140),
                new Size(830, 225)
            );

            // Gráfico 2 (Inferior Izquierdo): Volumen Histórico de Operaciones por Período
            chHistoricoOperaciones = CrearGraficoBase(
                "chHistoricoOperaciones",
                "2. Volumen de Ventas por Período (Cant. Operaciones)",
                new Point(45, 372),
                new Size(410, 222)
            );

            // Gráfico 3 (Inferior Derecho): Top 5 Mayor Recaudación en el Horizonte Histórico
            chTop5RecaudacionHist = CrearGraficoBase(
                "chTop5RecaudacionHist",
                "3. Top 5 Productos de Mayor Recaudación Histórica ($)",
                new Point(465, 372),
                new Size(410, 222)
            );

            this.Controls.Add(groupBox1);
            this.Controls.Add(chHistoricoVentas);
            this.Controls.Add(chHistoricoOperaciones);
            this.Controls.Add(chTop5RecaudacionHist);
            this.Controls.Add(rjTextBox2);

            this.Load += FrmGraficosHistorico_Load;
        }

        private Chart CrearGraficoBase(string nombre, string titulo, Point ubicacion, Size tamano)
        {
            Chart chart = new Chart()
            {
                Name = nombre,
                Location = ubicacion,
                Size = tamano
            };
            ChartArea area = new ChartArea("ChartArea1");
            area.AxisX.Interval = 1;
            area.AxisX.MajorGrid.Enabled = false;
            area.AxisX.LabelStyle.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
            area.AxisY.LabelStyle.Font = new Font("Segoe UI", 8F, FontStyle.Regular);
            chart.ChartAreas.Add(area);

            Title title = new Title(titulo, Docking.Top, new Font("Segoe UI", 9.5F, FontStyle.Bold), Color.White);
            chart.Titles.Add(title);
            return chart;
        }

        private void BtnIrDetallado_Click(object sender, EventArgs e)
        {
            if (InicioRef != null)
            {
                InicioRef.AbrirGraficosDetallado();
            }
        }

        private void FrmGraficosHistorico_Load(object sender, EventArgs e)
        {
            TemaCapybara.AplicarTema(this);
            EstilizarBotonCambioSeccion();
            CargarGraficosHistoricos();
        }

        private void EstilizarBotonCambioSeccion()
        {
            if (btnIrDetallado != null)
            {
                btnIrDetallado.Text = "  Ver Sección: Visión Detallada con Filtros (Fechas / Recientes)";
                btnIrDetallado.Image = IconChar.Sliders.ToBitmap(TemaCapybara.BotonTexto, 15);
                btnIrDetallado.BackColor = TemaCapybara.BotonPasto;
                btnIrDetallado.BackgroundColor = TemaCapybara.BotonPasto;
                btnIrDetallado.TextImageRelation = TextImageRelation.ImageBeforeText;
                btnIrDetallado.ImageAlign = ContentAlignment.MiddleLeft;
                btnIrDetallado.TextAlign = ContentAlignment.MiddleCenter;
                btnIrDetallado.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
            }
        }

        private void CargarGraficosHistoricos()
        {
            if (!(comboAgrupacion.SelectedItem is OpcionesCombo ocAgrup)) return;
            if (!(comboHorizonte.SelectedItem is OpcionesCombo ocHoriz)) return;

            string modoAgrupacion = ocAgrup.Valor.ToString();
            string etiquetaAgrupacion = ocAgrup.Texto;
            string horizonte = ocHoriz.Valor.ToString();

            DateTime hoy = DateTime.Now.Date;
            DateTime hasta = hoy.AddDays(1);
            DateTime desde;

            if (horizonte == "TRIMESTRE")
                desde = hoy.AddMonths(-3);
            else if (horizonte == "SEMESTRE")
                desde = hoy.AddMonths(-6);
            else if (horizonte == "ANIO")
                desde = hoy.AddYears(-1);
            else
                desde = hoy.AddYears(-5);

            CN_Grafico cn = new CN_Grafico();
            DataTable dtHist = cn.EvolucionVentasHistorica(modoAgrupacion, desde, hasta);

            // 1. Evolución Histórica General de Ventas ($)
            chHistoricoVentas.Titles.Clear();
            chHistoricoVentas.Titles.Add(new Title(
                $"1. Evolución Histórica General de Ventas ($) - Agrupada {etiquetaAgrupacion}",
                Docking.Top,
                new Font("Segoe UI", 9.5F, FontStyle.Bold),
                Color.White
            ));
            chHistoricoVentas.Series.Clear();
            chHistoricoVentas.Legends.Clear();
            chHistoricoVentas.ChartAreas[0].AxisX.Interval = 1;
            chHistoricoVentas.ChartAreas[0].AxisX.LabelStyle.Angle = dtHist.Rows.Count > 8 ? -25 : 0;

            Series sMonto = new Series("Ventas ($)")
            {
                ChartType = SeriesChartType.Column,
                IsValueShownAsLabel = true,
                LabelFormat = "${0:N0}",
                Font = new Font("Segoe UI", 8.5F, FontStyle.Bold)
            };

            // 2. Volumen Histórico de Operaciones por Período
            chHistoricoOperaciones.Titles.Clear();
            chHistoricoOperaciones.Titles.Add(new Title(
                $"2. Operaciones de Venta Agrupadas {etiquetaAgrupacion}",
                Docking.Top,
                new Font("Segoe UI", 9.5F, FontStyle.Bold),
                Color.White
            ));
            chHistoricoOperaciones.Series.Clear();
            chHistoricoOperaciones.Legends.Clear();
            chHistoricoOperaciones.ChartAreas[0].AxisX.Interval = 1;
            chHistoricoOperaciones.ChartAreas[0].AxisX.LabelStyle.Angle = dtHist.Rows.Count > 6 ? -25 : 0;

            Series sOperaciones = new Series("Operaciones")
            {
                ChartType = SeriesChartType.SplineArea,
                BorderWidth = 3,
                MarkerStyle = MarkerStyle.Circle,
                MarkerSize = 7,
                IsValueShownAsLabel = true,
                Font = new Font("Segoe UI", 8.5F, FontStyle.Bold)
            };

            decimal sumaTotal = 0;
            int totalPeriodos = dtHist.Rows.Count;

            foreach (DataRow row in dtHist.Rows)
            {
                string periodo = row["Periodo"].ToString();
                decimal totalPeriodo = Convert.ToDecimal(row["TotalVentas"]);
                int cantVentas = Convert.ToInt32(row["CantidadVentas"]);

                sumaTotal += totalPeriodo;
                sMonto.Points.AddXY(periodo, totalPeriodo);
                sOperaciones.Points.AddXY(periodo, cantVentas);
            }

            chHistoricoVentas.Series.Add(sMonto);
            chHistoricoOperaciones.Series.Add(sOperaciones);

            decimal promedioPeriodo = totalPeriodos > 0 ? (sumaTotal / totalPeriodos) : 0;
            cardTotalHistorico.Text = $"Total Histórico:\n${sumaTotal:N2}";
            cardPromedioPeriodo.Text = $"Promedio ({etiquetaAgrupacion}):\n${promedioPeriodo:N2}";

            // 3. Top 5 Productos de Mayor Recaudación en el Horizonte Histórico
            DataTable dtTop5Rec = cn.ProductosMayorRecaudacion(desde, hasta);
            chTop5RecaudacionHist.Series.Clear();
            chTop5RecaudacionHist.Legends.Clear();
            Legend legTop5 = new Legend("Legend1")
            {
                Docking = Docking.Bottom,
                Alignment = StringAlignment.Center,
                Font = new Font("Segoe UI", 7.5F, FontStyle.Regular)
            };
            chTop5RecaudacionHist.Legends.Add(legTop5);

            Series sTop5 = new Series("Top 5 Recaudación")
            {
                ChartType = SeriesChartType.Doughnut,
                IsValueShownAsLabel = true,
                LabelFormat = "${0:N0}",
                Font = new Font("Segoe UI", 8F, FontStyle.Bold)
            };

            int limiteTop5 = Math.Min(dtTop5Rec.Rows.Count, 5);
            for (int i = 0; i < limiteTop5; i++)
            {
                DataRow row = dtTop5Rec.Rows[i];
                string prod = AcortarNombre(row["Producto"].ToString(), 18);
                decimal monto = Convert.ToDecimal(row["Recaudacion"]);
                int idx = sTop5.Points.AddXY(prod, monto);
                sTop5.Points[idx].LegendText = prod;
            }
            chTop5RecaudacionHist.Series.Add(sTop5);

            TemaCapybara.AplicarTema(this);
            EstilizarBotonCambioSeccion();
        }

        private string AcortarNombre(string texto, int maxLen)
        {
            if (string.IsNullOrWhiteSpace(texto)) return "";
            return texto.Length <= maxLen ? texto : texto.Substring(0, maxLen - 2) + "..";
        }
    }
}
