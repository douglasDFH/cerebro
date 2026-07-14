using System;
using System.Data;
using System.Data.SqlClient;

namespace CapaDatos
{
    public class Dstaffs : DbConnection
    {
        public Dstaffs() : base()
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
                        cmd.CommandText = "SELECT staff_id, first_name, last_name, email, phone, active, store_id, manager_id FROM staffs ORDER BY staff_id";
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
                        if (campo == "staff_id")
                        {
                            query = "SELECT staff_id, first_name, last_name, email, phone, active, store_id, manager_id FROM staffs WHERE staff_id = @valor ORDER BY staff_id";
                        }
                        else
                        {
                            query = $"SELECT staff_id, first_name, last_name, email, phone, active, store_id, manager_id FROM staffs WHERE {campo} LIKE @valor ORDER BY staff_id";
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
        public string Insertar(string firstName, string lastName, string email, string phone, bool active, int storeId, int? managerId)
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
                        cmd.CommandText = @"INSERT INTO staffs (first_name, last_name, email, phone, active, store_id, manager_id) 
                                          VALUES (@firstName, @lastName, @email, @phone, @active, @storeId, @managerId)";
                        cmd.CommandType = CommandType.Text;

                        cmd.Parameters.AddWithValue("@firstName", firstName);
                        cmd.Parameters.AddWithValue("@lastName", lastName);
                        cmd.Parameters.AddWithValue("@email", email);
                        cmd.Parameters.AddWithValue("@phone", phone ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@active", active ? 1 : 0);
                        cmd.Parameters.AddWithValue("@storeId", storeId);
                        cmd.Parameters.AddWithValue("@managerId", (object)managerId ?? DBNull.Value);

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
        public string Editar(int staffId, string firstName, string lastName, string email, string phone, bool active, int storeId, int? managerId)
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
                        cmd.CommandText = @"UPDATE staffs SET 
                                          first_name = @firstName, 
                                          last_name = @lastName, 
                                          email = @email, 
                                          phone = @phone, 
                                          active = @active, 
                                          store_id = @storeId, 
                                          manager_id = @managerId 
                                          WHERE staff_id = @staffId";
                        cmd.CommandType = CommandType.Text;

                        cmd.Parameters.AddWithValue("@staffId", staffId);
                        cmd.Parameters.AddWithValue("@firstName", firstName);
                        cmd.Parameters.AddWithValue("@lastName", lastName);
                        cmd.Parameters.AddWithValue("@email", email);
                        cmd.Parameters.AddWithValue("@phone", phone ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@active", active ? 1 : 0);
                        cmd.Parameters.AddWithValue("@storeId", storeId);
                        cmd.Parameters.AddWithValue("@managerId", (object)managerId ?? DBNull.Value);

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
        public string Eliminar(int staffId)
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
                        cmd.CommandText = "DELETE FROM staffs WHERE staff_id = @staffId";
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@staffId", staffId);

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
        public bool Existe(int staffId)
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
                        cmd.CommandText = "SELECT COUNT(*) FROM staffs WHERE staff_id = @staffId";
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@staffId", staffId);

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

        // Verificar si un email ya existe (excluyendo un staff_id específico)
        public bool EmailExiste(string email, int excludeStaffId = 0)
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
                        if (excludeStaffId > 0)
                        {
                            cmd.CommandText = "SELECT COUNT(*) FROM staffs WHERE email = @email AND staff_id <> @staffId";
                            cmd.Parameters.AddWithValue("@staffId", excludeStaffId);
                        }
                        else
                        {
                            cmd.CommandText = "SELECT COUNT(*) FROM staffs WHERE email = @email";
                        }

                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@email", email);

                        int count = Convert.ToInt32(cmd.ExecuteScalar());
                        existe = count > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al verificar email: " + ex.Message);
            }

            return existe;
        }

        // Obtener lista de tiendas disponibles
        public DataTable ObtenerTiendas()
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
                        cmd.CommandText = "SELECT store_id, store_name FROM stores ORDER BY store_name";
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
                throw new Exception("Error al obtener tiendas: " + ex.Message);
            }

            return dt;
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
                                            COUNT(*) as Total,
                                            SUM(CASE WHEN active = 1 THEN 1 ELSE 0 END) as Activos,
                                            SUM(CASE WHEN active = 0 THEN 1 ELSE 0 END) as Inactivos
                                            FROM staffs";
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
    }
}