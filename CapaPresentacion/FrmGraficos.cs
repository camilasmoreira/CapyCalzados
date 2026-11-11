using CapaEntidad;
using CapaNegocio;
using CapaPresentacion.Modales;
using CapaPresentacion.Utilidades;
using CustomControls.RJControls;
using FontAwesome.Sharp;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace CapaPresentacion
{
    // Módulo de Estadísticas Analíticas - Sección: Visión Detallada con Filtros
    public partial class FrmGraficos : Form
    {
        public Inicio InicioRef { get; set; }

        private ComboBox comboPeriodo;
        private DateTimePicker dtFechaDesde;
        private DateTimePicker dtFechaHasta;
        private RJButton btnFiltrar;
        private RJButton btnIrHistorico;

        private Chart chMayorRecaudacion;
        private Chart chEvolucionVentas;
        private Chart chRankingVendedores;
        private Chart chDiasMovimiento;

        public FrmGraficos(Inicio inicio = null)
        {
            InitializeComponent();
            InicioRef = inicio;
            ConfigurarDashboardEstadisticas();
        }

        private void ConfigurarDashboardEstadisticas()
        {
            // 1. Configurar Panel Superior: Visión Detallada con Filtros (Períodos Recientes y Fechas Determinadas) + KPIs
            groupBox1.Text = "Visión Detallada con Filtros (Fechas Específicas y Períodos Recientes)";
            groupBox1.Location = new Point(45, 20);
            groupBox1.Size = new Size(830, 112);

            rjButton1.Visible = false;

            Label lblPeriodo = new Label()
            {
                AutoSize = true,
                Text = "Período Reciente:",
                Location = new Point(16, 22),
                Font = new Font("Segoe UI", 9F, FontStyle.Bold)
            };
            comboPeriodo = new ComboBox()
            {
                Name = "comboPeriodo",
                DropDownStyle = ComboBoxStyle.DropDownList,
                FlatStyle = FlatStyle.Flat,
                Location = new Point(16, 43),
                Size = new Size(140, 28)
            };
            comboPeriodo.Items.Add(new OpcionesCombo() { Valor = "MES", Texto = "Último Mes" });
            comboPeriodo.Items.Add(new OpcionesCombo() { Valor = "QUINCE", Texto = "Últimos 15 Días" });
            comboPeriodo.Items.Add(new OpcionesCombo() { Valor = "SEMANA", Texto = "Última Semana" });
            comboPeriodo.Items.Add(new OpcionesCombo() { Valor = "PERSONALIZADO", Texto = "Fechas Determinadas" });
            comboPeriodo.DisplayMember = "Texto";
            comboPeriodo.ValueMember = "Valor";
            comboPeriodo.SelectedIndex = 0;
            comboPeriodo.SelectedIndexChanged += ComboPeriodo_SelectedIndexChanged;

            Label lblDesde = new Label()
            {
                AutoSize = true,
                Text = "Desde:",
                Location = new Point(166, 22),
                Font = new Font("Segoe UI", 9F, FontStyle.Bold)
            };
            dtFechaDesde = new DateTimePicker()
            {
                Format = DateTimePickerFormat.Short,
                Location = new Point(166, 44),
                Size = new Size(102, 26),
                Value = DateTime.Now.Date.AddMonths(-1)
            };

            Label lblHasta = new Label()
            {
                AutoSize = true,
                Text = "Hasta:",
                Location = new Point(276, 22),
                Font = new Font("Segoe UI", 9F, FontStyle.Bold)
            };
            dtFechaHasta = new DateTimePicker()
            {
                Format = DateTimePickerFormat.Short,
                Location = new Point(276, 44),
                Size = new Size(102, 26),
                Value = DateTime.Now.Date.AddDays(1)
            };

            btnFiltrar = new RJButton()
            {
                Name = "btnFiltrar",
                Text = "  Filtrar",
                Location = new Point(386, 38),
                Size = new Size(105, 34)
            };
            btnFiltrar.Click += (s, ev) => ActualizarTodosLosGraficos();

            // Botón para ir a la sección (Form) independiente de Visión Histórica General (por Semana, Mes, etc.)
            btnIrHistorico = new RJButton()
            {
                Name = "btnIrHistorico",
                Text = "  Visión Histórica General",
                Location = new Point(16, 78),
                Size = new Size(475, 28)
            };
            btnIrHistorico.Click += BtnIrHistorico_Click;

            // Tarjetas KPI compactas a la derecha del filtro
            Ventas.Location = new Point(502, 22);
            Ventas.Size = new Size(155, 80);
            Ventas.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            Ventas.TextAlign = ContentAlignment.MiddleCenter;

            Compras.Location = new Point(664, 22);
            Compras.Size = new Size(155, 80);
            Compras.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            Compras.TextAlign = ContentAlignment.MiddleCenter;

            groupBox1.Controls.Add(lblPeriodo);
            groupBox1.Controls.Add(comboPeriodo);
            groupBox1.Controls.Add(lblDesde);
            groupBox1.Controls.Add(dtFechaDesde);
            groupBox1.Controls.Add(lblHasta);
            groupBox1.Controls.Add(dtFechaHasta);
            groupBox1.Controls.Add(btnFiltrar);
            groupBox1.Controls.Add(btnIrHistorico);

            // 2. Configurar los Gráficos de la Visión Detallada con limitación Top 5:
            // Fila Superior (3 gráficos):
            // (1) Top 5 Productos más vendidos (reutilizamos chVentas)
            chVentas.Location = new Point(45, 140);
            chVentas.Size = new Size(270, 222);
            chVentas.Titles.Clear();
            chVentas.Titles.Add(new Title("1. Top 5 Productos Más Vendidos", Docking.Top, new Font("Segoe UI", 9.5F, FontStyle.Bold), Color.White));
            chVentas.Legends.Clear();

            // (2) Top 5 Productos de mayor recaudación (reutilizamos chCompras)
            chCompras.Location = new Point(325, 140);
            chCompras.Size = new Size(270, 222);
            chCompras.Titles.Clear();
            chCompras.Titles.Add(new Title("2. Top 5 Mayor Recaudación ($)", Docking.Top, new Font("Segoe UI", 9.5F, FontStyle.Bold), Color.White));

            // (3) Ranking de Vendedores
            chRankingVendedores = CrearGraficoBase("chRankingVendedores", "3. Ranking de Vendedores ($)", new Point(605, 140), new Size(270, 222));
            this.Controls.Add(chRankingVendedores);
            chRankingVendedores.BringToFront();

            // Fila Inferior (2 gráficos anchos):
            // (4) Evolución Detallada de Ventas por Fecha (responde a los filtros de fechas específicas / recientes)
            chEvolucionVentas = CrearGraficoBase("chEvolucionVentas", "4. Evolución Detallada de Ventas por Fecha ($)", new Point(45, 370), new Size(410, 225));
            this.Controls.Add(chEvolucionVentas);
            chEvolucionVentas.BringToFront();

            // (5) Días de Mayor Movimiento
            chDiasMovimiento = CrearGraficoBase("chDiasMovimiento", "5. Días de Mayor Movimiento (Ventas por Día)", new Point(465, 370), new Size(410, 225));
            this.Controls.Add(chDiasMovimiento);
            chDiasMovimiento.BringToFront();

            chVentas.BringToFront();
            chCompras.BringToFront();
            groupBox1.BringToFront();
        }

        private void BtnIrHistorico_Click(object sender, EventArgs e)
        {
            if (InicioRef != null)
            {
                InicioRef.AbrirGraficosHistorico();
            }
            else
            {
                using (var frmHist = new FrmGraficosHistorico(null))
                {
                    frmHist.StartPosition = FormStartPosition.CenterParent;
                    frmHist.ShowDialog(this);
                }
            }
        }

        private void EstilizarBotonHistorico()
        {
            if (btnIrHistorico != null)
            {
                btnIrHistorico.Text = "  Ver Sección: Visión Histórica General (Por Semana / Mes / Año)";
                btnIrHistorico.Image = IconChar.ChartLine.ToBitmap(TemaCapybara.BotonTexto, 15);
                btnIrHistorico.BackColor = TemaCapybara.BotonPasto;
                btnIrHistorico.BackgroundColor = TemaCapybara.BotonPasto;
                btnIrHistorico.TextImageRelation = TextImageRelation.ImageBeforeText;
                btnIrHistorico.ImageAlign = ContentAlignment.MiddleLeft;
                btnIrHistorico.TextAlign = ContentAlignment.MiddleCenter;
                btnIrHistorico.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
            }
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
            area.AxisX.LabelStyle.Font = new Font("Segoe UI", 8F, FontStyle.Regular);
            area.AxisY.LabelStyle.Font = new Font("Segoe UI", 8F, FontStyle.Regular);
            chart.ChartAreas.Add(area);

            Title title = new Title(titulo, Docking.Top, new Font("Segoe UI", 9.5F, FontStyle.Bold), Color.White);
            chart.Titles.Add(title);
            return chart;
        }

        private void ComboPeriodo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!(comboPeriodo.SelectedItem is OpcionesCombo oc)) return;
            string val = oc.Valor.ToString();
            DateTime hoy = DateTime.Now.Date;
            dtFechaHasta.Value = hoy.AddDays(1);

            if (val == "SEMANA")
                dtFechaDesde.Value = hoy.AddDays(-7);
            else if (val == "QUINCE")
                dtFechaDesde.Value = hoy.AddDays(-15);
            else if (val == "MES")
                dtFechaDesde.Value = hoy.AddMonths(-1);

            ActualizarTodosLosGraficos();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {
        }

        private void FrmGraficos_Load(object sender, EventArgs e)
        {
            TemaCapybara.AplicarTema(this);
            EstilizarBotonHistorico();
            ActualizarTodosLosGraficos();
        }

        private void ActualizarTodosLosGraficos()
        {
            DateTime desde = dtFechaDesde.Value.Date;
            DateTime hasta = dtFechaHasta.Value.Date;
            CN_Grafico cn = new CN_Grafico();

            // Actualizar KPIs por fecha
            Grafico objFechas = cn.DatosFechas(desde, hasta);
            decimal totV = 0;
            decimal totC = 0;
            decimal.TryParse(objFechas.totalVentas, out totV);
            decimal.TryParse(objFechas.Compras, out totC);
            Ventas.Text = $"Total Ventas:\n${totV:N2}";
            Compras.Text = $"Total Compras:\n${totC:N2}";

            // 1. Top 5 Productos Más Vendidos (Unidades)
            DataTable dtMasVendidos = cn.ProductosMasVendidos(desde, hasta);
            chVentas.Series.Clear();
            chVentas.ChartAreas[0].AxisX.Interval = 1;
            chVentas.ChartAreas[0].AxisX.LabelStyle.Angle = -20;
            Series sVendidos = new Series("Unidades")
            {
                ChartType = SeriesChartType.Column,
                IsValueShownAsLabel = true,
                Font = new Font("Segoe UI", 8.5F, FontStyle.Bold)
            };
            int topVendidos = Math.Min(dtMasVendidos.Rows.Count, 5);
            for (int i = 0; i < topVendidos; i++)
            {
                DataRow row = dtMasVendidos.Rows[i];
                string prod = AcortarNombre(row["Producto"].ToString(), 16);
                int cant = Convert.ToInt32(row["Cantidad"]);
                sVendidos.Points.AddXY(prod, cant);
            }
            chVentas.Series.Add(sVendidos);

            // 2. Top 5 Productos de Mayor Recaudación ($)
            DataTable dtRecaudacion = cn.ProductosMayorRecaudacion(desde, hasta);
            chCompras.Series.Clear();
            chCompras.Legends.Clear();
            Legend legRec = new Legend("Legend1")
            {
                Docking = Docking.Bottom,
                Alignment = StringAlignment.Center,
                Font = new Font("Segoe UI", 7.5F, FontStyle.Regular)
            };
            chCompras.Legends.Add(legRec);
            Series sRecaudacion = new Series("Recaudacion")
            {
                ChartType = SeriesChartType.Doughnut,
                IsValueShownAsLabel = true,
                LabelFormat = "${0:N0}",
                Font = new Font("Segoe UI", 8F, FontStyle.Bold)
            };
            int topRecaudacion = Math.Min(dtRecaudacion.Rows.Count, 5);
            for (int i = 0; i < topRecaudacion; i++)
            {
                DataRow row = dtRecaudacion.Rows[i];
                string prod = AcortarNombre(row["Producto"].ToString(), 18);
                decimal monto = Convert.ToDecimal(row["Recaudacion"]);
                int idx = sRecaudacion.Points.AddXY(prod, monto);
                sRecaudacion.Points[idx].LegendText = prod;
            }
            chCompras.Series.Add(sRecaudacion);

            // 3. Ranking de Vendedores ($)
            DataTable dtVendedores = cn.RankingVendedores(desde, hasta);
            chRankingVendedores.Series.Clear();
            chRankingVendedores.ChartAreas[0].AxisX.Interval = 1;
            chRankingVendedores.ChartAreas[0].AxisX.LabelStyle.Angle = -15;
            Series sVendedores = new Series("Ventas ($)")
            {
                ChartType = SeriesChartType.Column,
                IsValueShownAsLabel = true,
                LabelFormat = "${0:N0}",
                Font = new Font("Segoe UI", 8F, FontStyle.Bold)
            };
            foreach (DataRow row in dtVendedores.Rows)
            {
                string vend = AcortarNombre(row["Vendedor"].ToString(), 15);
                int cantOp = Convert.ToInt32(row["CantidadVentas"]);
                decimal totalRec = Convert.ToDecimal(row["TotalRecaudado"]);
                int idx = sVendedores.Points.AddXY($"{vend} ({cantOp})", totalRec);
            }
            chRankingVendedores.Series.Add(sVendedores);

            // 4. Evolución Detallada de Ventas por Fecha ($)
            DataTable dtEvolucion = cn.EvolucionVentas(desde, hasta);
            chEvolucionVentas.Series.Clear();
            chEvolucionVentas.ChartAreas[0].AxisX.Interval = 1;
            chEvolucionVentas.ChartAreas[0].AxisX.LabelStyle.Angle = -30;
            Series sEvolucion = new Series("Evolución ($)")
            {
                ChartType = SeriesChartType.SplineArea,
                BorderWidth = 3,
                MarkerStyle = MarkerStyle.Circle,
                MarkerSize = 7,
                IsValueShownAsLabel = true,
                LabelFormat = "${0:N0}",
                Font = new Font("Segoe UI", 7.5F, FontStyle.Bold)
            };
            foreach (DataRow row in dtEvolucion.Rows)
            {
                string fecha = row["Fecha"].ToString().Substring(0, 5); // dd/MM
                decimal totalDia = Convert.ToDecimal(row["TotalVentas"]);
                sEvolucion.Points.AddXY(fecha, totalDia);
            }
            chEvolucionVentas.Series.Add(sEvolucion);

            // 5. Días de Mayor Movimiento (Lunes a Domingo)
            DataTable dtDias = cn.DiasMayorMovimiento(desde, hasta);
            chDiasMovimiento.Series.Clear();
            chDiasMovimiento.ChartAreas[0].AxisX.Interval = 1;
            Series sDias = new Series("Operaciones")
            {
                ChartType = SeriesChartType.Column,
                IsValueShownAsLabel = true,
                Font = new Font("Segoe UI", 8F, FontStyle.Bold)
            };
            foreach (DataRow row in dtDias.Rows)
            {
                string dia = row["DiaSemana"].ToString();
                int cantVentas = Convert.ToInt32(row["CantidadVentas"]);
                decimal montoDia = Convert.ToDecimal(row["TotalVentas"]);
                int idx = sDias.Points.AddXY(dia, montoDia);
                sDias.Points[idx].Label = $"${montoDia:N0} ({cantVentas} v.)";
            }
            chDiasMovimiento.Series.Add(sDias);

            // Aplicar paleta Capybara a todos los gráficos
            TemaCapybara.AplicarTema(this);
            EstilizarBotonHistorico();
        }

        private string AcortarNombre(string texto, int maxLen)
        {
            if (string.IsNullOrWhiteSpace(texto)) return "";
            return texto.Length <= maxLen ? texto : texto.Substring(0, maxLen - 2) + "..";
        }

        private void rjButton1_Click(object sender, EventArgs e)
        {
            ActualizarTodosLosGraficos();
        }
    }
}
