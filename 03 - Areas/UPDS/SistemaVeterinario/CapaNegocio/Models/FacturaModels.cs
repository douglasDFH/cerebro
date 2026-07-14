using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace CapaNegocio.Models
{
    /// <summary>
    /// Estructura completa de una factura con todos sus datos principales y detalles
    /// </summary>
    [XmlRoot("Factura")]
    public class FacturaCompleta
    {
        public FacturaDatosPrincipales DatosPrincipales { get; set; }
        public List<FacturaDetalleProducto> DetalleProductos { get; set; }
        public List<FacturaDetalleServicio> DetalleServicios { get; set; }

        public FacturaCompleta()
        {
            DetalleProductos = new List<FacturaDetalleProducto>();
            DetalleServicios = new List<FacturaDetalleServicio>();
        }
    }

    /// <summary>
    /// Datos principales de la factura (resultado de sp_factura_datos_principales)
    /// </summary>
    public class FacturaDatosPrincipales
    {
        // Datos de la factura
        public int FacturaId { get; set; }
        public string NumeroFactura { get; set; }
        public DateTime FechaEmision { get; set; }
        public DateTime? FechaVencimiento { get; set; }
        public decimal Subtotal { get; set; }
        public decimal Impuestos { get; set; }
        public decimal Descuentos { get; set; }
        public decimal Total { get; set; }
        public string Estado { get; set; }
        public string Notas { get; set; }

        // Datos del cliente (persona)
        public int ClienteId { get; set; }
        public string ClienteTipo { get; set; }
        public string ClienteEmail { get; set; }
        public string ClienteDireccion { get; set; }
        public string ClienteTelefono { get; set; }
        public string ClienteNombreCompleto { get; set; }
        public string ClienteDocumento { get; set; }

        // Datos adicionales para persona física
        public string ClienteNombres { get; set; }
        public string ClienteApellidos { get; set; }
        public string ClienteCi { get; set; }
        public DateTime? ClienteFechaNacimiento { get; set; }
        public string ClienteGenero { get; set; }

        // Datos adicionales para persona jurídica
        public string ClienteRazonSocial { get; set; }
        public string ClienteNit { get; set; }
        public string ClienteEncargadoNombre { get; set; }
        public string ClienteEncargadoCargo { get; set; }
    }

    /// <summary>
    /// Detalle de productos en la factura (resultado de sp_factura_detalle_productos)
    /// </summary>
    public class FacturaDetalleProducto
    {
        // Datos del detalle
        public int DetalleId { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal DescuentoUnitario { get; set; }
        public decimal Subtotal { get; set; }
        public bool RecetaVerificada { get; set; }
        public string Lote { get; set; }
        public DateTime? FechaVencimientoProducto { get; set; }

        // Datos del producto
        public int ProductoId { get; set; }
        public string ProductoCodigo { get; set; }
        public string ProductoNombre { get; set; }
        public string ProductoDescripcion { get; set; }
        public decimal ProductoPrecioCatalogo { get; set; }
        public bool ProductoRequiereReceta { get; set; }

        // Datos de la categoría
        public int CategoriaId { get; set; }
        public string CategoriaNombre { get; set; }
        public string CategoriaDescripcion { get; set; }

        // Cálculos adicionales
        public decimal SubtotalBruto { get; set; }
        public decimal DescuentoTotal { get; set; }
        public decimal SubtotalNeto { get; set; }
    }

    /// <summary>
    /// Detalle de servicios en la factura (resultado de sp_factura_detalle_servicios)
    /// </summary>
    public class FacturaDetalleServicio
    {
        // Datos del detalle del servicio
        public int DetalleId { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal DescuentoUnitario { get; set; }
        public decimal Subtotal { get; set; }

        // Datos del diagnóstico/servicio
        public int DiagnosticoId { get; set; }
        public string DiagnosticoCodigo { get; set; }
        public string DiagnosticoNombre { get; set; }
        public string DiagnosticoDescripcion { get; set; }
        public decimal DiagnosticoPrecioBase { get; set; }
        public string DiagnosticoCategoria { get; set; }
        public bool DiagnosticoRequiereEquipamiento { get; set; }

        // Datos del veterinario (si aplica)
        public int? VeterinarioId { get; set; }
        public string VeterinarioLicencia { get; set; }
        public string VeterinarioEspecialidad { get; set; }
        public string VeterinarioNombreCompleto { get; set; }

        // Datos del detalle histórico (si está vinculado)
        public int? HistoricoDetalleId { get; set; }
        public string HistoricoTipoEvento { get; set; }
        public DateTime? HistoricoFechaEvento { get; set; }
        public string HistoricoObservaciones { get; set; }
        public string HistoricoTratamiento { get; set; }

        // Datos del animal (si hay detalle histórico)
        public int? AnimalId { get; set; }
        public string AnimalNombre { get; set; }
        public string AnimalEspecie { get; set; }
        public string AnimalRaza { get; set; }

        // Cálculos adicionales
        public decimal SubtotalBruto { get; set; }
        public decimal DescuentoTotal { get; set; }
        public decimal SubtotalNeto { get; set; }
    }
}
