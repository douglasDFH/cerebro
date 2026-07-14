using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using CapaNegocio;
using SistemVeterinario.Navigation;

namespace SistemVeterinario.Forms
{
    public partial class ReportesModule : BaseModulos
    {
        public ReportesModule()
        {
            InitializeComponent();
            ConfigurarModulo();
        }

        protected override void OnLoad()
        {
            CargarDatosReportes();
            CargarEstadisticasGenerales();
        }

        protected override void OnBuscar()
        {
            CargarDatosReportes();
        }

        protected override void OnNuevo()
        {
            MessageBox.Show("Para generar un nuevo reporte, seleccione una opción del panel de reportes", "Información",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        protected override void OnEditar(DataGridViewRow fila)
        {
            MessageBox.Show("Los reportes no se pueden editar, solo visualizar y exportar", "Información",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        protected override void OnEliminarFila(DataGridViewRow fila)
        {
            MessageBox.Show("Los reportes no se pueden eliminar desde esta vista", "Información",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void CargarDatosReportes()
        {
            try
            {
                // Cargar reporte por defecto (clientes)
                DataTable clientes = NPersona.Mostrar();
                if (clientes != null && dgvDatos != null)
                {
                    dgvDatos.DataSource = clientes;
                    if (lblTituloReporte != null)
                        lblTituloReporte.Text = "Reporte de Clientes";
                    ConfigurarColumnasDGV("clientes");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error cargando datos: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void ConfigurarModulo()
        {
            try
            {
                ConfigurarEventos();
                ConfigurarDateTimePickers();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error configurando módulo: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConfigurarEventos()
        {
            try
            {
                if (btnClientesReporte != null) btnClientesReporte.Click += BtnClientesReporte_Click;
                if (btnMascotasReporte != null) btnMascotasReporte.Click += BtnMascotasReporte_Click;
                if (btnProductosReporte != null) btnProductosReporte.Click += BtnProductosReporte_Click;
                if (btnStockBajo != null) btnStockBajo.Click += BtnStockBajo_Click;
                if (btnVentasPorFecha != null) btnVentasPorFecha.Click += BtnVentasPorFecha_Click;
                if (btnExportarExcel != null) btnExportarExcel.Click += BtnExportarExcel_Click;
                if (btnActualizar != null) btnActualizar.Click += BtnActualizar_Click;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error configurando eventos: {ex.Message}");
            }
        }

        private void ConfigurarDateTimePickers()
        {
            try
            {
                if (dtpFechaInicio != null)
                {
                    dtpFechaInicio.Value = DateTime.Now.AddMonths(-1);
                }

                if (dtpFechaFin != null)
                {
                    dtpFechaFin.Value = DateTime.Now;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error configurando fechas: {ex.Message}");
            }
        }

        private void CargarEstadisticasGenerales()
        {
            try
            {
                // Estadísticas de clientes
                int totalClientes = NPersona.Mostrar()?.Rows.Count ?? 0;
                if (lblTotalClientes != null)
                    lblTotalClientes.Text = $"Total Clientes: {totalClientes}";

                // Estadísticas de mascotas
                int totalMascotas = NMascotas.ContarMascotasActivas();
                if (lblTotalMascotas != null)
                    lblTotalMascotas.Text = $"Total Mascotas: {totalMascotas}";

                // Estadísticas de productos
                int totalProductos = NProductos.ContarProductosActivos();
                int productosBajoStock = NProductos.ContarProductosBajoStock();
                decimal valorInventario = NProductos.CalcularValorInventarioTotal();

                if (lblTotalProductos != null)
                    lblTotalProductos.Text = $"Total Productos: {totalProductos}";

                if (lblProductosBajoStock != null)
                {
                    lblProductosBajoStock.Text = $"Productos Stock Bajo: {productosBajoStock}";
                    lblProductosBajoStock.ForeColor = productosBajoStock > 0 ? Color.Red : Color.Green;
                }

                if (lblValorInventario != null)
                    lblValorInventario.Text = $"Valor Inventario: {valorInventario:C2}";

                // Cargar gráficos iniciales
                CargarGraficoMascotasPorEspecie();
                CargarGraficoProductosPorCategoria();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error cargando estadísticas: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarGraficoMascotasPorEspecie()
        {
            try
            {
                if (dgvGraficoMascotas == null) return;

                DataTable estadisticas = NMascotas.ObtenerEstadisticasPorEspecie();
                if (estadisticas != null)
                {
                    dgvGraficoMascotas.DataSource = estadisticas;

                    // Configurar columnas
                    foreach (DataGridViewColumn column in dgvGraficoMascotas.Columns)
                    {
                        switch (column.Name.ToLower())
                        {
                            case "especie":
                                column.HeaderText = "Especie";
                                column.Width = 100;
                                break;
                            case "cantidad":
                                column.HeaderText = "Cantidad";
                                column.Width = 80;
                                break;
                            case "machos":
                                column.HeaderText = "Machos";
                                column.Width = 80;
                                break;
                            case "hembras":
                                column.HeaderText = "Hembras";
                                column.Width = 80;
                                break;
                            case "esterilizados":
                                column.HeaderText = "Esterilizados";
                                column.Width = 100;
                                break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error cargando gráfico de mascotas: {ex.Message}");
            }
        }

        private void CargarGraficoProductosPorCategoria()
        {
            try
            {
                if (dgvGraficoProductos == null) return;

                DataTable estadisticas = NProductos.ObtenerEstadisticasPorCategoria();
                if (estadisticas != null)
                {
                    dgvGraficoProductos.DataSource = estadisticas;

                    // Configurar columnas
                    foreach (DataGridViewColumn column in dgvGraficoProductos.Columns)
                    {
                        switch (column.Name.ToLower())
                        {
                            case "categoria":
                                column.HeaderText = "Categoría";
                                column.Width = 120;
                                break;
                            case "cantidad":
                                column.HeaderText = "Productos";
                                column.Width = 80;
                                break;
                            case "stocktotal":
                                column.HeaderText = "Stock Total";
                                column.Width = 80;
                                break;
                            case "valorinventario":
                                column.HeaderText = "Valor Inventario";
                                column.Width = 120;
                                column.DefaultCellStyle.Format = "C2";
                                break;
                            case "productosbajostock":
                                column.HeaderText = "Stock Bajo";
                                column.Width = 80;
                                break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error cargando gráfico de productos: {ex.Message}");
            }
        }

        #region Event Handlers

        private void BtnClientesReporte_Click(object? sender, EventArgs e)
        {
            try
            {
                if (dgvDatos == null) return;

                DataTable clientes = NPersona.Mostrar();
                if (clientes != null)
                {
                    dgvDatos.DataSource = clientes;
                    ConfigurarColumnasDGV("clientes");

                    if (lblTituloReporte != null)
                        lblTituloReporte.Text = "Reporte de Clientes";
                }
                else
                {
                    MessageBox.Show("No se pudieron cargar los datos de clientes", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error generando reporte de clientes: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnMascotasReporte_Click(object? sender, EventArgs e)
        {
            try
            {
                if (dgvDatos == null) return;

                DataTable mascotas = NMascotas.Mostrar();
                if (mascotas != null)
                {
                    dgvDatos.DataSource = mascotas;
                    ConfigurarColumnasDGV("mascotas");

                    if (lblTituloReporte != null)
                        lblTituloReporte.Text = "Reporte de Mascotas";
                }
                else
                {
                    MessageBox.Show("No se pudieron cargar los datos de mascotas", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error generando reporte de mascotas: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnProductosReporte_Click(object? sender, EventArgs e)
        {
            try
            {
                if (dgvDatos == null) return;

                DataTable productos = NProductos.Mostrar();
                if (productos != null)
                {
                    dgvDatos.DataSource = productos;
                    ConfigurarColumnasDGV("productos");

                    if (lblTituloReporte != null)
                        lblTituloReporte.Text = "Reporte de Productos";
                }
                else
                {
                    MessageBox.Show("No se pudieron cargar los datos de productos", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error generando reporte de productos: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnStockBajo_Click(object? sender, EventArgs e)
        {
            try
            {
                if (dgvDatos == null) return;

                DataTable stockBajo = NProductos.ObtenerProductosBajoStock();
                if (stockBajo != null)
                {
                    dgvDatos.DataSource = stockBajo;
                    ConfigurarColumnasDGV("stock_bajo");

                    if (lblTituloReporte != null)
                        lblTituloReporte.Text = "Reporte de Productos con Stock Bajo";

                    if (stockBajo.Rows.Count == 0)
                    {
                        MessageBox.Show("¡Excelente! No hay productos con stock bajo", "Stock OK",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show($"Se encontraron {stockBajo.Rows.Count} productos con stock bajo", "Atención",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    MessageBox.Show("No se pudieron cargar los datos de stock", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error generando reporte de stock: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnVentasPorFecha_Click(object? sender, EventArgs e)
        {
            try
            {
                DateTime fechaInicio = dtpFechaInicio?.Value ?? DateTime.Now.AddMonths(-1);
                DateTime fechaFin = dtpFechaFin?.Value ?? DateTime.Now;

                if (fechaInicio > fechaFin)
                {
                    MessageBox.Show("La fecha de inicio no puede ser mayor a la fecha de fin", "Error de Fechas",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                GenerarReporteVentasPorRango(fechaInicio, fechaFin);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error generando reporte de ventas: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnExportarExcel_Click(object? sender, EventArgs e)
        {
            try
            {
                if (dgvDatos?.DataSource == null)
                {
                    MessageBox.Show("No hay datos para exportar. Genere primero un reporte.", "Sin Datos",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                ExportarReporte("EXCEL");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error exportando reporte: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnActualizar_Click(object? sender, EventArgs e)
        {
            try
            {
                CargarEstadisticasGenerales();
                MessageBox.Show("Estadísticas actualizadas correctamente", "Actualizado",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error actualizando estadísticas: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        private void ConfigurarColumnasDGV(string tipoReporte)
        {
            if (dgvDatos?.DataSource == null) return;

            try
            {
                // Ocultar columnas de acción para reportes
                if (dgvDatos.Columns.Contains("btnEditar"))
                    dgvDatos.Columns["btnEditar"].Visible = false;
                if (dgvDatos.Columns.Contains("btnEliminar"))
                    dgvDatos.Columns["btnEliminar"].Visible = false;

                switch (tipoReporte.ToLower())
                {
                    case "clientes":
                        foreach (DataGridViewColumn column in dgvDatos.Columns)
                        {
                            switch (column.Name.ToLower())
                            {
                                case "id":
                                    column.HeaderText = "ID";
                                    column.Width = 50;
                                    break;
                                case "nombre_mostrar":
                                    column.HeaderText = "Nombre/Razón Social";
                                    column.Width = 200;
                                    break;
                                case "tipo":
                                    column.HeaderText = "Tipo";
                                    column.Width = 80;
                                    break;
                                case "email":
                                    column.HeaderText = "Email";
                                    column.Width = 150;
                                    break;
                                case "telefono":
                                    column.HeaderText = "Teléfono";
                                    column.Width = 100;
                                    break;
                                case "direccion":
                                    column.HeaderText = "Dirección";
                                    column.Width = 200;
                                    break;
                                default:
                                    column.Visible = false;
                                    break;
                            }
                        }
                        break;

                    case "mascotas":
                        foreach (DataGridViewColumn column in dgvDatos.Columns)
                        {
                            switch (column.Name.ToLower())
                            {
                                case "id":
                                    column.HeaderText = "ID";
                                    column.Width = 50;
                                    break;
                                case "nombre":
                                    column.HeaderText = "Nombre";
                                    column.Width = 120;
                                    break;
                                case "especie":
                                    column.HeaderText = "Especie";
                                    column.Width = 80;
                                    break;
                                case "raza":
                                    column.HeaderText = "Raza";
                                    column.Width = 100;
                                    break;
                                case "propietario":
                                    column.HeaderText = "Propietario";
                                    column.Width = 180;
                                    break;
                                case "genero":
                                    column.HeaderText = "Sexo";
                                    column.Width = 50;
                                    break;
                                case "esterilizado":
                                    column.HeaderText = "Esterilizado";
                                    column.Width = 80;
                                    break;
                                default:
                                    column.Visible = false;
                                    break;
                            }
                        }
                        break;

                    case "productos":
                        foreach (DataGridViewColumn column in dgvDatos.Columns)
                        {
                            switch (column.Name.ToLower())
                            {
                                case "id":
                                    column.HeaderText = "ID";
                                    column.Width = 50;
                                    break;
                                case "codigo":
                                    column.HeaderText = "Código";
                                    column.Width = 80;
                                    break;
                                case "nombre":
                                    column.HeaderText = "Producto";
                                    column.Width = 180;
                                    break;
                                case "categoria_nombre":
                                    column.HeaderText = "Categoría";
                                    column.Width = 100;
                                    break;
                                case "precio":
                                    column.HeaderText = "Precio";
                                    column.Width = 80;
                                    column.DefaultCellStyle.Format = "C2";
                                    break;
                                case "stock_actual":
                                    column.HeaderText = "Stock";
                                    column.Width = 60;
                                    break;
                                case "estado_stock":
                                    column.HeaderText = "Estado";
                                    column.Width = 80;
                                    break;
                                default:
                                    column.Visible = false;
                                    break;
                            }
                        }
                        break;

                    case "stock_bajo":
                        foreach (DataGridViewColumn column in dgvDatos.Columns)
                        {
                            switch (column.Name.ToLower())
                            {
                                case "codigo":
                                    column.HeaderText = "Código";
                                    column.Width = 80;
                                    break;
                                case "nombre":
                                    column.HeaderText = "Producto";
                                    column.Width = 200;
                                    break;
                                case "categoria_nombre":
                                    column.HeaderText = "Categoría";
                                    column.Width = 100;
                                    break;
                                case "stock_actual":
                                    column.HeaderText = "Stock Actual";
                                    column.Width = 80;
                                    break;
                                case "stock_minimo":
                                    column.HeaderText = "Stock Mínimo";
                                    column.Width = 80;
                                    break;
                                case "cantidad_necesaria":
                                    column.HeaderText = "Faltante";
                                    column.Width = 80;
                                    break;
                                default:
                                    column.Visible = false;
                                    break;
                            }
                        }
                        break;

                    // Nuevas configuraciones para reportes de ventas
                    case "ventas_rango":
                        ConfigurarColumnasVentasRango();
                        break;
                    case "ventas_resumen":
                        ConfigurarColumnasVentasResumen();
                        break;
                    case "ventas_detalle":
                        ConfigurarColumnasVentasDetalle();
                        break;
                    case "top_clientes":
                        ConfigurarColumnasTopClientes();
                        break;
                    case "top_productos":
                        ConfigurarColumnasTopProductos();
                        break;
                    case "top_servicios":
                        ConfigurarColumnasTopServicios();
                        break;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error configurando columnas: {ex.Message}");
            }
        }

        // ============================================
        // MÉTODOS PARA REPORTES DE VENTAS COMPLETOS
        // ============================================

        private void GenerarReporteVentasPorRango(DateTime fechaInicio, DateTime fechaFin, string estado = null)
        {
            try
            {
                if (dgvDatos == null) return;

                DataTable ventas = NVentas.ReporteVentasPorRango(fechaInicio, fechaFin, estado);
                if (ventas != null && ventas.Rows.Count > 0)
                {
                    dgvDatos.DataSource = ventas;
                    ConfigurarColumnasDGV("ventas_rango");

                    if (lblTituloReporte != null)
                        lblTituloReporte.Text = $"Reporte de Ventas ({fechaInicio:dd/MM/yyyy} - {fechaFin:dd/MM/yyyy})";

                    MostrarResumenVentas(ventas);
                }
                else
                {
                    MessageBox.Show($"No se encontraron ventas en el período del {fechaInicio:dd/MM/yyyy} al {fechaFin:dd/MM/yyyy}",
                        "Sin Datos", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    if (dgvDatos != null) dgvDatos.DataSource = null;
                    if (lblTituloReporte != null) lblTituloReporte.Text = "Sin ventas en el período seleccionado";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error generando reporte de ventas: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void GenerarReporteVentasResumen(DateTime fechaInicio, DateTime fechaFin, string agrupacion = "DIA")
        {
            try
            {
                if (dgvDatos == null) return;

                DataTable resumen = NVentas.ReporteVentasResumen(fechaInicio, fechaFin, agrupacion);
                if (resumen != null && resumen.Rows.Count > 0)
                {
                    dgvDatos.DataSource = resumen;
                    ConfigurarColumnasDGV("ventas_resumen");

                    if (lblTituloReporte != null)
                        lblTituloReporte.Text = $"Resumen de Ventas por {agrupacion} ({fechaInicio:dd/MM/yyyy} - {fechaFin:dd/MM/yyyy})";
                }
                else
                {
                    MessageBox.Show("No se encontraron datos para el resumen de ventas", "Sin Datos",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error generando resumen de ventas: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void GenerarReporteVentasDetalle(DateTime fechaInicio, DateTime fechaFin)
        {
            try
            {
                if (dgvDatos == null) return;

                DataTable detalle = NVentas.ReporteVentasDetalle(fechaInicio, fechaFin);
                if (detalle != null && detalle.Rows.Count > 0)
                {
                    dgvDatos.DataSource = detalle;
                    ConfigurarColumnasDGV("ventas_detalle");

                    if (lblTituloReporte != null)
                        lblTituloReporte.Text = $"Detalle de Ventas ({fechaInicio:dd/MM/yyyy} - {fechaFin:dd/MM/yyyy})";
                }
                else
                {
                    MessageBox.Show("No se encontraron detalles de ventas", "Sin Datos",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error generando detalle de ventas: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void GenerarReportePeriodoPredefinido(string periodo)
        {
            try
            {
                if (dgvDatos == null) return;

                DataTable ventas = NVentas.ReporteVentasPeriodosPredefinidos(periodo);
                if (ventas != null && ventas.Rows.Count > 0)
                {
                    dgvDatos.DataSource = ventas;
                    ConfigurarColumnasDGV("ventas_rango");

                    string nombrePeriodo = NVentas.ObtenerNombrePeriodo(periodo);
                    if (lblTituloReporte != null)
                        lblTituloReporte.Text = $"Reporte de {nombrePeriodo}";

                    MostrarResumenVentas(ventas);
                }
                else
                {
                    string nombrePeriodo = NVentas.ObtenerNombrePeriodo(periodo);
                    MessageBox.Show($"No se encontraron ventas para {nombrePeriodo}", "Sin Datos",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error generando reporte de período: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void GenerarReporteTopClientes(DateTime fechaInicio, DateTime fechaFin, int topCount = 10)
        {
            try
            {
                if (dgvDatos == null) return;

                DataTable topClientes = NVentas.ReporteVentasTopClientes(fechaInicio, fechaFin, topCount);
                if (topClientes != null && topClientes.Rows.Count > 0)
                {
                    dgvDatos.DataSource = topClientes;
                    ConfigurarColumnasDGV("top_clientes");

                    if (lblTituloReporte != null)
                        lblTituloReporte.Text = $"Top {topCount} Clientes ({fechaInicio:dd/MM/yyyy} - {fechaFin:dd/MM/yyyy})";
                }
                else
                {
                    MessageBox.Show("No se encontraron datos de clientes", "Sin Datos",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error generando reporte de top clientes: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void GenerarReporteTopProductos(DateTime fechaInicio, DateTime fechaFin, int topCount = 10)
        {
            try
            {
                if (dgvDatos == null) return;

                DataTable topProductos = NVentas.ReporteVentasProductosTop(fechaInicio, fechaFin, topCount);
                if (topProductos != null && topProductos.Rows.Count > 0)
                {
                    dgvDatos.DataSource = topProductos;
                    ConfigurarColumnasDGV("top_productos");

                    if (lblTituloReporte != null)
                        lblTituloReporte.Text = $"Top {topCount} Productos Vendidos ({fechaInicio:dd/MM/yyyy} - {fechaFin:dd/MM/yyyy})";
                }
                else
                {
                    MessageBox.Show("No se encontraron datos de productos vendidos", "Sin Datos",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error generando reporte de top productos: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void GenerarReporteTopServicios(DateTime fechaInicio, DateTime fechaFin, int topCount = 10)
        {
            try
            {
                if (dgvDatos == null) return;

                DataTable topServicios = NVentas.ReporteVentasServiciosTop(fechaInicio, fechaFin, topCount);
                if (topServicios != null && topServicios.Rows.Count > 0)
                {
                    dgvDatos.DataSource = topServicios;
                    ConfigurarColumnasDGV("top_servicios");

                    if (lblTituloReporte != null)
                        lblTituloReporte.Text = $"Top {topCount} Servicios Prestados ({fechaInicio:dd/MM/yyyy} - {fechaFin:dd/MM/yyyy})";
                }
                else
                {
                    MessageBox.Show("No se encontraron datos de servicios prestados", "Sin Datos",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error generando reporte de top servicios: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void MostrarResumenVentas(DataTable ventas)
        {
            try
            {
                if (ventas == null || ventas.Rows.Count == 0) return;

                decimal totalVentas = 0;
                int totalFacturas = ventas.Rows.Count;

                foreach (DataRow row in ventas.Rows)
                {
                    if (decimal.TryParse(row["total"]?.ToString(), out decimal total))
                    {
                        totalVentas += total;
                    }
                }

                decimal promedioVentas = totalFacturas > 0 ? totalVentas / totalFacturas : 0;

                string resumen = $"Total Facturas: {totalFacturas} | " +
                               $"Total Ventas: {totalVentas:C2} | " +
                               $"Promedio: {promedioVentas:C2}";

                MessageBox.Show(resumen, "Resumen de Ventas", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error mostrando resumen: {ex.Message}");
            }
        }

        private void ConfigurarColumnasReportesVentas(string tipoReporte)
        {
            if (dgvDatos?.DataSource == null) return;

            try
            {
                switch (tipoReporte.ToLower())
                {
                    case "ventas_rango":
                        ConfigurarColumnasVentasRango();
                        break;
                    case "ventas_resumen":
                        ConfigurarColumnasVentasResumen();
                        break;
                    case "ventas_detalle":
                        ConfigurarColumnasVentasDetalle();
                        break;
                    case "top_clientes":
                        ConfigurarColumnasTopClientes();
                        break;
                    case "top_productos":
                        ConfigurarColumnasTopProductos();
                        break;
                    case "top_servicios":
                        ConfigurarColumnasTopServicios();
                        break;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error configurando columnas: {ex.Message}");
            }
        }

        private void ConfigurarColumnasVentasRango()
        {
            foreach (DataGridViewColumn column in dgvDatos.Columns)
            {
                switch (column.Name.ToLower())
                {
                    case "id":
                        column.HeaderText = "ID";
                        column.Width = 50;
                        break;
                    case "numero_factura":
                        column.HeaderText = "N° Factura";
                        column.Width = 100;
                        break;
                    case "fecha_emision":
                        column.HeaderText = "Fecha";
                        column.Width = 80;
                        break;
                    case "cliente":
                        column.HeaderText = "Cliente";
                        column.Width = 200;
                        break;
                    case "subtotal":
                        column.HeaderText = "Subtotal";
                        column.Width = 80;
                        column.DefaultCellStyle.Format = "C2";
                        break;
                    case "impuestos":
                        column.HeaderText = "Impuestos";
                        column.Width = 80;
                        column.DefaultCellStyle.Format = "C2";
                        break;
                    case "descuentos":
                        column.HeaderText = "Descuentos";
                        column.Width = 80;
                        column.DefaultCellStyle.Format = "C2";
                        break;
                    case "total":
                        column.HeaderText = "Total";
                        column.Width = 80;
                        column.DefaultCellStyle.Format = "C2";
                        break;
                    case "estado":
                        column.HeaderText = "Estado";
                        column.Width = 80;
                        break;
                    case "total_productos":
                        column.HeaderText = "# Productos";
                        column.Width = 80;
                        break;
                    case "total_servicios":
                        column.HeaderText = "# Servicios";
                        column.Width = 80;
                        break;
                    default:
                        column.Visible = false;
                        break;
                }
            }
        }

        private void ConfigurarColumnasVentasResumen()
        {
            foreach (DataGridViewColumn column in dgvDatos.Columns)
            {
                switch (column.Name.ToLower())
                {
                    case "periodo":
                    case "año":
                    case "mes":
                    case "semana":
                        column.HeaderText = "Período";
                        column.Width = 100;
                        break;
                    case "nombre_mes":
                        column.HeaderText = "Mes";
                        column.Width = 80;
                        break;
                    case "dia_semana":
                        column.HeaderText = "Día";
                        column.Width = 80;
                        break;
                    case "total_facturas":
                        column.HeaderText = "Total Facturas";
                        column.Width = 100;
                        break;
                    case "facturas_pagadas":
                        column.HeaderText = "Pagadas";
                        column.Width = 80;
                        break;
                    case "facturas_pendientes":
                        column.HeaderText = "Pendientes";
                        column.Width = 80;
                        break;
                    case "ventas_netas":
                        column.HeaderText = "Ventas Netas";
                        column.Width = 120;
                        column.DefaultCellStyle.Format = "C2";
                        break;
                    case "promedio_venta":
                        column.HeaderText = "Promedio";
                        column.Width = 100;
                        column.DefaultCellStyle.Format = "C2";
                        break;
                    case "venta_minima":
                        column.HeaderText = "Mínima";
                        column.Width = 80;
                        column.DefaultCellStyle.Format = "C2";
                        break;
                    case "venta_maxima":
                        column.HeaderText = "Máxima";
                        column.Width = 80;
                        column.DefaultCellStyle.Format = "C2";
                        break;
                    default:
                        column.Visible = false;
                        break;
                }
            }
        }

        private void ConfigurarColumnasVentasDetalle()
        {
            foreach (DataGridViewColumn column in dgvDatos.Columns)
            {
                switch (column.Name.ToLower())
                {
                    case "tipo_item":
                        column.HeaderText = "Tipo";
                        column.Width = 80;
                        break;
                    case "numero_factura":
                        column.HeaderText = "N° Factura";
                        column.Width = 100;
                        break;
                    case "fecha_emision":
                        column.HeaderText = "Fecha";
                        column.Width = 80;
                        break;
                    case "cliente":
                        column.HeaderText = "Cliente";
                        column.Width = 150;
                        break;
                    case "codigo_item":
                        column.HeaderText = "Código";
                        column.Width = 80;
                        break;
                    case "nombre_item":
                        column.HeaderText = "Producto/Servicio";
                        column.Width = 200;
                        break;
                    case "categoria":
                        column.HeaderText = "Categoría";
                        column.Width = 100;
                        break;
                    case "cantidad":
                        column.HeaderText = "Cantidad";
                        column.Width = 80;
                        break;
                    case "precio_unitario":
                        column.HeaderText = "Precio Unit.";
                        column.Width = 80;
                        column.DefaultCellStyle.Format = "C2";
                        break;
                    case "subtotal":
                        column.HeaderText = "Subtotal";
                        column.Width = 80;
                        column.DefaultCellStyle.Format = "C2";
                        break;
                    default:
                        column.Visible = false;
                        break;
                }
            }
        }

        private void ConfigurarColumnasTopClientes()
        {
            foreach (DataGridViewColumn column in dgvDatos.Columns)
            {
                switch (column.Name.ToLower())
                {
                    case "cliente":
                        column.HeaderText = "Cliente";
                        column.Width = 200;
                        break;
                    case "tipo_cliente":
                        column.HeaderText = "Tipo";
                        column.Width = 80;
                        break;
                    case "total_facturas":
                        column.HeaderText = "# Facturas";
                        column.Width = 80;
                        break;
                    case "total_compras":
                        column.HeaderText = "Total Compras";
                        column.Width = 120;
                        column.DefaultCellStyle.Format = "C2";
                        break;
                    case "promedio_compra":
                        column.HeaderText = "Promedio";
                        column.Width = 100;
                        column.DefaultCellStyle.Format = "C2";
                        break;
                    case "ultima_compra":
                        column.HeaderText = "Última Compra";
                        column.Width = 100;
                        break;
                    default:
                        column.Visible = false;
                        break;
                }
            }
        }

        private void ConfigurarColumnasTopProductos()
        {
            foreach (DataGridViewColumn column in dgvDatos.Columns)
            {
                switch (column.Name.ToLower())
                {
                    case "codigo":
                        column.HeaderText = "Código";
                        column.Width = 80;
                        break;
                    case "producto":
                        column.HeaderText = "Producto";
                        column.Width = 200;
                        break;
                    case "categoria":
                        column.HeaderText = "Categoría";
                        column.Width = 100;
                        break;
                    case "total_vendido":
                        column.HeaderText = "Cantidad Vendida";
                        column.Width = 100;
                        break;
                    case "ingresos_generados":
                        column.HeaderText = "Ingresos";
                        column.Width = 100;
                        column.DefaultCellStyle.Format = "C2";
                        break;
                    case "precio_promedio":
                        column.HeaderText = "Precio Prom.";
                        column.Width = 80;
                        column.DefaultCellStyle.Format = "C2";
                        break;
                    case "stock_actual":
                        column.HeaderText = "Stock";
                        column.Width = 60;
                        break;
                    case "estado_stock":
                        column.HeaderText = "Estado Stock";
                        column.Width = 100;
                        break;
                    default:
                        column.Visible = false;
                        break;
                }
            }
        }

        private void ConfigurarColumnasTopServicios()
        {
            foreach (DataGridViewColumn column in dgvDatos.Columns)
            {
                switch (column.Name.ToLower())
                {
                    case "codigo":
                        column.HeaderText = "Código";
                        column.Width = 80;
                        break;
                    case "servicio":
                        column.HeaderText = "Servicio";
                        column.Width = 200;
                        break;
                    case "categoria":
                        column.HeaderText = "Categoría";
                        column.Width = 100;
                        break;
                    case "total_prestado":
                        column.HeaderText = "Veces Prestado";
                        column.Width = 100;
                        break;
                    case "ingresos_generados":
                        column.HeaderText = "Ingresos";
                        column.Width = 100;
                        column.DefaultCellStyle.Format = "C2";
                        break;
                    case "precio_promedio":
                        column.HeaderText = "Precio Prom.";
                        column.Width = 80;
                        column.DefaultCellStyle.Format = "C2";
                        break;
                    case "precio_base":
                        column.HeaderText = "Precio Base";
                        column.Width = 80;
                        column.DefaultCellStyle.Format = "C2";
                        break;
                    case "veterinarios_involucrados":
                        column.HeaderText = "# Veterinarios";
                        column.Width = 100;
                        break;
                    default:
                        column.Visible = false;
                        break;
                }
            }
        }

        // ============================================
        // MÉTODOS DE EXPORTACIÓN
        // ============================================

        private void ExportarReporte(string formato)
        {
            try
            {
                if (dgvDatos?.DataSource == null)
                {
                    MessageBox.Show("No hay datos para exportar", "Sin Datos",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                SaveFileDialog saveDialog = new SaveFileDialog();
                string tituloReporte = lblTituloReporte?.Text ?? "Reporte";
                string nombreArchivo = $"{tituloReporte}_{DateTime.Now:yyyyMMdd_HHmmss}";

                switch (formato.ToUpper())
                {
                    case "EXCEL":
                        saveDialog.Filter = "Archivos Excel (*.csv)|*.csv|Todos los archivos (*.*)|*.*";
                        saveDialog.FileName = $"{nombreArchivo}.csv";
                        break;
                    case "PDF":
                        saveDialog.Filter = "Archivos PDF (*.txt)|*.txt|Todos los archivos (*.*)|*.*";
                        saveDialog.FileName = $"{nombreArchivo}_reporte.txt";
                        break;
                    case "WORD":
                        saveDialog.Filter = "Archivos de texto (*.txt)|*.txt|Todos los archivos (*.*)|*.*";
                        saveDialog.FileName = $"{nombreArchivo}_documento.txt";
                        break;
                    default:
                        saveDialog.Filter = "Archivos de texto (*.txt)|*.txt|Todos los archivos (*.*)|*.*";
                        saveDialog.FileName = $"{nombreArchivo}.txt";
                        break;
                }

                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    switch (formato.ToUpper())
                    {
                        case "EXCEL":
                            ExportarACSV(saveDialog.FileName);
                            break;
                        case "PDF":
                            ExportarAPDF(saveDialog.FileName);
                            break;
                        case "WORD":
                            ExportarAWord(saveDialog.FileName);
                            break;
                        default:
                            ExportarATexto(saveDialog.FileName);
                            break;
                    }

                    MessageBox.Show($"Reporte exportado exitosamente a:\n{saveDialog.FileName}", "Exportación Exitosa",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error exportando reporte: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ExportarACSV(string rutaArchivo)
        {
            try
            {
                DataTable data = (DataTable)dgvDatos.DataSource;
                using (var writer = new System.IO.StreamWriter(rutaArchivo, false, System.Text.Encoding.UTF8))
                {
                    // Escribir encabezados
                    var encabezados = new List<string>();
                    foreach (DataGridViewColumn column in dgvDatos.Columns)
                    {
                        if (column.Visible)
                        {
                            encabezados.Add($"\"{column.HeaderText}\"");
                        }
                    }
                    writer.WriteLine(string.Join(",", encabezados));

                    // Escribir datos
                    foreach (DataRow row in data.Rows)
                    {
                        var valores = new List<string>();
                        foreach (DataGridViewColumn column in dgvDatos.Columns)
                        {
                            if (column.Visible)
                            {
                                string valor = row[column.Name]?.ToString() ?? "";
                                valores.Add($"\"{valor}\"");
                            }
                        }
                        writer.WriteLine(string.Join(",", valores));
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error exportando a CSV: {ex.Message}");
            }
        }

        private void ExportarAPDF(string rutaArchivo)
        {
            try
            {
                DataTable data = (DataTable)dgvDatos.DataSource;
                using (var writer = new System.IO.StreamWriter(rutaArchivo, false, System.Text.Encoding.UTF8))
                {
                    writer.WriteLine("=".PadRight(100, '='));
                    writer.WriteLine($" {lblTituloReporte?.Text ?? "REPORTE DE SISTEMA VETERINARIO"}");
                    writer.WriteLine($" Generado el: {DateTime.Now:dd/MM/yyyy HH:mm:ss}");
                    writer.WriteLine("=".PadRight(100, '='));
                    writer.WriteLine();

                    // Escribir encabezados
                    var encabezados = new List<string>();
                    foreach (DataGridViewColumn column in dgvDatos.Columns)
                    {
                        if (column.Visible)
                        {
                            encabezados.Add(column.HeaderText.PadRight(15));
                        }
                    }
                    writer.WriteLine(string.Join(" | ", encabezados));
                    writer.WriteLine("-".PadRight(100, '-'));

                    // Escribir datos
                    foreach (DataRow row in data.Rows)
                    {
                        var valores = new List<string>();
                        foreach (DataGridViewColumn column in dgvDatos.Columns)
                        {
                            if (column.Visible)
                            {
                                string valor = row[column.Name]?.ToString() ?? "";
                                if (valor.Length > 15) valor = valor.Substring(0, 12) + "...";
                                valores.Add(valor.PadRight(15));
                            }
                        }
                        writer.WriteLine(string.Join(" | ", valores));
                    }

                    writer.WriteLine();
                    writer.WriteLine($"Total de registros: {data.Rows.Count}");
                    writer.WriteLine("=".PadRight(100, '='));
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error exportando a PDF: {ex.Message}");
            }
        }

        private void ExportarAWord(string rutaArchivo)
        {
            try
            {
                DataTable data = (DataTable)dgvDatos.DataSource;
                using (var writer = new System.IO.StreamWriter(rutaArchivo, false, System.Text.Encoding.UTF8))
                {
                    writer.WriteLine($"{lblTituloReporte?.Text ?? "REPORTE DE SISTEMA VETERINARIO"}");
                    writer.WriteLine($"Fecha de generación: {DateTime.Now:dd/MM/yyyy HH:mm:ss}");
                    writer.WriteLine();

                    // Escribir encabezados en formato tabla
                    writer.WriteLine("DATOS DEL REPORTE:");
                    writer.WriteLine("─".PadRight(80, '─'));

                    foreach (DataRow row in data.Rows)
                    {
                        writer.WriteLine();
                        foreach (DataGridViewColumn column in dgvDatos.Columns)
                        {
                            if (column.Visible)
                            {
                                string valor = row[column.Name]?.ToString() ?? "";
                                writer.WriteLine($"{column.HeaderText}: {valor}");
                            }
                        }
                        writer.WriteLine("─".PadRight(80, '─'));
                    }

                    writer.WriteLine();
                    writer.WriteLine($"Total de registros en el reporte: {data.Rows.Count}");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error exportando a Word: {ex.Message}");
            }
        }

        private void ExportarATexto(string rutaArchivo)
        {
            try
            {
                DataTable data = (DataTable)dgvDatos.DataSource;
                using (var writer = new System.IO.StreamWriter(rutaArchivo, false, System.Text.Encoding.UTF8))
                {
                    writer.WriteLine($"REPORTE: {lblTituloReporte?.Text ?? "Sistema Veterinario"}");
                    writer.WriteLine($"FECHA: {DateTime.Now:dd/MM/yyyy HH:mm:ss}");
                    writer.WriteLine(new string('=', 50));
                    writer.WriteLine();

                    foreach (DataRow row in data.Rows)
                    {
                        foreach (DataGridViewColumn column in dgvDatos.Columns)
                        {
                            if (column.Visible)
                            {
                                string valor = row[column.Name]?.ToString() ?? "";
                                writer.WriteLine($"{column.HeaderText}: {valor}");
                            }
                        }
                        writer.WriteLine(new string('-', 30));
                    }

                    writer.WriteLine($"\nTotal: {data.Rows.Count} registros");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error exportando a texto: {ex.Message}");
            }
        }

        private void ImprimirReporte()
        {
            try
            {
                if (dgvDatos?.DataSource == null)
                {
                    MessageBox.Show("No hay datos para imprimir", "Sin Datos",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                MessageBox.Show("Funcionalidad de impresión disponible:\n\n" +
                    "1. Puede exportar a PDF/Word y luego imprimir desde esas aplicaciones\n" +
                    "2. Use Ctrl+P para imprimir directamente el reporte visible\n" +
                    "3. La función de impresión directa se puede implementar con PrintDocument",
                    "Opciones de Impresión", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error preparando impresión: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // UserControls no manejan FormClosing
    }
}