using CapaEntidad;
using CapaNegocio;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;
using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace CapaPresentacion
{
    // Formulario para mostrar el detalle de una venta
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

        }
        public FrmDetalleVenta(string numero, Inicio inicio)
        {
            InitializeComponent();
            Inicio = inicio;
            Inicio.pintar();
            this.doc = numero;
            txtBusqueda.Texts = this.doc;
            btnBuscar_Click(this, new EventArgs());
        }

        // Evento asociado al botón "Buscar", que busca una venta según el número de venta ingresado
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            // Obtener la venta utilizando el número de venta ingresado en el campo de búsqueda
            Venta oVenta = new CN_Venta().obtenerVenta(txtBusqueda.Texts);

            // Si se encuentra una venta válida
            if (oVenta.IdVenta != 0)
            {
                // Buscar los detalles del cliente asociado a la venta
                Cliente oCliente = new CN_Cliente().Listar().Where(d => d.Documento == oVenta.DocumentoCliente).First();

                // Llenar los campos del formulario con los datos de la venta
                txtDocumento.Texts = oVenta.NumeroDocumento;
                txtFecha.Texts = oVenta.FechaRegistro;
                txtTipoDocumento.Texts = oVenta.TipoDocumento;

                txtUsuario.Texts = oVenta.oUsuario.NombreCompleto;
                txtDocV.Texts = oVenta.oUsuario.Documento;

                txtDocumentoC.Texts = oVenta.DocumentoCliente;
                txtNombre.Texts = oVenta.NombreCliente;
                txtCorreoC.Texts = oCliente.Correo;

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
                MessageBox.Show("Debe colocar un numero de venta", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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
            txtBusqueda.Select();
          
        }

        // Evento asociado al botón "Descargar", que genera y guarda un archivo PDF con los detalles de la venta
        private void btnDescargar_Click(object sender, EventArgs e)
        {
            // Verifica si hay una venta seleccionada
            if (txtDocumento.Texts == "")
            {
                MessageBox.Show("No se encontraron resultados", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            // Cargar la plantilla HTML del PDF desde los recursos
            string Texto_Html = Properties.Resources.PlantillaVenta.ToString();
            Negocio oDatos = new CN_Negocio().obtenerDatos();

            // Reemplazar los marcadores de la plantilla con los datos de la venta y el negocio
            Texto_Html = Texto_Html.Replace("@nombrenegocio", oDatos.Nombre.ToUpper());
            Texto_Html = Texto_Html.Replace("@docnegocio", oDatos.RUC);
            Texto_Html = Texto_Html.Replace("@direcnegocio", oDatos.Direccion);

            Texto_Html = Texto_Html.Replace("@tipodocumento", txtTipoDocumento.Texts.ToUpper());
            Texto_Html = Texto_Html.Replace("@numerodocumento", txtDocumento.Texts);

            Texto_Html = Texto_Html.Replace("@doccliente", txtDocumentoC.Texts);
            Texto_Html = Texto_Html.Replace("@nombrecliente", txtNombre.Texts);
            Texto_Html = Texto_Html.Replace("@fecharegistro", txtFecha.Texts);
            Texto_Html = Texto_Html.Replace("@usuarioregistro", txtUsuario.Texts);

            // Generar las filas de la tabla con los productos vendidos
            string filas = string.Empty;
            decimal total = 0;
            foreach (DataGridViewRow row in dgvData.Rows)
            {
                filas += "<tr>";
                filas += "<td>" + row.Cells["Producto"].Value.ToString() + "</td>";
                filas += "<td>" + row.Cells["Precio"].Value.ToString() + "</td>";
                filas += "<td>" + row.Cells["Cantidad"].Value.ToString() + "</td>";
                filas += "<td>" + row.Cells["SubTotal"].Value.ToString() + "</td>";
                filas += "</tr>";
                total = total + Convert.ToDecimal(row.Cells["SubTotal"].Value);
            }

            // Reemplazar los marcadores con las filas y montos en el HTML
            Texto_Html = Texto_Html.Replace("@filas", filas);
            Texto_Html = Texto_Html.Replace("@tot", total.ToString());
            Texto_Html = Texto_Html.Replace("@montototal", txtTotal.Texts);
            Texto_Html = Texto_Html.Replace("@pagocon", txtPago.Texts);
            Texto_Html = Texto_Html.Replace("@cambio", txtCambio.Texts);

            // Mostrar el cuadro de diálogo para guardar el archivo PDF
            SaveFileDialog savefile = new SaveFileDialog();
            savefile.FileName = string.Format("Venta_{0}.pdf", txtDocumento.Texts);
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
                        img.SetAbsolutePosition(pdfDoc.Left, pdfDoc.GetTop(51)); // Ajusta la posición vertical
                       
                        
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
                    MessageBox.Show("Pdf Generado", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                // Realiza la búsqueda de la venta cuando se presiona Enter
                Venta oVenta = new CN_Venta().obtenerVenta(txtBusqueda.Texts);

                if (oVenta.IdVenta != 0)
                {
                    // Cargar los detalles de la venta y del cliente
                    Cliente oCliente = new CN_Cliente().Listar().Where(d => d.Documento == oVenta.DocumentoCliente).First();

                    txtDocumento.Texts = oVenta.NumeroDocumento;
                    txtFecha.Texts = oVenta.FechaRegistro;
                    txtTipoDocumento.Texts = oVenta.TipoDocumento;

                    txtUsuario.Texts = oVenta.oUsuario.NombreCompleto;
                    txtDocV.Texts = oVenta.oUsuario.Documento;

                    txtDocumentoC.Texts = oVenta.DocumentoCliente;
                    txtNombre.Texts = oVenta.NombreCliente;
                    txtCorreoC.Texts = oCliente.Correo;

                    dgvData.Rows.Clear();

                    // Agregar los productos de la venta al DataGridView
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
                    // Si no se encuentra la venta, limpiar los campos
                    txtBusqueda.ForeColor = Color.MistyRose;
                    btnLimpiar_Click(sender, e);
                }
            }
        }

    }
}
