using CapaDatos;
using CapaEntidad;
using System.Collections.Generic;

namespace CapaNegocio
{
    public class CN_Proveedor
    {
        private CD_Proveedor objcd_Proveedor = new CD_Proveedor();

        public List<Proveedor> Listar()
        {
            return objcd_Proveedor.Listar();
        }

        public int Registrar(Proveedor obj, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (obj.Documento == "")
            {
                Mensaje += "Es Necesario el Documento del Proveedor\n";
            }
            if (obj.RazonSocial == "")
            {
                Mensaje += "Es Necesario el Nombre de Proveedor\n";
            }
            if (obj.Direccion == "")
            {
                Mensaje += "Es Necesario la direccion del Proveedor\n";
            }
            if (obj.Telefono == "")
            {
                Mensaje += "Es Necesario la Clave del Proveedor\n";
            }
            if (obj.Correo == "")
            {
                Mensaje += "Es Necesario el Correo del Proveedor\n";
            }

            if (Mensaje != string.Empty)
            {
                return 0;
            }
            else
            {
                return objcd_Proveedor.Registrar(obj, out Mensaje);
            }

        }

        public bool Editar(Proveedor obj, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (obj.Documento == "")
            {
                Mensaje += "Es Necesario el Documento del Proveedor\n";
            }
            if (obj.RazonSocial == "")
            {
                Mensaje += "Es Necesario el Nombre de Proveedor\n";
            }
            if (obj.Direccion == "")
            {
                Mensaje += "Es Necesario la direccion del Proveedor\n";
            }
            if (obj.Telefono == "")
            {
                Mensaje += "Es Necesario la Clave del Proveedor\n";
            }
            if (obj.Correo == "")
            {
                Mensaje += "Es Necesario el Correo del Proveedor\n";
            }

            if (Mensaje != string.Empty)
            {
                return false;
            }
            else
            {
                return objcd_Proveedor.Editar(obj, out Mensaje);
            }
        }

        public bool Eliminar(Proveedor obj, out string Mensaje)
        {
            return objcd_Proveedor.Eliminar(obj, out Mensaje);
        }

    }
}
