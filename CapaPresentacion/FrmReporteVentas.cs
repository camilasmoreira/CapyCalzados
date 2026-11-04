using CapaEntidad;
using CapaNegocio;
using CapaPresentacion.Utilidades;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace CapaPresentacion
{
    public partial class FrmReporteVentas : Form
    {
        public Inicio Inicio { get; set; }
        public FrmReporteVentas(Inicio inicio)
        {
            InitializeComponent(); // Inicializa los componentes de la forma
            Inicio = inicio;
        }

        // Método que se ejecuta al cargar el formulario
        private void FrmReporteVentas_Load(object sender, EventArgs e)
        {
            TemaCapybara.AplicarTema(this);

            // Ocultar columnas innecesarias de cliente y agregar columna Forma de Pago
            if (dgvData.Columns.Contains("DocumentoCliente"))
                dgvData.Columns["DocumentoCliente"].Visible = false;
            if (dgvData.Columns.Contains("NombreCliente"))
                dgvData.Columns["NombreCliente"].Visible = false;

            if (!dgvData.Columns.Contains("FormaPago"))
            {
                DataGridViewTextBoxColumn colFormaPago = new DataGridViewTextBoxColumn();
                colFormaPago.Name = "FormaPago";
                colFormaPago.HeaderText = "Forma de Pago";
                colFormaPago.Width = 130;
                dgvData.Columns.Insert(3, colFormaPago);
            }

            // Llenar el combo de búsqueda con las columnas visibles del DataGridView
            foreach (DataGridViewColumn columna in dgvData.Columns)
            {
                if (columna.Visible && columna.Name != "btnDetalle")
                {
                    comboBusqueda.Items.Add(new OpcionesCombo() { Valor = columna.Name, Texto = columna.HeaderText });
                }
            }
            comboBusqueda.DisplayMember = "Texto"; // Asigna la propiedad para mostrar
            comboBusqueda.ValueMember = "Valor";   // Asigna la propiedad para el valor
            comboBusqueda.SelectedIndex = 0; // Selecciona el primer elemento
        }

        // Método que se ejecuta al hacer clic en el botón Exportar
        private void btnExportar_Click(object sender, EventArgs e)
        {
            // Verifica si hay datos para exportar
            if (dgvData.Rows.Count < 1)
            {
                MessageBox.Show("No hay datos para exportar", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                DataTable dt = new DataTable(); // Crea un DataTable para almacenar los datos

                // Agrega las columnas visibles al DataTable
                foreach (DataGridViewColumn colum in dgvData.Columns)
                {
                    if (colum.Visible && colum.Name != "btnDetalle")
                    {
                        dt.Columns.Add(colum.HeaderText, typeof(string));
                    }
                }

                // Agrega las filas al DataTable
                foreach (DataGridViewRow rows in dgvData.Rows)
                {
                    if (rows.Visible) // Solo agrega filas visibles
                    {
                        List<object> valores = new List<object>();
                        foreach (DataGridViewColumn colum in dgvData.Columns)
                        {
                            if (colum.Visible && colum.Name != "btnDetalle")
                            {
                                valores.Add(rows.Cells[colum.Index].Value?.ToString() ?? "");
                            }
                        }
                        dt.Rows.Add(valores.ToArray());
                    }
                }

                // Diálogo para guardar el archivo
                SaveFileDialog savefile = new SaveFileDialog
                {
                    FileName = string.Format("ReporteVentas_{0}.xlsx", DateTime.Now.ToString("ddMMyyyyHHmmss")), // Formato de nombre de archivo
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

        // Método que se ejecuta al hacer clic en el botón Buscar
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            List<ReporteVenta> lista = new List<ReporteVenta>(); // Lista para almacenar los reportes de ventas

            // Llama al método para obtener las ventas en el rango de fechas
            lista = new CN_Reporte().Venta(dtFechaInicio.Value, dtFechaFin.Value);

            dgvData.Rows.Clear(); // Limpia el DataGridView

            // Agrega los datos al DataGridView
            foreach (ReporteVenta rv in lista)
            {
                dgvData.Rows.Add(new object[]
                {
                    rv.FechaRegistro,
                    rv.TipoDocumento,
                    rv.NumeroDocumento,
                    string.IsNullOrEmpty(rv.FormaPago) ? "Efectivo" : rv.FormaPago,
                    rv.MontoTotal,
                    rv.UsuarioRegistro,
                    rv.DocumentoCliente,
                    rv.NombreCliente,
                    rv.CodigoProducto,
                    rv.NombreProducto,
                    rv.Categoria,
                    rv.PrecioVenta,
                    rv.Cantidad,
                    rv.SubTotal
                });
            }
        }

        // Método que se ejecuta al hacer clic en el botón de búsqueda
        private void btnBuscar2_Click(object sender, EventArgs e)
        {
            string columnaFiltro = ((OpcionesCombo)comboBusqueda.SelectedItem).Valor.ToString(); // Obtiene la columna seleccionada para filtrar

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

        // Método que se ejecuta al presionar una tecla en el campo de búsqueda
        private void txtBusqueda_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Verifica si el carácter es una tecla de control (como Backspace)
            bool esControl = Char.IsControl(e.KeyChar);

            // Verifica la longitud actual del texto y permite solo hasta 80 caracteres
            bool longitudPermitida = txtBusqueda.Texts.Length < 80;

            // Permite el carácter solo si es una tecla de control o si la longitud permitida no se ha alcanzado
            if (esControl || longitudPermitida)
            {
                e.Handled = false; // Permite el carácter
            }
            else
            {
                e.Handled = true; // Bloquea el carácter
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
                    Inicio.inicio(new FrmDetalleVenta(doc, Inicio));
                    this.Close();
                    
                }
            }
        }

        private void dgvData_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            TemaCapybara.ModernDgv_CellPainting(sender, e);
        }
    }
}
