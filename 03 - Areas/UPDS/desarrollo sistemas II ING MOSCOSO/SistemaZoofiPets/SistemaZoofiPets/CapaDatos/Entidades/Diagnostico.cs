using System;

namespace CapaDatos.Entidades
{
    public class Diagnostico
    {
        public int IdDiagnostico { get; set; }
        public int IdAnimal { get; set; }
        public int IdVeterinario { get; set; }
        public int? IdHistorico { get; set; }
        public DateTime Fecha { get; set; }
        public string Descripcion { get; set; }
        public string Tratamiento { get; set; }
        public string Observaciones { get; set; }
        public string Estado { get; set; }
        
        // Propiedades de navegación
        public string NombreAnimal { get; set; }
        public string TipoAnimal { get; set; }
        public string NombreVeterinario { get; set; }
        public string NombrePropietario { get; set; }
        
        public bool EsActivo => Estado == "Activo";
        public bool EsCompletado => Estado == "Completado";
    }
}