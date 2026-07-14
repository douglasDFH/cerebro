using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CapaEntidad;
using System.Data;
using CapaDatos;

namespace CapaNegocio
{
    public class CN_Permiso
    {
        private CD_PERMISO objcd_Permiso = new CD_PERMISO();

        public List<Permiso> Listar(int IdUsuario )
        {
            return objcd_Permiso.Listar(IdUsuario);
        }
    }
}
