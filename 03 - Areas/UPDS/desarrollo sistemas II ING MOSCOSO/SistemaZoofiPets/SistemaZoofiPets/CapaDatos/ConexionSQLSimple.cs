using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace CapaDatos
{
    public class ConexionSQLSimple
    {
        private readonly string connectionString;

        public ConexionSQLSimple()
        {
            connectionString = ConfigurationManager.ConnectionStrings["SistemaVeterinario"]?.ConnectionString
                             ?? "server=(localdb)\\MSSQLLocalDB;database=SistemaVeterinario;integrated security=true;TrustServerCertificate=true;";
        }

        public int ConsultaLogin(string usuario, string contrasena)
        {
            if (string.IsNullOrWhiteSpace(usuario) || string.IsNullOrWhiteSpace(contrasena))
                return 0;

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string query = "SELECT COUNT(*) FROM Usuarios WHERE Usuario = @usuario AND Contrasena = @contrasena AND Activo = 1";
                    
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@usuario", usuario.Trim());
                        cmd.Parameters.AddWithValue("@contrasena", contrasena.Trim());
                        
                        return Convert.ToInt32(cmd.ExecuteScalar());
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en consulta de login: {ex.Message}");
            }
        }

        public DataTable ConsultarUsuariosDG()
        {
            try
            {
                // Crear una tabla con las columnas que espera el FormUsers
                DataTable tabla = new DataTable();
                tabla.Columns.Add("id", typeof(int));
                tabla.Columns.Add("nombre", typeof(string));
                tabla.Columns.Add("apellido", typeof(string));
                tabla.Columns.Add("email", typeof(string));
                tabla.Columns.Add("usuario", typeof(string));
                tabla.Columns.Add("telefono", typeof(string));
                tabla.Columns.Add("direccion", typeof(string));
                tabla.Columns.Add("fecha_creacion", typeof(DateTime));

                // Datos de ejemplo que coinciden con la estructura esperada
                tabla.Rows.Add(1, "Administrador", "Sistema", "admin@zoofipets.com", "admin", "555-0001", "Oficina Principal", DateTime.Now.AddDays(-30));
                tabla.Rows.Add(2, "Dr. Juan", "García", "dr.garcia@veterinaria.com", "dr.garcia", "555-0002", "Clínica Veterinaria", DateTime.Now.AddDays(-20));
                tabla.Rows.Add(3, "Dra. María", "Fernández", "dra.fernandez@veterinaria.com", "dra.fernandez", "555-0003", "Clínica Veterinaria", DateTime.Now.AddDays(-15));
                tabla.Rows.Add(4, "Auxiliar Luis", "Martínez", "aux.martinez@veterinaria.com", "aux.martinez", "555-0004", "Clínica Veterinaria", DateTime.Now.AddDays(-10));

                return tabla;
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
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string query = @"INSERT INTO Usuarios (Usuario, Contrasena, Rol, Activo, FechaCreacion) 
                                   VALUES (@usuario, @contrasena, 'Admin', 1, GETDATE())";
                    
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@usuario", usuario.Trim());
                        cmd.Parameters.AddWithValue("@contrasena", contrasena.Trim());
                        
                        return cmd.ExecuteNonQuery();
                    }
                }
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
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string query = @"UPDATE Usuarios 
                                   SET Contrasena = @contrasena
                                   WHERE Usuario = @usuario AND Activo = 1";
                    
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@usuario", usuario.Trim());
                        cmd.Parameters.AddWithValue("@contrasena", contrasena.Trim());
                        
                        return cmd.ExecuteNonQuery();
                    }
                }
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
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string query = "UPDATE Usuarios SET Activo = 0 WHERE Usuario = @usuario";
                    
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@usuario", usuario.Trim());
                        return cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al eliminar usuario: {ex.Message}");
            }
        }

        public bool UsuarioExiste(string usuario, string email = null)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string query = "SELECT COUNT(*) FROM Usuarios WHERE Usuario = @usuario AND Activo = 1";
                    
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@usuario", usuario.Trim());
                        
                        return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al verificar existencia de usuario: {ex.Message}");
            }
        }
    }
}