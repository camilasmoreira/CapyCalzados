using CapaDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class CN_Backup
    {
        private CD_Backup objcd_backup = new CD_Backup();


        public string Backup()
        {
            return objcd_backup.Backup();
        }

        public string Restore(string ruta) {
            return objcd_backup.Restore(ruta);
        }

        public bool Backup(DateTime fecha)
        {
            return objcd_backup.Backup(fecha);
        }

    }
}
