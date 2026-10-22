using CapaEntidad;
using CapaNegocio;
using CapaPresentacion.Modales;
using CapaPresentacion.Utilidades;
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
            menuLateral.Renderer = new TemaCapybara.CapybaraRenderer();
            menuTitulo.Renderer = new TemaCapybara.CapybaraRenderer();
            inicio(new FrmInicio()); // Abre el formulario de inicio por defecto.
            lblIndicador.Text = "Inicio";
            lblRol.Text += " " + user.oRol.Descripcion; // Muestra el rol del usuario en la etiqueta.
        }

        public void pintar()
        {
            this.menuMantenimiento.BackColor = TemaCapybara.FondoLateral;
            this.menuMantenimiento.ForeColor = TemaCapybara.MenuTexto;
            this.menuMantenimiento.IconColor = TemaCapybara.MenuIcono;

            this.menuCompras.BackColor = TemaCapybara.FondoLateral;
            this.menuCompras.ForeColor = TemaCapybara.MenuTexto;
            this.menuCompras.IconColor = TemaCapybara.MenuIcono;

            this.menuVentas.BackColor = TemaCapybara.FondoLateral;
            this.menuVentas.ForeColor = TemaCapybara.MenuTexto;
            this.menuVentas.IconColor = TemaCapybara.MenuIcono;

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

            foreach (Form form in Contenedor.Controls.OfType<Form>())
            {
                if (mantenimientoForms.Contains(form.GetType()))
                {
                    this.menuMantenimiento.BackColor = TemaCapybara.MenuSeleccionado;
                    this.menuMantenimiento.ForeColor = Color.White;
                    this.menuMantenimiento.IconColor = TemaCapybara.BotonYuzu;
                    break;
                }
                else if (comprasForms.Contains(form.GetType()))
                {
                    this.menuCompras.BackColor = TemaCapybara.MenuSeleccionado;
                    this.menuCompras.ForeColor = Color.White;
                    this.menuCompras.IconColor = TemaCapybara.BotonYuzu;
                    break;
                }
                else if (ventasForms.Contains(form.GetType()))
                {
                    this.menuVentas.BackColor = TemaCapybara.MenuSeleccionado;
                    this.menuVentas.ForeColor = Color.White;
                    this.menuVentas.IconColor = TemaCapybara.BotonYuzu;
                    break;
                }
            }
        }


        // Método para abrir un formulario sin modificar el menú activo.
        public void inicio(Form formulario)
        {
            Contenedor.Controls.Clear();
            if (menuActivo != null)
            {
                menuActivo.BackColor = TemaCapybara.FondoLateral;
                menuActivo.ForeColor = TemaCapybara.MenuTexto;
                menuActivo.IconColor = TemaCapybara.MenuIcono;
            }

            if (formActivo != null)
            {
                formActivo.Close();
            }

            formActivo = formulario;
            formulario.TopLevel = false;
            formulario.FormBorderStyle = FormBorderStyle.None;
            formulario.Dock = DockStyle.Fill;
            formulario.BackColor = TemaCapybara.FondoFormulario;
            TemaCapybara.AplicarTema(formulario);
            Contenedor.Controls.Add(formulario);
            pintar();
            formulario.Show();
        }

        // Método para abrir un formulario y cambiar el menú activo.
        public void abrirFormulario(IconMenuItem menu, Form formulario)
        {
            Contenedor.Controls.Clear();
            if (menuActivo != null)
            {
                menuActivo.BackColor = TemaCapybara.FondoLateral;
                menuActivo.ForeColor = TemaCapybara.MenuTexto;
                menuActivo.IconColor = TemaCapybara.MenuIcono;
            }

            menu.BackColor = TemaCapybara.MenuSeleccionado;
            menu.ForeColor = Color.White;
            menu.IconColor = TemaCapybara.BotonYuzu;
            menuActivo = menu;

            if (formActivo != null)
            {
                formActivo.Close();
            }

            formActivo = formulario;
            formulario.TopLevel = false;
            formulario.FormBorderStyle = FormBorderStyle.None;
            formulario.Dock = DockStyle.Fill;
            formulario.BackColor = TemaCapybara.FondoFormulario;
            TemaCapybara.AplicarTema(formulario);
            Contenedor.Controls.Add(formulario);
            pintar();
            formulario.Show();
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
            try
            {
                new CN_Backup().Backup(fecha);
            }
            catch { }

            // Muestra el nombre completo del usuario en el formulario.
            nombreUser.Text = user.NombreCompleto;

            // Aplicar Estilo Capybara al contenedor y menús
            panelLateral.BackColor = TemaCapybara.FondoLateral;
            menuLateral.BackColor = TemaCapybara.FondoLateral;
            BarraTop.BackColor = TemaCapybara.FondoBarraTop;
            nombreUser.BackColor = TemaCapybara.FondoBarraTop;
            nombreUser.ForeColor = TemaCapybara.TextoPrincipal;
            lblIndicador.ForeColor = TemaCapybara.TextoPrincipal;
            lblRol.ForeColor = TemaCapybara.TextoDorado;
            Contenedor.BackColor = TemaCapybara.FondoFormulario;

            foreach (IconMenuItem iconmenu in menuLateral.Items)
            {
                iconmenu.BackColor = TemaCapybara.FondoLateral;
                iconmenu.ForeColor = TemaCapybara.MenuTexto;
                iconmenu.IconColor = TemaCapybara.MenuIcono;
                foreach (ToolStripItem subItem in iconmenu.DropDownItems)
                {
                    subItem.BackColor = TemaCapybara.FondoLateral;
                    subItem.ForeColor = TemaCapybara.MenuTexto;
                    subItem.Font = new Font("Segoe UI", 9.5f, FontStyle.Regular);
                    if (subItem is IconMenuItem subIcon)
                    {
                        subIcon.IconColor = TemaCapybara.MenuIcono;
                        subIcon.IconSize = 18;
                    }
                }
            }
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
