using CapaEntidad;
using CapaNegocio;

using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace CapaPresentacion
{
    // Formulario para mostrar el detalle de una compra
    public partial class FrmDetalleCompra : Form
    {
        public Inicio inicio {  get; set; }
        public string doc { get; set; }
        // Constructor que recibe un objeto Usuario y ajusta la visibilidad del botón de validación
        public FrmDetalleCompra(Usuario obj)
        {
            InitializeComponent();

            // Si el usuario no tiene el rol de administrador (IdRol != 1), se oculta el botón de validación
            if (obj.oRol.IdRol != 1)
            {
                btnValidar.Visible = false;
            }
        }
        public FrmDetalleCompra(string numero, Inicio pinicio)
        {
            InitializeComponent();
            inicio = pinicio;
            inicio.pintar();
            this.doc = numero;
            txtBusqueda.Texts = this.doc;
            btnBuscar_Click(this, new EventArgs());
        }


        // Evento que busca una compra por su número de documento cuando se hace clic en el botón "Buscar"
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            // Obtiene la compra utilizando el número ingresado en el campo de búsqueda
            Compra oCompra = new CN_Compra().obtenerCompra(txtBusqueda.Texts);

            // Si se encuentra la compra, se rellenan los campos del formulario con los datos de la compra
            if (oCompra.IdCompra != 0)
            {
                txtDocumento.Texts = oCompra.NumeroDocumento;
                txtFecha.Texts = oCompra.FechaRegistro;
                txtTipoDocumento.Texts = oCompra.TipoDocumento;

                txtUsuario.Texts = oCompra.oUsuario.NombreCompleto;
                txtDocV.Texts = oCompra.oUsuario.Documento;

                txtDocumentoProv.Texts = oCompra.oProveedor.Documento;
                txtRazon.Texts = oCompra.oProveedor.RazonSocial;
                txtCorreoP.Texts = oCompra.oProveedor.Correo;

                // Verifica el estado de la compra (En Camino o Recibida)
                if (oCompra.Estado == false)
                {
                    txtEstado.Texts = "En Camino";
                }
                else
                {
                    txtEstado.Texts = "Recibida";
                }

                // Limpiar el DataGridView y agregar los detalles de la compra
                dgvData.Rows.Clear();
                foreach (DetalleCompra dc in oCompra.oDetalleCompra)
                {
                    dgvData.Rows.Add(new object[]
                    {
                        dc.oProducto.Nombre,
                        dc.oProducto.Descripcion,
                        dc.PrecioCompra,
                        dc.Cantidad,
                        dc.MontoTotal
                    });
                }

                // Mostrar el monto total de la compra
                txtTotal.Texts = oCompra.MontoTotal.ToString("0.00");
            }
            else
            {
                // Si no se encuentra la compra, se muestra un mensaje de advertencia
                MessageBox.Show("Debe colocar un numero de compra", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        // Evento que limpia todos los campos del formulario cuando se hace clic en el botón "Limpiar"
        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtFecha.Texts = "";
            txtTipoDocumento.Texts = "";
            txtUsuario.Texts = "";
            txtDocV.Texts = "";
            txtDocumentoProv.Texts = "";
            txtRazon.Texts = "";
            txtCorreoP.Texts = "";
            txtEstado.Texts = "";
            txtTotal.Texts = "0.00";

            // Limpiar el DataGridView
            dgvData.Rows.Clear();
        }

        // Evento que genera y descarga un archivo PDF con los detalles de la compra
        private void btnDescargar_Click(object sender, EventArgs e)
        {
            // Verifica si hay una compra seleccionada antes de intentar generar el PDF
            if (txtDocumento.Texts == "")
            {
                MessageBox.Show("No se encontraron resultados", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            // Cargar la plantilla HTML del PDF desde los recursos
            string Texto_Html = Properties.Resources.PlantillaCompra.ToString();
            Negocio oDatos = new CN_Negocio().obtenerDatos();

            // Reemplazar los marcadores en el HTML con los datos de la compra y del negocio
            Texto_Html = Texto_Html.Replace("@nombrenegocio", oDatos.Nombre.ToUpper());
            Texto_Html = Texto_Html.Replace("@docnegocio", oDatos.RUC);
            Texto_Html = Texto_Html.Replace("@direcnegocio", oDatos.Direccion);

            Texto_Html = Texto_Html.Replace("@tipodocumento", txtTipoDocumento.Texts.ToUpper());
            Texto_Html = Texto_Html.Replace("@numerodocumento", txtDocumento.Texts);

            Texto_Html = Texto_Html.Replace("@docproveedor", txtDocumentoProv.Texts);
            Texto_Html = Texto_Html.Replace("@nombreproveedor", txtRazon.Texts);
            Texto_Html = Texto_Html.Replace("@fecharegistro", txtFecha.Texts);
            Texto_Html = Texto_Html.Replace("@usuarioregistro", txtUsuario.Texts);

            // Construir las filas de la tabla con los productos comprados
            string filas = string.Empty;
            foreach (DataGridViewRow row in dgvData.Rows)
            {
                filas += "<tr>";
                filas += "<td>" + row.Cells["Producto"].Value.ToString() + "</td>";
                filas += "<td>" + row.Cells["PrecioCompra"].Value.ToString() + "</td>";
                filas += "<td>" + row.Cells["Cantidad"].Value.ToString() + "</td>";
                filas += "<td>" + row.Cells["SubTotal"].Value.ToString() + "</td>";
                filas += "</tr>";
            }

            // Reemplazar las filas y el monto total en el HTML
            Texto_Html = Texto_Html.Replace("@filas", filas);
            Texto_Html = Texto_Html.Replace("@montototal", txtTotal.Texts);

            // Mostrar un cuadro de diálogo para seleccionar la ubicación donde guardar el PDF
            SaveFileDialog savefile = new SaveFileDialog();
            savefile.FileName = string.Format("Compra_{0}.pdf", txtDocumento.Texts);
            savefile.Filter = "Pdf Files | *.pdf";

            // Si el usuario confirma la descarga
            if (savefile.ShowDialog() == DialogResult.OK)
            {
                // Crear un nuevo archivo PDF
                using (FileStream stream = new FileStream(savefile.FileName, FileMode.Create))
                {
                    Document pdfDoc = new Document(PageSize.A4, 25, 25, 25, 25);

                    PdfWriter writer = PdfWriter.GetInstance(pdfDoc, stream);
                    pdfDoc.Open();

                    // Obtener y agregar el logo del negocio si está disponible
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
                  
                    // Leer el HTML y agregarlo al documento PDF
                    using (StringReader sr = new StringReader(Texto_Html))
                    {
                        XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
                    }

                    // Cerrar el documento y el stream
                    pdfDoc.Close();
                    stream.Close();
                    MessageBox.Show("Pdf Generado", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        // Evento que se ejecuta al cargar el formulario, seleccionando el campo de búsqueda
        private void FrmDetalleCompra_Load(object sender, EventArgs e)
        {
            txtBusqueda.Select();
        }

        // Evento que restringe la entrada en el campo de búsqueda a solo números y un máximo de 8 dígitos
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

        // Evento que busca la compra cuando se presiona la tecla Enter
        private void txtBusqueda_KeyDown(object sender, KeyEventArgs e)
        {
            // Si se presiona la tecla Enter
            if (e.KeyData == Keys.Enter)
            {
                // Buscar la compra utilizando el número ingresado
                Compra oCompra = new CN_Compra().obtenerCompra(txtBusqueda.Texts);

                if (oCompra.IdCompra != 0)
                {
                    // Si se encuentra la compra, mostrar sus detalles
                    txtBusqueda.ForeColor = Color.Honeydew;
                    txtDocumento.Texts = oCompra.NumeroDocumento;
                    txtFecha.Texts = oCompra.FechaRegistro;
                    txtTipoDocumento.Texts = oCompra.TipoDocumento;

                    txtUsuario.Texts = oCompra.oUsuario.NombreCompleto;
                    txtDocV.Texts = oCompra.oUsuario.Documento;

                    txtDocumentoProv.Texts = oCompra.oProveedor.Documento;
                    txtRazon.Texts = oCompra.oProveedor.RazonSocial;
                    txtCorreoP.Texts = oCompra.oProveedor.Correo;

                    // Verificar el estado de la compra (En Camino o Recibida)
                    if (oCompra.Estado == false)
                    {
                        txtEstado.Texts = "En Camino";
                    }
                    else
                    {
                        txtEstado.Texts = "Recibida";
                    }

                    // Limpiar el DataGridView y agregar los detalles de la compra
                    dgvData.Rows.Clear();
                    foreach (DetalleCompra dc in oCompra.oDetalleCompra)
                    {
                        dgvData.Rows.Add(new object[]
                        {
                            dc.oProducto.Nombre,
                            dc.oProducto.Descripcion,
                            dc.PrecioCompra,
                            dc.Cantidad,
                            dc.MontoTotal
                        });
                    }

                    // Mostrar el monto total de la compra
                    txtTotal.Texts = oCompra.MontoTotal.ToString("0.00");
                }
                else
                {
                    // Si no se encuentra la compra, limpiar los campos
                    txtBusqueda.ForeColor = Color.MistyRose;
                    btnLimpiar_Click(sender, e);
                }
            }
        }

        // Evento que valida la compra seleccionada
        private void rjButton2_Click(object sender, EventArgs e)
        {
            String Mensaje = string.Empty;

            // Verificar si se ha ingresado un número de compra
            if (txtBusqueda.Texts != "")
            {
                // Validar la compra
                bool Resultado = new CN_Compra().ValidarCompra(txtBusqueda.Texts, out Mensaje);
                MessageBox.Show(Mensaje, "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtEstado.Texts = "Recibida";
            }
            else
            {
                // Mostrar mensaje de error si no hay ninguna compra para validar
                MessageBox.Show("No hay ninguna compra para validar", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
    }
}
