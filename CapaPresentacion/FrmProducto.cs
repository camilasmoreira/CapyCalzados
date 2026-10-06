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
        // Constructor de la clase FrmProducto
        public FrmProducto(Inicio inicio)
        {
            InitializeComponent();
            Inicio = inicio;
            Inicio.pintar();
        }

        // Evento que se ejecuta al cargar el formulario
        private void FrmProducto_Load(object sender, EventArgs e)
        {
            // Inicializa el comboEstado con opciones "Activo" y "No Activo"
            comboEstado.Items.Add(new OpcionesCombo() { Valor = 1, Texto = "Activo" });
            comboEstado.Items.Add(new OpcionesCombo() { Valor = 0, Texto = "No Activo" });

            comboEstado.DisplayMember = "Texto";
            comboEstado.ValueMember = "Valor";
            comboEstado.SelectedIndex = 0; // Selecciona "Activo" por defecto

            // Carga la lista de categorías y las agrega al comboCategoria
            List<Categoria> listaCategoria = new CN_Categoria().Listar().Where(p => p.Estado == true).ToList();
            if (listaCategoria != null && listaCategoria.Count > 0)
            {
                foreach (Categoria item in listaCategoria)
                {
                    comboCategoria.Items.Add(new OpcionesCombo() { Valor = item.IdCategoria, Texto = item.Descripcion });
                }
            }
            else
            {
                // Si no hay categorías, se añade la opción "Todos"
                comboCategoria.Items.Add(new OpcionesCombo() { Valor = 0, Texto = "Todos" });
            }

            comboCategoria.DisplayMember = "Texto";
            comboCategoria.ValueMember = "Valor";
            comboCategoria.SelectedIndex = 0; // Solo establece el índice si hay elementos

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

            // Muestra los productos en el DataGridView
            List<Producto> listaProductos = new CN_Producto().Listar();
            foreach (Producto item in listaProductos)
            {
                dgvData.Rows.Add(new object[] {
                    "",
                    item.IdProducto,
                    item.Codigo,
                    item.Nombre,
                    item.Descripcion,
                    item.Marca,
                    item.oCategoria.IdCategoria,
                    item.oCategoria.Descripcion,
                    item.Stock,
                    item.PrecioCompra,
                    item.PrecioVenta,
                    item.Estado == true ? 1 : 0,
                    item.Estado == true ? "Activo": "No Activo"
                });
            }

            // Re-establece la selección del comboCategoria
            comboCategoria.DisplayMember = "Texto";
            comboCategoria.ValueMember = "Valor";
            comboCategoria.SelectedIndex = 0;
        }

        // Método para validar los campos del formulario
        private bool Validaciones()
        {
            bool validaciones = true;

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
                    // Crea un nuevo objeto Producto
                    Producto objProducto = new Producto()
                    {
                        IdProducto = Convert.ToInt32(txtid.Text),
                        Codigo = txtCodigo.Texts,
                        Nombre = txtNombre.Texts,
                        Descripcion = txtDescripcion.Texts,
                        Marca = txtMarca.Texts,
                        oCategoria = new Categoria() { IdCategoria = Convert.ToInt32(((OpcionesCombo)comboCategoria.SelectedItem).Valor) },
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
                                txtMarca.Texts,
                                ((OpcionesCombo)comboCategoria.SelectedItem).Valor.ToString(),
                                ((OpcionesCombo)comboCategoria.SelectedItem).Texto.ToString(),
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
                            row.Cells["Marca"].Value = txtMarca.Texts;
                            row.Cells["IdCategoria"].Value = ((OpcionesCombo)comboCategoria.SelectedItem).Valor.ToString();
                            row.Cells["Categoria"].Value = ((OpcionesCombo)comboCategoria.SelectedItem).Texto.ToString();
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
            txtMarca.Texts = "";
            comboCategoria.SelectedIndex = 0;
            comboEstado.SelectedIndex = 0;
            txtCodigo.Select(); // Enfoca el campo Codigo
        }

        // Evento que se ejecuta al hacer clic en el DataGridView
        private void dgvData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Comprueba si se ha hecho clic en el botón de selección
            if (dgvData.Columns[e.ColumnIndex].Name == "btnseleccionar")
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
            if (e.RowIndex < 0)
            {
                return; // No hacer nada si no es una fila válida
            }
            if (e.ColumnIndex == 0)
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All);
                var w = Properties.Resources.comprobado.Width - 15; // Ancho de la imagen
                var h = Properties.Resources.comprobado.Height - 15; // Alto de la imagen

                // Calcula la posición de la imagen centrada en la celda
                var x = e.CellBounds.Left + (e.CellBounds.Width - w) / 2;
                var y = e.CellBounds.Top + (e.CellBounds.Height - h) / 2;

                e.Graphics.DrawImage(Properties.Resources.comprobado, new Rectangle(x, y, w, h)); // Dibuja la imagen
                e.Handled = true; // Marca la celda como manejada
            }
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
                    if (row.Cells[columnaFiltro].Value.ToString().Trim().ToUpper().Contains(txtBusqueda.Texts.Trim().ToUpper()))
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
                        dt.Rows.Add(new object[] {
                            rows.Cells[2].Value.ToString(), // Código
                            rows.Cells[3].Value.ToString(), // Nombre
                            rows.Cells[4].Value.ToString(), // Descripción
                            rows.Cells[5].Value.ToString(), // Marca
                            rows.Cells[7].Value.ToString(), // Categoría
                            rows.Cells[8].Value.ToString(), // Stock
                            rows.Cells[9].Value.ToString(), // Precio Compra
                            rows.Cells[10].Value.ToString(), // Precio Venta
                            rows.Cells[12].Value.ToString(), // Estado
                        });
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

            // Verificar la longitud actual del texto y permitir solo hasta 8 dígitos
            bool longitudPermitida = txtNombre.Texts.Length < 80;

            // Permitir el carácter solo si es una tecla de control o un dígito y la longitud permitida no se ha alcanzado
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

            // Verificar si el carácter es un dígito
            bool esDigito = Char.IsDigit(e.KeyChar);

            // Verificar la longitud actual del texto y permitir solo hasta 8 dígitos
            bool longitudPermitida = txtDescripcion.Texts.Length < 100;

            // Permitir el carácter solo si es una tecla de control o un dígito y la longitud permitida no se ha alcanzado
            if (esControl ||  longitudPermitida)
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

            // Verificar la longitud actual del texto y permitir solo hasta 8 dígitos
            bool longitudPermitida = txtCodigo.Texts.Length < 100;

            // Permitir el carácter solo si es una tecla de control o un caracter y la longitud permitida no se ha alcanzado
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

            // Verificar la longitud actual del texto y permitir solo hasta 8 dígitos
            bool longitudPermitida = txtMarca.Texts.Length < 80;

            // Permitir el carácter solo si es una tecla de control o un dígito y la longitud permitida no se ha alcanzado
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

            // Verificar la longitud actual del texto y permitir solo hasta 8 dígitos
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
                if (e.Value.ToString() == "No Activo") // Si el estado es "No Activo"
                {
                    e.CellStyle.BackColor = Color.Red; // Cambia el color de fondo a rojo
                    e.CellStyle.ForeColor = Color.Black; // Cambia el color de texto a negro
                }
            }
        }
    }
}
