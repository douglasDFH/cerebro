using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Text.RegularExpressions;
using CapaDatos;

namespace CapaNegocio
{
    public class NPersona
    {
        #region Métodos de Inserción

        // Insertar Persona Física
        public static string InsertarPersonaFisica(string ci, string nombre, string apellido, 
            string email = null, string direccion = null, string telefono = null, 
            DateTime? fechaNacimiento = null, char? genero = null)
        {
            // Validaciones de campos obligatorios
            if (string.IsNullOrWhiteSpace(nombre))
                return "El nombre es obligatorio";
            
            if (string.IsNullOrWhiteSpace(apellido))
                return "El apellido es obligatorio";

            // Validaciones
            string validacion = ValidarDatosPersonaFisica(ci, nombre, apellido, email, telefono, genero);
            if (validacion != "OK")
                return validacion;

            DPersona objPersona = new DPersona()
            {
                Ci = ci,
                Nombre = nombre?.Trim(),
                Apellido = apellido?.Trim(),
                Email = email?.Trim(),
                Direccion = direccion?.Trim(),
                Telefono = telefono?.Trim(),
                FechaNacimiento = fechaNacimiento,
                Genero = genero
            };

            return objPersona.InsertarPersonaFisica(objPersona);
        }

        // Insertar Persona Jurídica
        public static string InsertarPersonaJuridica(string razonSocial, string nit = null, 
            string encargadoNombre = null, string encargadoCargo = null, string email = null, 
            string direccion = null, string telefono = null)
        {
            // Validaciones de campos obligatorios
            if (string.IsNullOrWhiteSpace(razonSocial))
                return "La razón social es obligatoria";

            // Validaciones
            string validacion = ValidarDatosPersonaJuridica(razonSocial, nit, email, telefono);
            if (validacion != "OK")
                return validacion;

            DPersona objPersona = new DPersona()
            {
                RazonSocial = razonSocial?.Trim(),
                Nit = nit?.Trim(),
                EncargadoNombre = encargadoNombre?.Trim(),
                EncargadoCargo = encargadoCargo?.Trim(),
                Email = email?.Trim(),
                Direccion = direccion?.Trim(),
                Telefono = telefono?.Trim()
            };

            return objPersona.InsertarPersonaJuridica(objPersona);
        }

        #endregion

        #region Métodos de Edición

        // Editar Persona Física
        public static string EditarPersonaFisica(int id, string ci, string nombre, string apellido,
            string email = null, string direccion = null, string telefono = null,
            DateTime? fechaNacimiento = null, char? genero = null)
        {
            if (id <= 0)
                return "ID de persona no válido";

            // Validar campos obligatorios
            if (string.IsNullOrWhiteSpace(nombre))
                return "El nombre es obligatorio";
            
            if (string.IsNullOrWhiteSpace(apellido))
                return "El apellido es obligatorio";

            // Validaciones
            string validacion = ValidarDatosPersonaFisica(ci, nombre, apellido, email, telefono, genero, id);
            if (validacion != "OK")
                return validacion;

            DPersona objPersona = new DPersona()
            {
                Id = id,
                Tipo = "Física",
                Ci = ci,
                Nombre = nombre?.Trim(),
                Apellido = apellido?.Trim(),
                Email = email?.Trim(),
                Direccion = direccion?.Trim(),
                Telefono = telefono?.Trim(),
                FechaNacimiento = fechaNacimiento,
                Genero = genero
            };

            return objPersona.Editar(objPersona);
        }

