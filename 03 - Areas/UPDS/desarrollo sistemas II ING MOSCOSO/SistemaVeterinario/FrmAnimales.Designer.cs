namespace VeterinariaApp
{
    partial class FrmAnimales
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.dataListado = new System.Windows.Forms.DataGridView();
            this.Eliminar = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.chkEliminar = new System.Windows.Forms.CheckBox();
            this.btnEliminar = new System.Windows.Forms.Button();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.txtBuscar = new System.Windows.Forms.TextBox();
            this.lblTotal = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chkVacunado = new System.Windows.Forms.CheckBox();
            this.chkEsterilizado = new System.Windows.Forms.CheckBox();
            this.txtNumPedigree = new System.Windows.Forms.TextBox();
            this.lblNumPedigree = new System.Windows.Forms.Label();
            this.txtAltura = new System.Windows.Forms.TextBox();
            this.lblAltura = new System.Windows.Forms.Label();
            this.lblEdad = new System.Windows.Forms.Label();
            this.txtObservaciones = new System.Windows.Forms.TextBox();
            this.lblObservaciones = new System.Windows.Forms.Label();
            this.txtMicrochip = new System.Windows.Forms.TextBox();
            this.lblMicrochip = new System.Windows.Forms.Label();
            this.txtPeso = new System.Windows.Forms.TextBox();
            this.lblPeso = new System.Windows.Forms.Label();
            this.chkFechaNacimiento = new System.Windows.Forms.CheckBox();
            this.dtpFechaNacimiento = new System.Windows.Forms.DateTimePicker();
            this.lblFechaNacimiento = new System.Windows.Forms.Label();
            this.cmbSexo = new System.Windows.Forms.ComboBox();
            this.lblSexo = new System.Windows.Forms.Label();
            this.txtColor = new System.Windows.Forms.TextBox();
            this.lblColor = new System.Windows.Forms.Label();
            this.cmbRaza = new System.Windows.Forms.ComboBox();
            this.lblRaza = new System.Windows.Forms.Label();
            this.cmbTipo = new System.Windows.Forms.ComboBox();
            this.lblTipo = new System.Windows.Forms.Label();
            this.txtNombre = new System.Windows.Forms.TextBox();
            this.lblNombre = new System.Windows.Forms.Label();
            this.cmbPropietario = new System.Windows.Forms.ComboBox();
            this.lblPropietario = new System.Windows.Forms.Label();
            this.txtIdAnimal = new System.Windows.Forms.TextBox();
            this.lblIdAnimal = new System.Windows.Forms.Label();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnEditar = new System.Windows.Forms.Button();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.btnNuevo = new System.Windows.Forms.Button();
            this.ttMensaje = new System.Windows.Forms.ToolTip(this.components);
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataListado)).BeginInit();
            this.tabPage2.SuspendLayout();
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
            this.tabControl1.Size = new System.Drawing.Size(1000, 600);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.dataListado);
            this.tabPage1.Controls.Add(this.chkEliminar);
            this.tabPage1.Controls.Add(this.btnEliminar);
            this.tabPage1.Controls.Add(this.btnBuscar);
            this.tabPage1.Controls.Add(this.txtBuscar);
            this.tabPage1.Controls.Add(this.lblTotal);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(992, 574);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Lista de Animales";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // dataListado
            // 
            this.dataListado.AllowUserToAddRows = false;
            this.dataListado.AllowUserToDeleteRows = false;
            this.dataListado.AllowUserToOrderColumns = true;
            this.dataListado.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataListado.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataListado.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataListado.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Eliminar});
            this.dataListado.Location = new System.Drawing.Point(6, 70);
            this.dataListado.MultiSelect = false;
            this.dataListado.Name = "dataListado";
            this.dataListado.ReadOnly = true;
            this.dataListado.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataListado.Size = new System.Drawing.Size(980, 450);
            this.dataListado.TabIndex = 5;
            this.dataListado.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataListado_CellContentClick);
            this.dataListado.DoubleClick += new System.EventHandler(this.dataListado_DoubleClick);
            // 
            // Eliminar
            // 
            this.Eliminar.HeaderText = "Eliminar";
            this.Eliminar.Name = "Eliminar";
            this.Eliminar.ReadOnly = true;
            this.Eliminar.Width = 69;
            // 
            // chkEliminar
            // 
            this.chkEliminar.AutoSize = true;
            this.chkEliminar.Location = new System.Drawing.Point(9, 47);
            this.chkEliminar.Name = "chkEliminar";
            this.chkEliminar.Size = new System.Drawing.Size(69, 17);
            this.chkEliminar.TabIndex = 4;
            this.chkEliminar.Text = "Eliminar";
            this.chkEliminar.UseVisualStyleBackColor = true;
            this.chkEliminar.CheckedChanged += new System.EventHandler(this.chkEliminar_CheckedChanged);
            // 
            // btnEliminar
            // 
            this.btnEliminar.BackColor = System.Drawing.Color.IndianRed;
            this.btnEliminar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEliminar.ForeColor = System.Drawing.Color.White;
            this.btnEliminar.Location = new System.Drawing.Point(84, 43);
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Size = new System.Drawing.Size(75, 23);
            this.btnEliminar.TabIndex = 3;
            this.btnEliminar.Text = "Eliminar";
            this.btnEliminar.UseVisualStyleBackColor = false;
            this.btnEliminar.Click += new System.EventHandler(this.btnEliminar_Click);
            // 
            // btnBuscar
            // 
            this.btnBuscar.BackColor = System.Drawing.Color.SteelBlue;
            this.btnBuscar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBuscar.ForeColor = System.Drawing.Color.White;
            this.btnBuscar.Location = new System.Drawing.Point(304, 14);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(75, 23);
            this.btnBuscar.TabIndex = 2;
            this.btnBuscar.Text = "Buscar";
            this.btnBuscar.UseVisualStyleBackColor = false;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // txtBuscar
            // 
            this.txtBuscar.Location = new System.Drawing.Point(9, 16);
            this.txtBuscar.Name = "txtBuscar";
            this.txtBuscar.Size = new System.Drawing.Size(289, 20);
            this.txtBuscar.TabIndex = 1;
            this.txtBuscar.TextChanged += new System.EventHandler(this.txtBuscar_TextChanged);
            // 
            // lblTotal
            // 
            this.lblTotal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTotal.AutoSize = true;
            this.lblTotal.Location = new System.Drawing.Point(800, 530);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(35, 13);
            this.lblTotal.TabIndex = 0;
            this.lblTotal.Text = "Total:";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.groupBox1);
            this.tabPage2.Controls.Add(this.btnCancelar);
            this.tabPage2.Controls.Add(this.btnEditar);
            this.tabPage2.Controls.Add(this.btnGuardar);
            this.tabPage2.Controls.Add(this.btnNuevo);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(992, 574);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Mantenimiento";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chkVacunado);
            this.groupBox1.Controls.Add(this.chkEsterilizado);
            this.groupBox1.Controls.Add(this.txtNumPedigree);
            this.groupBox1.Controls.Add(this.lblNumPedigree);
            this.groupBox1.Controls.Add(this.txtAltura);
            this.groupBox1.Controls.Add(this.lblAltura);
            this.groupBox1.Controls.Add(this.lblEdad);
            this.groupBox1.Controls.Add(this.txtObservaciones);
            this.groupBox1.Controls.Add(this.lblObservaciones);
            this.groupBox1.Controls.Add(this.txtMicrochip);
            this.groupBox1.Controls.Add(this.lblMicrochip);
            this.groupBox1.Controls.Add(this.txtPeso);
            this.groupBox1.Controls.Add(this.lblPeso);
            this.groupBox1.Controls.Add(this.chkFechaNacimiento);
            this.groupBox1.Controls.Add(this.dtpFechaNacimiento);
            this.groupBox1.Controls.Add(this.lblFechaNacimiento);
            this.groupBox1.Controls.Add(this.cmbSexo);
            this.groupBox1.Controls.Add(this.lblSexo);
            this.groupBox1.Controls.Add(this.txtColor);
            this.groupBox1.Controls.Add(this.lblColor);
            this.groupBox1.Controls.Add(this.cmbRaza);
            this.groupBox1.Controls.Add(this.lblRaza);
            this.groupBox1.Controls.Add(this.cmbTipo);
            this.groupBox1.Controls.Add(this.lblTipo);
            this.groupBox1.Controls.Add(this.txtNombre);
            this.groupBox1.Controls.Add(this.lblNombre);
            this.groupBox1.Controls.Add(this.cmbPropietario);
            this.groupBox1.Controls.Add(this.lblPropietario);
            this.groupBox1.Controls.Add(this.txtIdAnimal);
            this.groupBox1.Controls.Add(this.lblIdAnimal);
            this.groupBox1.Location = new System.Drawing.Point(6, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(980, 480);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Datos del Animal";
            // 
            // chkVacunado
            // 
            this.chkVacunado.AutoSize = true;
            this.chkVacunado.Location = new System.Drawing.Point(520, 380);
            this.chkVacunado.Name = "chkVacunado";
            this.chkVacunado.Size = new System.Drawing.Size(74, 17);
            this.chkVacunado.TabIndex = 27;
            this.chkVacunado.Text = "Vacunado";
            this.chkVacunado.UseVisualStyleBackColor = true;
            // 
            // chkEsterilizado
            // 
            this.chkEsterilizado.AutoSize = true;
            this.chkEsterilizado.Location = new System.Drawing.Point(430, 380);
            this.chkEsterilizado.Name = "chkEsterilizado";
            this.chkEsterilizado.Size = new System.Drawing.Size(79, 17);
            this.chkEsterilizado.TabIndex = 26;
            this.chkEsterilizado.Text = "Esterilizado";
            this.chkEsterilizado.UseVisualStyleBackColor = true;
            // 
            // txtNumPedigree
            // 
            this.txtNumPedigree.Location = new System.Drawing.Point(430, 340);
            this.txtNumPedigree.MaxLength = 50;
            this.txtNumPedigree.Name = "txtNumPedigree";
            this.txtNumPedigree.Size = new System.Drawing.Size(200, 20);
            this.txtNumPedigree.TabIndex = 25;
            // 
            // lblNumPedigree
            // 
            this.lblNumPedigree.AutoSize = true;
            this.lblNumPedigree.Location = new System.Drawing.Point(427, 324);
            this.lblNumPedigree.Name = "lblNumPedigree";
            this.lblNumPedigree.Size = new System.Drawing.Size(97, 13);
            this.lblNumPedigree.TabIndex = 24;
            this.lblNumPedigree.Text = "Número Pedigrí:";
            // 
            // txtAltura
            // 
            this.txtAltura.Location = new System.Drawing.Point(430, 280);
            this.txtAltura.Name = "txtAltura";
            this.txtAltura.Size = new System.Drawing.Size(100, 20);
            this.txtAltura.TabIndex = 23;
            // 
            // lblAltura
            // 
            this.lblAltura.AutoSize = true;
            this.lblAltura.Location = new System.Drawing.Point(427, 264);
            this.lblAltura.Name = "lblAltura";
            this.lblAltura.Size = new System.Drawing.Size(64, 13);
            this.lblAltura.TabIndex = 22;
            this.lblAltura.Text = "Altura (cm):";
            // 
            // lblEdad
            // 
            this.lblEdad.AutoSize = true;
            this.lblEdad.ForeColor = System.Drawing.Color.Blue;
            this.lblEdad.Location = new System.Drawing.Point(240, 244);
            this.lblEdad.Name = "lblEdad";
            this.lblEdad.Size = new System.Drawing.Size(32, 13);
            this.lblEdad.TabIndex = 28;
            this.lblEdad.Text = "Edad: --";
            // 
            // txtObservaciones
            // 
            this.txtObservaciones.Location = new System.Drawing.Point(15, 410);
            this.txtObservaciones.MaxLength = 500;
            this.txtObservaciones.Multiline = true;
            this.txtObservaciones.Name = "txtObservaciones";
            this.txtObservaciones.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtObservaciones.Size = new System.Drawing.Size(615, 60);
            this.txtObservaciones.TabIndex = 21;
            // 
            // lblObservaciones
            // 
            this.lblObservaciones.AutoSize = true;
            this.lblObservaciones.Location = new System.Drawing.Point(12, 394);
            this.lblObservaciones.Name = "lblObservaciones";
            this.lblObservaciones.Size = new System.Drawing.Size(81, 13);
            this.lblObservaciones.TabIndex = 20;
            this.lblObservaciones.Text = "Observaciones:";
            // 
            // txtMicrochip
            // 
            this.txtMicrochip.Location = new System.Drawing.Point(15, 340);
            this.txtMicrochip.MaxLength = 15;
            this.txtMicrochip.Name = "txtMicrochip";
            this.txtMicrochip.Size = new System.Drawing.Size(200, 20);
            this.txtMicrochip.TabIndex = 19;
            this.txtMicrochip.TextChanged += new System.EventHandler(this.txtMicrochip_TextChanged);
            // 
            // lblMicrochip
            // 
            this.lblMicrochip.AutoSize = true;
            this.lblMicrochip.Location = new System.Drawing.Point(12, 324);
            this.lblMicrochip.Name = "lblMicrochip";
            this.lblMicrochip.Size = new System.Drawing.Size(60, 13);
            this.lblMicrochip.TabIndex = 18;
            this.lblMicrochip.Text = "Microchip:";
            // 
            // txtPeso
            // 
            this.txtPeso.Location = new System.Drawing.Point(15, 280);
            this.txtPeso.Name = "txtPeso";
            this.txtPeso.Size = new System.Drawing.Size(100, 20);
            this.txtPeso.TabIndex = 17;
            this.txtPeso.TextChanged += new System.EventHandler(this.txtPeso_TextChanged);
            // 
            // lblPeso
            // 
            this.lblPeso.AutoSize = true;
            this.lblPeso.Location = new System.Drawing.Point(12, 264);
            this.lblPeso.Name = "lblPeso";
            this.lblPeso.Size = new System.Drawing.Size(60, 13);
            this.lblPeso.TabIndex = 16;
            this.lblPeso.Text = "Peso (kg):";
            // 
            // chkFechaNacimiento
            // 
            this.chkFechaNacimiento.AutoSize = true;
            this.chkFechaNacimiento.Location = new System.Drawing.Point(15, 215);
            this.chkFechaNacimiento.Name = "chkFechaNacimiento";
            this.chkFechaNacimiento.Size = new System.Drawing.Size(144, 17);
            this.chkFechaNacimiento.TabIndex = 15;
            this.chkFechaNacimiento.Text = "Fecha de nacimiento conocida";
            this.chkFechaNacimiento.UseVisualStyleBackColor = true;
            this.chkFechaNacimiento.CheckedChanged += new System.EventHandler(this.chkFechaNacimiento_CheckedChanged);
            // 
            // dtpFechaNacimiento
            // 
            this.dtpFechaNacimiento.Enabled = false;
            this.dtpFechaNacimiento.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFechaNacimiento.Location = new System.Drawing.Point(15, 240);
            this.dtpFechaNacimiento.MaxDate = new System.DateTime(2025, 12, 31, 0, 0, 0, 0);
            this.dtpFechaNacimiento.MinDate = new System.DateTime(1975, 1, 1, 0, 0, 0, 0);
            this.dtpFechaNacimiento.Name = "dtpFechaNacimiento";
            this.dtpFechaNacimiento.Size = new System.Drawing.Size(200, 20);
            this.dtpFechaNacimiento.TabIndex = 14;
            this.dtpFechaNacimiento.Value = new System.DateTime(2024, 8, 21, 0, 0, 0, 0);
            this.dtpFechaNacimiento.ValueChanged += new System.EventHandler(this.dtpFechaNacimiento_ValueChanged);
            // 
            // lblFechaNacimiento
            // 
            this.lblFechaNacimiento.AutoSize = true;
            this.lblFechaNacimiento.Location = new System.Drawing.Point(12, 199);
            this.lblFechaNacimiento.Name = "lblFechaNacimiento";
            this.lblFechaNacimiento.Size = new System.Drawing.Size(111, 13);
            this.lblFechaNacimiento.TabIndex = 13;
            this.lblFechaNacimiento.Text = "Fecha de nacimiento:";
            // 
            // cmbSexo
            // 
            this.cmbSexo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSexo.FormattingEnabled = true;
            this.cmbSexo.Location = new System.Drawing.Point(430, 170);
            this.cmbSexo.Name = "cmbSexo";
            this.cmbSexo.Size = new System.Drawing.Size(200, 21);
            this.cmbSexo.TabIndex = 12;
            // 
            // lblSexo
            // 
            this.lblSexo.AutoSize = true;
            this.lblSexo.Location = new System.Drawing.Point(427, 154);
            this.lblSexo.Name = "lblSexo";
            this.lblSexo.Size = new System.Drawing.Size(34, 13);
            this.lblSexo.TabIndex = 11;
            this.lblSexo.Text = "Sexo:";
            // 
            // txtColor
            // 
            this.txtColor.Location = new System.Drawing.Point(15, 170);
            this.txtColor.MaxLength = 50;
            this.txtColor.Name = "txtColor";
            this.txtColor.Size = new System.Drawing.Size(200, 20);
            this.txtColor.TabIndex = 10;
            // 
            // lblColor
            // 
            this.lblColor.AutoSize = true;
            this.lblColor.Location = new System.Drawing.Point(12, 154);
            this.lblColor.Name = "lblColor";
            this.lblColor.Size = new System.Drawing.Size(34, 13);
            this.lblColor.TabIndex = 9;
            this.lblColor.Text = "Color:";
            // 
            // cmbRaza
            // 
            this.cmbRaza.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRaza.FormattingEnabled = true;
            this.cmbRaza.Location = new System.Drawing.Point(430, 120);
            this.cmbRaza.Name = "cmbRaza";
            this.cmbRaza.Size = new System.Drawing.Size(200, 21);
            this.cmbRaza.TabIndex = 8;
            // 
            // lblRaza
            // 
            this.lblRaza.AutoSize = true;
            this.lblRaza.Location = new System.Drawing.Point(427, 104);
            this.lblRaza.Name = "lblRaza";
            this.lblRaza.Size = new System.Drawing.Size(35, 13);
            this.lblRaza.TabIndex = 7;
            this.lblRaza.Text = "Raza:";
            // 
            // cmbTipo
            // 
            this.cmbTipo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTipo.FormattingEnabled = true;
            this.cmbTipo.Location = new System.Drawing.Point(15, 120);
            this.cmbTipo.Name = "cmbTipo";
            this.cmbTipo.Size = new System.Drawing.Size(200, 21);
            this.cmbTipo.TabIndex = 6;
            this.cmbTipo.SelectedIndexChanged += new System.EventHandler(this.cmbTipo_SelectedIndexChanged);
            // 
            // lblTipo
            // 
            this.lblTipo.AutoSize = true;
            this.lblTipo.Location = new System.Drawing.Point(12, 104);
            this.lblTipo.Name = "lblTipo";
            this.lblTipo.Size = new System.Drawing.Size(31, 13);
            this.lblTipo.TabIndex = 5;
            this.lblTipo.Text = "Tipo:";
            // 
            // txtNombre
            // 
            this.txtNombre.Location = new System.Drawing.Point(430, 70);
            this.txtNombre.MaxLength = 100;
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.Size = new System.Drawing.Size(200, 20);
            this.txtNombre.TabIndex = 4;
            // 
            // lblNombre
            // 
            this.lblNombre.AutoSize = true;
            this.lblNombre.Location = new System.Drawing.Point(427, 54);
            this.lblNombre.Name = "lblNombre";
            this.lblNombre.Size = new System.Drawing.Size(47, 13);
            this.lblNombre.TabIndex = 3;
            this.lblNombre.Text = "Nombre:";
            // 
            // cmbPropietario
            // 
            this.cmbPropietario.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPropietario.FormattingEnabled = true;
            this.cmbPropietario.Location = new System.Drawing.Point(15, 70);
            this.cmbPropietario.Name = "cmbPropietario";
            this.cmbPropietario.Size = new System.Drawing.Size(350, 21);
            this.cmbPropietario.TabIndex = 2;
            // 
            // lblPropietario
            // 
            this.lblPropietario.AutoSize = true;
            this.lblPropietario.Location = new System.Drawing.Point(12, 54);
            this.lblPropietario.Name = "lblPropietario";
            this.lblPropietario.Size = new System.Drawing.Size(62, 13);
            this.lblPropietario.TabIndex = 1;
            this.lblPropietario.Text = "Propietario:";
            // 
            // txtIdAnimal
            // 
            this.txtIdAnimal.Location = new System.Drawing.Point(15, 30);
            this.txtIdAnimal.Name = "txtIdAnimal";
            this.txtIdAnimal.ReadOnly = true;
            this.txtIdAnimal.Size = new System.Drawing.Size(100, 20);
            this.txtIdAnimal.TabIndex = 1;
            // 
            // lblIdAnimal
            // 
            this.lblIdAnimal.AutoSize = true;
            this.lblIdAnimal.Location = new System.Drawing.Point(12, 14);
            this.lblIdAnimal.Name = "lblIdAnimal";
            this.lblIdAnimal.Size = new System.Drawing.Size(21, 13);
            this.lblIdAnimal.TabIndex = 0;
            this.lblIdAnimal.Text = "ID:";
            // 
            // btnCancelar
            // 
            this.btnCancelar.BackColor = System.Drawing.Color.Gray;
            this.btnCancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelar.ForeColor = System.Drawing.Color.White;
            this.btnCancelar.Location = new System.Drawing.Point(276, 500);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(75, 35);
            this.btnCancelar.TabIndex = 4;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = false;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // btnEditar
            // 
            this.btnEditar.BackColor = System.Drawing.Color.Orange;
            this.btnEditar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEditar.ForeColor = System.Drawing.Color.White;
            this.btnEditar.Location = new System.Drawing.Point(195, 500);
            this.btnEditar.Name = "btnEditar";
            this.btnEditar.Size = new System.Drawing.Size(75, 35);
            this.btnEditar.TabIndex = 3;
            this.btnEditar.Text = "Editar";
            this.btnEditar.UseVisualStyleBackColor = false;
            this.btnEditar.Click += new System.EventHandler(this.btnEditar_Click);
            // 
            // btnGuardar
            // 
            this.btnGuardar.BackColor = System.Drawing.Color.SteelBlue;
            this.btnGuardar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGuardar.ForeColor = System.Drawing.Color.White;
            this.btnGuardar.Location = new System.Drawing.Point(114, 500);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(75, 35);
            this.btnGuardar.TabIndex = 2;
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.UseVisualStyleBackColor = false;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // btnNuevo
            // 
            this.btnNuevo.BackColor = System.Drawing.Color.ForestGreen;
            this.btnNuevo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNuevo.ForeColor = System.Drawing.Color.White;
            this.btnNuevo.Location = new System.Drawing.Point(33, 500);
            this.btnNuevo.Name = "btnNuevo";
            this.btnNuevo.Size = new System.Drawing.Size(75, 35);
            this.btnNuevo.TabIndex = 1;
            this.btnNuevo.Text = "Nuevo";
            this.btnNuevo.UseVisualStyleBackColor = false;
            this.btnNuevo.Click += new System.EventHandler(this.btnNuevo_Click);
            // 
            // FrmAnimales
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 600);
            this.Controls.Add(this.tabControl1);
            this.MinimizeBox = false;
            this.Name = "FrmAnimales";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Gestión de Animales";
            this.Load += new System.EventHandler(this.FrmAnimales_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataListado)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.DataGridView dataListado;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Eliminar;
        private System.Windows.Forms.CheckBox chkEliminar;
        private System.Windows.Forms.Button btnEliminar;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.TextBox txtBuscar;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox chkVacunado;
        private System.Windows.Forms.CheckBox chkEsterilizado;
        private System.Windows.Forms.TextBox txtNumPedigree;
        private System.Windows.Forms.Label lblNumPedigree;
        private System.Windows.Forms.TextBox txtAltura;
        private System.Windows.Forms.Label lblAltura;
        private System.Windows.Forms.TextBox txtObservaciones;
        private System.Windows.Forms.Label lblObservaciones;
        private System.Windows.Forms.TextBox txtMicrochip;
        private System.Windows.Forms.Label lblMicrochip;
        private System.Windows.Forms.TextBox txtPeso;
        private System.Windows.Forms.Label lblPeso;
        private System.Windows.Forms.CheckBox chkFechaNacimiento;
        private System.Windows.Forms.DateTimePicker dtpFechaNacimiento;
        private System.Windows.Forms.Label lblFechaNacimiento;
        private System.Windows.Forms.ComboBox cmbSexo;
        private System.Windows.Forms.Label lblSexo;
        private System.Windows.Forms.TextBox txtColor;
        private System.Windows.Forms.Label lblColor;
        private System.Windows.Forms.ComboBox cmbRaza;
        private System.Windows.Forms.Label lblRaza;
        private System.Windows.Forms.ComboBox cmbTipo;
        private System.Windows.Forms.Label lblTipo;
        private System.Windows.Forms.TextBox txtNombre;
        private System.Windows.Forms.Label lblNombre;
        private System.Windows.Forms.ComboBox cmbPropietario;
        private System.Windows.Forms.Label lblPropietario;
        private System.Windows.Forms.TextBox txtIdAnimal;
        private System.Windows.Forms.Label lblIdAnimal;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Button btnEditar;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.Button btnNuevo;
        private System.Windows.Forms.ToolTip ttMensaje;
        private System.Windows.Forms.Label lblEdad;
    }
}