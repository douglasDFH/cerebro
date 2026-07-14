using System.Drawing;
using System.Windows.Forms;

namespace SistemVeterinario.Forms
{
    partial class ReportesModule
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        // Controles específicos de reportes (los controles base como tabInicio, tabConfiguraciones, etc. se heredan de BaseModulos)
        private Panel panelBotonesReportes;
        private Button btnClientesReporte;
        private Button btnMascotasReporte;
        private Button btnProductosReporte;
        private Button btnStockBajo;
        private Button btnVentasPorFecha;
        private Button btnExportarExcel;

        private Panel panelFiltroFechas;
        private Label lblFechaInicio;
        private DateTimePicker dtpFechaInicio;
        private Label lblFechaFin;
        private DateTimePicker dtpFechaFin;

        private Panel panelReporte;
        private Label lblTituloReporte;

        // Controles de estadísticas
        private Panel panelEstadisticasGenerales;
        private GroupBox grpResumenGeneral;
        private Label lblTotalClientes;
        private Label lblTotalMascotas;
        private Label lblTotalProductos;
        private Label lblProductosBajoStock;
        private Label lblValorInventario;
        private Button btnActualizar;

        private Panel panelGraficos;
        private GroupBox grpGraficoMascotas;
        private DataGridView dgvGraficoMascotas;
        private GroupBox grpGraficoProductos;
        private DataGridView dgvGraficoProductos;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private new void InitializeComponent()
        {
            components = new System.ComponentModel.Container();

            // Inicializar controles específicos de reportes
            InitializarControlesReportes();

            // Configurar las pestañas heredadas de BaseModulos
            ConfigurarPestanas();

            // Configurar panel de búsqueda
            ConfigurarPanelBusqueda();

            // Configurar formulario de reportes
            ConfigurarFormularioReportes();
        }

        private void ConfigurarPestanas()
        {
            // Configurar textos de las pestañas heredadas del BaseModulos
            tabInicio.Text = "Gestión de Reportes";
            tabConfiguraciones.Text = "Configuración de Reportes";
        }

        private void InitializarControlesReportes()
        {
            // Panel de botones de reportes
            panelBotonesReportes = new Panel();
            btnClientesReporte = new Button();
            btnMascotasReporte = new Button();
            btnProductosReporte = new Button();
            btnStockBajo = new Button();
            btnVentasPorFecha = new Button();
            btnExportarExcel = new Button();

            // Panel de filtros de fechas
            panelFiltroFechas = new Panel();
            lblFechaInicio = new Label();
            dtpFechaInicio = new DateTimePicker();
            lblFechaFin = new Label();
            dtpFechaFin = new DateTimePicker();

            // Panel de reporte
            panelReporte = new Panel();
            lblTituloReporte = new Label();

            // Inicializar controles de estadísticas
            InitializarControlesEstadisticas();
        }

        private void InitializarControlesEstadisticas()
        {
            // Panel de estadísticas generales
            panelEstadisticasGenerales = new Panel();
            grpResumenGeneral = new GroupBox();
            lblTotalClientes = new Label();
            lblTotalMascotas = new Label();
            lblTotalProductos = new Label();
            lblProductosBajoStock = new Label();
            lblValorInventario = new Label();
            btnActualizar = new Button();

            // Panel de gráficos
            panelGraficos = new Panel();
            grpGraficoMascotas = new GroupBox();
            dgvGraficoMascotas = new DataGridView();
            grpGraficoProductos = new GroupBox();
            dgvGraficoProductos = new DataGridView();
        }

