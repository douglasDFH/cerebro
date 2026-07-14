using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using CapaNegocio;

namespace VeterinariaApp
{
    public partial class FormPersona : Form
    {
        private bool IsNuevo = false;
        private bool IsEditar = false;
        private char TipoPersonaSeleccionada = 'F'; // F = Física, J = Jurídica
        private char TipoPersonaOriginal = 'F'; // Para detectar cambios al editar
        private char GeneroSeleccionado = 'F'; // F = Femenino, M = Masculino, O = Otro
        private char FiltroActual = 'T'; // T = Todos, F = Física, J = Jurídica

        public FormPersona()
        {
            InitializeComponent();
            this.ttMensaje.SetToolTip(this.txtEmail, "Ingrese el Email de la Persona");
            this.ttMensaje.SetToolTip(this.txtDireccion, "Ingrese la Dirección de la Persona");
            this.ttMensaje.SetToolTip(this.txtTelefono, "Ingrese el Teléfono de la Persona");
            this.ttMensaje.SetToolTip(this.txtDNI, "Ingrese el DNI (solo para personas físicas)");
            this.ttMensaje.SetToolTip(this.txtCIF, "Ingrese el CIF (solo para personas jurídicas)");
            this.ttMensaje.SetToolTip(this.txtNombre, "Ingrese el nombre");
            this.ttMensaje.SetToolTip(this.txtApellidos, "Ingrese los apellidos (solo para personas físicas)");
            this.ttMensaje.SetToolTip(this.txtRazonSocial, "Ingrese la razón social (solo para personas jurídicas)");
            
            // Inicializar la vista según el tipo de persona por defecto
            MostrarCamposSegunTipo();
        }

