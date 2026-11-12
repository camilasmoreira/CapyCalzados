using CapaEntidad;
using CapaNegocio;
using CapaPresentacion.Utilidades;
using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace CapaPresentacion
{
    public partial class FrmProducto : Form
    {
        public Inicio Inicio { get; set; }
        private Usuario usuarioActual;
        private ComboBox comboMarca;
        private ComboBox comboFiltroEstado;
        private Label lblFiltroEstado;

        private bool EsVendedor =>
            usuarioActual != null && usuarioActual.oRol != null &&
            (usuarioActual.oRol.IdRol == 2 || string.Equals(usuarioActual.oRol.Descripcion, "Vendedor", StringComparison.OrdinalIgnoreCase));

        // Constructor de la clase FrmProducto
        public FrmProducto(Inicio inicio, Usuario oUsuario = null)
        {
            InitializeComponent();
            Inicio = inicio;
            usuarioActual = oUsuario;
            Inicio.pintar();
        }

        // Evento que se ejecuta al cargar el formulario
        private void FrmProducto_Load(object sender, EventArgs e)
        {
            // Agregar columnas IdProveedor y Proveedor al DataGridView si no existen
            if (!dgvData.Columns.Contains("IdProveedor"))
            {
                DataGridViewTextBoxColumn colIdProv = new DataGridViewTextBoxColumn();
                colIdProv.Name = "IdProveedor";
                colIdProv.HeaderText = "IdProveedor";
                colIdProv.Visible = false;
                colIdProv.ReadOnly = true;
                int idxCat = dgvData.Columns.Contains("Categoria") ? dgvData.Columns["Categoria"].Index + 1 : dgvData.Columns.Count;
                dgvData.Columns.Insert(idxCat, colIdProv);
            }
            if (!dgvData.Columns.Contains("Proveedor"))
            {
                DataGridViewTextBoxColumn colProv = new DataGridViewTextBoxColumn();
                colProv.Name = "Proveedor";
                colProv.HeaderText = "Proveedor";
                colProv.ReadOnly = true;
                colProv.Width = 140;
                int idxIdProv = dgvData.Columns["IdProveedor"].Index + 1;
                dgvData.Columns.Insert(idxIdProv, colProv);
            }

            // Crear ComboBox para seleccionar Marca / Proveedor como categoría
            comboMarca = new ComboBox();
            comboMarca.Name = "comboMarca";
            comboMarca.DropDownStyle = ComboBoxStyle.DropDownList;
            comboMarca.FlatStyle = FlatStyle.Flat;
            comboMarca.Font = comboCategoria.Font;
            comboMarca.BackColor = comboCategoria.BackColor;
            comboMarca.ForeColor = comboCategoria.ForeColor;
            comboMarca.FormattingEnabled = true;
            comboMarca.Location = new Point(txtMarca.Location.X, comboCategoria.Location.Y);
            comboMarca.Size = comboCategoria.Size;
            comboMarca.TabIndex = txtMarca.TabIndex;
            lblMarca.Text = "Marca / Proveedor:";
            lblMarca.Location = new Point(txtMarca.Location.X, label7.Location.Y);
            txtMarca.Visible = false;
            groupBox1.Controls.Add(comboMarca);
            comboMarca.BringToFront();

            // Crear filtro de Estado (Todos / Activos / Productos Inactivos) en la barra de búsqueda
            lblFiltroEstado = new Label()
            {
                AutoSize = true,
                Text = "Ver:",
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                Location = new Point(230, 303)
            };
            comboFiltroEstado = new ComboBox()
            {
                Name = "comboFiltroEstado",
                DropDownStyle = ComboBoxStyle.DropDownList,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9.5F, FontStyle.Regular),
                Location = new Point(265, 300),
                Size = new Size(145, 29)
            };
            comboFiltroEstado.Items.Add(new OpcionesCombo() { Valor = -1, Texto = "Todos" });
            comboFiltroEstado.Items.Add(new OpcionesCombo() { Valor = 1, Texto = "Productos Activos" });
            comboFiltroEstado.Items.Add(new OpcionesCombo() { Valor = 0, Texto = "Productos Inactivos" });
            comboFiltroEstado.DisplayMember = "Texto";
            comboFiltroEstado.ValueMember = "Valor";
            comboFiltroEstado.SelectedIndex = 0;
            comboFiltroEstado.SelectedIndexChanged += (s, ev) => AplicarFiltrosTabla();

            // Ajustar posiciones de la barra de búsqueda para incluir el filtro de Productos Inactivos
            BtnGuardar2.Location = new Point(90, 293);
            BtnGuardar2.Size = new Size(130, 41);
            label11.Location = new Point(420, 303);
            comboBusqueda.Location = new Point(498, 300);
            comboBusqueda.Size = new Size(105, 29);
            txtBusqueda.Location = new Point(610, 295);
            txtBusqueda.Size = new Size(115, 36);
            rjButton5.Location = new Point(730, 295);
            rjButton4.Location = new Point(782, 295);
            label5.Location = new Point(80, 281);
            label5.Size = new Size(765, 62);
            dgvData.Location = new Point(80, 355);
            dgvData.Size = new Size(765, 225);

            this.Controls.Add(lblFiltroEstado);
            this.Controls.Add(comboFiltroEstado);
            lblFiltroEstado.BringToFront();
            comboFiltroEstado.BringToFront();

            TemaCapybara.AplicarTema(this);

            // Cargar proveedores/marcas en comboMarca (Solo Nike y Adidas)
            List<Proveedor> listaProveedores = new CN_Proveedor().Listar().Where(p => p.Estado == true).ToList();
            var marcasBase = new List<string> { "Nike", "Adidas" };
            foreach (string marcaNombre in marcasBase)
            {
                var prov = listaProveedores.FirstOrDefault(p => p.RazonSocial.Equals(marcaNombre, StringComparison.OrdinalIgnoreCase));
                int idProv = prov != null ? prov.IdProveedor : 0;
                comboMarca.Items.Add(new OpcionesCombo() { Valor = idProv, Texto = marcaNombre });
            }
            foreach (Proveedor item in listaProveedores)
            {
                bool yaExiste = false;
                foreach (OpcionesCombo oc in comboMarca.Items)
                {
                    if (oc.Texto.Equals(item.RazonSocial, StringComparison.OrdinalIgnoreCase))
                    {
                        yaExiste = true;
                        break;
                    }
                }
                if (!yaExiste)
                {
                    comboMarca.Items.Add(new OpcionesCombo() { Valor = item.IdProveedor, Texto = item.RazonSocial });
                }
            }
            comboMarca.DisplayMember = "Texto";
            comboMarca.ValueMember = "Valor";
            if (comboMarca.Items.Count > 0)
            {
                comboMarca.SelectedIndex = 0;
                txtMarca.Texts = ((OpcionesCombo)comboMarca.SelectedItem).Texto;
            }
            comboMarca.SelectedIndexChanged += (s, ev) =>
            {
                if (comboMarca.SelectedItem is OpcionesCombo sel)
                {
                    txtMarca.Texts = sel.Texto;
                }
            };

            // Inicializa el comboEstado con opciones "Activo" y "No Activo"
            comboEstado.Items.Clear();
            comboEstado.Items.Add(new OpcionesCombo() { Valor = 1, Texto = "Activo" });
            comboEstado.Items.Add(new OpcionesCombo() { Valor = 0, Texto = "No Activo" });
            comboEstado.DisplayMember = "Texto";
            comboEstado.ValueMember = "Valor";
            comboEstado.SelectedIndex = 0;

            // Carga SOLO las categorías Zapatillas Urbanas y Zapatillas Deportivas
            comboCategoria.Items.Clear();
            List<Categoria> listaCategoria = new CN_Categoria().Listar()
                .Where(p => p.Estado == true &&
                       (p.Descripcion.Equals("Zapatillas Urbanas", StringComparison.OrdinalIgnoreCase) ||
                        p.Descripcion.Equals("Zapatillas Deportivas", StringComparison.OrdinalIgnoreCase)))
                .ToList();

            if (listaCategoria != null && listaCategoria.Count > 0)
            {
                foreach (Categoria item in listaCategoria)
                {
                    comboCategoria.Items.Add(new OpcionesCombo() { Valor = item.IdCategoria, Texto = item.Descripcion });
                }
            }
            else
            {
                foreach (Categoria item in new CN_Categoria().Listar().Where(p => p.Estado == true))
                {
                    comboCategoria.Items.Add(new OpcionesCombo() { Valor = item.IdCategoria, Texto = item.Descripcion });
                }
            }

            comboCategoria.DisplayMember = "Texto";
            comboCategoria.ValueMember = "Valor";
            if (comboCategoria.Items.Count > 0)
            {
                comboCategoria.SelectedIndex = 0;
            }

            // Inicializa el comboBusqueda con las columnas visibles del DataGridView
            comboBusqueda.Items.Clear();
            foreach (DataGridViewColumn columna in dgvData.Columns)
            {
                if (columna.Visible == true && columna.Name != "btnseleccionar")
                {
                    comboBusqueda.Items.Add(new OpcionesCombo() { Valor = columna.Name, Texto = columna.HeaderText });
                }
            }
            comboBusqueda.DisplayMember = "Texto";
            comboBusqueda.ValueMember = "Valor";
            if (comboBusqueda.Items.Count > 0)
                comboBusqueda.SelectedIndex = 0;

            CargarProductosTabla();

            // Si el usuario es Vendedor (IdRol == 2), solo puede visualizar productos activos (no puede ver productos inactivos)
            if (EsVendedor)
            {
                groupBox1.Visible = false;
                rjButton1.Visible = false;    // Botón Guardar (+)
                BtnEliminar2.Visible = false; // Botón Eliminar
                BtnLimpiar2.Visible = false;  // Botón Limpiar
                lblFiltroEstado.Visible = false;
                comboFiltroEstado.Visible = false;
                comboFiltroEstado.Enabled = false;

                if (dgvData.Columns.Contains("btnseleccionar"))
                {
                    dgvData.Columns["btnseleccionar"].Visible = false;
                }

                // Ajustar la vista de búsqueda y tabla para modo solo visualización de productos activos
                label5.Location = new Point(80, 45);
                BtnGuardar2.Location = new Point(95, 55);
                BtnGuardar2.Size = new Size(155, 41);
                label11.Location = new Point(280, 67);
                comboBusqueda.Location = new Point(365, 64);
                comboBusqueda.Size = new Size(145, 29);
                txtBusqueda.Location = new Point(525, 60);
                txtBusqueda.Size = new Size(185, 36);
                rjButton5.Location = new Point(722, 60);
                rjButton4.Location = new Point(778, 60);

                dgvData.Location = new Point(80, 125);
                dgvData.Size = new Size(765, 455);
            }
        }

        private void CargarProductosTabla()
        {
            dgvData.Rows.Clear();
            List<Producto> listaProductos = new CN_Producto().Listar();
            if (EsVendedor)
            {
                // El vendedor no puede ver productos inactivos
                listaProductos = listaProductos.Where(p => p.Estado == true).ToList();
            }
            foreach (Producto item in listaProductos)
            {
                string nombreProveedor = (item.oProveedor != null && !string.IsNullOrWhiteSpace(item.oProveedor.RazonSocial))
                    ? item.oProveedor.RazonSocial
                    : (!string.IsNullOrWhiteSpace(item.Marca) ? item.Marca : "Sin proveedor");
                int idProv = item.oProveedor != null ? item.oProveedor.IdProveedor : 0;

                dgvData.Rows.Add(new object[] {
                    "",
                    item.IdProducto,
                    item.Codigo,
                    item.Nombre,
                    item.Descripcion,
                    item.Marca,
                    item.oCategoria.IdCategoria,
                    item.oCategoria.Descripcion,
                    idProv,
                    nombreProveedor,
                    item.Stock,
                    item.PrecioCompra,
                    item.PrecioVenta,
                    item.Estado == true ? 1 : 0,
                    item.Estado == true ? "Activo" : "No Activo"
                });
            }
            AplicarFiltrosTabla();
        }

        private void AplicarFiltrosTabla()
        {
            if (dgvData.Rows.Count == 0) return;

            int filtroEstado = EsVendedor ? 1 : -1;
            if (!EsVendedor && comboFiltroEstado != null && comboFiltroEstado.SelectedItem is OpcionesCombo ocEst)
            {
                filtroEstado = Convert.ToInt32(ocEst.Valor);
            }

            string columnaFiltro = (comboBusqueda != null && comboBusqueda.SelectedItem is OpcionesCombo ocCol)
                ? ocCol.Valor.ToString()
                : "";
            string textoBusq = txtBusqueda != null ? txtBusqueda.Texts.Trim().ToUpper() : "";

            foreach (DataGridViewRow row in dgvData.Rows)
            {
                int estadoFila = Convert.ToInt32(row.Cells["EstadoValor"].Value ?? 1);
                bool cumpleEstado = (filtroEstado == -1) || (estadoFila == filtroEstado);

                bool cumpleTexto = true;
                if (!string.IsNullOrEmpty(textoBusq) && !string.IsNullOrEmpty(columnaFiltro) && dgvData.Columns.Contains(columnaFiltro))
                {
                    string val = row.Cells[columnaFiltro].Value?.ToString() ?? "";
                    cumpleTexto = val.Trim().ToUpper().Contains(textoBusq);
                }

                row.Visible = cumpleEstado && cumpleTexto;
            }
        }

        // Método para validar los campos del formulario
        private bool Validaciones()
        {
            bool validaciones = true;

            if (comboMarca != null && comboMarca.SelectedItem is OpcionesCombo selMarca)
            {
                txtMarca.Texts = selMarca.Texto;
            }

            if (txtCodigo.Texts == "") { validaciones = false; }
            if (txtNombre.Texts == "") { validaciones = false; }
            if (txtDescripcion.Texts == "") { validaciones = false; }
            if (txtMarca.Texts == "") { validaciones = false; }

            return validaciones;
        }

        // Evento que se ejecuta al hacer clic en el botón "Guardar"
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (EsVendedor)
            {
                MessageBox.Show("El vendedor solo tiene permiso para visualizar productos.", "Acceso restringido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (Validaciones())
            {
                string mensaje = string.Empty;
                DialogResult confirmacion;

                if (Convert.ToInt32(txtid.Text) == 0)
                {
                    confirmacion = MessageBox.Show("¿Seguro desea agregar el producto?", "Confirmacion", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                }
                else
                {
                    confirmacion = MessageBox.Show("¿Seguro desea editar el producto?", "Confirmacion", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                }

                if (confirmacion == DialogResult.Yes)
                {
                    OpcionesCombo marcaSeleccionada = (OpcionesCombo)comboMarca.SelectedItem;
                    int idProveedorSel = marcaSeleccionada != null ? Convert.ToInt32(marcaSeleccionada.Valor) : 0;
                    string textoMarcaProv = marcaSeleccionada != null ? marcaSeleccionada.Texto : txtMarca.Texts;

                    Producto objProducto = new Producto()
                    {
                        IdProducto = Convert.ToInt32(txtid.Text),
                        Codigo = txtCodigo.Texts,
                        Nombre = txtNombre.Texts,
                        Descripcion = txtDescripcion.Texts,
                        Marca = textoMarcaProv,
                        oCategoria = new Categoria() { IdCategoria = Convert.ToInt32(((OpcionesCombo)comboCategoria.SelectedItem).Valor) },
                        oProveedor = new Proveedor() { IdProveedor = idProveedorSel, RazonSocial = textoMarcaProv },
                        Estado = Convert.ToInt32(((OpcionesCombo)comboEstado.SelectedItem).Valor) == 1
                    };

                    if (objProducto.IdProducto == 0)
                    {
                        int idProductoGenerado = new CN_Producto().Registrar(objProducto, out mensaje);
                        if (idProductoGenerado != 0)
                        {
                            dgvData.Rows.Add(new object[] {
                                "",
                                idProductoGenerado,
                                txtCodigo.Texts,
                                txtNombre.Texts,
                                txtDescripcion.Texts,
                                textoMarcaProv,
                                ((OpcionesCombo)comboCategoria.SelectedItem).Valor.ToString(),
                                ((OpcionesCombo)comboCategoria.SelectedItem).Texto.ToString(),
                                idProveedorSel.ToString(),
                                textoMarcaProv,
                                "0",
                                "0.00",
                                "0.00",
                                ((OpcionesCombo)comboEstado.SelectedItem).Valor.ToString(),
                                ((OpcionesCombo)comboEstado.SelectedItem).Texto.ToString()
                            });

                            AplicarFiltrosTabla();
                            LimpiarCampos();
                        }
                        else
                        {
                            MessageBox.Show(mensaje);
                        }
                    }
                    else
                    {
                        bool resultado = new CN_Producto().Editar(objProducto, out mensaje);
                        if (resultado == true)
                        {
                            DataGridViewRow row = dgvData.Rows[Convert.ToInt32(txtindice.Text)];
                            row.Cells["IdProducto"].Value = txtid.Text;
                            row.Cells["Codigo"].Value = txtCodigo.Texts;
                            row.Cells["Nombre"].Value = txtNombre.Texts;
                            row.Cells["Descripcion"].Value = txtDescripcion.Texts;
                            row.Cells["Marca"].Value = textoMarcaProv;
                            row.Cells["IdCategoria"].Value = ((OpcionesCombo)comboCategoria.SelectedItem).Valor.ToString();
                            row.Cells["Categoria"].Value = ((OpcionesCombo)comboCategoria.SelectedItem).Texto.ToString();
                            row.Cells["IdProveedor"].Value = idProveedorSel.ToString();
                            row.Cells["Proveedor"].Value = textoMarcaProv;
                            row.Cells["EstadoValor"].Value = ((OpcionesCombo)comboEstado.SelectedItem).Valor.ToString();
                            row.Cells["Estado"].Value = ((OpcionesCombo)comboEstado.SelectedItem).Texto.ToString();

                            AplicarFiltrosTabla();
                            LimpiarCampos();
                        }
                        else
                        {
                            MessageBox.Show(mensaje);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Debe Completar todos los campos", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
        }

        // Evento que se ejecuta al hacer clic en el botón "Eliminar" (da de baja lógica el producto activo)
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (EsVendedor)
            {
                MessageBox.Show("El vendedor solo tiene permiso para visualizar productos.", "Acceso restringido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (Convert.ToInt32(txtid.Text) != 0)
            {
                if (MessageBox.Show("¿Desea desactivar (pasar a Productos Inactivos) este producto?", "Mensaje", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    string mensaje = string.Empty;
                    Producto objProducto = new Producto()
                    {
                        IdProducto = Convert.ToInt32(txtid.Text),
                    };

                    bool respuesta = new CN_Producto().Eliminar(objProducto, out mensaje);
                    if (respuesta)
                    {
                        dgvData.Rows[Convert.ToInt32(txtindice.Text)].Cells["EstadoValor"].Value = 0;
                        dgvData.Rows[Convert.ToInt32(txtindice.Text)].Cells["Estado"].Value = "No Activo";

                        AplicarFiltrosTabla();
                        LimpiarCampos();
                    }
                    else
                    {
                        MessageBox.Show(mensaje, "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
            }
        }

        private void LimpiarCampos()
        {
            txtindice.Text = "-1";
            txtid.Text = "0";
            txtCodigo.Texts = "";
            txtNombre.Texts = "";
            txtDescripcion.Texts = "";
            if (comboMarca != null && comboMarca.Items.Count > 0)
            {
                comboMarca.SelectedIndex = 0;
                txtMarca.Texts = ((OpcionesCombo)comboMarca.SelectedItem).Texto;
            }
            else
            {
                txtMarca.Texts = "";
            }
            if (comboCategoria.Items.Count > 0)
                comboCategoria.SelectedIndex = 0;
            if (comboEstado.Items.Count > 0)
                comboEstado.SelectedIndex = 0;
            txtCodigo.Select();
        }

        private void dgvData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex >= 0 && dgvData.Columns[e.ColumnIndex].Name == "btnseleccionar")
            {
                int indice = e.RowIndex;

                if (indice >= 0)
                {
                    txtindice.Text = indice.ToString();
                    txtid.Text = dgvData.Rows[indice].Cells["IdProducto"].Value.ToString();
                    txtCodigo.Texts = dgvData.Rows[indice].Cells["Codigo"].Value.ToString();
                    txtNombre.Texts = dgvData.Rows[indice].Cells["Nombre"].Value.ToString();
                    txtDescripcion.Texts = dgvData.Rows[indice].Cells["Descripcion"].Value.ToString();
                    txtMarca.Texts = dgvData.Rows[indice].Cells["Marca"].Value.ToString();

                    if (comboMarca != null)
                    {
                        string marcaFila = dgvData.Rows[indice].Cells["Marca"].Value.ToString();
                        string provFila = dgvData.Columns.Contains("Proveedor") && dgvData.Rows[indice].Cells["Proveedor"].Value != null
                            ? dgvData.Rows[indice].Cells["Proveedor"].Value.ToString()
                            : marcaFila;

                        bool encontrado = false;
                        foreach (OpcionesCombo oc in comboMarca.Items)
                        {
                            if (oc.Texto.Equals(marcaFila, StringComparison.OrdinalIgnoreCase) ||
                                oc.Texto.Equals(provFila, StringComparison.OrdinalIgnoreCase))
                            {
                                comboMarca.SelectedIndex = comboMarca.Items.IndexOf(oc);
                                encontrado = true;
                                break;
                            }
                        }
                        if (!encontrado && comboMarca.Items.Count > 0)
                        {
                            foreach (OpcionesCombo oc in comboMarca.Items)
                            {
                                if (oc.Texto.IndexOf("Otros", StringComparison.OrdinalIgnoreCase) >= 0)
                                {
                                    comboMarca.SelectedIndex = comboMarca.Items.IndexOf(oc);
                                    break;
                                }
                            }
                        }
                    }

                    foreach (OpcionesCombo oc in comboCategoria.Items)
                    {
                        if (Convert.ToInt32(oc.Valor) == Convert.ToInt32(dgvData.Rows[indice].Cells["IdCategoria"].Value))
                        {
                            comboCategoria.SelectedIndex = comboCategoria.Items.IndexOf(oc);
                            break;
                        }
                    }

                    foreach (OpcionesCombo oc in comboEstado.Items)
                    {
                        if (Convert.ToInt32(oc.Valor) == Convert.ToInt32(dgvData.Rows[indice].Cells["EstadoValor"].Value))
                        {
                            comboEstado.SelectedIndex = comboEstado.Items.IndexOf(oc);
                            break;
                        }
                    }
                }
            }
        }

        private void dgvData_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            TemaCapybara.ModernDgv_CellPainting(sender, e);
        }

        private void iconButton1_Click(object sender, EventArgs e)
        {
            AplicarFiltrosTabla();
        }

        private void btnLimpiarBusqueda_Click_1(object sender, EventArgs e)
        {
            txtBusqueda.Texts = "";
            if (comboFiltroEstado != null && comboFiltroEstado.Items.Count > 0)
                comboFiltroEstado.SelectedIndex = 0;
            AplicarFiltrosTabla();
        }

        private void btnExportar_Click(object sender, EventArgs e)
        {
            if (dgvData.Rows.Count < 1)
            {
                MessageBox.Show("No hay datos para exportar", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                DataTable dt = new DataTable();
                foreach (DataGridViewColumn colum in dgvData.Columns)
                {
                    if (colum.HeaderText != "" && colum.Visible)
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
                            if (colum.HeaderText != "" && colum.Visible)
                            {
                                valores.Add(rows.Cells[colum.Name].Value != null ? rows.Cells[colum.Name].Value.ToString() : "");
                            }
                        }
                        dt.Rows.Add(valores.ToArray());
                    }
                }

                SaveFileDialog savefile = new SaveFileDialog();
                savefile.FileName = string.Format("ReporteProducto_{0}.xlsx", DateTime.Now.ToString("ddMMyyyyHHmmss"));
                savefile.Filter = "Excel Files | *.xlsx";

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
        }

        private void txtNombre_KeyPress(object sender, KeyPressEventArgs e)
        {
            bool esControl = Char.IsControl(e.KeyChar);
            bool longitudPermitida = txtNombre.Texts.Length < 80;
            e.Handled = !(esControl || longitudPermitida);
        }

        private void txtDescripcion_KeyPress(object sender, KeyPressEventArgs e)
        {
            bool esControl = Char.IsControl(e.KeyChar);
            bool longitudPermitida = txtDescripcion.Texts.Length < 100;
            e.Handled = !(esControl || longitudPermitida);
        }

        private void txtCodigo_KeyPress(object sender, KeyPressEventArgs e)
        {
            bool esControl = Char.IsControl(e.KeyChar);
            bool esDigito = Char.IsDigit(e.KeyChar);
            bool longitudPermitida = txtCodigo.Texts.Length < 100;
            e.Handled = !(esControl || (esDigito && longitudPermitida));
        }

        private void txtNombre__TextChanged(object sender, EventArgs e)
        {

        }

        private void txtMarca_KeyPress(object sender, KeyPressEventArgs e)
        {
            bool esControl = Char.IsControl(e.KeyChar);
            bool esDigito = Char.IsDigit(e.KeyChar);
            bool longitudPermitida = txtMarca.Texts.Length < 80;
            e.Handled = !(esControl || (!esDigito && longitudPermitida));
        }

        private void txtBusqueda_KeyPress(object sender, KeyPressEventArgs e)
        {
            bool esControl = Char.IsControl(e.KeyChar);
            bool longitudPermitida = txtBusqueda.Texts.Length < 80;
            e.Handled = !(esControl || longitudPermitida);
        }

        private void dgvData_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (this.dgvData.Columns[e.ColumnIndex].Name == "Estado")
            {
                if (e.Value != null && e.Value.ToString() == "No Activo")
                {
                    e.CellStyle.BackColor = Color.Red;
                    e.CellStyle.ForeColor = Color.Black;
                }
            }
        }
    }
}
