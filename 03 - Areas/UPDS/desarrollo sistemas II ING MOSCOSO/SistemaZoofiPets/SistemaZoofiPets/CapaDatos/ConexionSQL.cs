using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace CapaDatos
{
    public class ConexionSQL
    {
        private readonly string connectionString;

        public ConexionSQL()
        {
            connectionString = ConfigurationManager.ConnectionStrings["SistemaVeterinario"]?.ConnectionString
                             ?? "server=(localdb)\\MSSQLLocalDB;database=SistemaVeterinario;integrated security=true;TrustServerCertificate=true;";
        }

        protected SqlConnection GetConnection()
        {
            return new SqlConnection(connectionString);
        }

        protected void ExecuteNonQuery(string query, params SqlParameter[] parameters)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new SqlCommand(query, connection))
                {
                    if (parameters != null)
                        command.Parameters.AddRange(parameters);
                    command.ExecuteNonQuery();
                }
            }
        }

        protected T ExecuteScalar<T>(string query, params SqlParameter[] parameters)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new SqlCommand(query, connection))
                {
                    if (parameters != null)
                        command.Parameters.AddRange(parameters);
                    var result = command.ExecuteScalar();
                    return result != null && result != DBNull.Value ? (T)result : default(T);
                }
            }
        }

        protected DataTable ExecuteQuery(string query, params SqlParameter[] parameters)
        {
            using (var connection = GetConnection())
            {
                using (var command = new SqlCommand(query, connection))
                {
                    if (parameters != null)
                        command.Parameters.AddRange(parameters);
                    using (var adapter = new SqlDataAdapter(command))
                    {
                        var dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        return dataTable;
                    }
                }
            }
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

        public int InsertarUsuario(string nombre, string apellido, string email, string usuario, string contrasena, string telefono, string direccion)
        {
            if (string.IsNullOrWhiteSpace(nombre) || string.IsNullOrWhiteSpace(apellido) || 
                string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(usuario) || 
                string.IsNullOrWhiteSpace(contrasena))
                return 0;

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string query = @"INSERT INTO Usuarios (Usuario, Contrasena, Rol, Activo, FechaCreacion) 
                                   VALUES (@usuario, @contrasena, 'Admin', 1, GETDATE())";
                    
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@nombre", nombre.Trim());
                        cmd.Parameters.AddWithValue("@apellido", apellido.Trim());
                        cmd.Parameters.AddWithValue("@email", email.Trim());
                        cmd.Parameters.AddWithValue("@usuario", usuario.Trim());
                        cmd.Parameters.AddWithValue("@contrasena", contrasena.Trim());
                        cmd.Parameters.AddWithValue("@telefono", telefono?.Trim() ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@direccion", direccion?.Trim() ?? (object)DBNull.Value);
                        
                        return cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (SqlException ex)
            {
                if (ex.Number == 2627) // Violación de clave única
                    throw new Exception("El usuario o email ya existe en el sistema");
                
                throw new Exception($"Error al insertar usuario: {ex.Message}");
            }
            catch (Exception ex)
            {
                throw new Exception($"Error inesperado al insertar usuario: {ex.Message}");
            }
        }

        public int ModificarUsuario(string nombre, string apellido, string email, string usuario, string contrasena, string telefono, string direccion)
        {
            if (string.IsNullOrWhiteSpace(nombre) || string.IsNullOrWhiteSpace(apellido) || 
                string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(usuario) || 
                string.IsNullOrWhiteSpace(contrasena))
                return 0;

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string query = @"UPDATE Persona 
                                   SET nombre = @nombre, apellido = @apellido, email = @email, 
                                       contrasena = @contrasena, telefono = @telefono, direccion = @direccion
                                   WHERE usuario = @usuario AND activo = 1";
                    
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@nombre", nombre.Trim());
                        cmd.Parameters.AddWithValue("@apellido", apellido.Trim());
                        cmd.Parameters.AddWithValue("@email", email.Trim());
                        cmd.Parameters.AddWithValue("@usuario", usuario.Trim());
                        cmd.Parameters.AddWithValue("@contrasena", contrasena.Trim());
                        cmd.Parameters.AddWithValue("@telefono", telefono?.Trim() ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@direccion", direccion?.Trim() ?? (object)DBNull.Value);
                        
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
            if (string.IsNullOrWhiteSpace(usuario))
                return 0;

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    // Eliminación lógica en lugar de física
                    string query = "UPDATE Persona SET activo = 0 WHERE usuario = @usuario";
                    
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

        public DataTable ConsultarUsuariosDG()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string query = "SELECT id, nombre, apellido, email, usuario, telefono, direccion, fecha_creacion FROM Persona WHERE activo = 1 ORDER BY nombre, apellido";
                    
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        using (SqlDataAdapter data = new SqlDataAdapter(cmd))
                        {
                            DataTable tabla = new DataTable();
                            data.Fill(tabla);
                            return tabla;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al consultar usuarios: {ex.Message}");
            }
        }

        public bool UsuarioExiste(string usuario, string email = null)
        {
            if (string.IsNullOrWhiteSpace(usuario))
                return false;

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string query = "SELECT COUNT(*) FROM Persona WHERE (usuario = @usuario" + 
                                 (string.IsNullOrEmpty(email) ? "" : " OR email = @email") + ") AND activo = 1";
                    
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@usuario", usuario.Trim());
                        if (!string.IsNullOrEmpty(email))
                            cmd.Parameters.AddWithValue("@email", email.Trim());
                        
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