        private void ConfigurarPanelBusqueda()
        {
            // Configurar el txtBuscar heredado
            txtBuscar.PlaceholderText = "Buscar en reportes...";

            // Configurar panel de botones de reportes en el área de búsqueda
            panelBotonesReportes.Location = new Point(10, 80);
            panelBotonesReportes.Size = new Size(960, 50);
            panelBotonesReportes.BackColor = Color.FromArgb(245, 245, 245);
            panelBotonesReportes.BorderStyle = BorderStyle.FixedSingle;

            // Botón Clientes
            btnClientesReporte.Text = "Clientes";
            btnClientesReporte.Location = new Point(10, 10);
            btnClientesReporte.Size = new Size(80, 30);
            btnClientesReporte.BackColor = Color.LightBlue;

            // Botón Mascotas
            btnMascotasReporte.Text = "Mascotas";
            btnMascotasReporte.Location = new Point(100, 10);
            btnMascotasReporte.Size = new Size(80, 30);
            btnMascotasReporte.BackColor = Color.LightGreen;

            // Botón Productos
            btnProductosReporte.Text = "Productos";
            btnProductosReporte.Location = new Point(190, 10);
            btnProductosReporte.Size = new Size(80, 30);
            btnProductosReporte.BackColor = Color.LightYellow;

            // Botón Stock Bajo
            btnStockBajo.Text = "Stock Bajo";
            btnStockBajo.Location = new Point(280, 10);
            btnStockBajo.Size = new Size(80, 30);
            btnStockBajo.BackColor = Color.LightCoral;

            // Botón Ventas
            btnVentasPorFecha.Text = "Ventas";
            btnVentasPorFecha.Location = new Point(370, 10);
            btnVentasPorFecha.Size = new Size(80, 30);
            btnVentasPorFecha.BackColor = Color.LightCyan;

            // Botón Exportar
            btnExportarExcel.Text = "Exportar";
            btnExportarExcel.Location = new Point(460, 10);
            btnExportarExcel.Size = new Size(80, 30);
            btnExportarExcel.BackColor = Color.LightGoldenrodYellow;

            panelBotonesReportes.Controls.AddRange(new Control[] {
                btnClientesReporte, btnMascotasReporte, btnProductosReporte,
                btnStockBajo, btnVentasPorFecha, btnExportarExcel
            });

            // Configurar panel de filtros de fechas
            panelFiltroFechas.Location = new Point(10, 140);
            panelFiltroFechas.Size = new Size(960, 40);
            panelFiltroFechas.BackColor = Color.FromArgb(240, 240, 240);
            panelFiltroFechas.BorderStyle = BorderStyle.FixedSingle;

            lblFechaInicio.Text = "Fecha Inicio:";
            lblFechaInicio.Location = new Point(10, 10);
            lblFechaInicio.Size = new Size(80, 20);
            lblFechaInicio.AutoSize = true;

            dtpFechaInicio.Location = new Point(100, 8);
            dtpFechaInicio.Size = new Size(120, 23);
            dtpFechaInicio.Format = DateTimePickerFormat.Short;

            lblFechaFin.Text = "Fecha Fin:";
            lblFechaFin.Location = new Point(240, 10);
            lblFechaFin.Size = new Size(70, 20);
            lblFechaFin.AutoSize = true;

            dtpFechaFin.Location = new Point(320, 8);
            dtpFechaFin.Size = new Size(120, 23);
            dtpFechaFin.Format = DateTimePickerFormat.Short;

            panelFiltroFechas.Controls.AddRange(new Control[] {
                lblFechaInicio, dtpFechaInicio, lblFechaFin, dtpFechaFin
            });

            // Configurar panel de reporte
            panelReporte.Location = new Point(10, 190);
            panelReporte.Size = new Size(960, 40);
            panelReporte.BackColor = Color.White;

            lblTituloReporte.Text = "Seleccione un tipo de reporte";
            lblTituloReporte.Location = new Point(10, 10);
            lblTituloReporte.Size = new Size(400, 20);
            lblTituloReporte.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            lblTituloReporte.ForeColor = Color.DarkBlue;
            lblTituloReporte.AutoSize = true;

            panelReporte.Controls.Add(lblTituloReporte);

            // Agregar paneles al tabInicio heredado
            tabInicio.Controls.AddRange(new Control[] {
                panelReporte, panelFiltroFechas, panelBotonesReportes
            });
        }

        private void ConfigurarFormularioReportes()
        {
            // Configurar panel de estadísticas generales
            panelEstadisticasGenerales.Location = new Point(15, 100);
            panelEstadisticasGenerales.Size = new Size(960, 200);
            panelEstadisticasGenerales.BackColor = Color.White;
            panelEstadisticasGenerales.BorderStyle = BorderStyle.FixedSingle;

            // GroupBox resumen general
            grpResumenGeneral.Text = "Resumen General del Sistema";
            grpResumenGeneral.Location = new Point(10, 10);
            grpResumenGeneral.Size = new Size(940, 180);
            grpResumenGeneral.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point);
            grpResumenGeneral.ForeColor = Color.DarkBlue;

