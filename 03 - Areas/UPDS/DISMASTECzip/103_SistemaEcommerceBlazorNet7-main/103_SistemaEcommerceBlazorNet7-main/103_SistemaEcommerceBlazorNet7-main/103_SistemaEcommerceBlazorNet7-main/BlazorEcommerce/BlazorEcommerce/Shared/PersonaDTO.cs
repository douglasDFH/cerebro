using System;
using System.ComponentModel.DataAnnotations;

namespace BlazorEcommerce.Shared
{
    public class PersonaDTO
    {
        public int IdPersona { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        public string Nombre { get; set; } = string.Empty;

        [Required(ErrorMessage = "El apellido es obligatorio")]
        public string ApellidoPaterno { get; set; } = string.Empty;

        public string? ApellidoMaterno { get; set; }

        public string NombreCompleto { get; set; } = string.Empty;

        [Required(ErrorMessage = "El tipo de documento es obligatorio")]
        public string TipoDocumento { get; set; } = string.Empty;

        [Required(ErrorMessage = "El número de documento es obligatorio")]
        public string NumeroDocumento { get; set; } = string.Empty;

        [Required(ErrorMessage = "La fecha de nacimiento es obligatoria")]
        public DateTime FechaNacimiento { get; set; }

        [Required(ErrorMessage = "El teléfono es obligatorio")]
        public string Telefono { get; set; } = string.Empty;

        [Required(ErrorMessage = "El correo electrónico es obligatorio")]
        [EmailAddress(ErrorMessage = "Ingrese un correo válido")]
        public string Correo { get; set; } = string.Empty;

        [Required(ErrorMessage = "La contraseña es obligatoria")]
        [MinLength(6, ErrorMessage = "La contraseña debe tener al menos 6 caracteres")]
        public string Clave { get; set; } = string.Empty;

        [Required(ErrorMessage = "Debe confirmar la contraseña")]
        [Compare(nameof(Clave), ErrorMessage = "Las contraseñas no coinciden")]
        public string ConfirmarClave { get; set; } = string.Empty;

        public string Rol { get; set; } = "Cliente";

        public string? Imagen { get; set; }

        // Campos de dirección
        [Required(ErrorMessage = "La dirección es obligatoria")]
        public string Direccion { get; set; } = string.Empty;

        [Required(ErrorMessage = "La ciudad es obligatoria")]
        public string Ciudad { get; set; } = string.Empty;

        public string? CodigoPostal { get; set; }

        [Required(ErrorMessage = "El país es obligatorio")]
        public string Pais { get; set; } = string.Empty;

        public string? Referencias { get; set; }

        // Campos para las coordenadas de la ubicación
        public double Latitud { get; set; }
        public double Longitud { get; set; }

        // Campos para fotos adicionales
       
        public string? FacialImage { get; set; }
        public bool UseFacialRecognition { get; set; } = false;
    }
}