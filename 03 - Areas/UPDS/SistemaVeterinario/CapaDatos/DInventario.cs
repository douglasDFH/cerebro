using System;
using System.Data;
using Microsoft.Data.SqlClient;

namespace CapaDatos
{
    public class DInventario
    {
        // Propiedades del producto
        private int _id;
        private string _codigo = string.Empty;
        private string _nombre = string.Empty;
        private string _descripcion = string.Empty;
        private decimal _precio;
        private int _stockActual;
        private int _stockMinimo;
        private int _stockMaximo;
        private bool _activo;

        // Propiedades de movimientos
        private int _idUbicacion;
        private string _nombreUbicacion = string.Empty;
        private int _cantidad;
        private string _tipoMovimiento = string.Empty;
        private string _motivo = string.Empty;

        // Propiedades auxiliares
        private string _textoBuscar = string.Empty;

        #region Propiedades Públicas
        public int Id { get => _id; set => _id = value; }
        public string Codigo { get => _codigo; set => _codigo = value; }
        public string Nombre { get => _nombre; set => _nombre = value; }
        public string Descripcion { get => _descripcion; set => _descripcion = value; }
        public decimal Precio { get => _precio; set => _precio = value; }
        public int StockActual { get => _stockActual; set => _stockActual = value; }
        public int StockMinimo { get => _stockMinimo; set => _stockMinimo = value; }
        public int StockMaximo { get => _stockMaximo; set => _stockMaximo = value; }
        public bool Activo { get => _activo; set => _activo = value; }

        // Propiedades de movimientos
        public int IdUbicacion { get => _idUbicacion; set => _idUbicacion = value; }
        public string NombreUbicacion { get => _nombreUbicacion; set => _nombreUbicacion = value; }
        public int Cantidad { get => _cantidad; set => _cantidad = value; }
        public string TipoMovimiento { get => _tipoMovimiento; set => _tipoMovimiento = value; }
        public string Motivo { get => _motivo; set => _motivo = value; }

        // Auxiliares
        public string TextoBuscar { get => _textoBuscar; set => _textoBuscar = value; }
        #endregion

        #region Constructores
        public DInventario()
        {
            _activo = true;
            _stockMinimo = 5;
            _stockActual = 0;
            _stockMaximo = 100;
        }

        public DInventario(string nombre, decimal precio)
        {
            _nombre = nombre;
            _precio = precio;
            _activo = true;
            _stockMinimo = 5;
            _stockActual = 0;
            _stockMaximo = 100;
        }
        #endregion

        #region Métodos CRUD

