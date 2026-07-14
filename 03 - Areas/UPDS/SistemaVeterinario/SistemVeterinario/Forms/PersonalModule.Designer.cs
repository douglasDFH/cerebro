using System.Windows.Forms;
using System.Drawing;
namespace SistemVeterinario.Forms
{
    partial class PersonalModule
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
            this.cmbTipoPersonal = new System.Windows.Forms.ComboBox();
            this.lblTipoPersonal = new System.Windows.Forms.Label();
            this.lblContador = new System.Windows.Forms.Label();
            this.lblTipoPersonalForm = new System.Windows.Forms.Label();
            this.cmbTipoPersonalForm = new System.Windows.Forms.ComboBox();
            this.grpDatosPersonales = new System.Windows.Forms.GroupBox();
            this.tableLayoutPersonales = new System.Windows.Forms.TableLayoutPanel();
            this.lblNombre = new System.Windows.Forms.Label();
            this.txtNombre = new System.Windows.Forms.TextBox();
            this.lblApellido = new System.Windows.Forms.Label();
            this.txtApellido = new System.Windows.Forms.TextBox();
            this.lblEmail = new System.Windows.Forms.Label();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.lblTelefono = new System.Windows.Forms.Label();
            this.txtTelefono = new System.Windows.Forms.TextBox();
            this.lblDireccion = new System.Windows.Forms.Label();
            this.txtDireccion = new System.Windows.Forms.TextBox();
            this.grpDatosLaborales = new System.Windows.Forms.GroupBox();
            this.tableLayoutLaborales = new System.Windows.Forms.TableLayoutPanel();
            this.lblUsuario = new System.Windows.Forms.Label();
            this.txtUsuario = new System.Windows.Forms.TextBox();
            this.lblContrasena = new System.Windows.Forms.Label();
            this.txtContrasena = new System.Windows.Forms.TextBox();
            this.lblSalario = new System.Windows.Forms.Label();
            this.txtSalario = new System.Windows.Forms.TextBox();
            this.lblFechaContratacion = new System.Windows.Forms.Label();
            this.dtpFechaContratacion = new System.Windows.Forms.DateTimePicker();
            this.grpDatosEspecificos = new System.Windows.Forms.GroupBox();
            this.tableLayoutEspecificos = new System.Windows.Forms.TableLayoutPanel();
            this.lblLicencia = new System.Windows.Forms.Label();
            this.txtLicencia = new System.Windows.Forms.TextBox();
            this.lblEspecialidad = new System.Windows.Forms.Label();
            this.txtEspecialidad = new System.Windows.Forms.TextBox();
            this.lblUniversidad = new System.Windows.Forms.Label();
            this.txtUniversidad = new System.Windows.Forms.TextBox();
            this.lblExperiencia = new System.Windows.Forms.Label();
            this.numExperiencia = new System.Windows.Forms.NumericUpDown();
            this.lblArea = new System.Windows.Forms.Label();
            this.txtArea = new System.Windows.Forms.TextBox();
            this.lblTurno = new System.Windows.Forms.Label();
            this.cmbTurno = new System.Windows.Forms.ComboBox();
            this.lblNivel = new System.Windows.Forms.Label();
            this.cmbNivel = new System.Windows.Forms.ComboBox();
            this.tabControlPrincipal.SuspendLayout();
            this.tabInicio.SuspendLayout();
            this.tabConfiguraciones.SuspendLayout();
            this.panelBusqueda.SuspendLayout();
            this.panelFormulario.SuspendLayout();
            this.panelSuperior.SuspendLayout();
            this.panelBotones.SuspendLayout();
            this.grpDatosPersonales.SuspendLayout();
            this.tableLayoutPersonales.SuspendLayout();
            this.grpDatosLaborales.SuspendLayout();
            this.tableLayoutLaborales.SuspendLayout();
            this.grpDatosEspecificos.SuspendLayout();
            this.tableLayoutEspecificos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numExperiencia)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControlPrincipal
            // 
            this.tabControlPrincipal.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabControlPrincipal.Size = new System.Drawing.Size(1306, 853);
            // 
            // tabInicio
            // 
            this.tabInicio.Location = new System.Drawing.Point(4, 39);
            this.tabInicio.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabInicio.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabInicio.Size = new System.Drawing.Size(1135, 597);
            this.tabInicio.Text = "Gestión de Personal";
            // 
            // tabConfiguraciones
            // 
            this.tabConfiguraciones.Location = new System.Drawing.Point(4, 39);
            this.tabConfiguraciones.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabConfiguraciones.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabConfiguraciones.Size = new System.Drawing.Size(1298, 810);
            this.tabConfiguraciones.Text = "Configuración de Personal";
            // 
            // panelBusqueda
            // 
            this.panelBusqueda.Controls.Add(this.lblContador);
            this.panelBusqueda.Controls.Add(this.lblTipoPersonal);
            this.panelBusqueda.Controls.Add(this.cmbTipoPersonal);
            this.panelBusqueda.Location = new System.Drawing.Point(13, 14);
            this.panelBusqueda.Margin = new System.Windows.Forms.Padding(10);
            this.panelBusqueda.Size = new System.Drawing.Size(2087, 120);
            this.panelBusqueda.Controls.SetChildIndex(this.cmbTipoPersonal, 0);
            this.panelBusqueda.Controls.SetChildIndex(this.lblTipoPersonal, 0);
            this.panelBusqueda.Controls.SetChildIndex(this.lblContador, 0);
            this.panelBusqueda.Controls.SetChildIndex(this.chkMostrarTodo, 0);
            this.panelBusqueda.Controls.SetChildIndex(this.btnNuevo, 0);
            this.panelBusqueda.Controls.SetChildIndex(this.btnBuscar, 0);
            this.panelBusqueda.Controls.SetChildIndex(this.txtBuscar, 0);
            // 
            // txtBuscar
            // 
            this.txtBuscar.Location = new System.Drawing.Point(31, 11);
            this.txtBuscar.Margin = new System.Windows.Forms.Padding(5);
            this.txtBuscar.Size = new System.Drawing.Size(1259, 32);
            // 
            // btnBuscar
            // 
            this.btnBuscar.FlatAppearance.BorderSize = 0;
            this.btnBuscar.Location = new System.Drawing.Point(1332, 7);
            this.btnBuscar.Margin = new System.Windows.Forms.Padding(5);
            this.btnBuscar.Size = new System.Drawing.Size(120, 36);
            // 
            // btnNuevo
            // 
            this.btnNuevo.FlatAppearance.BorderSize = 0;
            this.btnNuevo.Location = new System.Drawing.Point(1744, 11);
            this.btnNuevo.Margin = new System.Windows.Forms.Padding(5);
            this.btnNuevo.Size = new System.Drawing.Size(140, 36);
            this.btnNuevo.Text = "➕ Nuevo Personal";
            // 
            // chkMostrarTodo
            // 
            this.chkMostrarTodo.Location = new System.Drawing.Point(180, 53);
            this.chkMostrarTodo.Margin = new System.Windows.Forms.Padding(5);
            // 
            // panelFormulario
            // 
            this.panelFormulario.Controls.Add(this.grpDatosEspecificos);
            this.panelFormulario.Controls.Add(this.grpDatosLaborales);
            this.panelFormulario.Controls.Add(this.grpDatosPersonales);
            this.panelFormulario.Controls.Add(this.cmbTipoPersonalForm);
            this.panelFormulario.Controls.Add(this.lblTipoPersonalForm);
            this.panelFormulario.Location = new System.Drawing.Point(3, 4);
            this.panelFormulario.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panelFormulario.Size = new System.Drawing.Size(1292, 802);
            this.panelFormulario.Controls.SetChildIndex(this.lblTipoPersonalForm, 0);
            this.panelFormulario.Controls.SetChildIndex(this.cmbTipoPersonalForm, 0);
            this.panelFormulario.Controls.SetChildIndex(this.grpDatosPersonales, 0);
            this.panelFormulario.Controls.SetChildIndex(this.grpDatosLaborales, 0);
            this.panelFormulario.Controls.SetChildIndex(this.grpDatosEspecificos, 0);
            this.panelFormulario.Controls.SetChildIndex(this.panelBotones, 0);
            this.panelFormulario.Controls.SetChildIndex(this.panelSuperior, 0);
            // 
            // panelSuperior
            // 
            this.panelSuperior.Location = new System.Drawing.Point(30, 41);
            this.panelSuperior.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panelSuperior.Size = new System.Drawing.Size(2168, 79);
            // 
            // lblModo
            // 
            this.lblModo.Location = new System.Drawing.Point(54, 21);
            // 
            // cmbModo
            // 
            this.cmbModo.Location = new System.Drawing.Point(174, 21);
            this.cmbModo.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cmbModo.Size = new System.Drawing.Size(156, 31);
            // 
            // lblId
            // 
            this.lblId.Location = new System.Drawing.Point(413, 27);
            // 
            // txtId
            // 
            this.txtId.Location = new System.Drawing.Point(484, 22);
            this.txtId.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtId.Size = new System.Drawing.Size(129, 30);
            // 
            // panelBotones
            // 
            this.panelBotones.Location = new System.Drawing.Point(100, 2625);
            this.panelBotones.Size = new System.Drawing.Size(2703, 65);
            // 
            // btnGuardar
            // 
            this.btnGuardar.FlatAppearance.BorderSize = 0;
            this.btnGuardar.Location = new System.Drawing.Point(2566, 15);
            // 
            // btnCancelar
            // 
            this.btnCancelar.FlatAppearance.BorderSize = 0;
            this.btnCancelar.Location = new System.Drawing.Point(2383, 15);
            // 
            // btnEliminar
            // 
            this.btnEliminar.FlatAppearance.BorderSize = 0;
            this.btnEliminar.Location = new System.Drawing.Point(18, 15);
            // 
            // cmbTipoPersonal
            // 
            this.cmbTipoPersonal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbTipoPersonal.BackColor = System.Drawing.Color.White;
            this.cmbTipoPersonal.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTipoPersonal.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbTipoPersonal.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbTipoPersonal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.cmbTipoPersonal.Location = new System.Drawing.Point(1500, 47);
            this.cmbTipoPersonal.Margin = new System.Windows.Forms.Padding(5);
            this.cmbTipoPersonal.Name = "cmbTipoPersonal";
            this.cmbTipoPersonal.Size = new System.Drawing.Size(180, 31);
            this.cmbTipoPersonal.TabIndex = 4;
            // 
            // lblTipoPersonal
            // 
            this.lblTipoPersonal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTipoPersonal.AutoSize = true;
            this.lblTipoPersonal.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblTipoPersonal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.lblTipoPersonal.Location = new System.Drawing.Point(1496, 15);
            this.lblTipoPersonal.Margin = new System.Windows.Forms.Padding(5);
            this.lblTipoPersonal.Name = "lblTipoPersonal";
            this.lblTipoPersonal.Size = new System.Drawing.Size(171, 23);
            this.lblTipoPersonal.TabIndex = 5;
            this.lblTipoPersonal.Text = "💼 Tipo de Personal";
            // 
            // lblContador
            // 
            this.lblContador.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblContador.AutoSize = true;
            this.lblContador.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblContador.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.lblContador.Location = new System.Drawing.Point(1740, 55);
            this.lblContador.Margin = new System.Windows.Forms.Padding(5);
            this.lblContador.Name = "lblContador";
            this.lblContador.Size = new System.Drawing.Size(169, 23);
            this.lblContador.TabIndex = 6;
            this.lblContador.Text = "Total de registros: 0";
            // 
            // lblTipoPersonalForm
            // 
            this.lblTipoPersonalForm.AutoSize = true;
            this.lblTipoPersonalForm.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblTipoPersonalForm.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.lblTipoPersonalForm.Location = new System.Drawing.Point(49, 127);
            this.lblTipoPersonalForm.Name = "lblTipoPersonalForm";
            this.lblTipoPersonalForm.Size = new System.Drawing.Size(165, 25);
            this.lblTipoPersonalForm.TabIndex = 20;
            this.lblTipoPersonalForm.Text = "Tipo de Personal:";
            // 
            // cmbTipoPersonalForm
            // 
            this.cmbTipoPersonalForm.BackColor = System.Drawing.Color.White;
            this.cmbTipoPersonalForm.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTipoPersonalForm.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.cmbTipoPersonalForm.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.cmbTipoPersonalForm.Location = new System.Drawing.Point(229, 124);
            this.cmbTipoPersonalForm.Name = "cmbTipoPersonalForm";
            this.cmbTipoPersonalForm.Size = new System.Drawing.Size(250, 33);
            this.cmbTipoPersonalForm.TabIndex = 21;
            // 
            // grpDatosPersonales
            // 
            this.grpDatosPersonales.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpDatosPersonales.BackColor = System.Drawing.Color.White;
            this.grpDatosPersonales.Controls.Add(this.tableLayoutPersonales);
            this.grpDatosPersonales.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.grpDatosPersonales.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.grpDatosPersonales.Location = new System.Drawing.Point(20, 180);
            this.grpDatosPersonales.Margin = new System.Windows.Forms.Padding(10);
            this.grpDatosPersonales.Name = "grpDatosPersonales";
            this.grpDatosPersonales.Padding = new System.Windows.Forms.Padding(15);
            this.grpDatosPersonales.Size = new System.Drawing.Size(1252, 140);
            this.grpDatosPersonales.TabIndex = 2;
            this.grpDatosPersonales.TabStop = false;
            this.grpDatosPersonales.Text = "👤 Datos Personales";
            // 
            // tableLayoutPersonales
            // 
            this.tableLayoutPersonales.ColumnCount = 5;
            this.tableLayoutPersonales.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPersonales.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPersonales.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPersonales.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPersonales.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPersonales.Controls.Add(this.lblNombre, 0, 0);
            this.tableLayoutPersonales.Controls.Add(this.txtNombre, 0, 1);
            this.tableLayoutPersonales.Controls.Add(this.lblApellido, 1, 0);
            this.tableLayoutPersonales.Controls.Add(this.txtApellido, 1, 1);
            this.tableLayoutPersonales.Controls.Add(this.lblEmail, 2, 0);
            this.tableLayoutPersonales.Controls.Add(this.txtEmail, 2, 1);
            this.tableLayoutPersonales.Controls.Add(this.lblTelefono, 3, 0);
            this.tableLayoutPersonales.Controls.Add(this.txtTelefono, 3, 1);
            this.tableLayoutPersonales.Controls.Add(this.lblDireccion, 4, 0);
            this.tableLayoutPersonales.Controls.Add(this.txtDireccion, 4, 1);
            this.tableLayoutPersonales.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPersonales.Location = new System.Drawing.Point(15, 40);
            this.tableLayoutPersonales.Name = "tableLayoutPersonales";
            this.tableLayoutPersonales.Padding = new System.Windows.Forms.Padding(10);
            this.tableLayoutPersonales.RowCount = 2;
            this.tableLayoutPersonales.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPersonales.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 45F));
            this.tableLayoutPersonales.Size = new System.Drawing.Size(1222, 85);
            this.tableLayoutPersonales.TabIndex = 0;
            // 
            // lblNombre
            // 
            this.lblNombre.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblNombre.AutoSize = true;
            this.lblNombre.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblNombre.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(76)))), ((int)(((byte)(60)))));
            this.lblNombre.Location = new System.Drawing.Point(15, 15);
            this.lblNombre.Margin = new System.Windows.Forms.Padding(5, 5, 5, 0);
            this.lblNombre.Name = "lblNombre";
            this.lblNombre.Size = new System.Drawing.Size(230, 23);
            this.lblNombre.TabIndex = 0;
            this.lblNombre.Text = "📝 Nombre *";
            // 
            // txtNombre
            // 
            this.txtNombre.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtNombre.BackColor = System.Drawing.Color.White;
            this.txtNombre.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNombre.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtNombre.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.txtNombre.Location = new System.Drawing.Point(15, 50);
            this.txtNombre.Margin = new System.Windows.Forms.Padding(5);
            this.txtNombre.MaxLength = 100;
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.Size = new System.Drawing.Size(230, 30);
            this.txtNombre.TabIndex = 1;
            // 
            // lblApellido
            // 
            this.lblApellido.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblApellido.AutoSize = true;
            this.lblApellido.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblApellido.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(76)))), ((int)(((byte)(60)))));
            this.lblApellido.Location = new System.Drawing.Point(255, 15);
            this.lblApellido.Margin = new System.Windows.Forms.Padding(5, 5, 5, 0);
            this.lblApellido.Name = "lblApellido";
            this.lblApellido.Size = new System.Drawing.Size(230, 23);
            this.lblApellido.TabIndex = 2;
            this.lblApellido.Text = "📝 Apellido *";
            // 
            // txtApellido
            // 
            this.txtApellido.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtApellido.BackColor = System.Drawing.Color.White;
            this.txtApellido.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtApellido.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtApellido.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.txtApellido.Location = new System.Drawing.Point(255, 50);
            this.txtApellido.Margin = new System.Windows.Forms.Padding(5);
            this.txtApellido.MaxLength = 100;
            this.txtApellido.Name = "txtApellido";
            this.txtApellido.Size = new System.Drawing.Size(230, 30);
            this.txtApellido.TabIndex = 3;
            // 
            // lblEmail
            // 
            this.lblEmail.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblEmail.AutoSize = true;
            this.lblEmail.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblEmail.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.lblEmail.Location = new System.Drawing.Point(495, 15);
            this.lblEmail.Margin = new System.Windows.Forms.Padding(5, 5, 5, 0);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.Size = new System.Drawing.Size(230, 23);
            this.lblEmail.TabIndex = 4;
            this.lblEmail.Text = "📧 Email";
            // 
            // txtEmail
            // 
            this.txtEmail.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtEmail.BackColor = System.Drawing.Color.White;
            this.txtEmail.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtEmail.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtEmail.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.txtEmail.Location = new System.Drawing.Point(495, 50);
            this.txtEmail.Margin = new System.Windows.Forms.Padding(5);
            this.txtEmail.MaxLength = 100;
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(230, 30);
            this.txtEmail.TabIndex = 5;
            // 
            // lblTelefono
            // 
            this.lblTelefono.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTelefono.AutoSize = true;
            this.lblTelefono.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblTelefono.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.lblTelefono.Location = new System.Drawing.Point(735, 15);
            this.lblTelefono.Margin = new System.Windows.Forms.Padding(5, 5, 5, 0);
            this.lblTelefono.Name = "lblTelefono";
            this.lblTelefono.Size = new System.Drawing.Size(230, 23);
            this.lblTelefono.TabIndex = 6;
            this.lblTelefono.Text = "📞 Teléfono";
            // 
            // txtTelefono
            // 
            this.txtTelefono.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTelefono.BackColor = System.Drawing.Color.White;
            this.txtTelefono.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTelefono.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtTelefono.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.txtTelefono.Location = new System.Drawing.Point(735, 50);
            this.txtTelefono.Margin = new System.Windows.Forms.Padding(5);
            this.txtTelefono.MaxLength = 20;
            this.txtTelefono.Name = "txtTelefono";
            this.txtTelefono.Size = new System.Drawing.Size(230, 30);
            this.txtTelefono.TabIndex = 7;
            // 
            // lblDireccion
            // 
            this.lblDireccion.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDireccion.AutoSize = true;
            this.lblDireccion.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblDireccion.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.lblDireccion.Location = new System.Drawing.Point(975, 15);
            this.lblDireccion.Margin = new System.Windows.Forms.Padding(5, 5, 5, 0);
            this.lblDireccion.Name = "lblDireccion";
            this.lblDireccion.Size = new System.Drawing.Size(232, 23);
            this.lblDireccion.TabIndex = 8;
            this.lblDireccion.Text = "📍 Dirección";
            // 
            // txtDireccion
            // 
            this.txtDireccion.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDireccion.BackColor = System.Drawing.Color.White;
            this.txtDireccion.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDireccion.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtDireccion.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.txtDireccion.Location = new System.Drawing.Point(975, 50);
            this.txtDireccion.Margin = new System.Windows.Forms.Padding(5);
            this.txtDireccion.MaxLength = 255;
            this.txtDireccion.Name = "txtDireccion";
            this.txtDireccion.Size = new System.Drawing.Size(232, 30);
            this.txtDireccion.TabIndex = 9;
            // 
            // grpDatosLaborales
            // 
            this.grpDatosLaborales.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpDatosLaborales.BackColor = System.Drawing.Color.White;
            this.grpDatosLaborales.Controls.Add(this.tableLayoutLaborales);
            this.grpDatosLaborales.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.grpDatosLaborales.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(174)))), ((int)(((byte)(96)))));
            this.grpDatosLaborales.Location = new System.Drawing.Point(20, 340);
            this.grpDatosLaborales.Margin = new System.Windows.Forms.Padding(10);
            this.grpDatosLaborales.Name = "grpDatosLaborales";
            this.grpDatosLaborales.Padding = new System.Windows.Forms.Padding(15);
            this.grpDatosLaborales.Size = new System.Drawing.Size(1252, 140);
            this.grpDatosLaborales.TabIndex = 3;
            this.grpDatosLaborales.TabStop = false;
            this.grpDatosLaborales.Text = "💼 Datos Laborales";
            // 
            // tableLayoutLaborales
            // 
            this.tableLayoutLaborales.ColumnCount = 4;
            this.tableLayoutLaborales.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutLaborales.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutLaborales.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutLaborales.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutLaborales.Controls.Add(this.lblUsuario, 0, 0);
            this.tableLayoutLaborales.Controls.Add(this.txtUsuario, 0, 1);
            this.tableLayoutLaborales.Controls.Add(this.lblContrasena, 1, 0);
            this.tableLayoutLaborales.Controls.Add(this.txtContrasena, 1, 1);
            this.tableLayoutLaborales.Controls.Add(this.lblSalario, 2, 0);
            this.tableLayoutLaborales.Controls.Add(this.txtSalario, 2, 1);
            this.tableLayoutLaborales.Controls.Add(this.lblFechaContratacion, 3, 0);
            this.tableLayoutLaborales.Controls.Add(this.dtpFechaContratacion, 3, 1);
            this.tableLayoutLaborales.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutLaborales.Location = new System.Drawing.Point(15, 40);
            this.tableLayoutLaborales.Name = "tableLayoutLaborales";
            this.tableLayoutLaborales.Padding = new System.Windows.Forms.Padding(10);
            this.tableLayoutLaborales.RowCount = 2;
            this.tableLayoutLaborales.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutLaborales.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 45F));
            this.tableLayoutLaborales.Size = new System.Drawing.Size(1222, 85);
            this.tableLayoutLaborales.TabIndex = 0;
            // 
            // lblUsuario
            // 
            this.lblUsuario.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblUsuario.AutoSize = true;
            this.lblUsuario.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblUsuario.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(76)))), ((int)(((byte)(60)))));
            this.lblUsuario.Location = new System.Drawing.Point(15, 15);
            this.lblUsuario.Margin = new System.Windows.Forms.Padding(5, 5, 5, 0);
            this.lblUsuario.Name = "lblUsuario";
            this.lblUsuario.Size = new System.Drawing.Size(290, 23);
            this.lblUsuario.TabIndex = 0;
            this.lblUsuario.Text = "👤 Usuario *";
            // 
            // txtUsuario
            // 
            this.txtUsuario.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtUsuario.BackColor = System.Drawing.Color.White;
            this.txtUsuario.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtUsuario.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtUsuario.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.txtUsuario.Location = new System.Drawing.Point(15, 50);
            this.txtUsuario.Margin = new System.Windows.Forms.Padding(5);
            this.txtUsuario.MaxLength = 50;
            this.txtUsuario.Name = "txtUsuario";
            this.txtUsuario.Size = new System.Drawing.Size(290, 30);
            this.txtUsuario.TabIndex = 1;
            // 
            // lblContrasena
            // 
            this.lblContrasena.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblContrasena.AutoSize = true;
            this.lblContrasena.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblContrasena.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(76)))), ((int)(((byte)(60)))));
            this.lblContrasena.Location = new System.Drawing.Point(315, 15);
            this.lblContrasena.Margin = new System.Windows.Forms.Padding(5, 5, 5, 0);
            this.lblContrasena.Name = "lblContrasena";
            this.lblContrasena.Size = new System.Drawing.Size(290, 23);
            this.lblContrasena.TabIndex = 2;
            this.lblContrasena.Text = "🔒 Contraseña *";
            // 
            // txtContrasena
            // 
            this.txtContrasena.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtContrasena.BackColor = System.Drawing.Color.White;
            this.txtContrasena.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtContrasena.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtContrasena.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.txtContrasena.Location = new System.Drawing.Point(315, 50);
            this.txtContrasena.Margin = new System.Windows.Forms.Padding(5);
            this.txtContrasena.MaxLength = 255;
            this.txtContrasena.Name = "txtContrasena";
            this.txtContrasena.PasswordChar = '*';
            this.txtContrasena.Size = new System.Drawing.Size(290, 30);
            this.txtContrasena.TabIndex = 3;
            // 
            // lblSalario
            // 
            this.lblSalario.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSalario.AutoSize = true;
            this.lblSalario.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblSalario.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.lblSalario.Location = new System.Drawing.Point(615, 15);
            this.lblSalario.Margin = new System.Windows.Forms.Padding(5, 5, 5, 0);
            this.lblSalario.Name = "lblSalario";
            this.lblSalario.Size = new System.Drawing.Size(290, 23);
            this.lblSalario.TabIndex = 4;
            this.lblSalario.Text = "💰 Salario";
            // 
            // txtSalario
            // 
            this.txtSalario.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSalario.BackColor = System.Drawing.Color.White;
            this.txtSalario.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSalario.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtSalario.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.txtSalario.Location = new System.Drawing.Point(615, 50);
            this.txtSalario.Margin = new System.Windows.Forms.Padding(5);
            this.txtSalario.MaxLength = 10;
            this.txtSalario.Name = "txtSalario";
            this.txtSalario.Size = new System.Drawing.Size(290, 30);
            this.txtSalario.TabIndex = 5;
            // 
            // lblFechaContratacion
            // 
            this.lblFechaContratacion.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblFechaContratacion.AutoSize = true;
            this.lblFechaContratacion.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblFechaContratacion.ForeColor = System.Drawing.Color.Black;
            this.lblFechaContratacion.Location = new System.Drawing.Point(915, 15);
            this.lblFechaContratacion.Margin = new System.Windows.Forms.Padding(5, 5, 5, 0);
            this.lblFechaContratacion.Name = "lblFechaContratacion";
            this.lblFechaContratacion.Size = new System.Drawing.Size(292, 20);
            this.lblFechaContratacion.TabIndex = 18;
            this.lblFechaContratacion.Text = "F. Contratación:";
            // 
            // dtpFechaContratacion
            // 
            this.dtpFechaContratacion.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dtpFechaContratacion.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.dtpFechaContratacion.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFechaContratacion.Location = new System.Drawing.Point(913, 47);
            this.dtpFechaContratacion.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dtpFechaContratacion.Name = "dtpFechaContratacion";
            this.dtpFechaContratacion.Size = new System.Drawing.Size(296, 27);
            this.dtpFechaContratacion.TabIndex = 19;
            // 
            // grpDatosEspecificos
            // 
            this.grpDatosEspecificos.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpDatosEspecificos.BackColor = System.Drawing.Color.White;
            this.grpDatosEspecificos.Controls.Add(this.tableLayoutEspecificos);
            this.grpDatosEspecificos.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.grpDatosEspecificos.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(89)))), ((int)(((byte)(182)))));
            this.grpDatosEspecificos.Location = new System.Drawing.Point(20, 500);
            this.grpDatosEspecificos.Margin = new System.Windows.Forms.Padding(10);
            this.grpDatosEspecificos.Name = "grpDatosEspecificos";
            this.grpDatosEspecificos.Padding = new System.Windows.Forms.Padding(15);
            this.grpDatosEspecificos.Size = new System.Drawing.Size(1252, 213);
            this.grpDatosEspecificos.TabIndex = 4;
            this.grpDatosEspecificos.TabStop = false;
            this.grpDatosEspecificos.Text = "🎓 Datos Específicos";
            // 
            // tableLayoutEspecificos
            // 
            this.tableLayoutEspecificos.ColumnCount = 5;
            this.tableLayoutEspecificos.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutEspecificos.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutEspecificos.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutEspecificos.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutEspecificos.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutEspecificos.Controls.Add(this.lblLicencia, 0, 0);
            this.tableLayoutEspecificos.Controls.Add(this.txtLicencia, 0, 1);
            this.tableLayoutEspecificos.Controls.Add(this.lblEspecialidad, 1, 0);
            this.tableLayoutEspecificos.Controls.Add(this.txtEspecialidad, 1, 1);
            this.tableLayoutEspecificos.Controls.Add(this.lblUniversidad, 2, 0);
            this.tableLayoutEspecificos.Controls.Add(this.txtUniversidad, 2, 1);
            this.tableLayoutEspecificos.Controls.Add(this.lblExperiencia, 3, 0);
            this.tableLayoutEspecificos.Controls.Add(this.numExperiencia, 3, 1);
            this.tableLayoutEspecificos.Controls.Add(this.lblArea, 0, 2);
            this.tableLayoutEspecificos.Controls.Add(this.txtArea, 0, 3);
            this.tableLayoutEspecificos.Controls.Add(this.lblTurno, 1, 2);
            this.tableLayoutEspecificos.Controls.Add(this.cmbTurno, 1, 3);
            this.tableLayoutEspecificos.Controls.Add(this.lblNivel, 2, 2);
            this.tableLayoutEspecificos.Controls.Add(this.cmbNivel, 2, 3);
            this.tableLayoutEspecificos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutEspecificos.Location = new System.Drawing.Point(15, 40);
            this.tableLayoutEspecificos.Name = "tableLayoutEspecificos";
            this.tableLayoutEspecificos.Padding = new System.Windows.Forms.Padding(10);
            this.tableLayoutEspecificos.RowCount = 4;
            this.tableLayoutEspecificos.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutEspecificos.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 45F));
            this.tableLayoutEspecificos.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutEspecificos.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 45F));
            this.tableLayoutEspecificos.Size = new System.Drawing.Size(1222, 158);
            this.tableLayoutEspecificos.TabIndex = 0;
            // 
            // lblLicencia
            // 
            this.lblLicencia.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblLicencia.AutoSize = true;
            this.lblLicencia.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblLicencia.ForeColor = System.Drawing.Color.Black;
            this.lblLicencia.Location = new System.Drawing.Point(15, 15);
            this.lblLicencia.Margin = new System.Windows.Forms.Padding(5, 5, 5, 0);
            this.lblLicencia.Name = "lblLicencia";
            this.lblLicencia.Size = new System.Drawing.Size(230, 20);
            this.lblLicencia.TabIndex = 22;
            this.lblLicencia.Text = "Núm. Licencia:";
            // 
            // txtLicencia
            // 
            this.txtLicencia.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtLicencia.BackColor = System.Drawing.Color.White;
            this.txtLicencia.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtLicencia.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtLicencia.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.txtLicencia.Location = new System.Drawing.Point(13, 47);
            this.txtLicencia.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtLicencia.MaxLength = 50;
            this.txtLicencia.Name = "txtLicencia";
            this.txtLicencia.Size = new System.Drawing.Size(234, 27);
            this.txtLicencia.TabIndex = 23;
            // 
            // lblEspecialidad
            // 
            this.lblEspecialidad.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblEspecialidad.AutoSize = true;
            this.lblEspecialidad.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblEspecialidad.ForeColor = System.Drawing.Color.Black;
            this.lblEspecialidad.Location = new System.Drawing.Point(255, 15);
            this.lblEspecialidad.Margin = new System.Windows.Forms.Padding(5, 5, 5, 0);
            this.lblEspecialidad.Name = "lblEspecialidad";
            this.lblEspecialidad.Size = new System.Drawing.Size(230, 20);
            this.lblEspecialidad.TabIndex = 24;
            this.lblEspecialidad.Text = "Especialidad:";
            // 
            // txtEspecialidad
            // 
            this.txtEspecialidad.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtEspecialidad.BackColor = System.Drawing.Color.White;
            this.txtEspecialidad.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtEspecialidad.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtEspecialidad.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.txtEspecialidad.Location = new System.Drawing.Point(253, 47);
            this.txtEspecialidad.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtEspecialidad.MaxLength = 100;
            this.txtEspecialidad.Name = "txtEspecialidad";
            this.txtEspecialidad.Size = new System.Drawing.Size(234, 27);
            this.txtEspecialidad.TabIndex = 25;
            // 
            // lblUniversidad
            // 
            this.lblUniversidad.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblUniversidad.AutoSize = true;
            this.lblUniversidad.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblUniversidad.ForeColor = System.Drawing.Color.Black;
            this.lblUniversidad.Location = new System.Drawing.Point(495, 15);
            this.lblUniversidad.Margin = new System.Windows.Forms.Padding(5, 5, 5, 0);
            this.lblUniversidad.Name = "lblUniversidad";
            this.lblUniversidad.Size = new System.Drawing.Size(230, 20);
            this.lblUniversidad.TabIndex = 26;
            this.lblUniversidad.Text = "Universidad:";
            // 
            // txtUniversidad
            // 
            this.txtUniversidad.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtUniversidad.BackColor = System.Drawing.Color.White;
            this.txtUniversidad.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtUniversidad.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtUniversidad.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.txtUniversidad.Location = new System.Drawing.Point(493, 47);
            this.txtUniversidad.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtUniversidad.MaxLength = 200;
            this.txtUniversidad.Name = "txtUniversidad";
            this.txtUniversidad.Size = new System.Drawing.Size(234, 27);
            this.txtUniversidad.TabIndex = 27;
            // 
            // lblExperiencia
            // 
            this.lblExperiencia.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblExperiencia.AutoSize = true;
            this.lblExperiencia.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblExperiencia.ForeColor = System.Drawing.Color.Black;
            this.lblExperiencia.Location = new System.Drawing.Point(735, 15);
            this.lblExperiencia.Margin = new System.Windows.Forms.Padding(5, 5, 5, 0);
            this.lblExperiencia.Name = "lblExperiencia";
            this.lblExperiencia.Size = new System.Drawing.Size(230, 20);
            this.lblExperiencia.TabIndex = 28;
            this.lblExperiencia.Text = "Años Experiencia:";
            // 
            // numExperiencia
            // 
            this.numExperiencia.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.numExperiencia.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.numExperiencia.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.numExperiencia.Location = new System.Drawing.Point(733, 47);
            this.numExperiencia.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.numExperiencia.Maximum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.numExperiencia.Name = "numExperiencia";
            this.numExperiencia.Size = new System.Drawing.Size(234, 27);
            this.numExperiencia.TabIndex = 29;
            // 
            // lblArea
            // 
            this.lblArea.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblArea.AutoSize = true;
            this.lblArea.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblArea.ForeColor = System.Drawing.Color.Black;
            this.lblArea.Location = new System.Drawing.Point(15, 95);
            this.lblArea.Margin = new System.Windows.Forms.Padding(5, 5, 5, 0);
            this.lblArea.Name = "lblArea";
            this.lblArea.Size = new System.Drawing.Size(230, 20);
            this.lblArea.TabIndex = 30;
            this.lblArea.Text = "🏢 Área:";
            // 
            // txtArea
            // 
            this.txtArea.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtArea.BackColor = System.Drawing.Color.White;
            this.txtArea.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtArea.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtArea.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.txtArea.Location = new System.Drawing.Point(15, 130);
            this.txtArea.Margin = new System.Windows.Forms.Padding(5);
            this.txtArea.MaxLength = 100;
            this.txtArea.Name = "txtArea";
            this.txtArea.Size = new System.Drawing.Size(230, 27);
            this.txtArea.TabIndex = 31;
            // 
            // lblTurno
            // 
            this.lblTurno.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTurno.AutoSize = true;
            this.lblTurno.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblTurno.ForeColor = System.Drawing.Color.Black;
            this.lblTurno.Location = new System.Drawing.Point(255, 95);
            this.lblTurno.Margin = new System.Windows.Forms.Padding(5, 5, 5, 0);
            this.lblTurno.Name = "lblTurno";
            this.lblTurno.Size = new System.Drawing.Size(230, 20);
            this.lblTurno.TabIndex = 32;
            this.lblTurno.Text = "🕐 Turno:";
            // 
            // cmbTurno
            // 
            this.cmbTurno.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbTurno.BackColor = System.Drawing.Color.White;
            this.cmbTurno.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTurno.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cmbTurno.ForeColor = System.Drawing.Color.Black;
            this.cmbTurno.Location = new System.Drawing.Point(255, 130);
            this.cmbTurno.Margin = new System.Windows.Forms.Padding(5);
            this.cmbTurno.Name = "cmbTurno";
            this.cmbTurno.Size = new System.Drawing.Size(230, 28);
            this.cmbTurno.TabIndex = 33;
            // 
            // lblNivel
            // 
            this.lblNivel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblNivel.AutoSize = true;
            this.lblNivel.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblNivel.ForeColor = System.Drawing.Color.Black;
            this.lblNivel.Location = new System.Drawing.Point(495, 95);
            this.lblNivel.Margin = new System.Windows.Forms.Padding(5, 5, 5, 0);
            this.lblNivel.Name = "lblNivel";
            this.lblNivel.Size = new System.Drawing.Size(230, 20);
            this.lblNivel.TabIndex = 34;
            this.lblNivel.Text = "📊 Nivel:";
            // 
            // cmbNivel
            // 
            this.cmbNivel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbNivel.BackColor = System.Drawing.Color.White;
            this.cmbNivel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbNivel.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cmbNivel.ForeColor = System.Drawing.Color.Black;
            this.cmbNivel.Location = new System.Drawing.Point(495, 130);
            this.cmbNivel.Margin = new System.Windows.Forms.Padding(5);
            this.cmbNivel.Name = "cmbNivel";
            this.cmbNivel.Size = new System.Drawing.Size(230, 28);
            this.cmbNivel.TabIndex = 35;
            // 
            // PersonalModule
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "PersonalModule";
            this.Size = new System.Drawing.Size(1306, 853);
            this.tabControlPrincipal.ResumeLayout(false);
            this.tabInicio.ResumeLayout(false);
            this.tabConfiguraciones.ResumeLayout(false);
            this.panelBusqueda.ResumeLayout(false);
            this.panelBusqueda.PerformLayout();
            this.panelFormulario.ResumeLayout(false);
            this.panelFormulario.PerformLayout();
            this.panelSuperior.ResumeLayout(false);
            this.panelSuperior.PerformLayout();
            this.panelBotones.ResumeLayout(false);
            this.grpDatosPersonales.ResumeLayout(false);
            this.tableLayoutPersonales.ResumeLayout(false);
            this.tableLayoutPersonales.PerformLayout();
            this.grpDatosLaborales.ResumeLayout(false);
            this.tableLayoutLaborales.ResumeLayout(false);
            this.tableLayoutLaborales.PerformLayout();
            this.grpDatosEspecificos.ResumeLayout(false);
            this.tableLayoutEspecificos.ResumeLayout(false);
            this.tableLayoutEspecificos.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numExperiencia)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private ComboBox cmbTipoPersonal;
        private Label lblTipoPersonal;
        private Label lblContador;
        private Label lblTipoPersonalForm;
        private ComboBox cmbTipoPersonalForm;
        private GroupBox grpDatosPersonales;
        private TableLayoutPanel tableLayoutPersonales;
        private Label lblNombre;
        private TextBox txtNombre;
        private Label lblApellido;
        private TextBox txtApellido;
        private Label lblEmail;
        private TextBox txtEmail;
        private Label lblTelefono;
        private TextBox txtTelefono;
        private Label lblDireccion;
        private TextBox txtDireccion;
        private GroupBox grpDatosLaborales;
        private TableLayoutPanel tableLayoutLaborales;
        private Label lblUsuario;
        private TextBox txtUsuario;
        private Label lblContrasena;
        private TextBox txtContrasena;
        private Label lblSalario;
        private TextBox txtSalario;
        private Label lblFechaContratacion;
        private DateTimePicker dtpFechaContratacion;
        private GroupBox grpDatosEspecificos;
        private TableLayoutPanel tableLayoutEspecificos;
        private Label lblLicencia;
        private TextBox txtLicencia;
        private Label lblEspecialidad;
        private TextBox txtEspecialidad;
        private Label lblUniversidad;
        private TextBox txtUniversidad;
        private Label lblExperiencia;
        private NumericUpDown numExperiencia;
        private Label lblArea;
        private TextBox txtArea;
        private Label lblTurno;
        private ComboBox cmbTurno;
        private Label lblNivel;
        private ComboBox cmbNivel;
    }
}