        // Editar Persona Jurídica
        public static string EditarPersonaJuridica(int id, string razonSocial, string nit = null,
            string encargadoNombre = null, string encargadoCargo = null, string email = null,
            string direccion = null, string telefono = null)
        {
            if (id <= 0)
                return "ID de persona no válido";

            // Validar campo obligatorio
            if (string.IsNullOrWhiteSpace(razonSocial))
                return "La razón social es obligatoria";

            // Validaciones
            string validacion = ValidarDatosPersonaJuridica(razonSocial, nit, email, telefono, id);
            if (validacion != "OK")
                return validacion;

            DPersona objPersona = new DPersona()
            {
                Id = id,
                Tipo = "Jurídica",
                RazonSocial = razonSocial?.Trim(),
                Nit = nit?.Trim(),
                EncargadoNombre = encargadoNombre?.Trim(),
                EncargadoCargo = encargadoCargo?.Trim(),
                Email = email?.Trim(),
                Direccion = direccion?.Trim(),
                Telefono = telefono?.Trim()
            };

            return objPersona.Editar(objPersona);
        }

        // Método genérico de edición
        public static string Editar(int id, string tipo, string ci = null, string nombre = null, 
            string apellido = null, string razonSocial = null, string nit = null,
            string encargadoNombre = null, string encargadoCargo = null, string email = null,
            string direccion = null, string telefono = null, DateTime? fechaNacimiento = null,
            char? genero = null)
        {
            if (tipo == "Física")
            {
                return EditarPersonaFisica(id, ci, nombre, apellido, email, direccion, telefono, fechaNacimiento, genero);
            }
            else if (tipo == "Jurídica")
            {
                return EditarPersonaJuridica(id, razonSocial, nit, encargadoNombre, encargadoCargo, email, direccion, telefono);
            }
            else
            {
                return "Tipo de persona no válido";
            }
        }

        #endregion

        #region Cambiar Tipo de Persona

        // Cambiar de Persona Física a Jurídica
        public static string CambiarTipoPersona(int id, string nuevoTipo, string ci = null, 
            string nombre = null, string apellido = null, string razonSocial = null, 
            string nit = null, string encargadoNombre = null, string encargadoCargo = null,
            string email = null, string direccion = null, string telefono = null,
            DateTime? fechaNacimiento = null, char? genero = null)
        {
            if (id <= 0)
                return "ID de persona no válido";

            if (nuevoTipo != "Física" && nuevoTipo != "Jurídica")
                return "Tipo de persona no válido. Debe ser 'Física' o 'Jurídica'";

            DPersona objPersona = new DPersona()
            {
                Id = id,
                Tipo = nuevoTipo,
                Email = email?.Trim(),
                Direccion = direccion?.Trim(),
                Telefono = telefono?.Trim()
            };

            // Validar y asignar campos específicos según el nuevo tipo
            if (nuevoTipo == "Física")
            {
                string validacion = ValidarDatosPersonaFisica(ci, nombre, apellido, email, telefono, genero);
                if (validacion != "OK")
                    return validacion;

                objPersona.Ci = ci;
                objPersona.Nombre = nombre?.Trim() ?? "";
                objPersona.Apellido = apellido?.Trim() ?? "";
                objPersona.FechaNacimiento = fechaNacimiento;
                objPersona.Genero = genero;
            }
            else if (nuevoTipo == "Jurídica")
            {
                string validacion = ValidarDatosPersonaJuridica(razonSocial, nit, email, telefono);
                if (validacion != "OK")
                    return validacion;

                objPersona.RazonSocial = razonSocial?.Trim() ?? "";
                objPersona.Nit = nit?.Trim();
                objPersona.EncargadoNombre = encargadoNombre?.Trim();
                objPersona.EncargadoCargo = encargadoCargo?.Trim();
            }

            return objPersona.CambiarTipoPersona(objPersona);
        }

        #endregion

        #region Eliminar

        public static string Eliminar(int idPersona)
        {
            if (idPersona <= 0)
                return "ID de persona no válido";

            DPersona objPersona = new DPersona()
            {
                Id = idPersona
            };

            return objPersona.Eliminar(objPersona);
        }

        #endregion

        #region Métodos de Consulta

        public static DataTable Mostrar()
        {
            return new DPersona().Mostrar();
        }

        public static DataTable BuscarTexto(string textoBuscar)
        {
            DPersona objPersona = new DPersona()
            {
                TextoBuscar = textoBuscar ?? ""
            };

            return objPersona.BuscarTexto(objPersona);
        }

