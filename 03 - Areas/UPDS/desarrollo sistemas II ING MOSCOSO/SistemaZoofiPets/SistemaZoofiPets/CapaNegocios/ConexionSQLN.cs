using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDatos;
using CapaDatos.Repositorios;
using CapaDatos.Entidades;

namespace CapaNegocios
{
    public class ConexionSQLN
    {
        private readonly ConexionSQLSimple cn = new ConexionSQLSimple();

        public int ValidarLogin(string usuario, string contrasena)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(usuario) || string.IsNullOrWhiteSpace(contrasena))
                    return 0;

                return cn.ConsultaLogin(usuario, contrasena);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en validación de login: {ex.Message}");
            }
        }

        public DataTable ConsultarUsuarios()
        {
            try
            {
                return cn.ConsultarUsuariosDG();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al consultar usuarios: {ex.Message}");
            }
        }

        public int InsertarUsuario(string nombre, string apellido, string email, string usuario, string contrasena, string telefono, string direccion)
        {
            try
            {
                // Validaciones de negocio
                if (string.IsNullOrWhiteSpace(nombre?.Trim()))
                    throw new ArgumentException("El nombre es obligatorio");
                
                if (string.IsNullOrWhiteSpace(apellido?.Trim()))
                    throw new ArgumentException("El apellido es obligatorio");
                
                if (string.IsNullOrWhiteSpace(email?.Trim()))
                    throw new ArgumentException("El email es obligatorio");
                
                if (string.IsNullOrWhiteSpace(usuario?.Trim()))
                    throw new ArgumentException("El usuario es obligatorio");
                
                if (string.IsNullOrWhiteSpace(contrasena?.Trim()))
                    throw new ArgumentException("La contraseña es obligatoria");
                
                if (contrasena.Length < 6)
                    throw new ArgumentException("La contraseña debe tener al menos 6 caracteres");

                // Validar formato de email básico
                if (!email.Contains("@") || !email.Contains("."))
                    throw new ArgumentException("El formato del email no es válido");

                // Verificar si el usuario ya existe
                if (cn.UsuarioExiste(usuario))
                    throw new Exception("El usuario ya existe en el sistema");

                return cn.InsertarUsuario(nombre, apellido, email, usuario, contrasena, telefono, direccion);
            }
            catch (ArgumentException)
            {
                throw; // Re-lanzar excepciones de validación sin modificar
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al insertar usuario: {ex.Message}");
            }
        }

        public int ModificarUsuario(string nombre, string apellido, string email, string usuario, string contrasena, string telefono, string direccion)
        {
            try
            {
                // Validaciones de negocio
                if (string.IsNullOrWhiteSpace(nombre?.Trim()))
                    throw new ArgumentException("El nombre es obligatorio");
                
                if (string.IsNullOrWhiteSpace(apellido?.Trim()))
                    throw new ArgumentException("El apellido es obligatorio");
                
                if (string.IsNullOrWhiteSpace(email?.Trim()))
                    throw new ArgumentException("El email es obligatorio");
                
                if (string.IsNullOrWhiteSpace(usuario?.Trim()))
                    throw new ArgumentException("El usuario es obligatorio");
                
                if (string.IsNullOrWhiteSpace(contrasena?.Trim()))
                    throw new ArgumentException("La contraseña es obligatoria");

                // Validar formato de email básico
                if (!email.Contains("@") || !email.Contains("."))
                    throw new ArgumentException("El formato del email no es válido");

                return cn.ModificarUsuario(nombre, apellido, email, usuario, contrasena, telefono, direccion);
            }
            catch (ArgumentException)
            {
                throw; // Re-lanzar excepciones de validación sin modificar
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al modificar usuario: {ex.Message}");
            }
        }

        public int EliminarUsuario(string usuario)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(usuario?.Trim()))
                    throw new ArgumentException("El usuario es obligatorio para eliminar");

                return cn.EliminarUsuario(usuario);
            }
            catch (ArgumentException)
            {
                throw; // Re-lanzar excepciones de validación sin modificar
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al eliminar usuario: {ex.Message}");
            }
        }

        public bool ExisteUsuario(string usuario, string email = null)
        {
            try
            {
                return cn.UsuarioExiste(usuario, email);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al verificar existencia de usuario: {ex.Message}");
            }
        }
    }
}