            // Labels de estadísticas
            lblTotalClientes.Text = "Total de Clientes: 0";
            lblTotalClientes.Location = new Point(20, 30);
            lblTotalClientes.Size = new Size(200, 20);
            lblTotalClientes.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            lblTotalClientes.ForeColor = Color.Black;
            lblTotalClientes.AutoSize = true;

            lblTotalMascotas.Text = "Total de Mascotas: 0";
            lblTotalMascotas.Location = new Point(20, 60);
            lblTotalMascotas.Size = new Size(200, 20);
            lblTotalMascotas.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            lblTotalMascotas.ForeColor = Color.Black;
            lblTotalMascotas.AutoSize = true;

            lblTotalProductos.Text = "Total de Productos: 0";
            lblTotalProductos.Location = new Point(20, 90);
            lblTotalProductos.Size = new Size(200, 20);
            lblTotalProductos.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            lblTotalProductos.ForeColor = Color.Black;
            lblTotalProductos.AutoSize = true;

            lblProductosBajoStock.Text = "Productos con Stock Bajo: 0";
            lblProductosBajoStock.Location = new Point(250, 30);
            lblProductosBajoStock.Size = new Size(200, 20);
            lblProductosBajoStock.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            lblProductosBajoStock.ForeColor = Color.DarkRed;
            lblProductosBajoStock.AutoSize = true;

            lblValorInventario.Text = "Valor Total Inventario: $0.00";
            lblValorInventario.Location = new Point(250, 60);
            lblValorInventario.Size = new Size(200, 20);
            lblValorInventario.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point);
            lblValorInventario.ForeColor = Color.DarkGreen;
            lblValorInventario.AutoSize = true;

            btnActualizar.Text = "Actualizar Estadísticas";
            btnActualizar.Location = new Point(250, 90);
            btnActualizar.Size = new Size(150, 30);
            btnActualizar.BackColor = Color.LightBlue;

            grpResumenGeneral.Controls.AddRange(new Control[] {
                lblTotalClientes, lblTotalMascotas, lblTotalProductos,
                lblProductosBajoStock, lblValorInventario, btnActualizar
            });

            panelEstadisticasGenerales.Controls.Add(grpResumenGeneral);

            // Configurar panel de gráficos
            panelGraficos.Location = new Point(15, 310);
            panelGraficos.Size = new Size(960, 200);
            panelGraficos.BackColor = Color.White;

            // GroupBox mascotas
            grpGraficoMascotas.Text = "Distribución por Especies";
            grpGraficoMascotas.Location = new Point(10, 10);
            grpGraficoMascotas.Size = new Size(460, 180);
            grpGraficoMascotas.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            grpGraficoMascotas.ForeColor = Color.DarkGreen;

            dgvGraficoMascotas.Location = new Point(10, 25);
            dgvGraficoMascotas.Size = new Size(440, 140);
            dgvGraficoMascotas.AllowUserToAddRows = false;
            dgvGraficoMascotas.ReadOnly = true;

            grpGraficoMascotas.Controls.Add(dgvGraficoMascotas);

            // GroupBox productos
            grpGraficoProductos.Text = "Productos por Categoría";
            grpGraficoProductos.Location = new Point(480, 10);
            grpGraficoProductos.Size = new Size(460, 180);
            grpGraficoProductos.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            grpGraficoProductos.ForeColor = Color.DarkSlateGray;

            dgvGraficoProductos.Location = new Point(10, 25);
            dgvGraficoProductos.Size = new Size(440, 140);
            dgvGraficoProductos.AllowUserToAddRows = false;
            dgvGraficoProductos.ReadOnly = true;

            grpGraficoProductos.Controls.Add(dgvGraficoProductos);

            panelGraficos.Controls.AddRange(new Control[] {
                grpGraficoMascotas, grpGraficoProductos
            });

            // Agregar paneles al tabConfiguraciones heredado de BaseModulos
            panelFormulario.Controls.AddRange(new Control[] {
                panelEstadisticasGenerales, panelGraficos
            });
        }

        #endregion
    }
}