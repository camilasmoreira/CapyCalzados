using CapaEntidad;
using CapaNegocio;
using CapaPresentacion.Modales;
using CapaPresentacion.Utilidades;
using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Color = System.Drawing.Color;

namespace CapaPresentacion
{
    public partial class FrmCompras : Form
    {
        private Usuario usuarioActual;
        public Inicio inicio {  get; set; }
        // Constructor de la clase FrmCompras
        public FrmCompras(Usuario oUsuario,Inicio pinicio)
        {
            usuarioActual = oUsuario; // Asigna el usuario actual si se proporciona uno, de lo contrario será null
            InitializeComponent(); // Inicializa los componentes del formulario
            inicio = pinicio;
        }


        // Maneja el evento Load del formulario FrmCompras
        private void FrmCompras_Load(object sender, EventArgs e)
        {
            // Agrega opciones al ComboBox comboDocumento
            comboDocumento.Items.Add(new OpcionesCombo() { Valor = "Recibo", Texto = "Recibo" }); // Opción para "Recibo"
            comboDocumento.Items.Add(new OpcionesCombo() { Valor = "Factura", Texto = "Factura" }); // Opción para "Factura"

            // Configura el ComboBox para mostrar el texto de las opciones y utilizar el valor asociado
            comboDocumento.DisplayMember = "Texto"; // Establece la propiedad para mostrar el texto
            comboDocumento.ValueMember = "Valor"; // Establece la propiedad para utilizar el valor
            comboDocumento.SelectedIndex = 0; // Selecciona la primera opción por defecto

            // Establece la fecha actual en el campo de texto correspondiente
            txtFecha.Texts = DateTime.Now.ToString("dd/MM/yyyy"); // Formato de fecha: día/mes/año

            // Inicializa los campos de ID de producto y proveedor
            txtIdProd.Text = "0"; // Establece el ID del producto a 0, indicando que no hay producto seleccionado
            txtIdProveedor.Text = "0"; // Establece el ID del proveedor a 0, indicando que no hay proveedor seleccionado
        }


        // Maneja el evento Click en iconButton1 para abrir un formulario modal de selección de producto
        private void iconButton1_Click(object sender, EventArgs e)
        {
            using (var modal = new mdProducto()) // Se crea una instancia del formulario modal mdProducto
            {
                var result = modal.ShowDialog(); // Muestra el formulario modal y espera su resultado
                if (result == DialogResult.OK) // Verifica si el usuario ha confirmado la selección
                {
                    // Asigna los valores del producto seleccionado a los campos de entrada correspondientes
                    txtIdProd.Text = modal._Producto.IdProducto.ToString(); // ID del producto
                    txtCod.Texts = modal._Producto.Codigo.ToString(); // Código del producto
                    txtProducto.Texts = modal._Producto.Nombre.ToString(); // Nombre del producto
                    txtPrecioC.Select(); // Selecciona el campo de precio de compra para facilitar la entrada
                }
                else
                {
                    txtCod.Select(); // Si no se confirma, selecciona el campo de código para volver a intentarlo
                }
            }
        }




        // Maneja el evento Click en el botón btnAgregar para agregar un producto a la lista de compras
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            decimal precioCompra = 0; // Variable para almacenar el precio de compra
            decimal precioVenta = 0; // Variable para almacenar el precio de venta
            bool prodExist = false; // Bandera para verificar si el producto ya existe en la lista

