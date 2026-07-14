using System;
using System.Collections.Generic;
using System.Data;
using CapaDatos;

namespace CapaNegocio
{
    public class NDiagnostico
    {
        private readonly DDiagnostico _dataDiagnostico;

        public NDiagnostico()
        {
            _dataDiagnostico = new DDiagnostico();
        }

        #region Métodos CRUD

        /// <summary>
        /// Obtiene la lista de diagnósticos
        /// </summary>
        public DataTable ListarDiagnosticos()
        {
            try
            {
                return _dataDiagnostico.ListarDiagnosticos();
            }
            catch (Exception ex)
            {
                throw new Exception("Error en capa de negocio al listar diagnósticos: " + ex.Message);
            }
        }

        /// <summary>
        /// Obtiene diagnósticos por categoría
        /// </summary>
        public DataTable ListarDiagnosticosPorCategoria(string categoria)
        {
            try
            {
                if (string.IsNullOrEmpty(categoria))
                    throw new ArgumentException("La categoría no puede estar vacía");

                return _dataDiagnostico.ListarDiagnosticosPorCategoria(categoria);
            }
            catch (Exception ex)
            {
                throw new Exception("Error en capa de negocio al listar diagnósticos por categoría: " + ex.Message);
            }
        }

        /// <summary>
        /// Busca diagnósticos por texto
        /// </summary>
        public DataTable BuscarDiagnosticos(string textoBusqueda)
        {
            try
            {
                if (string.IsNullOrEmpty(textoBusqueda))
                    return ListarDiagnosticos();

                return _dataDiagnostico.BuscarDiagnosticos(textoBusqueda);
            }
            catch (Exception ex)
            {
                throw new Exception("Error en capa de negocio al buscar diagnósticos: " + ex.Message);
            }
        }

        /// <summary>
        /// Inserta un nuevo diagnóstico con validaciones de negocio
        /// </summary>
        public bool InsertarDiagnostico(string codigo, string nombre, string categoria, string descripcion,
            decimal precioBase, bool requiereEquipamiento, bool activo = true)
        {
            try
            {
                // Validaciones de negocio
                ValidarDatosDiagnostico(codigo, nombre, precioBase);

                // Verificar código duplicado
                if (!string.IsNullOrEmpty(codigo) && _dataDiagnostico.ExisteCodigoDiagnostico(codigo))
                {
                    throw new Exception("Ya existe un diagnóstico con el código especificado");
                }

                return _dataDiagnostico.InsertarDiagnostico(codigo, nombre, categoria, descripcion,
                    precioBase, requiereEquipamiento, activo);
            }
            catch (Exception ex)
            {
                throw new Exception("Error en capa de negocio al insertar diagnóstico: " + ex.Message);
            }
        }

        /// <summary>
        /// Actualiza un diagnóstico existente
        /// </summary>
        public bool ActualizarDiagnostico(int id, string codigo, string nombre, string categoria, string descripcion,
            decimal precioBase, bool requiereEquipamiento, bool activo)
        {
            try
            {
                // Validaciones básicas
                if (id <= 0)
                    throw new ArgumentException("El ID debe ser mayor a cero");

                ValidarDatosDiagnostico(codigo, nombre, precioBase);

                // Verificar código duplicado (excluyendo el registro actual)
                if (!string.IsNullOrEmpty(codigo))
                {
                    var diagnosticoExistente = _dataDiagnostico.ObtenerDiagnosticoPorId(id);
                    if (diagnosticoExistente.Rows.Count > 0)
                    {
                        string codigoOriginal = diagnosticoExistente.Rows[0]["codigo"].ToString();
                        if (codigo != codigoOriginal && _dataDiagnostico.ExisteCodigoDiagnostico(codigo))
                        {
                            throw new Exception("Ya existe otro diagnóstico con el código especificado");
                        }
                    }
                }

                return _dataDiagnostico.ActualizarDiagnostico(id, codigo, nombre, categoria, descripcion,
                    precioBase, requiereEquipamiento, activo);
            }
            catch (Exception ex)
            {
                throw new Exception("Error en capa de negocio al actualizar diagnóstico: " + ex.Message);
            }
        }

        /// <summary>
        /// Elimina un diagnóstico
        /// </summary>
        public bool EliminarDiagnostico(int id)
        {
            try
            {
                if (id <= 0)
                    throw new ArgumentException("El ID debe ser mayor a cero");

                return _dataDiagnostico.EliminarDiagnostico(id);
            }
            catch (Exception ex)
            {
                throw new Exception("Error en capa de negocio al eliminar diagnóstico: " + ex.Message);
            }
        }

        #endregion

        #region Métodos de Utilidad

        /// <summary>
        /// Obtiene las categorías disponibles
        /// </summary>
        public List<string> ObtenerCategorias()
        {
            try
            {
                return _dataDiagnostico.ObtenerCategorias();
            }
            catch (Exception ex)
            {
                throw new Exception("Error en capa de negocio al obtener categorías: " + ex.Message);
            }
        }

        /// <summary>
        /// Obtiene un diagnóstico por ID
        /// </summary>
        public DataTable ObtenerDiagnosticoPorId(int id)
        {
            try
            {
                if (id <= 0)
                    throw new ArgumentException("El ID debe ser mayor a cero");

                return _dataDiagnostico.ObtenerDiagnosticoPorId(id);
            }
            catch (Exception ex)
            {
                throw new Exception("Error en capa de negocio al obtener diagnóstico: " + ex.Message);
            }
        }

        /// <summary>
        /// Verifica si existe un código de diagnóstico
        /// </summary>
        public bool ExisteCodigoDiagnostico(string codigo)
        {
            try
            {
                if (string.IsNullOrEmpty(codigo))
                    return false;

                return _dataDiagnostico.ExisteCodigoDiagnostico(codigo);
            }
            catch (Exception ex)
            {
                throw new Exception("Error en capa de negocio al verificar código: " + ex.Message);
            }
        }

        /// <summary>
        /// Genera un código automático único
        /// </summary>
        public string GenerarCodigoAutomatico()
        {
            try
            {
                return _dataDiagnostico.GenerarCodigoAutomatico();
            }
            catch (Exception ex)
            {
                throw new Exception("Error en capa de negocio al generar código: " + ex.Message);
            }
        }

        #endregion

        #region Validaciones de Negocio

        /// <summary>
        /// Valida los datos básicos de un diagnóstico
        /// </summary>
        private void ValidarDatosDiagnostico(string codigo, string nombre, decimal precioBase)
        {
            var errores = new List<string>();

            // Validar nombre
            if (string.IsNullOrWhiteSpace(nombre))
                errores.Add("El nombre del diagnóstico es obligatorio");
            else if (nombre.Length > 200)
                errores.Add("El nombre no puede exceder 200 caracteres");

            // Validar código
            if (!string.IsNullOrEmpty(codigo) && codigo.Length > 20)
                errores.Add("El código no puede exceder 20 caracteres");

            // Validar precio
            if (precioBase < 0)
                errores.Add("El precio base no puede ser negativo");
            else if (precioBase > 999999)
                errores.Add("El precio base no puede exceder 999,999");

            if (errores.Count > 0)
                throw new ArgumentException(string.Join(", ", errores));
        }

        #endregion
    }
}
