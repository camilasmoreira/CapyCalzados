using CapaNegocio;
using System;
using System.Data;
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


        private void FrmInicio_Load(object sender, EventArgs e) // Maneja el evento de carga del formulario.
        {
            // Obtiene la cantidad de ventas y actualiza el texto del botón Btn1.
            int ventas = new CN_Venta().obtenerCorrelativo() - 1; // Obtiene el correlativo de ventas y resta 1.
            Btn1.Text = "Cantidad de Ventas: " + ventas; // Actualiza el texto del botón con la cantidad de ventas.

            // Obtiene la cantidad de productos activos y actualiza el texto del botón Btn2.
            int prod = new CN_Producto().Listar().Where(p => p.Estado == true).Count(); // Cuenta los productos activos.
            Btn2.Text = "Cantidad de Productos: " + prod; // Actualiza el texto del botón con la cantidad de productos.

            // Obtiene la cantidad de usuarios activos y actualiza el texto del botón Btn3.
            int user = new CN_Usuario().Listar().Where(u => u.Estado == true).Count(); // Cuenta los usuarios activos.
            Btn3.Text = "Cantidad de Usuarios: " + user; // Actualiza el texto del botón con la cantidad de usuarios.

            // Obtiene la cantidad de clientes activos y actualiza el texto del botón Btn4.
            int clientes = new CN_Cliente().Listar().Where(c => c.Estado == true).Count(); // Cuenta los clientes activos.
            Btn4.Text = "Cantidad de Clientes: " + clientes; // Actualiza el texto del botón con la cantidad de clientes.

            // Obtiene la cantidad de compras y actualiza el texto del botón Btn5.
            int compras = new CN_Compra().obtenerCorrelativo() - 1; // Obtiene el correlativo de compras y resta 1.
            Btn5.Text = "Cantidad de Compras: " + compras; // Actualiza el texto del botón con la cantidad de compras.

            // Obtiene la cantidad de proveedores activos y actualiza el texto del botón Btn6.
            int proveedores = new CN_Proveedor().Listar().Where(c => c.Estado == true).Count(); // Cuenta los proveedores activos.
            Btn6.Text = "Cantidad de Proveedores: " + proveedores; // Actualiza el texto del botón con la cantidad de proveedores.
        }
    }


}
