using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;

namespace CapaDatos
{
    public class Dorders : DbConnection
    {
        // Variables
        private int _order_id;
        private int _customer_id;
        private int _usuario_id;

        // Propiedades
        public int Order_id { get => _order_id; set => _order_id = value; }
        public int Customer_id { get => _customer_id; set => _customer_id = value; }
        public int Usuario_id { get => _usuario_id; set => _usuario_id = value; }

        // Constructores
        public Dorders() { }
        public Dorders(int order_id, int customer_id, int usuario_id)
        {
            Order_id = order_id;
            Customer_id = customer_id;
            Usuario_id = usuario_id;
        }

        // Método para insertar una orden
        public string Insertar(Dorders Orders, List<Dorder_items> Detalle)
        {
            string rpta = string.Empty;
            SqlConnection sqlCon = new SqlConnection();
            try
            {
                sqlCon.ConnectionString = DbConnection.cn;
                sqlCon.Open();
                SqlTransaction sqlTra = sqlCon.BeginTransaction();

                SqlCommand sqlCmd = new SqlCommand();
                sqlCmd.Connection = sqlCon;
                sqlCmd.Transaction = sqlTra;
                sqlCmd.CommandText = "spinsertar_orders";
                sqlCmd.CommandType = CommandType.StoredProcedure;

                SqlParameter ParOrder_id = new SqlParameter("@order_id", SqlDbType.Int);
                ParOrder_id.Direction = ParameterDirection.Output;
                sqlCmd.Parameters.Add(ParOrder_id);

                sqlCmd.Parameters.AddWithValue("@customer_id", Orders.Customer_id);
                sqlCmd.Parameters.AddWithValue("@usuario_id", Orders.Usuario_id);

                rpta = sqlCmd.ExecuteNonQuery() == 1 ? "OK" : "NO SE INGRESO EL REGISTRO";

                if (rpta.Equals("OK"))
                {
                    Order_id = Convert.ToInt32(sqlCmd.Parameters["@order_id"].Value);
                    foreach (Dorder_items det in Detalle)
                    {
                        det.Order_id = Order_id;
                        rpta = det.Insertar(det, ref sqlCon, ref sqlTra);
                        if (!rpta.Equals("OK")) break;
                    }
                }

                if (rpta.Equals("OK")) sqlTra.Commit();
                else sqlTra.Rollback();
            }
            catch (Exception ex)
            {
                rpta = ex.Message;
            }
            finally
            {
                if (sqlCon.State == ConnectionState.Open) sqlCon.Close();
            }
            return rpta;
        }

        // Método para eliminar una orden
        // Método para eliminar una orden y sus detalles
        public string Eliminar(Dorders Orders)
        {
            string rpta = string.Empty;

            using (SqlConnection sqlCon = GetConnection())
            {
                sqlCon.Open();
                SqlTransaction sqlTran = sqlCon.BeginTransaction();

                try
                {
                    // 1. Eliminar detalles (order_items)
                    SqlCommand cmdDetalle = new SqlCommand("DELETE FROM order_items WHERE order_id = @order_id", sqlCon, sqlTran);
                    cmdDetalle.Parameters.AddWithValue("@order_id", Orders.Order_id);
                    cmdDetalle.ExecuteNonQuery();

                    // 2. Eliminar orden
                    SqlCommand cmdOrden = new SqlCommand("DELETE FROM orders WHERE order_id = @order_id", sqlCon, sqlTran);
                    cmdOrden.Parameters.AddWithValue("@order_id", Orders.Order_id);
                    int filas = cmdOrden.ExecuteNonQuery();

                    if (filas == 1)
                    {
                        sqlTran.Commit();
                        rpta = "OK";
                    }
                    else
                    {
                        sqlTran.Rollback();
                        rpta = "NO SE ELIMINO EL REGISTRO";
                    }
                }
                catch (Exception ex)
                {
                    sqlTran.Rollback();
                    rpta = "Error al eliminar: " + ex.Message;
                }
            }

            return rpta;
        }


