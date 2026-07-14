using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace CapaDatos
{
    public class Dusers : DbConnection
    {
        //campos y propiedades
        private int _usuario_id;
        private string _usuario_name;
        private string _usuario_clave;
        private string _usuario_email;
        private string _textobuscar;
        //Refactorizar las variables privadas para generar las propiedades
        public int Usuario_id { get => _usuario_id; set => _usuario_id = value; }
        public string Usuario_name { get => _usuario_name; set => _usuario_name = value; }
        public string Usuario_clave { get => _usuario_clave; set => _usuario_clave = value; }
        public string Usuario_email { get => _usuario_email; set => _usuario_email = value; }
        public string Textobuscar { get => _textobuscar; set => _textobuscar = value; }
        //Constructores
        public Dusers() { }
        public Dusers(int usuario_id, string usuario_name, string usuario_clave,
            string usuario_email, string textobuscar)
        {
            Usuario_id = usuario_id;
            Usuario_name = usuario_name;
            Usuario_clave = usuario_clave;
            Usuario_email = usuario_email;
            Textobuscar = textobuscar;
        }
        //Metodo Insertar
        public string Insertar(Dusers Users)
        {
            string rpta = string.Empty;
            using (SqlConnection connection = GetConnection())
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand())
                {
                    try
                    {
                        command.Connection = connection;
                        command.CommandText = "spinsertar_users";
                        command.CommandType = CommandType.StoredProcedure;

                        SqlParameter Paruser_id = new SqlParameter();
                        Paruser_id.ParameterName = "@usuario_id";
                        Paruser_id.SqlDbType = SqlDbType.Int;
                        Paruser_id.Direction = ParameterDirection.Output;
                        command.Parameters.Add(Paruser_id);

                        SqlParameter Paruser_name = new SqlParameter();
                        Paruser_name.ParameterName = "@usuario_name";
                        Paruser_name.SqlDbType = SqlDbType.VarChar;
                        Paruser_name.Size = 50;
                        Paruser_name.Value = Users.Usuario_name;
                        command.Parameters.Add(Paruser_name);

                        SqlParameter Paruser_clave = new SqlParameter();
                        Paruser_clave.ParameterName = "@usuario_clave";
                        Paruser_clave.SqlDbType = SqlDbType.VarChar;
                        Paruser_clave.Size = 250;
                        Paruser_clave.Value = Users.Usuario_clave;
                        command.Parameters.Add(Paruser_clave);

                        SqlParameter Paruser_email = new SqlParameter();
                        Paruser_email.ParameterName = "@usuario_email";
                        Paruser_email.SqlDbType = SqlDbType.VarChar;
                        Paruser_email.Size = 100;
                        Paruser_email.Value = Users.Usuario_email;
                        command.Parameters.Add(Paruser_email);
                        //Ejecutamos el comando
                        rpta = command.ExecuteNonQuery() == 1 ? "OK" : "NO SE INGRESO EL REGISTRO";
                    }
                    catch (Exception ex)
                    {
                        rpta = ex.Message;
                    }
                    finally { if (connection.State == ConnectionState.Open) connection.Close(); }
                }
            }
            return rpta;
        }
        //Metodo Editar
        public string Editar(Dusers Users)
        {
            string rpta = string.Empty;
            using (SqlConnection connection = GetConnection())
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand())
                {
                    try
                    {
                        command.Connection = connection;
                        command.CommandText = "speditar_users";
                        command.CommandType = CommandType.StoredProcedure;

                        SqlParameter Paruser_id = new SqlParameter();
                        Paruser_id.ParameterName = "@usuario_id";
                        Paruser_id.SqlDbType = SqlDbType.Int;
                        Paruser_id.Value = Users.Usuario_id;
                        command.Parameters.Add(Paruser_id);

                        SqlParameter Paruser_name = new SqlParameter();
                        Paruser_name.ParameterName = "@usuario_name";
                        Paruser_name.SqlDbType = SqlDbType.VarChar;
                        Paruser_name.Size = 50;
                        Paruser_name.Value = Users.Usuario_name;
                        command.Parameters.Add(Paruser_name);

                        SqlParameter Paruser_clave = new SqlParameter();
                        Paruser_clave.ParameterName = "@usuario_clave";
                        Paruser_clave.SqlDbType = SqlDbType.VarChar;
                        Paruser_clave.Size = 250;
                        Paruser_clave.Value = Users.Usuario_clave;
                        command.Parameters.Add(Paruser_clave);

                        SqlParameter Paruser_email = new SqlParameter();
                        Paruser_email.ParameterName = "@usuario_email";
                        Paruser_email.SqlDbType = SqlDbType.VarChar;
                        Paruser_email.Size = 100;
                        Paruser_email.Value = Users.Usuario_email;
                        command.Parameters.Add(Paruser_email);
                        //Ejecutamos el comando
                        rpta = command.ExecuteNonQuery() == 1 ? "OK" : "NO SE ACTUALIZO EL REGISTRO";
                    }
                    catch (Exception ex)
                    {
                        rpta = ex.Message;
                    }
                    finally { if (connection.State == ConnectionState.Open) connection.Close(); }
                }
            }
            return rpta;
        }
        //Metodo Eliminar
        public string Eliminar(Dusers Users)
        {
            string rpta = string.Empty;
            using (SqlConnection connection = GetConnection())
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand())
                {
                    try
                    {
                        command.Connection = connection;
                        command.CommandText = "speliminar_users";
                        command.CommandType = CommandType.StoredProcedure;

                        SqlParameter Paruser_id = new SqlParameter();
                        Paruser_id.ParameterName = "@usuario_id";
                        Paruser_id.SqlDbType = SqlDbType.Int;
                        Paruser_id.Value = Users.Usuario_id;
                        command.Parameters.Add(Paruser_id);
                        //Ejecutamos el comando
                        rpta = command.ExecuteNonQuery() == 1 ? "OK" : "NO SE ELIMINO EL REGISTRO";
                    }
                    catch (Exception ex)
                    {
                        rpta = ex.Message;
                    }
                    finally { if (connection.State == ConnectionState.Open) connection.Close(); }
                }
            }
            return rpta;
        }
        //Metodo Mostrar
        public DataTable Mostrar()
        {
            DataTable DtResultado = new DataTable("users");
            using (SqlConnection connection = GetConnection())
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand())
                {
                    try
                    {
                        command.Connection = connection;
                        command.CommandText = "spmostrar_users";
                        command.CommandType = CommandType.StoredProcedure;

                        SqlDataAdapter sqlDat = new SqlDataAdapter(command);
                        sqlDat.Fill(DtResultado);
                    }
                    catch (Exception)
                    {
                        DtResultado = null;
                    }
                }
            }
            return DtResultado;
        }
        //Metodo BuscarNombre de la categoria
        public DataTable BuscarNombre(Dusers Users)
        {
            DataTable DtResultado = new DataTable("users");
            using (SqlConnection connection = GetConnection())
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand())
                {
                    try
                    {
                        command.Connection = connection;
                        command.CommandText = "spbuscar_user_name";
                        command.CommandType = CommandType.StoredProcedure;

                        SqlParameter ParTextoBuscar = new SqlParameter();
                        ParTextoBuscar.ParameterName = "@textbuscar";
                        ParTextoBuscar.SqlDbType = SqlDbType.VarChar;
                        ParTextoBuscar.Size = 50;
                        ParTextoBuscar.Value = Users.Textobuscar;
                        command.Parameters.Add(ParTextoBuscar);

                        SqlDataAdapter sqlDat = new SqlDataAdapter(command);
                        sqlDat.Fill(DtResultado);
                    }
                    catch (Exception)
                    {
                        DtResultado = null;
                    }
                }
            }
            return DtResultado;
        }
        //Metodo Login
        public DataTable Login(Dusers Users)
        {
            DataTable DtResultado = new DataTable("users");
            using (SqlConnection connection = GetConnection())
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand())
                {
                    try
                    {
                        command.Connection = connection;
                        command.CommandText = "splogin";
                        command.CommandType = CommandType.StoredProcedure;

                        SqlParameter Paruser_name = new SqlParameter();
                        Paruser_name.ParameterName = "@usuario";
                        Paruser_name.SqlDbType = SqlDbType.VarChar;
                        Paruser_name.Size = 50;
                        Paruser_name.Value = Users.Usuario_name;
                        command.Parameters.Add(Paruser_name);

                        SqlParameter Paruser_clave = new SqlParameter();
                        Paruser_clave.ParameterName = "@clave";
                        Paruser_clave.SqlDbType = SqlDbType.VarChar;
                        Paruser_clave.Size = 250;
                        Paruser_clave.Value = Users.Usuario_clave;
                        command.Parameters.Add(Paruser_clave);

                        SqlDataAdapter sqlDat = new SqlDataAdapter(command);
                        sqlDat.Fill(DtResultado);
                    }
                    catch (Exception)
                    {
                        DtResultado = null;
                    }
                }
            }
            return DtResultado;
        }

        //Metodo Login con información de rol
        public DataTable LoginWithRole(Dusers Users)
        {
            DataTable DtResultado = new DataTable("users");
            using (SqlConnection connection = GetConnection())
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand())
                {
                    try
                    {
                        command.Connection = connection;
                        command.CommandText = "sp_login_with_role";
                        command.CommandType = CommandType.StoredProcedure;

                        SqlParameter Paruser_name = new SqlParameter();
                        Paruser_name.ParameterName = "@usuario";
                        Paruser_name.SqlDbType = SqlDbType.VarChar;
                        Paruser_name.Size = 50;
                        Paruser_name.Value = Users.Usuario_name;
                        command.Parameters.Add(Paruser_name);

                        SqlParameter Paruser_clave = new SqlParameter();
                        Paruser_clave.ParameterName = "@clave";
                        Paruser_clave.SqlDbType = SqlDbType.VarChar;
                        Paruser_clave.Size = 250;
                        Paruser_clave.Value = Users.Usuario_clave;
                        command.Parameters.Add(Paruser_clave);

                        SqlDataAdapter sqlDat = new SqlDataAdapter(command);
                        sqlDat.Fill(DtResultado);
                    }
                    catch (Exception)
                    {
                        DtResultado = null;
                    }
                }
            }
            return DtResultado;
        }

        //Metodo para verificar permisos
        public bool CheckUserPermission(int usuarioId, string permissionName)
        {
            using (SqlConnection connection = GetConnection())
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand())
                {
                    try
                    {
                        command.Connection = connection;
                        command.CommandText = "sp_check_user_permission";
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@usuario_id", usuarioId);
                        command.Parameters.AddWithValue("@permission_name", permissionName);

                        var result = command.ExecuteScalar();
                        return Convert.ToBoolean(result);
                    }
                    catch (Exception)
                    {
                        return false;
                    }
                }
            }
        }

        //Metodo para obtener permisos del usuario
        public DataTable GetUserPermissions(int usuarioId)
        {
            DataTable DtResultado = new DataTable("permissions");
            using (SqlConnection connection = GetConnection())
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand())
                {
                    try
                    {
                        command.Connection = connection;
                        command.CommandText = "sp_get_user_permissions";
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@usuario_id", usuarioId);

                        SqlDataAdapter sqlDat = new SqlDataAdapter(command);
                        sqlDat.Fill(DtResultado);
                    }
                    catch (Exception)
                    {
                        DtResultado = null;
                    }
                }
            }
            return DtResultado;
        }

        //Metodo para obtener usuarios con roles
        public DataTable MostrarConRoles()
        {
            DataTable DtResultado = new DataTable("users_roles");
            using (SqlConnection connection = GetConnection())
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand())
                {
                    try
                    {
                        command.Connection = connection;
                        command.CommandText = "SELECT * FROM vw_users_roles ORDER BY usuario_name";
                        command.CommandType = CommandType.Text;

                        SqlDataAdapter sqlDat = new SqlDataAdapter(command);
                        sqlDat.Fill(DtResultado);
                    }
                    catch (Exception)
                    {
                        DtResultado = null;
                    }
                }
            }
            return DtResultado;
        }

        //Metodo para obtener todos los roles
        public DataTable GetRoles()
        {
            DataTable DtResultado = new DataTable("roles");
            using (SqlConnection connection = GetConnection())
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand())
                {
                    try
                    {
                        command.Connection = connection;
                        command.CommandText = "SELECT role_id, role_name, role_description FROM roles ORDER BY role_name";
                        command.CommandType = CommandType.Text;

                        SqlDataAdapter sqlDat = new SqlDataAdapter(command);
                        sqlDat.Fill(DtResultado);
                    }
                    catch (Exception)
                    {
                        DtResultado = null;
                    }
                }
            }
            return DtResultado;
        }
    }
}
