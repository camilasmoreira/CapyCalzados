using CapaEntidad;
using CapaNegocio;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace CapaPresentacion
{
    // Formulario que permite gestionar los datos del negocio
    public partial class FrmNegocio : Form
    {
        public Inicio Inicio { get; set; }
        // Constructor del formulario, inicializa los componentes
        public FrmNegocio(Inicio inicio)
        {
            InitializeComponent();
            Inicio = inicio;
            Inicio.pintar();
        }

        // Convierte un arreglo de bytes (byte[]) en una imagen (Image)
        public Image byteToImage(byte[] img)
        {
            MemoryStream ms = new MemoryStream();  // Crear un stream en memoria
            ms.Write(img, 0, img.Length);  // Escribir el contenido del arreglo de bytes en el stream
            Image imagen = new Bitmap(ms);  // Crear una imagen a partir del stream

            return imagen;  // Retornar la imagen convertida
        }

        // Evento que se ejecuta cuando se carga el formulario
        private void FrmNegocio_Load(object sender, EventArgs e)
        {
            // Variable para indicar si se ha obtenido correctamente el logotipo
            bool obtenido = true;
            // Obtener el logotipo del negocio desde la capa de negocio
            byte[] byteimg = new CN_Negocio().obtenerLogo(out obtenido);

            // Si se obtuvo el logotipo, mostrarlo en el control PictureBox
            if (obtenido)
            {
                picLogo.Image = byteToImage(byteimg);
            }

            // Obtener los datos del negocio y asignarlos a los controles del formulario
            Negocio obj = new CN_Negocio().obtenerDatos();
            txtNombre.Texts = obj.Nombre;
            txtRuc.Texts = obj.RUC;
            txtDireccion.Texts = obj.Direccion;
        }

        // Evento que permite seleccionar y cargar una imagen de logotipo
        private void btnSubir_Click(object sender, EventArgs e)
        {
            string mensaje = string.Empty;
            OpenFileDialog open = new OpenFileDialog();  // Cuadro de diálogo para abrir archivos
            open.FileName = "Files | *.jpg;*.jpeg;*.png";  // Filtro para seleccionar imágenes

            // Si el usuario selecciona un archivo
            if (open.ShowDialog() == DialogResult.OK)
            {
                // Leer el archivo seleccionado en un arreglo de bytes
                byte[] img = File.ReadAllBytes(open.FileName);
                // Actualizar el logotipo en la base de datos a través de la capa de negocio
                bool respuesta = new CN_Negocio().actualizarLogo(img, out mensaje);

                // Si la actualización fue exitosa, mostrar la nueva imagen en el PictureBox
                if (respuesta)
                {
                    picLogo.Image = byteToImage(img);
                }
                else
                {
                    // Mostrar mensaje de error si no se pudo actualizar el logotipo
                    MessageBox.Show(mensaje, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }

        // Método para validar que los campos requeridos (Nombre, RUC, Dirección) estén completos
        private bool Validaciones()
        {
            bool validacion = true;

            // Validar que el campo de nombre no esté vacío
            if (txtNombre.Texts == "")
            {
                validacion = false;
            }
            // Validar que el campo de RUC no esté vacío
            if (txtRuc.Texts == "")
            {
                validacion = false;
            }
            // Validar que el campo de dirección no esté vacío
            if (txtDireccion.Texts == "")
            {
                validacion = false;
            }

            return validacion;  // Retornar true si todos los campos están completos, false en caso contrario
        }

        // Evento que se ejecuta cuando el usuario hace clic en el botón "Guardar"
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            string mensaje = string.Empty;

            // Verificar que todos los campos estén completos antes de proceder
            if (Validaciones())
            {
                // Solicitar confirmación del usuario antes de realizar cambios
                DialogResult confirmacion = MessageBox.Show("¿Seguro desea cambiar los datos?", "Confirmacion", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (confirmacion == DialogResult.Yes)
                {
                    // Crear un objeto Negocio con los datos ingresados por el usuario
                    Negocio obj = new Negocio()
                    {
                        Nombre = txtNombre.Texts,
                        RUC = txtRuc.Texts,
                        Direccion = txtDireccion.Texts
                    };

                    // Intentar guardar los datos en la base de datos a través de la capa de negocio
                    bool respuesta = new CN_Negocio().guardarDatos(obj, out mensaje);

                    // Mostrar mensajes según el resultado de la operación
                    if (respuesta)
                    {
                        MessageBox.Show("Los cambios fueron guardados", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("No se guardaron los cambios", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                // Mostrar un mensaje de advertencia si algún campo está incompleto
                MessageBox.Show("Debe Completar todos los campos", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        // Evento que permite la entrada solo de números en el campo RUC, con un límite de 11 dígitos
        private void txtRuc_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Verificar si el carácter es una tecla de control (como Backspace)
            bool esControl = Char.IsControl(e.KeyChar);

            // Verificar si el carácter es un dígito
            bool esDigito = Char.IsDigit(e.KeyChar);

            // Verificar la longitud actual del texto y permitir solo hasta 11 dígitos
            bool longitudPermitida = txtRuc.Texts.Length < 11;

            // Permitir el carácter solo si es una tecla de control o un dígito, y la longitud no ha superado el límite
            if (esControl || (esDigito && longitudPermitida))
            {
                e.Handled = false; // Permitir el carácter
            }
            else
            {
                e.Handled = true; // Bloquear el carácter
            }
        }

        // Evento que permite la entrada solo de letras y caracteres de control en el campo Nombre
        private void txtNombre_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Verificar si el carácter es una tecla de control (como Backspace)
            bool esControl = Char.IsControl(e.KeyChar);

            // Verificar si el carácter es un dígito (no permitir números)
            bool esDigito = Char.IsDigit(e.KeyChar);

            // Verificar la longitud actual del texto y permitir solo hasta 60 caracteres
            bool longitudPermitida = txtNombre.Texts.Length < 60;

            // Permitir el carácter solo si es una tecla de control o un carácter que no sea dígito, y la longitud es válida
            if (esControl || (!esDigito && longitudPermitida))
            {
                e.Handled = false; // Permitir el carácter
            }
            else
            {
                e.Handled = true; // Bloquear el carácter
            }
        }

        // Evento que permite la entrada de cualquier carácter en el campo Dirección, con un límite de 60 caracteres
        private void txtDireccion_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Verificar si el carácter es una tecla de control (como Backspace)
            bool esControl = Char.IsControl(e.KeyChar);

            // Verificar la longitud actual del texto y permitir solo hasta 60 caracteres
            bool longitudPermitida = txtNombre.Texts.Length < 60;

            // Permitir el carácter solo si es una tecla de control o la longitud es válida
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
