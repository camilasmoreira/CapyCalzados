namespace CapaPresentacion
{
    partial class FrmCompras
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgvData = new System.Windows.Forms.DataGridView();
            this.IdProducto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Producto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Descripcion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PrecioCompra = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PrecioVenta = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Cantidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SubTotal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnEliminar = new System.Windows.Forms.DataGridViewButtonColumn();
            this.txtCantidad = new System.Windows.Forms.NumericUpDown();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtFecha = new CustomControls.RJControls.RJTextBox();
            this.comboDocumento = new System.Windows.Forms.ComboBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.rjButton3 = new CustomControls.RJControls.RJButton();
            this.txtPrecioV = new CustomControls.RJControls.RJTextBox();
            this.txtPrecioC = new CustomControls.RJControls.RJTextBox();
            this.txtProducto = new CustomControls.RJControls.RJTextBox();
            this.txtCod = new CustomControls.RJControls.RJTextBox();
            this.rjButton1 = new CustomControls.RJControls.RJButton();
            this.txtIdProd = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.rjButton2 = new CustomControls.RJControls.RJButton();
            this.txtRazon = new CustomControls.RJControls.RJTextBox();
            this.txtDocumento = new CustomControls.RJControls.RJTextBox();
            this.txtIdProveedor = new System.Windows.Forms.TextBox();
            this.txtTotal = new CustomControls.RJControls.RJTextBox();
            this.btnRegistrar = new CustomControls.RJControls.RJButton();
            this.rjTextBox2 = new CustomControls.RJControls.RJTextBox();
            this.label9 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCantidad)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvData
            // 
            this.dgvData.AllowUserToAddRows = false;
            this.dgvData.AllowUserToDeleteRows = false;
            this.dgvData.AllowUserToResizeColumns = false;
            this.dgvData.AllowUserToResizeRows = false;
            this.dgvData.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Padding = new System.Windows.Forms.Padding(2);
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvData.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvData.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.IdProducto,
            this.Producto,
            this.Descripcion,
            this.PrecioCompra,
            this.PrecioVenta,
            this.Cantidad,
            this.SubTotal,
            this.btnEliminar});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.Padding = new System.Windows.Forms.Padding(2);
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvData.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvData.EnableHeadersVisualStyles = false;
            this.dgvData.Location = new System.Drawing.Point(81, 320);
            this.dgvData.MultiSelect = false;
            this.dgvData.Name = "dgvData";
            this.dgvData.ReadOnly = true;
            this.dgvData.RowHeadersVisible = false;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Black;
            this.dgvData.RowsDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvData.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(59)))), ((int)(((byte)(59)))));
            this.dgvData.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvData.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.dgvData.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(59)))), ((int)(((byte)(59)))));
            this.dgvData.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.SystemColors.Highlight;
            this.dgvData.RowTemplate.Height = 28;
            this.dgvData.Size = new System.Drawing.Size(599, 249);
            this.dgvData.TabIndex = 73;
            this.dgvData.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvData_CellContentClick);
            this.dgvData.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.dgvData_CellPainting);
            // 
            // IdProducto
            // 
            this.IdProducto.HeaderText = "IdProducto";
            this.IdProducto.Name = "IdProducto";
            this.IdProducto.ReadOnly = true;
            this.IdProducto.Visible = false;
            // 
            // Producto
            // 
            this.Producto.HeaderText = "Producto";
            this.Producto.Name = "Producto";
            this.Producto.ReadOnly = true;
            // 
            // Descripcion
            // 
            this.Descripcion.HeaderText = "Descripcion";
            this.Descripcion.Name = "Descripcion";
            this.Descripcion.ReadOnly = true;
            // 
            // PrecioCompra
            // 
            this.PrecioCompra.HeaderText = "Precio Compra";
            this.PrecioCompra.Name = "PrecioCompra";
            this.PrecioCompra.ReadOnly = true;
            // 
            // PrecioVenta
            // 
            this.PrecioVenta.HeaderText = "Precio Venta";
            this.PrecioVenta.Name = "PrecioVenta";
            this.PrecioVenta.ReadOnly = true;
            this.PrecioVenta.Visible = false;
            // 
            // Cantidad
            // 
            this.Cantidad.HeaderText = "Cantidad";
            this.Cantidad.Name = "Cantidad";
            this.Cantidad.ReadOnly = true;
            // 
            // SubTotal
            // 
            this.SubTotal.HeaderText = "Sub Total";
            this.SubTotal.Name = "SubTotal";
            this.SubTotal.ReadOnly = true;
            // 
            // btnEliminar
            // 
            this.btnEliminar.HeaderText = "";
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.ReadOnly = true;
            this.btnEliminar.Width = 32;
            // 
            // txtCantidad
            // 
            this.txtCantidad.BackColor = System.Drawing.Color.Black;
            this.txtCantidad.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCantidad.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.txtCantidad.ForeColor = System.Drawing.Color.White;
            this.txtCantidad.Location = new System.Drawing.Point(608, 66);
            this.txtCantidad.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.txtCantidad.Name = "txtCantidad";
            this.txtCantidad.Size = new System.Drawing.Size(53, 29);
            this.txtCantidad.TabIndex = 32;
            this.txtCantidad.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtFecha);
            this.groupBox1.Controls.Add(this.comboDocumento);
            this.groupBox1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            this.groupBox1.ForeColor = System.Drawing.Color.White;
            this.groupBox1.Location = new System.Drawing.Point(81, 47);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(342, 93);
            this.groupBox1.TabIndex = 68;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Informacion de Compra";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.label10.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(175, 20);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(89, 17);
            this.label10.TabIndex = 119;
            this.label10.Text = "Tipo Factura:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(47, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 17);
            this.label2.TabIndex = 118;
            this.label2.Text = "Fecha:";
            // 
            // txtFecha
            // 
            this.txtFecha.BackColor = System.Drawing.Color.Black;
            this.txtFecha.BorderColor = System.Drawing.Color.MediumSlateBlue;
            this.txtFecha.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(225)))), ((int)(((byte)(241)))));
            this.txtFecha.BorderRadius = 10;
            this.txtFecha.BorderSize = 2;
            this.txtFecha.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.txtFecha.ForeColor = System.Drawing.Color.White;
            this.txtFecha.IsReadOnly = true;
            this.txtFecha.Location = new System.Drawing.Point(30, 33);
            this.txtFecha.Margin = new System.Windows.Forms.Padding(4);
            this.txtFecha.Multiline = false;
            this.txtFecha.Name = "txtFecha";
            this.txtFecha.Padding = new System.Windows.Forms.Padding(10, 7, 10, 7);
            this.txtFecha.PasswordChar = false;
            this.txtFecha.PlaceholderColor = System.Drawing.Color.DarkGray;
            this.txtFecha.PlaceholderText = "";
            this.txtFecha.ShortcutsEnabled = true;
            this.txtFecha.Size = new System.Drawing.Size(115, 36);
            this.txtFecha.TabIndex = 100;
            this.txtFecha.Texts = "";
            this.txtFecha.UnderlinedStyle = false;
            // 
            // comboDocumento
            // 
            this.comboDocumento.BackColor = System.Drawing.Color.Black;
            this.comboDocumento.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboDocumento.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comboDocumento.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.comboDocumento.ForeColor = System.Drawing.Color.White;
            this.comboDocumento.FormattingEnabled = true;
            this.comboDocumento.Location = new System.Drawing.Point(166, 40);
            this.comboDocumento.Name = "comboDocumento";
            this.comboDocumento.Size = new System.Drawing.Size(114, 29);
            this.comboDocumento.TabIndex = 15;
            // 
            // groupBox3
            // 
            this.groupBox3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.rjButton3);
            this.groupBox3.Controls.Add(this.txtPrecioV);
            this.groupBox3.Controls.Add(this.txtPrecioC);
            this.groupBox3.Controls.Add(this.txtProducto);
            this.groupBox3.Controls.Add(this.txtCod);
            this.groupBox3.Controls.Add(this.rjButton1);
            this.groupBox3.Controls.Add(this.txtCantidad);
            this.groupBox3.Controls.Add(this.txtIdProd);
            this.groupBox3.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            this.groupBox3.ForeColor = System.Drawing.Color.White;
            this.groupBox3.Location = new System.Drawing.Point(81, 162);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(744, 119);
            this.groupBox3.TabIndex = 70;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Informacion de Producto";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.label8.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(600, 49);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(67, 17);
            this.label8.TabIndex = 123;
            this.label8.Text = "Cantidad:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.label7.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(481, 43);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(89, 17);
            this.label7.TabIndex = 122;
            this.label7.Text = "Precio Venta:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.label6.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(336, 43);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(100, 17);
            this.label6.TabIndex = 121;
            this.label6.Text = "Precio compra:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.label4.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(212, 43);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(68, 17);
            this.label4.TabIndex = 120;
            this.label4.Text = "Producto:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.label3.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(14, 43);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(116, 17);
            this.label3.TabIndex = 119;
            this.label3.Text = "Codigo Producto:";
            // 
            // rjButton3
            // 
            this.rjButton3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.rjButton3.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.rjButton3.BorderColor = System.Drawing.Color.MediumSlateBlue;
            this.rjButton3.BorderRadius = 20;
            this.rjButton3.BorderSize = 2;
            this.rjButton3.Cursor = System.Windows.Forms.Cursors.Hand;
            this.rjButton3.FlatAppearance.BorderSize = 0;
            this.rjButton3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rjButton3.Font = new System.Drawing.Font("Microsoft JhengHei UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rjButton3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(243)))), ((int)(((byte)(243)))));
            this.rjButton3.Image = global::CapaPresentacion.Properties.Resources.buscar__1_;
            this.rjButton3.Location = new System.Drawing.Point(141, 58);
            this.rjButton3.Margin = new System.Windows.Forms.Padding(2);
            this.rjButton3.Name = "rjButton3";
            this.rjButton3.Size = new System.Drawing.Size(42, 37);
            this.rjButton3.TabIndex = 115;
            this.rjButton3.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(243)))), ((int)(((byte)(243)))));
            this.rjButton3.UseVisualStyleBackColor = false;
            this.rjButton3.Click += new System.EventHandler(this.iconButton1_Click);
            // 
            // txtPrecioV
            // 
            this.txtPrecioV.BackColor = System.Drawing.Color.Black;
            this.txtPrecioV.BorderColor = System.Drawing.Color.MediumSlateBlue;
            this.txtPrecioV.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(225)))), ((int)(((byte)(241)))));
            this.txtPrecioV.BorderRadius = 10;
            this.txtPrecioV.BorderSize = 2;
            this.txtPrecioV.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.txtPrecioV.ForeColor = System.Drawing.Color.White;
            this.txtPrecioV.IsReadOnly = false;
            this.txtPrecioV.Location = new System.Drawing.Point(470, 59);
            this.txtPrecioV.Margin = new System.Windows.Forms.Padding(4);
            this.txtPrecioV.Multiline = false;
            this.txtPrecioV.Name = "txtPrecioV";
            this.txtPrecioV.Padding = new System.Windows.Forms.Padding(10, 7, 10, 7);
            this.txtPrecioV.PasswordChar = false;
            this.txtPrecioV.PlaceholderColor = System.Drawing.Color.White;
            this.txtPrecioV.PlaceholderText = "Precio Venta";
            this.txtPrecioV.ShortcutsEnabled = false;
            this.txtPrecioV.Size = new System.Drawing.Size(123, 36);
            this.txtPrecioV.TabIndex = 105;
            this.txtPrecioV.Texts = "";
            this.txtPrecioV.UnderlinedStyle = false;
            this.txtPrecioV.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPrecioV_KeyDown);
            this.txtPrecioV.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPrecioV_KeyPress);
            // 
            // txtPrecioC
            // 
            this.txtPrecioC.BackColor = System.Drawing.Color.Black;
            this.txtPrecioC.BorderColor = System.Drawing.Color.MediumSlateBlue;
            this.txtPrecioC.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(225)))), ((int)(((byte)(241)))));
            this.txtPrecioC.BorderRadius = 10;
            this.txtPrecioC.BorderSize = 2;
            this.txtPrecioC.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.txtPrecioC.ForeColor = System.Drawing.Color.White;
            this.txtPrecioC.IsReadOnly = false;
            this.txtPrecioC.Location = new System.Drawing.Point(318, 59);
            this.txtPrecioC.Margin = new System.Windows.Forms.Padding(4);
            this.txtPrecioC.Multiline = false;
            this.txtPrecioC.Name = "txtPrecioC";
            this.txtPrecioC.Padding = new System.Windows.Forms.Padding(10, 7, 10, 7);
            this.txtPrecioC.PasswordChar = false;
            this.txtPrecioC.PlaceholderColor = System.Drawing.Color.White;
            this.txtPrecioC.PlaceholderText = "Precio Compra";
            this.txtPrecioC.ShortcutsEnabled = false;
            this.txtPrecioC.Size = new System.Drawing.Size(141, 36);
            this.txtPrecioC.TabIndex = 104;
            this.txtPrecioC.Texts = "";
            this.txtPrecioC.UnderlinedStyle = false;
            this.txtPrecioC.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPrecioC_KeyPress);
            // 
            // txtProducto
            // 
            this.txtProducto.BackColor = System.Drawing.Color.Black;
            this.txtProducto.BorderColor = System.Drawing.Color.MediumSlateBlue;
            this.txtProducto.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(225)))), ((int)(((byte)(241)))));
            this.txtProducto.BorderRadius = 10;
            this.txtProducto.BorderSize = 2;
            this.txtProducto.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.txtProducto.ForeColor = System.Drawing.Color.White;
            this.txtProducto.IsReadOnly = true;
            this.txtProducto.Location = new System.Drawing.Point(193, 59);
            this.txtProducto.Margin = new System.Windows.Forms.Padding(4);
            this.txtProducto.Multiline = false;
            this.txtProducto.Name = "txtProducto";
            this.txtProducto.Padding = new System.Windows.Forms.Padding(10, 7, 10, 7);
            this.txtProducto.PasswordChar = false;
            this.txtProducto.PlaceholderColor = System.Drawing.Color.White;
            this.txtProducto.PlaceholderText = "Producto";
            this.txtProducto.ShortcutsEnabled = false;
            this.txtProducto.Size = new System.Drawing.Size(116, 36);
            this.txtProducto.TabIndex = 103;
            this.txtProducto.Texts = "";
            this.txtProducto.UnderlinedStyle = false;
            // 
            // txtCod
            // 
            this.txtCod.BackColor = System.Drawing.Color.Black;
            this.txtCod.BorderColor = System.Drawing.Color.MediumSlateBlue;
            this.txtCod.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(225)))), ((int)(((byte)(241)))));
            this.txtCod.BorderRadius = 10;
            this.txtCod.BorderSize = 2;
            this.txtCod.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.txtCod.ForeColor = System.Drawing.Color.White;
            this.txtCod.IsReadOnly = false;
            this.txtCod.Location = new System.Drawing.Point(6, 59);
            this.txtCod.Margin = new System.Windows.Forms.Padding(4);
            this.txtCod.Multiline = false;
            this.txtCod.Name = "txtCod";
            this.txtCod.Padding = new System.Windows.Forms.Padding(10, 7, 10, 7);
            this.txtCod.PasswordChar = false;
            this.txtCod.PlaceholderColor = System.Drawing.Color.White;
            this.txtCod.PlaceholderText = "Cod Producto";
            this.txtCod.ShortcutsEnabled = false;
            this.txtCod.Size = new System.Drawing.Size(125, 36);
            this.txtCod.TabIndex = 102;
            this.txtCod.Texts = "";
            this.txtCod.UnderlinedStyle = false;
            this.txtCod.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCod_KeyDown);
            // 
            // rjButton1
            // 
            this.rjButton1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.rjButton1.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.rjButton1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(50)))), ((int)(((byte)(56)))));
            this.rjButton1.BorderRadius = 22;
            this.rjButton1.BorderSize = 1;
            this.rjButton1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.rjButton1.FlatAppearance.BorderSize = 0;
            this.rjButton1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rjButton1.Font = new System.Drawing.Font("Microsoft JhengHei UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rjButton1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(243)))), ((int)(((byte)(243)))));
            this.rjButton1.Image = global::CapaPresentacion.Properties.Resources.mas__1_;
            this.rjButton1.Location = new System.Drawing.Point(676, 49);
            this.rjButton1.Margin = new System.Windows.Forms.Padding(2);
            this.rjButton1.Name = "rjButton1";
            this.rjButton1.Size = new System.Drawing.Size(41, 42);
            this.rjButton1.TabIndex = 66;
            this.rjButton1.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(243)))), ((int)(((byte)(243)))));
            this.rjButton1.UseVisualStyleBackColor = false;
            this.rjButton1.Click += new System.EventHandler(this.btnAgregar_Click);
            // 
            // txtIdProd
            // 
            this.txtIdProd.Location = new System.Drawing.Point(50, 20);
            this.txtIdProd.Name = "txtIdProd";
            this.txtIdProd.Size = new System.Drawing.Size(19, 25);
            this.txtIdProd.TabIndex = 16;
            this.txtIdProd.Visible = false;
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.rjButton2);
            this.groupBox2.Controls.Add(this.txtRazon);
            this.groupBox2.Controls.Add(this.txtDocumento);
            this.groupBox2.Controls.Add(this.txtIdProveedor);
            this.groupBox2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            this.groupBox2.ForeColor = System.Drawing.Color.White;
            this.groupBox2.Location = new System.Drawing.Point(460, 47);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(365, 93);
            this.groupBox2.TabIndex = 69;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Informacion de Proveedor";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(213, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 17);
            this.label1.TabIndex = 117;
            this.label1.Text = "Razon Social:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.label5.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(30, 18);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(84, 17);
            this.label5.TabIndex = 116;
            this.label5.Text = "Documento:";
            // 
            // rjButton2
            // 
            this.rjButton2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.rjButton2.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.rjButton2.BorderColor = System.Drawing.Color.MediumSlateBlue;
            this.rjButton2.BorderRadius = 20;
            this.rjButton2.BorderSize = 2;
            this.rjButton2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.rjButton2.FlatAppearance.BorderSize = 0;
            this.rjButton2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rjButton2.Font = new System.Drawing.Font("Microsoft JhengHei UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rjButton2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(243)))), ((int)(((byte)(243)))));
            this.rjButton2.Image = global::CapaPresentacion.Properties.Resources.buscar__1_;
            this.rjButton2.Location = new System.Drawing.Point(142, 36);
            this.rjButton2.Margin = new System.Windows.Forms.Padding(2);
            this.rjButton2.Name = "rjButton2";
            this.rjButton2.Size = new System.Drawing.Size(51, 37);
            this.rjButton2.TabIndex = 114;
            this.rjButton2.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(243)))), ((int)(((byte)(243)))));
            this.rjButton2.UseVisualStyleBackColor = false;
            this.rjButton2.Click += new System.EventHandler(this.rjButton2_Click);
            // 
            // txtRazon
            // 
            this.txtRazon.BackColor = System.Drawing.Color.Black;
            this.txtRazon.BorderColor = System.Drawing.Color.MediumSlateBlue;
            this.txtRazon.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(225)))), ((int)(((byte)(241)))));
            this.txtRazon.BorderRadius = 10;
            this.txtRazon.BorderSize = 2;
            this.txtRazon.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.txtRazon.ForeColor = System.Drawing.Color.White;
            this.txtRazon.IsReadOnly = true;
            this.txtRazon.Location = new System.Drawing.Point(200, 36);
            this.txtRazon.Margin = new System.Windows.Forms.Padding(4);
            this.txtRazon.Multiline = false;
            this.txtRazon.Name = "txtRazon";
            this.txtRazon.Padding = new System.Windows.Forms.Padding(10, 7, 10, 7);
            this.txtRazon.PasswordChar = false;
            this.txtRazon.PlaceholderColor = System.Drawing.Color.White;
            this.txtRazon.PlaceholderText = "Razon Social";
            this.txtRazon.ShortcutsEnabled = false;
            this.txtRazon.Size = new System.Drawing.Size(138, 36);
            this.txtRazon.TabIndex = 100;
            this.txtRazon.Texts = "";
            this.txtRazon.UnderlinedStyle = false;
            // 
            // txtDocumento
            // 
            this.txtDocumento.BackColor = System.Drawing.Color.Black;
            this.txtDocumento.BorderColor = System.Drawing.Color.MediumSlateBlue;
            this.txtDocumento.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(225)))), ((int)(((byte)(241)))));
            this.txtDocumento.BorderRadius = 10;
            this.txtDocumento.BorderSize = 2;
            this.txtDocumento.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.txtDocumento.ForeColor = System.Drawing.Color.White;
            this.txtDocumento.IsReadOnly = false;
            this.txtDocumento.Location = new System.Drawing.Point(10, 35);
            this.txtDocumento.Margin = new System.Windows.Forms.Padding(4);
            this.txtDocumento.Multiline = false;
            this.txtDocumento.Name = "txtDocumento";
            this.txtDocumento.Padding = new System.Windows.Forms.Padding(10, 7, 10, 7);
            this.txtDocumento.PasswordChar = false;
            this.txtDocumento.PlaceholderColor = System.Drawing.Color.White;
            this.txtDocumento.PlaceholderText = "Documento";
            this.txtDocumento.ShortcutsEnabled = false;
            this.txtDocumento.Size = new System.Drawing.Size(122, 36);
            this.txtDocumento.TabIndex = 101;
            this.txtDocumento.Texts = "";
            this.txtDocumento.UnderlinedStyle = false;
            this.txtDocumento.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtDocumento_KeyDown);
            this.txtDocumento.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDocumento_KeyPress);
            // 
            // txtIdProveedor
            // 
            this.txtIdProveedor.Location = new System.Drawing.Point(232, 9);
            this.txtIdProveedor.Name = "txtIdProveedor";
            this.txtIdProveedor.Size = new System.Drawing.Size(24, 25);
            this.txtIdProveedor.TabIndex = 16;
            this.txtIdProveedor.Visible = false;
            // 
            // txtTotal
            // 
            this.txtTotal.BackColor = System.Drawing.Color.Black;
            this.txtTotal.BorderColor = System.Drawing.Color.MediumSlateBlue;
            this.txtTotal.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(225)))), ((int)(((byte)(241)))));
            this.txtTotal.BorderRadius = 10;
            this.txtTotal.BorderSize = 2;
            this.txtTotal.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.txtTotal.ForeColor = System.Drawing.Color.White;
            this.txtTotal.IsReadOnly = true;
            this.txtTotal.Location = new System.Drawing.Point(691, 416);
            this.txtTotal.Margin = new System.Windows.Forms.Padding(4);
            this.txtTotal.Multiline = false;
            this.txtTotal.Name = "txtTotal";
            this.txtTotal.Padding = new System.Windows.Forms.Padding(10, 7, 10, 7);
            this.txtTotal.PasswordChar = false;
            this.txtTotal.PlaceholderColor = System.Drawing.Color.White;
            this.txtTotal.PlaceholderText = "0.00";
            this.txtTotal.ShortcutsEnabled = false;
            this.txtTotal.Size = new System.Drawing.Size(100, 36);
            this.txtTotal.TabIndex = 106;
            this.txtTotal.Texts = "";
            this.txtTotal.UnderlinedStyle = false;
            // 
            // btnRegistrar
            // 
            this.btnRegistrar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.btnRegistrar.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.btnRegistrar.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(50)))), ((int)(((byte)(56)))));
            this.btnRegistrar.BorderRadius = 22;
            this.btnRegistrar.BorderSize = 1;
            this.btnRegistrar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRegistrar.FlatAppearance.BorderSize = 0;
            this.btnRegistrar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRegistrar.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnRegistrar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(243)))), ((int)(((byte)(243)))));
            this.btnRegistrar.Image = global::CapaPresentacion.Properties.Resources.etiqueta_de_precio;
            this.btnRegistrar.Location = new System.Drawing.Point(685, 502);
            this.btnRegistrar.Margin = new System.Windows.Forms.Padding(2);
            this.btnRegistrar.Name = "btnRegistrar";
            this.btnRegistrar.Size = new System.Drawing.Size(140, 47);
            this.btnRegistrar.TabIndex = 74;
            this.btnRegistrar.Text = "Registrar";
            this.btnRegistrar.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(243)))), ((int)(((byte)(243)))));
            this.btnRegistrar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnRegistrar.UseVisualStyleBackColor = false;
            this.btnRegistrar.Click += new System.EventHandler(this.btnRegistrar_Click);
            // 
            // rjTextBox2
            // 
            this.rjTextBox2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(43)))), ((int)(((byte)(59)))));
            this.rjTextBox2.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(8)))), ((int)(((byte)(119)))), ((int)(((byte)(220)))));
            this.rjTextBox2.BorderFocusColor = System.Drawing.Color.HotPink;
            this.rjTextBox2.BorderRadius = 15;
            this.rjTextBox2.BorderSize = 2;
            this.rjTextBox2.Enabled = false;
            this.rjTextBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rjTextBox2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.rjTextBox2.IsReadOnly = true;
            this.rjTextBox2.Location = new System.Drawing.Point(25, 13);
            this.rjTextBox2.Margin = new System.Windows.Forms.Padding(4);
            this.rjTextBox2.Multiline = true;
            this.rjTextBox2.Name = "rjTextBox2";
            this.rjTextBox2.Padding = new System.Windows.Forms.Padding(10, 7, 10, 7);
            this.rjTextBox2.PasswordChar = false;
            this.rjTextBox2.PlaceholderColor = System.Drawing.Color.DarkGray;
            this.rjTextBox2.PlaceholderText = "";
            this.rjTextBox2.ShortcutsEnabled = true;
            this.rjTextBox2.Size = new System.Drawing.Size(885, 595);
            this.rjTextBox2.TabIndex = 107;
            this.rjTextBox2.Texts = "";
            this.rjTextBox2.UnderlinedStyle = false;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(43)))), ((int)(((byte)(59)))));
            this.label9.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.White;
            this.label9.Location = new System.Drawing.Point(705, 395);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(43, 17);
            this.label9.TabIndex = 124;
            this.label9.Text = "Total:";
            // 
            // FrmCompras
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(902, 620);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.txtTotal);
            this.Controls.Add(this.btnRegistrar);
            this.Controls.Add(this.dgvData);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.rjTextBox2);
            this.Name = "FrmCompras";
            this.Text = "FrmCompras";
            this.Load += new System.EventHandler(this.FrmCompras_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCantidad)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private CustomControls.RJControls.RJButton btnRegistrar;
        private System.Windows.Forms.DataGridView dgvData;
        private CustomControls.RJControls.RJButton rjButton1;
        private System.Windows.Forms.NumericUpDown txtCantidad;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox comboDocumento;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtIdProveedor;
        private CustomControls.RJControls.RJTextBox txtFecha;
        private CustomControls.RJControls.RJTextBox txtDocumento;
        private CustomControls.RJControls.RJTextBox txtPrecioV;
        private CustomControls.RJControls.RJTextBox txtPrecioC;
        private CustomControls.RJControls.RJTextBox txtProducto;
        private CustomControls.RJControls.RJTextBox txtCod;
        private CustomControls.RJControls.RJTextBox txtRazon;
        private CustomControls.RJControls.RJTextBox txtTotal;
        private CustomControls.RJControls.RJTextBox rjTextBox2;
        private System.Windows.Forms.TextBox txtIdProd;
        private CustomControls.RJControls.RJButton rjButton2;
        private CustomControls.RJControls.RJButton rjButton3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.DataGridViewTextBoxColumn IdProducto;
        private System.Windows.Forms.DataGridViewTextBoxColumn Producto;
        private System.Windows.Forms.DataGridViewTextBoxColumn Descripcion;
        private System.Windows.Forms.DataGridViewTextBoxColumn PrecioCompra;
        private System.Windows.Forms.DataGridViewTextBoxColumn PrecioVenta;
        private System.Windows.Forms.DataGridViewTextBoxColumn Cantidad;
        private System.Windows.Forms.DataGridViewTextBoxColumn SubTotal;
        private System.Windows.Forms.DataGridViewButtonColumn btnEliminar;
    }
}