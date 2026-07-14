using System;
using System.Collections.Generic;

namespace CapaDatos.Entidades
{
    public class Factura
    {
        public int IdFactura { get; set; }
        public int IdCliente { get; set; }
        public int IdPersonal { get; set; }
        public string RefFactura { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Subtotal { get; set; }
        public decimal Impuestos { get; set; }
        public decimal Total { get; set; }
        public string Estado { get; set; }
        public string FormaPago { get; set; }
        public DateTime FechaCreacion { get; set; }
        
        // Propiedades de navegación
        public string NombreCliente { get; set; }
        public string NombreEmpleado { get; set; }
        public List<ElementoFactura> Elementos { get; set; } = new List<ElementoFactura>();
        
        public bool EsPendiente => Estado == "Pendiente";
        public bool EsPagada => Estado == "Pagada";
        public bool EsCancelada => Estado == "Cancelada";
    }

    public class ElementoFactura
    {
        public int IdElementoFactura { get; set; }
        public int IdFactura { get; set; }
        public string Elemento { get; set; }
        public decimal Precio { get; set; }
        public int Cantidad { get; set; }
        public decimal Subtotal { get; set; }
        public string TipoElemento { get; set; }
        
        public bool EsServicio => TipoElemento == "Servicio";
        public bool EsProducto => TipoElemento == "Producto";
        public bool EsMedicamento => TipoElemento == "Medicamento";
    }
}