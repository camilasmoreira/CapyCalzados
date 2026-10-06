using CapaEntidad;
using CapaNegocio;
using CapaPresentacion.Modales;
using CustomControls.RJControls;
using FontAwesome.Sharp;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace CapaPresentacion
{
    // Clase que representa el formulario principal de la aplicación.
    public partial class Inicio : Form
    {
        // Variables estáticas para gestionar el usuario, el menú activo y el formulario activo.
        private static Usuario user; // Variable para almacenar el usuario actual.
        public static IconMenuItem menuActivo = null; // Almacena el menú que está activo.
        private static Form formActivo = null; // Almacena el formulario activo.

        // Constructor de la clase, recibe el objeto Usuario y lo asigna.
        public Inicio(Usuario objusuario)
        {
            user = objusuario; // Asigna el usuario recibido.
            InitializeComponent(); // Inicializa los componentes visuales del formulario.
            inicio(new FrmInicio()); // Abre el formulario de inicio por defecto.
            menuLateral.Renderer = new MiRenderizador();
            menuTitulo.Renderer = new MiRenderizador();
            lblIndicador.Text = "Inicio";
            lblRol.Text += " " + user.oRol.Descripcion; // Muestra el rol del usuario en la etiqueta.
        }

        private class MiRenderizador : ToolStripProfessionalRenderer
        {
            protected override void OnRenderMenuItemBackground(ToolStripItemRenderEventArgs e)
            {
                if (!e.Item.Selected) base.OnRenderMenuItemBackground(e);
                else
                {
                    Rectangle rc = new Rectangle(Point.Empty, e.Item.Size);      
                    e.Graphics.FillRectangle(Brushes.Gray, rc); //Elige el color que desees
                    e.Graphics.DrawRectangle(Pens.Black, 1, 0, rc.Width - 2, rc.Height - 1);
               
                }
            }
        }



        public void pintar()
        {
            this.menuMantenimiento.BackColor = Color.FromArgb(38, 50, 56);
            this.menuCompras.BackColor = Color.FromArgb(38, 50, 56);
            this.menuVentas.BackColor = Color.FromArgb(38, 50, 56);
            // Definimos conjuntos de tipos de formularios para cada menú
            var mantenimientoForms = new HashSet<Type>
    {
        typeof(FrmCategoria),
        typeof(FrmNegocio),
        typeof(FrmProducto)
    };

            var comprasForms = new HashSet<Type>
    {
        typeof(FrmDetalleCompra),
        typeof(FrmCompras)
    };

            var ventasForms = new HashSet<Type>
    {
        typeof(FrmDetalleVenta),
        typeof(FrmVentas)
    };

            // Inicializamos el color de fondo por defecto
            Color defaultColor = Color.FromArgb(38, 50, 56);
            Color selectedColor = Color.FromArgb(30, 30, 30);

            // Variable para determinar si encontramos un formulario específico
            bool found = false;

            foreach (Form form in Contenedor.Controls.OfType<Form>())
            {
                if (mantenimientoForms.Contains(form.GetType()))
                {
                    this.menuMantenimiento.BackColor = selectedColor;
                    found = true;
                    break; // Salimos del bucle si encontramos un formulario de mantenimiento
                }
                else if (comprasForms.Contains(form.GetType()))
                {
                    this.menuCompras.BackColor = selectedColor;
                    found = true;
                    break; // Salimos del bucle si encontramos un formulario de compras
                }
                else if (ventasForms.Contains(form.GetType()))
                {
                    this.menuVentas.BackColor = selectedColor;
                    found = true;
                    break; // Salimos del bucle si encontramos un formulario de ventas
                }
            }

            // Si no se encontró ningún formulario, aplicamos el color por defecto
            if (!found)
            {
                this.menuMantenimiento.BackColor = defaultColor;
            }
        }


        // Método para abrir un formulario sin modificar el menú activo.
        public void inicio(Form formulario)
        {
            Contenedor.Controls.Clear();
            // Restablece el color del menú activo si existe.
            if (menuActivo != null)
            {
                menuActivo.BackColor = Color.FromArgb(38, 50, 56); // Color por defecto.
            }

            // Cierra el formulario activo si ya hay uno abierto.
            if (formActivo != null)
            {
                formActivo.Close();
            }

            formActivo = formulario; // Asigna el nuevo formulario como activo.
            formulario.TopLevel = false; // Configura el formulario para que no sea de nivel superior.
            formulario.FormBorderStyle = FormBorderStyle.None; // Elimina los bordes del formulario.
            formulario.Dock = DockStyle.Fill; // Establece el formulario para que ocupe todo el contenedor.
            formulario.BackColor = Color.FromArgb(44, 53, 68); // Cambia el color de fondo.
            Contenedor.Controls.Add(formulario); // Añade el formulario al contenedor visual.
            pintar();
            formulario.Show(); // Muestra el formulario.
        }

        // Método para abrir un formulario y cambiar el menú activo.
        public void abrirFormulario(IconMenuItem menu, Form formulario)
        {
              Contenedor.Controls.Clear();
            // Restablece el color del menú activo si existe.
            if (menuActivo != null)
            {
                menuActivo.BackColor = Color.FromArgb(38, 50, 56);
            }

            // Cambia el color del menú seleccionado.
            menu.BackColor = Color.FromArgb(30, 30, 30);
            menuActivo = menu; // Asigna el nuevo menú como activo.

            // Cierra el formulario activo si ya hay uno abierto.
            if (formActivo != null)
            {
                formActivo.Close();
            }

            formActivo = formulario; // Asigna el nuevo formulario como activo.
            formulario.TopLevel = false; // Configura el formulario para que no sea de nivel superior.
            formulario.FormBorderStyle = FormBorderStyle.None; // Elimina los bordes del formulario.
            formulario.Dock = DockStyle.Fill; // Establece el formulario para que ocupe todo el contenedor.
            formulario.BackColor = Color.FromArgb(44, 53, 68); // Cambia el color de fondo.
            Contenedor.Controls.Add(formulario); // Añade el formulario al contenedor visual.
            pintar();
            formulario.Show(); // Muestra el formulario.
        }




        // Evento que se ejecuta al cargar el formulario principal.
        private void Inicio_Load(object sender, EventArgs e)
        {
            // Obtiene los permisos del usuario actual desde la capa de negocio.
            List<Permiso> listaPermisos = new CN_Permiso().Listar(user.IdUsuario);

            // Recorre los elementos del menú lateral y oculta aquellos que el usuario no tiene permiso para ver.
            foreach (IconMenuItem iconmenu in menuLateral.Items)
            {
                bool encontrado = listaPermisos.Any(m => m.NombreMenu == iconmenu.Name);

                if (!encontrado)
                {
                    iconmenu.Visible = false; // Oculta el menú si el usuario no tiene permiso.
                }
            }

            if (user.oRol.IdRol != 1)
            {
                Negocio.Visible = false;
                Backup.Visible = false;
            }

      
            DateTime fecha = DateTime.Now;

            bool resultado = new CN_Backup().Backup(fecha);
            if(resultado == true)
            {
                MessageBox.Show("Se esta realizando una copia de seguridad automatica, aguarde","Aviso",MessageBoxButtons.OK,MessageBoxIcon.Stop);
            }

            // Muestra el nombre completo del usuario en el formulario.
            nombreUser.Text = user.NombreCompleto;
        }

        // Evento que abre el formulario de productos cuando se hace clic en el menú correspondiente.
        private void Productos_Click(object sender, EventArgs e)
        {
            abrirFormulario((IconMenuItem)sender, new FrmProducto(this)); // Abre el formulario de productos.
            this.lblIndicador.Text = "Gestion Productos";
        }

        // Evento que abre el formulario de inicio cuando se hace clic en el menú correspondiente.
        private void menuInicio_Click(object sender, EventArgs e)
        {
            abrirFormulario((IconMenuItem)sender, new FrmInicio()); // Abre el formulario de inicio.
            this.lblIndicador.Text = "Inicio";
        }

        // Evento que abre el formulario de clientes cuando se hace clic en el menú correspondiente.
        private void menuClientes_Click(object sender, EventArgs e)
        {
            abrirFormulario((IconMenuItem)sender, new FrmClientes()); // Abre el formulario de clientes.
            this.lblIndicador.Text = "Gestion Clientes";
        }

        // Evento que abre el formulario de proveedores cuando se hace clic en el menú correspondiente.
        private void menuProveedores_Click(object sender, EventArgs e)
        {
            abrirFormulario((IconMenuItem)sender, new FrmProveedores()); // Abre el formulario de proveedores.
            this.lblIndicador.Text = "Gestion Proveedores";
        }

        // Evento que abre el formulario de usuarios cuando se hace clic en el menú correspondiente.
        private void menuUsuarios_Click(object sender, EventArgs e)
        {
            abrirFormulario((IconMenuItem)sender, new FrmUsuario(user)); // Abre el formulario de usuarios.
            this.lblIndicador.Text = "Gestion Usuarios";
        }

        // Evento que abre el formulario de categorías cuando se hace clic en el menú correspondiente.
        private void Categorias_Click(object sender, EventArgs e)
        {
            abrirFormulario((IconMenuItem)sender, new FrmCategoria(this)); // Abre el formulario de categorías.
            this.lblIndicador.Text = "Gestion Categorias";
        }

        // Evento que abre el formulario para registrar ventas cuando se hace clic en el menú correspondiente.
        private void menuRegistrarVenta_Click(object sender, EventArgs e)
        {
            abrirFormulario((IconMenuItem)sender, new FrmVentas(user,this)); // Abre el formulario de registro de ventas.
            this.lblIndicador.Text = "Ventas";
        }

        // Evento que abre el formulario para registrar compras cuando se hace clic en el menú correspondiente.
        private void menuRegistrarCompra_Click(object sender, EventArgs e)
        {
            abrirFormulario((IconMenuItem)sender, new FrmCompras(user,this)); // Abre el formulario de registro de compras.
            this.lblIndicador.Text = "Compras";
        }

        // Evento que abre el formulario de detalles de ventas cuando se hace clic en el menú correspondiente.
        private void menuVerDetalle_Click(object sender, EventArgs e)
        {
            abrirFormulario((IconMenuItem)sender, new FrmDetalleVenta(this)); // Abre el formulario de detalle de ventas.
            this.lblIndicador.Text = "Detalle Ventas";
        }

        // Evento que abre el formulario de detalles de compras cuando se hace clic en el menú correspondiente.
        private void menuVerDetalleCompra_Click(object sender, EventArgs e)
        {
            abrirFormulario((IconMenuItem)sender, new FrmDetalleCompra(user)); // Abre el formulario de detalle de compras.
            this.lblIndicador.Text = "Detalle Compras";
        }

        // Evento que abre el formulario de configuración del negocio, si el usuario tiene permisos.
        private void Negocio_Click(object sender, EventArgs e)
        {
            if (user.oRol.IdRol == 1) // Verifica si el usuario es administrador.
            {
                abrirFormulario((IconMenuItem)sender, new FrmNegocio(this)); // Abre el formulario de configuración del negocio.
                this.lblIndicador.Text = "Gestion Negocio";
            }
            else
            {
                MessageBox.Show("No tiene los permisos", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Warning); // Muestra una alerta si no tiene permiso.
            }
        }

        // Evento para cerrar la sesión del usuario actual.
        private void iconMenuItem9_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("¿Desea Cerrar la sesión?", "Alerta", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

            if (result == DialogResult.OK)
            {
                this.Close(); // Cierra el formulario y finaliza la sesión.
            }
        }

        // Evento que abre el submenú de reportes de ventas.
        private void subMenuRVentas_Click(object sender, EventArgs e)
        {
            abrirFormulario(menuReportes, new FrmReporteVentas(this)); // Abre el formulario de reportes de ventas.
            this.lblIndicador.Text = "Reporte Ventas";
        }

        // Evento que abre el submenú de reportes de compras.
        private void subMenuRCompras_Click(object sender, EventArgs e)
        {
            abrirFormulario(menuReportes, new FrmReporteCompra(this)); // Abre el formulario de reportes de compras.
            this.lblIndicador.Text = "Reporte Compras";
        }

        // Evento que abre el formulario de gráficos cuando se hace clic en el menú correspondiente.
        private void menuGraficos_Click(object sender, EventArgs e)
        {
            abrirFormulario((IconMenuItem)sender, new FrmGraficos()); // Abre el formulario de gráficos.
            this.lblIndicador.Text = "Gestion Graficos";
        }

        private void iconMenuItem9_MouseEnter(object sender, EventArgs e)
        {
            this.BackColor = Color.FromArgb(55, 71, 70);
        }

        private void iconMenuItem9_MouseLeave(object sender, EventArgs e)
        {
            this.BackColor = Color.FromArgb(55, 71, 79);
        }

        private void menuMantenimiento_Click(object sender, EventArgs e)
        {

        }

        private void Backup_Click(object sender, EventArgs e)
        {
            using (var modal = new mdBackup())
            {
                var result = modal.ShowDialog(); // Muestra el diálogo modal.

  
            }
        }
    }
}
