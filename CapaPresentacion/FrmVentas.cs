using CapaEntidad;
using CapaNegocio;
using CapaPresentacion.Modales;
using CapaPresentacion.Utilidades;
using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Forms;

namespace CapaPresentacion
{
    public partial class FrmVentas : Form
    {
        private Usuario usuarioActual;
        public Inicio inicio { get; set; }
        public FrmVentas(Usuario oUsuario, Inicio pinicio)
        {
            usuarioActual = oUsuario; // Se asigna el usuario actual que inició la sesión.
            InitializeComponent(); // Inicializa los componentes visuales del formulario.
            this.inicio = pinicio;
        }

        private void FrmVentas_Load(object sender, EventArgs e)
        {
            TemaCapybara.AplicarTema(this);
            // Configuración inicial del comboDocumento (tipo de documento).
            comboDocumento.Items.Add(new OpcionesCombo() { Valor = "Recibo", Texto = "Recibo" });
            comboDocumento.Items.Add(new OpcionesCombo() { Valor = "Factura", Texto = "Factura" });

            // Estilo de comboDocumento (fondo negro y texto blanco).
            comboDocumento.BackColor = Color.Black;
            comboDocumento.ForeColor = Color.White;

            // Configuración del combo para mostrar el texto y obtener el valor.
            comboDocumento.DisplayMember = "Texto";
            comboDocumento.ValueMember = "Valor";
            comboDocumento.SelectedIndex = 0; // Selecciona por defecto la primera opción.

            // Asigna la fecha actual al campo de texto txtFecha.
            txtFecha.Texts = DateTime.Now.ToString("dd/MM/yyyy");

            // Inicializa el id del producto como "0" (sin producto seleccionado).
            txtIdProd.Text = "0";
            txtCupon.Text = "0";
            // Inicializa los campos de pago, cambio y total.
            txtPaga.Texts = "";
            txtCambio.Texts = "";
            txtTotal.Texts = "0";
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            // Abre un modal para seleccionar un cliente.
            using (var modal = new mdCliente())
            {
                var result = modal.ShowDialog(); // Muestra el diálogo modal.

                if (result == DialogResult.OK) // Si se selecciona un cliente, llena los campos de documento y nombre.
                {
                    txtIdCliente.Text = modal._Cliente.IdCliente.ToString();
                    txtDocumento.Texts = modal._Cliente.Documento.ToString();
                    txtNombreC.Texts = modal._Cliente.NombreCompleto.ToString();
                    txtCod.Select(); // Mueve el foco al campo de código del producto.
                }
                else
                {
                    txtDocumento.Select(); // Si no se selecciona cliente, el foco vuelve al campo de documento.
                }
            }
        }

        private void btnBuscarProd_Click(object sender, EventArgs e)
        {
            // Abre un modal para seleccionar un producto.
            using (var modal = new mdProducto())
            {
                var result = modal.ShowDialog(); // Muestra el diálogo modal.

                if (result == DialogResult.OK) // Si se selecciona un producto, llena los campos de producto y precio.
                {
                    txtIdProd.Text = modal._Producto.IdProducto.ToString();
                    txtCod.Texts = modal._Producto.Codigo.ToString();
                    txtProducto.Texts = modal._Producto.Nombre.ToString();
                    txtPrecio.Texts = modal._Producto.PrecioVenta.ToString("0.00");
                    txtStock.Texts = modal._Producto.Stock.ToString();

                    // Configura el control de cantidad mínimo y máximo.
                    txtCantidad.Select();
                    txtCantidad.Minimum = 1;
                    txtCantidad.Maximum = modal._Producto.Stock;
                }
                else
                {
                    txtCod.Select(); // Si no se selecciona producto, el foco vuelve al campo de código del producto.
                }
            }
        }

  
            

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            decimal precio = 0;
            bool prodExist = false;

