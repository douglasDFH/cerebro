using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class DProductos
    {
        private int _id;
        private string _codigo = string.Empty;
        private string _nombre = string.Empty;
        private string _descripcion = string.Empty;
        private decimal _precio;
        private int _stockMinimo;
        private int _stockActual;
        private bool _requiereReceta;
        private int _categoriaId;
        private bool _activo;
        private string _textoBuscar = string.Empty;

        #region Propiedades
        public int Id { get => _id; set => _id = value; }
        public string Codigo { get => _codigo; set => _codigo = value; }
        public string Nombre { get => _nombre; set => _nombre = value; }
        public string Descripcion { get => _descripcion; set => _descripcion = value; }
        public decimal Precio { get => _precio; set => _precio = value; }
        public int StockMinimo { get => _stockMinimo; set => _stockMinimo = value; }
        public int StockActual { get => _stockActual; set => _stockActual = value; }
        public bool RequiereReceta { get => _requiereReceta; set => _requiereReceta = value; }
        public int CategoriaId { get => _categoriaId; set => _categoriaId = value; }
        public bool Activo { get => _activo; set => _activo = value; }
        public string TextoBuscar { get => _textoBuscar; set => _textoBuscar = value; }
        #endregion

        #region Constructores
        public DProductos()
        {
            _activo = true;
            _requiereReceta = false;
            _stockMinimo = 5;
            _stockActual = 0;
        }

        public DProductos(string codigo, string nombre, decimal precio, int categoriaId)
        {
            Codigo = codigo;
            Nombre = nombre;
            Precio = precio;
            CategoriaId = categoriaId;
            Activo = true;
            RequiereReceta = false;
            StockMinimo = 5;
            StockActual = 0;
        }
        #endregion

        #region Métodos CRUD

        public string Insertar(DProductos producto)
        {
            string rpta = string.Empty;
            SqlConnection connection = DbConnection.Instance.GetConnection();
            try
            {
                using (SqlCommand command = new SqlCommand("SP07_CreateOrUpdateProducto", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@id", DBNull.Value);
                    command.Parameters.AddWithValue("@codigo", producto.Codigo ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@nombre", producto.Nombre ?? "");
                    command.Parameters.AddWithValue("@descripcion", producto.Descripcion ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@precio", producto.Precio);
                    command.Parameters.AddWithValue("@stock_minimo", producto.StockMinimo);
                    command.Parameters.AddWithValue("@stock_actual", producto.StockActual);
                    command.Parameters.AddWithValue("@requiere_receta", producto.RequiereReceta);
                    command.Parameters.AddWithValue("@categoria_id", producto.CategoriaId);
                    command.Parameters.AddWithValue("@activo", producto.Activo);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            int id = Convert.ToInt32(reader["id"]);
                            if (id > 0)
                            {
                                producto.Id = id;
                                rpta = "OK";
                            }
                            else
                            {
                                rpta = reader["mensaje"]?.ToString() ?? "No se recibió mensaje del procedimiento";
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                rpta = ex.Message;
            }
            return rpta;
        }

        public string Editar(DProductos producto)
        {
            string rpta = string.Empty;
            SqlConnection connection = DbConnection.Instance.GetConnection();
            try
            {
                using (SqlCommand command = new SqlCommand("SP07_CreateOrUpdateProducto", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@id", producto.Id);
                    command.Parameters.AddWithValue("@codigo", producto.Codigo ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@nombre", producto.Nombre ?? "");
                    command.Parameters.AddWithValue("@descripcion", producto.Descripcion ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@precio", producto.Precio);
                    command.Parameters.AddWithValue("@stock_minimo", producto.StockMinimo);
                    command.Parameters.AddWithValue("@stock_actual", producto.StockActual);
                    command.Parameters.AddWithValue("@requiere_receta", producto.RequiereReceta);
                    command.Parameters.AddWithValue("@categoria_id", producto.CategoriaId);
                    command.Parameters.AddWithValue("@activo", producto.Activo);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            rpta = reader["mensaje"]?.ToString() ?? "No se recibió mensaje del procedimiento";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                rpta = ex.Message;
            }
            return rpta;
        }

        public string Eliminar(DProductos producto)
        {
            string rpta = string.Empty;
            SqlConnection connection = DbConnection.Instance.GetConnection();
            try
            {
                string query = "UPDATE producto SET activo = 0 WHERE id = @id";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", producto.Id);

                    int filasAfectadas = command.ExecuteNonQuery();
                    rpta = filasAfectadas > 0 ? "OK" : "No se encontró el producto";
                }
            }
            catch (Exception ex)
            {
                rpta = ex.Message;
            }
            return rpta;
        }

        public DataTable Mostrar()
        {
            DataTable dtResultado = new DataTable("Productos");
            SqlConnection connection = DbConnection.Instance.GetConnection();
            try
            {
                string query = @"SELECT p.id, p.codigo, p.nombre, p.descripcion, p.precio, 
                                          p.stock_minimo, p.stock_actual, p.requiere_receta, p.activo,
                                          c.nombre as categoria_nombre, p.categoria_id,
                                          CASE 
                                            WHEN p.stock_actual = 0 THEN 'Sin Stock'
                                            WHEN p.stock_actual <= p.stock_minimo THEN 'Stock Bajo'
                                            ELSE 'Stock OK'
                                          END as estado_stock
                                   FROM producto p
                                   INNER JOIN categoria c ON p.categoria_id = c.id
                                   WHERE p.activo = 1
                                   ORDER BY p.nombre";

                using (SqlCommand command = new SqlCommand(query, connection))
                using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                {
                    adapter.Fill(dtResultado);
                }
            }
            catch
            {
                dtResultado = new DataTable("Productos");
            }
            return dtResultado;
        }

        public DataTable BuscarPorNombre(DProductos producto)
        {
            DataTable dtResultado = new DataTable("ProductosBusqueda");
            SqlConnection connection = DbConnection.Instance.GetConnection();
            try
            {
                string query = @"SELECT p.id, p.codigo, p.nombre, p.descripcion, p.precio, 
                                          p.stock_minimo, p.stock_actual, p.requiere_receta, p.activo,
                                          c.nombre as categoria_nombre, p.categoria_id,
                                          CASE 
                                            WHEN p.stock_actual = 0 THEN 'Sin Stock'
                                            WHEN p.stock_actual <= p.stock_minimo THEN 'Stock Bajo'
                                            ELSE 'Stock OK'
                                          END as estado_stock
                                   FROM producto p
                                   INNER JOIN categoria c ON p.categoria_id = c.id
                                   WHERE p.activo = 1 
                                     AND (p.nombre LIKE @textoBuscar 
                                          OR p.codigo LIKE @textoBuscar
                                          OR p.descripcion LIKE @textoBuscar
                                          OR c.nombre LIKE @textoBuscar)
                                   ORDER BY p.nombre";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@textoBuscar", "%" + (producto.TextoBuscar ?? "") + "%");
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(dtResultado);
                    }
                }
            }
            catch
            {
                dtResultado = new DataTable("ProductosBusqueda");
            }
            return dtResultado;
        }

        public DataTable BuscarPorCategoria(int categoriaId)
        {
            DataTable dtResultado = new DataTable("ProductosPorCategoria");
            SqlConnection connection = DbConnection.Instance.GetConnection();
            try
            {
                string query = @"SELECT p.id, p.codigo, p.nombre, p.descripcion, p.precio, 
                                          p.stock_minimo, p.stock_actual, p.requiere_receta, p.activo,
                                          c.nombre as categoria_nombre
                                   FROM producto p
                                   INNER JOIN categoria c ON p.categoria_id = c.id
                                   WHERE p.activo = 1 AND p.categoria_id = @categoriaId
                                   ORDER BY p.nombre";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@categoriaId", categoriaId);
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(dtResultado);
                    }
                }
            }
            catch
            {
                dtResultado = new DataTable("ProductosPorCategoria");
            }
            return dtResultado;
        }

        public DataTable ObtenerPorId(DProductos producto)
        {
            DataTable dtResultado = new DataTable("ProductoPorId");
            SqlConnection connection = DbConnection.Instance.GetConnection();
            try
            {
                string query = @"SELECT p.*, c.nombre as categoria_nombre 
                                   FROM producto p
                                   INNER JOIN categoria c ON p.categoria_id = c.id
                                   WHERE p.id = @id";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", producto.Id);
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(dtResultado);
                    }
                }
            }
            catch
            {
                dtResultado = new DataTable("ProductoPorId");
            }
            return dtResultado;
        }

        public DataTable ObtenerProductosBajoStock()
        {
            DataTable dtResultado = new DataTable("ProductosBajoStock");
            SqlConnection connection = DbConnection.Instance.GetConnection();
            try
            {
                string query = @"SELECT p.id, p.codigo, p.nombre, p.stock_actual, p.stock_minimo,
                                          c.nombre as categoria_nombre,
                                          (p.stock_minimo - p.stock_actual) as cantidad_necesaria
                                   FROM producto p
                                   INNER JOIN categoria c ON p.categoria_id = c.id
                                   WHERE p.activo = 1 AND p.stock_actual <= p.stock_minimo
                                   ORDER BY p.stock_actual ASC, p.nombre";

                using (SqlCommand command = new SqlCommand(query, connection))
                using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                {
                    adapter.Fill(dtResultado);
                }
            }
            catch
            {
                dtResultado = new DataTable("ProductosBajoStock");
            }
            return dtResultado;
        }

        public string ActualizarStock(int productoId, int nuevaCantidad, string operacion = "SET")
        {
            string rpta = string.Empty;
            SqlConnection connection = DbConnection.Instance.GetConnection();
            try
            {
                string query = "";
                switch (operacion.ToUpper())
                {
                    case "ADD":
                        query = "UPDATE producto SET stock_actual = stock_actual + @cantidad WHERE id = @id";
                        break;
                    case "SUBTRACT":
                        query = "UPDATE producto SET stock_actual = stock_actual - @cantidad WHERE id = @id AND stock_actual >= @cantidad";
                        break;
                    default: // SET
                        query = "UPDATE producto SET stock_actual = @cantidad WHERE id = @id";
                        break;
                }

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", productoId);
                    command.Parameters.AddWithValue("@cantidad", nuevaCantidad);

                    int filasAfectadas = command.ExecuteNonQuery();
                    if (filasAfectadas > 0)
                    {
                        rpta = "OK";
                    }
                    else
                    {
                        rpta = operacion == "SUBTRACT" ? "Stock insuficiente" : "No se encontró el producto";
                    }
                }
            }
            catch (Exception ex)
            {
                rpta = ex.Message;
            }
            return rpta;
        }

        public bool ExisteCodigo(string codigo, int? idExcluir = null)
        {
            SqlConnection connection = DbConnection.Instance.GetConnection();
            try
            {
                string query = "SELECT COUNT(*) FROM producto WHERE codigo = @codigo AND activo = 1";
                if (idExcluir.HasValue)
                {
                    query += " AND id != @idExcluir";
                }

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@codigo", codigo);
                    if (idExcluir.HasValue)
                    {
                        command.Parameters.AddWithValue("@idExcluir", idExcluir.Value);
                    }

                    int count = Convert.ToInt32(command.ExecuteScalar());
                    return count > 0;
                }
            }
            catch
            {
                return false;
            }
        }

        #endregion

        #region Métodos para Categorías

        public DataTable ObtenerCategorias()
        {
            DataTable dtResultado = new DataTable("Categorias");
            SqlConnection connection = DbConnection.Instance.GetConnection();
            try
            {
                string query = @"SELECT id, nombre, descripcion FROM categoria WHERE activo = 1 ORDER BY nombre";

                using (SqlCommand command = new SqlCommand(query, connection))
                using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                {
                    adapter.Fill(dtResultado);
                }
            }
            catch
            {
                dtResultado = new DataTable();
            }
            return dtResultado;
        }

        public string CrearCategoria(string nombre, string descripcion = "")
        {
            string rpta = string.Empty;
            SqlConnection connection = DbConnection.Instance.GetConnection();
            try
            {
                using (SqlCommand command = new SqlCommand("SP06_CreateOrUpdateCategoria", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@id", DBNull.Value);
                    command.Parameters.AddWithValue("@nombre", nombre);
                    command.Parameters.AddWithValue("@descripcion", descripcion ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@activo", true);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            int id = Convert.ToInt32(reader["id"]);
                            rpta = id > 0 ? "OK" : reader["mensaje"]?.ToString() ?? "No se recibió mensaje del procedimiento";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                rpta = ex.Message;
            }
            return rpta;
        }

        #endregion
    }
}