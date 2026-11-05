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
using System.Windows.Forms;

namespace CapaPresentacion
{
    // Formulario Maestro-Detalle de Compras con Filtros de Fecha y Ticket Comprobante de Compra
    public partial class FrmDetalleCompra : Form
    {
        public Inicio inicio { get; set; }
        public string doc { get; set; }

        private DataGridView dgvFacturas;
        private DateTimePicker dtFechaInicio;
        private DateTimePicker dtFechaFin;
        private ComboBox comboPeriodo;
        private Label lblMaestro;
        private Label lblDetalle;
        private List<Compra> _listaComprasActual = new List<Compra>();

        public FrmDetalleCompra(Usuario obj)
        {
            InitializeComponent();
            ConfigurarInterfazMaestroDetalle();

            bool puedeValidar = obj != null && obj.oRol != null &&
                (obj.oRol.IdRol == 3 || obj.oRol.IdRol == 1 || string.Equals(obj.oRol.Descripcion, "Manager", StringComparison.OrdinalIgnoreCase));
            if (!puedeValidar)
            {
                btnValidar.Visible = false;
            }
        }

        public FrmDetalleCompra(string numero, Inicio pinicio)
        {
            InitializeComponent();
            inicio = pinicio;
            inicio.pintar();
            ConfigurarInterfazMaestroDetalle();
            this.doc = numero;
        }

