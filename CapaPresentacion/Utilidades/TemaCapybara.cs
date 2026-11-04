using CustomControls.RJControls;
using FontAwesome.Sharp;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace CapaPresentacion.Utilidades
{
    /// <summary>
    /// Paleta y motor de estilos Capybara (Aesthetic Carpincho / Cuero Cálido Moderno).
    /// Proporciona un diseño contemporáneo, limpio y refinado con temática de zapatería/calzados.
    /// </summary>
    public static class TemaCapybara
    {
        // -------------------------------------------------------------
        // PALETA DE COLORES CAPYBARA MODERNO
        // -------------------------------------------------------------
        // Fondos estructurales
        public static readonly Color FondoLateral = Color.FromArgb(34, 24, 18);       // #221812 Menú lateral
        public static readonly Color FondoBarraTop = Color.FromArgb(44, 31, 24);      // #2C1F18 Barra superior
        public static readonly Color FondoFormulario = Color.FromArgb(48, 35, 27);    // #30231B Fondo general
        public static readonly Color ContenedorCard = Color.FromArgb(38, 27, 21);     // #261B15 Tarjeta principal
        public static readonly Color BordeCard = Color.FromArgb(85, 62, 48);          // #553E30 Borde sutil

        // Menú lateral
        public static readonly Color MenuTexto = Color.FromArgb(235, 222, 210);       // Beige claro
        public static readonly Color MenuIcono = Color.FromArgb(224, 155, 98);        // Canela dorado
        public static readonly Color MenuHover = Color.FromArgb(65, 46, 36);          // Marrón cálido hover
        public static readonly Color MenuSeleccionado = Color.FromArgb(82, 58, 45);   // Marrón activo

        // Agrupadores (Cards / Paneles interiores)
        public static readonly Color GroupBoxFondo = Color.FromArgb(44, 31, 24);      // #2C1F18
        public static readonly Color GroupBoxBorde = Color.FromArgb(78, 56, 43);      // #4E382B
        public static readonly Color BordeSuave = Color.FromArgb(88, 64, 49);         // #584031

        // Controles de entrada (Inputs, ComboBoxes, NumericUpDown)
        public static readonly Color InputFondo = Color.FromArgb(28, 20, 16);         // #1C1410 Exacto a campoDNI en Login
        public static readonly Color InputBorde = Color.FromArgb(90, 65, 48);         // #5A4130 Exacto a campoDNI en Login
        public static readonly Color InputBordeFocus = Color.FromArgb(217, 130, 43);  // #D9822B Naranja terracota Login
        public static readonly Color InputTexto = Color.FromArgb(250, 245, 238);      // Blanco crema nítido
        public static readonly Color InputPlaceholder = Color.FromArgb(150, 130, 115);// Beige neutro Login

        // Botones temáticos
        public static readonly Color BotonYuzu = Color.FromArgb(217, 130, 43);        // #D9822B Naranja cítrico / Guardar
        public static readonly Color BotonToffee = Color.FromArgb(92, 66, 50);        // #5C4232 Marrón toffee / Limpiar
        public static readonly Color BotonPasto = Color.FromArgb(82, 118, 68);        // #527644 Verde bosque / Excel
        public static readonly Color BotonTerracota = Color.FromArgb(168, 68, 54);    // #A84436 Terracota cálido / Eliminar
        public static readonly Color BotonTexto = Color.FromArgb(255, 250, 245);      // Blanco cálido

        // Tablas de datos (DataGridView)
        public static readonly Color GridFondo = Color.FromArgb(36, 26, 20);          // Fondo tabla
        public static readonly Color GridHeader = Color.FromArgb(58, 41, 31);         // Encabezado
        public static readonly Color GridHeaderTexto = Color.FromArgb(255, 248, 240);
        public static readonly Color GridFila = Color.FromArgb(42, 30, 23);           // Fila regular
        public static readonly Color GridFilaAlt = Color.FromArgb(48, 34, 26);        // Fila alterna
        public static readonly Color GridFilaSeleccion = Color.FromArgb(92, 65, 48);  // Fila seleccionada
        public static readonly Color GridLineas = Color.FromArgb(85, 62, 48);         // #553E30 Líneas divisorias nítidas

        // Textos
        public static readonly Color TextoPrincipal = Color.FromArgb(250, 245, 238);  // Blanco crema
        public static readonly Color TextoSecundario = Color.FromArgb(200, 180, 160); // Beige tostado
        public static readonly Color TextoDorado = Color.FromArgb(224, 168, 108);     // Dorado miel acento

        // Registro de manejadores de pintado de ComboBoxes (evita duplicidad y fugas de memoria)
        private static readonly ConditionalWeakTable<ComboBox, ComboBoxFlatPainter> _comboPainters = new ConditionalWeakTable<ComboBox, ComboBoxFlatPainter>();

        /// <summary>
        /// Aplica la paleta Capybara moderna de manera recursiva a todos los controles de un contenedor.
        /// </summary>
        public static void AplicarTema(Control parent)
        {
            if (parent == null) return;

            if (parent is Form frm)
            {
                if (frm.TransparencyKey == Color.Empty || frm.TransparencyKey != frm.BackColor)
                {
                    frm.BackColor = FondoFormulario;
                }
            }

            Control[] listaControles = new Control[parent.Controls.Count];
            parent.Controls.CopyTo(listaControles, 0);

            foreach (Control c in listaControles)
            {
                AplicarControl(c);

                if (c.HasChildren)
                {
                    AplicarTema(c);
                }
            }

            // 1. Enviar al fondo el banner decorativo de búsqueda (label10)
            foreach (Control c in parent.Controls)
            {
                if (c is Label lbl && lbl.Width > 200 && lbl.Height > 30 && string.IsNullOrWhiteSpace(lbl.Text))
                {
                    lbl.SendToBack();
                }
            }

            // 2. Enviar al fondo del todo el contenedor decorativo rjTextBox2
            foreach (Control c in parent.Controls)
            {
                if (c is RJTextBox rj && (rj.Name == "rjTextBox2" || (rj.Multiline && rj.Width > 500 && rj.Height > 300)))
                {
                    rj.SendToBack();
                }
            }

            // 3. Traer al frente todos los controles funcionales para que nunca queden tapados
            foreach (Control c in parent.Controls)
            {
                if (c is GroupBox || c is DataGridView || c is RJButton || c is ComboBox || c is Chart ||
                    (c is RJTextBox rjInput && rjInput.Name != "rjTextBox2" && (rjInput.Width <= 500 || rjInput.Height <= 300)) ||
                    (c is Label lblText && !string.IsNullOrWhiteSpace(lblText.Text)))
                {
                    c.BringToFront();
                }
            }

            // 3. Limpiar cualquier panel superpuesto residual si existiera
            for (int i = parent.Controls.Count - 1; i >= 0; i--)
            {
                Control c = parent.Controls[i];
                if (c.Name != null && (c.Name.StartsWith("pnlArrow_") || c.Name.StartsWith("pnlHeader_")))
                {
                    parent.Controls.RemoveAt(i);
                    c.Dispose();
                }
            }
        }

        private static void AplicarControl(Control c)
        {
            // Protección de branding
            if (c is Label lblBrand && (lblBrand.Name == "lblBrandTop" || lblBrand.Name == "lblBrandSubTop"))
            {
                if (lblBrand.Name == "lblBrandSubTop")
                    lblBrand.ForeColor = BotonYuzu;
                else
                    lblBrand.ForeColor = TextoPrincipal;
                return;
            }

            // Tabla moderna DataGridView
            if (c is DataGridView dgv)
            {
                dgv.BackgroundColor = GridFondo;
                dgv.GridColor = GridLineas;
                dgv.BorderStyle = BorderStyle.FixedSingle;
                dgv.CellBorderStyle = DataGridViewCellBorderStyle.Single;
                dgv.EnableHeadersVisualStyles = false;
                dgv.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
                dgv.RowHeadersVisible = false;
                dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
                dgv.ColumnHeadersHeight = 34;
                dgv.RowTemplate.Height = 32;

                // Encabezados
                dgv.ColumnHeadersDefaultCellStyle.BackColor = GridHeader;
                dgv.ColumnHeadersDefaultCellStyle.ForeColor = GridHeaderTexto;
                dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9.5f, FontStyle.Bold);
                dgv.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                dgv.ColumnHeadersDefaultCellStyle.SelectionBackColor = GridHeader;
                dgv.ColumnHeadersDefaultCellStyle.SelectionForeColor = GridHeaderTexto;

                // Filas normales
                dgv.DefaultCellStyle.BackColor = GridFila;
                dgv.DefaultCellStyle.ForeColor = TextoPrincipal;
                dgv.DefaultCellStyle.Font = new Font("Segoe UI", 9f, FontStyle.Regular);
                dgv.DefaultCellStyle.SelectionBackColor = GridFilaSeleccion;
                dgv.DefaultCellStyle.SelectionForeColor = Color.White;

                dgv.RowsDefaultCellStyle.BackColor = GridFila;
                dgv.RowsDefaultCellStyle.ForeColor = TextoPrincipal;
                dgv.RowsDefaultCellStyle.SelectionBackColor = GridFilaSeleccion;
                dgv.RowsDefaultCellStyle.SelectionForeColor = Color.White;

                // Filas alternadas
                dgv.AlternatingRowsDefaultCellStyle.BackColor = GridFilaAlt;
                dgv.AlternatingRowsDefaultCellStyle.ForeColor = TextoPrincipal;
                dgv.AlternatingRowsDefaultCellStyle.SelectionBackColor = GridFilaSeleccion;
                dgv.AlternatingRowsDefaultCellStyle.SelectionForeColor = Color.White;

                // Plantilla de fila base
                dgv.RowTemplate.DefaultCellStyle.BackColor = GridFila;
                dgv.RowTemplate.DefaultCellStyle.ForeColor = TextoPrincipal;
                dgv.RowTemplate.DefaultCellStyle.SelectionBackColor = GridFilaSeleccion;
                dgv.RowTemplate.DefaultCellStyle.SelectionForeColor = Color.White;

                // Limpiar estilos grises o negros heredados del diseñador en columnas individuales
                foreach (DataGridViewColumn col in dgv.Columns)
                {
                    col.DefaultCellStyle.BackColor = Color.Empty;
                    col.DefaultCellStyle.ForeColor = Color.Empty;
                    col.DefaultCellStyle.SelectionBackColor = Color.Empty;
                    col.DefaultCellStyle.SelectionForeColor = Color.Empty;
                }

                dgv.RowsAdded -= Dgv_RowsAdded;
                dgv.RowsAdded += Dgv_RowsAdded;

                dgv.CellPainting -= ModernDgv_CellPainting;
                dgv.CellPainting += ModernDgv_CellPainting;

                foreach (DataGridViewRow row in dgv.Rows)
                {
                    row.Height = 32;
                    row.DefaultCellStyle.BackColor = Color.Empty;
                    row.DefaultCellStyle.ForeColor = Color.Empty;
                }
                return;
            }

            // Tarjeta agrupador moderna (GroupBox)
            if (c is GroupBox gb)
            {
                gb.BackColor = GroupBoxFondo;
                gb.ForeColor = BotonYuzu;
                gb.Font = new Font("Segoe UI", 9.5f, FontStyle.Bold);
                return;
            }

            // Entradas de texto estilizadas
            if (c is RJTextBox rj)
            {
                if (rj.Name == "rjTextBox2" || (rj.Multiline && rj.Width > 500 && rj.Height > 300))
                {
                    rj.BackColor = ContenedorCard;
                    rj.BorderColor = BordeCard;
                    rj.BorderFocusColor = BordeCard;
                    rj.ForeColor = TextoPrincipal;
                    rj.BorderRadius = 14;
                    rj.BorderSize = 1;
                }
                else
                {
                    rj.BackColor = InputFondo;
                    rj.BorderColor = InputBorde;
                    rj.BorderFocusColor = InputBordeFocus;
                    rj.ForeColor = InputTexto;
                    rj.PlaceholderColor = InputPlaceholder;
                    rj.Font = new Font("Segoe UI", 9.5f, FontStyle.Regular);
                    rj.BorderRadius = 8;
                    rj.BorderSize = 1;
                }
                return;
            }

            // Botones modernos con iconos FontAwesome
            if (c is RJButton btn)
            {
                string nombre = (btn.Name ?? "").ToLower();
                string texto = (btn.Text ?? "").Trim().ToLower();
                Form frmParent = ObtenerFormularioContenedor(btn);
                string frmName = frmParent?.GetType().Name ?? "";

                // Métricas inicio (Btn1 a Btn6)
                if (nombre.StartsWith("btn") && nombre.Length == 4 && char.IsDigit(nombre[3]))
                {
                    btn.BackColor = ContenedorCard;
                    btn.BackgroundColor = ContenedorCard;
                    btn.BorderColor = BordeSuave;
                    btn.BorderRadius = 12;
                    btn.BorderSize = 1;
                    btn.TextColor = TextoPrincipal;
                    btn.ForeColor = TextoPrincipal;
                    btn.Cursor = Cursors.Hand;
                    return;
                }

                // Tarjetas métricas de FrmGraficos (Total Ventas y Total Compras)
                if (frmName == "FrmGraficos" && (nombre == "ventas" || nombre == "compras"))
                {
                    btn.BackColor = ContenedorCard;
                    btn.BackgroundColor = ContenedorCard;
                    btn.BorderColor = BordeCard;
                    btn.BorderRadius = 14;
                    btn.BorderSize = 1;
                    btn.TextColor = TextoPrincipal;
                    btn.ForeColor = TextoPrincipal;
                    btn.Font = new Font("Segoe UI", 11.5f, FontStyle.Bold);
                    if (nombre == "ventas")
                        btn.Image = IconChar.CartShopping.ToBitmap(BotonYuzu, 26);
                    else
                        btn.Image = IconChar.TruckFast.ToBitmap(MenuIcono, 26);
                    btn.TextImageRelation = TextImageRelation.ImageBeforeText;
                    btn.ImageAlign = ContentAlignment.MiddleLeft;
                    btn.TextAlign = ContentAlignment.MiddleCenter;
                    btn.Padding = new Padding(16, 0, 16, 0);
                    btn.Cursor = Cursors.Default;
                    return;
                }

                // Botón de filtro de fechas en FrmGraficos (rjButton1)
                if ((frmName == "FrmGraficos" || texto.Contains("graficar") || (btn.Parent != null && btn.Parent.Name == "groupBox1" && btn.Width < 80)) && nombre == "rjbutton1")
                {
                    btn.Text = "";
                    btn.Image = IconChar.CalendarDays.ToBitmap(BotonTexto, 22);
                    btn.BackColor = BotonYuzu;
                    btn.BackgroundColor = BotonYuzu;
                    btn.BorderColor = Color.Transparent;
                    btn.ImageAlign = ContentAlignment.MiddleCenter;
                    btn.BorderRadius = 10;
                    btn.BorderSize = 0;
                    btn.Cursor = Cursors.Hand;
                    return;
                }

                btn.BorderRadius = 8;
                btn.BorderSize = 0;
                btn.Cursor = Cursors.Hand;

                // 1. Botones compactos de búsqueda / limpiar filtro (<= 65px de ancho)
                if (btn.Width <= 65 && btn.Height <= 45)
                {
                    btn.Text = "";
                    btn.ImageAlign = ContentAlignment.MiddleCenter;

                    if (nombre.Contains("limpiar") || nombre.Contains("brush") || nombre == "rjbutton3" || nombre == "btnlimpiarbusqueda" ||
                        (frmName == "FrmProducto" && nombre == "rjbutton4") || (frmName == "FrmReporteVentas" && nombre == "rjButton5".ToLower()))
                    {
                        btn.BackColor = BotonToffee;
                        btn.BackgroundColor = BotonToffee;
                        btn.BorderColor = Color.Transparent;
                        btn.Image = IconChar.Eraser.ToBitmap(BotonTexto, 16);
                    }
                    else
                    {
                        btn.BackColor = BotonYuzu;
                        btn.BackgroundColor = BotonYuzu;
                        btn.BorderColor = Color.Transparent;
                        btn.Image = IconChar.MagnifyingGlass.ToBitmap(BotonTexto, 16);
                    }
                    return;
                }

                // 2. Reportes: Exportar y Buscar
                if (frmName.StartsWith("FrmReporte"))
                {
                    if (nombre == "btnguardar2" || nombre.Contains("exportar"))
                    {
                        btn.Text = "  Descargar Excel";
                        btn.Image = IconChar.FileExcel.ToBitmap(BotonTexto, 16);
                        btn.BackColor = BotonPasto;
                        btn.BackgroundColor = BotonPasto;
                        btn.BorderColor = Color.Transparent;
                        btn.TextColor = BotonTexto;
                        btn.ForeColor = BotonTexto;
                        btn.TextImageRelation = TextImageRelation.ImageBeforeText;
                        btn.ImageAlign = ContentAlignment.MiddleLeft;
                        btn.TextAlign = ContentAlignment.MiddleCenter;
                        btn.Font = new Font("Segoe UI", 9.5f, FontStyle.Bold);
                        btn.Padding = new Padding(12, 0, 12, 0);
                        return;
                    }
                    if (nombre == "rjbutton1" || nombre.Contains("buscar"))
                    {
                        btn.Text = "  Buscar";
                        btn.Image = IconChar.MagnifyingGlass.ToBitmap(BotonTexto, 16);
                        btn.BackColor = BotonYuzu;
                        btn.BackgroundColor = BotonYuzu;
                        btn.BorderColor = Color.Transparent;
                        btn.TextColor = BotonTexto;
                        btn.ForeColor = BotonTexto;
                        btn.TextImageRelation = TextImageRelation.ImageBeforeText;
                        btn.ImageAlign = ContentAlignment.MiddleLeft;
                        btn.TextAlign = ContentAlignment.MiddleCenter;
                        btn.Font = new Font("Segoe UI", 9.5f, FontStyle.Bold);
                        btn.Padding = new Padding(12, 0, 12, 0);
                        return;
                    }
                }

                // 3. Botones CRUD principales con Icono + Texto
                btn.TextImageRelation = TextImageRelation.ImageBeforeText;
                btn.ImageAlign = ContentAlignment.MiddleLeft;
                btn.TextAlign = ContentAlignment.MiddleCenter;
                btn.Font = new Font("Segoe UI", 9.5f, FontStyle.Bold);
                btn.Padding = new Padding(12, 0, 12, 0);

                if (texto.Contains("guardar") || texto.Contains("registrar") || texto.Contains("agregar") ||
                    nombre.Contains("guardar") || (nombre == "rjbutton1" && frmName != "FrmReporteVentas" && frmName != "FrmReporteCompra" && frmName != "FrmGraficos" && !texto.Contains("graficar")))
                {
                    btn.Text = "  Guardar";
                    btn.Image = IconChar.FloppyDisk.ToBitmap(BotonTexto, 16);
                    btn.BackColor = BotonYuzu;
                    btn.BackgroundColor = BotonYuzu;
                    btn.BorderColor = Color.Transparent;
                    btn.TextColor = BotonTexto;
                    btn.ForeColor = BotonTexto;
                }
                else if (texto.Contains("eliminar") || texto.Contains("borrar") || nombre.Contains("eliminar"))
                {
                    btn.Text = "  Eliminar";
                    btn.Image = IconChar.TrashCan.ToBitmap(BotonTexto, 16);
                    btn.BackColor = BotonTerracota;
                    btn.BackgroundColor = BotonTerracota;
                    btn.BorderColor = Color.Transparent;
                    btn.TextColor = BotonTexto;
                    btn.ForeColor = BotonTexto;
                }
                else if (texto.Contains("limpiar") || nombre.Contains("limpiar"))
                {
                    btn.Text = "  Limpiar";
                    btn.Image = IconChar.Broom.ToBitmap(BotonTexto, 16);
                    btn.BackColor = BotonToffee;
                    btn.BackgroundColor = BotonToffee;
                    btn.BorderColor = Color.Transparent;
                    btn.TextColor = BotonTexto;
                    btn.ForeColor = BotonTexto;
                }
                else if (texto.Contains("excel") || texto.Contains("descargar") || nombre.Contains("exportar"))
                {
                    btn.Text = "  Descargar Excel";
                    btn.Image = IconChar.FileExcel.ToBitmap(BotonTexto, 16);
                    btn.BackColor = BotonPasto;
                    btn.BackgroundColor = BotonPasto;
                    btn.BorderColor = Color.Transparent;
                    btn.TextColor = BotonTexto;
                    btn.ForeColor = BotonTexto;
                }
                else
                {
                    btn.BackColor = BotonToffee;
                    btn.BackgroundColor = BotonToffee;
                    btn.BorderColor = Color.Transparent;
                    btn.TextColor = BotonTexto;
                    btn.ForeColor = BotonTexto;
                }
                return;
            }

            // Desplegables modernos (ComboBox)
            if (c is ComboBox cb)
            {
                cb.BackColor = InputFondo;
                cb.ForeColor = InputTexto;
                cb.FlatStyle = FlatStyle.Flat;
                cb.DrawMode = DrawMode.OwnerDrawFixed;
                cb.ItemHeight = Math.Max(cb.Font.Height + 6, 24);
                cb.Font = new Font("Segoe UI", 9.5f, FontStyle.Regular);
                cb.DrawItem -= ComboBox_DrawItem;
                cb.DrawItem += ComboBox_DrawItem;

                // Pintar borde blanco uniforme de 1px y flecha terracota sin recortes
                if (!_comboPainters.TryGetValue(cb, out _))
                {
                    ComboBoxFlatPainter painter = new ComboBoxFlatPainter(cb);
                    _comboPainters.Add(cb, painter);
                }
                return;
            }

            if (c is NumericUpDown num)
            {
                num.BackColor = InputFondo;
                num.ForeColor = InputTexto;
                num.Font = new Font("Segoe UI", 9.5f, FontStyle.Regular);
                foreach (Control child in num.Controls)
                {
                    child.BackColor = InputFondo;
                    child.ForeColor = InputTexto;
                }
                return;
            }

            if (c is DateTimePicker dtp)
            {
                dtp.CalendarMonthBackground = InputFondo;
                dtp.CalendarTitleBackColor = BotonYuzu;
                dtp.CalendarTitleForeColor = Color.White;
                dtp.CalendarForeColor = InputTexto;
                dtp.CalendarTrailingForeColor = TextoSecundario;
                dtp.Font = new Font("Segoe UI", 9.5f, FontStyle.Regular);
                return;
            }

            if (c is TextBox txt)
            {
                if (txt.Visible)
                {
                    txt.BackColor = InputFondo;
                    txt.ForeColor = InputTexto;
                    txt.Font = new Font("Segoe UI", 9.5f, FontStyle.Regular);
                }
                return;
            }

            // Etiquetas descriptivas y banners de búsqueda
            if (c is Label lbl)
            {
                // Banners/Contenedores de búsqueda creados con Label sin texto
                if (lbl.Width > 200 && lbl.Height > 30 && string.IsNullOrWhiteSpace(lbl.Text))
                {
                    lbl.BackColor = GroupBoxFondo;
                    lbl.SendToBack();
                    lbl.Paint -= LabelPanel_Paint;
                    lbl.Paint += LabelPanel_Paint;
                    return;
                }

                Form frmParent = ObtenerFormularioContenedor(lbl);
                string frmName = frmParent?.GetType().Name ?? "";

                if (frmName == "mdGraficos")
                {
                    lbl.BackColor = ContenedorCard;
                }
                else if (lbl.Parent is GroupBox || (lbl.Location.Y >= 280 && lbl.Location.Y <= 350))
                {
                    lbl.BackColor = GroupBoxFondo;
                }
                else if (lbl.Parent != null && lbl.Parent.Name == "rjTextBox2")
                {
                    lbl.BackColor = ContenedorCard;
                }
                else
                {
                    lbl.BackColor = Color.Transparent;
                }

                string lblTxt = (lbl.Text ?? "").Trim().ToLower();
                string lblNom = (lbl.Name ?? "").ToLower();

                if (lblNom.Contains("titulo") || lblTxt.Contains("lista de") || lbl.Font.Size >= 12f)
                {
                    lbl.Font = new Font("Segoe UI", 10f, FontStyle.Bold);
                    lbl.ForeColor = TextoPrincipal;
                }
                else
                {
                    lbl.Font = new Font("Segoe UI", 9f, FontStyle.Bold);
                    lbl.ForeColor = BotonYuzu; // Naranja terracota cálido igual al Login
                }

                lbl.BringToFront();
                return;
            }

            if (c is IconButton ibtn)
            {
                ibtn.BackColor = BotonToffee;
                ibtn.ForeColor = BotonTexto;
                ibtn.IconColor = TextoDorado;
                ibtn.FlatAppearance.BorderColor = BordeCard;
                return;
            }

            if (c is Button btnRegular && !(c is RJButton))
            {
                string nombre = (btnRegular.Name ?? "").ToLower();
                if (nombre.Contains("salir") || btnRegular.Text.Trim() == "X")
                {
                    btnRegular.BackColor = BotonTerracota;
                    btnRegular.ForeColor = Color.White;
                    btnRegular.FlatStyle = FlatStyle.Flat;
                    btnRegular.FlatAppearance.BorderSize = 0;
                    btnRegular.FlatAppearance.MouseOverBackColor = Color.FromArgb(200, 75, 60);
                    btnRegular.Cursor = Cursors.Hand;
                    return;
                }
                btnRegular.BackColor = BotonToffee;
                btnRegular.ForeColor = BotonTexto;
                btnRegular.FlatStyle = FlatStyle.Flat;
                btnRegular.FlatAppearance.BorderSize = 0;
                btnRegular.Cursor = Cursors.Hand;
                return;
            }

            // Gráficos modernos (MSChart)
            if (c is Chart chart)
            {
                AplicarEstiloGrafico(chart);
                return;
            }
        }

        public static void AplicarEstiloGrafico(Chart chart)
        {
            if (chart == null) return;

            chart.BackColor = ContenedorCard; // #261B15 Fondo tarjeta cálida
            chart.BorderlineColor = BordeCard; // #553E30
            chart.BorderlineWidth = 1;
            chart.BorderlineDashStyle = ChartDashStyle.Solid;
            chart.BorderSkin.SkinStyle = BorderSkinStyle.None;
            chart.BorderSkin.BackColor = Color.Transparent;
            chart.BorderSkin.BorderColor = Color.Transparent;

            // Paleta de colores cálidos Capybara para series y sectores (donuts/barras)
            chart.Palette = ChartColorPalette.None;
            chart.PaletteCustomColors = new Color[]
            {
                BotonYuzu,                       // #D9822B Naranja cítrico
                BotonPasto,                      // #527644 Verde bosque
                TextoDorado,                     // #E0A86C Dorado miel
                BotonTerracota,                  // #A84436 Terracota cálido
                Color.FromArgb(184, 115, 51),    // Cobre cálido
                Color.FromArgb(92, 122, 138),    // Pizarra azulada suave
                Color.FromArgb(145, 95, 75)      // Cuero tostado
            };

            foreach (Title t in chart.Titles)
            {
                t.Font = new Font("Segoe UI", 10.5f, FontStyle.Bold);
                t.ForeColor = TextoPrincipal;
            }

            foreach (ChartArea ca in chart.ChartAreas)
            {
                ca.BackColor = InputFondo; // #1C1410 Área interior de gráfico profunda y elegante
                ca.BorderColor = BordeSuave;
                ca.BorderWidth = 1;
                ca.BorderDashStyle = ChartDashStyle.Solid;

                // Eje X
                ca.AxisX.LineColor = BordeSuave;
                ca.AxisX.LabelStyle.ForeColor = TextoSecundario;
                ca.AxisX.LabelStyle.Font = new Font("Segoe UI", 8.5f, FontStyle.Bold);
                ca.AxisX.MajorGrid.LineColor = Color.FromArgb(52, 38, 29); // Rejilla muy sutil y cálida
                ca.AxisX.MinorGrid.LineColor = Color.FromArgb(40, 28, 22);
                ca.AxisX.MajorTickMark.LineColor = BordeSuave;
                ca.AxisX.TitleForeColor = TextoDorado;

                // Eje Y
                ca.AxisY.LineColor = BordeSuave;
                ca.AxisY.LabelStyle.ForeColor = TextoSecundario;
                ca.AxisY.LabelStyle.Font = new Font("Segoe UI", 8.5f, FontStyle.Bold);
                ca.AxisY.MajorGrid.LineColor = Color.FromArgb(52, 38, 29);
                ca.AxisY.MinorGrid.LineColor = Color.FromArgb(40, 28, 22);
                ca.AxisY.MajorTickMark.LineColor = BordeSuave;
                ca.AxisY.TitleForeColor = TextoDorado;
            }

            foreach (Legend leg in chart.Legends)
            {
                leg.BackColor = ContenedorCard;
                leg.ForeColor = TextoPrincipal;
                leg.Font = new Font("Segoe UI", 9f, FontStyle.Regular);
                leg.TitleForeColor = TextoDorado;
                leg.TitleBackColor = ContenedorCard;
            }

            int colorIdx = 0;
            foreach (Series s in chart.Series)
            {
                s.Font = new Font("Segoe UI", 9f, FontStyle.Bold);
                s.LabelForeColor = TextoPrincipal;

                if (s.ChartType != SeriesChartType.Doughnut && s.ChartType != SeriesChartType.Pie)
                {
                    s.Color = chart.PaletteCustomColors[colorIdx % chart.PaletteCustomColors.Length];
                    colorIdx++;
                }
                else
                {
                    for (int p = 0; p < s.Points.Count; p++)
                    {
                        s.Points[p].Color = chart.PaletteCustomColors[p % chart.PaletteCustomColors.Length];
                        s.Points[p].LabelForeColor = TextoPrincipal;
                        s.Points[p].Font = new Font("Segoe UI", 8.5f, FontStyle.Bold);
                    }
                }
            }
        }

        private static void Dgv_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            if (sender is DataGridView dgv)
            {
                for (int i = 0; i < e.RowCount; i++)
                {
                    int idx = e.RowIndex + i;
                    if (idx < dgv.Rows.Count)
                    {
                        dgv.Rows[idx].Height = 32;
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene el formulario contenedor específico del control, incluso si está embebido en Inicio.
        /// </summary>
        public static Form ObtenerFormularioContenedor(Control c)
        {
            Control p = c;
            while (p != null)
            {
                if (p is Form f && f.GetType().Name != "Inicio")
                {
                    return f;
                }
                p = p.Parent;
            }
            return c?.FindForm();
        }

        public static void ModernDgv_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.Handled || e.RowIndex < 0) return;
            DataGridView dgv = sender as DataGridView;
            if (dgv == null) return;

            string colName = (dgv.Columns[e.ColumnIndex].Name ?? "").ToLower();
            string headerText = (dgv.Columns[e.ColumnIndex].HeaderText ?? "").Trim().ToLower();
            bool isButtonCol = dgv.Columns[e.ColumnIndex] is DataGridViewButtonColumn;
            Form frmParent = ObtenerFormularioContenedor(dgv);
            string frmName = frmParent?.GetType().Name ?? "";

            bool isSelected = (e.State & DataGridViewElementStates.Selected) == DataGridViewElementStates.Selected;
            Color rowBg = isSelected ? GridFilaSeleccion : ((e.RowIndex % 2 == 0) ? GridFila : GridFilaAlt);

            // 1. Columna Eliminar
            bool isDeleteCol = colName.Contains("eliminar") || colName.Contains("borrar") ||
                               headerText.Contains("eliminar") || headerText.Contains("borrar") ||
                               (frmName == "FrmCompras" && e.ColumnIndex == 7) ||
                               (frmName == "FrmVentas" && e.ColumnIndex == 6);

            if (isDeleteCol)
            {
                using (SolidBrush sb = new SolidBrush(rowBg))
                {
                    e.Graphics.FillRectangle(sb, e.CellBounds);
                }

                using (Pen pGrid = new Pen(GridLineas, 1f))
                {
                    e.Graphics.DrawRectangle(pGrid, e.CellBounds.Left - 1, e.CellBounds.Top - 1, e.CellBounds.Width, e.CellBounds.Height);
                }

                Image img = Properties.Resources.borrar_1_;
                if (img != null)
                {
                    int w = Math.Min(img.Width, 20);
                    int h = Math.Min(img.Height, 20);
                    int x = e.CellBounds.Left + (e.CellBounds.Width - w) / 2;
                    int y = e.CellBounds.Top + (e.CellBounds.Height - h) / 2;
                    e.Graphics.DrawImage(img, new Rectangle(x, y, w, h));
                }

                e.Handled = true;
                return;
            }

            // 2. Columna Selección
            bool isSelectCol = (!isDeleteCol && (
                                   (e.ColumnIndex == 0 && (colName.Contains("selec") || string.IsNullOrEmpty(headerText) || isButtonCol)) ||
                                   colName.Contains("btnseleccionar") ||
                                   (isButtonCol && string.IsNullOrEmpty(headerText)) ||
                                   (frmName == "FrmReporteCompra" && e.ColumnIndex == 14) ||
                                   (frmName == "FrmReporteVentas" && e.ColumnIndex == 13)
                               ));

            if (isSelectCol)
            {
                using (SolidBrush sb = new SolidBrush(rowBg))
                {
                    e.Graphics.FillRectangle(sb, e.CellBounds);
                }

                using (Pen pGrid = new Pen(GridLineas, 1f))
                {
                    e.Graphics.DrawRectangle(pGrid, e.CellBounds.Left - 1, e.CellBounds.Top - 1, e.CellBounds.Width, e.CellBounds.Height);
                }

                Image img = Properties.Resources.comprobado;
                if (img != null)
                {
                    int w = Math.Min(img.Width, 20);
                    int h = Math.Min(img.Height, 20);
                    int x = e.CellBounds.Left + (e.CellBounds.Width - w) / 2;
                    int y = e.CellBounds.Top + (e.CellBounds.Height - h) / 2;
                    e.Graphics.DrawImage(img, new Rectangle(x, y, w, h));
                }

                e.Handled = true;
                return;
            }

            // 3. Columna "Estado": Badge / Pill moderno
            if (colName.Equals("estado", StringComparison.OrdinalIgnoreCase) || headerText.Equals("estado", StringComparison.OrdinalIgnoreCase))
            {
                using (SolidBrush sb = new SolidBrush(rowBg))
                {
                    e.Graphics.FillRectangle(sb, e.CellBounds);
                }

                using (Pen pGrid = new Pen(GridLineas, 1f))
                {
                    e.Graphics.DrawRectangle(pGrid, e.CellBounds.Left - 1, e.CellBounds.Top - 1, e.CellBounds.Width, e.CellBounds.Height);
                }

                string val = (e.Value ?? "").ToString().Trim();
                bool isActivo = val.Equals("Activo", StringComparison.OrdinalIgnoreCase) || val == "1" || val.Equals("True", StringComparison.OrdinalIgnoreCase);

                Color pillBg = isActivo ? Color.FromArgb(30, 68, 42) : Color.FromArgb(75, 30, 32);
                Color pillBorder = isActivo ? Color.FromArgb(50, 125, 75) : Color.FromArgb(135, 55, 60);
                Color pillFg = isActivo ? Color.FromArgb(130, 235, 150) : Color.FromArgb(255, 160, 160);
                string pillText = isActivo ? "●  Activo" : "●  Inactivo";

                int pillW = 84;
                int pillH = 22;
                int px = e.CellBounds.Left + (e.CellBounds.Width - pillW) / 2;
                int py = e.CellBounds.Top + (e.CellBounds.Height - pillH) / 2;
                Rectangle pillRect = new Rectangle(px, py, pillW, pillH);

                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                using (GraphicsPath path = GetRoundedPath(pillRect, 10))
                using (SolidBrush bPill = new SolidBrush(pillBg))
                using (Pen pPill = new Pen(pillBorder, 1f))
                {
                    e.Graphics.FillPath(bPill, path);
                    e.Graphics.DrawPath(pPill, path);
                }

                using (Font fPill = new Font("Segoe UI", 8.5f, FontStyle.Bold))
                using (SolidBrush bFg = new SolidBrush(pillFg))
                using (StringFormat sf = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center })
                {
                    RectangleF pillRectF = new RectangleF(px, py, pillW, pillH);
                    e.Graphics.DrawString(pillText, fPill, bFg, pillRectF, sf);
                }

                e.Handled = true;
                return;
            }

            // 4. Todas las celdas regulares de datos (Documento, Nombre, Correo, Rol, etc.):
            // Garantiza el fondo cálido consistente del tema (nunca gris) y bordes nítidos
            using (SolidBrush sb = new SolidBrush(rowBg))
            {
                e.Graphics.FillRectangle(sb, e.CellBounds);
            }

            using (Pen pGrid = new Pen(GridLineas, 1f))
            {
                e.Graphics.DrawRectangle(pGrid, e.CellBounds.Left - 1, e.CellBounds.Top - 1, e.CellBounds.Width, e.CellBounds.Height);
            }

            if (e.Value != null)
            {
                string text = e.Value.ToString();
                using (SolidBrush bText = new SolidBrush(isSelected ? Color.White : TextoPrincipal))
                using (StringFormat sf = new StringFormat
                {
                    Alignment = StringAlignment.Near,
                    LineAlignment = StringAlignment.Center,
                    Trimming = StringTrimming.EllipsisCharacter,
                    FormatFlags = StringFormatFlags.NoWrap
                })
                {
                    Rectangle textRect = new Rectangle(e.CellBounds.Left + 8, e.CellBounds.Top, Math.Max(0, e.CellBounds.Width - 14), e.CellBounds.Height);
                    e.Graphics.DrawString(text, e.CellStyle.Font ?? dgv.Font, bText, textRect, sf);
                }
            }

            e.Handled = true;
            return;
        }

        /// <summary>
        /// Subclasifica ComboBox mediante NativeWindow para pintar de forma infalible:
        /// 1. Fondo de botón derecho oscuro uniforme (oculta botón nativo 3D).
        /// 2. Flecha chevron moderna en color BotonYuzu (#D9822B).
        /// 3. Borde perimetral nítido blanco/crema de 1px en los 4 lados sin recortes.
        /// </summary>
        public class ComboBoxFlatPainter : NativeWindow
        {
            private const int WM_PAINT = 0x000F;
            private readonly ComboBox _cb;

            public ComboBoxFlatPainter(ComboBox cb)
            {
                _cb = cb;
                if (cb.IsHandleCreated)
                {
                    AssignHandle(cb.Handle);
                }
                cb.HandleCreated += (s, e) => AssignHandle(cb.Handle);
                cb.HandleDestroyed += (s, e) => ReleaseHandle();
            }

            protected override void WndProc(ref Message m)
            {
                base.WndProc(ref m);

                if (m.Msg == WM_PAINT && _cb.IsHandleCreated && _cb.Visible && _cb.Width > 0 && _cb.Height > 0)
                {
                    PaintCustomAdornments();
                }
            }

            private void PaintCustomAdornments()
            {
                try
                {
                    using (Graphics g = Graphics.FromHwnd(_cb.Handle))
                    {
                        int cw = _cb.ClientSize.Width;
                        int ch = _cb.ClientSize.Height;
                        if (cw <= 0 || ch <= 0) return;

                        // 1. Cubrir el área del botón nativo de la derecha con el fondo oscuro
                        int btnW = Math.Max(21, SystemInformation.VerticalScrollBarWidth + 2);
                        Rectangle btnRect = new Rectangle(cw - btnW - 1, 1, btnW, ch - 2);
                        using (SolidBrush bBg = new SolidBrush(TemaCapybara.InputFondo))
                        {
                            g.FillRectangle(bBg, btnRect);
                        }

                        // 2. Dibujar flecha chevron centrada en el botón
                        g.SmoothingMode = SmoothingMode.AntiAlias;
                        int mx = btnRect.Left + btnRect.Width / 2;
                        int my = btnRect.Top + btnRect.Height / 2;
                        Point[] pts = new Point[] {
                            new Point(mx - 4, my - 2),
                            new Point(mx + 4, my - 2),
                            new Point(mx, my + 3)
                        };
                        using (SolidBrush bArrow = new SolidBrush(TemaCapybara.BotonYuzu))
                        {
                            g.FillPolygon(bArrow, pts);
                        }

                        // 3. Dibujar borde blanco/crema de 1px en los 4 lados exactos sin recortes
                        g.SmoothingMode = SmoothingMode.None;
                        Color borderColor = Color.FromArgb(240, 235, 230); // Blanco crema uniforme
                        using (Pen p = new Pen(borderColor, 1f))
                        {
                            g.DrawRectangle(p, 0, 0, cw - 1, ch - 1);
                        }
                    }
                }
                catch
                {
                    // Manejo seguro si el handle se invalida
                }
            }
        }

        private static void LabelPanel_Paint(object sender, PaintEventArgs e)
        {
            if (sender is Label lbl)
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                using (GraphicsPath path = GetRoundedPath(new Rectangle(0, 0, lbl.Width - 1, lbl.Height - 1), 8))
                using (Pen p = new Pen(BordeSuave, 1f))
                {
                    e.Graphics.DrawPath(p, path);
                }
            }
        }

        private static void ComboBox_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (!(sender is ComboBox cb)) return;
            if (e.Index < 0 || e.Index >= cb.Items.Count) return;

            // Solo debe resaltarse en naranja Yuzu si el desplegable está ABIERTO y el ítem está seleccionado/hover
            bool isDropdownHover = cb.DroppedDown
                && (e.State & DrawItemState.Selected) == DrawItemState.Selected
                && (e.State & DrawItemState.ComboBoxEdit) == 0;

            Color bg = isDropdownHover ? BotonYuzu : InputFondo;
            Color fg = isDropdownHover ? Color.White : InputTexto;

            using (SolidBrush brushBg = new SolidBrush(bg))
            {
                e.Graphics.FillRectangle(brushBg, e.Bounds);
            }

            string text = cb.GetItemText(cb.Items[e.Index]);
            if (string.IsNullOrEmpty(text) && cb.Items[e.Index] != null)
            {
                text = cb.Items[e.Index].ToString();
            }

            using (SolidBrush brushFg = new SolidBrush(fg))
            using (StringFormat sf = new StringFormat
            {
                LineAlignment = StringAlignment.Center,
                Alignment = StringAlignment.Near,
                Trimming = StringTrimming.EllipsisCharacter
            })
            {
                Rectangle textRect = new Rectangle(e.Bounds.X + 6, e.Bounds.Y, Math.Max(0, e.Bounds.Width - 10), e.Bounds.Height);
                e.Graphics.DrawString(text, cb.Font, brushFg, textRect, sf);
            }
        }

        public static GraphicsPath GetRoundedPath(Rectangle rect, int radius)
        {
            GraphicsPath path = new GraphicsPath();
            int d = radius * 2;
            path.AddArc(rect.X, rect.Y, d, d, 180, 90);
            path.AddArc(rect.Right - d, rect.Y, d, d, 270, 90);
            path.AddArc(rect.Right - d, rect.Bottom - d, d, d, 0, 90);
            path.AddArc(rect.X, rect.Bottom - d, d, d, 90, 90);
            path.CloseFigure();
            return path;
        }

        // -------------------------------------------------------------
        // RENDERIZADOR PROFESIONAL PARA MENÚS
        // -------------------------------------------------------------
        public class CapybaraRenderer : ToolStripProfessionalRenderer
        {
            public CapybaraRenderer() : base(new CapybaraColorTable()) { }

            protected override void OnRenderMenuItemBackground(ToolStripItemRenderEventArgs e)
            {
                Rectangle rc = new Rectangle(Point.Empty, e.Item.Size);
                if (e.Item.Selected)
                {
                    using (SolidBrush b = new SolidBrush(TemaCapybara.MenuHover))
                    {
                        e.Graphics.FillRectangle(b, rc);
                    }
                    if (e.Item.Owner is MenuStrip)
                    {
                        using (SolidBrush bIndicator = new SolidBrush(TemaCapybara.BotonYuzu))
                        {
                            e.Graphics.FillRectangle(bIndicator, 0, 0, 4, rc.Height);
                        }
                    }
                    else
                    {
                        using (Pen p = new Pen(TemaCapybara.BordeCard, 1))
                        {
                            e.Graphics.DrawRectangle(p, 0, 0, rc.Width - 1, rc.Height - 1);
                        }
                    }
                }
                else
                {
                    Color bg = e.Item.BackColor != Color.Transparent ? e.Item.BackColor : TemaCapybara.FondoLateral;
                    using (SolidBrush b = new SolidBrush(bg))
                    {
                        e.Graphics.FillRectangle(b, rc);
                    }
                }
            }
        }

        public class CapybaraColorTable : ProfessionalColorTable
        {
            public override Color ToolStripDropDownBackground => Color.FromArgb(30, 21, 16);
            public override Color ImageMarginGradientBegin => Color.FromArgb(30, 21, 16);
            public override Color ImageMarginGradientMiddle => Color.FromArgb(30, 21, 16);
            public override Color ImageMarginGradientEnd => Color.FromArgb(30, 21, 16);
            public override Color MenuBorder => Color.FromArgb(85, 62, 48);
            public override Color MenuItemBorder => Color.FromArgb(217, 130, 43);
            public override Color MenuItemSelected => Color.FromArgb(65, 46, 36);
            public override Color MenuStripGradientBegin => Color.FromArgb(44, 31, 24);
            public override Color MenuStripGradientEnd => Color.FromArgb(44, 31, 24);
            public override Color SeparatorDark => Color.FromArgb(88, 64, 49);
            public override Color SeparatorLight => Color.FromArgb(85, 62, 48);
        }
    }
}
