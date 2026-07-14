using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class DVentas : DbConnection
    {
        private int _idFactura;
        private string _numeroFactura;
        private DateTime _fechaEmision;
        private DateTime? _fechaVencimiento;
        private int _personaId;
        private decimal _subtotal;
        private decimal _impuestos;
        private decimal _descuentos;
        private decimal _total;
        private string _estado;
        private string _notas;
        private string _productosJson;
        private string _serviciosJson;
        private bool _finalizar;

        public int IdFactura { get => _idFactura; set => _idFactura = value; }
        public string NumeroFactura { get => _numeroFactura; set => _numeroFactura = value; }
        public DateTime FechaEmision { get => _fechaEmision; set => _fechaEmision = value; }
        public DateTime? FechaVencimiento { get => _fechaVencimiento; set => _fechaVencimiento = value; }
        public int PersonaId { get => _personaId; set => _personaId = value; }
        public decimal Subtotal { get => _subtotal; set => _subtotal = value; }
        public decimal Impuestos { get => _impuestos; set => _impuestos = value; }
        public decimal Descuentos { get => _descuentos; set => _descuentos = value; }
        public decimal Total { get => _total; set => _total = value; }
        public string Estado { get => _estado; set => _estado = value; }
        public string Notas { get => _notas; set => _notas = value; }
        public string ProductosJson { get => _productosJson; set => _productosJson = value; }
        public string ServiciosJson { get => _serviciosJson; set => _serviciosJson = value; }
        public bool Finalizar { get => _finalizar; set => _finalizar = value; }

        public DVentas() { }

        public DVentas(int idFactura, string numeroFactura, int personaId, 
            string estado = "Pendiente", decimal impuestos = 0, decimal descuentos = 0)
        {
            IdFactura = idFactura;
            NumeroFactura = numeroFactura;
            PersonaId = personaId;
            Estado = estado;
            Impuestos = impuestos;
            Descuentos = descuentos;
            FechaEmision = DateTime.Now;
        }

        public string Insertar(DVentas venta)
        {
            string rpta = string.Empty;
            using (SqlConnection connection = GetConnection())
            {
                connection.Open();
                try
                {
                    using (SqlCommand command = new SqlCommand("SP_SaveFactura", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        
                        command.Parameters.AddWithValue("@factura_id", DBNull.Value);
                        command.Parameters.AddWithValue("@persona_id", venta.PersonaId);
                        command.Parameters.AddWithValue("@numero_factura", venta.NumeroFactura ?? "");
                        command.Parameters.AddWithValue("@fecha_vencimiento", venta.FechaVencimiento ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@notas", venta.Notas ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@productos", venta.ProductosJson ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@servicios", venta.ServiciosJson ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@impuestos", venta.Impuestos);
                        command.Parameters.AddWithValue("@descuentos", venta.Descuentos);
                        command.Parameters.AddWithValue("@estado", venta.Estado ?? "Pendiente");
                        command.Parameters.AddWithValue("@finalizar", venta.Finalizar);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                int facturaId = Convert.ToInt32(reader["factura_id"]);
                                if (facturaId > 0)
                                {
                                    venta.IdFactura = facturaId;
                                    rpta = "OK";
                                }
                                else
                                {
                                    rpta = reader["mensaje"].ToString();
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    rpta = ex.Message;
                }
            }
            return rpta;
        }

        public string Editar(DVentas venta)
        {
            string rpta = string.Empty;
            using (SqlConnection connection = GetConnection())
            {
                connection.Open();
                try
                {
                    using (SqlCommand command = new SqlCommand("SP_SaveFactura", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        
                        command.Parameters.AddWithValue("@factura_id", venta.IdFactura);
                        command.Parameters.AddWithValue("@persona_id", venta.PersonaId);
                        command.Parameters.AddWithValue("@numero_factura", venta.NumeroFactura ?? "");
                        command.Parameters.AddWithValue("@fecha_vencimiento", venta.FechaVencimiento ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@notas", venta.Notas ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@productos", venta.ProductosJson ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@servicios", venta.ServiciosJson ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@impuestos", venta.Impuestos);
                        command.Parameters.AddWithValue("@descuentos", venta.Descuentos);
                        command.Parameters.AddWithValue("@estado", venta.Estado ?? "Pendiente");
                        command.Parameters.AddWithValue("@finalizar", venta.Finalizar);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                string mensaje = reader["mensaje"].ToString();
                                rpta = mensaje.Contains("OK") ? "OK" : mensaje;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    rpta = ex.Message;
                }
            }
            return rpta;
        }

        public DataTable Mostrar()
        {
            DataTable dtResultado = new DataTable("Facturas");
            using (SqlConnection connection = GetConnection())
            {
                connection.Open();
                try
                {
                    string query = @"SELECT f.id, f.numero_factura, f.fecha_emision, f.fecha_vencimiento,
                                            f.subtotal, f.impuestos, f.descuentos, f.total, f.estado, f.notas,
                                            p.nombre_mostrar as cliente
                                     FROM factura f
                                     LEFT JOIN VW_PersonasCompletas p ON f.persona_id = p.id
                                     ORDER BY f.fecha_emision DESC";
                    
                    using (SqlCommand command = new SqlCommand(query, connection))
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(dtResultado);
                    }
                }
                catch
                {
                    dtResultado = null;
                }
            }
            return dtResultado;
        }

        public DataTable BuscarPorPersona(DVentas venta)
        {
            DataTable dtResultado = new DataTable("FacturasPorPersona");
            using (SqlConnection connection = GetConnection())
            {
                connection.Open();
                try
                {
                    string query = @"SELECT f.id, f.numero_factura, f.fecha_emision, f.fecha_vencimiento,
                                            f.subtotal, f.impuestos, f.descuentos, f.total, f.estado, f.notas,
                                            p.nombre_mostrar as cliente
                                     FROM factura f
                                     LEFT JOIN VW_PersonasCompletas p ON f.persona_id = p.id
                                     WHERE f.persona_id = @PersonaId
                                     ORDER BY f.fecha_emision DESC";
                    
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@PersonaId", venta.PersonaId);
                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            adapter.Fill(dtResultado);
                        }
                    }
                }
                catch
                {
                    dtResultado = null;
                }
            }
            return dtResultado;
        }

        public DataTable ObtenerDetalle(DVentas venta)
        {
            DataTable dtResultado = new DataTable("DetalleFactura");
            using (SqlConnection connection = GetConnection())
            {
                connection.Open();
                try
                {
                    string query = @"SELECT 
                                        f.*,
                                        p.nombre_mostrar as cliente,
                                        dp.producto_id,
                                        pr.nombre as producto_nombre,
                                        dp.cantidad as producto_cantidad,
                                        dp.precio_unitario as producto_precio,
                                        ds.diagnostico_id,
                                        d.nombre as servicio_nombre,
                                        ds.cantidad as servicio_cantidad,
                                        ds.precio_unitario as servicio_precio
                                    FROM factura f
                                    LEFT JOIN VW_PersonasCompletas p ON f.persona_id = p.id
                                    LEFT JOIN detalle_productos dp ON f.id = dp.factura_id
                                    LEFT JOIN producto pr ON dp.producto_id = pr.id
                                    LEFT JOIN detalle_servicios ds ON f.id = ds.factura_id
                                    LEFT JOIN diagnostico d ON ds.diagnostico_id = d.id
                                    WHERE f.id = @IdFactura";
                    
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@IdFactura", venta.IdFactura);
                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            adapter.Fill(dtResultado);
                        }
                    }
                }
                catch
                {
                    dtResultado = null;
                }
            }
            return dtResultado;
        }

        // ============================================
        // MÉTODOS PARA REPORTES DE VENTAS
        // ============================================

        public DataTable ReporteVentasPorRango(DateTime fechaInicio, DateTime fechaFin, string estado = null)
        {
            DataTable dtResultado = new DataTable("ReporteVentasPorRango");
            using (SqlConnection connection = GetConnection())
            {
                connection.Open();
                try
                {
                    using (SqlCommand command = new SqlCommand("SP_ReporteVentasPorRango", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@fecha_inicio", fechaInicio.Date);
                        command.Parameters.AddWithValue("@fecha_fin", fechaFin.Date);
                        command.Parameters.AddWithValue("@estado", estado ?? (object)DBNull.Value);

                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            adapter.Fill(dtResultado);
                        }
                    }
                }
                catch
                {
                    dtResultado = null;
                }
            }
            return dtResultado;
        }

        public DataTable ReporteVentasResumen(DateTime fechaInicio, DateTime fechaFin, string agrupacion = "DIA")
        {
            DataTable dtResultado = new DataTable("ReporteVentasResumen");
            using (SqlConnection connection = GetConnection())
            {
                connection.Open();
                try
                {
                    using (SqlCommand command = new SqlCommand("SP_ReporteVentasResumen", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@fecha_inicio", fechaInicio.Date);
                        command.Parameters.AddWithValue("@fecha_fin", fechaFin.Date);
                        command.Parameters.AddWithValue("@agrupacion", agrupacion);

                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            adapter.Fill(dtResultado);
                        }
                    }
                }
                catch
                {
                    dtResultado = null;
                }
            }
            return dtResultado;
        }

        public DataTable ReporteVentasDetalle(DateTime fechaInicio, DateTime fechaFin, int? facturaId = null)
        {
            DataTable dtResultado = new DataTable("ReporteVentasDetalle");
            using (SqlConnection connection = GetConnection())
            {
                connection.Open();
                try
                {
                    using (SqlCommand command = new SqlCommand("SP_ReporteVentasDetalle", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@fecha_inicio", fechaInicio.Date);
                        command.Parameters.AddWithValue("@fecha_fin", fechaFin.Date);
                        command.Parameters.AddWithValue("@factura_id", facturaId ?? (object)DBNull.Value);

                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            adapter.Fill(dtResultado);
                        }
                    }
                }
                catch
                {
                    dtResultado = null;
                }
            }
            return dtResultado;
        }

        public DataTable ReporteVentasPeriodosPredefinidos(string periodo)
        {
            DataTable dtResultado = new DataTable("ReporteVentasPeriodosPredefinidos");
            using (SqlConnection connection = GetConnection())
            {
                connection.Open();
                try
                {
                    using (SqlCommand command = new SqlCommand("SP_ReporteVentasPeriodosPredefinidos", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@periodo", periodo);

                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            adapter.Fill(dtResultado);
                        }
                    }
                }
                catch
                {
                    dtResultado = null;
                }
            }
            return dtResultado;
        }

        public DataTable ReporteVentasTopClientes(DateTime fechaInicio, DateTime fechaFin, int topCount = 10)
        {
            DataTable dtResultado = new DataTable("ReporteVentasTopClientes");
            using (SqlConnection connection = GetConnection())
            {
                connection.Open();
                try
                {
                    using (SqlCommand command = new SqlCommand("SP_ReporteVentasTopClientes", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@fecha_inicio", fechaInicio.Date);
                        command.Parameters.AddWithValue("@fecha_fin", fechaFin.Date);
                        command.Parameters.AddWithValue("@top_count", topCount);

                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            adapter.Fill(dtResultado);
                        }
                    }
                }
                catch
                {
                    dtResultado = null;
                }
            }
            return dtResultado;
        }

        public DataTable ReporteVentasProductosTop(DateTime fechaInicio, DateTime fechaFin, int topCount = 10)
        {
            DataTable dtResultado = new DataTable("ReporteVentasProductosTop");
            using (SqlConnection connection = GetConnection())
            {
                connection.Open();
                try
                {
                    using (SqlCommand command = new SqlCommand("SP_ReporteVentasProductosTop", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@fecha_inicio", fechaInicio.Date);
                        command.Parameters.AddWithValue("@fecha_fin", fechaFin.Date);
                        command.Parameters.AddWithValue("@top_count", topCount);

                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            adapter.Fill(dtResultado);
                        }
                    }
                }
                catch
                {
                    dtResultado = null;
                }
            }
            return dtResultado;
        }

        public DataTable ReporteVentasServiciosTop(DateTime fechaInicio, DateTime fechaFin, int topCount = 10)
        {
            DataTable dtResultado = new DataTable("ReporteVentasServiciosTop");
            using (SqlConnection connection = GetConnection())
            {
                connection.Open();
                try
                {
                    using (SqlCommand command = new SqlCommand("SP_ReporteVentasServiciosTop", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@fecha_inicio", fechaInicio.Date);
                        command.Parameters.AddWithValue("@fecha_fin", fechaFin.Date);
                        command.Parameters.AddWithValue("@top_count", topCount);

                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            adapter.Fill(dtResultado);
                        }
                    }
                }
                catch
                {
                    dtResultado = null;
                }
            }
            return dtResultado;
        }

        public DataTable ReporteVentasEstadisticasGenerales()
        {
            DataTable dtResultado = new DataTable("ReporteVentasEstadisticasGenerales");
            using (SqlConnection connection = GetConnection())
            {
                connection.Open();
                try
                {
                    using (SqlCommand command = new SqlCommand("SP_ReporteVentasEstadisticasGenerales", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            adapter.Fill(dtResultado);
                        }
                    }
                }
                catch
                {
                    dtResultado = null;
                }
            }
            return dtResultado;
        }
    }
}