        private void FormPersona_Load(object sender, EventArgs e)
        {
            try
            {
                ConfigurarTipoPersona();
                ConfigurarGeneros();
                CargarFiltros();
                this.Mostrar();
                this.Habilitar(false);
                this.Botones();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar formulario: " + ex.Message, 
                    "Sistema Veterinario", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConfigurarTipoPersona()
        {
            // Configurar radio buttons o combobox para tipo de persona
            rbPersonaFisica.Checked = true;
            rbPersonaJuridica.Checked = false;
            TipoPersonaSeleccionada = 'F';
            MostrarCamposSegunTipo();
        }

        private void MostrarCamposSegunTipo()
        {
            if (TipoPersonaSeleccionada == 'F') // Persona Física
            {
                // Mostrar campos para persona física
                lblDNI.Visible = txtDNI.Visible = true;
                lblNombre.Visible = txtNombre.Visible = true;
                lblApellidos.Visible = txtApellidos.Visible = true;
                groupBox3.Visible = true; // Mostrar grupo de género
                
                // Ocultar campos para persona jurídica
                lblCIF.Visible = txtCIF.Visible = false;
                lblRazonSocial.Visible = txtRazonSocial.Visible = false;
                
                // Cambiar etiquetas obligatorias
                lblDNI.Text = "DNI (*)";
                lblNombre.Text = "Nombre (*)";
                lblApellidos.Text = "Apellidos (*)";
            }
            else // Persona Jurídica
            {
                // Ocultar campos para persona física
                lblDNI.Visible = txtDNI.Visible = false;
                lblApellidos.Visible = txtApellidos.Visible = false;
                groupBox3.Visible = false; // Ocultar grupo de género
                
                // Mostrar campos para persona jurídica
                lblCIF.Visible = txtCIF.Visible = true;
                lblRazonSocial.Visible = txtRazonSocial.Visible = true;
                lblNombre.Visible = txtNombre.Visible = true;
                
                // Cambiar etiquetas obligatorias
                lblCIF.Text = "CIF (*)";
                lblRazonSocial.Text = "Razón Social (*)";
                lblNombre.Text = "Nombre Comercial";
            }
        }

        private void ConfigurarGeneros()
        {
            try
            {
                // Los géneros se manejan con RadioButtons en lugar de ComboBox
                // rbGeneroFemenino, rbGeneroMasculino, rbGeneroOtro ya están en el diseñador
            }
            catch (Exception ex)
            {
                // Control opcional, no crítico si no existe
            }
        }

        private void CargarFiltros()
        {
            try
            {
                // Los filtros se manejan con RadioButtons: rbFiltroTodos, rbFiltroFisica, rbFiltroJuridica
                // Ya están implementados en el diseñador y con sus eventos correspondientes
            }
            catch (Exception ex)
            {
                // Control opcional, no crítico si no existe
            }
        }

        private void Mostrar()
        {
            DataTable datos;
            
            switch (FiltroActual)
            {
                case 'F': // Solo personas físicas
                    datos = NPersona.BuscarPersonasFisicas();
                    break;
                case 'J': // Solo personas jurídicas
                    datos = NPersona.BuscarPersonasJuridicas();
                    break;
                default: // Todos
                    datos = NPersona.Mostrar();
                    break;
            }
            
            this.dataListado.DataSource = datos;
            this.lblTotal.Text = "Total Registros: " + Convert.ToString(dataListado.Rows.Count);
        }

        private void Buscar()
        {
            DataTable datos;
            
            if (string.IsNullOrWhiteSpace(this.txtBuscar.Text))
            {
                // Si no hay texto de búsqueda, mostrar todos según el filtro
                this.Mostrar();
                return;
            }
            
            // Buscar con filtro aplicado
            datos = NPersona.BuscarTexto(this.txtBuscar.Text);
            
            // Aplicar filtro adicional si no es "Todos"
            if (FiltroActual != 'T' && datos != null && datos.Rows.Count > 0)
            {
                // Filtrar por tipo de persona
                var filteredRows = datos.AsEnumerable().Where(row => 
                    row["TipoPersona"].ToString() == FiltroActual.ToString());
                
                if (filteredRows.Any())
                {
                    datos = filteredRows.CopyToDataTable();
                }
                else
                {
                    datos = datos.Clone(); // Tabla vacía con la misma estructura
                }
            }
            
            this.dataListado.DataSource = datos;
            this.lblTotal.Text = "Total Registros: " + Convert.ToString(dataListado.Rows.Count);
        }

        private void Habilitar(bool valor)
        {
            this.txtDNI.ReadOnly = !valor;
            this.txtCIF.ReadOnly = !valor;
            this.txtNombre.ReadOnly = !valor;
            this.txtApellidos.ReadOnly = !valor;
            this.txtRazonSocial.ReadOnly = !valor;
            this.txtEmail.ReadOnly = !valor;
            this.txtDireccion.ReadOnly = !valor;
            this.txtTelefono.ReadOnly = !valor;
            this.txtTelefonoAlternativo.ReadOnly = !valor;
            this.txtObservaciones.ReadOnly = !valor;
            
            // Habilitar RadioButtons tanto para nuevo como para editar
            this.rbPersonaFisica.Enabled = valor;
            this.rbPersonaJuridica.Enabled = valor;
            this.rbGeneroFemenino.Enabled = valor;
            this.rbGeneroMasculino.Enabled = valor;
            this.rbGeneroOtro.Enabled = valor;
        }

        private void Limpiar()
        {
            this.txtIdPersona.Text = string.Empty;
            this.txtDNI.Text = string.Empty;
            this.txtCIF.Text = string.Empty;
            this.txtNombre.Text = string.Empty;
            this.txtApellidos.Text = string.Empty;
            this.txtRazonSocial.Text = string.Empty;
            this.txtEmail.Text = string.Empty;
            this.txtDireccion.Text = string.Empty;
            this.txtTelefono.Text = string.Empty;
            this.txtTelefonoAlternativo.Text = string.Empty;
            this.txtObservaciones.Text = string.Empty;
            this.rbPersonaFisica.Checked = true;
            this.rbPersonaJuridica.Checked = false;
            this.rbGeneroFemenino.Checked = true;
            this.rbGeneroMasculino.Checked = false;
            this.rbGeneroOtro.Checked = false;
            TipoPersonaSeleccionada = 'F';
            GeneroSeleccionado = 'F';
            MostrarCamposSegunTipo();
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

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            this.Buscar();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult Opcion;
                Opcion = MessageBox.Show("¿Desea eliminar los registros seleccionados?", "Sistema Veterinaria", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

                if (Opcion == DialogResult.OK)
                {
                    string Codigo;
                    string Respuesta = "";

                    foreach (DataGridViewRow row in dataListado.Rows)
                    {
                        if (Convert.ToBoolean(row.Cells[0].Value))
                        {
                            Codigo = Convert.ToString(row.Cells[1].Value);
                            Respuesta = NPersona.Eliminar(Convert.ToInt32(Codigo));

                            if (Respuesta.Equals("OK"))
                            {
                                this.Mostrar();
                            }
                            else
                            {
                                MessageBox.Show(Respuesta);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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
                DataGridViewCheckBoxCell ChkEliminar = (DataGridViewCheckBoxCell)dataListado.Rows[e.RowIndex].Cells["Eliminar"];
                ChkEliminar.Value = !Convert.ToBoolean(ChkEliminar.Value);
            }
        }
        
        private void dataListado_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Solo procesar si se hace clic en una fila válida y no en la columna de eliminar
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0 && 
                e.ColumnIndex != dataListado.Columns["Eliminar"].Index)
            {
                // Seleccionar la fila
                dataListado.CurrentCell = dataListado.Rows[e.RowIndex].Cells[e.ColumnIndex];
                
                // Llamar al mismo método que el doble clic
                dataListado_DoubleClick(sender, e);
            }
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            this.IsNuevo = true;
            this.IsEditar = false;
            this.Limpiar();
            this.Habilitar(true);
            this.Botones();
            this.txtEmail.Focus();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                string respuesta = "";
                
                if (this.IsNuevo)
                {
                    if (TipoPersonaSeleccionada == 'F') // Persona Física
                    {
                        if (string.IsNullOrWhiteSpace(txtDNI.Text) || 
                            string.IsNullOrWhiteSpace(txtNombre.Text) || 
                            string.IsNullOrWhiteSpace(txtApellidos.Text))
                        {
                            MessageBox.Show("DNI, nombre y apellidos son obligatorios para personas físicas", 
                                "Sistema Veterinario", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        if (!NPersona.ValidarDNI(txtDNI.Text))
                        {
                            MessageBox.Show("El formato del DNI no es válido", 
                                "Sistema Veterinario", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        respuesta = NPersona.InsertarPersonaFisica(
                            txtDNI.Text.Trim(),
                            txtNombre.Text.Trim(),
                            txtApellidos.Text.Trim(),
                            GeneroSeleccionado,
                            string.IsNullOrWhiteSpace(txtEmail.Text) ? null : txtEmail.Text.Trim(),
                            string.IsNullOrWhiteSpace(txtDireccion.Text) ? null : txtDireccion.Text.Trim(),
                            string.IsNullOrWhiteSpace(txtTelefono.Text) ? null : txtTelefono.Text.Trim(),
                            string.IsNullOrWhiteSpace(txtObservaciones.Text) ? null : txtObservaciones.Text.Trim()
                        );
                    }
                    else // Persona Jurídica
                    {
                        if (string.IsNullOrWhiteSpace(txtCIF.Text) || 
                            string.IsNullOrWhiteSpace(txtRazonSocial.Text))
                        {
                            MessageBox.Show("CIF y razón social son obligatorios para personas jurídicas", 
                                "Sistema Veterinario", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        if (!NPersona.ValidarCIF(txtCIF.Text))
                        {
                            MessageBox.Show("El formato del CIF no es válido", 
                                "Sistema Veterinario", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        respuesta = NPersona.InsertarPersonaJuridica(
                            txtCIF.Text.Trim(),
                            txtRazonSocial.Text.Trim(),
                            string.IsNullOrWhiteSpace(txtEmail.Text) ? null : txtEmail.Text.Trim(),
                            string.IsNullOrWhiteSpace(txtDireccion.Text) ? null : txtDireccion.Text.Trim(),
                            string.IsNullOrWhiteSpace(txtTelefono.Text) ? null : txtTelefono.Text.Trim(),
                            string.IsNullOrWhiteSpace(txtObservaciones.Text) ? null : txtObservaciones.Text.Trim()
                        );
                    }
                }
                else // Editar
                {
                    // Verificar si se cambió el tipo de persona
                    if (TipoPersonaSeleccionada != TipoPersonaOriginal)
                    {
                        // Cambio de tipo de persona
                        if (TipoPersonaSeleccionada == 'F') // Cambio a Persona Física
                        {
                            if (string.IsNullOrWhiteSpace(txtDNI.Text) || 
                                string.IsNullOrWhiteSpace(txtNombre.Text) || 
                                string.IsNullOrWhiteSpace(txtApellidos.Text))
                            {
                                MessageBox.Show("DNI, nombre y apellidos son obligatorios para personas físicas", 
                                    "Sistema Veterinario", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }

                            if (!NPersona.ValidarDNI(txtDNI.Text))
                            {
                                MessageBox.Show("El formato del DNI no es válido", 
                                    "Sistema Veterinario", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }

                            respuesta = NPersona.CambiarTipoPersona(
                                Convert.ToInt32(this.txtIdPersona.Text),
                                'F',
                                txtDNI.Text.Trim(),
                                txtNombre.Text.Trim(),
                                txtApellidos.Text.Trim(),
                                GeneroSeleccionado,
                                null, // CIF
                                null, // RazonSocial
                                string.IsNullOrWhiteSpace(txtEmail.Text) ? null : txtEmail.Text.Trim(),
                                string.IsNullOrWhiteSpace(txtDireccion.Text) ? null : txtDireccion.Text.Trim(),
                                string.IsNullOrWhiteSpace(txtTelefono.Text) ? null : txtTelefono.Text.Trim(),
                                string.IsNullOrWhiteSpace(txtTelefonoAlternativo.Text) ? null : txtTelefonoAlternativo.Text.Trim(),
                                string.IsNullOrWhiteSpace(txtObservaciones.Text) ? null : txtObservaciones.Text.Trim()
                            );
                        }
                        else // Cambio a Persona Jurídica
                        {
                            if (string.IsNullOrWhiteSpace(txtCIF.Text) || 
                                string.IsNullOrWhiteSpace(txtRazonSocial.Text))
                            {
                                MessageBox.Show("CIF y razón social son obligatorios para personas jurídicas", 
                                    "Sistema Veterinario", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }

                            if (!NPersona.ValidarCIF(txtCIF.Text))
                            {
                                MessageBox.Show("El formato del CIF no es válido", 
                                    "Sistema Veterinario", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }

                            respuesta = NPersona.CambiarTipoPersona(
                                Convert.ToInt32(this.txtIdPersona.Text),
                                'J',
                                null, // DNI
                                string.IsNullOrWhiteSpace(txtNombre.Text) ? null : txtNombre.Text.Trim(),
                                null, // Apellidos
                                'F', // Género por defecto
                                txtCIF.Text.Trim(),
                                txtRazonSocial.Text.Trim(),
                                string.IsNullOrWhiteSpace(txtEmail.Text) ? null : txtEmail.Text.Trim(),
                                string.IsNullOrWhiteSpace(txtDireccion.Text) ? null : txtDireccion.Text.Trim(),
                                string.IsNullOrWhiteSpace(txtTelefono.Text) ? null : txtTelefono.Text.Trim(),
                                string.IsNullOrWhiteSpace(txtTelefonoAlternativo.Text) ? null : txtTelefonoAlternativo.Text.Trim(),
                                string.IsNullOrWhiteSpace(txtObservaciones.Text) ? null : txtObservaciones.Text.Trim()
                            );
                        }
                    }
                    else
                    {
                        // Edición normal sin cambio de tipo
                        respuesta = NPersona.Editar(
                            Convert.ToInt32(this.txtIdPersona.Text),
                            string.IsNullOrWhiteSpace(txtEmail.Text) ? null : txtEmail.Text.Trim(),
                            string.IsNullOrWhiteSpace(txtDireccion.Text) ? null : txtDireccion.Text.Trim(),
                            string.IsNullOrWhiteSpace(txtTelefono.Text) ? null : txtTelefono.Text.Trim(),
                            string.IsNullOrWhiteSpace(txtTelefonoAlternativo.Text) ? null : txtTelefonoAlternativo.Text.Trim(),
                            string.IsNullOrWhiteSpace(txtObservaciones.Text) ? null : txtObservaciones.Text.Trim()
                        );
                    }
                }

                if (respuesta.Equals("OK"))
                {
                    if (this.IsNuevo)
                    {
                        MessageBox.Show("Persona registrada correctamente", "Sistema Veterinario", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Persona actualizada correctamente", "Sistema Veterinario", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    this.IsNuevo = false;
                    this.IsEditar = false;
                    this.Botones();
                    this.Limpiar();
                    this.Mostrar();
                    
                    // Navegar automáticamente a la pestaña de lista después del mensaje de éxito
                    this.tabControl1.SelectedIndex = 0;
                }
                else
                {
                    MessageBox.Show(respuesta, "Sistema Veterinario", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar: " + ex.Message, "Sistema Veterinario", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (!this.txtIdPersona.Text.Equals(""))
            {
                this.IsEditar = true;
                this.Botones();
                this.Habilitar(true);
            }
            else
            {
                MessageBox.Show("Debe seleccionar un registro para editar", "Sistema Veterinaria", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                // Obtener información básica para mostrar en el diálogo
                string idPersona = Convert.ToString(this.dataListado.CurrentRow.Cells["IdPersona"].Value);
                char tipoPersona = Convert.ToChar(this.dataListado.CurrentRow.Cells["TipoPersona"].Value);
                string nombre = Convert.ToString(this.dataListado.CurrentRow.Cells["Nombre"].Value);
                
                string tipoTexto = tipoPersona == 'F' ? "Persona Física" : "Persona Jurídica";
                string mensaje = $"¿Desea editar los datos de esta {tipoTexto}?\n\n" +
                               $"ID: {idPersona}\n" +
                               $"Nombre: {nombre}";
                
                DialogResult resultado = MessageBox.Show(
                    mensaje,
                    "Editar Persona - Sistema Veterinario",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );
                
                if (resultado == DialogResult.Yes)
                {
                    // Usuario confirmó que quiere editar - cargar datos
                    CargarDatosPersonaSeleccionada();
                }
                // Si dice No, no hace nada y se queda en la lista
            }
        }
        
        private void CargarDatosPersonaSeleccionada()
        {
            if (dataListado.CurrentRow != null)
            {
                this.txtIdPersona.Text = Convert.ToString(this.dataListado.CurrentRow.Cells["IdPersona"].Value);
                
                char tipoPersona = Convert.ToChar(this.dataListado.CurrentRow.Cells["TipoPersona"].Value);
                TipoPersonaSeleccionada = tipoPersona;
                TipoPersonaOriginal = tipoPersona; // Guardar el tipo original
                
                if (tipoPersona == 'F')
                {
                    rbPersonaFisica.Checked = true;
                    rbPersonaJuridica.Checked = false;
                    txtDNI.Text = Convert.ToString(this.dataListado.CurrentRow.Cells["DNI"].Value);
                    txtNombre.Text = Convert.ToString(this.dataListado.CurrentRow.Cells["Nombre"].Value);
                    txtApellidos.Text = Convert.ToString(this.dataListado.CurrentRow.Cells["Apellidos"].Value);
                    
                    // Cargar género si existe
                    if (this.dataListado.CurrentRow.Cells["Genero"].Value != null && 
                        this.dataListado.CurrentRow.Cells["Genero"].Value != DBNull.Value)
                    {
                        char genero = Convert.ToChar(this.dataListado.CurrentRow.Cells["Genero"].Value);
                        GeneroSeleccionado = genero;
                        switch (genero)
                        {
                            case 'F':
                                rbGeneroFemenino.Checked = true;
                                break;
                            case 'M':
                                rbGeneroMasculino.Checked = true;
                                break;
                            case 'O':
                                rbGeneroOtro.Checked = true;
                                break;
                        }
                    }
                    else
                    {
                        // Valor por defecto si no hay género asignado
                        rbGeneroFemenino.Checked = true;
                        GeneroSeleccionado = 'F';
                    }
                }
                else
                {
                    rbPersonaFisica.Checked = false;
                    rbPersonaJuridica.Checked = true;
                    txtCIF.Text = Convert.ToString(this.dataListado.CurrentRow.Cells["CIF"].Value);
                    txtRazonSocial.Text = Convert.ToString(this.dataListado.CurrentRow.Cells["RazonSocial"].Value);
                    txtNombre.Text = Convert.ToString(this.dataListado.CurrentRow.Cells["Nombre"].Value);
                }
                
                MostrarCamposSegunTipo();
                
                this.txtEmail.Text = Convert.ToString(this.dataListado.CurrentRow.Cells["Email"].Value);
                this.txtDireccion.Text = Convert.ToString(this.dataListado.CurrentRow.Cells["Direccion"].Value);
                this.txtTelefono.Text = Convert.ToString(this.dataListado.CurrentRow.Cells["Telefono"].Value);
                this.txtTelefonoAlternativo.Text = Convert.ToString(this.dataListado.CurrentRow.Cells["TelefonoAlternativo"].Value);
                this.txtObservaciones.Text = Convert.ToString(this.dataListado.CurrentRow.Cells["Observaciones"].Value);

                // Cambiar a la pestaña de mantenimiento
                this.tabControl1.SelectedIndex = 1;
            }
        }

        private void rbPersonaFisica_CheckedChanged(object sender, EventArgs e)
        {
            if (rbPersonaFisica.Checked)
            {
                // Si está editando y cambia de jurídica a física, confirmar
                if (IsEditar && TipoPersonaOriginal == 'J' && TipoPersonaSeleccionada != 'F')
                {
                    DialogResult resultado = MessageBox.Show(
                        "¿Está seguro de cambiar de Persona Jurídica a Persona Física?\n\n" +
                        "ADVERTENCIA: Se perderán los siguientes datos:\n" +
                        "- CIF\n" +
                        "- Razón Social\n\n" +
                        "Deberá ingresar:\n" +
                        "- DNI\n" +
                        "- Nombre\n" +
                        "- Apellidos\n" +
                        "- Género",
                        "Cambio de Tipo de Persona - Sistema Veterinario",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Warning
                    );
                    
                    if (resultado == DialogResult.No)
                    {
                        // Revertir el cambio
                        rbPersonaJuridica.Checked = true;
                        return;
                    }
                }
                
                TipoPersonaSeleccionada = 'F';
                MostrarCamposSegunTipo();
                
                if (IsNuevo) 
                {
                    // Limpiar solo los campos específicos, no todos
                    txtDNI.Text = string.Empty;
                    txtNombre.Text = string.Empty;
                    txtApellidos.Text = string.Empty;
                    rbGeneroFemenino.Checked = true;
                    GeneroSeleccionado = 'F';
                }
                else if (IsEditar && TipoPersonaOriginal == 'J')
                {
                    // Limpiar datos de persona jurídica al cambiar
                    txtCIF.Text = string.Empty;
                    txtRazonSocial.Text = string.Empty;
                    txtDNI.Text = string.Empty;
                    txtNombre.Text = string.Empty;
                    txtApellidos.Text = string.Empty;
                    rbGeneroFemenino.Checked = true;
                    GeneroSeleccionado = 'F';
                }
            }
        }

        private void rbPersonaJuridica_CheckedChanged(object sender, EventArgs e)
        {
            if (rbPersonaJuridica.Checked)
            {
                // Si está editando y cambia de física a jurídica, confirmar
                if (IsEditar && TipoPersonaOriginal == 'F' && TipoPersonaSeleccionada != 'J')
                {
                    DialogResult resultado = MessageBox.Show(
                        "¿Está seguro de cambiar de Persona Física a Persona Jurídica?\n\n" +
                        "ADVERTENCIA: Se perderán los siguientes datos:\n" +
                        "- DNI\n" +
                        "- Apellidos\n" +
                        "- Género\n\n" +
                        "Deberá ingresar:\n" +
                        "- CIF\n" +
                        "- Razón Social",
                        "Cambio de Tipo de Persona - Sistema Veterinario",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Warning
                    );
                    
                    if (resultado == DialogResult.No)
                    {
                        // Revertir el cambio
                        rbPersonaFisica.Checked = true;
                        return;
                    }
                }
                
                TipoPersonaSeleccionada = 'J';
                MostrarCamposSegunTipo();
                
                if (IsNuevo) 
                {
                    // Limpiar solo los campos específicos, no todos
                    txtCIF.Text = string.Empty;
                    txtRazonSocial.Text = string.Empty;
                    txtNombre.Text = string.Empty;
                }
                else if (IsEditar && TipoPersonaOriginal == 'F')
                {
                    // Limpiar datos de persona física al cambiar
                    txtDNI.Text = string.Empty;
                    txtApellidos.Text = string.Empty;
                    txtCIF.Text = string.Empty;
                    txtRazonSocial.Text = string.Empty;
                    txtNombre.Text = string.Empty;
                }
            }
        }

        private void lblTotal_Click(object sender, EventArgs e)
        {

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void chkEliminar_CheckedChanged_1(object sender, EventArgs e)
        {

        }

        private void btnEliminar_Click_1(object sender, EventArgs e)
        {

        }

        private void btnBuscar_Click_1(object sender, EventArgs e)
        {

        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataListado_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void txtTelefono_TextChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void txtDireccion_TextChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void txtEmail_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void txtIdPersona_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void btnCancelar_Click_1(object sender, EventArgs e)
        {

        }

        private void btnEditar_Click_1(object sender, EventArgs e)
        {

        }

        private void btnGuardar_Click_1(object sender, EventArgs e)
        {

        }

        private void btnNuevo_Click_1(object sender, EventArgs e)
        {

        }

        private void ttMensaje_Popup(object sender, PopupEventArgs e)
        {

        }

        private void rbGeneroFemenino_CheckedChanged(object sender, EventArgs e)
        {
            if (rbGeneroFemenino.Checked)
            {
                GeneroSeleccionado = 'F';
            }
        }

        private void rbGeneroMasculino_CheckedChanged(object sender, EventArgs e)
        {
            if (rbGeneroMasculino.Checked)
            {
                GeneroSeleccionado = 'M';
            }
        }

        private void rbGeneroOtro_CheckedChanged(object sender, EventArgs e)
        {
            if (rbGeneroOtro.Checked)
            {
                GeneroSeleccionado = 'O';
            }
        }

        private void rbFiltroTodos_CheckedChanged(object sender, EventArgs e)
        {
            if (rbFiltroTodos.Checked)
            {
                FiltroActual = 'T';
                if (!string.IsNullOrWhiteSpace(this.txtBuscar.Text))
                {
                    this.Buscar();
                }
                else
                {
                    this.Mostrar();
                }
            }
        }

        private void rbFiltroFisica_CheckedChanged(object sender, EventArgs e)
        {
            if (rbFiltroFisica.Checked)
            {
                FiltroActual = 'F';
                if (!string.IsNullOrWhiteSpace(this.txtBuscar.Text))
                {
                    this.Buscar();
                }
                else
                {
                    this.Mostrar();
                }
            }
        }

        private void rbFiltroJuridica_CheckedChanged(object sender, EventArgs e)
        {
            if (rbFiltroJuridica.Checked)
            {
                FiltroActual = 'J';
                if (!string.IsNullOrWhiteSpace(this.txtBuscar.Text))
                {
                    this.Buscar();
                }
                else
                {
                    this.Mostrar();
                }
            }
        }

    }
}