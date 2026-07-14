using System;
using System.Data;
using CapaDatos;

namespace CapaNegocio
{
    public class Nstores
    {
        private Dstores dstores;

        public Nstores()
        {
            dstores = new Dstores();
        }

        // Mostrar todos los registros
        public DataTable Mostrar()
        {
            try
            {
                return dstores.Mostrar();
            }
            catch (Exception ex)
            {
                throw new Exception("Error en capa de negocio - Mostrar: " + ex.Message);
            }
        }

        // Buscar registros
        public DataTable Buscar(string campo, string valor)
        {
            try
            {
                // Validaciones de negocio
                if (string.IsNullOrWhiteSpace(campo))
                    throw new ArgumentException("El campo de búsqueda es requerido");

                if (string.IsNullOrWhiteSpace(valor))
                    throw new ArgumentException("El valor de búsqueda es requerido");

                // Validar campos permitidos
                string[] camposPermitidos = { "store_id", "store_name", "phone", "email", "city", "state" };
                bool campoValido = false;
                foreach (string campoPermitido in camposPermitidos)
                {
                    if (campo.ToLower() == campoPermitido.ToLower())
                    {
                        campo = campoPermitido;
                        campoValido = true;
                        break;
                    }
                }

                if (!campoValido)
                    throw new ArgumentException("Campo de búsqueda no válido");

                return dstores.Buscar(campo, valor);
            }
            catch (Exception ex)
            {
                throw new Exception("Error en capa de negocio - Buscar: " + ex.Message);
            }
        }

        // Insertar nuevo registro
        public string Insertar(string storeName, string phone, string email, string street, string city, string state, string zipCode)
        {
            try
            {
                // Validaciones de negocio
                string validationResult = ValidateStoreData(storeName, phone, email, street, city, state, zipCode);
                if (validationResult != "OK")
                    return validationResult;

                return dstores.Insertar(storeName, phone, email, street, city, state, zipCode);
            }
            catch (Exception ex)
            {
                return "Error en capa de negocio - Insertar: " + ex.Message;
            }
        }

        // Editar registro existente
        public string Editar(int storeId, string storeName, string phone, string email, string street, string city, string state, string zipCode)
        {
            try
            {
                // Validaciones de negocio
                if (storeId <= 0)
                    return "ID de tienda no válido";

                if (!dstores.Existe(storeId))
                    return "La tienda no existe";

                string validationResult = ValidateStoreData(storeName, phone, email, street, city, state, zipCode);
                if (validationResult != "OK")
                    return validationResult;

                return dstores.Editar(storeId, storeName, phone, email, street, city, state, zipCode);
            }
            catch (Exception ex)
            {
                return "Error en capa de negocio - Editar: " + ex.Message;
            }
        }

        // Eliminar registro
        public string Eliminar(int storeId)
        {
            try
            {
                // Validaciones de negocio
                if (storeId <= 0)
                    return "ID de tienda no válido";

                if (!dstores.Existe(storeId))
                    return "La tienda no existe";

                // Verificar si hay empleados asignados a esta tienda
                DataTable staffAssigned = CheckStaffAssigned(storeId);
                if (staffAssigned.Rows.Count > 0)
                    return "No se puede eliminar la tienda porque tiene empleados asignados. Reasigne o elimine primero los empleados.";

                return dstores.Eliminar(storeId);
            }
            catch (Exception ex)
            {
                return "Error en capa de negocio - Eliminar: " + ex.Message;
            }
        }

        // Verificar si existe un registro
        public bool Existe(int storeId)
        {
            try
            {
                return dstores.Existe(storeId);
            }
            catch (Exception ex)
            {
                throw new Exception("Error en capa de negocio - Existe: " + ex.Message);
            }
        }

        // Validaciones de datos de tienda
        private string ValidateStoreData(string storeName, string phone, string email, string street, string city, string state, string zipCode)
        {
            // Validar nombre de tienda
            if (string.IsNullOrWhiteSpace(storeName))
                return "El nombre de la tienda es requerido";

            if (storeName.Trim().Length < 3)
                return "El nombre de la tienda debe tener al menos 3 caracteres";

            if (storeName.Trim().Length > 100)
                return "El nombre de la tienda no puede exceder 100 caracteres";

            // Validar email si se proporciona
            if (!string.IsNullOrWhiteSpace(email))
            {
                if (!IsValidEmail(email))
                    return "El formato del email no es válido";

                if (email.Length > 100)
                    return "El email no puede exceder 100 caracteres";
            }

            // Validar teléfono si se proporciona
            if (!string.IsNullOrWhiteSpace(phone))
            {
                if (phone.Length > 20)
                    return "El teléfono no puede exceder 20 caracteres";

                // Validar que solo contenga números, espacios, guiones y paréntesis
                foreach (char c in phone)
                {
                    if (!char.IsDigit(c) && c != ' ' && c != '-' && c != '(' && c != ')' && c != '+')
                        return "El teléfono contiene caracteres no válidos";
                }
            }

            // Validar dirección si se proporciona
            if (!string.IsNullOrWhiteSpace(street) && street.Length > 200)
                return "La dirección no puede exceder 200 caracteres";

            if (!string.IsNullOrWhiteSpace(city) && city.Length > 50)
                return "La ciudad no puede exceder 50 caracteres";

            if (!string.IsNullOrWhiteSpace(state) && state.Length > 50)
                return "El estado no puede exceder 50 caracteres";

            if (!string.IsNullOrWhiteSpace(zipCode) && zipCode.Length > 10)
                return "El código postal no puede exceder 10 caracteres";

            return "OK";
        }

        // Validar formato de email
        private bool IsValidEmail(string email)
        {
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

        // Verificar si hay staff asignado a la tienda
        private DataTable CheckStaffAssigned(int storeId)
        {
            try
            {
                DataTable dt = new DataTable();
                // Usar Dstaffs para verificar si hay empleados asignados
                Dstaffs dstaffs = new Dstaffs();
                dt = dstaffs.Buscar("store_id", storeId.ToString());
                return dt;
            }
            catch
            {
                return new DataTable(); // Retorna tabla vacía si hay error
            }
        }

        // Obtener estadísticas
        public DataTable GetStatistics()
        {
            try
            {
                return dstores.ObtenerEstadisticas();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener estadísticas: " + ex.Message);
            }
        }

        // Obtener tiendas con información de staff
        public DataTable GetStoresWithStaffInfo()
        {
            try
            {
                return dstores.ObtenerTiendasConStaff();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener tiendas con información de staff: " + ex.Message);
            }
        }
    }
}