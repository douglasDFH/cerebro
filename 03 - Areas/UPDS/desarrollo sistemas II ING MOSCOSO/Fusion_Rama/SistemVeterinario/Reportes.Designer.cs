using System.Drawing;

namespace SistemVeterinario
{
    partial class Reportes
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        // Controles principales
        private TabControl tabControl;
        private TabPage tabReportes;
        private TabPage tabEstadisticas;

        // Controles de reportes
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
        private DataGridView dgvReportes;

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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            
            // Configuración del formulario
            this.Text = "Reportes y Estadísticas";
            this.Size = new Size(1200, 750);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.MinimumSize = new Size(1000, 600);

            // TabControl principal
            tabControl = new TabControl();
            tabControl.Dock = DockStyle.Fill;

            // Pestaña de Reportes
            tabReportes = new TabPage("Reportes");
            InitializeTabReportes();
            tabControl.TabPages.Add(tabReportes);

            // Pestaña de Estadísticas
            tabEstadisticas = new TabPage("Estadísticas");
            InitializeTabEstadisticas();
            tabControl.TabPages.Add(tabEstadisticas);

            // Agregar el TabControl al formulario
            this.Controls.Add(tabControl);
        }

        private void InitializeTabReportes()
        {
            // Panel de botones de reportes
            panelBotonesReportes = new Panel();
            panelBotonesReportes.Height = 60;
            panelBotonesReportes.Dock = DockStyle.Top;
            panelBotonesReportes.BackColor = Color.LightGray;
            panelBotonesReportes.Padding = new Padding(10);

            btnClientesReporte = new Button();
            btnClientesReporte.Text = "Clientes";
            btnClientesReporte.Location = new Point(10, 15);
            btnClientesReporte.Size = new Size(80, 30);
            btnClientesReporte.BackColor = Color.LightBlue;

            btnMascotasReporte = new Button();
            btnMascotasReporte.Text = "Mascotas";
            btnMascotasReporte.Location = new Point(100, 15);
            btnMascotasReporte.Size = new Size(80, 30);
            btnMascotasReporte.BackColor = Color.LightGreen;

            btnProductosReporte = new Button();
            btnProductosReporte.Text = "Productos";
            btnProductosReporte.Location = new Point(190, 15);
            btnProductosReporte.Size = new Size(80, 30);
            btnProductosReporte.BackColor = Color.LightYellow;

            btnStockBajo = new Button();
            btnStockBajo.Text = "Stock Bajo";
            btnStockBajo.Location = new Point(280, 15);
            btnStockBajo.Size = new Size(80, 30);
            btnStockBajo.BackColor = Color.LightSalmon;

            btnVentasPorFecha = new Button();
            btnVentasPorFecha.Text = "Ventas";
            btnVentasPorFecha.Location = new Point(370, 15);
            btnVentasPorFecha.Size = new Size(80, 30);
            btnVentasPorFecha.BackColor = Color.LightCyan;

            btnExportarExcel = new Button();
            btnExportarExcel.Text = "Exportar Excel";
            btnExportarExcel.Location = new Point(470, 15);
            btnExportarExcel.Size = new Size(100, 30);
            btnExportarExcel.BackColor = Color.PaleGoldenrod;

            panelBotonesReportes.Controls.AddRange(new Control[] {
                btnClientesReporte, btnMascotasReporte, btnProductosReporte, 
                btnStockBajo, btnVentasPorFecha, btnExportarExcel
            });

            // Panel de filtro por fechas
            panelFiltroFechas = new Panel();
            panelFiltroFechas.Height = 50;
            panelFiltroFechas.Dock = DockStyle.Top;
            panelFiltroFechas.BackColor = Color.WhiteSmoke;
            panelFiltroFechas.Padding = new Padding(10);

            lblFechaInicio = new Label();
            lblFechaInicio.Text = "Fecha Inicio:";
            lblFechaInicio.Location = new Point(10, 15);
            lblFechaInicio.Size = new Size(80, 20);

            dtpFechaInicio = new DateTimePicker();
            dtpFechaInicio.Location = new Point(95, 13);
            dtpFechaInicio.Size = new Size(120, 23);
            dtpFechaInicio.Format = DateTimePickerFormat.Short;

            lblFechaFin = new Label();
            lblFechaFin.Text = "Fecha Fin:";
            lblFechaFin.Location = new Point(230, 15);
            lblFechaFin.Size = new Size(70, 20);

            dtpFechaFin = new DateTimePicker();
            dtpFechaFin.Location = new Point(305, 13);
            dtpFechaFin.Size = new Size(120, 23);
            dtpFechaFin.Format = DateTimePickerFormat.Short;

            panelFiltroFechas.Controls.AddRange(new Control[] {
                lblFechaInicio, dtpFechaInicio, lblFechaFin, dtpFechaFin
            });

            // Panel del reporte
            panelReporte = new Panel();
            panelReporte.Dock = DockStyle.Fill;
            panelReporte.Padding = new Padding(10);

            lblTituloReporte = new Label();
            lblTituloReporte.Text = "Seleccione un tipo de reporte";
            lblTituloReporte.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Bold);
            lblTituloReporte.Location = new Point(10, 10);
            lblTituloReporte.Size = new Size(300, 25);
            lblTituloReporte.ForeColor = Color.DarkBlue;

            dgvReportes = new DataGridView();
            dgvReportes.Location = new Point(10, 45);
            dgvReportes.Size = new Size(1150, 500);
            dgvReportes.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvReportes.AllowUserToAddRows = false;
            dgvReportes.AllowUserToDeleteRows = false;
            dgvReportes.ReadOnly = true;
            dgvReportes.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvReportes.MultiSelect = false;
            dgvReportes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            dgvReportes.BackgroundColor = Color.White;
            dgvReportes.AlternatingRowsDefaultCellStyle.BackColor = Color.AliceBlue;
            dgvReportes.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvReportes.RowHeadersVisible = false;
            dgvReportes.BorderStyle = BorderStyle.FixedSingle;

            panelReporte.Controls.AddRange(new Control[] {
                lblTituloReporte, dgvReportes
            });

            tabReportes.Controls.Add(panelReporte);
            tabReportes.Controls.Add(panelFiltroFechas);
            tabReportes.Controls.Add(panelBotonesReportes);
        }

        private void InitializeTabEstadisticas()
        {
            // Panel de estadísticas generales
            panelEstadisticasGenerales = new Panel();
            panelEstadisticasGenerales.Height = 200;
            panelEstadisticasGenerales.Dock = DockStyle.Top;
            panelEstadisticasGenerales.Padding = new Padding(10);

            // GroupBox resumen general
            grpResumenGeneral = new GroupBox();
            grpResumenGeneral.Text = "Resumen General del Sistema";
            grpResumenGeneral.Dock = DockStyle.Fill;
            grpResumenGeneral.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Bold);

            lblTotalClientes = new Label();
            lblTotalClientes.Text = "Total Clientes: 0";
            lblTotalClientes.Location = new Point(20, 30);
            lblTotalClientes.Size = new Size(150, 25);
            lblTotalClientes.Font = new Font("Microsoft Sans Serif", 10F);
            lblTotalClientes.ForeColor = Color.DarkBlue;

            lblTotalMascotas = new Label();
            lblTotalMascotas.Text = "Total Mascotas: 0";
            lblTotalMascotas.Location = new Point(200, 30);
            lblTotalMascotas.Size = new Size(150, 25);
            lblTotalMascotas.Font = new Font("Microsoft Sans Serif", 10F);
            lblTotalMascotas.ForeColor = Color.DarkGreen;

            lblTotalProductos = new Label();
            lblTotalProductos.Text = "Total Productos: 0";
            lblTotalProductos.Location = new Point(380, 30);
            lblTotalProductos.Size = new Size(150, 25);
            lblTotalProductos.Font = new Font("Microsoft Sans Serif", 10F);
            lblTotalProductos.ForeColor = Color.DarkOrange;

            lblProductosBajoStock = new Label();
            lblProductosBajoStock.Text = "Productos Stock Bajo: 0";
            lblProductosBajoStock.Location = new Point(20, 70);
            lblProductosBajoStock.Size = new Size(200, 25);
            lblProductosBajoStock.Font = new Font("Microsoft Sans Serif", 10F);
            lblProductosBajoStock.ForeColor = Color.Red;

            lblValorInventario = new Label();
            lblValorInventario.Text = "Valor Inventario: $0.00";
            lblValorInventario.Location = new Point(250, 70);
            lblValorInventario.Size = new Size(200, 25);
            lblValorInventario.Font = new Font("Microsoft Sans Serif", 10F);
            lblValorInventario.ForeColor = Color.DarkMagenta;

            btnActualizar = new Button();
            btnActualizar.Text = "Actualizar Estadísticas";
            btnActualizar.Location = new Point(500, 110);
            btnActualizar.Size = new Size(150, 35);
            btnActualizar.BackColor = Color.LightGreen;
            btnActualizar.Font = new Font("Microsoft Sans Serif", 9F);

            grpResumenGeneral.Controls.AddRange(new Control[] {
                lblTotalClientes, lblTotalMascotas, lblTotalProductos, 
                lblProductosBajoStock, lblValorInventario, btnActualizar
            });

            panelEstadisticasGenerales.Controls.Add(grpResumenGeneral);

            // Panel de gráficos
            panelGraficos = new Panel();
            panelGraficos.Dock = DockStyle.Fill;
            panelGraficos.Padding = new Padding(10);

            // GroupBox gráfico mascotas
            grpGraficoMascotas = new GroupBox();
            grpGraficoMascotas.Text = "Estadísticas de Mascotas por Especie";
            grpGraficoMascotas.Location = new Point(10, 10);
            grpGraficoMascotas.Size = new Size(550, 300);
            grpGraficoMascotas.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Bottom;

            dgvGraficoMascotas = new DataGridView();
            dgvGraficoMascotas.Location = new Point(15, 25);
            dgvGraficoMascotas.Size = new Size(520, 260);
            dgvGraficoMascotas.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvGraficoMascotas.AllowUserToAddRows = false;
            dgvGraficoMascotas.AllowUserToDeleteRows = false;
            dgvGraficoMascotas.ReadOnly = true;
            dgvGraficoMascotas.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvGraficoMascotas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvGraficoMascotas.BackgroundColor = Color.White;
            dgvGraficoMascotas.AlternatingRowsDefaultCellStyle.BackColor = Color.LightBlue;
            dgvGraficoMascotas.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvGraficoMascotas.RowHeadersVisible = false;

            grpGraficoMascotas.Controls.Add(dgvGraficoMascotas);

            // GroupBox gráfico productos
            grpGraficoProductos = new GroupBox();
            grpGraficoProductos.Text = "Estadísticas de Productos por Categoría";
            grpGraficoProductos.Location = new Point(580, 10);
            grpGraficoProductos.Size = new Size(580, 300);
            grpGraficoProductos.Anchor = AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom;

            dgvGraficoProductos = new DataGridView();
            dgvGraficoProductos.Location = new Point(15, 25);
            dgvGraficoProductos.Size = new Size(550, 260);
            dgvGraficoProductos.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvGraficoProductos.AllowUserToAddRows = false;
            dgvGraficoProductos.AllowUserToDeleteRows = false;
            dgvGraficoProductos.ReadOnly = true;
            dgvGraficoProductos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvGraficoProductos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvGraficoProductos.BackgroundColor = Color.White;
            dgvGraficoProductos.AlternatingRowsDefaultCellStyle.BackColor = Color.LightYellow;
            dgvGraficoProductos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvGraficoProductos.RowHeadersVisible = false;

            grpGraficoProductos.Controls.Add(dgvGraficoProductos);

            panelGraficos.Controls.AddRange(new Control[] {
                grpGraficoMascotas, grpGraficoProductos
            });

            tabEstadisticas.Controls.Add(panelGraficos);
            tabEstadisticas.Controls.Add(panelEstadisticasGenerales);
        }

        #endregion
    }
}