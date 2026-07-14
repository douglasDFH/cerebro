using System;

namespace CapaDatos.Entidades
{
    public class Cita
    {
        public int IdCita { get; set; }
        public int IdAnimal { get; set; }
        public int IdVeterinario { get; set; }
        public DateTime FechaCita { get; set; }
        public string TipoCita { get; set; }
        public string Motivo { get; set; }
        public string Estado { get; set; }
        public string Observaciones { get; set; }
        public DateTime FechaCreacion { get; set; }
        
        // Propiedades de navegación
        public string NombreMascota { get; set; }
        public string TipoAnimal { get; set; }
        public string NombrePropietario { get; set; }
        public string TelefonoPropietario { get; set; }
        public string NombreVeterinario { get; set; }
        
        public bool EsProgramada => Estado == "Programada";
        public bool EsCompletada => Estado == "Completada";
        public bool EsCancelada => Estado == "Cancelada";
        public bool EsNoAsistio => Estado == "NoAsistio";
    }
}