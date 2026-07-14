using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using CapaNegocio;

namespace SistemVeterinario
{
    /// <summary>
    /// Formulario para gestión de clientes/personas
    /// Incluye listado y mantenimiento (CRUD)
    /// Maneja tanto personas físicas como jurídicas
    /// </summary>
    public partial class Cliente : Form
    {
        private bool IsEditing = false;
        private int CurrentClienteId = 0;
        private string CurrentTipoPersona = "Física";

        public Cliente()
        {
            InitializeComponent();
            ValidarControlesInicializados();
            ConfigurarFormulario();
            
            if (dgvClientes != null)
            {
                CargarDatos();
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("No se cargan datos: dgvClientes es null");
            }
        }

        private void ValidarControlesInicializados()
        {
            var controlesRequeridos = new Dictionary<string, Control?>
            {
                { "tabControl", tabControl },
                { "dgvClientes", dgvClientes },
                { "btnNuevo", btnNuevo },
                { "btnGuardar", btnGuardar },
                { "btnEditar", btnEditar },
                { "btnEliminar", btnEliminar },
                { "btnCancelar", btnCancelar },
                { "cmbTipoPersona", cmbTipoPersona }
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
                if (dgvClientes != null)
                {
                    dgvClientes.SelectionChanged += DgvClientes_SelectionChanged;
                    dgvClientes.CellDoubleClick += DgvClientes_CellDoubleClick;
                }

                // Configurar ComboBox de Tipo
                if (cmbTipoPersona != null)
                {
                    cmbTipoPersona.Items.AddRange(new string[] { "Física", "Jurídica" });
                    cmbTipoPersona.SelectedIndex = 0;
                    cmbTipoPersona.SelectedIndexChanged += CmbTipoPersona_SelectedIndexChanged;
                }

                // Configurar ComboBox de Género
                if (cmbGenero != null)
                {
                    cmbGenero.Items.AddRange(new string[] { "", "M", "F" });
                    cmbGenero.SelectedIndex = 0;
                }

                // Configurar fechas
                if (dtpFechaNacimiento != null)
                {
                    dtpFechaNacimiento.Value = DateTime.Now.AddYears(-18);
                    dtpFechaNacimiento.MaxDate = DateTime.Now;
                }

                // Estado inicial
                LimpiarFormulario();
                ConfigurarEstadoBotones(false);
                ConfigurarCamposPorTipo("Física");
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
            CurrentClienteId = 0;
            ConfigurarEstadoBotones(true);
            tabControl.SelectedTab = tabMantenimiento;
            
            // Enfocar el primer campo según el tipo
            if (CurrentTipoPersona == "Física")
                txtNombre?.Focus();
            else
                txtRazonSocial?.Focus();
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
                        resultado = GuardarEdicion();
                    }
                    else
                    {
                        resultado = GuardarNuevo();
                    }

                    if (resultado == "OK" || resultado.Contains("exitosamente"))
                    {
                        MessageBox.Show(IsEditing ? "Cliente actualizado exitosamente" : "Cliente creado exitosamente", 
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
            if (dgvClientes?.CurrentRow != null)
            {
                CargarDatosParaEdicion();
                IsEditing = true;
                ConfigurarEstadoBotones(true);
                tabControl.SelectedTab = tabMantenimiento;
                
                // Enfocar el primer campo según el tipo
                if (CurrentTipoPersona == "Física")
                    txtNombre?.Focus();
                else
                    txtRazonSocial?.Focus();
            }
            else
            {
                MessageBox.Show("Seleccione un cliente para editar", "Información", 
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
                CurrentClienteId = 0;
            }
        }

        private void BtnEliminar_Click(object? sender, EventArgs e)
        {
            if (dgvClientes?.CurrentRow != null)
            {
                if (MessageBox.Show("¿Está seguro de eliminar este cliente?", "Confirmar Eliminación", 
                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    try
                    {
                        int id = Convert.ToInt32(dgvClientes.CurrentRow.Cells["id"].Value);
                        string resultado = NPersonas.Eliminar(id);
                        
                        if (resultado == "OK")
                        {
                            MessageBox.Show("Cliente eliminado exitosamente", "Éxito", 
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                            CargarDatos();
                        }
                        else
                        {
                            MessageBox.Show($"Error al eliminar: {resultado}", "Error", 
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
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
                MessageBox.Show("Seleccione un cliente para eliminar", "Información", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void BtnBuscar_Click(object? sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtBuscar?.Text))
            {
                try
                {
                    DataTable dt = NPersonas.BuscarPorNombre(txtBuscar.Text);
                    
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        if (dgvClientes != null)
                        {
                            dgvClientes.DataSource = dt;
                            ConfigurarColumnasGrid();
                        }
                        if (lblTotalRegistros != null)
                            lblTotalRegistros.Text = $"Total registros: {dt.Rows.Count}";
                    }
                    else
                    {
                        MessageBox.Show("No se encontraron clientes con ese criterio", "Información", 
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        if (dgvClientes != null) dgvClientes.DataSource = null;
                        if (lblTotalRegistros != null) lblTotalRegistros.Text = "Total registros: 0";
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
            if (txtBuscar != null) txtBuscar.Clear();
        }

        private void CmbTipoPersona_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (cmbTipoPersona != null)
            {
                CurrentTipoPersona = cmbTipoPersona.Text;
                ConfigurarCamposPorTipo(CurrentTipoPersona);
                LimpiarCamposEspecificos();
            }
        }

        #endregion

        #region Eventos del DataGridView

        private void DgvClientes_SelectionChanged(object? sender, EventArgs e)
        {
            bool haySeleccion = dgvClientes?.CurrentRow != null;
            if (btnEditar != null) btnEditar.Enabled = haySeleccion;
            if (btnEliminar != null) btnEliminar.Enabled = haySeleccion;
        }

        private void DgvClientes_CellDoubleClick(object? sender, DataGridViewCellEventArgs e)
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
                if (dgvClientes == null)
                {
                    System.Diagnostics.Debug.WriteLine("dgvClientes es null en CargarDatos");
                    return;
                }

                DataTable dt = NPersonas.Mostrar();
                
                if (dt != null && dt.Rows.Count >= 0)
                {
                    dgvClientes.DataSource = dt;
                    
                    if (dt.Columns.Count > 0)
                    {
                        try
                        {
                            ConfigurarColumnasGrid();
                        }
                        catch (Exception ex)
                        {
                            System.Diagnostics.Debug.WriteLine($"Falló ConfigurarColumnasGrid: {ex.Message}");
                        }
                    }
                    
                    if (lblTotalRegistros != null) 
                        lblTotalRegistros.Text = $"Total registros: {dt.Rows.Count}";
                }
                else
                {
                    dgvClientes.DataSource = null;
                    if (lblTotalRegistros != null) 
                        lblTotalRegistros.Text = "Total registros: 0";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar datos: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (dgvClientes != null) dgvClientes.DataSource = null;
                if (lblTotalRegistros != null) 
                    lblTotalRegistros.Text = "Total registros: 0 (Error)";
            }
        }

        private void ConfigurarColumnasGrid()
        {
            try
            {
                if (dgvClientes?.DataSource == null || dgvClientes.Columns.Count == 0)
                    return;

                foreach (DataGridViewColumn column in dgvClientes.Columns)
                {
                    if (column == null || string.IsNullOrEmpty(column.Name)) 
                        continue;

                    try
                    {
                        switch (column.Name.ToLower())
                        {
                            case "id":
                                column.HeaderText = "ID";
                                break;
                            case "tipo":
                                column.HeaderText = "Tipo";
                                break;
                            case "nombre_mostrar":
                                column.HeaderText = "Nombre/Razón Social";
                                break;
                            case "ci":
                                column.HeaderText = "Cédula";
                                break;
                            case "nit":
                                column.HeaderText = "NIT";
                                break;
                            case "email":
                                column.HeaderText = "Email";
                                break;
                            case "telefono":
                                column.HeaderText = "Teléfono";
                                break;
                            case "direccion":
                                column.HeaderText = "Dirección";
                                break;
                        }
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine($"Error configurando header de columna {column.Name}: {ex.Message}");
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error en ConfigurarColumnasGrid: {ex.Message}");
            }
        }

        private void ConfigurarCamposPorTipo(string tipo)
        {
            try
            {
                if (tipo == "Física")
                {
                    // Mostrar campos de persona física
                    if (grpPersonaFisica != null) grpPersonaFisica.Visible = true;
                    if (grpPersonaJuridica != null) grpPersonaJuridica.Visible = false;
                }
                else
                {
                    // Mostrar campos de persona jurídica
                    if (grpPersonaFisica != null) grpPersonaFisica.Visible = false;
                    if (grpPersonaJuridica != null) grpPersonaJuridica.Visible = true;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error en ConfigurarCamposPorTipo: {ex.Message}");
            }
        }

        private void LimpiarFormulario()
        {
            try
            {
                // Campos generales
                txtEmail?.Clear();
                txtDireccion?.Clear();
                txtTelefono?.Clear();
                
                // Campos persona física
                txtCi?.Clear();
                txtNombre?.Clear();
                txtApellido?.Clear();
                if (dtpFechaNacimiento != null) dtpFechaNacimiento.Value = DateTime.Now.AddYears(-18);
                if (cmbGenero != null) cmbGenero.SelectedIndex = 0;
                
                // Campos persona jurídica
                txtRazonSocial?.Clear();
                txtNit?.Clear();
                txtEncargadoNombre?.Clear();
                txtEncargadoCargo?.Clear();
                
                // Tipo por defecto
                if (cmbTipoPersona != null) cmbTipoPersona.SelectedIndex = 0;
                CurrentTipoPersona = "Física";
                ConfigurarCamposPorTipo(CurrentTipoPersona);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error en LimpiarFormulario: {ex.Message}");
            }
        }

        private void LimpiarCamposEspecificos()
        {
            try
            {
                if (CurrentTipoPersona == "Física")
                {
                    // Limpiar campos de persona jurídica
                    txtRazonSocial?.Clear();
                    txtNit?.Clear();
                    txtEncargadoNombre?.Clear();
                    txtEncargadoCargo?.Clear();
                }
                else
                {
                    // Limpiar campos de persona física
                    txtCi?.Clear();
                    txtNombre?.Clear();
                    txtApellido?.Clear();
                    if (dtpFechaNacimiento != null) dtpFechaNacimiento.Value = DateTime.Now.AddYears(-18);
                    if (cmbGenero != null) cmbGenero.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error en LimpiarCamposEspecificos: {ex.Message}");
            }
        }

        private void ConfigurarEstadoBotones(bool editando)
        {
            try
            {
                if (btnNuevo != null) btnNuevo.Enabled = !editando;
                if (btnGuardar != null) btnGuardar.Enabled = editando;
                if (btnCancelar != null) btnCancelar.Enabled = editando;
                if (btnEditar != null) btnEditar.Enabled = !editando && dgvClientes?.CurrentRow != null;
                if (btnEliminar != null) btnEliminar.Enabled = !editando && dgvClientes?.CurrentRow != null;

                // Habilitar/deshabilitar controles de edición
                if (cmbTipoPersona != null) cmbTipoPersona.Enabled = editando && !IsEditing; // Solo al crear nuevo
                if (txtEmail != null) txtEmail.Enabled = editando;
                if (txtDireccion != null) txtDireccion.Enabled = editando;
                if (txtTelefono != null) txtTelefono.Enabled = editando;
                
                // Campos persona física
                if (txtCi != null) txtCi.Enabled = editando;
                if (txtNombre != null) txtNombre.Enabled = editando;
                if (txtApellido != null) txtApellido.Enabled = editando;
                if (dtpFechaNacimiento != null) dtpFechaNacimiento.Enabled = editando;
                if (cmbGenero != null) cmbGenero.Enabled = editando;
                
                // Campos persona jurídica
                if (txtRazonSocial != null) txtRazonSocial.Enabled = editando;
                if (txtNit != null) txtNit.Enabled = editando;
                if (txtEncargadoNombre != null) txtEncargadoNombre.Enabled = editando;
                if (txtEncargadoCargo != null) txtEncargadoCargo.Enabled = editando;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error en ConfigurarEstadoBotones: {ex.Message}");
            }
        }

        private bool ValidarDatos()
        {
            try
            {
                if (CurrentTipoPersona == "Física")
                {
                    string errores = NPersonas.ValidarDatosPersonaFisica(
                        txtCi?.Text ?? "",
                        txtNombre?.Text ?? "",
                        txtApellido?.Text ?? "",
                        txtEmail?.Text ?? "",
                        txtTelefono?.Text ?? "",
                        cmbGenero?.Text ?? ""
                    );
                    
                    if (!string.IsNullOrEmpty(errores))
                    {
                        MessageBox.Show(errores, "Errores de Validación", 
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }
                }
                else
                {
                    string errores = NPersonas.ValidarDatosPersonaJuridica(
                        txtRazonSocial?.Text ?? "",
                        txtEmail?.Text ?? "",
                        txtTelefono?.Text ?? "",
                        txtNit?.Text ?? ""
                    );
                    
                    if (!string.IsNullOrEmpty(errores))
                    {
                        MessageBox.Show(errores, "Errores de Validación", 
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error en validación: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private string GuardarNuevo()
        {
            if (CurrentTipoPersona == "Física")
            {
                return NPersonas.InsertarPersonaFisica(
                    txtCi?.Text ?? "",
                    txtNombre?.Text ?? "",
                    txtApellido?.Text ?? "",
                    txtEmail?.Text ?? "",
                    txtDireccion?.Text ?? "",
                    txtTelefono?.Text ?? "",
                    dtpFechaNacimiento?.Value,
                    cmbGenero?.Text ?? ""
                );
            }
            else
            {
                return NPersonas.InsertarPersonaJuridica(
                    txtRazonSocial?.Text ?? "",
                    txtNit?.Text ?? "",
                    txtEmail?.Text ?? "",
                    txtDireccion?.Text ?? "",
                    txtTelefono?.Text ?? "",
                    txtEncargadoNombre?.Text ?? "",
                    txtEncargadoCargo?.Text ?? ""
                );
            }
        }

        private string GuardarEdicion()
        {
            if (CurrentTipoPersona == "Física")
            {
                return NPersonas.EditarPersonaFisica(
                    CurrentClienteId,
                    txtCi?.Text ?? "",
                    txtNombre?.Text ?? "",
                    txtApellido?.Text ?? "",
                    txtEmail?.Text ?? "",
                    txtDireccion?.Text ?? "",
                    txtTelefono?.Text ?? "",
                    dtpFechaNacimiento?.Value,
                    cmbGenero?.Text ?? ""
                );
            }
            else
            {
                return NPersonas.EditarPersonaJuridica(
                    CurrentClienteId,
                    txtRazonSocial?.Text ?? "",
                    txtNit?.Text ?? "",
                    txtEmail?.Text ?? "",
                    txtDireccion?.Text ?? "",
                    txtTelefono?.Text ?? "",
                    txtEncargadoNombre?.Text ?? "",
                    txtEncargadoCargo?.Text ?? ""
                );
            }
        }

        private void CargarDatosParaEdicion()
        {
            try
            {
                if (dgvClientes?.CurrentRow != null)
                {
                    CurrentClienteId = Convert.ToInt32(dgvClientes.CurrentRow.Cells["id"].Value);
                    CurrentTipoPersona = dgvClientes.CurrentRow.Cells["tipo"].Value?.ToString() ?? "Física";
                    
                    // Configurar tipo
                    if (cmbTipoPersona != null) cmbTipoPersona.Text = CurrentTipoPersona;
                    ConfigurarCamposPorTipo(CurrentTipoPersona);
                    
                    // Cargar datos generales
                    if (txtEmail != null) txtEmail.Text = dgvClientes.CurrentRow.Cells["email"].Value?.ToString() ?? "";
                    if (txtDireccion != null) txtDireccion.Text = dgvClientes.CurrentRow.Cells["direccion"].Value?.ToString() ?? "";
                    if (txtTelefono != null) txtTelefono.Text = dgvClientes.CurrentRow.Cells["telefono"].Value?.ToString() ?? "";
                    
                    if (CurrentTipoPersona == "Física")
                    {
                        // Cargar datos persona física
                        if (txtCi != null) txtCi.Text = dgvClientes.CurrentRow.Cells["ci"].Value?.ToString() ?? "";
                        if (txtNombre != null) txtNombre.Text = dgvClientes.CurrentRow.Cells["nombre"].Value?.ToString() ?? "";
                        if (txtApellido != null) txtApellido.Text = dgvClientes.CurrentRow.Cells["apellido"].Value?.ToString() ?? "";
                        if (cmbGenero != null) cmbGenero.Text = dgvClientes.CurrentRow.Cells["genero"].Value?.ToString() ?? "";
                        
                        if (dtpFechaNacimiento != null && dgvClientes.CurrentRow.Cells["fecha_nacimiento"].Value != DBNull.Value)
                        {
                            dtpFechaNacimiento.Value = Convert.ToDateTime(dgvClientes.CurrentRow.Cells["fecha_nacimiento"].Value);
                        }
                    }
                    else
                    {
                        // Cargar datos persona jurídica
                        if (txtRazonSocial != null) txtRazonSocial.Text = dgvClientes.CurrentRow.Cells["razon_social"].Value?.ToString() ?? "";
                        if (txtNit != null) txtNit.Text = dgvClientes.CurrentRow.Cells["nit"].Value?.ToString() ?? "";
                        if (txtEncargadoNombre != null) txtEncargadoNombre.Text = dgvClientes.CurrentRow.Cells["encargado_nombre"].Value?.ToString() ?? "";
                        if (txtEncargadoCargo != null) txtEncargadoCargo.Text = dgvClientes.CurrentRow.Cells["encargado_cargo"].Value?.ToString() ?? "";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar datos para edición: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (btnGuardar?.Enabled == true)
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