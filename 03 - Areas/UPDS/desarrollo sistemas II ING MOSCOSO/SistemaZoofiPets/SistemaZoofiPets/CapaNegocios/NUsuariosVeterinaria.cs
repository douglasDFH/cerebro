using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using CapaDatos;

namespace CapaNegocios
{
    public class NUsuariosVeterinaria
    {
        //Metodo Insertar que llama al metodo Insertar de la clase DUsuariosVeterinaria
        public static string Insertar(string user_name, string user_clave, string user_email, string user_rol)
        {
            // Validaciones de negocio
            if (string.IsNullOrWhiteSpace(user_name))
                throw new ArgumentException("El nombre de usuario es obligatorio");
            
            if (string.IsNullOrWhiteSpace(user_clave))
                throw new ArgumentException("La contraseña es obligatoria");
            
            if (user_clave.Length < 6)
                throw new ArgumentException("La contraseña debe tener al menos 6 caracteres");
            
            if (!string.IsNullOrWhiteSpace(user_email) && !IsValidEmail(user_email))
                throw new ArgumentException("El formato del email no es válido");

            DUsuariosVeterinaria obj = new DUsuariosVeterinaria();
            obj.Usuario_name = user_name.Trim();
            obj.Usuario_clave = user_clave.Trim();
            obj.Usuario_email = user_email?.Trim();
            obj.Usuario_rol = user_rol ?? "Usuario";
            obj.Activo = true;
            
            return obj.Insertar(obj);
        }

        public static string Editar(int user_id, string user_name, string user_clave, 
            string user_email, string user_rol)
        {
            // Validaciones de negocio
            if (user_id <= 0)
                throw new ArgumentException("ID de usuario inválido");
            
            if (string.IsNullOrWhiteSpace(user_name))
                throw new ArgumentException("El nombre de usuario es obligatorio");
            
            if (string.IsNullOrWhiteSpace(user_clave))
                throw new ArgumentException("La contraseña es obligatoria");
            
            if (user_clave.Length < 6)
                throw new ArgumentException("La contraseña debe tener al menos 6 caracteres");
            
            if (!string.IsNullOrWhiteSpace(user_email) && !IsValidEmail(user_email))
                throw new ArgumentException("El formato del email no es válido");

            DUsuariosVeterinaria obj = new DUsuariosVeterinaria();
            obj.Usuario_id = user_id;
            obj.Usuario_name = user_name.Trim();
            obj.Usuario_clave = user_clave.Trim();
            obj.Usuario_email = user_email?.Trim();
            obj.Usuario_rol = user_rol ?? "Usuario";
            
            return obj.Editar(obj);
        }

        public static string Eliminar(int user_id)
        {
            if (user_id <= 0)
                throw new ArgumentException("ID de usuario inválido");

            DUsuariosVeterinaria obj = new DUsuariosVeterinaria();
            obj.Usuario_id = user_id;
            return obj.Eliminar(obj);
        }

        public static DataTable Mostrar()
        {
            return new DUsuariosVeterinaria().Mostrar();
        }

        public static DataTable BuscarNombre(string textobuscar)
        {
            if (string.IsNullOrWhiteSpace(textobuscar))
                return Mostrar();

            DUsuariosVeterinaria obj = new DUsuariosVeterinaria();
            obj.Textobuscar = textobuscar.Trim();
            return obj.BuscarNombre(obj);
        }

        public static DataTable Login(string user_name, string user_clave)
        {
            if (string.IsNullOrWhiteSpace(user_name))
                throw new ArgumentException("El nombre de usuario es obligatorio");
            
            if (string.IsNullOrWhiteSpace(user_clave))
                throw new ArgumentException("La contraseña es obligatoria");

            DUsuariosVeterinaria obj = new DUsuariosVeterinaria();
            obj.Usuario_name = user_name.Trim();
            obj.Usuario_clave = user_clave.Trim();
            return obj.Login(obj);
        }

        // Método auxiliar para validar email
        private static bool IsValidEmail(string email)
        {
            try
            {
                return email.Contains("@") && email.Contains(".");
            }
            catch
            {
                return false;
            }
        }
    }
}