using System;
using System.Data;
using System.Data.SqlClient;
using CapaDatos.Entidades;

namespace CapaDatos.Repositorios
{
    public class UsuarioRepositorio
    {
        private readonly ConexionSQLSimple conexion;

        public UsuarioRepositorio()
        {
            conexion = new ConexionSQLSimple();
        }

        public Usuario ValidarLogin(string usuario, string contrasena)
        {
            if (string.IsNullOrWhiteSpace(usuario) || string.IsNullOrWhiteSpace(contrasena))
                return null;

            try
            {
                int resultado = conexion.ConsultaLogin(usuario, contrasena);
                
                if (resultado > 0)
                {
                    // Crear un usuario básico para compatibilidad
                    return new Usuario
                    {
                        IdUsuario = 1,
                        UsuarioLogin = usuario,
                        Rol = "Admin",
                        NombreCompleto = "Usuario del Sistema",
                        TipoPersonal = "Admin"
                    };
                }
                
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en validación de login: {ex.Message}");
            }
        }

        public bool CrearUsuario(Usuario usuario, int? idPersonal = null)
        {
            if (usuario == null || string.IsNullOrWhiteSpace(usuario.UsuarioLogin) || 
                string.IsNullOrWhiteSpace(usuario.Contrasena))
                return false;

            try
            {
                // Verificar si el usuario ya existe
                if (UsuarioExiste(usuario.UsuarioLogin))
                    throw new Exception("El usuario ya existe en el sistema");

                int resultado = conexion.InsertarUsuario("", "", "", usuario.UsuarioLogin, usuario.Contrasena, "", "");
                return resultado > 0;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al crear usuario: {ex.Message}");
            }
        }

        public bool UsuarioExiste(string usuario)
        {
            if (string.IsNullOrWhiteSpace(usuario))
                return false;

            try
            {
                return conexion.UsuarioExiste(usuario);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al verificar existencia de usuario: {ex.Message}");
            }
        }

        public DataTable ObtenerUsuarios()
        {
            try
            {
                return conexion.ConsultarUsuariosDG();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al consultar usuarios: {ex.Message}");
            }
        }
    }
}