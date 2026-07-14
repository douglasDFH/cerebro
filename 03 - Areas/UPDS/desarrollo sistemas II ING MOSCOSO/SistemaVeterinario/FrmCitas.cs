using CapaNegocio;
using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;


namespace VeterinariaApp
{
    public partial class FrmCitas : Form
    {
        private bool IsNuevo = false;
        private bool IsEditar = false;
        private string FiltroActual = "HOY"; // HOY, SEMANA, PROXIMAS, TODAS
        

        public FrmCitas()
        {
            try
            {
                InitializeComponent();
                ConfigurarFormulario();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error en constructor FrmCitas: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }

        private void ConfigurarFormulario()
        {
            this.ttMensaje.SetToolTip(this.dtpFechaCita, "Seleccione la fecha y hora de la cita");
            this.ttMensaje.SetToolTip(this.cmbAnimal, "Seleccione el animal para la cita");
            this.ttMensaje.SetToolTip(this.cmbVeterinario, "Seleccione el veterinario (opcional)");
            this.ttMensaje.SetToolTip(this.txtMotivo, "Ingrese el motivo de la consulta");
            this.ttMensaje.SetToolTip(this.cmbTipoCita, "Seleccione el tipo de cita");
            this.ttMensaje.SetToolTip(this.chkUrgente, "Marque si es una consulta urgente");
            this.ttMensaje.SetToolTip(this.txtCosto, "Ingrese el costo de la consulta (opcional)");
            this.ttMensaje.SetToolTip(this.txtObservaciones, "Observaciones adicionales");
            this.ttMensaje.SetToolTip(this.cmbEstado, "Estado de la cita");
        }

        private void FrmCitas_Load(object sender, EventArgs e)
        {
            try
            {
                ConfigurarDateTimePicker();
                CargarAnimales();
                CargarVeterinarios();
                CargarTiposCita();
                CargarEstados();
                ConfigurarFiltros();
                this.Mostrar();
                this.Habilitar(false);
                this.Botones();
                ActualizarContadores();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar FrmCitas: " + ex.Message + "\n\n" + ex.StackTrace, 
                    "Error de Carga", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConfigurarDateTimePicker()
        {
            dtpFechaCita.Format = DateTimePickerFormat.Custom;
            dtpFechaCita.CustomFormat = "dd/MM/yyyy HH:mm";
            dtpFechaCita.ShowUpDown = false;
            
            // Establecer hora por defecto a la próxima hora disponible
            DateTime ahora = DateTime.Now;
            DateTime proximaHora = ahora.AddHours(1);
            proximaHora = new DateTime(proximaHora.Year, proximaHora.Month, proximaHora.Day, proximaHora.Hour, 0, 0);
            
            // Si es fuera del horario laboral, programar para el día siguiente a las 8:00 AM
            if (!NCita.EsHorarioValido(proximaHora))
            {
                DateTime manana = ahora.AddDays(1);
                proximaHora = new DateTime(manana.Year, manana.Month, manana.Day, 8, 0, 0);
                
                // Si es domingo, programar para lunes
                if (proximaHora.DayOfWeek == DayOfWeek.Sunday)
                    proximaHora = proximaHora.AddDays(1);
            }
            
            dtpFechaCita.Value = proximaHora;
        }

        private void CargarAnimales()
        {
            try
            {
                cmbAnimal.DataSource = NAnimal.Mostrar();
                cmbAnimal.DisplayMember = "NombreAnimal";
                cmbAnimal.ValueMember = "IdAnimal";
                cmbAnimal.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MensajeError("Error al cargar animales: " + ex.Message);
            }
        }

        private void CargarVeterinarios()
        {
            try
            {
                // Cargar usuarios con rol VETERINARIO o ADMIN
                DataTable dtVeterinarios = NUsuario.Mostrar();
                
                // Filtrar solo veterinarios activos
                if (dtVeterinarios != null && dtVeterinarios.Rows.Count > 0)
                {
                    var veterinarios = dtVeterinarios.AsEnumerable()
                        .Where(row => row.Field<string>("Rol").ToUpper() == "VETERINARIO" || 
                                     row.Field<string>("Rol").ToUpper() == "ADMIN")
                        .Where(row => row.Field<bool>("Estado") == true)
                        .CopyToDataTable();
                    
                    cmbVeterinario.DataSource = veterinarios;
                    cmbVeterinario.DisplayMember = "NombreUsuario";
                    cmbVeterinario.ValueMember = "IdUsuario";
                }
                
                cmbVeterinario.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MensajeError("Error al cargar veterinarios: " + ex.Message);
            }
        }

        private void CargarTiposCita()
        {
            try
            {
                cmbTipoCita.Items.Clear();
                string[] tipos = NCita.GetTiposCita();
                
                foreach (string tipo in tipos)
                {
                    cmbTipoCita.Items.Add(new { Text = NCita.GetDescripcionTipo(tipo), Value = tipo });
                }
                
                cmbTipoCita.DisplayMember = "Text";
                cmbTipoCita.ValueMember = "Value";
                cmbTipoCita.SelectedIndex = 0; // Seleccionar "PRIMERA_VEZ" por defecto
            }
            catch (Exception ex)
            {
                MensajeError("Error al cargar tipos de cita: " + ex.Message);
            }
        }

        private void CargarEstados()
        {
            try
            {
                cmbEstado.Items.Clear();
                string[] estados = NCita.GetEstadosCita();
                
                foreach (string estado in estados)
                {
                    cmbEstado.Items.Add(new { Text = NCita.GetDescripcionEstado(estado), Value = estado });
                }
                
                cmbEstado.DisplayMember = "Text";
                cmbEstado.ValueMember = "Value";
                cmbEstado.SelectedIndex = 0; // Seleccionar "PROGRAMADO" por defecto
            }
            catch (Exception ex)
            {
                MensajeError("Error al cargar estados: " + ex.Message);
            }
        }

        private void ConfigurarFiltros()
        {
            cmbFiltro.Items.Clear();
            cmbFiltro.Items.Add(new { Text = "Citas de Hoy", Value = "HOY" });
            cmbFiltro.Items.Add(new { Text = "Esta Semana", Value = "SEMANA" });
            cmbFiltro.Items.Add(new { Text = "Próximas Citas", Value = "PROXIMAS" });
            cmbFiltro.Items.Add(new { Text = "Todas las Citas", Value = "TODAS" });
            cmbFiltro.Items.Add(new { Text = "Pendientes", Value = "PENDIENTES" });
            
            cmbFiltro.DisplayMember = "Text";
            cmbFiltro.ValueMember = "Value";
            cmbFiltro.SelectedIndex = 0; // "Citas de Hoy" por defecto
        }

        private void ActualizarContadores()
        {
            try
            {
                int citasHoy = NCita.ContarCitasDelDia();
                int citasPendientes = NCita.ContarCitasPendientes();
                decimal ingresosHoy = NCita.CalcularIngresosDia();
                
                lblCitasHoy.Text = $"Citas Hoy: {citasHoy}";
                lblPendientes.Text = $"Pendientes: {citasPendientes}";
                lblIngresos.Text = $"Ingresos Hoy: ${ingresosHoy:N2}";
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al actualizar contadores: " + ex.Message);
            }
        }

        private void Mostrar()
        {
            try
            {
                DataTable dt = null;
                
                switch (FiltroActual)
                {
                    case "HOY":
                        dt = NCita.CitasDelDia();
                        break;
                    case "SEMANA":
                        dt = NCita.GetResumenSemanal(DateTime.Today);
                        break;
                    case "PROXIMAS":
                        dt = NCita.ProximasCitas();
                        break;
                    case "PENDIENTES":
                        dt = NCita.CitasPendientesConfirmacion();
                        break;
                    default:
                        dt = NCita.Mostrar();
                        break;
                }

                dataListado.DataSource = dt;
                ConfigurarDataGrid();
                lblTotal.Text = "Total Registros: " + Convert.ToString(dataListado.Rows.Count);
            }
            catch (Exception ex)
            {
                MensajeError(ex.Message);
            }
        }

        private void ConfigurarDataGrid()
        {
            if (dataListado.DataSource != null)
            {
                dataListado.Columns["IdDiagnostico"].HeaderText = "ID";
                dataListado.Columns["IdDiagnostico"].Width = 50;
                
                if (dataListado.Columns.Contains("FechaCita"))
                {
                    dataListado.Columns["FechaCita"].HeaderText = "Fecha/Hora";
                    dataListado.Columns["FechaCita"].Width = 120;
                    dataListado.Columns["FechaCita"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
                }
                
                if (dataListado.Columns.Contains("NombreAnimal"))
                {
                    dataListado.Columns["NombreAnimal"].HeaderText = "Animal";
                    dataListado.Columns["NombreAnimal"].Width = 100;
                }
                
                if (dataListado.Columns.Contains("Propietario"))
                {
                    dataListado.Columns["Propietario"].HeaderText = "Propietario";
                    dataListado.Columns["Propietario"].Width = 150;
                }
                
                if (dataListado.Columns.Contains("Veterinario"))
                {
                    dataListado.Columns["Veterinario"].HeaderText = "Veterinario";
                    dataListado.Columns["Veterinario"].Width = 120;
                }
                
                if (dataListado.Columns.Contains("Motivo"))
                {
                    dataListado.Columns["Motivo"].HeaderText = "Motivo";
                    dataListado.Columns["Motivo"].Width = 200;
                }
                
                if (dataListado.Columns.Contains("Estado"))
                {
                    dataListado.Columns["Estado"].HeaderText = "Estado";
                    dataListado.Columns["Estado"].Width = 100;
                }
                
                if (dataListado.Columns.Contains("Urgencia"))
                {
                    dataListado.Columns["Urgencia"].HeaderText = "Urgencia";
                    dataListado.Columns["Urgencia"].Width = 80;
                }
                
                if (dataListado.Columns.Contains("Costo"))
                {
                    dataListado.Columns["Costo"].HeaderText = "Costo";
                    dataListado.Columns["Costo"].Width = 80;
                    dataListado.Columns["Costo"].DefaultCellStyle.Format = "C2";
                }

                // Colorear filas según estado y urgencia
                foreach (DataGridViewRow row in dataListado.Rows)
                {
                    if (row.Cells["Estado"] != null && row.Cells["Estado"].Value != null)
                    {
                        string estado = row.Cells["Estado"].Value.ToString();
                        bool esUrgente = row.Cells["Urgencia"]?.Value?.ToString().ToUpper() == "ALTA";
                        
                        switch (estado.ToUpper())
                        {
                            case "PROGRAMADO":
                                row.DefaultCellStyle.BackColor = esUrgente ? Color.Orange : Color.LightYellow;
                                break;
                            case "CONFIRMADO":
                                row.DefaultCellStyle.BackColor = esUrgente ? Color.IndianRed : Color.LightGreen;
                                break;
                            case "EN_PROCESO":
                                row.DefaultCellStyle.BackColor = Color.LightBlue;
                                break;
                            case "COMPLETADO":
                                row.DefaultCellStyle.BackColor = Color.LightGray;
                                break;
                            case "CANCELADO":
                                row.DefaultCellStyle.BackColor = Color.MistyRose;
                                row.DefaultCellStyle.ForeColor = Color.Gray;
                                break;
                        }
                    }
                }
            }
        }

        private void Habilitar(bool valor)
        {
            txtIdCita.ReadOnly = !valor;
            dtpFechaCita.Enabled = valor;
            cmbAnimal.Enabled = valor;
            cmbVeterinario.Enabled = valor;
            txtMotivo.ReadOnly = !valor;
            cmbTipoCita.Enabled = valor;
            chkUrgente.Enabled = valor;
            txtCosto.ReadOnly = !valor;
            txtObservaciones.ReadOnly = !valor;
            cmbEstado.Enabled = valor && IsEditar;
        }

        private void Botones()
        {
            if (IsNuevo || IsEditar)
            {
                this.Habilitar(true);
                btnNuevo.Enabled = false;
                btnGuardar.Enabled = true;
                btnEditar.Enabled = false;
                btnCancelar.Enabled = true;
            }
            else
            {
                this.Habilitar(false);
                btnNuevo.Enabled = true;
                btnGuardar.Enabled = false;
                btnEditar.Enabled = true;
                btnCancelar.Enabled = false;
            }
        }

        private void Limpiar()
        {
            txtIdCita.Text = string.Empty;
            ConfigurarDateTimePicker();
            cmbAnimal.SelectedIndex = -1;
            cmbVeterinario.SelectedIndex = -1;
            txtMotivo.Text = string.Empty;
            cmbTipoCita.SelectedIndex = 0;
            chkUrgente.Checked = false;
            txtCosto.Text = string.Empty;
            txtObservaciones.Text = string.Empty;
            cmbEstado.SelectedIndex = 0;
        }

        private void MensajeOk(string mensaje)
        {
            MessageBox.Show(mensaje, "Sistema Veterinario", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void MensajeError(string mensaje)
        {
            MessageBox.Show(mensaje, "Sistema Veterinario", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private bool ValidarDatos()
        {
            if (cmbAnimal.SelectedIndex == -1)
            {
                MensajeError("Debe seleccionar un animal");
                cmbAnimal.Focus();
                return false;
            }

            if (cmbVeterinario.SelectedIndex == -1)
            {
                MensajeError("Debe seleccionar un veterinario");
                cmbVeterinario.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtMotivo.Text))
            {
                MensajeError("Debe ingresar el motivo de la consulta");
                txtMotivo.Focus();
                return false;
            }

            // Validar disponibilidad del veterinario
            int idVeterinario = Convert.ToInt32(cmbVeterinario.SelectedValue);
            DateTime fechaHora = dtpFechaCita.Value;
            int idCitaExcluir = IsEditar && !string.IsNullOrEmpty(txtIdCita.Text) ? Convert.ToInt32(txtIdCita.Text) : 0;
            
            if (!NCita.ValidarDisponibilidadVeterinario(idVeterinario, fechaHora, idCitaExcluir))
            {
                MensajeError("El veterinario no está disponible en el horario seleccionado");
                dtpFechaCita.Focus();
                return false;
            }

            // Validar costo si se ingresó
            if (!string.IsNullOrEmpty(txtCosto.Text))
            {
                if (!decimal.TryParse(txtCosto.Text, out decimal costo) || costo < 0)
                {
                    MensajeError("El costo debe ser un número válido mayor o igual a 0");
                    txtCosto.Focus();
                    return false;
                }
            }

            return true;
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            this.IsNuevo = true;
            this.IsEditar = false;
            this.Botones();
            this.Limpiar();
            this.Habilitar(true);
            dtpFechaCita.Focus();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                string rpta = "";
                
                if (!ValidarDatos())
                    return;

                int idAnimal = Convert.ToInt32(cmbAnimal.SelectedValue);
                int idVeterinario = Convert.ToInt32(cmbVeterinario.SelectedValue);
                DateTime fechaHora = dtpFechaCita.Value;
                string motivo = txtMotivo.Text.Trim();
                string tipo = cmbTipoCita.SelectedValue.ToString();
                bool esUrgente = chkUrgente.Checked;
                decimal? costo = string.IsNullOrEmpty(txtCosto.Text) ? (decimal?)null : Convert.ToDecimal(txtCosto.Text);
                string observaciones = string.IsNullOrEmpty(txtObservaciones.Text) ? null : txtObservaciones.Text.Trim();

                if (this.IsNuevo)
                {
                    rpta = NCita.ProgramarCita(idAnimal, idVeterinario, fechaHora, motivo, esUrgente, tipo, costo, observaciones);
                }
                else
                {
                    int idCita = Convert.ToInt32(txtIdCita.Text);
                    rpta = NCita.ReprogramarCita(idCita, fechaHora, idVeterinario, observaciones);
                }

                if (rpta.Equals("OK"))
                {
                    if (this.IsNuevo)
                        this.MensajeOk("Cita programada correctamente");
                    else
                        this.MensajeOk("Cita actualizada correctamente");

                    this.IsNuevo = false;
                    this.IsEditar = false;
                    this.Botones();
                    this.Limpiar();
                    this.Mostrar();
                    ActualizarContadores();
                }
                else
                {
                    this.MensajeError(rpta);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtIdCita.Text))
            {
                this.IsEditar = true;
                this.Botones();
                this.Habilitar(true);
                dtpFechaCita.Focus();
            }
            else
            {
                this.MensajeError("Debe seleccionar el registro que desea editar");
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.IsNuevo = false;
            this.IsEditar = false;
            this.Botones();
            this.Limpiar();
            this.Habilitar(false);
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult opcion = MessageBox.Show("¿Realmente desea cancelar esta cita?", "Sistema Veterinario",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (opcion == DialogResult.Yes)
                {
                    int idCita = Convert.ToInt32(txtIdCita.Text);
                    string motivo = Microsoft.VisualBasic.Interaction.InputBox(
                        "Ingrese el motivo de la cancelación:", 
                        "Cancelar Cita", 
                        "Cancelación solicitada por el cliente");

                    string rpta = NCita.CancelarCita(idCita, motivo);
                    if (rpta.Equals("OK"))
                    {
                        this.MensajeOk("Cita cancelada correctamente");
                        this.Mostrar();
                        ActualizarContadores();
                    }
                    else
                    {
                        this.MensajeError(rpta);
                    }
                }
            }
            catch (Exception ex)
            {
                this.MensajeError(ex.Message);
            }
        }

        private void dataListado_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                try
                {
                    txtIdCita.Text = dataListado.Rows[e.RowIndex].Cells["IdDiagnostico"].Value.ToString();
                    
                    // Cargar datos de la cita seleccionada
                    if (dataListado.Rows[e.RowIndex].Cells["FechaCita"].Value != DBNull.Value)
                        dtpFechaCita.Value = Convert.ToDateTime(dataListado.Rows[e.RowIndex].Cells["FechaCita"].Value);
                    
                    // Seleccionar animal
                    if (dataListado.Rows[e.RowIndex].Cells["IdAnimal"] != null && 
                        dataListado.Rows[e.RowIndex].Cells["IdAnimal"].Value != DBNull.Value)
                    {
                        int idAnimal = Convert.ToInt32(dataListado.Rows[e.RowIndex].Cells["IdAnimal"].Value);
                        cmbAnimal.SelectedValue = idAnimal;
                    }
                    
                    // Seleccionar veterinario
                    if (dataListado.Rows[e.RowIndex].Cells["IdVeterinario"] != null && 
                        dataListado.Rows[e.RowIndex].Cells["IdVeterinario"].Value != DBNull.Value)
                    {
                        int idVeterinario = Convert.ToInt32(dataListado.Rows[e.RowIndex].Cells["IdVeterinario"].Value);
                        cmbVeterinario.SelectedValue = idVeterinario;
                    }
                    
                    txtMotivo.Text = dataListado.Rows[e.RowIndex].Cells["Motivo"]?.Value?.ToString() ?? "";
                    
                    // Establecer estado de urgencia
                    if (dataListado.Rows[e.RowIndex].Cells["Urgencia"] != null)
                    {
                        string urgencia = dataListado.Rows[e.RowIndex].Cells["Urgencia"].Value?.ToString();
                        chkUrgente.Checked = urgencia?.ToUpper() == "ALTA";
                    }
                    
                    // Establecer costo
                    if (dataListado.Rows[e.RowIndex].Cells["Costo"] != null && 
                        dataListado.Rows[e.RowIndex].Cells["Costo"].Value != DBNull.Value)
                    {
                        txtCosto.Text = dataListado.Rows[e.RowIndex].Cells["Costo"].Value.ToString();
                    }
                    
                    // Establecer estado
                    if (dataListado.Rows[e.RowIndex].Cells["Estado"] != null)
                    {
                        string estado = dataListado.Rows[e.RowIndex].Cells["Estado"].Value?.ToString();
                        for (int i = 0; i < cmbEstado.Items.Count; i++)
                        {
                            dynamic item = cmbEstado.Items[i];
                            if (item.Value.ToString() == estado)
                            {
                                cmbEstado.SelectedIndex = i;
                                break;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MensajeError("Error al cargar datos de la cita: " + ex.Message);
                }
            }
        }

        private void cmbFiltro_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbFiltro.SelectedIndex >= 0)
                {
                    dynamic selectedItem = cmbFiltro.SelectedItem;
                    FiltroActual = selectedItem.Value;
                    Mostrar();
                }
            }
            catch (Exception ex)
            {
                MensajeError("Error al cambiar filtro: " + ex.Message);
            }
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(txtBuscar.Text))
                {
                    DataTable dt = NCita.BuscarTexto(txtBuscar.Text.Trim());
                    dataListado.DataSource = dt;
                    ConfigurarDataGrid();
                    lblTotal.Text = "Total Registros: " + Convert.ToString(dataListado.Rows.Count);
                }
                else
                {
                    Mostrar();
                }
            }
            catch (Exception ex)
            {
                MensajeError("Error en búsqueda: " + ex.Message);
            }
        }

        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtIdCita.Text))
                {
                    int idCita = Convert.ToInt32(txtIdCita.Text);
                    string observaciones = "Cita confirmada el " + DateTime.Now.ToString("dd/MM/yyyy HH:mm");
                    
                    string rpta = NCita.ConfirmarCita(idCita, observaciones);
                    if (rpta.Equals("OK"))
                    {
                        MensajeOk("Cita confirmada correctamente");
                        Mostrar();
                        ActualizarContadores();
                    }
                    else
                    {
                        MensajeError(rpta);
                    }
                }
                else
                {
                    MensajeError("Debe seleccionar una cita para confirmar");
                }
            }
            catch (Exception ex)
            {
                MensajeError("Error al confirmar cita: " + ex.Message);
            }
        }

