using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Text;

namespace CapaDatos
{
    public class DDiagnostico
    {
        #region Métodos CRUD

        /// <summary>
        /// Obtiene todos los diagnósticos con filtros opcionales
        /// </summary>
        public DataTable ListarDiagnosticos(bool? activo = null, string categoria = null, string buscar = null)
        {
            DataTable dataTable = new DataTable();
            SqlConnection connection = DbConnection.Instance.GetConnection();

            try
            {
                StringBuilder query = new StringBuilder(@"
                    SELECT 
                        id,
                        codigo,
                        nombre,
                        categoria_diagnostico as categoria,
                        descripcion,
                        precio_base,
                        requiere_equipamiento,
                        activo,
                        created_at as fecha_creacion,
                        updated_at as fecha_actualizacion
                    FROM diagnostico 
                    WHERE 1=1");

                var parameters = new List<SqlParameter>();

                if (activo.HasValue)
                {
                    query.Append(" AND activo = @activo");
                    parameters.Add(new SqlParameter("@activo", activo.Value));
                }

                if (!string.IsNullOrEmpty(categoria))
                {
                    query.Append(" AND categoria_diagnostico = @categoria");
                    parameters.Add(new SqlParameter("@categoria", categoria));
                }

                if (!string.IsNullOrEmpty(buscar))
                {
                    query.Append(" AND (codigo LIKE @buscar OR nombre LIKE @buscar OR descripcion LIKE @buscar)");
                    parameters.Add(new SqlParameter("@buscar", $"%{buscar}%"));
                }

                query.Append(" ORDER BY nombre ASC");

                using (SqlCommand command = new SqlCommand(query.ToString(), connection))
                {
                    command.Parameters.AddRange(parameters.ToArray());
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(dataTable);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al listar diagnósticos: {ex.Message}");
            }

            return dataTable;
        }

        /// <summary>
        /// Obtiene diagnósticos filtrados por categoría
        /// </summary>
        public DataTable ListarDiagnosticosPorCategoria(string categoria)
        {
            return ListarDiagnosticos(null, categoria, null);
        }

        /// <summary>
        /// Busca diagnósticos por texto en nombre, código o descripción
        /// </summary>
        public DataTable BuscarDiagnosticos(string textoBusqueda)
        {
            return ListarDiagnosticos(null, null, textoBusqueda);
        }

        /// <summary>
        /// Inserta un nuevo diagnóstico
        /// </summary>
        public bool InsertarDiagnostico(string codigo, string nombre, string categoria, string descripcion, 
                                       decimal precioBase, bool requiereEquipamiento, bool activo)
        {
            SqlConnection connection = DbConnection.Instance.GetConnection();

            try
            {
                string query = @"
                    INSERT INTO diagnostico 
                    (codigo, nombre, categoria_diagnostico, descripcion, precio_base, requiere_equipamiento, activo)
                    VALUES 
                    (@codigo, @nombre, @categoria, @descripcion, @precio_base, @requiere_equipamiento, @activo)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@codigo", codigo ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@nombre", nombre);
                    command.Parameters.AddWithValue("@categoria", categoria ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@descripcion", descripcion ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@precio_base", precioBase);
                    command.Parameters.AddWithValue("@requiere_equipamiento", requiereEquipamiento);
                    command.Parameters.AddWithValue("@activo", activo);

                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al insertar diagnóstico: {ex.Message}");
            }
        }

        /// <summary>
        /// Actualiza un diagnóstico existente
        /// </summary>
        public bool ActualizarDiagnostico(int id, string codigo, string nombre, string categoria, string descripcion,
                                         decimal precioBase, bool requiereEquipamiento, bool activo)
        {
            SqlConnection connection = DbConnection.Instance.GetConnection();

            try
            {
                string query = @"
                    UPDATE diagnostico 
                    SET 
                        codigo = @codigo,
                        nombre = @nombre,
                        categoria_diagnostico = @categoria,
                        descripcion = @descripcion,
                        precio_base = @precio_base,
                        requiere_equipamiento = @requiere_equipamiento,
                        activo = @activo,
                        updated_at = GETDATE()
                    WHERE id = @id";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    command.Parameters.AddWithValue("@codigo", codigo ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@nombre", nombre);
                    command.Parameters.AddWithValue("@categoria", categoria ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@descripcion", descripcion ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@precio_base", precioBase);
                    command.Parameters.AddWithValue("@requiere_equipamiento", requiereEquipamiento);
                    command.Parameters.AddWithValue("@activo", activo);

                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al actualizar diagnóstico: {ex.Message}");
            }
        }

