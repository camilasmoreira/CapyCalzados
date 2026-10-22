namespace CapaPresentacion
{
    partial class FrmDetalleCompra
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
            this.Producto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Descripcion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PrecioCompra = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Cantidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SubTotal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtEstado = new CustomControls.RJControls.RJTextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtBusqueda = new CustomControls.RJControls.RJTextBox();
            this.rjButton1 = new CustomControls.RJControls.RJButton();
            this.BtnLimpiar2 = new CustomControls.RJControls.RJButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtDocV = new CustomControls.RJControls.RJTextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtFecha = new CustomControls.RJControls.RJTextBox();
            this.txtUsuario = new CustomControls.RJControls.RJTextBox();
            this.txtTipoDocumento = new CustomControls.RJControls.RJTextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtCorreoP = new CustomControls.RJControls.RJTextBox();
            this.txtRazon = new CustomControls.RJControls.RJTextBox();
            this.txtDocumentoProv = new CustomControls.RJControls.RJTextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.btnValidar = new CustomControls.RJControls.RJButton();
            this.txtDocumento = new CustomControls.RJControls.RJTextBox();
            this.txtTotal = new CustomControls.RJControls.RJTextBox();
            this.BtnGuardar2 = new CustomControls.RJControls.RJButton();
            this.rjTextBox2 = new CustomControls.RJControls.RJTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
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
            this.Producto,
            this.Descripcion,
            this.PrecioCompra,
            this.Cantidad,
            this.SubTotal});
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
            this.dgvData.Location = new System.Drawing.Point(107, 321);
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
            this.dgvData.Size = new System.Drawing.Size(710, 192);
            this.dgvData.TabIndex = 60;
            // 
            // Producto
            // 
            this.Producto.HeaderText = "Producto";
            this.Producto.Name = "Producto";
            this.Producto.ReadOnly = true;
            this.Producto.Width = 150;
            // 
            // Descripcion
            // 
            this.Descripcion.HeaderText = "Descripcion";
            this.Descripcion.Name = "Descripcion";
            this.Descripcion.ReadOnly = true;
            this.Descripcion.Width = 200;
            // 
            // PrecioCompra
            // 
            this.PrecioCompra.HeaderText = "Precio Compra";
            this.PrecioCompra.Name = "PrecioCompra";
            this.PrecioCompra.ReadOnly = true;
            this.PrecioCompra.Width = 130;
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
            this.SubTotal.Width = 127;
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.txtEstado);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.txtBusqueda);
            this.groupBox1.Controls.Add(this.rjButton1);
            this.groupBox1.Controls.Add(this.BtnLimpiar2);
            this.groupBox1.ForeColor = System.Drawing.Color.White;
            this.groupBox1.Location = new System.Drawing.Point(107, 48);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(710, 80);
            this.groupBox1.TabIndex = 108;
            this.groupBox1.TabStop = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.label6.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(608, 12);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 17);
            this.label6.TabIndex = 111;
            this.label6.Text = "Estado:";
            // 
            // txtEstado
            // 
            this.txtEstado.BackColor = System.Drawing.Color.Black;
            this.txtEstado.BorderColor = System.Drawing.Color.MediumSlateBlue;
            this.txtEstado.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(225)))), ((int)(((byte)(241)))));
            this.txtEstado.BorderRadius = 10;
            this.txtEstado.BorderSize = 2;
            this.txtEstado.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.txtEstado.ForeColor = System.Drawing.Color.White;
            this.txtEstado.IsReadOnly = true;
            this.txtEstado.Location = new System.Drawing.Point(575, 30);
            this.txtEstado.Margin = new System.Windows.Forms.Padding(4);
            this.txtEstado.Multiline = false;
            this.txtEstado.Name = "txtEstado";
            this.txtEstado.Padding = new System.Windows.Forms.Padding(10, 7, 10, 7);
            this.txtEstado.PasswordChar = false;
            this.txtEstado.PlaceholderColor = System.Drawing.Color.White;
            this.txtEstado.PlaceholderText = "";
            this.txtEstado.ShortcutsEnabled = true;
            this.txtEstado.Size = new System.Drawing.Size(115, 36);
            this.txtEstado.TabIndex = 113;
            this.txtEstado.Texts = "";
            this.txtEstado.UnderlinedStyle = false;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.label7.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(22, 12);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(114, 17);
            this.label7.TabIndex = 105;
            this.label7.Text = "Numero Compra:";
            // 
            // txtBusqueda
            // 
            this.txtBusqueda.BackColor = System.Drawing.Color.Black;
            this.txtBusqueda.BorderColor = System.Drawing.Color.MediumSlateBlue;
            this.txtBusqueda.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(225)))), ((int)(((byte)(241)))));
            this.txtBusqueda.BorderRadius = 10;
            this.txtBusqueda.BorderSize = 2;
            this.txtBusqueda.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.txtBusqueda.ForeColor = System.Drawing.Color.White;
            this.txtBusqueda.IsReadOnly = false;
            this.txtBusqueda.Location = new System.Drawing.Point(19, 30);
            this.txtBusqueda.Margin = new System.Windows.Forms.Padding(4);
            this.txtBusqueda.Multiline = false;
            this.txtBusqueda.Name = "txtBusqueda";
            this.txtBusqueda.Padding = new System.Windows.Forms.Padding(10, 7, 10, 7);
            this.txtBusqueda.PasswordChar = false;
            this.txtBusqueda.PlaceholderColor = System.Drawing.Color.White;
            this.txtBusqueda.PlaceholderText = "Numero Compra";
            this.txtBusqueda.ShortcutsEnabled = true;
            this.txtBusqueda.Size = new System.Drawing.Size(180, 36);
            this.txtBusqueda.TabIndex = 99;
            this.txtBusqueda.Texts = "";
            this.txtBusqueda.UnderlinedStyle = false;
            this.txtBusqueda.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBusqueda_KeyDown);
            this.txtBusqueda.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtBusqueda_KeyPress);
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
            this.rjButton1.Font = new System.Drawing.Font("Microsoft JhengHei UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rjButton1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(243)))), ((int)(((byte)(243)))));
            this.rjButton1.Image = global::CapaPresentacion.Properties.Resources.buscar__1_;
            this.rjButton1.Location = new System.Drawing.Point(320, 23);
            this.rjButton1.Margin = new System.Windows.Forms.Padding(2);
            this.rjButton1.Name = "rjButton1";
            this.rjButton1.Size = new System.Drawing.Size(109, 43);
            this.rjButton1.TabIndex = 65;
            this.rjButton1.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(243)))), ((int)(((byte)(243)))));
            this.rjButton1.UseVisualStyleBackColor = false;
            this.rjButton1.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // BtnLimpiar2
            // 
            this.BtnLimpiar2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.BtnLimpiar2.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.BtnLimpiar2.BorderColor = System.Drawing.Color.MediumSlateBlue;
            this.BtnLimpiar2.BorderRadius = 20;
            this.BtnLimpiar2.BorderSize = 2;
            this.BtnLimpiar2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnLimpiar2.FlatAppearance.BorderSize = 0;
            this.BtnLimpiar2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnLimpiar2.Font = new System.Drawing.Font("Microsoft JhengHei UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnLimpiar2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(243)))), ((int)(((byte)(243)))));
            this.BtnLimpiar2.Image = global::CapaPresentacion.Properties.Resources.escoba;
            this.BtnLimpiar2.Location = new System.Drawing.Point(445, 23);
            this.BtnLimpiar2.Margin = new System.Windows.Forms.Padding(2);
            this.BtnLimpiar2.Name = "BtnLimpiar2";
            this.BtnLimpiar2.Size = new System.Drawing.Size(109, 43);
            this.BtnLimpiar2.TabIndex = 64;
            this.BtnLimpiar2.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(243)))), ((int)(((byte)(243)))));
            this.BtnLimpiar2.UseVisualStyleBackColor = false;
            this.BtnLimpiar2.Click += new System.EventHandler(this.btnLimpiar_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.txtDocV);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.txtFecha);
            this.groupBox2.Controls.Add(this.txtUsuario);
            this.groupBox2.Controls.Add(this.txtTipoDocumento);
            this.groupBox2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            this.groupBox2.ForeColor = System.Drawing.Color.White;
            this.groupBox2.Location = new System.Drawing.Point(107, 134);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(710, 89);
            this.groupBox2.TabIndex = 109;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Informacion Compra";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.label11.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(572, 24);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(100, 17);
            this.label11.TabIndex = 110;
            this.label11.Text = "Doc Vendedor:";
            // 
            // txtDocV
            // 
            this.txtDocV.BackColor = System.Drawing.Color.Black;
            this.txtDocV.BorderColor = System.Drawing.Color.MediumSlateBlue;
            this.txtDocV.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(225)))), ((int)(((byte)(241)))));
            this.txtDocV.BorderRadius = 10;
            this.txtDocV.BorderSize = 2;
            this.txtDocV.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDocV.ForeColor = System.Drawing.Color.White;
            this.txtDocV.IsReadOnly = true;
            this.txtDocV.Location = new System.Drawing.Point(566, 40);
            this.txtDocV.Margin = new System.Windows.Forms.Padding(4);
            this.txtDocV.Multiline = false;
            this.txtDocV.Name = "txtDocV";
            this.txtDocV.Padding = new System.Windows.Forms.Padding(10, 7, 10, 7);
            this.txtDocV.PasswordChar = false;
            this.txtDocV.PlaceholderColor = System.Drawing.Color.White;
            this.txtDocV.PlaceholderText = "Documento";
            this.txtDocV.ShortcutsEnabled = true;
            this.txtDocV.Size = new System.Drawing.Size(114, 36);
            this.txtDocV.TabIndex = 109;
            this.txtDocV.Texts = "";
            this.txtDocV.UnderlinedStyle = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.label5.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(400, 24);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(72, 17);
            this.label5.TabIndex = 108;
            this.label5.Text = "Vendedor:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.label3.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(214, 24);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(116, 17);
            this.label3.TabIndex = 106;
            this.label3.Text = "Tipo Documento:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(16, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(118, 17);
            this.label1.TabIndex = 104;
            this.label1.Text = "Fecha de Compra:";
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
            this.txtFecha.Location = new System.Drawing.Point(12, 39);
            this.txtFecha.Margin = new System.Windows.Forms.Padding(4);
            this.txtFecha.Multiline = false;
            this.txtFecha.Name = "txtFecha";
            this.txtFecha.Padding = new System.Windows.Forms.Padding(10, 7, 10, 7);
            this.txtFecha.PasswordChar = false;
            this.txtFecha.PlaceholderColor = System.Drawing.Color.White;
            this.txtFecha.PlaceholderText = "Fecha";
            this.txtFecha.ShortcutsEnabled = true;
            this.txtFecha.Size = new System.Drawing.Size(157, 36);
            this.txtFecha.TabIndex = 102;
            this.txtFecha.Texts = "";
            this.txtFecha.UnderlinedStyle = false;
            // 
            // txtUsuario
            // 
            this.txtUsuario.BackColor = System.Drawing.Color.Black;
            this.txtUsuario.BorderColor = System.Drawing.Color.MediumSlateBlue;
            this.txtUsuario.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(225)))), ((int)(((byte)(241)))));
            this.txtUsuario.BorderRadius = 10;
            this.txtUsuario.BorderSize = 2;
            this.txtUsuario.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.txtUsuario.ForeColor = System.Drawing.Color.White;
            this.txtUsuario.IsReadOnly = true;
            this.txtUsuario.Location = new System.Drawing.Point(398, 40);
            this.txtUsuario.Margin = new System.Windows.Forms.Padding(4);
            this.txtUsuario.Multiline = false;
            this.txtUsuario.Name = "txtUsuario";
            this.txtUsuario.Padding = new System.Windows.Forms.Padding(10, 7, 10, 7);
            this.txtUsuario.PasswordChar = false;
            this.txtUsuario.PlaceholderColor = System.Drawing.Color.White;
            this.txtUsuario.PlaceholderText = "Vendedor";
            this.txtUsuario.ShortcutsEnabled = true;
            this.txtUsuario.Size = new System.Drawing.Size(149, 36);
            this.txtUsuario.TabIndex = 101;
            this.txtUsuario.Texts = "";
            this.txtUsuario.UnderlinedStyle = false;
            // 
            // txtTipoDocumento
            // 
            this.txtTipoDocumento.BackColor = System.Drawing.Color.Black;
            this.txtTipoDocumento.BorderColor = System.Drawing.Color.MediumSlateBlue;
            this.txtTipoDocumento.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(225)))), ((int)(((byte)(241)))));
            this.txtTipoDocumento.BorderRadius = 10;
            this.txtTipoDocumento.BorderSize = 2;
            this.txtTipoDocumento.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.txtTipoDocumento.ForeColor = System.Drawing.Color.White;
            this.txtTipoDocumento.IsReadOnly = true;
            this.txtTipoDocumento.Location = new System.Drawing.Point(206, 40);
            this.txtTipoDocumento.Margin = new System.Windows.Forms.Padding(4);
            this.txtTipoDocumento.Multiline = false;
            this.txtTipoDocumento.Name = "txtTipoDocumento";
            this.txtTipoDocumento.Padding = new System.Windows.Forms.Padding(10, 7, 10, 7);
            this.txtTipoDocumento.PasswordChar = false;
            this.txtTipoDocumento.PlaceholderColor = System.Drawing.Color.White;
            this.txtTipoDocumento.PlaceholderText = "Tipo Documento";
            this.txtTipoDocumento.ShortcutsEnabled = true;
            this.txtTipoDocumento.Size = new System.Drawing.Size(158, 36);
            this.txtTipoDocumento.TabIndex = 100;
            this.txtTipoDocumento.Texts = "";
            this.txtTipoDocumento.UnderlinedStyle = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.label4.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(214, 23);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(89, 17);
            this.label4.TabIndex = 107;
            this.label4.Text = "Razon Social:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(16, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(84, 17);
            this.label2.TabIndex = 105;
            this.label2.Text = "Documento:";
            // 
            // groupBox3
            // 
            this.groupBox3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.groupBox3.Controls.Add(this.label10);
            this.groupBox3.Controls.Add(this.txtCorreoP);
            this.groupBox3.Controls.Add(this.txtRazon);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.txtDocumentoProv);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            this.groupBox3.ForeColor = System.Drawing.Color.White;
            this.groupBox3.Location = new System.Drawing.Point(107, 228);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(710, 86);
            this.groupBox3.TabIndex = 110;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Informacion Proveedor";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.label10.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(406, 23);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(121, 17);
            this.label10.TabIndex = 109;
            this.label10.Text = "Correo Proveedor:";
            // 
            // txtCorreoP
            // 
            this.txtCorreoP.BackColor = System.Drawing.Color.Black;
            this.txtCorreoP.BorderColor = System.Drawing.Color.MediumSlateBlue;
            this.txtCorreoP.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(225)))), ((int)(((byte)(241)))));
            this.txtCorreoP.BorderRadius = 10;
            this.txtCorreoP.BorderSize = 2;
            this.txtCorreoP.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCorreoP.ForeColor = System.Drawing.Color.White;
            this.txtCorreoP.IsReadOnly = true;
            this.txtCorreoP.Location = new System.Drawing.Point(398, 38);
            this.txtCorreoP.Margin = new System.Windows.Forms.Padding(4);
            this.txtCorreoP.Multiline = false;
            this.txtCorreoP.Name = "txtCorreoP";
            this.txtCorreoP.Padding = new System.Windows.Forms.Padding(10, 7, 10, 7);
            this.txtCorreoP.PasswordChar = false;
            this.txtCorreoP.PlaceholderColor = System.Drawing.Color.White;
            this.txtCorreoP.PlaceholderText = "Correo Proveedor";
            this.txtCorreoP.ShortcutsEnabled = true;
            this.txtCorreoP.Size = new System.Drawing.Size(274, 36);
            this.txtCorreoP.TabIndex = 108;
            this.txtCorreoP.Texts = "";
            this.txtCorreoP.UnderlinedStyle = false;
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
            this.txtRazon.Location = new System.Drawing.Point(206, 38);
            this.txtRazon.Margin = new System.Windows.Forms.Padding(4);
            this.txtRazon.Multiline = false;
            this.txtRazon.Name = "txtRazon";
            this.txtRazon.Padding = new System.Windows.Forms.Padding(10, 7, 10, 7);
            this.txtRazon.PasswordChar = false;
            this.txtRazon.PlaceholderColor = System.Drawing.Color.White;
            this.txtRazon.PlaceholderText = "Razon Social";
            this.txtRazon.ShortcutsEnabled = true;
            this.txtRazon.Size = new System.Drawing.Size(158, 36);
            this.txtRazon.TabIndex = 103;
            this.txtRazon.Texts = "";
            this.txtRazon.UnderlinedStyle = false;
            // 
            // txtDocumentoProv
            // 
            this.txtDocumentoProv.BackColor = System.Drawing.Color.Black;
            this.txtDocumentoProv.BorderColor = System.Drawing.Color.MediumSlateBlue;
            this.txtDocumentoProv.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(225)))), ((int)(((byte)(241)))));
            this.txtDocumentoProv.BorderRadius = 10;
            this.txtDocumentoProv.BorderSize = 2;
            this.txtDocumentoProv.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.txtDocumentoProv.ForeColor = System.Drawing.Color.White;
            this.txtDocumentoProv.IsReadOnly = true;
            this.txtDocumentoProv.Location = new System.Drawing.Point(12, 38);
            this.txtDocumentoProv.Margin = new System.Windows.Forms.Padding(4);
            this.txtDocumentoProv.Multiline = false;
            this.txtDocumentoProv.Name = "txtDocumentoProv";
            this.txtDocumentoProv.Padding = new System.Windows.Forms.Padding(10, 7, 10, 7);
            this.txtDocumentoProv.PasswordChar = false;
            this.txtDocumentoProv.PlaceholderColor = System.Drawing.Color.White;
            this.txtDocumentoProv.PlaceholderText = "Documento";
            this.txtDocumentoProv.ShortcutsEnabled = true;
            this.txtDocumentoProv.Size = new System.Drawing.Size(157, 36);
            this.txtDocumentoProv.TabIndex = 102;
            this.txtDocumentoProv.Texts = "";
            this.txtDocumentoProv.UnderlinedStyle = false;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(43)))), ((int)(((byte)(59)))));
            this.label9.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.White;
            this.label9.Location = new System.Drawing.Point(116, 528);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(88, 17);
            this.label9.TabIndex = 111;
            this.label9.Text = "Monto Total:";
            // 
            // btnValidar
            // 
            this.btnValidar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.btnValidar.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.btnValidar.BorderColor = System.Drawing.Color.MediumSlateBlue;
            this.btnValidar.BorderRadius = 20;
            this.btnValidar.BorderSize = 2;
            this.btnValidar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnValidar.FlatAppearance.BorderSize = 0;
            this.btnValidar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnValidar.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnValidar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(243)))), ((int)(((byte)(243)))));
            this.btnValidar.Image = global::CapaPresentacion.Properties.Resources.comprobado;
            this.btnValidar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnValidar.Location = new System.Drawing.Point(505, 542);
            this.btnValidar.Margin = new System.Windows.Forms.Padding(2);
            this.btnValidar.Name = "btnValidar";
            this.btnValidar.Size = new System.Drawing.Size(156, 49);
            this.btnValidar.TabIndex = 112;
            this.btnValidar.Text = "Validar";
            this.btnValidar.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(243)))), ((int)(((byte)(243)))));
            this.btnValidar.UseVisualStyleBackColor = false;
            this.btnValidar.Click += new System.EventHandler(this.rjButton2_Click);
            // 
            // txtDocumento
            // 
            this.txtDocumento.BackColor = System.Drawing.Color.Black;
            this.txtDocumento.BorderColor = System.Drawing.Color.MediumSlateBlue;
            this.txtDocumento.BorderFocusColor = System.Drawing.Color.HotPink;
            this.txtDocumento.BorderRadius = 10;
            this.txtDocumento.BorderSize = 2;
            this.txtDocumento.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDocumento.ForeColor = System.Drawing.Color.White;
            this.txtDocumento.IsReadOnly = true;
            this.txtDocumento.Location = new System.Drawing.Point(266, 554);
            this.txtDocumento.Margin = new System.Windows.Forms.Padding(4);
            this.txtDocumento.Multiline = false;
            this.txtDocumento.Name = "txtDocumento";
            this.txtDocumento.Padding = new System.Windows.Forms.Padding(10, 7, 10, 7);
            this.txtDocumento.PasswordChar = false;
            this.txtDocumento.PlaceholderColor = System.Drawing.Color.DarkGray;
            this.txtDocumento.PlaceholderText = "";
            this.txtDocumento.ShortcutsEnabled = true;
            this.txtDocumento.Size = new System.Drawing.Size(40, 31);
            this.txtDocumento.TabIndex = 104;
            this.txtDocumento.Texts = "";
            this.txtDocumento.UnderlinedStyle = false;
            this.txtDocumento.Visible = false;
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
            this.txtTotal.Location = new System.Drawing.Point(113, 549);
            this.txtTotal.Margin = new System.Windows.Forms.Padding(4);
            this.txtTotal.Multiline = false;
            this.txtTotal.Name = "txtTotal";
            this.txtTotal.Padding = new System.Windows.Forms.Padding(10, 7, 10, 7);
            this.txtTotal.PasswordChar = false;
            this.txtTotal.PlaceholderColor = System.Drawing.Color.White;
            this.txtTotal.PlaceholderText = "0.00";
            this.txtTotal.ShortcutsEnabled = true;
            this.txtTotal.Size = new System.Drawing.Size(122, 36);
            this.txtTotal.TabIndex = 105;
            this.txtTotal.Texts = "";
            this.txtTotal.UnderlinedStyle = false;
            // 
            // BtnGuardar2
            // 
            this.BtnGuardar2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.BtnGuardar2.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.BtnGuardar2.BorderColor = System.Drawing.Color.MediumSlateBlue;
            this.BtnGuardar2.BorderRadius = 20;
            this.BtnGuardar2.BorderSize = 2;
            this.BtnGuardar2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnGuardar2.FlatAppearance.BorderSize = 0;
            this.BtnGuardar2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnGuardar2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnGuardar2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(243)))), ((int)(((byte)(243)))));
            this.BtnGuardar2.Image = global::CapaPresentacion.Properties.Resources.pdf;
            this.BtnGuardar2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.BtnGuardar2.Location = new System.Drawing.Point(682, 547);
            this.BtnGuardar2.Margin = new System.Windows.Forms.Padding(2);
            this.BtnGuardar2.Name = "BtnGuardar2";
            this.BtnGuardar2.Size = new System.Drawing.Size(135, 38);
            this.BtnGuardar2.TabIndex = 63;
            this.BtnGuardar2.Text = "Descargar PDF";
            this.BtnGuardar2.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(243)))), ((int)(((byte)(243)))));
            this.BtnGuardar2.UseVisualStyleBackColor = false;
            this.BtnGuardar2.Click += new System.EventHandler(this.btnDescargar_Click);
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
            this.rjTextBox2.TabIndex = 106;
            this.rjTextBox2.Texts = "";
            this.rjTextBox2.UnderlinedStyle = false;
            // 
            // FrmDetalleCompra
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(902, 620);
            this.Controls.Add(this.btnValidar);
            this.Controls.Add(this.txtDocumento);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.txtTotal);
            this.Controls.Add(this.BtnGuardar2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.dgvData);
            this.Controls.Add(this.rjTextBox2);
            this.Name = "FrmDetalleCompra";
            this.Text = "FrmDetalleCompra";
            this.Load += new System.EventHandler(this.FrmDetalleCompra_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private CustomControls.RJControls.RJButton BtnGuardar2;
        private System.Windows.Forms.DataGridView dgvData;
        private CustomControls.RJControls.RJTextBox txtDocumento;
        private CustomControls.RJControls.RJTextBox txtRazon;
        private CustomControls.RJControls.RJTextBox txtFecha;
        private CustomControls.RJControls.RJTextBox txtUsuario;
        private CustomControls.RJControls.RJTextBox txtTipoDocumento;
        private CustomControls.RJControls.RJTextBox txtBusqueda;
        private CustomControls.RJControls.RJTextBox txtDocumentoProv;
        private CustomControls.RJControls.RJTextBox txtTotal;
        private CustomControls.RJControls.RJTextBox rjTextBox2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private CustomControls.RJControls.RJButton rjButton1;
        private CustomControls.RJControls.RJButton BtnLimpiar2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label11;
        private CustomControls.RJControls.RJTextBox txtDocV;
        private System.Windows.Forms.Label label10;
        private CustomControls.RJControls.RJTextBox txtCorreoP;
        private System.Windows.Forms.DataGridViewTextBoxColumn Producto;
        private System.Windows.Forms.DataGridViewTextBoxColumn Descripcion;
        private System.Windows.Forms.DataGridViewTextBoxColumn PrecioCompra;
        private System.Windows.Forms.DataGridViewTextBoxColumn Cantidad;
        private System.Windows.Forms.DataGridViewTextBoxColumn SubTotal;
        private CustomControls.RJControls.RJButton btnValidar;
        private CustomControls.RJControls.RJTextBox txtEstado;
        private System.Windows.Forms.Label label6;
    }
}