        private void ConfigurarInterfazMaestroDetalle()
        {
            // 1. Panel superior de Búsqueda y Filtros (Fecha + N° Compra + Buscar + Validar)
            groupBox2.Text = "Búsqueda y Filtros de Compras";
            groupBox2.Location = new Point(55, 22);
            groupBox2.Size = new Size(810, 78);

            label1.Text = "N° Compra:";
            label1.Location = new Point(14, 18);
            txtBusqueda.Location = new Point(14, 37);
            txtBusqueda.Size = new Size(105, 32);
            txtBusqueda.PlaceholderText = "Todos / N°";

            Label lblPeriodo = new Label()
            {
                AutoSize = true,
                Text = "Período:",
                Location = new Point(128, 18),
                Font = new System.Drawing.Font("Segoe UI", 9F, FontStyle.Bold)
            };
            comboPeriodo = new ComboBox()
            {
                Name = "comboPeriodo",
                DropDownStyle = ComboBoxStyle.DropDownList,
                FlatStyle = FlatStyle.Flat,
                Location = new Point(128, 39),
                Size = new Size(115, 28)
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
                Location = new Point(252, 18),
                Font = new System.Drawing.Font("Segoe UI", 9F, FontStyle.Bold)
            };
            dtFechaInicio = new DateTimePicker()
            {
                Format = DateTimePickerFormat.Short,
                Location = new Point(252, 40),
                Size = new Size(98, 26),
                Value = DateTime.Now.AddYears(-2)
            };

            Label lblHasta = new Label()
            {
                AutoSize = true,
                Text = "Hasta:",
                Location = new Point(358, 18),
                Font = new System.Drawing.Font("Segoe UI", 9F, FontStyle.Bold)
            };
            dtFechaFin = new DateTimePicker()
            {
                Format = DateTimePickerFormat.Short,
                Location = new Point(358, 40),
                Size = new Size(98, 26),
                Value = DateTime.Now.AddDays(1)
            };

            rjButton1.Text = "  Buscar";
            rjButton1.Location = new Point(468, 28);
            rjButton1.Size = new Size(110, 38);

            BtnLimpiar2.Text = "  Limpiar";
            BtnLimpiar2.Location = new Point(584, 28);
            BtnLimpiar2.Size = new Size(110, 38);

            btnValidar.Text = "Validar";
            btnValidar.Location = new Point(700, 28);
            btnValidar.Size = new Size(98, 38);

            groupBox2.Controls.Add(lblPeriodo);
            groupBox2.Controls.Add(comboPeriodo);
            groupBox2.Controls.Add(lblDesde);
            groupBox2.Controls.Add(dtFechaInicio);
            groupBox2.Controls.Add(lblHasta);
            groupBox2.Controls.Add(dtFechaFin);

            // Compactar encabezado seleccionado
            groupBox3.Visible = false;
            groupBox1.Text = "Encabezado de la Compra Seleccionada";
            groupBox1.Location = new Point(55, 290);
            groupBox1.Size = new Size(810, 74);

            label2.Text = "Fecha:";
            label2.Location = new Point(14, 16);
            txtFecha.Location = new Point(14, 34);
            txtFecha.Size = new Size(110, 32);

            label3.Text = "Tipo Doc:";
            label3.Location = new Point(135, 16);
            txtTipoDocumento.Location = new Point(135, 34);
            txtTipoDocumento.Size = new Size(120, 32);

            label6.Text = "Responsable:";
            label6.Location = new Point(268, 16);
            txtUsuario.Location = new Point(268, 34);
            txtUsuario.Size = new Size(165, 32);

            label11.Text = "Proveedor:";
            label11.Location = new Point(445, 16);
            txtDocV.Location = new Point(445, 34);
            txtDocV.Size = new Size(195, 32);

            txtEstado.Location = new Point(655, 34);
            txtEstado.Size = new Size(140, 32);

            // 2. Crear DataGrid Principal (Maestro - Encabezados Únicos de Compra)
            lblMaestro = new Label()
            {
                AutoSize = true,
                Text = "1. Listado de Facturas de Compra (Seleccione una fila para ver sus productos):",
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
            dgvFacturas.Columns.Add(new DataGridViewTextBoxColumn() { Name = "IdCompra", HeaderText = "IdCompra", Visible = false });
            dgvFacturas.Columns.Add(new DataGridViewTextBoxColumn() { Name = "FechaRegistro", HeaderText = "Fecha", FillWeight = 15 });
            dgvFacturas.Columns.Add(new DataGridViewTextBoxColumn() { Name = "TipoDocumento", HeaderText = "Tipo Doc.", FillWeight = 15 });
            dgvFacturas.Columns.Add(new DataGridViewTextBoxColumn() { Name = "NumeroDocumento", HeaderText = "N° Compra", FillWeight = 15 });
            dgvFacturas.Columns.Add(new DataGridViewTextBoxColumn() { Name = "Proveedor", HeaderText = "Proveedor", FillWeight = 22 });
            dgvFacturas.Columns.Add(new DataGridViewTextBoxColumn() { Name = "Usuario", HeaderText = "Responsable", FillWeight = 20 });
            dgvFacturas.Columns.Add(new DataGridViewTextBoxColumn() { Name = "Estado", HeaderText = "Estado", FillWeight = 14 });
            dgvFacturas.Columns.Add(new DataGridViewTextBoxColumn() { Name = "MontoTotal", HeaderText = "Monto Total ($)", FillWeight = 16 });

            dgvFacturas.CellPainting += (s, ev) => TemaCapybara.ModernDgv_CellPainting(s, ev);
            dgvFacturas.CellClick += DgvFacturas_CellClick;
            dgvFacturas.SelectionChanged += DgvFacturas_SelectionChanged;

            // 3. Configurar Segunda Tabla Abajo (Detalle de Productos de la Compra Seleccionada)
            lblDetalle = new Label()
            {
                AutoSize = true,
                Text = "2. Detalle de Productos de la Compra Seleccionada:",
                Location = new Point(55, 370),
                Font = new System.Drawing.Font("Segoe UI", 9.75F, FontStyle.Bold)
            };

            dgvData.Location = new Point(55, 392);
            dgvData.Size = new Size(810, 130);
            dgvData.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvData.CellPainting += (s, ev) => TemaCapybara.ModernDgv_CellPainting(s, ev);

            label9.Location = new Point(392, 530);
            txtTotal.Location = new Point(389, 550);
            txtTotal.Size = new Size(160, 34);

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

            CargarMaestroCompras();
        }

        private void FrmDetalleCompra_Load(object sender, EventArgs e)
        {
            TemaCapybara.AplicarTema(this);
            rjButton1.Text = "  Buscar";
            BtnGuardar2.Text = "  Ticket PDF";

            if (!string.IsNullOrWhiteSpace(this.doc))
            {
                txtBusqueda.Texts = this.doc;
            }

            CargarMaestroCompras();
        }

        private void CargarMaestroCompras()
        {
            string filtroNumero = txtBusqueda.Texts != null ? txtBusqueda.Texts.Trim() : "";
            _listaComprasActual = new CN_Compra().ListarCompras(dtFechaInicio.Value, dtFechaFin.Value, 0, filtroNumero);

            dgvFacturas.Rows.Clear();
            LimpiarDetalleSeleccionado();

            foreach (Compra c in _listaComprasActual)
            {
                dgvFacturas.Rows.Add(new object[]
                {
                    "",
                    c.IdCompra,
                    c.FechaRegistro,
                    c.TipoDocumento,
                    c.NumeroDocumento,
                    c.oProveedor != null ? c.oProveedor.RazonSocial : "",
                    c.oUsuario != null ? c.oUsuario.NombreCompleto : "",
                    c.Estado ? "Recibida" : "En Camino",
                    c.MontoTotal.ToString("0.00")
                });
            }

            dgvFacturas.ClearSelection();

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

            Compra oCompra = new CN_Compra().obtenerCompra(numeroDoc);
            if (oCompra.IdCompra != 0)
            {
                txtDocumento.Texts = oCompra.NumeroDocumento;
                txtFecha.Texts = oCompra.FechaRegistro;
                txtTipoDocumento.Texts = oCompra.TipoDocumento;
                txtUsuario.Texts = oCompra.oUsuario.NombreCompleto;
                txtDocV.Texts = oCompra.oProveedor.RazonSocial;

                txtDocumentoProv.Texts = oCompra.oProveedor.Documento;
                txtRazon.Texts = oCompra.oProveedor.RazonSocial;
                txtCorreoP.Texts = oCompra.oProveedor.Correo;
                txtEstado.Texts = oCompra.Estado ? "Recibida" : "En Camino";

                dgvData.Rows.Clear();
                foreach (DetalleCompra dc in oCompra.oDetalleCompra)
                {
                    dgvData.Rows.Add(new object[]
                    {
                        dc.oProducto.Nombre,
                        dc.oProducto.Descripcion,
                        dc.PrecioCompra.ToString("0.00"),
                        dc.Cantidad,
                        dc.MontoTotal.ToString("0.00")
                    });
                }

                txtTotal.Texts = oCompra.MontoTotal.ToString("0.00");
            }
        }

        private void LimpiarDetalleSeleccionado()
        {
            txtDocumento.Texts = "";
            txtFecha.Texts = "";
            txtTipoDocumento.Texts = "";
            txtUsuario.Texts = "";
            txtDocV.Texts = "";
            txtDocumentoProv.Texts = "";
            txtRazon.Texts = "";
            txtCorreoP.Texts = "";
            txtEstado.Texts = "";
            txtTotal.Texts = "0.00";
            dgvData.Rows.Clear();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            CargarMaestroCompras();
            if (dgvFacturas.Rows.Count == 0)
            {
                MessageBox.Show("No se encontraron compras en el rango de fechas o número indicado.", "Sin resultados", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtBusqueda.Texts = "";
            if (comboPeriodo != null && comboPeriodo.Items.Count > 0)
                comboPeriodo.SelectedIndex = 0;
            dtFechaInicio.Value = DateTime.Now.AddYears(-2);
            dtFechaFin.Value = DateTime.Now.AddDays(1);
            CargarMaestroCompras();
        }

        // Genera y descarga el Ticket Comprobante de Compra en PDF
        private void btnDescargar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtDocumento.Texts) || dgvData.Rows.Count == 0)
            {
                MessageBox.Show("Seleccione primero una factura en la tabla superior para generar su Ticket de Compra.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            string Texto_Html = Properties.Resources.PlantillaCompra.ToString();
            Negocio oDatos = new CN_Negocio().obtenerDatos();

            Texto_Html = Texto_Html.Replace("@nombrenegocio", oDatos.Nombre.ToUpper());
            Texto_Html = Texto_Html.Replace("@docnegocio", oDatos.RUC);
            Texto_Html = Texto_Html.Replace("@direcnegocio", oDatos.Direccion);

            Texto_Html = Texto_Html.Replace("@tipodocumento", txtTipoDocumento.Texts.ToUpper());
            Texto_Html = Texto_Html.Replace("@numerodocumento", txtDocumento.Texts);

            Texto_Html = Texto_Html.Replace("@docproveedor", txtDocumentoProv.Texts);
            Texto_Html = Texto_Html.Replace("@nombreproveedor", txtRazon.Texts);
            Texto_Html = Texto_Html.Replace("@fecharegistro", txtFecha.Texts);
            Texto_Html = Texto_Html.Replace("@usuarioregistro", txtUsuario.Texts);

            string filas = string.Empty;
            foreach (DataGridViewRow row in dgvData.Rows)
            {
                filas += "<tr>";
                filas += "<td>" + row.Cells["Producto"].Value.ToString() + "</td>";
                filas += "<td>$" + row.Cells["PrecioCompra"].Value.ToString() + "</td>";
                filas += "<td>" + row.Cells["Cantidad"].Value.ToString() + "</td>";
                filas += "<td>$" + row.Cells["SubTotal"].Value.ToString() + "</td>";
                filas += "</tr>";
            }

            Texto_Html = Texto_Html.Replace("@filas", filas);
            Texto_Html = Texto_Html.Replace("@montototal", txtTotal.Texts);

            SaveFileDialog savefile = new SaveFileDialog();
            savefile.FileName = string.Format("Ticket_Compra_{0}.pdf", txtDocumento.Texts);
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
                    MessageBox.Show("Ticket Comprobante de Compra generado correctamente.", "Ticket Generado", MessageBoxButtons.OK, MessageBoxIcon.Information);

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

        private void rjButton2_Click(object sender, EventArgs e)
        {
            string docValidar = !string.IsNullOrWhiteSpace(txtDocumento.Texts) ? txtDocumento.Texts : txtBusqueda.Texts;
            if (!string.IsNullOrWhiteSpace(docValidar))
            {
                string Mensaje = string.Empty;
                bool Resultado = new CN_Compra().ValidarCompra(docValidar, out Mensaje);
                MessageBox.Show(Mensaje, "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtEstado.Texts = "Recibida";
                CargarMaestroCompras();
            }
            else
            {
                MessageBox.Show("Seleccione primero una compra en la tabla superior para validarla.", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
    }
}
