using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using CapaNegocio;
using SistemVeterinario.Navigation;

namespace SistemVeterinario.Forms
{
    /// <summary>
    /// Módulo para gestión de Personal (Veterinarios y Auxiliares)
    /// Hereda de BaseModulos para funcionalidad estándar de CRUD
    /// </summary>
    public partial class PersonalModule : BaseModulos
    {
        #region Variables Privadas
        private string _tipoPersonalSeleccionado = "";
        #endregion

        #region Constructor
        public PersonalModule()
        {
            InitializeComponent();
            ConfigurarModulo();
            
            // Configurar botones editables después de la inicialización
            this.Load += (s, e) => {
                ConfigurarBotonesEditables();
                OptimizarLayoutPaneles();
            };
            
            // También configurar cuando se cambia a la pestaña de configuración
            if (this.tabControlPrincipal != null)
            {
                this.tabControlPrincipal.SelectedIndexChanged += (s, e) =>
                {
                    if (this.tabControlPrincipal.SelectedTab == this.tabConfiguraciones)
                    {
                        // Pequeño delay para que el tab se renderice completamente
                        var timer = new System.Windows.Forms.Timer();
                        timer.Interval = 50;
                        timer.Tick += (ts, te) =>
                        {
                            timer.Stop();
                            ConfigurarBotonesEditables();
                        };
                        timer.Start();
                    }
                    else if (this.tabControlPrincipal.SelectedTab == this.tabInicio)
                    {
                        // Optimizar layout cuando se va al tab de inicio
                        OptimizarLayoutPaneles();
                    }
                };
            }
            
            // Configurar redimensionamiento automático
            this.Resize += (s, e) => OptimizarLayoutPaneles();
        }
        #endregion

        #region Configuración Inicial
        private void ConfigurarModulo()
        {
            // Configurar ComboBox de tipo de personal
            cmbTipoPersonal.Items.Clear();
            cmbTipoPersonal.Items.Add("Todos");
            cmbTipoPersonal.Items.Add("Veterinario");
            cmbTipoPersonal.Items.Add("Auxiliar");
            cmbTipoPersonal.SelectedIndex = 0;

            // Configurar ComboBox de tipo para formulario
            cmbTipoPersonalForm.Items.Clear();
            cmbTipoPersonalForm.Items.Add("Veterinario");
            cmbTipoPersonalForm.Items.Add("Auxiliar");
            cmbTipoPersonalForm.SelectedIndex = 0;

            // Configurar ComboBox de turno (para auxiliares)
            cmbTurno.Items.Clear();
            cmbTurno.Items.Add("Mañana");
            cmbTurno.Items.Add("Tarde");
            cmbTurno.Items.Add("Noche");
            cmbTurno.SelectedIndex = 0;

            // Configurar ComboBox de nivel (para auxiliares)
            cmbNivel.Items.Clear();
            cmbNivel.Items.Add("Básico");
            cmbNivel.Items.Add("Intermedio");
            cmbNivel.Items.Add("Avanzado");
            cmbNivel.SelectedIndex = 0;

            // Configurar eventos
            cmbTipoPersonal.SelectedIndexChanged += CmbTipoPersonal_SelectedIndexChanged;
            cmbTipoPersonalForm.SelectedIndexChanged += CmbTipoPersonalForm_SelectedIndexChanged;
            dtpFechaContratacion.Value = DateTime.Now;

            // Configurar validaciones en tiempo real
            ConfigurarValidacionEnTiempoReal();

            // Configurar scroll horizontal del DataGridView
            ConfigurarScrollDataGrid();

            // Ocultar campos específicos inicialmente
            MostrarCamposSegunTipo();
        }

        private void ConfigurarValidacionEnTiempoReal()
        {
            // Validación de email en tiempo real
            txtEmail.TextChanged += (s, e) => ValidarEmailEnTiempoReal();
            txtEmail.Leave += (s, e) => ValidarCampoCompleto(txtEmail, "email");
            
            // Validación de teléfono - solo números
            txtTelefono.KeyPress += (s, e) => ValidarSoloNumeros(s, e);
            txtTelefono.Leave += (s, e) => ValidarCampoCompleto(txtTelefono, "telefono");
            
            // Validación de campos obligatorios
            txtNombre.Leave += (s, e) => ValidarCampoObligatorio(txtNombre, lblNombre);
            txtApellido.Leave += (s, e) => ValidarCampoObligatorio(txtApellido, lblApellido);
            txtUsuario.Leave += (s, e) => ValidarCampoObligatorio(txtUsuario, lblUsuario);
        }