        // Método para mostrar todas las órdenes
        public DataTable Mostrar()
        {
            DataTable DtResultado = new DataTable("order");
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new SqlCommand())
                {
                    try
                    {
                        command.Connection = connection;
                        command.CommandText = "spmostrar_order";
                        command.CommandType = CommandType.StoredProcedure;
                        SqlDataAdapter sqlDat = new SqlDataAdapter(command);
                        sqlDat.Fill(DtResultado);
                    }
                    catch (Exception)
                    {
                        DtResultado = null;
                    }
                }
            }
            return DtResultado;
        }

        // Método para buscar órdenes por fecha
        public DataTable BuscarFecha(string TextoBuscar1, string TextoBuscar2)
        {
            DataTable DtResultado = new DataTable("order");
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new SqlCommand())
                {
                    try
                    {
                        command.Connection = connection;
                        command.CommandText = "spbuscar_order_fecha";
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@textobuscar1", TextoBuscar1);
                        command.Parameters.AddWithValue("@textobuscar2", TextoBuscar2);
                        SqlDataAdapter sqlDat = new SqlDataAdapter(command);
                        sqlDat.Fill(DtResultado);
                    }
                    catch (Exception)
                    {
                        DtResultado = null;
                    }
                }
            }
            return DtResultado;
        }

        // Método para mostrar los detalles de una orden
        public DataTable MostrarDetalle(string TextoBuscar)
        {
            DataTable DtResultado = new DataTable("order_items");
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new SqlCommand())
                {
                    try
                    {
                        command.Connection = connection;
                        command.CommandText = "spmostrar_order_items";
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@textobuscar", TextoBuscar);
                        SqlDataAdapter sqlDat = new SqlDataAdapter(command);
                        sqlDat.Fill(DtResultado);
                    }
                    catch (Exception)
                    {
                        DtResultado = null;
                    }
                }
            }
            return DtResultado;
        }

        // Métodos para reportes

        public DataTable ReportePedidosPorCliente(DateTime fechaInicio, DateTime fechaFin)
        {
            DataTable DtResultado = new DataTable("reporte_pedidos");
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new SqlCommand())
                {
                    try
                    {
                        command.Connection = connection;
                        command.CommandText = "sp_reporte_pedidos_por_cliente";
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@fecha_inicio", fechaInicio);
                        command.Parameters.AddWithValue("@fecha_fin", fechaFin);
                        SqlDataAdapter sqlDat = new SqlDataAdapter(command);
                        sqlDat.Fill(DtResultado);
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Error al generar reporte: " + ex.Message);
                    }
                }
            }
            return DtResultado;
        }

        public DataTable ReporteProductosMasVendidos(DateTime fechaInicio, DateTime fechaFin)
        {
            DataTable DtResultado = new DataTable("reporte_productos");
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new SqlCommand())
                {
                    try
                    {
                        command.Connection = connection;
                        command.CommandText = "sp_reporte_productos_mas_vendidos";
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@fecha_inicio", fechaInicio);
                        command.Parameters.AddWithValue("@fecha_fin", fechaFin);
                        SqlDataAdapter sqlDat = new SqlDataAdapter(command);
                        sqlDat.Fill(DtResultado);
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Error al generar reporte: " + ex.Message);
                    }
                }
            }
            return DtResultado;
        }
        // Agrega este método a la clase Dorders
        public DataTable ReporteVentasPorPeriodo(string tipoPeriodo, DateTime fechaInicio, DateTime fechaFin)
        {
            DataTable DtResultado = new DataTable("reporte_ventas");
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new SqlCommand())
                {
                    try
                    {
                        command.Connection = connection;
                        command.CommandText = "sp_reporte_ventas_por_periodo";
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@tipo_periodo", tipoPeriodo);
                        command.Parameters.AddWithValue("@fecha_inicio", fechaInicio);
                        command.Parameters.AddWithValue("@fecha_fin", fechaFin);

                        SqlDataAdapter sqlDat = new SqlDataAdapter(command);
                        sqlDat.Fill(DtResultado);
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Error al generar reporte de ventas: " + ex.Message);
                    }
                }
            }
            return DtResultado;
        }
    }
}