using CapaEntidad;
using CapaNegocio;
using CapaPresentacion.Utilidades;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;
using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace CapaPresentacion
{
    // Formulario para mostrar el detalle de una venta y generar el Ticket Comprobante de Venta
    public partial class FrmDetalleVenta : Form
    {
        public Inicio Inicio { get; set; }
        public string doc { get; set; }

        // Constructor que inicializa los componentes del formulario
        public FrmDetalleVenta(Inicio inicio)
        {
            InitializeComponent();
            Inicio = inicio;
            Inicio.pintar();
            ConfigurarVistaFormaPago();
        }

        public FrmDetalleVenta(string numero, Inicio inicio)
        {
            InitializeComponent();
            Inicio = inicio;
            Inicio.pintar();
            ConfigurarVistaFormaPago();
            this.doc = numero;
            txtBusqueda.Texts = this.doc;
            btnBuscar_Click(this, new EventArgs());
        }

        private void ConfigurarVistaFormaPago()
        {
            // Se muestra Forma de Pago en lugar de Documento y Nombre de cliente innecesarios
            groupBox3.Text = "Información de Pago";
            label4.Text = "Forma de Pago:";
            txtDocumentoC.PlaceholderText = "Forma de Pago...";
            txtDocumentoC.Size = new Size(220, txtDocumentoC.Size.Height);

            label5.Visible = false;
            txtNombre.Visible = false;
            label10.Visible = false;
            txtCorreoC.Visible = false;

            BtnGuardar2.Text = "Ticket PDF";
        }

        // Evento asociado al botón "Buscar", que busca una venta según el número de venta ingresado
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            // Obtener la venta utilizando el número de venta ingresado en el campo de búsqueda
            Venta oVenta = new CN_Venta().obtenerVenta(txtBusqueda.Texts);

            // Si se encuentra una venta válida
            if (oVenta.IdVenta != 0)
            {
                // Llenar los campos del formulario con los datos de la venta
                txtDocumento.Texts = oVenta.NumeroDocumento;
                txtFecha.Texts = oVenta.FechaRegistro;
                txtTipoDocumento.Texts = oVenta.TipoDocumento;

                txtUsuario.Texts = oVenta.oUsuario.NombreCompleto;
                txtDocV.Texts = oVenta.oUsuario.Documento;

                txtDocumentoC.Texts = string.IsNullOrEmpty(oVenta.FormaPago) ? "Efectivo" : oVenta.FormaPago;
                txtNombre.Texts = string.IsNullOrEmpty(oVenta.NombreCliente) ? "Consumidor Final" : oVenta.NombreCliente;
                txtCorreoC.Texts = "";

                // Limpiar la tabla de detalles de la venta
                dgvData.Rows.Clear();

                // Agregar cada detalle de la venta al DataGridView
                foreach (DetalleVenta dc in oVenta.oDetalleVenta)
                {
                    dgvData.Rows.Add(new object[]
                    {
                        dc.oProducto.Nombre,
                        dc.oProducto.Descripcion,
                        dc.PrecioVenta,
                        dc.Cantidad,
                        dc.SubTotal
                    });
                }

                // Mostrar los montos de pago, cambio y total de la venta
                txtPago.Texts = oVenta.MontoPago.ToString("0.00");
                txtCambio.Texts = oVenta.MontoCambio.ToString("0.00");
                txtTotal.Texts = oVenta.MontoTotal.ToString("0.00");
            }
            else
            {
                // Mostrar un mensaje si no se encuentra la venta
                MessageBox.Show("Debe colocar un numero de venta válido", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        // Evento asociado al botón "Limpiar", que resetea los campos del formulario
        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtFecha.Texts = "";
            txtTipoDocumento.Texts = "";
            txtUsuario.Texts = "";
            txtDocumentoC.Texts = "";
            txtNombre.Texts = "";
            txtDocV.Texts = "";
            txtCorreoC.Texts = "";

            dgvData.Rows.Clear();
            txtPago.Texts = "0.00";
            txtCambio.Texts = "0.00";
            txtTotal.Texts = "0.00";
        }

        // Evento que se ejecuta cuando el formulario carga, selecciona el campo de búsqueda
        private void FrmDetalleVenta_Load(object sender, EventArgs e)
        {
            TemaCapybara.AplicarTema(this);
            ConfigurarVistaFormaPago();
            txtBusqueda.Select();
        }

        // Evento asociado al botón "Descargar", que genera y guarda un archivo PDF tipo Ticket Comprobante de Venta
        private void btnDescargar_Click(object sender, EventArgs e)
        {
            // Verifica si hay una venta seleccionada
            if (txtDocumento.Texts == "")
            {
                MessageBox.Show("No se encontraron resultados para generar el ticket", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            // Cargar la plantilla HTML del Ticket Comprobante de Venta
            string Texto_Html = Properties.Resources.PlantillaVenta.ToString();
            Negocio oDatos = new CN_Negocio().obtenerDatos();

            string formaPago = string.IsNullOrEmpty(txtDocumentoC.Texts) ? "Efectivo" : txtDocumentoC.Texts;

            // Reemplazar los marcadores de la plantilla con los datos de la venta y el negocio
            Texto_Html = Texto_Html.Replace("@nombrenegocio", oDatos.Nombre.ToUpper());
            Texto_Html = Texto_Html.Replace("@docnegocio", oDatos.RUC);
            Texto_Html = Texto_Html.Replace("@direcnegocio", oDatos.Direccion);

            Texto_Html = Texto_Html.Replace("@tipodocumento", txtTipoDocumento.Texts.ToUpper());
            Texto_Html = Texto_Html.Replace("@numerodocumento", txtDocumento.Texts);

            Texto_Html = Texto_Html.Replace("@formapago", formaPago);
            Texto_Html = Texto_Html.Replace("@doccliente", formaPago);
            Texto_Html = Texto_Html.Replace("@nombrecliente", string.IsNullOrEmpty(txtNombre.Texts) ? "Consumidor Final" : txtNombre.Texts);
            Texto_Html = Texto_Html.Replace("@fecharegistro", txtFecha.Texts);
            Texto_Html = Texto_Html.Replace("@usuarioregistro", txtUsuario.Texts);

            // Generar las filas de la tabla con los productos vendidos
            string filas = string.Empty;
            decimal total = 0;
            foreach (DataGridViewRow row in dgvData.Rows)
            {
                filas += "<tr>";
                filas += "<td>" + row.Cells["Producto"].Value.ToString() + "</td>";
                filas += "<td>$" + row.Cells["Precio"].Value.ToString() + "</td>";
                filas += "<td>" + row.Cells["Cantidad"].Value.ToString() + "</td>";
                filas += "<td>$" + row.Cells["SubTotal"].Value.ToString() + "</td>";
                filas += "</tr>";
                total = total + Convert.ToDecimal(row.Cells["SubTotal"].Value);
            }

            // Reemplazar los marcadores con las filas y montos en el HTML
            Texto_Html = Texto_Html.Replace("@filas", filas);
            Texto_Html = Texto_Html.Replace("@tot", total.ToString("0.00"));
            Texto_Html = Texto_Html.Replace("@montototal", txtTotal.Texts);
            Texto_Html = Texto_Html.Replace("@pagocon", txtPago.Texts);
            Texto_Html = Texto_Html.Replace("@cambio", txtCambio.Texts);

            // Mostrar el cuadro de diálogo para guardar el Ticket PDF
            SaveFileDialog savefile = new SaveFileDialog();
            savefile.FileName = string.Format("Ticket_Venta_{0}.pdf", txtDocumento.Texts);
            savefile.Filter = "Pdf Files | *.pdf";

            // Si el usuario selecciona guardar el archivo
            if (savefile.ShowDialog() == DialogResult.OK)
            {
                // Crear un stream de archivo para el PDF
                using (FileStream stream = new FileStream(savefile.FileName, FileMode.Create))
                {
                    // Crear el documento PDF
                    Document pdfDoc = new Document(PageSize.A4, 25, 25, 25, 25);
                    PdfWriter writer = PdfWriter.GetInstance(pdfDoc, stream);
                    pdfDoc.Open();

                    // Obtener el logo del negocio y agregarlo al documento si existe
                    bool obtener = true;
                    byte[] byteImage = new CN_Negocio().obtenerLogo(out obtener);

                    if (obtener)
                    {
                        iTextSharp.text.Image img = iTextSharp.text.Image.GetInstance(byteImage);
                        img.ScaleToFit(60, 60);
                        img.Alignment = iTextSharp.text.Image.UNDERLYING;
                        img.SetAbsolutePosition(pdfDoc.Left, pdfDoc.GetTop(51));
                        pdfDoc.Add(img);
                    }

                    // Leer el HTML y generar el contenido del PDF
                    using (StringReader sr = new StringReader(Texto_Html))
                    {
                        XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
                    }

                    // Cerrar el documento PDF
                    pdfDoc.Close();
                    stream.Close();
                    MessageBox.Show("Ticket Comprobante de Venta generado correctamente.", "Ticket Generado", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    try
                    {
                        Process.Start(savefile.FileName);
                    }
                    catch { }
                }
            }
        }

        // Evento que limita la entrada de caracteres en el campo de búsqueda a solo números y un máximo de 8 dígitos
        private void txtBusqueda_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Verificar si el carácter es una tecla de control (como Backspace)
            bool esControl = Char.IsControl(e.KeyChar);

            // Verificar si el carácter es un dígito
            bool esDigito = Char.IsDigit(e.KeyChar);

            // Verificar la longitud actual del texto y permitir solo hasta 8 dígitos
            bool longitudPermitida = txtBusqueda.Texts.Length < 8;

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

        // Evento que permite buscar la venta cuando se presiona la tecla "Enter"
        private void txtBusqueda_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                btnBuscar_Click(sender, e);
            }
        }

    }
}
