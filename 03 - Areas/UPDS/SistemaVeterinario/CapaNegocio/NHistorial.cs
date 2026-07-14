using System;
using System.Collections.Generic;
using System.Data;
using CapaDatos;

namespace CapaNegocio
{
    public class NHistorial
    {
        private readonly DHistorial _dataHistorial;

        public NHistorial()
        {
            _dataHistorial = new DHistorial();
        }

        #region Métodos CRUD para Historial

        /// <summary>
        /// Obtiene la lista de historiales médicos
        /// </summary>
        public DataTable ListarHistoriales(int? animalId = null, string buscar = null)
        {
            try
            {
                return _dataHistorial.ListarHistoriales(animalId, buscar);
            }
            catch (Exception ex)
            {
                throw new Exception("Error en capa de negocio al listar historiales: " + ex.Message);
            }
        }

        /// <summary>
        /// Obtiene un historial específico por ID
        /// </summary>
        public DataTable ObtenerHistorialPorId(int id)
        {
            try
            {
                if (id <= 0)
                    throw new ArgumentException("El ID del historial debe ser mayor que cero");

                return _dataHistorial.ObtenerHistorialPorId(id);
            }
            catch (Exception ex)
            {
                throw new Exception("Error en capa de negocio al obtener historial: " + ex.Message);
            }
        }

        /// <summary>
        /// Inserta un nuevo historial médico con validaciones de negocio
        /// </summary>
        public bool InsertarHistorial(int animalId, string notasGenerales, string alergias, string condicionesMedicas)
        {
            try
            {
                // Validaciones de negocio
                if (animalId <= 0)
                    throw new ArgumentException("El ID del animal debe ser mayor que cero");

                if (string.IsNullOrWhiteSpace(notasGenerales))
                    throw new ArgumentException("Las notas generales son obligatorias");

                if (notasGenerales.Trim().Length > 2000)
                    throw new ArgumentException("Las notas generales no pueden superar los 2000 caracteres");

                if (!string.IsNullOrEmpty(alergias) && alergias.Trim().Length > 500)
                    throw new ArgumentException("Las alergias no pueden superar los 500 caracteres");

                if (!string.IsNullOrEmpty(condicionesMedicas) && condicionesMedicas.Trim().Length > 500)
                    throw new ArgumentException("Las condiciones médicas no pueden superar los 500 caracteres");

                // Verificar si ya existe un historial para este animal
                if (_dataHistorial.ExisteHistorialParaAnimal(animalId))
                    throw new InvalidOperationException("Ya existe un historial médico para este animal");

                return _dataHistorial.InsertarHistorial(
                    animalId, 
                    notasGenerales?.Trim(), 
                    alergias?.Trim(), 
                    condicionesMedicas?.Trim()
                );
            }
            catch (Exception ex)
            {
                throw new Exception("Error en capa de negocio al insertar historial: " + ex.Message);
            }
        }

        /// <summary>
        /// Actualiza un historial médico existente con validaciones
        /// </summary>
        public bool ActualizarHistorial(int id, int animalId, string notasGenerales, string alergias, string condicionesMedicas)
        {
            try
            {
                // Validaciones de negocio
                if (id <= 0)
                    throw new ArgumentException("El ID del historial debe ser mayor que cero");

                if (animalId <= 0)
                    throw new ArgumentException("El ID del animal debe ser mayor que cero");

                if (string.IsNullOrWhiteSpace(notasGenerales))
                    throw new ArgumentException("Las notas generales son obligatorias");

                if (notasGenerales.Trim().Length > 2000)
                    throw new ArgumentException("Las notas generales no pueden superar los 2000 caracteres");

                if (!string.IsNullOrEmpty(alergias) && alergias.Trim().Length > 500)
                    throw new ArgumentException("Las alergias no pueden superar los 500 caracteres");

                if (!string.IsNullOrEmpty(condicionesMedicas) && condicionesMedicas.Trim().Length > 500)
                    throw new ArgumentException("Las condiciones médicas no pueden superar los 500 caracteres");

                return _dataHistorial.ActualizarHistorial(
                    id, 
                    animalId, 
                    notasGenerales?.Trim(), 
                    alergias?.Trim(), 
                    condicionesMedicas?.Trim()
                );
            }
            catch (Exception ex)
            {
                throw new Exception("Error en capa de negocio al actualizar historial: " + ex.Message);
            }
        }

