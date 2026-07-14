using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDatos;

namespace CapaNegocio
{
    public class NPersonas
    {
        #region Métodos para Persona Física

        public static string InsertarPersonaFisica(string ci, string nombre, string apellido, 
            string email, string direccion = "", string telefono = "", 
            DateTime? fechaNacimiento = null, string genero = "")
        {
            DPersonas objPersona = new DPersonas()
            {
                Tipo = "Física",
                Ci = ci,
                Nombre = nombre,
                Apellido = apellido,
                Email = email,
                Direccion = direccion,
                Telefono = telefono,
                FechaNacimiento = fechaNacimiento,
                Genero = genero
            };
            return objPersona.InsertarPersonaFisica(objPersona);
        }

        public static string EditarPersonaFisica(int id, string ci, string nombre, string apellido,
            string email, string direccion = "", string telefono = "",
            DateTime? fechaNacimiento = null, string genero = "")
        {
            DPersonas objPersona = new DPersonas()
            {
                Id = id,
                Tipo = "Física",
                Ci = ci,
                Nombre = nombre,
                Apellido = apellido,
                Email = email,
                Direccion = direccion,
                Telefono = telefono,
                FechaNacimiento = fechaNacimiento,
                Genero = genero
            };
            return objPersona.EditarPersonaFisica(objPersona);
        }

        #endregion

        #region Métodos para Persona Jurídica

        public static string InsertarPersonaJuridica(string razonSocial, string nit, string email,
            string direccion = "", string telefono = "", string encargadoNombre = "", 
            string encargadoCargo = "")
        {
            DPersonas objPersona = new DPersonas()
            {
                Tipo = "Jurídica",
                RazonSocial = razonSocial,
                Nit = nit,
                Email = email,
                Direccion = direccion,
                Telefono = telefono,
                EncargadoNombre = encargadoNombre,
                EncargadoCargo = encargadoCargo
            };
            return objPersona.InsertarPersonaJuridica(objPersona);
        }

        public static string EditarPersonaJuridica(int id, string razonSocial, string nit, string email,
            string direccion = "", string telefono = "", string encargadoNombre = "",
            string encargadoCargo = "")
        {
            DPersonas objPersona = new DPersonas()
            {
                Id = id,
                Tipo = "Jurídica",
                RazonSocial = razonSocial,
                Nit = nit,
                Email = email,
                Direccion = direccion,
                Telefono = telefono,
                EncargadoNombre = encargadoNombre,
                EncargadoCargo = encargadoCargo
            };
            return objPersona.EditarPersonaJuridica(objPersona);
        }

        #endregion

        #region Métodos Generales

        public static string Insertar(string tipo, object parametros)
        {
            try
            {
                if (tipo == "Física")
                {
                    var param = parametros as dynamic;
                    return InsertarPersonaFisica(param.ci, param.nombre, param.apellido, 
                        param.email, param.direccion, param.telefono, param.fechaNacimiento, param.genero);
                }
                else if (tipo == "Jurídica")
                {
                    var param = parametros as dynamic;
                    return InsertarPersonaJuridica(param.razonSocial, param.nit, param.email,
                        param.direccion, param.telefono, param.encargadoNombre, param.encargadoCargo);
                }
                else
                {
                    return "Error: Tipo de persona no válido";
                }
            }
            catch (Exception ex)
            {
                return $"Error: {ex.Message}";
            }
        }

        public static string Editar(int id, string tipo, object parametros)
        {
            try
            {
                if (tipo == "Física")
                {
                    var param = parametros as dynamic;
                    return EditarPersonaFisica(id, param.ci, param.nombre, param.apellido,
                        param.email, param.direccion, param.telefono, param.fechaNacimiento, param.genero);
                }
                else if (tipo == "Jurídica")
                {
                    var param = parametros as dynamic;
                    return EditarPersonaJuridica(id, param.razonSocial, param.nit, param.email,
                        param.direccion, param.telefono, param.encargadoNombre, param.encargadoCargo);
                }
                else
                {
                    return "Error: Tipo de persona no válido";
                }
            }
            catch (Exception ex)
            {
                return $"Error: {ex.Message}";
            }
        }

        public static string Eliminar(int id)
        {
            DPersonas objPersona = new DPersonas()
            {
                Id = id
            };
            return objPersona.Eliminar(objPersona);
        }

        public static DataTable Mostrar()
        {
            return new DPersonas().Mostrar();
        }

