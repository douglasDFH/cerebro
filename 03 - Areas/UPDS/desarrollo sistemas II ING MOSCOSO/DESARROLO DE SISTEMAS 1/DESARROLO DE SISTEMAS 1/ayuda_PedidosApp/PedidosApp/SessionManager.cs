using System;
using System.Data;
using System.Windows.Forms;
using CapaNegocio;

namespace PedidosApp
{
    public static class SessionManager
    {
        public static bool IniciarSesion(string usuario_name, string usuario_clave)
        {
            try
            {
                var loginResult = Nusers.ValidarCredenciales(usuario_name, usuario_clave);
                
                if (loginResult != null && loginResult.Rows.Count > 0)
                {
                    var row = loginResult.Rows[0];
                    var userSession = UserSession.Instance;
                    
                    userSession.SetUserData(
                        Convert.ToInt32(row["usuario_id"]),
                        row["usuario_name"].ToString(),
                        row["usuario_email"].ToString(),
                        Convert.ToInt32(row["role_id"]),
                        row["role_name"].ToString(),
                        row["role_description"].ToString()
                    );
                    
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al iniciar sesión: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public static void CerrarSesion()
        {
            UserSession.Instance.ClearSession();
        }

        public static bool VerificarSesion()
        {
            return UserSession.Instance.IsLoggedIn();
        }

        public static void MostrarErrorPermisos(string accion)
        {
            MessageBox.Show($"No tiene permisos para {accion}.", "Acceso Denegado", 
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        public static bool VerificarPermiso(string permiso, string accion = null)
        {
            if (!UserSession.Instance.IsLoggedIn())
            {
                MessageBox.Show("Debe iniciar sesión para continuar.", "Sesión Requerida", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            if (!UserSession.Instance.HasPermission(permiso))
            {
                if (!string.IsNullOrEmpty(accion))
                    MostrarErrorPermisos(accion);
                return false;
            }

            return true;
        }
    }
}