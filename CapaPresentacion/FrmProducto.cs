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
            TemaCapybara.AplicarTema(this);

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

            // Cargar proveedores/marcas en comboMarca (Nike, Adidas, Otros proveedores + proveedores de BD)
            List<Proveedor> listaProveedores = new CN_Proveedor().Listar().Where(p => p.Estado == true).ToList();
            var marcasBase = new List<string> { "Nike", "Adidas", "Otros proveedores" };
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
            comboEstado.Items.Add(new OpcionesCombo() { Valor = 1, Texto = "Activo" });
            comboEstado.Items.Add(new OpcionesCombo() { Valor = 0, Texto = "No Activo" });

            comboEstado.DisplayMember = "Texto";
            comboEstado.ValueMember = "Valor";
            comboEstado.SelectedIndex = 0; // Selecciona "Activo" por defecto

            // Carga SOLO las categorías Zapatillas Urbanas y Zapatillas Deportivas
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
                // Si aún no están filtradas, cargar las activas
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
            foreach (DataGridViewColumn columna in dgvData.Columns)
            {
                if (columna.Visible == true && columna.Name != "btnseleccionar")
                {
                    comboBusqueda.Items.Add(new OpcionesCombo() { Valor = columna.Name, Texto = columna.HeaderText });
                }
            }
            comboBusqueda.DisplayMember = "Texto";
            comboBusqueda.ValueMember = "Valor";
            comboBusqueda.SelectedIndex = 0;

            // Muestra los productos en el DataGridView incluyendo su Proveedor
            List<Producto> listaProductos = new CN_Producto().Listar();
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
                    item.Estado == true ? "Activo": "No Activo"
                });
            }

            // Si el usuario es Vendedor (IdRol != 1), solo puede visualizar productos
            if (usuarioActual != null && usuarioActual.oRol != null && usuarioActual.oRol.IdRol != 1)
            {
                groupBox1.Visible = false;
                rjButton1.Visible = false;    // Botón Guardar (+)
                BtnEliminar2.Visible = false; // Botón Eliminar
                BtnLimpiar2.Visible = false;  // Botón Limpiar
                if (dgvData.Columns.Contains("btnseleccionar"))
                {
                    dgvData.Columns["btnseleccionar"].Visible = false;
                }

                // Ajustar la vista de búsqueda y tabla para modo solo visualización
                label5.Location = new Point(label5.Location.X, 45);
                BtnGuardar2.Location = new Point(BtnGuardar2.Location.X, 51);
                label11.Location = new Point(label11.Location.X, 67);
                comboBusqueda.Location = new Point(comboBusqueda.Location.X, 64);
                txtBusqueda.Location = new Point(txtBusqueda.Location.X, 60);
                rjButton5.Location = new Point(rjButton5.Location.X, 60);
                rjButton4.Location = new Point(rjButton4.Location.X, 60);

                dgvData.Location = new Point(dgvData.Location.X, 125);
                dgvData.Size = new Size(dgvData.Size.Width, 455);
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

            // Comprueba que los campos requeridos no estén vacíos
            if (txtCodigo.Texts == "") { validaciones = false; }
            if (txtNombre.Texts == "") { validaciones = false; }
            if (txtDescripcion.Texts == "") { validaciones = false; }
            if (txtMarca.Texts == "") { validaciones = false; }

            return validaciones;
        }

        // Evento que se ejecuta al hacer clic en el botón "Guardar"
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (usuarioActual != null && usuarioActual.oRol != null && usuarioActual.oRol.IdRol != 1)
            {
                MessageBox.Show("El vendedor solo tiene permiso para visualizar productos.", "Acceso restringido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Verifica si todos los campos son válidos
            if (Validaciones())
            {
                string mensaje = string.Empty;
                DialogResult confirmacion;

                // Pregunta al usuario si desea agregar o editar el producto
                if (Convert.ToInt32(txtid.Text) == 0)
                {
                    confirmacion = MessageBox.Show("¿Seguro desea agregar el producto?", "Confirmacion", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                }
                else
                {
                    confirmacion = MessageBox.Show("¿Seguro desea editar el producto?", "Confirmacion", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                }

                // Si el usuario confirma la acción
                if (confirmacion == DialogResult.Yes)
                {
                    OpcionesCombo marcaSeleccionada = (OpcionesCombo)comboMarca.SelectedItem;
                    int idProveedorSel = marcaSeleccionada != null ? Convert.ToInt32(marcaSeleccionada.Valor) : 0;
                    string textoMarcaProv = marcaSeleccionada != null ? marcaSeleccionada.Texto : txtMarca.Texts;

                    // Crea un nuevo objeto Producto
                    Producto objProducto = new Producto()
                    {
                        IdProducto = Convert.ToInt32(txtid.Text),
                        Codigo = txtCodigo.Texts,
                        Nombre = txtNombre.Texts,
                        Descripcion = txtDescripcion.Texts,
                        Marca = textoMarcaProv,
                        oCategoria = new Categoria() { IdCategoria = Convert.ToInt32(((OpcionesCombo)comboCategoria.SelectedItem).Valor) },
                        oProveedor = new Proveedor() { IdProveedor = idProveedorSel, RazonSocial = textoMarcaProv },
                        Estado = Convert.ToInt32(((OpcionesCombo)comboEstado.SelectedItem).Valor) == 1 ? true : false
                    };

                    // Si es un nuevo producto
                    if (objProducto.IdProducto == 0)
                    {
                        // Intenta registrar el producto
                        int idProductoGenerado = new CN_Producto().Registrar(objProducto, out mensaje);
                        if (idProductoGenerado != 0)
                        {
                            // Agrega el nuevo producto al DataGridView
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

                            LimpiarCampos(); // Limpia los campos del formulario
                        }
                        else
                        {
                            // Muestra un mensaje de error si no se pudo registrar
                            MessageBox.Show(mensaje);
                        }
                    }
                    else
                    {
                        // Si se está editando un producto existente
                        bool resultado = new CN_Producto().Editar(objProducto, out mensaje);
                        if (resultado == true)
                        {
                            // Actualiza la fila correspondiente en el DataGridView
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

                            LimpiarCampos(); // Limpia los campos del formulario
                        }
                        else
                        {
                            // Muestra un mensaje de error si no se pudo editar
                            MessageBox.Show(mensaje);
                        }
                    }
                }
            }
            else
            {
                // Muestra un mensaje de alerta si no se completaron todos los campos
                MessageBox.Show("Debe Completar todos los campos", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        // Evento que se ejecuta al hacer clic en el botón "Limpiar"
        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarCampos(); // Limpia los campos del formulario
        }

        // Evento que se ejecuta al hacer clic en el botón "Eliminar"
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (usuarioActual != null && usuarioActual.oRol != null && usuarioActual.oRol.IdRol != 1)
            {
                MessageBox.Show("El vendedor solo tiene permiso para visualizar productos.", "Acceso restringido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Verifica si se ha seleccionado un producto
            if (Convert.ToInt32(txtid.Text) != 0)
            {
                // Pregunta al usuario si desea eliminar el producto
                if (MessageBox.Show("¿Desea eliminar el Producto?", "Mensaje", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    string mensaje = string.Empty;
                    Producto objProducto = new Producto()
                    {
                        IdProducto = Convert.ToInt32(txtid.Text),
                    };

                    // Intenta eliminar el producto
                    bool respuesta = new CN_Producto().Eliminar(objProducto, out mensaje);
                    if (respuesta)
                    {
                        // Cambia el estado del producto a "No Activo" en el DataGridView
                        dgvData.Rows[Convert.ToInt32(txtindice.Text)].Cells["EstadoValor"].Value = 0;
                        dgvData.Rows[Convert.ToInt32(txtindice.Text)].Cells["Estado"].Value = "No Activo";

                        LimpiarCampos(); // Limpia los campos del formulario
                    }
                    else
                    {
                        // Muestra un mensaje de error si no se pudo eliminar
                        MessageBox.Show(mensaje, "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
            }
        }

        // Método para limpiar los campos del formulario
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
            txtCodigo.Select(); // Enfoca el campo Codigo
        }

        // Evento que se ejecuta al hacer clic en el DataGridView
        private void dgvData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Comprueba si se ha hecho clic en el botón de selección
            if (e.ColumnIndex >= 0 && dgvData.Columns[e.ColumnIndex].Name == "btnseleccionar")
            {
                int indice = e.RowIndex;

                if (indice >= 0)
                {
                    // Rellena los campos del formulario con los datos del producto seleccionado
                    txtindice.Text = indice.ToString();
                    txtid.Text = dgvData.Rows[indice].Cells["IdProducto"].Value.ToString();
                    txtCodigo.Texts = dgvData.Rows[indice].Cells["Codigo"].Value.ToString();
                    txtNombre.Texts = dgvData.Rows[indice].Cells["Nombre"].Value.ToString();
                    txtDescripcion.Texts = dgvData.Rows[indice].Cells["Descripcion"].Value.ToString();
                    txtMarca.Texts = dgvData.Rows[indice].Cells["Marca"].Value.ToString();

                    // Selecciona la marca/proveedor correspondiente en comboMarca
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
                            // Seleccionar "Otros proveedores" por defecto si no coincide con Nike/Adidas
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

                    // Selecciona la categoría correspondiente en el comboCategoria
                    foreach (OpcionesCombo oc in comboCategoria.Items)
                    {
                        if (Convert.ToInt32(oc.Valor) == Convert.ToInt32(dgvData.Rows[indice].Cells["IdCategoria"].Value))
                        {
                            int indiceCombo = comboCategoria.Items.IndexOf(oc);
                            comboCategoria.SelectedIndex = indiceCombo;
                            break;
                        }
                    }

                    // Selecciona el estado correspondiente en el comboEstado
                    foreach (OpcionesCombo oc in comboEstado.Items)
                    {
                        if (Convert.ToInt32(oc.Valor) == Convert.ToInt32(dgvData.Rows[indice].Cells["EstadoValor"].Value))
                        {
                            int indiceCombo = comboEstado.Items.IndexOf(oc);
                            comboEstado.SelectedIndex = indiceCombo;
                            break;
                        }
                    }
                }
            }
        }

        // Evento que se ejecuta al pintar celdas en el DataGridView
        private void dgvData_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            TemaCapybara.ModernDgv_CellPainting(sender, e);
        }

        // Evento que se ejecuta al hacer clic en el botón de búsqueda
        private void iconButton1_Click(object sender, EventArgs e)
        {
            string columnaFiltro = ((OpcionesCombo)comboBusqueda.SelectedItem).Valor.ToString();

            // Filtra las filas según el texto ingresado en txtBusqueda
            if (dgvData.Rows.Count > 0)
            {
                foreach (DataGridViewRow row in dgvData.Rows)
                {
                    if (row.Cells[columnaFiltro].Value != null &&
                        row.Cells[columnaFiltro].Value.ToString().Trim().ToUpper().Contains(txtBusqueda.Texts.Trim().ToUpper()))
                    {
                        row.Visible = true; // Muestra la fila si coincide
                    }
                    else
                    {
                        row.Visible = false; // Oculta la fila si no coincide
                    }
                }
            }
        }

        // Evento que se ejecuta al hacer clic en el botón de limpiar búsqueda
        private void btnLimpiarBusqueda_Click_1(object sender, EventArgs e)
        {
            txtBusqueda.Texts = ""; // Limpia el campo de búsqueda
            foreach (DataGridViewRow row in dgvData.Rows)
            {
                row.Visible = true; // Muestra todas las filas
            }
        }

        // Evento que se ejecuta al hacer clic en el botón de exportar a Excel
        private void btnExportar_Click(object sender, EventArgs e)
        {
            // Comprueba si hay filas para exportar
            if (dgvData.Rows.Count < 1)
            {
                MessageBox.Show("No hay datos para exportar", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                // Crea un DataTable para almacenar los datos a exportar
                DataTable dt = new DataTable();
                foreach (DataGridViewColumn colum in dgvData.Columns)
                {
                    if (colum.HeaderText != "" && colum.Visible)
                    {
                        dt.Columns.Add(colum.HeaderText, typeof(string)); // Agrega las columnas visibles
                    }
                }

                // Agrega las filas visibles al DataTable
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

                // Muestra un cuadro de diálogo para guardar el archivo
                SaveFileDialog savefile = new SaveFileDialog();
                savefile.FileName = string.Format("ReporteProducto_{0}.xlsx", DateTime.Now.ToString("ddMMyyyyHHmmss"));
                savefile.Filter = "Excel Files | *.xlsx";

                // Si el usuario selecciona un archivo para guardar
                if (savefile.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        // Crea un nuevo libro de Excel y guarda los datos
                        XLWorkbook wb = new XLWorkbook();
                        var hoja = wb.Worksheets.Add(dt, "Informe");
                        hoja.ColumnsUsed().AdjustToContents(); // Ajusta las columnas al contenido
                        wb.SaveAs(savefile.FileName); // Guarda el libro de Excel
                        MessageBox.Show("Reporte Generado", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch
                    {
                        // Muestra un mensaje de error si ocurre un problema al guardar
                        MessageBox.Show("Error al generar el informe", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
            }
        }

        private void txtNombre_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Verificar si el carácter es una tecla de control (como Backspace)
            bool esControl = Char.IsControl(e.KeyChar);

            // Verificar si el carácter es un dígito
            bool esDigito = Char.IsDigit(e.KeyChar);

            // Verificar la longitud actual del texto y permitir solo hasta 80 caracteres
            bool longitudPermitida = txtNombre.Texts.Length < 80;

            // Permitir el carácter solo si es una tecla de control o no es dígito y la longitud permitida no se ha alcanzado
            if (esControl || (!esDigito && longitudPermitida))
            {
                e.Handled = false; // Permitir el carácter
            }
            else
            {
                e.Handled = true; // Bloquear el carácter
            }
        }

        private void txtDescripcion_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Verificar si el carácter es una tecla de control (como Backspace)
            bool esControl = Char.IsControl(e.KeyChar);

            // Verificar la longitud actual del texto y permitir solo hasta 100 caracteres
            bool longitudPermitida = txtDescripcion.Texts.Length < 100;

            // Permitir el carácter solo si es una tecla de control o la longitud permitida no se ha alcanzado
            if (esControl || longitudPermitida)
            {
                e.Handled = false; // Permitir el carácter
            }
            else
            {
                e.Handled = true; // Bloquear el carácter
            }
        }

        private void txtCodigo_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Verificar si el carácter es una tecla de control (como Backspace)
            bool esControl = Char.IsControl(e.KeyChar);

            // Verificar si el carácter es un dígito
            bool esDigito = Char.IsDigit(e.KeyChar);

            // Verificar la longitud actual del texto y permitir solo hasta 100 dígitos
            bool longitudPermitida = txtCodigo.Texts.Length < 100;

            // Permitir el carácter solo si es una tecla de control o un dígito y la longitud permitida no se ha alcanzado
            if (esControl || (esDigito && longitudPermitida))
            {
                e.Handled = false; // Permitir el carácter
            }
            else
            {
                e.Handled = true; // Bloquear el carácter
            }
        }

        private void txtNombre__TextChanged(object sender, EventArgs e)
        {

        }

        private void txtMarca_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Verificar si el carácter es una tecla de control (como Backspace)
            bool esControl = Char.IsControl(e.KeyChar);

            // Verificar si el carácter es un dígito
            bool esDigito = Char.IsDigit(e.KeyChar);

            // Verificar la longitud actual del texto y permitir solo hasta 80 caracteres
            bool longitudPermitida = txtMarca.Texts.Length < 80;

            // Permitir el carácter solo si es una tecla de control o no es dígito y la longitud permitida no se ha alcanzado
            if (esControl || (!esDigito && longitudPermitida))
            {
                e.Handled = false; // Permitir el carácter
            }
            else
            {
                e.Handled = true; // Bloquear el carácter
            }
        }

        private void txtBusqueda_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Verificar si el carácter es una tecla de control (como Backspace)
            bool esControl = Char.IsControl(e.KeyChar);

            // Verificar la longitud actual del texto y permitir solo hasta 80 caracteres
            bool longitudPermitida = txtBusqueda.Texts.Length < 80;

            // Permitir el carácter solo si es una tecla de control o un caracter y la longitud permitida no se ha alcanzado
            if (esControl || longitudPermitida)
            {
                e.Handled = false; // Permitir el carácter
            }
            else
            {
                e.Handled = true; // Bloquear el carácter
            }
        }

        private void dgvData_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (this.dgvData.Columns[e.ColumnIndex].Name == "Estado") // Verifica la columna "Estado"
            {
                if (e.Value != null && e.Value.ToString() == "No Activo") // Si el estado es "No Activo"
                {
                    e.CellStyle.BackColor = Color.Red; // Cambia el color de fondo a rojo
                    e.CellStyle.ForeColor = Color.Black; // Cambia el color de texto a negro
                }
            }
        }
    }
}
