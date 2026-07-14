using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace CapaDatos
{
    public class DUsuariosVeterinaria : DbConnectionVeterinaria
    {
        //campos y propiedades
        private int _usuario_id;
        private string _usuario_name;
        private string _usuario_clave;
        private string _usuario_email;
        private string _usuario_rol;
        private bool _activo;
        private string _textobuscar;

        //Propiedades
        public int Usuario_id { get => _usuario_id; set => _usuario_id = value; }
        public string Usuario_name { get => _usuario_name; set => _usuario_name = value; }
        public string Usuario_clave { get => _usuario_clave; set => _usuario_clave = value; }
        public string Usuario_email { get => _usuario_email; set => _usuario_email = value; }
        public string Usuario_rol { get => _usuario_rol; set => _usuario_rol = value; }
        public bool Activo { get => _activo; set => _activo = value; }
        public string Textobuscar { get => _textobuscar; set => _textobuscar = value; }

        //Constructores
        public DUsuariosVeterinaria() { }
        
        public DUsuariosVeterinaria(int usuario_id, string usuario_name, string usuario_clave,
            string usuario_email, string usuario_rol, bool activo, string textobuscar)
        {
            Usuario_id = usuario_id;
            Usuario_name = usuario_name;
            Usuario_clave = usuario_clave;
            Usuario_email = usuario_email;
            Usuario_rol = usuario_rol;
            Activo = activo;
            Textobuscar = textobuscar;
        }

        //Metodo Insertar
        public string Insertar(DUsuariosVeterinaria usuario)
        {
            string rpta = string.Empty;
            using (SqlConnection connection = GetConnection())
            {
                try
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandText = @"INSERT INTO Usuarios (Usuario, Contrasena, Rol, Activo, FechaCreacion) 
                                               VALUES (@usuario, @contrasena, @rol, @activo, GETDATE())";
                        command.CommandType = CommandType.Text;

                        command.Parameters.AddWithValue("@usuario", usuario.Usuario_name);
                        command.Parameters.AddWithValue("@contrasena", usuario.Usuario_clave);
                        command.Parameters.AddWithValue("@rol", usuario.Usuario_rol ?? "Usuario");
                        command.Parameters.AddWithValue("@activo", usuario.Activo);

                        rpta = command.ExecuteNonQuery() == 1 ? "OK" : "NO SE INGRESO EL REGISTRO";
                    }
                }
                catch (Exception ex)
                {
                    rpta = ex.Message;
                }
                finally 
                { 
                    if (connection.State == ConnectionState.Open) 
                        connection.Close(); 
                }
            }
            return rpta;
        }

        //Metodo Editar
        public string Editar(DUsuariosVeterinaria usuario)
        {
            string rpta = string.Empty;
            using (SqlConnection connection = GetConnection())
            {
                try
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandText = @"UPDATE Usuarios 
                                               SET Contrasena = @contrasena, Rol = @rol 
                                               WHERE IdUsuario = @id AND Activo = 1";
                        command.CommandType = CommandType.Text;

                        command.Parameters.AddWithValue("@id", usuario.Usuario_id);
                        command.Parameters.AddWithValue("@contrasena", usuario.Usuario_clave);
                        command.Parameters.AddWithValue("@rol", usuario.Usuario_rol ?? "Usuario");

                        rpta = command.ExecuteNonQuery() == 1 ? "OK" : "NO SE ACTUALIZO EL REGISTRO";
                    }
                }
                catch (Exception ex)
                {
                    rpta = ex.Message;
                }
                finally 
                { 
                    if (connection.State == ConnectionState.Open) 
                        connection.Close(); 
                }
            }
            return rpta;
        }

        //Metodo Eliminar (logico)
        public string Eliminar(DUsuariosVeterinaria usuario)
        {
            string rpta = string.Empty;
            using (SqlConnection connection = GetConnection())
            {
                try
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandText = "UPDATE Usuarios SET Activo = 0 WHERE IdUsuario = @id";
                        command.CommandType = CommandType.Text;

                        command.Parameters.AddWithValue("@id", usuario.Usuario_id);

                        rpta = command.ExecuteNonQuery() == 1 ? "OK" : "NO SE ELIMINO EL REGISTRO";
                    }
                }
                catch (Exception ex)
                {
                    rpta = ex.Message;
                }
                finally 
                { 
                    if (connection.State == ConnectionState.Open) 
                        connection.Close(); 
                }
            }
            return rpta;
        }

        //Metodo Mostrar
        public DataTable Mostrar()
        {
            DataTable DtResultado = new DataTable("usuarios");
            using (SqlConnection connection = GetConnection())
            {
                try
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandText = @"SELECT IdUsuario as id, Usuario as usuario, 
                                               Email as email, Rol as rol, 
                                               CASE WHEN Activo = 1 THEN 'Activo' ELSE 'Inactivo' END as estado,
                                               FechaCreacion as fecha_creacion
                                               FROM Usuarios 
                                               WHERE Activo = 1
                                               ORDER BY Usuario";
                        command.CommandType = CommandType.Text;

                        SqlDataAdapter sqlDat = new SqlDataAdapter(command);
                        sqlDat.Fill(DtResultado);
                    }
                }
                catch (Exception)
                {
                    DtResultado = null;
                }
            }
            return DtResultado;
        }

        //Metodo BuscarNombre
        public DataTable BuscarNombre(DUsuariosVeterinaria usuario)
        {
            DataTable DtResultado = new DataTable("usuarios");
            using (SqlConnection connection = GetConnection())
            {
                try
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandText = @"SELECT IdUsuario as id, Usuario as usuario, 
                                               Email as email, Rol as rol, 
                                               CASE WHEN Activo = 1 THEN 'Activo' ELSE 'Inactivo' END as estado,
                                               FechaCreacion as fecha_creacion
                                               FROM Usuarios 
                                               WHERE Usuario LIKE @textbuscar AND Activo = 1
                                               ORDER BY Usuario";
                        command.CommandType = CommandType.Text;

                        command.Parameters.AddWithValue("@textbuscar", "%" + usuario.Textobuscar + "%");

                        SqlDataAdapter sqlDat = new SqlDataAdapter(command);
                        sqlDat.Fill(DtResultado);
                    }
                }
                catch (Exception)
                {
                    DtResultado = null;
                }
            }
            return DtResultado;
        }

        //Metodo Login
        public DataTable Login(DUsuariosVeterinaria usuario)
        {
            DataTable DtResultado = new DataTable("login");
            using (SqlConnection connection = GetConnection())
            {
                try
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandText = @"SELECT IdUsuario, Usuario, Email, Rol 
                                               FROM Usuarios 
                                               WHERE Usuario = @usuario AND Contrasena = @clave AND Activo = 1";
                        command.CommandType = CommandType.Text;

                        command.Parameters.AddWithValue("@usuario", usuario.Usuario_name);
                        command.Parameters.AddWithValue("@clave", usuario.Usuario_clave);

                        SqlDataAdapter sqlDat = new SqlDataAdapter(command);
                        sqlDat.Fill(DtResultado);
                    }
                }
                catch (Exception)
                {
                    DtResultado = null;
                }
            }
            return DtResultado;
        }
    }
}