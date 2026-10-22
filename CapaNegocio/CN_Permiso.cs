using CapaDatos;
using CapaEntidad;
using System.Collections.Generic;

namespace CapaNegocio
{
    public class CN_Permiso
    {
        private CD_Permiso objcd_Permiso = new CD_Permiso();

        public List<Permiso> Listar(int IdUsuario)
        {
            return objcd_Permiso.Listar(IdUsuario);
        }

    }
}

