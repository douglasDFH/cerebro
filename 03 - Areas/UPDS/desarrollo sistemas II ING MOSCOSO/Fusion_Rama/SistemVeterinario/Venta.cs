using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using CapaNegocio;

namespace SistemVeterinario
{
    /// <summary>
    /// Formulario para gestión de ventas/facturas
    /// Incluye listado y mantenimiento (CRUD)
    /// </summary>
    public partial class Venta : Form
    {
        private bool IsEditing = false;
        private int CurrentFacturaId = 0;

        public Venta()
        {
            InitializeComponent();
            ValidarControlesInicializados();
            ConfigurarFormulario();
            
            // Solo cargar datos si el DataGridView está correctamente inicializado
            if (dgvVentas != null)
            {
                CargarDatos();
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("No se cargan datos: dgvVentas es null");
            }
        }

        private void ValidarControlesInicializados()
        {
            var controlesRequeridos = new Dictionary<string, Control?>
            {
                { "tabControl", tabControl },
                { "dgvVentas", dgvVentas },
                { "btnNuevo", btnNuevo },
                { "btnGuardar", btnGuardar },
                { "btnEditar", btnEditar },
                { "btnEliminar", btnEliminar },
                { "btnCancelar", btnCancelar },
                { "cmbEstado", cmbEstado }
            };

            var controlesNulos = controlesRequeridos.Where(c => c.Value == null).Select(c => c.Key).ToList();
            
            if (controlesNulos.Any())
            {
                string mensaje = $"Error de Inicialización:\n\nLos siguientes controles no se inicializaron:\n• {string.Join("\n• ", controlesNulos)}\n\nEl formulario puede no funcionar correctamente.";
                MessageBox.Show(mensaje, "Error de Inicialización", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                System.Diagnostics.Debug.WriteLine($"Controles no inicializados: {string.Join(", ", controlesNulos)}");
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("Todos los controles críticos se inicializaron correctamente");
            }
        }

        private void ConfigurarFormulario()
        {
            try
            {
                // Configurar eventos - con validación null
                if (btnNuevo != null) btnNuevo.Click += BtnNuevo_Click;
                if (btnGuardar != null) btnGuardar.Click += BtnGuardar_Click;
                if (btnEditar != null) btnEditar.Click += BtnEditar_Click;
                if (btnCancelar != null) btnCancelar.Click += BtnCancelar_Click;
                if (btnEliminar != null) btnEliminar.Click += BtnEliminar_Click;
                if (btnBuscar != null) btnBuscar.Click += BtnBuscar_Click;
                if (btnRefrescar != null) btnRefrescar.Click += BtnRefrescar_Click;

                // Eventos del DataGridView
                if (dgvVentas != null)
                {
                    dgvVentas.SelectionChanged += DgvVentas_SelectionChanged;
                    dgvVentas.CellDoubleClick += DgvVentas_CellDoubleClick;
                }

                // Configurar ComboBox de Estado
                if (cmbEstado != null)
                {
                    cmbEstado.Items.AddRange(new string[] { "Pendiente", "Pagada", "Cancelada", "Anulada" });
                    cmbEstado.SelectedIndex = 0;
                }

                // Configurar fechas
                if (dtpFechaEmision != null) dtpFechaEmision.Value = DateTime.Now;
                if (dtpFechaVencimiento != null) dtpFechaVencimiento.Value = DateTime.Now.AddDays(30);

                // Estado inicial
                LimpiarFormulario();
                ConfigurarEstadoBotones(false);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al configurar formulario: {ex.Message}\n\nDetalle: {ex.StackTrace}", 
                    "Error de Inicialización", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #region Eventos de Botones

        private void BtnNuevo_Click(object? sender, EventArgs e)
        {
            LimpiarFormulario();
            IsEditing = false;
            CurrentFacturaId = 0;
            ConfigurarEstadoBotones(true);
            tabControl.SelectedTab = tabMantenimiento;
            txtNumeroFactura.Focus();
        }

        private void BtnGuardar_Click(object? sender, EventArgs e)
        {
            if (ValidarDatos())
            {
                try
                {
                    string resultado;
                    
                    if (IsEditing)
                    {
                        resultado = NVentas.Editar(
                            CurrentFacturaId,
                            Convert.ToInt32(txtPersonaId.Text),
                            txtNumeroFactura.Text,
                            chkTieneFechaVencimiento.Checked ? dtpFechaVencimiento.Value : (DateTime?)null,
                            txtNotas.Text,
                            txtProductosJson.Text,
                            txtServiciosJson.Text,
                            nudImpuestos.Value,
                            nudDescuentos.Value,
                            cmbEstado.Text,
                            chkFinalizar.Checked
                        );
                    }
                    else
                    {
                        resultado = NVentas.CrearFacturaCompleta(
                            Convert.ToInt32(txtPersonaId.Text),
                            txtNumeroFactura.Text,
                            chkTieneFechaVencimiento.Checked ? dtpFechaVencimiento.Value : (DateTime?)null,
                            txtNotas.Text,
                            txtProductosJson.Text,
                            txtServiciosJson.Text,
                            nudImpuestos.Value,
                            nudDescuentos.Value,
                            chkFinalizar.Checked
                        );
                    }

                    if (resultado == "OK")
                    {
                        MessageBox.Show(IsEditing ? "Factura actualizada exitosamente" : "Factura creada exitosamente", 
                            "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        CargarDatos();
                        LimpiarFormulario();
                        ConfigurarEstadoBotones(false);
                        tabControl.SelectedTab = tabListado;
                    }
                    else
                    {
                        MessageBox.Show($"Error: {resultado}", "Error", 
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error inesperado: {ex.Message}", "Error", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void BtnEditar_Click(object? sender, EventArgs e)
        {
            if (dgvVentas.CurrentRow != null)
            {
                CargarDatosParaEdicion();
                IsEditing = true;
                ConfigurarEstadoBotones(true);
                tabControl.SelectedTab = tabMantenimiento;
                txtNumeroFactura.Focus();
            }
            else
            {
                MessageBox.Show("Seleccione una factura para editar", "Información", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void BtnCancelar_Click(object? sender, EventArgs e)
        {
            if (MessageBox.Show("¿Está seguro de cancelar? Se perderán los cambios no guardados.", 
                "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                LimpiarFormulario();
                ConfigurarEstadoBotones(false);
                IsEditing = false;
                CurrentFacturaId = 0;
            }
        }

        private void BtnEliminar_Click(object? sender, EventArgs e)
        {
            if (dgvVentas.CurrentRow != null)
            {
                if (MessageBox.Show("¿Está seguro de eliminar esta factura?", "Confirmar Eliminación", 
                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    try
                    {
                        // Aquí implementarías la lógica de eliminación
                        // Por ahora solo mostramos un mensaje
                        MessageBox.Show("Funcionalidad de eliminación - Próximamente", "Información", 
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error al eliminar: {ex.Message}", "Error", 
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Seleccione una factura para eliminar", "Información", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void BtnBuscar_Click(object? sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtBuscarPersonaId.Text))
            {
                try
                {
                    int personaId = Convert.ToInt32(txtBuscarPersonaId.Text);
                    DataTable dt = NVentas.BuscarPorPersona(personaId);
                    
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        dgvVentas.DataSource = dt;
                        ConfigurarColumnasGrid();
                        lblTotalRegistros.Text = $"Total registros: {dt.Rows.Count}";
                    }
                    else
                    {
                        MessageBox.Show("No se encontraron facturas para esta persona", "Información", 
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        dgvVentas.DataSource = null;
                        lblTotalRegistros.Text = "Total registros: 0";
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error en la búsqueda: {ex.Message}", "Error", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                CargarDatos();
            }
        }

        private void BtnRefrescar_Click(object? sender, EventArgs e)
        {
            CargarDatos();
            txtBuscarPersonaId.Clear();
        }

        #endregion

        #region Eventos del DataGridView

        private void DgvVentas_SelectionChanged(object? sender, EventArgs e)
        {
            bool haySeleccion = dgvVentas?.CurrentRow != null;
            if (btnEditar != null) btnEditar.Enabled = haySeleccion;
            if (btnEliminar != null) btnEliminar.Enabled = haySeleccion;
        }

        private void DgvVentas_CellDoubleClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                BtnEditar_Click(sender, e);
            }
        }

        #endregion

        #region Métodos Auxiliares

        private void CargarDatos()
        {
            try
            {
                if (dgvVentas == null)
                {
                    System.Diagnostics.Debug.WriteLine("dgvVentas es null en CargarDatos");
                    return;
                }

                DataTable dt = NVentas.Mostrar();
                
                if (dt != null && dt.Rows.Count >= 0)
                {
                    dgvVentas.DataSource = dt;
                    
                    // Solo configurar columnas si hay datos o al menos estructura
                    // y solo si no falla la configuración
                    if (dt.Columns.Count > 0)
                    {
                        try
                        {
                            ConfigurarColumnasGrid();
                        }
                        catch (Exception ex)
                        {
                            System.Diagnostics.Debug.WriteLine($"Falló ConfigurarColumnasGrid, continuando sin configuración personalizada: {ex.Message}");
                            // Si falla la configuración de columnas, simplemente continuar sin ella
                            // El DataGridView funcionará con configuración por defecto
                        }
                    }
                    
                    if (lblTotalRegistros != null) 
                        lblTotalRegistros.Text = $"Total registros: {dt.Rows.Count}";
                }
                else
                {
                    dgvVentas.DataSource = null;
                    if (lblTotalRegistros != null) 
                        lblTotalRegistros.Text = "Total registros: 0";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar datos: {ex.Message}\n\nDetalle: {ex.StackTrace}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                
                if (dgvVentas != null) 
                {
                    try
                    {
                        dgvVentas.DataSource = null;
                    }
                    catch
                    {
                        // Si falla hasta limpiar el DataSource, solo loggear
                        System.Diagnostics.Debug.WriteLine("Error limpiando DataSource");
                    }
                }
                
                if (lblTotalRegistros != null) 
                    lblTotalRegistros.Text = "Total registros: 0 (Error)";
            }
        }

        private void ConfigurarColumnasGrid()
        {
            try
            {
                if (dgvVentas?.DataSource == null || dgvVentas.Columns.Count == 0)
                    return;

                // SOLO configurar encabezados de texto - NADA más
                foreach (DataGridViewColumn column in dgvVentas.Columns)
                {
                    if (column == null || string.IsNullOrEmpty(column.Name)) 
                        continue;

                    try
                    {
                        // Solo cambiar el texto del header - operación más básica posible
                        switch (column.Name.ToLower())
                        {
                            case "id":
                                column.HeaderText = "ID";
                                break;
                            case "numero_factura":
                                column.HeaderText = "Nº Factura";
                                break;
                            case "fecha_emision":
                                column.HeaderText = "Fecha Emisión";
                                break;
                            case "fecha_vencimiento":
                                column.HeaderText = "Fecha Vencimiento";
                                break;
                            case "cliente":
                                column.HeaderText = "Cliente";
                                break;
                            case "subtotal":
                                column.HeaderText = "Subtotal";
                                break;
                            case "impuestos":
                                column.HeaderText = "Impuestos";
                                break;
                            case "descuentos":
                                column.HeaderText = "Descuentos";
                                break;
                            case "total":
                                column.HeaderText = "Total";
                                break;
                            case "estado":
                                column.HeaderText = "Estado";
                                break;
                        }
                    }
                    catch (Exception ex)
                    {
                        // Si falla hasta cambiar el header, solo loggear y continuar
                        System.Diagnostics.Debug.WriteLine($"Error configurando header de columna {column.Name}: {ex.Message}");
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error en ConfigurarColumnasGrid: {ex.Message}");
            }
        }

        private void CargarDatosParaEdicion()
        {
            if (dgvVentas.CurrentRow != null)
            {
                CurrentFacturaId = Convert.ToInt32(dgvVentas.CurrentRow.Cells["id"].Value);
                
                // Cargar datos básicos desde el grid
                txtNumeroFactura.Text = dgvVentas.CurrentRow.Cells["numero_factura"].Value?.ToString() ?? "";
                dtpFechaEmision.Value = Convert.ToDateTime(dgvVentas.CurrentRow.Cells["fecha_emision"].Value);
                
                if (dgvVentas.CurrentRow.Cells["fecha_vencimiento"].Value != DBNull.Value)
                {
                    chkTieneFechaVencimiento.Checked = true;
                    dtpFechaVencimiento.Value = Convert.ToDateTime(dgvVentas.CurrentRow.Cells["fecha_vencimiento"].Value);
                }
                else
                {
                    chkTieneFechaVencimiento.Checked = false;
                }

                nudImpuestos.Value = Convert.ToDecimal(dgvVentas.CurrentRow.Cells["impuestos"].Value ?? 0);
                nudDescuentos.Value = Convert.ToDecimal(dgvVentas.CurrentRow.Cells["descuentos"].Value ?? 0);
                cmbEstado.Text = dgvVentas.CurrentRow.Cells["estado"].Value?.ToString() ?? "Pendiente";
                txtNotas.Text = dgvVentas.CurrentRow.Cells["notas"].Value?.ToString() ?? "";

                // Para obtener más detalles, deberías hacer una consulta adicional
                // Por ahora asignamos valores por defecto
                txtPersonaId.Text = "1"; // Esto debería venir de la consulta detallada
                txtProductosJson.Text = "";
                txtServiciosJson.Text = "";
            }
        }

        private void LimpiarFormulario()
        {
            try
            {
                txtNumeroFactura?.Clear();
                txtPersonaId?.Clear();
                if (dtpFechaEmision != null) dtpFechaEmision.Value = DateTime.Now;
                if (dtpFechaVencimiento != null) dtpFechaVencimiento.Value = DateTime.Now.AddDays(30);
                if (chkTieneFechaVencimiento != null) chkTieneFechaVencimiento.Checked = true;
                if (nudImpuestos != null) nudImpuestos.Value = 0;
                if (nudDescuentos != null) nudDescuentos.Value = 0;
                if (cmbEstado != null) cmbEstado.SelectedIndex = 0;
                txtNotas?.Clear();
                txtProductosJson?.Clear();
                txtServiciosJson?.Clear();
                if (chkFinalizar != null) chkFinalizar.Checked = false;
            }
            catch (Exception ex)
            {
                // Log error but don't show to user - this is cleanup
                System.Diagnostics.Debug.WriteLine($"Error in LimpiarFormulario: {ex.Message}");
            }
        }

        private void ConfigurarEstadoBotones(bool editando)
        {
            try
            {
                if (btnNuevo != null) btnNuevo.Enabled = !editando;
                if (btnGuardar != null) btnGuardar.Enabled = editando;
                if (btnCancelar != null) btnCancelar.Enabled = editando;
                if (btnEditar != null) btnEditar.Enabled = !editando && dgvVentas?.CurrentRow != null;
                if (btnEliminar != null) btnEliminar.Enabled = !editando && dgvVentas?.CurrentRow != null;

                // Habilitar/deshabilitar controles de edición
                if (txtNumeroFactura != null) txtNumeroFactura.Enabled = editando;
                if (txtPersonaId != null) txtPersonaId.Enabled = editando;
                if (dtpFechaEmision != null) dtpFechaEmision.Enabled = editando;
                if (dtpFechaVencimiento != null) dtpFechaVencimiento.Enabled = editando;
                if (chkTieneFechaVencimiento != null) chkTieneFechaVencimiento.Enabled = editando;
                if (nudImpuestos != null) nudImpuestos.Enabled = editando;
                if (nudDescuentos != null) nudDescuentos.Enabled = editando;
                if (cmbEstado != null) cmbEstado.Enabled = editando;
                if (txtNotas != null) txtNotas.Enabled = editando;
                if (txtProductosJson != null) txtProductosJson.Enabled = editando;
                if (txtServiciosJson != null) txtServiciosJson.Enabled = editando;
                if (chkFinalizar != null) chkFinalizar.Enabled = editando;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error in ConfigurarEstadoBotones: {ex.Message}");
            }
        }

        private bool ValidarDatos()
        {
            if (string.IsNullOrWhiteSpace(txtNumeroFactura.Text))
            {
                MessageBox.Show("El número de factura es requerido", "Validación", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNumeroFactura.Focus();
                return false;
            }

            if (!NVentas.ValidarFactura(txtNumeroFactura.Text))
            {
                MessageBox.Show("El número de factura no tiene un formato válido", "Validación", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNumeroFactura.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtPersonaId.Text))
            {
                MessageBox.Show("El ID de la persona es requerido", "Validación", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPersonaId.Focus();
                return false;
            }

            if (!int.TryParse(txtPersonaId.Text, out _))
            {
                MessageBox.Show("El ID de la persona debe ser un número válido", "Validación", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPersonaId.Focus();
                return false;
            }

            return true;
        }

        #endregion

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (btnGuardar.Enabled)
            {
                var result = MessageBox.Show("Hay cambios sin guardar. ¿Está seguro de cerrar?", 
                    "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                
                if (result == DialogResult.No)
                {
                    e.Cancel = true;
                    return;
                }
            }

            base.OnFormClosing(e);
        }
    }
}