        public static DataTable BuscarPorTipo(string tipoPersona)
        {
            if (tipoPersona != "Física" && tipoPersona != "Jurídica")
                return new DataTable("Persona"); // Retorna tabla vacía

            return new DPersona().BuscarPorTipo(tipoPersona);
        }

        public static DataTable BuscarPorNombre(string nombre)
        {
            if (string.IsNullOrWhiteSpace(nombre))
                return new DataTable("Persona"); // Retorna tabla vacía

            return new DPersona().BuscarPorNombre(nombre);
        }

        #endregion

        #region Métodos de Validación

        // Validar CI (Cédula de Identidad)
        public static bool ValidarCi(string ci)
        {
            if (string.IsNullOrEmpty(ci))
                return false;

            // Remover espacios y guiones
            ci = ci.Replace(" ", "").Replace("-", "");

            // Verificar que solo contenga números
            if (!Regex.IsMatch(ci, @"^\d+$"))
                return false;

            // Verificar longitud (entre 7 y 15 dígitos)
            if (ci.Length < 7 || ci.Length > 15)
                return false;

            return true;
        }

        // Validar Email
        public static bool ValidarEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
                return true; // Email es opcional

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

        // Validar NIT
        public static bool ValidarNit(string nit)
        {
            if (string.IsNullOrEmpty(nit))
                return true; // NIT es opcional

            // Remover espacios y guiones
            nit = nit.Replace(" ", "").Replace("-", "");

            // Verificar que solo contenga números
            if (!Regex.IsMatch(nit, @"^\d+$"))
                return false;

            // Verificar longitud (entre 8 y 15 dígitos)
            if (nit.Length < 8 || nit.Length > 15)
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

        // Validar Razón Social
        public static bool ValidarRazonSocial(string razonSocial)
        {
            if (string.IsNullOrWhiteSpace(razonSocial))
                return false;

            // Verificar longitud
            if (razonSocial.Trim().Length < 2 || razonSocial.Trim().Length > 255)
                return false;

            return true;
        }

        // Validar Género
        public static bool ValidarGenero(char? genero)
        {
            if (!genero.HasValue)
                return true; // Género es opcional

            return genero.Value == 'M' || genero.Value == 'F' || genero.Value == 'm' || genero.Value == 'f';
        }

        // Validar Fecha de Nacimiento
        public static bool ValidarFechaNacimiento(DateTime? fechaNacimiento)
        {
            if (!fechaNacimiento.HasValue)
                return true; // Fecha es opcional

            // No puede ser mayor a la fecha actual
            if (fechaNacimiento.Value > DateTime.Now)
                return false;

            // No puede ser menor a 150 años atrás
            if (fechaNacimiento.Value < DateTime.Now.AddYears(-150))
                return false;

            return true;
        }

        // Validación completa para Persona Física
        private static string ValidarDatosPersonaFisica(string ci, string nombre, string apellido, 
            string email, string telefono, char? genero, int? idPersona = null)
        {
            // Validar campos obligatorios
            if (string.IsNullOrWhiteSpace(nombre))
                return "El nombre es obligatorio";

            if (string.IsNullOrWhiteSpace(apellido))
                return "El apellido es obligatorio";

            // Validar formato de campos
            if (!ValidarNombre(nombre))
                return "El nombre no tiene un formato válido";

            if (!ValidarNombre(apellido))
                return "El apellido no tiene un formato válido";

            if (!string.IsNullOrEmpty(ci) && !ValidarCi(ci))
                return "La cédula de identidad no tiene un formato válido";

            if (!ValidarEmail(email))
                return "El email no tiene un formato válido";

            if (!ValidarTelefono(telefono))
                return "El teléfono no tiene un formato válido";

            if (!ValidarGenero(genero))
                return "El género debe ser 'M' o 'F'";

            // Validar unicidad de CI
            if (!string.IsNullOrEmpty(ci) && !new DPersona().ValidarCIUnico(ci, idPersona))
                return "Ya existe una persona con esta cédula de identidad";

            // Validar unicidad de email
            if (!string.IsNullOrEmpty(email) && !new DPersona().ValidarEmailUnico(email, idPersona))
                return "Ya existe una persona con este email";

            return "OK";
        }

        // Validación completa para Persona Jurídica
        private static string ValidarDatosPersonaJuridica(string razonSocial, string nit, 
            string email, string telefono, int? idPersona = null)
        {
            // Validar campos obligatorios
            if (string.IsNullOrWhiteSpace(razonSocial))
                return "La razón social es obligatoria";

            // Validar formato de campos
            if (!ValidarRazonSocial(razonSocial))
                return "La razón social no tiene un formato válido";

            if (!string.IsNullOrEmpty(nit) && !ValidarNit(nit))
                return "El NIT no tiene un formato válido";

            if (!ValidarEmail(email))
                return "El email no tiene un formato válido";

            if (!ValidarTelefono(telefono))
                return "El teléfono no tiene un formato válido";

            // Validar unicidad de NIT
            if (!string.IsNullOrEmpty(nit) && !new DPersona().ValidarNITUnico(nit, idPersona))
                return "Ya existe una persona jurídica con este NIT";

            // Validar unicidad de email
            if (!string.IsNullOrEmpty(email) && !new DPersona().ValidarEmailUnico(email, idPersona))
                return "Ya existe una persona con este email";

            return "OK";
        }

        #endregion

        #region Métodos Auxiliares

        // Verificar si existe una persona por CI
        public static bool ExistePersonaPorCi(string ci, int? idPersona = null)
        {
            if (string.IsNullOrEmpty(ci))
                return false;

            return !new DPersona().ValidarCIUnico(ci, idPersona);
        }

        // Verificar si existe una persona por NIT
        public static bool ExistePersonaPorNit(string nit, int? idPersona = null)
        {
            if (string.IsNullOrEmpty(nit))
                return false;

            return !new DPersona().ValidarNITUnico(nit, idPersona);
        }

        // Verificar si existe una persona por Email
        public static bool ExistePersonaPorEmail(string email, int? idPersona = null)
        {
            if (string.IsNullOrEmpty(email))
                return false;

            return !new DPersona().ValidarEmailUnico(email, idPersona);
        }

        // Obtener persona por ID
        public static DataTable ObtenerPorId(int id)
        {
            return new DPersona().ObtenerPorId(id);
        }

        // Obtener nombre completo de una persona
        public static string ObtenerNombreCompleto(DataRow persona)
        {
            if (persona == null)
                return "";

            string tipo = persona["tipo"]?.ToString() ?? "";
            
            if (tipo == "Física")
            {
                string nombre = persona["nombre"]?.ToString() ?? "";
                string apellido = persona["apellido"]?.ToString() ?? "";
                return $"{nombre} {apellido}".Trim();
            }
            else if (tipo == "Jurídica")
            {
                return persona["razon_social"]?.ToString() ?? "";
            }

            return "";
        }

        // Formatear datos para mostrar
        public static string FormatearPersonaParaMostrar(DataRow persona)
        {
            if (persona == null)
                return "";

            string nombreCompleto = ObtenerNombreCompleto(persona);
            string tipo = persona["tipo"]?.ToString() ?? "";
            string documento = "";

            if (tipo == "Física")
            {
                documento = persona["ci"]?.ToString() ?? "";
                if (!string.IsNullOrEmpty(documento))
                    documento = $"CI: {documento}";
            }
            else if (tipo == "Jurídica")
            {
                documento = persona["nit"]?.ToString() ?? "";
                if (!string.IsNullOrEmpty(documento))
                    documento = $"NIT: {documento}";
            }

            string resultado = nombreCompleto;
            if (!string.IsNullOrEmpty(documento))
                resultado += $" ({documento})";

            return resultado;
        }

        #endregion
    }
}
