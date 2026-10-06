using CapaEntidad;
using CapaNegocio;
using CapaPresentacion.Utilidades;
using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace CapaPresentacion
{
    public partial class FrmReporteCompra : Form
    {
        public Inicio inicio {  get; set; }
        public FrmReporteCompra(Inicio inicio)
        {
            InitializeComponent(); // Inicializa los componentes de la forma
            this.inicio = inicio;
        }

        // Método que se ejecuta al cargar el formulario
        private void FrmReporteCompra_Load(object sender, EventArgs e)
        {
            // Mostrar Proveedores en el combo
            List<Proveedor> listaProveedores = new CN_Proveedor().Listar();

            // Agregar opción "Todos" al combo de proveedores
            comboProveedor.Items.Add(new OpcionesCombo() { Valor = 0, Texto = "Todos" });
            foreach (Proveedor item in listaProveedores)
            {
                comboProveedor.Items.Add(new OpcionesCombo() { Valor = item.IdProveedor, Texto = item.RazonSocial });
            }

            comboProveedor.DisplayMember = "Texto"; // Asigna la propiedad para mostrar
            comboProveedor.ValueMember = "Valor";   // Asigna la propiedad para el valor
            comboProveedor.SelectedIndex = 0; // Selecciona el primer elemento

            // Llenar el combo de búsqueda con las columnas visibles del DataGridView
            foreach (DataGridViewColumn columna in dgvData.Columns)
            {
                if (columna.Visible)
                {
                    comboBusqueda.Items.Add(new OpcionesCombo() { Valor = columna.Name, Texto = columna.HeaderText });
                }
            }

            comboBusqueda.DisplayMember = "Texto"; // Asigna la propiedad para mostrar
            comboBusqueda.ValueMember = "Valor";   // Asigna la propiedad para el valor
            comboBusqueda.SelectedIndex = 0; // Selecciona el primer elemento
        }

        // Método que se ejecuta al hacer clic en el botón Buscar
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            int idproveedor = Convert.ToInt32(((OpcionesCombo)comboProveedor.SelectedItem).Valor.ToString());

            List<ReporteCompra> lista = new List<ReporteCompra>();

            // Obtiene las compras en el rango de fechas y para el proveedor seleccionado
            lista = new CN_Reporte().Compra(dtFechaInicio.Value, dtFechaFin.Value, idproveedor);

            dgvData.Rows.Clear(); // Limpia el DataGridView

            // Agrega los datos de compras al DataGridView
            foreach (ReporteCompra rc in lista)
            {
                dgvData.Rows.Add(new object[]
                {
                    rc.FechaRegistro,
                    rc.TipoDocumento,
                    rc.NumeroDocumento,
                    rc.MontoTotal,
                    rc.UsuarioRegistro,
                    rc.DocumentoProveedor,
                    rc.RazonSocial,
                    rc.CodigoProducto,
                    rc.NombreProducto,
                    rc.Categoria,
                    rc.PrecioCompra,
                    rc.PrecioVenta,
                    rc.Cantidad,
                    rc.SubTotal
                });
            }
        }

        // Método que se ejecuta al hacer clic en el botón de búsqueda
        private void btnBuscar2_Click(object sender, EventArgs e)
        {
            string columnaFiltro = ((OpcionesCombo)comboBusqueda.SelectedItem).Valor.ToString(); // Obtiene la columna seleccionada

            if (dgvData.Rows.Count > 0) // Verifica que haya filas en el DataGridView
            {
                // Filtra las filas según el texto ingresado
                foreach (DataGridViewRow row in dgvData.Rows)
                {
                    if (row.Cells[columnaFiltro].Value.ToString().Trim().ToUpper().Contains(txtBusqueda.Texts.Trim().ToUpper()))
                    {
                        row.Visible = true; // Muestra la fila si coincide
                    }
                    else
                    {
                        row.Visible = false; // Oculta la fila si no coincide
                    }
                }
            }
        }

        // Método que se ejecuta al hacer clic en el botón de limpiar búsqueda
        private void btnLimpiarBusqueda_Click(object sender, EventArgs e)
        {
            txtBusqueda.Texts = ""; // Limpia el campo de búsqueda
            foreach (DataGridViewRow row in dgvData.Rows)
            {
                row.Visible = true; // Muestra todas las filas
            }
        }

        // Método que se ejecuta al hacer clic en el botón Exportar
        private void btnExportar_Click(object sender, EventArgs e)
        {
            if (dgvData.Rows.Count < 1)
            {
                MessageBox.Show("No hay datos para exportar", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                DataTable dt = new DataTable(); // Crea un DataTable para almacenar los datos
                foreach (DataGridViewColumn colum in dgvData.Columns)
                {
                    dt.Columns.Add(colum.HeaderText, typeof(string)); // Agrega las columnas al DataTable
                }

                // Agrega las filas al DataTable
                foreach (DataGridViewRow rows in dgvData.Rows)
                {
                    if (rows.Visible) // Solo agrega filas visibles
                    {
                        dt.Rows.Add(new object[]
                        {
                            rows.Cells[0].Value.ToString(),
                            rows.Cells[1].Value.ToString(),
                            rows.Cells[2].Value.ToString(),
                            rows.Cells[3].Value.ToString(),
                            rows.Cells[4].Value.ToString(),
                            rows.Cells[5].Value.ToString(),
                            rows.Cells[6].Value.ToString(),
                            rows.Cells[7].Value.ToString(),
                            rows.Cells[8].Value.ToString(),
                            rows.Cells[9].Value.ToString(),
                            rows.Cells[10].Value.ToString(),
                            rows.Cells[11].Value.ToString(),
                            rows.Cells[12].Value.ToString(),
                            rows.Cells[13].Value.ToString(),
                        });
                    }
                }

                // Diálogo para guardar el archivo
                SaveFileDialog savefile = new SaveFileDialog
                {
                    FileName = string.Format("ReporteCompras_{0}.xlsx", DateTime.Now.ToString("ddMMyyyyHHmmss")), // Formato de nombre de archivo
                    Filter = "Excel Files | *.xlsx" // Filtro para el tipo de archivo
                };

                if (savefile.ShowDialog() == DialogResult.OK) // Si el usuario confirma el guardado
                {
                    try
                    {
                        XLWorkbook wb = new XLWorkbook(); // Crea un nuevo libro de Excel
                        var hoja = wb.Worksheets.Add(dt, "Informe"); // Agrega una nueva hoja con los datos
                        hoja.ColumnsUsed().AdjustToContents(); // Ajusta el tamaño de las columnas
                        wb.SaveAs(savefile.FileName); // Guarda el archivo
                        MessageBox.Show("Reporte Generado", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information); // Mensaje de éxito
                    }
                    catch
                    {
                        MessageBox.Show("Error al generar el informe", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); // Mensaje de error
                    }
                }
            }
        }

        // Método que se ejecuta al hacer clic en un botón para guardar
        private void BtnGuardar2_Click(object sender, EventArgs e)
        {
            // Similar al método btnExportar_Click, pero puedes personalizar la lógica según sea necesario
            if (dgvData.Rows.Count < 1)
            {
                MessageBox.Show("No hay datos para exportar", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                DataTable dt = new DataTable();
                foreach (DataGridViewColumn colum in dgvData.Columns)
                {
                    dt.Columns.Add(colum.HeaderText, typeof(string));
                }

                foreach (DataGridViewRow rows in dgvData.Rows)
                {
                    if (rows.Visible)
                    {
                        dt.Rows.Add(new object[]
                        {
                            rows.Cells[0].Value.ToString(),
                            rows.Cells[1].Value.ToString(),
                            rows.Cells[2].Value.ToString(),
                            rows.Cells[3].Value.ToString(),
                            rows.Cells[4].Value.ToString(),
                            rows.Cells[5].Value.ToString(),
                            rows.Cells[6].Value.ToString(),
                            rows.Cells[7].Value.ToString(),
                            rows.Cells[8].Value.ToString(),
                            rows.Cells[9].Value.ToString(),
                            rows.Cells[10].Value.ToString(),
                            rows.Cells[11].Value.ToString(),
                            rows.Cells[12].Value.ToString(),
                            rows.Cells[13].Value.ToString(),
                        });
                    }
                }
                SaveFileDialog savefile = new SaveFileDialog();
                savefile.FileName = string.Format("ReporteCompras_{0}.xlsx", DateTime.Now.ToString("ddMMyyyyHHmmss"));
                savefile.Filter = "Excel Files | *.xlsx";

                if (savefile.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        XLWorkbook wb = new XLWorkbook();
                        var hoja = wb.Worksheets.Add(dt, "Informe");
                        hoja.ColumnsUsed().AdjustToContents();
                        wb.SaveAs(savefile.FileName);
                        MessageBox.Show("Reporte Generado", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch
                    {
                        MessageBox.Show("Error al generar el informe", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
            }
        }

        // Método que se ejecuta al presionar una tecla en el campo de búsqueda
        private void txtBusqueda_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Verifica si el carácter es una tecla de control (como Backspace)
            bool esControl = Char.IsControl(e.KeyChar);

            // Verifica la longitud actual del texto y permite solo hasta 80 caracteres
            bool longitudPermitida = txtBusqueda.Texts.Length < 80;

            // Permitir el carácter solo si es una tecla de control o si la longitud permitida no se ha alcanzado
            if (esControl || longitudPermitida)
            {
                e.Handled = false; // Permitir el carácter
            }
            else
            {
                e.Handled = true; // Bloquear el carácter
            }
        }

        private void dgvData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Comprueba si se ha hecho clic en el botón de selección
            if (dgvData.Columns[e.ColumnIndex].Name == "btnDetalle")
            {
                int indice = e.RowIndex;

                if (indice >= 0)
                {
                    // Rellena los campos del formulario con los datos del producto seleccionado
                    string doc = dgvData.Rows[indice].Cells["NumeroDocumento"].Value.ToString();
                    inicio.inicio(new FrmDetalleCompra(doc, inicio));
                    this.Close();

                }
            }
        }

        private void dgvData_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            // Evita la ejecución para el encabezado del DataGridView.
            if (e.RowIndex < 0)
            {
                return;
            }
            // Verifica si la columna es la de selección.
            if (e.ColumnIndex == 14)
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All); // Dibuja la celda.
                var w = Properties.Resources.comprobado.Width - 15; // Ancho del icono.
                var h = Properties.Resources.comprobado.Height - 15; // Alto del icono.

                // Calcula la posición para centrar el icono.
                var x = e.CellBounds.Left + (e.CellBounds.Width - w) / 2;
                var y = e.CellBounds.Top + (e.CellBounds.Height - h) / 2;

                e.Graphics.DrawImage(Properties.Resources.comprobado, new Rectangle(x, y, w, h)); // Dibuja el icono.
                e.Handled = true; // Marca la celda como manejada.
            }
        }
    }
}
