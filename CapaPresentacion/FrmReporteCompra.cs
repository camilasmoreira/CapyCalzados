using CapaEntidad;
using CapaNegocio;
using CapaPresentacion.Utilidades;
using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace CapaPresentacion
{
    public partial class FrmReporteCompra : Form
    {
        public Inicio inicio { get; set; }
        private DataGridView dgvDetalle;
        private Label lblMaestro;
        private Label lblDetalle;
        private ComboBox comboPeriodo;

        public FrmReporteCompra(Inicio inicio)
        {
            InitializeComponent();
            this.inicio = inicio;
            ConfigurarMaestroDetalleYFechas();
        }

        private void ConfigurarMaestroDetalleYFechas()
        {
            // Agregar selector rápido de período (Semana, Mes, Año, Todo) en groupBox1
            label3.Location = new Point(16, 36);
            dtFechaInicio.Location = new Point(16, 58);
            dtFechaInicio.Size = new Size(118, 25);

            label4.Location = new Point(144, 36);
            dtFechaFin.Location = new Point(144, 58);
            dtFechaFin.Size = new Size(118, 25);

            label5.Location = new Point(272, 36);
            comboProveedor.Location = new Point(272, 57);
            comboProveedor.Size = new Size(135, 29);

            Label lblPer = new Label()
            {
                AutoSize = true,
                Text = "Período:",
                Location = new Point(418, 36),
                Font = new Font("Segoe UI", 10F, FontStyle.Bold)
            };
            comboPeriodo = new ComboBox()
            {
                Name = "comboPeriodo",
                DropDownStyle = ComboBoxStyle.DropDownList,
                FlatStyle = FlatStyle.Flat,
                Location = new Point(418, 57),
                Size = new Size(125, 29)
            };
            comboPeriodo.Items.Add(new OpcionesCombo() { Valor = "TODO", Texto = "Todo el historial" });
            comboPeriodo.Items.Add(new OpcionesCombo() { Valor = "SEMANA", Texto = "Última Semana" });
            comboPeriodo.Items.Add(new OpcionesCombo() { Valor = "MES", Texto = "Último Mes" });
            comboPeriodo.Items.Add(new OpcionesCombo() { Valor = "ANIO", Texto = "Último Año" });
            comboPeriodo.DisplayMember = "Texto";
            comboPeriodo.ValueMember = "Valor";
            comboPeriodo.SelectedIndex = 0;
            comboPeriodo.SelectedIndexChanged += ComboPeriodo_SelectedIndexChanged;

            rjButton2.Location = new Point(555, 51);
            groupBox1.Controls.Add(lblPer);
            groupBox1.Controls.Add(comboPeriodo);

            // Configurar Tabla Principal (Maestro): Solo encabezado único de cada factura de compra
            lblMaestro = new Label()
            {
                AutoSize = true,
                Text = "1. Encabezados de Facturas de Compra (Seleccione una fila para ver el detalle abajo):",
                Location = new Point(107, 250),
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold)
            };

            dgvData.Location = new Point(107, 272);
            dgvData.Size = new Size(710, 145);
            dgvData.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvData.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvData.CellClick += DgvData_CellClick;
            dgvData.SelectionChanged += DgvData_SelectionChanged;

            // Configurar Segunda Tabla Abajo (Detalle de Productos de la Compra Seleccionada)
            lblDetalle = new Label()
            {
                AutoSize = true,
                Text = "2. Detalle de Productos de la Compra Seleccionada:",
                Location = new Point(107, 424),
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold)
            };

            dgvDetalle = new DataGridView()
            {
                Name = "dgvDetalle",
                Location = new Point(107, 446),
                Size = new Size(710, 145),
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                AllowUserToResizeRows = false,
                ReadOnly = true,
                MultiSelect = false,
                RowHeadersVisible = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            };
            dgvDetalle.Columns.Add(new DataGridViewTextBoxColumn() { Name = "CodigoProducto", HeaderText = "Código", FillWeight = 14 });
            dgvDetalle.Columns.Add(new DataGridViewTextBoxColumn() { Name = "NombreProducto", HeaderText = "Producto", FillWeight = 26 });
            dgvDetalle.Columns.Add(new DataGridViewTextBoxColumn() { Name = "Categoria", HeaderText = "Categoría", FillWeight = 20 });
            dgvDetalle.Columns.Add(new DataGridViewTextBoxColumn() { Name = "PrecioCompra", HeaderText = "Precio Compra ($)", FillWeight = 14 });
            dgvDetalle.Columns.Add(new DataGridViewTextBoxColumn() { Name = "PrecioVenta", HeaderText = "Precio Venta ($)", FillWeight = 14 });
            dgvDetalle.Columns.Add(new DataGridViewTextBoxColumn() { Name = "Cantidad", HeaderText = "Cant.", FillWeight = 10 });
            dgvDetalle.Columns.Add(new DataGridViewTextBoxColumn() { Name = "SubTotal", HeaderText = "SubTotal ($)", FillWeight = 14 });
            dgvDetalle.CellPainting += (s, ev) => TemaCapybara.ModernDgv_CellPainting(s, ev);

            // Reubicar barra de filtro superior
            label10.Location = new Point(104, 178);
            label10.Size = new Size(713, 62);
            BtnGuardar2.Location = new Point(120, 189);
            label11.Location = new Point(280, 200);
            comboBusqueda.Location = new Point(358, 197);
            txtBusqueda.Location = new Point(512, 192);
            rjButton4.Location = new Point(692, 192);
            rjButton5.Location = new Point(752, 192);

            this.Controls.Add(lblMaestro);
            this.Controls.Add(lblDetalle);
            this.Controls.Add(dgvDetalle);
            lblMaestro.BringToFront();
            lblDetalle.BringToFront();
            dgvDetalle.BringToFront();
        }

        private void ComboPeriodo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!(comboPeriodo.SelectedItem is OpcionesCombo oc)) return;
            string val = oc.Valor.ToString();
            DateTime hoy = DateTime.Now.Date;
            dtFechaFin.Value = hoy.AddDays(1);

            if (val == "SEMANA")
                dtFechaInicio.Value = hoy.AddDays(-7);
            else if (val == "MES")
                dtFechaInicio.Value = hoy.AddMonths(-1);
            else if (val == "ANIO")
                dtFechaInicio.Value = hoy.AddYears(-1);
            else
                dtFechaInicio.Value = hoy.AddYears(-3);

            CargarComprasMaestro();
        }

        private void FrmReporteCompra_Load(object sender, EventArgs e)
        {
            TemaCapybara.AplicarTema(this);

            // En la tabla principal (Maestro) solo mostrar el encabezado único de la compra
            UsuarioRegistro.Visible = true;
            UsuarioRegistro.HeaderText = "Responsable";
            CodigoProducto.Visible = false;
            NombreProducto.Visible = false;
            Categoria.Visible = false;
            PrecioCompra.Visible = false;
            PrecioVenta.Visible = false;
            Cantidad.Visible = false;
            SubTotal.Visible = false;
            btnDetalle.HeaderText = "Ticket";
            btnDetalle.Width = 50;
            btnDetalle.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;

            comboProveedor.Items.Clear();
            List<Proveedor> listaProveedores = new CN_Proveedor().Listar();
            comboProveedor.Items.Add(new OpcionesCombo() { Valor = 0, Texto = "Todos" });
            foreach (Proveedor item in listaProveedores)
            {
                comboProveedor.Items.Add(new OpcionesCombo() { Valor = item.IdProveedor, Texto = item.RazonSocial });
            }
            comboProveedor.DisplayMember = "Texto";
            comboProveedor.ValueMember = "Valor";
            comboProveedor.SelectedIndex = 0;

            comboBusqueda.Items.Clear();
            foreach (DataGridViewColumn columna in dgvData.Columns)
            {
                if (columna.Visible && columna.Name != "btnDetalle")
                {
                    comboBusqueda.Items.Add(new OpcionesCombo() { Valor = columna.Name, Texto = columna.HeaderText });
                }
            }
            comboBusqueda.DisplayMember = "Texto";
            comboBusqueda.ValueMember = "Valor";
            if (comboBusqueda.Items.Count > 0)
                comboBusqueda.SelectedIndex = 0;

            dtFechaInicio.Value = DateTime.Now.AddYears(-2);
            dtFechaFin.Value = DateTime.Now.AddDays(1);
            CargarComprasMaestro();
        }

        private void CargarComprasMaestro()
        {
            int idproveedor = comboProveedor.SelectedItem is OpcionesCombo ocProv ? Convert.ToInt32(ocProv.Valor) : 0;
            List<Compra> listaCompras = new CN_Compra().ListarCompras(dtFechaInicio.Value, dtFechaFin.Value, idproveedor);

            dgvData.Rows.Clear();
            dgvDetalle.Rows.Clear();

            foreach (Compra c in listaCompras)
            {
                dgvData.Rows.Add(new object[]
                {
                    c.FechaRegistro,
                    c.TipoDocumento,
                    c.NumeroDocumento,
                    c.MontoTotal.ToString("0.00"),
                    c.oUsuario != null ? c.oUsuario.NombreCompleto : "",
                    c.oProveedor != null ? c.oProveedor.Documento : "",
                    c.oProveedor != null ? c.oProveedor.RazonSocial : "",
                    "",
                    "",
                    "",
                    "",
                    "",
                    "",
                    "",
                    ""
                });
            }

            dgvData.ClearSelection();
        }

        private void DgvData_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                CargarDetalleCompra(e.RowIndex);
            }
        }

        private void DgvData_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvData.Focused && dgvData.SelectedRows.Count > 0)
            {
                CargarDetalleCompra(dgvData.SelectedRows[0].Index);
            }
        }

        private void CargarDetalleCompra(int rowIndex)
        {
            if (rowIndex < 0 || rowIndex >= dgvData.Rows.Count) return;

            string numeroDoc = dgvData.Rows[rowIndex].Cells["NumeroDocumento"].Value?.ToString() ?? "";
            if (string.IsNullOrWhiteSpace(numeroDoc)) return;

            Compra oCompra = new CN_Compra().obtenerCompra(numeroDoc);
            dgvDetalle.Rows.Clear();
            if (oCompra != null && oCompra.oDetalleCompra != null)
            {
                foreach (DetalleCompra dc in oCompra.oDetalleCompra)
                {
                    dgvDetalle.Rows.Add(new object[]
                    {
                        dc.oProducto?.Codigo ?? "",
                        dc.oProducto?.Nombre ?? "",
                        dc.oProducto?.oCategoria?.Descripcion ?? dc.oProducto?.Descripcion ?? "",
                        dc.PrecioCompra.ToString("0.00"),
                        dc.PrecioVenta.ToString("0.00"),
                        dc.Cantidad,
                        dc.MontoTotal.ToString("0.00")
                    });
                }
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            CargarComprasMaestro();
        }

        private void btnBuscar2_Click(object sender, EventArgs e)
        {
            if (comboBusqueda.SelectedItem == null) return;
            string columnaFiltro = ((OpcionesCombo)comboBusqueda.SelectedItem).Valor.ToString();

            if (dgvData.Rows.Count > 0)
            {
                dgvDetalle.Rows.Clear();
                foreach (DataGridViewRow row in dgvData.Rows)
                {
                    string val = row.Cells[columnaFiltro].Value?.ToString() ?? "";
                    row.Visible = val.Trim().ToUpper().Contains(txtBusqueda.Texts.Trim().ToUpper());
                }
            }
        }

        private void btnLimpiarBusqueda_Click(object sender, EventArgs e)
        {
            txtBusqueda.Texts = "";
            foreach (DataGridViewRow row in dgvData.Rows)
            {
                row.Visible = true;
            }
        }

        private void btnExportar_Click(object sender, EventArgs e)
        {
            ExportarExcel();
        }

        private void BtnGuardar2_Click(object sender, EventArgs e)
        {
            ExportarExcel();
        }

        private void ExportarExcel()
        {
            if (dgvData.Rows.Count < 1)
            {
                MessageBox.Show("No hay datos para exportar", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            DataTable dt = new DataTable();
            foreach (DataGridViewColumn colum in dgvData.Columns)
            {
                if (colum.Visible && colum.Name != "btnDetalle")
                {
                    dt.Columns.Add(colum.HeaderText, typeof(string));
                }
            }

            foreach (DataGridViewRow rows in dgvData.Rows)
            {
                if (rows.Visible)
                {
                    List<object> valores = new List<object>();
                    foreach (DataGridViewColumn colum in dgvData.Columns)
                    {
                        if (colum.Visible && colum.Name != "btnDetalle")
                        {
                            valores.Add(rows.Cells[colum.Index].Value?.ToString() ?? "");
                        }
                    }
                    dt.Rows.Add(valores.ToArray());
                }
            }

            SaveFileDialog savefile = new SaveFileDialog
            {
                FileName = string.Format("ReporteCompras_{0}.xlsx", DateTime.Now.ToString("ddMMyyyyHHmmss")),
                Filter = "Excel Files | *.xlsx"
            };

            if (savefile.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    XLWorkbook wb = new XLWorkbook();
                    var hoja = wb.Worksheets.Add(dt, "Informe");
                    hoja.ColumnsUsed().AdjustToContents();
                    wb.SaveAs(savefile.FileName);
                    MessageBox.Show("Reporte Generado", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch
                {
                    MessageBox.Show("Error al generar el informe", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }

        private void txtBusqueda_KeyPress(object sender, KeyPressEventArgs e)
        {
            bool esControl = Char.IsControl(e.KeyChar);
            bool longitudPermitida = txtBusqueda.Texts.Length < 80;
            e.Handled = !(esControl || longitudPermitida);
        }

        private void dgvData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dgvData.Columns[e.ColumnIndex].Name == "btnDetalle")
            {
                string doc = dgvData.Rows[e.RowIndex].Cells["NumeroDocumento"].Value.ToString();
                inicio.inicio(new FrmDetalleCompra(doc, inicio));
                this.Close();
            }
        }

        private void dgvData_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            TemaCapybara.ModernDgv_CellPainting(sender, e);
        }
    }
}
