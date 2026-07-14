using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Text;

namespace CapaDatos
{
    public class DHistorial
    {
        #region Métodos CRUD

        /// <summary>
        /// Obtiene todos los historiales con filtros opcionales
        /// </summary>
        public DataTable ListarHistoriales(int? animalId = null, string buscar = null)
        {
            DataTable dataTable = new DataTable();
            SqlConnection connection = DbConnection.Instance.GetConnection();

            try
            {
                StringBuilder query = new StringBuilder(@"
                    SELECT 
                        h.id,
                        h.animal_id,
                        a.nombre as animal_nombre,
                        a.especie as tipo_animal,
                        pf.nombre + ' ' + pf.apellido as propietario_nombre,
                        h.notas_generales,
                        h.alergias,
                        h.condiciones_medicas,
                        h.created_at,
                        h.updated_at
                    FROM historico h
                    INNER JOIN animal a ON h.animal_id = a.id
                    LEFT JOIN persona p ON a.persona_id = p.id
                    LEFT JOIN persona_fisica pf ON p.id = pf.id
                    WHERE 1=1");

                var parameters = new List<SqlParameter>();

                if (animalId.HasValue)
                {
                    query.Append(" AND h.animal_id = @animalId");
                    parameters.Add(new SqlParameter("@animalId", animalId.Value));
                }

                if (!string.IsNullOrEmpty(buscar))
                {
                    query.Append(" AND (a.nombre LIKE @buscar OR h.notas_generales LIKE @buscar OR h.alergias LIKE @buscar OR h.condiciones_medicas LIKE @buscar)");
                    parameters.Add(new SqlParameter("@buscar", $"%{buscar}%"));
                }

                query.Append(" ORDER BY h.updated_at DESC");

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
                throw new Exception($"Error al listar historiales: {ex.Message}");
            }

            return dataTable;
        }

        /// <summary>
        /// Obtiene un historial específico por ID
        /// </summary>
        public DataTable ObtenerHistorialPorId(int id)
        {
            DataTable dataTable = new DataTable();
            SqlConnection connection = DbConnection.Instance.GetConnection();

            try
            {
                string query = @"
                    SELECT 
                        h.id,
                        h.animal_id,
                        a.nombre as animal_nombre,
                        a.especie as tipo_animal,
                        pf.nombre + ' ' + pf.apellido as propietario_nombre,
                        h.notas_generales,
                        h.alergias,
                        h.condiciones_medicas,
                        h.created_at,
                        h.updated_at
                    FROM historico h
                    INNER JOIN animal a ON h.animal_id = a.id
                    LEFT JOIN persona p ON a.persona_id = p.id
                    LEFT JOIN persona_fisica pf ON p.id = pf.id
                    WHERE h.id = @id";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add(new SqlParameter("@id", id));
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(dataTable);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener historial: {ex.Message}");
            }

            return dataTable;
        }

        /// <summary>
        /// Inserta un nuevo historial médico
        /// </summary>
        public bool InsertarHistorial(int animalId, string notasGenerales, string alergias, string condicionesMedicas)
        {
            SqlConnection connection = DbConnection.Instance.GetConnection();

            try
            {
                string query = @"
                    INSERT INTO historico (animal_id, notas_generales, alergias, condiciones_medicas, created_at, updated_at)
                    VALUES (@animal_id, @notas_generales, @alergias, @condiciones_medicas, GETDATE(), GETDATE())";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add(new SqlParameter("@animal_id", animalId));
                    command.Parameters.Add(new SqlParameter("@notas_generales", string.IsNullOrEmpty(notasGenerales) ? (object)DBNull.Value : notasGenerales));
                    command.Parameters.Add(new SqlParameter("@alergias", string.IsNullOrEmpty(alergias) ? (object)DBNull.Value : alergias));
                    command.Parameters.Add(new SqlParameter("@condiciones_medicas", string.IsNullOrEmpty(condicionesMedicas) ? (object)DBNull.Value : condicionesMedicas));

                    int filasAfectadas = command.ExecuteNonQuery();
                    return filasAfectadas > 0;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al insertar historial: {ex.Message}");
            }
        }

        /// <summary>
        /// Actualiza un historial médico existente
        /// </summary>
        public bool ActualizarHistorial(int id, int animalId, string notasGenerales, string alergias, string condicionesMedicas)
        {
            SqlConnection connection = DbConnection.Instance.GetConnection();

            try
            {
                string query = @"
                    UPDATE historico 
                    SET animal_id = @animal_id, 
                        notas_generales = @notas_generales, 
                        alergias = @alergias, 
                        condiciones_medicas = @condiciones_medicas,
                        updated_at = GETDATE()
                    WHERE id = @id";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add(new SqlParameter("@id", id));
                    command.Parameters.Add(new SqlParameter("@animal_id", animalId));
                    command.Parameters.Add(new SqlParameter("@notas_generales", string.IsNullOrEmpty(notasGenerales) ? (object)DBNull.Value : notasGenerales));
                    command.Parameters.Add(new SqlParameter("@alergias", string.IsNullOrEmpty(alergias) ? (object)DBNull.Value : alergias));
                    command.Parameters.Add(new SqlParameter("@condiciones_medicas", string.IsNullOrEmpty(condicionesMedicas) ? (object)DBNull.Value : condicionesMedicas));

                    int filasAfectadas = command.ExecuteNonQuery();
                    return filasAfectadas > 0;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al actualizar historial: {ex.Message}");
            }
        }

        /// <summary>
        /// Elimina un historial médico
        /// </summary>
        public bool EliminarHistorial(int id)
        {
            SqlConnection connection = DbConnection.Instance.GetConnection();

            try
            {
                string query = "DELETE FROM historico WHERE id = @id";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add(new SqlParameter("@id", id));

                    int filasAfectadas = command.ExecuteNonQuery();
                    return filasAfectadas > 0;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al eliminar historial: {ex.Message}");
            }
        }

        #endregion

        #region Métodos de Detalle de Historial

        /// <summary>
        /// Obtiene los detalles de un historial específico
        /// </summary>
        public DataTable ListarDetallesHistorial(int historialId)
        {
            DataTable dataTable = new DataTable();
            SqlConnection connection = DbConnection.Instance.GetConnection();

            try
            {
                string query = @"
                    SELECT 
                        dh.id,
                        dh.historico_id,
                        dh.tipo_registro,
                        dh.descripcion,
                        dh.fecha_registro,
                        dh.veterinario_id,
                        pr.nombre + ' ' + pr.apellido as veterinario_nombre,
                        dh.tratamiento,
                        dh.medicamentos,
                        dh.observaciones,
                        dh.created_at
                    FROM detalle_historico dh
                    LEFT JOIN personal pr ON dh.veterinario_id = pr.id
                    WHERE dh.historico_id = @historial_id
                    ORDER BY dh.fecha_registro DESC, dh.created_at DESC";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add(new SqlParameter("@historial_id", historialId));
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(dataTable);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener detalles del historial: {ex.Message}");
            }

            return dataTable;
        }

        /// <summary>
        /// Inserta un nuevo detalle de historial
        /// </summary>
        public bool InsertarDetalleHistorial(int historialId, string tipoRegistro, string descripcion, 
            DateTime fechaRegistro, int? veterinarioId, string tratamiento, string medicamentos, string observaciones)
        {
            SqlConnection connection = DbConnection.Instance.GetConnection();

            try
            {
                string query = @"
                    INSERT INTO detalle_historico (historico_id, tipo_registro, descripcion, fecha_registro, 
                                                 veterinario_id, tratamiento, medicamentos, observaciones, created_at)
                    VALUES (@historico_id, @tipo_registro, @descripcion, @fecha_registro, 
                            @veterinario_id, @tratamiento, @medicamentos, @observaciones, GETDATE())";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add(new SqlParameter("@historico_id", historialId));
                    command.Parameters.Add(new SqlParameter("@tipo_registro", tipoRegistro));
                    command.Parameters.Add(new SqlParameter("@descripcion", descripcion));
                    command.Parameters.Add(new SqlParameter("@fecha_registro", fechaRegistro));
                    command.Parameters.Add(new SqlParameter("@veterinario_id", veterinarioId.HasValue ? (object)veterinarioId.Value : DBNull.Value));
                    command.Parameters.Add(new SqlParameter("@tratamiento", string.IsNullOrEmpty(tratamiento) ? (object)DBNull.Value : tratamiento));
                    command.Parameters.Add(new SqlParameter("@medicamentos", string.IsNullOrEmpty(medicamentos) ? (object)DBNull.Value : medicamentos));
                    command.Parameters.Add(new SqlParameter("@observaciones", string.IsNullOrEmpty(observaciones) ? (object)DBNull.Value : observaciones));

                    int filasAfectadas = command.ExecuteNonQuery();
                    return filasAfectadas > 0;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al insertar detalle de historial: {ex.Message}");
            }
        }

        /// <summary>
        /// Elimina un detalle de historial
        /// </summary>
        public bool EliminarDetalleHistorial(int detalleId)
        {
            SqlConnection connection = DbConnection.Instance.GetConnection();

            try
            {
                string query = "DELETE FROM detalle_historico WHERE id = @id";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add(new SqlParameter("@id", detalleId));

                    int filasAfectadas = command.ExecuteNonQuery();
                    return filasAfectadas > 0;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al eliminar detalle de historial: {ex.Message}");
            }
        }

        #endregion

        #region Métodos de Búsqueda

        /// <summary>
        /// Busca historiales por criterios específicos
        /// </summary>
        public DataTable BuscarHistoriales(string criterio, string valor)
        {
            DataTable dataTable = new DataTable();
            SqlConnection connection = DbConnection.Instance.GetConnection();

            try
            {
                StringBuilder query = new StringBuilder(@"
                    SELECT 
                        h.id,
                        h.animal_id,
                        a.nombre as animal_nombre,
                        a.especie as tipo_animal,
                        pf.nombre + ' ' + pf.apellido as propietario_nombre,
                        h.notas_generales,
                        h.alergias,
                        h.condiciones_medicas,
                        h.created_at,
                        h.updated_at
                    FROM historico h
                    INNER JOIN animal a ON h.animal_id = a.id
                    LEFT JOIN persona p ON a.persona_id = p.id
                    LEFT JOIN persona_fisica pf ON p.id = pf.id
                    WHERE 1=1");

                var parameters = new List<SqlParameter>();

                switch (criterio.ToLower())
                {
                    case "animal":
                        query.Append(" AND a.nombre LIKE @valor");
                        break;
                    case "propietario":
                        query.Append(" AND (pf.nombre LIKE @valor OR pf.apellido LIKE @valor)");
                        break;
                    case "tipo_animal":
                        query.Append(" AND a.especie LIKE @valor");
                        break;
                    case "alergias":
                        query.Append(" AND h.alergias LIKE @valor");
                        break;
                    case "condiciones":
                        query.Append(" AND h.condiciones_medicas LIKE @valor");
                        break;
                    default:
                        query.Append(" AND (a.nombre LIKE @valor OR h.notas_generales LIKE @valor OR h.alergias LIKE @valor OR h.condiciones_medicas LIKE @valor)");
                        break;
                }

                parameters.Add(new SqlParameter("@valor", $"%{valor}%"));
                query.Append(" ORDER BY h.updated_at DESC");

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
                throw new Exception($"Error al buscar historiales: {ex.Message}");
            }

            return dataTable;
        }

        #endregion

        #region Métodos de Validación

        /// <summary>
        /// Verifica si existe un historial para un animal específico
        /// </summary>
        public bool ExisteHistorialParaAnimal(int animalId)
        {
            SqlConnection connection = DbConnection.Instance.GetConnection();

            try
            {
                string query = "SELECT COUNT(*) FROM historico WHERE animal_id = @animal_id";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add(new SqlParameter("@animal_id", animalId));
                    int count = (int)command.ExecuteScalar();
                    return count > 0;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al verificar historial: {ex.Message}");
            }
        }

        /// <summary>
        /// Obtiene la lista de animales para ComboBox
        /// </summary>
        public DataTable ObtenerAnimales()
        {
            DataTable dataTable = new DataTable();
            SqlConnection connection = DbConnection.Instance.GetConnection();

            try
            {
                string query = @"
                    SELECT 
                        a.id,
                        a.nombre + ' - ' + a.especie + ' (' + pf.nombre + ' ' + pf.apellido + ')' as display_text,
                        a.nombre,
                        a.especie as tipo_animal,
                        pf.nombre + ' ' + pf.apellido as propietario
                    FROM animal a
                    LEFT JOIN persona p ON a.persona_id = p.id
                    LEFT JOIN persona_fisica pf ON p.id = pf.id
                    ORDER BY a.nombre";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(dataTable);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener animales: {ex.Message}");
            }

            return dataTable;
        }

        /// <summary>
        /// Obtiene la lista de veterinarios para ComboBox
        /// </summary>
        public DataTable ObtenerVeterinarios()
        {
            DataTable dataTable = new DataTable();
            SqlConnection connection = DbConnection.Instance.GetConnection();

            try
            {
                string query = @"
                    SELECT 
                        p.id,
                        p.nombre + ' ' + p.apellido + ' - ' + pv.especialidad as display_text,
                        p.nombre,
                        p.apellido,
                        pv.especialidad
                    FROM personal p
                    INNER JOIN personal_veterinario pv ON p.id = pv.id
                    WHERE p.activo = 1
                    ORDER BY p.nombre, p.apellido";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(dataTable);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener veterinarios: {ex.Message}");
            }

            return dataTable;
        }

        #endregion
    }
}
