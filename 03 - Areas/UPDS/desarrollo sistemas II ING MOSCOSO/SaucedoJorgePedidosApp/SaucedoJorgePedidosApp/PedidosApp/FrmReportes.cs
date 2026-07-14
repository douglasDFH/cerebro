using CapaNegocio;
using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PedidosApp
{
    public partial class FrmReportes : Form
    {
        private readonly Norders nOrders = new Norders();

        public FrmReportes()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Reporte de Ventas por Período";

            // Configurar ComboBox de tipos de período
            cmbTipoPeriodo.Items.AddRange(new object[] { "Diario", "Semanal", "Mensual" });
            cmbTipoPeriodo.SelectedIndex = 0;

            // Configurar estilo del DataGridView
            ConfigurarDataGridView();
        }

        private void ConfigurarDataGridView()
        {
            dgvReporte.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvReporte.AllowUserToAddRows = false;
            dgvReporte.AllowUserToDeleteRows = false;
            dgvReporte.ReadOnly = true;
            dgvReporte.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvReporte.MultiSelect = false;
            dgvReporte.RowHeadersVisible = false;
            dgvReporte.AlternatingRowsDefaultCellStyle.BackColor = Color.LightGray;
        }

        private void FrmReportes_Load(object sender, EventArgs e)
        {
            dtpFechaInicio.Value = DateTime.Today.AddMonths(-1);
            dtpFechaFin.Value = DateTime.Today;
        }

        private void CargarReporte()
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                string tipoPeriodo = cmbTipoPeriodo.SelectedItem.ToString().ToLower();
                DataTable reporte = Norders.ReporteVentasPorPeriodo(
                    tipoPeriodo,
                    dtpFechaInicio.Value.Date,
                    dtpFechaFin.Value.Date
                );

                dgvReporte.DataSource = reporte;

                // Calcular totales
                int cantidadPedidos = 0;
                decimal ventaTotal = 0;

                foreach (DataRow row in reporte.Rows)
                {
                    cantidadPedidos += Convert.ToInt32(row["CantidadPedidos"]);
                    ventaTotal += Convert.ToDecimal(row["VentaTotal"]);
                }

                lblTotal.Text = $"Total Períodos: {reporte.Rows.Count}";
                lblPedidos.Text = $"Total Pedidos: {cantidadPedidos:N0}";
                lblMonto.Text = $"Venta Total: {ventaTotal:C}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar reporte: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        private void btnGenerar_Click(object sender, EventArgs e)
        {
            CargarReporte();
        }

        private void btnExportarExcel_Click(object sender, EventArgs e)
        {
            ExportarAExcel();
        }

        private void ExportarAExcel()
        {
            try
            {
                if (dgvReporte.Rows.Count == 0)
                {
                    MessageBox.Show("No hay datos para exportar", "Advertencia",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                SaveFileDialog saveFileDialog = new SaveFileDialog
                {
                    Filter = "Archivos CSV|*.csv|Archivos Excel|*.xlsx",
                    Title = "Guardar reporte",
                    FileName = $"Reporte_Ventas_{DateTime.Now:yyyyMMddHHmmss}",
                    DefaultExt = ".csv"
                };

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    if (saveFileDialog.FilterIndex == 1) // CSV
                    {
                        ExportarACsv(dgvReporte, saveFileDialog.FileName);
                    }
                    else // Excel
                    {
                        ExportarDataGridViewAExcel(dgvReporte, saveFileDialog.FileName);
                    }

                    MessageBox.Show("Reporte exportado exitosamente", "Éxito",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al exportar: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ExportarACsv(DataGridView dgv, string filePath)
        {
            StringBuilder sb = new StringBuilder();

            // Encabezados
            var headers = dgv.Columns.Cast<DataGridViewColumn>()
                              .Select(c => c.HeaderText);
            sb.AppendLine(string.Join(",", headers));

            // Datos
            foreach (DataGridViewRow row in dgv.Rows)
            {
                var cells = row.Cells.Cast<DataGridViewCell>()
                               .Select(c => EscapeCsvValue(c.Value?.ToString() ?? ""));
                sb.AppendLine(string.Join(",", cells));
            }

            File.WriteAllText(filePath, sb.ToString(), Encoding.UTF8);
        }

        private string EscapeCsvValue(string value)
        {
            if (value.Contains(",") || value.Contains("\"") || value.Contains("\n"))
            {
                return "\"" + value.Replace("\"", "\"\"") + "\"";
            }
            return value;
        }

        // Método alternativo para exportar a Excel sin Office Interop
        private void ExportarDataGridViewAExcel(DataGridView dgv, string filePath)
        {
            // Crear un DataTable desde el DataGridView
            DataTable dt = new DataTable();

            // Agregar columnas
            foreach (DataGridViewColumn column in dgv.Columns)
            {
                dt.Columns.Add(column.HeaderText);
            }

            // Agregar filas
            foreach (DataGridViewRow row in dgv.Rows)
            {
                DataRow dr = dt.NewRow();
                foreach (DataGridViewCell cell in row.Cells)
                {
                    dr[cell.ColumnIndex] = cell.Value;
                }
                dt.Rows.Add(dr);
            }

            // Exportar a CSV (que Excel puede abrir)
            StringBuilder sb = new StringBuilder();

            // Encabezados
            string[] columnNames = dt.Columns.Cast<DataColumn>()
                                  .Select(column => column.ColumnName)
                                  .ToArray();
            sb.AppendLine(string.Join(",", columnNames));

            // Datos
            foreach (DataRow row in dt.Rows)
            {
                string[] fields = row.ItemArray.Select(field =>
                    field.ToString().Replace(",", ";")).ToArray();
                sb.AppendLine(string.Join(",", fields));
            }

            System.IO.File.WriteAllText(filePath, sb.ToString());
        }


        private void dgvReporte_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            // Mostrar número de fila
            var grid = sender as DataGridView;
            var rowIdx = (e.RowIndex + 1).ToString();

            var centerFormat = new StringFormat()
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            };

            var headerBounds = new Rectangle(
                e.RowBounds.Left,
                e.RowBounds.Top,
                grid.RowHeadersWidth,
                e.RowBounds.Height);

            e.Graphics.DrawString(
                rowIdx,
                this.Font,
                SystemBrushes.ControlText,
                headerBounds,
                centerFormat);
        }
    }
}