using CapaDatos;
using System;
using System.Data;
using System.Linq;

namespace CapaNegocio
{
    public class NCita
    {
        public static string ProgramarCita(int idAnimal, int idVeterinario, DateTime fechaHora, 
            string motivo, bool esUrgente = false, string tipo = "PRIMERA_VEZ", 
            decimal? costoConsulta = null, string observaciones = null)
        {
            // Validaciones de negocio
            if (idAnimal <= 0)
                return "Debe seleccionar un animal válido";

            if (idVeterinario <= 0)
                return "Debe seleccionar un veterinario válido";

            if (string.IsNullOrWhiteSpace(motivo))
                return "El motivo de la consulta es obligatorio";

            if (fechaHora <= DateTime.Now.AddMinutes(-5)) // 5 min de tolerancia
                return "No se pueden programar citas en el pasado";

            if (fechaHora > DateTime.Now.AddYears(1))
                return "No se pueden programar citas con más de un año de anticipación";

            // Validar horario laboral (8:00 AM - 6:30 PM)
            if (!EsHorarioValido(fechaHora))
                return "La cita debe programarse en horario de atención (8:00 AM - 6:30 PM)";

            // Validar que no sea domingo
            if (fechaHora.DayOfWeek == DayOfWeek.Sunday)
                return "No se pueden programar citas los domingos";

            // Validar tipo de cita
            string[] tiposValidos = { "PRIMERA_VEZ", "SEGUIMIENTO", "URGENCIA", "REVISION", "VACUNACION" };
            if (!tiposValidos.Contains(tipo.ToUpper()))
                return "Tipo de cita no válido";

            DCita obj = new DCita();
            obj.IdAnimal = idAnimal;
            obj.IdVeterinario = idVeterinario;
            obj.FechaCita = fechaHora;
            obj.Motivo = motivo;
            obj.Tipo = tipo.ToUpper();
            obj.EsUrgente = esUrgente;
            obj.CostoConsulta = costoConsulta;
            obj.Observaciones = observaciones;

            return obj.ProgramarCita(obj);
        }

        public static string ReprogramarCita(int idDiagnostico, DateTime nuevaFechaHora, 
            int? nuevoVeterinario = null, string observaciones = null)
        {
            if (idDiagnostico <= 0)
                return "ID de cita no válido";

            if (nuevaFechaHora <= DateTime.Now.AddMinutes(-5))
                return "No se puede reprogramar a una fecha pasada";

            if (!EsHorarioValido(nuevaFechaHora))
                return "La nueva fecha debe estar en horario de atención (8:00 AM - 6:30 PM)";

            if (nuevaFechaHora.DayOfWeek == DayOfWeek.Sunday)
                return "No se pueden programar citas los domingos";

            DCita obj = new DCita();
            obj.IdDiagnostico = idDiagnostico;
            obj.FechaCita = nuevaFechaHora;
            obj.Estado = "PROGRAMADO";
            
            if (nuevoVeterinario.HasValue && nuevoVeterinario.Value > 0)
                obj.IdVeterinario = nuevoVeterinario.Value;

            if (!string.IsNullOrEmpty(observaciones))
                obj.Observaciones = observaciones;

            return obj.ReprogramarCita(obj);
        }

        public static string CancelarCita(int idDiagnostico, string motivoCancelacion = null)
        {
            if (idDiagnostico <= 0)
                return "ID de cita no válido";

            return new DCita().CancelarCita(idDiagnostico, motivoCancelacion);
        }

        public static string ConfirmarCita(int idDiagnostico, string observaciones = null)
        {
            if (idDiagnostico <= 0)
                return "ID de cita no válido";

            DCita obj = new DCita();
            obj.IdDiagnostico = idDiagnostico;
            obj.Estado = "CONFIRMADO";
            obj.Observaciones = observaciones;

            return obj.CambiarEstado(obj);
        }

