using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;

namespace PedidosApp.Reportes
{
    public partial class FrmReporteFactura : Form
    {
        private int order_id;
        private DateTime fechaInicio;
        private DateTime fechaFin;

        public int Order_id
        {
            get { return order_id; }
            set { order_id = value; }
        }

        public DateTime FechaInicio
        {
            get { return fechaInicio; }
            set { fechaInicio = value; }
        }

        public DateTime FechaFin
        {
            get { return fechaFin; }
            set { fechaFin = value; }
        }

        public FrmReporteFactura()
        {
            InitializeComponent();
        }

        private void FrmReporteFactura_Load(object sender, EventArgs e)
        {
            try
            {
                // Cargar los datos
                this.spreporte_facturaTableAdapter.Fill(this.dsPrincipal.spreporte_factura, this.Order_id);

                if (this.dsPrincipal.spreporte_factura.Rows.Count == 0)
                {
                    MessageBox.Show("No se encontraron datos para la orden #" + this.Order_id.ToString(),
                        "Sin datos", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                    return;
                }

                // Configurar el reporte
                ConfigurarReporte();
                ConfigurarParametros();
                
                this.reportViewer1.RefreshReport();
            }
            catch (System.Data.SqlClient.SqlException sqlEx)
            {
                MessageBox.Show("Error de base de datos: " + sqlEx.Message + 
                    "\n\nVerifica que exista el procedimiento almacenado 'spreporte_factura'", 
                    "Error SQL", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConfigurarReporte()
        {
            try
            {
                this.reportViewer1.LocalReport.DataSources.Clear();
                ReportDataSource rds = new ReportDataSource();
                rds.Name = "dsPrincipal";
                rds.Value = this.dsPrincipal.spreporte_factura;
                this.reportViewer1.LocalReport.DataSources.Add(rds);
                this.reportViewer1.SetDisplayMode(DisplayMode.PrintLayout);
                this.reportViewer1.ZoomMode = ZoomMode.Percent;
                this.reportViewer1.ZoomPercent = 100;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al configurar el reporte: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConfigurarParametros()
        {
            try
            {
                List<ReportParameter> parametros = new List<ReportParameter>();
                
                // Agregar parámetros adicionales si el reporte los necesita
                parametros.Add(new ReportParameter("FechaImpresion", DateTime.Now.ToString("dd/MM/yyyy HH:mm")));
                parametros.Add(new ReportParameter("NumeroOrden", this.Order_id.ToString()));
                
                // Solo aplicar si hay parámetros
                if (parametros.Count > 0)
                {
                    this.reportViewer1.LocalReport.SetParameters(parametros);
                }
            }
            catch (Exception ex)
            {
                // No mostrar error si no hay parámetros configurados en el reporte
                System.Diagnostics.Debug.WriteLine("Advertencia configurando parámetros: " + ex.Message);
            }
        }

        public void ExportarAPDF(string rutaArchivo)
        {
            try
            {
                Warning[] warnings;
                string[] streamids;
                string mimeType;
                string encoding;
                string extension;

                byte[] bytes = this.reportViewer1.LocalReport.Render(
                    "PDF", null, out mimeType, out encoding, out extension,
                    out streamids, out warnings);

                System.IO.File.WriteAllBytes(rutaArchivo, bytes);

                MessageBox.Show("Factura exportada exitosamente a: " + rutaArchivo, "Éxito",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al exportar a PDF: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void ImprimirFactura()
        {
            try
            {
                this.reportViewer1.PrintDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al imprimir: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void reportViewer1_ReportError(object sender, ReportErrorEventArgs e)
        {
            MessageBox.Show("Error en el reporte: " + e.Exception.Message, "Error del Reporte",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void FrmReporteFactura_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                if (this.reportViewer1 != null)
                {
                    this.reportViewer1.LocalReport.DataSources.Clear();
                    this.reportViewer1.Reset();
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error al cerrar formulario: " + ex.Message);
            }
        }

        private void reportViewer1_Load(object sender, EventArgs e)
        {
            // Este evento se llama después de FrmReporteFactura_Load
            // Solo configurar visualización adicional si es necesario
            try
            {
                if (this.dsPrincipal.spreporte_factura.Rows.Count > 0)
                {
                    // Configuración adicional de visualización
                    this.reportViewer1.SetDisplayMode(DisplayMode.PrintLayout);
                    this.reportViewer1.ZoomMode = ZoomMode.PageWidth;
                    this.reportViewer1.RefreshReport();
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error en reportViewer1_Load: " + ex.Message);
            }
        }
    }
}