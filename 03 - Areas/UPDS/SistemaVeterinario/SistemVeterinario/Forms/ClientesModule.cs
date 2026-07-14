using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using CapaNegocio;
using SistemVeterinario.Navigation;

namespace SistemVeterinario.Forms
{
    /// <summary>
    /// Módulo para gestión de Personas (Físicas y Jurídicas)
    /// Hereda de BaseModulos para funcionalidad estándar de CRUD
    /// </summary>
    public partial class ClientesModule : BaseModulos
    {
        #region Variables Privadas
        private string _tipoPersonaSeleccionado = "";
        #endregion

        #region Constructor
        public ClientesModule()
        {
            InitializeComponent();
            ConfigurarModulo();
            ConfigurarValidacionEnTiempoReal();
            ConfigurarEstilosModernos();
            
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
            // Configurar ComboBox de tipo de persona
            cmbTipoPersona.Items.Clear();
            cmbTipoPersona.Items.Add("Todos");
            cmbTipoPersona.Items.Add("Física");
            cmbTipoPersona.Items.Add("Jurídica");
            cmbTipoPersona.SelectedIndex = 0;

            // Configurar ComboBox de género
            cmbGenero.Items.Clear();
            cmbGenero.Items.Add("Seleccionar...");
            cmbGenero.Items.Add("M");
            cmbGenero.Items.Add("F");
            cmbGenero.SelectedIndex = 0;

            // Configurar ComboBox de tipo para formulario
            cmbTipoPersonaForm.Items.Clear();
            cmbTipoPersonaForm.Items.Add("Física");
            cmbTipoPersonaForm.Items.Add("Jurídica");
            cmbTipoPersonaForm.SelectedIndex = 0;

            // Configurar eventos
            cmbTipoPersona.SelectedIndexChanged += CmbTipoPersona_SelectedIndexChanged;
            cmbTipoPersonaForm.SelectedIndexChanged += CmbTipoPersonaForm_SelectedIndexChanged;
            dtpFechaNacimiento.Value = DateTime.Now.AddYears(-18);

            // Configurar campos iniciales
            MostrarCamposSegunTipo("Física");
        }

        private void ConfigurarValidacionEnTiempoReal()
        {
            // Validación de email en tiempo real
            txtEmail.TextChanged += (s, e) => ValidarEmailEnTiempoReal();
            txtEmail.Leave += (s, e) => ValidarCampoCompleto(txtEmail, "email");
            
            // Validación de teléfono - solo números
            txtTelefono.KeyPress += (s, e) => ValidarSoloNumeros(s, e);
            txtTelefono.Leave += (s, e) => ValidarCampoCompleto(txtTelefono, "telefono");
            
            // Validación de CI - solo números
            txtCi.KeyPress += (s, e) => ValidarSoloNumeros(s, e);
            txtCi.Leave += (s, e) => ValidarCampoCompleto(txtCi, "ci");
            
            // Validación de NIT - solo números
            txtNit.KeyPress += (s, e) => ValidarSoloNumeros(s, e);
            txtNit.Leave += (s, e) => ValidarCampoCompleto(txtNit, "nit");
            
            // Validación de campos obligatorios
            txtNombre.Leave += (s, e) => ValidarCampoObligatorio(txtNombre, lblNombre);
            txtApellido.Leave += (s, e) => ValidarCampoObligatorio(txtApellido, lblApellido);
            txtRazonSocial.Leave += (s, e) => ValidarCampoObligatorio(txtRazonSocial, lblRazonSocial);
        }

