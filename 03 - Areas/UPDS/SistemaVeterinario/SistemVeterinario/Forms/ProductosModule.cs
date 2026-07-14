using System;
using System.Data;
using System.Drawing;
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
        private string _categoriaSeleccionada = "";
        #endregion

        #region Constructor
        public ProductosModule()
        {
            InitializeComponent();
            ConfigurarModulo();
            ConfigurarValidacionEnTiempoReal();
            ConfigurarEstilosModernos();

            // Configurar botones editables después de la inicialización
            this.Load += (s, e) =>
            {
                ConfigurarBotonesEditables();
            };

            // También configurar cuando se cambia a la pestaña de configuración
            ConfigurarBotonesEditables();
        }
        #endregion

        #region Configuración Inicial
        private void ConfigurarModulo()
        {
            // Configurar ComboBox de categoría filtro
            cmbCategoriaFiltro.Items.Clear();
            cmbCategoriaFiltro.Items.Add("Todas");
            CargarCategorias(cmbCategoriaFiltro);
            cmbCategoriaFiltro.SelectedIndex = 0;

            // Configurar ComboBox de categoría para formulario
            cmbCategoria.Items.Clear();
            CargarCategorias(cmbCategoria);
            if (cmbCategoria.Items.Count > 0)
                cmbCategoria.SelectedIndex = 0;

            // Configurar eventos
            cmbCategoriaFiltro.SelectedIndexChanged += CmbCategoriaFiltro_SelectedIndexChanged;
            btnGenerarCodigo.Click += BtnGenerarCodigo_Click;
            btnNuevaCategoria.Click += BtnNuevaCategoria_Click;
            btnStockBajo.Click += BtnStockBajo_Click;

            // Configurar valores por defecto de NumericUpDown
            nudPrecio.DecimalPlaces = 2;
            nudPrecio.Minimum = 0;
            nudPrecio.Maximum = 999999;

            nudStockMinimo.Minimum = 0;
            nudStockMinimo.Maximum = 99999;

            nudStockActual.Minimum = 0;
            nudStockActual.Maximum = 99999;

            // Configurar gestión de categorías
            ConfigurarGestionCategorias();
        }

        private void ConfigurarGestionCategorias()
        {
            // Configurar eventos para gestión de categorías
            btnNuevaConfigCat.Click += BtnNuevaConfigCat_Click;
            btnGuardarCategoria.Click += BtnGuardarCategoria_Click;
            btnEditarCategoria.Click += BtnEditarCategoria_Click;
            btnEliminarCategoria.Click += BtnEliminarCategoria_Click;
            btnCancelarCategoria.Click += BtnCancelarCategoria_Click;
            btnInicializarCategorias.Click += BtnInicializarCategorias_Click;

            // Cargar categorías en el DataGridView
            CargarDatosCategorias();
        }

        private void ConfigurarValidacionEnTiempoReal()
        {
            // Validación de código - solo alfanumérico
            txtCodigo.KeyPress += (s, e) => ValidarCodigoAlphaNumerico(s, e);
            txtCodigo.Leave += (s, e) => ValidarCampoCompleto(txtCodigo, "codigo");

            // Validación de nombre - obligatorio
            txtNombre.Leave += (s, e) => ValidarCampoObligatorio(txtNombre, lblNombre);

            // Validación de stock - alertar si stock actual < stock mínimo
            nudStockActual.ValueChanged += (s, e) => ValidarStock();
            nudStockMinimo.ValueChanged += (s, e) => ValidarStock();

            // Validación de precio - debe ser mayor a 0
            nudPrecio.ValueChanged += (s, e) => ValidarPrecio();

            // Validación para categorías
            txtNombreCategoria.Leave += (s, e) => ValidarCampoObligatorio(txtNombreCategoria, lblNombreCategoria);
        }

        private void ConfigurarEstilosModernos()
        {
            // Configurar efectos hover para botones
            ConfigurarEfectoHover(btnBuscar);
            ConfigurarEfectoHover(btnNuevo);
            ConfigurarEfectoHover(btnGenerarCodigo);
            ConfigurarEfectoHover(btnNuevaCategoria);
            ConfigurarEfectoHover(btnStockBajo);

            // Aplicar efectos de enfoque a los TextBox
            AplicarEfectosFocusTextBox(txtCodigo);
            AplicarEfectosFocusTextBox(txtNombre);
            AplicarEfectosFocusTextBox(txtDescripcion);
            AplicarEfectosFocusTextBox(txtNombreCategoria);
            AplicarEfectosFocusTextBox(txtDescripcionCategoria);
        }

        private void ConfigurarBotonesEditables()
        {
            // Asegurar que tenemos acceso a los botones a través de la base
            if (this.btnEliminar == null || this.btnCancelar == null || this.btnGuardar == null)
                return;

            // Usar un Timer para configurar después de que la UI se haya cargado completamente
            var timer = new System.Windows.Forms.Timer();
            timer.Interval = 100; // 100ms delay
            timer.Tick += (s, e) =>
            {
                timer.Stop();
                timer.Dispose();

                try
                {
                    this.btnEliminar.Visible = true; // En productos sí permitimos eliminar
                    this.btnEliminar.Enabled = ModoEdicion;
                    this.btnGuardar.Enabled = true;
                    this.btnCancelar.Enabled = true;
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"Error configurando botones: {ex.Message}");
                }
            };
            timer.Start();
        }


        #endregion

        #region Métodos Override de BaseModulos
        protected override void OnLoad()
        {
            CargarDatos();
        }

        protected override void OnBuscar()
        {
            string textoBuscar = txtBuscar.Text.Trim();
            string categoriaFiltro = cmbCategoriaFiltro.SelectedItem?.ToString() ?? "Todas";

            try
            {
                DataTable datos;

                if (!string.IsNullOrEmpty(textoBuscar))
                {
                    // Buscar por texto
                    datos = NProductos.BuscarPorNombre(textoBuscar);
                }
                else if (categoriaFiltro != "Todas")
                {
                    // Filtrar por categoría - obtener ID de categoría
                    int categoriaId = ObtenerIdCategoriaPorNombre(categoriaFiltro);
                    if (categoriaId > 0)
                        datos = NProductos.BuscarPorCategoria(categoriaId);
                    else
                        datos = NProductos.Mostrar();
                }
                else
                {
                    // Mostrar todos
                    datos = NProductos.Mostrar();
                }

                CargarDatos(datos);
                PersonalizarColumnasProductos();
                ActualizarContadorRegistros(datos.Rows.Count);
            }
            catch (Exception ex)
            {
                MostrarMensaje($"Error al buscar productos: {ex.Message}", "Error", MessageBoxIcon.Error);
            }
        }

        protected override void OnNuevo()
        {
            base.OnNuevo();
            LimpiarFormulario();
            GenerarCodigoAutomatico();
        }

        protected override void OnGuardar()
        {
            if (!ValidarCampos())
                return;

            try
            {
                string resultado;

                if (ModoEdicion)
                {
                    resultado = NProductos.Editar(
                        IdSeleccionado,
                        txtCodigo.Text.Trim(),
                        txtNombre.Text.Trim(),
                        nudPrecio.Value,
                        ObtenerIdCategoriaSeleccionada(),
                        txtDescripcion.Text.Trim(),
                        (int)nudStockMinimo.Value,
                        (int)nudStockActual.Value,
                        chkRequiereReceta.Checked
                    );
                }
                else
                {
                    resultado = NProductos.Insertar(
                        txtCodigo.Text.Trim(),
                        txtNombre.Text.Trim(),
                        nudPrecio.Value,
                        ObtenerIdCategoriaSeleccionada(),
                        txtDescripcion.Text.Trim(),
                        (int)nudStockMinimo.Value,
                        (int)nudStockActual.Value,
                        chkRequiereReceta.Checked
                    );
                }

                if (resultado == "OK" || resultado.Contains("actualizado exitosamente") || resultado.Contains("creado exitosamente"))
                {
                    MostrarMensaje("Producto guardado exitosamente", "Éxito", MessageBoxIcon.Information);
                    OnCancelar(); // Volver al inicio
                    OnBuscar(); // Refrescar la lista
                }
                else
                {
                    MostrarMensaje($"Error al guardar: {resultado}", "Error", MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MostrarMensaje($"Error al guardar producto: {ex.Message}", "Error", MessageBoxIcon.Error);
            }
        }

        protected override void EliminarRegistro(int id)
        {
            try
            {
                string resultado = NProductos.Eliminar(id);

                if (resultado == "OK")
                {
                    MostrarMensaje("Producto eliminado exitosamente", "Éxito", MessageBoxIcon.Information);
                }
                else
                {
                    MostrarMensaje($"Error al eliminar: {resultado}", "Error", MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MostrarMensaje($"Error al eliminar producto: {ex.Message}", "Error", MessageBoxIcon.Error);
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

                    txtCodigo.Text = row["codigo"].ToString();
                    txtNombre.Text = row["nombre"].ToString();
                    txtDescripcion.Text = row["descripcion"].ToString();
                    nudPrecio.Value = Convert.ToDecimal(row["precio"]);
                    nudStockMinimo.Value = Convert.ToInt32(row["stock_minimo"]);
                    nudStockActual.Value = Convert.ToInt32(row["stock_actual"]);
                    chkRequiereReceta.Checked = Convert.ToBoolean(row["requiere_receta"]);

                    // Seleccionar la categoría correspondiente
                    if (row["categoria_id"] != DBNull.Value)
                    {
                        int categoriaId = Convert.ToInt32(row["categoria_id"]);
                        for (int i = 0; i < cmbCategoria.Items.Count; i++)
                        {
                            if (((DataRowView)cmbCategoria.Items[i])["id"].ToString() == categoriaId.ToString())
                            {
                                cmbCategoria.SelectedIndex = i;
                                break;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MostrarMensaje($"Error al cargar datos del producto: {ex.Message}", "Error", MessageBoxIcon.Error);
            }
        }

        protected override void LimpiarFormulario()
        {
            // Limpiar campos del producto
            txtCodigo.Text = "";
            txtNombre.Text = "";
            txtDescripcion.Text = "";
            nudPrecio.Value = 0;
            nudStockMinimo.Value = 0;
            nudStockActual.Value = 0;
            chkRequiereReceta.Checked = false;

            if (cmbCategoria.Items.Count > 0)
                cmbCategoria.SelectedIndex = 0;
        }

        protected override void OnCambioModo(bool esEdicion)
        {
            base.OnCambioModo(esEdicion);
            // En productos permitimos eliminar
            if (btnEliminar != null)
                btnEliminar.Visible = esEdicion;
        }

        protected override void OnEliminarFila(DataGridViewRow row)
        {
            // Obtener ID de la fila seleccionada
            if (row.DataBoundItem is DataRowView dataRow)
            {
                int id = Convert.ToInt32(dataRow["id"]);
                // Para productos usamos la columna "nombre" en lugar de "nombre_completo"
                string nombre = dataRow["nombre"]?.ToString() ?? "registro";

                var resultado = MostrarConfirmacion(
                    $"¿Está seguro que desea eliminar el producto '{nombre}'?",
                    "Confirmar eliminación"
                );

                if (resultado == DialogResult.Yes)
                {
                    EliminarRegistro(id);
                    OnBuscar(); // Refrescar la lista
                }
            }
        }
        #endregion

        #region Eventos
        private void CmbCategoriaFiltro_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Filtrar automáticamente cuando cambie la categoría
            OnBuscar();
        }

        private void BtnGenerarCodigo_Click(object sender, EventArgs e)
        {
            GenerarCodigoAutomatico();
        }

        private void BtnNuevaCategoria_Click(object sender, EventArgs e)
        {
            // Cambiar a la pestaña de gestión de categorías
            if (tabControlPrincipal.TabPages.Count > 2)
            {
                tabControlPrincipal.SelectedIndex = 2; // Asumiendo que es la tercera pestaña
            }
        }

        private void BtnStockBajo_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable datos = NProductos.ObtenerProductosBajoStock();
                CargarDatos(datos);
                PersonalizarColumnasProductos();
                ActualizarContadorRegistros(datos.Rows.Count);

                MostrarMensaje($"Se encontraron {datos.Rows.Count} productos con stock bajo", "Información", MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MostrarMensaje($"Error al obtener productos con stock bajo: {ex.Message}", "Error", MessageBoxIcon.Error);
            }
        }

        // Eventos para gestión de categorías
        private void BtnNuevaConfigCat_Click(object sender, EventArgs e)
        {
            LimpiarFormularioCategoria();
            HabilitarFormularioCategoria(true);
        }

        private void BtnGuardarCategoria_Click(object sender, EventArgs e)
        {
            if (!ValidarCamposCategoria())
                return;

            try
            {
                string resultado = NProductos.CrearCategoria(
                    txtNombreCategoria.Text.Trim(),
                    txtDescripcionCategoria.Text.Trim()
                );

                if (resultado == "OK")
                {
                    MostrarMensaje("Categoría guardada exitosamente", "Éxito", MessageBoxIcon.Information);
                    CargarDatosCategorias();
                    ActualizarComboBoxCategorias();
                    LimpiarFormularioCategoria();
                    HabilitarFormularioCategoria(false);
                }
                else
                {
                    MostrarMensaje($"Error al guardar categoría: {resultado}", "Error", MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MostrarMensaje($"Error al guardar categoría: {ex.Message}", "Error", MessageBoxIcon.Error);
            }
        }

        private void BtnEditarCategoria_Click(object sender, EventArgs e)
        {
            if (dgvCategorias.SelectedRows.Count == 0)
            {
                MostrarMensaje("Seleccione una categoría para editar", "Información", MessageBoxIcon.Information);
                return;
            }

            // Cargar datos de la categoría seleccionada para edición
            DataGridViewRow row = dgvCategorias.SelectedRows[0];
            txtNombreCategoria.Text = row.Cells["nombre"].Value?.ToString() ?? "";
            txtDescripcionCategoria.Text = row.Cells["descripcion"].Value?.ToString() ?? "";

            HabilitarFormularioCategoria(true);
        }

        private void BtnEliminarCategoria_Click(object sender, EventArgs e)
        {
            if (dgvCategorias.SelectedRows.Count == 0)
            {
                MostrarMensaje("Seleccione una categoría para eliminar", "Información", MessageBoxIcon.Information);
                return;
            }

            DataGridViewRow row = dgvCategorias.SelectedRows[0];
            int idCategoria = Convert.ToInt32(row.Cells["id"].Value);
            string nombreCategoria = row.Cells["nombre"].Value?.ToString() ?? "";

            var resultado = MostrarConfirmacion(
                $"¿Está seguro que desea eliminar la categoría '{nombreCategoria}'?",
                "Confirmar eliminación"
            );

            if (resultado == DialogResult.Yes)
            {
                try
                {
                    // Por ahora mostrar mensaje que no está implementado
                    MostrarMensaje("La eliminación de categorías no está implementada por seguridad.", "Información", MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MostrarMensaje($"Error al eliminar categoría: {ex.Message}", "Error", MessageBoxIcon.Error);
                }
            }
        }

        private void BtnCancelarCategoria_Click(object sender, EventArgs e)
        {
            LimpiarFormularioCategoria();
            HabilitarFormularioCategoria(false);
        }

        private void BtnInicializarCategorias_Click(object sender, EventArgs e)
        {
            var resultado = MostrarConfirmacion(
                "¿Desea inicializar las categorías predeterminadas? Esto agregará categorías básicas si no existen.",
                "Confirmar inicialización"
            );

            if (resultado == DialogResult.Yes)
            {
                try
                {
                    string resultadoInicializacion = NProductos.InicializarCategoriasVeterinarias();
                    MostrarMensaje($"Resultado: {resultadoInicializacion}", "Información", MessageBoxIcon.Information);
                    CargarDatosCategorias();
                    ActualizarComboBoxCategorias();
                }
                catch (Exception ex)
                {
                    MostrarMensaje($"Error al inicializar categorías: {ex.Message}", "Error", MessageBoxIcon.Error);
                }
            }
        }
        #endregion

        #region Métodos Auxiliares
        private void CargarDatos()
        {
            try
            {
                DataTable datos = NProductos.Mostrar();
                CargarDatos(datos);
                PersonalizarColumnasProductos();
                ActualizarContadorRegistros(datos.Rows.Count);
            }
            catch (Exception ex)
            {
                MostrarMensaje($"Error al cargar productos: {ex.Message}", "Error", MessageBoxIcon.Error);
            }
        }

        private void PersonalizarColumnasProductos()
        {
            if (dgvDatos?.DataSource == null) return;

            try
            {
                // Solo configurar headers y ocultar ID - sin modificar anchos ni estilos
                if (dgvDatos.Columns["id"] != null)
                    dgvDatos.Columns["id"].Visible = false;

                // Personalizar headers solamente
                if (dgvDatos.Columns["codigo"] != null)
                    dgvDatos.Columns["codigo"].HeaderText = "Código";

                if (dgvDatos.Columns["nombre"] != null)
                    dgvDatos.Columns["nombre"].HeaderText = "Nombre";
                    dgvDatos.Columns["nombre"].Width = 420;

                if (dgvDatos.Columns["categoria"] != null)
                    dgvDatos.Columns["categoria"].HeaderText = "Categoría";

                if (dgvDatos.Columns["precio"] != null)
                {
                    dgvDatos.Columns["precio"].HeaderText = "Precio";
                    dgvDatos.Columns["precio"].DefaultCellStyle.Format = "C2";
                }

                if (dgvDatos.Columns["stock_actual"] != null)
                    dgvDatos.Columns["stock_actual"].HeaderText = "Stock Actual";

                if (dgvDatos.Columns["stock_minimo"] != null)
                    dgvDatos.Columns["stock_minimo"].HeaderText = "Stock Mínimo";

                if (dgvDatos.Columns["requiere_receta"] != null)
                    dgvDatos.Columns["requiere_receta"].HeaderText = "Requiere Receta";

                // Removed width configurations and stock highlighting to prevent layout issues
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error personalizando columnas: {ex.Message}");
            }
        }

        private void CargarCategorias(ComboBox combo)
        {
            try
            {
                DataTable categorias = NProductos.ObtenerCategorias();
                combo.DataSource = categorias;
                combo.DisplayMember = "nombre";
                combo.ValueMember = "id";
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error cargando categorías: {ex.Message}");
            }
        }

        private void ActualizarComboBoxCategorias()
        {
            // Actualizar ambos ComboBox de categorías
            CargarCategorias(cmbCategoria);

            // Para el filtro, agregar "Todas" al inicio
            cmbCategoriaFiltro.Items.Clear();
            cmbCategoriaFiltro.Items.Add("Todas");
            CargarCategorias(cmbCategoriaFiltro);
            cmbCategoriaFiltro.SelectedIndex = 0;
        }

        private int ObtenerIdCategoriaSeleccionada()
        {
            if (cmbCategoria.SelectedValue != null)
                return Convert.ToInt32(cmbCategoria.SelectedValue);
            return 1; // ID por defecto
        }

        private int ObtenerIdCategoriaPorNombre(string nombreCategoria)
        {
            try
            {
                DataTable categorias = NProductos.ObtenerCategorias();
                foreach (DataRow row in categorias.Rows)
                {
                    if (row["nombre"].ToString() == nombreCategoria)
                    {
                        return Convert.ToInt32(row["id"]);
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error obteniendo ID de categoría: {ex.Message}");
            }
            return 1; // ID por defecto si no se encuentra
        }

        private void GenerarCodigoAutomatico()
        {
            try
            {
                string nuevoCodigo = NProductos.GenerarCodigoAutomatico("PRODUCTO", "GEN");
                txtCodigo.Text = nuevoCodigo;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error generando código: {ex.Message}");
                // Generar código básico como fallback
                txtCodigo.Text = "PROD" + DateTime.Now.ToString("yyyyMMddHHmmss");
            }
        }

        private void ActualizarContadorRegistros(int cantidad)
        {
            if (lblContador != null)
            {
                lblContador.Text = $"Total de registros: {cantidad}";
            }
        }

        // Métodos para gestión de categorías
        private void CargarDatosCategorias()
        {
            try
            {
                DataTable categorias = NProductos.ObtenerCategorias();
                dgvCategorias.DataSource = categorias;

                if (dgvCategorias.Columns["id"] != null)
                    dgvCategorias.Columns["id"].Visible = false;

                if (dgvCategorias.Columns["nombre"] != null)
                    dgvCategorias.Columns["nombre"].HeaderText = "Nombre";

                if (dgvCategorias.Columns["descripcion"] != null)
                    dgvCategorias.Columns["descripcion"].HeaderText = "Descripción";
            }
            catch (Exception ex)
            {
                MostrarMensaje($"Error al cargar categorías: {ex.Message}", "Error", MessageBoxIcon.Error);
            }
        }

        private void LimpiarFormularioCategoria()
        {
            txtNombreCategoria.Text = "";
            txtDescripcionCategoria.Text = "";
        }

        private void HabilitarFormularioCategoria(bool habilitar)
        {
            grpFormCategoria.Enabled = habilitar;
            btnGuardarCategoria.Enabled = habilitar;
            btnCancelarCategoria.Enabled = habilitar;
            btnNuevaConfigCat.Enabled = !habilitar;
        }

        private bool ValidarCamposCategoria()
        {
            if (string.IsNullOrWhiteSpace(txtNombreCategoria.Text))
            {
                MostrarMensaje("El nombre de la categoría es obligatorio", "Validación", MessageBoxIcon.Warning);
                txtNombreCategoria.Focus();
                return false;
            }

            return true;
        }
        #endregion

        #region Métodos de Validación y Efectos Visuales
        private bool ValidarCampos()
        {
            if (string.IsNullOrWhiteSpace(txtCodigo.Text))
            {
                MostrarMensaje("El código del producto es obligatorio", "Validación", MessageBoxIcon.Warning);
                txtCodigo.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                MostrarMensaje("El nombre del producto es obligatorio", "Validación", MessageBoxIcon.Warning);
                txtNombre.Focus();
                return false;
            }

            if (nudPrecio.Value <= 0)
            {
                MostrarMensaje("El precio debe ser mayor a 0", "Validación", MessageBoxIcon.Warning);
                nudPrecio.Focus();
                return false;
            }

            return true;
        }

        private void ValidarCodigoAlphaNumerico(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsLetterOrDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void ValidarCampoCompleto(TextBox textBox, string tipoCampo)
        {
            switch (tipoCampo.ToLower())
            {
                case "codigo":
                    if (!string.IsNullOrWhiteSpace(textBox.Text))
                    {
                        textBox.BackColor = Color.White;
                    }
                    else
                    {
                        textBox.BackColor = Color.FromArgb(255, 235, 235);
                    }
                    break;
            }
        }

        private void ValidarCampoObligatorio(TextBox textBox, Label label)
        {
            if (string.IsNullOrWhiteSpace(textBox.Text))
            {
                textBox.BackColor = Color.FromArgb(255, 235, 235);
                label.ForeColor = Color.FromArgb(231, 76, 60);
            }
            else
            {
                textBox.BackColor = Color.White;
                label.ForeColor = Color.FromArgb(52, 73, 94);
            }
        }

        private void ValidarStock()
        {
            if (nudStockActual.Value <= nudStockMinimo.Value)
            {
                nudStockActual.BackColor = Color.FromArgb(255, 235, 235);
                nudStockMinimo.BackColor = Color.FromArgb(255, 235, 235);
            }
            else
            {
                nudStockActual.BackColor = Color.White;
                nudStockMinimo.BackColor = Color.White;
            }
        }

        private void ValidarPrecio()
        {
            if (nudPrecio.Value <= 0)
            {
                nudPrecio.BackColor = Color.FromArgb(255, 235, 235);
            }
            else
            {
                nudPrecio.BackColor = Color.White;
            }
        }

        private void ConfigurarEfectoHover(Button boton)
        {
            Color colorOriginal = boton.BackColor;
            Color colorHover = Color.FromArgb(Math.Max(0, colorOriginal.R - 30),
                                           Math.Max(0, colorOriginal.G - 30),
                                           Math.Max(0, colorOriginal.B - 30));

            boton.MouseEnter += (s, e) => boton.BackColor = colorHover;
            boton.MouseLeave += (s, e) => boton.BackColor = colorOriginal;
        }

        private void AplicarEfectosFocusTextBox(TextBox textBox)
        {
            Color colorBordeOriginal = Color.FromArgb(204, 204, 204);
            Color colorBordeFocus = Color.FromArgb(0, 120, 215);

            textBox.Enter += (s, e) =>
            {
                textBox.BorderStyle = BorderStyle.FixedSingle;
                // Simular cambio de color de borde con efecto visual
            };

            textBox.Leave += (s, e) =>
            {
                textBox.BorderStyle = BorderStyle.Fixed3D;
            };
        }
        #endregion
    }
}
