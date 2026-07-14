using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using CapaNegocio;
using SistemVeterinario.Navigation;

namespace SistemVeterinario.Forms
{
    public partial class HistorialModule : BaseModulos
    {
        #region Propiedades y Variables
        private NHistorial nHistorial;
        private DataTable dtHistoriales;
        private DataTable dtDetalles;
        private bool isLoadingData = false;
        private int historialActualId = 0;
        private int detalleActualId = 0;
        private bool mostrandoFormularioDetalle = false;
        
        // Enum para los modos específicos del módulo
        public enum ModoOperacion
        {
            Consulta = 1,
            NuevoHistorial = 2,
            EditarHistorial = 3,
            VerDetalles = 4
        }
        #endregion

        #region Constructor y Inicialización
        public HistorialModule()
        {
            InitializeComponent();
            nHistorial = new NHistorial();
            ConfigurarModulo();
            ConfigurarEventos();
            CargarDatosIniciales();
        }

        private void ConfigurarModulo()
        {
            // Configurar modos específicos
            ConfigurarModos();
            
            // Configurar controles específicos
            ConfigurarControles();
            
            // Aplicar tema visual
            AplicarTemaVisual();
            
            // Configurar dgvDatos para que ocupe todo el ancho
            ConfigurarDataGridView();
        }

        private void ConfigurarModos()
        {
            // Limpiar modos existentes y agregar los específicos
            cmbModo.Items.Clear();
            cmbModo.Items.Add("Consulta");
            cmbModo.Items.Add("Nuevo Historial");
            cmbModo.Items.Add("Editar Historial");
            cmbModo.Items.Add("Ver Detalles");
            cmbModo.SelectedIndex = 0;
        }

        private void ConfigurarControles()
        {
            // Configurar ComboBoxes
            CargarAnimales();
            CargarVeterinarios();
            CargarTiposRegistro();
            
            // Configurar DateTimePickers
            dtpFechaCreacion.Enabled = false;
            dtpFechaActualizacion.Enabled = false;
            dtpFechaRegistro.Value = DateTime.Now;
            
            // Configurar TextBox multilínea
            txtNotasGenerales.AcceptsReturn = true;
            txtNotasGenerales.AcceptsTab = true;
            txtNotasGenerales.WordWrap = true;
            txtNotasGenerales.ScrollBars = ScrollBars.Vertical;
            
            txtAlergias.AcceptsReturn = true;
            txtAlergias.WordWrap = true;
            txtAlergias.ScrollBars = ScrollBars.Vertical;
            
            txtCondicionesMedicas.AcceptsReturn = true;
            txtCondicionesMedicas.WordWrap = true;
            txtCondicionesMedicas.ScrollBars = ScrollBars.Vertical;
            
            txtDescripcionDetalle.AcceptsReturn = true;
            txtDescripcionDetalle.WordWrap = true;
            txtDescripcionDetalle.ScrollBars = ScrollBars.Vertical;
            
            txtTratamiento.AcceptsReturn = true;
            txtTratamiento.WordWrap = true;
            txtTratamiento.ScrollBars = ScrollBars.Vertical;
            
            txtMedicamentos.AcceptsReturn = true;
            txtMedicamentos.WordWrap = true;
            txtMedicamentos.ScrollBars = ScrollBars.Vertical;
            
            txtObservaciones.AcceptsReturn = true;
            txtObservaciones.WordWrap = true;
            txtObservaciones.ScrollBars = ScrollBars.Vertical;
            
            // Configurar validaciones de longitud
            txtNotasGenerales.MaxLength = 2000;
            txtAlergias.MaxLength = 500;
            txtCondicionesMedicas.MaxLength = 500;
            txtDescripcionDetalle.MaxLength = 1000;
            txtTratamiento.MaxLength = 500;
            txtMedicamentos.MaxLength = 500;
            txtObservaciones.MaxLength = 1000;
            
            // Configurar DataGridView
            ConfigurarDataGridViewDetalles();
        }

