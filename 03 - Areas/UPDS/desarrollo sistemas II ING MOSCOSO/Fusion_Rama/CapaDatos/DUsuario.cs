using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace CapaDatos
{
    public class DUsuario : DbConnection
    {
        private int _idUsuario;
        private string _nombreUsuario;
        private string _clave;
        private string _email;
        private int? _idPersonal;
        private string _rol;
        private bool _estado;
        private DateTime? _fechaUltimoAcceso;
        private int _intentosLogin;
        private bool _bloqueado;
        private string _textoBuscar;

        public int IdUsuario { get => _idUsuario; set => _idUsuario = value; }
        public string NombreUsuario { get => _nombreUsuario; set => _nombreUsuario = value; }
        public string Clave { get => _clave; set => _clave = value; }
        public string Email { get => _email; set => _email = value; }
        public int? IdPersonal { get => _idPersonal; set => _idPersonal = value; }
        public string Rol { get => _rol; set => _rol = value; }
        public bool Estado { get => _estado; set => _estado = value; }
        public DateTime? FechaUltimoAcceso { get => _fechaUltimoAcceso; set => _fechaUltimoAcceso = value; }
        public int IntentosLogin { get => _intentosLogin; set => _intentosLogin = value; }
        public bool Bloqueado { get => _bloqueado; set => _bloqueado = value; }
        public string TextoBuscar { get => _textoBuscar; set => _textoBuscar = value; }
        public DUsuario() { }

        public DUsuario(int idUsuario, string nombreUsuario, string clave, string email, 
            string rol = "AUXILIAR", int? idPersonal = null)
        {
            IdUsuario = idUsuario;
            NombreUsuario = nombreUsuario;
            Clave = clave;
            Email = email;
            Rol = rol;
            IdPersonal = idPersonal;
            Estado = true;
            IntentosLogin = 0;
            Bloqueado = false;
        }
        public string Insertar(DUsuario usuario)
        {
            string rpta = string.Empty;
            using (SqlConnection connection = GetConnection())
            {
                connection.Open();
                try
                {
                    using (SqlCommand command = new SqlCommand("SP_InsertarUsuario", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@NombreUsuario", usuario.NombreUsuario);
                        command.Parameters.AddWithValue("@Clave", usuario.Clave);
                        command.Parameters.AddWithValue("@Email", usuario.Email);
                        command.Parameters.AddWithValue("@Rol", usuario.Rol ?? "AUXILIAR");
                        command.Parameters.AddWithValue("@IdPersonal", usuario.IdPersonal ?? (object)DBNull.Value);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                int idUsuario = Convert.ToInt32(reader["IdUsuario"]);
                                if (idUsuario > 0)
                                {
                                    usuario.IdUsuario = idUsuario;
                                    rpta = "OK";
                                }
                                else
                                {
                                    rpta = reader["Mensaje"].ToString();
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    rpta = ex.Message;
                }
            }
            return rpta;
        }
        public string Editar(DUsuario usuario)
        {
            string rpta = string.Empty;
            using (SqlConnection connection = GetConnection())
            {
                connection.Open();
                try
                {
                    using (SqlCommand command = new SqlCommand("SP_EditarUsuario", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@IdUsuario", usuario.IdUsuario);
                        command.Parameters.AddWithValue("@NombreUsuario", usuario.NombreUsuario);
                        command.Parameters.AddWithValue("@Clave", usuario.Clave);
                        command.Parameters.AddWithValue("@Email", usuario.Email);
                        command.Parameters.AddWithValue("@Rol", usuario.Rol ?? "AUXILIAR");
                        command.Parameters.AddWithValue("@IdPersonal", usuario.IdPersonal ?? (object)DBNull.Value);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                int resultado = Convert.ToInt32(reader["Resultado"]);
                                rpta = resultado == 1 ? "OK" : reader["Mensaje"].ToString();
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    rpta = ex.Message;
                }
            }
            return rpta;
        }
        public string Eliminar(DUsuario usuario)
        {
            string rpta = string.Empty;
            using (SqlConnection connection = GetConnection())
            {
                connection.Open();
                try
                {
                    using (SqlCommand command = new SqlCommand("SP_EliminarUsuario", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@IdUsuario", usuario.IdUsuario);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                int resultado = Convert.ToInt32(reader["Resultado"]);
                                rpta = resultado == 1 ? "OK" : reader["Mensaje"].ToString();
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    rpta = ex.Message;
                }
            }
            return rpta;
        }
        public DataTable Mostrar()
        {
            DataTable dtResultado = new DataTable("Usuario");
            using (SqlConnection connection = GetConnection())
            {
                connection.Open();
                try
                {
                    using (SqlCommand command = new SqlCommand("SP_MostrarUsuarios", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            adapter.Fill(dtResultado);
                        }
                    }
                }
                catch
                {
                    dtResultado = null;
                }
            }
            return dtResultado;
        }
        public DataTable BuscarPorNombre(DUsuario usuario)
        {
            DataTable dtResultado = new DataTable("Usuario");
            using (SqlConnection connection = GetConnection())
            {
                connection.Open();
                try
                {
                    using (SqlCommand command = new SqlCommand("SP_BuscarUsuarioPorNombre", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@TextoBuscar", usuario.TextoBuscar ?? "");

                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            adapter.Fill(dtResultado);
                        }
                    }
                }
                catch
                {
                    dtResultado = null;
                }
            }
            return dtResultado;
        }
        public DataTable Login(DUsuario usuario)
        {
            DataTable dtResultado = new DataTable("LoginResult");
            using (SqlConnection connection = GetConnection())
            {
                connection.Open();
                try
                {
                    string query = @"SELECT id AS UsuarioID, usuario AS NombreUsuario, 
                                           CONCAT(nombre, ' ', apellido) AS NombreCompleto, 
                                           email AS Email, rol AS Rol 
                                   FROM personal 
                                   WHERE usuario = @Usuario 
                                   AND contrasena = @Clave 
                                   AND activo = 1";
                    
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Usuario", usuario.NombreUsuario);
                        command.Parameters.AddWithValue("@Clave", usuario.Clave);

                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            adapter.Fill(dtResultado);
                        }
                        
                        // Actualizar fecha de último acceso si el login es exitoso
                        if (dtResultado.Rows.Count > 0)
                        {
                            string updateQuery = @"UPDATE personal 
                                                 SET fecha_ultimo_acceso = GETDATE() 
                                                 WHERE usuario = @Usuario";
                            using (SqlCommand updateCommand = new SqlCommand(updateQuery, connection))
                            {
                                updateCommand.Parameters.AddWithValue("@Usuario", usuario.NombreUsuario);
                                updateCommand.ExecuteNonQuery();
                            }
                        }
                    }
                }
                catch
                {
                    dtResultado = null;
                }
            }
            return dtResultado;
        }

        public string DesbloquearUsuario(int idUsuario)
        {
            string rpta = string.Empty;
            using (SqlConnection connection = GetConnection())
            {
                connection.Open();
                try
                {
                    using (SqlCommand command = new SqlCommand("SP_DesbloquearUsuario", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@IdUsuario", idUsuario);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                int resultado = Convert.ToInt32(reader["Resultado"]);
                                rpta = resultado == 1 ? "OK" : reader["Mensaje"].ToString();
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    rpta = ex.Message;
                }
            }
            return rpta;
        }

        public string CambiarClave(int idUsuario, string claveAnterior, string claveNueva)
        {
            string rpta = string.Empty;
            using (SqlConnection connection = GetConnection())
            {
                connection.Open();
                try
                {
                    using (SqlCommand command = new SqlCommand("SP_CambiarClave", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@IdUsuario", idUsuario);
                        command.Parameters.AddWithValue("@ClaveAnterior", claveAnterior);
                        command.Parameters.AddWithValue("@ClaveNueva", claveNueva);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                int resultado = Convert.ToInt32(reader["Resultado"]);
                                rpta = resultado == 1 ? "OK" : reader["Mensaje"].ToString();
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    rpta = ex.Message;
                }
            }
            return rpta;
        }
    }
}