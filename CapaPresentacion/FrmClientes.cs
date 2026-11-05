using CapaEntidad;
using CapaNegocio;
using CapaPresentacion.Utilidades;
using CustomControls.RJControls;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace CapaPresentacion
{
    public partial class FrmClientes : Form
    {
        private Usuario usuarioActual;
        private RJTextBox txtApellido;
        private Label lblApellido;

        public FrmClientes(Usuario oUsuario = null)
        {
            InitializeComponent();
            usuarioActual = oUsuario;
            ConfigurarNombreYApellidoSeparados();
        }

        private void ConfigurarNombreYApellidoSeparados()
        {
            // Ampliar groupBox1 y dgvData para mostrar cómodamente Nombre y Apellido separados
            groupBox1.Location = new Point(55, 38);
            groupBox1.Size = new Size(810, 155);

            rjButton1.Location = new Point(107, 210);
            BtnEliminar2.Location = new Point(367, 210);
            BtnLimpiar2.Location = new Point(627, 210);

            label6.Location = new Point(55, 268);
            label6.Size = new Size(810, 58);
            label1.Location = new Point(75, 289);
            comboBusqueda.Location = new Point(160, 286);
            comboBusqueda.Size = new Size(165, 25);
            txtBusqueda.Location = new Point(345, 281);
            txtBusqueda.Size = new Size(260, 36);
            rjButton2.Location = new Point(620, 281);
            rjButton3.Location = new Point(680, 281);

            dgvData.Location = new Point(55, 338);
            dgvData.Size = new Size(810, 255);
            dgvData.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // Fila 1 de groupBox1: Documento | Nombre | Apellido | Estado
            label5.Text = "DNI / Documento:";
            label5.Location = new Point(16, 22);
            txtDocumento.Location = new Point(14, 43);
            txtDocumento.Size = new Size(165, 36);
            txtDocumento.PlaceholderText = "DNI (8 dígitos)";

            label2.Text = "Nombre:";
            label2.Location = new Point(195, 22);
            txtNombre.Location = new Point(193, 43);
            txtNombre.Size = new Size(195, 36);
            txtNombre.PlaceholderText = "Nombre";

            lblApellido = new Label()
            {
                AutoSize = true,
                Text = "Apellido:",
                Location = new Point(406, 22),
                Font = new Font("Segoe UI", 9.75F, FontStyle.Bold)
            };
            txtApellido = new RJTextBox()
            {
                Name = "txtApellido",
                Location = new Point(404, 43),
                Size = new Size(195, 36),
                PlaceholderText = "Apellido",
                BorderRadius = 10,
                BorderSize = 2,
                Padding = new Padding(10, 7, 10, 7),
                Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                Texts = ""
            };
            txtApellido.KeyPress += txtNombre_KeyPress;

            label7.Text = "Estado:";
            label7.Location = new Point(618, 22);
            comboEstado.Location = new Point(618, 45);
            comboEstado.Size = new Size(170, 29);

            // Fila 2 de groupBox1: Dirección | Teléfono | Correo Electrónico
            label8.Text = "Dirección:";
            label8.Location = new Point(16, 88);
            txtDireccion.Location = new Point(14, 108);
            txtDireccion.Size = new Size(250, 36);
            txtDireccion.PlaceholderText = "Calle y Número";

            label4.Text = "Teléfono:";
            label4.Location = new Point(282, 88);
            txtTelefono.Location = new Point(280, 108);
            txtTelefono.Size = new Size(210, 36);
            txtTelefono.PlaceholderText = "Teléfono";

            label3.Text = "Correo Electrónico:";
            label3.Location = new Point(508, 88);
            txtCorreo.Location = new Point(506, 108);
            txtCorreo.Size = new Size(282, 36);
            txtCorreo.PlaceholderText = "Correo Electrónico";

            groupBox1.Controls.Add(lblApellido);
            groupBox1.Controls.Add(txtApellido);

            // Agregar columnas separadas Nombre y Apellido en el DataGridView (dgvData)
            if (!dgvData.Columns.Contains("Nombre"))
            {
                int idxNombreCompleto = dgvData.Columns["NombreCompleto"].Index;
                NombreCompleto.Visible = false;

                var colNombre = new DataGridViewTextBoxColumn()
                {
                    Name = "Nombre",
                    HeaderText = "Nombre",
                    ReadOnly = true,
                    FillWeight = 18
                };
                var colApellido = new DataGridViewTextBoxColumn()
                {
                    Name = "Apellido",
                    HeaderText = "Apellido",
                    ReadOnly = true,
                    FillWeight = 18
                };

                dgvData.Columns.Insert(idxNombreCompleto, colNombre);
                dgvData.Columns.Insert(idxNombreCompleto + 1, colApellido);

                btnseleccionar.Width = 36;
                btnseleccionar.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                Documento.HeaderText = "DNI";
                Documento.FillWeight = 13;
                Direccion.HeaderText = "Dirección";
                Direccion.FillWeight = 22;
                Correo.FillWeight = 22;
                Telefono.HeaderText = "Teléfono";
                Telefono.FillWeight = 14;
                Estado.FillWeight = 11;
            }
        }

        private void FrmClientes_Load(object sender, EventArgs e)
        {
            TemaCapybara.AplicarTema(this);

            comboEstado.Items.Clear();
            comboEstado.Items.Add(new OpcionesCombo() { Valor = 1, Texto = "Activo" });
            comboEstado.Items.Add(new OpcionesCombo() { Valor = 0, Texto = "No Activo" });
            comboEstado.DisplayMember = "Texto";
            comboEstado.ValueMember = "Valor";
            comboEstado.SelectedIndex = 0;

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

            dgvData.Rows.Clear();
            List<Cliente> listaClientes = new CN_Cliente().Listar();
            foreach (Cliente item in listaClientes)
            {
                dgvData.Rows.Add(new object[] {
                    "",
                    item.IdCliente,
                    item.Documento,
                    item.Nombre,
                    item.Apellido,
                    item.NombreCompleto,
                    item.Direccion,
                    item.Correo,
                    item.Telefono,
                    item.Estado ? 1 : 0,
                    item.Estado ? "Activo" : "No Activo"
                });
            }

            // Si es vendedor (rol == 2), no puede eliminar clientes
            if (usuarioActual != null && usuarioActual.oRol != null &&
                (usuarioActual.oRol.IdRol == 2 || string.Equals(usuarioActual.oRol.Descripcion, "Vendedor", StringComparison.OrdinalIgnoreCase)))
            {
                BtnEliminar2.Enabled = false;
                BtnEliminar2.Visible = false;
            }
        }

        private bool Validaciones()
        {
            if (string.IsNullOrWhiteSpace(txtDocumento.Texts)) return false;
            if (string.IsNullOrWhiteSpace(txtNombre.Texts)) return false;
            if (string.IsNullOrWhiteSpace(txtApellido.Texts)) return false;
            if (string.IsNullOrWhiteSpace(txtDireccion.Texts)) return false;
            if (string.IsNullOrWhiteSpace(txtCorreo.Texts) || !txtCorreo.Texts.Contains("@")) return false;
            if (string.IsNullOrWhiteSpace(txtTelefono.Texts)) return false;
            return true;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (Validaciones())
            {
                DialogResult confirmacion;
                string mensaje = string.Empty;

                if (Convert.ToInt32(txtid.Text) == 0)
                {
                    confirmacion = MessageBox.Show("¿Seguro desea agregar el Cliente?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                }
                else
                {
                    confirmacion = MessageBox.Show("¿Seguro desea editar el Cliente?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                }

                if (confirmacion == DialogResult.Yes)
                {
                    string nombre = txtNombre.Texts.Trim();
                    string apellido = txtApellido.Texts.Trim();
                    string nombreCompleto = $"{nombre} {apellido}".Trim();

                    Cliente objCliente = new Cliente()
                    {
                        IdCliente = Convert.ToInt32(txtid.Text),
                        Documento = txtDocumento.Texts.Trim(),
                        Nombre = nombre,
                        Apellido = apellido,
                        NombreCompleto = nombreCompleto,
                        Direccion = txtDireccion.Texts.Trim(),
                        Correo = txtCorreo.Texts.Trim(),
                        Telefono = txtTelefono.Texts.Trim(),
                        Estado = Convert.ToInt32(((OpcionesCombo)comboEstado.SelectedItem).Valor) == 1
                    };

                    if (objCliente.IdCliente == 0)
                    {
                        int idClienteGenerado = new CN_Cliente().Registrar(objCliente, out mensaje);
                        if (idClienteGenerado != 0)
                        {
                            dgvData.Rows.Add(new object[] {
                                "",
                                idClienteGenerado,
                                objCliente.Documento,
                                objCliente.Nombre,
                                objCliente.Apellido,
                                objCliente.NombreCompleto,
                                objCliente.Direccion,
                                objCliente.Correo,
                                objCliente.Telefono,
                                ((OpcionesCombo)comboEstado.SelectedItem).Valor.ToString(),
                                ((OpcionesCombo)comboEstado.SelectedItem).Texto.ToString()
                            });
                            LimpiarCampos();
                        }
                        else
                        {
                            MessageBox.Show(mensaje, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        bool resultado = new CN_Cliente().Editar(objCliente, out mensaje);
                        if (resultado)
                        {
                            DataGridViewRow row = dgvData.Rows[Convert.ToInt32(txtindice.Text)];
                            row.Cells["IdCliente"].Value = objCliente.IdCliente;
                            row.Cells["Documento"].Value = objCliente.Documento;
                            row.Cells["Nombre"].Value = objCliente.Nombre;
                            row.Cells["Apellido"].Value = objCliente.Apellido;
                            row.Cells["NombreCompleto"].Value = objCliente.NombreCompleto;
                            row.Cells["Direccion"].Value = objCliente.Direccion;
                            row.Cells["Correo"].Value = objCliente.Correo;
                            row.Cells["Telefono"].Value = objCliente.Telefono;
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
                MessageBox.Show("Debe completar todos los campos (DNI, Nombre, Apellido, Dirección, Teléfono y Correo válido).", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (usuarioActual != null && usuarioActual.oRol != null &&
                (usuarioActual.oRol.IdRol == 2 || string.Equals(usuarioActual.oRol.Descripcion, "Vendedor", StringComparison.OrdinalIgnoreCase)))
            {
                MessageBox.Show("El vendedor no tiene permisos para eliminar clientes.", "Permiso denegado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (Convert.ToInt32(txtid.Text) != 0)
            {
                if (MessageBox.Show("¿Desea eliminar el Cliente?", "Mensaje", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    string mensaje = string.Empty;
                    Cliente objCliente = new Cliente()
                    {
                        IdCliente = Convert.ToInt32(txtid.Text),
                    };

                    bool respuesta = new CN_Cliente().Eliminar(objCliente, out mensaje);
                    if (respuesta)
                    {
                        dgvData.Rows[Convert.ToInt32(txtindice.Text)].Cells["EstadoValor"].Value = 0;
                        dgvData.Rows[Convert.ToInt32(txtindice.Text)].Cells["Estado"].Value = "No Activo";
                        LimpiarCampos();
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
            if (comboBusqueda.SelectedItem == null) return;
            string columnaFiltro = ((OpcionesCombo)comboBusqueda.SelectedItem).Valor.ToString();

            if (dgvData.Rows.Count > 0)
            {
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

        private void dgvData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            if (dgvData.Columns[e.ColumnIndex].Name == "btnseleccionar")
            {
                int indice = e.RowIndex;
                txtindice.Text = indice.ToString();
                txtid.Text = dgvData.Rows[indice].Cells["IdCliente"].Value.ToString();
                txtDocumento.Texts = dgvData.Rows[indice].Cells["Documento"].Value?.ToString() ?? "";
                txtNombre.Texts = dgvData.Rows[indice].Cells["Nombre"].Value?.ToString() ?? "";
                txtApellido.Texts = dgvData.Rows[indice].Cells["Apellido"].Value?.ToString() ?? "";
                txtDireccion.Texts = dgvData.Rows[indice].Cells["Direccion"].Value?.ToString() ?? "";
                txtCorreo.Texts = dgvData.Rows[indice].Cells["Correo"].Value?.ToString() ?? "";
                txtTelefono.Texts = dgvData.Rows[indice].Cells["Telefono"].Value?.ToString() ?? "";

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

        private void dgvData_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            TemaCapybara.ModernDgv_CellPainting(sender, e);
        }

        private void LimpiarCampos()
        {
            txtindice.Text = "-1";
            txtid.Text = "0";
            txtDocumento.Texts = "";
            txtNombre.Texts = "";
            txtApellido.Texts = "";
            txtCorreo.Texts = "";
            txtTelefono.Texts = "";
            txtDireccion.Texts = "";
            if (comboEstado.Items.Count > 0) comboEstado.SelectedIndex = 0;
            txtDocumento.Select();
        }

        private void txtDocumento_KeyPress(object sender, KeyPressEventArgs e)
        {
            bool esControl = Char.IsControl(e.KeyChar);
            bool esDigito = Char.IsDigit(e.KeyChar);
            bool longitudPermitida = txtDocumento.Texts.Length < 8;
            e.Handled = !(esControl || (esDigito && longitudPermitida));
        }

        private void txtNombre_KeyPress(object sender, KeyPressEventArgs e)
        {
            bool esControl = Char.IsControl(e.KeyChar);
            bool esDigito = Char.IsDigit(e.KeyChar);
            e.Handled = !(esControl || !esDigito);
        }

        private void txtTelefono_KeyPress(object sender, KeyPressEventArgs e)
        {
            bool esControl = Char.IsControl(e.KeyChar);
            bool esDigito = Char.IsDigit(e.KeyChar);
            bool longitudPermitida = txtTelefono.Texts.Length < 12;
            e.Handled = !(esControl || (esDigito && longitudPermitida));
        }

        private void txtCorreo_KeyPress(object sender, KeyPressEventArgs e)
        {
            bool esControl = Char.IsControl(e.KeyChar);
            bool longitudPermitida = txtCorreo.Texts.Length < 100;
            e.Handled = !(esControl || longitudPermitida);
        }

        private void txtDireccion_KeyPress(object sender, KeyPressEventArgs e)
        {
            bool esControl = Char.IsControl(e.KeyChar);
            bool longitudPermitida = txtDireccion.Texts.Length < 100;
            e.Handled = !(esControl || longitudPermitida);
        }

        private void txtBusqueda_KeyPress(object sender, KeyPressEventArgs e)
        {
            bool esControl = Char.IsControl(e.KeyChar);
            bool longitudPermitida = txtBusqueda.Texts.Length < 80;
            e.Handled = !(esControl || longitudPermitida);
        }

        private void dgvData_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (this.dgvData.Columns[e.ColumnIndex].Name == "Estado" && e.Value != null)
            {
                if (e.Value.ToString() == "No Activo")
                {
                    e.CellStyle.BackColor = Color.Red;
                    e.CellStyle.ForeColor = Color.Black;
                }
            }
        }
    }
}
