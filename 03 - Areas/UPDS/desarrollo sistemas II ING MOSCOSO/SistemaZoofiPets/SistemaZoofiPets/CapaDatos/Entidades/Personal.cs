using System;

namespace CapaDatos.Entidades
{
    public class Personal
    {
        public int IdPersonal { get; set; }
        public int IdPersona { get; set; }
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public DateTime FechaContratacion { get; set; }
        public string TipoPersonal { get; set; }
        public string NumeroLicencia { get; set; }
        public string Especialidad { get; set; }
        public decimal? Salario { get; set; }
        public bool Activo { get; set; }
        
        // Propiedades de navegación
        public string Email { get; set; }
        public string Telefono { get; set; }
        public string Direccion { get; set; }
        
        public string NombreCompleto => $"{Nombre} {Apellidos}";
        public bool EsVeterinario => TipoPersonal == "Veterinario";
        public bool EsAuxiliar => TipoPersonal == "Auxiliar";
    }
}