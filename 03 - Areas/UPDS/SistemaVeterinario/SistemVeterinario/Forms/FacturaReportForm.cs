using System;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;
using CapaNegocio;
using CapaDatos;
using System.IO;
using Microsoft.Data.SqlClient;
using SistemVeterinario.Reportes;
using QRCoder;
using System.Linq;

namespace SistemVeterinario.Forms
{
    public partial class FacturaReportForm : Form
    {
        private int facturaId;

        public FacturaReportForm(int facturaId)
        {
            InitializeComponent();
            this.facturaId = facturaId;
            
            InicializarFormulario();
        }

        private void InicializarFormulario()
        {
            try
            {
                // Configurar el ReportViewer
                reportViewer.ProcessingMode = ProcessingMode.Local;
                // Cargar el reporte
                CargarReporteFactura();
            }
            catch (Exception ex)
            {
                MostrarErrorCompatibilidad(ex);
            }
        }

        private void CargarReporteFactura()
        {
            try
            {
                // Crear el DataSet para los datos de la factura
                DataSetReportes dataSet = new DataSetReportes();

                using (SqlConnection connection = DbConnection.Instance.GetConnection())
                {
                    // Cargar datos principales de la factura
                    using (SqlCommand command = new SqlCommand("sp_factura_datos_principales", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add(new SqlParameter("@factura_id", SqlDbType.Int) { Value = facturaId });

                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            adapter.Fill(dataSet, "sp_factura_datos_principales");
                        }
                    }

                    // Cargar detalle de productos
                    using (SqlCommand command = new SqlCommand("sp_factura_detalle_productos", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add(new SqlParameter("@factura_id", SqlDbType.Int) { Value = facturaId });

                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            adapter.Fill(dataSet, "sp_factura_detalle_productos");
                        }
                    }

                    // Cargar detalle de servicios
                    using (SqlCommand command = new SqlCommand("sp_factura_detalle_servicios", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add(new SqlParameter("@factura_id", SqlDbType.Int) { Value = facturaId });

                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            adapter.Fill(dataSet, "sp_factura_detalle_servicios");
                        }
                    }
                }

                // Verificar si hay datos principales
                if (dataSet.Tables["sp_factura_datos_principales"].Rows.Count == 0)
                {
                    MessageBox.Show("No se encontraron datos para esta factura.", "Sin resultados",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                    return;
                }

                // Buscar el archivo del reporte
                string currentDir = Directory.GetCurrentDirectory();
                string repoRoot = currentDir;

                // Subir directorios hasta encontrar la carpeta SistemVeterinario
                while (repoRoot != null && !Directory.Exists(Path.Combine(repoRoot, "SistemVeterinario")))
                {
                    var parent = Directory.GetParent(repoRoot);
                    repoRoot = parent?.FullName;
                }

                string reportPath = Path.Combine(repoRoot, "SistemVeterinario", "Reportes", "InvoiceReport.rdlc");
                if (!File.Exists(reportPath))
                {
                    throw new FileNotFoundException($"No se encontró el archivo de reporte en: {reportPath}");
                }

                // Configurar el ReportViewer
                reportViewer.LocalReport.ReportPath = reportPath;
                reportViewer.LocalReport.DataSources.Clear();

                // Generar código QR de la factura
                string qrCodeBase64 = GenerarQRFactura();
                if (!string.IsNullOrEmpty(qrCodeBase64) && !qrCodeBase64.StartsWith("Error"))
                {
                    // Agregar parámetro del QR al reporte
                    ReportParameter qrParameter = new ReportParameter("QRCodeImage", qrCodeBase64);
                    reportViewer.LocalReport.SetParameters(new ReportParameter[] { qrParameter });
                }

                // Asignar los DataSets al ReportViewer
                reportViewer.LocalReport.DataSources.Add(
                    new ReportDataSource("sp_factura_datos_principales", dataSet.Tables["sp_factura_datos_principales"]));
                reportViewer.LocalReport.DataSources.Add(
                    new ReportDataSource("sp_factura_detalle_productos", dataSet.Tables["sp_factura_detalle_productos"]));
                reportViewer.LocalReport.DataSources.Add(
                    new ReportDataSource("sp_factura_detalle_servicios", dataSet.Tables["sp_factura_detalle_servicios"]));

                // Actualizar el título de la ventana
                if (dataSet.Tables["sp_factura_datos_principales"].Rows.Count > 0)
                {
                    var row = dataSet.Tables["sp_factura_datos_principales"].Rows[0];
                    this.Text = $"Factura {row["numero_factura"]} - {row["cliente_nombre_completo"]}";
                }

                // Refrescar el ReportViewer
                reportViewer.RefreshReport();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar el reporte de factura: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }

        private void MostrarErrorCompatibilidad(Exception ex)
        {
            this.Controls.Clear();

            Label lblError = new Label();
            lblError.Text = $"Error de compatibilidad con ReportViewer en .NET 8:\n\n{ex.Message}\n\nPor favor, ejecute los comandos de actualización.";
            lblError.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Regular);
            lblError.ForeColor = Color.Red;
            lblError.TextAlign = ContentAlignment.MiddleCenter;
            lblError.Dock = DockStyle.Fill;

            this.Controls.Add(lblError);
        }

        /// <summary>
        /// Genera un código QR con el JSON completo de la factura
        /// </summary>
        /// <returns>Imagen del código QR en formato Base64 o mensaje de error</returns>
        private string GenerarQRFactura()
        {
            try
            {
                // Obtener el JSON de la factura
                string jsonData = NVentas.VentaToJson(facturaId);
                
                // Verificar si hubo error al generar el JSON
                if (jsonData.StartsWith("Error"))
                {
                    return null; // No generar QR si hay error en JSON
                }

                // Intentar generar el código QR directamente del JSON sin comprimir
                using (var qrGenerator = new QRCodeGenerator())
                {
                    var qrCodeData = qrGenerator.CreateQrCode(jsonData, QRCodeGenerator.ECCLevel.M);
                    using (var qrCode = new QRCode(qrCodeData))
                    {
                        using (Bitmap qrCodeImage = qrCode.GetGraphic(4)) // Píxeles de tamaño medio
                        {
                            // Convertir la imagen a Base64
                            using (var ms = new MemoryStream())
                            {
                                qrCodeImage.Save(ms, ImageFormat.Png);
                                byte[] imageBytes = ms.ToArray();
                                return Convert.ToBase64String(imageBytes);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Para otros errores, escribir en debug y devolver null
                System.Diagnostics.Debug.WriteLine($"Error al generar QR: {ex.Message}");
                return null;
            }
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
            base.OnKeyDown(e);
        }
    }
}
