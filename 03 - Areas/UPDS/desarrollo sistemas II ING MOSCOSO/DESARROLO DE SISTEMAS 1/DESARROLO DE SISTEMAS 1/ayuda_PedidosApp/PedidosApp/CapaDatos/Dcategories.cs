using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;


namespace CapaDatos
{
    public class Dcategories : DbConnection
    {
        //campos y propiedades
        private int _category_id;
        private string _category_name;
        private string _textobuscar;
        //Refactorizar las variables privadas para generar las propiedades
        public int Category_id { get => _category_id; set => _category_id = value; }
        public string Category_name { get => _category_name; set => _category_name = value; }
        public string TextoBuscar { get => _textobuscar; set => _textobuscar = value; }
        //Constructores
        public Dcategories()
        { }
        public Dcategories(int category_id, string category_name, string textoBuscar)
        {
            Category_id = category_id;
            Category_name = category_name;
            TextoBuscar = textoBuscar;
        }
        //Metodo Insertar
        public string Insertar(Dcategories Categories)
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
                        command.CommandText = "spinsertar_categories";
                        command.CommandType = CommandType.StoredProcedure;

                        SqlParameter Parcategory_id = new SqlParameter();
                        Parcategory_id.ParameterName = "@category_id";
                        Parcategory_id.SqlDbType = SqlDbType.Int;
                        Parcategory_id.Direction = ParameterDirection.Output;
                        command.Parameters.Add(Parcategory_id);

                        SqlParameter Parcategory_name = new SqlParameter();
                        Parcategory_name.ParameterName = "@category_name";
                        Parcategory_name.SqlDbType = SqlDbType.VarChar;
                        Parcategory_name.Size = 255;
                        Parcategory_name.Value = Categories.Category_name;
                        command.Parameters.Add(Parcategory_name);
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
        public string Editar(Dcategories Categories)
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
                        command.CommandText = "speditar_categories";
                        command.CommandType = CommandType.StoredProcedure;

                        SqlParameter Parcategory_id = new SqlParameter();
                        Parcategory_id.ParameterName = "@category_id";
                        Parcategory_id.SqlDbType = SqlDbType.Int;
                        Parcategory_id.Value = Categories.Category_id;
                        command.Parameters.Add(Parcategory_id);

                        SqlParameter Parcategory_name = new SqlParameter();
                        Parcategory_name.ParameterName = "@category_name";
                        Parcategory_name.SqlDbType = SqlDbType.VarChar;
                        Parcategory_name.Size = 255;
                        Parcategory_name.Value = Categories.Category_name;
                        command.Parameters.Add(Parcategory_name);
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
        public string Eliminar(Dcategories Categories)
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
                        command.CommandText = "speliminar_categories";
                        command.CommandType = CommandType.StoredProcedure;

                        SqlParameter Parcategory_id = new SqlParameter();
                        Parcategory_id.ParameterName = "@category_id";
                        Parcategory_id.SqlDbType = SqlDbType.Int;
                        Parcategory_id.Value = Categories.Category_id;
                        command.Parameters.Add(Parcategory_id);
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
            DataTable DtResultado = new DataTable("categories");
            using (SqlConnection connection = GetConnection())
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand())
                {
                    try
                    {
                        command.Connection = connection;
                        command.CommandText = "spmostrar_categories";
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
        public DataTable BuscarNombre(Dcategories Categories)
        {
            DataTable DtResultado = new DataTable("categories");
            using (SqlConnection connection = GetConnection())
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand())
                {
                    try
                    {
                        command.Connection = connection;
                        command.CommandText = "spbuscar_category_name";
                        command.CommandType = CommandType.StoredProcedure;

                        SqlParameter ParTextoBuscar = new SqlParameter();
                        ParTextoBuscar.ParameterName = "@textbuscar";
                        ParTextoBuscar.SqlDbType = SqlDbType.VarChar;
                        ParTextoBuscar.Size = 200;
                        ParTextoBuscar.Value = Categories.TextoBuscar;
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
    }
}