        /// <summary>
        /// Elimina un historial médico con validaciones
        /// </summary>
        public bool EliminarHistorial(int id)
        {
            try
            {
                if (id <= 0)
                    throw new ArgumentException("El ID del historial debe ser mayor que cero");

                // Verificar si el historial tiene detalles asociados antes de eliminar
                if (TieneDetallesAsociados(id))
                    throw new InvalidOperationException("No se puede eliminar el historial porque tiene detalles médicos asociados. Elimine primero los detalles.");

                return _dataHistorial.EliminarHistorial(id);
            }
            catch (Exception ex)
            {
                throw new Exception("Error en capa de negocio al eliminar historial: " + ex.Message);
            }
        }

        /// <summary>
        /// Busca historiales por criterios específicos
        /// </summary>
        public DataTable BuscarHistoriales(string criterio, string valor)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(valor))
                    return ListarHistoriales();

                return _dataHistorial.BuscarHistoriales(criterio, valor.Trim());
            }
            catch (Exception ex)
            {
                throw new Exception("Error en capa de negocio al buscar historiales: " + ex.Message);
            }
        }

        #endregion

        #region Métodos para Detalle de Historial

        /// <summary>
        /// Obtiene los detalles de un historial específico
        /// </summary>
        public DataTable ListarDetallesHistorial(int historialId)
        {
            try
            {
                if (historialId <= 0)
                    throw new ArgumentException("El ID del historial debe ser mayor que cero");

                return _dataHistorial.ListarDetallesHistorial(historialId);
            }
            catch (Exception ex)
            {
                throw new Exception("Error en capa de negocio al obtener detalles del historial: " + ex.Message);
            }
        }

        /// <summary>
        /// Inserta un nuevo detalle de historial con validaciones
        /// </summary>
        public bool InsertarDetalleHistorial(int historialId, string tipoRegistro, string descripcion, 
            DateTime fechaRegistro, int? veterinarioId, string tratamiento, string medicamentos, string observaciones)
        {
            try
            {
                // Validaciones de negocio
                if (historialId <= 0)
                    throw new ArgumentException("El ID del historial debe ser mayor que cero");

                if (string.IsNullOrWhiteSpace(tipoRegistro))
                    throw new ArgumentException("El tipo de registro es obligatorio");

                if (string.IsNullOrWhiteSpace(descripcion))
                    throw new ArgumentException("La descripción es obligatoria");

                if (descripcion.Trim().Length > 1000)
                    throw new ArgumentException("La descripción no puede superar los 1000 caracteres");

                if (fechaRegistro > DateTime.Now)
                    throw new ArgumentException("La fecha de registro no puede ser futura");

                if (fechaRegistro < DateTime.Now.AddYears(-10))
                    throw new ArgumentException("La fecha de registro no puede ser anterior a 10 años");

                if (!string.IsNullOrEmpty(tratamiento) && tratamiento.Trim().Length > 500)
                    throw new ArgumentException("El tratamiento no puede superar los 500 caracteres");

                if (!string.IsNullOrEmpty(medicamentos) && medicamentos.Trim().Length > 500)
                    throw new ArgumentException("Los medicamentos no pueden superar los 500 caracteres");

                if (!string.IsNullOrEmpty(observaciones) && observaciones.Trim().Length > 1000)
                    throw new ArgumentException("Las observaciones no pueden superar los 1000 caracteres");

                // Validar que el tipo de registro sea válido
                var tiposValidos = ObtenerTiposRegistro();
                if (!tiposValidos.Contains(tipoRegistro.Trim()))
                    throw new ArgumentException("El tipo de registro no es válido");

                return _dataHistorial.InsertarDetalleHistorial(
                    historialId,
                    tipoRegistro.Trim(),
                    descripcion.Trim(),
                    fechaRegistro,
                    veterinarioId,
                    tratamiento?.Trim(),
                    medicamentos?.Trim(),
                    observaciones?.Trim()
                );
            }
            catch (Exception ex)
            {
                throw new Exception("Error en capa de negocio al insertar detalle del historial: " + ex.Message);
            }
        }

        /// <summary>
        /// Elimina un detalle de historial
        /// </summary>
        public bool EliminarDetalleHistorial(int detalleId)
        {
            try
            {
                if (detalleId <= 0)
                    throw new ArgumentException("El ID del detalle debe ser mayor que cero");

                return _dataHistorial.EliminarDetalleHistorial(detalleId);
            }
            catch (Exception ex)
            {
                throw new Exception("Error en capa de negocio al eliminar detalle del historial: " + ex.Message);
            }
        }

        #endregion

        #region Métodos de Apoyo

        /// <summary>
        /// Obtiene la lista de animales para ComboBox
        /// </summary>
        public DataTable ObtenerAnimales()
        {
            try
            {
                return _dataHistorial.ObtenerAnimales();
            }
            catch (Exception ex)
            {
                throw new Exception("Error en capa de negocio al obtener animales: " + ex.Message);
            }
        }

        /// <summary>
        /// Obtiene la lista de veterinarios para ComboBox
        /// </summary>
        public DataTable ObtenerVeterinarios()
        {
            try
            {
                return _dataHistorial.ObtenerVeterinarios();
            }
            catch (Exception ex)
            {
                throw new Exception("Error en capa de negocio al obtener veterinarios: " + ex.Message);
            }
        }

        /// <summary>
        /// Obtiene los tipos de registro válidos para detalles de historial
        /// </summary>
        public List<string> ObtenerTiposRegistro()
        {
            return new List<string>
            {
                "Consulta",
                "Vacunación", 
                "Cirugía",
                "Emergencia",
                "Control",
                "Examen",
                "Tratamiento",
                "Observación"
            };
        }

        /// <summary>
        /// Verifica si existe un historial para un animal específico
        /// </summary>
        public bool ExisteHistorialParaAnimal(int animalId)
        {
            try
            {
                if (animalId <= 0)
                    return false;

                return _dataHistorial.ExisteHistorialParaAnimal(animalId);
            }
            catch (Exception ex)
            {
                throw new Exception("Error en capa de negocio al verificar historial: " + ex.Message);
            }
        }

        /// <summary>
        /// Verifica si un historial tiene detalles asociados
        /// </summary>
        private bool TieneDetallesAsociados(int historialId)
        {
            try
            {
                DataTable detalles = _dataHistorial.ListarDetallesHistorial(historialId);
                return detalles.Rows.Count > 0;
            }
            catch
            {
                return false;
            }
        }

        #endregion

        #region Métodos de Validación

        /// <summary>
        /// Valida el formato y contenido de las notas generales
        /// </summary>
        public bool ValidarNotasGenerales(string notas)
        {
            if (string.IsNullOrWhiteSpace(notas))
                return false;

            if (notas.Trim().Length > 2000)
                return false;

            // Validar que no contenga solo caracteres especiales
            string notasLimpia = notas.Trim();
            if (string.IsNullOrWhiteSpace(notasLimpia.Replace(".", "").Replace(",", "").Replace(";", "")))
                return false;

            return true;
        }

        /// <summary>
        /// Valida el formato de alergias
        /// </summary>
        public bool ValidarAlergias(string alergias)
        {
            if (string.IsNullOrEmpty(alergias))
                return true; // Las alergias son opcionales

            return alergias.Trim().Length <= 500;
        }

        /// <summary>
        /// Valida el formato de condiciones médicas
        /// </summary>
        public bool ValidarCondicionesMedicas(string condiciones)
        {
            if (string.IsNullOrEmpty(condiciones))
                return true; // Las condiciones son opcionales

            return condiciones.Trim().Length <= 500;
        }

        /// <summary>
        /// Valida una fecha de registro
        /// </summary>
        public bool ValidarFechaRegistro(DateTime fecha)
        {
            return fecha <= DateTime.Now && fecha >= DateTime.Now.AddYears(-10);
        }

        #endregion
    }
}