        public static DataTable BuscarPorNombre(string textoBuscar)
        {
            DPersonas objPersona = new DPersonas()
            {
                TextoBuscar = textoBuscar
            };
            return objPersona.BuscarPorNombre(objPersona);
        }

        public static DataTable ObtenerPorId(int id)
        {
            DPersonas objPersona = new DPersonas()
            {
                Id = id
            };
            return objPersona.ObtenerPorId(objPersona);
        }

        public static DataTable ObtenerTipos()
        {
            return new DPersonas().ObtenerTipos();
        }

        #endregion

        #region Validaciones de Negocio

        public static bool ValidarPersonaFisica(string ci, string nombre, string apellido, string email)
        {
            // Validaciones básicas
            if (string.IsNullOrWhiteSpace(nombre))
                return false;
            
            if (string.IsNullOrWhiteSpace(apellido))
                return false;
            
            // Validar email si se proporciona
            if (!string.IsNullOrWhiteSpace(email) && !ValidarEmail(email))
                return false;
            
            // Validar CI si se proporciona
            if (!string.IsNullOrWhiteSpace(ci) && ci.Length > 15)
                return false;
                
            return true;
        }

        public static bool ValidarPersonaJuridica(string razonSocial, string email)
        {
            // Validaciones básicas
            if (string.IsNullOrWhiteSpace(razonSocial))
                return false;
            
            // Validar email si se proporciona
            if (!string.IsNullOrWhiteSpace(email) && !ValidarEmail(email))
                return false;
                
            return true;
        }

        public static bool ValidarEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        public static bool ValidarTelefono(string telefono)
        {
            if (string.IsNullOrWhiteSpace(telefono))
                return true; // Teléfono es opcional
            
            // Validar que solo contenga números, espacios, guiones y paréntesis
            return System.Text.RegularExpressions.Regex.IsMatch(telefono, @"^[\d\s\-\(\)]+$");
        }

        public static bool ValidarGenero(string genero)
        {
            if (string.IsNullOrWhiteSpace(genero))
                return true; // Género es opcional
            
            return genero == "M" || genero == "F";
        }

        public static string ValidarDatosPersonaFisica(string ci, string nombre, string apellido, 
            string email, string telefono, string genero)
        {
            var errores = new List<string>();

            if (string.IsNullOrWhiteSpace(nombre))
                errores.Add("El nombre es requerido");
            
            if (string.IsNullOrWhiteSpace(apellido))
                errores.Add("El apellido es requerido");
            
            if (!string.IsNullOrWhiteSpace(email) && !ValidarEmail(email))
                errores.Add("El email no tiene un formato válido");
            
            if (!ValidarTelefono(telefono))
                errores.Add("El teléfono no tiene un formato válido");
            
            if (!ValidarGenero(genero))
                errores.Add("El género debe ser 'M' o 'F'");
            
            if (!string.IsNullOrWhiteSpace(ci) && ci.Length > 15)
                errores.Add("La cédula no puede tener más de 15 caracteres");

            return errores.Any() ? string.Join(", ", errores) : "";
        }

        public static string ValidarDatosPersonaJuridica(string razonSocial, string email, 
            string telefono, string nit)
        {
            var errores = new List<string>();

            if (string.IsNullOrWhiteSpace(razonSocial))
                errores.Add("La razón social es requerida");
            
            if (!string.IsNullOrWhiteSpace(email) && !ValidarEmail(email))
                errores.Add("El email no tiene un formato válido");
            
            if (!ValidarTelefono(telefono))
                errores.Add("El teléfono no tiene un formato válido");
            
            if (!string.IsNullOrWhiteSpace(nit) && nit.Length > 20)
                errores.Add("El NIT no puede tener más de 20 caracteres");

            return errores.Any() ? string.Join(", ", errores) : "";
        }

        public static bool ExistePersona(string ci = "", string nit = "", string email = "")
        {
            try
            {
                string criterioBusqueda = "";
                if (!string.IsNullOrWhiteSpace(ci)) criterioBusqueda = ci;
                else if (!string.IsNullOrWhiteSpace(nit)) criterioBusqueda = nit;
                else if (!string.IsNullOrWhiteSpace(email)) criterioBusqueda = email;
                else return false;

                DataTable resultado = BuscarPorNombre(criterioBusqueda);
                return resultado != null && resultado.Rows.Count > 0;
            }
            catch
            {
                return false;
            }
        }

        #endregion
    }
}