using CapaDatos;
using System;
using System.Collections.Generic;
using System.Data;

namespace CapaNegocio
{
    public class Norders
    {
        // Método para insertar una orden
        public static string Insertar(int customer_id, int usuario_id, DataTable dtDetalles)
        {
            Dorders Obj = new Dorders();
            Obj.Customer_id = customer_id;
            Obj.Usuario_id = usuario_id;
            List<Dorder_items> detalles = new List<Dorder_items>();

            foreach (DataRow row in dtDetalles.Rows)
            {
                Dorder_items dorder = new Dorder_items();
                dorder.Product_id = Convert.ToInt32(row["product_id"].ToString());
                dorder.Quantity = Convert.ToInt32(row["quantity"].ToString());
                dorder.Price = Convert.ToDecimal(row["price"].ToString());
                dorder.Discount = Convert.ToDecimal(row["discount"].ToString());
                detalles.Add(dorder);
            }
            return Obj.Insertar(Obj, detalles);
        }

        // Método para eliminar una orden
        public static string Eliminar(int order_id)
        {
            Dorders Obj = new Dorders();
            Obj.Order_id = order_id;
            return Obj.Eliminar(Obj);
        }

        // Método para mostrar todas las órdenes
        public static DataTable Mostrar()
        {
            return new Dorders().Mostrar();
        }

        // Método para buscar órdenes por fecha
        public static DataTable BuscarFecha(string textobuscar1, string textobuscar2)
        {
            Dorders Obj = new Dorders();
            return Obj.BuscarFecha(textobuscar1, textobuscar2);
        }

        // Método para mostrar los detalles de una orden
        public static DataTable MostrarDetalle(string textobuscar)
        {
            Dorders Obj = new Dorders();
            return Obj.MostrarDetalle(textobuscar);
        }

        // Métodos para reportes

        public static DataTable ReportePedidosPorCliente(DateTime fechaInicio, DateTime fechaFin)
        {
            try
            {
                return new Dorders().ReportePedidosPorCliente(fechaInicio, fechaFin);
            }
            catch (Exception ex)
            {
                throw new Exception("Error en capa negocio: " + ex.Message);
            }
        }

        public static DataTable ReporteProductosMasVendidos(DateTime fechaInicio, DateTime fechaFin)
        {
            try
            {
                return new Dorders().ReporteProductosMasVendidos(fechaInicio, fechaFin);
            }
            catch (Exception ex)
            {
                throw new Exception("Error en capa negocio: " + ex.Message);
            }
        }

        // Método adicional para reporte de ventas por período
        public static DataTable ReporteVentasPorPeriodo(string tipoPeriodo, DateTime fechaInicio, DateTime fechaFin)
        {
            try
            {
                return new Dorders().ReporteVentasPorPeriodo(tipoPeriodo, fechaInicio, fechaFin);
            }
            catch (Exception ex)
            {
                throw new Exception("Error en capa negocio: " + ex.Message);
            }
        }
    }
}