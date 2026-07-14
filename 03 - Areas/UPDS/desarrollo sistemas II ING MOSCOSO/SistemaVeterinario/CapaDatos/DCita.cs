using System;
using System.Data;
using System.Data.SqlClient;

namespace CapaDatos
{
    public class DCita : DbConnection
    {
        // Propiedades para manejar tanto citas nuevas como seguimientos
        private int _idDiagnostico;
        private int _idAnimal;
        private int _idVeterinario;
        private DateTime _fechaCita;
        private string _motivo;
        private string _tipo; // PRIMERA_VEZ, SEGUIMIENTO, URGENCIA
        private string _estado; // PROGRAMADA, CONFIRMADA, COMPLETADA, CANCELADA
        private string _observaciones;
        private decimal? _costoConsulta;
        private bool _esUrgente;
        private string _textoBuscar;
        private DateTime? _fechaInicio;
        private DateTime? _fechaFin;

        // Propiedades públicas
        public int IdDiagnostico { get => _idDiagnostico; set => _idDiagnostico = value; }
        public int IdAnimal { get => _idAnimal; set => _idAnimal = value; }
        public int IdVeterinario { get => _idVeterinario; set => _idVeterinario = value; }
        public DateTime FechaCita { get => _fechaCita; set => _fechaCita = value; }
        public string Motivo { get => _motivo; set => _motivo = value; }
        public string Tipo { get => _tipo; set => _tipo = value; }
        public string Estado { get => _estado; set => _estado = value; }
        public string Observaciones { get => _observaciones; set => _observaciones = value; }
        public decimal? CostoConsulta { get => _costoConsulta; set => _costoConsulta = value; }
        public bool EsUrgente { get => _esUrgente; set => _esUrgente = value; }
        public string TextoBuscar { get => _textoBuscar; set => _textoBuscar = value; }
        public DateTime? FechaInicio { get => _fechaInicio; set => _fechaInicio = value; }
        public DateTime? FechaFin { get => _fechaFin; set => _fechaFin = value; }

        public DCita()
        {
            Estado = "PROGRAMADA";
            Tipo = "PRIMERA_VEZ";
            EsUrgente = false;
        }

