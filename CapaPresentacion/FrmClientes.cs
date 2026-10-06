using CapaEntidad;
using CapaNegocio;
using CapaPresentacion.Utilidades;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace CapaPresentacion
{
    public partial class FrmClientes : Form
    {
        public FrmClientes()
        {
            InitializeComponent();
        }
        private void FrmClientes_Load(object sender, EventArgs e)
        {
            // Inicializa el comboEstado con opciones para el estado del cliente (Activo, No Activo).
            comboEstado.Items.Add(new OpcionesCombo() { Valor = 1, Texto = "Activo" });
            comboEstado.Items.Add(new OpcionesCombo() { Valor = 0, Texto = "No Activo" });

            // Configura las propiedades de visualización del comboEstado.
            comboEstado.DisplayMember = "Texto";
            comboEstado.ValueMember = "Valor";
            comboEstado.SelectedIndex = 0;

            // Configura el comboBusqueda para filtrar los resultados en el DataGridView.
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

            // Muestra los clientes en el DataGridView.
            List<Cliente> listaClientes = new CN_Cliente().Listar();

            foreach (Cliente item in listaClientes)
            {
                dgvData.Rows.Add(new object[] {
            "", // Columna para el icono de selección
            item.IdCliente,
            item.Documento,
            item.NombreCompleto,
            item.Direccion,
            item.Correo,
            item.Telefono,
            item.Estado == true ? 1 : 0, // Estado como valor
            item.Estado == true ? "Activo" : "No Activo" // Estado como texto
        });
            }
        }


        private bool Validaciones()
        {
            bool confirmacion = true;

            // Verifica que el campo de Documento no esté vacío.
            if (txtDocumento.Texts == "")
            {
                confirmacion = false;
            }
            // Verifica que el campo de Dirección no esté vacío.
            if (txtDireccion.Texts == "")
            {
                confirmacion = false;
            }
            // Verifica que el correo tenga formato válido.
            if (txtCorreo.Texts == "" || !(txtCorreo.Texts.Contains("@")))
            {
                confirmacion = false;
            }
            // Verifica que el campo de Nombre no esté vacío.
            if (txtNombre.Texts == "")
            {
                confirmacion = false;
            }
            // Verifica que el campo de Teléfono no esté vacío.
            if (txtTelefono.Texts == "")
            {
                confirmacion = false;
            }

            // Retorna el resultado de las validaciones.
            return confirmacion;
        }


        private void btnGuardar_Click(object sender, EventArgs e)
        {
            // Valida los campos antes de proceder.
            if (Validaciones())
            {
                DialogResult confirmacion;

                string mensaje = string.Empty;

                // Confirma si se va a agregar o editar un cliente.
                if (Convert.ToInt32(txtid.Text) == 0)
                {
                    confirmacion = MessageBox.Show("¿Seguro desea agregar el Cliente?", "Confirmacion", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                }
                else
                {
                    confirmacion = MessageBox.Show("¿Seguro desea editar el Cliente?", "Confirmacion", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                }

                // Si la respuesta es afirmativa, se crea o edita el cliente.
                if (confirmacion == DialogResult.Yes)
                {
                    Cliente objCliente = new Cliente()
                    {
                        IdCliente = Convert.ToInt32(txtid.Text),
                        Documento = txtDocumento.Texts,
                        NombreCompleto = txtNombre.Texts,
                        Direccion = txtDireccion.Texts,
                        Correo = txtCorreo.Texts,
                        Telefono = txtTelefono.Texts,
                        Estado = Convert.ToInt32(((OpcionesCombo)comboEstado.SelectedItem).Valor) == 1 ? true : false
                    };

                    // Si es un nuevo cliente, se registra en la base de datos.
                    if (objCliente.IdCliente == 0)
                    {
                        int idClienteGenerado = new CN_Cliente().Registrar(objCliente, out mensaje);
                        if (idClienteGenerado != 0)
                        {
                            // Agrega el nuevo cliente al DataGridView.
                            dgvData.Rows.Add(new object[] {
                        "", // Columna para el icono de selección
                        idClienteGenerado,
                        txtDocumento.Texts,
                        txtNombre.Texts,
                        txtDireccion.Texts,
                        txtCorreo.Texts,
                        txtTelefono.Texts,
                        ((OpcionesCombo)comboEstado.SelectedItem).Valor.ToString(),
                        ((OpcionesCombo)comboEstado.SelectedItem).Texto.ToString()
                    });
                            LimpiarCampos(); // Limpia los campos del formulario.
                        }
                        else
                        {
                            MessageBox.Show(mensaje, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        // Si se está editando, actualiza la información del cliente.
                        bool resultado = new CN_Cliente().Editar(objCliente, out mensaje);
                        if (resultado == true)
                        {
                            DataGridViewRow row = dgvData.Rows[Convert.ToInt32(txtindice.Text)];
                            row.Cells["IdCliente"].Value = txtid.Text;
                            row.Cells["Documento"].Value = txtDocumento.Texts;
                            row.Cells["NombreCompleto"].Value = txtNombre.Texts;
                            row.Cells["Direccion"].Value = txtDireccion.Texts;
                            row.Cells["Correo"].Value = txtCorreo.Texts;
                            row.Cells["Telefono"].Value = txtTelefono.Texts;
                            row.Cells["EstadoValor"].Value = ((OpcionesCombo)comboEstado.SelectedItem).Valor.ToString();
                            row.Cells["Estado"].Value = ((OpcionesCombo)comboEstado.SelectedItem).Texto.ToString();
                            LimpiarCampos(); // Limpia los campos del formulario.
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
                // Muestra un mensaje si no se completan todos los campos obligatorios.
                MessageBox.Show("Debe Completar todos los campos", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }


        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            // Limpia todos los campos del formulario.
            LimpiarCampos();
        }


        private void btnEliminar_Click(object sender, EventArgs e)
        {
            // Verifica si se ha seleccionado un cliente para eliminar.
            if (Convert.ToInt32(txtid.Text) != 0)
            {
                // Pregunta al usuario si desea eliminar el cliente.
                if (MessageBox.Show("¿Desea eliminar el Cliente?", "Mensaje", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    string mensaje = string.Empty;
                    Cliente objCliente = new Cliente()
                    {
                        IdCliente = Convert.ToInt32(txtid.Text),
                    };

                    // Llama al método de eliminación en la base de datos.
                    bool respuesta = new CN_Cliente().Eliminar(objCliente, out mensaje);
                    if (respuesta)
                    {
                        // Cambia el estado del cliente en el DataGridView a "No Activo".
                        dgvData.Rows[Convert.ToInt32(txtindice.Text)].Cells["EstadoValor"].Value = 0;
                        dgvData.Rows[Convert.ToInt32(txtindice.Text)].Cells["Estado"].Value = "No Activo";
                        LimpiarCampos(); // Limpia los campos del formulario.
                    }
                    else
                    {
                        MessageBox.Show(mensaje, "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
            }
        }


        private void btnBuscar_Click(object sender, EventArgs e)
        {
            // Obtiene el nombre de la columna seleccionada para la búsqueda.
            string columnaFiltro = ((OpcionesCombo)comboBusqueda.SelectedItem).Valor.ToString();

            // Verifica si hay filas en el DataGridView.
            if (dgvData.Rows.Count > 0)
            {
                // Filtra las filas del DataGridView según el texto de búsqueda.
                foreach (DataGridViewRow row in dgvData.Rows)
                {
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
            // Verifica si se ha hecho clic en la columna de selección.
            if (dgvData.Columns[e.ColumnIndex].Name == "btnseleccionar")
            {
                int indice = e.RowIndex;

                if (indice >= 0)
                {
                    // Carga los datos del cliente seleccionado en los campos del formulario.
                    txtindice.Text = indice.ToString();
                    txtid.Text = dgvData.Rows[indice].Cells["IdCliente"].Value.ToString();
                    txtDocumento.Texts = dgvData.Rows[indice].Cells["Documento"].Value.ToString();
                    txtNombre.Texts = dgvData.Rows[indice].Cells["NombreCompleto"].Value.ToString();
                    txtDireccion.Texts = dgvData.Rows[indice].Cells["Direccion"].Value.ToString();
                    txtCorreo.Texts = dgvData.Rows[indice].Cells["Correo"].Value.ToString();
                    txtTelefono.Texts = dgvData.Rows[indice].Cells["Telefono"].Value.ToString();

                    // Selecciona el estado correspondiente en el comboEstado.
                    foreach (OpcionesCombo oc in comboEstado.Items)
                    {
                        if (Convert.ToInt32(oc.Valor) == Convert.ToInt32(dgvData.Rows[indice].Cells["EstadoValor"].Value))
                        {
                            int indiceCombo = comboEstado.Items.IndexOf(oc);
                            comboEstado.SelectedIndex = indiceCombo; // Selecciona el estado en el combo.
                            break;
                        }
                    }
                }
            }
        }


        private void dgvData_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            // Evita la ejecución para el encabezado del DataGridView.
            if (e.RowIndex < 0)
            {
                return;
            }
            // Verifica si la columna es la de selección.
            if (e.ColumnIndex == 0)
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All); // Dibuja la celda.
                var w = Properties.Resources.comprobado.Width - 15; // Ancho del icono.
                var h = Properties.Resources.comprobado.Height - 15; // Alto del icono.

                // Calcula la posición para centrar el icono.
                var x = e.CellBounds.Left + (e.CellBounds.Width - w) / 2;
                var y = e.CellBounds.Top + (e.CellBounds.Height - h) / 2;

                e.Graphics.DrawImage(Properties.Resources.comprobado, new Rectangle(x, y, w, h)); // Dibuja el icono.
                e.Handled = true; // Marca la celda como manejada.
            }
        }

        private void LimpiarCampos()
        {
            txtindice.Text = "-1";
            txtid.Text = "0";
            txtDocumento.Texts = "";
            txtNombre.Texts = "";
            txtCorreo.Texts = "";
            txtTelefono.Texts = "";
            txtDireccion.Texts = "";
            comboEstado.SelectedIndex = 0;
            txtDocumento.Select();
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