            // Validación: Verifica si se ha seleccionado un producto.
            if (int.Parse(txtIdProd.Text) == 0)
            {
                MessageBox.Show("Debe seleccionar un producto", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Validación: Verifica si se ha ingresado un código de producto.
            if (txtCod.Texts == "")
            {
                MessageBox.Show("No ingresó un código al producto", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Validación: Verifica si se ha ingresado un nombre de producto.
            if (txtProducto.Texts == "")
            {
                MessageBox.Show("No ingresó un nombre al producto", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Validación: Verifica que el precio ingresado sea un valor decimal válido.
            if (!decimal.TryParse(txtPrecio.Texts, out precio))
            {
                MessageBox.Show("Precio - Formato de moneda incorrecto", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPrecio.Select();
                return;
            }

            // Validación: Verifica que la cantidad solicitada no exceda el stock disponible.
            if (Convert.ToInt32(txtStock.Texts) < Convert.ToInt32(txtCantidad.Value.ToString()))
            {
                MessageBox.Show("La cantidad no puede ser mayor que el stock", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPrecio.Select();
                return;
            }

            // Validación: Verifica que la cantidad sea mayor que cero.
            if (Convert.ToInt32(txtCantidad.Value.ToString()) == 0)
            {
                MessageBox.Show("No hay Stock suficiente", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPrecio.Select();
                return;
            }

            // Verifica si el producto ya existe en la lista (DataGridView).
            foreach (DataGridViewRow fila in dgvData.Rows)
            {
                if (fila.Cells["IdProducto"].Value.ToString() == txtIdProd.Text)
                {
                    prodExist = true; // Marca que el producto ya está en la lista.
                    return; // No agrega el producto duplicado.
                }
            }

            // Si el producto no está duplicado, lo agrega al DataGridView.
            if (!prodExist)
            {
                // Busca el producto por código y verifica si está activo.
                Producto oProducto = new CN_Producto().Listar().Where(p => p.Codigo == txtCod.Texts && p.Estado == true).FirstOrDefault();

                // Resta la cantidad del stock del producto en la base de datos.
                bool respuesta = new CN_Venta().RestarStock(Convert.ToInt32(txtIdProd.Text), Convert.ToInt32(txtCantidad.Value.ToString()));

                if (respuesta) // Si la actualización del stock es exitosa.
                {
                    // Agrega el producto a la lista (DataGridView).
                    dgvData.Rows.Add(new object[]
                    {
                txtIdProd.Text,
                txtProducto.Texts,
                oProducto.Descripcion,
                precio.ToString("0.00"),
                txtCantidad.Value.ToString(),
                (txtCantidad.Value * precio).ToString("0.00") // Total por producto (cantidad * precio).
                    });

                    // Calcula el total de la venta.
                    calcularTotal();

                    // Limpia los campos para un nuevo ingreso de producto.
                    limpiarCampos();
                    txtCod.Select(); // Foco en el campo de código del producto para nueva entrada.
                }
            }
        }


        private void txtCod_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter) // Si el usuario presiona la tecla Enter.
            {
                // Busca el producto en la lista por código.
                Producto oProducto = new CN_Producto().Listar().Where(p => p.Codigo == txtCod.Texts && p.Estado == true).FirstOrDefault();

                if (oProducto != null) // Si el producto se encuentra.
                {
                    // Muestra los detalles del producto en los campos correspondientes.
                    txtCod.ForeColor = Color.Honeydew;
                    txtIdProd.Text = oProducto.IdProducto.ToString();
                    txtProducto.Texts = oProducto.Nombre;
                    txtPrecio.Texts = oProducto.PrecioVenta.ToString("0.00");
                    txtStock.Texts = oProducto.Stock.ToString();
                    txtPrecio.Select(); // Mueve el foco al campo de precio.

                    // Establece las cantidades mínimas y máximas basadas en el stock disponible.
                    txtCantidad.Minimum = 1;
                    txtCantidad.Maximum = oProducto.Stock;
                }
                else // Si no se encuentra el producto.
                {
                    // Limpia los campos y cambia el color del campo de código para indicar un error.
                    txtCod.ForeColor = Color.MistyRose;
                    txtIdProd.Text = "0";
                    txtProducto.Texts = "";
                    txtPrecio.Texts = "";
                    txtStock.Texts = "";
                    txtCantidad.Value = 1;
                }
            }
        }


        private void limpiarCampos()
        {
            txtIdProd.Text = "0";
            txtCod.Texts = "";
            txtCupon.Text = "0";
            txtProducto.Texts = "";
            txtPrecio.Texts = "";
            txtStock.Texts = "";
        
        }


        private void calcularTotal()
        {
            decimal total = 0; // Variable para almacenar el total.

            // Si hay filas en el DataGridView, recorre cada una y suma el subtotal de cada producto.
            if (dgvData.Rows.Count > 0)
            {
                foreach (DataGridViewRow rows in dgvData.Rows)
                {
                    total += Convert.ToDecimal(rows.Cells["SubTotal"].Value.ToString());
                }
            }

            // Muestra el total en el campo txtTotal.
            txtTotal.Texts = total.ToString("0.00");
        }


        private void dgvData_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            TemaCapybara.ModernDgv_CellPainting(sender, e);
        }


        private void dgvData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Verifica si se hizo clic en la columna del botón "Eliminar".
            if (dgvData.Columns[e.ColumnIndex].Name == "btnEliminar")
            {
                int indice = e.RowIndex; // Obtiene el índice de la fila donde se hizo clic.

                if (indice >= 0) // Verifica que el índice sea válido (no es cabecera).
                {
                    // Suma el stock del producto eliminado.
                    bool respuesta = new CN_Venta().SumarStock(
                        Convert.ToInt32(dgvData.Rows[indice].Cells["IdProducto"].Value.ToString()),
                        Convert.ToInt32(dgvData.Rows[indice].Cells["Cantidad"].Value.ToString())
                    );

                    if (respuesta) // Si el stock se actualizó correctamente.
                    {
                        dgvData.Rows.RemoveAt(indice); // Elimina la fila del producto en el DataGridView.
                        calcularTotal(); // Recalcula el total después de eliminar el producto.
                    }
                }
            }
        }


        private void txtPrecio_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Verificar si el carácter es una tecla de control (como Backspace)
            bool esControl = Char.IsControl(e.KeyChar);

            // Verificar si el carácter es un dígito
            bool esDigito = Char.IsDigit(e.KeyChar);

            bool longitudPermitida = txtPrecio.Texts.Length < 8;
            // Permitir solo dígitos, control (como Backspace), o una coma decimal
            if (esControl || (esDigito && longitudPermitida))
            {
                e.Handled = false;
            }
            else if (e.KeyChar == ',' && (txtPrecio.Texts.Trim().Length > 0 && longitudPermitida) )
            {
                // Permitir una coma decimal solo si no existe ya una en el Textso
                if (txtPrecio.Texts.Contains(","))
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

        private void txtStock_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar) || Char.IsControl(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void txtPaga_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Permitir solo dígitos, control (como Backspace), o una coma decimal
            if (Char.IsDigit(e.KeyChar) || Char.IsControl(e.KeyChar))
            {
                e.Handled = false;
            }
            else if (e.KeyChar == ',' && txtPrecio.Texts.Trim().Length > 0)
            {
                // Permitir una coma decimal solo si no existe ya una en el Textso
                if (txtPrecio.Texts.Contains(","))
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

        private void calcularCambio()
        {
            if (txtTotal.Texts.Trim() == "") // Verifica si el total es vacío.
            {
                MessageBox.Show("No existen productos en la venta", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return; // Si no hay productos en la venta, muestra un mensaje de advertencia.
            }

            decimal pagacon; // Cantidad con la que el cliente paga.
            decimal total = Convert.ToDecimal(txtTotal.Texts); // Convierte el total a decimal.

            if (txtPaga.Texts.Trim() == "") // Si el campo de pago está vacío.
            {
                txtPaga.Texts = "0"; // Asigna un valor de 0 para evitar errores de conversión.
            }

            if (decimal.TryParse(txtPaga.Texts.Trim(), out pagacon)) // Intenta convertir el texto a decimal.
            {
                if (pagacon < total) // Si la cantidad pagada es menor que el total.
                {
                    txtCambio.Texts = "0.00"; // No hay cambio.
                }
                else
                {
                    decimal cambio = pagacon - total; // Calcula el cambio.
                    txtCambio.Texts = cambio.ToString("0.00"); // Muestra el cambio formateado con dos decimales.
                }
            }
        }


        private void txtPaga_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter) // Si el usuario presiona la tecla Enter.
            {
                calcularCambio(); // Llama a la función para calcular el cambio.
            }
        }


        private bool validaciones()
        {
            bool bandera = true; // Variable bandera para validar el estado.

            if (txtDocumento.Texts == "") // Verifica si el campo de documento está vacío.
            {
                bandera = false; // Si está vacío, marca la bandera como falsa.
            }
            if (txtNombreC.Texts == "") // Verifica si el campo de nombre está vacío.
            {
                bandera = false;
            }
            if (dgvData.Rows.Count < 1) // Verifica si hay productos en el DataGridView.
            {
                bandera = false;
            }
            if (txtPaga.Texts == "" || txtPaga.Texts == "0.00") // Verifica si el campo de pago está vacío o es 0.00.
            {
                bandera = false;
            }

            return bandera; // Devuelve verdadero si todos los campos son válidos, falso si falta algo.
        }


        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            if (validaciones()) // Primero verifica si todas las validaciones pasan.
            {



                // Pide confirmación antes de registrar la venta.
                DialogResult confirmacion = MessageBox.Show("¿Seguro que desea registrar la venta?", "Confirmacion", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (confirmacion == DialogResult.Yes) // Si el usuario confirma que quiere registrar la venta.
                {


                

                    // Crea un DataTable para los detalles de la venta.
                    DataTable detalle_Venta = new DataTable();
                    detalle_Venta.Columns.Add("IdProducto", typeof(int));
                    detalle_Venta.Columns.Add("PrecioVenta", typeof(decimal));
                    detalle_Venta.Columns.Add("Cantidad", typeof(int));
                    detalle_Venta.Columns.Add("SubTotal", typeof(decimal));

                    // Llena el DataTable con los datos de cada fila en el DataGridView.
                    foreach (DataGridViewRow row in dgvData.Rows)
                    {
                        detalle_Venta.Rows.Add(
                            new Object[]
                            {
                        Convert.ToInt32(row.Cells["IdProducto"].Value.ToString()),
                        Convert.ToDecimal(row.Cells["Precio"].Value.ToString()),
                        Convert.ToInt32(row.Cells["Cantidad"].Value.ToString()),
                        Convert.ToDecimal(row.Cells["SubTotal"].Value.ToString())
                            }
                        );
                    }

                    // Obtiene el correlativo para el número de venta.
                    int idCorrelativo = new CN_Venta().obtenerCorrelativo();
                    string numeroDocumento = string.Format("{0:00000}", idCorrelativo);

                    // Calcula el cambio antes de registrar la venta.
                    calcularCambio();

                    int idCliente = 0;
                    int.TryParse(txtIdCliente.Text, out idCliente);
                    if (idCliente == 0 && !string.IsNullOrWhiteSpace(txtDocumento.Texts))
                    {
                        var clienteEncontrado = new CN_Cliente().Listar().FirstOrDefault(c => c.Documento == txtDocumento.Texts.Trim() && c.Estado);
                        if (clienteEncontrado != null)
                        {
                            idCliente = clienteEncontrado.IdCliente;
                            txtIdCliente.Text = idCliente.ToString();
                        }
                    }

                    // Crea una instancia de la clase Venta y asigna los datos.
                    Venta oVenta = new Venta()
                    {
                        oUsuario = new Usuario() { IdUsuario = usuarioActual.IdUsuario },
                        oCliente = new Cliente() { IdCliente = idCliente },
                        TipoDocumento = ((OpcionesCombo)comboDocumento.SelectedItem).Texto,
                        NumeroDocumento = numeroDocumento,
                        DocumentoCliente = txtDocumento.Texts,
                        NombreCliente = txtNombreC.Texts,
                        IdCupon = Convert.ToInt32(txtCupon.Text),
                        MontoPago = Convert.ToDecimal(txtPaga.Texts),
                        MontoCambio = Convert.ToDecimal(txtCambio.Texts),
                        MontoTotal = Convert.ToDecimal(txtTotal.Texts)
                    };
                    

                    // Llama al método para registrar la venta en la base de datos.
                    string Mensaje = string.Empty;
                    bool respuesta = new CN_Venta().Registrar(oVenta, detalle_Venta, out Mensaje);

                    if (respuesta) // Si la venta se registró correctamente.
                    {
                        // Muestra un mensaje con el número de venta generado y ofrece copiarlo al portapapeles.
                        var result = MessageBox.Show("Numero de Venta generada:\n" + numeroDocumento + "\n\n¿Desea ver el detalle de venta?", "Mensaje", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                        if (result == DialogResult.Yes)
                        {
                            Clipboard.SetText(numeroDocumento); // Copia el número de venta al portapapeles.
                            inicio.inicio(new FrmDetalleVenta(numeroDocumento, inicio));
                            txtIdCliente.Text = "0";
                            txtDocumento.Texts = "";
                            txtNombreC.Texts = "";
                            dgvData.Rows.Clear();
                            calcularTotal();
                            txtCupon.Text = "0";
                            txtPaga.Texts = "";
                            txtCambio.Texts = "";
                            this.Close();
                        }

                        // Limpia los campos después de registrar la venta.
                        txtIdCliente.Text = "0";
                        txtDocumento.Texts = "";
                        txtNombreC.Texts = "";
                        dgvData.Rows.Clear();
                        calcularTotal();
                        txtCupon.Text = "0";
                        txtPaga.Texts = "";
                        txtCambio.Texts = "";
                    }
                    else
                    {
                        MessageBox.Show(Mensaje, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); // Muestra un mensaje de error si la venta no se registró.
                    }
                }
            }
            else
            {
                // Si alguna validación falla, muestra un mensaje de error.
                MessageBox.Show("No se pudo registrar la compra", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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

        private void txtDocumento_KeyDown(object sender, KeyEventArgs e)
        {
            // Verifica si el usuario presiona la tecla Enter
            if (e.KeyData == Keys.Enter)
            {
                // Busca al cliente en la base de datos por su documento, y que esté activo (Estado == true)
                Cliente oCliente = new CN_Cliente().Listar()
                                     .Where(p => p.Documento == txtDocumento.Texts && p.Estado == true)
                                     .FirstOrDefault();

                // Si se encuentra un cliente válido
                if (oCliente != null)
                {
                    // Cambia el color del texto a "Honeydew" (verde suave) para indicar éxito
                    txtDocumento.ForeColor = Color.Honeydew;

                    // Asigna los datos del cliente a los campos correspondientes
                    txtIdCliente.Text = oCliente.IdCliente.ToString(); // Asigna el ID del cliente
                    txtNombreC.Texts = oCliente.NombreCompleto; // Asigna el nombre completo del cliente
                }
                else
                {
                    // Si no se encuentra un cliente, cambia el color del texto a "MistyRose" (rojo suave) para indicar error
                    txtDocumento.ForeColor = Color.MistyRose;

                    // Limpia los campos de cliente
                    txtIdCliente.Text = "0"; // El ID de cliente se resetea a 0
                    txtNombreC.Texts = ""; // El nombre completo del cliente se resetea a vacío
                }
            }
        }


    }
}