        public static string IniciarConsulta(int idDiagnostico)
        {
            if (idDiagnostico <= 0)
                return "ID de cita no válido";

            DCita obj = new DCita();
            obj.IdDiagnostico = idDiagnostico;
            obj.Estado = "EN_PROCESO";
            obj.Observaciones = "Consulta iniciada el " + DateTime.Now.ToString("dd/MM/yyyy HH:mm");

            return obj.CambiarEstado(obj);
        }

        public static DataTable CitasDelDia(DateTime? fecha = null)
        {
            return new DCita().CitasDelDia(fecha);
        }

        public static DataTable ProximasCitas()
        {
            return new DCita().ProximasCitas();
        }

        public static DataTable CitasPendientesConfirmacion()
        {
            DCita obj = new DCita();
            obj.FechaInicio = DateTime.Today;
            obj.FechaFin = DateTime.Today.AddDays(7); // Próxima semana
            
            var dt = obj.BuscarPorFecha(obj);
            
            if (dt != null && dt.Rows.Count > 0)
            {
                // Filtrar solo las programadas (pendientes de confirmación)
                var filteredRows = dt.AsEnumerable()
                    .Where(row => row.Field<string>("Estado") == "PROGRAMADO")
                    .CopyToDataTable();
                return filteredRows;
            }
            
            return dt;
        }

        public static DataTable BuscarPorFecha(DateTime fechaInicio, DateTime fechaFin, int idVeterinario = 0)
        {
            if (fechaFin < fechaInicio)
                return null;

            if ((fechaFin - fechaInicio).TotalDays > 365)
                return null; // No permitir búsquedas de más de un año

            DCita obj = new DCita();
            obj.FechaInicio = fechaInicio.Date;
            obj.FechaFin = fechaFin.Date.AddDays(1).AddSeconds(-1); // Incluir todo el último día
            obj.IdVeterinario = idVeterinario;

            return obj.BuscarPorFecha(obj);
        }

        public static DataTable BuscarPorAnimal(int idAnimal)
        {
            if (idAnimal <= 0)
                return null;

            return new DCita().BuscarPorAnimal(idAnimal);
        }

        public static DataTable BuscarTexto(string textoBuscar)
        {
            if (string.IsNullOrWhiteSpace(textoBuscar))
                return new DCita().Mostrar();

            DCita obj = new DCita();
            obj.TextoBuscar = textoBuscar.Trim();
            return obj.BuscarTexto(obj);
        }

        public static DataTable GetHorariosDisponibles(DateTime fecha, int idVeterinario)
        {
            if (idVeterinario <= 0)
                return null;

            if (fecha.Date < DateTime.Today)
                return null; // No mostrar horarios de fechas pasadas

            if (fecha.DayOfWeek == DayOfWeek.Sunday)
                return null; // No hay atención los domingos

            return new DCita().GetHorariosDisponibles(fecha, idVeterinario);
        }

        public static DataTable GetAgendaVeterinario(int idVeterinario, DateTime fecha)
        {
            if (idVeterinario <= 0)
                return null;

            DCita obj = new DCita();
            obj.FechaInicio = fecha.Date;
            obj.FechaFin = fecha.Date.AddDays(1).AddSeconds(-1);
            obj.IdVeterinario = idVeterinario;

            return obj.BuscarPorFecha(obj);
        }

        public static DataTable GetResumenSemanal(DateTime fechaInicio)
        {
            DateTime fechaFin = fechaInicio.AddDays(6); // Una semana
            
            DCita obj = new DCita();
            obj.FechaInicio = fechaInicio.Date;
            obj.FechaFin = fechaFin.Date.AddDays(1).AddSeconds(-1);

            return obj.BuscarPorFecha(obj);
        }

        public static DataTable Mostrar()
        {
            return new DCita().Mostrar();
        }

        // Métodos de utilidad
        public static bool EsHorarioValido(DateTime fechaHora)
        {
            TimeSpan hora = fechaHora.TimeOfDay;
            TimeSpan horaInicio = new TimeSpan(8, 0, 0);   // 8:00 AM
            TimeSpan horaFin = new TimeSpan(18, 30, 0);    // 6:30 PM
            TimeSpan horaAlmuerzoInicio = new TimeSpan(12, 0, 0);  // 12:00 PM
            TimeSpan horaAlmuerzoFin = new TimeSpan(14, 0, 0);     // 2:00 PM

            // Verificar que esté en horario de atención
            if (hora < horaInicio || hora > horaFin)
                return false;

            // Verificar que no esté en horario de almuerzo
            if (hora >= horaAlmuerzoInicio && hora < horaAlmuerzoFin)
                return false;

            return true;
        }