        private void ConfigurarScrollDataGrid()
        {
            // Configuración inicial del DataGridView para scroll horizontal y vertical
            dgvDatos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            dgvDatos.ScrollBars = ScrollBars.Both;
            dgvDatos.AllowUserToResizeColumns = true;
            dgvDatos.AllowUserToResizeRows = false;
            
            // Asegurar que el DataGridView use scroll cuando sea necesario
            dgvDatos.AutoSize = false;
            dgvDatos.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        }

        private void CmbTipoPersonalForm_SelectedIndexChanged(object sender, EventArgs e)
        {
            MostrarCamposSegunTipo();
        }

        private void MostrarCamposSegunTipo()
        {
            bool esVeterinario = cmbTipoPersonalForm.Text == "Veterinario";

            // Suspender el layout para evitar parpadeo
            tabConfiguraciones.SuspendLayout();

            // Campos específicos de veterinario
            lblLicencia.Visible = esVeterinario;
            txtLicencia.Visible = esVeterinario;
            lblEspecialidad.Visible = esVeterinario;
            txtEspecialidad.Visible = esVeterinario;
            lblUniversidad.Visible = esVeterinario;
            txtUniversidad.Visible = esVeterinario;
            lblExperiencia.Visible = esVeterinario;
            numExperiencia.Visible = esVeterinario;

            // Campos específicos de auxiliar
            lblArea.Visible = !esVeterinario;
            txtArea.Visible = !esVeterinario;
            lblTurno.Visible = !esVeterinario;
            cmbTurno.Visible = !esVeterinario;
            lblNivel.Visible = !esVeterinario;
            cmbNivel.Visible = !esVeterinario;

            // CAMPOS COMUNES - SIEMPRE VISIBLES
            // El campo rol se maneja directamente en la base de datos, no a través de la UI

            // Reanudar el layout y actualizar
            tabConfiguraciones.ResumeLayout(true);
            tabConfiguraciones.Refresh();

            // Mostrar mensaje informativo
            MostrarMensajeInformativoPersonal(esVeterinario ? "Veterinario" : "Auxiliar");
        }

        private void MostrarMensajeInformativoPersonal(string tipo)
        {
            string mensaje = tipo == "Veterinario" 
                ? "Complete los datos del veterinario. Los campos específicos de veterinario están habilitados."
                : "Complete los datos del auxiliar. Los campos específicos de auxiliar están habilitados.";
            
            // Actualizar el título del formulario si es posible
            if (tabConfiguraciones != null)
            {
                tabConfiguraciones.Text = $"Configuración de Personal - {tipo}";
            }
        }

        private void CmbTipoPersonal_SelectedIndexChanged(object sender, EventArgs e)
        {
            _tipoPersonalSeleccionado = cmbTipoPersonal.Text;
            CargarDatos();
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
                
                try
                {
                    // Configurar propiedades básicas primero
                    var buttonWidth = 100;
                    var buttonHeight = 35;
                    var spacing = 15;
                    
                    // Asegurar que los botones estén en el tab correcto
                    if (this.tabConfiguraciones != null)
                    {
                        // Remover de cualquier contenedor anterior
                        if (this.btnEliminar.Parent != this.tabConfiguraciones)
                            this.tabConfiguraciones.Controls.Add(this.btnEliminar);
                        if (this.btnCancelar.Parent != this.tabConfiguraciones)
                            this.tabConfiguraciones.Controls.Add(this.btnCancelar);
                        if (this.btnGuardar.Parent != this.tabConfiguraciones)
                            this.tabConfiguraciones.Controls.Add(this.btnGuardar);
                        
                        var tabWidth = this.tabConfiguraciones.ClientSize.Width;
                        var tabHeight = this.tabConfiguraciones.ClientSize.Height;
                        
                        // Calcular posición centrada más robusta
                        var totalWidth = (buttonWidth * 3) + (spacing * 2);
                        var startX = Math.Max(10, (tabWidth - totalWidth) / 2);
                        var buttonY = Math.Max(10, tabHeight - 60); // 60px desde el bottom
                        
                        // Configurar btnEliminar
                        this.btnEliminar.Size = new Size(buttonWidth, buttonHeight);
                        this.btnEliminar.Location = new Point(startX, buttonY);
                        this.btnEliminar.Visible = true;
                        this.btnEliminar.Enabled = true;
                        this.btnEliminar.BringToFront();
                        this.btnEliminar.TabIndex = 0;
                        
                        // Configurar btnCancelar
                        this.btnCancelar.Size = new Size(buttonWidth, buttonHeight);
                        this.btnCancelar.Location = new Point(startX + buttonWidth + spacing, buttonY);
                        this.btnCancelar.Visible = true;
                        this.btnCancelar.Enabled = true;
                        this.btnCancelar.BringToFront();
                        this.btnCancelar.TabIndex = 1;
                        
                        // Configurar btnGuardar
                        this.btnGuardar.Size = new Size(buttonWidth, buttonHeight);
                        this.btnGuardar.Location = new Point(startX + (buttonWidth + spacing) * 2, buttonY);
                        this.btnGuardar.Visible = true;
                        this.btnGuardar.Enabled = true;
                        this.btnGuardar.BringToFront();
                        this.btnGuardar.TabIndex = 2;
                        
                        // Forzar actualización
                        this.tabConfiguraciones.Refresh();
                    }
                }
                catch (Exception)
                {
                    // Si hay algún error, al menos hacer los botones visibles
                    this.btnEliminar.Visible = true;
                    this.btnCancelar.Visible = true;
                    this.btnGuardar.Visible = true;
                }
            };
            timer.Start();
        }

