using CapaDatos;
using CapaEntidad;
using System.Collections.Generic;

namespace CapaNegocio
{
    public class CN_Cliente
    {
        private CD_Cliente objcd_Cliente = new CD_Cliente();

        public List<Cliente> Listar()
        {
            return objcd_Cliente.Listar();
        }

        public int Registrar(Cliente obj, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (string.IsNullOrWhiteSpace(obj.NombreCompleto))
            {
                obj.NombreCompleto = $"{obj.Nombre} {obj.Apellido}".Trim();
            }

            if (string.IsNullOrWhiteSpace(obj.Documento))
            {
                Mensaje += "Es Necesario el Documento del Cliente\n";
            }
            if (string.IsNullOrWhiteSpace(obj.Nombre))
            {
                Mensaje += "Es Necesario el Nombre del Cliente\n";
            }
            if (string.IsNullOrWhiteSpace(obj.Apellido))
            {
                Mensaje += "Es Necesario el Apellido del Cliente\n";
            }
            if (string.IsNullOrWhiteSpace(obj.Direccion))
            {
                Mensaje += "Es Necesario la Dirección del Cliente\n";
            }
            if (string.IsNullOrWhiteSpace(obj.Telefono))
            {
                Mensaje += "Es Necesario el Teléfono del Cliente\n";
            }
            if (string.IsNullOrWhiteSpace(obj.Correo))
            {
                Mensaje += "Es Necesario el Correo del Cliente\n";
            }

            if (Mensaje != string.Empty)
            {
                return 0;
            }
            else
            {
                return objcd_Cliente.Registrar(obj, out Mensaje);
            }
        }

        public bool Editar(Cliente obj, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (string.IsNullOrWhiteSpace(obj.NombreCompleto))
            {
                obj.NombreCompleto = $"{obj.Nombre} {obj.Apellido}".Trim();
            }

            if (string.IsNullOrWhiteSpace(obj.Documento))
            {
                Mensaje += "Es Necesario el Documento del Cliente\n";
            }
            if (string.IsNullOrWhiteSpace(obj.Nombre))
            {
                Mensaje += "Es Necesario el Nombre del Cliente\n";
            }
            if (string.IsNullOrWhiteSpace(obj.Apellido))
            {
                Mensaje += "Es Necesario el Apellido del Cliente\n";
            }
            if (string.IsNullOrWhiteSpace(obj.Direccion))
            {
                Mensaje += "Es Necesario la Dirección del Cliente\n";
            }
            if (string.IsNullOrWhiteSpace(obj.Telefono))
            {
                Mensaje += "Es Necesario el Teléfono del Cliente\n";
            }
            if (string.IsNullOrWhiteSpace(obj.Correo))
            {
                Mensaje += "Es Necesario el Correo del Cliente\n";
            }

            if (Mensaje != string.Empty)
            {
                return false;
            }
            else
            {
                return objcd_Cliente.Editar(obj, out Mensaje);
            }
        }

        public bool Eliminar(Cliente obj, out string Mensaje)
        {
            return objcd_Cliente.Eliminar(obj, out Mensaje);
        }
    }
}
