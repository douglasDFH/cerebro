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

            // Configurar ComboBox de rol
            cmbRol.Items.Clear();
            cmbRol.Items.Add("Seleccionar...");
            cmbRol.Items.Add("Usuario");
            cmbRol.Items.Add("Administrador");
            cmbRol.Items.Add("Supervisor");
            cmbRol.SelectedIndex = 0;

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

            // Configurar ComboBox de rol para formulario
            cmbRolForm.Items.Clear();
            cmbRolForm.Items.Add("Usuario");
            cmbRolForm.Items.Add("Administrador");
            cmbRolForm.Items.Add("Supervisor");
            cmbRolForm.SelectedIndex = 0;

            // Configurar eventos
            cmbTipoPersonal.SelectedIndexChanged += CmbTipoPersonal_SelectedIndexChanged;
            cmbTipoPersonalForm.SelectedIndexChanged += CmbTipoPersonalForm_SelectedIndexChanged;
            dtpFechaContratacion.Value = DateTime.Now;

            // Ocultar campos específicos inicialmente
            MostrarCamposSegunTipo();
        }

        private void CmbTipoPersonalForm_SelectedIndexChanged(object? sender, EventArgs e)
        {
            MostrarCamposSegunTipo();
        }

        private void MostrarCamposSegunTipo()
        {
            bool esVeterinario = cmbTipoPersonalForm.Text == "Veterinario";

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
        }

        private void CmbTipoPersonal_SelectedIndexChanged(object? sender, EventArgs e)
        {
            _tipoPersonalSeleccionado = cmbTipoPersonal.Text;
            CargarDatos();
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

                dgvDatos.DataSource = dt;
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
                dgvDatos.DataSource = dt;
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

            if (cmbRolForm.SelectedIndex == -1)
            {
                MostrarMensaje("Debe seleccionar un rol", "Validación", MessageBoxIcon.Warning);
                cmbRolForm.Focus();
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
            GuardarRegistro();
        }

        private bool GuardarRegistro()
        {
            try
            {
                if (!ValidarFormulario())
                    return false;

                string resultado;
                string tipoPersonal = cmbTipoPersonalForm.Text;

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
                            string.IsNullOrEmpty(txtSalario.Text) ? null : decimal.Parse(txtSalario.Text),
                            cmbRolForm.Text,
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
                            string.IsNullOrEmpty(txtSalario.Text) ? null : decimal.Parse(txtSalario.Text),
                            cmbRolForm.Text,
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
                            string.IsNullOrEmpty(txtSalario.Text) ? null : decimal.Parse(txtSalario.Text),
                            cmbRolForm.Text,
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
                            string.IsNullOrEmpty(txtSalario.Text) ? null : decimal.Parse(txtSalario.Text),
                            cmbRolForm.Text,
                            txtArea.Text.Trim(),
                            cmbTurno.Text,
                            cmbNivel.Text
                        );
                    }
                }

                if (resultado == "OK")
                {
                    string accion = ModoEdicion ? "actualizado" : "registrado";
                    MostrarMensaje($"Personal {accion} correctamente", "Éxito", MessageBoxIcon.Information);
                    LimpiarFormulario();
                    CargarDatos();
                    return true;
                }
                else
                {
                    MostrarMensaje(resultado, "Error", MessageBoxIcon.Error);
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
                    cmbRolForm.Text = row["rol"]?.ToString() ?? "Usuario";

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
            cmbRolForm.SelectedIndex = 0;
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
                // Configurar encabezados de columnas
                if (dgvDatos.Columns.Contains("id"))
                    dgvDatos.Columns["id"].Visible = false;
                    
                if (dgvDatos.Columns.Contains("nombre"))
                {
                    dgvDatos.Columns["nombre"].HeaderText = "Nombre";
                    dgvDatos.Columns["nombre"].Width = 150;
                }
                
                if (dgvDatos.Columns.Contains("apellido"))
                {
                    dgvDatos.Columns["apellido"].HeaderText = "Apellido";
                    dgvDatos.Columns["apellido"].Width = 150;
                }
                
                if (dgvDatos.Columns.Contains("email"))
                {
                    dgvDatos.Columns["email"].HeaderText = "Email";
                    dgvDatos.Columns["email"].Width = 200;
                }
                
                if (dgvDatos.Columns.Contains("usuario"))
                {
                    dgvDatos.Columns["usuario"].HeaderText = "Usuario";
                    dgvDatos.Columns["usuario"].Width = 120;
                }
                
                if (dgvDatos.Columns.Contains("telefono"))
                {
                    dgvDatos.Columns["telefono"].HeaderText = "Teléfono";
                    dgvDatos.Columns["telefono"].Width = 120;
                }
                
                if (dgvDatos.Columns.Contains("rol"))
                {
                    dgvDatos.Columns["rol"].HeaderText = "Rol";
                    dgvDatos.Columns["rol"].Width = 100;
                }
                
                if (dgvDatos.Columns.Contains("fecha_contratacion"))
                {
                    dgvDatos.Columns["fecha_contratacion"].HeaderText = "F. Contratación";
                    dgvDatos.Columns["fecha_contratacion"].Width = 120;
                }
                
                if (dgvDatos.Columns.Contains("salario"))
                {
                    dgvDatos.Columns["salario"].HeaderText = "Salario";
                    dgvDatos.Columns["salario"].Width = 100;
                }
                
                // Campos específicos
                if (dgvDatos.Columns.Contains("num_licencia"))
                {
                    dgvDatos.Columns["num_licencia"].HeaderText = "Licencia";
                    dgvDatos.Columns["num_licencia"].Width = 120;
                }
                
                if (dgvDatos.Columns.Contains("especialidad"))
                {
                    dgvDatos.Columns["especialidad"].HeaderText = "Especialidad";
                    dgvDatos.Columns["especialidad"].Width = 150;
                }
                
                if (dgvDatos.Columns.Contains("area"))
                {
                    dgvDatos.Columns["area"].HeaderText = "Área";
                    dgvDatos.Columns["area"].Width = 120;
                }
                
                if (dgvDatos.Columns.Contains("turno"))
                {
                    dgvDatos.Columns["turno"].HeaderText = "Turno";
                    dgvDatos.Columns["turno"].Width = 80;
                }
                
                // Ocultar campos internos
                if (dgvDatos.Columns.Contains("contrasena"))
                    dgvDatos.Columns["contrasena"].Visible = false;
                if (dgvDatos.Columns.Contains("created_at"))
                    dgvDatos.Columns["created_at"].Visible = false;
                if (dgvDatos.Columns.Contains("updated_at"))
                    dgvDatos.Columns["updated_at"].Visible = false;
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

        #endregion
    }
}