        private void OptimizarLayoutPaneles()
        {
            try
            {
                if (this.tabInicio != null && this.panelBusqueda != null && this.dgvDatos != null)
                {
                    // Obtener el tamaño disponible del tab
                    var tabWidth = this.tabInicio.ClientSize.Width;
                    var tabHeight = this.tabInicio.ClientSize.Height;
                    
                    // Configurar panel de búsqueda
                    var margin = 10;
                    var panelBusquedaHeight = 80; // Altura óptima para el panel de búsqueda
                    
                    this.panelBusqueda.Location = new Point(margin, margin);
                    this.panelBusqueda.Size = new Size(tabWidth - (margin * 2), panelBusquedaHeight);
                    this.panelBusqueda.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
                    
                    // Configurar DataGridView para ocupar todo el espacio restante
                    var dgvTop = this.panelBusqueda.Bottom + margin;
                    var dgvHeight = tabHeight - dgvTop - margin;
                    
                    this.dgvDatos.Location = new Point(margin, dgvTop);
                    this.dgvDatos.Size = new Size(tabWidth - (margin * 2), dgvHeight);
                    this.dgvDatos.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
                    
                    // Asegurar que el DataGridView llene completamente el espacio
                    this.dgvDatos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                    
                    // Optimizar la visualización de las columnas
                    if (this.dgvDatos.Columns.Count > 0)
                    {
                        // Ajustar ancho de columnas para mejor visualización
                        var totalWidth = this.dgvDatos.ClientSize.Width;
                        var columnCount = this.dgvDatos.Columns.Count;
                        var columnWidth = totalWidth / columnCount;
                        
                        foreach (DataGridViewColumn column in this.dgvDatos.Columns)
                        {
                            column.MinimumWidth = Math.Max(100, columnWidth - 20);
                            column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                        }
                    }
                    
                    // Forzar actualización visual
                    this.tabInicio.Invalidate();
                    this.Refresh();
                }
            }
            catch (Exception ex)
            {
                // Error silencioso para no interrumpir la funcionalidad
                System.Diagnostics.Debug.WriteLine($"Error optimizando layout: {ex.Message}");
            }
        }
        #endregion

        #region Métodos Override de BaseModulos

        protected override void OnLoad()
        {
            CargarDatos();
        }

        private void CargarDatos()
        {
            try
            {
                DataTable dt;

                if (_tipoPersonalSeleccionado == "Todos" || string.IsNullOrEmpty(_tipoPersonalSeleccionado))
                {
                    dt = NPersonal.Mostrar();
                }
                else
                {
                    dt = NPersonal.BuscarPorTipo(_tipoPersonalSeleccionado);
                }

                base.CargarDatos(dt);
                ConfigurarColumnasGrid();
            }
            catch (Exception ex)
            {
                MostrarMensaje($"Error al cargar datos: {ex.Message}", "Error", MessageBoxIcon.Error);
            }
        }

