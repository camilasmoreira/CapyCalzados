namespace CapaPresentacion
{
    partial class Login
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblMarca = new System.Windows.Forms.Label();
            this.lblSubmarca = new System.Windows.Forms.Label();
            this.lblSeparador = new System.Windows.Forms.Label();
            this.lblLoginTitulo = new System.Windows.Forms.Label();
            this.lblLoginDesc = new System.Windows.Forms.Label();
            this.lblDNI = new System.Windows.Forms.Label();
            this.campoDNI = new CustomControls.RJControls.RJTextBox();
            this.lblClave = new System.Windows.Forms.Label();
            this.campoClave = new CustomControls.RJControls.RJTextBox();
            this.rjButton1 = new CustomControls.RJControls.RJButton();
            this.lblCredencialesHint = new System.Windows.Forms.Label();
            this.lblFooter = new System.Windows.Forms.Label();
            this.Fondo = new System.Windows.Forms.PictureBox();
            this.lblHeroTag = new System.Windows.Forms.Label();
            this.Titulo = new System.Windows.Forms.Label();
            this.lblHeroSub = new System.Windows.Forms.Label();
            this.Desc = new System.Windows.Forms.Label();
            this.lblHeroBadge = new System.Windows.Forms.Label();
            this.btnSalir = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Fondo)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(26)))), ((int)(((byte)(20)))));
            this.panel1.Controls.Add(this.lblMarca);
            this.panel1.Controls.Add(this.lblSubmarca);
            this.panel1.Controls.Add(this.lblSeparador);
            this.panel1.Controls.Add(this.lblLoginTitulo);
            this.panel1.Controls.Add(this.lblLoginDesc);
            this.panel1.Controls.Add(this.lblDNI);
            this.panel1.Controls.Add(this.campoDNI);
            this.panel1.Controls.Add(this.lblClave);
            this.panel1.Controls.Add(this.campoClave);
            this.panel1.Controls.Add(this.rjButton1);
            this.panel1.Controls.Add(this.lblCredencialesHint);
            this.panel1.Controls.Add(this.lblFooter);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(428, 517);
            this.panel1.TabIndex = 0;
            // 
            // lblMarca
            // 
            this.lblMarca.Font = new System.Drawing.Font("Segoe UI", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMarca.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(248)))), ((int)(((byte)(240)))));
            this.lblMarca.Location = new System.Drawing.Point(44, 30);
            this.lblMarca.Name = "lblMarca";
            this.lblMarca.Size = new System.Drawing.Size(340, 42);
            this.lblMarca.TabIndex = 0;
            this.lblMarca.Text = "CapyCalzados";
            // 
            // lblSubmarca
            // 
            this.lblSubmarca.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSubmarca.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(130)))), ((int)(((byte)(43)))));
            this.lblSubmarca.Location = new System.Drawing.Point(46, 72);
            this.lblSubmarca.Name = "lblSubmarca";
            this.lblSubmarca.Size = new System.Drawing.Size(340, 18);
            this.lblSubmarca.TabIndex = 1;
            this.lblSubmarca.Text = "TIENDA & GESTIÓN DE CALZADOS";
            this.lblSubmarca.UseMnemonic = false;
            // 
            // lblSeparador
            // 
            this.lblSeparador.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(44)))), ((int)(((byte)(34)))));
            this.lblSeparador.Location = new System.Drawing.Point(44, 95);
            this.lblSeparador.Name = "lblSeparador";
            this.lblSeparador.Size = new System.Drawing.Size(340, 1);
            this.lblSeparador.TabIndex = 2;
            // 
            // lblLoginTitulo
            // 
            this.lblLoginTitulo.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLoginTitulo.ForeColor = System.Drawing.Color.White;
            this.lblLoginTitulo.Location = new System.Drawing.Point(44, 110);
            this.lblLoginTitulo.Name = "lblLoginTitulo";
            this.lblLoginTitulo.Size = new System.Drawing.Size(340, 28);
            this.lblLoginTitulo.TabIndex = 3;
            this.lblLoginTitulo.Text = "Iniciar Sesión";
            // 
            // lblLoginDesc
            // 
            this.lblLoginDesc.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLoginDesc.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(216)))), ((int)(((byte)(202)))), ((int)(((byte)(184)))));
            this.lblLoginDesc.Location = new System.Drawing.Point(44, 138);
            this.lblLoginDesc.Name = "lblLoginDesc";
            this.lblLoginDesc.Size = new System.Drawing.Size(340, 20);
            this.lblLoginDesc.TabIndex = 4;
            this.lblLoginDesc.Text = "Ingrese sus credenciales de acceso";
            // 
            // lblDNI
            // 
            this.lblDNI.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDNI.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(130)))), ((int)(((byte)(43)))));
            this.lblDNI.Location = new System.Drawing.Point(44, 170);
            this.lblDNI.Name = "lblDNI";
            this.lblDNI.Size = new System.Drawing.Size(340, 16);
            this.lblDNI.TabIndex = 5;
            this.lblDNI.Text = "DOCUMENTO";
            // 
            // campoDNI
            // 
            this.campoDNI.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(20)))), ((int)(((byte)(16)))));
            this.campoDNI.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(65)))), ((int)(((byte)(48)))));
            this.campoDNI.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(130)))), ((int)(((byte)(43)))));
            this.campoDNI.BorderRadius = 8;
            this.campoDNI.BorderSize = 1;
            this.campoDNI.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.campoDNI.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.campoDNI.ForeColor = System.Drawing.Color.White;
            this.campoDNI.IsReadOnly = false;
            this.campoDNI.Location = new System.Drawing.Point(44, 190);
            this.campoDNI.Margin = new System.Windows.Forms.Padding(4);
            this.campoDNI.Multiline = false;
            this.campoDNI.Name = "campoDNI";
            this.campoDNI.Padding = new System.Windows.Forms.Padding(12, 8, 12, 8);
            this.campoDNI.PasswordChar = false;
            this.campoDNI.PlaceholderColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(130)))), ((int)(((byte)(115)))));
            this.campoDNI.PlaceholderText = "Número de documento (ej. 1234)";
            this.campoDNI.ShortcutsEnabled = false;
            this.campoDNI.Size = new System.Drawing.Size(340, 36);
            this.campoDNI.TabIndex = 6;
            this.campoDNI.Texts = "";
            this.campoDNI.UnderlinedStyle = false;
            this.campoDNI.KeyDown += new System.Windows.Forms.KeyEventHandler(this.campoDNI_KeyDown);
            this.campoDNI.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.campoDNI_KeyPress);
            // 
            // lblClave
            // 
            this.lblClave.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblClave.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(130)))), ((int)(((byte)(43)))));
            this.lblClave.Location = new System.Drawing.Point(44, 236);
            this.lblClave.Name = "lblClave";
            this.lblClave.Size = new System.Drawing.Size(340, 16);
            this.lblClave.TabIndex = 7;
            this.lblClave.Text = "CONTRASEÑA";
            // 
            // campoClave
            // 
            this.campoClave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(20)))), ((int)(((byte)(16)))));
            this.campoClave.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(65)))), ((int)(((byte)(48)))));
            this.campoClave.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(130)))), ((int)(((byte)(43)))));
            this.campoClave.BorderRadius = 8;
            this.campoClave.BorderSize = 1;
            this.campoClave.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.campoClave.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.campoClave.ForeColor = System.Drawing.Color.White;
            this.campoClave.IsReadOnly = false;
            this.campoClave.Location = new System.Drawing.Point(44, 256);
            this.campoClave.Margin = new System.Windows.Forms.Padding(4);
            this.campoClave.Multiline = false;
            this.campoClave.Name = "campoClave";
            this.campoClave.Padding = new System.Windows.Forms.Padding(12, 8, 12, 8);
            this.campoClave.PasswordChar = true;
            this.campoClave.PlaceholderColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(130)))), ((int)(((byte)(115)))));
            this.campoClave.PlaceholderText = "Contraseña";
            this.campoClave.ShortcutsEnabled = false;
            this.campoClave.Size = new System.Drawing.Size(340, 36);
            this.campoClave.TabIndex = 8;
            this.campoClave.Texts = "";
            this.campoClave.UnderlinedStyle = false;
            this.campoClave.KeyDown += new System.Windows.Forms.KeyEventHandler(this.campoClave_KeyDown);
            this.campoClave.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.campoClave_KeyPress);
            // 
            // rjButton1
            // 
            this.rjButton1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(130)))), ((int)(((byte)(43)))));
            this.rjButton1.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(130)))), ((int)(((byte)(43)))));
            this.rjButton1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(170)))), ((int)(((byte)(95)))), ((int)(((byte)(25)))));
            this.rjButton1.BorderRadius = 10;
            this.rjButton1.BorderSize = 0;
            this.rjButton1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.rjButton1.FlatAppearance.BorderSize = 0;
            this.rjButton1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rjButton1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rjButton1.ForeColor = System.Drawing.Color.White;
            this.rjButton1.Location = new System.Drawing.Point(44, 320);
            this.rjButton1.Name = "rjButton1";
            this.rjButton1.Size = new System.Drawing.Size(340, 46);
            this.rjButton1.TabIndex = 9;
            this.rjButton1.Text = "Ingresar al Sistema";
            this.rjButton1.TextColor = System.Drawing.Color.White;
            this.rjButton1.UseVisualStyleBackColor = false;
            this.rjButton1.Click += new System.EventHandler(this.btnIngresar2_Click);
            // 
            // lblCredencialesHint
            // 
            this.lblCredencialesHint.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCredencialesHint.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(165)))), ((int)(((byte)(145)))), ((int)(((byte)(130)))));
            this.lblCredencialesHint.Location = new System.Drawing.Point(44, 380);
            this.lblCredencialesHint.Name = "lblCredencialesHint";
            this.lblCredencialesHint.Size = new System.Drawing.Size(340, 22);
            this.lblCredencialesHint.TabIndex = 10;
            this.lblCredencialesHint.Text = "Credenciales de prueba: 1234 / 1234";
            this.lblCredencialesHint.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblFooter
            // 
            this.lblFooter.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFooter.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(115)))), ((int)(((byte)(90)))), ((int)(((byte)(75)))));
            this.lblFooter.Location = new System.Drawing.Point(44, 475);
            this.lblFooter.Name = "lblFooter";
            this.lblFooter.Size = new System.Drawing.Size(340, 20);
            this.lblFooter.TabIndex = 11;
            this.lblFooter.Text = "CapyCalzados © 2026 • Sistema de Ventas";
            this.lblFooter.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Fondo
            // 
            this.Fondo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(24)))), ((int)(((byte)(18)))));
            this.Fondo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Fondo.Image = global::CapaPresentacion.Properties.Resources.tienda_zapatos;
            this.Fondo.Location = new System.Drawing.Point(428, 0);
            this.Fondo.Name = "Fondo";
            this.Fondo.Size = new System.Drawing.Size(566, 517);
            this.Fondo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.Fondo.TabIndex = 1;
            this.Fondo.TabStop = false;
            // 
            // lblHeroTag
            // 
            this.lblHeroTag.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeroTag.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(130)))), ((int)(((byte)(43)))));
            this.lblHeroTag.Location = new System.Drawing.Point(70, 135);
            this.lblHeroTag.Name = "lblHeroTag";
            this.lblHeroTag.Size = new System.Drawing.Size(425, 20);
            this.lblHeroTag.TabIndex = 2;
            this.lblHeroTag.Text = "SISTEMA INTEGRAL DE GESTIÓN";
            // 
            // Titulo
            // 
            this.Titulo.Font = new System.Drawing.Font("Segoe UI", 32F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Titulo.ForeColor = System.Drawing.Color.White;
            this.Titulo.Location = new System.Drawing.Point(65, 155);
            this.Titulo.Name = "Titulo";
            this.Titulo.Size = new System.Drawing.Size(430, 58);
            this.Titulo.TabIndex = 3;
            this.Titulo.Text = "CapyCalzados";
            // 
            // lblHeroSub
            // 
            this.lblHeroSub.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeroSub.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(175)))), ((int)(((byte)(100)))));
            this.lblHeroSub.Location = new System.Drawing.Point(70, 215);
            this.lblHeroSub.Name = "lblHeroSub";
            this.lblHeroSub.Size = new System.Drawing.Size(425, 25);
            this.lblHeroSub.TabIndex = 4;
            this.lblHeroSub.Text = "Venta y Control Integral de Calzados";
            // 
            // Desc
            // 
            this.Desc.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Desc.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(230)))), ((int)(((byte)(220)))));
            this.Desc.Location = new System.Drawing.Point(70, 245);
            this.Desc.Name = "Desc";
            this.Desc.Size = new System.Drawing.Size(425, 60);
            this.Desc.TabIndex = 5;
            this.Desc.Text = "Administración de stock por talles y marcas, facturación rápida, compras a proveedores y reportes estadísticos.";
            // 
            // lblHeroBadge
            // 
            this.lblHeroBadge.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeroBadge.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(130)))), ((int)(((byte)(43)))));
            this.lblHeroBadge.Location = new System.Drawing.Point(70, 318);
            this.lblHeroBadge.Name = "lblHeroBadge";
            this.lblHeroBadge.Size = new System.Drawing.Size(425, 25);
            this.lblHeroBadge.TabIndex = 6;
            this.lblHeroBadge.Text = "✦ Deportivas   ✦ Vestir   ✦ Botas   ✦ Sandalias   ✦ Mocasines";
            // 
            // btnSalir
            // 
            this.btnSalir.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(20)))), ((int)(((byte)(14)))), ((int)(((byte)(10)))));
            this.btnSalir.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSalir.FlatAppearance.BorderSize = 0;
            this.btnSalir.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.btnSalir.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSalir.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSalir.ForeColor = System.Drawing.Color.White;
            this.btnSalir.Location = new System.Drawing.Point(520, 10);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(36, 32);
            this.btnSalir.TabIndex = 7;
            this.btnSalir.Text = "✕";
            this.btnSalir.UseVisualStyleBackColor = false;
            this.btnSalir.Click += new System.EventHandler(this.Salir_Click);
            // 
            // Login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(26)))), ((int)(((byte)(20)))));
            this.ClientSize = new System.Drawing.Size(994, 517);
            this.Controls.Add(this.btnSalir);
            this.Controls.Add(this.lblHeroBadge);
            this.Controls.Add(this.Desc);
            this.Controls.Add(this.lblHeroSub);
            this.Controls.Add(this.Titulo);
            this.Controls.Add(this.lblHeroTag);
            this.Controls.Add(this.Fondo);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Login";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CapyCalzados - Login";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.Login_Load);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Fondo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblMarca;
        private System.Windows.Forms.Label lblSubmarca;
        private System.Windows.Forms.Label lblSeparador;
        private System.Windows.Forms.Label lblLoginTitulo;
        private System.Windows.Forms.Label lblLoginDesc;
        private System.Windows.Forms.Label lblDNI;
        private CustomControls.RJControls.RJTextBox campoDNI;
        private System.Windows.Forms.Label lblClave;
        private CustomControls.RJControls.RJTextBox campoClave;
        private CustomControls.RJControls.RJButton rjButton1;
        private System.Windows.Forms.Label lblCredencialesHint;
        private System.Windows.Forms.Label lblFooter;
        private System.Windows.Forms.PictureBox Fondo;
        private System.Windows.Forms.Label lblHeroTag;
        private System.Windows.Forms.Label Titulo;
        private System.Windows.Forms.Label lblHeroSub;
        private System.Windows.Forms.Label Desc;
        private System.Windows.Forms.Label lblHeroBadge;
        private System.Windows.Forms.Button btnSalir;
    }
}