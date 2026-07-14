using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using CapaDatos;

namespace CapaNegocio
{
    public class NUsuario
    {
        public static string Insertar(int idUsuario, string nombreUsuario, string clave, string email, 
            string rol, int? idPersonal)
        {
            DUsuario objUsuario = new DUsuario()
            {
                IdUsuario = idUsuario,
                NombreUsuario = nombreUsuario,
                Clave = clave,
                Email = email,
                Rol = rol,
                IdPersonal = idPersonal
            };
            return objUsuario.Insertar(objUsuario);
        }

        public static string Editar(int idUsuario, string nombreUsuario, string clave, string email,
            string rol, int? idPersonal)
        {
            DUsuario objUsuario = new DUsuario()
            {
                IdUsuario = idUsuario,
                NombreUsuario = nombreUsuario,
                Clave = clave,
                Email = email,
                Rol = rol,
                IdPersonal = idPersonal
            };
            return objUsuario.Editar(objUsuario);
        }

        public static string Eliminar(int idUsuario)
        {
            DUsuario objUsuario = new DUsuario()
            {
                IdUsuario = idUsuario
            };
            return objUsuario.Eliminar(objUsuario);
        }

        public static DataTable Mostrar()
        {
            return new DUsuario().Mostrar();
        }

        public static DataTable BuscarPorNombre(string textoBuscar)
        {
            DUsuario objUsuario = new DUsuario()
            {
                TextoBuscar = textoBuscar
            };
            return objUsuario.BuscarPorNombre(objUsuario);
        }

        public static DataTable Login(string nombreUsuario, string clave)
        {
            DUsuario objUsuario = new DUsuario()
            {
                NombreUsuario = nombreUsuario,
                Clave = clave
            };
            return objUsuario.Login(objUsuario);
        }

        public static string DesbloquearUsuario(int idUsuario)
        {
            return new DUsuario().DesbloquearUsuario(idUsuario);
        }

        public static string CambiarClave(int idUsuario, string claveAnterior, string claveNueva)
        {
            return new DUsuario().CambiarClave(idUsuario, claveAnterior, claveNueva);
        }

        public static bool ValidarLogin(string nombreUsuario, string contrasena)
        {
            DataTable resultado = Login(nombreUsuario, contrasena);
            return resultado != null && resultado.Rows.Count > 0;
        }
    }
}