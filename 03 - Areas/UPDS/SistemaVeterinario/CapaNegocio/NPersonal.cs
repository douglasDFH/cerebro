using System;
using System.Data;
using System.Text.RegularExpressions;
using CapaDatos;

namespace CapaNegocio
{
    public class NPersonal
    {
        #region Métodos de Inserción

        // Insertar Personal Auxiliar
        public static string InsertarPersonalAuxiliar(string nombre, string apellido, string email, 
            string usuario, string contrasena, string telefono = null, string direccion = null, 
            DateTime? fechaContratacion = null, decimal? salario = null, string rol = "Usuario",
            string area = null, string turno = "Mañana", string nivel = "Básico")
        {
            // Validaciones de campos obligatorios
            if (string.IsNullOrWhiteSpace(nombre))
                return "El nombre es obligatorio";
            
            if (string.IsNullOrWhiteSpace(apellido))
                return "El apellido es obligatorio";

            if (string.IsNullOrWhiteSpace(email))
                return "El email es obligatorio";

            if (string.IsNullOrWhiteSpace(usuario))
                return "El usuario es obligatorio";

            if (string.IsNullOrWhiteSpace(contrasena))
                return "La contraseña es obligatoria";

            // Validaciones
            string validacion = ValidarDatosPersonalAuxiliar(nombre, apellido, email, usuario, contrasena, 
                telefono, salario, turno, nivel);
            if (validacion != "OK")
                return validacion;

            DPersonal objPersonal = new DPersonal()
            {
                Nombre = nombre.Trim(),
                Apellido = apellido.Trim(),
                Email = email.Trim(),
                Usuario = usuario.Trim(),
                Contrasena = contrasena, // Debería estar encriptada
                Telefono = telefono?.Trim(),
                Direccion = direccion?.Trim(),
                FechaContratacion = fechaContratacion ?? DateTime.Now,
                Salario = salario,
                Rol = rol,
                Area = area?.Trim(),
                Turno = turno,
                Nivel = nivel,
                TipoPersonal = "Auxiliar"
            };

            return objPersonal.InsertarPersonalAuxiliar(objPersonal);
        }

        // Insertar Personal Veterinario
        public static string InsertarPersonalVeterinario(string nombre, string apellido, string email, 
            string usuario, string contrasena, string telefono = null, string direccion = null,
            DateTime? fechaContratacion = null, decimal? salario = null, string rol = "Usuario",
            string numLicencia = null, string especialidad = null, string universidad = null,
            int? aniosExperiencia = null)
        {
            // Validaciones de campos obligatorios
            if (string.IsNullOrWhiteSpace(nombre))
                return "El nombre es obligatorio";
            
            if (string.IsNullOrWhiteSpace(apellido))
                return "El apellido es obligatorio";

            if (string.IsNullOrWhiteSpace(email))
                return "El email es obligatorio";

            if (string.IsNullOrWhiteSpace(usuario))
                return "El usuario es obligatorio";

            if (string.IsNullOrWhiteSpace(contrasena))
                return "La contraseña es obligatoria";

            // Validaciones
            string validacion = ValidarDatosPersonalVeterinario(nombre, apellido, email, usuario, contrasena, 
                telefono, salario, numLicencia, aniosExperiencia);
            if (validacion != "OK")
                return validacion;

            DPersonal objPersonal = new DPersonal()
            {
                Nombre = nombre.Trim(),
                Apellido = apellido.Trim(),
                Email = email.Trim(),
                Usuario = usuario.Trim(),
                Contrasena = contrasena, // Debería estar encriptada
                Telefono = telefono?.Trim(),
                Direccion = direccion?.Trim(),
                FechaContratacion = fechaContratacion ?? DateTime.Now,
                Salario = salario,
                Rol = rol,
                NumLicencia = numLicencia?.Trim(),
                Especialidad = especialidad?.Trim(),
                Universidad = universidad?.Trim(),
                AniosExperiencia = aniosExperiencia ?? 0,
                TipoPersonal = "Veterinario"
            };

            return objPersonal.InsertarPersonalVeterinario(objPersonal);
        }

        #endregion

        #region Métodos de Edición

