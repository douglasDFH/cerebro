using System;

namespace CapaDatos.Entidades
{
    public class Persona
    {
        public int IdPersona { get; set; }
        public string Email { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string TipoPersona { get; set; }
        public DateTime FechaCreacion { get; set; }
        public bool Activo { get; set; }
    }

    public class PersonaFisica : Persona
    {
        public string DNI { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        
        public string NombreCompleto => $"{Nombre} {Apellido}";
    }

    public class PersonaJuridica : Persona
    {
        public string CIF { get; set; }
        public string RazonSocial { get; set; }
        public DateTime? FechaConstitucion { get; set; }
    }
}