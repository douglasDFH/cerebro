using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace CapaDatos
{
    public class Dcustomer : DbConnection
    {
        //campos y propiedades
        private int _customer_id;
        private string _first_name;
        private string _last_name;
        private string _phone;
        private string _email;
        private string _street;
        private string _city;
        private string _state;
        private string _textobuscar;
        public int Customer_id { get => _customer_id; set => _customer_id = value; }
        public string First_name { get => _first_name; set => _first_name = value; }
        public string Last_name { get => _last_name; set => _last_name = value; }
        public string Phone { get => _phone; set => _phone = value; }
        public string Email { get => _email; set => _email = value; }
        public string Street { get => _street; set => _street = value; }
        public string City { get => _city; set => _city = value; }
        public string State { get => _state; set => _state = value; }
        public string Textobuscar { get => _textobuscar; set => _textobuscar = value; }
        //Constructores
        public Dcustomer() { }
        public Dcustomer(int customer_id, string first_name, string last_name, 
            string phone, string email, string street, string city, 
            string state, string textobuscar)
        {
            Customer_id = customer_id;
            First_name = first_name;
            Last_name = last_name;
            Phone = phone;
            Email = email;
            Street = street;
            City = city;
            State = state;
            Textobuscar = textobuscar;
        }
        //Metodo Insertar
        public string Insertar(Dcustomer Customer)
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
                        command.CommandText = "spinsertar_customers";
                        command.CommandType = CommandType.StoredProcedure;

                        SqlParameter Parcustomer_id = new SqlParameter();
                        Parcustomer_id.ParameterName = "@customer_id";
                        Parcustomer_id.SqlDbType = SqlDbType.Int;
                        Parcustomer_id.Direction = ParameterDirection.Output;
                        command.Parameters.Add(Parcustomer_id);

                        SqlParameter Parfirst_name = new SqlParameter();
                        Parfirst_name.ParameterName = "@first_name";
                        Parfirst_name.SqlDbType = SqlDbType.VarChar;
                        Parfirst_name.Size = 255;
                        Parfirst_name.Value = Customer.First_name;
                        command.Parameters.Add(Parfirst_name);

                        SqlParameter Parlast_name = new SqlParameter();
                        Parlast_name.ParameterName = "@last_name";
                        Parlast_name.SqlDbType = SqlDbType.VarChar;
                        Parlast_name.Size = 255;
                        Parlast_name.Value = Customer.Last_name;
                        command.Parameters.Add(Parlast_name);

                        SqlParameter Parphone = new SqlParameter();
                        Parphone.ParameterName = "@phone";
                        Parphone.SqlDbType = SqlDbType.VarChar;
                        Parphone.Size = 25;
                        Parphone.Value = Customer.Phone;
                        command.Parameters.Add(Parphone);

                        SqlParameter Paremail = new SqlParameter();
                        Paremail.ParameterName = "@email";
                        Paremail.SqlDbType = SqlDbType.VarChar;
                        Paremail.Size = 255;
                        Paremail.Value = Customer.Email;
                        command.Parameters.Add(Paremail);

                        SqlParameter Parstreet = new SqlParameter();
                        Parstreet.ParameterName = "@street";
                        Parstreet.SqlDbType = SqlDbType.VarChar;
                        Parstreet.Size = 255;
                        Parstreet.Value = Customer.Street;
                        command.Parameters.Add(Parstreet);

                        SqlParameter Parcity = new SqlParameter();
                        Parcity.ParameterName = "@city";
                        Parcity.SqlDbType = SqlDbType.VarChar;
                        Parcity.Size = 50;
                        Parcity.Value = Customer.City;
                        command.Parameters.Add(Parcity);

                        SqlParameter Parstate = new SqlParameter();
                        Parstate.ParameterName = "@state";
                        Parstate.SqlDbType = SqlDbType.VarChar;
                        Parstate.Size = 25;
                        Parstate.Value = Customer.State;
                        command.Parameters.Add(Parstate);
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
        public string Editar(Dcustomer Customer)
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
                        command.CommandText = "speditar_customers";
                        command.CommandType = CommandType.StoredProcedure;

                        SqlParameter Parcustomer_id = new SqlParameter();
                        Parcustomer_id.ParameterName = "@customer_id";
                        Parcustomer_id.SqlDbType = SqlDbType.Int;
                        Parcustomer_id.Value = Customer.Customer_id;
                        command.Parameters.Add(Parcustomer_id);

                        SqlParameter Parfirst_name = new SqlParameter();
                        Parfirst_name.ParameterName = "@first_name";
                        Parfirst_name.SqlDbType = SqlDbType.VarChar;
                        Parfirst_name.Size = 255;
                        Parfirst_name.Value = Customer.First_name;
                        command.Parameters.Add(Parfirst_name);

                        SqlParameter Parlast_name = new SqlParameter();
                        Parlast_name.ParameterName = "@last_name";
                        Parlast_name.SqlDbType = SqlDbType.VarChar;
                        Parlast_name.Size = 255;
                        Parlast_name.Value = Customer.Last_name;
                        command.Parameters.Add(Parlast_name);

                        SqlParameter Parphone = new SqlParameter();
                        Parphone.ParameterName = "@phone";
                        Parphone.SqlDbType = SqlDbType.VarChar;
                        Parphone.Size = 25;
                        Parphone.Value = Customer.Phone;
                        command.Parameters.Add(Parphone);

                        SqlParameter Paremail = new SqlParameter();
                        Paremail.ParameterName = "@email";
                        Paremail.SqlDbType = SqlDbType.VarChar;
                        Paremail.Size = 255;
                        Paremail.Value = Customer.Email;
                        command.Parameters.Add(Paremail);

                        SqlParameter Parstreet = new SqlParameter();
                        Parstreet.ParameterName = "@street";
                        Parstreet.SqlDbType = SqlDbType.VarChar;
                        Parstreet.Size = 255;
                        Parstreet.Value = Customer.Street;
                        command.Parameters.Add(Parstreet);

                        SqlParameter Parcity = new SqlParameter();
                        Parcity.ParameterName = "@city";
                        Parcity.SqlDbType = SqlDbType.VarChar;
                        Parcity.Size = 50;
                        Parcity.Value = Customer.City;
                        command.Parameters.Add(Parcity);

                        SqlParameter Parstate = new SqlParameter();
                        Parstate.ParameterName = "@state";
                        Parstate.SqlDbType = SqlDbType.VarChar;
                        Parstate.Size = 25;
                        Parstate.Value = Customer.State;
                        command.Parameters.Add(Parstate);
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
        public string Eliminar(Dcustomer Customer)
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
                        command.CommandText = "speliminar_customers";
                        command.CommandType = CommandType.StoredProcedure;

                        SqlParameter Parcustomer_id = new SqlParameter();
                        Parcustomer_id.ParameterName = "@customer_id";
                        Parcustomer_id.SqlDbType = SqlDbType.Int;
                        Parcustomer_id.Value = Customer.Customer_id;
                        command.Parameters.Add(Parcustomer_id);
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
            DataTable DtResultado = new DataTable("customers");
            using (SqlConnection connection = GetConnection())
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand())
                {
                    try
                    {
                        command.Connection = connection;
                        command.CommandText = "spmostrar_customers";
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
        //Metodo BuscarNombre del cliente
        public DataTable BuscarNombre(Dcustomer Customer)
        {
            DataTable DtResultado = new DataTable("customers");
            using (SqlConnection connection = GetConnection())
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand())
                {
                    try
                    {
                        command.Connection = connection;
                        command.CommandText = "spbuscar_customer_name";
                        command.CommandType = CommandType.StoredProcedure;

                        SqlParameter ParTextoBuscar = new SqlParameter();
                        ParTextoBuscar.ParameterName = "@textobuscar";
                        ParTextoBuscar.SqlDbType = SqlDbType.VarChar;
                        ParTextoBuscar.Size = 50;
                        ParTextoBuscar.Value = Customer.Textobuscar;
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