        // Programar una nueva cita (crea un diagnóstico preliminar)
        public string ProgramarCita(DCita cita)
        {
            string rpta = "";
            using (SqlConnection connection = GetConnection())
            {
                connection.Open();
                try
                {
                    using (SqlCommand command = new SqlCommand("SP_ProgramarCita", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@IdAnimal", cita.IdAnimal);
                        command.Parameters.AddWithValue("@IdVeterinario", cita.IdVeterinario);
                        command.Parameters.AddWithValue("@FechaCita", cita.FechaCita);
                        command.Parameters.AddWithValue("@Motivo", cita.Motivo);
                        command.Parameters.AddWithValue("@Tipo", cita.Tipo);
                        command.Parameters.AddWithValue("@EsUrgente", cita.EsUrgente);
                        command.Parameters.AddWithValue("@CostoConsulta", cita.CostoConsulta ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@Observaciones", cita.Observaciones ?? (object)DBNull.Value);

                        var result = command.ExecuteScalar();
                        if (result != null && int.TryParse(result.ToString(), out int idDiagnostico))
                        {
                            cita.IdDiagnostico = idDiagnostico;
                            rpta = "OK";
                        }
                        else
                        {
                            rpta = "No se pudo programar la cita";
                        }
                    }
                }
                catch (Exception ex)
                {
                    rpta = ex.Message;
                }
            }
            return rpta;
        }

        // Reprogramar una cita existente
        public string ReprogramarCita(DCita cita)
        {
            string rpta = "";
            using (SqlConnection connection = GetConnection())
            {
                connection.Open();
                try
                {
                    string sql = @"
                        UPDATE Diagnostico 
                        SET Fecha = @FechaCita,
                            IdVeterinario = @IdVeterinario,
                            Descripcion = @Motivo,
                            Urgencia = CASE WHEN @EsUrgente = 1 THEN 'ALTA' ELSE 'NORMAL' END,
                            Estado = @Estado,
                            Costo = @CostoConsulta,
                            Observaciones = @Observaciones
                        WHERE IdDiagnostico = @IdDiagnostico";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@IdDiagnostico", cita.IdDiagnostico);
                        command.Parameters.AddWithValue("@FechaCita", cita.FechaCita);
                        command.Parameters.AddWithValue("@IdVeterinario", cita.IdVeterinario);
                        command.Parameters.AddWithValue("@Motivo", cita.Motivo);
                        command.Parameters.AddWithValue("@Estado", cita.Estado);
                        command.Parameters.AddWithValue("@EsUrgente", cita.EsUrgente);
                        command.Parameters.AddWithValue("@CostoConsulta", cita.CostoConsulta ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@Observaciones", cita.Observaciones ?? (object)DBNull.Value);

                        rpta = command.ExecuteNonQuery() == 1 ? "OK" : "No se actualizó la cita";
                    }
                }
                catch (Exception ex)
                {
                    rpta = ex.Message;
                }
            }
            return rpta;
        }

        // Cambiar estado de la cita
        public string CambiarEstado(DCita cita)
        {
            string rpta = "";
            using (SqlConnection connection = GetConnection())
            {
                connection.Open();
                try
                {
                    string sql = @"
                        UPDATE Diagnostico 
                        SET Estado = @Estado,
                            Observaciones = CASE 
                                WHEN @Observaciones IS NOT NULL THEN ISNULL(Observaciones, '') + CHAR(13) + @Observaciones
                                ELSE Observaciones
                            END
                        WHERE IdDiagnostico = @IdDiagnostico";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@IdDiagnostico", cita.IdDiagnostico);
                        command.Parameters.AddWithValue("@Estado", cita.Estado);
                        command.Parameters.AddWithValue("@Observaciones", cita.Observaciones ?? (object)DBNull.Value);

                        rpta = command.ExecuteNonQuery() == 1 ? "OK" : "No se pudo actualizar el estado";
                    }
                }
                catch (Exception ex)
                {
                    rpta = ex.Message;
                }
            }
            return rpta;
        }

