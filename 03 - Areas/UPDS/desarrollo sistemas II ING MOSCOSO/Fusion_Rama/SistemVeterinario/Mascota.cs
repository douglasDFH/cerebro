using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using CapaNegocio;

namespace SistemVeterinario
{
    public partial class Mascota : Form
    {
        private bool IsEditing = false;
        private int CurrentMascotaId = 0;
        private int CurrentPropietarioId = 0;

        public Mascota()
        {
            InitializeComponent();
            ValidarControlesInicializados();
            ConfigurarFormulario();
            
            if (dgvMascotas != null)
            {
                CargarDatos();
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("No se cargan datos: dgvMascotas es null");
            }
        }

        private void ValidarControlesInicializados()
        {
            var controlesRequeridos = new Dictionary<string, Control?>
            {
                { "tabControl", tabControl },
                { "dgvMascotas", dgvMascotas },
                { "btnNuevo", btnNuevo },
                { "btnGuardar", btnGuardar },
                { "btnEditar", btnEditar },
                { "btnEliminar", btnEliminar },
                { "btnCancelar", btnCancelar },
                { "cmbEspecie", cmbEspecie }
            };

            var controlesNulos = controlesRequeridos.Where(c => c.Value == null).Select(c => c.Key).ToList();

            if (controlesNulos.Any())
            {
                string mensaje = $"Controles no inicializados: {string.Join(", ", controlesNulos)}";
                System.Diagnostics.Debug.WriteLine(mensaje);
                MessageBox.Show(mensaje, "Error de Inicialización", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void ConfigurarFormulario()
        {
            try
            {
                ConfigurarDataGridView();
                ConfigurarEventos();
                ConfigurarComboBoxes();
                ConfigurarControlesIniciales();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error configurando formulario: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConfigurarDataGridView()
        {
            if (dgvMascotas == null) return;

            try
            {
                dgvMascotas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dgvMascotas.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgvMascotas.MultiSelect = false;
                dgvMascotas.ReadOnly = true;
                dgvMascotas.AllowUserToAddRows = false;
                dgvMascotas.AllowUserToDeleteRows = false;
                dgvMascotas.RowHeadersVisible = false;
                dgvMascotas.BackgroundColor = Color.White;
                dgvMascotas.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(240, 240, 240);
                
                dgvMascotas.CellClick += DgvMascotas_CellClick;
                dgvMascotas.CellDoubleClick += DgvMascotas_CellDoubleClick;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error configurando DataGridView: {ex.Message}");
            }
        }

        private void ConfigurarEventos()
        {
            try
            {
                if (btnNuevo != null) btnNuevo.Click += BtnNuevo_Click;
                if (btnGuardar != null) btnGuardar.Click += BtnGuardar_Click;
                if (btnEditar != null) btnEditar.Click += BtnEditar_Click;
                if (btnEliminar != null) btnEliminar.Click += BtnEliminar_Click;
                if (btnCancelar != null) btnCancelar.Click += BtnCancelar_Click;
                if (btnBuscar != null) btnBuscar.Click += BtnBuscar_Click;
                if (btnRefrescar != null) btnRefrescar.Click += BtnRefrescar_Click;
                if (btnBuscarPropietario != null) btnBuscarPropietario.Click += BtnBuscarPropietario_Click;
                if (btnHistorial != null) btnHistorial.Click += BtnHistorial_Click;
                if (cmbEspecie != null) cmbEspecie.SelectedIndexChanged += CmbEspecie_SelectedIndexChanged;
                if (chkTieneFechaNacimiento != null) chkTieneFechaNacimiento.CheckedChanged += ChkTieneFechaNacimiento_CheckedChanged;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error configurando eventos: {ex.Message}");
            }
        }

        private void ConfigurarComboBoxes()
        {
            try
            {
                if (cmbEspecie != null)
                {
                    cmbEspecie.Items.Clear();
                    var especies = NMascotas.ObtenerEspeciesComunes();
                    foreach (string especie in especies)
                    {
                        cmbEspecie.Items.Add(especie);
                    }
                    cmbEspecie.SelectedIndex = 0;
                }

                if (cmbGenero != null)
                {
                    cmbGenero.Items.Clear();
                    cmbGenero.Items.Add("");
                    cmbGenero.Items.Add("M");
                    cmbGenero.Items.Add("F");
                    cmbGenero.SelectedIndex = 0;
                }

                if (cmbRaza != null)
                {
                    cmbRaza.Items.Clear();
                    cmbRaza.Items.Add("");
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error configurando ComboBoxes: {ex.Message}");
            }
        }

        private void ConfigurarControlesIniciales()
        {
            EstablecerEstadoControles(false);
            LimpiarCampos();
            
            if (dtpFechaNacimiento != null)
            {
                dtpFechaNacimiento.Value = DateTime.Now.AddYears(-1);
                dtpFechaNacimiento.Enabled = false;
            }
        }

        private void CargarDatos()
        {
            try
            {
                if (dgvMascotas == null) 
                {
                    System.Diagnostics.Debug.WriteLine("dgvMascotas es null en CargarDatos");
                    return;
                }

                System.Diagnostics.Debug.WriteLine("Iniciando carga de datos de mascotas...");
                DataTable dt = NMascotas.Mostrar();
                
                if (dt != null)
                {
                    System.Diagnostics.Debug.WriteLine($"DataTable obtenido con {dt.Rows.Count} filas y {dt.Columns.Count} columnas");
                    
                    // Mostrar nombres de columnas para debug
                    foreach (DataColumn column in dt.Columns)
                    {
                        System.Diagnostics.Debug.WriteLine($"Columna disponible: {column.ColumnName}");
                    }
                    
                    dgvMascotas.DataSource = dt;
                    
                    // Dar tiempo para que se generen las columnas
                    dgvMascotas.Refresh();
                    
                    ConfigurarColumnasDataGridView();
                    ActualizarTotalRegistros();
                    
                    System.Diagnostics.Debug.WriteLine($"Datos cargados exitosamente: {dt.Rows.Count} registros");
                }
                else
                {
                    dgvMascotas.DataSource = null;
                    ActualizarTotalRegistros();
                    System.Diagnostics.Debug.WriteLine("DataTable es null - no se obtuvieron datos");
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error cargando datos: {ex.Message}");
                System.Diagnostics.Debug.WriteLine($"StackTrace: {ex.StackTrace}");
                
                MessageBox.Show($"Error cargando datos de mascotas: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                
                if (dgvMascotas != null)
                {
                    dgvMascotas.DataSource = null;
                    ActualizarTotalRegistros();
                }
            }
        }

        private void ConfigurarColumnasDataGridView()
        {
            if (dgvMascotas?.DataSource == null || dgvMascotas.Columns.Count == 0) 
            {
                System.Diagnostics.Debug.WriteLine("No hay columnas para configurar");
                return;
            }

            try
            {
                System.Diagnostics.Debug.WriteLine($"Configurando {dgvMascotas.Columns.Count} columnas");
                
                foreach (DataGridViewColumn column in dgvMascotas.Columns)
                {
                    if (column == null) continue;

                    string columnName = column.Name?.ToLower() ?? "";
                    System.Diagnostics.Debug.WriteLine($"Configurando columna: {columnName}");
                    
                    switch (columnName)
                    {
                        case "id":
                            column.HeaderText = "ID";
                            column.Width = 50;
                            column.Visible = true;
                            column.DisplayIndex = 0;
                            break;
                        case "nombre":
                            column.HeaderText = "Nombre";
                            column.Width = 120;
                            column.Visible = true;
                            column.DisplayIndex = 1;
                            break;
                        case "especie":
                            column.HeaderText = "Especie";
                            column.Width = 80;
                            column.Visible = true;
                            column.DisplayIndex = 2;
                            break;
                        case "raza":
                            column.HeaderText = "Raza";
                            column.Width = 100;
                            column.Visible = true;
                            column.DisplayIndex = 3;
                            break;
                        case "genero":
                            column.HeaderText = "Sexo";
                            column.Width = 50;
                            column.Visible = true;
                            column.DisplayIndex = 4;
                            break;
                        case "peso":
                            column.HeaderText = "Peso (kg)";
                            column.Width = 70;
                            column.Visible = true;
                            column.DisplayIndex = 5;
                            break;
                        case "color":
                            column.HeaderText = "Color";
                            column.Width = 80;
                            column.Visible = true;
                            column.DisplayIndex = 6;
                            break;
                        case "propietario":
                            column.HeaderText = "Propietario";
                            column.Width = 200;
                            column.Visible = true;
                            column.DisplayIndex = 7;
                            break;
                        case "telefono_propietario":
                            column.HeaderText = "Teléfono";
                            column.Width = 100;
                            column.Visible = true;
                            column.DisplayIndex = 8;
                            break;
                        case "esterilizado":
                            column.HeaderText = "Esterilizado";
                            column.Width = 80;
                            column.Visible = true;
                            column.DisplayIndex = 9;
                            break;
                        default:
                            // Ocultar todas las demás columnas
                            column.Visible = false;
                            System.Diagnostics.Debug.WriteLine($"Ocultando columna: {columnName}");
                            break;
                    }
                }
                
                System.Diagnostics.Debug.WriteLine("Configuración de columnas completada");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error configurando columnas: {ex.Message}");
                System.Diagnostics.Debug.WriteLine($"StackTrace: {ex.StackTrace}");
                MessageBox.Show($"Error configurando columnas del grid: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void ActualizarTotalRegistros()
        {
            if (dgvMascotas != null && lblTotalRegistros != null)
            {
                lblTotalRegistros.Text = $"Total de registros: {dgvMascotas.Rows.Count}";
            }
        }

        private void EstablecerEstadoControles(bool habilitado)
        {
            try
            {
                if (grpDatosMascota != null)
                {
                    foreach (Control control in grpDatosMascota.Controls)
                    {
                        if (control is TextBox || control is ComboBox || control is DateTimePicker || 
                            control is CheckBox || control is NumericUpDown)
                        {
                            control.Enabled = habilitado;
                        }
                    }
                }

                if (btnGuardar != null) btnGuardar.Enabled = habilitado;
                if (btnCancelar != null) btnCancelar.Enabled = habilitado;
                if (btnBuscarPropietario != null) btnBuscarPropietario.Enabled = habilitado;
                if (btnNuevo != null) btnNuevo.Enabled = !habilitado;
                if (btnEditar != null) btnEditar.Enabled = !habilitado && dgvMascotas?.SelectedRows.Count > 0;
                if (btnEliminar != null) btnEliminar.Enabled = !habilitado && dgvMascotas?.SelectedRows.Count > 0;
                if (btnHistorial != null) btnHistorial.Enabled = !habilitado && dgvMascotas?.SelectedRows.Count > 0;

                if (dtpFechaNacimiento != null) 
                    dtpFechaNacimiento.Enabled = habilitado && (chkTieneFechaNacimiento?.Checked ?? false);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error estableciendo estado de controles: {ex.Message}");
            }
        }

        private void LimpiarCampos()
        {
            try
            {
                if (txtNombre != null) txtNombre.Text = "";
                if (txtPropietario != null) txtPropietario.Text = "";
                if (txtRaza != null) txtRaza.Text = "";
                if (txtColor != null) txtColor.Text = "";
                if (txtMicrochip != null) txtMicrochip.Text = "";
                if (nudPeso != null) nudPeso.Value = 0;
                if (cmbEspecie != null) cmbEspecie.SelectedIndex = 0;
                if (cmbGenero != null) cmbGenero.SelectedIndex = 0;
                if (cmbRaza != null) cmbRaza.SelectedIndex = 0;
                if (chkEsterilizado != null) chkEsterilizado.Checked = false;
                if (chkTieneFechaNacimiento != null) chkTieneFechaNacimiento.Checked = false;
                if (dtpFechaNacimiento != null) 
                {
                    dtpFechaNacimiento.Value = DateTime.Now.AddYears(-1);
                    dtpFechaNacimiento.Enabled = false;
                }

                CurrentMascotaId = 0;
                CurrentPropietarioId = 0;
                IsEditing = false;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error limpiando campos: {ex.Message}");
            }
        }

        private void CargarDatosEnFormulario(DataGridViewRow row)
        {
            try
            {
                if (row?.Cells == null) return;

                CurrentMascotaId = Convert.ToInt32(row.Cells["id"]?.Value ?? 0);

                if (txtNombre != null) txtNombre.Text = row.Cells["animal_nombre"]?.Value?.ToString() ?? "";
                if (txtPropietario != null) txtPropietario.Text = row.Cells["propietario"]?.Value?.ToString() ?? "";
                if (txtColor != null) txtColor.Text = row.Cells["color"]?.Value?.ToString() ?? "";
                if (txtMicrochip != null) txtMicrochip.Text = row.Cells["microchip"]?.Value?.ToString() ?? "";

                if (cmbEspecie != null)
                {
                    string especie = row.Cells["especie"]?.Value?.ToString() ?? "";
                    cmbEspecie.Text = especie;
                    CargarRazasPorEspecie(especie);
                }

                if (cmbRaza != null)
                {
                    cmbRaza.Text = row.Cells["raza"]?.Value?.ToString() ?? "";
                }

                if (cmbGenero != null)
                {
                    string genero = row.Cells["genero"]?.Value?.ToString() ?? "";
                    cmbGenero.SelectedItem = genero;
                }

                if (nudPeso != null)
                {
                    var peso = row.Cells["peso"]?.Value;
                    if (peso != null && peso != DBNull.Value)
                    {
                        nudPeso.Value = Convert.ToDecimal(peso);
                    }
                    else
                    {
                        nudPeso.Value = 0;
                    }
                }

                if (chkEsterilizado != null)
                {
                    var esterilizado = row.Cells["esterilizado"]?.Value;
                    chkEsterilizado.Checked = esterilizado != null && Convert.ToBoolean(esterilizado);
                }

                if (chkTieneFechaNacimiento != null && dtpFechaNacimiento != null)
                {
                    var fechaNacimiento = row.Cells["fecha_nacimiento"]?.Value;
                    if (fechaNacimiento != null && fechaNacimiento != DBNull.Value)
                    {
                        chkTieneFechaNacimiento.Checked = true;
                        dtpFechaNacimiento.Value = Convert.ToDateTime(fechaNacimiento);
                        dtpFechaNacimiento.Enabled = true;
                    }
                    else
                    {
                        chkTieneFechaNacimiento.Checked = false;
                        dtpFechaNacimiento.Enabled = false;
                    }
                }

                // Buscar ID del propietario
                CurrentPropietarioId = ObtenerIdPropietario(txtPropietario?.Text ?? "");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error cargando datos en formulario: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private int ObtenerIdPropietario(string nombrePropietario)
        {
            try
            {
                if (string.IsNullOrEmpty(nombrePropietario)) return 0;

                DataTable personas = NPersonas.BuscarPorNombre(nombrePropietario);
                if (personas != null && personas.Rows.Count > 0)
                {
                    var propietario = personas.AsEnumerable()
                        .FirstOrDefault(row => row.Field<string>("nombre_mostrar") == nombrePropietario);
                    
                    if (propietario != null)
                    {
                        return propietario.Field<int>("id");
                    }
                }
                return 0;
            }
            catch
            {
                return 0;
            }
        }

        private void CargarRazasPorEspecie(string especie)
        {
            try
            {
                if (cmbRaza == null) return;

                cmbRaza.Items.Clear();
                cmbRaza.Items.Add("");

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
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error cargando razas: {ex.Message}");
            }
        }

        private bool ValidarCampos()
        {
            try
            {
                string nombre = txtNombre?.Text?.Trim() ?? "";
                string especie = cmbEspecie?.Text?.Trim() ?? "";
                decimal? peso = nudPeso?.Value > 0 ? nudPeso.Value : null;
                string genero = cmbGenero?.Text?.Trim() ?? "";
                string microchip = txtMicrochip?.Text?.Trim() ?? "";
                DateTime? fechaNacimiento = null;

                if (chkTieneFechaNacimiento?.Checked == true)
                {
                    fechaNacimiento = dtpFechaNacimiento?.Value;
                }

                string errores = NMascotas.ValidarDatosMascota(nombre, especie, CurrentPropietarioId, 
                    peso, genero, microchip, fechaNacimiento);

                if (!string.IsNullOrEmpty(errores))
                {
                    MessageBox.Show(errores, "Errores de Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error validando campos: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        #region Event Handlers

        private void BtnNuevo_Click(object? sender, EventArgs e)
        {
            try
            {
                LimpiarCampos();
                EstablecerEstadoControles(true);
                IsEditing = false;
                
                if (tabControl != null && tabMantenimiento != null)
                {
                    tabControl.SelectedTab = tabMantenimiento;
                }

                if (txtNombre != null)
                {
                    txtNombre.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al crear nueva mascota: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnGuardar_Click(object? sender, EventArgs e)
        {
            try
            {
                if (!ValidarCampos())
                    return;

                string nombre = txtNombre?.Text?.Trim() ?? "";
                string especie = cmbEspecie?.Text?.Trim() ?? "";
                string raza = txtRaza?.Text?.Trim() ?? "";
                string color = txtColor?.Text?.Trim() ?? "";
                string genero = cmbGenero?.Text?.Trim() ?? "";
                string microchip = txtMicrochip?.Text?.Trim() ?? "";
                decimal? peso = nudPeso?.Value > 0 ? nudPeso.Value : null;
                bool esterilizado = chkEsterilizado?.Checked ?? false;
                DateTime? fechaNacimiento = null;

                if (chkTieneFechaNacimiento?.Checked == true)
                {
                    fechaNacimiento = dtpFechaNacimiento?.Value;
                }

                string resultado = "";

                if (IsEditing)
                {
                    resultado = NMascotas.Editar(CurrentMascotaId, nombre, especie, CurrentPropietarioId,
                        raza, fechaNacimiento, peso, color, genero, esterilizado, microchip);
                }
                else
                {
                    resultado = NMascotas.Insertar(nombre, especie, CurrentPropietarioId,
                        raza, fechaNacimiento, peso, color, genero, esterilizado, microchip);
                }

                if (resultado == "OK" || resultado.Contains("exitosamente"))
                {
                    string accion = IsEditing ? "actualizada" : "registrada";
                    MessageBox.Show($"Mascota {accion} correctamente", "Éxito", 
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    
                    BtnCancelar_Click(null, EventArgs.Empty);
                    CargarDatos();
                    
                    if (tabControl != null && tabListado != null)
                    {
                        tabControl.SelectedTab = tabListado;
                    }
                }
                else
                {
                    MessageBox.Show($"Error: {resultado}", "Error", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error guardando mascota: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnEditar_Click(object? sender, EventArgs e)
        {
            try
            {
                if (dgvMascotas?.SelectedRows.Count > 0)
                {
                    CargarDatosEnFormulario(dgvMascotas.SelectedRows[0]);
                    EstablecerEstadoControles(true);
                    IsEditing = true;
                    
                    if (tabControl != null && tabMantenimiento != null)
                    {
                        tabControl.SelectedTab = tabMantenimiento;
                    }
                }
                else
                {
                    MessageBox.Show("Seleccione una mascota para editar", "Información", 
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al editar mascota: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnEliminar_Click(object? sender, EventArgs e)
        {
            try
            {
                if (dgvMascotas?.SelectedRows.Count > 0)
                {
                    var result = MessageBox.Show("¿Está seguro que desea eliminar esta mascota?", 
                        "Confirmar Eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (result == DialogResult.Yes)
                    {
                        int id = Convert.ToInt32(dgvMascotas.SelectedRows[0].Cells["id"].Value);
                        string resultado = NMascotas.Eliminar(id);

                        if (resultado == "OK")
                        {
                            MessageBox.Show("Mascota eliminada correctamente", "Éxito", 
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                            CargarDatos();
                        }
                        else
                        {
                            MessageBox.Show($"Error eliminando mascota: {resultado}", "Error", 
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Seleccione una mascota para eliminar", "Información", 
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al eliminar mascota: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnCancelar_Click(object? sender, EventArgs e)
        {
            try
            {
                LimpiarCampos();
                EstablecerEstadoControles(false);
                
                if (tabControl != null && tabListado != null)
                {
                    tabControl.SelectedTab = tabListado;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cancelar: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnBuscar_Click(object? sender, EventArgs e)
        {
            try
            {
                string textoBuscar = txtBuscar?.Text?.Trim() ?? "";
                
                DataTable dt;
                if (string.IsNullOrEmpty(textoBuscar))
                {
                    dt = NMascotas.Mostrar();
                }
                else
                {
                    dt = NMascotas.BuscarPorNombre(textoBuscar);
                }

                if (dt != null && dgvMascotas != null)
                {
                    dgvMascotas.DataSource = dt;
                    ConfigurarColumnasDataGridView();
                    ActualizarTotalRegistros();
                }
                else
                {
                    MessageBox.Show("No se encontraron resultados", "Información", 
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error en la búsqueda: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnRefrescar_Click(object? sender, EventArgs e)
        {
            try
            {
                if (txtBuscar != null) txtBuscar.Text = "";
                CargarDatos();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al refrescar: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnBuscarPropietario_Click(object? sender, EventArgs e)
        {
            try
            {
                using (var frmBuscarCliente = new BuscarClienteDialog())
                {
                    if (frmBuscarCliente.ShowDialog() == DialogResult.OK)
                    {
                        CurrentPropietarioId = frmBuscarCliente.ClienteSeleccionadoId;
                        if (txtPropietario != null)
                        {
                            txtPropietario.Text = frmBuscarCliente.ClienteSeleccionadoNombre;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error buscando propietario: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnHistorial_Click(object? sender, EventArgs e)
        {
            try
            {
                if (dgvMascotas?.SelectedRows.Count > 0)
                {
                    int mascotaId = Convert.ToInt32(dgvMascotas.SelectedRows[0].Cells["id"].Value);
                    string nombreMascota = dgvMascotas.SelectedRows[0].Cells["animal_nombre"].Value?.ToString() ?? "Mascota";
                    
                    MessageBox.Show($"Historial clínico de {nombreMascota} - Próximamente", "Información", 
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Seleccione una mascota para ver su historial", "Información", 
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al abrir historial: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CmbEspecie_SelectedIndexChanged(object? sender, EventArgs e)
        {
            try
            {
                string especie = cmbEspecie?.Text ?? "";
                CargarRazasPorEspecie(especie);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error cambiando especie: {ex.Message}");
            }
        }

        private void ChkTieneFechaNacimiento_CheckedChanged(object? sender, EventArgs e)
        {
            try
            {
                if (dtpFechaNacimiento != null)
                {
                    dtpFechaNacimiento.Enabled = chkTieneFechaNacimiento?.Checked ?? false;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error cambiando checkbox fecha: {ex.Message}");
            }
        }

        private void DgvMascotas_CellClick(object? sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0 && dgvMascotas?.Rows.Count > e.RowIndex)
                {
                    EstablecerEstadoControles(false);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error en CellClick: {ex.Message}");
            }
        }

        private void DgvMascotas_CellDoubleClick(object? sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    BtnEditar_Click(null, EventArgs.Empty);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error en CellDoubleClick: {ex.Message}");
            }
        }

        #endregion

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            try
            {
                if (IsEditing)
                {
                    var result = MessageBox.Show("¿Desea guardar los cambios antes de cerrar?", 
                        "Cambios Pendientes", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

                    if (result == DialogResult.Cancel)
                    {
                        e.Cancel = true;
                        return;
                    }
                    else if (result == DialogResult.Yes)
                    {
                        BtnGuardar_Click(null, EventArgs.Empty);
                        if (IsEditing)
                        {
                            e.Cancel = true;
                            return;
                        }
                    }
                }

                base.OnFormClosing(e);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error en OnFormClosing: {ex.Message}");
                base.OnFormClosing(e);
            }
        }
    }

    // Clase auxiliar para el diálogo de búsqueda de cliente
    public class BuscarClienteDialog : Form
    {
        public int ClienteSeleccionadoId { get; private set; }
        public string ClienteSeleccionadoNombre { get; private set; } = "";

        private DataGridView dgvClientes;
        private TextBox txtBuscar;
        private Button btnBuscar;
        private Button btnSeleccionar;
        private Button btnCancelar;

        public BuscarClienteDialog()
        {
            InitializeComponent();
            CargarClientes();
        }

        private void InitializeComponent()
        {
            this.Text = "Buscar Cliente";
            this.Size = new Size(600, 400);
            this.StartPosition = FormStartPosition.CenterParent;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            txtBuscar = new TextBox();
            txtBuscar.Location = new Point(10, 10);
            txtBuscar.Size = new Size(200, 23);
            txtBuscar.PlaceholderText = "Buscar cliente...";

            btnBuscar = new Button();
            btnBuscar.Text = "Buscar";
            btnBuscar.Location = new Point(220, 10);
            btnBuscar.Size = new Size(75, 25);
            btnBuscar.Click += BtnBuscar_Click;

            dgvClientes = new DataGridView();
            dgvClientes.Location = new Point(10, 45);
            dgvClientes.Size = new Size(560, 280);
            dgvClientes.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvClientes.MultiSelect = false;
            dgvClientes.ReadOnly = true;
            dgvClientes.AllowUserToAddRows = false;
            dgvClientes.DoubleClick += DgvClientes_DoubleClick;

            btnSeleccionar = new Button();
            btnSeleccionar.Text = "Seleccionar";
            btnSeleccionar.Location = new Point(410, 335);
            btnSeleccionar.Size = new Size(80, 30);
            btnSeleccionar.Click += BtnSeleccionar_Click;

            btnCancelar = new Button();
            btnCancelar.Text = "Cancelar";
            btnCancelar.Location = new Point(500, 335);
            btnCancelar.Size = new Size(80, 30);
            btnCancelar.Click += (s, e) => this.DialogResult = DialogResult.Cancel;

            this.Controls.AddRange(new Control[] {
                txtBuscar, btnBuscar, dgvClientes, btnSeleccionar, btnCancelar
            });
        }

        private void CargarClientes()
        {
            try
            {
                DataTable dt = NPersonas.Mostrar();
                if (dt != null)
                {
                    dgvClientes.DataSource = dt;
                    foreach (DataGridViewColumn column in dgvClientes.Columns)
                    {
                        column.Visible = false;
                    }
                    dgvClientes.Columns["id"].Visible = true;
                    dgvClientes.Columns["nombre_mostrar"].Visible = true;
                    dgvClientes.Columns["tipo"].Visible = true;
                    dgvClientes.Columns["telefono"].Visible = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error cargando clientes: {ex.Message}");
            }
        }

        private void BtnBuscar_Click(object? sender, EventArgs e)
        {
            try
            {
                string texto = txtBuscar.Text.Trim();
                DataTable dt = string.IsNullOrEmpty(texto) ? 
                    NPersonas.Mostrar() : 
                    NPersonas.BuscarPorNombre(texto);
                
                dgvClientes.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error en búsqueda: {ex.Message}");
            }
        }

        private void BtnSeleccionar_Click(object? sender, EventArgs e)
        {
            SeleccionarCliente();
        }

        private void DgvClientes_DoubleClick(object? sender, EventArgs e)
        {
            SeleccionarCliente();
        }

        private void SeleccionarCliente()
        {
            if (dgvClientes.SelectedRows.Count > 0)
            {
                ClienteSeleccionadoId = Convert.ToInt32(dgvClientes.SelectedRows[0].Cells["id"].Value);
                ClienteSeleccionadoNombre = dgvClientes.SelectedRows[0].Cells["nombre_mostrar"].Value?.ToString() ?? "";
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                MessageBox.Show("Seleccione un cliente");
            }
        }
    }
}