        // Editar Personal Auxiliar
        public static string EditarPersonalAuxiliar(int id, string nombre, string apellido, string email, 
            string usuario, string telefono = null, string direccion = null, decimal? salario = null,
            string rol = "Usuario", string area = null, string turno = "Mañana", string nivel = "Básico")
        {
            if (id <= 0)
                return "ID de personal no válido";

            // Validar campos obligatorios
            if (string.IsNullOrWhiteSpace(nombre))
                return "El nombre es obligatorio";
            
            if (string.IsNullOrWhiteSpace(apellido))
                return "El apellido es obligatorio";

            if (string.IsNullOrWhiteSpace(email))
                return "El email es obligatorio";

            if (string.IsNullOrWhiteSpace(usuario))
                return "El usuario es obligatorio";

            // Validaciones
            string validacion = ValidarDatosPersonalAuxiliar(nombre, apellido, email, usuario, "", 
                telefono, salario, turno, nivel, id);
            if (validacion != "OK")
                return validacion;

            DPersonal objPersonal = new DPersonal()
            {
                Id = id,
                Nombre = nombre.Trim(),
                Apellido = apellido.Trim(),
                Email = email.Trim(),
                Usuario = usuario.Trim(),
                Telefono = telefono?.Trim(),
                Direccion = direccion?.Trim(),
                Salario = salario,
                Rol = rol,
                Area = area?.Trim(),
                Turno = turno,
                Nivel = nivel,
                TipoPersonal = "Auxiliar"
            };

            return objPersonal.Editar(objPersonal);
        }

        // Editar Personal Veterinario
        public static string EditarPersonalVeterinario(int id, string nombre, string apellido, string email, 
            string usuario, string telefono = null, string direccion = null, decimal? salario = null,
            string rol = "Usuario", string numLicencia = null, string especialidad = null, 
            string universidad = null, int? aniosExperiencia = null)
        {
            if (id <= 0)
                return "ID de personal no válido";

            // Validar campos obligatorios
            if (string.IsNullOrWhiteSpace(nombre))
                return "El nombre es obligatorio";
            
            if (string.IsNullOrWhiteSpace(apellido))
                return "El apellido es obligatorio";

            if (string.IsNullOrWhiteSpace(email))
                return "El email es obligatorio";

            if (string.IsNullOrWhiteSpace(usuario))
                return "El usuario es obligatorio";

            // Validaciones
            string validacion = ValidarDatosPersonalVeterinario(nombre, apellido, email, usuario, "", 
                telefono, salario, numLicencia, aniosExperiencia, id);
            if (validacion != "OK")
                return validacion;

            DPersonal objPersonal = new DPersonal()
            {
                Id = id,
                Nombre = nombre.Trim(),
                Apellido = apellido.Trim(),
                Email = email.Trim(),
                Usuario = usuario.Trim(),
                Telefono = telefono?.Trim(),
                Direccion = direccion?.Trim(),
                Salario = salario,
                Rol = rol,
                NumLicencia = numLicencia?.Trim(),
                Especialidad = especialidad?.Trim(),
                Universidad = universidad?.Trim(),
                AniosExperiencia = aniosExperiencia ?? 0,
                TipoPersonal = "Veterinario"
            };

            return objPersonal.Editar(objPersonal);
        }

        #endregion

        #region Eliminar

        public static string Eliminar(int idPersonal)
        {
            if (idPersonal <= 0)
                return "ID de personal no válido";

            DPersonal objPersonal = new DPersonal()
            {
                Id = idPersonal
            };

            return objPersonal.Eliminar(objPersonal);
        }

        #endregion

        #region Métodos de Consulta

        public static DataTable Mostrar()
        {
            return new DPersonal().Mostrar();
        }

        public static DataTable BuscarTexto(string textoBuscar)
        {
            DPersonal objPersonal = new DPersonal()
            {
                TextoBuscar = textoBuscar ?? ""
            };

            return objPersonal.BuscarTexto(objPersonal);
        }

        public static DataTable BuscarPorTipo(string tipoPersonal)
        {
            if (tipoPersonal != "Veterinario" && tipoPersonal != "Auxiliar")
                return new DataTable("Personal"); // Retorna tabla vacía

            return new DPersonal().BuscarPorTipo(tipoPersonal);
        }

