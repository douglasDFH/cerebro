using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using CapaNegocio;

namespace SistemVeterinario
{
    public partial class Producto : Form
    {
        private bool IsEditing = false;
        private int CurrentProductoId = 0;
        private int CurrentCategoriaId = 0;

        public Producto()
        {
            InitializeComponent();
            ValidarControlesInicializados();
            ConfigurarFormulario();
            
            if (dgvProductos != null)
            {
                CargarDatos();
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("No se cargan datos: dgvProductos es null");
            }
        }

        private void ValidarControlesInicializados()
        {
            var controlesRequeridos = new Dictionary<string, Control?>
            {
                { "tabControl", tabControl },
                { "dgvProductos", dgvProductos },
                { "btnNuevo", btnNuevo },
                { "btnGuardar", btnGuardar },
                { "btnEditar", btnEditar },
                { "btnEliminar", btnEliminar },
                { "btnCancelar", btnCancelar },
                { "cmbCategoria", cmbCategoria }
            };

            var controlesNulos = controlesRequeridos.Where(c => c.Value == null).Select(c => c.Key).ToList();

            if (controlesNulos.Any())
            {
                string mensaje = $"Controles no inicializados: {string.Join(", ", controlesNulos)}";
                System.Diagnostics.Debug.WriteLine(mensaje);
                MessageBox.Show(mensaje, "Error de Inicialización", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void ConfigurarFormulario()
        {
            try
            {
                ConfigurarDataGridView();
                ConfigurarEventos();
                ConfigurarControles();
                ConfigurarControlesIniciales();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error configurando formulario: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConfigurarDataGridView()
        {
            if (dgvProductos == null) return;

            try
            {
                dgvProductos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
                dgvProductos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgvProductos.MultiSelect = false;
                dgvProductos.ReadOnly = true;
                dgvProductos.AllowUserToAddRows = false;
                dgvProductos.AllowUserToDeleteRows = false;
                dgvProductos.RowHeadersVisible = false;
                dgvProductos.BackgroundColor = Color.White;
                dgvProductos.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(240, 240, 240);
                dgvProductos.AutoGenerateColumns = true;
                dgvProductos.BorderStyle = BorderStyle.FixedSingle;
                
                dgvProductos.CellClick += DgvProductos_CellClick;
                dgvProductos.CellDoubleClick += DgvProductos_CellDoubleClick;
                dgvProductos.CellFormatting += DgvProductos_CellFormatting;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error configurando DataGridView: {ex.Message}");
            }
        }

        private void ConfigurarEventos()
        {
            try
            {
                if (btnNuevo != null) btnNuevo.Click += BtnNuevo_Click;
                if (btnGuardar != null) btnGuardar.Click += BtnGuardar_Click;
                if (btnEditar != null) btnEditar.Click += BtnEditar_Click;
                if (btnEliminar != null) btnEliminar.Click += BtnEliminar_Click;
                if (btnCancelar != null) btnCancelar.Click += BtnCancelar_Click;
                if (btnBuscar != null) btnBuscar.Click += BtnBuscar_Click;
                if (btnRefrescar != null) btnRefrescar.Click += BtnRefrescar_Click;
                if (btnGenerarCodigo != null) btnGenerarCodigo.Click += BtnGenerarCodigo_Click;
                if (btnNuevaCategoria != null) btnNuevaCategoria.Click += BtnNuevaCategoria_Click;
                if (btnStockBajo != null) btnStockBajo.Click += BtnStockBajo_Click;
                if (txtNombre != null) txtNombre.TextChanged += TxtNombre_TextChanged;
                if (cmbCategoria != null) cmbCategoria.SelectedIndexChanged += CmbCategoria_SelectedIndexChanged;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error configurando eventos: {ex.Message}");
            }
        }

        private void ConfigurarControles()
        {
            try
            {
                // Configurar NumericUpDown
                if (nudPrecio != null)
                {
                    nudPrecio.DecimalPlaces = 2;
                    nudPrecio.Maximum = 999999.99m;
                    nudPrecio.Minimum = 0.01m;
                    nudPrecio.Value = 0.01m;
                }

                if (nudStockMinimo != null)
                {
                    nudStockMinimo.Maximum = 99999;
                    nudStockMinimo.Minimum = 0;
                    nudStockMinimo.Value = 5;
                }

                if (nudStockActual != null)
                {
                    nudStockActual.Maximum = 99999;
                    nudStockActual.Minimum = 0;
                    nudStockActual.Value = 0;
                }

                // Cargar categorías
                CargarCategorias();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error configurando controles: {ex.Message}");
            }
        }

        private void ConfigurarControlesIniciales()
        {
            EstablecerEstadoControles(false);
            LimpiarCampos();
        }

        private void CargarCategorias()
        {
            try
            {
                if (cmbCategoria == null) return;

                cmbCategoria.Items.Clear();
                cmbCategoria.DisplayMember = "nombre";
                cmbCategoria.ValueMember = "id";

                DataTable categorias = NProductos.ObtenerCategorias();
                if (categorias != null && categorias.Rows.Count > 0)
                {
                    cmbCategoria.DataSource = categorias;
                    cmbCategoria.SelectedIndex = 0;
                }
                else
                {
                    MessageBox.Show("No hay categorías disponibles. Cree al menos una categoría antes de registrar productos.",
                        "Sin Categorías", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error cargando categorías: {ex.Message}");
                MessageBox.Show($"Error cargando categorías: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarDatos()
        {
            try
            {
                if (dgvProductos == null) 
                {
                    System.Diagnostics.Debug.WriteLine("dgvProductos es null en CargarDatos");
                    return;
                }

                System.Diagnostics.Debug.WriteLine("Iniciando carga de datos de productos...");
                DataTable dt = NProductos.Mostrar();
                
                if (dt != null)
                {
                    System.Diagnostics.Debug.WriteLine($"DataTable obtenido con {dt.Rows.Count} filas y {dt.Columns.Count} columnas");
                    
                    foreach (DataColumn column in dt.Columns)
                    {
                        System.Diagnostics.Debug.WriteLine($"Columna disponible: {column.ColumnName}");
                    }
                    
                    dgvProductos.DataSource = dt;
                    dgvProductos.Refresh();
                    
                    ConfigurarColumnasDataGridView();
                    ActualizarTotalRegistros();
                    
                    System.Diagnostics.Debug.WriteLine($"Datos cargados exitosamente: {dt.Rows.Count} registros");
                }
                else
                {
                    dgvProductos.DataSource = null;
                    ActualizarTotalRegistros();
                    System.Diagnostics.Debug.WriteLine("DataTable es null - no se obtuvieron datos");
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error cargando datos: {ex.Message}");
                MessageBox.Show($"Error cargando datos de productos: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                
                if (dgvProductos != null)
                {
                    dgvProductos.DataSource = null;
                    ActualizarTotalRegistros();
                }
            }
        }

        private void ConfigurarColumnasDataGridView()
        {
            if (dgvProductos?.DataSource == null || dgvProductos.Columns.Count == 0) 
            {
                System.Diagnostics.Debug.WriteLine("No hay columnas para configurar en productos");
                return;
            }

            try
            {
                System.Diagnostics.Debug.WriteLine($"Configurando {dgvProductos.Columns.Count} columnas de productos");
                
                foreach (DataGridViewColumn column in dgvProductos.Columns)
                {
                    if (column == null) continue;

                    string columnName = column.Name?.ToLower() ?? "";
                    System.Diagnostics.Debug.WriteLine($"Configurando columna: {columnName}");
                    
                    switch (columnName)
                    {
                        case "id":
                            column.HeaderText = "ID";
                            column.Width = 50;
                            column.Visible = true;
                            column.DisplayIndex = 0;
                            break;
                        case "codigo":
                            column.HeaderText = "Código";
                            column.Width = 80;
                            column.Visible = true;
                            column.DisplayIndex = 1;
                            break;
                        case "nombre":
                            column.HeaderText = "Nombre";
                            column.Width = 200;
                            column.Visible = true;
                            column.DisplayIndex = 2;
                            break;
                        case "categoria_nombre":
                            column.HeaderText = "Categoría";
                            column.Width = 120;
                            column.Visible = true;
                            column.DisplayIndex = 3;
                            break;
                        case "precio":
                            column.HeaderText = "Precio";
                            column.Width = 80;
                            column.Visible = true;
                            column.DisplayIndex = 4;
                            column.DefaultCellStyle.Format = "C2";
                            column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                            break;
                        case "stock_actual":
                            column.HeaderText = "Stock";
                            column.Width = 60;
                            column.Visible = true;
                            column.DisplayIndex = 5;
                            column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                            break;
                        case "stock_minimo":
                            column.HeaderText = "Stock Mín.";
                            column.Width = 70;
                            column.Visible = true;
                            column.DisplayIndex = 6;
                            column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                            break;
                        case "estado_stock":
                            column.HeaderText = "Estado";
                            column.Width = 80;
                            column.Visible = true;
                            column.DisplayIndex = 7;
                            column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                            break;
                        case "requiere_receta":
                            column.HeaderText = "Receta";
                            column.Width = 60;
                            column.Visible = true;
                            column.DisplayIndex = 8;
                            column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                            break;
                        case "descripcion":
                            column.HeaderText = "Descripción";
                            column.Width = 150;
                            column.Visible = true;
                            column.DisplayIndex = 9;
                            break;
                        default:
                            // Ocultar todas las demás columnas
                            column.Visible = false;
                            System.Diagnostics.Debug.WriteLine($"Ocultando columna: {columnName}");
                            break;
                    }
                }
                
                System.Diagnostics.Debug.WriteLine("Configuración de columnas de productos completada");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error configurando columnas: {ex.Message}");
                MessageBox.Show($"Error configurando columnas del grid: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void ActualizarTotalRegistros()
        {
            if (dgvProductos != null && lblTotalRegistros != null)
            {
                lblTotalRegistros.Text = $"Total de registros: {dgvProductos.Rows.Count}";
            }
        }

        private void EstablecerEstadoControles(bool habilitado)
        {
            try
            {
                if (grpDatosProducto != null)
                {
                    foreach (Control control in grpDatosProducto.Controls)
                    {
                        if (control is TextBox || control is ComboBox || control is NumericUpDown || control is CheckBox)
                        {
                            control.Enabled = habilitado;
                        }
                    }
                }

                if (btnGuardar != null) btnGuardar.Enabled = habilitado;
                if (btnCancelar != null) btnCancelar.Enabled = habilitado;
                if (btnGenerarCodigo != null) btnGenerarCodigo.Enabled = habilitado;
                if (btnNuevaCategoria != null) btnNuevaCategoria.Enabled = habilitado;
                if (btnNuevo != null) btnNuevo.Enabled = !habilitado;
                if (btnEditar != null) btnEditar.Enabled = !habilitado && dgvProductos?.SelectedRows.Count > 0;
                if (btnEliminar != null) btnEliminar.Enabled = !habilitado && dgvProductos?.SelectedRows.Count > 0;
                if (btnStockBajo != null) btnStockBajo.Enabled = !habilitado;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error estableciendo estado de controles: {ex.Message}");
            }
        }

        private void LimpiarCampos()
        {
            try
            {
                if (txtCodigo != null) txtCodigo.Text = "";
                if (txtNombre != null) txtNombre.Text = "";
                if (txtDescripcion != null) txtDescripcion.Text = "";
                if (nudPrecio != null) nudPrecio.Value = 0.01m;
                if (nudStockMinimo != null) nudStockMinimo.Value = 5;
                if (nudStockActual != null) nudStockActual.Value = 0;
                if (chkRequiereReceta != null) chkRequiereReceta.Checked = false;
                if (cmbCategoria != null && cmbCategoria.Items.Count > 0) cmbCategoria.SelectedIndex = 0;

                CurrentProductoId = 0;
                CurrentCategoriaId = 0;
                IsEditing = false;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error limpiando campos: {ex.Message}");
            }
        }

        private void CargarDatosEnFormulario(DataGridViewRow row)
        {
            try
            {
                if (row?.Cells == null) return;

                CurrentProductoId = Convert.ToInt32(row.Cells["id"]?.Value ?? 0);

                if (txtCodigo != null) txtCodigo.Text = row.Cells["codigo"]?.Value?.ToString() ?? "";
                if (txtNombre != null) txtNombre.Text = row.Cells["nombre"]?.Value?.ToString() ?? "";
                if (txtDescripcion != null) txtDescripcion.Text = row.Cells["descripcion"]?.Value?.ToString() ?? "";

                if (nudPrecio != null)
                {
                    var precio = row.Cells["precio"]?.Value;
                    if (precio != null && precio != DBNull.Value)
                    {
                        nudPrecio.Value = Convert.ToDecimal(precio);
                    }
                }

                if (nudStockMinimo != null)
                {
                    var stockMin = row.Cells["stock_minimo"]?.Value;
                    if (stockMin != null && stockMin != DBNull.Value)
                    {
                        nudStockMinimo.Value = Convert.ToDecimal(stockMin);
                    }
                }

                if (nudStockActual != null)
                {
                    var stockActual = row.Cells["stock_actual"]?.Value;
                    if (stockActual != null && stockActual != DBNull.Value)
                    {
                        nudStockActual.Value = Convert.ToDecimal(stockActual);
                    }
                }

                if (chkRequiereReceta != null)
                {
                    var requiereReceta = row.Cells["requiere_receta"]?.Value;
                    chkRequiereReceta.Checked = requiereReceta != null && Convert.ToBoolean(requiereReceta);
                }

                // Seleccionar categoría
                if (cmbCategoria != null)
                {
                    var categoriaId = row.Cells["categoria_id"]?.Value;
                    if (categoriaId != null && categoriaId != DBNull.Value)
                    {
                        CurrentCategoriaId = Convert.ToInt32(categoriaId);
                        cmbCategoria.SelectedValue = CurrentCategoriaId;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error cargando datos en formulario: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ValidarCampos()
        {
            try
            {
                string codigo = txtCodigo?.Text?.Trim() ?? "";
                string nombre = txtNombre?.Text?.Trim() ?? "";
                string descripcion = txtDescripcion?.Text?.Trim() ?? "";
                decimal precio = nudPrecio?.Value ?? 0;
                int stockMinimo = (int)(nudStockMinimo?.Value ?? 0);
                int stockActual = (int)(nudStockActual?.Value ?? 0);
                int categoriaId = 0;

                if (cmbCategoria?.SelectedValue != null)
                {
                    int.TryParse(cmbCategoria.SelectedValue.ToString(), out categoriaId);
                }

                string errores = NProductos.ValidarDatosProducto(codigo, nombre, precio, categoriaId, 
                    stockMinimo, stockActual, descripcion);

                if (!string.IsNullOrEmpty(errores))
                {
                    MessageBox.Show(errores, "Errores de Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }

                // Validar código único
                string errorCodigo = NProductos.ValidarCodigoUnico(codigo, IsEditing ? CurrentProductoId : null);
                if (!string.IsNullOrEmpty(errorCodigo))
                {
                    MessageBox.Show(errorCodigo, "Error de Código", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error validando campos: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        #region Event Handlers

        private void BtnNuevo_Click(object? sender, EventArgs e)
        {
            try
            {
                LimpiarCampos();
                EstablecerEstadoControles(true);
                IsEditing = false;
                
                if (tabControl != null && tabMantenimiento != null)
                {
                    tabControl.SelectedTab = tabMantenimiento;
                }

                if (txtNombre != null)
                {
                    txtNombre.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al crear nuevo producto: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnGuardar_Click(object? sender, EventArgs e)
        {
            try
            {
                if (!ValidarCampos())
                    return;

                string codigo = txtCodigo?.Text?.Trim() ?? "";
                string nombre = txtNombre?.Text?.Trim() ?? "";
                string descripcion = txtDescripcion?.Text?.Trim() ?? "";
                decimal precio = nudPrecio?.Value ?? 0;
                int stockMinimo = (int)(nudStockMinimo?.Value ?? 0);
                int stockActual = (int)(nudStockActual?.Value ?? 0);
                bool requiereReceta = chkRequiereReceta?.Checked ?? false;
                int categoriaId = 0;

                if (cmbCategoria?.SelectedValue != null)
                {
                    int.TryParse(cmbCategoria.SelectedValue.ToString(), out categoriaId);
                }

                string resultado = "";

                if (IsEditing)
                {
                    resultado = NProductos.Editar(CurrentProductoId, codigo, nombre, precio, categoriaId,
                        descripcion, stockMinimo, stockActual, requiereReceta);
                }
                else
                {
                    resultado = NProductos.Insertar(codigo, nombre, precio, categoriaId,
                        descripcion, stockMinimo, stockActual, requiereReceta);
                }

                if (resultado == "OK" || resultado.Contains("exitosamente"))
                {
                    string accion = IsEditing ? "actualizado" : "registrado";
                    MessageBox.Show($"Producto {accion} correctamente", "Éxito", 
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    
                    BtnCancelar_Click(null, EventArgs.Empty);
                    CargarDatos();
                    
                    if (tabControl != null && tabListado != null)
                    {
                        tabControl.SelectedTab = tabListado;
                    }
                }
                else
                {
                    MessageBox.Show($"Error: {resultado}", "Error", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error guardando producto: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnEditar_Click(object? sender, EventArgs e)
        {
            try
            {
                if (dgvProductos?.SelectedRows.Count > 0)
                {
                    CargarDatosEnFormulario(dgvProductos.SelectedRows[0]);
                    EstablecerEstadoControles(true);
                    IsEditing = true;
                    
                    if (tabControl != null && tabMantenimiento != null)
                    {
                        tabControl.SelectedTab = tabMantenimiento;
                    }
                }
                else
                {
                    MessageBox.Show("Seleccione un producto para editar", "Información", 
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al editar producto: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnEliminar_Click(object? sender, EventArgs e)
        {
            try
            {
                if (dgvProductos?.SelectedRows.Count > 0)
                {
                    var result = MessageBox.Show("¿Está seguro que desea eliminar este producto?", 
                        "Confirmar Eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (result == DialogResult.Yes)
                    {
                        int id = Convert.ToInt32(dgvProductos.SelectedRows[0].Cells["id"].Value);
                        string resultado = NProductos.Eliminar(id);

                        if (resultado == "OK")
                        {
                            MessageBox.Show("Producto eliminado correctamente", "Éxito", 
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                            CargarDatos();
                        }
                        else
                        {
                            MessageBox.Show($"Error eliminando producto: {resultado}", "Error", 
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Seleccione un producto para eliminar", "Información", 
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al eliminar producto: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnCancelar_Click(object? sender, EventArgs e)
        {
            try
            {
                LimpiarCampos();
                EstablecerEstadoControles(false);
                
                if (tabControl != null && tabListado != null)
                {
                    tabControl.SelectedTab = tabListado;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cancelar: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnBuscar_Click(object? sender, EventArgs e)
        {
            try
            {
                string textoBuscar = txtBuscar?.Text?.Trim() ?? "";
                
                DataTable dt;
                if (string.IsNullOrEmpty(textoBuscar))
                {
                    dt = NProductos.Mostrar();
                }
                else
                {
                    dt = NProductos.BuscarPorNombre(textoBuscar);
                }

                if (dt != null && dgvProductos != null)
                {
                    dgvProductos.DataSource = dt;
                    ConfigurarColumnasDataGridView();
                    ActualizarTotalRegistros();
                }
                else
                {
                    MessageBox.Show("No se encontraron resultados", "Información", 
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error en la búsqueda: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnRefrescar_Click(object? sender, EventArgs e)
        {
            try
            {
                if (txtBuscar != null) txtBuscar.Text = "";
                CargarDatos();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al refrescar: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnGenerarCodigo_Click(object? sender, EventArgs e)
        {
            try
            {
                string nombre = txtNombre?.Text?.Trim() ?? "";
                string categoria = cmbCategoria?.Text?.Trim() ?? "";

                if (string.IsNullOrEmpty(nombre))
                {
                    MessageBox.Show("Ingrese primero el nombre del producto", "Información", 
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                string codigoGenerado = NProductos.GenerarCodigoAutomatico(nombre, categoria);
                if (txtCodigo != null)
                {
                    txtCodigo.Text = codigoGenerado;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error generando código: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnNuevaCategoria_Click(object? sender, EventArgs e)
        {
            try
            {
                using (var frmCategoria = new NuevaCategoriaDialog())
                {
                    if (frmCategoria.ShowDialog() == DialogResult.OK)
                    {
                        CargarCategorias();
                        
                        // Seleccionar la nueva categoría si fue creada
                        if (frmCategoria.CategoriaCreatedId > 0 && cmbCategoria != null)
                        {
                            cmbCategoria.SelectedValue = frmCategoria.CategoriaCreatedId;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error creando categoría: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnStockBajo_Click(object? sender, EventArgs e)
        {
            try
            {
                DataTable dt = NProductos.ObtenerProductosBajoStock();
                if (dt != null && dgvProductos != null)
                {
                    dgvProductos.DataSource = dt;
                    ConfigurarColumnasDataGridView();
                    ActualizarTotalRegistros();

                    if (dt.Rows.Count == 0)
                    {
                        MessageBox.Show("No hay productos con stock bajo", "Información", 
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show($"Se encontraron {dt.Rows.Count} productos con stock bajo", "Stock Bajo", 
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error obteniendo productos con stock bajo: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void TxtNombre_TextChanged(object? sender, EventArgs e)
        {
            // Actualizar sugerencia de código si está vacío
            if (string.IsNullOrEmpty(txtCodigo?.Text))
            {
                // Mostrar vista previa del código que se generaría
                // Esto es opcional, solo para UX
            }
        }

        private void CmbCategoria_SelectedIndexChanged(object? sender, EventArgs e)
        {
            try
            {
                if (cmbCategoria?.SelectedValue != null)
                {
                    int.TryParse(cmbCategoria.SelectedValue.ToString(), out CurrentCategoriaId);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error cambiando categoría: {ex.Message}");
            }
        }

        private void DgvProductos_CellClick(object? sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0 && dgvProductos?.Rows.Count > e.RowIndex)
                {
                    EstablecerEstadoControles(false);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error en CellClick: {ex.Message}");
            }
        }

        private void DgvProductos_CellDoubleClick(object? sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    BtnEditar_Click(null, EventArgs.Empty);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error en CellDoubleClick: {ex.Message}");
            }
        }

        private void DgvProductos_CellFormatting(object? sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                if (dgvProductos?.Columns[e.ColumnIndex].Name == "estado_stock" && e.Value != null)
                {
                    string estado = e.Value.ToString() ?? "";
                    switch (estado.ToLower())
                    {
                        case "sin stock":
                            e.CellStyle.BackColor = Color.LightCoral;
                            e.CellStyle.ForeColor = Color.DarkRed;
                            break;
                        case "stock bajo":
                            e.CellStyle.BackColor = Color.LightYellow;
                            e.CellStyle.ForeColor = Color.DarkOrange;
                            break;
                        case "stock ok":
                            e.CellStyle.BackColor = Color.LightGreen;
                            e.CellStyle.ForeColor = Color.DarkGreen;
                            break;
                    }
                }
                
                if (dgvProductos?.Columns[e.ColumnIndex].Name == "requiere_receta" && e.Value != null)
                {
                    bool requiere = Convert.ToBoolean(e.Value);
                    e.Value = requiere ? "Sí" : "No";
                    e.FormattingApplied = true;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error en CellFormatting: {ex.Message}");
            }
        }

        #endregion

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            try
            {
                if (IsEditing)
                {
                    var result = MessageBox.Show("¿Desea guardar los cambios antes de cerrar?", 
                        "Cambios Pendientes", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

                    if (result == DialogResult.Cancel)
                    {
                        e.Cancel = true;
                        return;
                    }
                    else if (result == DialogResult.Yes)
                    {
                        BtnGuardar_Click(null, EventArgs.Empty);
                        if (IsEditing)
                        {
                            e.Cancel = true;
                            return;
                        }
                    }
                }

                base.OnFormClosing(e);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error en OnFormClosing: {ex.Message}");
                base.OnFormClosing(e);
            }
        }
    }

    // Clase auxiliar para el diálogo de nueva categoría
    public class NuevaCategoriaDialog : Form
    {
        public int CategoriaCreatedId { get; private set; }

        private TextBox txtNombre;
        private TextBox txtDescripcion;
        private Button btnGuardar;
        private Button btnCancelar;

        public NuevaCategoriaDialog()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Text = "Nueva Categoría";
            this.Size = new Size(400, 200);
            this.StartPosition = FormStartPosition.CenterParent;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            Label lblNombre = new Label();
            lblNombre.Text = "Nombre *:";
            lblNombre.Location = new Point(20, 20);
            lblNombre.Size = new Size(60, 20);

            txtNombre = new TextBox();
            txtNombre.Location = new Point(90, 20);
            txtNombre.Size = new Size(280, 23);

            Label lblDescripcion = new Label();
            lblDescripcion.Text = "Descripción:";
            lblDescripcion.Location = new Point(20, 55);
            lblDescripcion.Size = new Size(80, 20);

            txtDescripcion = new TextBox();
            txtDescripcion.Location = new Point(90, 55);
            txtDescripcion.Size = new Size(280, 60);
            txtDescripcion.Multiline = true;

            btnGuardar = new Button();
            btnGuardar.Text = "Guardar";
            btnGuardar.Location = new Point(220, 130);
            btnGuardar.Size = new Size(80, 30);
            btnGuardar.Click += BtnGuardar_Click;

            btnCancelar = new Button();
            btnCancelar.Text = "Cancelar";
            btnCancelar.Location = new Point(310, 130);
            btnCancelar.Size = new Size(80, 30);
            btnCancelar.Click += (s, e) => this.DialogResult = DialogResult.Cancel;

            this.Controls.AddRange(new Control[] {
                lblNombre, txtNombre, lblDescripcion, txtDescripcion, btnGuardar, btnCancelar
            });
        }

        private void BtnGuardar_Click(object? sender, EventArgs e)
        {
            try
            {
                string nombre = txtNombre.Text.Trim();
                if (string.IsNullOrEmpty(nombre))
                {
                    MessageBox.Show("El nombre es requerido", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string descripcion = txtDescripcion.Text.Trim();
                string resultado = NProductos.CrearCategoria(nombre, descripcion);

                if (resultado == "OK")
                {
                    MessageBox.Show("Categoría creada exitosamente", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                }
                else
                {
                    MessageBox.Show($"Error creando categoría: {resultado}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}