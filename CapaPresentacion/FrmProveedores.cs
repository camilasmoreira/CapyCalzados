using CapaEntidad;
using CapaNegocio;
using CapaPresentacion.Utilidades;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace CapaPresentacion
{
    public partial class FrmProveedores : Form
    {
        public FrmProveedores()
        {
            InitializeComponent();
        }

        private void FrmProveedores_Load(object sender, EventArgs e)
        {
            // Inicializa un ToolTip para el botón de eliminación y lo configura con el texto "Eliminar".
            ToolTip toolTip1 = new ToolTip();
            toolTip1.SetToolTip(BtnEliminar2, "Eliminar");

            // Agrega opciones de estado (Activo y No Activo) al ComboBox.
            comboEstado.Items.Add(new OpcionesCombo() { Valor = 1, Texto = "Activo" });
            comboEstado.Items.Add(new OpcionesCombo() { Valor = 0, Texto = "No Activo" });

            // Configura las propiedades del ComboBox para mostrar texto y valor.
            comboEstado.DisplayMember = "Texto";
            comboEstado.ValueMember = "Valor";
            comboEstado.SelectedIndex = 0; // Establece el valor por defecto en "Activo".

            // Agrega las columnas visibles del DataGridView al ComboBox de búsqueda.
            foreach (DataGridViewColumn columna in dgvData.Columns)
            {
                if (columna.Visible == true && columna.Name != "btnseleccionar")
                {
                    comboBusqueda.Items.Add(new OpcionesCombo() { Valor = columna.Name, Texto = columna.HeaderText });
                }
            }
            comboBusqueda.DisplayMember = "Texto";
            comboBusqueda.ValueMember = "Valor";
            comboBusqueda.SelectedIndex = 0; // Establece el valor por defecto.

            // Carga la lista de proveedores desde la capa de negocio y la muestra en el DataGridView.
            List<Proveedor> listaProveedors = new CN_Proveedor().Listar();
            foreach (Proveedor item in listaProveedors)
            {
                dgvData.Rows.Add(new object[] {
            "", // Columna para íconos (si aplica)
            item.IdProveedor,
            item.Documento,
            item.RazonSocial,
            item.Direccion,
            item.Correo,
            item.Telefono,
            item.Estado == true ? 1 : 0, // Representación del estado como entero.
            item.Estado == true ? "Activo" : "No Activo" // Texto del estado.
        });
            }
        }



        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            // Llama al método para limpiar todos los campos del formulario.
            LimpiarCampos();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            // Verifica si el ID del proveedor es diferente de 0.
            if (Convert.ToInt32(txtid.Text) != 0)
            {
                // Muestra un mensaje de confirmación antes de proceder a eliminar.
                if (MessageBox.Show("¿Desea eliminar el Proveedor?", "Mensaje", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    string mensaje = string.Empty;
                    Proveedor objProveedor = new Proveedor()
                    {
                        IdProveedor = Convert.ToInt32(txtid.Text), // Asigna el ID del proveedor a eliminar.
                    };

                    // Llama al método de eliminación en la capa de negocio y recibe el mensaje de respuesta.
                    bool respuesta = new CN_Proveedor().Eliminar(objProveedor, out mensaje);
                    if (respuesta)
                    {
                        // Elimina la fila correspondiente del DataGridView.
                        dgvData.Rows.RemoveAt(Convert.ToInt32(txtindice.Text));
                    }
                    else
                    {
                        // Muestra un mensaje de alerta si la eliminación falla.
                        MessageBox.Show(mensaje, "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
            }
        }


        private void btnBuscar_Click(object sender, EventArgs e)
        {
            // Obtiene el nombre de la columna seleccionada en el ComboBox de búsqueda.
            string columnaFiltro = ((OpcionesCombo)comboBusqueda.SelectedItem).Valor.ToString();

            // Verifica que haya filas en el DataGridView para realizar la búsqueda.
            if (dgvData.Rows.Count > 0)
            {
                // Recorre cada fila del DataGridView para aplicar el filtro de búsqueda.
                foreach (DataGridViewRow row in dgvData.Rows)
                {
                    // Comprueba si el valor de la celda en la columna seleccionada contiene el texto de búsqueda.
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

        private void btnLimpiarBusqueda_Click(object sender, EventArgs e)
        {
            // Limpia el campo de búsqueda y muestra todas las filas del DataGridView.
            txtBusqueda.Texts = "";
            foreach (DataGridViewRow row in dgvData.Rows)
            {
                row.Visible = true; // Muestra todas las filas.
            }
        }


        private void dgvData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Verifica si el clic fue en la columna "btnseleccionar".
            if (dgvData.Columns[e.ColumnIndex].Name == "btnseleccionar")
            {
                int indice = e.RowIndex; // Obtiene el índice de la fila seleccionada.

                if (indice >= 0)
                {
                    LimpiarCampos(); // Limpia los campos del formulario antes de cargar nuevos datos.
                    txtindice.Text = indice.ToString(); // Guarda el índice de la fila seleccionada.
                    txtid.Text = dgvData.Rows[indice].Cells["IdProveedor"].Value.ToString(); // Asigna el ID del proveedor.
                    txtDocumento.Texts = dgvData.Rows[indice].Cells["Documento"].Value.ToString(); // Asigna el Documento.
                    txtRazon.Texts = dgvData.Rows[indice].Cells["RazonSocial"].Value.ToString(); // Asigna la Razón Social.
                    txtDireccion.Texts = dgvData.Rows[indice].Cells["Direccion"].Value.ToString(); // Asigna la Dirección.
                    txtCorreo.Texts = dgvData.Rows[indice].Cells["Correo"].Value.ToString(); // Asigna el Correo.
                    txtTelefono.Texts = dgvData.Rows[indice].Cells["Telefono"].Value.ToString(); // Asigna el Teléfono.

                    // Establece el estado del proveedor en el ComboBox de estado.
                    foreach (OpcionesCombo oc in comboEstado.Items)
                    {
                        if (Convert.ToInt32(oc.Valor) == Convert.ToInt32(dgvData.Rows[indice].Cells["EstadoValor"].Value))
                        {
                            int indiceCombo = comboEstado.Items.IndexOf(oc);
                            comboEstado.SelectedIndex = indiceCombo; // Selecciona el estado correspondiente.
                            break;
                        }
                    }
                }
            }
        }

        private void dgvData_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            // Verifica si el índice de la fila es válido.
            if (e.RowIndex < 0)
            {
                return; // No realiza ninguna acción si el índice es inválido.
            }
            // Verifica si la columna actual es la columna de íconos.
            if (e.ColumnIndex == 0)
            {
                // Dibuja la celda y centra el ícono.
                e.Paint(e.CellBounds, DataGridViewPaintParts.All);
                var w = Properties.Resources.comprobado.Width - 15; // Ancho del ícono ajustado.
                var h = Properties.Resources.comprobado.Height - 15; // Alto del ícono ajustado.

                var x = e.CellBounds.Left + (e.CellBounds.Width - w) / 2; // Calcula la posición X.
                var y = e.CellBounds.Top + (e.CellBounds.Height - h) / 2; // Calcula la posición Y.

                // Dibuja el ícono en la celda.
                e.Graphics.DrawImage(Properties.Resources.comprobado, new Rectangle(x, y, w, h));
                e.Handled = true; // Indica que el evento ha sido manejado.
            }
        }

        private void LimpiarCampos()
        {
            // Restablece todos los campos a su estado inicial.
            txtindice.Text = "-1"; // Restablece el índice.
            txtid.Text = "0"; // Restablece el ID del proveedor.
            txtDocumento.Texts = ""; // Limpia el campo de Documento.
            txtRazon.Texts = ""; // Limpia el campo de Razón Social.
            txtCorreo.Texts = ""; // Limpia el campo de Correo.
            txtTelefono.Texts = ""; // Limpia el campo de Teléfono.
            txtDireccion.Texts = ""; // Limpia el campo de Dirección.
            comboEstado.SelectedIndex = 0; // Restablece el ComboBox de estado a "Activo".
            txtDocumento.Select(); // Selecciona el campo de Documento para facilitar la entrada.
        }

        private bool Validaciones()
        {
            // Método que valida los campos del formulario y devuelve un valor booleano.
            bool validaciones = true;

            // Verifica que los campos requeridos no estén vacíos.
            if (txtDocumento.Texts == "")
            {
                validaciones = false; // Documento vacío.
            }
            if (txtRazon.Texts == "")
            {
                validaciones = false; // Razón Social vacía.
            }
            if (txtDireccion.Texts == "")
            {
                validaciones = false; // Dirección vacía.
            }
            if (txtTelefono.Texts == "")
            {
                validaciones = false; // Teléfono vacío.
            }
            if (txtCorreo.Texts == "" || !(txtCorreo.Texts.Contains("@")))
            {
                validaciones = false; // Correo vacío o inválido.
            }
            return validaciones; // Retorna el resultado de las validaciones.
        }



        private void BtnGuardar_Click(object sender, EventArgs e)
        {
            if (Validaciones())
            {
                string mensaje = string.Empty;
                DialogResult confirmacion;


                if (Convert.ToInt32(txtid.Text) == 0)
                {
                    confirmacion = MessageBox.Show("¿Seguro desea agregar el proveedor?", "Confirmacion", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                }
                else
                {
                    confirmacion = MessageBox.Show("¿Seguro desea editar el proveedor?", "Confirmacion", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                }

                if (confirmacion == DialogResult.Yes)
                {
                   
                    Proveedor objProveedor = new Proveedor()
                    {
                        IdProveedor = Convert.ToInt32(txtid.Text),
                        Documento = txtDocumento.Texts,
                        RazonSocial = txtRazon.Texts,
                        Direccion = txtDireccion.Texts,
                        Correo = txtCorreo.Texts,
                        Telefono = txtTelefono.Texts,
                        Estado = Convert.ToInt32(((OpcionesCombo)comboEstado.SelectedItem).Valor) == 1 ? true : false
                    };

                    if (objProveedor.IdProveedor == 0)
                    {
                        int idProveedorGenerado = new CN_Proveedor().Registrar(objProveedor, out mensaje);
                        if (idProveedorGenerado != 0)
                        {
                            dgvData.Rows.Add(new object[] {"",idProveedorGenerado,txtDocumento.Texts,txtRazon.Texts,txtDireccion.Texts,txtCorreo.Texts,txtTelefono.Texts,
                            ((OpcionesCombo)comboEstado.SelectedItem).Valor.ToString(),
                            ((OpcionesCombo)comboEstado.SelectedItem).Texto.ToString()});
                            LimpiarCampos();
                        }
                        else
                        {
                            MessageBox.Show(mensaje, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        bool resultado = new CN_Proveedor().Editar(objProveedor, out mensaje);
                        if (resultado == true)
                        {
                            DataGridViewRow row = dgvData.Rows[Convert.ToInt32(txtindice.Text)];
                            row.Cells["IdProveedor"].Value = txtid.Text;
                            row.Cells["Documento"].Value = txtDocumento.Texts;
                            row.Cells["RazonSocial"].Value = txtRazon.Texts;
                            row.Cells["Direccion"].Value = txtDireccion.Texts;
                            row.Cells["Correo"].Value = txtCorreo.Texts;
                            row.Cells["Telefono"].Value = txtTelefono.Texts;
                            row.Cells["EstadoValor"].Value = ((OpcionesCombo)comboEstado.SelectedItem).Valor.ToString();
                            row.Cells["Estado"].Value = ((OpcionesCombo)comboEstado.SelectedItem).Texto.ToString();
                            LimpiarCampos();
                        }
                        else
                        {
                            MessageBox.Show(mensaje, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }

                }
            }
            else
            {

                MessageBox.Show("Debe Completar todos los campos", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void BtnLimpiar2_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
        }

        private void BtnEliminar2_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(txtid.Text) != 0)
            {
                if (MessageBox.Show("¿Desea eliminar el Proveedor?", "Mensaje", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    string mensaje = string.Empty;
                    Proveedor objProveedor = new Proveedor()
                    {
                        IdProveedor = Convert.ToInt32(txtid.Text),
                    };

                    bool respuesta = new CN_Proveedor().Eliminar(objProveedor, out mensaje); ;
                    if (respuesta)
                    {
                        dgvData.Rows.RemoveAt(Convert.ToInt32(txtindice.Text));
                    }
                    else
                    {
                        MessageBox.Show(mensaje, "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
            }
        }

       

        private void txtDocumento_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Verificar si el carácter es una tecla de control (como Backspace)
            bool esControl = Char.IsControl(e.KeyChar);

            // Verificar si el carácter es un dígito
            bool esDigito = Char.IsDigit(e.KeyChar);

            // Verificar la longitud actual del texto y permitir solo hasta 8 dígitos
            bool longitudPermitida = txtDocumento.Texts.Length < 8;

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

        private void txtTelefono_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Verificar si el carácter es una tecla de control (como Backspace)
            bool esControl = Char.IsControl(e.KeyChar);

            // Verificar si el carácter es un dígito
            bool esDigito = Char.IsDigit(e.KeyChar);

           

            // Verificar la longitud actual del texto y permitir solo hasta 8 dígitos
            bool longitudPermitida = txtTelefono.Texts.Length < 11;

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

        private void txtRazon_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Verificar si el carácter es una tecla de control (como Backspace)
            bool esControl = Char.IsControl(e.KeyChar);

            // Verificar si el carácter es un dígito
            bool esDigito = Char.IsDigit(e.KeyChar);

            // Verificar la longitud actual del texto y permitir solo hasta 8 dígitos
            bool longitudPermitida = txtRazon.Texts.Length < 80;

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

        private void txtBusqueda__TextChanged(object sender, EventArgs e)
        {

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

        private void txtCorreo_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Verificar si el carácter es una tecla de control (como Backspace)
            bool esControl = Char.IsControl(e.KeyChar);

            // Verificar la longitud actual del texto y permitir solo hasta 8 dígitos
            bool longitudPermitida = txtCorreo.Texts.Length < 100;

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

        private void txtDireccion_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Verificar si el carácter es una tecla de control (como Backspace)
            bool esControl = Char.IsControl(e.KeyChar);

            // Verificar la longitud actual del texto y permitir solo hasta 8 dígitos
            bool longitudPermitida = txtCorreo.Texts.Length < 100;

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
        // Método que se ejecuta al formatear las celdas del DataGridView
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

        private void txtDocumento__TextChanged(object sender, EventArgs e)
        {

        }
    }
}