            // Verifica si se ha seleccionado un producto
            if (int.Parse(txtIdProd.Text) == 0)
            {
                MessageBox.Show("Debe seleccionar un producto", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; // Sale del método si no se selecciona un producto
            }

            // Intenta convertir el precio de compra a decimal
            if (!decimal.TryParse(txtPrecioC.Texts, out precioCompra))
            {
                MessageBox.Show("Precio de compra formato de moneda incorrecto", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPrecioC.Select(); // Selecciona el campo de precio de compra para corrección
                return; // Sale del método si el formato es incorrecto
            }

            // Intenta convertir el precio de venta a decimal
            if (!decimal.TryParse(txtPrecioV.Texts, out precioVenta))
            {
                MessageBox.Show("Precio de venta formato de moneda incorrecto", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPrecioV.Select(); // Selecciona el campo de precio de venta para corrección
                return; // Sale del método si el formato es incorrecto
            }

            // Busca el producto en la lista utilizando su código
            Producto oProducto = new CN_Producto().Listar().Where(p => p.Codigo == txtCod.Texts && p.Estado == true).FirstOrDefault();

            // Verifica si el producto ya existe en la DataGridView
            foreach (DataGridViewRow fila in dgvData.Rows)
            {
                if (fila.Cells["IdProducto"].Value.ToString() == txtIdProd.Text)
                {
                    prodExist = true; // Marca que el producto ya existe
                    return; // Sale del método si el producto ya está en la lista
                }
            }

            // Si el producto no existe, lo agrega a la DataGridView
            if (!prodExist)
            {
                dgvData.Rows.Add(new object[]
                {
            txtIdProd.Text, // ID del producto
            txtProducto.Texts, // Nombre del producto
            oProducto.Descripcion, // Descripción del producto
            precioCompra.ToString("0.00"), // Precio de compra con formato
            precioVenta.ToString("0.00"), // Precio de venta con formato
            txtCantidad.Value.ToString(), // Cantidad seleccionada
            (txtCantidad.Value * precioCompra).ToString("0.00") // Cálculo del subtotal
                });

                calcularTotal(); // Llama al método para calcular el total de la compra
                limpiarCampos(); // Limpia los campos de entrada
                txtCod.Select(); // Selecciona el campo de código para agregar otro producto
            }
        }


        // Maneja el evento KeyDown en el campo de texto txtCod para buscar un producto por su código
        private void txtCod_KeyDown(object sender, KeyEventArgs e)
        {
            // Verifica si la tecla presionada es Enter
            if (e.KeyData == Keys.Enter)
            {
                // Intenta buscar un producto que coincida con el código ingresado
                Producto oProducto = new CN_Producto().Listar().Where(p => p.Codigo == txtCod.Texts && p.Estado == true).FirstOrDefault();

                // Si se encuentra un producto, se actualizan los campos correspondientes
                if (oProducto != null)
                {
                    // Cambia el color del texto a verde claro para indicar que se encontró el producto
                    txtCod.ForeColor = Color.Honeydew;

                    // Asigna el ID del producto encontrado al campo txtIdProd
                    txtIdProd.Text = oProducto.IdProducto.ToString();

                    // Asigna el nombre del producto encontrado al campo txtProducto
                    txtProducto.Texts = oProducto.Nombre;

                    // Selecciona el campo txtPrecioC para que el usuario pueda ingresar el precio de compra
                    txtPrecioC.Select();
                }
                else // Si no se encuentra un producto, se reinician los campos
                {
                    // Cambia el color del texto a rojo claro para indicar que no se encontró el producto
                    txtCod.ForeColor = Color.MistyRose;

                    // Resetea el ID del producto a 0
                    txtIdProd.Text = "0";

                    // Limpia el campo del nombre del producto
                    txtProducto.Texts = "";
                }
            }
        }

        // Limpia los campos
        private void limpiarCampos()
        {
            txtIdProd.Text = "0";
            txtCod.Texts = "";
            txtProducto.Texts = "";
            txtPrecioC.Texts = "";
            txtPrecioV.Texts = "";
            txtCantidad.Value = 1;
        }


        // Método que calcula el total de los subtotales en el DataGridView
        private void calcularTotal()
        {
            decimal total = 0; // Inicializa una variable para almacenar el total

            // Verifica si hay filas en el DataGridView
            if (dgvData.Rows.Count > 0)
            {
                // Recorre cada fila del DataGridView
                foreach (DataGridViewRow rows in dgvData.Rows)
                {
                    // Suma el valor de la celda "SubTotal" de cada fila al total
                    total += Convert.ToDecimal(rows.Cells["SubTotal"].Value.ToString());
                }
            }

            // Asigna el total calculado al campo de texto, formateado a dos decimales
            txtTotal.Texts = total.ToString("0.00");
        }


        // Método que maneja el evento de pintura de celdas del DataGridView
        private void dgvData_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            // Verifica si el índice de la fila es menor que 0 (indica que no es una fila de datos)
            if (e.RowIndex < 0)
            {
                return; // Sale del método si es una fila de encabezado o similar
            }

            // Verifica si la columna que se está pintando es la columna 7 (que se supone es la de eliminar)
            if (e.ColumnIndex == 7)
            {
                // Dibuja la celda utilizando todos los elementos visuales
                e.Paint(e.CellBounds, DataGridViewPaintParts.All);

                // Define el ancho y alto del icono de eliminar, ajustando su tamaño
                var w = Properties.Resources.borrar_1_.Width - 8;
                var h = Properties.Resources.borrar_1_.Height - 8;

                // Calcula la posición X y Y para centrar el icono dentro de la celda
                var x = e.CellBounds.Left + (e.CellBounds.Width - w) / 2;
                var y = e.CellBounds.Top + (e.CellBounds.Height - h) / 2;

                // Dibuja el icono de eliminar en la celda
                e.Graphics.DrawImage(Properties.Resources.borrar_1_, new Rectangle(x, y, w, h));

                // Indica que el evento ha sido manejado
                e.Handled = true;
            }
        }


        // Evento que maneja el clic en una celda del DataGridView para eliminar una fila
        private void dgvData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Verifica si la columna clicada es la de eliminar
            if (dgvData.Columns[e.ColumnIndex].Name == "btnEliminar")
            {
                int indice = e.RowIndex; // Obtiene el índice de la fila clicada

                // Verifica que el índice de la fila sea válido
                if (indice >= 0)
                {
                    // Elimina la fila seleccionada del DataGridView
                    dgvData.Rows.RemoveAt(indice);
                    calcularTotal(); // Llama al método para recalcular el total después de eliminar la fila
                }
            }
        }


