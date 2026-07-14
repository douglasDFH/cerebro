using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace CapaDatos
{
    public class Dproducts : DbConnection
    {
        //campos y propiedades
        private int _product_id;
        private string _product_name;
        private int _model_year;
        private decimal _price;
        private byte[] _imagen;
        private int _category_id;
        private string _textobuscar;
        //Refactorizar las variables privadas para generar las propiedades
        public int Product_id { get => _product_id; set => _product_id = value; }
        public string Product_name { get => _product_name; set => _product_name = value; }
        public int Model_year { get => _model_year; set => _model_year = value; }
        public decimal Price { get => _price; set => _price = value; }
        public byte[] Imagen { get => _imagen; set => _imagen = value; }
        public int Category_id { get => _category_id; set => _category_id = value; }
        public string TextoBuscar { get => _textobuscar; set => _textobuscar = value; }
        //Constructores
        public Dproducts()
        { }
        public Dproducts(int product_id, string product_name, int model_year,
            decimal price, byte[] imagen, int category_id, string textoBuscar)
        {
            Product_id = product_id;
            Product_name = product_name;
            Model_year = model_year;
            Price = price;
            Imagen = imagen;
            Category_id = category_id;
            TextoBuscar = textoBuscar;
        }
        //Metodo Insertar
        public string Insertar(Dproducts Products)
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
                        command.CommandText = "spinsertar_products";
                        command.CommandType = CommandType.StoredProcedure;

                        SqlParameter Parproduct_id = new SqlParameter();
                        Parproduct_id.ParameterName = "@product_id";
                        Parproduct_id.SqlDbType = SqlDbType.Int;
                        Parproduct_id.Direction = ParameterDirection.Output;
                        command.Parameters.Add(Parproduct_id);

                        SqlParameter Parproduct_name = new SqlParameter();
                        Parproduct_name.ParameterName = "@product_name";
                        Parproduct_name.SqlDbType = SqlDbType.VarChar;
                        Parproduct_name.Size = 200;
                        Parproduct_name.Value = Products.Product_name;
                        command.Parameters.Add(Parproduct_name);

                        SqlParameter Parmodel_year = new SqlParameter();
                        Parmodel_year.ParameterName = "@model_year";
                        Parmodel_year.SqlDbType = SqlDbType.SmallInt;
                        Parmodel_year.Value = Products.Model_year;
                        command.Parameters.Add(Parmodel_year);

                        SqlParameter Parprice = new SqlParameter();
                        Parprice.ParameterName = "@price";
                        Parprice.SqlDbType = SqlDbType.Money;
                        Parprice.Value = Products.Price;
                        command.Parameters.Add(Parprice);

                        SqlParameter Parimagen = new SqlParameter();
                        Parimagen.ParameterName = "@imagen";
                        Parimagen.SqlDbType = SqlDbType.Image;
                        Parimagen.Value = Products.Imagen;
                        command.Parameters.Add(Parimagen);

                        SqlParameter Parcategory_id = new SqlParameter();
                        Parcategory_id.ParameterName = "@category_id";
                        Parcategory_id.SqlDbType = SqlDbType.Int;
                        Parcategory_id.Value = Products.Category_id;
                        command.Parameters.Add(Parcategory_id);
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
        public string Editar(Dproducts Products)
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
                        command.CommandText = "speditar_products";
                        command.CommandType = CommandType.StoredProcedure;

                        SqlParameter Parproduct_id = new SqlParameter();
                        Parproduct_id.ParameterName = "@product_id";
                        Parproduct_id.SqlDbType = SqlDbType.Int;
                        Parproduct_id.Value = Products.Product_id;
                        command.Parameters.Add(Parproduct_id);

                        SqlParameter Parproduct_name = new SqlParameter();
                        Parproduct_name.ParameterName = "@product_name";
                        Parproduct_name.SqlDbType = SqlDbType.VarChar;
                        Parproduct_name.Size = 200;
                        Parproduct_name.Value = Products.Product_name;
                        command.Parameters.Add(Parproduct_name);

                        SqlParameter Parmodel_year = new SqlParameter();
                        Parmodel_year.ParameterName = "@model_year";
                        Parmodel_year.SqlDbType = SqlDbType.SmallInt;
                        Parmodel_year.Value = Products.Model_year;
                        command.Parameters.Add(Parmodel_year);

                        SqlParameter Parprice = new SqlParameter();
                        Parprice.ParameterName = "@price";
                        Parprice.SqlDbType = SqlDbType.Money;
                        Parprice.Value = Products.Price;
                        command.Parameters.Add(Parprice);

                        SqlParameter Parimagen = new SqlParameter();
                        Parimagen.ParameterName = "@imagen";
                        Parimagen.SqlDbType = SqlDbType.Image;
                        Parimagen.Value = Products.Imagen;
                        command.Parameters.Add(Parimagen);

                        SqlParameter Parcategory_id = new SqlParameter();
                        Parcategory_id.ParameterName = "@category_id";
                        Parcategory_id.SqlDbType = SqlDbType.Int;
                        Parcategory_id.Value = Products.Category_id;
                        command.Parameters.Add(Parcategory_id);
                        //Ejecutamos el comando
                        try
                        {
                            command.Connection = connection;
                            command.CommandText = "speditar_products";
                            command.CommandType = CommandType.StoredProcedure;

                            // Agregar parámetros aquí...

                            command.ExecuteNonQuery();  // Ejecuta el SP, sin importar filas afectadas
                            rpta = "OK"; // Siempre OK si no hubo excepción
                        }
                        catch (Exception ex)
                        {
                            rpta = ex.Message; // Si hubo error, devuelve mensaje de error real
                        }

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
        public string Eliminar(Dproducts Products)
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
                        command.CommandText = "speliminar_products";
                        command.CommandType = CommandType.StoredProcedure;

                        SqlParameter Parproduct_id = new SqlParameter();
                        Parproduct_id.ParameterName = "@product_id";
                        Parproduct_id.SqlDbType = SqlDbType.Int;
                        Parproduct_id.Value = Products.Product_id;
                        command.Parameters.Add(Parproduct_id);
                        //Ejecutamos el comando
                        int filasAfectadas = command.ExecuteNonQuery();
                        rpta = filasAfectadas >= 0 ? "OK" : "NO SE ELIMINO EL REGISTRO";

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
            DataTable DtResultado = new DataTable("products");
            using (SqlConnection connection = GetConnection())
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand())
                {
                    try
                    {
                        command.Connection = connection;
                        command.CommandText = @"SELECT p.product_id, p.product_name, p.model_year, p.price, p.imagen,
                                                p.category_id, c.category_name AS category
                                        FROM products p
                                        INNER JOIN categories c ON p.category_id = c.category_id";
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

        //Metodo BuscarNombre del producto
        public DataTable BuscarNombre(Dproducts Products)
        {
            DataTable DtResultado = new DataTable("products");
            using (SqlConnection connection = GetConnection())
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand())
                {
                    try
                    {
                        command.Connection = connection;
                        command.CommandText = "spbuscar_product_name";
                        command.CommandType = CommandType.StoredProcedure;

                        SqlParameter ParTextoBuscar = new SqlParameter();
                        ParTextoBuscar.ParameterName = "@textbuscar";
                        ParTextoBuscar.SqlDbType = SqlDbType.VarChar;
                        ParTextoBuscar.Size = 200;
                        ParTextoBuscar.Value = Products.TextoBuscar;
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
