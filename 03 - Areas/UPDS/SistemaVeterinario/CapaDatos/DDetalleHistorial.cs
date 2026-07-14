using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace CapaDatos
{
    public class DDetalleHistorial
    {
        #region Propiedades

        public int Id { get; set; }
        public int HistorialId { get; set; }
        public string TipoEvento { get; set; }
        public DateTime FechaEvento { get; set; }
        public string Observaciones { get; set; }
        public string Tratamiento { get; set; }
        public string Medicamentos { get; set; }
        public string Dosis { get; set; }
        public int? VeterinarioId { get; set; }
        public decimal? PesoAnimal { get; set; }
        public decimal? Temperatura { get; set; }
        public bool Cobrado { get; set; }
        public decimal Costo { get; set; }
        public string TextoBuscar { get; set; }

        #endregion

        #region Constructores

        public DDetalleHistorial()
        {
        }

        public DDetalleHistorial(int id, int historialId, string tipoEvento, DateTime fechaEvento,
            string observaciones, string tratamiento, string medicamentos, string dosis,
            int? veterinarioId, decimal? pesoAnimal, decimal? temperatura, bool cobrado, decimal costo)
        {
            this.Id = id;
            this.HistorialId = historialId;
            this.TipoEvento = tipoEvento;
            this.FechaEvento = fechaEvento;
            this.Observaciones = observaciones;
            this.Tratamiento = tratamiento;
            this.Medicamentos = medicamentos;
            this.Dosis = dosis;
            this.VeterinarioId = veterinarioId;
            this.PesoAnimal = pesoAnimal;
            this.Temperatura = temperatura;
            this.Cobrado = cobrado;
            this.Costo = costo;
        }

        #endregion

        #region Métodos CRUD

        /// <summary>
        /// Inserta un nuevo detalle de historial médico
        /// </summary>
        public string Insertar()
        {
            string respuesta = "";
            using (SqlConnection conexion = new SqlConnection(DbConnection.GetConnectionString()))
            {
                try
                {
                    conexion.Open();
                    SqlCommand comando = new SqlCommand("sp_insertar_detalle_historial", conexion)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    comando.Parameters.AddWithValue("@historico_id", this.HistorialId);
                    comando.Parameters.AddWithValue("@tipo_evento", this.TipoEvento);
                    comando.Parameters.AddWithValue("@fecha_evento", this.FechaEvento);
                    comando.Parameters.AddWithValue("@observaciones", this.Observaciones);
                    comando.Parameters.AddWithValue("@tratamiento", (object)this.Tratamiento ?? DBNull.Value);
                    comando.Parameters.AddWithValue("@medicamentos", (object)this.Medicamentos ?? DBNull.Value);
                    comando.Parameters.AddWithValue("@dosis", (object)this.Dosis ?? DBNull.Value);
                    comando.Parameters.AddWithValue("@veterinario_id", (object)this.VeterinarioId ?? DBNull.Value);
                    comando.Parameters.AddWithValue("@peso_animal", (object)this.PesoAnimal ?? DBNull.Value);
                    comando.Parameters.AddWithValue("@temperatura", (object)this.Temperatura ?? DBNull.Value);
                    comando.Parameters.AddWithValue("@cobrado", this.Cobrado);
                    comando.Parameters.AddWithValue("@costo", this.Costo);

                    respuesta = comando.ExecuteScalar()?.ToString();
                }
                catch (Exception ex)
                {
                    respuesta = $"Error: {ex.Message}";
                }
            }
            return respuesta;
        }

        /// <summary>
        /// Edita un detalle de historial médico existente
        /// </summary>
        public string Editar()
        {
            string respuesta = "";
            using (SqlConnection conexion = new SqlConnection(DbConnection.GetConnectionString()))
            {
                try
                {
                    conexion.Open();
                    SqlCommand comando = new SqlCommand("sp_editar_detalle_historial", conexion)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    comando.Parameters.AddWithValue("@id", this.Id);
                    comando.Parameters.AddWithValue("@historico_id", this.HistorialId);
                    comando.Parameters.AddWithValue("@tipo_evento", this.TipoEvento);
                    comando.Parameters.AddWithValue("@fecha_evento", this.FechaEvento);
                    comando.Parameters.AddWithValue("@observaciones", this.Observaciones);
                    comando.Parameters.AddWithValue("@tratamiento", (object)this.Tratamiento ?? DBNull.Value);
                    comando.Parameters.AddWithValue("@medicamentos", (object)this.Medicamentos ?? DBNull.Value);
                    comando.Parameters.AddWithValue("@dosis", (object)this.Dosis ?? DBNull.Value);
                    comando.Parameters.AddWithValue("@veterinario_id", (object)this.VeterinarioId ?? DBNull.Value);
                    comando.Parameters.AddWithValue("@peso_animal", (object)this.PesoAnimal ?? DBNull.Value);
                    comando.Parameters.AddWithValue("@temperatura", (object)this.Temperatura ?? DBNull.Value);
                    comando.Parameters.AddWithValue("@cobrado", this.Cobrado);
                    comando.Parameters.AddWithValue("@costo", this.Costo);

                    respuesta = comando.ExecuteScalar()?.ToString();
                }
                catch (Exception ex)
                {
                    respuesta = $"Error: {ex.Message}";
                }
            }
            return respuesta;
        }

        /// <summary>
        /// Elimina un detalle de historial médico
        /// </summary>
        public string Eliminar()
        {
            string respuesta = "";
            using (SqlConnection conexion = new SqlConnection(DbConnection.GetConnectionString()))
            {
                try
                {
                    conexion.Open();
                    SqlCommand comando = new SqlCommand("sp_eliminar_detalle_historial", conexion)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    comando.Parameters.AddWithValue("@id", this.Id);
                    respuesta = comando.ExecuteScalar()?.ToString();
                }
                catch (Exception ex)
                {
                    respuesta = $"Error: {ex.Message}";
                }
            }
            return respuesta;
        }

        /// <summary>
        /// Obtiene todos los detalles de un historial específico
        /// </summary>
        public DataTable ObtenerPorHistorial(int historialId)
        {
            DataTable tabla = new DataTable();
            using (SqlConnection conexion = new SqlConnection(DbConnection.GetConnectionString()))
            {
                try
                {
                    conexion.Open();
                    SqlCommand comando = new SqlCommand("sp_mostrar_detalles_por_historial", conexion)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    comando.Parameters.AddWithValue("@historico_id", historialId);

                    SqlDataAdapter adaptador = new SqlDataAdapter(comando);
                    adaptador.Fill(tabla);
                }
                catch (Exception ex)
                {
                    tabla = new DataTable();
                    Console.WriteLine($"Error en ObtenerPorHistorial: {ex.Message}");
                }
            }
            return tabla;
        }

        /// <summary>
        /// Obtiene un detalle específico por ID
        /// </summary>
        public DataTable ObtenerPorId(int id)
        {
            DataTable tabla = new DataTable();
            using (SqlConnection conexion = new SqlConnection(DbConnection.GetConnectionString()))
            {
                try
                {
                    conexion.Open();
                    SqlCommand comando = new SqlCommand("sp_obtener_detalle_historial_por_id", conexion)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    comando.Parameters.AddWithValue("@id", id);

                    SqlDataAdapter adaptador = new SqlDataAdapter(comando);
                    adaptador.Fill(tabla);
                }
                catch (Exception ex)
                {
                    tabla = new DataTable();
                    Console.WriteLine($"Error en ObtenerPorId: {ex.Message}");
                }
            }
            return tabla;
        }

        /// <summary>
        /// Obtiene todos los veterinarios disponibles
        /// </summary>
        public DataTable ObtenerVeterinarios()
        {
            DataTable tabla = new DataTable();
            using (SqlConnection conexion = new SqlConnection(DbConnection.GetConnectionString()))
            {
                try
                {
                    conexion.Open();
                    SqlCommand comando = new SqlCommand(@"
                        SELECT p.id, pf.nombre, pf.apellido, pv.especialidad,
                               CONCAT(pf.nombre, ' ', pf.apellido) AS nombre_completo
                        FROM personal p
                        INNER JOIN persona pe ON p.id = pe.id
                        INNER JOIN persona_fisica pf ON pe.id = pf.id
                        LEFT JOIN personal_veterinario pv ON p.id = pv.id
                        WHERE p.rol = 'Veterinario' 
                          AND p.activo = 1
                        ORDER BY pf.nombre, pf.apellido", conexion);

                    SqlDataAdapter adaptador = new SqlDataAdapter(comando);
                    adaptador.Fill(tabla);
                }
                catch (Exception ex)
                {
                    tabla = new DataTable();
                    Console.WriteLine($"Error en ObtenerVeterinarios: {ex.Message}");
                }
            }
            return tabla;
        }

        /// <summary>
        /// Busca detalles de historial por texto
        /// </summary>
        public DataTable BuscarTexto(string textoBuscar)
        {
            DataTable tabla = new DataTable();
            using (SqlConnection conexion = new SqlConnection(DbConnection.GetConnectionString()))
            {
                try
                {
                    conexion.Open();
                    SqlCommand comando = new SqlCommand(@"
                        SELECT dh.id, dh.historico_id, dh.tipo_evento, dh.fecha_evento,
                               dh.observaciones, dh.tratamiento, dh.medicamentos, dh.dosis,
                               dh.veterinario_id, dh.peso_animal, dh.temperatura, dh.cobrado, dh.costo,
                               h.animal_id, a.nombre as animal_nombre,
                               CASE 
                                   WHEN dh.veterinario_id IS NOT NULL 
                                   THEN CONCAT(pf.nombre, ' ', pf.apellido)
                                   ELSE 'Sin asignar'
                               END as veterinario_nombre
                        FROM detalle_historico dh
                        INNER JOIN historico h ON dh.historico_id = h.id
                        INNER JOIN animal a ON h.animal_id = a.id
                        LEFT JOIN personal p ON dh.veterinario_id = p.id
                        LEFT JOIN persona pe ON p.id = pe.id
                        LEFT JOIN persona_fisica pf ON pe.id = pf.id
                        WHERE dh.observaciones LIKE '%' + @texto + '%' 
                           OR dh.tratamiento LIKE '%' + @texto + '%'
                           OR dh.medicamentos LIKE '%' + @texto + '%'
                           OR dh.tipo_evento LIKE '%' + @texto + '%'
                           OR a.nombre LIKE '%' + @texto + '%'
                        ORDER BY dh.fecha_evento DESC", conexion);

                    comando.Parameters.AddWithValue("@texto", textoBuscar);
                    SqlDataAdapter adaptador = new SqlDataAdapter(comando);
                    adaptador.Fill(tabla);
                }
                catch (Exception ex)
                {
                    tabla = new DataTable();
                    Console.WriteLine($"Error en BuscarTexto: {ex.Message}");
                }
            }
            return tabla;
        }

        /// <summary>
        /// Verifica si existe un detalle de historial
        /// </summary>
        public bool ExisteDetalle(int id)
        {
            bool existe = false;
            using (SqlConnection conexion = new SqlConnection(DbConnection.GetConnectionString()))
            {
                try
                {
                    conexion.Open();
                    SqlCommand comando = new SqlCommand(
                        "SELECT COUNT(*) FROM detalle_historico WHERE id = @id", conexion);
                    comando.Parameters.AddWithValue("@id", id);

                    int count = (int)comando.ExecuteScalar();
                    existe = count > 0;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error en ExisteDetalle: {ex.Message}");
                    existe = false;
                }
            }
            return existe;
        }

        /// <summary>
        /// Obtiene estadísticas de detalles de historial
        /// </summary>
        public DataTable ObtenerEstadisticas()
        {
            DataTable tabla = new DataTable();
            using (SqlConnection conexion = new SqlConnection(DbConnection.GetConnectionString()))
            {
                try
                {
                    conexion.Open();
                    SqlCommand comando = new SqlCommand(@"
                        SELECT 
                            tipo_evento,
                            COUNT(*) as cantidad,
                            AVG(CAST(costo as float)) as costo_promedio,
                            SUM(costo) as costo_total
                        FROM detalle_historico 
                        GROUP BY tipo_evento
                        ORDER BY cantidad DESC", conexion);

                    SqlDataAdapter adaptador = new SqlDataAdapter(comando);
                    adaptador.Fill(tabla);
                }
                catch (Exception ex)
                {
                    tabla = new DataTable();
                    Console.WriteLine($"Error en ObtenerEstadisticas: {ex.Message}");
                }
            }
            return tabla;
        }

        #endregion
    }
}
