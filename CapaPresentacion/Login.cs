using CapaEntidad;
using CapaNegocio;
using DocumentFormat.OpenXml.Bibliography;
using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Forms;
using MessageBox = System.Windows.Forms.MessageBox;
using Point = System.Drawing.Point;

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
            // Configura la transparencia y ubicación de los controles en el formulario.
            Titulo.Parent = Fondo; // Asigna el fondo como padre del control Título.
            Titulo.BackColor = Color.Transparent; // Establece el fondo del título como transparente.
            Titulo.Location = new Point(120, 200); // Define la ubicación del título.

            Desc.Parent = Fondo; // Asigna el fondo como padre del control Descripción.
            Desc.BackColor = Color.Transparent; // Establece el fondo de la descripción como transparente.
            Desc.Location = new Point(134, 296); // Define la ubicación de la descripción.

            btnSalir.Parent = Fondo; // Asigna el fondo como padre del botón Salir.
            btnSalir.BackColor = Color.Transparent; // Establece el fondo del botón como transparente.
            btnSalir.Location = new Point(528, 0); // Define la ubicación del botón Salir.
        }

        // Evento para cerrar el formulario cuando se presiona el botón "Salir".
        private void Salir_Click(object sender, EventArgs e)
        {
            this.Close(); // Cierra el formulario actual.
        }

        // Evento que se dispara al hacer clic en el botón "Ingresar" para validar credenciales.
        private void btnIngresar_Click(object sender, EventArgs e)
        {
            // Busca el usuario en la base de datos mediante el documento y la clave proporcionados.
            Usuario ousuario = new CN_Usuario().Listar().Where(u => u.Documento == campoDNI.Text && u.Clave == campoClave.Text && u.Estado == true).FirstOrDefault();

            // Si el usuario es encontrado y está activo, abre el formulario principal "Inicio".
            if (ousuario != null)
            {
                Inicio form = new Inicio(ousuario); // Crea una instancia del formulario Inicio pasando el usuario.
                form.Show(); // Muestra el formulario de Inicio.
                this.Hide(); // Oculta el formulario de Login.

                form.FormClosing += frm_closing; // Asocia el evento de cierre del formulario para reiniciar Login.
            }
            else
            {
                // Muestra un mensaje de advertencia si no se encuentra el usuario.
                System.Windows.MessageBox.Show("No se encuentra el Usuario", "Alerta", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        // Evento que se dispara cuando el formulario "Inicio" se cierra.
        private void frm_closing(object sender, FormClosingEventArgs e)
        {
            campoClave.Text = ""; // Limpia el campo de la clave.
            campoDNI.Text = ""; // Limpia el campo del documento.
            this.Show(); // Muestra de nuevo el formulario Login.
        }

        // Evento alternativo para el botón "Ingresar" que utiliza otra propiedad para los textos.
        private void btnIngresar2_Click(object sender, EventArgs e)
        {
            // Similar al evento btnIngresar_Click pero usa campoDNI.Texts y campoClave.Texts.
            Usuario ousuario = new CN_Usuario().Listar().Where(u => u.Documento == campoDNI.Texts && u.Clave == campoClave.Texts && u.Estado == true).FirstOrDefault();

            if (ousuario != null)
            {
                Inicio form = new Inicio(ousuario); // Abre el formulario Inicio.
                form.Show(); // Muestra el formulario Inicio.
                this.Hide(); // Oculta el formulario Login.

                form.FormClosing += frm_closing; // Asocia el evento para cuando el formulario Inicio se cierre.
            }
            else
            {
                System.Windows.MessageBox.Show("No se encuentra el Usuario", "Alerta", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        // Evento para cerrar el formulario al hacer clic en el botón correspondiente (icono).
        private void iconButton1_Click(object sender, EventArgs e)
        {
            this.Close(); // Cierra el formulario Login.
        }

        // Validación para restringir el número máximo de caracteres en el campo de clave.
        private void campoClave_KeyPress(object sender, KeyPressEventArgs e)
        {
            bool esControl = Char.IsControl(e.KeyChar); // Verifica si la tecla presionada es de control.
            bool longitudPermitida = campoClave.Texts.Trim().Length < 80; // Permite hasta 80 caracteres.

            // Permite la entrada solo si es una tecla de control o si no se ha alcanzado el límite.
            if (longitudPermitida || esControl)
            {
                e.Handled = false; // Permite el carácter.
            }
            else
            {
                e.Handled = true; // Bloquea el carácter.
            }
        }

        // Validación para permitir solo dígitos en el campo de documento (DNI) y limitar la longitud.
        private void campoDNI_KeyPress(object sender, KeyPressEventArgs e)
        {
            bool esControl = Char.IsControl(e.KeyChar); // Verifica si la tecla presionada es de control.
            bool esDigito = Char.IsDigit(e.KeyChar); // Verifica si el carácter es un dígito.
            bool longitudPermitida = campoDNI.Texts.Length < 8; // Permite hasta 8 dígitos.

            // Permite solo dígitos y teclas de control si la longitud es permitida.
            if (esControl || (esDigito && longitudPermitida))
            {
                e.Handled = false; // Permite el carácter.
            }
            else
            {
                e.Handled = true; // Bloquea el carácter.
            }
        }

        private void campoDNI_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter) // Si el usuario presiona la tecla Enter.
            {
                if(campoDNI.Texts != "" && campoClave.Texts != "")
                {
                    btnIngresar2_Click(sender,e);
                }
            }
        }

        private void campoClave_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter) // Si el usuario presiona la tecla Enter.
            {
                if (campoDNI.Texts != "" && campoClave.Texts != "")
                {
                    btnIngresar2_Click(sender, e);
                }
            }
        }
    }
}
