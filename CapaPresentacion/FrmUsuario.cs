using CapaEntidad;
using CapaNegocio;
using CapaPresentacion.Utilidades;
using CustomControls.RJControls;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace CapaPresentacion
{
    public partial class FrmUsuario : Form
    {
        private Usuario usuarioActual;
        private RJTextBox txtApellido;
        private RJTextBox txtDireccion;
        private RJTextBox txtTelefono;
        private Label lblApellido;
        private Label lblDireccion;
        private Label lblTelefono;

        public FrmUsuario(Usuario oUsuario = null)
        {
            InitializeComponent();
            usuarioActual = oUsuario;
            ConfigurarControlesYColumnasExtras();
        }

        private void ConfigurarControlesYColumnasExtras()
        {
            // Ampliar contenedor de datos y tabla para mostrar cómodamente todas las columnas nuevas
            groupBox1.Location = new Point(55, 32);
            groupBox1.Size = new Size(810, 165);

            BtnGuardar2.Location = new Point(107, 210);
            BtnEliminar2.Location = new Point(367, 210);
            BtnLimpiar2.Location = new Point(627, 210);

            label10.Location = new Point(55, 268);
            label10.Size = new Size(810, 58);
            label11.Location = new Point(75, 289);
            comboBusqueda.Location = new Point(160, 286);
            txtBusqueda.Location = new Point(345, 281);
            txtBusqueda.Size = new Size(260, 36);
            rjButton1.Location = new Point(620, 281);
            rjButton3.Location = new Point(680, 281);

            dgvData.Location = new Point(55, 338);
            dgvData.Size = new Size(810, 255);

            // Reorganizar controles existentes en Fila 1 y Fila 2 dentro de groupBox1
            // Fila 1: Documento | Nombre | Apellido | Teléfono
            label2.Text = "Documento:";
            label2.Location = new Point(16, 24);
            txtDocumento.Location = new Point(14, 45);
            txtDocumento.Size = new Size(165, 36);
            txtDocumento.PlaceholderText = "Documento";

            label3.Text = "Nombre:";
            label3.Location = new Point(195, 24);
            txtNombre.Location = new Point(193, 45);
            txtNombre.Size = new Size(185, 36);
            txtNombre.PlaceholderText = "Nombre";

            lblApellido = new Label()
            {
                AutoSize = true,
                Text = "Apellido:",
                Location = new Point(394, 24),
                Font = new Font("Segoe UI", 9.75F, FontStyle.Bold)
            };
            txtApellido = new RJTextBox()
            {
                Name = "txtApellido",
                Location = new Point(392, 45),
                Size = new Size(185, 36),
                PlaceholderText = "Apellido",
                BorderRadius = 10,
                BorderSize = 2,
                Padding = new Padding(10, 7, 10, 7),
                Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                Texts = ""
            };
            txtApellido.KeyPress += txtNombre_KeyPress;

            lblTelefono = new Label()
            {
                AutoSize = true,
                Text = "Teléfono:",
                Location = new Point(593, 24),
                Font = new Font("Segoe UI", 9.75F, FontStyle.Bold)
            };
            txtTelefono = new RJTextBox()
            {
                Name = "txtTelefono",
                Location = new Point(591, 45),
                Size = new Size(200, 36),
                PlaceholderText = "Teléfono",
                BorderRadius = 10,
                BorderSize = 2,
                Padding = new Padding(10, 7, 10, 7),
                Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                Texts = ""
            };

            // Fila 2: Dirección | Correo | Contraseña | Rol | Estado
            lblDireccion = new Label()
            {
                AutoSize = true,
                Text = "Dirección:",
                Location = new Point(16, 92),
                Font = new Font("Segoe UI", 9.75F, FontStyle.Bold)
            };
            txtDireccion = new RJTextBox()
            {
                Name = "txtDireccion",
                Location = new Point(14, 113),
                Size = new Size(165, 36),
                PlaceholderText = "Dirección",
                BorderRadius = 10,
                BorderSize = 2,
                Padding = new Padding(10, 7, 10, 7),
                Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                Texts = ""
            };

            label1.Text = "Correo Electrónico:";
            label1.Location = new Point(195, 92);
            txtCorreo.Location = new Point(193, 113);
            txtCorreo.Size = new Size(185, 36);
            txtCorreo.PlaceholderText = "Correo";

            label4.Text = "Contraseña:";
            label4.Location = new Point(394, 92);
            txtClave.Location = new Point(392, 113);
            txtClave.Size = new Size(140, 36);
            txtClave.PlaceholderText = "Contraseña";

            label7.Text = "Rol:";
            label7.Location = new Point(546, 92);
            comboRol.Location = new Point(546, 115);
            comboRol.Size = new Size(125, 29);

            label8.Text = "Estado:";
            label8.Location = new Point(682, 92);
            comboEstado.Location = new Point(682, 115);
            comboEstado.Size = new Size(110, 29);

            groupBox1.Controls.Add(lblApellido);
            groupBox1.Controls.Add(txtApellido);
            groupBox1.Controls.Add(lblTelefono);
            groupBox1.Controls.Add(txtTelefono);
            groupBox1.Controls.Add(lblDireccion);
            groupBox1.Controls.Add(txtDireccion);

            // Configurar columnas separadas en dgvData: Nombre, Apellido, Dirección, Teléfono
            if (!dgvData.Columns.Contains("Nombre"))
            {
                int idxNombreCompleto = dgvData.Columns["NombreCompleto"].Index;
                NombreCompleto.Visible = false;

                var colNombre = new DataGridViewTextBoxColumn()
                {
                    Name = "Nombre",
                    HeaderText = "Nombre",
                    ReadOnly = true,
                    Width = 105
                };
                var colApellido = new DataGridViewTextBoxColumn()
                {
                    Name = "Apellido",
                    HeaderText = "Apellido",
                    ReadOnly = true,
                    Width = 105
                };
                var colDireccion = new DataGridViewTextBoxColumn()
                {
                    Name = "Direccion",
                    HeaderText = "Dirección",
                    ReadOnly = true,
                    Width = 135
                };
                var colTelefono = new DataGridViewTextBoxColumn()
                {
                    Name = "Telefono",
                    HeaderText = "Teléfono",
                    ReadOnly = true,
                    Width = 95
                };

                dgvData.Columns.Insert(idxNombreCompleto, colNombre);
                dgvData.Columns.Insert(idxNombreCompleto + 1, colApellido);
                dgvData.Columns.Insert(idxNombreCompleto + 3, colDireccion);
                dgvData.Columns.Insert(idxNombreCompleto + 4, colTelefono);

                Documento.Width = 85;
                Correo.Width = 135;
                Rol.Width = 85;
                Estado.Width = 75;
            }
        }

        private void FrmUsuario_Load(object sender, EventArgs e)
        {
            TemaCapybara.AplicarTema(this);

            comboEstado.Items.Clear();
            comboEstado.Items.Add(new OpcionesCombo() { Valor = 1, Texto = "Activo" });
            comboEstado.Items.Add(new OpcionesCombo() { Valor = 0, Texto = "No Activo" });
            comboEstado.DisplayMember = "Texto";
            comboEstado.ValueMember = "Valor";
            comboEstado.SelectedIndex = 0;

            // Cargar roles sin duplicados
            comboRol.Items.Clear();
            List<Rol> listaRol = new CN_Rol().Listar()
                .GroupBy(r => r.Descripcion.Trim(), StringComparer.OrdinalIgnoreCase)
                .Select(g => g.First())
                .ToList();

            foreach (Rol item in listaRol)
            {
                comboRol.Items.Add(new OpcionesCombo() { Valor = item.IdRol, Texto = item.Descripcion });
            }
            comboRol.DisplayMember = "Texto";
            comboRol.ValueMember = "Valor";
            if (comboRol.Items.Count > 0)
                comboRol.SelectedIndex = 0;

            // Cargar opciones de búsqueda
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

            CargarUsuariosTabla();
        }

        private void CargarUsuariosTabla()
        {
            dgvData.Rows.Clear();
            List<Usuario> listaUsuarios = new CN_Usuario().Listar();
            foreach (Usuario item in listaUsuarios)
            {
                dgvData.Rows.Add(new object[] {
                    "",
                    item.IdUsuario,
                    item.Documento,
                    item.Nombre,
                    item.Apellido,
                    item.NombreCompleto,
                    item.Direccion,
                    item.Telefono,
                    item.Correo,
                    item.Clave,
                    item.oRol.IdRol,
                    item.oRol.Descripcion,
                    item.Estado ? 1 : 0,
                    item.Estado ? "Activo" : "No Activo"
                });
            }
        }

        private void LimpiarCampos()
        {
            txtindice.Text = "-1";
            txtid.Text = "0";
            txtDocumento.Texts = "";
            txtNombre.Texts = "";
            txtApellido.Texts = "";
            txtDireccion.Texts = "";
            txtTelefono.Texts = "";
            txtCorreo.Texts = "";
            txtClave.Texts = "";

            if (comboRol.Items.Count > 0) comboRol.SelectedIndex = 0;
            if (comboEstado.Items.Count > 0) comboEstado.SelectedIndex = 0;
            txtDocumento.Select();
        }

        private void dgvData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            if (dgvData.Columns[e.ColumnIndex].Name == "btnseleccionar")
            {
                int indice = e.RowIndex;
                txtindice.Text = indice.ToString();
                txtid.Text = dgvData.Rows[indice].Cells["IdUsuario"].Value.ToString();
                txtDocumento.Texts = dgvData.Rows[indice].Cells["Documento"].Value?.ToString() ?? "";
                txtNombre.Texts = dgvData.Rows[indice].Cells["Nombre"].Value?.ToString() ?? "";
                txtApellido.Texts = dgvData.Rows[indice].Cells["Apellido"].Value?.ToString() ?? "";
                txtDireccion.Texts = dgvData.Rows[indice].Cells["Direccion"].Value?.ToString() ?? "";
                txtTelefono.Texts = dgvData.Rows[indice].Cells["Telefono"].Value?.ToString() ?? "";
                txtCorreo.Texts = dgvData.Rows[indice].Cells["Correo"].Value?.ToString() ?? "";
                txtClave.Texts = dgvData.Rows[indice].Cells["Clave"].Value?.ToString() ?? "";

                string rolTexto = dgvData.Rows[indice].Cells["Rol"].Value?.ToString() ?? "";
                int idRolFila = Convert.ToInt32(dgvData.Rows[indice].Cells["IdRol"].Value);

                foreach (OpcionesCombo oc in comboRol.Items)
                {
                    if (Convert.ToInt32(oc.Valor) == idRolFila || string.Equals(oc.Texto, rolTexto, StringComparison.OrdinalIgnoreCase))
                    {
                        comboRol.SelectedIndex = comboRol.Items.IndexOf(oc);
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

        private void dgvData_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            TemaCapybara.ModernDgv_CellPainting(sender, e);
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            BtnGuardar2_Click(sender, e);
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            BtnEliminar2_Click(sender, e);
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
        }

        private void btnLimpiarBusqueda_Click(object sender, EventArgs e)
        {
            txtBusqueda.Texts = "";
            foreach (DataGridViewRow row in dgvData.Rows)
            {
                row.Visible = true;
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
                    string valorCelda = row.Cells[columnaFiltro].Value?.ToString() ?? "";
                    row.Visible = valorCelda.Trim().ToUpper().Contains(txtBusqueda.Texts.Trim().ToUpper());
                }
            }
        }

        private bool Validaciones()
        {
            if (string.IsNullOrWhiteSpace(txtDocumento.Texts)) return false;
            if (string.IsNullOrWhiteSpace(txtNombre.Texts)) return false;
            if (string.IsNullOrWhiteSpace(txtApellido.Texts)) return false;
            if (string.IsNullOrWhiteSpace(txtCorreo.Texts) || !txtCorreo.Texts.Contains("@")) return false;
            if (string.IsNullOrWhiteSpace(txtClave.Texts)) return false;
            return true;
        }

        private void BtnGuardar2_Click(object sender, EventArgs e)
        {
            if (Validaciones())
            {
                string mensaje = string.Empty;
                DialogResult confirmacion;

                string rolSeleccionado = ((OpcionesCombo)comboRol.SelectedItem).Texto;
                string estadoSeleccionado = ((OpcionesCombo)comboEstado.SelectedItem).Texto;

                if ((rolSeleccionado == "Gerente" || rolSeleccionado == "Manager") && estadoSeleccionado == "No Activo" &&
                    usuarioActual != null && Convert.ToInt32(txtid.Text) == usuarioActual.IdUsuario)
                {
                    MessageBox.Show("No puede desactivar su propio usuario activo.", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (Convert.ToInt32(txtid.Text) == 0)
                {
                    confirmacion = MessageBox.Show("¿Seguro desea agregar el usuario?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                }
                else
                {
                    confirmacion = MessageBox.Show("¿Seguro desea editar el usuario?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                }

                if (confirmacion == DialogResult.Yes)
                {
                    string nombre = txtNombre.Texts.Trim();
                    string apellido = txtApellido.Texts.Trim();
                    string nombreCompleto = $"{nombre} {apellido}".Trim();

                    Usuario objUsuario = new Usuario()
                    {
                        IdUsuario = Convert.ToInt32(txtid.Text),
                        Documento = txtDocumento.Texts.Trim(),
                        Nombre = nombre,
                        Apellido = apellido,
                        NombreCompleto = nombreCompleto,
                        Direccion = txtDireccion.Texts.Trim(),
                        Telefono = txtTelefono.Texts.Trim(),
                        Correo = txtCorreo.Texts.Trim(),
                        Clave = txtClave.Texts,
                        oRol = new Rol()
                        {
                            IdRol = Convert.ToInt32(((OpcionesCombo)comboRol.SelectedItem).Valor),
                            Descripcion = ((OpcionesCombo)comboRol.SelectedItem).Texto
                        },
                        Estado = Convert.ToInt32(((OpcionesCombo)comboEstado.SelectedItem).Valor) == 1
                    };

                    if (objUsuario.IdUsuario == 0)
                    {
                        int idUsuarioGenerado = new CN_Usuario().Registrar(objUsuario, out mensaje);
                        if (idUsuarioGenerado != 0)
                        {
                            dgvData.Rows.Add(new object[] {
                                "",
                                idUsuarioGenerado,
                                objUsuario.Documento,
                                objUsuario.Nombre,
                                objUsuario.Apellido,
                                objUsuario.NombreCompleto,
                                objUsuario.Direccion,
                                objUsuario.Telefono,
                                objUsuario.Correo,
                                objUsuario.Clave,
                                objUsuario.oRol.IdRol.ToString(),
                                objUsuario.oRol.Descripcion,
                                ((OpcionesCombo)comboEstado.SelectedItem).Valor.ToString(),
                                ((OpcionesCombo)comboEstado.SelectedItem).Texto
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
                        bool resultado = new CN_Usuario().Editar(objUsuario, out mensaje);
                        if (resultado)
                        {
                            DataGridViewRow row = dgvData.Rows[Convert.ToInt32(txtindice.Text)];
                            row.Cells["IdUsuario"].Value = objUsuario.IdUsuario;
                            row.Cells["Documento"].Value = objUsuario.Documento;
                            row.Cells["Nombre"].Value = objUsuario.Nombre;
                            row.Cells["Apellido"].Value = objUsuario.Apellido;
                            row.Cells["NombreCompleto"].Value = objUsuario.NombreCompleto;
                            row.Cells["Direccion"].Value = objUsuario.Direccion;
                            row.Cells["Telefono"].Value = objUsuario.Telefono;
                            row.Cells["Correo"].Value = objUsuario.Correo;
                            row.Cells["Clave"].Value = objUsuario.Clave;
                            row.Cells["IdRol"].Value = objUsuario.oRol.IdRol.ToString();
                            row.Cells["Rol"].Value = objUsuario.oRol.Descripcion;
                            row.Cells["EstadoValor"].Value = ((OpcionesCombo)comboEstado.SelectedItem).Valor.ToString();
                            row.Cells["Estado"].Value = ((OpcionesCombo)comboEstado.SelectedItem).Texto;
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
                MessageBox.Show("Debe completar Documento, Nombre, Apellido, Correo válido y Contraseña.", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void BtnEliminar2_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(txtid.Text) != 0 && (usuarioActual == null || Convert.ToInt32(txtid.Text) != usuarioActual.IdUsuario))
            {
                if (((OpcionesCombo)comboRol.SelectedItem).Texto == "Gerente")
                {
                    MessageBox.Show("No se puede eliminar al Gerente", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (MessageBox.Show("¿Desea dar de baja/eliminar el usuario?", "Mensaje", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    string mensaje = string.Empty;
                    Usuario objUsuario = new Usuario()
                    {
                        IdUsuario = Convert.ToInt32(txtid.Text),
                    };

                    bool respuesta = new CN_Usuario().Eliminar(objUsuario, out mensaje);
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

        private void rjButton1_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
        }

        private void txtDocumento_KeyPress(object sender, KeyPressEventArgs e)
        {
            bool esControl = Char.IsControl(e.KeyChar);
            bool esDigito = Char.IsDigit(e.KeyChar);
            bool longitudPermitida = txtDocumento.Texts.Length < 12;

            e.Handled = !(esControl || (esDigito && longitudPermitida));
        }

        private void txtNombre_KeyPress(object sender, KeyPressEventArgs e)
        {
            bool esControl = Char.IsControl(e.KeyChar);
            bool esDigito = Char.IsDigit(e.KeyChar);
            e.Handled = !(esControl || !esDigito);
        }

        private void txtCorreo_KeyPress(object sender, KeyPressEventArgs e)
        {
            bool esControl = Char.IsControl(e.KeyChar);
            bool longitudPermitida = txtCorreo.Texts.Length < 100;
            e.Handled = !(esControl || longitudPermitida);
        }

        private void txtClave_KeyPress(object sender, KeyPressEventArgs e)
        {
            bool esControl = Char.IsControl(e.KeyChar);
            bool longitudPermitida = txtClave.Texts.Length < 80;
            e.Handled = !(esControl || longitudPermitida);
        }

        private void txtBusqueda_KeyPress(object sender, KeyPressEventArgs e)
        {
            bool esControl = Char.IsControl(e.KeyChar);
            bool longitudPermitida = txtBusqueda.Texts.Length < 80;
            e.Handled = !(esControl || longitudPermitida);
        }
    }
}
