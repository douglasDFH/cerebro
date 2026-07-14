using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using CapaDatos;

namespace CapaNegocio
{
    public class NProductos
    {
        #region Métodos CRUD

        public static string Insertar(string codigo, string nombre, decimal precio, int categoriaId,
            string descripcion = "", int stockMinimo = 5, int stockActual = 0, bool requiereReceta = false)
        {
            DProductos objProducto = new DProductos()
            {
                Codigo = codigo,
                Nombre = nombre,
                Descripcion = descripcion,
                Precio = precio,
                StockMinimo = stockMinimo,
                StockActual = stockActual,
                RequiereReceta = requiereReceta,
                CategoriaId = categoriaId
            };
            return objProducto.Insertar(objProducto);
        }

        public static string Editar(int id, string codigo, string nombre, decimal precio, int categoriaId,
            string descripcion = "", int stockMinimo = 5, int stockActual = 0, bool requiereReceta = false)
        {
            DProductos objProducto = new DProductos()
            {
                Id = id,
                Codigo = codigo,
                Nombre = nombre,
                Descripcion = descripcion,
                Precio = precio,
                StockMinimo = stockMinimo,
                StockActual = stockActual,
                RequiereReceta = requiereReceta,
                CategoriaId = categoriaId
            };
            return objProducto.Editar(objProducto);
        }

        public static string Eliminar(int id)
        {
            DProductos objProducto = new DProductos()
            {
                Id = id
            };
            return objProducto.Eliminar(objProducto);
        }

        public static DataTable Mostrar()
        {
            return new DProductos().Mostrar();
        }

        public static DataTable BuscarPorNombre(string textoBuscar)
        {
            DProductos objProducto = new DProductos()
            {
                TextoBuscar = textoBuscar
            };
            return objProducto.BuscarPorNombre(objProducto);
        }

        public static DataTable BuscarPorCategoria(int categoriaId)
        {
            return new DProductos().BuscarPorCategoria(categoriaId);
        }

        public static DataTable ObtenerPorId(int id)
        {
            DProductos objProducto = new DProductos()
            {
                Id = id
            };
            return objProducto.ObtenerPorId(objProducto);
        }

        public static DataTable ObtenerProductosBajoStock()
        {
            return new DProductos().ObtenerProductosBajoStock();
        }

        public static string ActualizarStock(int productoId, int nuevaCantidad, string operacion = "SET")
        {
            return new DProductos().ActualizarStock(productoId, nuevaCantidad, operacion);
        }

        #endregion

        #region Métodos para Categorías

        public static DataTable ObtenerCategorias()
        {
            return new DProductos().ObtenerCategorias();
        }

        public static string CrearCategoria(string nombre, string descripcion = "")
        {
            return new DProductos().CrearCategoria(nombre, descripcion);
        }

        public static List<string> ObtenerCategoriasComunes()
        {
            return new List<string>
            {
                "Medicamentos",
                "Suplementos",
                "Alimentos",
                "Juguetes",
                "Higiene y Cuidado",
                "Accesorios",
                "Equipos Médicos",
                "Material Quirúrgico",
                "Vacunas",
                "Antiparasitarios",
                "Antibióticos",
                "Analgésicos",
                "Vitaminas",
                "Collares y Correas",
                "Camas y Casas",
                "Comederos y Bebederos"
            };
        }

        public static string InicializarCategoriasVeterinarias()
        {
            try
            {
                // Verificar si ya existen categorías
                DataTable categoriasExistentes = ObtenerCategorias();
                if (categoriasExistentes != null && categoriasExistentes.Rows.Count > 0)
                {
                    return "Las categorías ya están inicializadas";
                }

                // Definir categorías veterinarias esenciales con descripciones
                var categoriasVeterinarias = new Dictionary<string, string>
                {
                    {"Medicamentos", "Fármacos y medicinas para tratamiento veterinario"},
                    {"Vacunas", "Inmunizaciones preventivas para animales"},
                    {"Antiparasitarios", "Tratamientos contra parásitos internos y externos"},
                    {"Antibióticos", "Medicamentos antibacterianos para animales"},
                    {"Analgésicos", "Medicamentos para el control del dolor"},
                    {"Alimentos Terapéuticos", "Alimentación especializada para tratamientos"},
                    {"Suplementos", "Complementos nutricionales y vitamínicos"},
                    {"Material Quirúrgico", "Instrumental y suministros médicos"},
                    {"Higiene y Cuidado", "Productos de aseo y cuidado animal"},
                    {"Equipos Médicos", "Dispositivos e instrumentos veterinarios"},
                    {"Accesorios", "Collares, correas, camas y juguetes"},
                    {"Primeros Auxilios", "Suministros para atención de emergencia"}
                };

                int categoriasCreadas = 0;
                string errores = "";

                // Crear cada categoría
                foreach (var categoria in categoriasVeterinarias)
                {
                    string resultado = CrearCategoria(categoria.Key, categoria.Value);
                    if (resultado == "OK" || resultado.Contains("exitosamente"))
                    {
                        categoriasCreadas++;
                    }
                    else
                    {
                        errores += $"{categoria.Key}: {resultado}; ";
                    }
                }

                if (categoriasCreadas > 0)
                {
                    string mensaje = $"Se crearon {categoriasCreadas} categorías veterinarias automáticamente.";
                    if (!string.IsNullOrEmpty(errores))
                    {
                        mensaje += $" Errores: {errores}";
                    }
                    return mensaje;
                }
                else
                {
                    return $"No se pudieron crear las categorías. Errores: {errores}";
                }
            }
            catch (Exception ex)
            {
                return $"Error inicializando categorías: {ex.Message}";
            }
        }

