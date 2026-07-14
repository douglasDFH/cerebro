using CapaDatos;
using System;
using System.Data;

namespace CapaNegocio
{
    public class NInventario
    {
        public static string InsertarProductoInventario(string nombre, string descripcion, decimal precio, int stockActual, int stockMinimo, int stockMaximo)
        {
            DInventario obj = new DInventario();
            obj.Nombre = nombre;
            obj.Descripcion = descripcion;
            obj.Precio = precio;
            obj.StockActual = stockActual;
            obj.StockMinimo = stockMinimo;
            obj.StockMaximo = stockMaximo;

            if (ValidarDatosProducto(obj))
            {
                return obj.InsertarProductoInventario(obj);
            }
            else
            {
                return "Datos inválidos";
            }
        }

        public static string ActualizarProductoInventario(int idProducto, string nombre, string descripcion, decimal precio, int stockMinimo, int stockMaximo)
        {
            DInventario obj = new DInventario();
            obj.Id = idProducto;
            obj.Nombre = nombre;
            obj.Descripcion = descripcion;
            obj.Precio = precio;
            obj.StockMinimo = stockMinimo;
            obj.StockMaximo = stockMaximo;

            if (ValidarDatosProducto(obj))
            {
                return obj.ActualizarProductoInventario(obj);
            }
            else
            {
                return "Datos inválidos";
            }
        }

        public static string RegistrarMovimientoInventario(int idProducto, string tipoMovimiento, int cantidad, string motivo, int idUbicacion = 1)
        {
            if (ValidarMovimiento(tipoMovimiento, cantidad, motivo))
            {
                DInventario obj = new DInventario();
                obj.Id = idProducto;
                obj.TipoMovimiento = tipoMovimiento;
                obj.Cantidad = cantidad;
                obj.Motivo = motivo;
                obj.IdUbicacion = idUbicacion;

                return obj.RegistrarMovimientoInventario(obj);
            }
            else
            {
                return "Datos de movimiento inválidos";
            }
        }

        public static string EliminarProducto(int idProducto)
        {
            DInventario obj = new DInventario();
            obj.Id = idProducto;
            return obj.EliminarProducto(obj);
        }

        public static DataTable MostrarInventario()
        {
            DInventario obj = new DInventario();
            return obj.MostrarInventario();
        }

        public static DataTable BuscarProductoInventario(string textoBuscar)
        {
            if (string.IsNullOrWhiteSpace(textoBuscar))
            {
                return MostrarInventario();
            }

            DInventario obj = new DInventario();
            return obj.BuscarProductoInventario(textoBuscar);
        }

        public static DataTable ObtenerProductosStockBajo()
        {
            DInventario obj = new DInventario();
            return obj.ObtenerProductosStockBajo();
        }

        public static DataTable ObtenerMovimientosInventario(DateTime fechaInicio, DateTime fechaFin)
        {
            if (fechaInicio > fechaFin)
            {
                return new DataTable();
            }

            DInventario obj = new DInventario();
            return obj.ObtenerMovimientosInventario(fechaInicio, fechaFin);
        }

        public static DataTable ObtenerUbicacionesAlmacen()
        {
            DInventario obj = new DInventario();
            return obj.ObtenerUbicacionesAlmacen();
        }

        public static DataTable ObtenerStockPorUbicacion(int idProducto)
        {
            DInventario obj = new DInventario();
            return obj.ObtenerStockPorUbicacion(idProducto);
        }

        public static bool ValidarDatosProducto(DInventario inventario)
        {
            if (string.IsNullOrWhiteSpace(inventario.Nombre))
                return false;

            if (inventario.Nombre.Length > 100)
                return false;

            if (inventario.Precio <= 0)
                return false;

            if (inventario.StockMinimo < 0)
                return false;

            if (inventario.StockMaximo < inventario.StockMinimo)
                return false;

            if (inventario.StockActual < 0)
                return false;

            return true;
        }

        public static bool ValidarMovimiento(string tipoMovimiento, int cantidad, string motivo)
        {
            if (string.IsNullOrWhiteSpace(tipoMovimiento))
                return false;

            if (tipoMovimiento != "ENTRADA" && tipoMovimiento != "SALIDA")
                return false;

            if (cantidad <= 0)
                return false;

            if (string.IsNullOrWhiteSpace(motivo))
                return false;

            if (motivo.Length > 200)
                return false;

            return true;
        }

        public static bool ValidarStock(int stockActual, int stockMinimo)
        {
            return stockActual >= stockMinimo;
        }

        public static string ObtenerMensajeAlertaStock(string nombreProducto, int stockActual, int stockMinimo)
        {
            if (stockActual <= 0)
            {
                return $"¡ALERTA! El producto '{nombreProducto}' está agotado.";
            }
            else if (stockActual <= stockMinimo)
            {
                return $"¡ATENCIÓN! El producto '{nombreProducto}' tiene stock bajo ({stockActual} unidades).";
            }

            return "";
        }
    }
}