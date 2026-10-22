namespace CapaPresentacion
{
    partial class FrmGraficos
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Title title1 = new System.Windows.Forms.DataVisualization.Charting.Title();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Title title2 = new System.Windows.Forms.DataVisualization.Charting.Title();
            this.chVentas = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.chCompras = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rjButton1 = new CustomControls.RJControls.RJButton();
            this.Compras = new CustomControls.RJControls.RJButton();
            this.Ventas = new CustomControls.RJControls.RJButton();
            this.rjTextBox2 = new CustomControls.RJControls.RJTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.chVentas)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chCompras)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // chVentas
            // 
            this.chVentas.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(43)))), ((int)(((byte)(59)))));
            this.chVentas.BorderSkin.BackColor = System.Drawing.Color.White;
            this.chVentas.BorderSkin.BorderColor = System.Drawing.Color.White;
            this.chVentas.BorderSkin.BorderDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.DashDot;
            chartArea1.AxisX.InterlacedColor = System.Drawing.Color.White;
            chartArea1.AxisX.IntervalAutoMode = System.Windows.Forms.DataVisualization.Charting.IntervalAutoMode.VariableCount;
            chartArea1.AxisX.LabelStyle.ForeColor = System.Drawing.Color.White;
            chartArea1.AxisX.LineColor = System.Drawing.Color.White;
            chartArea1.AxisX.MajorGrid.LineColor = System.Drawing.Color.White;
            chartArea1.AxisX.MajorTickMark.LineColor = System.Drawing.Color.White;
            chartArea1.AxisX.MinorGrid.LineColor = System.Drawing.Color.White;
            chartArea1.AxisX.MinorTickMark.LineColor = System.Drawing.Color.White;
            chartArea1.AxisX.TitleForeColor = System.Drawing.Color.White;
            chartArea1.AxisX2.LineColor = System.Drawing.Color.White;
            chartArea1.AxisX2.TitleForeColor = System.Drawing.Color.White;
            chartArea1.AxisY.LabelStyle.ForeColor = System.Drawing.Color.White;
            chartArea1.AxisY.LineColor = System.Drawing.Color.White;
            chartArea1.AxisY.MajorGrid.LineColor = System.Drawing.Color.White;
            chartArea1.AxisY.TitleForeColor = System.Drawing.Color.White;
            chartArea1.AxisY2.LineColor = System.Drawing.Color.White;
            chartArea1.AxisY2.TitleForeColor = System.Drawing.Color.White;
            chartArea1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(43)))), ((int)(((byte)(59)))));
            chartArea1.BorderColor = System.Drawing.Color.White;
            chartArea1.Name = "ChartArea1";
            this.chVentas.ChartAreas.Add(chartArea1);
            legend1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(43)))), ((int)(((byte)(59)))));
            legend1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            legend1.ForeColor = System.Drawing.Color.White;
            legend1.IsTextAutoFit = false;
            legend1.Name = "Legend1";
            legend1.Title = "Categorias";
            legend1.TitleBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(43)))), ((int)(((byte)(59)))));
            legend1.TitleForeColor = System.Drawing.Color.Silver;
            this.chVentas.Legends.Add(legend1);
            this.chVentas.Location = new System.Drawing.Point(83, 288);
            this.chVentas.Name = "chVentas";
            this.chVentas.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.SeaGreen;
            this.chVentas.Size = new System.Drawing.Size(351, 262);
            this.chVentas.TabIndex = 95;
            this.chVentas.Text = "Categorias Vendidas";
            title1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            title1.ForeColor = System.Drawing.Color.White;
            title1.Name = "Title1";
            title1.Text = "Categorias Más Vendidas";
            this.chVentas.Titles.Add(title1);
            // 
            // chCompras
            // 
            this.chCompras.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(43)))), ((int)(((byte)(59)))));
            chartArea2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(43)))), ((int)(((byte)(59)))));
            chartArea2.Name = "ChartArea1";
            this.chCompras.ChartAreas.Add(chartArea2);
            legend2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(43)))), ((int)(((byte)(59)))));
            legend2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            legend2.ForeColor = System.Drawing.Color.White;
            legend2.IsTextAutoFit = false;
            legend2.Name = "Legend1";
            legend2.Title = "Productos";
            legend2.TitleForeColor = System.Drawing.Color.Silver;
            this.chCompras.Legends.Add(legend2);
            this.chCompras.Location = new System.Drawing.Point(467, 288);
            this.chCompras.Name = "chCompras";
            this.chCompras.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.SeaGreen;
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Doughnut;
            series1.CustomProperties = "CollectedColor=Black";
            series1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            series1.IsValueShownAsLabel = true;
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.chCompras.Series.Add(series1);
            this.chCompras.Size = new System.Drawing.Size(360, 262);
            this.chCompras.TabIndex = 96;
            this.chCompras.Text = "Productos Vendidos";
            title2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            title2.ForeColor = System.Drawing.Color.White;
            title2.Name = "Title1";
            title2.Text = "Producto Más Vendido";
            this.chCompras.Titles.Add(title2);
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.groupBox1.Controls.Add(this.rjButton1);
            this.groupBox1.Controls.Add(this.Compras);
            this.groupBox1.Controls.Add(this.Ventas);
            this.groupBox1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.Color.White;
            this.groupBox1.Location = new System.Drawing.Point(83, 43);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(763, 189);
            this.groupBox1.TabIndex = 98;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Informacion General";
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // rjButton1
            // 
            this.rjButton1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.rjButton1.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.rjButton1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(50)))), ((int)(((byte)(56)))));
            this.rjButton1.BorderRadius = 25;
            this.rjButton1.BorderSize = 1;
            this.rjButton1.Cursor = System.Windows.Forms.Cursors.Default;
            this.rjButton1.FlatAppearance.BorderSize = 0;
            this.rjButton1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rjButton1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rjButton1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(243)))), ((int)(((byte)(243)))));
            this.rjButton1.Image = global::CapaPresentacion.Properties.Resources.horario_de_busqueda__1_;
            this.rjButton1.Location = new System.Drawing.Point(692, 76);
            this.rjButton1.Margin = new System.Windows.Forms.Padding(2);
            this.rjButton1.Name = "rjButton1";
            this.rjButton1.Size = new System.Drawing.Size(62, 51);
            this.rjButton1.TabIndex = 107;
            this.rjButton1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.rjButton1.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(243)))), ((int)(((byte)(243)))));
            this.rjButton1.UseVisualStyleBackColor = false;
            this.rjButton1.Click += new System.EventHandler(this.rjButton1_Click);
            // 
            // Compras
            // 
            this.Compras.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.Compras.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.Compras.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(50)))), ((int)(((byte)(56)))));
            this.Compras.BorderRadius = 20;
            this.Compras.BorderSize = 1;
            this.Compras.Cursor = System.Windows.Forms.Cursors.Default;
            this.Compras.FlatAppearance.BorderSize = 0;
            this.Compras.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Compras.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Compras.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(243)))), ((int)(((byte)(243)))));
            this.Compras.Image = global::CapaPresentacion.Properties.Resources.entrega;
            this.Compras.Location = new System.Drawing.Point(384, 31);
            this.Compras.Margin = new System.Windows.Forms.Padding(2);
            this.Compras.Name = "Compras";
            this.Compras.Size = new System.Drawing.Size(293, 139);
            this.Compras.TabIndex = 106;
            this.Compras.Text = "Total Compras:";
            this.Compras.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.Compras.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(243)))), ((int)(((byte)(243)))));
            this.Compras.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.Compras.UseVisualStyleBackColor = false;
            // 
            // Ventas
            // 
            this.Ventas.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.Ventas.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.Ventas.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(50)))), ((int)(((byte)(56)))));
            this.Ventas.BorderRadius = 20;
            this.Ventas.BorderSize = 1;
            this.Ventas.Cursor = System.Windows.Forms.Cursors.Default;
            this.Ventas.FlatAppearance.BorderSize = 0;
            this.Ventas.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Ventas.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Ventas.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(243)))), ((int)(((byte)(243)))));
            this.Ventas.Image = global::CapaPresentacion.Properties.Resources.carrito_de_compras;
            this.Ventas.Location = new System.Drawing.Point(30, 31);
            this.Ventas.Margin = new System.Windows.Forms.Padding(2);
            this.Ventas.Name = "Ventas";
            this.Ventas.Size = new System.Drawing.Size(293, 139);
            this.Ventas.TabIndex = 105;
            this.Ventas.Text = "Total Ventas:";
            this.Ventas.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.Ventas.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(243)))), ((int)(((byte)(243)))));
            this.Ventas.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.Ventas.UseVisualStyleBackColor = false;
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
            this.rjTextBox2.Size = new System.Drawing.Size(874, 595);
            this.rjTextBox2.TabIndex = 94;
            this.rjTextBox2.Texts = "";
            this.rjTextBox2.UnderlinedStyle = false;
            // 
            // FrmGraficos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(902, 620);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.chCompras);
            this.Controls.Add(this.chVentas);
            this.Controls.Add(this.rjTextBox2);
            this.Name = "FrmGraficos";
            this.Text = "FrmGraficos";
            this.Load += new System.EventHandler(this.FrmGraficos_Load);
            ((System.ComponentModel.ISupportInitialize)(this.chVentas)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chCompras)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private CustomControls.RJControls.RJTextBox rjTextBox2;
        private System.Windows.Forms.DataVisualization.Charting.Chart chVentas;
        private System.Windows.Forms.DataVisualization.Charting.Chart chCompras;
        private System.Windows.Forms.GroupBox groupBox1;
        private CustomControls.RJControls.RJButton Ventas;
        private CustomControls.RJControls.RJButton Compras;
        private CustomControls.RJControls.RJButton rjButton1;
    }
}