        /// <summary>
        /// Elimina un diagnóstico
        /// </summary>
        public bool EliminarDiagnostico(int id)
        {
            SqlConnection connection = DbConnection.Instance.GetConnection();

            try
            {
                string query = "DELETE FROM diagnostico WHERE id = @id";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", id);

                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al eliminar diagnóstico: {ex.Message}");
            }
        }

        #endregion

        #region Métodos de Utilidad

        /// <summary>
        /// Verifica si existe un código de diagnóstico
        /// </summary>
        public bool ExisteCodigoDiagnostico(string codigo)
        {
            SqlConnection connection = DbConnection.Instance.GetConnection();

            try
            {
                string query = "SELECT COUNT(*) FROM diagnostico WHERE codigo = @codigo";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@codigo", codigo);
                    
                    int count = (int)command.ExecuteScalar();
                    return count > 0;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al verificar código: {ex.Message}");
            }
        }

        /// <summary>
        /// Obtiene un diagnóstico por su ID
        /// </summary>
        public DataTable ObtenerDiagnosticoPorId(int id)
        {
            DataTable dataTable = new DataTable();
            SqlConnection connection = DbConnection.Instance.GetConnection();

            try
            {
                string query = @"
                    SELECT 
                        id,
                        codigo,
                        nombre,
                        categoria_diagnostico as categoria,
                        descripcion,
                        precio_base,
                        requiere_equipamiento,
                        activo,
                        created_at as fecha_creacion,
                        updated_at as fecha_actualizacion
                    FROM diagnostico 
                    WHERE id = @id";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(dataTable);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener diagnóstico: {ex.Message}");
            }

            return dataTable;
        }

        /// <summary>
        /// Obtiene todas las categorías únicas de diagnósticos
        /// </summary>
        public List<string> ObtenerCategorias()
        {
            List<string> categorias = new List<string>();
            SqlConnection connection = DbConnection.Instance.GetConnection();

            try
            {
                string query = @"
                    SELECT DISTINCT categoria_diagnostico 
                    FROM diagnostico 
                    WHERE categoria_diagnostico IS NOT NULL AND categoria_diagnostico != '' 
                    ORDER BY categoria_diagnostico";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            categorias.Add(reader.GetString(0));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener categorías: {ex.Message}");
            }

            return categorias;
        }

        /// <summary>
        /// Genera un código automático único para diagnósticos
        /// </summary>
        public string GenerarCodigoAutomatico()
        {
            SqlConnection connection = DbConnection.Instance.GetConnection();

            try
            {
                // Obtener el siguiente número secuencial
                string query = @"
                    SELECT ISNULL(MAX(CAST(SUBSTRING(codigo, 4, LEN(codigo) - 3) AS INT)), 0) + 1 AS siguiente_numero
                    FROM diagnostico 
                    WHERE codigo LIKE 'DX%' AND ISNUMERIC(SUBSTRING(codigo, 4, LEN(codigo) - 3)) = 1";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    object result = command.ExecuteScalar();
                    int siguienteNumero = result != null ? (int)result : 1;
                    
                    string codigoGenerado;
                    bool codigoExiste = true;
                    int intentos = 0;
                    
                    // Generar código único con reintentos si es necesario
                    do
                    {
                        codigoGenerado = $"DX{siguienteNumero.ToString().PadLeft(4, '0')}";
                        codigoExiste = ExisteCodigoDiagnostico(codigoGenerado);
                        
                        if (codigoExiste)
                        {
                            siguienteNumero++;
                            intentos++;
                        }
                        
                        if (intentos > 1000) // Evitar bucle infinito
                            throw new Exception("No se pudo generar un código único después de 1000 intentos");
                            
                    } while (codigoExiste);
                    
                    return codigoGenerado;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al generar código automático: {ex.Message}");
            }
        }

        #endregion
    }
}
