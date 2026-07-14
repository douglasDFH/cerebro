using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using CapaNegocio;
using SistemVeterinario.Navigation;

namespace SistemVeterinario.Forms
{
    /// <summary>
    /// Módulo para gestión de Mascotas
    /// Hereda de BaseModulos para funcionalidad estándar de CRUD
    /// </summary>
    public partial class MascotasModule : BaseModulos
    {
        #region Variables Privadas
        private int _propietarioSeleccionadoId = 0;
        private string _propietarioSeleccionadoNombre = "";
        #endregion

        #region Constructor
        public MascotasModule()
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
                    ConfigurarComboBoxes();
                    ConfigurarEventos();
                    ConfigurarControlesIniciales();
                };
            }
            catch (Exception ex)
            {
                MostrarMensaje($"Error configurando módulo: {ex.Message}", "Error", MessageBoxIcon.Error);
            }
        }

        private void ConfigurarEventos()
        {
            try
            {
                // Los eventos ya están configurados en el Designer.cs
                // Solo configuramos eventos adicionales si es necesario
                if (cmbEspecie != null) cmbEspecie.SelectedIndexChanged += CmbEspecie_SelectedIndexChanged;
                if (chkTieneFechaNacimiento != null) chkTieneFechaNacimiento.CheckedChanged += ChkTieneFechaNacimiento_CheckedChanged;
            }
            catch (Exception ex)
            {
                MostrarMensaje($"Error configurando eventos: {ex.Message}", "Error", MessageBoxIcon.Error);
            }
        }

        private void ConfigurarComboBoxes()
        {
            try
            {
                // Configurar ComboBox de especie
                if (cmbEspecie != null)
                {
                    cmbEspecie.Items.Clear();
                    var especies = NMascotas.ObtenerEspeciesComunes();
                    foreach (string especie in especies)
                    {
                        cmbEspecie.Items.Add(especie);
                    }
                    if (cmbEspecie.Items.Count > 0)
                        cmbEspecie.SelectedIndex = 0;
                }

                // Configurar ComboBox de género
                if (cmbGenero != null)
                {
                    cmbGenero.Items.Clear();
                    cmbGenero.Items.Add("Seleccionar...");
                    cmbGenero.Items.Add("M");
                    cmbGenero.Items.Add("F");
                    cmbGenero.SelectedIndex = 0;
                }

                // Configurar ComboBox de raza
                if (cmbRaza != null)
                {
                    cmbRaza.Items.Clear();
                    cmbRaza.Items.Add("Seleccionar...");
                }
            }
            catch (Exception ex)
            {
                MostrarMensaje($"Error configurando ComboBoxes: {ex.Message}", "Error", MessageBoxIcon.Error);
            }
        }

        private void ConfigurarControlesIniciales()
        {
            // Configurar fecha por defecto
            if (dtpFechaNacimiento != null)
            {
                dtpFechaNacimiento.Value = DateTime.Now.AddYears(-1);
                dtpFechaNacimiento.Enabled = false;
            }

            // Configurar campos iniciales
            if (cmbEspecie != null)
            {
                CargarRazasPorEspecie(cmbEspecie.Text);
            }
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
                    datos = NMascotas.BuscarPorNombre(textoBuscar);
                }
                else
                {
                    datos = NMascotas.Mostrar();
                }

                if (datos != null)
                {
                    base.CargarDatos(datos);
                    ActualizarContadorRegistros(datos.Rows.Count);
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
            if (cmbEspecie != null && cmbEspecie.Items.Count > 0)
            {
                cmbEspecie.SelectedIndex = 0;
                CargarRazasPorEspecie(cmbEspecie.Text);
            }
        }

        protected override void OnGuardar()
        {
            if (!ValidarCampos())
                return;

            try
            {
                string resultado = "";
                string nombre = txtNombre?.Text?.Trim() ?? "";
                string especie = cmbEspecie?.Text?.Trim() ?? "";
                string raza = txtRaza?.Text?.Trim() ?? "";
                string color = txtColor?.Text?.Trim() ?? "";
                string genero = ObtenerGeneroSeleccionado();
                string microchip = txtMicrochip?.Text?.Trim() ?? "";
                decimal? peso = nudPeso?.Value > 0 ? nudPeso.Value : null;
                bool esterilizado = chkEsterilizado?.Checked ?? false;
                DateTime? fechaNacimiento = null;

                if (chkTieneFechaNacimiento?.Checked == true)
                {
                    fechaNacimiento = dtpFechaNacimiento?.Value;
                }

                if (ModoEdicion)
                {
                    resultado = NMascotas.Editar(
                        IdSeleccionado,
                        nombre,
                        especie,
                        _propietarioSeleccionadoId,
                        raza,
                        fechaNacimiento,
                        peso,
                        color,
                        genero,
                        esterilizado,
                        microchip
                    );
                }
                else
                {
                    resultado = NMascotas.Insertar(
                        nombre,
                        especie,
                        _propietarioSeleccionadoId,
                        raza,
                        fechaNacimiento,
                        peso,
                        color,
                        genero,
                        esterilizado,
                        microchip
                    );
                }

                if (resultado == "OK" || resultado.Contains("exitosamente"))
                {
                    MostrarMensaje(ModoEdicion ? "Mascota actualizada correctamente" : "Mascota registrada correctamente");
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
                string resultado = NMascotas.Eliminar(id);

                if (resultado == "OK")
                {
                    MostrarMensaje("Mascota eliminada correctamente");
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
                // Debug: Mostrar información sobre el ID que se está buscando
                System.Diagnostics.Debug.WriteLine($"CargarDatosParaEdicion - ID recibido: {id}");
                
                DataTable datos = NMascotas.ObtenerPorId(id);
                
                // Debug: Verificar qué devuelve la consulta
                if (datos == null)
                {
                    System.Diagnostics.Debug.WriteLine("CargarDatosParaEdicion - datos es NULL");
                    MostrarMensaje($"Error: La consulta no devolvió ningún resultado para el ID: {id}", "Error de datos", MessageBoxIcon.Error);
                    return;
                }
                
                System.Diagnostics.Debug.WriteLine($"CargarDatosParaEdicion - Filas devueltas: {datos.Rows.Count}");
                
                if (datos.Rows.Count == 0)
                {
                    System.Diagnostics.Debug.WriteLine($"CargarDatosParaEdicion - No se encontraron filas para ID: {id}");
                    MostrarMensaje($"No se encontraron datos para la mascota con ID: {id}\n\nVerifique que el registro existe en la base de datos.", "Registro no encontrado", MessageBoxIcon.Warning);
                    return;
                }

                DataRow row = datos.Rows[0];

                // Cargar datos básicos con verificación segura
                if (txtNombre != null) 
                    txtNombre.Text = ObtenerValorSeguro(row, "nombre");
                if (txtColor != null) 
                    txtColor.Text = ObtenerValorSeguro(row, "color");
                if (txtMicrochip != null) 
                    txtMicrochip.Text = ObtenerValorSeguro(row, "microchip");

                // Configurar especie y raza
                string especie = ObtenerValorSeguro(row, "especie");
                if (cmbEspecie != null && !string.IsNullOrEmpty(especie))
                {
                    // Buscar la especie en el combo
                    for (int i = 0; i < cmbEspecie.Items.Count; i++)
                    {
                        if (cmbEspecie.Items[i].ToString().Equals(especie, StringComparison.OrdinalIgnoreCase))
                        {
                            cmbEspecie.SelectedIndex = i;
                            break;
                        }
                    }
                    CargarRazasPorEspecie(especie);
                }
                
                if (txtRaza != null) 
                    txtRaza.Text = ObtenerValorSeguro(row, "raza");

                // Configurar género
                string genero = ObtenerValorSeguro(row, "genero");
                if (!string.IsNullOrEmpty(genero) && cmbGenero != null)
                {
                    for (int i = 0; i < cmbGenero.Items.Count; i++)
                    {
                        if (cmbGenero.Items[i].ToString().Equals(genero, StringComparison.OrdinalIgnoreCase))
                        {
                            cmbGenero.SelectedIndex = i;
                            break;
                        }
                    }
                }

                // Configurar peso
                if (nudPeso != null)
                {
                    decimal peso = ObtenerDecimalSeguro(row, "peso");
                    if (peso > 0)
                    {
                        nudPeso.Value = peso;
                    }
                    else
                    {
                        nudPeso.Value = 0;
                    }
                }

                // Configurar esterilizado
                if (chkEsterilizado != null)
                {
                    chkEsterilizado.Checked = ObtenerBooleanSeguro(row, "esterilizado");
                }

                // Configurar fecha de nacimiento
                DateTime? fechaNacimiento = ObtenerFechaSegura(row, "fecha_nacimiento");
                if (fechaNacimiento.HasValue)
                {
                    if (chkTieneFechaNacimiento != null) 
                        chkTieneFechaNacimiento.Checked = true;
                    if (dtpFechaNacimiento != null)
                    {
                        dtpFechaNacimiento.Value = fechaNacimiento.Value;
                        dtpFechaNacimiento.Enabled = true;
                    }
                }
                else
                {
                    if (chkTieneFechaNacimiento != null) 
                        chkTieneFechaNacimiento.Checked = false;
                    if (dtpFechaNacimiento != null) 
                        dtpFechaNacimiento.Enabled = false;
                }

                // Cargar propietario usando datos del query actual
                string propietario = ObtenerValorSeguro(row, "propietario");
                int propietarioId = ObtenerIntSeguro(row, "persona_id");
                
                if (propietarioId > 0 && !string.IsNullOrEmpty(propietario))
                {
                    _propietarioSeleccionadoId = propietarioId;
                    _propietarioSeleccionadoNombre = propietario;
                    if (txtPropietario != null)
                    {
                        txtPropietario.Text = propietario;
                    }
                }
                else if (propietarioId > 0)
                {
                    // Fallback: cargar desde la tabla persona si no viene en el query
                    CargarDatosPropietario(propietarioId);
                }
            }
            catch (Exception ex)
            {
                MostrarMensaje($"Error al cargar datos: {ex.Message}\n\nDetalles: {ex.StackTrace}", "Error", MessageBoxIcon.Error);
            }
        }

        protected override void LimpiarFormulario()
        {
            // Limpiar campos de mascota
            if (txtNombre != null) txtNombre.Text = "";
            if (txtRaza != null) txtRaza.Text = "";
            if (txtColor != null) txtColor.Text = "";
            if (txtMicrochip != null) txtMicrochip.Text = "";
            if (txtPropietario != null) txtPropietario.Text = "";
            if (nudPeso != null) nudPeso.Value = 0;
            if (cmbGenero != null) cmbGenero.SelectedIndex = 0;
            if (chkEsterilizado != null) chkEsterilizado.Checked = false;
            if (chkTieneFechaNacimiento != null) chkTieneFechaNacimiento.Checked = false;
            if (dtpFechaNacimiento != null)
            {
                dtpFechaNacimiento.Value = DateTime.Now.AddYears(-1);
                dtpFechaNacimiento.Enabled = false;
            }

            // Limpiar propietario seleccionado
            _propietarioSeleccionadoId = 0;
            _propietarioSeleccionadoNombre = "";
        }
        #endregion

        #region Métodos Override Adicionales
        protected override void OnEditar(DataGridViewRow row)
        {
            try
            {
                // Debug: Verificar los datos de la fila
                System.Diagnostics.Debug.WriteLine("=== OnEditar Debug Info ===");
                System.Diagnostics.Debug.WriteLine($"Fila seleccionada - Index: {row.Index}");
                
                if (row.DataBoundItem is DataRowView dataRow)
                {
                    System.Diagnostics.Debug.WriteLine($"DataBoundItem encontrado");
                    
                    // Mostrar todas las columnas disponibles
                    foreach (DataColumn column in dataRow.Row.Table.Columns)
                    {
                        var value = dataRow.Row[column.ColumnName];
                        System.Diagnostics.Debug.WriteLine($"Columna: {column.ColumnName}, Valor: {value ?? "NULL"}");
                    }
                    
                    // Verificar específicamente el ID
                    if (dataRow.Row.Table.Columns.Contains("id"))
                    {
                        var idValue = dataRow["id"];
                        System.Diagnostics.Debug.WriteLine($"ID Value: {idValue}, Type: {idValue?.GetType()}");
                        
                        if (idValue != null && idValue != DBNull.Value)
                        {
                            int id = Convert.ToInt32(idValue);
                            System.Diagnostics.Debug.WriteLine($"ID convertido: {id}");
                            
                            // Llamar al método base
                            base.OnEditar(row);
                            return;
                        }
                        else
                        {
                            System.Diagnostics.Debug.WriteLine("ERROR: ID es NULL o DBNull");
                            MostrarMensaje("Error: No se pudo obtener el ID de la mascota seleccionada", "Error", MessageBoxIcon.Error);
                            return;
                        }
                    }
                    else
                    {
                        System.Diagnostics.Debug.WriteLine("ERROR: Columna 'id' no encontrada");
                        MostrarMensaje("Error: La columna ID no está disponible en los datos", "Error", MessageBoxIcon.Error);
                        return;
                    }
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("ERROR: row.DataBoundItem no es DataRowView");
                    MostrarMensaje("Error: No se pudo acceder a los datos de la fila seleccionada", "Error", MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"ERROR en OnEditar: {ex.Message}");
                MostrarMensaje($"Error al intentar editar: {ex.Message}", "Error", MessageBoxIcon.Error);
            }
        }
        #endregion

        #region Eventos
        private void CmbEspecie_SelectedIndexChanged(object? sender, EventArgs e)
        {
            string especieSeleccionada = cmbEspecie?.Text ?? "";
            CargarRazasPorEspecie(especieSeleccionada);
        }

        private void BtnBuscarPropietario_Click(object sender, EventArgs e)
        {
            try
            {
                using (var frmBuscarCliente = new BuscarClienteDialog())
                {
                    if (frmBuscarCliente.ShowDialog() == DialogResult.OK)
                    {
                        _propietarioSeleccionadoId = frmBuscarCliente.ClienteSeleccionadoId;
                        _propietarioSeleccionadoNombre = frmBuscarCliente.ClienteSeleccionadoNombre;
                        if (txtPropietario != null)
                        {
                            txtPropietario.Text = _propietarioSeleccionadoNombre;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MostrarMensaje($"Error buscando propietario: {ex.Message}", "Error", MessageBoxIcon.Error);
            }
        }

        private void ChkTieneFechaNacimiento_CheckedChanged(object? sender, EventArgs e)
        {
            if (dtpFechaNacimiento != null)
            {
                dtpFechaNacimiento.Enabled = chkTieneFechaNacimiento?.Checked ?? false;
            }
        }
        #endregion

        #region Métodos Auxiliares
        private void CargarDatos()
        {
            try
            {
                DataTable datos = NMascotas.Mostrar();
                if (datos != null)
                {
                    base.CargarDatos(datos);
                    ActualizarContadorRegistros(datos.Rows.Count);
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

        private void CargarRazasPorEspecie(string especie)
        {
            try
            {
                if (cmbRaza == null) return;

                cmbRaza.Items.Clear();
                cmbRaza.Items.Add("Seleccionar...");

                if (!string.IsNullOrEmpty(especie))
                {
                    var razas = NMascotas.ObtenerRazasComunes(especie);
                    foreach (string raza in razas)
                    {
                        cmbRaza.Items.Add(raza);
                    }

                    // Cargar también razas de la base de datos
                    DataTable razasBD = NMascotas.ObtenerRazasPorEspecie(especie);
                    if (razasBD != null)
                    {
                        foreach (DataRow row in razasBD.Rows)
                        {
                            string raza = row["raza"].ToString();
                            if (!cmbRaza.Items.Contains(raza))
                            {
                                cmbRaza.Items.Add(raza);
                            }
                        }
                    }
                }

                cmbRaza.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MostrarMensaje($"Error cargando razas: {ex.Message}", "Error", MessageBoxIcon.Error);
            }
        }

        private void CargarDatosPropietario(int propietarioId)
        {
            try
            {
                DataTable datos = NPersona.ObtenerPorId(propietarioId);
                if (datos != null && datos.Rows.Count > 0)
                {
                    DataRow row = datos.Rows[0];
                    _propietarioSeleccionadoId = propietarioId;
                    
                    // Intentar obtener el nombre completo, si no existe, construirlo
                    string nombreCompleto = ObtenerValorSeguro(row, "nombre_completo");
                    if (string.IsNullOrEmpty(nombreCompleto))
                    {
                        // Construir nombre completo desde campos separados
                        string nombre = ObtenerValorSeguro(row, "nombre");
                        string apellido = ObtenerValorSeguro(row, "apellido");
                        string razonSocial = ObtenerValorSeguro(row, "razon_social");
                        
                        if (!string.IsNullOrEmpty(razonSocial))
                        {
                            nombreCompleto = razonSocial;
                        }
                        else
                        {
                            nombreCompleto = $"{nombre} {apellido}".Trim();
                        }
                    }
                    
                    _propietarioSeleccionadoNombre = nombreCompleto;
                    if (txtPropietario != null)
                    {
                        txtPropietario.Text = _propietarioSeleccionadoNombre;
                    }
                }
                else
                {
                    // Si no se encuentra el propietario, limpiar la selección
                    _propietarioSeleccionadoId = 0;
                    _propietarioSeleccionadoNombre = "";
                    if (txtPropietario != null)
                    {
                        txtPropietario.Text = "Propietario no encontrado";
                    }
                }
            }
            catch (Exception ex)
            {
                MostrarMensaje($"Error cargando propietario: {ex.Message}", "Error", MessageBoxIcon.Error);
                // En caso de error, limpiar la selección
                _propietarioSeleccionadoId = 0;
                _propietarioSeleccionadoNombre = "";
                if (txtPropietario != null)
                {
                    txtPropietario.Text = "Error al cargar propietario";
                }
            }
        }

        private bool ValidarCampos()
        {
            if (string.IsNullOrWhiteSpace(txtNombre?.Text))
            {
                MostrarMensaje("El nombre de la mascota es obligatorio", "Validación", MessageBoxIcon.Warning);
                txtNombre?.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(cmbEspecie?.Text))
            {
                MostrarMensaje("La especie es obligatoria", "Validación", MessageBoxIcon.Warning);
                cmbEspecie?.Focus();
                return false;
            }

            if (_propietarioSeleccionadoId <= 0)
            {
                MostrarMensaje("Debe seleccionar un propietario", "Validación", MessageBoxIcon.Warning);
                btnBuscarPropietario?.Focus();
                return false;
            }

            return true;
        }

        private string ObtenerGeneroSeleccionado()
        {
            if (cmbGenero?.SelectedIndex > 0)
            {
                return cmbGenero.SelectedItem?.ToString() ?? "";
            }
            return "";
        }

        private void ActualizarContadorRegistros(int cantidad)
        {
            if (lblContador != null)
            {
                lblContador.Text = $"Total de registros: {cantidad}";
            }
        }

        // Métodos auxiliares seguros para obtener datos de DataRow
        private string ObtenerValorSeguro(DataRow row, string columnName)
        {
            try
            {
                if (row == null || !row.Table.Columns.Contains(columnName))
                    return "";

                var value = row[columnName];
                return (value == null || value == DBNull.Value) ? "" : value.ToString();
            }
            catch (Exception)
            {
                return "";
            }
        }

        private int ObtenerIntSeguro(DataRow row, string columnName)
        {
            try
            {
                if (row == null || !row.Table.Columns.Contains(columnName))
                    return 0;

                var value = row[columnName];
                if (value == null || value == DBNull.Value)
                    return 0;

                return Convert.ToInt32(value);
            }
            catch (Exception)
            {
                return 0;
            }
        }

        private decimal ObtenerDecimalSeguro(DataRow row, string columnName)
        {
            try
            {
                if (row == null || !row.Table.Columns.Contains(columnName))
                    return 0;

                var value = row[columnName];
                if (value == null || value == DBNull.Value)
                    return 0;

                return Convert.ToDecimal(value);
            }
            catch (Exception)
            {
                return 0;
            }
        }

        private bool ObtenerBooleanSeguro(DataRow row, string columnName)
        {
            try
            {
                if (row == null || !row.Table.Columns.Contains(columnName))
                    return false;

                var value = row[columnName];
                if (value == null || value == DBNull.Value)
                    return false;

                return Convert.ToBoolean(value);
            }
            catch (Exception)
            {
                return false;
            }
        }

        private DateTime? ObtenerFechaSegura(DataRow row, string columnName)
        {
            try
            {
                if (row == null || !row.Table.Columns.Contains(columnName))
                    return null;

                var value = row[columnName];
                if (value == null || value == DBNull.Value)
                    return null;

                return Convert.ToDateTime(value);
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion
    }
}