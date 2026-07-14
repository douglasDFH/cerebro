using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using CapaDatos;

namespace CapaNegocio
{
    public class Nusers
    {
        //Metodo Insertar que llama al metodo Insertar de la clase Dusers de la capa datos
        public static string Insertar(string user_name, string user_clave, string user_email)
        {
            Dusers Obj = new Dusers();
            Obj.Usuario_name = user_name;
            Obj.Usuario_clave = user_clave;
            Obj.Usuario_email = user_email;
            return Obj.Insertar(Obj);
        }
        public static string Editar(int user_id, string user_name, string user_clave, 
            string user_email)
        {
            Dusers Obj = new Dusers();
            Obj.Usuario_id = user_id;
            Obj.Usuario_name = user_name;
            Obj.Usuario_clave = user_clave;
            Obj.Usuario_email = user_email;
            return Obj.Editar(Obj);
        }
        public static string Eliminar(int user_id)
        {
            Dusers Obj = new Dusers();
            Obj.Usuario_id = user_id;
            return Obj.Eliminar(Obj);
        }
        public static DataTable Mostrar()
        {
            return new Dusers().Mostrar();
        }
        public static DataTable BuscarNombre(string textobuscar)
        {
            Dusers Obj = new Dusers();
            Obj.Textobuscar = textobuscar;
            return Obj.BuscarNombre(Obj);
        }
        public static DataTable Login(string user_name, string user_clave)
        {
            Dusers Obj = new Dusers();
            Obj.Usuario_name = user_name;
            Obj.Usuario_clave = user_clave;
            return Obj.Login(Obj);
        }

        // Métodos para sistema de roles
        public static DataTable LoginWithRole(string usuario_name, string usuario_clave)
        {
            Dusers Obj = new Dusers();
            Obj.Usuario_name = usuario_name;
            Obj.Usuario_clave = usuario_clave;
            return Obj.LoginWithRole(Obj);
        }

        public static bool CheckUserPermission(int usuarioId, string permissionName)
        {
            Dusers Obj = new Dusers();
            return Obj.CheckUserPermission(usuarioId, permissionName);
        }

        public static DataTable GetUserPermissions(int usuarioId)
        {
            Dusers Obj = new Dusers();
            return Obj.GetUserPermissions(usuarioId);
        }

        public static DataTable MostrarConRoles()
        {
            return new Dusers().MostrarConRoles();
        }

        public static DataTable GetRoles()
        {
            return new Dusers().GetRoles();
        }

        // Método para validar credenciales - sin establecer sesión para evitar referencia circular
        public static DataTable ValidarCredenciales(string usuario_name, string usuario_clave)
        {
            try
            {
                return LoginWithRole(usuario_name, usuario_clave);
            }
            catch
            {
                return null;
            }
        }
    }
}
