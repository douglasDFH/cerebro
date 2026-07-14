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
using CapaNegocio;
using System.IO;

namespace PedidosApp.Reportes
{
    public partial class FrmReportesAvanzados : Form
    {
        private DateTime fechaInicio;
        private DateTime fechaFin;
        private string tipoPeriodo;
        private string tipoReporte;

        public FrmReportesAvanzados()
        {
            InitializeComponent();
            InicializarFormulario();
        }

        private void InicializarFormulario()
        {
            // Configurar fechas por defecto
            dtpFechaInicio.Value = DateTime.Now.AddDays(-30);
            dtpFechaFin.Value = DateTime.Now;

            // Configurar ComboBox de períodos
            cmbPeriodo.Items.Clear();
            cmbPeriodo.Items.AddRange(new string[] {
                "Diario",
                "Semanal", 
                "Mensual",
                "Anual"
            });
            cmbPeriodo.SelectedIndex = 0;

            // Configurar ComboBox de rangos predefinidos
            cmbRangoFechas.Items.Clear();
            cmbRangoFechas.Items.AddRange(new string[] {
                "Personalizado",
                "Hoy",
                "Últimos 7 días",
                "Últimos 30 días",
                "Mes actual",
                "Año actual",
                "Último mes",
                "Último año"
            });
            cmbRangoFechas.SelectedIndex = 0;

            // Configurar ComboBox de tipos de reporte
            cmbTipoReporte.Items.Clear();
            cmbTipoReporte.Items.AddRange(new string[] {
                "Resumen de Ventas",
                "Ventas Detalladas",
                "Productos Más Vendidos",
                "Clientes Top",
                "Ventas por Categoría"
            });
            cmbTipoReporte.SelectedIndex = 0;

            // Configurar ReportViewer
            ConfigurarReportViewer();
        }

