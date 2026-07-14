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
    public partial class PersonasModule : BaseModulos
    {
        #region Variables Privadas
        private string _tipoPersonaSeleccionado = "";
        #endregion

        #region Constructor
        public PersonasModule()
        {
            InitializeComponent();
            ConfigurarModulo();
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

                if (resultado == "OK")
                {
                    MostrarMensaje(ModoEdicion ? "Persona actualizada correctamente" : "Persona registrada correctamente");
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

                        string? genero = row["genero"]?.ToString();
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
        #endregion

        #region Eventos
        private void CmbTipoPersona_SelectedIndexChanged(object? sender, EventArgs e)
        {
            // Filtrar automáticamente cuando cambie el tipo
            OnBuscar();
        }

        private void CmbTipoPersonaForm_SelectedIndexChanged(object? sender, EventArgs e)
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
                ActualizarContadorRegistros(datos.Rows.Count);
            }
            catch (Exception ex)
            {
                MostrarMensaje($"Error al cargar datos: {ex.Message}", "Error", MessageBoxIcon.Error);
            }
        }

        private void MostrarCamposSegunTipo(string tipo)
        {
            _tipoPersonaSeleccionado = tipo;

            if (tipo == "Física")
            {
                // Mostrar campos de persona física
                grpPersonaFisica.Visible = true;
                grpPersonaJuridica.Visible = false;

                // Hacer obligatorios los campos de persona física
                lblNombre.ForeColor = Color.DarkRed;
                lblApellido.ForeColor = Color.DarkRed;
            }
            else
            {
                // Mostrar campos de persona jurídica
                grpPersonaFisica.Visible = false;
                grpPersonaJuridica.Visible = true;

                // Hacer obligatorio el campo de razón social
                lblRazonSocial.ForeColor = Color.DarkRed;
            }

            // Reajustar el layout del panel
            panelFormulario.Invalidate();
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
    }
}