        public static string[] GetTiposCita()
        {
            return new string[]
            {
                "PRIMERA_VEZ",
                "SEGUIMIENTO", 
                "REVISION",
                "VACUNACION",
                "URGENCIA",
                "CIRUGIA",
                "ESTETICA",
                "CONSULTA_GENERAL"
            };
        }

        public static string[] GetEstadosCita()
        {
            return new string[]
            {
                "PROGRAMADO",
                "CONFIRMADO",
                "EN_PROCESO", 
                "COMPLETADO",
                "CANCELADO",
                "NO_ASISTIO"
            };
        }

        public static string GetDescripcionEstado(string estado)
        {
            switch (estado?.ToUpper())
            {
                case "PROGRAMADO": return "Programada";
                case "CONFIRMADO": return "Confirmada";
                case "EN_PROCESO": return "En Proceso";
                case "COMPLETADO": return "Completada";
                case "CANCELADO": return "Cancelada";
                case "NO_ASISTIO": return "No Asistió";
                default: return estado ?? "Desconocido";
            }
        }

        public static string GetDescripcionTipo(string tipo)
        {
            switch (tipo?.ToUpper())
            {
                case "PRIMERA_VEZ": return "Primera Vez";
                case "SEGUIMIENTO": return "Seguimiento";
                case "REVISION": return "Revisión";
                case "VACUNACION": return "Vacunación";
                case "URGENCIA": return "Urgencia";
                case "CIRUGIA": return "Cirugía";
                case "ESTETICA": return "Estética";
                case "CONSULTA_GENERAL": return "Consulta General";
                default: return tipo ?? "Consulta";
            }
        }

        public static bool EsCitaUrgente(DataRow cita)
        {
            if (cita == null) return false;
            
            string urgencia = cita["Urgencia"]?.ToString();
            return urgencia?.ToUpper() == "ALTA";
        }

        public static bool EsCitaProxima(DataRow cita, int minutosAntes = 30)
        {
            if (cita == null) return false;
            
            if (cita["FechaCita"] == DBNull.Value) return false;
            
            DateTime fechaCita = Convert.ToDateTime(cita["FechaCita"]);
            DateTime ahora = DateTime.Now;
            
            return fechaCita >= ahora && fechaCita <= ahora.AddMinutes(minutosAntes);
        }

        public static int ContarCitasDelDia(DateTime? fecha = null)
        {
            var dt = CitasDelDia(fecha);
            return dt?.Rows.Count ?? 0;
        }

        public static int ContarCitasPendientes()
        {
            var dt = CitasPendientesConfirmacion();
            return dt?.Rows.Count ?? 0;
        }

        public static decimal CalcularIngresosDia(DateTime? fecha = null)
        {
            var dt = CitasDelDia(fecha);
            if (dt == null || dt.Rows.Count == 0) return 0;
            
            decimal total = 0;
            foreach (DataRow row in dt.Rows)
            {
                if (row["Costo"] != DBNull.Value && row["Estado"].ToString() == "COMPLETADO")
                {
                    total += Convert.ToDecimal(row["Costo"]);
                }
            }
            
            return total;
        }

        public static bool ValidarDisponibilidadVeterinario(int idVeterinario, DateTime fechaHora, int idCitaExcluir = 0)
        {
            var horarios = GetHorariosDisponibles(fechaHora.Date, idVeterinario);
            if (horarios == null) return false;

            string horaTexto = fechaHora.ToString("HH:mm");
            
            foreach (DataRow row in horarios.Rows)
            {
                if (row["HoraTexto"].ToString() == horaTexto)
                {
                    return row["Estado"].ToString() == "DISPONIBLE";
                }
            }
            
            return false;
        }
    }
}