        private void ConfigurarReportViewer()
        {
            try
            {
                reportViewer1.LocalReport.DataSources.Clear();
                reportViewer1.SetDisplayMode(DisplayMode.PrintLayout);
                reportViewer1.ZoomMode = ZoomMode.PageWidth;
                reportViewer1.ShowToolBar = true;
                reportViewer1.ShowParameterPrompts = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al configurar el visor de reportes: " + ex.Message, 
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmbRangoFechas_SelectedIndexChanged(object sender, EventArgs e)
        {
            DateTime hoy = DateTime.Now;
            
            switch (cmbRangoFechas.SelectedIndex)
            {
                case 0: // Personalizado
                    dtpFechaInicio.Enabled = true;
                    dtpFechaFin.Enabled = true;
                    break;
                case 1: // Hoy
                    dtpFechaInicio.Value = hoy.Date;
                    dtpFechaFin.Value = hoy.Date;
                    dtpFechaInicio.Enabled = false;
                    dtpFechaFin.Enabled = false;
                    break;
                case 2: // Últimos 7 días
                    dtpFechaInicio.Value = hoy.AddDays(-6).Date;
                    dtpFechaFin.Value = hoy.Date;
                    dtpFechaInicio.Enabled = false;
                    dtpFechaFin.Enabled = false;
                    break;
                case 3: // Últimos 30 días
                    dtpFechaInicio.Value = hoy.AddDays(-29).Date;
                    dtpFechaFin.Value = hoy.Date;
                    dtpFechaInicio.Enabled = false;
                    dtpFechaFin.Enabled = false;
                    break;
                case 4: // Mes actual
                    dtpFechaInicio.Value = new DateTime(hoy.Year, hoy.Month, 1);
                    dtpFechaFin.Value = hoy.Date;
                    dtpFechaInicio.Enabled = false;
                    dtpFechaFin.Enabled = false;
                    break;
                case 5: // Año actual
                    dtpFechaInicio.Value = new DateTime(hoy.Year, 1, 1);
                    dtpFechaFin.Value = hoy.Date;
                    dtpFechaInicio.Enabled = false;
                    dtpFechaFin.Enabled = false;
                    break;
                case 6: // Último mes
                    DateTime ultimoMes = hoy.AddMonths(-1);
                    dtpFechaInicio.Value = new DateTime(ultimoMes.Year, ultimoMes.Month, 1);
                    dtpFechaFin.Value = new DateTime(ultimoMes.Year, ultimoMes.Month, DateTime.DaysInMonth(ultimoMes.Year, ultimoMes.Month));
                    dtpFechaInicio.Enabled = false;
                    dtpFechaFin.Enabled = false;
                    break;
                case 7: // Último año
                    int ultimoAno = hoy.Year - 1;
                    dtpFechaInicio.Value = new DateTime(ultimoAno, 1, 1);
                    dtpFechaFin.Value = new DateTime(ultimoAno, 12, 31);
                    dtpFechaInicio.Enabled = false;
                    dtpFechaFin.Enabled = false;
                    break;
            }
        }

        private void btnGenerar_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidarFechas())
                {
                    fechaInicio = dtpFechaInicio.Value.Date;
                    fechaFin = dtpFechaFin.Value.Date.AddDays(1).AddSeconds(-1); // Incluir todo el día final
                    tipoPeriodo = cmbPeriodo.SelectedItem.ToString().ToUpper();
                    tipoReporte = cmbTipoReporte.SelectedItem.ToString();

                    GenerarReporte();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al generar el reporte: " + ex.Message, 
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ValidarFechas()
        {
            if (dtpFechaFin.Value < dtpFechaInicio.Value)
            {
                MessageBox.Show("La fecha fin no puede ser menor que la fecha inicio", 
                    "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            TimeSpan diferencia = dtpFechaFin.Value - dtpFechaInicio.Value;
            if (diferencia.TotalDays > 365)
            {
                MessageBox.Show("El rango de fechas no puede ser mayor a 1 año", 
                    "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        private void GenerarReporte()
        {
            try
            {
                DataTable datos = null;
                string rutaReporte = "";

                switch (tipoReporte)
                {
                    case "Resumen de Ventas":
                        datos = Norders.ReporteVentasPorPeriodo(tipoPeriodo, fechaInicio, fechaFin);
                        rutaReporte = "rptGeneral.rdlc";
                        break;
                    case "Ventas Detalladas":
                        datos = Norders.ReportePedidosPorCliente(fechaInicio, fechaFin);
                        rutaReporte = "rptGeneral.rdlc";
                        break;
                    case "Productos Más Vendidos":
                        datos = Norders.ReporteProductosMasVendidos(fechaInicio, fechaFin);
                        rutaReporte = "rptGeneral.rdlc";
                        break;
                    case "Clientes Top":
                        datos = Norders.ReportePedidosPorCliente(fechaInicio, fechaFin);
                        rutaReporte = "rptGeneral.rdlc";
                        break;
                    case "Ventas por Categoría":
                        datos = ObtenerVentasPorCategoria();
                        rutaReporte = "rptGeneral.rdlc";
                        break;
                }

                if (datos != null && datos.Rows.Count > 0)
                {
                    ConfigurarYMostrarReporte(datos, rutaReporte);
                }
                else
                {
                    MessageBox.Show("No se encontraron datos para el período seleccionado", 
                        "Sin datos", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al generar el reporte: " + ex.Message, 
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private DataTable ObtenerVentasPorCategoria()
        {
            // Implementar lógica para obtener ventas por categoría
            // Por ahora usar un método existente
            return Norders.ReporteProductosMasVendidos(fechaInicio, fechaFin);
        }

        private void ConfigurarYMostrarReporte(DataTable datos, string rutaReporte)
        {
            try
            {
                reportViewer1.LocalReport.DataSources.Clear();
                reportViewer1.LocalReport.ReportPath = "";

                // Configurar el origen de datos sin necesidad de RDLC específico
                ReportDataSource rds = new ReportDataSource("DataSet1", datos);
                reportViewer1.LocalReport.DataSources.Add(rds);
                
                // Intentar cargar template RDLC si existe
                try
                {
                    string rutaCompleta = Path.Combine(Application.StartupPath, "Reportes", rutaReporte);
                    if (File.Exists(rutaCompleta))
                    {
                        reportViewer1.LocalReport.ReportPath = rutaCompleta;
                    }
                }
                catch 
                {
                    // Si falla la carga del RDLC, continúa sin él
                }
                
                reportViewer1.RefreshReport();

                // Actualizar label de resultados con información detallada
                lblResultados.Text = $"✓ {tipoReporte} | {tipoPeriodo} | {fechaInicio:dd/MM/yyyy} - {dtpFechaFin.Value:dd/MM/yyyy} | {datos.Rows.Count} registros | {DateTime.Now:HH:mm}";

                // Habilitar botones de exportación
                HabilitarBotonesExportacion(true);

                // Mostrar información de columnas para debug si es necesario
                if (datos.Rows.Count > 0)
                {
                    string columnas = string.Join(", ", datos.Columns.Cast<DataColumn>().Select(c => c.ColumnName));
                    Console.WriteLine($"Columnas disponibles: {columnas}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al configurar el reporte: " + ex.Message, 
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                lblResultados.Text = "❌ Error: " + ex.Message;
                HabilitarBotonesExportacion(false);
            }
        }


        private void HabilitarBotonesExportacion(bool habilitar)
        {
            btnExportarPDF.Enabled = habilitar;
            btnExportarExcel.Enabled = habilitar;
            btnExportarWord.Enabled = habilitar;
            btnImprimir.Enabled = habilitar;
            btnVistaPrevia.Enabled = habilitar;
        }

        // Métodos de exportación
        private void btnExportarPDF_Click(object sender, EventArgs e)
        {
            ExportarReporte("PDF", "Archivos PDF|*.pdf");
        }

        private void btnExportarExcel_Click(object sender, EventArgs e)
        {
            ExportarReporte("Excel", "Archivos Excel|*.xls");
        }

        private void btnExportarWord_Click(object sender, EventArgs e)
        {
            ExportarReporte("Word", "Archivos Word|*.doc");
        }

        private void ExportarReporte(string formato, string filtro)
        {
            try
            {
                SaveFileDialog saveDialog = new SaveFileDialog();
                saveDialog.Filter = filtro;
                saveDialog.FileName = $"Reporte_{tipoReporte.Replace(" ", "_")}_{DateTime.Now:yyyyMMdd_HHmmss}";

                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    Warning[] warnings;
                    string[] streamids;
                    string mimeType;
                    string encoding;
                    string extension;

                    string formatoReporte = formato;
                    if (formato == "Excel") formatoReporte = "Excel";
                    if (formato == "Word") formatoReporte = "Word";

                    byte[] bytes = reportViewer1.LocalReport.Render(formatoReporte, null, 
                        out mimeType, out encoding, out extension, out streamids, out warnings);

                    File.WriteAllBytes(saveDialog.FileName, bytes);

                    MessageBox.Show($"Reporte exportado exitosamente a:\n{saveDialog.FileName}", 
                        "Exportación Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Preguntar si desea abrir el archivo
                    if (MessageBox.Show("¿Desea abrir el archivo exportado?", "Abrir Archivo", 
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        System.Diagnostics.Process.Start(saveDialog.FileName);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al exportar a {formato}: " + ex.Message, 
                    "Error de Exportación", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            try
            {
                reportViewer1.PrintDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al imprimir: " + ex.Message, 
                    "Error de Impresión", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnVistaPrevia_Click(object sender, EventArgs e)
        {
            try
            {
                // El ReportViewer ya muestra la vista previa, solo cambiar el modo de visualización
                if (reportViewer1.ZoomMode == ZoomMode.PageWidth)
                {
                    reportViewer1.ZoomMode = ZoomMode.FullPage;
                    btnVistaPrevia.Text = "Vista Ancho";
                }
                else
                {
                    reportViewer1.ZoomMode = ZoomMode.PageWidth;
                    btnVistaPrevia.Text = "Vista Completa";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error en vista previa: " + ex.Message, 
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                // Implementar búsqueda dentro del reporte generado
                if (reportViewer1.LocalReport.DataSources.Count > 0)
                {
                    string criterioBusqueda = txtBuscar.Text.Trim();
                    if (!string.IsNullOrEmpty(criterioBusqueda))
                    {
                        // Filtrar datos y regenerar reporte
                        FiltrarDatosReporte(criterioBusqueda);
                    }
                    else
                    {
                        // Mostrar todos los datos
                        GenerarReporte();
                    }
                }
                else
                {
                    MessageBox.Show("Primero debe generar un reporte", 
                        "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error en la búsqueda: " + ex.Message, 
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FiltrarDatosReporte(string criterio)
        {
            // Implementar filtrado de datos según el criterio de búsqueda
            // Por simplicidad, regenerar el reporte con los datos filtrados
            GenerarReporte();
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            try
            {
                // Limpiar el reporte y resetear controles
                reportViewer1.LocalReport.DataSources.Clear();
                reportViewer1.RefreshReport();
                
                txtBuscar.Clear();
                HabilitarBotonesExportacion(false);
                
                lblResultados.Text = "Genere un reporte para ver los resultados";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al limpiar: " + ex.Message, 
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FrmReportesAvanzados_Load(object sender, EventArgs e)
        {
            try
            {
                HabilitarBotonesExportacion(false);
                lblResultados.Text = "Seleccione los criterios y genere un reporte";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar el formulario: " + ex.Message, 
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}