using System;
using System.Data;
using System.Data.SqlClient;

namespace CapaDatos
{
    public class Dstores : DbConnection
    {
        public Dstores() : base()
        {
        }

        // Mostrar todos los registros
        public DataTable Mostrar()
        {
            DataTable dt = new DataTable();

            try
            {
                using (SqlConnection cn = GetConnection())
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = cn;
                        cmd.CommandText = "SELECT store_id, store_name, phone, email, street, city, state, zip_code FROM stores ORDER BY store_name";
                        cmd.CommandType = CommandType.Text;

                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            adapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al mostrar datos: " + ex.Message);
            }

            return dt;
        }

        // Buscar registros
        public DataTable Buscar(string campo, string valor)
        {
            DataTable dt = new DataTable();

            try
            {
                using (SqlConnection cn = GetConnection())
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = cn;

                        string query = "";
                        if (campo == "store_id")
                        {
                            query = "SELECT store_id, store_name, phone, email, street, city, state, zip_code FROM stores WHERE store_id = @valor ORDER BY store_name";
                        }
                        else
                        {
                            query = $"SELECT store_id, store_name, phone, email, street, city, state, zip_code FROM stores WHERE {campo} LIKE @valor ORDER BY store_name";
                            valor = "%" + valor + "%";
                        }

                        cmd.CommandText = query;
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@valor", valor);

                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            adapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error en la búsqueda: " + ex.Message);
            }

            return dt;
        }

        // Insertar nuevo registro
        public string Insertar(string storeName, string phone, string email, string street, string city, string state, string zipCode)
        {
            string result = "";

            try
            {
                using (SqlConnection cn = GetConnection())
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = cn;
                        cmd.CommandText = @"INSERT INTO stores (store_name, phone, email, street, city, state, zip_code) 
                                          VALUES (@storeName, @phone, @email, @street, @city, @state, @zipCode)";
                        cmd.CommandType = CommandType.Text;

                        cmd.Parameters.AddWithValue("@storeName", storeName);
                        cmd.Parameters.AddWithValue("@phone", phone ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@email", email ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@street", street ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@city", city ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@state", state ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@zipCode", zipCode ?? (object)DBNull.Value);

                        int rowsAffected = cmd.ExecuteNonQuery();
                        result = rowsAffected > 0 ? "OK" : "No se pudo insertar el registro";
                    }
                }
            }
            catch (Exception ex)
            {
                result = "Error al insertar: " + ex.Message;
            }

            return result;
        }

        // Editar registro existente
        public string Editar(int storeId, string storeName, string phone, string email, string street, string city, string state, string zipCode)
        {
            string result = "";

            try
            {
                using (SqlConnection cn = GetConnection())
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = cn;
                        cmd.CommandText = @"UPDATE stores SET 
                                          store_name = @storeName, 
                                          phone = @phone, 
                                          email = @email, 
                                          street = @street, 
                                          city = @city, 
                                          state = @state, 
                                          zip_code = @zipCode 
                                          WHERE store_id = @storeId";
                        cmd.CommandType = CommandType.Text;

                        cmd.Parameters.AddWithValue("@storeId", storeId);
                        cmd.Parameters.AddWithValue("@storeName", storeName);
                        cmd.Parameters.AddWithValue("@phone", phone ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@email", email ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@street", street ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@city", city ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@state", state ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@zipCode", zipCode ?? (object)DBNull.Value);

                        int rowsAffected = cmd.ExecuteNonQuery();
                        result = rowsAffected > 0 ? "OK" : "No se pudo actualizar el registro";
                    }
                }
            }
            catch (Exception ex)
            {
                result = "Error al actualizar: " + ex.Message;
            }

            return result;
        }

        // Eliminar registro
        public string Eliminar(int storeId)
        {
            string result = "";

            try
            {
                using (SqlConnection cn = GetConnection())
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = cn;
                        cmd.CommandText = "DELETE FROM stores WHERE store_id = @storeId";
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@storeId", storeId);

                        int rowsAffected = cmd.ExecuteNonQuery();
                        result = rowsAffected > 0 ? "OK" : "No se pudo eliminar el registro";
                    }
                }
            }
            catch (Exception ex)
            {
                result = "Error al eliminar: " + ex.Message;
            }

            return result;
        }

        // Verificar si existe un registro
        public bool Existe(int storeId)
        {
            bool existe = false;

            try
            {
                using (SqlConnection cn = GetConnection())
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = cn;
                        cmd.CommandText = "SELECT COUNT(*) FROM stores WHERE store_id = @storeId";
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@storeId", storeId);

                        int count = Convert.ToInt32(cmd.ExecuteScalar());
                        existe = count > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al verificar existencia: " + ex.Message);
            }

            return existe;
        }

        // Obtener estadísticas básicas
        public DataTable ObtenerEstadisticas()
        {
            DataTable dt = new DataTable();

            try
            {
                using (SqlConnection cn = GetConnection())
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = cn;
                        cmd.CommandText = @"SELECT 
                                            COUNT(*) as TotalTiendas,
                                            COUNT(CASE WHEN phone IS NOT NULL THEN 1 END) as ConTelefono,
                                            COUNT(CASE WHEN email IS NOT NULL THEN 1 END) as ConEmail
                                            FROM stores";
                        cmd.CommandType = CommandType.Text;

                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            adapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener estadísticas: " + ex.Message);
            }

            return dt;
        }

        // Obtener tiendas con número de staff asignado
        public DataTable ObtenerTiendasConStaff()
        {
            DataTable dt = new DataTable();

            try
            {
                using (SqlConnection cn = GetConnection())
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = cn;
                        cmd.CommandText = @"SELECT s.store_id, s.store_name, s.city, s.state, 
                                           COUNT(st.staff_id) as TotalStaff,
                                           COUNT(CASE WHEN st.active = 1 THEN 1 END) as StaffActivo
                                           FROM stores s
                                           LEFT JOIN staffs st ON s.store_id = st.store_id
                                           GROUP BY s.store_id, s.store_name, s.city, s.state
                                           ORDER BY s.store_name";
                        cmd.CommandType = CommandType.Text;

                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            adapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener tiendas con staff: " + ex.Message);
            }

            return dt;
        }
    }
}