        public string InsertarProductoInventario(DInventario inventario)
        {
            string rpta = string.Empty;
            SqlConnection connection = DbConnection.Instance.GetConnection();

            try
            {
                using (SqlCommand command = new SqlCommand("SP_InsertarProductoInventario", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@nombre", inventario.Nombre ?? string.Empty);
                    command.Parameters.AddWithValue("@descripcion", inventario.Descripcion ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@precio", inventario.Precio);
                    command.Parameters.AddWithValue("@stock_actual", inventario.StockActual);
                    command.Parameters.AddWithValue("@stock_minimo", inventario.StockMinimo);
                    command.Parameters.AddWithValue("@stock_maximo", inventario.StockMaximo);

                    int result = command.ExecuteNonQuery();
                    rpta = result >= 1 ? "OK" : "Error al insertar producto";
                }
            }
            catch (Exception ex)
            {
                rpta = "Error: " + ex.Message;
            }

            return rpta;
        }

        public string ActualizarProductoInventario(DInventario inventario)
        {
            string rpta = string.Empty;
            SqlConnection connection = DbConnection.Instance.GetConnection();

            try
            {
                using (SqlCommand command = new SqlCommand("SP_ActualizarProductoInventario", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@id_producto", inventario.Id);
                    command.Parameters.AddWithValue("@nombre", inventario.Nombre ?? string.Empty);
                    command.Parameters.AddWithValue("@descripcion", inventario.Descripcion ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@precio", inventario.Precio);
                    command.Parameters.AddWithValue("@stock_minimo", inventario.StockMinimo);
                    command.Parameters.AddWithValue("@stock_maximo", inventario.StockMaximo);

                    int result = command.ExecuteNonQuery();
                    rpta = result >= 1 ? "OK" : "Error al actualizar producto";
                }
            }
            catch (Exception ex)
            {
                rpta = "Error: " + ex.Message;
            }

            return rpta;
        }

        public string RegistrarMovimientoInventario(DInventario movimiento)
        {
            string rpta = string.Empty;
            SqlConnection connection = DbConnection.Instance.GetConnection();

            try
            {
                using (SqlCommand command = new SqlCommand("SP_RegistrarMovimientoInventario", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@id_producto", movimiento.Id);
                    command.Parameters.AddWithValue("@tipo_movimiento", movimiento.TipoMovimiento);
                    command.Parameters.AddWithValue("@cantidad", movimiento.Cantidad);
                    command.Parameters.AddWithValue("@motivo", movimiento.Motivo);
                    command.Parameters.AddWithValue("@id_ubicacion", movimiento.IdUbicacion);

                    int result = command.ExecuteNonQuery();
                    rpta = result >= 1 ? "OK" : "Error al registrar movimiento";
                }
            }
            catch (Exception ex)
            {
                rpta = "Error: " + ex.Message;
            }

            return rpta;
        }

        public string EliminarProducto(DInventario inventario)
        {
            string rpta = string.Empty;
            SqlConnection connection = DbConnection.Instance.GetConnection();

            try
            {
                using (SqlCommand command = new SqlCommand("SP_EliminarProducto", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@id_producto", inventario.Id);

                    int result = command.ExecuteNonQuery();
                    rpta = result >= 1 ? "OK" : "Error al eliminar producto";
                }
            }
            catch (Exception ex)
            {
                rpta = "Error: " + ex.Message;
            }

            return rpta;
        }

        public DataTable MostrarInventario()
        {
            DataTable tabla = new DataTable();
            SqlConnection connection = DbConnection.Instance.GetConnection();

            try
            {
                using (SqlDataAdapter adapter = new SqlDataAdapter("SP_MostrarInventario", connection))
                {
                    adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                    adapter.Fill(tabla);
                }
            }
            catch (Exception)
            {
                tabla = new DataTable();
            }

            return tabla;
        }

        public DataTable BuscarProductoInventario(string textoBuscar)
        {
            DataTable tabla = new DataTable();
            SqlConnection connection = DbConnection.Instance.GetConnection();

            try
            {
                using (SqlDataAdapter adapter = new SqlDataAdapter("SP_BuscarProductoInventario", connection))
                {
                    adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                    adapter.SelectCommand.Parameters.AddWithValue("@texto", textoBuscar);
                    adapter.Fill(tabla);
                }
            }
            catch (Exception)
            {
                tabla = new DataTable();
            }

            return tabla;
        }

        public DataTable ObtenerProductosStockBajo()
        {
            DataTable tabla = new DataTable();
            SqlConnection connection = DbConnection.Instance.GetConnection();

            try
            {
                using (SqlDataAdapter adapter = new SqlDataAdapter("SP_ObtenerProductosStockBajo", connection))
                {
                    adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                    adapter.Fill(tabla);
                }
            }
            catch (Exception)
            {
                tabla = new DataTable();
            }

            return tabla;
        }

        public DataTable ObtenerMovimientosInventario(DateTime fechaInicio, DateTime fechaFin)
        {
            DataTable tabla = new DataTable();
            SqlConnection connection = DbConnection.Instance.GetConnection();

            try
            {
                using (SqlDataAdapter adapter = new SqlDataAdapter("SP_ObtenerMovimientosInventario", connection))
                {
                    adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                    adapter.SelectCommand.Parameters.AddWithValue("@fecha_inicio", fechaInicio);
                    adapter.SelectCommand.Parameters.AddWithValue("@fecha_fin", fechaFin);
                    adapter.Fill(tabla);
                }
            }
            catch (Exception)
            {
                tabla = new DataTable();
            }

            return tabla;
        }

        public DataTable ObtenerUbicacionesAlmacen()
        {
            DataTable tabla = new DataTable();
            SqlConnection connection = DbConnection.Instance.GetConnection();

            try
            {
                using (SqlDataAdapter adapter = new SqlDataAdapter("SP_ObtenerUbicaciones", connection))
                {
                    adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                    adapter.Fill(tabla);
                }
            }
            catch (Exception)
            {
                tabla = new DataTable();
            }

            return tabla;
        }

        public DataTable ObtenerStockPorUbicacion(int idProducto)
        {
            DataTable tabla = new DataTable();
            SqlConnection connection = DbConnection.Instance.GetConnection();

            try
            {
                using (SqlDataAdapter adapter = new SqlDataAdapter("SP_ObtenerStockPorUbicacion", connection))
                {
                    adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                    adapter.SelectCommand.Parameters.AddWithValue("@id_producto", idProducto);
                    adapter.Fill(tabla);
                }
            }
            catch (Exception)
            {
                tabla = new DataTable();
            }

            return tabla;
        }

        #endregion
    }
}