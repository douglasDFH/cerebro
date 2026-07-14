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
                this.spreporte_facturaTableAdapter.Fill(this.dsPrincipal.spreporte_factura, this.Order_id);

                if (this.dsPrincipal.spreporte_factura.Rows.Count == 0)
                {
                    MessageBox.Show("No se encontraron datos para la orden #" + this.Order_id.ToString(),
                        "Sin datos", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                this.reportViewer1.RefreshReport();
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
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al configurar parámetros: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            try
            {
                this.spreporte_facturaTableAdapter.Fill(this.dsPrincipal.spreporte_factura, this.Order_id);

                if (this.dsPrincipal.spreporte_factura.Rows.Count == 0)
                {
                    MessageBox.Show("No se encontraron datos para la orden #" + this.Order_id.ToString(),
                        "Sin datos", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // Configuración de visualización del ReportViewer
                this.reportViewer1.SetDisplayMode(DisplayMode.PrintLayout);
                this.reportViewer1.ZoomMode = ZoomMode.PageWidth;

                this.reportViewer1.RefreshReport();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            //var row = dsPrincipal.spreporte_factura.Rows[0];
            //MessageBox.Show("Teléfono: " + row["phone"].ToString());

        }
    }
}