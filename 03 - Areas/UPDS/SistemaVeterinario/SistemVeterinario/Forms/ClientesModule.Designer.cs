using System.Windows.Forms;
using System.Drawing;
namespace SistemVeterinario.Forms
{
    partial class ClientesModule
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
            this.cmbTipoPersona = new System.Windows.Forms.ComboBox();
            this.lblTipoPersona = new System.Windows.Forms.Label();
            this.lblContador = new System.Windows.Forms.Label();
            this.lblTipoPersonaForm = new System.Windows.Forms.Label();
            this.cmbTipoPersonaForm = new System.Windows.Forms.ComboBox();
            this.grpPersonaFisica = new System.Windows.Forms.GroupBox();
            this.tableLayoutFisica = new System.Windows.Forms.TableLayoutPanel();
            this.lblCi = new System.Windows.Forms.Label();
            this.txtCi = new System.Windows.Forms.TextBox();
            this.lblNombre = new System.Windows.Forms.Label();
            this.txtNombre = new System.Windows.Forms.TextBox();
            this.lblApellido = new System.Windows.Forms.Label();
            this.txtApellido = new System.Windows.Forms.TextBox();
            this.lblFechaNacimiento = new System.Windows.Forms.Label();
            this.dtpFechaNacimiento = new System.Windows.Forms.DateTimePicker();
            this.lblGenero = new System.Windows.Forms.Label();
            this.cmbGenero = new System.Windows.Forms.ComboBox();
            this.grpPersonaJuridica = new System.Windows.Forms.GroupBox();
            this.txtEncargadoCargo = new System.Windows.Forms.TextBox();
            this.lblEncargadoCargo = new System.Windows.Forms.Label();
            this.txtEncargadoNombre = new System.Windows.Forms.TextBox();
            this.lblEncargadoNombre = new System.Windows.Forms.Label();
            this.txtNit = new System.Windows.Forms.TextBox();
            this.lblNit = new System.Windows.Forms.Label();
            this.txtRazonSocial = new System.Windows.Forms.TextBox();
            this.lblRazonSocial = new System.Windows.Forms.Label();
            this.tableLayoutJuridica = new System.Windows.Forms.TableLayoutPanel();
            this.grpDatosComunes = new System.Windows.Forms.GroupBox();
            this.txtTelefono = new System.Windows.Forms.TextBox();
            this.lblTelefono = new System.Windows.Forms.Label();
            this.txtDireccion = new System.Windows.Forms.TextBox();
            this.lblDireccion = new System.Windows.Forms.Label();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.lblEmail = new System.Windows.Forms.Label();
            this.tableLayoutComunes = new System.Windows.Forms.TableLayoutPanel();
            this.tabControlPrincipal.SuspendLayout();
            this.tabInicio.SuspendLayout();
            this.tabConfiguraciones.SuspendLayout();
            this.panelBusqueda.SuspendLayout();
            this.panelFormulario.SuspendLayout();
            this.panelSuperior.SuspendLayout();
            this.panelBotones.SuspendLayout();
            this.grpPersonaFisica.SuspendLayout();
            this.tableLayoutFisica.SuspendLayout();
            this.grpPersonaJuridica.SuspendLayout();
            this.grpDatosComunes.SuspendLayout();
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
            this.tabInicio.Text = "Gestión de Personas";
            // 
            // tabConfiguraciones
            // 
            this.tabConfiguraciones.Location = new System.Drawing.Point(4, 39);
            this.tabConfiguraciones.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabConfiguraciones.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabConfiguraciones.Size = new System.Drawing.Size(1298, 810);
            this.tabConfiguraciones.Text = "Configuración de Persona";
            // 
            // panelBusqueda
            // 
            this.panelBusqueda.Controls.Add(this.lblContador);
            this.panelBusqueda.Controls.Add(this.lblTipoPersona);
            this.panelBusqueda.Controls.Add(this.cmbTipoPersona);
            this.panelBusqueda.Location = new System.Drawing.Point(13, 14);
            this.panelBusqueda.Margin = new System.Windows.Forms.Padding(10);
            this.panelBusqueda.Size = new System.Drawing.Size(1409, 141);
            this.panelBusqueda.Controls.SetChildIndex(this.cmbTipoPersona, 0);
            this.panelBusqueda.Controls.SetChildIndex(this.lblTipoPersona, 0);
            this.panelBusqueda.Controls.SetChildIndex(this.lblContador, 0);
            this.panelBusqueda.Controls.SetChildIndex(this.chkMostrarTodo, 0);
            this.panelBusqueda.Controls.SetChildIndex(this.btnNuevo, 0);
            this.panelBusqueda.Controls.SetChildIndex(this.btnBuscar, 0);
            this.panelBusqueda.Controls.SetChildIndex(this.txtBuscar, 0);
            // 
            // txtBuscar
            // 
            this.txtBuscar.Location = new System.Drawing.Point(22, 21);
            this.txtBuscar.Margin = new System.Windows.Forms.Padding(5);
            this.txtBuscar.Size = new System.Drawing.Size(581, 32);
            // 
            // btnBuscar
            // 
            this.btnBuscar.FlatAppearance.BorderSize = 0;
            this.btnBuscar.Location = new System.Drawing.Point(625, 18);
            this.btnBuscar.Margin = new System.Windows.Forms.Padding(5);
            this.btnBuscar.Size = new System.Drawing.Size(120, 36);
            // 
            // btnNuevo
            // 
            this.btnNuevo.FlatAppearance.BorderSize = 0;
            this.btnNuevo.Location = new System.Drawing.Point(1098, 9);
            this.btnNuevo.Margin = new System.Windows.Forms.Padding(5);
            this.btnNuevo.Size = new System.Drawing.Size(150, 40);
            this.btnNuevo.Text = "➕ Nueva Persona";
            // 
            // chkMostrarTodo
            // 
            this.chkMostrarTodo.Location = new System.Drawing.Point(160, 65);
            this.chkMostrarTodo.Margin = new System.Windows.Forms.Padding(5);
            // 
            // panelFormulario
            // 
            this.panelFormulario.Controls.Add(this.grpDatosComunes);
            this.panelFormulario.Controls.Add(this.grpPersonaJuridica);
            this.panelFormulario.Controls.Add(this.grpPersonaFisica);
            this.panelFormulario.Controls.Add(this.cmbTipoPersonaForm);
            this.panelFormulario.Controls.Add(this.lblTipoPersonaForm);
            this.panelFormulario.Location = new System.Drawing.Point(3, 4);
            this.panelFormulario.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panelFormulario.Size = new System.Drawing.Size(1292, 802);
            this.panelFormulario.Controls.SetChildIndex(this.lblTipoPersonaForm, 0);
            this.panelFormulario.Controls.SetChildIndex(this.cmbTipoPersonaForm, 0);
            this.panelFormulario.Controls.SetChildIndex(this.grpPersonaFisica, 0);
            this.panelFormulario.Controls.SetChildIndex(this.grpPersonaJuridica, 0);
            this.panelFormulario.Controls.SetChildIndex(this.grpDatosComunes, 0);
            this.panelFormulario.Controls.SetChildIndex(this.panelBotones, 0);
            this.panelFormulario.Controls.SetChildIndex(this.panelSuperior, 0);
            // 
            // panelSuperior
            // 
            this.panelSuperior.Location = new System.Drawing.Point(35, 35);
            this.panelSuperior.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panelSuperior.Size = new System.Drawing.Size(1385, 72);
            // 
            // lblModo
            // 
            this.lblModo.Location = new System.Drawing.Point(97, 24);
            // 
            // cmbModo
            // 
            this.cmbModo.Location = new System.Drawing.Point(227, 24);
            this.cmbModo.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cmbModo.Size = new System.Drawing.Size(156, 31);
            // 
            // lblId
            // 
            this.lblId.Location = new System.Drawing.Point(438, 24);
            // 
            // txtId
            // 
            this.txtId.Location = new System.Drawing.Point(513, 24);
            this.txtId.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtId.Size = new System.Drawing.Size(129, 30);
            // 
            // panelBotones
            // 
            this.panelBotones.Location = new System.Drawing.Point(100, 2186);
            this.panelBotones.Size = new System.Drawing.Size(2214, 65);
            // 
            // btnGuardar
            // 
            this.btnGuardar.FlatAppearance.BorderSize = 0;
            this.btnGuardar.Location = new System.Drawing.Point(2077, 15);
            // 
            // btnCancelar
            // 
            this.btnCancelar.FlatAppearance.BorderSize = 0;
            this.btnCancelar.Location = new System.Drawing.Point(1894, 15);
            // 
            // btnEliminar
            // 
            this.btnEliminar.FlatAppearance.BorderSize = 0;
            // 
            // cmbTipoPersona
            // 
            this.cmbTipoPersona.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbTipoPersona.BackColor = System.Drawing.Color.White;
            this.cmbTipoPersona.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTipoPersona.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbTipoPersona.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbTipoPersona.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.cmbTipoPersona.Location = new System.Drawing.Point(819, 42);
            this.cmbTipoPersona.Margin = new System.Windows.Forms.Padding(5);
            this.cmbTipoPersona.Name = "cmbTipoPersona";
            this.cmbTipoPersona.Size = new System.Drawing.Size(180, 31);
            this.cmbTipoPersona.TabIndex = 4;
            // 
            // lblTipoPersona
            // 
            this.lblTipoPersona.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTipoPersona.AutoSize = true;
            this.lblTipoPersona.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblTipoPersona.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.lblTipoPersona.Location = new System.Drawing.Point(833, 9);
            this.lblTipoPersona.Margin = new System.Windows.Forms.Padding(5);
            this.lblTipoPersona.Name = "lblTipoPersona";
            this.lblTipoPersona.Size = new System.Drawing.Size(166, 23);
            this.lblTipoPersona.TabIndex = 5;
            this.lblTipoPersona.Text = "💼 Tipo de Persona";
            // 
            // lblContador
            // 
            this.lblContador.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblContador.AutoSize = true;
            this.lblContador.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblContador.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.lblContador.Location = new System.Drawing.Point(1079, 59);
            this.lblContador.Margin = new System.Windows.Forms.Padding(5);
            this.lblContador.Name = "lblContador";
            this.lblContador.Size = new System.Drawing.Size(169, 23);
            this.lblContador.TabIndex = 6;
            this.lblContador.Text = "Total de registros: 0";
            // 
            // lblTipoPersonaForm
            // 
            this.lblTipoPersonaForm.AutoSize = true;
            this.lblTipoPersonaForm.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblTipoPersonaForm.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.lblTipoPersonaForm.Location = new System.Drawing.Point(49, 117);
            this.lblTipoPersonaForm.Name = "lblTipoPersonaForm";
            this.lblTipoPersonaForm.Size = new System.Drawing.Size(160, 25);
            this.lblTipoPersonaForm.TabIndex = 0;
            this.lblTipoPersonaForm.Text = "Tipo de Persona:";
            // 
            // cmbTipoPersonaForm
            // 
            this.cmbTipoPersonaForm.BackColor = System.Drawing.Color.White;
            this.cmbTipoPersonaForm.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTipoPersonaForm.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.cmbTipoPersonaForm.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.cmbTipoPersonaForm.Location = new System.Drawing.Point(229, 114);
            this.cmbTipoPersonaForm.Name = "cmbTipoPersonaForm";
            this.cmbTipoPersonaForm.Size = new System.Drawing.Size(250, 33);
            this.cmbTipoPersonaForm.TabIndex = 1;
            // 
            // grpPersonaFisica
            // 
            this.grpPersonaFisica.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpPersonaFisica.BackColor = System.Drawing.Color.White;
            this.grpPersonaFisica.Controls.Add(this.tableLayoutFisica);
            this.grpPersonaFisica.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.grpPersonaFisica.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.grpPersonaFisica.Location = new System.Drawing.Point(20, 180);
            this.grpPersonaFisica.Margin = new System.Windows.Forms.Padding(10);
            this.grpPersonaFisica.Name = "grpPersonaFisica";
            this.grpPersonaFisica.Padding = new System.Windows.Forms.Padding(15);
            this.grpPersonaFisica.Size = new System.Drawing.Size(1252, 176);
            this.grpPersonaFisica.TabIndex = 2;
            this.grpPersonaFisica.TabStop = false;
            this.grpPersonaFisica.Text = "👤 Datos de Persona Física";
            // 
            // tableLayoutFisica
            // 
            this.tableLayoutFisica.ColumnCount = 5;
            this.tableLayoutFisica.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutFisica.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutFisica.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutFisica.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutFisica.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutFisica.Controls.Add(this.lblCi, 0, 0);
            this.tableLayoutFisica.Controls.Add(this.txtCi, 0, 1);
            this.tableLayoutFisica.Controls.Add(this.lblNombre, 1, 0);
            this.tableLayoutFisica.Controls.Add(this.txtNombre, 1, 1);
            this.tableLayoutFisica.Controls.Add(this.lblApellido, 2, 0);
            this.tableLayoutFisica.Controls.Add(this.txtApellido, 2, 1);
            this.tableLayoutFisica.Controls.Add(this.lblFechaNacimiento, 3, 0);
            this.tableLayoutFisica.Controls.Add(this.dtpFechaNacimiento, 3, 1);
            this.tableLayoutFisica.Controls.Add(this.lblGenero, 4, 0);
            this.tableLayoutFisica.Controls.Add(this.cmbGenero, 4, 1);
            this.tableLayoutFisica.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutFisica.Location = new System.Drawing.Point(15, 40);
            this.tableLayoutFisica.Name = "tableLayoutFisica";
            this.tableLayoutFisica.Padding = new System.Windows.Forms.Padding(10);
            this.tableLayoutFisica.RowCount = 2;
            this.tableLayoutFisica.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutFisica.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 45F));
            this.tableLayoutFisica.Size = new System.Drawing.Size(1222, 121);
            this.tableLayoutFisica.TabIndex = 0;
            // 
            // lblCi
            // 
            this.lblCi.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCi.AutoSize = true;
            this.lblCi.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblCi.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(76)))), ((int)(((byte)(60)))));
            this.lblCi.Location = new System.Drawing.Point(15, 15);
            this.lblCi.Margin = new System.Windows.Forms.Padding(5, 5, 5, 0);
            this.lblCi.Name = "lblCi";
            this.lblCi.Size = new System.Drawing.Size(230, 23);
            this.lblCi.TabIndex = 0;
            this.lblCi.Text = "🎫 CI *";
            // 
            // txtCi
            // 
            this.txtCi.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCi.BackColor = System.Drawing.Color.White;
            this.txtCi.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCi.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtCi.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.txtCi.Location = new System.Drawing.Point(15, 50);
            this.txtCi.Margin = new System.Windows.Forms.Padding(5);
            this.txtCi.MaxLength = 15;
            this.txtCi.Name = "txtCi";
            this.txtCi.Size = new System.Drawing.Size(230, 30);
            this.txtCi.TabIndex = 1;
            // 
            // lblNombre
            // 
            this.lblNombre.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblNombre.AutoSize = true;
            this.lblNombre.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblNombre.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.lblNombre.Location = new System.Drawing.Point(255, 15);
            this.lblNombre.Margin = new System.Windows.Forms.Padding(5, 5, 5, 0);
            this.lblNombre.Name = "lblNombre";
            this.lblNombre.Size = new System.Drawing.Size(230, 23);
            this.lblNombre.TabIndex = 2;
            this.lblNombre.Text = "📝 Nombre";
            // 
            // txtNombre
            // 
            this.txtNombre.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtNombre.BackColor = System.Drawing.Color.White;
            this.txtNombre.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNombre.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtNombre.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.txtNombre.Location = new System.Drawing.Point(255, 50);
            this.txtNombre.Margin = new System.Windows.Forms.Padding(5);
            this.txtNombre.MaxLength = 100;
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.Size = new System.Drawing.Size(230, 30);
            this.txtNombre.TabIndex = 3;
            // 
            // lblApellido
            // 
            this.lblApellido.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblApellido.AutoSize = true;
            this.lblApellido.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblApellido.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(76)))), ((int)(((byte)(60)))));
            this.lblApellido.Location = new System.Drawing.Point(495, 15);
            this.lblApellido.Margin = new System.Windows.Forms.Padding(5, 5, 5, 0);
            this.lblApellido.Name = "lblApellido";
            this.lblApellido.Size = new System.Drawing.Size(230, 23);
            this.lblApellido.TabIndex = 4;
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
            this.txtApellido.Location = new System.Drawing.Point(495, 50);
            this.txtApellido.Margin = new System.Windows.Forms.Padding(5);
            this.txtApellido.MaxLength = 100;
            this.txtApellido.Name = "txtApellido";
            this.txtApellido.Size = new System.Drawing.Size(230, 30);
            this.txtApellido.TabIndex = 5;
            // 
            // lblFechaNacimiento
            // 
            this.lblFechaNacimiento.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblFechaNacimiento.AutoSize = true;
            this.lblFechaNacimiento.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblFechaNacimiento.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.lblFechaNacimiento.Location = new System.Drawing.Point(735, 15);
            this.lblFechaNacimiento.Margin = new System.Windows.Forms.Padding(5, 5, 5, 0);
            this.lblFechaNacimiento.Name = "lblFechaNacimiento";
            this.lblFechaNacimiento.Size = new System.Drawing.Size(230, 23);
            this.lblFechaNacimiento.TabIndex = 6;
            this.lblFechaNacimiento.Text = "📅 Fecha Nacimiento";
            // 
            // dtpFechaNacimiento
            // 
            this.dtpFechaNacimiento.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dtpFechaNacimiento.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.dtpFechaNacimiento.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFechaNacimiento.Location = new System.Drawing.Point(735, 50);
            this.dtpFechaNacimiento.Margin = new System.Windows.Forms.Padding(5);
            this.dtpFechaNacimiento.Name = "dtpFechaNacimiento";
            this.dtpFechaNacimiento.Size = new System.Drawing.Size(230, 30);
            this.dtpFechaNacimiento.TabIndex = 7;
            // 
            // lblGenero
            // 
            this.lblGenero.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblGenero.AutoSize = true;
            this.lblGenero.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblGenero.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.lblGenero.Location = new System.Drawing.Point(975, 15);
            this.lblGenero.Margin = new System.Windows.Forms.Padding(5, 5, 5, 0);
            this.lblGenero.Name = "lblGenero";
            this.lblGenero.Size = new System.Drawing.Size(232, 23);
            this.lblGenero.TabIndex = 8;
            this.lblGenero.Text = "🐾 Género";
            // 
            // cmbGenero
            // 
            this.cmbGenero.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbGenero.BackColor = System.Drawing.Color.White;
            this.cmbGenero.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbGenero.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbGenero.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbGenero.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.cmbGenero.Location = new System.Drawing.Point(975, 50);
            this.cmbGenero.Margin = new System.Windows.Forms.Padding(5);
            this.cmbGenero.Name = "cmbGenero";
            this.cmbGenero.Size = new System.Drawing.Size(232, 31);
            this.cmbGenero.TabIndex = 9;
            // 
            // grpPersonaJuridica
            // 
            this.grpPersonaJuridica.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpPersonaJuridica.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.grpPersonaJuridica.Controls.Add(this.tableLayoutJuridica);
            this.grpPersonaJuridica.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.grpPersonaJuridica.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(174)))), ((int)(((byte)(96)))));
            this.grpPersonaJuridica.Location = new System.Drawing.Point(20, 180);
            this.grpPersonaJuridica.Margin = new System.Windows.Forms.Padding(10);
            this.grpPersonaJuridica.Name = "grpPersonaJuridica";
            this.grpPersonaJuridica.Padding = new System.Windows.Forms.Padding(15);
            this.grpPersonaJuridica.Size = new System.Drawing.Size(1252, 176);
            this.grpPersonaJuridica.TabIndex = 3;
            this.grpPersonaJuridica.TabStop = false;
            this.grpPersonaJuridica.Text = "🏢 Datos de Persona Jurídica";
            this.grpPersonaJuridica.Visible = false;
            // 
            // txtEncargadoCargo
            // 
            this.txtEncargadoCargo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtEncargadoCargo.BackColor = System.Drawing.Color.White;
            this.txtEncargadoCargo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtEncargadoCargo.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtEncargadoCargo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.txtEncargadoCargo.Location = new System.Drawing.Point(991, 50);
            this.txtEncargadoCargo.Margin = new System.Windows.Forms.Padding(5);
            this.txtEncargadoCargo.MaxLength = 100;
            this.txtEncargadoCargo.Name = "txtEncargadoCargo";
            this.txtEncargadoCargo.Size = new System.Drawing.Size(216, 30);
            this.txtEncargadoCargo.TabIndex = 7;
            // 
            // lblEncargadoCargo
            // 
            this.lblEncargadoCargo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblEncargadoCargo.AutoSize = true;
            this.lblEncargadoCargo.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblEncargadoCargo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.lblEncargadoCargo.Location = new System.Drawing.Point(991, 15);
            this.lblEncargadoCargo.Margin = new System.Windows.Forms.Padding(5, 5, 5, 0);
            this.lblEncargadoCargo.Name = "lblEncargadoCargo";
            this.lblEncargadoCargo.Size = new System.Drawing.Size(216, 23);
            this.lblEncargadoCargo.TabIndex = 6;
            this.lblEncargadoCargo.Text = "💼 Cargo";
            // 
            // txtEncargadoNombre
            // 
            this.txtEncargadoNombre.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtEncargadoNombre.BackColor = System.Drawing.Color.White;
            this.txtEncargadoNombre.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtEncargadoNombre.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtEncargadoNombre.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.txtEncargadoNombre.Location = new System.Drawing.Point(747, 50);
            this.txtEncargadoNombre.Margin = new System.Windows.Forms.Padding(5);
            this.txtEncargadoNombre.MaxLength = 100;
            this.txtEncargadoNombre.Name = "txtEncargadoNombre";
            this.txtEncargadoNombre.Size = new System.Drawing.Size(234, 30);
            this.txtEncargadoNombre.TabIndex = 5;
            // 
            // lblEncargadoNombre
            // 
            this.lblEncargadoNombre.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblEncargadoNombre.AutoSize = true;
            this.lblEncargadoNombre.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblEncargadoNombre.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.lblEncargadoNombre.Location = new System.Drawing.Point(747, 15);
            this.lblEncargadoNombre.Margin = new System.Windows.Forms.Padding(5, 5, 5, 0);
            this.lblEncargadoNombre.Name = "lblEncargadoNombre";
            this.lblEncargadoNombre.Size = new System.Drawing.Size(234, 23);
            this.lblEncargadoNombre.TabIndex = 4;
            this.lblEncargadoNombre.Text = "👤 Encargado";
            // 
            // txtNit
            // 
            this.txtNit.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtNit.BackColor = System.Drawing.Color.White;
            this.txtNit.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNit.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtNit.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.txtNit.Location = new System.Drawing.Point(503, 50);
            this.txtNit.Margin = new System.Windows.Forms.Padding(5);
            this.txtNit.MaxLength = 15;
            this.txtNit.Name = "txtNit";
            this.txtNit.Size = new System.Drawing.Size(234, 30);
            this.txtNit.TabIndex = 3;
            // 
            // lblNit
            // 
            this.lblNit.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblNit.AutoSize = true;
            this.lblNit.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblNit.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.lblNit.Location = new System.Drawing.Point(503, 15);
            this.lblNit.Margin = new System.Windows.Forms.Padding(5, 5, 5, 0);
            this.lblNit.Name = "lblNit";
            this.lblNit.Size = new System.Drawing.Size(234, 23);
            this.lblNit.TabIndex = 2;
            this.lblNit.Text = "🎫 NIT";
            // 
            // txtRazonSocial
            // 
            this.txtRazonSocial.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtRazonSocial.BackColor = System.Drawing.Color.White;
            this.txtRazonSocial.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtRazonSocial.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtRazonSocial.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.txtRazonSocial.Location = new System.Drawing.Point(15, 50);
            this.txtRazonSocial.Margin = new System.Windows.Forms.Padding(5);
            this.txtRazonSocial.MaxLength = 255;
            this.txtRazonSocial.Name = "txtRazonSocial";
            this.txtRazonSocial.Size = new System.Drawing.Size(478, 30);
            this.txtRazonSocial.TabIndex = 1;
            // 
            // lblRazonSocial
            // 
            this.lblRazonSocial.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblRazonSocial.AutoSize = true;
            this.lblRazonSocial.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblRazonSocial.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(76)))), ((int)(((byte)(60)))));
            this.lblRazonSocial.Location = new System.Drawing.Point(15, 15);
            this.lblRazonSocial.Margin = new System.Windows.Forms.Padding(5, 5, 5, 0);
            this.lblRazonSocial.Name = "lblRazonSocial";
            this.lblRazonSocial.Size = new System.Drawing.Size(478, 23);
            this.lblRazonSocial.TabIndex = 0;
            this.lblRazonSocial.Text = "🏢 Razón Social *";
            // 
            // tableLayoutJuridica
            // 
            this.tableLayoutJuridica.ColumnCount = 4;
            this.tableLayoutJuridica.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tableLayoutJuridica.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutJuridica.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutJuridica.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutJuridica.Controls.Add(this.lblRazonSocial, 0, 0);
            this.tableLayoutJuridica.Controls.Add(this.txtRazonSocial, 0, 1);
            this.tableLayoutJuridica.Controls.Add(this.lblNit, 1, 0);
            this.tableLayoutJuridica.Controls.Add(this.txtNit, 1, 1);
            this.tableLayoutJuridica.Controls.Add(this.lblEncargadoNombre, 2, 0);
            this.tableLayoutJuridica.Controls.Add(this.txtEncargadoNombre, 2, 1);
            this.tableLayoutJuridica.Controls.Add(this.lblEncargadoCargo, 3, 0);
            this.tableLayoutJuridica.Controls.Add(this.txtEncargadoCargo, 3, 1);
            this.tableLayoutJuridica.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutJuridica.Location = new System.Drawing.Point(15, 40);
            this.tableLayoutJuridica.Name = "tableLayoutJuridica";
            this.tableLayoutJuridica.Padding = new System.Windows.Forms.Padding(10);
            this.tableLayoutJuridica.RowCount = 2;
            this.tableLayoutJuridica.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutJuridica.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 45F));
            this.tableLayoutJuridica.Size = new System.Drawing.Size(1222, 121);
            this.tableLayoutJuridica.TabIndex = 0;
            // 
            // grpDatosComunes
            // 
            this.grpDatosComunes.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpDatosComunes.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.grpDatosComunes.Controls.Add(this.txtTelefono);
            this.grpDatosComunes.Controls.Add(this.lblTelefono);
            this.grpDatosComunes.Controls.Add(this.txtDireccion);
            this.grpDatosComunes.Controls.Add(this.lblDireccion);
            this.grpDatosComunes.Controls.Add(this.txtEmail);
            this.grpDatosComunes.Controls.Add(this.lblEmail);
            this.grpDatosComunes.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.grpDatosComunes.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(89)))), ((int)(((byte)(182)))));
            this.grpDatosComunes.Location = new System.Drawing.Point(99, 397);
            this.grpDatosComunes.Name = "grpDatosComunes";
            this.grpDatosComunes.Padding = new System.Windows.Forms.Padding(15);
            this.grpDatosComunes.Size = new System.Drawing.Size(1068, 213);
            this.grpDatosComunes.TabIndex = 4;
            this.grpDatosComunes.TabStop = false;
            this.grpDatosComunes.Text = "Datos de Contacto";
            // 
            // txtTelefono
            // 
            this.txtTelefono.BackColor = System.Drawing.Color.White;
            this.txtTelefono.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTelefono.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtTelefono.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.txtTelefono.Location = new System.Drawing.Point(480, 50);
            this.txtTelefono.MaxLength = 15;
            this.txtTelefono.Name = "txtTelefono";
            this.txtTelefono.Size = new System.Drawing.Size(180, 27);
            this.txtTelefono.TabIndex = 5;
            // 
            // lblTelefono
            // 
            this.lblTelefono.AutoSize = true;
            this.lblTelefono.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblTelefono.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.lblTelefono.Location = new System.Drawing.Point(480, 30);
            this.lblTelefono.Name = "lblTelefono";
            this.lblTelefono.Size = new System.Drawing.Size(70, 20);
            this.lblTelefono.TabIndex = 4;
            this.lblTelefono.Text = "Teléfono:";
            // 
            // txtDireccion
            // 
            this.txtDireccion.BackColor = System.Drawing.Color.White;
            this.txtDireccion.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDireccion.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtDireccion.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.txtDireccion.Location = new System.Drawing.Point(50, 100);
            this.txtDireccion.MaxLength = 255;
            this.txtDireccion.Multiline = true;
            this.txtDireccion.Name = "txtDireccion";
            this.txtDireccion.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtDireccion.Size = new System.Drawing.Size(950, 60);
            this.txtDireccion.TabIndex = 3;
            // 
            // lblDireccion
            // 
            this.lblDireccion.AutoSize = true;
            this.lblDireccion.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblDireccion.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.lblDireccion.Location = new System.Drawing.Point(50, 80);
            this.lblDireccion.Name = "lblDireccion";
            this.lblDireccion.Size = new System.Drawing.Size(75, 20);
            this.lblDireccion.TabIndex = 2;
            this.lblDireccion.Text = "Dirección:";
            // 
            // txtEmail
            // 
            this.txtEmail.BackColor = System.Drawing.Color.White;
            this.txtEmail.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtEmail.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtEmail.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.txtEmail.Location = new System.Drawing.Point(50, 50);
            this.txtEmail.MaxLength = 100;
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(400, 27);
            this.txtEmail.TabIndex = 1;
            // 
            // lblEmail
            // 
            this.lblEmail.AutoSize = true;
            this.lblEmail.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblEmail.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.lblEmail.Location = new System.Drawing.Point(50, 30);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.Size = new System.Drawing.Size(49, 20);
            this.lblEmail.TabIndex = 0;
            this.lblEmail.Text = "Email:";
            // 
            // tableLayoutComunes
            // 
            this.tableLayoutComunes.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutComunes.Name = "tableLayoutComunes";
            this.tableLayoutComunes.Size = new System.Drawing.Size(200, 100);
            this.tableLayoutComunes.TabIndex = 0;
            // 
            // ClientesModule
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "ClientesModule";
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
            this.grpPersonaFisica.ResumeLayout(false);
            this.tableLayoutFisica.ResumeLayout(false);
            this.tableLayoutFisica.PerformLayout();
            this.grpPersonaJuridica.ResumeLayout(false);
            this.grpPersonaJuridica.PerformLayout();
            this.grpDatosComunes.ResumeLayout(false);
            this.grpDatosComunes.PerformLayout();
            this.ResumeLayout(false);

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
        private TableLayoutPanel tableLayoutFisica;
        private TableLayoutPanel tableLayoutJuridica;
        private TableLayoutPanel tableLayoutComunes;
    }
}