        public static bool VerificarYCrearCategoriasIniciales()
        {
            try
            {
                DataTable categorias = ObtenerCategorias();
                if (categorias == null || categorias.Rows.Count == 0)
                {
                    string resultado = InicializarCategoriasVeterinarias();
                    return resultado.Contains("crearon") || resultado.Contains("inicializadas");
                }
                return true; // Ya existen categorías
            }
            catch
            {
                return false;
            }
        }

        #endregion

        #region Validaciones de Negocio

        public static bool ValidarProducto(string nombre, decimal precio, int categoriaId)
        {
            if (string.IsNullOrWhiteSpace(nombre))
                return false;

            if (precio <= 0)
                return false;

            if (categoriaId <= 0)
                return false;

            return true;
        }

        public static bool ValidarCodigo(string codigo)
        {
            if (string.IsNullOrWhiteSpace(codigo))
                return true; // Código es opcional

            // Validar formato de código (letras, números, guiones)
            return System.Text.RegularExpressions.Regex.IsMatch(codigo, @"^[A-Za-z0-9\-_]+$");
        }

        public static bool ValidarStock(int stock)
        {
            return stock >= 0;
        }

        public static bool ValidarPrecio(decimal precio)
        {
            return precio > 0 && precio <= 999999.99m;
        }

        public static string ValidarDatosProducto(string codigo, string nombre, decimal precio,
            int categoriaId, int stockMinimo, int stockActual, string descripcion)
        {
            var errores = new List<string>();

            if (string.IsNullOrWhiteSpace(nombre))
                errores.Add("El nombre del producto es requerido");

            if (!ValidarPrecio(precio))
                errores.Add("El precio debe ser mayor a 0 y menor a $999,999.99");

            if (categoriaId <= 0)
                errores.Add("Debe seleccionar una categoría válida");

            if (!ValidarCodigo(codigo))
                errores.Add("El código solo puede contener letras, números, guiones y guiones bajos");

            if (!ValidarStock(stockMinimo))
                errores.Add("El stock mínimo debe ser mayor o igual a 0");

            if (!ValidarStock(stockActual))
                errores.Add("El stock actual debe ser mayor o igual a 0");

            if (!string.IsNullOrWhiteSpace(nombre) && nombre.Length > 200)
                errores.Add("El nombre no puede tener más de 200 caracteres");

            if (!string.IsNullOrWhiteSpace(codigo) && codigo.Length > 50)
                errores.Add("El código no puede tener más de 50 caracteres");

            if (!string.IsNullOrWhiteSpace(descripcion) && descripcion.Length > 1000)
                errores.Add("La descripción no puede tener más de 1000 caracteres");

            return errores.Any() ? string.Join(", ", errores) : "";
        }

        public static bool ExisteCodigo(string codigo, int? idExcluir = null)
        {
            if (string.IsNullOrWhiteSpace(codigo))
                return false;

            return new DProductos().ExisteCodigo(codigo, idExcluir);
        }

        public static string ValidarCodigoUnico(string codigo, int? idExcluir = null)
        {
            if (string.IsNullOrWhiteSpace(codigo))
                return ""; // Código es opcional

            if (!ValidarCodigo(codigo))
                return "El código tiene un formato inválido";

            if (ExisteCodigo(codigo, idExcluir))
                return "Ya existe un producto con este código";

            return "";
        }

        #endregion

        #region Estadísticas y Reportes