        private void btnIniciarConsulta_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtIdCita.Text))
                {
                    int idCita = Convert.ToInt32(txtIdCita.Text);
                    
                    string rpta = NCita.IniciarConsulta(idCita);
                    if (rpta.Equals("OK"))
                    {
                        MensajeOk("Consulta iniciada. Puede proceder con el diagnóstico.");
                        Mostrar();
                        ActualizarContadores();
                    }
                    else
                    {
                        MensajeError(rpta);
                    }
                }
                else
                {
                    MensajeError("Debe seleccionar una cita para iniciar");
                }
            }
            catch (Exception ex)
            {
                MensajeError("Error al iniciar consulta: " + ex.Message);
            }
        }

        private void cmbAnimal_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Cuando se selecciona un animal, se podría cargar información adicional
            // o mostrar el historial de citas previas
            if (cmbAnimal.SelectedIndex >= 0)
            {
                try
                {
                    int idAnimal = Convert.ToInt32(cmbAnimal.SelectedValue);
                    // Aquí se podría mostrar info adicional del animal o propietario
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error al seleccionar animal: " + ex.Message);
                }
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            try
            {
                Mostrar();
                ActualizarContadores();
                MensajeOk("Datos actualizados correctamente");
            }
            catch (Exception ex)
            {
                MensajeError("Error al actualizar: " + ex.Message);
            }
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            try
            {
                // Aquí se implementaría la funcionalidad de impresión
                // Por ejemplo, generar un reporte de las citas mostradas
                MensajeOk("Funcionalidad de impresión en desarrollo");
            }
            catch (Exception ex)
            {
                MensajeError("Error al imprimir: " + ex.Message);
            }
        }

        // Timer para actualizar automáticamente la información cada 5 minutos
        private void timer1_Tick(object sender, EventArgs e)
        {
            ActualizarContadores();
            
            // Si estamos mostrando las citas del día, actualizar automáticamente
            if (FiltroActual == "HOY")
            {
                Mostrar();
            }
        }

        private void FrmCitas_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Detener el timer si existe
            if (timer1 != null)
            {
                timer1.Stop();
                timer1.Dispose();
            }
        }
    }
}