using CapaNegocio;
using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace VeterinariaApp
{
    public partial class FrmAnimales : Form
    {
        private bool IsNuevo = false;
        private bool IsEditar = false;

        public FrmAnimales()
        {
            try
            {
                InitializeComponent();
                ConfigurarFormulario();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error en constructor FrmAnimales: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }

        private void ConfigurarFormulario()
        {
            this.ttMensaje.SetToolTip(this.txtNombre, "Ingrese el nombre del animal");
            this.ttMensaje.SetToolTip(this.cmbTipo, "Seleccione el tipo de animal");
            this.ttMensaje.SetToolTip(this.cmbRaza, "Seleccione la raza (opcional)");
            this.ttMensaje.SetToolTip(this.txtColor, "Ingrese el color del animal");
            this.ttMensaje.SetToolTip(this.cmbSexo, "Seleccione el sexo del animal");
            this.ttMensaje.SetToolTip(this.dtpFechaNacimiento, "Seleccione la fecha de nacimiento");
            this.ttMensaje.SetToolTip(this.txtPeso, "Ingrese el peso en kilogramos");
            this.ttMensaje.SetToolTip(this.txtMicrochip, "Ingrese el número de microchip");
            this.ttMensaje.SetToolTip(this.txtObservaciones, "Observaciones adicionales");
            this.ttMensaje.SetToolTip(this.txtAltura, "Ingrese la altura en centímetros");
            this.ttMensaje.SetToolTip(this.txtNumPedigree, "Ingrese el número de pedigrí (opcional)");
            this.ttMensaje.SetToolTip(this.chkEsterilizado, "Marque si el animal está esterilizado");
            this.ttMensaje.SetToolTip(this.chkVacunado, "Marque si el animal está vacunado");
        }

        private void FrmAnimales_Load(object sender, EventArgs e)
        {
            try
            {
                CargarPropietarios();
                CargarTipos();
                CargarSexos();
                CargarRazas();
                ConfigurarFechaNacimiento();
                this.Mostrar();
                this.Habilitar(false);
                this.Botones();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar FrmAnimales: " + ex.Message + "\n\n" + ex.StackTrace, 
                    "Error de Carga", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarPropietarios()
        {
            try
            {
                cmbPropietario.DataSource = NPersona.Mostrar();
                cmbPropietario.DisplayMember = "NombreCompleto";
                cmbPropietario.ValueMember = "IdPersona";
                cmbPropietario.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MensajeError("Error al cargar propietarios: " + ex.Message);
            }
        }

        private void CargarTipos()
        {
            try
            {
                cmbTipo.Items.Clear();
                cmbTipo.Items.AddRange(NAnimal.GetTiposAnimales());
                cmbTipo.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MensajeError("Error al cargar tipos: " + ex.Message);
            }
        }

        private void CargarSexos()
        {
            try
            {
                cmbSexo.Items.Clear();
                cmbSexo.Items.Add(new { Text = "Macho", Value = 'M' });
                cmbSexo.Items.Add(new { Text = "Hembra", Value = 'H' });
                cmbSexo.Items.Add(new { Text = "Indefinido", Value = 'I' });
                cmbSexo.DisplayMember = "Text";
                cmbSexo.ValueMember = "Value";
                cmbSexo.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MensajeError("Error al cargar sexos: " + ex.Message);
            }
        }

        private void CargarRazasPorTipo(string tipo)
        {
            try
            {
                cmbRaza.Items.Clear();
                
                switch (tipo.ToLower())
                {
                    case "perro":
                        cmbRaza.Items.AddRange(NAnimal.GetRazasPerro());
                        break;
                    case "gato":
                        cmbRaza.Items.AddRange(NAnimal.GetRazasGato());
                        break;
                    default:
                        cmbRaza.Items.Add("No especificada");
                        break;
                }
                
                cmbRaza.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MensajeError("Error al cargar razas: " + ex.Message);
            }
        }

        private void MensajeOK(string mensaje)
        {
            MessageBox.Show(mensaje, "Sistema Veterinario", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void MensajeError(string mensaje)
        {
            MessageBox.Show(mensaje, "Sistema Veterinario", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void CargarRazas()
        {
            try
            {
                cmbRaza.Items.Clear();
                cmbRaza.Items.Add("Seleccione un tipo primero");
                cmbRaza.SelectedIndex = 0;
                cmbRaza.Enabled = false;
            }
            catch (Exception ex)
            {
                MensajeError("Error al cargar razas: " + ex.Message);
            }
        }

        private void ConfigurarFechaNacimiento()
        {
            try
            {
                // El diseñador ya tiene configuraciones básicas, solo ajustamos dinámicamente
                dtpFechaNacimiento.Format = DateTimePickerFormat.Short;
                
                // Configurar rangos de fecha válidos para animales (dinámicos)
                DateTime minDate = DateTime.Today.AddYears(-50);
                DateTime maxDate = DateTime.Today;
                DateTime defaultValue = DateTime.Today.AddYears(-1);
                
                // Solo actualizar si es necesario para evitar conflictos
                if (dtpFechaNacimiento.MinDate > minDate || dtpFechaNacimiento.MinDate < minDate.AddDays(-1))
                    dtpFechaNacimiento.MinDate = minDate;
                    
                if (dtpFechaNacimiento.MaxDate < maxDate || dtpFechaNacimiento.MaxDate > maxDate.AddDays(1))
                    dtpFechaNacimiento.MaxDate = maxDate;
                
                // Establecer valor por defecto solo si el actual está fuera del rango
                if (dtpFechaNacimiento.Value < minDate || dtpFechaNacimiento.Value > maxDate)
                    dtpFechaNacimiento.Value = defaultValue;
                
                // Inicialmente deshabilitar el control hasta que se marque el checkbox
                dtpFechaNacimiento.Enabled = false;
            }
            catch (Exception ex)
            {
                MensajeError("Error al configurar fecha: " + ex.Message);
            }
        }

        private void EstablecerFechaSegura(DateTime fecha)
        {
            try
            {
                // Asegurar que la fecha esté dentro del rango válido
                if (fecha < dtpFechaNacimiento.MinDate)
                    fecha = dtpFechaNacimiento.MinDate;
                else if (fecha > dtpFechaNacimiento.MaxDate)
                    fecha = dtpFechaNacimiento.MaxDate;
                    
                dtpFechaNacimiento.Value = fecha;
            }
            catch (Exception ex)
            {
                // Si hay un error, usar una fecha por defecto válida
                dtpFechaNacimiento.Value = DateTime.Today.AddYears(-1);
            }
        }

        private void cmbTipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbTipo.SelectedIndex != -1)
            {
                string tipoSeleccionado = cmbTipo.SelectedItem.ToString();
                CargarRazasPorTipo(tipoSeleccionado);
                cmbRaza.Enabled = true;
            }
            else
            {
                cmbRaza.Items.Clear();
                cmbRaza.Enabled = false;
            }
        }

        private void chkFechaNacimiento_CheckedChanged(object sender, EventArgs e)
        {
            dtpFechaNacimiento.Enabled = chkFechaNacimiento.Checked;
            if (chkFechaNacimiento.Checked)
            {
                EstablecerFechaSegura(DateTime.Today.AddYears(-1));
                int edad = NAnimal.CalcularEdad(dtpFechaNacimiento.Value);
                lblEdad.Text = $"Edad: {edad} años";
            }
            else
            {
                lblEdad.Text = "Edad: --";
            }
        }

        private void txtPeso_TextChanged(object sender, EventArgs e)
        {
            // Validar peso en tiempo real
            if (!string.IsNullOrWhiteSpace(txtPeso.Text))
            {
                if (!decimal.TryParse(txtPeso.Text, out decimal peso) || peso <= 0)
                {
                    txtPeso.BackColor = Color.LightPink;
                    ttMensaje.SetToolTip(txtPeso, "Ingrese un peso válido (números positivos)");
                }
                else
                {
                    txtPeso.BackColor = SystemColors.Window;
                    ttMensaje.SetToolTip(txtPeso, "Ingrese el peso en kilogramos");
                }
            }
            else
            {
                txtPeso.BackColor = SystemColors.Window;
            }
        }

        private void txtMicrochip_TextChanged(object sender, EventArgs e)
        {
            // Validar microchip en tiempo real
            if (!string.IsNullOrWhiteSpace(txtMicrochip.Text))
            {
                if (!NAnimal.ValidarMicrochip(txtMicrochip.Text))
                {
                    txtMicrochip.BackColor = Color.LightPink;
                    ttMensaje.SetToolTip(txtMicrochip, "El microchip debe tener 15 dígitos");
                }
                else
                {
                    txtMicrochip.BackColor = SystemColors.Window;
                    ttMensaje.SetToolTip(txtMicrochip, "Ingrese el número de microchip");
                }
            }
            else
            {
                txtMicrochip.BackColor = SystemColors.Window;
            }
        }

        private void dtpFechaNacimiento_ValueChanged(object sender, EventArgs e)
        {
            if (chkFechaNacimiento.Checked)
            {
                // Calcular y mostrar edad automáticamente
                int edad = NAnimal.CalcularEdad(dtpFechaNacimiento.Value);
                lblEdad.Text = $"Edad: {edad} años";
            }
            else
            {
                lblEdad.Text = "Edad: --";
            }
        }

        private void Habilitar(bool valor)
        {
            this.cmbPropietario.Enabled = valor && IsNuevo;
            this.txtNombre.ReadOnly = !valor;
            this.cmbTipo.Enabled = valor;
            this.cmbRaza.Enabled = valor;
            this.txtColor.ReadOnly = !valor;
            this.cmbSexo.Enabled = valor;
            this.dtpFechaNacimiento.Enabled = valor;
            this.chkFechaNacimiento.Enabled = valor;
            this.txtPeso.ReadOnly = !valor;
            this.txtAltura.ReadOnly = !valor;
            this.txtMicrochip.ReadOnly = !valor;
            this.txtNumPedigree.ReadOnly = !valor;
            this.chkEsterilizado.Enabled = valor;
            this.chkVacunado.Enabled = valor;
            this.txtObservaciones.ReadOnly = !valor;
        }

        private void Botones()
        {
            if (this.IsNuevo || this.IsEditar)
            {
                this.Habilitar(true);
                this.btnNuevo.Enabled = false;
                this.btnGuardar.Enabled = true;
                this.btnEditar.Enabled = false;
                this.btnCancelar.Enabled = true;
            }
            else
            {
                this.Habilitar(false);
                this.btnNuevo.Enabled = true;
                this.btnGuardar.Enabled = false;
                this.btnEditar.Enabled = true;
                this.btnCancelar.Enabled = false;
            }
        }

        private void OcultarColumnas()
        {
            if (dataListado.RowCount > 0)
            {
                dataListado.Columns[0].Visible = false; // Columna de eliminación
            }
        }

        private void Mostrar()
        {
            dataListado.DataSource = NAnimal.Mostrar();
            OcultarColumnas();
            lblTotal.Text = "Registros encontrados: " + Convert.ToString(dataListado.Rows.Count);
            tabControl1.SelectedIndex = 0;
        }

        private void BuscarAnimal()
        {
            dataListado.DataSource = NAnimal.BuscarTexto(txtBuscar.Text);
            OcultarColumnas();
            lblTotal.Text = "Registros encontrados: " + Convert.ToString(dataListado.Rows.Count);
        }

        private void Limpiar()
        {
            this.txtIdAnimal.Text = string.Empty;
            this.cmbPropietario.SelectedIndex = -1;
            this.txtNombre.Text = string.Empty;
            this.cmbTipo.SelectedIndex = -1;
            this.cmbRaza.SelectedIndex = -1;
            this.txtColor.Text = string.Empty;
            this.cmbSexo.SelectedIndex = -1;
            this.chkFechaNacimiento.Checked = false;
            EstablecerFechaSegura(DateTime.Today.AddYears(-1)); // Fecha válida dentro del rango
            this.dtpFechaNacimiento.Enabled = false;
            this.txtPeso.Text = string.Empty;
            this.txtAltura.Text = string.Empty;
            this.txtMicrochip.Text = string.Empty;
            this.txtNumPedigree.Text = string.Empty;
            this.chkEsterilizado.Checked = false;
            this.chkVacunado.Checked = false;
            this.txtObservaciones.Text = string.Empty;
            this.lblEdad.Text = "Edad: --";
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            BuscarAnimal();
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            BuscarAnimal();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            this.IsNuevo = true;
            this.IsEditar = false;
            this.Botones();
            this.Limpiar();
            this.Habilitar(true);
            this.cmbPropietario.Focus();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                string respuesta = "";

                // Validar campos obligatorios
                if (cmbPropietario.SelectedValue == null)
                {
                    MensajeError("Debe seleccionar un propietario");
                    cmbPropietario.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtNombre.Text))
                {
                    MensajeError("El nombre del animal es obligatorio");
                    txtNombre.Focus();
                    return;
                }

                if (cmbTipo.SelectedIndex == -1)
                {
                    MensajeError("Debe seleccionar el tipo de animal");
                    cmbTipo.Focus();
                    return;
                }

                // Validar peso si se ingresó
                decimal? peso = null;
                if (!string.IsNullOrWhiteSpace(txtPeso.Text))
                {
                    if (decimal.TryParse(txtPeso.Text, out decimal pesoValue) && pesoValue > 0)
                    {
                        peso = pesoValue;
                    }
                    else
                    {
                        MensajeError("El peso debe ser un número positivo");
                        txtPeso.Focus();
                        return;
                    }
                }

                // Validar altura si se ingresó
                decimal? altura = null;
                if (!string.IsNullOrWhiteSpace(txtAltura.Text))
                {
                    if (decimal.TryParse(txtAltura.Text, out decimal alturaValue) && alturaValue > 0)
                    {
                        altura = alturaValue;
                    }
                    else
                    {
                        MensajeError("La altura debe ser un número positivo");
                        txtAltura.Focus();
                        return;
                    }
                }

                // Validar microchip si se ingresó
                if (!string.IsNullOrWhiteSpace(txtMicrochip.Text) && !NAnimal.ValidarMicrochip(txtMicrochip.Text))
                {
                    MensajeError("El formato del microchip no es válido (debe tener 15 dígitos)");
                    txtMicrochip.Focus();
                    return;
                }

                int idPropietario = Convert.ToInt32(cmbPropietario.SelectedValue);
                string raza = cmbRaza.SelectedItem?.ToString();
                char? sexo = cmbSexo.SelectedValue as char?;
                DateTime? fechaNacimiento = chkFechaNacimiento.Checked ? dtpFechaNacimiento.Value : (DateTime?)null;

                if (this.IsNuevo)
                {
                    respuesta = NAnimal.Insertar(
                        idPropietario,
                        txtNombre.Text.Trim(),
                        cmbTipo.SelectedItem.ToString(),
                        raza,
                        string.IsNullOrWhiteSpace(txtColor.Text) ? null : txtColor.Text.Trim(),
                        sexo,
                        fechaNacimiento,
                        peso,
                        string.IsNullOrWhiteSpace(txtMicrochip.Text) ? null : txtMicrochip.Text.Trim(),
                        string.IsNullOrWhiteSpace(txtObservaciones.Text) ? null : txtObservaciones.Text.Trim()
                    );
                }
                else
                {
                    respuesta = NAnimal.Editar(
                        Convert.ToInt32(txtIdAnimal.Text),
                        txtNombre.Text.Trim(),
                        cmbTipo.SelectedItem.ToString(),
                        raza,
                        string.IsNullOrWhiteSpace(txtColor.Text) ? null : txtColor.Text.Trim(),
                        sexo,
                        fechaNacimiento,
                        peso,
                        altura,
                        string.IsNullOrWhiteSpace(txtMicrochip.Text) ? null : txtMicrochip.Text.Trim(),
                        string.IsNullOrWhiteSpace(txtNumPedigree.Text) ? null : txtNumPedigree.Text.Trim(),
                        chkEsterilizado.Checked,
                        chkVacunado.Checked,
                        string.IsNullOrWhiteSpace(txtObservaciones.Text) ? null : txtObservaciones.Text.Trim()
                    );
                }

                if (respuesta.Equals("OK"))
                {
                    if (this.IsNuevo)
                    {
                        MensajeOK("Animal registrado correctamente");
                    }
                    else
                    {
                        MensajeOK("Animal actualizado correctamente");
                    }

                    this.IsNuevo = false;
                    this.IsEditar = false;
                    this.Botones();
                    this.Limpiar();
                    this.Mostrar();
                }
                else
                {
                    MensajeError(respuesta);
                }
            }
            catch (Exception ex)
            {
                MensajeError("Error al guardar: " + ex.Message);
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtIdAnimal.Text))
            {
                this.IsEditar = true;
                this.Botones();
                this.Habilitar(true);
                this.txtNombre.Focus();
            }
            else
            {
                MensajeError("Debe seleccionar un animal para editar");
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.IsNuevo = false;
            this.IsEditar = false;
            this.Botones();
            this.Habilitar(false);
            this.Limpiar();
        }

        private void dataListado_DoubleClick(object sender, EventArgs e)
        {
            if (dataListado.CurrentRow != null)
            {
                this.txtIdAnimal.Text = Convert.ToString(this.dataListado.CurrentRow.Cells["IdAnimal"].Value);
                
                // Buscar y seleccionar propietario
                int idPropietario = Convert.ToInt32(this.dataListado.CurrentRow.Cells["IdPropietario"].Value);
                cmbPropietario.SelectedValue = idPropietario;

                this.txtNombre.Text = Convert.ToString(this.dataListado.CurrentRow.Cells["NombreAnimal"].Value);
                
                string tipo = Convert.ToString(this.dataListado.CurrentRow.Cells["Tipo"].Value);
                cmbTipo.SelectedItem = tipo;
                CargarRazasPorTipo(tipo);
                
                string raza = Convert.ToString(this.dataListado.CurrentRow.Cells["Raza"].Value);
                if (!string.IsNullOrEmpty(raza))
                    cmbRaza.SelectedItem = raza;

                this.txtColor.Text = Convert.ToString(this.dataListado.CurrentRow.Cells["Color"].Value);
                
                string sexo = Convert.ToString(this.dataListado.CurrentRow.Cells["Sexo"].Value);
                if (!string.IsNullOrEmpty(sexo))
                {
                    foreach (var item in cmbSexo.Items)
                    {
                        var sexoItem = (dynamic)item;
                        if (sexoItem.Value.ToString() == sexo)
                        {
                            cmbSexo.SelectedItem = item;
                            break;
                        }
                    }
                }

                // Manejar fecha de nacimiento
                var fechaNacimientoValue = this.dataListado.CurrentRow.Cells["FechaNacimiento"].Value;
                if (fechaNacimientoValue != DBNull.Value)
                {
                    this.dtpFechaNacimiento.Value = Convert.ToDateTime(fechaNacimientoValue);
                    this.chkFechaNacimiento.Checked = true;
                }

                var pesoValue = this.dataListado.CurrentRow.Cells["Peso"].Value;
                if (pesoValue != DBNull.Value)
                    this.txtPeso.Text = Convert.ToString(pesoValue);

                var alturaValue = this.dataListado.CurrentRow.Cells["Altura"].Value;
                if (alturaValue != DBNull.Value)
                    this.txtAltura.Text = Convert.ToString(alturaValue);

                this.txtMicrochip.Text = Convert.ToString(this.dataListado.CurrentRow.Cells["Microchip"].Value);
                this.txtNumPedigree.Text = Convert.ToString(this.dataListado.CurrentRow.Cells["NumPedigree"].Value);

                var esterilizadoValue = this.dataListado.CurrentRow.Cells["Esterilizado"].Value;
                if (esterilizadoValue != DBNull.Value)
                    this.chkEsterilizado.Checked = Convert.ToBoolean(esterilizadoValue);

                var vacunadoValue = this.dataListado.CurrentRow.Cells["Vacunado"].Value;
                if (vacunadoValue != DBNull.Value)
                    this.chkVacunado.Checked = Convert.ToBoolean(vacunadoValue);

                this.txtObservaciones.Text = Convert.ToString(this.dataListado.CurrentRow.Cells["Observaciones"].Value);

                this.tabControl1.SelectedIndex = 1;
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult opcion = MessageBox.Show("¿Desea eliminar los animales seleccionados?", 
                    "Sistema Veterinario", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

                if (opcion == DialogResult.OK)
                {
                    string codigo;
                    string respuesta = "";

                    foreach (DataGridViewRow row in dataListado.Rows)
                    {
                        if (Convert.ToBoolean(row.Cells[0].Value))
                        {
                            codigo = Convert.ToString(row.Cells["IdAnimal"].Value);
                            respuesta = NAnimal.Eliminar(Convert.ToInt32(codigo));

                            if (respuesta.Equals("OK"))
                            {
                                this.Mostrar();
                            }
                            else
                            {
                                MessageBox.Show(respuesta);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar: " + ex.Message);
            }
        }

        private void chkEliminar_CheckedChanged(object sender, EventArgs e)
        {
            if (chkEliminar.Checked)
            {
                this.dataListado.Columns[0].Visible = true;
            }
            else
            {
                this.dataListado.Columns[0].Visible = false;
            }
        }

        private void dataListado_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dataListado.Columns["Eliminar"].Index)
            {
                DataGridViewCheckBoxCell chkEliminar = 
                    (DataGridViewCheckBoxCell)dataListado.Rows[e.RowIndex].Cells["Eliminar"];
                chkEliminar.Value = !Convert.ToBoolean(chkEliminar.Value);
            }
        }
    }
}