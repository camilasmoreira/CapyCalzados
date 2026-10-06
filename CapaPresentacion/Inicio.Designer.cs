namespace CapaPresentacion
{
    partial class Inicio
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.nombreUser = new System.Windows.Forms.Label();
            this.panelLateral = new System.Windows.Forms.Panel();
            this.menuLateral = new System.Windows.Forms.MenuStrip();
            this.menuInicio = new FontAwesome.Sharp.IconMenuItem();
            this.menuVentas = new FontAwesome.Sharp.IconMenuItem();
            this.menuRegistrarVenta = new FontAwesome.Sharp.IconMenuItem();
            this.menuVerDetalle = new FontAwesome.Sharp.IconMenuItem();
            this.menuCompras = new FontAwesome.Sharp.IconMenuItem();
            this.menuRegistrarCompra = new FontAwesome.Sharp.IconMenuItem();
            this.menuVerDetalleCompra = new FontAwesome.Sharp.IconMenuItem();
            this.menuClientes = new FontAwesome.Sharp.IconMenuItem();
            this.menuMantenimiento = new FontAwesome.Sharp.IconMenuItem();
            this.Productos = new FontAwesome.Sharp.IconMenuItem();
            this.Categorias = new FontAwesome.Sharp.IconMenuItem();
            this.Negocio = new FontAwesome.Sharp.IconMenuItem();
            this.menuReportes = new FontAwesome.Sharp.IconMenuItem();
            this.subMenuRVentas = new System.Windows.Forms.ToolStripMenuItem();
            this.subMenuRCompras = new System.Windows.Forms.ToolStripMenuItem();
            this.menuProveedores = new FontAwesome.Sharp.IconMenuItem();
            this.menuUsuarios = new FontAwesome.Sharp.IconMenuItem();
            this.menuGraficos = new FontAwesome.Sharp.IconMenuItem();
            this.lblRol = new System.Windows.Forms.Label();
            this.BarraTop = new System.Windows.Forms.Panel();
            this.lblIndicador = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.menuTitulo = new System.Windows.Forms.MenuStrip();
            this.iconMenuItem9 = new FontAwesome.Sharp.IconMenuItem();
            this.Contenedor = new System.Windows.Forms.Panel();
            this.Backup = new FontAwesome.Sharp.IconMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panelLateral.SuspendLayout();
            this.menuLateral.SuspendLayout();
            this.BarraTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.menuTitulo.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::CapaPresentacion.Properties.Resources.Logo;
            this.pictureBox1.Location = new System.Drawing.Point(-43, -32);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(296, 196);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 4;
            this.pictureBox1.TabStop = false;
            // 
            // nombreUser
            // 
            this.nombreUser.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.nombreUser.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(71)))), ((int)(((byte)(79)))));
            this.nombreUser.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nombreUser.ForeColor = System.Drawing.Color.White;
            this.nombreUser.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.nombreUser.Location = new System.Drawing.Point(916, 15);
            this.nombreUser.Name = "nombreUser";
            this.nombreUser.Size = new System.Drawing.Size(136, 31);
            this.nombreUser.TabIndex = 3;
            this.nombreUser.Text = "Juan Jose Perez";
            this.nombreUser.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panelLateral
            // 
            this.panelLateral.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(50)))), ((int)(((byte)(56)))));
            this.panelLateral.Controls.Add(this.menuLateral);
            this.panelLateral.Location = new System.Drawing.Point(0, 0);
            this.panelLateral.Name = "panelLateral";
            this.panelLateral.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.panelLateral.Size = new System.Drawing.Size(203, 699);
            this.panelLateral.TabIndex = 5;
            // 
            // menuLateral
            // 
            this.menuLateral.AutoSize = false;
            this.menuLateral.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(50)))), ((int)(((byte)(56)))));
            this.menuLateral.Dock = System.Windows.Forms.DockStyle.None;
            this.menuLateral.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.menuLateral.GripMargin = new System.Windows.Forms.Padding(2);
            this.menuLateral.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuInicio,
            this.menuVentas,
            this.menuCompras,
            this.menuClientes,
            this.menuMantenimiento,
            this.menuReportes,
            this.menuProveedores,
            this.menuUsuarios,
            this.menuGraficos});
            this.menuLateral.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.VerticalStackWithOverflow;
            this.menuLateral.Location = new System.Drawing.Point(0, 131);
            this.menuLateral.Name = "menuLateral";
            this.menuLateral.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.menuLateral.Size = new System.Drawing.Size(203, 533);
            this.menuLateral.TabIndex = 0;
            this.menuLateral.Text = "menuLateral";
            // 
            // menuInicio
            // 
            this.menuInicio.AutoSize = false;
            this.menuInicio.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(50)))), ((int)(((byte)(56)))));
            this.menuInicio.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.menuInicio.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(179)))), ((int)(((byte)(179)))), ((int)(((byte)(179)))));
            this.menuInicio.IconChar = FontAwesome.Sharp.IconChar.HomeUser;
            this.menuInicio.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(179)))), ((int)(((byte)(179)))), ((int)(((byte)(179)))));
            this.menuInicio.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.menuInicio.IconSize = 35;
            this.menuInicio.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.menuInicio.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.menuInicio.Margin = new System.Windows.Forms.Padding(0, 0, 0, 4);
            this.menuInicio.Name = "menuInicio";
            this.menuInicio.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.menuInicio.Size = new System.Drawing.Size(196, 50);
            this.menuInicio.Text = "Inicio";
            this.menuInicio.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay;
            this.menuInicio.Click += new System.EventHandler(this.menuInicio_Click);
            // 
            // menuVentas
            // 
            this.menuVentas.AutoSize = false;
            this.menuVentas.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(50)))), ((int)(((byte)(56)))));
            this.menuVentas.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuRegistrarVenta,
            this.menuVerDetalle});
            this.menuVentas.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.menuVentas.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(179)))), ((int)(((byte)(179)))), ((int)(((byte)(179)))));
            this.menuVentas.IconChar = FontAwesome.Sharp.IconChar.Tags;
            this.menuVentas.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(179)))), ((int)(((byte)(179)))), ((int)(((byte)(179)))));
            this.menuVentas.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.menuVentas.IconSize = 35;
            this.menuVentas.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.menuVentas.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.menuVentas.Margin = new System.Windows.Forms.Padding(0, 5, 0, 4);
            this.menuVentas.Name = "menuVentas";
            this.menuVentas.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.menuVentas.Size = new System.Drawing.Size(196, 50);
            this.menuVentas.Text = "Ventas";
            this.menuVentas.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay;
            // 
            // menuRegistrarVenta
            // 
            this.menuRegistrarVenta.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(50)))), ((int)(((byte)(56)))));
            this.menuRegistrarVenta.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(179)))), ((int)(((byte)(179)))), ((int)(((byte)(179)))));
            this.menuRegistrarVenta.IconChar = FontAwesome.Sharp.IconChar.None;
            this.menuRegistrarVenta.IconColor = System.Drawing.Color.Black;
            this.menuRegistrarVenta.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.menuRegistrarVenta.ImageTransparentColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(50)))), ((int)(((byte)(56)))));
            this.menuRegistrarVenta.Name = "menuRegistrarVenta";
            this.menuRegistrarVenta.Size = new System.Drawing.Size(208, 28);
            this.menuRegistrarVenta.Text = "Registrar Venta";
            this.menuRegistrarVenta.Click += new System.EventHandler(this.menuRegistrarVenta_Click);
            // 
            // menuVerDetalle
            // 
            this.menuVerDetalle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(50)))), ((int)(((byte)(56)))));
            this.menuVerDetalle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(179)))), ((int)(((byte)(179)))), ((int)(((byte)(179)))));
            this.menuVerDetalle.IconChar = FontAwesome.Sharp.IconChar.None;
            this.menuVerDetalle.IconColor = System.Drawing.Color.Black;
            this.menuVerDetalle.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.menuVerDetalle.ImageTransparentColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(50)))), ((int)(((byte)(56)))));
            this.menuVerDetalle.Name = "menuVerDetalle";
            this.menuVerDetalle.Size = new System.Drawing.Size(208, 28);
            this.menuVerDetalle.Text = "Ver Detalle";
            this.menuVerDetalle.Click += new System.EventHandler(this.menuVerDetalle_Click);
            // 
            // menuCompras
            // 
            this.menuCompras.AutoSize = false;
            this.menuCompras.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(50)))), ((int)(((byte)(56)))));
            this.menuCompras.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuRegistrarCompra,
            this.menuVerDetalleCompra});
            this.menuCompras.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.menuCompras.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(179)))), ((int)(((byte)(179)))), ((int)(((byte)(179)))));
            this.menuCompras.IconChar = FontAwesome.Sharp.IconChar.Shopify;
            this.menuCompras.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(179)))), ((int)(((byte)(179)))), ((int)(((byte)(179)))));
            this.menuCompras.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.menuCompras.IconSize = 35;
            this.menuCompras.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.menuCompras.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.menuCompras.Margin = new System.Windows.Forms.Padding(0, 5, 0, 4);
            this.menuCompras.Name = "menuCompras";
            this.menuCompras.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.menuCompras.Size = new System.Drawing.Size(196, 50);
            this.menuCompras.Text = "Compras";
            this.menuCompras.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay;
            // 
            // menuRegistrarCompra
            // 
            this.menuRegistrarCompra.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(50)))), ((int)(((byte)(56)))));
            this.menuRegistrarCompra.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(179)))), ((int)(((byte)(179)))), ((int)(((byte)(179)))));
            this.menuRegistrarCompra.IconChar = FontAwesome.Sharp.IconChar.None;
            this.menuRegistrarCompra.IconColor = System.Drawing.Color.Black;
            this.menuRegistrarCompra.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.menuRegistrarCompra.Name = "menuRegistrarCompra";
            this.menuRegistrarCompra.Size = new System.Drawing.Size(244, 28);
            this.menuRegistrarCompra.Text = "Registrar Compra";
            this.menuRegistrarCompra.Click += new System.EventHandler(this.menuRegistrarCompra_Click);
            // 
            // menuVerDetalleCompra
            // 
            this.menuVerDetalleCompra.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(50)))), ((int)(((byte)(56)))));
            this.menuVerDetalleCompra.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(179)))), ((int)(((byte)(179)))), ((int)(((byte)(179)))));
            this.menuVerDetalleCompra.IconChar = FontAwesome.Sharp.IconChar.None;
            this.menuVerDetalleCompra.IconColor = System.Drawing.Color.Black;
            this.menuVerDetalleCompra.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.menuVerDetalleCompra.Name = "menuVerDetalleCompra";
            this.menuVerDetalleCompra.Size = new System.Drawing.Size(244, 28);
            this.menuVerDetalleCompra.Text = "Ver Detalle Compra";
            this.menuVerDetalleCompra.Click += new System.EventHandler(this.menuVerDetalleCompra_Click);
            // 
            // menuClientes
            // 
            this.menuClientes.AutoSize = false;
            this.menuClientes.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(50)))), ((int)(((byte)(56)))));
            this.menuClientes.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.menuClientes.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(179)))), ((int)(((byte)(179)))), ((int)(((byte)(179)))));
            this.menuClientes.IconChar = FontAwesome.Sharp.IconChar.Shopware;
            this.menuClientes.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(179)))), ((int)(((byte)(179)))), ((int)(((byte)(179)))));
            this.menuClientes.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.menuClientes.IconSize = 35;
            this.menuClientes.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.menuClientes.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.menuClientes.Margin = new System.Windows.Forms.Padding(0, 5, 0, 4);
            this.menuClientes.Name = "menuClientes";
            this.menuClientes.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.menuClientes.Size = new System.Drawing.Size(196, 50);
            this.menuClientes.Text = "Clientes";
            this.menuClientes.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay;
            this.menuClientes.Click += new System.EventHandler(this.menuClientes_Click);
            // 
            // menuMantenimiento
            // 
            this.menuMantenimiento.AutoSize = false;
            this.menuMantenimiento.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(50)))), ((int)(((byte)(56)))));
            this.menuMantenimiento.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Productos,
            this.Categorias,
            this.Negocio,
            this.Backup});
            this.menuMantenimiento.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.menuMantenimiento.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(179)))), ((int)(((byte)(179)))), ((int)(((byte)(179)))));
            this.menuMantenimiento.IconChar = FontAwesome.Sharp.IconChar.Wrench;
            this.menuMantenimiento.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(179)))), ((int)(((byte)(179)))), ((int)(((byte)(179)))));
            this.menuMantenimiento.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.menuMantenimiento.IconSize = 35;
            this.menuMantenimiento.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.menuMantenimiento.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.menuMantenimiento.Margin = new System.Windows.Forms.Padding(0, 5, 0, 4);
            this.menuMantenimiento.Name = "menuMantenimiento";
            this.menuMantenimiento.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.menuMantenimiento.Size = new System.Drawing.Size(196, 50);
            this.menuMantenimiento.Text = "Mantenimiento";
            this.menuMantenimiento.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay;
            this.menuMantenimiento.Click += new System.EventHandler(this.menuMantenimiento_Click);
            // 
            // Productos
            // 
            this.Productos.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(50)))), ((int)(((byte)(56)))));
            this.Productos.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(179)))), ((int)(((byte)(179)))), ((int)(((byte)(179)))));
            this.Productos.IconChar = FontAwesome.Sharp.IconChar.None;
            this.Productos.IconColor = System.Drawing.Color.Black;
            this.Productos.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.Productos.Name = "Productos";
            this.Productos.Size = new System.Drawing.Size(180, 28);
            this.Productos.Text = "Productos";
            this.Productos.Click += new System.EventHandler(this.Productos_Click);
            // 
            // Categorias
            // 
            this.Categorias.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(50)))), ((int)(((byte)(56)))));
            this.Categorias.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(179)))), ((int)(((byte)(179)))), ((int)(((byte)(179)))));
            this.Categorias.IconChar = FontAwesome.Sharp.IconChar.None;
            this.Categorias.IconColor = System.Drawing.Color.Black;
            this.Categorias.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.Categorias.Name = "Categorias";
            this.Categorias.Size = new System.Drawing.Size(180, 28);
            this.Categorias.Text = "Categorias";
            this.Categorias.Click += new System.EventHandler(this.Categorias_Click);
            // 
            // Negocio
            // 
            this.Negocio.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(50)))), ((int)(((byte)(56)))));
            this.Negocio.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(179)))), ((int)(((byte)(179)))), ((int)(((byte)(179)))));
            this.Negocio.IconChar = FontAwesome.Sharp.IconChar.None;
            this.Negocio.IconColor = System.Drawing.Color.Black;
            this.Negocio.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.Negocio.Name = "Negocio";
            this.Negocio.Size = new System.Drawing.Size(180, 28);
            this.Negocio.Text = "Negocio";
            this.Negocio.Click += new System.EventHandler(this.Negocio_Click);
            // menuReportes
            // 
            this.menuReportes.AutoSize = false;
            this.menuReportes.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(50)))), ((int)(((byte)(56)))));
            this.menuReportes.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.subMenuRVentas,
            this.subMenuRCompras});
            this.menuReportes.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.menuReportes.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(179)))), ((int)(((byte)(179)))), ((int)(((byte)(179)))));
            this.menuReportes.IconChar = FontAwesome.Sharp.IconChar.Receipt;
            this.menuReportes.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(179)))), ((int)(((byte)(179)))), ((int)(((byte)(179)))));
            this.menuReportes.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.menuReportes.IconSize = 35;
            this.menuReportes.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.menuReportes.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.menuReportes.Margin = new System.Windows.Forms.Padding(0, 5, 0, 4);
            this.menuReportes.Name = "menuReportes";
            this.menuReportes.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.menuReportes.Size = new System.Drawing.Size(196, 50);
            this.menuReportes.Text = "Reportes";
            this.menuReportes.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay;
            // 
            // subMenuRVentas
            // 
            this.subMenuRVentas.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(50)))), ((int)(((byte)(56)))));
            this.subMenuRVentas.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(179)))), ((int)(((byte)(179)))), ((int)(((byte)(179)))));
            this.subMenuRVentas.Name = "subMenuRVentas";
            this.subMenuRVentas.Size = new System.Drawing.Size(228, 28);
            this.subMenuRVentas.Text = "Reporte Ventas";
            this.subMenuRVentas.Click += new System.EventHandler(this.subMenuRVentas_Click);
            // 
            // subMenuRCompras
            // 
            this.subMenuRCompras.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(50)))), ((int)(((byte)(56)))));
            this.subMenuRCompras.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(179)))), ((int)(((byte)(179)))), ((int)(((byte)(179)))));
            this.subMenuRCompras.Name = "subMenuRCompras";
            this.subMenuRCompras.Size = new System.Drawing.Size(228, 28);
            this.subMenuRCompras.Text = "Reporte Compras";
            this.subMenuRCompras.Click += new System.EventHandler(this.subMenuRCompras_Click);
            // 
            // menuProveedores
            // 
            this.menuProveedores.AutoSize = false;
            this.menuProveedores.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(50)))), ((int)(((byte)(56)))));
            this.menuProveedores.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.menuProveedores.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(179)))), ((int)(((byte)(179)))), ((int)(((byte)(179)))));
            this.menuProveedores.IconChar = FontAwesome.Sharp.IconChar.TruckFast;
            this.menuProveedores.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(179)))), ((int)(((byte)(179)))), ((int)(((byte)(179)))));
            this.menuProveedores.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.menuProveedores.IconSize = 35;
            this.menuProveedores.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.menuProveedores.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.menuProveedores.Margin = new System.Windows.Forms.Padding(0, 5, 0, 4);
            this.menuProveedores.Name = "menuProveedores";
            this.menuProveedores.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.menuProveedores.Size = new System.Drawing.Size(196, 50);
            this.menuProveedores.Text = "Proveedores";
            this.menuProveedores.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay;
            this.menuProveedores.Click += new System.EventHandler(this.menuProveedores_Click);
            // 
            // menuUsuarios
            // 
            this.menuUsuarios.AutoSize = false;
            this.menuUsuarios.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(50)))), ((int)(((byte)(56)))));
            this.menuUsuarios.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.menuUsuarios.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(179)))), ((int)(((byte)(179)))), ((int)(((byte)(179)))));
            this.menuUsuarios.IconChar = FontAwesome.Sharp.IconChar.UserGroup;
            this.menuUsuarios.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(179)))), ((int)(((byte)(179)))), ((int)(((byte)(179)))));
            this.menuUsuarios.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.menuUsuarios.IconSize = 35;
            this.menuUsuarios.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.menuUsuarios.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.menuUsuarios.Margin = new System.Windows.Forms.Padding(0, 5, 0, 4);
            this.menuUsuarios.Name = "menuUsuarios";
            this.menuUsuarios.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.menuUsuarios.Size = new System.Drawing.Size(200, 50);
            this.menuUsuarios.Text = "Usuarios";
            this.menuUsuarios.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay;
            this.menuUsuarios.Click += new System.EventHandler(this.menuUsuarios_Click);
            // 
            // menuGraficos
            // 
            this.menuGraficos.AutoSize = false;
            this.menuGraficos.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.menuGraficos.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(179)))), ((int)(((byte)(179)))), ((int)(((byte)(179)))));
            this.menuGraficos.IconChar = FontAwesome.Sharp.IconChar.ChartLine;
            this.menuGraficos.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(179)))), ((int)(((byte)(179)))), ((int)(((byte)(179)))));
            this.menuGraficos.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.menuGraficos.IconSize = 35;
            this.menuGraficos.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.menuGraficos.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.menuGraficos.Margin = new System.Windows.Forms.Padding(0, 5, 0, 4);
            this.menuGraficos.Name = "menuGraficos";
            this.menuGraficos.Size = new System.Drawing.Size(196, 50);
            this.menuGraficos.Text = "Graficos";
            this.menuGraficos.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay;
            this.menuGraficos.Click += new System.EventHandler(this.menuGraficos_Click);
            // 
            // lblRol
            // 
            this.lblRol.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblRol.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(71)))), ((int)(((byte)(79)))));
            this.lblRol.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRol.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(225)))), ((int)(((byte)(241)))));
            this.lblRol.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.lblRol.Location = new System.Drawing.Point(910, 36);
            this.lblRol.Name = "lblRol";
            this.lblRol.Size = new System.Drawing.Size(136, 31);
            this.lblRol.TabIndex = 6;
            this.lblRol.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // BarraTop
            // 
            this.BarraTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(71)))), ((int)(((byte)(79)))));
            this.BarraTop.Controls.Add(this.lblIndicador);
            this.BarraTop.Controls.Add(this.lblRol);
            this.BarraTop.Controls.Add(this.pictureBox2);
            this.BarraTop.Controls.Add(this.pictureBox1);
            this.BarraTop.Controls.Add(this.nombreUser);
            this.BarraTop.Controls.Add(this.menuTitulo);
            this.BarraTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.BarraTop.Location = new System.Drawing.Point(0, 0);
            this.BarraTop.Name = "BarraTop";
            this.BarraTop.Size = new System.Drawing.Size(1116, 82);
            this.BarraTop.TabIndex = 7;
            // 
            // lblIndicador
            // 
            this.lblIndicador.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblIndicador.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(71)))), ((int)(((byte)(79)))));
            this.lblIndicador.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblIndicador.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(225)))), ((int)(((byte)(241)))));
            this.lblIndicador.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.lblIndicador.Location = new System.Drawing.Point(312, 30);
            this.lblIndicador.Name = "lblIndicador";
            this.lblIndicador.Size = new System.Drawing.Size(410, 31);
            this.lblIndicador.TabIndex = 7;
            this.lblIndicador.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::CapaPresentacion.Properties.Resources.image_Photoroom1;
            this.pictureBox2.Location = new System.Drawing.Point(847, 8);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(63, 64);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 5;
            this.pictureBox2.TabStop = false;
            // 
            // menuTitulo
            // 
            this.menuTitulo.AutoSize = false;
            this.menuTitulo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(71)))), ((int)(((byte)(79)))));
            this.menuTitulo.Dock = System.Windows.Forms.DockStyle.None;
            this.menuTitulo.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.iconMenuItem9});
            this.menuTitulo.Location = new System.Drawing.Point(699, 21);
            this.menuTitulo.Name = "menuTitulo";
            this.menuTitulo.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.menuTitulo.Size = new System.Drawing.Size(408, 40);
            this.menuTitulo.TabIndex = 0;
            this.menuTitulo.Text = "Titulo";
            // 
            // iconMenuItem9
            // 
            this.iconMenuItem9.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.iconMenuItem9.IconChar = FontAwesome.Sharp.IconChar.RightFromBracket;
            this.iconMenuItem9.IconColor = System.Drawing.Color.White;
            this.iconMenuItem9.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.iconMenuItem9.IconSize = 40;
            this.iconMenuItem9.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.iconMenuItem9.Name = "iconMenuItem9";
            this.iconMenuItem9.ShowShortcutKeys = false;
            this.iconMenuItem9.Size = new System.Drawing.Size(52, 36);
            this.iconMenuItem9.Click += new System.EventHandler(this.iconMenuItem9_Click);
            this.iconMenuItem9.MouseEnter += new System.EventHandler(this.iconMenuItem9_MouseEnter);
            this.iconMenuItem9.MouseLeave += new System.EventHandler(this.iconMenuItem9_MouseLeave);
            // 
            // Contenedor
            // 
            this.Contenedor.BackColor = System.Drawing.Color.White;
            this.Contenedor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Contenedor.Location = new System.Drawing.Point(198, 81);
            this.Contenedor.Name = "Contenedor";
            this.Contenedor.Size = new System.Drawing.Size(918, 659);
            this.Contenedor.TabIndex = 6;
            // 
            // Backup
            // 
            this.Backup.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(50)))), ((int)(((byte)(56)))));
            this.Backup.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(179)))), ((int)(((byte)(179)))), ((int)(((byte)(179)))));
            this.Backup.IconChar = FontAwesome.Sharp.IconChar.None;
            this.Backup.IconColor = System.Drawing.Color.Black;
            this.Backup.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.Backup.Name = "Backup";
            this.Backup.Size = new System.Drawing.Size(180, 28);
            this.Backup.Text = "Backup";
            this.Backup.Click += new System.EventHandler(this.Backup_Click);
            // 
            // Inicio
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1116, 696);
            this.Controls.Add(this.BarraTop);
            this.Controls.Add(this.panelLateral);
            this.Controls.Add(this.Contenedor);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Inicio";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Sistema de Ventas de Zapatos";
            this.Load += new System.EventHandler(this.Inicio_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panelLateral.ResumeLayout(false);
            this.menuLateral.ResumeLayout(false);
            this.menuLateral.PerformLayout();
            this.BarraTop.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.menuTitulo.ResumeLayout(false);
            this.menuTitulo.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label nombreUser;
        private System.Windows.Forms.Panel panelLateral;
        private System.Windows.Forms.MenuStrip menuLateral;
        private FontAwesome.Sharp.IconMenuItem menuInicio;
        private FontAwesome.Sharp.IconMenuItem menuVentas;
        private FontAwesome.Sharp.IconMenuItem menuRegistrarVenta;
        private FontAwesome.Sharp.IconMenuItem menuVerDetalle;
        private FontAwesome.Sharp.IconMenuItem menuCompras;
        private FontAwesome.Sharp.IconMenuItem menuRegistrarCompra;
        private FontAwesome.Sharp.IconMenuItem menuVerDetalleCompra;
        private FontAwesome.Sharp.IconMenuItem menuClientes;
        private FontAwesome.Sharp.IconMenuItem menuMantenimiento;
        private FontAwesome.Sharp.IconMenuItem Productos;
        private FontAwesome.Sharp.IconMenuItem Categorias;
        private FontAwesome.Sharp.IconMenuItem Negocio;
        private FontAwesome.Sharp.IconMenuItem menuReportes;
        private System.Windows.Forms.ToolStripMenuItem subMenuRVentas;
        private System.Windows.Forms.ToolStripMenuItem subMenuRCompras;
        private FontAwesome.Sharp.IconMenuItem menuProveedores;
        private FontAwesome.Sharp.IconMenuItem menuUsuarios;
        private System.Windows.Forms.Panel BarraTop;
        private System.Windows.Forms.MenuStrip menuTitulo;
        private FontAwesome.Sharp.IconMenuItem iconMenuItem9;
        private System.Windows.Forms.Panel Contenedor;
        private System.Windows.Forms.PictureBox pictureBox2;
        private FontAwesome.Sharp.IconMenuItem menuGraficos;
        private System.Windows.Forms.Label lblRol;
        private System.Windows.Forms.Label lblIndicador;
        private FontAwesome.Sharp.IconMenuItem Backup;
    }
}

