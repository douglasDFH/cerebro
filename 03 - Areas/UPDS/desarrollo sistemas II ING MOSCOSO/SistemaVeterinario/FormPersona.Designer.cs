namespace VeterinariaApp
{
    partial class FormPersona
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rbFiltroJuridica = new System.Windows.Forms.RadioButton();
            this.rbFiltroFisica = new System.Windows.Forms.RadioButton();
            this.rbFiltroTodos = new System.Windows.Forms.RadioButton();
            this.btnEliminar = new System.Windows.Forms.Button();
            this.chkEliminar = new System.Windows.Forms.CheckBox();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.txtBuscar = new System.Windows.Forms.TextBox();
            this.lblTotal = new System.Windows.Forms.Label();
            this.dataListado = new System.Windows.Forms.DataGridView();
            this.Eliminar = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.rbGeneroOtro = new System.Windows.Forms.RadioButton();
            this.rbGeneroMasculino = new System.Windows.Forms.RadioButton();
            this.rbGeneroFemenino = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbPersonaJuridica = new System.Windows.Forms.RadioButton();
            this.rbPersonaFisica = new System.Windows.Forms.RadioButton();
            this.txtObservaciones = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtTelefonoAlternativo = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnEditar = new System.Windows.Forms.Button();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.btnNuevo = new System.Windows.Forms.Button();
            this.txtTelefono = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtDireccion = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtRazonSocial = new System.Windows.Forms.TextBox();
            this.lblRazonSocial = new System.Windows.Forms.Label();
            this.txtApellidos = new System.Windows.Forms.TextBox();
            this.lblApellidos = new System.Windows.Forms.Label();
            this.txtNombre = new System.Windows.Forms.TextBox();
            this.lblNombre = new System.Windows.Forms.Label();
            this.txtCIF = new System.Windows.Forms.TextBox();
            this.lblCIF = new System.Windows.Forms.Label();
            this.txtDNI = new System.Windows.Forms.TextBox();
            this.lblDNI = new System.Windows.Forms.Label();
            this.txtIdPersona = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.ttMensaje = new System.Windows.Forms.ToolTip(this.components);
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataListado)).BeginInit();
            this.tabPage2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(800, 600);
            this.tabControl1.TabIndex = 0;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.groupBox2);
            this.tabPage1.Controls.Add(this.btnEliminar);
            this.tabPage1.Controls.Add(this.chkEliminar);
            this.tabPage1.Controls.Add(this.btnBuscar);
            this.tabPage1.Controls.Add(this.txtBuscar);
            this.tabPage1.Controls.Add(this.lblTotal);
            this.tabPage1.Controls.Add(this.dataListado);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(792, 574);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Listado";
            this.tabPage1.UseVisualStyleBackColor = true;
            this.tabPage1.Click += new System.EventHandler(this.tabPage1_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rbFiltroJuridica);
            this.groupBox2.Controls.Add(this.rbFiltroFisica);
            this.groupBox2.Controls.Add(this.rbFiltroTodos);
            this.groupBox2.Location = new System.Drawing.Point(250, 15);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(300, 50);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Filtro por Tipo";
            // 
            // rbFiltroJuridica
            // 
            this.rbFiltroJuridica.AutoSize = true;
            this.rbFiltroJuridica.Location = new System.Drawing.Point(195, 20);
            this.rbFiltroJuridica.Name = "rbFiltroJuridica";
            this.rbFiltroJuridica.Size = new System.Drawing.Size(65, 17);
            this.rbFiltroJuridica.TabIndex = 2;
            this.rbFiltroJuridica.Text = "Jurídica";
            this.rbFiltroJuridica.UseVisualStyleBackColor = true;
            this.rbFiltroJuridica.CheckedChanged += new System.EventHandler(this.rbFiltroJuridica_CheckedChanged);
            // 
            // rbFiltroFisica
            // 
            this.rbFiltroFisica.AutoSize = true;
            this.rbFiltroFisica.Location = new System.Drawing.Point(120, 20);
            this.rbFiltroFisica.Name = "rbFiltroFisica";
            this.rbFiltroFisica.Size = new System.Drawing.Size(52, 17);
            this.rbFiltroFisica.TabIndex = 1;
            this.rbFiltroFisica.Text = "Física";
            this.rbFiltroFisica.UseVisualStyleBackColor = true;
            this.rbFiltroFisica.CheckedChanged += new System.EventHandler(this.rbFiltroFisica_CheckedChanged);
            // 
            // rbFiltroTodos
            // 
            this.rbFiltroTodos.AutoSize = true;
            this.rbFiltroTodos.Checked = true;
            this.rbFiltroTodos.Location = new System.Drawing.Point(20, 20);
            this.rbFiltroTodos.Name = "rbFiltroTodos";
            this.rbFiltroTodos.Size = new System.Drawing.Size(55, 17);
            this.rbFiltroTodos.TabIndex = 0;
            this.rbFiltroTodos.TabStop = true;
            this.rbFiltroTodos.Text = "Todos";
            this.rbFiltroTodos.UseVisualStyleBackColor = true;
            this.rbFiltroTodos.CheckedChanged += new System.EventHandler(this.rbFiltroTodos_CheckedChanged);
            // 
            // btnEliminar
            // 
            this.btnEliminar.Location = new System.Drawing.Point(95, 40);
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Size = new System.Drawing.Size(75, 23);
            this.btnEliminar.TabIndex = 5;
            this.btnEliminar.Text = "Eliminar";
            this.btnEliminar.UseVisualStyleBackColor = true;
            this.btnEliminar.Click += new System.EventHandler(this.btnEliminar_Click);
            // 
            // chkEliminar
            // 
            this.chkEliminar.AutoSize = true;
            this.chkEliminar.Location = new System.Drawing.Point(20, 44);
            this.chkEliminar.Name = "chkEliminar";
            this.chkEliminar.Size = new System.Drawing.Size(69, 17);
            this.chkEliminar.TabIndex = 4;
            this.chkEliminar.Text = "Eliminar";
            this.chkEliminar.UseVisualStyleBackColor = true;
            this.chkEliminar.CheckedChanged += new System.EventHandler(this.chkEliminar_CheckedChanged);
            // 
            // btnBuscar
            // 
            this.btnBuscar.Location = new System.Drawing.Point(180, 15);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(60, 23);
            this.btnBuscar.TabIndex = 3;
            this.btnBuscar.Text = "Buscar";
            this.btnBuscar.UseVisualStyleBackColor = true;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // txtBuscar
            // 
            this.txtBuscar.Location = new System.Drawing.Point(20, 17);
            this.txtBuscar.Name = "txtBuscar";
            this.txtBuscar.Size = new System.Drawing.Size(150, 20);
            this.txtBuscar.TabIndex = 2;
            this.txtBuscar.TextChanged += new System.EventHandler(this.txtBuscar_TextChanged);
            // 
            // lblTotal
            // 
            this.lblTotal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblTotal.AutoSize = true;
            this.lblTotal.Location = new System.Drawing.Point(17, 550);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(89, 13);
            this.lblTotal.TabIndex = 1;
            this.lblTotal.Text = "Total Registros: 0";
            this.lblTotal.Click += new System.EventHandler(this.lblTotal_Click);
            // 
            // dataListado
            // 
            this.dataListado.AllowUserToAddRows = false;
            this.dataListado.AllowUserToDeleteRows = false;
            this.dataListado.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataListado.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataListado.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Eliminar});
            this.dataListado.Location = new System.Drawing.Point(20, 80);
            this.dataListado.MultiSelect = false;
            this.dataListado.Name = "dataListado";
            this.dataListado.ReadOnly = true;
            this.dataListado.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataListado.Size = new System.Drawing.Size(752, 460);
            this.dataListado.TabIndex = 0;
            this.dataListado.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataListado_CellClick);
            this.dataListado.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataListado_CellContentClick);
            this.dataListado.DoubleClick += new System.EventHandler(this.dataListado_DoubleClick);
            // 
            // Eliminar
            // 
            this.Eliminar.HeaderText = "Eliminar";
            this.Eliminar.Name = "Eliminar";
            this.Eliminar.ReadOnly = true;
            this.Eliminar.Visible = false;
            this.Eliminar.Width = 60;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.groupBox3);
            this.tabPage2.Controls.Add(this.groupBox1);
            this.tabPage2.Controls.Add(this.txtObservaciones);
            this.tabPage2.Controls.Add(this.label11);
            this.tabPage2.Controls.Add(this.txtTelefonoAlternativo);
            this.tabPage2.Controls.Add(this.label10);
            this.tabPage2.Controls.Add(this.btnCancelar);
            this.tabPage2.Controls.Add(this.btnEditar);
            this.tabPage2.Controls.Add(this.btnGuardar);
            this.tabPage2.Controls.Add(this.btnNuevo);
            this.tabPage2.Controls.Add(this.txtTelefono);
            this.tabPage2.Controls.Add(this.label5);
            this.tabPage2.Controls.Add(this.txtDireccion);
            this.tabPage2.Controls.Add(this.label4);
            this.tabPage2.Controls.Add(this.txtEmail);
            this.tabPage2.Controls.Add(this.label3);
            this.tabPage2.Controls.Add(this.txtRazonSocial);
            this.tabPage2.Controls.Add(this.lblRazonSocial);
            this.tabPage2.Controls.Add(this.txtApellidos);
            this.tabPage2.Controls.Add(this.lblApellidos);
            this.tabPage2.Controls.Add(this.txtNombre);
            this.tabPage2.Controls.Add(this.lblNombre);
            this.tabPage2.Controls.Add(this.txtCIF);
            this.tabPage2.Controls.Add(this.lblCIF);
            this.tabPage2.Controls.Add(this.txtDNI);
            this.tabPage2.Controls.Add(this.lblDNI);
            this.tabPage2.Controls.Add(this.txtIdPersona);
            this.tabPage2.Controls.Add(this.label2);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(792, 574);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Mantenimiento";
            this.tabPage2.UseVisualStyleBackColor = true;
            this.tabPage2.Click += new System.EventHandler(this.tabPage2_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.rbGeneroOtro);
            this.groupBox3.Controls.Add(this.rbGeneroMasculino);
            this.groupBox3.Controls.Add(this.rbGeneroFemenino);
            this.groupBox3.Location = new System.Drawing.Point(400, 120);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(250, 50);
            this.groupBox3.TabIndex = 27;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Género";
            // 
            // rbGeneroOtro
            // 
            this.rbGeneroOtro.AutoSize = true;
            this.rbGeneroOtro.Location = new System.Drawing.Point(150, 20);
            this.rbGeneroOtro.Name = "rbGeneroOtro";
            this.rbGeneroOtro.Size = new System.Drawing.Size(45, 17);
            this.rbGeneroOtro.TabIndex = 2;
            this.rbGeneroOtro.Text = "Otro";
            this.rbGeneroOtro.UseVisualStyleBackColor = true;
            this.rbGeneroOtro.CheckedChanged += new System.EventHandler(this.rbGeneroOtro_CheckedChanged);
            // 
            // rbGeneroMasculino
            // 
            this.rbGeneroMasculino.AutoSize = true;
            this.rbGeneroMasculino.Location = new System.Drawing.Point(75, 20);
            this.rbGeneroMasculino.Name = "rbGeneroMasculino";
            this.rbGeneroMasculino.Size = new System.Drawing.Size(73, 17);
            this.rbGeneroMasculino.TabIndex = 1;
            this.rbGeneroMasculino.Text = "Masculino";
            this.rbGeneroMasculino.UseVisualStyleBackColor = true;
            this.rbGeneroMasculino.CheckedChanged += new System.EventHandler(this.rbGeneroMasculino_CheckedChanged);
            // 
            // rbGeneroFemenino
            // 
            this.rbGeneroFemenino.AutoSize = true;
            this.rbGeneroFemenino.Checked = true;
            this.rbGeneroFemenino.Location = new System.Drawing.Point(10, 20);
            this.rbGeneroFemenino.Name = "rbGeneroFemenino";
            this.rbGeneroFemenino.Size = new System.Drawing.Size(71, 17);
            this.rbGeneroFemenino.TabIndex = 0;
            this.rbGeneroFemenino.TabStop = true;
            this.rbGeneroFemenino.Text = "Femenino";
            this.rbGeneroFemenino.UseVisualStyleBackColor = true;
            this.rbGeneroFemenino.CheckedChanged += new System.EventHandler(this.rbGeneroFemenino_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbPersonaJuridica);
            this.groupBox1.Controls.Add(this.rbPersonaFisica);
            this.groupBox1.Location = new System.Drawing.Point(30, 55);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(200, 50);
            this.groupBox1.TabIndex = 26;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Tipo de Persona";
            // 
            // rbPersonaJuridica
            // 
            this.rbPersonaJuridica.AutoSize = true;
            this.rbPersonaJuridica.Location = new System.Drawing.Point(100, 20);
            this.rbPersonaJuridica.Name = "rbPersonaJuridica";
            this.rbPersonaJuridica.Size = new System.Drawing.Size(65, 17);
            this.rbPersonaJuridica.TabIndex = 1;
            this.rbPersonaJuridica.Text = "Jurídica";
            this.rbPersonaJuridica.UseVisualStyleBackColor = true;
            this.rbPersonaJuridica.CheckedChanged += new System.EventHandler(this.rbPersonaJuridica_CheckedChanged);
            // 
            // rbPersonaFisica
            // 
            this.rbPersonaFisica.AutoSize = true;
            this.rbPersonaFisica.Checked = true;
            this.rbPersonaFisica.Location = new System.Drawing.Point(20, 20);
            this.rbPersonaFisica.Name = "rbPersonaFisica";
            this.rbPersonaFisica.Size = new System.Drawing.Size(52, 17);
            this.rbPersonaFisica.TabIndex = 0;
            this.rbPersonaFisica.TabStop = true;
            this.rbPersonaFisica.Text = "Física";
            this.rbPersonaFisica.UseVisualStyleBackColor = true;
            this.rbPersonaFisica.CheckedChanged += new System.EventHandler(this.rbPersonaFisica_CheckedChanged);
            // 
            // txtObservaciones
            // 
            this.txtObservaciones.Location = new System.Drawing.Point(140, 420);
            this.txtObservaciones.Multiline = true;
            this.txtObservaciones.Name = "txtObservaciones";
            this.txtObservaciones.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtObservaciones.Size = new System.Drawing.Size(350, 80);
            this.txtObservaciones.TabIndex = 25;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(30, 425);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(81, 13);
            this.label11.TabIndex = 24;
            this.label11.Text = "Observaciones:";
            // 
            // txtTelefonoAlternativo
            // 
            this.txtTelefonoAlternativo.Location = new System.Drawing.Point(450, 360);
            this.txtTelefonoAlternativo.Name = "txtTelefonoAlternativo";
            this.txtTelefonoAlternativo.Size = new System.Drawing.Size(200, 20);
            this.txtTelefonoAlternativo.TabIndex = 23;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(400, 340);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(107, 13);
            this.label10.TabIndex = 22;
            this.label10.Text = "Teléfono Alternativo:";
            // 
            // btnCancelar
            // 
            this.btnCancelar.Location = new System.Drawing.Point(480, 520);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(80, 30);
            this.btnCancelar.TabIndex = 21;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // btnEditar
            // 
            this.btnEditar.Location = new System.Drawing.Point(300, 520);
            this.btnEditar.Name = "btnEditar";
            this.btnEditar.Size = new System.Drawing.Size(80, 30);
            this.btnEditar.TabIndex = 20;
            this.btnEditar.Text = "Editar";
            this.btnEditar.UseVisualStyleBackColor = true;
            this.btnEditar.Click += new System.EventHandler(this.btnEditar_Click);
            // 
            // btnGuardar
            // 
            this.btnGuardar.Location = new System.Drawing.Point(210, 520);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(80, 30);
            this.btnGuardar.TabIndex = 19;
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.UseVisualStyleBackColor = true;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // btnNuevo
            // 
            this.btnNuevo.Location = new System.Drawing.Point(120, 520);
            this.btnNuevo.Name = "btnNuevo";
            this.btnNuevo.Size = new System.Drawing.Size(80, 30);
            this.btnNuevo.TabIndex = 18;
            this.btnNuevo.Text = "Nuevo";
            this.btnNuevo.UseVisualStyleBackColor = true;
            this.btnNuevo.Click += new System.EventHandler(this.btnNuevo_Click);
            // 
            // txtTelefono
            // 
            this.txtTelefono.Location = new System.Drawing.Point(140, 360);
            this.txtTelefono.Name = "txtTelefono";
            this.txtTelefono.Size = new System.Drawing.Size(200, 20);
            this.txtTelefono.TabIndex = 17;
            this.txtTelefono.TextChanged += new System.EventHandler(this.txtTelefono_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(30, 363);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(52, 13);
            this.label5.TabIndex = 16;
            this.label5.Text = "Teléfono:";
            this.label5.Click += new System.EventHandler(this.label5_Click);
            // 
            // txtDireccion
            // 
            this.txtDireccion.Location = new System.Drawing.Point(140, 320);
            this.txtDireccion.Name = "txtDireccion";
            this.txtDireccion.Size = new System.Drawing.Size(350, 20);
            this.txtDireccion.TabIndex = 15;
            this.txtDireccion.TextChanged += new System.EventHandler(this.txtDireccion_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(30, 323);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(55, 13);
            this.label4.TabIndex = 14;
            this.label4.Text = "Dirección:";
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // txtEmail
            // 
            this.txtEmail.Location = new System.Drawing.Point(140, 280);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(350, 20);
            this.txtEmail.TabIndex = 13;
            this.txtEmail.TextChanged += new System.EventHandler(this.txtEmail_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(30, 283);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 13);
            this.label3.TabIndex = 12;
            this.label3.Text = "Email:";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // txtRazonSocial
            // 
            this.txtRazonSocial.Location = new System.Drawing.Point(140, 240);
            this.txtRazonSocial.Name = "txtRazonSocial";
            this.txtRazonSocial.Size = new System.Drawing.Size(350, 20);
            this.txtRazonSocial.TabIndex = 11;
            // 
            // lblRazonSocial
            // 
            this.lblRazonSocial.AutoSize = true;
            this.lblRazonSocial.Location = new System.Drawing.Point(30, 243);
            this.lblRazonSocial.Name = "lblRazonSocial";
            this.lblRazonSocial.Size = new System.Drawing.Size(73, 13);
            this.lblRazonSocial.TabIndex = 10;
            this.lblRazonSocial.Text = "Razón Social:";
            // 
            // txtApellidos
            // 
            this.txtApellidos.Location = new System.Drawing.Point(450, 200);
            this.txtApellidos.Name = "txtApellidos";
            this.txtApellidos.Size = new System.Drawing.Size(200, 20);
            this.txtApellidos.TabIndex = 9;
            // 
            // lblApellidos
            // 
            this.lblApellidos.AutoSize = true;
            this.lblApellidos.Location = new System.Drawing.Point(400, 203);
            this.lblApellidos.Name = "lblApellidos";
            this.lblApellidos.Size = new System.Drawing.Size(52, 13);
            this.lblApellidos.TabIndex = 8;
            this.lblApellidos.Text = "Apellidos:";
            // 
            // txtNombre
            // 
            this.txtNombre.Location = new System.Drawing.Point(140, 200);
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.Size = new System.Drawing.Size(200, 20);
            this.txtNombre.TabIndex = 7;
            // 
            // lblNombre
            // 
            this.lblNombre.AutoSize = true;
            this.lblNombre.Location = new System.Drawing.Point(30, 203);
            this.lblNombre.Name = "lblNombre";
            this.lblNombre.Size = new System.Drawing.Size(47, 13);
            this.lblNombre.TabIndex = 6;
            this.lblNombre.Text = "Nombre:";
            // 
            // txtCIF
            // 
            this.txtCIF.Location = new System.Drawing.Point(450, 160);
            this.txtCIF.Name = "txtCIF";
            this.txtCIF.Size = new System.Drawing.Size(200, 20);
            this.txtCIF.TabIndex = 5;
            // 
            // lblCIF
            // 
            this.lblCIF.AutoSize = true;
            this.lblCIF.Location = new System.Drawing.Point(400, 163);
            this.lblCIF.Name = "lblCIF";
            this.lblCIF.Size = new System.Drawing.Size(26, 13);
            this.lblCIF.TabIndex = 4;
            this.lblCIF.Text = "CIF:";
            // 
            // txtDNI
            // 
            this.txtDNI.Location = new System.Drawing.Point(140, 160);
            this.txtDNI.Name = "txtDNI";
            this.txtDNI.Size = new System.Drawing.Size(200, 20);
            this.txtDNI.TabIndex = 3;
            // 
            // lblDNI
            // 
            this.lblDNI.AutoSize = true;
            this.lblDNI.Location = new System.Drawing.Point(30, 163);
            this.lblDNI.Name = "lblDNI";
            this.lblDNI.Size = new System.Drawing.Size(29, 13);
            this.lblDNI.TabIndex = 2;
            this.lblDNI.Text = "DNI:";
            // 
            // txtIdPersona
            // 
            this.txtIdPersona.Location = new System.Drawing.Point(140, 20);
            this.txtIdPersona.Name = "txtIdPersona";
            this.txtIdPersona.ReadOnly = true;
            this.txtIdPersona.Size = new System.Drawing.Size(100, 20);
            this.txtIdPersona.TabIndex = 1;
            this.txtIdPersona.TextChanged += new System.EventHandler(this.txtIdPersona_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(30, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(21, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "ID:";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // FormPersona
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 600);
            this.Controls.Add(this.tabControl1);
            this.Name = "FormPersona";
            this.Text = "Gestión de Personas";
            this.Load += new System.EventHandler(this.FormPersona_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataListado)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.DataGridView dataListado;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.TextBox txtBuscar;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.CheckBox chkEliminar;
        private System.Windows.Forms.Button btnEliminar;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Eliminar;
        private System.Windows.Forms.TextBox txtIdPersona;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtDNI;
        private System.Windows.Forms.Label lblDNI;
        private System.Windows.Forms.TextBox txtCIF;
        private System.Windows.Forms.Label lblCIF;
        private System.Windows.Forms.TextBox txtNombre;
        private System.Windows.Forms.Label lblNombre;
        private System.Windows.Forms.TextBox txtApellidos;
        private System.Windows.Forms.Label lblApellidos;
        private System.Windows.Forms.TextBox txtRazonSocial;
        private System.Windows.Forms.Label lblRazonSocial;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtDireccion;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtTelefono;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnNuevo;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.Button btnEditar;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.TextBox txtTelefonoAlternativo;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtObservaciones;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbPersonaJuridica;
        private System.Windows.Forms.RadioButton rbPersonaFisica;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RadioButton rbGeneroOtro;
        private System.Windows.Forms.RadioButton rbGeneroMasculino;
        private System.Windows.Forms.RadioButton rbGeneroFemenino;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton rbFiltroJuridica;
        private System.Windows.Forms.RadioButton rbFiltroFisica;
        private System.Windows.Forms.RadioButton rbFiltroTodos;
        private System.Windows.Forms.ToolTip ttMensaje;
    }
}