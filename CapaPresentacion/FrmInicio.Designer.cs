namespace CapaPresentacion
{
    partial class FrmInicio
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.Btn2 = new CustomControls.RJControls.RJButton();
            this.Btn3 = new CustomControls.RJControls.RJButton();
            this.Btn4 = new CustomControls.RJControls.RJButton();
            this.Btn1 = new CustomControls.RJControls.RJButton();
            this.Btn5 = new CustomControls.RJControls.RJButton();
            this.Btn6 = new CustomControls.RJControls.RJButton();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.lblSubtitulo = new System.Windows.Forms.Label();
            this.lblBanner = new System.Windows.Forms.Label();
            this.lblSeparador = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitulo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(248)))), ((int)(((byte)(240)))));
            this.lblTitulo.Location = new System.Drawing.Point(50, 22);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(215, 30);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "PANEL PRINCIPAL";
            // 
            // lblSubtitulo
            // 
            this.lblSubtitulo.AutoSize = true;
            this.lblSubtitulo.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSubtitulo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(130)))), ((int)(((byte)(43)))));
            this.lblSubtitulo.Location = new System.Drawing.Point(52, 52);
            this.lblSubtitulo.Name = "lblSubtitulo";
            this.lblSubtitulo.Size = new System.Drawing.Size(370, 17);
            this.lblSubtitulo.TabIndex = 1;
            this.lblSubtitulo.Text = "Resumen en tiempo real de operaciones, inventario y ventas";
            // 
            // lblBanner
            // 
            this.lblBanner.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblBanner.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBanner.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(216)))), ((int)(((byte)(202)))), ((int)(((byte)(184)))));
            this.lblBanner.Location = new System.Drawing.Point(520, 28);
            this.lblBanner.Name = "lblBanner";
            this.lblBanner.Size = new System.Drawing.Size(330, 24);
            this.lblBanner.TabIndex = 2;
            this.lblBanner.Text = "CapyCalzados • Gestión Integral de Calzados";
            this.lblBanner.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblSeparador
            // 
            this.lblSeparador.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSeparador.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(50)))), ((int)(((byte)(38)))));
            this.lblSeparador.Location = new System.Drawing.Point(50, 78);
            this.lblSeparador.Name = "lblSeparador";
            this.lblSeparador.Size = new System.Drawing.Size(800, 1);
            this.lblSeparador.TabIndex = 3;
            // 
            // Btn2
            // 
            this.Btn2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(29)))), ((int)(((byte)(23)))));
            this.Btn2.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(29)))), ((int)(((byte)(23)))));
            this.Btn2.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(65)))), ((int)(((byte)(48)))));
            this.Btn2.BorderRadius = 12;
            this.Btn2.BorderSize = 1;
            this.Btn2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Btn2.FlatAppearance.BorderSize = 0;
            this.Btn2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Btn2.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold);
            this.Btn2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(245)))), ((int)(((byte)(238)))));
            this.Btn2.Image = global::CapaPresentacion.Properties.Resources.etiqueta_de_precio;
            this.Btn2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Btn2.Location = new System.Drawing.Point(50, 95);
            this.Btn2.Name = "Btn2";
            this.Btn2.Padding = new System.Windows.Forms.Padding(18, 0, 15, 0);
            this.Btn2.Size = new System.Drawing.Size(380, 145);
            this.Btn2.TabIndex = 4;
            this.Btn2.Text = "CALZADOS EN STOCK\r\n\r\n0\r\n\r\nModelos activos en catálogo";
            this.Btn2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.Btn2.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(245)))), ((int)(((byte)(238)))));
            this.Btn2.UseVisualStyleBackColor = false;
            // 
            // Btn6
            // 
            this.Btn6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(29)))), ((int)(((byte)(23)))));
            this.Btn6.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(29)))), ((int)(((byte)(23)))));
            this.Btn6.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(65)))), ((int)(((byte)(48)))));
            this.Btn6.BorderRadius = 12;
            this.Btn6.BorderSize = 1;
            this.Btn6.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Btn6.FlatAppearance.BorderSize = 0;
            this.Btn6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Btn6.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold);
            this.Btn6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(245)))), ((int)(((byte)(238)))));
            this.Btn6.Image = global::CapaPresentacion.Properties.Resources.entrega;
            this.Btn6.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Btn6.Location = new System.Drawing.Point(470, 95);
            this.Btn6.Name = "Btn6";
            this.Btn6.Padding = new System.Windows.Forms.Padding(18, 0, 15, 0);
            this.Btn6.Size = new System.Drawing.Size(380, 145);
            this.Btn6.TabIndex = 5;
            this.Btn6.Text = "PROVEEDORES ACTIVOS\r\n\r\n0\r\n\r\nFábricas y distribuidores";
            this.Btn6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.Btn6.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(245)))), ((int)(((byte)(238)))));
            this.Btn6.UseVisualStyleBackColor = false;
            // 
            // Btn1
            // 
            this.Btn1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(29)))), ((int)(((byte)(23)))));
            this.Btn1.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(29)))), ((int)(((byte)(23)))));
            this.Btn1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(65)))), ((int)(((byte)(48)))));
            this.Btn1.BorderRadius = 12;
            this.Btn1.BorderSize = 1;
            this.Btn1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Btn1.FlatAppearance.BorderSize = 0;
            this.Btn1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Btn1.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold);
            this.Btn1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(245)))), ((int)(((byte)(238)))));
            this.Btn1.Image = global::CapaPresentacion.Properties.Resources.carrito_de_compras;
            this.Btn1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Btn1.Location = new System.Drawing.Point(50, 260);
            this.Btn1.Name = "Btn1";
            this.Btn1.Padding = new System.Windows.Forms.Padding(18, 0, 15, 0);
            this.Btn1.Size = new System.Drawing.Size(380, 145);
            this.Btn1.TabIndex = 6;
            this.Btn1.Text = "VENTAS REALIZADAS\r\n\r\n0\r\n\r\nComprobantes emitidos";
            this.Btn1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.Btn1.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(245)))), ((int)(((byte)(238)))));
            this.Btn1.UseVisualStyleBackColor = false;
            // 
            // Btn5
            // 
            this.Btn5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(29)))), ((int)(((byte)(23)))));
            this.Btn5.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(29)))), ((int)(((byte)(23)))));
            this.Btn5.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(65)))), ((int)(((byte)(48)))));
            this.Btn5.BorderRadius = 12;
            this.Btn5.BorderSize = 1;
            this.Btn5.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Btn5.FlatAppearance.BorderSize = 0;
            this.Btn5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Btn5.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold);
            this.Btn5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(245)))), ((int)(((byte)(238)))));
            this.Btn5.Image = global::CapaPresentacion.Properties.Resources.caja_de_entrega;
            this.Btn5.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Btn5.Location = new System.Drawing.Point(470, 260);
            this.Btn5.Name = "Btn5";
            this.Btn5.Padding = new System.Windows.Forms.Padding(18, 0, 15, 0);
            this.Btn5.Size = new System.Drawing.Size(380, 145);
            this.Btn5.TabIndex = 7;
            this.Btn5.Text = "COMPRAS A PROVEEDORES\r\n\r\n0\r\n\r\nÓrdenes de calzado";
            this.Btn5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.Btn5.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(245)))), ((int)(((byte)(238)))));
            this.Btn5.UseVisualStyleBackColor = false;
            // 
            // Btn4
            // 
            this.Btn4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(29)))), ((int)(((byte)(23)))));
            this.Btn4.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(29)))), ((int)(((byte)(23)))));
            this.Btn4.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(65)))), ((int)(((byte)(48)))));
            this.Btn4.BorderRadius = 12;
            this.Btn4.BorderSize = 1;
            this.Btn4.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Btn4.FlatAppearance.BorderSize = 0;
            this.Btn4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Btn4.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold);
            this.Btn4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(245)))), ((int)(((byte)(238)))));
            this.Btn4.Image = global::CapaPresentacion.Properties.Resources.usuario;
            this.Btn4.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Btn4.Location = new System.Drawing.Point(50, 425);
            this.Btn4.Name = "Btn4";
            this.Btn4.Padding = new System.Windows.Forms.Padding(18, 0, 15, 0);
            this.Btn4.Size = new System.Drawing.Size(380, 145);
            this.Btn4.TabIndex = 8;
            this.Btn4.Text = "CLIENTES REGISTRADOS\r\n\r\n0\r\n\r\nCartera de clientes";
            this.Btn4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.Btn4.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(245)))), ((int)(((byte)(238)))));
            this.Btn4.UseVisualStyleBackColor = false;
            // 
            // Btn3
            // 
            this.Btn3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(29)))), ((int)(((byte)(23)))));
            this.Btn3.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(29)))), ((int)(((byte)(23)))));
            this.Btn3.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(65)))), ((int)(((byte)(48)))));
            this.Btn3.BorderRadius = 12;
            this.Btn3.BorderSize = 1;
            this.Btn3.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Btn3.FlatAppearance.BorderSize = 0;
            this.Btn3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Btn3.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold);
            this.Btn3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(245)))), ((int)(((byte)(238)))));
            this.Btn3.Image = global::CapaPresentacion.Properties.Resources.tarjeta_de_visita;
            this.Btn3.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Btn3.Location = new System.Drawing.Point(470, 425);
            this.Btn3.Name = "Btn3";
            this.Btn3.Padding = new System.Windows.Forms.Padding(18, 0, 15, 0);
            this.Btn3.Size = new System.Drawing.Size(380, 145);
            this.Btn3.TabIndex = 9;
            this.Btn3.Text = "USUARIOS DEL SISTEMA\r\n\r\n0\r\n\r\nCuentas con acceso";
            this.Btn3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.Btn3.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(245)))), ((int)(((byte)(238)))));
            this.Btn3.UseVisualStyleBackColor = false;
            // 
            // FrmInicio
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(38)))), ((int)(((byte)(30)))));
            this.ClientSize = new System.Drawing.Size(902, 620);
            this.Controls.Add(this.Btn3);
            this.Controls.Add(this.Btn4);
            this.Controls.Add(this.Btn5);
            this.Controls.Add(this.Btn1);
            this.Controls.Add(this.Btn6);
            this.Controls.Add(this.Btn2);
            this.Controls.Add(this.lblSeparador);
            this.Controls.Add(this.lblBanner);
            this.Controls.Add(this.lblSubtitulo);
            this.Controls.Add(this.lblTitulo);
            this.Name = "FrmInicio";
            this.Text = "Inicio";
            this.Load += new System.EventHandler(this.FrmInicio_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private CustomControls.RJControls.RJButton Btn2;
        private CustomControls.RJControls.RJButton Btn3;
        private CustomControls.RJControls.RJButton Btn4;
        private CustomControls.RJControls.RJButton Btn1;
        private CustomControls.RJControls.RJButton Btn5;
        private CustomControls.RJControls.RJButton Btn6;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Label lblSubtitulo;
        private System.Windows.Forms.Label lblBanner;
        private System.Windows.Forms.Label lblSeparador;
    }
}