        public static DataTable ObtenerPorId(int id)
        {
            return new DPersonal().ObtenerPorId(id);
        }

        #endregion

        #region Métodos de Validación

        // Validar Email
        public static bool ValidarEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
                return false; // Email es obligatorio para personal

            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email && Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
            }
            catch
            {
                return false;
            }
        }

        // Validar Teléfono
        public static bool ValidarTelefono(string telefono)
        {
            if (string.IsNullOrEmpty(telefono))
                return true; // Teléfono es opcional

            // Remover espacios, guiones y paréntesis
            telefono = telefono.Replace(" ", "").Replace("-", "").Replace("(", "").Replace(")", "").Replace("+", "");

            // Verificar que solo contenga números
            if (!Regex.IsMatch(telefono, @"^\d+$"))
                return false;

            // Verificar longitud (entre 7 y 15 dígitos)
            if (telefono.Length < 7 || telefono.Length > 15)
                return false;

            return true;
        }

        // Validar Nombre
        public static bool ValidarNombre(string nombre)
        {
            if (string.IsNullOrWhiteSpace(nombre))
                return false;

            // Verificar que solo contenga letras, espacios y algunos caracteres especiales
            if (!Regex.IsMatch(nombre, @"^[a-zA-ZáéíóúÁÉÍÓÚñÑüÜ\s'.-]+$"))
                return false;

            // Verificar longitud
            if (nombre.Trim().Length < 2 || nombre.Trim().Length > 100)
                return false;

            return true;
        }

        // Validar Usuario
        public static bool ValidarUsuario(string usuario)
        {
            if (string.IsNullOrWhiteSpace(usuario))
                return false;

            // Verificar longitud
            if (usuario.Trim().Length < 3 || usuario.Trim().Length > 50)
                return false;

            // Verificar formato (solo letras, números y algunos caracteres especiales)
            if (!Regex.IsMatch(usuario, @"^[a-zA-Z0-9._-]+$"))
                return false;

            return true;
        }

        // Validar Contraseña
        public static bool ValidarContrasena(string contrasena)
        {
            if (string.IsNullOrWhiteSpace(contrasena))
                return false;

            // Verificar longitud mínima
            if (contrasena.Length < 6)
                return false;

            return true;
        }

        // Validar Salario
        public static bool ValidarSalario(decimal? salario)
        {
            if (!salario.HasValue)
                return true; // Salario es opcional

            return salario.Value >= 0;
        }

        // Validar Años de Experiencia
        public static bool ValidarAniosExperiencia(int? anios)
        {
            if (!anios.HasValue)
                return true; // Es opcional

            return anios.Value >= 0 && anios.Value <= 50;
        }

        // Validar Turno
        public static bool ValidarTurno(string turno)
        {
            if (string.IsNullOrWhiteSpace(turno))
                return false;

            string[] turnosValidos = { "Mañana", "Tarde", "Noche" };
            return Array.Exists(turnosValidos, t => t.Equals(turno, StringComparison.OrdinalIgnoreCase));
        }

        // Validar Nivel
        public static bool ValidarNivel(string nivel)
        {
            if (string.IsNullOrWhiteSpace(nivel))
                return false;

            string[] nivelesValidos = { "Básico", "Intermedio", "Avanzado" };
            return Array.Exists(nivelesValidos, n => n.Equals(nivel, StringComparison.OrdinalIgnoreCase));
        }

        // Validar Rol
        public static bool ValidarRol(string rol)
        {
            if (string.IsNullOrWhiteSpace(rol))
                return false;

            string[] rolesValidos = { "Usuario", "Administrador", "Supervisor" };
            return Array.Exists(rolesValidos, r => r.Equals(rol, StringComparison.OrdinalIgnoreCase));
        }

        // Validación completa para Personal Auxiliar
        private static string ValidarDatosPersonalAuxiliar(string nombre, string apellido, string email, 
            string usuario, string contrasena, string telefono, decimal? salario, string turno, 
            string nivel, int? idPersonal = null)
        {
            // Validar formato de campos
            if (!ValidarNombre(nombre))
                return "El nombre no tiene un formato válido";

            if (!ValidarNombre(apellido))
                return "El apellido no tiene un formato válido";

            if (!ValidarEmail(email))
                return "El email no tiene un formato válido";

            if (!ValidarUsuario(usuario))
                return "El usuario no tiene un formato válido";

            if (!string.IsNullOrEmpty(contrasena) && !ValidarContrasena(contrasena))
                return "La contraseña debe tener al menos 6 caracteres";

            if (!ValidarTelefono(telefono))
                return "El teléfono no tiene un formato válido";

            if (!ValidarSalario(salario))
                return "El salario debe ser mayor o igual a 0";

            if (!ValidarTurno(turno))
                return "El turno debe ser Mañana, Tarde o Noche";

            if (!ValidarNivel(nivel))
                return "El nivel debe ser Básico, Intermedio o Avanzado";

            // Validar unicidad de usuario
            if (!new DPersonal().ValidarUsuarioUnico(usuario, idPersonal))
                return "Ya existe un usuario con este nombre de usuario";

            // Validar unicidad de email
            if (!new DPersonal().ValidarEmailUnico(email, idPersonal))
                return "Ya existe una persona con este email";

            return "OK";
        }

        // Validación completa para Personal Veterinario
        private static string ValidarDatosPersonalVeterinario(string nombre, string apellido, string email, 
            string usuario, string contrasena, string telefono, decimal? salario, string numLicencia,
            int? aniosExperiencia, int? idPersonal = null)
        {
            // Validar formato de campos
            if (!ValidarNombre(nombre))
                return "El nombre no tiene un formato válido";

            if (!ValidarNombre(apellido))
                return "El apellido no tiene un formato válido";

            if (!ValidarEmail(email))
                return "El email no tiene un formato válido";

            if (!ValidarUsuario(usuario))
                return "El usuario no tiene un formato válido";

            if (!string.IsNullOrEmpty(contrasena) && !ValidarContrasena(contrasena))
                return "La contraseña debe tener al menos 6 caracteres";

            if (!ValidarTelefono(telefono))
                return "El teléfono no tiene un formato válido";

            if (!ValidarSalario(salario))
                return "El salario debe ser mayor o igual a 0";

            if (!ValidarAniosExperiencia(aniosExperiencia))
                return "Los años de experiencia deben estar entre 0 y 50";

            // Validar unicidad de usuario
            if (!new DPersonal().ValidarUsuarioUnico(usuario, idPersonal))
                return "Ya existe un usuario con este nombre de usuario";

            // Validar unicidad de email
            if (!new DPersonal().ValidarEmailUnico(email, idPersonal))
                return "Ya existe una persona con este email";

            return "OK";
        }

        #endregion

        #region Métodos Auxiliares

        // Verificar si existe un usuario
        public static bool ExisteUsuario(string usuario, int? idPersonal = null)
        {
            if (string.IsNullOrEmpty(usuario))
                return false;

            return !new DPersonal().ValidarUsuarioUnico(usuario, idPersonal);
        }

        // Verificar si existe un email
        public static bool ExisteEmail(string email, int? idPersonal = null)
        {
            if (string.IsNullOrEmpty(email))
                return false;

            return !new DPersonal().ValidarEmailUnico(email, idPersonal);
        }

        // Obtener nombre completo
        public static string ObtenerNombreCompleto(DataRow personal)
        {
            if (personal == null)
                return "";

            string nombre = personal["nombre"]?.ToString() ?? "";
            string apellido = personal["apellido"]?.ToString() ?? "";
            return $"{nombre} {apellido}".Trim();
        }

        // Formatear personal para mostrar
        public static string FormatearPersonalParaMostrar(DataRow personal)
        {
            if (personal == null)
                return "";

            string nombreCompleto = ObtenerNombreCompleto(personal);
            string usuario = personal["usuario"]?.ToString() ?? "";
            string rol = personal["rol"]?.ToString() ?? "";

            string resultado = nombreCompleto;
            if (!string.IsNullOrEmpty(usuario))
                resultado += $" ({usuario})";
            if (!string.IsNullOrEmpty(rol))
                resultado += $" - {rol}";

            return resultado;
        }

        #endregion
    }
}