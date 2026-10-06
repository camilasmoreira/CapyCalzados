using CapaEntidad; 
using CapaNegocio; 
using CapaPresentacion.Utilidades; 
using System; 
using System.Collections.Generic; 
using System.Drawing; 
using System.Windows.Forms; 

namespace CapaPresentacion // Define el espacio de nombres para la capa de presentación.
{
    public partial class FrmCategoria : Form // Define la clase FrmCategoria que hereda de Form.
    {
        public Inicio Inicio2 { get; set; }
        public FrmCategoria(Inicio inicio) // Constructor de la clase.
        {
            InitializeComponent(); // Inicializa los componentes del formulario.
            Inicio2 = inicio;
            Inicio2.pintar();
        }

        private void FrmCategoria_Load(object sender, EventArgs e) // Maneja el evento de carga del formulario.
        {

         
            // Agrega opciones de estado al comboEstado.
            comboEstado.Items.Add(new OpcionesCombo() { Valor = 1, Texto = "Activo" });
            comboEstado.Items.Add(new OpcionesCombo() { Valor = 0, Texto = "No Activo" });

            comboEstado.DisplayMember = "Texto"; // Define el miembro que se mostrará en el combo.
            comboEstado.ValueMember = "Valor"; // Define el miembro que tendrá el valor del combo.
            comboEstado.SelectedIndex = 0; // Establece el primer elemento como seleccionado.

            // Llena el comboBusqueda con columnas visibles del DataGridView.
            foreach (DataGridViewColumn columna in dgvData.Columns)
            {
                if (columna.Visible == true && columna.Name != "btnseleccionar")
                {
                    comboBusqueda.Items.Add(new OpcionesCombo() { Valor = columna.Name, Texto = columna.HeaderText });
                }
            }
            comboBusqueda.DisplayMember = "Texto"; // Define el miembro que se mostrará en el combo.
            comboBusqueda.ValueMember = "Valor"; // Define el miembro que tendrá el valor del combo.
            comboBusqueda.SelectedIndex = 0; // Establece el primer elemento como seleccionado.

            // Muestra las categorías en el DataGridView.
            List<Categoria> listaCategorias = new CN_Categoria().Listar(); // Obtiene la lista de categorías.

            foreach (Categoria item in listaCategorias)
            {
                dgvData.Rows.Add(new object[] { "", item.IdCategoria, item.Descripcion,
                    item.Estado == true ? 1 : 0,
                    item.Estado == true ? "Activo" : "No Activo"
                });
            }
        }

        private void LimpiarCampos() // Método para limpiar los campos del formulario.
        {
            txtindice.Text = "-1"; // Reinicia el índice.
            txtid.Text = "0"; // Reinicia el ID.
            txtDescripcion.Texts = ""; // Limpia el campo de descripción.
            comboEstado.SelectedIndex = 0; // Restablece el estado al primero.
            txtDescripcion.Select(); // Selecciona el campo de descripción.
        }

        private bool Validaciones() // Método para validar campos antes de guardar.
        {
            bool confirmacion = true;

            if (txtDescripcion.Texts == "") // Verifica si el campo de descripción está vacío.
            {
                confirmacion = false;
            }

            return confirmacion; // Retorna el resultado de la validación.
        }

