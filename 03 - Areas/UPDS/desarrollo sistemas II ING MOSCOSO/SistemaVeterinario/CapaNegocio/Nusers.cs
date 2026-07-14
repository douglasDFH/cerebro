using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using CapaDatos;
using System.Security.Cryptography;

namespace CapaNegocio
{
    public class NUsuario
    {
        public static string Insertar(string nombreUsuario, string clave, string email, 
            string rol = "AUXILIAR", int? idPersonal = null)
        {
            if (string.IsNullOrWhiteSpace(nombreUsuario) || string.IsNullOrWhiteSpace(clave) || string.IsNullOrWhiteSpace(email))
                return "Nombre de usuario, contraseña y email son obligatorios";

            if (!ValidarEmail(email))
                return "El formato del email no es válido";

            if (clave.Length < 6)
                return "La contraseña debe tener al mínimo 6 caracteres";

            DUsuario obj = new DUsuario();
            obj.NombreUsuario = nombreUsuario;
            obj.Clave = EncriptarClave(clave);
            obj.Email = email;
            obj.Rol = rol;
            obj.IdPersonal = idPersonal;
            obj.Estado = true;

            return obj.Insertar(obj);
        }

        public static string Editar(int idUsuario, string nombreUsuario, string clave, string email,
            string rol = "AUXILIAR", int? idPersonal = null)
        {
            if (idUsuario <= 0)
                return "ID de usuario no válido";

            if (string.IsNullOrWhiteSpace(nombreUsuario) || string.IsNullOrWhiteSpace(clave) || string.IsNullOrWhiteSpace(email))
                return "Nombre de usuario, contraseña y email son obligatorios";

            if (!ValidarEmail(email))
                return "El formato del email no es válido";

            if (clave.Length < 6)
                return "La contraseña debe tener al mínimo 6 caracteres";

            DUsuario obj = new DUsuario();
            obj.IdUsuario = idUsuario;
            obj.NombreUsuario = nombreUsuario;
            obj.Clave = EncriptarClave(clave);
            obj.Email = email;
            obj.Rol = rol;
            obj.IdPersonal = idPersonal;

            return obj.Editar(obj);
        }

        public static string Eliminar(int idUsuario)
        {
            if (idUsuario <= 0)
                return "ID de usuario no válido";

            DUsuario obj = new DUsuario();
            obj.IdUsuario = idUsuario;
            return obj.Eliminar(obj);
        }

        public static DataTable Mostrar()
        {
            return new DUsuario().Mostrar();
        }

        public static DataTable BuscarPorNombre(string textoBuscar)
        {
            DUsuario obj = new DUsuario();
            obj.TextoBuscar = textoBuscar;
            return obj.BuscarPorNombre(obj);
        }

        public static DataTable Login(string usuario, string clave)
        {
            if (string.IsNullOrWhiteSpace(usuario) || string.IsNullOrWhiteSpace(clave))
                return null;

            DUsuario obj = new DUsuario();
            obj.NombreUsuario = usuario;
            obj.Clave = EncriptarClave(clave);
            return obj.Login(obj);
        }

        public static string DesbloquearUsuario(int idUsuario)
        {
            if (idUsuario <= 0)
                return "ID de usuario no válido";

            return new DUsuario().DesbloquearUsuario(idUsuario);
        }

        public static string CambiarClave(int idUsuario, string claveAnterior, string claveNueva)
        {
            if (idUsuario <= 0)
                return "ID de usuario no válido";

            if (string.IsNullOrWhiteSpace(claveAnterior) || string.IsNullOrWhiteSpace(claveNueva))
                return "Debe proporcionar la contraseña anterior y la nueva";

            if (claveNueva.Length < 6)
                return "La nueva contraseña debe tener al mínimo 6 caracteres";

            return new DUsuario().CambiarClave(idUsuario, EncriptarClave(claveAnterior), EncriptarClave(claveNueva));
        }

        private static string EncriptarClave(string clave)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(clave));
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        private static bool ValidarEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        public static bool ValidarRol(string rol)
        {
            string[] rolesValidos = { "ADMIN", "VETERINARIO", "AUXILIAR", "RECEPCIONISTA" };
            return rolesValidos.Contains(rol.ToUpper());
        }

        public static bool EsAdministrador(string rol)
        {
            return rol?.ToUpper() == "ADMIN";
        }

        public static bool EsVeterinario(string rol)
        {
            return rol?.ToUpper() == "VETERINARIO";
        }
    }
}