        // Cancelar cita
        public string CancelarCita(int idDiagnostico, string motivo = null)
        {
            string rpta = "";
            using (SqlConnection connection = GetConnection())
            {
                connection.Open();
                try
                {
                    string sql = @"
                        UPDATE Diagnostico 
                        SET Estado = 'CANCELADO',
                            Observaciones = CASE 
                                WHEN @Motivo IS NOT NULL THEN ISNULL(Observaciones, '') + CHAR(13) + 'CANCELADO: ' + @Motivo
                                ELSE ISNULL(Observaciones, '') + CHAR(13) + 'CITA CANCELADA'
                            END
                        WHERE IdDiagnostico = @IdDiagnostico";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@IdDiagnostico", idDiagnostico);
                        command.Parameters.AddWithValue("@Motivo", motivo ?? (object)DBNull.Value);

                        rpta = command.ExecuteNonQuery() == 1 ? "OK" : "No se pudo cancelar la cita";
                    }
                }
                catch (Exception ex)
                {
                    rpta = ex.Message;
                }
            }
            return rpta;
        }

        // Obtener citas del día (usando vista existente y diagnósticos programados)
        public DataTable CitasDelDia(DateTime? fecha = null)
        {
            DataTable dtResultado = new DataTable("CitasDelDia");
            using (SqlConnection connection = GetConnection())
            {
                connection.Open();
                try
                {
                    DateTime fechaConsulta = fecha ?? DateTime.Today;
                    
                    string sql = @"
                        SELECT 
                            d.IdDiagnostico,
                            d.IdAnimal,
                            d.IdVeterinario,
                            d.Fecha AS FechaCita,
                            FORMAT(d.Fecha, 'HH:mm') AS Hora,
                            a.Nombre AS NombreAnimal,
                            a.Tipo AS TipoAnimal,
                            CASE 
                                WHEN p.TipoPersona = 'F' THEN p.Nombre + ' ' + ISNULL(p.Apellidos, '')
                                ELSE p.RazonSocial
                            END AS Propietario,
                            p.Telefono,
                            u.NombreUsuario AS Veterinario,
                            d.Descripcion AS Motivo,
                            d.Estado,
                            d.Urgencia,
                            CASE 
                                WHEN d.Urgencia = 'ALTA' THEN 1
                                ELSE 0
                            END AS EsUrgente,
                            d.Costo
                        FROM Diagnostico d
                        INNER JOIN Animal a ON d.IdAnimal = a.IdAnimal
                        INNER JOIN Persona p ON a.IdPropietario = p.IdPersona
                        INNER JOIN Usuario u ON d.IdVeterinario = u.IdUsuario
                        WHERE CAST(d.Fecha AS DATE) = @Fecha
                        AND d.Estado IN ('PROGRAMADO', 'CONFIRMADO', 'EN_PROCESO')
                        AND a.Estado = 1
                        ORDER BY d.Fecha";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@Fecha", fechaConsulta.Date);
                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            adapter.Fill(dtResultado);
                        }
                    }
                }
                catch
                {
                    dtResultado = null;
                }
            }
            return dtResultado;
        }

        // Obtener próximas citas (usa la vista existente)
        public DataTable ProximasCitas()
        {
            DataTable dtResultado = new DataTable("ProximasCitas");
            using (SqlConnection connection = GetConnection())
            {
                connection.Open();
                try
                {
                    string sql = @"
                        SELECT 
                            IdDiagnostico,
                            NombreAnimal,
                            Propietario,
                            Telefono,
                            Veterinario,
                            ProximaVisita,
                            UltimoDiagnostico,
                            DiasHasta
                        FROM VW_CitasProximas
                        WHERE ProximaVisita IS NOT NULL
                        AND DiasHasta >= 0
                        ORDER BY ProximaVisita";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            adapter.Fill(dtResultado);
                        }
                    }
                }
                catch
                {
                    dtResultado = null;
                }
            }
            return dtResultado;
        }

        // Buscar citas por rango de fechas
        public DataTable BuscarPorFecha(DCita cita)
        {
            DataTable dtResultado = new DataTable("CitasPorFecha");
            using (SqlConnection connection = GetConnection())
            {
                connection.Open();
                try
                {
                    string sql = @"
                        SELECT 
                            d.IdDiagnostico,
                            d.IdAnimal,
                            d.IdVeterinario,
                            d.Fecha AS FechaCita,
                            a.Nombre AS NombreAnimal,
                            CASE 
                                WHEN p.TipoPersona = 'F' THEN p.Nombre + ' ' + ISNULL(p.Apellidos, '')
                                ELSE p.RazonSocial
                            END AS Propietario,
                            p.Telefono,
                            u.NombreUsuario AS Veterinario,
                            d.Descripcion AS Motivo,
                            d.Estado,
                            d.Urgencia,
                            d.Costo
                        FROM Diagnostico d
                        INNER JOIN Animal a ON d.IdAnimal = a.IdAnimal
                        INNER JOIN Persona p ON a.IdPropietario = p.IdPersona
                        INNER JOIN Usuario u ON d.IdVeterinario = u.IdUsuario
                        WHERE CAST(d.Fecha AS DATE) BETWEEN @FechaInicio AND @FechaFin
                        AND (@IdVeterinario = 0 OR d.IdVeterinario = @IdVeterinario)
                        AND a.Estado = 1
                        ORDER BY d.Fecha";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@FechaInicio", cita.FechaInicio ?? DateTime.Today);
                        command.Parameters.AddWithValue("@FechaFin", cita.FechaFin ?? DateTime.Today.AddDays(30));
                        command.Parameters.AddWithValue("@IdVeterinario", cita.IdVeterinario);
                        
                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            adapter.Fill(dtResultado);
                        }
                    }
                }
                catch
                {
                    dtResultado = null;
                }
            }
            return dtResultado;
        }

        // Buscar citas por animal
        public DataTable BuscarPorAnimal(int idAnimal)
        {
            DataTable dtResultado = new DataTable("CitasPorAnimal");
            using (SqlConnection connection = GetConnection())
            {
                connection.Open();
                try
                {
                    string sql = @"
                        SELECT 
                            d.IdDiagnostico,
                            d.Fecha AS FechaCita,
                            a.Nombre AS NombreAnimal,
                            u.NombreUsuario AS Veterinario,
                            d.Descripcion AS Motivo,
                            d.Estado,
                            d.Urgencia,
                            d.ProximaVisita,
                            d.Costo
                        FROM Diagnostico d
                        INNER JOIN Animal a ON d.IdAnimal = a.IdAnimal
                        INNER JOIN Usuario u ON d.IdVeterinario = u.IdUsuario
                        WHERE d.IdAnimal = @IdAnimal
                        AND a.Estado = 1
                        ORDER BY d.Fecha DESC";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@IdAnimal", idAnimal);
                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            adapter.Fill(dtResultado);
                        }
                    }
                }
                catch
                {
                    dtResultado = null;
                }
            }
            return dtResultado;
        }

        // Obtener horarios disponibles para un veterinario en una fecha
        public DataTable GetHorariosDisponibles(DateTime fecha, int idVeterinario)
        {
            DataTable dtResultado = new DataTable("HorariosDisponibles");
            using (SqlConnection connection = GetConnection())
            {
                connection.Open();
                try
                {
                    string sql = @"
                        WITH HorariosBase AS (
                            SELECT CAST(@Fecha AS DATETIME2) + CAST('08:00:00' AS TIME) AS Hora, '08:00' AS HoraTexto
                            UNION ALL SELECT CAST(@Fecha AS DATETIME2) + CAST('08:30:00' AS TIME), '08:30'
                            UNION ALL SELECT CAST(@Fecha AS DATETIME2) + CAST('09:00:00' AS TIME), '09:00'
                            UNION ALL SELECT CAST(@Fecha AS DATETIME2) + CAST('09:30:00' AS TIME), '09:30'
                            UNION ALL SELECT CAST(@Fecha AS DATETIME2) + CAST('10:00:00' AS TIME), '10:00'
                            UNION ALL SELECT CAST(@Fecha AS DATETIME2) + CAST('10:30:00' AS TIME), '10:30'
                            UNION ALL SELECT CAST(@Fecha AS DATETIME2) + CAST('11:00:00' AS TIME), '11:00'
                            UNION ALL SELECT CAST(@Fecha AS DATETIME2) + CAST('11:30:00' AS TIME), '11:30'
                            UNION ALL SELECT CAST(@Fecha AS DATETIME2) + CAST('14:00:00' AS TIME), '14:00'
                            UNION ALL SELECT CAST(@Fecha AS DATETIME2) + CAST('14:30:00' AS TIME), '14:30'
                            UNION ALL SELECT CAST(@Fecha AS DATETIME2) + CAST('15:00:00' AS TIME), '15:00'
                            UNION ALL SELECT CAST(@Fecha AS DATETIME2) + CAST('15:30:00' AS TIME), '15:30'
                            UNION ALL SELECT CAST(@Fecha AS DATETIME2) + CAST('16:00:00' AS TIME), '16:00'
                            UNION ALL SELECT CAST(@Fecha AS DATETIME2) + CAST('16:30:00' AS TIME), '16:30'
                            UNION ALL SELECT CAST(@Fecha AS DATETIME2) + CAST('17:00:00' AS TIME), '17:00'
                            UNION ALL SELECT CAST(@Fecha AS DATETIME2) + CAST('17:30:00' AS TIME), '17:30'
                            UNION ALL SELECT CAST(@Fecha AS DATETIME2) + CAST('18:00:00' AS TIME), '18:00'
                        )
                        SELECT 
                            h.Hora,
                            h.HoraTexto,
                            CASE 
                                WHEN d.IdDiagnostico IS NOT NULL THEN 'OCUPADO'
                                WHEN h.Hora < GETDATE() THEN 'PASADO'
                                ELSE 'DISPONIBLE'
                            END AS Estado
                        FROM HorariosBase h
                        LEFT JOIN Diagnostico d ON ABS(DATEDIFF(MINUTE, d.Fecha, h.Hora)) < 30
                                                 AND d.IdVeterinario = @IdVeterinario
                                                 AND d.Estado IN ('PROGRAMADO', 'CONFIRMADO', 'EN_PROCESO')
                        ORDER BY h.Hora";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@Fecha", fecha.Date);
                        command.Parameters.AddWithValue("@IdVeterinario", idVeterinario);
                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            adapter.Fill(dtResultado);
                        }
                    }
                }
                catch
                {
                    dtResultado = null;
                }
            }
            return dtResultado;
        }

        // Buscar citas por texto
        public DataTable BuscarTexto(DCita cita)
        {
            DataTable dtResultado = new DataTable("BusquedaCitas");
            using (SqlConnection connection = GetConnection())
            {
                connection.Open();
                try
                {
                    string sql = @"
                        SELECT 
                            d.IdDiagnostico,
                            d.IdAnimal,
                            d.IdVeterinario,
                            d.Fecha AS FechaCita,
                            a.Nombre AS NombreAnimal,
                            a.Tipo AS TipoAnimal,
                            CASE 
                                WHEN p.TipoPersona = 'F' THEN p.Nombre + ' ' + ISNULL(p.Apellidos, '')
                                ELSE p.RazonSocial
                            END AS Propietario,
                            p.Telefono,
                            u.NombreUsuario AS Veterinario,
                            d.Descripcion AS Motivo,
                            d.Estado,
                            d.Urgencia,
                            d.ProximaVisita,
                            d.Costo
                        FROM Diagnostico d
                        INNER JOIN Animal a ON d.IdAnimal = a.IdAnimal
                        INNER JOIN Persona p ON a.IdPropietario = p.IdPersona
                        INNER JOIN Usuario u ON d.IdVeterinario = u.IdUsuario
                        WHERE a.Estado = 1
                        AND (
                            a.Nombre LIKE '%' + @TextoBuscar + '%' OR
                            d.Descripcion LIKE '%' + @TextoBuscar + '%' OR
                            (CASE 
                                WHEN p.TipoPersona = 'F' THEN p.Nombre + ' ' + ISNULL(p.Apellidos, '')
                                ELSE p.RazonSocial
                            END) LIKE '%' + @TextoBuscar + '%' OR
                            u.NombreUsuario LIKE '%' + @TextoBuscar + '%' OR
                            d.Estado LIKE '%' + @TextoBuscar + '%'
                        )
                        ORDER BY d.Fecha DESC";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@TextoBuscar", cita.TextoBuscar ?? "");
                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            adapter.Fill(dtResultado);
                        }
                    }
                }
                catch
                {
                    dtResultado = null;
                }
            }
            return dtResultado;
        }

        // Mostrar todas las citas programadas
        public DataTable Mostrar()
        {
            DataTable dtResultado = new DataTable("TodasCitas");
            using (SqlConnection connection = GetConnection())
            {
                connection.Open();
                try
                {
                    string sql = @"
                        SELECT 
                            d.IdDiagnostico,
                            d.IdAnimal,
                            d.IdVeterinario,
                            d.Fecha AS FechaCita,
                            a.Nombre AS NombreAnimal,
                            a.Tipo AS TipoAnimal,
                            CASE 
                                WHEN p.TipoPersona = 'F' THEN p.Nombre + ' ' + ISNULL(p.Apellidos, '')
                                ELSE p.RazonSocial
                            END AS Propietario,
                            p.Telefono,
                            u.NombreUsuario AS Veterinario,
                            d.Descripcion AS Motivo,
                            d.Estado,
                            d.Urgencia,
                            d.ProximaVisita,
                            d.Costo
                        FROM Diagnostico d
                        INNER JOIN Animal a ON d.IdAnimal = a.IdAnimal
                        INNER JOIN Persona p ON a.IdPropietario = p.IdPersona
                        INNER JOIN Usuario u ON d.IdVeterinario = u.IdUsuario
                        WHERE a.Estado = 1
                        ORDER BY d.Fecha DESC";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            adapter.Fill(dtResultado);
                        }
                    }
                }
                catch
                {
                    dtResultado = null;
                }
            }
            return dtResultado;
        }
    }
}