using CapaNegocio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media;

namespace CapaPresentacion.Modales
{
    public partial class mdBackup : Form
    {
        public mdBackup()
        {
            InitializeComponent();
            // Configura la opacidad del formulario (50% de transparencia)
           // this.Opacity = 1;
            // Establece un color clave de transparencia (el color de fondo del formulario será transparente)
            //this.BackColor = Color.FromArgb(36, 35, 58); // El color que quieres transparente
            //this.TransparencyKey = this.BackColor;
        }

        private void mdBackup_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.FileName = "Files | *.Archivo;*.BAK";

            if (open.ShowDialog() == DialogResult.OK)
            {

                string ruta = open.FileName;

                string mensaje = new CN_Backup().Restore(ruta);
                DialogResult respuesta = MessageBox.Show(mensaje, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (respuesta == DialogResult.OK)
                {
                    this.Close();
                }
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string mensaje = new CN_Backup().Backup();
            DialogResult respuesta = MessageBox.Show(mensaje, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);

            if (respuesta == DialogResult.OK)
            {
                this.Close();
            }



            

        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
