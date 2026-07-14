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
    /// Módulo para gestión de ventas/facturas
    /// Incluye listado y mantenimiento (CRUD)
    /// Hereda de BaseModulos para funcionalidad estándar
    /// </summary>
    public partial class VentasModule : BaseModulos
    {
        private bool IsEditing = false;
        private int CurrentFacturaId = 0;

        public VentasModule()
        {
            InitializeComponent();
            ValidarControlesInicializados();
            ConfigurarModulo();
        }

        protected override void OnLoad()
        {
            CargarDatosVentas();
        }

        protected override void OnBuscar()
        {
            if (!string.IsNullOrWhiteSpace(txtBuscar?.Text))
            {
                BuscarVentas(txtBuscar.Text);
            }
            else
            {
                CargarDatosVentas();
            }
        }

        protected override void OnNuevo()
        {
            LimpiarFormulario();
            IsEditing = false;
            CurrentFacturaId = 0;
            ConfigurarEstadoBotones(true);
            if (tabControlPrincipal != null)
                tabControlPrincipal.SelectedTab = tabConfiguraciones;
            txtNumeroFactura?.Focus();
        }

        protected override void OnEditar(DataGridViewRow fila)
        {
            if (fila != null)
            {
                CargarDatosParaEdicion(fila);
                IsEditing = true;
                ConfigurarEstadoBotones(true);
                if (tabControlPrincipal != null)
                    tabControlPrincipal.SelectedTab = tabConfiguraciones;
                txtNumeroFactura?.Focus();
            }
        }

        protected override void OnEliminarFila(DataGridViewRow fila)
        {
            if (fila != null)
            {
                if (MessageBox.Show("¿Está seguro de eliminar esta factura?", "Confirmar Eliminación",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    try
                    {
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
        }

        private void CargarDatosVentas()
        {
            try
            {
                if (dgvDatos == null)
                {
                    System.Diagnostics.Debug.WriteLine("dgvDatos es null en CargarDatos");
                    return;
                }

                DataTable dt = NVentas.Mostrar();

                if (dt != null && dt.Rows.Count >= 0)
                {
                    dgvDatos.DataSource = dt;
                    PersonalizarColumnasVentas();
                }
                else
                {
                    dgvDatos.DataSource = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar datos: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);

                if (dgvDatos != null)
                    dgvDatos.DataSource = null;
            }
        }

        private void PersonalizarColumnasVentas()
        {
            if (dgvDatos?.DataSource == null) return;

            try
            {
                foreach (DataGridViewColumn column in dgvDatos.Columns)
                {
                    if (column == null || string.IsNullOrEmpty(column.Name))
                        continue;

                    switch (column.Name.ToLower())
                    {
                        case "id":
                            column.HeaderText = "ID";
                            column.Width = 50;
                            break;
                        case "numero_factura":
                            column.HeaderText = "Nº Factura";
                            column.Width = 100;
                            break;
                        case "fecha_emision":
                            column.HeaderText = "Fecha Emisión";
                            column.Width = 100;
                            break;
                        case "fecha_vencimiento":
                            column.HeaderText = "Fecha Vencimiento";
                            column.Width = 120;
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
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error en PersonalizarColumnas: {ex.Message}");
            }
        }

        private void BuscarVentas(string textoBuscar)
        {
            try
            {
                if (int.TryParse(textoBuscar, out int personaId))
                {
                    DataTable dt = NVentas.BuscarPorPersona(personaId);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        dgvDatos.DataSource = dt;
                        PersonalizarColumnasVentas();
                    }
                    else
                    {
                        MessageBox.Show("No se encontraron facturas para esta persona", "Información",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        dgvDatos.DataSource = null;
                    }
                }
                else
                {
                    MessageBox.Show("Ingrese un ID de persona válido para buscar", "Información",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error en la búsqueda: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ValidarControlesInicializados()
        {
            var controlesRequeridos = new Dictionary<string, Control?>
            {
                { "tabControlPrincipal", tabControlPrincipal },
                { "dgvDatos", dgvDatos },
                { "btnNuevo", btnNuevo },
                { "btnGuardar", btnGuardar },
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

        private void ConfigurarModulo()
        {
            try
            {
                // Configurar eventos - con validación null
                if (btnNuevo != null) btnNuevo.Click += BtnNuevo_Click;
                if (btnGuardar != null) btnGuardar.Click += BtnGuardar_Click;
                // btnEditar se maneja desde BaseModulos
                if (btnCancelar != null) btnCancelar.Click += BtnCancelar_Click;
                if (btnEliminar != null) btnEliminar.Click += BtnEliminar_Click;
                if (btnBuscar != null) btnBuscar.Click += BtnBuscar_Click;
                if (btnRefrescar != null) btnRefrescar.Click += BtnRefrescar_Click;

                // Eventos del DataGridView - usar dgvDatos del BaseModulos
                if (dgvDatos != null)
                {
                    dgvDatos.SelectionChanged += DgvDatos_SelectionChanged;
                    dgvDatos.CellDoubleClick += DgvDatos_CellDoubleClick;
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
                MessageBox.Show($"Error al configurar módulo: {ex.Message}\n\nDetalle: {ex.StackTrace}",
                    "Error de Inicialización", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #region Eventos de Botones

        private void BtnNuevo_Click(object? sender, EventArgs e)
        {
            OnNuevo();
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
                        CargarDatosVentas();
                        LimpiarFormulario();
                        ConfigurarEstadoBotones(false);
                        tabControlPrincipal.SelectedTab = tabInicio;
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
            if (dgvDatos?.CurrentRow != null)
            {
                OnEditar(dgvDatos.CurrentRow);
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
            if (dgvDatos?.CurrentRow != null)
            {
                OnEliminarFila(dgvDatos.CurrentRow);
            }
            else
            {
                MessageBox.Show("Seleccione una factura para eliminar", "Información",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void BtnBuscar_Click(object? sender, EventArgs e)
        {
            OnBuscar();
        }

        private void BtnRefrescar_Click(object? sender, EventArgs e)
        {
            CargarDatosVentas();
            if (txtBuscarPersonaId != null) txtBuscarPersonaId.Clear();
        }

        #endregion

        #region Eventos del DataGridView

        private void DgvDatos_SelectionChanged(object? sender, EventArgs e)
        {
            bool haySeleccion = dgvDatos?.CurrentRow != null;
            // btnEditar se maneja desde BaseModulos
            if (btnEliminar != null) btnEliminar.Enabled = haySeleccion;
        }

        private void DgvDatos_CellDoubleClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dgvDatos?.Rows[e.RowIndex] != null)
            {
                OnEditar(dgvDatos.Rows[e.RowIndex]);
            }
        }

        #endregion

        #region Métodos Auxiliares


        private void ConfigurarColumnasGrid()
        {
            try
            {
                if (dgvDatos?.DataSource == null || dgvDatos.Columns.Count == 0)
                    return;

                // SOLO configurar encabezados de texto - NADA más
                foreach (DataGridViewColumn column in dgvDatos.Columns)
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

        private void CargarDatosParaEdicion(DataGridViewRow fila)
        {
            if (fila != null)
            {
                CurrentFacturaId = Convert.ToInt32(fila.Cells["id"].Value);

                // Cargar datos básicos desde el grid
                if (txtNumeroFactura != null)
                    txtNumeroFactura.Text = fila.Cells["numero_factura"].Value?.ToString() ?? "";

                if (dtpFechaEmision != null)
                    dtpFechaEmision.Value = Convert.ToDateTime(fila.Cells["fecha_emision"].Value);

                if (fila.Cells["fecha_vencimiento"].Value != DBNull.Value)
                {
                    if (chkTieneFechaVencimiento != null) chkTieneFechaVencimiento.Checked = true;
                    if (dtpFechaVencimiento != null)
                        dtpFechaVencimiento.Value = Convert.ToDateTime(fila.Cells["fecha_vencimiento"].Value);
                }
                else
                {
                    if (chkTieneFechaVencimiento != null) chkTieneFechaVencimiento.Checked = false;
                }

                if (nudImpuestos != null)
                    nudImpuestos.Value = Convert.ToDecimal(fila.Cells["impuestos"].Value ?? 0);
                if (nudDescuentos != null)
                    nudDescuentos.Value = Convert.ToDecimal(fila.Cells["descuentos"].Value ?? 0);
                if (cmbEstado != null)
                    cmbEstado.Text = fila.Cells["estado"].Value?.ToString() ?? "Pendiente";
                if (txtNotas != null)
                    txtNotas.Text = fila.Cells["notas"].Value?.ToString() ?? "";

                // Para obtener más detalles, deberías hacer una consulta adicional
                // Por ahora asignamos valores por defecto
                if (txtPersonaId != null) txtPersonaId.Text = "1"; // Esto debería venir de la consulta detallada
                if (txtProductosJson != null) txtProductosJson.Text = "";
                if (txtServiciosJson != null) txtServiciosJson.Text = "";
            }
        }

        private new void LimpiarFormulario()
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
                // btnEditar se maneja desde BaseModulos
                if (btnEliminar != null) btnEliminar.Enabled = !editando && dgvDatos?.CurrentRow != null;

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

        // UserControls no manejan FormClosing, se puede implementar en eventos del Dashboard si es necesario
    }
}