        private void btnGuardar_Click(object sender, EventArgs e) // Maneja el evento click del botón guardar.
        {
            if (Validaciones()) // Verifica si los campos son válidos.
            {
                DialogResult confirmacion; // Variable para almacenar la respuesta del diálogo de confirmación.
                string mensaje = string.Empty; // Mensaje para almacenar posibles errores.

                // Pregunta al usuario si desea agregar o editar una categoría.
                if (Convert.ToInt32(txtid.Text) == 0)
                {
                    confirmacion = MessageBox.Show("¿Seguro desea agregar la Categoria?", "Confirmacion", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                }
                else
                {
                    confirmacion = MessageBox.Show("¿Seguro desea editar la Categoria?", "Confirmacion", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                }

                if (confirmacion == DialogResult.Yes) // Si el usuario confirma la acción.
                {
                    // Crea un objeto de categoría con los datos ingresados.
                    Categoria objCategoria = new Categoria()
                    {
                        IdCategoria = Convert.ToInt32(txtid.Text),
                        Descripcion = txtDescripcion.Texts,
                        Estado = Convert.ToInt32(((OpcionesCombo)comboEstado.SelectedItem).Valor) == 1 ? true : false
                    };

                    // Si es una nueva categoría, la registra.
                    if (objCategoria.IdCategoria == 0)
                    {
                        int idCategoriaGenerado = new CN_Categoria().Registrar(objCategoria, out mensaje);
                        if (idCategoriaGenerado != 0) // Si se genera un ID válido.
                        {
                            // Agrega la nueva categoría al DataGridView.
                            dgvData.Rows.Add(new object[] { "", idCategoriaGenerado, txtDescripcion.Texts,
                                ((OpcionesCombo)comboEstado.SelectedItem).Valor.ToString(),
                                ((OpcionesCombo)comboEstado.SelectedItem).Texto.ToString() });
                            LimpiarCampos(); // Limpia los campos.
                        }
                        else
                        {
                            MessageBox.Show(mensaje); // Muestra un mensaje de error.
                        }
                    }
                    else // Si se está editando una categoría existente.
                    {
                        bool resultado = new CN_Categoria().Editar(objCategoria, out mensaje);
                        if (resultado == true) // Si la edición es exitosa.
                        {
                            DataGridViewRow row = dgvData.Rows[Convert.ToInt32(txtindice.Text)]; // Obtiene la fila correspondiente.
                            // Actualiza los valores en la fila.
                            row.Cells["IdCategoria"].Value = txtid.Text;
                            row.Cells["Descripcion"].Value = txtDescripcion.Texts;
                            row.Cells["EstadoValor"].Value = ((OpcionesCombo)comboEstado.SelectedItem).Valor.ToString();
                            row.Cells["Estado"].Value = ((OpcionesCombo)comboEstado.SelectedItem).Texto.ToString();
                            LimpiarCampos(); // Limpia los campos.
                        }
                        else
                        {
                            MessageBox.Show(mensaje); // Muestra un mensaje de error.
                        }
                    }
                }
            }
            else // Si las validaciones fallan.
            {
                MessageBox.Show("Debe Completar todos los campos", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void dgvData_CellContentClick(object sender, DataGridViewCellEventArgs e) // Maneja el evento click en las celdas del DataGridView.
        {
            if (dgvData.Columns[e.ColumnIndex].Name == "btnseleccionar") // Si la columna es de selección.
            {
                int indice = e.RowIndex; // Obtiene el índice de la fila seleccionada.

                if (indice >= 0) // Si el índice es válido.
                {
                    // Carga los datos de la categoría en los campos del formulario.
                    txtindice.Text = indice.ToString();
                    txtid.Text = dgvData.Rows[indice].Cells["IdCategoria"].Value.ToString();
                    txtDescripcion.Texts = dgvData.Rows[indice].Cells["Descripcion"].Value.ToString();

                    // Selecciona el estado correspondiente en el comboEstado.
                    foreach (OpcionesCombo oc in comboEstado.Items)
                    {
                        if (Convert.ToInt32(oc.Valor) == Convert.ToInt32(dgvData.Rows[indice].Cells["EstadoValor"].Value))
                        {
                            int indiceCombo = comboEstado.Items.IndexOf(oc);
                            comboEstado.SelectedIndex = indiceCombo; // Establece el índice seleccionado.
                            break;
                        }
                    }
                }
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e) // Maneja el evento click del botón eliminar.
        {
            if (Convert.ToInt32(txtid.Text) != 0) // Verifica que haya un ID válido.
            {
                if (MessageBox.Show("¿Desea eliminar la Categoria?", "Mensaje", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes) // Confirma la eliminación.
                {
                    string mensaje = string.Empty; // Mensaje para almacenar posibles errores.
                    Categoria objCategoria = new Categoria() // Crea un objeto de categoría con el ID.
                    {
                        IdCategoria = Convert.ToInt32(txtid.Text),
                    };

                    bool respuesta = new CN_Categoria().Eliminar(objCategoria, out mensaje); // Llama al método de eliminación.
                    if (respuesta) // Si la eliminación es exitosa.
                    {
                        dgvData.Rows.RemoveAt(Convert.ToInt32(txtindice.Text)); // Elimina la fila del DataGridView.
                        LimpiarCampos(); // Limpia los campos.
                    }
                    else
                    {
                        MessageBox.Show(mensaje, "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); // Muestra un mensaje de error.
                    }
                }
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e) // Maneja el evento click del botón limpiar.
        {
            LimpiarCampos(); // Limpia los campos.
        }

        private void dgvData_CellPainting_1(object sender, DataGridViewCellPaintingEventArgs e) // Maneja el evento de pintura de celdas del DataGridView.
        {
            if (e.RowIndex < 0) // Si no es una fila válida.
            {
                return;
            }
            if (e.ColumnIndex == 0) // Si la columna es la primera.
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All); // Dibuja la celda.
                var w = Properties.Resources.comprobado.Width - 15; // Ancho de la imagen.
                var h = Properties.Resources.comprobado.Height - 15; // Alto de la imagen.

                var x = e.CellBounds.Left + (e.CellBounds.Width - w) / 2; // Calcula la posición X para centrar la imagen.
                var y = e.CellBounds.Top + (e.CellBounds.Height - h) / 2; // Calcula la posición Y para centrar la imagen.

                e.Graphics.DrawImage(Properties.Resources.comprobado, new Rectangle(x, y, w, h)); // Dibuja la imagen en la celda.
                e.Handled = true; // Marca el evento como manejado.
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e) // Maneja el evento click del botón buscar.
        {
            string columnaFiltro = ((OpcionesCombo)comboBusqueda.SelectedItem).Valor.ToString(); // Obtiene la columna seleccionada para filtrar.

            if (dgvData.Rows.Count > 0) // Si hay filas en el DataGridView.
            {
                foreach (DataGridViewRow row in dgvData.Rows) // Recorre cada fila.
                {
                    // Filtra las filas basándose en el texto de búsqueda.
                    if (row.Cells[columnaFiltro].Value.ToString().Trim().ToUpper().Contains(txtBusqueda.Texts.Trim().ToUpper()))
                    {
                        row.Visible = true; // Muestra la fila si coincide.
                    }
                    else
                    {
                        row.Visible = false; // Oculta la fila si no coincide.
                    }
                }
            }
        }

        private void btnLimpiarBusqueda_Click(object sender, EventArgs e) // Maneja el evento click del botón limpiar búsqueda.
        {
            txtBusqueda.Texts = ""; // Limpia el campo de búsqueda.
            foreach (DataGridViewRow row in dgvData.Rows) // Muestra todas las filas del DataGridView.
            {
                row.Visible = true; // Restablece la visibilidad de las filas.
            }
        }

        private void txtDescripcion_KeyPress(object sender, KeyPressEventArgs e) // Maneja el evento de presión de teclas en el campo de descripción.
        {
            // Verifica si el carácter es una tecla de control (como Backspace).
            bool esControl = Char.IsControl(e.KeyChar);

            // Verifica si el carácter es un dígito.
            bool esDigito = Char.IsDigit(e.KeyChar);

            // Verifica la longitud actual del texto y permite solo hasta 80 caracteres.
            bool longitudPermitida = txtDescripcion.Texts.Length < 80;

            // Permite el carácter solo si es una tecla de control o un carácter y la longitud permitida no se ha alcanzado.
            if (esControl || (!esDigito && longitudPermitida))
            {
                e.Handled = false; // Permite el carácter.
            }
            else
            {
                e.Handled = true; // Bloquea el carácter.
            }
        }

        private void txtBusqueda_KeyPress(object sender, KeyPressEventArgs e) // Maneja el evento de presión de teclas en el campo de búsqueda.
        {
            // Verifica si el carácter es una tecla de control (como Backspace).
            bool esControl = Char.IsControl(e.KeyChar);

            // Verifica la longitud actual del texto y permite solo hasta 80 caracteres.
            bool longitudPermitida = txtBusqueda.Texts.Length < 80;

            // Permite el carácter solo si es una tecla de control o un carácter y la longitud permitida no se ha alcanzado.
            if (esControl || longitudPermitida)
            {
                e.Handled = false; // Permite el carácter.
            }
            else
            {
                e.Handled = true; // Bloquea el carácter.
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
