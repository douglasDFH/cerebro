using System;
using System.Data;
using CapaDatos;

namespace CapaNegocio
{
    public class Nstaffs
    {
        private Dstaffs dstaffs;

        public Nstaffs()
        {
            dstaffs = new Dstaffs();
        }

        // Mostrar todos los registros
        public DataTable Mostrar()
        {
            try
            {
                return dstaffs.Mostrar();
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
                string[] camposPermitidos = { "staff_id", "first_name", "last_name", "email", "phone" };
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

                return dstaffs.Buscar(campo, valor);
            }
            catch (Exception ex)
            {
                throw new Exception("Error en capa de negocio - Buscar: " + ex.Message);
            }
        }

        // Insertar nuevo registro
        public string Insertar(string firstName, string lastName, string email, string phone, bool active, int storeId, int? managerId)
        {
            try
            {
                // Validaciones de negocio
                string validationResult = ValidateStaffData(firstName, lastName, email, phone, storeId);
                if (validationResult != "OK")
                    return validationResult;

                // Validar email único
                if (EmailExists(email, 0))
                    return "El email ya existe en el sistema";

                return dstaffs.Insertar(firstName, lastName, email, phone, active, storeId, managerId);
            }
            catch (Exception ex)
            {
                return "Error en capa de negocio - Insertar: " + ex.Message;
            }
        }

        // Editar registro existente
        public string Editar(int staffId, string firstName, string lastName, string email, string phone, bool active, int storeId, int? managerId)
        {
            try
            {
                // Validaciones de negocio
                if (staffId <= 0)
                    return "ID de staff no válido";

                if (!dstaffs.Existe(staffId))
                    return "El registro no existe";

                string validationResult = ValidateStaffData(firstName, lastName, email, phone, storeId);
                if (validationResult != "OK")
                    return validationResult;

                // Validar email único (excluyendo el registro actual)
                if (EmailExists(email, staffId))
                    return "El email ya existe en el sistema";

                return dstaffs.Editar(staffId, firstName, lastName, email, phone, active, storeId, managerId);
            }
            catch (Exception ex)
            {
                return "Error en capa de negocio - Editar: " + ex.Message;
            }
        }

        // Eliminar registro
        public string Eliminar(int staffId)
        {
            try
            {
                // Validaciones de negocio
                if (staffId <= 0)
                    return "ID de staff no válido";

                if (!dstaffs.Existe(staffId))
                    return "El registro no existe";

                // Aquí podrías agregar validaciones adicionales como:
                // - Verificar si el staff tiene órdenes asociadas
                // - Verificar si es manager de otros empleados
                // - etc.

                return dstaffs.Eliminar(staffId);
            }
            catch (Exception ex)
            {
                return "Error en capa de negocio - Eliminar: " + ex.Message;
            }
        }

        // Verificar si existe un registro
        public bool Existe(int staffId)
        {
            try
            {
                return dstaffs.Existe(staffId);
            }
            catch (Exception ex)
            {
                throw new Exception("Error en capa de negocio - Existe: " + ex.Message);
            }
        }

        // Validaciones de datos de staff
        private string ValidateStaffData(string firstName, string lastName, string email, string phone, int storeId)
        {
            // Validar nombres
            if (string.IsNullOrWhiteSpace(firstName))
                return "El nombre es requerido";

            if (firstName.Trim().Length < 2)
                return "El nombre debe tener al menos 2 caracteres";

            if (firstName.Trim().Length > 50)
                return "El nombre no puede exceder 50 caracteres";

            if (string.IsNullOrWhiteSpace(lastName))
                return "El apellido es requerido";

            if (lastName.Trim().Length < 2)
                return "El apellido debe tener al menos 2 caracteres";

            if (lastName.Trim().Length > 50)
                return "El apellido no puede exceder 50 caracteres";

            // Validar email
            if (string.IsNullOrWhiteSpace(email))
                return "El email es requerido";

            if (!IsValidEmail(email))
                return "El formato del email no es válido";

            if (email.Length > 100)
                return "El email no puede exceder 100 caracteres";

            // Validar teléfono
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

            // Validar store_id
            if (storeId <= 0)
                return "ID de tienda no válido";

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

        // Verificar si el email ya existe
        private bool EmailExists(string email, int excludeStaffId)
        {
            try
            {
                DataTable dt = dstaffs.Buscar("email", email);
                foreach (DataRow row in dt.Rows)
                {
                    int existingStaffId = Convert.ToInt32(row["staff_id"]);
                    if (existingStaffId != excludeStaffId)
                        return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        // Obtener lista de managers para ComboBox
        public DataTable GetManagers()
        {
            try
            {
                // Obtener todos los staff activos que pueden ser managers
                DataTable allStaff = dstaffs.Mostrar();
                DataTable managers = new DataTable();

                managers.Columns.Add("staff_id", typeof(int));
                managers.Columns.Add("full_name", typeof(string));

                // Agregar opción "Sin Manager"
                DataRow noManagerRow = managers.NewRow();
                noManagerRow["staff_id"] = DBNull.Value;
                noManagerRow["full_name"] = "Sin Manager";
                managers.Rows.Add(noManagerRow);

                // Agregar staff activos como posibles managers
                foreach (DataRow row in allStaff.Rows)
                {
                    if (Convert.ToBoolean(row["active"]))
                    {
                        DataRow managerRow = managers.NewRow();
                        managerRow["staff_id"] = row["staff_id"];
                        managerRow["full_name"] = $"{row["first_name"]} {row["last_name"]}";
                        managers.Rows.Add(managerRow);
                    }
                }

                return managers;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener managers: " + ex.Message);
            }
        }

        // Obtener estadísticas básicas
        public DataTable GetStatistics()
        {
            try
            {
                DataTable allStaff = dstaffs.Mostrar();
                DataTable stats = new DataTable();

                stats.Columns.Add("Descripcion", typeof(string));
                stats.Columns.Add("Cantidad", typeof(int));

                int totalStaff = allStaff.Rows.Count;
                int activeStaff = 0;
                int inactiveStaff = 0;

                foreach (DataRow row in allStaff.Rows)
                {
                    if (Convert.ToBoolean(row["active"]))
                        activeStaff++;
                    else
                        inactiveStaff++;
                }

                stats.Rows.Add("Total de Staff", totalStaff);
                stats.Rows.Add("Staff Activo", activeStaff);
                stats.Rows.Add("Staff Inactivo", inactiveStaff);

                return stats;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener estadísticas: " + ex.Message);
            }
        }
    }
}