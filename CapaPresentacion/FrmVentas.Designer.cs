namespace CapaPresentacion
{
    partial class FrmVentas
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
            this.comboDocumento = new System.Windows.Forms.ComboBox();
            this.txtIdProd = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.dgvData = new System.Windows.Forms.DataGridView();
            this.IdProducto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Producto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Descripcion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Precio = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Cantidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SubTotal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnEliminar = new System.Windows.Forms.DataGridViewButtonColumn();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtFecha = new CustomControls.RJControls.RJTextBox();
            this.txtCantidad = new System.Windows.Forms.NumericUpDown();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.rjButton2 = new CustomControls.RJControls.RJButton();
            this.txtNombreC = new CustomControls.RJControls.RJTextBox();
            this.txtDocumento = new CustomControls.RJControls.RJTextBox();
            this.txtIdCliente = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.btnBuscarProd = new CustomControls.RJControls.RJButton();
            this.txtProducto = new CustomControls.RJControls.RJTextBox();
            this.txtStock = new CustomControls.RJControls.RJTextBox();
            this.txtCod = new CustomControls.RJControls.RJTextBox();
            this.txtPrecio = new CustomControls.RJControls.RJTextBox();
            this.BtnAgregar = new CustomControls.RJControls.RJButton();
            this.txtCupon = new System.Windows.Forms.TextBox();
            this.txtCambio = new CustomControls.RJControls.RJTextBox();
            this.txtPaga = new CustomControls.RJControls.RJTextBox();
            this.txtTotal = new CustomControls.RJControls.RJTextBox();
            this.rjButton1 = new CustomControls.RJControls.RJButton();
            this.rjTextBox2 = new CustomControls.RJControls.RJTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtCantidad)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // comboDocumento
            // 
            this.comboDocumento.BackColor = System.Drawing.Color.Black;
            this.comboDocumento.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboDocumento.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comboDocumento.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboDocumento.ForeColor = System.Drawing.Color.White;
            this.comboDocumento.FormattingEnabled = true;
            this.comboDocumento.Location = new System.Drawing.Point(200, 47);
            this.comboDocumento.Name = "comboDocumento";
            this.comboDocumento.Size = new System.Drawing.Size(114, 29);
            this.comboDocumento.TabIndex = 15;
            // 
            // txtIdProd
            // 
            this.txtIdProd.Location = new System.Drawing.Point(323, 17);
            this.txtIdProd.Name = "txtIdProd";
            this.txtIdProd.Size = new System.Drawing.Size(19, 25);
            this.txtIdProd.TabIndex = 16;
            this.txtIdProd.Visible = false;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(43)))), ((int)(((byte)(59)))));
            this.label13.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            this.label13.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label13.Location = new System.Drawing.Point(732, 435);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(59, 17);
            this.label13.TabIndex = 76;
            this.label13.Text = "Cambio:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(43)))), ((int)(((byte)(59)))));
            this.label10.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            this.label10.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label10.Location = new System.Drawing.Point(732, 370);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(68, 17);
            this.label10.TabIndex = 74;
            this.label10.Text = "Paga con:";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(43)))), ((int)(((byte)(59)))));
            this.label12.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            this.label12.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label12.Location = new System.Drawing.Point(731, 300);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(94, 17);
            this.label12.TabIndex = 71;
            this.label12.Text = "Total a Pagar:";
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
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
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
            this.Precio,
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
            this.dgvData.Location = new System.Drawing.Point(107, 296);
            this.dgvData.MultiSelect = false;
            this.dgvData.Name = "dgvData";
            this.dgvData.ReadOnly = true;
            this.dgvData.RowHeadersVisible = false;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Black;
            this.dgvData.RowsDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvData.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(59)))), ((int)(((byte)(59)))));
            this.dgvData.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvData.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.Color.White;
            this.dgvData.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(59)))), ((int)(((byte)(59)))));
            this.dgvData.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.SystemColors.Highlight;
            this.dgvData.RowTemplate.Height = 28;
            this.dgvData.Size = new System.Drawing.Size(586, 275);
            this.dgvData.TabIndex = 72;
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
            this.Descripcion.Width = 150;
            // 
            // Precio
            // 
            this.Precio.HeaderText = "Precio";
            this.Precio.Name = "Precio";
            this.Precio.ReadOnly = true;
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
            this.btnEliminar.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.btnEliminar.Width = 32;
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtFecha);
            this.groupBox1.Controls.Add(this.comboDocumento);
            this.groupBox1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            this.groupBox1.ForeColor = System.Drawing.Color.White;
            this.groupBox1.Location = new System.Drawing.Point(107, 51);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(342, 93);
            this.groupBox1.TabIndex = 67;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Informacion de Venta";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.label3.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(14, 24);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 17);
            this.label3.TabIndex = 70;
            this.label3.Text = "Fecha:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(197, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 17);
            this.label1.TabIndex = 69;
            this.label1.Text = "Tipo Factura:";
            // 
            // txtFecha
            // 
            this.txtFecha.BackColor = System.Drawing.Color.Black;
            this.txtFecha.BorderColor = System.Drawing.Color.MediumSlateBlue;
            this.txtFecha.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(225)))), ((int)(((byte)(241)))));
            this.txtFecha.BorderRadius = 10;
            this.txtFecha.BorderSize = 2;
            this.txtFecha.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFecha.ForeColor = System.Drawing.Color.White;
            this.txtFecha.IsReadOnly = true;
            this.txtFecha.Location = new System.Drawing.Point(7, 40);
            this.txtFecha.Margin = new System.Windows.Forms.Padding(4);
            this.txtFecha.Multiline = false;
            this.txtFecha.Name = "txtFecha";
            this.txtFecha.Padding = new System.Windows.Forms.Padding(10, 7, 10, 7);
            this.txtFecha.PasswordChar = false;
            this.txtFecha.PlaceholderColor = System.Drawing.Color.White;
            this.txtFecha.PlaceholderText = "Fecha";
            this.txtFecha.ShortcutsEnabled = false;
            this.txtFecha.Size = new System.Drawing.Size(137, 36);
            this.txtFecha.TabIndex = 1;
            this.txtFecha.Texts = "";
            this.txtFecha.UnderlinedStyle = false;
            // 
            // txtCantidad
            // 
            this.txtCantidad.BackColor = System.Drawing.Color.Black;
            this.txtCantidad.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCantidad.Cursor = System.Windows.Forms.Cursors.Default;
            this.txtCantidad.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCantidad.ForeColor = System.Drawing.Color.White;
            this.txtCantidad.Location = new System.Drawing.Point(617, 47);
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
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.rjButton2);
            this.groupBox2.Controls.Add(this.txtNombreC);
            this.groupBox2.Controls.Add(this.txtDocumento);
            this.groupBox2.Controls.Add(this.txtIdCliente);
            this.groupBox2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            this.groupBox2.ForeColor = System.Drawing.Color.White;
            this.groupBox2.Location = new System.Drawing.Point(494, 51);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(354, 93);
            this.groupBox2.TabIndex = 68;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Informacion de Cliente";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.label4.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(200, 24);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(109, 17);
            this.label4.TabIndex = 89;
            this.label4.Text = "Nombre Cliente:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(20, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(84, 17);
            this.label2.TabIndex = 88;
            this.label2.Text = "Documento:";
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
            this.rjButton2.Location = new System.Drawing.Point(137, 36);
            this.rjButton2.Margin = new System.Windows.Forms.Padding(2);
            this.rjButton2.Name = "rjButton2";
            this.rjButton2.Size = new System.Drawing.Size(46, 40);
            this.rjButton2.TabIndex = 87;
            this.rjButton2.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(243)))), ((int)(((byte)(243)))));
            this.rjButton2.UseVisualStyleBackColor = false;
            this.rjButton2.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // txtNombreC
            // 
            this.txtNombreC.BackColor = System.Drawing.Color.Black;
            this.txtNombreC.BorderColor = System.Drawing.Color.MediumSlateBlue;
            this.txtNombreC.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(225)))), ((int)(((byte)(241)))));
            this.txtNombreC.BorderRadius = 10;
            this.txtNombreC.BorderSize = 2;
            this.txtNombreC.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNombreC.ForeColor = System.Drawing.Color.White;
            this.txtNombreC.IsReadOnly = true;
            this.txtNombreC.Location = new System.Drawing.Point(189, 40);
            this.txtNombreC.Margin = new System.Windows.Forms.Padding(4);
            this.txtNombreC.Multiline = false;
            this.txtNombreC.Name = "txtNombreC";
            this.txtNombreC.Padding = new System.Windows.Forms.Padding(10, 7, 10, 7);
            this.txtNombreC.PasswordChar = false;
            this.txtNombreC.PlaceholderColor = System.Drawing.Color.White;
            this.txtNombreC.PlaceholderText = "Nombre Cliente";
            this.txtNombreC.ShortcutsEnabled = false;
            this.txtNombreC.Size = new System.Drawing.Size(152, 36);
            this.txtNombreC.TabIndex = 3;
            this.txtNombreC.Texts = "";
            this.txtNombreC.UnderlinedStyle = false;
            // 
            // txtDocumento
            // 
            this.txtDocumento.BackColor = System.Drawing.Color.Black;
            this.txtDocumento.BorderColor = System.Drawing.Color.MediumSlateBlue;
            this.txtDocumento.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(225)))), ((int)(((byte)(241)))));
            this.txtDocumento.BorderRadius = 10;
            this.txtDocumento.BorderSize = 2;
            this.txtDocumento.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDocumento.ForeColor = System.Drawing.Color.White;
            this.txtDocumento.IsReadOnly = false;
            this.txtDocumento.Location = new System.Drawing.Point(7, 40);
            this.txtDocumento.Margin = new System.Windows.Forms.Padding(4);
            this.txtDocumento.Multiline = false;
            this.txtDocumento.Name = "txtDocumento";
            this.txtDocumento.Padding = new System.Windows.Forms.Padding(10, 7, 10, 7);
            this.txtDocumento.PasswordChar = false;
            this.txtDocumento.PlaceholderColor = System.Drawing.Color.White;
            this.txtDocumento.PlaceholderText = "Documento";
            this.txtDocumento.ShortcutsEnabled = false;
            this.txtDocumento.Size = new System.Drawing.Size(122, 36);
            this.txtDocumento.TabIndex = 2;
            this.txtDocumento.Texts = "";
            this.txtDocumento.UnderlinedStyle = false;
            this.txtDocumento.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtDocumento_KeyDown);
            this.txtDocumento.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDocumento_KeyPress);
            // 
            // txtIdCliente
            // 
            this.txtIdCliente.Location = new System.Drawing.Point(300, 9);
            this.txtIdCliente.Name = "txtIdCliente";
            this.txtIdCliente.Size = new System.Drawing.Size(24, 25);
            this.txtIdCliente.TabIndex = 16;
            this.txtIdCliente.Visible = false;
            // 
            // groupBox3
            // 
            this.groupBox3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.btnBuscarProd);
            this.groupBox3.Controls.Add(this.txtProducto);
            this.groupBox3.Controls.Add(this.txtStock);
            this.groupBox3.Controls.Add(this.txtCod);
            this.groupBox3.Controls.Add(this.txtPrecio);
            this.groupBox3.Controls.Add(this.BtnAgregar);
            this.groupBox3.Controls.Add(this.txtCantidad);
            this.groupBox3.Controls.Add(this.txtIdProd);
            this.groupBox3.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            this.groupBox3.ForeColor = System.Drawing.Color.White;
            this.groupBox3.Location = new System.Drawing.Point(107, 164);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(741, 98);
            this.groupBox3.TabIndex = 69;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = " Informacion Producto";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.label8.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(499, 27);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(46, 17);
            this.label8.TabIndex = 90;
            this.label8.Text = "Stock:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.label7.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(374, 27);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(50, 17);
            this.label7.TabIndex = 89;
            this.label7.Text = "Precio:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.label6.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(14, 25);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(116, 17);
            this.label6.TabIndex = 88;
            this.label6.Text = "Codigo Producto:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.label5.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(228, 27);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(68, 17);
            this.label5.TabIndex = 87;
            this.label5.Text = "Producto:";
            // 
            // btnBuscarProd
            // 
            this.btnBuscarProd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.btnBuscarProd.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.btnBuscarProd.BorderColor = System.Drawing.Color.MediumSlateBlue;
            this.btnBuscarProd.BorderRadius = 20;
            this.btnBuscarProd.BorderSize = 2;
            this.btnBuscarProd.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBuscarProd.FlatAppearance.BorderSize = 0;
            this.btnBuscarProd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBuscarProd.Font = new System.Drawing.Font("Microsoft JhengHei UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBuscarProd.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(243)))), ((int)(((byte)(243)))));
            this.btnBuscarProd.Image = global::CapaPresentacion.Properties.Resources.buscar__1_;
            this.btnBuscarProd.Location = new System.Drawing.Point(150, 37);
            this.btnBuscarProd.Margin = new System.Windows.Forms.Padding(2);
            this.btnBuscarProd.Name = "btnBuscarProd";
            this.btnBuscarProd.Size = new System.Drawing.Size(49, 43);
            this.btnBuscarProd.TabIndex = 86;
            this.btnBuscarProd.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(243)))), ((int)(((byte)(243)))));
            this.btnBuscarProd.UseVisualStyleBackColor = false;
            this.btnBuscarProd.Click += new System.EventHandler(this.btnBuscarProd_Click);
            // 
            // txtProducto
            // 
            this.txtProducto.BackColor = System.Drawing.Color.Black;
            this.txtProducto.BorderColor = System.Drawing.Color.MediumSlateBlue;
            this.txtProducto.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(225)))), ((int)(((byte)(241)))));
            this.txtProducto.BorderRadius = 10;
            this.txtProducto.BorderSize = 2;
            this.txtProducto.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtProducto.ForeColor = System.Drawing.Color.White;
            this.txtProducto.IsReadOnly = true;
            this.txtProducto.Location = new System.Drawing.Point(205, 43);
            this.txtProducto.Margin = new System.Windows.Forms.Padding(4);
            this.txtProducto.Multiline = false;
            this.txtProducto.Name = "txtProducto";
            this.txtProducto.Padding = new System.Windows.Forms.Padding(10, 7, 10, 7);
            this.txtProducto.PasswordChar = false;
            this.txtProducto.PlaceholderColor = System.Drawing.Color.White;
            this.txtProducto.PlaceholderText = "Producto";
            this.txtProducto.ShortcutsEnabled = false;
            this.txtProducto.Size = new System.Drawing.Size(137, 36);
            this.txtProducto.TabIndex = 5;
            this.txtProducto.Texts = "";
            this.txtProducto.UnderlinedStyle = false;
            // 
            // txtStock
            // 
            this.txtStock.BackColor = System.Drawing.Color.Black;
            this.txtStock.BorderColor = System.Drawing.Color.MediumSlateBlue;
            this.txtStock.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(225)))), ((int)(((byte)(241)))));
            this.txtStock.BorderRadius = 10;
            this.txtStock.BorderSize = 2;
            this.txtStock.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtStock.ForeColor = System.Drawing.Color.White;
            this.txtStock.IsReadOnly = true;
            this.txtStock.Location = new System.Drawing.Point(486, 43);
            this.txtStock.Margin = new System.Windows.Forms.Padding(4);
            this.txtStock.Multiline = false;
            this.txtStock.Name = "txtStock";
            this.txtStock.Padding = new System.Windows.Forms.Padding(10, 7, 10, 7);
            this.txtStock.PasswordChar = false;
            this.txtStock.PlaceholderColor = System.Drawing.Color.White;
            this.txtStock.PlaceholderText = "Stock";
            this.txtStock.ShortcutsEnabled = false;
            this.txtStock.Size = new System.Drawing.Size(100, 36);
            this.txtStock.TabIndex = 7;
            this.txtStock.Texts = "";
            this.txtStock.UnderlinedStyle = false;
            this.txtStock.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtStock_KeyPress);
            // 
            // txtCod
            // 
            this.txtCod.BackColor = System.Drawing.Color.Black;
            this.txtCod.BorderColor = System.Drawing.Color.MediumSlateBlue;
            this.txtCod.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(225)))), ((int)(((byte)(241)))));
            this.txtCod.BorderRadius = 10;
            this.txtCod.BorderSize = 2;
            this.txtCod.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCod.ForeColor = System.Drawing.Color.White;
            this.txtCod.IsReadOnly = false;
            this.txtCod.Location = new System.Drawing.Point(8, 43);
            this.txtCod.Margin = new System.Windows.Forms.Padding(4);
            this.txtCod.Multiline = false;
            this.txtCod.Name = "txtCod";
            this.txtCod.Padding = new System.Windows.Forms.Padding(10, 7, 10, 7);
            this.txtCod.PasswordChar = false;
            this.txtCod.PlaceholderColor = System.Drawing.Color.White;
            this.txtCod.PlaceholderText = "Cod Producto";
            this.txtCod.ShortcutsEnabled = false;
            this.txtCod.Size = new System.Drawing.Size(136, 36);
            this.txtCod.TabIndex = 4;
            this.txtCod.Texts = "";
            this.txtCod.UnderlinedStyle = false;
            this.txtCod.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCod_KeyDown);
            // 
            // txtPrecio
            // 
            this.txtPrecio.BackColor = System.Drawing.Color.Black;
            this.txtPrecio.BorderColor = System.Drawing.Color.MediumSlateBlue;
            this.txtPrecio.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(225)))), ((int)(((byte)(241)))));
            this.txtPrecio.BorderRadius = 10;
            this.txtPrecio.BorderSize = 2;
            this.txtPrecio.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPrecio.ForeColor = System.Drawing.Color.White;
            this.txtPrecio.IsReadOnly = false;
            this.txtPrecio.Location = new System.Drawing.Point(364, 43);
            this.txtPrecio.Margin = new System.Windows.Forms.Padding(4);
            this.txtPrecio.Multiline = false;
            this.txtPrecio.Name = "txtPrecio";
            this.txtPrecio.Padding = new System.Windows.Forms.Padding(10, 7, 10, 7);
            this.txtPrecio.PasswordChar = false;
            this.txtPrecio.PlaceholderColor = System.Drawing.Color.White;
            this.txtPrecio.PlaceholderText = "Precio";
            this.txtPrecio.ShortcutsEnabled = false;
            this.txtPrecio.Size = new System.Drawing.Size(92, 36);
            this.txtPrecio.TabIndex = 6;
            this.txtPrecio.Texts = "";
            this.txtPrecio.UnderlinedStyle = false;
            this.txtPrecio.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPrecio_KeyPress);
            // 
            // BtnAgregar
            // 
            this.BtnAgregar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.BtnAgregar.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.BtnAgregar.BorderColor = System.Drawing.Color.MediumSlateBlue;
            this.BtnAgregar.BorderRadius = 20;
            this.BtnAgregar.BorderSize = 2;
            this.BtnAgregar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnAgregar.FlatAppearance.BorderSize = 0;
            this.BtnAgregar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnAgregar.Font = new System.Drawing.Font("Microsoft JhengHei UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnAgregar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(243)))), ((int)(((byte)(243)))));
            this.BtnAgregar.Image = global::CapaPresentacion.Properties.Resources.mas__1_;
            this.BtnAgregar.Location = new System.Drawing.Point(687, 37);
            this.BtnAgregar.Margin = new System.Windows.Forms.Padding(2);
            this.BtnAgregar.Name = "BtnAgregar";
            this.BtnAgregar.Size = new System.Drawing.Size(41, 42);
            this.BtnAgregar.TabIndex = 65;
            this.BtnAgregar.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(243)))), ((int)(((byte)(243)))));
            this.BtnAgregar.UseVisualStyleBackColor = false;
            this.BtnAgregar.Click += new System.EventHandler(this.btnAgregar_Click);
            // 
            // txtCupon
            // 
            this.txtCupon.Location = new System.Drawing.Point(82, 186);
            this.txtCupon.Name = "txtCupon";
            this.txtCupon.Size = new System.Drawing.Size(19, 20);
            this.txtCupon.TabIndex = 91;
            this.txtCupon.Visible = false;
            // 
            // txtCambio
            // 
            this.txtCambio.BackColor = System.Drawing.Color.Black;
            this.txtCambio.BorderColor = System.Drawing.Color.MediumSlateBlue;
            this.txtCambio.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(225)))), ((int)(((byte)(241)))));
            this.txtCambio.BorderRadius = 10;
            this.txtCambio.BorderSize = 2;
            this.txtCambio.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCambio.ForeColor = System.Drawing.Color.White;
            this.txtCambio.IsReadOnly = true;
            this.txtCambio.Location = new System.Drawing.Point(729, 453);
            this.txtCambio.Margin = new System.Windows.Forms.Padding(4);
            this.txtCambio.Multiline = false;
            this.txtCambio.Name = "txtCambio";
            this.txtCambio.Padding = new System.Windows.Forms.Padding(10, 7, 10, 7);
            this.txtCambio.PasswordChar = false;
            this.txtCambio.PlaceholderColor = System.Drawing.Color.White;
            this.txtCambio.PlaceholderText = "0.00";
            this.txtCambio.ShortcutsEnabled = false;
            this.txtCambio.Size = new System.Drawing.Size(106, 36);
            this.txtCambio.TabIndex = 10;
            this.txtCambio.Texts = "";
            this.txtCambio.UnderlinedStyle = false;
            // 
            // txtPaga
            // 
            this.txtPaga.BackColor = System.Drawing.Color.Black;
            this.txtPaga.BorderColor = System.Drawing.Color.MediumSlateBlue;
            this.txtPaga.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(225)))), ((int)(((byte)(241)))));
            this.txtPaga.BorderRadius = 10;
            this.txtPaga.BorderSize = 2;
            this.txtPaga.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPaga.ForeColor = System.Drawing.Color.White;
            this.txtPaga.IsReadOnly = false;
            this.txtPaga.Location = new System.Drawing.Point(729, 389);
            this.txtPaga.Margin = new System.Windows.Forms.Padding(4);
            this.txtPaga.Multiline = false;
            this.txtPaga.Name = "txtPaga";
            this.txtPaga.Padding = new System.Windows.Forms.Padding(10, 7, 10, 7);
            this.txtPaga.PasswordChar = false;
            this.txtPaga.PlaceholderColor = System.Drawing.Color.White;
            this.txtPaga.PlaceholderText = "0.00";
            this.txtPaga.ShortcutsEnabled = false;
            this.txtPaga.Size = new System.Drawing.Size(106, 36);
            this.txtPaga.TabIndex = 9;
            this.txtPaga.Texts = "";
            this.txtPaga.UnderlinedStyle = false;
            this.txtPaga.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPaga_KeyDown);
            this.txtPaga.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPaga_KeyPress);
            // 
            // txtTotal
            // 
            this.txtTotal.BackColor = System.Drawing.Color.Black;
            this.txtTotal.BorderColor = System.Drawing.Color.MediumSlateBlue;
            this.txtTotal.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(225)))), ((int)(((byte)(241)))));
            this.txtTotal.BorderRadius = 10;
            this.txtTotal.BorderSize = 2;
            this.txtTotal.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTotal.ForeColor = System.Drawing.Color.White;
            this.txtTotal.IsReadOnly = true;
            this.txtTotal.Location = new System.Drawing.Point(728, 320);
            this.txtTotal.Margin = new System.Windows.Forms.Padding(4);
            this.txtTotal.Multiline = false;
            this.txtTotal.Name = "txtTotal";
            this.txtTotal.Padding = new System.Windows.Forms.Padding(10, 7, 10, 7);
            this.txtTotal.PasswordChar = false;
            this.txtTotal.PlaceholderColor = System.Drawing.Color.White;
            this.txtTotal.PlaceholderText = "0.00";
            this.txtTotal.ShortcutsEnabled = false;
            this.txtTotal.Size = new System.Drawing.Size(106, 36);
            this.txtTotal.TabIndex = 8;
            this.txtTotal.Texts = "";
            this.txtTotal.UnderlinedStyle = false;
            // 
            // rjButton1
            // 
            this.rjButton1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.rjButton1.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.rjButton1.BorderColor = System.Drawing.Color.MediumSlateBlue;
            this.rjButton1.BorderRadius = 20;
            this.rjButton1.BorderSize = 2;
            this.rjButton1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.rjButton1.FlatAppearance.BorderSize = 0;
            this.rjButton1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rjButton1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            this.rjButton1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(243)))), ((int)(((byte)(243)))));
            this.rjButton1.Image = global::CapaPresentacion.Properties.Resources.cajero_automatico;
            this.rjButton1.Location = new System.Drawing.Point(718, 524);
            this.rjButton1.Margin = new System.Windows.Forms.Padding(2);
            this.rjButton1.Name = "rjButton1";
            this.rjButton1.Size = new System.Drawing.Size(130, 47);
            this.rjButton1.TabIndex = 77;
            this.rjButton1.Text = "Registrar";
            this.rjButton1.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(243)))), ((int)(((byte)(243)))));
            this.rjButton1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.rjButton1.UseVisualStyleBackColor = false;
            this.rjButton1.Click += new System.EventHandler(this.btnRegistrar_Click);
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
            this.rjTextBox2.Location = new System.Drawing.Point(25, 22);
            this.rjTextBox2.Margin = new System.Windows.Forms.Padding(4);
            this.rjTextBox2.Multiline = true;
            this.rjTextBox2.Name = "rjTextBox2";
            this.rjTextBox2.Padding = new System.Windows.Forms.Padding(10, 7, 10, 7);
            this.rjTextBox2.PasswordChar = false;
            this.rjTextBox2.PlaceholderColor = System.Drawing.Color.DarkGray;
            this.rjTextBox2.PlaceholderText = "";
            this.rjTextBox2.ShortcutsEnabled = true;
            this.rjTextBox2.Size = new System.Drawing.Size(874, 585);
            this.rjTextBox2.TabIndex = 95;
            this.rjTextBox2.Texts = "";
            this.rjTextBox2.UnderlinedStyle = false;
            // 
            // FrmVentas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(902, 620);
            this.Controls.Add(this.txtCupon);
            this.Controls.Add(this.txtCambio);
            this.Controls.Add(this.txtPaga);
            this.Controls.Add(this.txtTotal);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.rjButton1);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.dgvData);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.rjTextBox2);
            this.Name = "FrmVentas";
            this.Text = "FrmVentas";
            this.Load += new System.EventHandler(this.FrmVentas_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtCantidad)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private CustomControls.RJControls.RJButton BtnAgregar;
        private System.Windows.Forms.ComboBox comboDocumento;
        private System.Windows.Forms.TextBox txtIdProd;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label10;
        private CustomControls.RJControls.RJButton rjButton1;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.DataGridView dgvData;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.NumericUpDown txtCantidad;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtIdCliente;
        private System.Windows.Forms.GroupBox groupBox3;
        private CustomControls.RJControls.RJTextBox txtCod;
        private CustomControls.RJControls.RJTextBox txtPrecio;
        private CustomControls.RJControls.RJTextBox txtProducto;
        private CustomControls.RJControls.RJTextBox txtNombreC;
        private CustomControls.RJControls.RJTextBox txtStock;
        private CustomControls.RJControls.RJTextBox txtFecha;
        private CustomControls.RJControls.RJTextBox txtDocumento;
        private CustomControls.RJControls.RJTextBox txtTotal;
        private CustomControls.RJControls.RJTextBox txtPaga;
        private CustomControls.RJControls.RJTextBox txtCambio;
        private CustomControls.RJControls.RJTextBox rjTextBox2;
        private CustomControls.RJControls.RJButton btnBuscarProd;
        private CustomControls.RJControls.RJButton rjButton2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DataGridViewTextBoxColumn IdProducto;
        private System.Windows.Forms.DataGridViewTextBoxColumn Producto;
        private System.Windows.Forms.DataGridViewTextBoxColumn Descripcion;
        private System.Windows.Forms.DataGridViewTextBoxColumn Precio;
        private System.Windows.Forms.DataGridViewTextBoxColumn Cantidad;
        private System.Windows.Forms.DataGridViewTextBoxColumn SubTotal;
        private System.Windows.Forms.DataGridViewButtonColumn btnEliminar;
        private System.Windows.Forms.TextBox txtCupon;
    }
}