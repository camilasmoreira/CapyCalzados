using CapaNegocio;
using CapaPresentacion.Utilidades;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace CapaPresentacion
{
    public partial class FrmInicio : Form
    {
        public FrmInicio()
        {
            InitializeComponent();
        }

        private void FrmInicio_Load(object sender, EventArgs e)
        {
            TemaCapybara.AplicarTema(this);
            try
            {
                // Calzados en stock / catálogo
                int prod = new CN_Producto().Listar().Where(p => p.Estado == true).Count();
                Btn2.Text = string.Format("CALZADOS EN STOCK\n\n{0}\n\nModelos activos en catálogo", prod);

                // Proveedores activos
                int proveedores = new CN_Proveedor().Listar().Where(c => c.Estado == true).Count();
                Btn6.Text = string.Format("PROVEEDORES ACTIVOS\n\n{0}\n\nFábricas y distribuidores", proveedores);

                // Ventas realizadas
                int ventas = new CN_Venta().obtenerCorrelativo() - 1;
                if (ventas < 0) ventas = 0;
                Btn1.Text = string.Format("VENTAS REALIZADAS\n\n{0}\n\nComprobantes emitidos", ventas);

                // Compras realizadas
                int compras = new CN_Compra().obtenerCorrelativo() - 1;
                if (compras < 0) compras = 0;
                Btn5.Text = string.Format("COMPRAS REALIZADAS\n\n{0}\n\nÓrdenes de calzado", compras);

                // Clientes registrados
                int clientes = new CN_Cliente().Listar().Where(c => c.Estado == true).Count();
                Btn4.Text = string.Format("CLIENTES REGISTRADOS\n\n{0}\n\nCartera de clientes", clientes);

                // Usuarios activos
                int user = new CN_Usuario().Listar().Where(u => u.Estado == true).Count();
                Btn3.Text = string.Format("USUARIOS DEL SISTEMA\n\n{0}\n\nCuentas con acceso", user);
            }
            catch (Exception)
            {
                // Fallback seguro si la base de datos no está disponible temporalmente
            }
        }
    }
}
