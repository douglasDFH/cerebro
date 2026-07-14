using System;
using System.Collections.Generic;

namespace BlazorEcommerce.Server.Modelos
{
    public partial class Persona
    {
        public int IdPersona { get; set; }
        public string? NombreCompleto { get; set; }
        public string? Correo { get; set; }
        public string? Clave { get; set; }
        public string? Rol { get; set; }
        public string? Imagen { get; set; }
        public string? Telefono { get; set; }

        // Nuevos campos para dirección
        public string? Direccion { get; set; }
        public string? Ciudad { get; set; }
        public string? CodigoPostal { get; set; }
        public string? Pais { get; set; }
        public string? Referencias { get; set; }

        // Nuevos campos para fotos adicionales
       
        public DateTime? FechaCreacion { get; set; }
        public virtual ICollection<Venta> Venta { get; set; } = new List<Venta>();
        // Añadir este nuevo campo al modelo existente

        public string? FacialImage { get; set; }       // Imagen para reconocimiento facial
        public bool UseFacialRecognition { get; set; } // Flag para indicar si usa reconocimiento facial
    }
}