        private void ConfigurarEstilosModernos()
        {
            // Configurar efectos hover para botones
            ConfigurarEfectoHover(btnBuscar);
            ConfigurarEfectoHover(btnNuevo);
            
            // Aplicar efectos de enfoque a los TextBox
            AplicarEfectosFocusTextBox(txtNombre);
            AplicarEfectosFocusTextBox(txtApellido);
            AplicarEfectosFocusTextBox(txtCi);
            AplicarEfectosFocusTextBox(txtEmail);
            AplicarEfectosFocusTextBox(txtTelefono);
            AplicarEfectosFocusTextBox(txtDireccion);
            AplicarEfectosFocusTextBox(txtRazonSocial);
            AplicarEfectosFocusTextBox(txtNit);
            AplicarEfectosFocusTextBox(txtEncargadoNombre);
            AplicarEfectosFocusTextBox(txtEncargadoCargo);
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

        protected override void OnBuscar()
        {
            string textoBuscar = txtBuscar.Text.Trim();
            string tipoFiltro = cmbTipoPersona.SelectedItem?.ToString() ?? "Todos";

            try
            {
                DataTable datos;

                if (!string.IsNullOrEmpty(textoBuscar))
                {
                    // Buscar por texto
                    datos = NPersona.BuscarTexto(textoBuscar);
                }
                else if (tipoFiltro != "Todos")
                {
                    // Filtrar por tipo
                    datos = NPersona.BuscarPorTipo(tipoFiltro);
                }
                else
                {
                    // Mostrar todos
                    datos = NPersona.Mostrar();
                }

                // Aplicar filtro adicional de tipo si es necesario
                if (!string.IsNullOrEmpty(textoBuscar) && tipoFiltro != "Todos")
                {
                    DataView dv = datos.DefaultView;
                    dv.RowFilter = $"tipo = '{tipoFiltro}'";
                    datos = dv.ToTable();
                }

                base.CargarDatos(datos);
                PersonalizarColumnasClientes();
                ActualizarContadorRegistros(datos.Rows.Count);
            }
            catch (Exception ex)
            {
                MostrarMensaje($"Error al buscar: {ex.Message}", "Error", MessageBoxIcon.Error);
            }
        }

        protected override void OnNuevo()
        {
            base.OnNuevo();
            LimpiarFormulario();
            cmbTipoPersonaForm.SelectedIndex = 0;
            MostrarCamposSegunTipo("Física");
        }

        protected override void OnGuardar()
        {
            if (!ValidarCampos())
                return;

            try
            {
                string resultado = "";
                string tipoPersona = cmbTipoPersonaForm.SelectedItem?.ToString() ?? "Física";

                if (ModoEdicion)
                {
                    // Editar registro existente
                    if (tipoPersona == "Física")
                    {
                        resultado = NPersona.EditarPersonaFisica(
                            IdSeleccionado,
                            txtCi.Text.Trim(),
                            txtNombre.Text.Trim(),
                            txtApellido.Text.Trim(),
                            txtEmail.Text.Trim(),
                            txtDireccion.Text.Trim(),
                            txtTelefono.Text.Trim(),
                            dtpFechaNacimiento.Value,
                            ObtenerGeneroSeleccionado()
                        );
                    }
                    else
                    {
                        resultado = NPersona.EditarPersonaJuridica(
                            IdSeleccionado,
                            txtRazonSocial.Text.Trim(),
                            txtNit.Text.Trim(),
                            txtEncargadoNombre.Text.Trim(),
                            txtEncargadoCargo.Text.Trim(),
                            txtEmail.Text.Trim(),
                            txtDireccion.Text.Trim(),
                            txtTelefono.Text.Trim()
                        );
                    }
                }
                else
                {
                    // Insertar nuevo registro
                    if (tipoPersona == "Física")
                    {
                        resultado = NPersona.InsertarPersonaFisica(
                            txtCi.Text.Trim(),
                            txtNombre.Text.Trim(),
                            txtApellido.Text.Trim(),
                            txtEmail.Text.Trim(),
                            txtDireccion.Text.Trim(),
                            txtTelefono.Text.Trim(),
                            dtpFechaNacimiento.Value,
                            ObtenerGeneroSeleccionado()
                        );
                    }
                    else
                    {
                        resultado = NPersona.InsertarPersonaJuridica(
                            txtRazonSocial.Text.Trim(),
                            txtNit.Text.Trim(),
                            txtEncargadoNombre.Text.Trim(),
                            txtEncargadoCargo.Text.Trim(),
                            txtEmail.Text.Trim(),
                            txtDireccion.Text.Trim(),
                            txtTelefono.Text.Trim()
                        );
                    }
                }

                if (resultado == "OK" || resultado.Contains("actualizada exitosamente") || resultado.Contains("creada exitosamente"))
                {
                    MostrarMensaje(ModoEdicion ? "Cliente actualizado exitosamente" : "Cliente registrado exitosamente");
                    OnCancelar();
                    OnBuscar();
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
                string resultado = NPersona.Eliminar(id);

                if (resultado == "OK")
                {
                    MostrarMensaje("Persona eliminada correctamente");
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
                DataTable datos = NPersona.ObtenerPorId(id);
                if (datos.Rows.Count > 0)
                {
                    DataRow row = datos.Rows[0];
                    string tipo = row["tipo"].ToString() ?? "Física";

                    // Configurar tipo de persona
                    cmbTipoPersonaForm.SelectedItem = tipo;
                    MostrarCamposSegunTipo(tipo);

                    // Cargar datos comunes
                    txtEmail.Text = row["email"]?.ToString() ?? "";
                    txtDireccion.Text = row["direccion"]?.ToString() ?? "";
                    txtTelefono.Text = row["telefono"]?.ToString() ?? "";

                    if (tipo == "Física")
                    {
                        txtCi.Text = row["ci"]?.ToString() ?? "";
                        txtNombre.Text = row["nombre"]?.ToString() ?? "";
                        txtApellido.Text = row["apellido"]?.ToString() ?? "";
                        
                        if (row["fecha_nacimiento"] != DBNull.Value && row["fecha_nacimiento"] != null)
                            dtpFechaNacimiento.Value = Convert.ToDateTime(row["fecha_nacimiento"]);

                        string genero = row["genero"]?.ToString();
                        if (!string.IsNullOrEmpty(genero))
                        {
                            cmbGenero.SelectedItem = genero;
                        }
                    }
                    else
                    {
                        txtRazonSocial.Text = row["razon_social"]?.ToString() ?? "";
                        txtNit.Text = row["nit"]?.ToString() ?? "";
                        txtEncargadoNombre.Text = row["encargado_nombre"]?.ToString() ?? "";
                        txtEncargadoCargo.Text = row["encargado_cargo"]?.ToString() ?? "";
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
            // Limpiar campos comunes
            txtEmail.Text = "";
            txtDireccion.Text = "";
            txtTelefono.Text = "";

            // Limpiar campos de persona física
            txtCi.Text = "";
            txtNombre.Text = "";
            txtApellido.Text = "";
            dtpFechaNacimiento.Value = DateTime.Now.AddYears(-18);
            cmbGenero.SelectedIndex = 0;

            // Limpiar campos de persona jurídica
            txtRazonSocial.Text = "";
            txtNit.Text = "";
            txtEncargadoNombre.Text = "";
            txtEncargadoCargo.Text = "";
        }
        protected override void OnCambioModo(bool esEdicion)
        {
            base.OnCambioModo(esEdicion);
            // Siempre mantener oculto el botón de eliminar en el módulo de Cliente
            btnEliminar.Visible = false;
        }
        #endregion

        #region Eventos
        private void CmbTipoPersona_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Filtrar automáticamente cuando cambie el tipo
            OnBuscar();
        }

        private void CmbTipoPersonaForm_SelectedIndexChanged(object sender, EventArgs e)
        {
            string tipoSeleccionado = cmbTipoPersonaForm.SelectedItem?.ToString() ?? "Física";
            MostrarCamposSegunTipo(tipoSeleccionado);
        }
        #endregion

        #region Métodos Auxiliares
        private void CargarDatos()
        {
            try
            {
                DataTable datos = NPersona.Mostrar();
                base.CargarDatos(datos);
                PersonalizarColumnasClientes();
                ActualizarContadorRegistros(datos.Rows.Count);
            }
            catch (Exception ex)
            {
                MostrarMensaje($"Error al cargar datos: {ex.Message}", "Error", MessageBoxIcon.Error);
            }
        }

        private void PersonalizarColumnasClientes()
        {
            if (dgvDatos?.DataSource == null) return;

            try
            {
                foreach (DataGridViewColumn column in dgvDatos.Columns)
                {
                    if (column == null || string.IsNullOrEmpty(column.Name))
                        continue;

                    // Evitar configurar las columnas de botones
                    if (column.Name == "btnEditar" || column.Name == "btnEliminar")
                    {
                        column.Width = 80; // Reducir el ancho de los botones
                        continue;
                    }

                    switch (column.Name.ToLower())
                    {
                        case "id":
                            column.HeaderText = "ID";
                            column.Width = 40;
                            column.Visible = false;
                            break;
                        case "nombre":
                            column.HeaderText = "Nombre";
                            column.Width = 80;
                            break;
                        case "apellido":
                            column.HeaderText = "Apellido";
                            column.Width = 80;
                            break;
                        case "email":
                            column.HeaderText = "Email";
                            column.Width = 120;
                            break;
                        case "usuario":
                            column.HeaderText = "Usuario";
                            column.Width = 60;
                            break;
                        case "telefono":
                            column.HeaderText = "Teléfono";
                            column.Width = 80;
                            break;
                        case "direccion":
                            column.HeaderText = "Dirección";
                            column.Width = 100;
                            break;
                        case "fecha_nacimiento":
                            column.HeaderText = "F. Nac.";
                            column.Width = 70;
                            break;
                        case "salario":
                            column.HeaderText = "Salario";
                            column.Width = 70;
                            column.DefaultCellStyle.Format = "C0";
                            column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                            break;
                        case "rol":
                            column.HeaderText = "Rol";
                            column.Width = 60;
                            break;
                        case "activo":
                            column.HeaderText = "Activo";
                            column.Width = 50;
                            break;
                        case "fecha_ultimo_acceso":
                            column.HeaderText = "Último Acc.";
                            column.Width = 80;
                            break;
                        case "licencia":
                            column.HeaderText = "Licencia";
                            column.Width = 60;
                            break;
                        case "especialidad":
                            column.HeaderText = "Especialidad";
                            column.Width = 80;
                            break;
                        case "universidad":
                            column.HeaderText = "Universidad";
                            column.Width = 80;
                            break;
                        case "anos_experiencia":
                            column.HeaderText = "Años Exp.";
                            column.Width = 60;
                            break;
                        case "area":
                            column.HeaderText = "Área";
                            column.Width = 60;
                            break;
                        case "tipo":
                            column.HeaderText = "Tipo";
                            column.Width = 60;
                            break;
                        case "ci":
                            column.HeaderText = "CI";
                            column.Width = 70;
                            break;
                        case "genero":
                            column.HeaderText = "Género";
                            column.Width = 50;
                            break;
                        case "razon_social":
                            column.HeaderText = "Razón Social";
                            column.Width = 100;
                            break;
                        case "nit":
                            column.HeaderText = "NIT";
                            column.Width = 70;
                            break;
                        case "encargado_nombre":
                            column.HeaderText = "Encargado";
                            column.Width = 80;
                            break;
                        case "encargado_cargo":
                            column.HeaderText = "Cargo Enc.";
                            column.Width = 70;
                            break;
                    }
                }

                // No usar AutoSizeColumnsMode.Fill para evitar que se expandan demasiado
                dgvDatos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
                
                // Habilitar scroll horizontal si es necesario
                dgvDatos.ScrollBars = ScrollBars.Both;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error en PersonalizarColumnasClientes: {ex.Message}");
            }
        }

        private void MostrarCamposSegunTipo(string tipo)
        {
            _tipoPersonaSeleccionado = tipo;

            // Suspender el layout para evitar parpadeo
            panelFormulario.SuspendLayout();

            if (tipo == "Física")
            {
                // Mostrar campos de persona física con transición suave
                grpPersonaJuridica.Visible = false;
                grpPersonaFisica.Visible = true;
                grpPersonaFisica.BringToFront();
                grpPersonaJuridica.SendToBack();
                // Configurar colores modernos para campos obligatorios
                lblNombre.ForeColor = Color.FromArgb(231, 76, 60);
                lblApellido.ForeColor = Color.FromArgb(231, 76, 60);
                lblRazonSocial.ForeColor = Color.FromArgb(52, 73, 94); // Reset

                // Enfocar primer campo
                txtCi.Focus();
            }
            else
            {
                // Mostrar campos de persona jurídica con transición suave
                grpPersonaFisica.Visible = false;
                grpPersonaJuridica.Visible = true;
                grpPersonaJuridica.BringToFront();
                grpPersonaFisica.SendToBack();
                // Configurar colores modernos para campos obligatorios
                lblRazonSocial.ForeColor = Color.FromArgb(231, 76, 60);
                lblNombre.ForeColor = Color.FromArgb(52, 73, 94); // Reset
                lblApellido.ForeColor = Color.FromArgb(52, 73, 94); // Reset

                // Enfocar primer campo
                txtRazonSocial.Focus();
            }

            // Reanudar el layout y actualizar
            panelFormulario.ResumeLayout(true);
            panelFormulario.Refresh();

            // Mostrar mensaje informativo
            MostrarMensajeInformativo(tipo);
        }

        private void MostrarMensajeInformativo(string tipo)
        {
            string mensaje = tipo == "Física" 
                ? "Complete los datos de la persona física. Los campos marcados con * son obligatorios."
                : "Complete los datos de la persona jurídica. La razón social es obligatoria.";
            
            // Podrías mostrar esto en un panel de información si lo deseas
            // Por ahora solo actualizamos el título del formulario
            if (panelFormulario.Parent is TabPage tabPage)
            {
                tabPage.Text = $"Configuración de Persona {tipo}";
            }
        }

        private bool ValidarCampos()
        {
            string tipoPersona = cmbTipoPersonaForm.SelectedItem?.ToString() ?? "Física";

            if (tipoPersona == "Física")
            {
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
            }
            else
            {
                if (string.IsNullOrWhiteSpace(txtRazonSocial.Text))
                {
                    MostrarMensaje("La razón social es obligatoria", "Validación", MessageBoxIcon.Warning);
                    txtRazonSocial.Focus();
                    return false;
                }
            }

            // Validar email si se proporciona
            if (!string.IsNullOrWhiteSpace(txtEmail.Text) && !NPersona.ValidarEmail(txtEmail.Text))
            {
                MostrarMensaje("El formato del email no es válido", "Validación", MessageBoxIcon.Warning);
                txtEmail.Focus();
                return false;
            }

            return true;
        }

        private char? ObtenerGeneroSeleccionado()
        {
            if (cmbGenero.SelectedIndex > 0)
            {
                return cmbGenero.SelectedItem?.ToString()?[0];
            }
            return null;
        }

        private void ActualizarContadorRegistros(int cantidad)
        {
            lblContador.Text = $"Total de registros: {cantidad}";
        }
        #endregion

        #region Métodos de Validación y Efectos Visuales
        private void ValidarEmailEnTiempoReal()
        {
            if (string.IsNullOrWhiteSpace(txtEmail.Text)) return;
            
            bool esValido = NPersona.ValidarEmail(txtEmail.Text);
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
                        textBox.BackColor = NPersona.ValidarEmail(textBox.Text) ? colorValido : colorInvalido;
                    }
                    else
                    {
                        textBox.BackColor = colorNormal;
                    }
                    break;
                case "telefono":
                case "ci":
                case "nit":
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
                label.ForeColor = Color.FromArgb(231, 76, 60);
            }
            else
            {
                textBox.BackColor = colorValido;
                label.ForeColor = Color.FromArgb(39, 174, 96);
            }
        }

        private void ConfigurarEfectoHover(Button boton)
        {
            Color colorOriginal = boton.BackColor;
            Color colorHover = Color.FromArgb(52, 152, 219);

            boton.MouseEnter += (s, e) => boton.BackColor = colorHover;
            boton.MouseLeave += (s, e) => boton.BackColor = colorOriginal;
        }

        private void AplicarEfectosFocusTextBox(TextBox textBox)
        {
            Color colorFocus = Color.FromArgb(52, 152, 219);
            Color colorNormal = Color.FromArgb(149, 165, 166);

            textBox.Enter += (s, e) => 
            {
                textBox.BorderStyle = BorderStyle.FixedSingle;
                ((TextBox)s).Parent.Refresh();
            };

            textBox.Leave += (s, e) => 
            {
                ((TextBox)s).Parent.Refresh();
            };
        }
        #endregion
    }
}