        private void txtPrecioC_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Verificar si el carácter es una tecla de control (como Backspace)
            bool esControl = Char.IsControl(e.KeyChar);

            // Verificar si el carácter es un dígito
            bool esDigito = Char.IsDigit(e.KeyChar);

            bool longitudPermitida = txtPrecioC.Texts.Length < 8;
            // Permitir solo dígitos, control (como Backspace), o una coma decimal
            if (esControl || (esDigito && longitudPermitida))
            {
                e.Handled = false;
            }
            else if (e.KeyChar == ',' && (txtPrecioC.Texts.Trim().Length > 0 && longitudPermitida))
            {
                // Permitir una coma decimal solo si no existe ya una en el Textso
                if (txtPrecioC.Texts.Contains(","))
                {
                    e.Handled = true;
                }
                else
                {
                    e.Handled = false;
                }
            }
            else
            {
                // Bloquear cualquier otro carácter
                e.Handled = true;
            }
        }

        private void txtPrecioV_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Verificar si el carácter es una tecla de control (como Backspace)
            bool esControl = Char.IsControl(e.KeyChar);

            // Verificar si el carácter es un dígito
            bool esDigito = Char.IsDigit(e.KeyChar);

            bool longitudPermitida = txtPrecioV.Texts.Length < 8;
            // Permitir solo dígitos, control (como Backspace), o una coma decimal
            if (esControl || (esDigito && longitudPermitida))
            {
                e.Handled = false;
            }
            else if (e.KeyChar == ',' && (txtPrecioV.Texts.Trim().Length > 0 && longitudPermitida))
            {
                // Permitir una coma decimal solo si no existe ya una en el Textso
                if (txtPrecioV.Texts.Contains(","))
                {
                    e.Handled = true;
                }
                else
                {
                    e.Handled = false;
                }
            }
            else
            {
                // Bloquear cualquier otro carácter
                e.Handled = true;
            }
        }


        // Método que realiza las validaciones necesarias antes de registrar una compra
        private bool Validaciones()
        {
            // Variable bandera que indica si todas las validaciones son exitosas
            bool bandera = true;

            // Verifica si el campo de documento está vacío
            if (txtDocumento.Texts == "")
            {
                bandera = false; // Marca la bandera como falsa si el campo está vacío
            }

            // Verifica si el campo de razón social está vacío
            if (txtRazon.Texts == "")
            {
                bandera = false; // Marca la bandera como falsa si el campo está vacío
            }

            // Verifica si no hay filas en el DataGridView
            if (dgvData.Rows.Count < 1)
            {
                bandera = false; // Marca la bandera como falsa si no hay filas
            }

            // Verifica si el ID del proveedor es cero
            if (Convert.ToInt32(txtIdProveedor.Text) == 0)
            {
                bandera = false; // Marca la bandera como falsa si el ID del proveedor es cero
            }

            // Devuelve el resultado de las validaciones
            return bandera;
        }


        // Evento que se ejecuta al hacer clic en el botón de registro
        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            // Verifica si las validaciones se cumplen
            if (Validaciones())
            {
                // Muestra un cuadro de diálogo de confirmación para registrar la compra
                DialogResult confirmacion = MessageBox.Show("¿Desea registrar la compra?", "Confirmacion", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                // Si el usuario confirma, procede a registrar la compra
                if (confirmacion == DialogResult.Yes)
                {
                    // Crea un DataTable para almacenar los detalles de la compra
                    DataTable detalle_compra = new DataTable();
                    detalle_compra.Columns.Add("IdProducto", typeof(int));     // Columna para el ID del producto
                    detalle_compra.Columns.Add("PrecioCompra", typeof(decimal)); // Columna para el precio de compra
                    detalle_compra.Columns.Add("PrecioVenta", typeof(decimal));  // Columna para el precio de venta
                    detalle_compra.Columns.Add("Cantidad", typeof(int));         // Columna para la cantidad
                    detalle_compra.Columns.Add("MontoTotal", typeof(decimal));    // Columna para el monto total

                    // Itera a través de las filas del DataGridView para llenar el DataTable
                    foreach (DataGridViewRow row in dgvData.Rows)
                    {
                        detalle_compra.Rows.Add(
                            new Object[]
                            {
                        Convert.ToInt32(row.Cells["IdProducto"].Value.ToString()), // Convierte el ID del producto a entero
                        Convert.ToDecimal(row.Cells["PrecioCompra"].Value.ToString()), // Convierte el precio de compra a decimal
                        Convert.ToDecimal(row.Cells["PrecioVenta"].Value.ToString()), // Convierte el precio de venta a decimal
                        Convert.ToInt32(row.Cells["Cantidad"].Value.ToString()), // Convierte la cantidad a entero
                        Convert.ToDecimal(row.Cells["SubTotal"].Value.ToString()) // Convierte el subtotal a decimal
                            }
                        );
                    }

                    // Obtiene el siguiente número correlativo para la compra
                    int idCorrelativo = new CN_Compra().obtenerCorrelativo();
                    // Formatea el número del documento
                    string numeroDocumento = string.Format("{0:00000}", idCorrelativo);

                    // Crea un objeto de compra con la información necesaria
                    Compra oCompra = new Compra()
                    {
                        oUsuario = new Usuario() { IdUsuario = usuarioActual.IdUsuario }, // Establece el usuario actual
                        oProveedor = new Proveedor() { IdProveedor = Convert.ToInt32(txtIdProveedor.Text) }, // Establece el proveedor seleccionado
                        TipoDocumento = ((OpcionesCombo)comboDocumento.SelectedItem).Texto, // Establece el tipo de documento
                        NumeroDocumento = numeroDocumento, // Asigna el número de documento generado
                        MontoTotal = Convert.ToDecimal(txtTotal.Texts) // Establece el monto total
                    };

                    string Mensaje = string.Empty;
                    // Intenta registrar la compra y obtiene un mensaje de respuesta
                    bool respuesta = new CN_Compra().Registrar(oCompra, detalle_compra, out Mensaje);

                    // Si el registro fue exitoso
                    if (respuesta)
                    {
                        // Muestra un mensaje con el número de compra generada y pregunta si desea copiarlo al portapapeles
                        var result = MessageBox.Show("Numero de compra generada:\n" + numeroDocumento + "\n\n¿Desea ver el detalle de compra?", "Mensaje", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                        if (result == DialogResult.Yes)
                        {
                            Clipboard.SetText(numeroDocumento); // Copia el número de venta al portapapeles.
                            inicio.inicio(new FrmDetalleVenta(numeroDocumento, inicio));
                            txtIdProveedor.Text = "0";
                            txtDocumento.Texts = "";
                            txtRazon.Texts = "";
                            // Limpia los datos del DataGridView
                            dgvData.Rows.Clear();
                            // Recalcula el total
                            calcularTotal();
                            this.Close();
                        }

                        // Limpia los campos de entrada
                        txtIdProveedor.Text = "0";
                        txtDocumento.Texts = "";
                        txtRazon.Texts = "";
                        // Limpia los datos del DataGridView
                        dgvData.Rows.Clear();
                        // Recalcula el total
                        calcularTotal();
                    }
                    else
                    {
                        // Si el registro falla, muestra el mensaje de error
                        MessageBox.Show(Mensaje, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
            }
            else
            {
                // Si las validaciones no se cumplen, muestra un mensaje de error
                MessageBox.Show("No se pudo registrar la compra", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }




        // Evento que se ejecuta al presionar una tecla en el campo de texto txtDocumento
        private void txtDocumento_KeyDown(object sender, KeyEventArgs e)
        {
            // Verifica si la tecla presionada es la tecla "Enter"
            if (e.KeyData == Keys.Enter)
            {
                // Busca un proveedor en la lista que coincida con el documento ingresado y que esté activo
                Proveedor oProveedor = new CN_Proveedor()
                    .Listar() // Obtiene la lista de proveedores
                    .Where(p => p.Documento == txtDocumento.Texts && p.Estado == true) // Filtra por documento y estado activo
                    .FirstOrDefault(); // Obtiene el primer proveedor que cumpla con los criterios o null si no existe

                // Verifica si se encontró un proveedor
                if (oProveedor != null)
                {
                    // Si se encontró el proveedor, cambia el fondo del campo txtDocumento a un color de éxito
                    txtDocumento.ForeColor = Color.Honeydew;
                    // Establece el ID del proveedor en el campo txtIdProveedor
                    txtIdProveedor.Text = oProveedor.IdProveedor.ToString();
                    // Establece la razón social del proveedor en el campo txtRazon
                    txtRazon.Texts = oProveedor.RazonSocial;
                }
                else
                {
                    // Si no se encontró un proveedor, cambia el fondo del campo txtDocumento a un color de error
                    txtDocumento.ForeColor = Color.MistyRose;
                    // Establece el ID del proveedor a "0" para indicar que no se encontró
                    txtIdProveedor.Text = "0";
                    // Limpia el campo txtRazon
                    txtRazon.Texts = "";
                }
            }
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

        private void rjButton2_Click(object sender, EventArgs e)
        {
            using (var modal = new mdProveedor())
            {
                var result = modal.ShowDialog();
                if (result == DialogResult.OK)
                {
                    txtIdProveedor.Text = modal._Proveedor.IdProveedor.ToString();
                    txtDocumento.Texts = modal._Proveedor.Documento.ToString();
                    txtRazon.Texts = modal._Proveedor.RazonSocial.ToString();
                }
                else
                {
                    txtDocumento.Select();
                }
            }
        }

        private void txtPrecioV_KeyDown(object sender, KeyEventArgs e)
        {
            // Verifica si la tecla presionada es la tecla "Enter"
            if (e.KeyData == Keys.Enter && txtPrecioC.Texts != "")
            {
                decimal total = Convert.ToDecimal(txtPrecioC.Texts) + (Convert.ToDecimal(txtPrecioC.Texts) * 0.25m);
                txtPrecioV.Texts = total.ToString();
            }
        }
    }
}
