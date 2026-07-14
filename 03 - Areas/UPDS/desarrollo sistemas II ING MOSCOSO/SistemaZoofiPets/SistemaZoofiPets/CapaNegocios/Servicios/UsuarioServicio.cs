using System;
using System.Data;
using CapaDatos.Repositorios;
using CapaDatos.Entidades;

namespace CapaNegocios.Servicios
{
    public class UsuarioServicio
    {
        private readonly UsuarioRepositorio usuarioRepo;

        public UsuarioServicio()
        {
            usuarioRepo = new UsuarioRepositorio();
        }

        public Usuario IniciarSesion(string usuario, string contrasena)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(usuario) || string.IsNullOrWhiteSpace(contrasena))
                    throw new ArgumentException("Usuario y contraseña son requeridos");

                return usuarioRepo.ValidarLogin(usuario, contrasena);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al iniciar sesión: {ex.Message}");
            }
        }

        public bool CrearUsuario(string usuarioLogin, string contrasena, string rol, int? idPersonal = null)
        {
            try
            {
                ValidarDatosUsuario(usuarioLogin, contrasena, rol);

                var usuario = new Usuario
                {
                    UsuarioLogin = usuarioLogin,
                    Contrasena = contrasena,
                    Rol = rol
                };

                return usuarioRepo.CrearUsuario(usuario, idPersonal);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al crear usuario: {ex.Message}");
            }
        }

        public DataTable ConsultarUsuarios()
        {
            try
            {
                return usuarioRepo.ObtenerUsuarios();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al consultar usuarios: {ex.Message}");
            }
        }

        public bool VerificarExistenciaUsuario(string usuario)
        {
            try
            {
                return usuarioRepo.UsuarioExiste(usuario);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al verificar usuario: {ex.Message}");
            }
        }

        private void ValidarDatosUsuario(string usuario, string contrasena, string rol)
        {
            if (string.IsNullOrWhiteSpace(usuario))
                throw new ArgumentException("El usuario es requerido");

            if (string.IsNullOrWhiteSpace(contrasena))
                throw new ArgumentException("La contraseña es requerida");

            if (string.IsNullOrWhiteSpace(rol))
                throw new ArgumentException("El rol es requerido");

            if (usuario.Length < 3)
                throw new ArgumentException("El usuario debe tener al menos 3 caracteres");

            if (contrasena.Length < 6)
                throw new ArgumentException("La contraseña debe tener al menos 6 caracteres");

            string[] rolesValidos = { "Admin", "Veterinario", "Auxiliar", "Recepcionista" };
            if (Array.IndexOf(rolesValidos, rol) == -1)
                throw new ArgumentException("Rol no válido");

            if (usuarioRepo.UsuarioExiste(usuario))
                throw new Exception("El usuario ya existe en el sistema");
        }
    }
}