        private void ConfigurarDataGridViewDetalles()
        {
            dgvDetalles.AutoGenerateColumns = false;
            dgvDetalles.AllowUserToAddRows = false;
            dgvDetalles.AllowUserToDeleteRows = false;
            dgvDetalles.ReadOnly = true;
            dgvDetalles.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvDetalles.MultiSelect = false;
            
            // Configurar columnas
            dgvDetalles.Columns.Clear();
            dgvDetalles.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "id",
                HeaderText = "ID",
                DataPropertyName = "id",
                Width = 60,
                Visible = false
            });
            dgvDetalles.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "tipo_registro",
                HeaderText = "Tipo",
                DataPropertyName = "tipo_registro",
                Width = 100
            });
            dgvDetalles.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "fecha_registro",
                HeaderText = "Fecha",
                DataPropertyName = "fecha_registro",
                Width = 100,
                DefaultCellStyle = { Format = "dd/MM/yyyy" }
            });
            dgvDetalles.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "veterinario_nombre",
                HeaderText = "Veterinario",
                DataPropertyName = "veterinario_nombre",
                Width = 150
            });
            dgvDetalles.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "descripcion",
                HeaderText = "Descripción",
                DataPropertyName = "descripcion",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });
        }

        private void ConfigurarEventos()
        {
            // Eventos específicos del módulo
            cmbAnimalFiltro.SelectedIndexChanged += CmbAnimalFiltro_SelectedIndexChanged;
            btnVerDetalles.Click += BtnVerDetalles_Click;
            btnBuscarAnimal.Click += BtnBuscarAnimal_Click;
            
            // Eventos para gestión de detalles
            btnNuevoDetalle.Click += BtnNuevoDetalle_Click;
            btnEliminarDetalle.Click += BtnEliminarDetalle_Click;
            btnGuardarDetalle.Click += BtnGuardarDetalle_Click;
            btnCancelarDetalle.Click += BtnCancelarDetalle_Click;
            dgvDetalles.SelectionChanged += DgvDetalles_SelectionChanged;
            
            // Eventos de validación
            txtNotasGenerales.TextChanged += TxtNotasGenerales_TextChanged;
            cmbAnimal.SelectedIndexChanged += CmbAnimal_SelectedIndexChanged;
        }

        private void AplicarTemaVisual()
        {
            // Aplicar colores y estilos consistentes
            this.BackColor = Color.FromArgb(248, 249, 250);
            
            // Configurar colores de los grupos
            grpDatosHistorial.ForeColor = Color.FromArgb(52, 73, 94);
            grpDetallesHistorial.ForeColor = Color.FromArgb(52, 73, 94);
            grpDatosDetalle.ForeColor = Color.FromArgb(52, 73, 94);
        }

        private void ConfigurarDataGridView()
        {
            // Ajustar el dgvDatos para que ocupe todo el ancho como en MascotasModule
            dgvDatos.Size = new Size(1121, 400);
            dgvDatos.Location = new Point(10, 150);
            
            // Configurar para que las columnas se distribuyan automáticamente
            dgvDatos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            
            // Configurar alineación central para encabezados
            dgvDatos.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            
            // Mejorar la apariencia
            dgvDatos.AllowUserToResizeColumns = true;
            dgvDatos.AllowUserToResizeRows = false;
            
            // Configurar el estilo de las celdas para mejor presentación
            dgvDatos.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvDatos.DefaultCellStyle.WrapMode = DataGridViewTriState.False;
        }
        #endregion

        #region Carga de Datos
        private void CargarDatosIniciales()
        {
            try
            {
                isLoadingData = true;
                CargarHistoriales();
                ActualizarContador();
                LimpiarFormulario();
            }
            catch (Exception ex)
            {
                MostrarError("Error al cargar datos iniciales: " + ex.Message);
            }
            finally
            {
                isLoadingData = false;
            }
        }

        private void CargarAnimales()
        {
            try
            {
                DataTable dtAnimales = nHistorial.ObtenerAnimales();
                
                // Cargar ComboBox principal
                cmbAnimal.DataSource = dtAnimales.Copy();
                cmbAnimal.DisplayMember = "display_text";
                cmbAnimal.ValueMember = "id";
                cmbAnimal.SelectedIndex = -1;
                
                // Cargar ComboBox de filtro
                DataTable dtFiltro = dtAnimales.Copy();
                DataRow rowTodos = dtFiltro.NewRow();
                rowTodos["id"] = 0;
                rowTodos["display_text"] = "🐕 Todos los animales";
                dtFiltro.Rows.InsertAt(rowTodos, 0);
                
                cmbAnimalFiltro.DataSource = dtFiltro;
                cmbAnimalFiltro.DisplayMember = "display_text";
                cmbAnimalFiltro.ValueMember = "id";
                cmbAnimalFiltro.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MostrarError("Error al cargar animales: " + ex.Message);
            }
        }

        private void CargarVeterinarios()
        {
            try
            {
                DataTable dtVeterinarios = nHistorial.ObtenerVeterinarios();
                
                DataRow rowVacio = dtVeterinarios.NewRow();
                rowVacio["id"] = DBNull.Value;
                rowVacio["display_text"] = "Sin veterinario asignado";
                dtVeterinarios.Rows.InsertAt(rowVacio, 0);
                
                cmbVeterinario.DataSource = dtVeterinarios;
                cmbVeterinario.DisplayMember = "display_text";
                cmbVeterinario.ValueMember = "id";
                cmbVeterinario.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MostrarError("Error al cargar veterinarios: " + ex.Message);
            }
        }

        private void CargarTiposRegistro()
        {
            try
            {
                List<string> tipos = nHistorial.ObtenerTiposRegistro();
                cmbTipoRegistro.Items.Clear();
                cmbTipoRegistro.Items.AddRange(tipos.ToArray());
                if (cmbTipoRegistro.Items.Count > 0)
                    cmbTipoRegistro.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MostrarError("Error al cargar tipos de registro: " + ex.Message);
            }
        }

        private void CargarHistoriales(int? animalId = null, string buscar = null)
        {
            try
            {
                dtHistoriales = nHistorial.ListarHistoriales(animalId, buscar);
                dgvDatos.DataSource = dtHistoriales;
                ConfigurarColumnasHistoriales();
                ActualizarContador();
            }
            catch (Exception ex)
            {
                MostrarError("Error al cargar historiales: " + ex.Message);
                dtHistoriales = new DataTable();
                dgvDatos.DataSource = dtHistoriales;
            }
        }

        private void ConfigurarColumnasHistoriales()
        {
            if (dgvDatos.Columns.Count > 0)
            {
                // Primero configurar el DataGridView para que ocupe todo el ancho
                dgvDatos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
                
                // Ocultar ID
                if (dgvDatos.Columns["id"] != null)
                    dgvDatos.Columns["id"].Visible = false;
                if (dgvDatos.Columns["animal_id"] != null)
                    dgvDatos.Columns["animal_id"].Visible = false;

                // Configurar encabezados y anchos proporcionales para ocupar exactamente el 100% del espacio
                int totalWidth = 1121; // Ancho total del DataGridView
                
                if (dgvDatos.Columns["animal_nombre"] != null)
                {
                    dgvDatos.Columns["animal_nombre"].HeaderText = "🐾 Animal";
                    dgvDatos.Columns["animal_nombre"].Width = (int)(totalWidth * 0.16); // 16%
                    dgvDatos.Columns["animal_nombre"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                    dgvDatos.Columns["animal_nombre"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }
                if (dgvDatos.Columns["tipo_animal"] != null)
                {
                    dgvDatos.Columns["tipo_animal"].HeaderText = "📋 Tipo";
                    dgvDatos.Columns["tipo_animal"].Width = (int)(totalWidth * 0.10); // 10%
                    dgvDatos.Columns["tipo_animal"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    dgvDatos.Columns["tipo_animal"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }
                if (dgvDatos.Columns["propietario_nombre"] != null)
                {
                    dgvDatos.Columns["propietario_nombre"].HeaderText = "👤 Propietario";
                    dgvDatos.Columns["propietario_nombre"].Width = (int)(totalWidth * 0.25); // 25%
                    dgvDatos.Columns["propietario_nombre"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                    dgvDatos.Columns["propietario_nombre"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }
                if (dgvDatos.Columns["notas_generales"] != null)
                {
                    dgvDatos.Columns["notas_generales"].HeaderText = "📝 Notas Generales";
                    dgvDatos.Columns["notas_generales"].Width = (int)(totalWidth * 0.24); // 24%
                    dgvDatos.Columns["notas_generales"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                    dgvDatos.Columns["notas_generales"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }
                if (dgvDatos.Columns["alergias"] != null)
                {
                    dgvDatos.Columns["alergias"].HeaderText = "⚠️ Alergias";
                    dgvDatos.Columns["alergias"].Width = (int)(totalWidth * 0.12); // 12%
                    dgvDatos.Columns["alergias"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                    dgvDatos.Columns["alergias"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }
                if (dgvDatos.Columns["condiciones_medicas"] != null)
                {
                    dgvDatos.Columns["condiciones_medicas"].HeaderText = "🏥 Condiciones";
                    dgvDatos.Columns["condiciones_medicas"].Width = (int)(totalWidth * 0.12); // 12%
                    dgvDatos.Columns["condiciones_medicas"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                    dgvDatos.Columns["condiciones_medicas"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }
                if (dgvDatos.Columns["created_at"] != null)
                {
                    dgvDatos.Columns["created_at"].HeaderText = "📅 Creado";
                    dgvDatos.Columns["created_at"].Width = (int)(totalWidth * 0.10); // 10%
                    dgvDatos.Columns["created_at"].DefaultCellStyle.Format = "dd/MM/yyyy";
                    dgvDatos.Columns["created_at"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    dgvDatos.Columns["created_at"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }
                if (dgvDatos.Columns["updated_at"] != null)
                {
                    dgvDatos.Columns["updated_at"].HeaderText = "🔄 Actualizado";
                    dgvDatos.Columns["updated_at"].Width = (int)(totalWidth * 0.10); // 10%
                    dgvDatos.Columns["updated_at"].DefaultCellStyle.Format = "dd/MM/yyyy";
                    dgvDatos.Columns["updated_at"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    dgvDatos.Columns["updated_at"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }
            }
        }

        private void CargarDetallesHistorial(int historialId)
        {
            try
            {
                if (historialId <= 0)
                {
                    dtDetalles = new DataTable();
                    dgvDetalles.DataSource = dtDetalles;
                    return;
                }

                dtDetalles = nHistorial.ListarDetallesHistorial(historialId);
                dgvDetalles.DataSource = dtDetalles;
                historialActualId = historialId;
            }
            catch (Exception ex)
            {
                MostrarError("Error al cargar detalles del historial: " + ex.Message);
                dtDetalles = new DataTable();
                dgvDetalles.DataSource = dtDetalles;
            }
        }
        #endregion

        #region Métodos Override de BaseModulos
        protected override void OnLoad()
        {
            base.OnLoad();
        }

        protected override void OnNuevo()
        {
            try
            {
                LimpiarFormulario();
                HabilitarControles(true);
                EstablecerModo(ModoOperacion.NuevoHistorial);
                cmbAnimal.Focus();
            }
            catch (Exception ex)
            {
                MostrarError("Error al crear nuevo historial: " + ex.Message);
            }
        }

        protected override void OnEditar(DataGridViewRow row)
        {
            try
            {
                if (row == null || row.Cells["id"].Value == null)
                {
                    MostrarAdvertencia("Seleccione un historial para editar.");
                    return;
                }

                IdSeleccionado = Convert.ToInt32(row.Cells["id"].Value);
                CargarDatosParaEdicion(IdSeleccionado);
                HabilitarControles(true);
                EstablecerModo(ModoOperacion.EditarHistorial);
                cmbAnimal.Focus();
            }
            catch (Exception ex)
            {
                MostrarError("Error al editar historial: " + ex.Message);
            }
        }

        protected override void OnGuardar()
        {
            try
            {
                if (!ValidarDatos())
                    return;

                bool resultado = false;
                ModoOperacion modoActual = (ModoOperacion)cmbModo.SelectedIndex + 1;

                if (modoActual == ModoOperacion.NuevoHistorial)
                {
                    resultado = nHistorial.InsertarHistorial(
                        Convert.ToInt32(cmbAnimal.SelectedValue),
                        txtNotasGenerales.Text.Trim(),
                        txtAlergias.Text.Trim(),
                        txtCondicionesMedicas.Text.Trim()
                    );

                    if (resultado)
                        MostrarExito("Historial creado exitosamente.");
                }
                else if (modoActual == ModoOperacion.EditarHistorial)
                {
                    resultado = nHistorial.ActualizarHistorial(
                        IdSeleccionado,
                        Convert.ToInt32(cmbAnimal.SelectedValue),
                        txtNotasGenerales.Text.Trim(),
                        txtAlergias.Text.Trim(),
                        txtCondicionesMedicas.Text.Trim()
                    );

                    if (resultado)
                        MostrarExito("Historial actualizado exitosamente.");
                }

                if (resultado)
                {
                    CargarHistoriales();
                    OnCancelar();
                }
                else
                {
                    MostrarError("No se pudo guardar el historial.");
                }
            }
            catch (Exception ex)
            {
                MostrarError("Error al guardar: " + ex.Message);
            }
        }

        protected override void OnEliminarFila(DataGridViewRow row)
        {
            try
            {
                if (row == null || row.Cells["id"].Value == null)
                {
                    MostrarAdvertencia("Seleccione un historial para eliminar.");
                    return;
                }

                int id = Convert.ToInt32(row.Cells["id"].Value);
                
                var result = MessageBox.Show(
                    "¿Está seguro de que desea eliminar este historial médico?\n\nEsta acción no se puede deshacer.",
                    "Confirmar Eliminación",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning
                );

                if (result == DialogResult.Yes)
                {
                    bool resultado = nHistorial.EliminarHistorial(id);
                    
                    if (resultado)
                    {
                        MostrarExito("Historial eliminado exitosamente.");
                        CargarHistoriales();
                        LimpiarFormulario();
                    }
                    else
                    {
                        MostrarError("No se pudo eliminar el historial.");
                    }
                }
            }
            catch (Exception ex)
            {
                MostrarError("Error al eliminar: " + ex.Message);
            }
        }

        protected override void OnBuscar()
        {
            try
            {
                string textoBusqueda = txtBuscar.Text.Trim();
                int? animalFiltro = null;
                
                if (cmbAnimalFiltro.SelectedValue != null && Convert.ToInt32(cmbAnimalFiltro.SelectedValue) > 0)
                {
                    animalFiltro = Convert.ToInt32(cmbAnimalFiltro.SelectedValue);
                }

                if (!string.IsNullOrEmpty(textoBusqueda) || animalFiltro.HasValue)
                {
                    CargarHistoriales(animalFiltro, textoBusqueda);
                }
                else
                {
                    CargarHistoriales();
                }
            }
            catch (Exception ex)
            {
                MostrarError("Error al buscar: " + ex.Message);
            }
        }

        protected override void OnCancelar()
        {
            try
            {
                LimpiarFormulario();
                HabilitarControles(false);
                EstablecerModo(ModoOperacion.Consulta);
                OcultarFormularioDetalle();
                IdSeleccionado = 0;
                ModoEdicion = false;
            }
            catch (Exception ex)
            {
                MostrarError("Error al cancelar: " + ex.Message);
            }
        }

        protected override void LimpiarFormulario()
        {
            if (!isLoadingData)
            {
                txtId.Clear();
                cmbAnimal.SelectedIndex = -1;
                txtNotasGenerales.Clear();
                txtAlergias.Clear();
                txtCondicionesMedicas.Clear();
                dtpFechaCreacion.Value = DateTime.Now;
                dtpFechaActualizacion.Value = DateTime.Now;
                
                LimpiarFormularioDetalle();
                historialActualId = 0;
                detalleActualId = 0;
            }
        }

        protected override void CargarDatosParaEdicion(int id)
        {
            try
            {
                if (id <= 0) return;

                DataTable dtHistorial = nHistorial.ObtenerHistorialPorId(id);
                if (dtHistorial.Rows.Count > 0)
                {
                    DataRow row = dtHistorial.Rows[0];
                    
                    txtId.Text = row["id"].ToString();
                    cmbAnimal.SelectedValue = row["animal_id"];
                    txtNotasGenerales.Text = row["notas_generales"].ToString();
                    txtAlergias.Text = row["alergias"].ToString();
                    txtCondicionesMedicas.Text = row["condiciones_medicas"].ToString();
                    
                    if (row["created_at"] != DBNull.Value)
                        dtpFechaCreacion.Value = Convert.ToDateTime(row["created_at"]);
                    if (row["updated_at"] != DBNull.Value)
                        dtpFechaActualizacion.Value = Convert.ToDateTime(row["updated_at"]);
                }
            }
            catch (Exception ex)
            {
                MostrarError("Error al cargar datos para editar: " + ex.Message);
            }
        }
        #endregion

        #region Métodos Auxiliares
        private void EstablecerModo(ModoOperacion modo)
        {
            cmbModo.SelectedIndex = (int)modo - 1;
            
            switch (modo)
            {
                case ModoOperacion.Consulta:
                    tabControlPrincipal.SelectedTab = tabInicio;
                    break;
                case ModoOperacion.NuevoHistorial:
                case ModoOperacion.EditarHistorial:
                    tabControlPrincipal.SelectedTab = tabConfiguraciones;
                    break;
                case ModoOperacion.VerDetalles:
                    tabControlPrincipal.SelectedTab = tabConfiguraciones;
                    MostrarFormularioDetalle();
                    break;
            }
        }

        private void LimpiarFormularioDetalle()
        {
            cmbTipoRegistro.SelectedIndex = -1;
            txtDescripcionDetalle.Clear();
            dtpFechaRegistro.Value = DateTime.Now;
            cmbVeterinario.SelectedIndex = 0;
            txtTratamiento.Clear();
            txtMedicamentos.Clear();
            txtObservaciones.Clear();
        }

        private void HabilitarControles(bool habilitar)
        {
            cmbAnimal.Enabled = habilitar;
            txtNotasGenerales.Enabled = habilitar;
            txtAlergias.Enabled = habilitar;
            txtCondicionesMedicas.Enabled = habilitar;
            btnBuscarAnimal.Enabled = habilitar;
        }

        private void CargarDatosParaEditar()
        {
            try
            {
                int id = ObtenerIdSeleccionado();
                if (id <= 0) return;

                DataTable dtHistorial = nHistorial.ObtenerHistorialPorId(id);
                if (dtHistorial.Rows.Count > 0)
                {
                    DataRow row = dtHistorial.Rows[0];
                    
                    txtId.Text = row["id"].ToString();
                    cmbAnimal.SelectedValue = row["animal_id"];
                    txtNotasGenerales.Text = row["notas_generales"].ToString();
                    txtAlergias.Text = row["alergias"].ToString();
                    txtCondicionesMedicas.Text = row["condiciones_medicas"].ToString();
                    
                    if (row["created_at"] != DBNull.Value)
                        dtpFechaCreacion.Value = Convert.ToDateTime(row["created_at"]);
                    if (row["updated_at"] != DBNull.Value)
                        dtpFechaActualizacion.Value = Convert.ToDateTime(row["updated_at"]);
                }
            }
            catch (Exception ex)
            {
                MostrarError("Error al cargar datos para editar: " + ex.Message);
            }
        }

        private int ObtenerIdSeleccionado()
        {
            try
            {
                if (dgvDatos.SelectedRows.Count > 0)
                {
                    return Convert.ToInt32(dgvDatos.SelectedRows[0].Cells["id"].Value);
                }
                return 0;
            }
            catch
            {
                return 0;
            }
        }

        private bool ValidarDatos()
        {
            // Validar animal seleccionado
            if (cmbAnimal.SelectedIndex == -1 || cmbAnimal.SelectedValue == null)
            {
                MostrarAdvertencia("Debe seleccionar un animal.");
                cmbAnimal.Focus();
                return false;
            }

            // Validar notas generales
            if (string.IsNullOrWhiteSpace(txtNotasGenerales.Text))
            {
                MostrarAdvertencia("Las notas generales son obligatorias.");
                txtNotasGenerales.Focus();
                return false;
            }

            if (txtNotasGenerales.Text.Trim().Length > 2000)
            {
                MostrarAdvertencia("Las notas generales no pueden superar los 2000 caracteres.");
                txtNotasGenerales.Focus();
                return false;
            }

            if (!string.IsNullOrEmpty(txtAlergias.Text) && txtAlergias.Text.Trim().Length > 500)
            {
                MostrarAdvertencia("Las alergias no pueden superar los 500 caracteres.");
                txtAlergias.Focus();
                return false;
            }

            if (!string.IsNullOrEmpty(txtCondicionesMedicas.Text) && txtCondicionesMedicas.Text.Trim().Length > 500)
            {
                MostrarAdvertencia("Las condiciones médicas no pueden superar los 500 caracteres.");
                txtCondicionesMedicas.Focus();
                return false;
            }

            return true;
        }

        private bool ValidarDatosDetalle()
        {
            if (cmbTipoRegistro.SelectedIndex == -1)
            {
                MostrarAdvertencia("Debe seleccionar un tipo de registro.");
                cmbTipoRegistro.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtDescripcionDetalle.Text))
            {
                MostrarAdvertencia("La descripción es obligatoria.");
                txtDescripcionDetalle.Focus();
                return false;
            }

            if (dtpFechaRegistro.Value > DateTime.Now)
            {
                MostrarAdvertencia("La fecha de registro no puede ser futura.");
                dtpFechaRegistro.Focus();
                return false;
            }

            return true;
        }

        private void ActualizarContador()
        {
            if (dtHistoriales != null)
            {
                lblContador.Text = $"📊 Total: {dtHistoriales.Rows.Count} registros";
            }
            else
            {
                lblContador.Text = "📊 Total: 0 registros";
            }
        }

        private void MostrarFormularioDetalle()
        {
            grpDatosDetalle.Visible = true;
            mostrandoFormularioDetalle = true;
        }

        private void OcultarFormularioDetalle()
        {
            grpDatosDetalle.Visible = false;
            mostrandoFormularioDetalle = false;
            LimpiarFormularioDetalle();
        }
        #endregion

        #region Event Handlers
        private void CmbAnimalFiltro_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!isLoadingData)
            {
                OnBuscar();
            }
        }

        private void BtnVerDetalles_Click(object sender, EventArgs e)
        {
            try
            {
                int id = ObtenerIdSeleccionado();
                if (id <= 0)
                {
                    MostrarAdvertencia("Seleccione un historial para ver sus detalles.");
                    return;
                }

                CargarDetallesHistorial(id);
                EstablecerModo(ModoOperacion.VerDetalles);
            }
            catch (Exception ex)
            {
                MostrarError("Error al ver detalles: " + ex.Message);
            }
        }

        private void BtnBuscarAnimal_Click(object sender, EventArgs e)
        {
            try
            {
                // Aquí podríamos abrir un diálogo de búsqueda de animales más avanzado
                MostrarInformacion("Funcionalidad de búsqueda avanzada de animales por implementar.");
            }
            catch (Exception ex)
            {
                MostrarError("Error en búsqueda de animal: " + ex.Message);
            }
        }

        private void BtnNuevoDetalle_Click(object sender, EventArgs e)
        {
            try
            {
                if (historialActualId <= 0)
                {
                    MostrarAdvertencia("Debe seleccionar un historial primero.");
                    return;
                }

                LimpiarFormularioDetalle();
                MostrarFormularioDetalle();
                cmbTipoRegistro.Focus();
            }
            catch (Exception ex)
            {
                MostrarError("Error al crear nuevo detalle: " + ex.Message);
            }
        }

        private void BtnEliminarDetalle_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvDetalles.SelectedRows.Count == 0)
                {
                    MostrarAdvertencia("Seleccione un detalle para eliminar.");
                    return;
                }

                int detalleId = Convert.ToInt32(dgvDetalles.SelectedRows[0].Cells["id"].Value);
                
                var result = MessageBox.Show(
                    "¿Está seguro de que desea eliminar este detalle del historial?",
                    "Confirmar Eliminación",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning
                );

                if (result == DialogResult.Yes)
                {
                    bool resultado = nHistorial.EliminarDetalleHistorial(detalleId);
                    
                    if (resultado)
                    {
                        MostrarExito("Detalle eliminado exitosamente.");
                        CargarDetallesHistorial(historialActualId);
                    }
                    else
                    {
                        MostrarError("No se pudo eliminar el detalle.");
                    }
                }
            }
            catch (Exception ex)
            {
                MostrarError("Error al eliminar detalle: " + ex.Message);
            }
        }

        private void BtnGuardarDetalle_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidarDatosDetalle())
                    return;

                if (historialActualId <= 0)
                {
                    MostrarAdvertencia("Error: No hay historial seleccionado.");
                    return;
                }

                int? veterinarioId = null;
                if (cmbVeterinario.SelectedValue != null && cmbVeterinario.SelectedValue != DBNull.Value)
                {
                    veterinarioId = Convert.ToInt32(cmbVeterinario.SelectedValue);
                }

                bool resultado = nHistorial.InsertarDetalleHistorial(
                    historialActualId,
                    cmbTipoRegistro.SelectedItem.ToString(),
                    txtDescripcionDetalle.Text.Trim(),
                    dtpFechaRegistro.Value,
                    veterinarioId,
                    txtTratamiento.Text.Trim(),
                    txtMedicamentos.Text.Trim(),
                    txtObservaciones.Text.Trim()
                );

                if (resultado)
                {
                    MostrarExito("Detalle guardado exitosamente.");
                    CargarDetallesHistorial(historialActualId);
                    OcultarFormularioDetalle();
                }
                else
                {
                    MostrarError("No se pudo guardar el detalle.");
                }
            }
            catch (Exception ex)
            {
                MostrarError("Error al guardar detalle: " + ex.Message);
            }
        }

        private void BtnCancelarDetalle_Click(object sender, EventArgs e)
        {
            OcultarFormularioDetalle();
        }

        private void DgvDetalles_SelectionChanged(object sender, EventArgs e)
        {
            // Opcional: Cargar datos del detalle seleccionado para edición
        }

        private void TxtNotasGenerales_TextChanged(object sender, EventArgs e)
        {
            // Mostrar contador de caracteres
            int remaining = 2000 - txtNotasGenerales.Text.Length;
            // Aquí podrías mostrar el contador si tuvieras un label para ello
        }

        private void CmbAnimal_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Opcional: Validar si el animal ya tiene historial
            if (!isLoadingData && cmbAnimal.SelectedValue != null && cmbModo.SelectedIndex == 1) // Nuevo
            {
                try
                {
                    int animalId = Convert.ToInt32(cmbAnimal.SelectedValue);
                    if (nHistorial.ExisteHistorialParaAnimal(animalId))
                    {
                        MostrarAdvertencia("Este animal ya tiene un historial médico registrado.");
                    }
                }
                catch
                {
                    // Manejar error silenciosamente
                }
            }
        }
        #endregion

        #region Métodos de Mensajes
        private void MostrarError(string mensaje)
        {
            MessageBox.Show(mensaje, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void MostrarAdvertencia(string mensaje)
        {
            MessageBox.Show(mensaje, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void MostrarExito(string mensaje)
        {
            MessageBox.Show(mensaje, "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void MostrarInformacion(string mensaje)
        {
            MessageBox.Show(mensaje, "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        #endregion
    }
}
