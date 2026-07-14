using System;

namespace CapaDatos.Entidades
{
    public class Usuario
    {
        public int IdUsuario { get; set; }
        public int? IdPersonal { get; set; }
        public string UsuarioLogin { get; set; }
        public string Contrasena { get; set; }
        public string Rol { get; set; }
        public DateTime? UltimoAcceso { get; set; }
        public bool Activo { get; set; }
        public DateTime FechaCreacion { get; set; }
        
        // Propiedades de navegación
        public string NombreCompleto { get; set; }
        public string TipoPersonal { get; set; }
        
        public bool EsAdmin => Rol == "Admin";
        public bool EsVeterinario => Rol == "Veterinario";
        public bool EsAuxiliar => Rol == "Auxiliar";
        public bool EsRecepcionista => Rol == "Recepcionista";
    }
}