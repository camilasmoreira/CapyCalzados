using CapaEntidad;
using CapaNegocio;
using CapaPresentacion.Utilidades;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace CapaPresentacion
{
    public partial class FrmUsuario : Form
    {


        private Usuario usuarioActual;

        public FrmUsuario(Usuario oUsuario = null)
        {
            InitializeComponent();
            usuarioActual = oUsuario;
        }



        private void btnGuardar_Click(object sender, EventArgs e)
        {
            string mensaje = string.Empty; // Variable para almacenar mensajes de error
            Usuario objUsuario = new Usuario() // Crea una nueva instancia de Usuario
            {
                IdUsuario = Convert.ToInt32(txtid.Text), // Obtiene el ID del usuario
                Documento = txtDocumento.Texts, // Obtiene el documento
                NombreCompleto = txtNombre.Texts, // Obtiene el nombre completo
                Correo = txtCorreo.Texts, // Obtiene el correo electrónico
                Clave = txtClave.Texts, // Obtiene la contraseña
                oRol = new Rol() { IdRol = Convert.ToInt32(((OpcionesCombo)comboRol.SelectedItem).Valor) }, // Establece el rol
                Estado = Convert.ToInt32(((OpcionesCombo)comboEstado.SelectedItem).Valor) == 1 ? true : false // Establece el estado
            };

            if (objUsuario.IdUsuario == 0) // Verifica si es un nuevo usuario
            {
                int idUsuarioGenerado = new CN_Usuario().Registrar(objUsuario, out mensaje); // Registra el nuevo usuario
                if (idUsuarioGenerado != 0) // Verifica si se generó un ID
                {
                    // Agrega el nuevo usuario al DataGridView
                    dgvData.Rows.Add(new object[] {
                    "",
                    idUsuarioGenerado,
                    txtDocumento.Texts,
                    txtNombre.Texts,
                    txtCorreo.Texts,
                    txtClave.Texts,
                    ((OpcionesCombo)comboRol.SelectedItem).Valor.ToString(),
                    ((OpcionesCombo)comboRol.SelectedItem).Texto.ToString(),
                    ((OpcionesCombo)comboEstado.SelectedItem).Valor.ToString(),
                    ((OpcionesCombo)comboEstado.SelectedItem).Texto.ToString()
                });
                    LimpiarCampos(); // Limpia los campos del formulario
                }
                else
                {
                    MessageBox.Show(mensaje); // Muestra un mensaje de error
                }
            }
            else // Si es un usuario existente
            {
                bool resultado = new CN_Usuario().Editar(objUsuario, out mensaje); // Edita el usuario
                if (resultado == true) // Verifica si la edición fue exitosa
                {
                    // Actualiza la fila correspondiente en el DataGridView
                    DataGridViewRow row = dgvData.Rows[Convert.ToInt32(txtindice.Text)];
                    row.Cells["IdUsuario"].Value = txtid.Text;
                    row.Cells["Documento"].Value = txtDocumento.Texts;
                    row.Cells["NombreCompleto"].Value = txtNombre.Texts;
                    row.Cells["Correo"].Value = txtCorreo.Texts;
                    row.Cells["Clave"].Value = txtClave.Texts;
                    row.Cells["IdRol"].Value = ((OpcionesCombo)comboRol.SelectedItem).Valor.ToString();
                    row.Cells["Rol"].Value = ((OpcionesCombo)comboRol.SelectedItem).Texto.ToString();
                    row.Cells["EstadoValor"].Value = ((OpcionesCombo)comboEstado.SelectedItem).Valor.ToString();
                    row.Cells["Estado"].Value = ((OpcionesCombo)comboEstado.SelectedItem).Texto.ToString();
                    LimpiarCampos(); // Limpia los campos del formulario
                }
                else
                {
                    MessageBox.Show(mensaje); // Muestra un mensaje de error
                }
            }
        }

        private void FrmUsuario_Load(object sender, EventArgs e)
        {
            // Inicializa los elementos del comboEstado
            comboEstado.Items.Add(new OpcionesCombo() { Valor = 1, Texto = "Activo" });
            comboEstado.Items.Add(new OpcionesCombo() { Valor = 0, Texto = "No Activo" });

            comboEstado.DisplayMember = "Texto"; // Establece el texto visible
            comboEstado.ValueMember = "Valor"; // Establece el valor asociado
            comboEstado.SelectedIndex = 0; // Selecciona el primer ítem

            // Carga los roles disponibles
            List<Rol> listaRol = new CN_Rol().Listar();
            foreach (Rol item in listaRol)
            {
                comboRol.Items.Add(new OpcionesCombo() { Valor = item.IdRol, Texto = item.Descripcion });
            }
            comboRol.DisplayMember = "Texto"; // Establece el texto visible
            comboRol.ValueMember = "Valor"; // Establece el valor asociado
            comboRol.SelectedIndex = 0; // Selecciona el primer ítem

            // Carga las opciones de búsqueda
            foreach (DataGridViewColumn columna in dgvData.Columns)
            {
                if (columna.Visible == true && columna.Name != "btnseleccionar")
                {
                    comboBusqueda.Items.Add(new OpcionesCombo() { Valor = columna.Name, Texto = columna.HeaderText });
                }
            }
            comboBusqueda.DisplayMember = "Texto"; // Establece el texto visible
            comboBusqueda.ValueMember = "Valor"; // Establece el valor asociado
            comboBusqueda.SelectedIndex = 0; // Selecciona el primer ítem

            // Muestra los usuarios en el DataGridView
            List<Usuario> listaUsuarios = new CN_Usuario().Listar();
            foreach (Usuario item in listaUsuarios)
            {
                dgvData.Rows.Add(new object[] {
                "",
                item.IdUsuario,
                item.Documento,
                item.NombreCompleto,
                item.Correo,
                item.Clave,
                item.oRol.IdRol,
                item.oRol.Descripcion,
                item.Estado == true ? 1 : 0,
                item.Estado == true ? "Activo": "No Activo"
            });
            }
            comboRol.DisplayMember = "Texto"; // Establece el texto visible
            comboRol.ValueMember = "Valor"; // Establece el valor asociado
            comboRol.SelectedIndex = 0; // Selecciona el primer ítem
        }


        private void LimpiarCampos()
        {
            txtindice.Text = "-1";
            txtid.Text = "0";
            txtDocumento.Texts = "";
            txtNombre.Texts = "";
            txtCorreo.Texts = "";
            txtClave.Texts = "";

            comboRol.SelectedIndex = 0;
            comboEstado.SelectedIndex = 0;
            txtDocumento.Select();
        }

        // Método que maneja el evento de clic en las celdas del DataGridView
        private void dgvData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvData.Columns[e.ColumnIndex].Name == "btnseleccionar") // Verifica si se hizo clic en la columna "btnseleccionar"
            {
                int indice = e.RowIndex; // Obtiene el índice de la fila

                if (indice >= 0) // Verifica que el índice sea válido
                {
                    // Rellena los campos del formulario con los datos del usuario seleccionado
                    txtindice.Text = indice.ToString();
                    txtid.Text = dgvData.Rows[indice].Cells["IdUsuario"].Value.ToString();
                    txtDocumento.Texts = dgvData.Rows[indice].Cells["Documento"].Value.ToString();
                    txtNombre.Texts = dgvData.Rows[indice].Cells["NombreCompleto"].Value.ToString();
                    txtCorreo.Texts = dgvData.Rows[indice].Cells["Correo"].Value.ToString();
                    txtClave.Texts = dgvData.Rows[indice].Cells["Clave"].Value.ToString();

                    // Establece el rol seleccionado en el comboRol
                    foreach (OpcionesCombo oc in comboRol.Items)
                    {
                        if (Convert.ToInt32(oc.Valor) == Convert.ToInt32(dgvData.Rows[indice].Cells["IdRol"].Value))
                        {
                            int indiceCombo = comboRol.Items.IndexOf(oc); // Obtiene el índice del rol
                            comboRol.SelectedIndex = indiceCombo; // Establece el rol seleccionado
                            break;
                        }
                    }

                    // Establece el estado seleccionado en el comboEstado
                    foreach (OpcionesCombo oc in comboEstado.Items)
                    {
                        if (Convert.ToInt32(oc.Valor) == Convert.ToInt32(dgvData.Rows[indice].Cells["EstadoValor"].Value))
                        {
                            int indiceCombo = comboEstado.Items.IndexOf(oc); // Obtiene el índice del estado
                            comboEstado.SelectedIndex = indiceCombo; // Establece el estado seleccionado
                            break;
                        }
                    }
                }
            }
        }


        private void dgvData_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 0) // Verifica que no se esté pintando el encabezado
            {
                return;
            }
            if (e.ColumnIndex == 0) // Verifica si es la primera columna
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All); // Dibuja el contenido de la celda
                var w = Properties.Resources.comprobado.Width - 15; // Ancho de la imagen
                var h = Properties.Resources.comprobado.Height - 15; // Altura de la imagen

                var x = e.CellBounds.Left + (e.CellBounds.Width - w) / 2; // Posición X
                var y = e.CellBounds.Top + (e.CellBounds.Height - h) / 2; // Posición Y

                e.Graphics.DrawImage(Properties.Resources.comprobado, new Rectangle(x, y, w, h)); // Dibuja la imagen
                e.Handled = true; // Marca la celda como manejada
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(txtid.Text) != 0) // Verifica si hay un usuario seleccionado
            {
                if (MessageBox.Show("¿Desea eliminar el usuario?", "Mensaje", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    string mensaje = string.Empty; // Variable para almacenar mensajes de error
                    Usuario objUsuario = new Usuario()
                    {
                        IdUsuario = Convert.ToInt32(txtid.Text), // Obtiene el ID del usuario
                    };

                    // Elimina el usuario
                    bool respuesta = new CN_Usuario().Eliminar(objUsuario, out mensaje);
                    if (respuesta)
                    {
                        // Actualiza el estado del usuario en el DataGridView
                        dgvData.Rows[Convert.ToInt32(txtindice.Text)].Cells["EstadoValor"].Value = 0;
                        dgvData.Rows[Convert.ToInt32(txtindice.Text)].Cells["Estado"].Value = "No Activo";
                        LimpiarCampos(); // Limpia los campos del formulario
                    }
                    else
                    {
                        MessageBox.Show(mensaje, "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); // Muestra un mensaje de error
                    }
                }
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarCampos(); // Limpia los campos del formulario
        }

        // Método que se ejecuta al hacer clic en el botón Limpiar Búsqueda
        private void btnLimpiarBusqueda_Click(object sender, EventArgs e)
        {
            txtBusqueda.Texts = ""; // Limpia el campo de búsqueda
            foreach (DataGridViewRow row in dgvData.Rows)
            {
                row.Visible = true; // Muestra todas las filas
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            string columnaFiltro = ((OpcionesCombo)comboBusqueda.SelectedItem).Valor.ToString(); // Obtiene la columna de filtro

            if (dgvData.Rows.Count > 0) // Verifica si hay filas en el DataGridView
            {
                foreach (DataGridViewRow row in dgvData.Rows)
                {
                    // Verifica si el valor de la celda contiene el texto de búsqueda
                    if (row.Cells[columnaFiltro].Value.ToString().Trim().ToUpper().Contains(txtBusqueda.Texts.Trim().ToUpper()))
                    {
                        row.Visible = true; // Muestra la fila
                    }
                    else
                    {
                        row.Visible = false; // Oculta la fila
                    }
                }
            }
        }


        private bool Validaciones()
        {
            bool validaciones = true; // Variable que indica si las validaciones son correctas

            // Verifica que el nombre no esté vacío
            if (txtNombre.Texts == "")
            {
                validaciones = false; // Marca como inválido si el nombre está vacío
            }

            // Verifica que el documento no esté vacío
            if (txtDocumento.Texts == "")
            {
                validaciones = false; // Marca como inválido si el documento está vacío
            }

            // Verifica que el correo no esté vacío y contenga el carácter '@'
            if (txtCorreo.Texts == "" || !(txtCorreo.Texts.Contains("@")))
            {
                validaciones = false; // Marca como inválido si el correo está vacío o no es válido
            }

            // Verifica que la contraseña no esté vacía
            if (txtClave.Texts == "")
            {
                validaciones = false; // Marca como inválido si la contraseña está vacía
            }

            return validaciones; // Retorna el resultado de las validaciones
        }

        // Método que se ejecuta al hacer clic en el botón Guardar/Editar
        private void BtnGuardar2_Click(object sender, EventArgs e)
        {
            if (Validaciones()) // Llama a la función de validación
            {
                string mensaje = string.Empty; // Variable para almacenar mensajes de error
                DialogResult confirmacion;

                if (((OpcionesCombo)comboRol.SelectedItem).Texto == "Gerente" && ((OpcionesCombo)comboEstado.SelectedItem).Texto == "No Activo")
                {
                    MessageBox.Show("No se puede desactivar al Gerente", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }



                // Determina si es un nuevo usuario o una edición
                if (Convert.ToInt32(txtid.Text) == 0)
                {
                    confirmacion = MessageBox.Show("¿Seguro desea agregar el usuario?", "Confirmacion", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                }
                else
                {
                    confirmacion = MessageBox.Show("¿Seguro desea editar el usuario?", "Confirmacion", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                }

                if (confirmacion == DialogResult.Yes) // Confirma la acción del usuario
                {
                    // Crea un nuevo objeto Usuario con los datos ingresados
                    Usuario objUsuario = new Usuario()
                    {
                        IdUsuario = Convert.ToInt32(txtid.Text), // Asigna el ID del usuario
                        Documento = txtDocumento.Texts, // Asigna el documento
                        NombreCompleto = txtNombre.Texts, // Asigna el nombre completo
                        Correo = txtCorreo.Texts, // Asigna el correo
                        Clave = txtClave.Texts, // Asigna la contraseña
                        oRol = new Rol() { IdRol = Convert.ToInt32(((OpcionesCombo)comboRol.SelectedItem).Valor) }, // Asigna el rol
                        Estado = Convert.ToInt32(((OpcionesCombo)comboEstado.SelectedItem).Valor) == 1 // Asigna el estado
                    };

                    // Verifica si es un nuevo usuario
                    if (objUsuario.IdUsuario == 0)
                    {
                        // Intenta registrar el nuevo usuario
                        int idUsuarioGenerado = new CN_Usuario().Registrar(objUsuario, out mensaje);
                        if (idUsuarioGenerado != 0) // Verifica si se generó un ID
                        {
                            // Agrega el nuevo usuario al DataGridView
                            dgvData.Rows.Add(new object[] {
                            "",
                            idUsuarioGenerado,
                            txtDocumento.Texts,
                            txtNombre.Texts,
                            txtCorreo.Texts,
                            txtClave.Texts,
                            ((OpcionesCombo)comboRol.SelectedItem).Valor.ToString(),
                            ((OpcionesCombo)comboRol.SelectedItem).Texto.ToString(),
                            ((OpcionesCombo)comboEstado.SelectedItem).Valor.ToString(),
                            ((OpcionesCombo)comboEstado.SelectedItem).Texto.ToString()
                        });
                            LimpiarCampos(); // Limpia los campos del formulario
                        }
                        else
                        {
                            MessageBox.Show(mensaje, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); // Muestra un mensaje de error
                        }
                    }
                    else // Si es un usuario existente
                    {
                        // Intenta editar el usuario existente
                        bool resultado = new CN_Usuario().Editar(objUsuario, out mensaje);
                        if (resultado == true) // Verifica si la edición fue exitosa
                        {
                            // Actualiza la fila correspondiente en el DataGridView
                            DataGridViewRow row = dgvData.Rows[Convert.ToInt32(txtindice.Text)];
                            row.Cells["IdUsuario"].Value = txtid.Text;
                            row.Cells["Documento"].Value = txtDocumento.Texts;
                            row.Cells["NombreCompleto"].Value = txtNombre.Texts;
                            row.Cells["Correo"].Value = txtCorreo.Texts;
                            row.Cells["Clave"].Value = txtClave.Texts;
                            row.Cells["IdRol"].Value = ((OpcionesCombo)comboRol.SelectedItem).Valor.ToString();
                            row.Cells["Rol"].Value = ((OpcionesCombo)comboRol.SelectedItem).Texto.ToString();
                            row.Cells["EstadoValor"].Value = ((OpcionesCombo)comboEstado.SelectedItem).Valor.ToString();
                            row.Cells["Estado"].Value = ((OpcionesCombo)comboEstado.SelectedItem).Texto.ToString();
                            LimpiarCampos(); // Limpia los campos del formulario
                        }
                        else
                        {
                            MessageBox.Show(mensaje, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); // Muestra un mensaje de error
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Debe Completar todos los campos", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); // Alerta si hay campos vacíos
            }
        }

        // Método que se ejecuta al hacer clic en el botón Eliminar
        private void BtnEliminar2_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(txtid.Text) != 0 && Convert.ToInt32(txtid.Text) != usuarioActual.IdUsuario) // Verifica que haya un usuario seleccionado
            {

                if (((OpcionesCombo)comboRol.SelectedItem).Texto == "Gerente")
                {
                    MessageBox.Show("No se puede eliminar el Gerente","Alerta",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                    return;
                }



                if (MessageBox.Show("¿Desea eliminar el usuario?", "Mensaje", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    string mensaje = string.Empty; // Variable para almacenar mensajes de error
                    Usuario objUsuario = new Usuario()
                    {
                        IdUsuario = Convert.ToInt32(txtid.Text), // Asigna el ID del usuario a eliminar
                    };

                    // Intenta eliminar el usuario
                    bool respuesta = new CN_Usuario().Eliminar(objUsuario, out mensaje);
                    if (respuesta)
                    {
                        // Actualiza el estado del usuario en el DataGridView
                        dgvData.Rows[Convert.ToInt32(txtindice.Text)].Cells["EstadoValor"].Value = 0;
                        dgvData.Rows[Convert.ToInt32(txtindice.Text)].Cells["Estado"].Value = "No Activo";
                        LimpiarCampos(); // Limpia los campos del formulario
                    }
                    else
                    {
                        MessageBox.Show(mensaje, "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); // Muestra un mensaje de error
                    }
                }
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

        // Método que se ejecuta al hacer clic en el botón Limpiar
        private void rjButton1_Click(object sender, EventArgs e)
        {
            LimpiarCampos(); // Limpia los campos del formulario
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

        private void txtNombre_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Verificar si el carácter es una tecla de control (como Backspace)
            bool esControl = Char.IsControl(e.KeyChar);

            // Verificar si el carácter es un dígito
            bool esDigito = Char.IsDigit(e.KeyChar);

            // Verificar la longitud actual del texto y permitir solo hasta 8 dígitos
            bool longitudPermitida = txtNombre.Texts.Length < 100;

            // Permitir el carácter solo si es una tecla de control o un caracter y la longitud permitida no se ha alcanzado
            if (esControl || (!esDigito && longitudPermitida))
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

        private void txtClave_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Verificar si el carácter es una tecla de control (como Backspace)
            bool esControl = Char.IsControl(e.KeyChar);

            // Verificar la longitud actual del texto y permitir solo hasta 8 dígitos
            bool longitudPermitida = txtClave.Texts.Length < 80;

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
    }
}