        protected override void OnBuscar()
        {
            try
            {
                string textoBuscar = txtBuscar.Text.Trim();
                DataTable dt;

                if (string.IsNullOrEmpty(textoBuscar))
                {
                    CargarDatos();
                    return;
                }

                dt = NPersonal.BuscarTexto(textoBuscar);
                base.CargarDatos(dt);
                ConfigurarColumnasGrid();
            }
            catch (Exception ex)
            {
                MostrarMensaje($"Error en búsqueda: {ex.Message}", "Error", MessageBoxIcon.Error);
            }
        }

        private bool ValidarFormulario()
        {
            // Validar campos obligatorios comunes
            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                MostrarMensaje("El nombre es obligatorio", "Validación", MessageBoxIcon.Warning);
                txtNombre.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtApellido.Text))
            {
                MostrarMensaje("El apellido es obligatorio", "Validación", MessageBoxIcon.Warning);
                txtApellido.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                MostrarMensaje("El email es obligatorio", "Validación", MessageBoxIcon.Warning);
                txtEmail.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtUsuario.Text))
            {
                MostrarMensaje("El usuario es obligatorio", "Validación", MessageBoxIcon.Warning);
                txtUsuario.Focus();
                return false;
            }

            if (!ModoEdicion && string.IsNullOrWhiteSpace(txtContrasena.Text))
            {
                MostrarMensaje("La contraseña es obligatoria", "Validación", MessageBoxIcon.Warning);
                txtContrasena.Focus();
                return false;
            }

            if (cmbTipoPersonalForm.SelectedIndex == -1)
            {
                MostrarMensaje("Debe seleccionar un tipo de personal", "Validación", MessageBoxIcon.Warning);
                cmbTipoPersonalForm.Focus();
                return false;
            }

            // Validaciones específicas según el tipo
            if (cmbTipoPersonalForm.Text == "Auxiliar")
            {
                if (cmbTurno.SelectedIndex == -1)
                {
                    MostrarMensaje("Debe seleccionar un turno", "Validación", MessageBoxIcon.Warning);
                    cmbTurno.Focus();
                    return false;
                }

                if (cmbNivel.SelectedIndex == -1)
                {
                    MostrarMensaje("Debe seleccionar un nivel", "Validación", MessageBoxIcon.Warning);
                    cmbNivel.Focus();
                    return false;
                }
            }

            return true;
        }

        protected override void OnGuardar()
        {
            if (GuardarRegistro())
            {
                // Si el guardado fue exitoso, mantener en pestaña de configuraciones
                // para permitir seguir editando o agregar más registros
            }
        }

        protected override void OnCancelar()
        {
            // Implementar funcionalidad específica de cancelar para Personal
            base.OnCancelar(); // Esto cambia a la pestaña Inicio y limpia el formulario
            
            // Recargar datos para asegurar que el ComboBox de filtro se mantenga
            CargarDatos();
        }

        private bool GuardarRegistro()
        {
            try
            {
                if (!ValidarFormulario())
                    return false;

                string resultado;
                string tipoPersonal = cmbTipoPersonalForm.Text;

                decimal? salario = string.IsNullOrEmpty(txtSalario.Text) ? (decimal?)null : decimal.Parse(txtSalario.Text);
                if (ModoEdicion)
                {
                    // Editar registro existente
                    if (tipoPersonal == "Veterinario")
                    {

                        resultado = NPersonal.EditarPersonalVeterinario(
                            IdSeleccionado,
                            txtNombre.Text.Trim(),
                            txtApellido.Text.Trim(),
                            txtEmail.Text.Trim(),
                            txtUsuario.Text.Trim(),
                            txtTelefono.Text.Trim(),
                            txtDireccion.Text.Trim(),
                            salario,
                            "Usuario", // Rol por defecto
                            txtLicencia.Text.Trim(),
                            txtEspecialidad.Text.Trim(),
                            txtUniversidad.Text.Trim(),
                            (int)numExperiencia.Value
                        );
                    }
                    else
                    {
                        resultado = NPersonal.EditarPersonalAuxiliar(
                            IdSeleccionado,
                            txtNombre.Text.Trim(),
                            txtApellido.Text.Trim(),
                            txtEmail.Text.Trim(),
                            txtUsuario.Text.Trim(),
                            txtTelefono.Text.Trim(),
                            txtDireccion.Text.Trim(),
                            salario,
                            "Usuario", // Rol por defecto
                            txtArea.Text.Trim(),
                            cmbTurno.Text,
                            cmbNivel.Text
                        );
                    }
                }
                else
                {
                    // Crear nuevo registro
                    if (tipoPersonal == "Veterinario")
                    {
                        resultado = NPersonal.InsertarPersonalVeterinario(
                            txtNombre.Text.Trim(),
                            txtApellido.Text.Trim(),
                            txtEmail.Text.Trim(),
                            txtUsuario.Text.Trim(),
                            txtContrasena.Text,
                            txtTelefono.Text.Trim(),
                            txtDireccion.Text.Trim(),
                            dtpFechaContratacion.Value,
                            salario,
                            "Usuario", // Rol por defecto
                            txtLicencia.Text.Trim(),
                            txtEspecialidad.Text.Trim(),
                            txtUniversidad.Text.Trim(),
                            (int)numExperiencia.Value
                        );
                    }
                    else
                    {
                        resultado = NPersonal.InsertarPersonalAuxiliar(
                            txtNombre.Text.Trim(),
                            txtApellido.Text.Trim(),
                            txtEmail.Text.Trim(),
                            txtUsuario.Text.Trim(),
                            txtContrasena.Text,
                            txtTelefono.Text.Trim(),
                            txtDireccion.Text.Trim(),
                            dtpFechaContratacion.Value,
                            salario,
                            "Usuario", // Rol por defecto
                            txtArea.Text.Trim(),
                            cmbTurno.Text,
                            cmbNivel.Text
                        );
                    }
                }

                // Verificar si el resultado indica éxito
                bool esExitoso = resultado == "OK" || 
                               resultado.Contains("correctamente") || 
                               resultado.Contains("exitosamente") ||
                               resultado.Contains("actualizado") ||
                               resultado.Contains("registrado") ||
                               resultado.Contains("creado");

                if (esExitoso)
                {
                    string accion = ModoEdicion ? "actualizado" : "registrado";
                    MostrarMensaje($"Personal {accion} correctamente", "Éxito", MessageBoxIcon.Information);
                    
                    // Cambiar a la pestaña de Inicio
                    tabControlPrincipal.SelectedTab = tabInicio;
                    
                    // Recargar los datos en el grid
                    CargarDatos();
                    
                    // Limpiar formulario y resetear modo
                    LimpiarFormulario();
                    cmbModo.SelectedIndex = 0; // Cambiar a modo inserción
                    ModoEdicion = false;
                    IdSeleccionado = 0;
                    txtId.Text = "";
                    
                    return true;
                }
                else
                {
                    MostrarMensaje($"Error al guardar: {resultado}", "Error", MessageBoxIcon.Error);
                    return false;
                }
            }
            catch (Exception ex)
            {
                MostrarMensaje($"Error al guardar: {ex.Message}", "Error", MessageBoxIcon.Error);
                return false;
            }
        }

        protected override void OnEliminar()
        {
            EliminarRegistro();
        }

        protected override void OnEliminarFila(DataGridViewRow row)
        {
            // Obtener ID de la fila seleccionada
            if (row.DataBoundItem is DataRowView dataRow)
            {
                int id = Convert.ToInt32(dataRow["id"]);
                // Para personal, construir el nombre a partir de nombre y apellido
                string nombre = dataRow["nombre"]?.ToString() ?? "";
                string apellido = dataRow["apellido"]?.ToString() ?? "";
                string nombreCompleto = $"{nombre} {apellido}".Trim();
                
                if (string.IsNullOrEmpty(nombreCompleto))
                    nombreCompleto = "registro";

                var resultado = MostrarConfirmacion(
                    $"¿Está seguro que desea eliminar al personal '{nombreCompleto}'?",
                    "Confirmar eliminación"
                );

                if (resultado == DialogResult.Yes)
                {
                    // Usar el método de eliminación específico del módulo
                    IdSeleccionado = id;
                    if (EliminarRegistro())
                    {
                        OnBuscar(); // Refrescar la lista
                    }
                }
            }
        }

        protected override void EliminarRegistro(int id)
        {
            try
            {
                string resultado = NPersonal.Eliminar(id);

                if (resultado == "OK")
                {
                    MostrarMensaje("Personal eliminado correctamente", "Éxito", MessageBoxIcon.Information);
                    CargarDatos();
                    LimpiarFormulario();
                }
                else
                {
                    MostrarMensaje(resultado, "Error", MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MostrarMensaje($"Error al eliminar: {ex.Message}", "Error", MessageBoxIcon.Error);
            }
        }

        private bool EliminarRegistro()
        {
            try
            {
                if (IdSeleccionado <= 0)
                {
                    MostrarMensaje("Debe seleccionar un registro para eliminar", "Validación", MessageBoxIcon.Warning);
                    return false;
                }

                var confirmacion = MessageBox.Show(
                    "¿Está seguro de eliminar este personal?",
                    "Confirmar Eliminación",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (confirmacion == DialogResult.Yes)
                {
                    string resultado = NPersonal.Eliminar(IdSeleccionado);

                    if (resultado == "OK")
                    {
                        MostrarMensaje("Personal eliminado correctamente", "Éxito", MessageBoxIcon.Information);
                        CargarDatos();
                        LimpiarFormulario();
                        return true;
                    }
                    else
                    {
                        MostrarMensaje(resultado, "Error", MessageBoxIcon.Error);
                        return false;
                    }
                }

                return false;
            }
            catch (Exception ex)
            {
                MostrarMensaje($"Error al eliminar: {ex.Message}", "Error", MessageBoxIcon.Error);
                return false;
            }
        }

        protected override void CargarDatosParaEdicion(int id)
        {
            IdSeleccionado = id;
            CargarDatosFormulario();
        }

        private void CargarDatosFormulario()
        {
            try
            {
                if (IdSeleccionado <= 0) return;

                DataTable dt = NPersonal.ObtenerPorId(IdSeleccionado);
                if (dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];

                    txtNombre.Text = row["nombre"]?.ToString() ?? "";
                    txtApellido.Text = row["apellido"]?.ToString() ?? "";
                    txtEmail.Text = row["email"]?.ToString() ?? "";
                    txtUsuario.Text = row["usuario"]?.ToString() ?? "";
                    txtTelefono.Text = row["telefono"]?.ToString() ?? "";
                    txtDireccion.Text = row["direccion"]?.ToString() ?? "";
                    txtSalario.Text = row["salario"]?.ToString() ?? "";
                    // El campo rol se almacena en la BD pero no se edita desde la UI

                    if (DateTime.TryParse(row["fecha_contratacion"]?.ToString(), out DateTime fechaContratacion))
                        dtpFechaContratacion.Value = fechaContratacion;

                    // Determinar tipo de personal basado en campos específicos
                    bool esVeterinario = !string.IsNullOrEmpty(row["num_licencia"]?.ToString());
                    
                    cmbTipoPersonalForm.Text = esVeterinario ? "Veterinario" : "Auxiliar";

                    if (esVeterinario)
                    {
                        txtLicencia.Text = row["num_licencia"]?.ToString() ?? "";
                        txtEspecialidad.Text = row["especialidad"]?.ToString() ?? "";
                        txtUniversidad.Text = row["universidad"]?.ToString() ?? "";
                        if (int.TryParse(row["anios_experiencia"]?.ToString(), out int experiencia))
                            numExperiencia.Value = experiencia;
                    }
                    else
                    {
                        txtArea.Text = row["area"]?.ToString() ?? "";
                        cmbTurno.Text = row["turno"]?.ToString() ?? "Mañana";
                        cmbNivel.Text = row["nivel"]?.ToString() ?? "Básico";
                    }

                    MostrarCamposSegunTipo();
                    txtContrasena.Text = ""; // No mostrar contraseña por seguridad
                }
            }
            catch (Exception ex)
            {
                MostrarMensaje($"Error al cargar datos del formulario: {ex.Message}", "Error", MessageBoxIcon.Error);
            }
        }

        protected override void LimpiarFormulario()
        {
            base.LimpiarFormulario();
            txtNombre.Text = "";
            txtApellido.Text = "";
            txtEmail.Text = "";
            txtUsuario.Text = "";
            txtContrasena.Text = "";
            txtTelefono.Text = "";
            txtDireccion.Text = "";
            txtSalario.Text = "";
            dtpFechaContratacion.Value = DateTime.Now;

            // Campos específicos
            txtLicencia.Text = "";
            txtEspecialidad.Text = "";
            txtUniversidad.Text = "";
            numExperiencia.Value = 0;
            txtArea.Text = "";
            cmbTurno.SelectedIndex = 0;
            cmbNivel.SelectedIndex = 0;

            cmbTipoPersonalForm.SelectedIndex = 0;
            MostrarCamposSegunTipo();
        }

        private void PersonalizarColumnas()
        {
            if (dgvDatos.DataSource == null) return;

            try
            {
                // Configurar encabezados de columnas con anchos reducidos
                if (dgvDatos.Columns.Contains("id"))
                    dgvDatos.Columns["id"].Visible = false;
                    
                if (dgvDatos.Columns.Contains("nombre"))
                {
                    dgvDatos.Columns["nombre"].HeaderText = "Nombre";
                    dgvDatos.Columns["nombre"].Width = 80;
                }
                
                if (dgvDatos.Columns.Contains("apellido"))
                {
                    dgvDatos.Columns["apellido"].HeaderText = "Apellido";
                    dgvDatos.Columns["apellido"].Width = 80;
                }
                
                if (dgvDatos.Columns.Contains("email"))
                {
                    dgvDatos.Columns["email"].HeaderText = "Email";
                    dgvDatos.Columns["email"].Width = 120;
                }
                
                if (dgvDatos.Columns.Contains("usuario"))
                {
                    dgvDatos.Columns["usuario"].HeaderText = "Usuario";
                    dgvDatos.Columns["usuario"].Width = 70;
                }
                
                if (dgvDatos.Columns.Contains("telefono"))
                {
                    dgvDatos.Columns["telefono"].HeaderText = "Teléfono";
                    dgvDatos.Columns["telefono"].Width = 80;
                }
                
                if (dgvDatos.Columns.Contains("rol"))
                {
                    dgvDatos.Columns["rol"].HeaderText = "Rol";
                    dgvDatos.Columns["rol"].Width = 70;
                }
                
                if (dgvDatos.Columns.Contains("fecha_contratacion"))
                {
                    dgvDatos.Columns["fecha_contratacion"].HeaderText = "F. Contrat.";
                    dgvDatos.Columns["fecha_contratacion"].Width = 80;
                }
                
                if (dgvDatos.Columns.Contains("salario"))
                {
                    dgvDatos.Columns["salario"].HeaderText = "Salario";
                    dgvDatos.Columns["salario"].Width = 70;
                }
                
                // Campos específicos con anchos reducidos
                if (dgvDatos.Columns.Contains("num_licencia"))
                {
                    dgvDatos.Columns["num_licencia"].HeaderText = "Licencia";
                    dgvDatos.Columns["num_licencia"].Width = 70;
                }
                
                if (dgvDatos.Columns.Contains("especialidad"))
                {
                    dgvDatos.Columns["especialidad"].HeaderText = "Especialidad";
                    dgvDatos.Columns["especialidad"].Width = 90;
                }
                
                if (dgvDatos.Columns.Contains("universidad"))
                {
                    dgvDatos.Columns["universidad"].HeaderText = "Universidad";
                    dgvDatos.Columns["universidad"].Width = 80;
                }
                
                if (dgvDatos.Columns.Contains("anos_experiencia"))
                {
                    dgvDatos.Columns["anos_experiencia"].HeaderText = "Años Exp.";
                    dgvDatos.Columns["anos_experiencia"].Width = 60;
                }
                
                if (dgvDatos.Columns.Contains("area"))
                {
                    dgvDatos.Columns["area"].HeaderText = "Área";
                    dgvDatos.Columns["area"].Width = 60;
                }
                
                if (dgvDatos.Columns.Contains("turno"))
                {
                    dgvDatos.Columns["turno"].HeaderText = "Turno";
                    dgvDatos.Columns["turno"].Width = 60;
                }
                
                if (dgvDatos.Columns.Contains("nivel"))
                {
                    dgvDatos.Columns["nivel"].HeaderText = "Nivel";
                    dgvDatos.Columns["nivel"].Width = 50;
                }

                if (dgvDatos.Columns.Contains("activo"))
                {
                    dgvDatos.Columns["activo"].HeaderText = "Activo";
                    dgvDatos.Columns["activo"].Width = 50;
                }

                if (dgvDatos.Columns.Contains("fecha_ultimo_acceso"))
                {
                    dgvDatos.Columns["fecha_ultimo_acceso"].HeaderText = "Últ. Acc.";
                    dgvDatos.Columns["fecha_ultimo_acceso"].Width = 80;
                }
                
                // Configurar los botones de acción con ancho reducido
                if (dgvDatos.Columns.Contains("btnEditar"))
                {
                    dgvDatos.Columns["btnEditar"].Width = 80;
                }
                
                if (dgvDatos.Columns.Contains("btnEliminar"))
                {
                    dgvDatos.Columns["btnEliminar"].Width = 80;
                }

                // Ocultar campos internos
                if (dgvDatos.Columns.Contains("contrasena"))
                    dgvDatos.Columns["contrasena"].Visible = false;
                if (dgvDatos.Columns.Contains("created_at"))
                    dgvDatos.Columns["created_at"].Visible = false;
                if (dgvDatos.Columns.Contains("updated_at"))
                    dgvDatos.Columns["updated_at"].Visible = false;

                // No usar AutoSizeColumnsMode.Fill para evitar que se expandan
                dgvDatos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
                
                // Habilitar scroll horizontal si es necesario
                dgvDatos.ScrollBars = ScrollBars.Both;
            }
            catch (Exception ex)
            {
                MostrarMensaje($"Error al personalizar columnas: {ex.Message}", "Error", MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Métodos Auxiliares

        private void ConfigurarColumnasGrid()
        {
            PersonalizarColumnas();
            ConfigurarScrollHorizontal();

            // Configurar formato de columnas específicas
            if (dgvDatos.Columns.Contains("salario"))
            {
                dgvDatos.Columns["salario"].DefaultCellStyle.Format = "C2";
                dgvDatos.Columns["salario"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }

            if (dgvDatos.Columns.Contains("fecha_contratacion"))
            {
                dgvDatos.Columns["fecha_contratacion"].DefaultCellStyle.Format = "dd/MM/yyyy";
            }
        }

        private void ConfigurarScrollHorizontal()
        {
            // Configurar scroll horizontal automático
            dgvDatos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            dgvDatos.ScrollBars = ScrollBars.Both;
            
            // Permitir que las columnas mantengan su ancho fijo para habilitar scroll horizontal
            foreach (DataGridViewColumn column in dgvDatos.Columns)
            {
                if (column.Name != "btnEditar" && column.Name != "btnEliminar")
                {
                    column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                    // Asegurar un ancho mínimo para que sea necesario hacer scroll
                    if (column.Width < 100)
                        column.Width = 120;
                }
            }
        }

        #endregion

        #region Métodos de Validación y Efectos Visuales
        private void ValidarEmailEnTiempoReal()
        {
            if (string.IsNullOrWhiteSpace(txtEmail.Text)) return;
            
            bool esValido = NPersonal.ValidarEmail(txtEmail.Text);
            txtEmail.BackColor = esValido ? Color.FromArgb(240, 248, 255) : Color.FromArgb(255, 240, 240);
        }

        private void ValidarSoloNumeros(object sender, KeyPressEventArgs e)
        {
            // Permitir solo números, backspace y delete
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void ValidarCampoCompleto(TextBox textBox, string tipoCampo)
        {
            Color colorValido = Color.FromArgb(240, 248, 255);
            Color colorInvalido = Color.FromArgb(255, 240, 240);
            Color colorNormal = Color.White;

            switch (tipoCampo.ToLower())
            {
                case "email":
                    if (!string.IsNullOrWhiteSpace(textBox.Text))
                    {
                        textBox.BackColor = NPersonal.ValidarEmail(textBox.Text) ? colorValido : colorInvalido;
                    }
                    else
                    {
                        textBox.BackColor = colorNormal;
                    }
                    break;
                case "telefono":
                    if (!string.IsNullOrWhiteSpace(textBox.Text))
                    {
                        textBox.BackColor = textBox.Text.Length >= 6 ? colorValido : colorInvalido;
                    }
                    else
                    {
                        textBox.BackColor = colorNormal;
                    }
                    break;
            }
        }

        private void ValidarCampoObligatorio(TextBox textBox, Label label)
        {
            Color colorValido = Color.FromArgb(240, 248, 255);
            Color colorInvalido = Color.FromArgb(255, 240, 240);
            Color colorNormal = Color.White;

            if (string.IsNullOrWhiteSpace(textBox.Text))
            {
                textBox.BackColor = colorInvalido;
                if (label != null) label.ForeColor = Color.FromArgb(231, 76, 60);
            }
            else
            {
                textBox.BackColor = colorValido;
                if (label != null) label.ForeColor = Color.FromArgb(39, 174, 96);
            }
        }
        #endregion


    }
}