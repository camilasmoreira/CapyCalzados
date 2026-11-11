using CapaEntidad;
using CapaNegocio;
using CapaPresentacion.Utilidades;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace CapaPresentacion
{
    // Formulario Maestro-Detalle de Ventas con Filtros de Fecha y Ticket Comprobante de Venta
    public partial class FrmDetalleVenta : Form
    {
        public Inicio Inicio { get; set; }
        public string doc { get; set; }
        private Usuario usuarioActual;

        private bool EsVendedor =>
            usuarioActual != null && usuarioActual.oRol != null &&
            (usuarioActual.oRol.IdRol == 2 || string.Equals(usuarioActual.oRol.Descripcion, "Vendedor", StringComparison.OrdinalIgnoreCase));

        private DataGridView dgvFacturas;
        private DateTimePicker dtFechaInicio;
        private DateTimePicker dtFechaFin;
        private ComboBox comboPeriodo;
        private Label lblMaestro;
        private Label lblDetalle;
        private List<Venta> _listaVentasActual = new List<Venta>();

        public FrmDetalleVenta(Inicio inicio)
        {
            InitializeComponent();
            Inicio = inicio;
            usuarioActual = inicio?.UsuarioActual;
            Inicio.pintar();
            ConfigurarInterfazMaestroDetalle();
        }

        public FrmDetalleVenta(string numero, Inicio inicio)
        {
            InitializeComponent();
            Inicio = inicio;
            usuarioActual = inicio?.UsuarioActual;
            Inicio.pintar();
            ConfigurarInterfazMaestroDetalle();
            this.doc = numero;
        }

        private void ConfigurarInterfazMaestroDetalle()
        {
            // 1. Panel superior de Búsqueda y Filtros (Fecha + N° Factura + Botón Buscar)
            groupBox2.Text = "Búsqueda y Filtros de Ventas";
            groupBox2.Location = new Point(55, 22);
            groupBox2.Size = new Size(810, 78);

            label1.Text = "N° Factura:";
            label1.Location = new Point(14, 18);
            txtBusqueda.Location = new Point(14, 37);
            txtBusqueda.Size = new Size(115, 32);
            txtBusqueda.PlaceholderText = "Todos / N°";

            Label lblPeriodo = new Label()
            {
                AutoSize = true,
                Text = "Período:",
                Location = new Point(140, 18),
                Font = new System.Drawing.Font("Segoe UI", 9F, FontStyle.Bold)
            };
            comboPeriodo = new ComboBox()
            {
                Name = "comboPeriodo",
                DropDownStyle = ComboBoxStyle.DropDownList,
                FlatStyle = FlatStyle.Flat,
                Location = new Point(140, 39),
                Size = new Size(125, 28)
            };
            comboPeriodo.Items.Add(new OpcionesCombo() { Valor = "TODO", Texto = "Todo el historial" });
            comboPeriodo.Items.Add(new OpcionesCombo() { Valor = "SEMANA", Texto = "Última Semana" });
            comboPeriodo.Items.Add(new OpcionesCombo() { Valor = "MES", Texto = "Último Mes" });
            comboPeriodo.Items.Add(new OpcionesCombo() { Valor = "ANIO", Texto = "Último Año" });
            comboPeriodo.DisplayMember = "Texto";
            comboPeriodo.ValueMember = "Valor";
            comboPeriodo.SelectedIndex = 0;
            comboPeriodo.SelectedIndexChanged += ComboPeriodo_SelectedIndexChanged;

            Label lblDesde = new Label()
            {
                AutoSize = true,
                Text = "Desde:",
                Location = new Point(275, 18),
                Font = new System.Drawing.Font("Segoe UI", 9F, FontStyle.Bold)
            };
            dtFechaInicio = new DateTimePicker()
            {
                Format = DateTimePickerFormat.Short,
                Location = new Point(275, 40),
                Size = new Size(105, 26),
                Value = DateTime.Now.AddYears(-2)
            };

            Label lblHasta = new Label()
            {
                AutoSize = true,
                Text = "Hasta:",
                Location = new Point(390, 18),
                Font = new System.Drawing.Font("Segoe UI", 9F, FontStyle.Bold)
            };
            dtFechaFin = new DateTimePicker()
            {
                Format = DateTimePickerFormat.Short,
                Location = new Point(390, 40),
                Size = new Size(105, 26),
                Value = DateTime.Now.AddDays(1)
            };

            rjButton1.Text = "  Buscar";
            rjButton1.Location = new Point(510, 28);
            rjButton1.Size = new Size(135, 38);

            BtnLimpiar2.Text = "  Limpiar";
            BtnLimpiar2.Location = new Point(655, 28);
            BtnLimpiar2.Size = new Size(135, 38);

            groupBox2.Controls.Add(lblPeriodo);
            groupBox2.Controls.Add(comboPeriodo);
            groupBox2.Controls.Add(lblDesde);
            groupBox2.Controls.Add(dtFechaInicio);
            groupBox2.Controls.Add(lblHasta);
            groupBox2.Controls.Add(dtFechaFin);

            // Ocultar cajas redundantes de cliente y compactar encabezado seleccionado
            groupBox3.Visible = false;
            groupBox1.Text = "Encabezado de la Factura Seleccionada";
            groupBox1.Location = new Point(55, 290);
            groupBox1.Size = new Size(810, 74);

            label2.Text = "Fecha:";
            label2.Location = new Point(14, 16);
            txtFecha.Location = new Point(14, 34);
            txtFecha.Size = new Size(130, 32);

            label3.Text = "Tipo Factura:";
            label3.Location = new Point(160, 16);
            txtTipoDocumento.Location = new Point(160, 34);
            txtTipoDocumento.Size = new Size(140, 32);

            label6.Text = "Vendedor:";
            label6.Location = new Point(315, 16);
            txtUsuario.Location = new Point(315, 34);
            txtUsuario.Size = new Size(185, 32);

            label11.Text = "Forma de Pago:";
            label11.Location = new Point(515, 16);
            txtDocV.Location = new Point(515, 34);
            txtDocV.Size = new Size(155, 32);

            txtDocumento.Visible = true;
            txtDocumento.IsReadOnly = true;
            Label lblNumDoc = new Label()
            {
                AutoSize = true,
                Text = "N° Factura:",
                Location = new Point(682, 16),
                Font = new System.Drawing.Font("Segoe UI", 9F, FontStyle.Bold)
            };
            txtDocumento.Location = new Point(682, 34);
            txtDocumento.Size = new Size(112, 32);
            groupBox1.Controls.Add(lblNumDoc);
            groupBox1.Controls.Add(txtDocumento);

            // 2. Crear DataGrid Principal (Maestro - Encabezados Únicos de Factura)
            lblMaestro = new Label()
            {
                AutoSize = true,
                Text = "1. Listado de Facturas de Venta (Seleccione una fila para ver sus productos):",
                Location = new Point(55, 106),
                Font = new System.Drawing.Font("Segoe UI", 9.75F, FontStyle.Bold)
            };

            dgvFacturas = new DataGridView()
            {
                Name = "dgvFacturas",
                Location = new Point(55, 128),
                Size = new Size(810, 155),
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                AllowUserToResizeRows = false,
                ReadOnly = true,
                MultiSelect = false,
                RowHeadersVisible = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            };

            var colSel = new DataGridViewButtonColumn()
            {
                Name = "btnseleccionar",
                HeaderText = "",
                Width = 36,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            };
            dgvFacturas.Columns.Add(colSel);
            dgvFacturas.Columns.Add(new DataGridViewTextBoxColumn() { Name = "IdVenta", HeaderText = "IdVenta", Visible = false });
            dgvFacturas.Columns.Add(new DataGridViewTextBoxColumn() { Name = "FechaRegistro", HeaderText = "Fecha", FillWeight = 16 });
            dgvFacturas.Columns.Add(new DataGridViewTextBoxColumn() { Name = "TipoDocumento", HeaderText = "Tipo Factura", FillWeight = 16 });
            dgvFacturas.Columns.Add(new DataGridViewTextBoxColumn() { Name = "NumeroDocumento", HeaderText = "N° Factura", FillWeight = 16 });
            dgvFacturas.Columns.Add(new DataGridViewTextBoxColumn() { Name = "FormaPago", HeaderText = "Forma de Pago", FillWeight = 20 });
            dgvFacturas.Columns.Add(new DataGridViewTextBoxColumn() { Name = "Vendedor", HeaderText = "Vendedor", FillWeight = 22 });
            dgvFacturas.Columns.Add(new DataGridViewTextBoxColumn() { Name = "MontoTotal", HeaderText = "Monto Total ($)", FillWeight = 16 });

            dgvFacturas.CellPainting += (s, ev) => TemaCapybara.ModernDgv_CellPainting(s, ev);
            dgvFacturas.CellClick += DgvFacturas_CellClick;
            dgvFacturas.SelectionChanged += DgvFacturas_SelectionChanged;

            // 3. Configurar Segunda Tabla Abajo (Detalle de Productos de la Factura Seleccionada)
            lblDetalle = new Label()
            {
                AutoSize = true,
                Text = "2. Detalle de Productos de la Factura Seleccionada:",
                Location = new Point(55, 370),
                Font = new System.Drawing.Font("Segoe UI", 9.75F, FontStyle.Bold)
            };

            dgvData.Location = new Point(55, 392);
            dgvData.Size = new Size(810, 130);
            dgvData.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvData.CellPainting -= dgvData_CellPainting;
            dgvData.CellPainting += dgvData_CellPainting;

            // Totales y botón Ticket PDF abajo
            label7.Location = new Point(58, 530);
            txtPago.Location = new Point(55, 550);
            txtPago.Size = new Size(140, 34);

            label8.Location = new Point(225, 530);
            txtCambio.Location = new Point(222, 550);
            txtCambio.Size = new Size(140, 34);

            label9.Location = new Point(392, 530);
            txtTotal.Location = new Point(389, 550);
            txtTotal.Size = new Size(150, 34);

            BtnGuardar2.Text = "  Ticket PDF";
            BtnGuardar2.Location = new Point(665, 544);
            BtnGuardar2.Size = new Size(195, 42);

            this.Controls.Add(lblMaestro);
            this.Controls.Add(dgvFacturas);
            this.Controls.Add(lblDetalle);
            lblMaestro.BringToFront();
            dgvFacturas.BringToFront();
            lblDetalle.BringToFront();
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

            CargarMaestroVentas();
        }

        private void FrmDetalleVenta_Load(object sender, EventArgs e)
        {
            TemaCapybara.AplicarTema(this);
            rjButton1.Text = "  Buscar";
            BtnGuardar2.Text = "  Ticket PDF";

            if (!string.IsNullOrWhiteSpace(this.doc))
            {
                txtBusqueda.Texts = this.doc;
            }

            CargarMaestroVentas();
        }

        private void CargarMaestroVentas()
        {
            string filtroNumero = txtBusqueda.Texts != null ? txtBusqueda.Texts.Trim() : "";
            int idUsuarioFiltro = (EsVendedor && usuarioActual != null) ? usuarioActual.IdUsuario : 0;
            _listaVentasActual = new CN_Venta().ListarVentas(dtFechaInicio.Value, dtFechaFin.Value, filtroNumero, idUsuarioFiltro);

            dgvFacturas.Rows.Clear();
            LimpiarDetalleSeleccionado();

            foreach (Venta v in _listaVentasActual)
            {
                dgvFacturas.Rows.Add(new object[]
                {
                    "",
                    v.IdVenta,
                    v.FechaRegistro,
                    v.TipoDocumento,
                    v.NumeroDocumento,
                    string.IsNullOrWhiteSpace(v.FormaPago) ? "Efectivo" : v.FormaPago,
                    v.oUsuario != null ? v.oUsuario.NombreCompleto : "",
                    v.MontoTotal.ToString("0.00")
                });
            }

            dgvFacturas.ClearSelection();

            // Si se abrió buscando un documento específico y hay coincidencia única, seleccionarla
            if (!string.IsNullOrWhiteSpace(filtroNumero) && dgvFacturas.Rows.Count > 0)
            {
                dgvFacturas.Rows[0].Selected = true;
                MostrarDetalleDeFila(0);
            }
        }

        private void DgvFacturas_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                MostrarDetalleDeFila(e.RowIndex);
            }
        }

        private void DgvFacturas_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvFacturas.Focused && dgvFacturas.SelectedRows.Count > 0)
            {
                MostrarDetalleDeFila(dgvFacturas.SelectedRows[0].Index);
            }
        }

        private void MostrarDetalleDeFila(int rowIndex)
        {
            if (rowIndex < 0 || rowIndex >= dgvFacturas.Rows.Count) return;

            string numeroDoc = dgvFacturas.Rows[rowIndex].Cells["NumeroDocumento"].Value?.ToString() ?? "";
            if (string.IsNullOrWhiteSpace(numeroDoc)) return;

            Venta oVenta = new CN_Venta().obtenerVenta(numeroDoc);
            if (oVenta.IdVenta != 0)
            {
                txtDocumento.Texts = oVenta.NumeroDocumento;
                txtFecha.Texts = oVenta.FechaRegistro;
                txtTipoDocumento.Texts = oVenta.TipoDocumento;
                txtUsuario.Texts = oVenta.oUsuario.NombreCompleto;
                txtDocV.Texts = string.IsNullOrEmpty(oVenta.FormaPago) ? "Efectivo" : oVenta.FormaPago;
                txtDocumentoC.Texts = txtDocV.Texts;
                txtNombre.Texts = string.IsNullOrEmpty(oVenta.NombreCliente) ? "Consumidor Final" : oVenta.NombreCliente;

                dgvData.Rows.Clear();
                foreach (DetalleVenta dc in oVenta.oDetalleVenta)
                {
                    dgvData.Rows.Add(new object[]
                    {
                        dc.oProducto.Nombre,
                        dc.oProducto.Descripcion,
                        dc.PrecioVenta.ToString("0.00"),
                        dc.Cantidad,
                        dc.SubTotal.ToString("0.00")
                    });
                }

                txtPago.Texts = oVenta.MontoPago.ToString("0.00");
                txtCambio.Texts = oVenta.MontoCambio.ToString("0.00");
                txtTotal.Texts = oVenta.MontoTotal.ToString("0.00");
            }
        }

        private void LimpiarDetalleSeleccionado()
        {
            txtDocumento.Texts = "";
            txtFecha.Texts = "";
            txtTipoDocumento.Texts = "";
            txtUsuario.Texts = "";
            txtDocV.Texts = "";
            txtDocumentoC.Texts = "";
            txtNombre.Texts = "";
            txtCorreoC.Texts = "";

            dgvData.Rows.Clear();
            txtPago.Texts = "0.00";
            txtCambio.Texts = "0.00";
            txtTotal.Texts = "0.00";
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            CargarMaestroVentas();
            if (dgvFacturas.Rows.Count == 0)
            {
                MessageBox.Show("No se encontraron ventas en el rango de fechas o número indicado.", "Sin resultados", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtBusqueda.Texts = "";
            if (comboPeriodo != null && comboPeriodo.Items.Count > 0)
                comboPeriodo.SelectedIndex = 0;
            dtFechaInicio.Value = DateTime.Now.AddYears(-2);
            dtFechaFin.Value = DateTime.Now.AddDays(1);
            CargarMaestroVentas();
        }

        private void dgvData_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            TemaCapybara.ModernDgv_CellPainting(sender, e);
        }

        // Genera y abre el Ticket Comprobante de Venta en PDF
        private void btnDescargar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtDocumento.Texts) || dgvData.Rows.Count == 0)
            {
                MessageBox.Show("Seleccione primero una factura en la tabla superior para generar su Ticket de Venta.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            string Texto_Html = Properties.Resources.PlantillaVenta.ToString();
            Negocio oDatos = new CN_Negocio().obtenerDatos();

            string formaPago = string.IsNullOrEmpty(txtDocV.Texts) ? "Efectivo" : txtDocV.Texts;

            Texto_Html = Texto_Html.Replace("@nombrenegocio", oDatos.Nombre.ToUpper());
            Texto_Html = Texto_Html.Replace("@docnegocio", oDatos.RUC);
            Texto_Html = Texto_Html.Replace("@direcnegocio", oDatos.Direccion);

            Texto_Html = Texto_Html.Replace("@tipodocumento", txtTipoDocumento.Texts.ToUpper());
            Texto_Html = Texto_Html.Replace("@numerodocumento", txtDocumento.Texts);

            Texto_Html = Texto_Html.Replace("@formapago", formaPago);
            Texto_Html = Texto_Html.Replace("@doccliente", formaPago);
            Texto_Html = Texto_Html.Replace("@nombrecliente", string.IsNullOrEmpty(txtNombre.Texts) ? "Consumidor Final" : txtNombre.Texts);
            Texto_Html = Texto_Html.Replace("@fecharegistro", txtFecha.Texts);
            Texto_Html = Texto_Html.Replace("@usuarioregistro", txtUsuario.Texts);

            string filas = string.Empty;
            decimal total = 0;
            foreach (DataGridViewRow row in dgvData.Rows)
            {
                filas += "<tr>";
                filas += "<td>" + row.Cells["Producto"].Value.ToString() + "</td>";
                filas += "<td>$" + row.Cells["Precio"].Value.ToString() + "</td>";
                filas += "<td>" + row.Cells["Cantidad"].Value.ToString() + "</td>";
                filas += "<td>$" + row.Cells["SubTotal"].Value.ToString() + "</td>";
                filas += "</tr>";
                total += Convert.ToDecimal(row.Cells["SubTotal"].Value);
            }

            Texto_Html = Texto_Html.Replace("@filas", filas);
            Texto_Html = Texto_Html.Replace("@tot", total.ToString("0.00"));
            Texto_Html = Texto_Html.Replace("@montototal", txtTotal.Texts);
            Texto_Html = Texto_Html.Replace("@pagocon", txtPago.Texts);
            Texto_Html = Texto_Html.Replace("@cambio", txtCambio.Texts);

            SaveFileDialog savefile = new SaveFileDialog();
            savefile.FileName = string.Format("Ticket_Venta_{0}.pdf", txtDocumento.Texts);
            savefile.Filter = "Pdf Files | *.pdf";

            if (savefile.ShowDialog() == DialogResult.OK)
            {
                using (FileStream stream = new FileStream(savefile.FileName, FileMode.Create))
                {
                    Document pdfDoc = new Document(PageSize.A4, 25, 25, 25, 25);
                    PdfWriter writer = PdfWriter.GetInstance(pdfDoc, stream);
                    pdfDoc.Open();

                    bool obtener = true;
                    byte[] byteImage = new CN_Negocio().obtenerLogo(out obtener);

                    if (obtener)
                    {
                        iTextSharp.text.Image img = iTextSharp.text.Image.GetInstance(byteImage);
                        img.ScaleToFit(60, 60);
                        img.Alignment = iTextSharp.text.Image.UNDERLYING;
                        img.SetAbsolutePosition(pdfDoc.Left, pdfDoc.GetTop(51));
                        pdfDoc.Add(img);
                    }

                    using (StringReader sr = new StringReader(Texto_Html))
                    {
                        XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
                    }

                    pdfDoc.Close();
                    stream.Close();
                    MessageBox.Show("Ticket Comprobante de Venta generado correctamente.", "Ticket Generado", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    try
                    {
                        Process.Start(savefile.FileName);
                    }
                    catch { }
                }
            }
        }

        private void txtBusqueda_KeyPress(object sender, KeyPressEventArgs e)
        {
            bool esControl = Char.IsControl(e.KeyChar);
            bool esDigito = Char.IsDigit(e.KeyChar);
            bool longitudPermitida = txtBusqueda.Texts.Length < 10;
            e.Handled = !(esControl || (esDigito && longitudPermitida));
        }

        private void txtBusqueda_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                btnBuscar_Click(sender, e);
            }
        }
    }
}
