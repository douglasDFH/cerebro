using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorEcommerce.Shared
{
 public class FacialLoginDTO
    {
     
        public string? Clave { get; set; }
        public string? Imagen { get; set; }
        public string? NombreCompleto { get; set; }
        public string? Rol { get; set; }
        public bool EsCorrecto { get; set; }
        public string? Mensaje { get; set; }
        [Required(ErrorMessage = "El correo electrónico es obligatorio")]
        [EmailAddress(ErrorMessage = "Ingrese un correo válido")]
        public string Correo { get; set; } = string.Empty;

        [Required(ErrorMessage = "La imagen facial es obligatoria")]
        public string FacialImage { get; set; } = string.Empty;
    }
}
