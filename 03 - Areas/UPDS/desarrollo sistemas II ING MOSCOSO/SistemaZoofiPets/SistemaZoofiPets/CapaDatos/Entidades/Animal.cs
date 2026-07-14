using System;

namespace CapaDatos.Entidades
{
    public class Animal
    {
        public int IdAnimal { get; set; }
        public int IdPropietario { get; set; }
        public string Tipo { get; set; }
        public string Nombre { get; set; }
        public int? Edad { get; set; }
        public string Raza { get; set; }
        public decimal? Peso { get; set; }
        public string Color { get; set; }
        public string Observaciones { get; set; }
        public DateTime FechaRegistro { get; set; }
        public bool Activo { get; set; }
        
        // Propiedades calculadas/de navegación
        public string NombrePropietario { get; set; }
        public string TelefonoPropietario { get; set; }
        public string EmailPropietario { get; set; }
    }
}