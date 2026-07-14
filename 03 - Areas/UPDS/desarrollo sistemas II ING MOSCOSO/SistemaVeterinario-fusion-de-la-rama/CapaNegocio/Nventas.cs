using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDatos;

namespace CapaNegocio
{
    public class NVentas
    {
        public static string Insertar(int personaId, string numeroFactura, DateTime? fechaVencimiento,
            string notas, string productosJson, string serviciosJson, decimal impuestos, 
            decimal descuentos, bool finalizar)
        {
            DVentas objVenta = new DVentas()
            {
                PersonaId = personaId,
                NumeroFactura = numeroFactura,
                FechaVencimiento = fechaVencimiento,
                Notas = notas,
                ProductosJson = productosJson,
                ServiciosJson = serviciosJson,
                Impuestos = impuestos,
                Descuentos = descuentos,
                Estado = "Pendiente",
                Finalizar = finalizar
            };
            return objVenta.Insertar(objVenta);
        }

        public static string Editar(int idFactura, int personaId, string numeroFactura, 
            DateTime? fechaVencimiento, string notas, string productosJson, string serviciosJson, 
            decimal impuestos, decimal descuentos, string estado, bool finalizar)
        {
            DVentas objVenta = new DVentas()
            {
                IdFactura = idFactura,
                PersonaId = personaId,
                NumeroFactura = numeroFactura,
                FechaVencimiento = fechaVencimiento,
                Notas = notas,
                ProductosJson = productosJson,
                ServiciosJson = serviciosJson,
                Impuestos = impuestos,
                Descuentos = descuentos,
                Estado = estado,
                Finalizar = finalizar
            };
            return objVenta.Editar(objVenta);
        }

        public static DataTable Mostrar()
        {
            return new DVentas().Mostrar();
        }

        public static DataTable BuscarPorPersona(int personaId)
        {
            DVentas objVenta = new DVentas()
            {
                PersonaId = personaId
            };
            return objVenta.BuscarPorPersona(objVenta);
        }

        public static DataTable ObtenerDetalle(int idFactura)
        {
            DVentas objVenta = new DVentas()
            {
                IdFactura = idFactura
            };
            return objVenta.ObtenerDetalle(objVenta);
        }

        public static bool ValidarFactura(string numeroFactura)
        {
            if (string.IsNullOrWhiteSpace(numeroFactura))
                return false;
            
            if (numeroFactura.Length < 5)
                return false;
                
            // Validar formato básico (solo letras, números y guiones)
            return System.Text.RegularExpressions.Regex.IsMatch(numeroFactura, @"^[A-Za-z0-9\-]+$");
        }

        public static bool ValidarPersonaExiste(int personaId)
        {
            // Verificar que la persona existe antes de crear factura
            try
            {
                var facturas = BuscarPorPersona(personaId);
                return facturas != null;
            }
            catch
            {
                return false;
            }
        }

        public static string CrearFacturaCompleta(int personaId, string numeroFactura, 
            DateTime? fechaVencimiento, string notas, string productosJson, string serviciosJson, 
            decimal impuestos, decimal descuentos, bool finalizar = false)
        {
            // Validaciones de negocio
            if (!ValidarFactura(numeroFactura))
                return "Error: Número de factura inválido";
                
            if (!ValidarPersonaExiste(personaId))
                return "Error: La persona especificada no existe";

            return Insertar(personaId, numeroFactura, fechaVencimiento, notas, 
                productosJson, serviciosJson, impuestos, descuentos, finalizar);
        }

        // ============================================
        // MÉTODOS PARA REPORTES DE VENTAS
        // ============================================

        public static DataTable ReporteVentasPorRango(DateTime fechaInicio, DateTime fechaFin, string estado = null)
        {
            return new DVentas().ReporteVentasPorRango(fechaInicio, fechaFin, estado);
        }

        public static DataTable ReporteVentasResumen(DateTime fechaInicio, DateTime fechaFin, string agrupacion = "DIA")
        {
            return new DVentas().ReporteVentasResumen(fechaInicio, fechaFin, agrupacion);
        }

        public static DataTable ReporteVentasDetalle(DateTime fechaInicio, DateTime fechaFin, int? facturaId = null)
        {
            return new DVentas().ReporteVentasDetalle(fechaInicio, fechaFin, facturaId);
        }

        public static DataTable ReporteVentasPeriodosPredefinidos(string periodo)
        {
            return new DVentas().ReporteVentasPeriodosPredefinidos(periodo);
        }

        public static DataTable ReporteVentasTopClientes(DateTime fechaInicio, DateTime fechaFin, int topCount = 10)
        {
            return new DVentas().ReporteVentasTopClientes(fechaInicio, fechaFin, topCount);
        }

        public static DataTable ReporteVentasProductosTop(DateTime fechaInicio, DateTime fechaFin, int topCount = 10)
        {
            return new DVentas().ReporteVentasProductosTop(fechaInicio, fechaFin, topCount);
        }

        public static DataTable ReporteVentasServiciosTop(DateTime fechaInicio, DateTime fechaFin, int topCount = 10)
        {
            return new DVentas().ReporteVentasServiciosTop(fechaInicio, fechaFin, topCount);
        }

        public static DataTable ReporteVentasEstadisticasGenerales()
        {
            return new DVentas().ReporteVentasEstadisticasGenerales();
        }

        // ============================================
        // MÉTODOS UTILITARIOS PARA REPORTES
        // ============================================

        public static List<string> ObtenerPeriodosPredefinidos()
        {
            return new List<string>
            {
                "HOY",
                "AYER", 
                "ULTIMOS_7_DIAS",
                "MES_ACTUAL",
                "ULTIMOS_30_DIAS",
                "AÑO_ACTUAL"
            };
        }

        public static List<string> ObtenerTiposAgrupacion()
        {
            return new List<string>
            {
                "DIA",
                "SEMANA", 
                "MES",
                "AÑO"
            };
        }

        public static string ObtenerNombrePeriodo(string periodo)
        {
            return periodo switch
            {
                "HOY" => "Ventas de Hoy",
                "AYER" => "Ventas de Ayer", 
                "ULTIMOS_7_DIAS" => "Últimos 7 Días",
                "MES_ACTUAL" => "Mes Actual",
                "ULTIMOS_30_DIAS" => "Últimos 30 Días",
                "AÑO_ACTUAL" => "Año Actual",
                _ => "Período Personalizado"
            };
        }

        public static (DateTime fechaInicio, DateTime fechaFin) CalcularFechasPeriodo(string periodo)
        {
            DateTime hoy = DateTime.Now.Date;
            
            return periodo switch
            {
                "HOY" => (hoy, hoy),
                "AYER" => (hoy.AddDays(-1), hoy.AddDays(-1)),
                "ULTIMOS_7_DIAS" => (hoy.AddDays(-6), hoy),
                "MES_ACTUAL" => (new DateTime(hoy.Year, hoy.Month, 1), hoy),
                "ULTIMOS_30_DIAS" => (hoy.AddDays(-29), hoy),
                "AÑO_ACTUAL" => (new DateTime(hoy.Year, 1, 1), hoy),
                _ => (hoy.AddMonths(-1), hoy)
            };
        }
    }
}
