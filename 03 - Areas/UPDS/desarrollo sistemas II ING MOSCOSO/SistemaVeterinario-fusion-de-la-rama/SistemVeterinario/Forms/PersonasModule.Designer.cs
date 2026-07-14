namespace SistemVeterinario.Forms
{
    partial class PersonasModule
    {
        /// <summary> 
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de componentes

        /// <summary> 
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private new void InitializeComponent()
        {
            cmbTipoPersona = new ComboBox();
            lblTipoPersona = new Label();
            lblContador = new Label();
            lblTipoPersonaForm = new Label();
            cmbTipoPersonaForm = new ComboBox();
            grpPersonaFisica = new GroupBox();
            lblCi = new Label();
            txtCi = new TextBox();
            lblNombre = new Label();
            txtNombre = new TextBox();
            lblApellido = new Label();
            txtApellido = new TextBox();
            lblFechaNacimiento = new Label();
            dtpFechaNacimiento = new DateTimePicker();
            lblGenero = new Label();
            cmbGenero = new ComboBox();
            grpPersonaJuridica = new GroupBox();
            lblRazonSocial = new Label();
            txtRazonSocial = new TextBox();
            lblNit = new Label();
            txtNit = new TextBox();
            lblEncargadoNombre = new Label();
            txtEncargadoNombre = new TextBox();
            lblEncargadoCargo = new Label();
            txtEncargadoCargo = new TextBox();
            grpDatosComunes = new GroupBox();
            lblEmail = new Label();
            txtEmail = new TextBox();
            lblDireccion = new Label();
            txtDireccion = new TextBox();
            lblTelefono = new Label();
            txtTelefono = new TextBox();
            tabInicio.SuspendLayout();
            panelBusqueda.SuspendLayout();
            tabConfiguraciones.SuspendLayout();
            panelFormulario.SuspendLayout();
            panelSuperior.SuspendLayout();
            panelBotones.SuspendLayout();
            grpPersonaFisica.SuspendLayout();
            grpPersonaJuridica.SuspendLayout();
            grpDatosComunes.SuspendLayout();
            SuspendLayout();
            // 
            // tabInicio
            // 
            tabInicio.Text = "Gestión de Personas";
            // 
            // tabConfiguraciones
            // 
            tabConfiguraciones.Text = "Configuración de Persona";
            // 
            // panelBusqueda
            // 
            panelBusqueda.Controls.Add(lblContador);
            panelBusqueda.Controls.Add(lblTipoPersona);
            panelBusqueda.Controls.Add(cmbTipoPersona);
            panelBusqueda.Size = new Size(960, 80);
            // 
            // txtBuscar
            // 
            txtBuscar.PlaceholderText = "Buscar por nombre, CI, NIT, email, razón social...";
            txtBuscar.Size = new Size(350, 23);
            // 
            // btnBuscar
            // 
            btnBuscar.Location = new Point(360, 17);
            // 
            // btnNuevo
            // 
            btnNuevo.Location = new Point(860, 17);
            // 
            // chkMostrarTodo
            // 
            chkMostrarTodo.Location = new Point(470, 20);
            // 
            // panelFormulario
            // 
            panelFormulario.Controls.Add(grpDatosComunes);
            panelFormulario.Controls.Add(grpPersonaJuridica);
            panelFormulario.Controls.Add(grpPersonaFisica);
            panelFormulario.Controls.Add(cmbTipoPersonaForm);
            panelFormulario.Controls.Add(lblTipoPersonaForm);
            // 
            // panelSuperior
            // 
            panelSuperior.Size = new Size(992, 50);
            // 
            // cmbTipoPersona
            // 
            cmbTipoPersona.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbTipoPersona.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            cmbTipoPersona.ForeColor = Color.Black;
            cmbTipoPersona.BackColor = Color.White;
            cmbTipoPersona.Location = new Point(10, 45);
            cmbTipoPersona.Name = "cmbTipoPersona";
            cmbTipoPersona.Size = new Size(150, 23);
            cmbTipoPersona.TabIndex = 4;
            // 
            // lblTipoPersona
            // 
            lblTipoPersona.AutoSize = true;
            lblTipoPersona.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            lblTipoPersona.ForeColor = Color.Black;
            lblTipoPersona.Location = new Point(10, 27);
            lblTipoPersona.Name = "lblTipoPersona";
            lblTipoPersona.Size = new Size(91, 15);
            lblTipoPersona.TabIndex = 5;
            lblTipoPersona.Text = "Tipo de Persona:";
            // 
            // lblContador
            // 
            lblContador.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblContador.AutoSize = true;
            lblContador.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            lblContador.Location = new Point(750, 50);
            lblContador.Name = "lblContador";
            lblContador.Size = new Size(121, 15);
            lblContador.TabIndex = 6;
            lblContador.Text = "Total de registros: 0";
            // 
            // lblTipoPersonaForm
            // 
            lblTipoPersonaForm.AutoSize = true;
            lblTipoPersonaForm.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            lblTipoPersonaForm.ForeColor = Color.DarkBlue;
            lblTipoPersonaForm.Location = new Point(15, 65);
            lblTipoPersonaForm.Name = "lblTipoPersonaForm";
            lblTipoPersonaForm.Size = new Size(106, 15);
            lblTipoPersonaForm.TabIndex = 0;
            lblTipoPersonaForm.Text = "Tipo de Persona *";
            // 
            // cmbTipoPersonaForm
            // 
            cmbTipoPersonaForm.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbTipoPersonaForm.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            cmbTipoPersonaForm.ForeColor = Color.Black;
            cmbTipoPersonaForm.BackColor = Color.White;
            cmbTipoPersonaForm.Location = new Point(130, 62);
            cmbTipoPersonaForm.Name = "cmbTipoPersonaForm";
            cmbTipoPersonaForm.Size = new Size(150, 23);
            cmbTipoPersonaForm.TabIndex = 1;
            // 
            // grpPersonaFisica
            // 
            grpPersonaFisica.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            grpPersonaFisica.Controls.Add(cmbGenero);
            grpPersonaFisica.Controls.Add(lblGenero);
            grpPersonaFisica.Controls.Add(dtpFechaNacimiento);
            grpPersonaFisica.Controls.Add(lblFechaNacimiento);
            grpPersonaFisica.Controls.Add(txtApellido);
            grpPersonaFisica.Controls.Add(lblApellido);
            grpPersonaFisica.Controls.Add(txtNombre);
            grpPersonaFisica.Controls.Add(lblNombre);
            grpPersonaFisica.Controls.Add(txtCi);
            grpPersonaFisica.Controls.Add(lblCi);
            grpPersonaFisica.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            grpPersonaFisica.ForeColor = Color.DarkBlue;
            grpPersonaFisica.Location = new Point(15, 100);
            grpPersonaFisica.Name = "grpPersonaFisica";
            grpPersonaFisica.Size = new Size(960, 120);
            grpPersonaFisica.TabIndex = 2;
            grpPersonaFisica.TabStop = false;
            grpPersonaFisica.Text = "Datos de Persona Física";
            // 
            // lblCi
            // 
            lblCi.AutoSize = true;
            lblCi.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            lblCi.ForeColor = Color.Black;
            lblCi.Location = new Point(15, 25);
            lblCi.Name = "lblCi";
            lblCi.Size = new Size(24, 15);
            lblCi.TabIndex = 0;
            lblCi.Text = "CI:";
            // 
            // txtCi
            // 
            txtCi.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            txtCi.ForeColor = Color.Black;
            txtCi.BackColor = Color.White;
            txtCi.Location = new Point(15, 45);
            txtCi.MaxLength = 15;
            txtCi.Name = "txtCi";
            txtCi.Size = new Size(150, 23);
            txtCi.TabIndex = 1;
            // 
            // lblNombre
            // 
            lblNombre.AutoSize = true;
            lblNombre.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            lblNombre.ForeColor = Color.DarkRed;
            lblNombre.Location = new Point(180, 25);
            lblNombre.Name = "lblNombre";
            lblNombre.Size = new Size(59, 15);
            lblNombre.TabIndex = 2;
            lblNombre.Text = "Nombre *";
            // 
            // txtNombre
            // 
            txtNombre.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            txtNombre.ForeColor = Color.Black;
            txtNombre.BackColor = Color.White;
            txtNombre.Location = new Point(180, 45);
            txtNombre.MaxLength = 100;
            txtNombre.Name = "txtNombre";
            txtNombre.Size = new Size(200, 23);
            txtNombre.TabIndex = 3;
            // 
            // lblApellido
            // 
            lblApellido.AutoSize = true;
            lblApellido.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            lblApellido.ForeColor = Color.DarkRed;
            lblApellido.Location = new Point(395, 25);
            lblApellido.Name = "lblApellido";
            lblApellido.Size = new Size(60, 15);
            lblApellido.TabIndex = 4;
            lblApellido.Text = "Apellido *";
            // 
            // txtApellido
            // 
            txtApellido.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtApellido.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            txtApellido.ForeColor = Color.Black;
            txtApellido.BackColor = Color.White;
            txtApellido.Location = new Point(395, 45);
            txtApellido.MaxLength = 100;
            txtApellido.Name = "txtApellido";
            txtApellido.Size = new Size(200, 23);
            txtApellido.TabIndex = 5;
            // 
            // lblFechaNacimiento
            // 
            lblFechaNacimiento.AutoSize = true;
            lblFechaNacimiento.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            lblFechaNacimiento.ForeColor = Color.Black;
            lblFechaNacimiento.Location = new Point(15, 80);
            lblFechaNacimiento.Name = "lblFechaNacimiento";
            lblFechaNacimiento.Size = new Size(119, 15);
            lblFechaNacimiento.TabIndex = 6;
            lblFechaNacimiento.Text = "Fecha de Nacimiento:";
            // 
            // dtpFechaNacimiento
            // 
            dtpFechaNacimiento.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            dtpFechaNacimiento.Format = DateTimePickerFormat.Short;
            dtpFechaNacimiento.Location = new Point(140, 77);
            dtpFechaNacimiento.Name = "dtpFechaNacimiento";
            dtpFechaNacimiento.Size = new Size(120, 23);
            dtpFechaNacimiento.TabIndex = 7;
            // 
            // lblGenero
            // 
            lblGenero.AutoSize = true;
            lblGenero.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            lblGenero.ForeColor = Color.Black;
            lblGenero.Location = new Point(280, 80);
            lblGenero.Name = "lblGenero";
            lblGenero.Size = new Size(48, 15);
            lblGenero.TabIndex = 8;
            lblGenero.Text = "Género:";
            // 
            // cmbGenero
            // 
            cmbGenero.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbGenero.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            cmbGenero.ForeColor = Color.Black;
            cmbGenero.BackColor = Color.White;
            cmbGenero.Location = new Point(335, 77);
            cmbGenero.Name = "cmbGenero";
            cmbGenero.Size = new Size(80, 23);
            cmbGenero.TabIndex = 9;
            // 
            // grpPersonaJuridica
            // 
            grpPersonaJuridica.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            grpPersonaJuridica.Controls.Add(txtEncargadoCargo);
            grpPersonaJuridica.Controls.Add(lblEncargadoCargo);
            grpPersonaJuridica.Controls.Add(txtEncargadoNombre);
            grpPersonaJuridica.Controls.Add(lblEncargadoNombre);
            grpPersonaJuridica.Controls.Add(txtNit);
            grpPersonaJuridica.Controls.Add(lblNit);
            grpPersonaJuridica.Controls.Add(txtRazonSocial);
            grpPersonaJuridica.Controls.Add(lblRazonSocial);
            grpPersonaJuridica.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            grpPersonaJuridica.ForeColor = Color.DarkGreen;
            grpPersonaJuridica.Location = new Point(15, 100);
            grpPersonaJuridica.Name = "grpPersonaJuridica";
            grpPersonaJuridica.Size = new Size(960, 120);
            grpPersonaJuridica.TabIndex = 3;
            grpPersonaJuridica.TabStop = false;
            grpPersonaJuridica.Text = "Datos de Persona Jurídica";
            grpPersonaJuridica.Visible = false;
            // 
            // lblRazonSocial
            // 
            lblRazonSocial.AutoSize = true;
            lblRazonSocial.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            lblRazonSocial.ForeColor = Color.DarkRed;
            lblRazonSocial.Location = new Point(15, 25);
            lblRazonSocial.Name = "lblRazonSocial";
            lblRazonSocial.Size = new Size(86, 15);
            lblRazonSocial.TabIndex = 0;
            lblRazonSocial.Text = "Razón Social *";
            // 
            // txtRazonSocial
            // 
            txtRazonSocial.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtRazonSocial.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            txtRazonSocial.Location = new Point(15, 45);
            txtRazonSocial.MaxLength = 255;
            txtRazonSocial.Name = "txtRazonSocial";
            txtRazonSocial.Size = new Size(400, 23);
            txtRazonSocial.TabIndex = 1;
            // 
            // lblNit
            // 
            lblNit.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblNit.AutoSize = true;
            lblNit.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            lblNit.ForeColor = Color.Black;
            lblNit.Location = new Point(435, 25);
            lblNit.Name = "lblNit";
            lblNit.Size = new Size(28, 15);
            lblNit.TabIndex = 2;
            lblNit.Text = "NIT:";
            // 
            // txtNit
            // 
            txtNit.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            txtNit.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            txtNit.Location = new Point(435, 45);
            txtNit.MaxLength = 15;
            txtNit.Name = "txtNit";
            txtNit.Size = new Size(150, 23);
            txtNit.TabIndex = 3;
            // 
            // lblEncargadoNombre
            // 
            lblEncargadoNombre.AutoSize = true;
            lblEncargadoNombre.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            lblEncargadoNombre.ForeColor = Color.Black;
            lblEncargadoNombre.Location = new Point(15, 80);
            lblEncargadoNombre.Name = "lblEncargadoNombre";
            lblEncargadoNombre.Size = new Size(128, 15);
            lblEncargadoNombre.TabIndex = 4;
            lblEncargadoNombre.Text = "Nombre del Encargado:";
            // 
            // txtEncargadoNombre
            // 
            txtEncargadoNombre.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            txtEncargadoNombre.Location = new Point(150, 77);
            txtEncargadoNombre.MaxLength = 100;
            txtEncargadoNombre.Name = "txtEncargadoNombre";
            txtEncargadoNombre.Size = new Size(200, 23);
            txtEncargadoNombre.TabIndex = 5;
            // 
            // lblEncargadoCargo
            // 
            lblEncargadoCargo.AutoSize = true;
            lblEncargadoCargo.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            lblEncargadoCargo.ForeColor = Color.Black;
            lblEncargadoCargo.Location = new Point(370, 80);
            lblEncargadoCargo.Name = "lblEncargadoCargo";
            lblEncargadoCargo.Size = new Size(113, 15);
            lblEncargadoCargo.TabIndex = 6;
            lblEncargadoCargo.Text = "Cargo del Encargado:";
            // 
            // txtEncargadoCargo
            // 
            txtEncargadoCargo.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtEncargadoCargo.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            txtEncargadoCargo.Location = new Point(490, 77);
            txtEncargadoCargo.MaxLength = 100;
            txtEncargadoCargo.Name = "txtEncargadoCargo";
            txtEncargadoCargo.Size = new Size(200, 23);
            txtEncargadoCargo.TabIndex = 7;
            // 
            // grpDatosComunes
            // 
            grpDatosComunes.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            grpDatosComunes.Controls.Add(txtTelefono);
            grpDatosComunes.Controls.Add(lblTelefono);
            grpDatosComunes.Controls.Add(txtDireccion);
            grpDatosComunes.Controls.Add(lblDireccion);
            grpDatosComunes.Controls.Add(txtEmail);
            grpDatosComunes.Controls.Add(lblEmail);
            grpDatosComunes.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            grpDatosComunes.ForeColor = Color.DarkSlateGray;
            grpDatosComunes.Location = new Point(15, 235);
            grpDatosComunes.Name = "grpDatosComunes";
            grpDatosComunes.Size = new Size(960, 150);
            grpDatosComunes.TabIndex = 4;
            grpDatosComunes.TabStop = false;
            grpDatosComunes.Text = "Datos de Contacto";
            // 
            // lblEmail
            // 
            lblEmail.AutoSize = true;
            lblEmail.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            lblEmail.ForeColor = Color.Black;
            lblEmail.Location = new Point(15, 25);
            lblEmail.Name = "lblEmail";
            lblEmail.Size = new Size(39, 15);
            lblEmail.TabIndex = 0;
            lblEmail.Text = "Email:";
            // 
            // txtEmail
            // 
            txtEmail.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtEmail.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            txtEmail.ForeColor = Color.Black;
            txtEmail.BackColor = Color.White;
            txtEmail.Location = new Point(15, 45);
            txtEmail.MaxLength = 100;
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new Size(400, 23);
            txtEmail.TabIndex = 1;
            // 
            // lblDireccion
            // 
            lblDireccion.AutoSize = true;
            lblDireccion.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            lblDireccion.ForeColor = Color.Black;
            lblDireccion.Location = new Point(15, 80);
            lblDireccion.Name = "lblDireccion";
            lblDireccion.Size = new Size(60, 15);
            lblDireccion.TabIndex = 2;
            lblDireccion.Text = "Dirección:";
            // 
            // txtDireccion
            // 
            txtDireccion.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtDireccion.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            txtDireccion.ForeColor = Color.Black;
            txtDireccion.BackColor = Color.White;
            txtDireccion.Location = new Point(15, 100);
            txtDireccion.MaxLength = 255;
            txtDireccion.Multiline = true;
            txtDireccion.Name = "txtDireccion";
            txtDireccion.ScrollBars = ScrollBars.Vertical;
            txtDireccion.Size = new Size(600, 40);
            txtDireccion.TabIndex = 3;
            // 
            // lblTelefono
            // 
            lblTelefono.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblTelefono.AutoSize = true;
            lblTelefono.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            lblTelefono.ForeColor = Color.Black;
            lblTelefono.Location = new Point(650, 80);
            lblTelefono.Name = "lblTelefono";
            lblTelefono.Size = new Size(55, 15);
            lblTelefono.TabIndex = 4;
            lblTelefono.Text = "Teléfono:";
            // 
            // txtTelefono
            // 
            txtTelefono.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            txtTelefono.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            txtTelefono.ForeColor = Color.Black;
            txtTelefono.BackColor = Color.White;
            txtTelefono.Location = new Point(650, 100);
            txtTelefono.MaxLength = 15;
            txtTelefono.Name = "txtTelefono";
            txtTelefono.Size = new Size(150, 23);
            txtTelefono.TabIndex = 5;
            // 
            // panelBotones
            // 
            panelBotones.Location = new Point(0, 490);
            // 
            // PersonasModule
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Name = "PersonasModule";
            tabInicio.ResumeLayout(false);
            panelBusqueda.ResumeLayout(false);
            panelBusqueda.PerformLayout();
            tabConfiguraciones.ResumeLayout(false);
            panelFormulario.ResumeLayout(false);
            panelFormulario.PerformLayout();
            panelSuperior.ResumeLayout(false);
            panelSuperior.PerformLayout();
            panelBotones.ResumeLayout(false);
            grpPersonaFisica.ResumeLayout(false);
            grpPersonaFisica.PerformLayout();
            grpPersonaJuridica.ResumeLayout(false);
            grpPersonaJuridica.PerformLayout();
            grpDatosComunes.ResumeLayout(false);
            grpDatosComunes.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private ComboBox cmbTipoPersona;
        private Label lblTipoPersona;
        private Label lblContador;
        private Label lblTipoPersonaForm;
        private ComboBox cmbTipoPersonaForm;
        private GroupBox grpPersonaFisica;
        private Label lblCi;
        private TextBox txtCi;
        private Label lblNombre;
        private TextBox txtNombre;
        private Label lblApellido;
        private TextBox txtApellido;
        private Label lblFechaNacimiento;
        private DateTimePicker dtpFechaNacimiento;
        private Label lblGenero;
        private ComboBox cmbGenero;
        private GroupBox grpPersonaJuridica;
        private Label lblRazonSocial;
        private TextBox txtRazonSocial;
        private Label lblNit;
        private TextBox txtNit;
        private Label lblEncargadoNombre;
        private TextBox txtEncargadoNombre;
        private Label lblEncargadoCargo;
        private TextBox txtEncargadoCargo;
        private GroupBox grpDatosComunes;
        private Label lblEmail;
        private TextBox txtEmail;
        private Label lblDireccion;
        private TextBox txtDireccion;
        private Label lblTelefono;
        private TextBox txtTelefono;
    }
}