        public static DataTable ObtenerEstadisticasPorCategoria()
        {
            DataTable dtResultado = new DataTable("EstadisticasPorCategoria");
            try
            {
                DataTable productos = Mostrar();
                if (productos != null)
                {
                    var estadisticas = productos.AsEnumerable()
                        .GroupBy(row => row.Field<string>("categoria_nombre"))
                        .Select(g => new
                        {
                            Categoria = g.Key ?? "Sin categoría",
                            Cantidad = g.Count(),
                            StockTotal = g.Sum(r => r.Field<int?>("stock_actual") ?? 0),
                            ValorInventario = g.Sum(r => (r.Field<int?>("stock_actual") ?? 0) * (r.Field<decimal?>("precio") ?? 0)),
                            ProductosBajoStock = g.Count(r => (r.Field<int?>("stock_actual") ?? 0) <= (r.Field<int?>("stock_minimo") ?? 0))
                        })
                        .OrderByDescending(x => x.ValorInventario);

                    dtResultado.Columns.Add("Categoria", typeof(string));
                    dtResultado.Columns.Add("Cantidad", typeof(int));
                    dtResultado.Columns.Add("StockTotal", typeof(int));
                    dtResultado.Columns.Add("ValorInventario", typeof(decimal));
                    dtResultado.Columns.Add("ProductosBajoStock", typeof(int));

                    foreach (var item in estadisticas)
                    {
                        dtResultado.Rows.Add(item.Categoria, item.Cantidad, item.StockTotal, item.ValorInventario, item.ProductosBajoStock);
                    }
                }
            }
            catch (Exception ex)
            {
                dtResultado = new DataTable("EstadisticasPorCategoria");
                System.Diagnostics.Debug.WriteLine($"Error: {ex.Message}");
            }
            return dtResultado;
        }

        public static int ContarProductosActivos()
        {
            try
            {
                var connection = DbConnection.Instance.GetConnection();
                using (var command = new SqlCommand("SP_ContarProductosActivos", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    var result = command.ExecuteScalar();
                    return Convert.ToInt32(result);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error contando productos activos: {ex.Message}");
                return 0;
            }
        }

        public static int ContarProductosBajoStock()
        {
            try
            {
                var connection = DbConnection.Instance.GetConnection();
                using (var command = new SqlCommand("SP_ContarProductosBajoStock", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    var result = command.ExecuteScalar();
                    return Convert.ToInt32(result);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error contando productos bajo stock: {ex.Message}");
                return 0;
            }
        }

        public static decimal CalcularValorInventarioTotal()
        {
            try
            {
                var connection = DbConnection.Instance.GetConnection();
                using (var command = new SqlCommand("SP_CalcularValorInventarioTotal", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    var result = command.ExecuteScalar();
                    return Convert.ToDecimal(result);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error calculando valor inventario: {ex.Message}");
                return 0;
            }
        }

        public static DataTable ObtenerProductosMasVendidos(int limite = 10)
        {
            // Esta implementación requeriría una tabla de ventas detallada
            // Por ahora retorno un DataTable vacío
            DataTable dtResultado = new DataTable("ProductosMasVendidos");
            dtResultado.Columns.Add("ProductoId", typeof(int));
            dtResultado.Columns.Add("Nombre", typeof(string));
            dtResultado.Columns.Add("CantidadVendida", typeof(int));
            dtResultado.Columns.Add("IngresosTotales", typeof(decimal));

            // TODO: Implementar consulta real cuando esté la tabla de detalle de ventas
            return dtResultado;
        }

        #endregion

        #region Utilidades

        public static string GenerarCodigoAutomatico(string nombreProducto, string categoria = "")
        {
            try
            {
                // Generar código basado en las primeras letras del nombre y categoría
                string prefijo = "";

                if (!string.IsNullOrWhiteSpace(categoria))
                {
                    prefijo = categoria.Substring(0, Math.Min(3, categoria.Length)).ToUpper();
                }

                string nombreLimpio = new string(nombreProducto.Where(c => char.IsLetterOrDigit(c)).ToArray());
                string sufijo = nombreLimpio.Substring(0, Math.Min(6, nombreLimpio.Length)).ToUpper();

                string codigoBase = $"{prefijo}{sufijo}";

                // Verificar si existe y agregar número si es necesario
                string codigoFinal = codigoBase;
                int contador = 1;

                while (ExisteCodigo(codigoFinal))
                {
                    codigoFinal = $"{codigoBase}{contador:D2}";
                    contador++;

                    if (contador > 99) // Evitar bucle infinito
                    {
                        codigoFinal = $"{codigoBase}{DateTime.Now.Ticks % 1000}";
                        break;
                    }
                }

                return codigoFinal;
            }
            catch
            {
                return $"PROD{DateTime.Now.Ticks % 10000}";
            }
        }

        public static string FormatearPrecio(decimal precio)
        {
            return precio.ToString("C2");
        }

        public static string ObtenerEstadoStock(int stockActual, int stockMinimo)
        {
            if (stockActual <= 0)
                return "Sin Stock";
            else if (stockActual <= stockMinimo)
                return "Stock Bajo";
            else
                return "Stock OK";
        }

        public static Color ObtenerColorEstadoStock(int stockActual, int stockMinimo)
        {
            if (stockActual <= 0)
                return System.Drawing.Color.Red;
            else if (stockActual <= stockMinimo)
                return System.Drawing.Color.Orange;
            else
                return System.Drawing.Color.Green;
        }

        #endregion
    }
}