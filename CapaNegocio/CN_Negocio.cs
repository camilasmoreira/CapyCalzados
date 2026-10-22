using CapaDatos;
using CapaEntidad;

namespace CapaNegocio
{
    public class CN_Negocio
    {
        private CD_Negocio objcd_Negocio = new CD_Negocio();

        public Negocio obtenerDatos()
        {
            return objcd_Negocio.ObtenerDatos();
        }

        public bool guardarDatos(Negocio obj, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (obj.Nombre == "")
            {
                Mensaje += "Es Necesario el nombre del Negocio\n";
            }
            if (obj.RUC == "")
            {
                Mensaje += "Es Necesario el RUC de Negocio\n";
            }
            if (obj.Direccion == "")
            {
                Mensaje += "Es Necesario la direccion del Negocio\n";
            }

            if (Mensaje != string.Empty)
            {
                return false;
            }
            else
            {
                return objcd_Negocio.guardarDatos(obj, out Mensaje);
            }

        }

        public byte[] obtenerLogo(out bool obtenido)
        {
            return objcd_Negocio.obtenerLogo(out obtenido);
        }


        public bool actualizarLogo(byte[] img, out string mensaje)
        {
            return objcd_Negocio.actualizarLogo(img, out mensaje);
        }
    }
}
