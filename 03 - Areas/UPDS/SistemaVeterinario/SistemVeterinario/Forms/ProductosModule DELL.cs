using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using CapaNegocio;
using SistemVeterinario.Navigation;

namespace SistemVeterinario.Forms
{
    /// <summary>
    /// Módulo para gestión de Productos
    /// Hereda de BaseModulos para funcionalidad estándar de CRUD
    /// </summary>
    public partial class ProductosModule : BaseModulos
    {
        #region Variables Privadas
        private int _currentProductoId = 0;
        private int _currentCategoriaId = 0;
        #endregion

        #region Constructor
        public ProductosModule()
        {
            InitializeComponent();
            ConfigurarModulo();
        }
        #endregion

        #region Configuración Inicial
        private void ConfigurarModulo()
        {
            try
            {
                // Configurar controles solo después de que estén inicializados
                this.Load += (s, e) => {
                    ConfigurarControlesEspecificos();
                    ConfigurarEventosEspecificos();
                    ConfigurarControlesIniciales();
                };
            }
            catch (Exception ex)
            {
                MostrarMensaje($"Error configurando módulo: {ex.Message}", "Error", MessageBoxIcon.Error);
            }
        }

        private void ConfigurarControlesEspecificos()
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
                MostrarMensaje($"Error configurando controles: {ex.Message}", "Error", MessageBoxIcon.Error);
            }
        }

        private void ConfigurarEventosEspecificos()
        {
            try
            {
                if (btnGenerarCodigo != null) btnGenerarCodigo.Click += BtnGenerarCodigo_Click;
                if (btnNuevaCategoria != null) btnNuevaCategoria.Click += BtnNuevaCategoria_Click;
                if (btnStockBajo != null) btnStockBajo.Click += BtnStockBajo_Click;
                if (txtNombre != null) txtNombre.TextChanged += TxtNombre_TextChanged;
                if (cmbCategoria != null) cmbCategoria.SelectedIndexChanged += CmbCategoria_SelectedIndexChanged;

                // Configurar eventos del DataGridView
                if (dgvDatos != null)
                {
                    dgvDatos.CellFormatting += DgvDatos_CellFormatting;
                    dgvDatos.DataSourceChanged += DgvDatos_DataSourceChanged;
                    dgvDatos.DataError += DgvDatos_DataError;
                }
            }
            catch (Exception ex)
            {
                MostrarMensaje($"Error configurando eventos: {ex.Message}", "Error", MessageBoxIcon.Error);
            }
        }

        private void ConfigurarControlesIniciales()
        {
            LimpiarFormulario();
        }
        #endregion

        #region Métodos Override de BaseModulos
        protected override void OnLoad()
        {
            try
            {
                CargarDatos();
            }
            catch (Exception ex)
            {
                MostrarMensaje($"Error en OnLoad: {ex.Message}", "Error", MessageBoxIcon.Error);
            }
        }

        protected override void OnBuscar()
        {
            string textoBuscar = txtBuscar?.Text?.Trim() ?? "";

            try
            {
                DataTable datos;

                if (!string.IsNullOrEmpty(textoBuscar))
                {
                    datos = NProductos.BuscarPorNombre(textoBuscar);
                }
                else
                {
                    datos = NProductos.Mostrar();
                }

                if (datos != null)
                {
                    base.CargarDatos(datos);
                    ActualizarContadorRegistros(datos.Rows.Count);
                    ConfigurarColumnasDataGridView();
                }
                else
                {
                    ActualizarContadorRegistros(0);
                }
            }
            catch (Exception ex)
            {
                MostrarMensaje($"Error al buscar: {ex.Message}", "Error", MessageBoxIcon.Error);
                ActualizarContadorRegistros(0);
            }
        }

        protected override void OnNuevo()
        {
            base.OnNuevo();
            LimpiarFormulario();
            if (cmbCategoria != null && cmbCategoria.Items.Count > 0)
            {
                cmbCategoria.SelectedIndex = 0;
            }
        }

        protected override void OnGuardar()
        {
            if (!ValidarCampos())
                return;

            try
            {
                string resultado = "";
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

                if (ModoEdicion)
                {
                    resultado = NProductos.Editar(_currentProductoId, codigo, nombre, precio, categoriaId,
                        descripcion, stockMinimo, stockActual, requiereReceta);
                }
                else
                {
                    resultado = NProductos.Insertar(codigo, nombre, precio, categoriaId,
                        descripcion, stockMinimo, stockActual, requiereReceta);
                }

                if (resultado == "OK" || resultado.Contains("exitosamente"))
                {
                    MostrarMensaje(ModoEdicion ? "Producto actualizado correctamente" : "Producto registrado correctamente");
                    // Cambiar a la pestaña de inicio y recargar datos
                    tabControlPrincipal.SelectedTab = tabInicio;
                    LimpiarFormulario();
                    // Recargar los datos para mostrar el registro recién guardado
                    CargarDatos();
                }
                else
                {
                    MostrarMensaje($"Error: {resultado}", "Error", MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MostrarMensaje($"Error al guardar: {ex.Message}", "Error", MessageBoxIcon.Error);
            }
        }

        protected override void EliminarRegistro(int id)
        {
            try
            {
                string resultado = NProductos.Eliminar(id);

                if (resultado == "OK")
                {
                    MostrarMensaje("Producto eliminado correctamente");
                }
                else
                {
                    MostrarMensaje($"Error al eliminar: {resultado}", "Error", MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MostrarMensaje($"Error al eliminar: {ex.Message}", "Error", MessageBoxIcon.Error);
            }
        }

        protected override void CargarDatosParaEdicion(int id)
        {
            try
            {
                DataTable datos = NProductos.ObtenerPorId(id);
                if (datos.Rows.Count > 0)
                {
                    DataRow row = datos.Rows[0];

                    _currentProductoId = id;

                    // Cargar datos básicos
                    if (txtCodigo != null) txtCodigo.Text = row["codigo"]?.ToString() ?? "";
                    if (txtNombre != null) txtNombre.Text = row["nombre"]?.ToString() ?? "";
                    if (txtDescripcion != null) txtDescripcion.Text = row["descripcion"]?.ToString() ?? "";

                    // Configurar precio
                    if (row["precio"] != DBNull.Value && row["precio"] != null && nudPrecio != null)
                    {
                        nudPrecio.Value = Convert.ToDecimal(row["precio"]);
                    }

                    // Configurar stock
                    if (row["stock_minimo"] != DBNull.Value && nudStockMinimo != null)
                    {
                        nudStockMinimo.Value = Convert.ToDecimal(row["stock_minimo"]);
                    }

                    if (row["stock_actual"] != DBNull.Value && nudStockActual != null)
                    {
                        nudStockActual.Value = Convert.ToDecimal(row["stock_actual"]);
                    }

                    // Configurar requiere receta
                    if (row["requiere_receta"] != DBNull.Value && chkRequiereReceta != null)
                    {
                        chkRequiereReceta.Checked = Convert.ToBoolean(row["requiere_receta"]);
                    }

                    // Seleccionar categoría
                    if (cmbCategoria != null)
                    {
                        var categoriaId = row["categoria_id"];
                        if (categoriaId != null && categoriaId != DBNull.Value)
                        {
                            _currentCategoriaId = Convert.ToInt32(categoriaId);
                            cmbCategoria.SelectedValue = _currentCategoriaId;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MostrarMensaje($"Error al cargar datos: {ex.Message}", "Error", MessageBoxIcon.Error);
            }
        }

        protected override void LimpiarFormulario()
        {
            // Limpiar campos de producto
            if (txtCodigo != null) txtCodigo.Text = "";
            if (txtNombre != null) txtNombre.Text = "";
            if (txtDescripcion != null) txtDescripcion.Text = "";
            if (nudPrecio != null) nudPrecio.Value = 0.01m;
            if (nudStockMinimo != null) nudStockMinimo.Value = 5;
            if (nudStockActual != null) nudStockActual.Value = 0;
            if (chkRequiereReceta != null) chkRequiereReceta.Checked = false;
            if (cmbCategoria != null && cmbCategoria.Items.Count > 0) cmbCategoria.SelectedIndex = 0;

            _currentProductoId = 0;
            _currentCategoriaId = 0;
        }
        #endregion

        #region Eventos Específicos de Productos
        private void BtnGenerarCodigo_Click(object? sender, EventArgs e)
        {
            try
            {
                string nombre = txtNombre?.Text?.Trim() ?? "";
                string categoria = cmbCategoria?.Text?.Trim() ?? "";

                if (string.IsNullOrEmpty(nombre))
                {
                    MostrarMensaje("Ingrese primero el nombre del producto", "Información", MessageBoxIcon.Information);
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
                MostrarMensaje($"Error generando código: {ex.Message}", "Error", MessageBoxIcon.Error);
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
                        // Recargar categorías sin mostrar el mensaje de inicialización
                        CargarCategoriassilencioso();
                        
                        // Seleccionar la nueva categoría si fue creada
                        if (frmCategoria.CategoriaCreatedId > 0 && cmbCategoria != null)
                        {
                            cmbCategoria.SelectedValue = frmCategoria.CategoriaCreatedId;
                        }
                        
                        MostrarMensaje("Categoría creada exitosamente", "Éxito", MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MostrarMensaje($"Error creando categoría: {ex.Message}", "Error", MessageBoxIcon.Error);
            }
        }

        private void CargarCategoriassilencioso()
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
            }
            catch (Exception ex)
            {
                MostrarMensaje($"Error cargando categorías: {ex.Message}", "Error", MessageBoxIcon.Error);
            }
        }

        private void BtnStockBajo_Click(object? sender, EventArgs e)
        {
            try
            {
                DataTable datos = NProductos.ObtenerProductosBajoStock();
                if (datos != null)
                {
                    base.CargarDatos(datos);
                    ConfigurarColumnasDataGridView();
                    ActualizarContadorRegistros(datos.Rows.Count);

                    if (datos.Rows.Count == 0)
                    {
                        MostrarMensaje("No hay productos con stock bajo", "Información", MessageBoxIcon.Information);
                    }
                    else
                    {
                        MostrarMensaje($"Se encontraron {datos.Rows.Count} productos con stock bajo", "Stock Bajo", MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                MostrarMensaje($"Error obteniendo productos con stock bajo: {ex.Message}", "Error", MessageBoxIcon.Error);
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
                    int.TryParse(cmbCategoria.SelectedValue.ToString(), out _currentCategoriaId);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error cambiando categoría: {ex.Message}");
            }
        }

        private void DgvDatos_DataSourceChanged(object? sender, EventArgs e)
        {
            ConfigurarColumnasDataGridView();
        }

        private void DgvDatos_CellFormatting(object? sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                if (dgvDatos?.Columns[e.ColumnIndex].Name == "estado_stock" && e.Value != null && e.Value != DBNull.Value)
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
                
                if (dgvDatos?.Columns[e.ColumnIndex].Name == "requiere_receta" && e.Value != null && e.Value != DBNull.Value)
                {
                    if (bool.TryParse(e.Value.ToString(), out bool requiere))
                    {
                        e.Value = requiere ? "Sí" : "No";
                        e.FormattingApplied = true;
                    }
                    else
                    {
                        // Si no se puede convertir, mostrar valor por defecto
                        e.Value = "No";
                        e.FormattingApplied = true;
                    }
                }

                if (dgvDatos?.Columns[e.ColumnIndex].Name == "precio" && e.Value != null && e.Value != DBNull.Value)
                {
                    if (decimal.TryParse(e.Value.ToString(), out decimal precio))
                    {
                        e.Value = precio.ToString("C2");
                        e.FormattingApplied = true;
                    }
                    else
                    {
                        e.Value = "$0.00";
                        e.FormattingApplied = true;
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error en CellFormatting: {ex.Message}");
                // En caso de error, no aplicar formato personalizado
                e.FormattingApplied = false;
            }
        }

        private void DgvDatos_DataError(object? sender, DataGridViewDataErrorEventArgs e)
        {
            try
            {
                string columnName = dgvDatos.Columns[e.ColumnIndex].Name;
                string errorMsg = $"Error en columna '{columnName}', fila {e.RowIndex + 1}: {e.Exception.Message}";
                
                System.Diagnostics.Debug.WriteLine(errorMsg);
                
                // Marcar como manejado para evitar el diálogo por defecto
                e.ThrowException = false;
                
                // Log adicional para diferentes tipos de errores
                if (e.Exception is FormatException)
                {
                    System.Diagnostics.Debug.WriteLine($"Error de formato en columna {columnName}: {e.Exception.Message}");
                }
                else if (e.Exception is InvalidCastException)
                {
                    System.Diagnostics.Debug.WriteLine($"Error de conversión en columna {columnName}: {e.Exception.Message}");
                }
                
                // Log del contexto del error (solo lectura)
                System.Diagnostics.Debug.WriteLine($"Contexto del error: {e.Context}");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error en manejo de DataError: {ex.Message}");
                e.ThrowException = false;
            }
        }
        #endregion

        #region Métodos Auxiliares
        private void CargarDatos()
        {
            try
            {
                DataTable datos = NProductos.Mostrar();
                if (datos != null)
                {
                    base.CargarDatos(datos);
                    ActualizarContadorRegistros(datos.Rows.Count);
                    ConfigurarColumnasDataGridView();
                }
                else
                {
                    ActualizarContadorRegistros(0);
                }
            }
            catch (Exception ex)
            {
                MostrarMensaje($"Error al cargar datos: {ex.Message}", "Error", MessageBoxIcon.Error);
                ActualizarContadorRegistros(0);
            }
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
                
                // Si no hay categorías, intentar crear las categorías iniciales automáticamente
                if (categorias == null || categorias.Rows.Count == 0)
                {
                    bool categoriasCreadas = NProductos.VerificarYCrearCategoriasIniciales();
                    
                    if (categoriasCreadas)
                    {
                        // Recargar categorías después de crearlas
                        categorias = NProductos.ObtenerCategorias();
                        
                        if (categorias != null && categorias.Rows.Count > 0)
                        {
                            cmbCategoria.DataSource = categorias;
                            cmbCategoria.SelectedIndex = 0;
                            
                            // Mensaje informativo mejorado
                            MostrarMensaje("Se han creado las categorías veterinarias básicas automáticamente.\n" +
                                         "Puede agregar más categorías usando el botón 'Nueva'.",
                                         "Categorías Inicializadas", MessageBoxIcon.Information);
                        }
                    }
                    else
                    {
                        MostrarMensaje("No se pudieron crear las categorías automáticamente.\n" +
                                     "Use el botón 'Nueva' para crear categorías manualmente.",
                                     "Error de Inicialización", MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    cmbCategoria.DataSource = categorias;
                    cmbCategoria.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MostrarMensaje($"Error cargando categorías: {ex.Message}", "Error", MessageBoxIcon.Error);
            }
        }

        private void ConfigurarColumnasDataGridView()
        {
            if (dgvDatos?.DataSource == null || dgvDatos.Columns.Count == 0) 
                return;

            try
            {
                foreach (DataGridViewColumn column in dgvDatos.Columns)
                {
                    if (column == null) continue;

                    string columnName = column.Name?.ToLower() ?? "";
                    
                    switch (columnName)
                    {
                        case "id":
                            column.HeaderText = "ID";
                            column.Width = 50;
                            column.Visible = false; // BaseModulos ya oculta esta columna
                            break;
                        case "codigo":
                            column.HeaderText = "Código";
                            column.Width = 80;
                            column.Visible = true;
                            column.DefaultCellStyle.NullValue = "-";
                            break;
                        case "nombre":
                            column.HeaderText = "Nombre";
                            column.Width = 200;
                            column.Visible = true;
                            column.DefaultCellStyle.NullValue = "";
                            break;
                        case "categoria_nombre":
                            column.HeaderText = "Categoría";
                            column.Width = 120;
                            column.Visible = true;
                            column.DefaultCellStyle.NullValue = "Sin categoría";
                            break;
                        case "precio":
                            column.HeaderText = "Precio";
                            column.Width = 80;
                            column.Visible = true;
                            // No aplicar formato automático aquí para evitar errores
                            // El formato se maneja en CellFormatting
                            column.DefaultCellStyle.NullValue = "$0.00";
                            column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                            break;
                        case "stock_actual":
                            column.HeaderText = "Stock";
                            column.Width = 60;
                            column.Visible = true;
                            column.DefaultCellStyle.NullValue = "0";
                            column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                            break;
                        case "stock_minimo":
                            column.HeaderText = "Stock Mín.";
                            column.Width = 70;
                            column.Visible = true;
                            column.DefaultCellStyle.NullValue = "0";
                            column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                            break;
                        case "estado_stock":
                            column.HeaderText = "Estado";
                            column.Width = 80;
                            column.Visible = true;
                            column.DefaultCellStyle.NullValue = "Stock OK";
                            column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                            break;
                        case "requiere_receta":
                            column.HeaderText = "Receta";
                            column.Width = 60;
                            column.Visible = true;
                            column.DefaultCellStyle.NullValue = "No";
                            column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                            break;
                        case "descripcion":
                            column.HeaderText = "Descripción";
                            column.Width = 150;
                            column.Visible = true;
                            column.DefaultCellStyle.NullValue = "";
                            break;
                        case "categoria_id":
                            column.Visible = false;
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error configurando columnas: {ex.Message}");
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
                    MostrarMensaje(errores, "Errores de Validación", MessageBoxIcon.Warning);
                    return false;
                }

                // Validar código único
                string errorCodigo = NProductos.ValidarCodigoUnico(codigo, ModoEdicion ? _currentProductoId : null);
                if (!string.IsNullOrEmpty(errorCodigo))
                {
                    MostrarMensaje(errorCodigo, "Error de Código", MessageBoxIcon.Warning);
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                MostrarMensaje($"Error validando campos: {ex.Message}", "Error", MessageBoxIcon.Error);
                return false;
            }
        }

        private void ActualizarContadorRegistros(int cantidad)
        {
            if (lblContador != null)
            {
                lblContador.Text = $"Total de registros: {cantidad}";
            }
        }
        #endregion
    }

    // Clase auxiliar para el diálogo de nueva categoría
    public class NuevaCategoriaDialog : Form
    {
        public int CategoriaCreatedId { get; private set; }

        private ComboBox cmbSugerencias;
        private TextBox txtNombre;
        private TextBox txtDescripcion;
        private Button btnGuardar;
        private Button btnCancelar;

        public NuevaCategoriaDialog()
        {
            InitializeComponent();
        }

        private void CargarSugerencias()
        {
            try
            {
                var sugerencias = NProductos.ObtenerCategoriasComunes();
                cmbSugerencias.Items.Add("-- Seleccionar sugerencia --");
                for (int i = 0; i < sugerencias.Count; i++)
                {
                    string sugerencia = sugerencias[i];
                    cmbSugerencias.Items.Add(sugerencia);
                }
            }
            catch (Exception ex)
            {
                // En caso de error, solo agregar la opción por defecto
                cmbSugerencias.Items.Add("-- Seleccionar sugerencia --");
                System.Diagnostics.Debug.WriteLine($"Error cargando sugerencias: {ex.Message}");
            }
        }

        private void InitializeComponent()
        {
            Text = "Nueva Categoría Veterinaria";
            this.Size = new Size(450, 280);
            this.StartPosition = FormStartPosition.CenterParent;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            Label lblSugerencias = new Label();
            lblSugerencias.Text = "Sugerencias:";
            lblSugerencias.Location = new Point(20, 20);
            lblSugerencias.Size = new Size(80, 20);

            cmbSugerencias = new ComboBox();
            cmbSugerencias.Location = new Point(110, 18);
            cmbSugerencias.Size = new Size(300, 23);
            cmbSugerencias.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbSugerencias.SelectedIndexChanged += CmbSugerencias_SelectedIndexChanged;
            
            // Cargar sugerencias veterinarias
            CargarSugerencias();
            cmbSugerencias.SelectedIndex = 0;

            Label lblNombre = new Label();
            lblNombre.Text = "Nombre *:";
            lblNombre.Location = new Point(20, 60);
            lblNombre.Size = new Size(60, 20);

            txtNombre = new TextBox();
            txtNombre.Location = new Point(110, 58);
            txtNombre.Size = new Size(300, 23);

            Label lblDescripcion = new Label();
            lblDescripcion.Text = "Descripción:";
            lblDescripcion.Location = new Point(20, 95);
            lblDescripcion.Size = new Size(80, 20);

            txtDescripcion = new TextBox();
            txtDescripcion.Location = new Point(20, 120);
            txtDescripcion.Size = new Size(390, 80);
            txtDescripcion.Multiline = true;
            txtDescripcion.ScrollBars = ScrollBars.Vertical;

            btnGuardar = new Button();
            btnGuardar.Text = "Guardar";
            btnGuardar.Location = new Point(250, 220);
            btnGuardar.Size = new Size(80, 30);
            btnGuardar.Click += BtnGuardar_Click;

            btnCancelar = new Button();
            btnCancelar.Text = "Cancelar";
            btnCancelar.Location = new Point(340, 220);
            btnCancelar.Size = new Size(80, 30);
            btnCancelar.Click += (s, e) => this.DialogResult = DialogResult.Cancel;

            this.Controls.AddRange(new Control[] {
                lblSugerencias, cmbSugerencias, lblNombre, txtNombre, lblDescripcion, txtDescripcion, btnGuardar, btnCancelar
            });
        }

        private void CmbSugerencias_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (cmbSugerencias.SelectedIndex > 0)
            {
                string categoriaSeleccionada = cmbSugerencias.SelectedItem?.ToString() ?? "";
                txtNombre.Text = categoriaSeleccionada;
                
                // Agregar descripción automática
                var descripciones = new Dictionary<string, string>
                {
                    {"Medicamentos", "Fármacos y medicinas para tratamiento veterinario"},
                    {"Suplementos", "Complementos nutricionales y vitamínicos"},
                    {"Alimentos", "Alimentación especializada para mascotas"},
                    {"Juguetes", "Elementos de entretenimiento para animales"},
                    {"Higiene y Cuidado", "Productos de aseo y cuidado animal"},
                    {"Accesorios", "Collares, correas, camas y elementos complementarios"},
                    {"Equipos Médicos", "Dispositivos e instrumentos veterinarios"},
                    {"Material Quirúrgico", "Instrumental y suministros médicos quirúrgicos"},
                    {"Vacunas", "Inmunizaciones preventivas para animales"},
                    {"Antiparasitarios", "Tratamientos contra parásitos internos y externos"},
                    {"Antibióticos", "Medicamentos antibacterianos para animales"},
                    {"Analgésicos", "Medicamentos para el control del dolor"},
                    {"Vitaminas", "Suplementos vitamínicos esenciales"},
                    {"Collares y Correas", "Elementos de sujeción y paseo"},
                    {"Camas y Casas", "Elementos de descanso y refugio"},
                    {"Comederos y Bebederos", "Recipientes para alimentación e hidratación"}
                };

                if (descripciones.ContainsKey(categoriaSeleccionada))
                {
                    txtDescripcion.Text = descripciones[categoriaSeleccionada];
                }
            }
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