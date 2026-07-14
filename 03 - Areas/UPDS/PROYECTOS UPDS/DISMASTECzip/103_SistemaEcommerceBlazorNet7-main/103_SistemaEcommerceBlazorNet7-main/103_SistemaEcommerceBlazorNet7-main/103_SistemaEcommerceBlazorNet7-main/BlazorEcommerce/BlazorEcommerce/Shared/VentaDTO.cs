using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorEcommerce.Shared
{
    public class VentaDTO
    {
        public int IdVenta { get; set; }

        public int? IdPersona { get; set; }

        public decimal? Total { get; set; }
        public string? MetodoEnvio { get; set; }
        public string? MetodoPago { get; set; }
        public string? DireccionEnvio { get; set; }
        public List<DetalleVentaDTO> DetalleVenta { get; set; } = new List<DetalleVentaDTO>();

        //public virtual ICollection<DetalleVentaDTO> DetalleVenta { get; set; } = new List<DetalleVentaDTO>();

    }
}
