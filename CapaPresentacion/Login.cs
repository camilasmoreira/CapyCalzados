using CapaEntidad;
using CapaNegocio;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace CapaPresentacion
{
    // Clase que representa el formulario de inicio de sesión (Login).
    public partial class Login : Form
    {
        // Constructor de la clase Login.
        public Login()
        {
            InitializeComponent(); // Inicializa los componentes visuales del formulario.
        }

        // Evento que se dispara cuando se carga el formulario Login.
        private void Login_Load(object sender, EventArgs e)
        {
            // Asigna Fondo como contenedor padre para transparencia visual sobre la imagen
            lblHeroTag.Parent = Fondo;
            lblHeroTag.BackColor = Color.Transparent;

            Titulo.Parent = Fondo;
            Titulo.BackColor = Color.Transparent;

            lblHeroSub.Parent = Fondo;
            lblHeroSub.BackColor = Color.Transparent;

            Desc.Parent = Fondo;
            Desc.BackColor = Color.Transparent;

            lblHeroBadge.Parent = Fondo;
            lblHeroBadge.BackColor = Color.Transparent;

            btnSalir.Parent = Fondo;
            btnSalir.BackColor = Color.FromArgb(140, 20, 14, 10);
            btnSalir.Location = new Point(Fondo.Width - 44, 8);

            // Suscribir el dibujado de la tarjeta translúcida sobre Fondo
            Fondo.Paint -= Fondo_Paint;
            Fondo.Paint += Fondo_Paint;

            // Asegurar estilos Capybara en campos
            campoDNI.BackColor = Color.FromArgb(28, 20, 16);
            campoDNI.ForeColor = Color.White;
            campoDNI.BorderColor = Color.FromArgb(90, 65, 48);
            campoDNI.BorderFocusColor = Color.FromArgb(217, 130, 43);

            campoClave.BackColor = Color.FromArgb(28, 20, 16);
            campoClave.ForeColor = Color.White;
            campoClave.BorderColor = Color.FromArgb(90, 65, 48);
            campoClave.BorderFocusColor = Color.FromArgb(217, 130, 43);

            rjButton1.BackColor = Color.FromArgb(217, 130, 43);
            rjButton1.BackgroundColor = Color.FromArgb(217, 130, 43);
            rjButton1.TextColor = Color.White;

            // Enfocar campo de documento al abrir
            campoDNI.Select();
        }

        private void Fondo_Paint(object sender, PaintEventArgs e)
        {
            // Dibujar tarjeta translúcida oscura con borde yuzu para máxima legibilidad y estilo premium
            int cardX = 45;
            int cardY = 115;
            int cardWidth = 475;
            int cardHeight = 245;

            using (SolidBrush brush = new SolidBrush(Color.FromArgb(175, 20, 14, 10)))
            {
                e.Graphics.FillRectangle(brush, cardX, cardY, cardWidth, cardHeight);
            }

            using (Pen pen = new Pen(Color.FromArgb(100, 217, 130, 43), 1))
            {
                e.Graphics.DrawRectangle(pen, cardX, cardY, cardWidth, cardHeight);
            }
        }

        // Evento para cerrar el formulario cuando se presiona el botón "Salir".
        private void Salir_Click(object sender, EventArgs e)
        {
            this.Close(); // Cierra el formulario actual.
        }

        // Evento que se dispara al hacer clic en el botón "Ingresar" para validar credenciales.
        private void btnIngresar_Click(object sender, EventArgs e)
        {
            ValidarIngreso();
        }

        // Evento alternativo para el botón "Ingresar"
        private void btnIngresar2_Click(object sender, EventArgs e)
        {
            ValidarIngreso();
        }

        private void ValidarIngreso()
        {
            string documento = !string.IsNullOrEmpty(campoDNI.Texts) ? campoDNI.Texts.Trim() : campoDNI.Text.Trim();
            string clave = !string.IsNullOrEmpty(campoClave.Texts) ? campoClave.Texts.Trim() : campoClave.Text.Trim();

            Usuario ousuario = new CN_Usuario().Listar().Where(u => u.Documento == documento && u.Clave == clave && u.Estado == true).FirstOrDefault();

            if (ousuario != null)
            {
                Inicio form = new Inicio(ousuario);
                form.Show();
                this.Hide();

                form.FormClosing += frm_closing;
            }
            else
            {
                MessageBox.Show("No se encuentra el Usuario o las credenciales son incorrectas.\n\nUsuario demo:\nDocumento: 1234\nClave: 1234", "Alerta de Acceso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        // Evento que se dispara cuando el formulario "Inicio" se cierra.
        private void frm_closing(object sender, FormClosingEventArgs e)
        {
            campoClave.Texts = "";
            campoDNI.Texts = "";
            this.Show();
            campoDNI.Select();
        }

        // Validación para restringir el número máximo de caracteres en el campo de clave.
        private void campoClave_KeyPress(object sender, KeyPressEventArgs e)
        {
            bool esControl = Char.IsControl(e.KeyChar);
            bool longitudPermitida = campoClave.Texts.Trim().Length < 80;

            if (longitudPermitida || esControl)
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        // Validación para permitir solo dígitos en el campo de documento (DNI) y limitar la longitud.
        private void campoDNI_KeyPress(object sender, KeyPressEventArgs e)
        {
            bool esControl = Char.IsControl(e.KeyChar);
            bool esDigito = Char.IsDigit(e.KeyChar);
            bool longitudPermitida = campoDNI.Texts.Length < 8;

            if (esControl || (esDigito && longitudPermitida))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void campoDNI_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                if (!string.IsNullOrWhiteSpace(campoDNI.Texts) && !string.IsNullOrWhiteSpace(campoClave.Texts))
                {
                    ValidarIngreso();
                }
                else if (!string.IsNullOrWhiteSpace(campoDNI.Texts))
                {
                    campoClave.Focus();
                }
            }
        }

        private void campoClave_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                if (!string.IsNullOrWhiteSpace(campoDNI.Texts) && !string.IsNullOrWhiteSpace(campoClave.Texts))
                {
                    ValidarIngreso();
                }
            }
        }
    }
}
