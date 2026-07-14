namespace SistemVeterinario.Navigation
{
    partial class BaseModulos
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
        protected void InitializeComponent()
        {
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            tabControlPrincipal = new TabControl();
            tabInicio = new TabPage();
            panelBusqueda = new Panel();
            txtBuscar = new TextBox();
            btnBuscar = new Button();
            btnNuevo = new Button();
            chkMostrarTodo = new CheckBox();
            dgvDatos = new DataGridView();
            tabConfiguraciones = new TabPage();
            panelFormulario = new Panel();
            panelSuperior = new Panel();
            lblModo = new Label();
            cmbModo = new ComboBox();
            lblId = new Label();
            txtId = new TextBox();
            panelBotones = new Panel();
            btnGuardar = new Button();
            btnCancelar = new Button();
            btnEliminar = new Button();
            tabControlPrincipal.SuspendLayout();
            tabInicio.SuspendLayout();
            panelBusqueda.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvDatos).BeginInit();
            tabConfiguraciones.SuspendLayout();
            panelFormulario.SuspendLayout();
            panelSuperior.SuspendLayout();
            panelBotones.SuspendLayout();
            SuspendLayout();
            // 
            // tabControlPrincipal
            // 
            tabControlPrincipal.Controls.Add(tabInicio);
            tabControlPrincipal.Controls.Add(tabConfiguraciones);
            tabControlPrincipal.Dock = DockStyle.Fill;
            tabControlPrincipal.Location = new Point(0, 0);
            tabControlPrincipal.Margin = new Padding(3, 4, 3, 4);
            tabControlPrincipal.Name = "tabControlPrincipal";
            tabControlPrincipal.SelectedIndex = 0;
            tabControlPrincipal.Size = new Size(1143, 800);
            tabControlPrincipal.TabIndex = 0;
            // 
            // tabInicio
            // 
            tabInicio.BackColor = Color.White;
            tabInicio.Controls.Add(panelBusqueda);
            tabInicio.Controls.Add(dgvDatos);
            tabInicio.Location = new Point(4, 29);
            tabInicio.Margin = new Padding(3, 4, 3, 4);
            tabInicio.Name = "tabInicio";
            tabInicio.Padding = new Padding(3, 4, 3, 4);
            tabInicio.Size = new Size(1135, 767);
            tabInicio.TabIndex = 0;
            tabInicio.Text = "Inicio";
            // 
            // panelBusqueda
            // 
            panelBusqueda.BackColor = Color.FromArgb(240, 240, 240);
            panelBusqueda.BorderStyle = BorderStyle.FixedSingle;
            panelBusqueda.Controls.Add(txtBuscar);
            panelBusqueda.Controls.Add(btnBuscar);
            panelBusqueda.Controls.Add(btnNuevo);
            panelBusqueda.Controls.Add(chkMostrarTodo);
            panelBusqueda.Location = new Point(11, 13);
            panelBusqueda.Margin = new Padding(3, 4, 3, 4);
            panelBusqueda.Name = "panelBusqueda";
            panelBusqueda.Size = new Size(1097, 79);
            panelBusqueda.TabIndex = 0;
            // 
            // txtBuscar
            // 
            txtBuscar.Location = new Point(11, 24);
            txtBuscar.Margin = new Padding(3, 4, 3, 4);
            txtBuscar.Name = "txtBuscar";
            txtBuscar.PlaceholderText = "Buscar...";
            txtBuscar.Size = new Size(342, 27);
            txtBuscar.TabIndex = 0;
            txtBuscar.KeyPress += TxtBuscar_KeyPress;
            // 
            // btnBuscar
            // 
            btnBuscar.BackColor = Color.FromArgb(0, 120, 215);
            btnBuscar.FlatStyle = FlatStyle.Flat;
            btnBuscar.ForeColor = Color.White;
            btnBuscar.Location = new Point(366, 23);
            btnBuscar.Margin = new Padding(3, 4, 3, 4);
            btnBuscar.Name = "btnBuscar";
            btnBuscar.Size = new Size(91, 33);
            btnBuscar.TabIndex = 1;
            btnBuscar.Text = "Buscar";
            btnBuscar.UseVisualStyleBackColor = false;
            btnBuscar.Click += BtnBuscar_Click;
            // 
            // btnNuevo
            // 
            btnNuevo.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnNuevo.BackColor = Color.FromArgb(16, 137, 62);
            btnNuevo.FlatStyle = FlatStyle.Flat;
            btnNuevo.ForeColor = Color.White;
            btnNuevo.Location = new Point(983, 23);
            btnNuevo.Margin = new Padding(3, 4, 3, 4);
            btnNuevo.Name = "btnNuevo";
            btnNuevo.Size = new Size(91, 33);
            btnNuevo.TabIndex = 2;
            btnNuevo.Text = "Nuevo";
            btnNuevo.UseVisualStyleBackColor = false;
            btnNuevo.Click += BtnNuevo_Click;
            // 
            // chkMostrarTodo
            // 
            chkMostrarTodo.AutoSize = true;
            chkMostrarTodo.Checked = true;
            chkMostrarTodo.CheckState = CheckState.Checked;
            chkMostrarTodo.Location = new Point(480, 27);
            chkMostrarTodo.Margin = new Padding(3, 4, 3, 4);
            chkMostrarTodo.Name = "chkMostrarTodo";
            chkMostrarTodo.Size = new Size(212, 24);
            chkMostrarTodo.TabIndex = 3;
            chkMostrarTodo.Text = "Mostrar todas las columnas";
            chkMostrarTodo.UseVisualStyleBackColor = true;
            chkMostrarTodo.CheckedChanged += ChkMostrarTodo_CheckedChanged;
            // 
            // dgvDatos
            // 
            dgvDatos.AllowUserToAddRows = false;
            dgvDatos.AllowUserToDeleteRows = false;
            dgvDatos.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvDatos.BackgroundColor = Color.Gainsboro;
            dgvDatos.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = Color.FromArgb(240, 240, 240);
            dataGridViewCellStyle1.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = Color.Black;
            dataGridViewCellStyle1.SelectionBackColor = Color.FromArgb(240, 240, 240);
            dataGridViewCellStyle1.SelectionForeColor = Color.Black;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            dgvDatos.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dgvDatos.ColumnHeadersHeight = 29;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = Color.White;
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle2.ForeColor = Color.Black;
            dataGridViewCellStyle2.SelectionBackColor = Color.FromArgb(0, 120, 215);
            dataGridViewCellStyle2.SelectionForeColor = Color.White;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
            dgvDatos.DefaultCellStyle = dataGridViewCellStyle2;
            dgvDatos.EnableHeadersVisualStyles = false;
            dgvDatos.GridColor = Color.FromArgb(255, 192, 192);
            dgvDatos.Location = new Point(11, 107);
            dgvDatos.Margin = new Padding(3, 4, 3, 4);
            dgvDatos.MultiSelect = false;
            dgvDatos.Name = "dgvDatos";
            dgvDatos.ReadOnly = true;
            dgvDatos.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = Color.FromArgb(240, 240, 240);
            dataGridViewCellStyle3.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle3.ForeColor = Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = Color.FromArgb(0, 120, 215);
            dataGridViewCellStyle3.SelectionForeColor = Color.White;
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.True;
            dgvDatos.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            dgvDatos.RowHeadersWidth = 51;
            dgvDatos.RowTemplate.Height = 30;
            dgvDatos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvDatos.Size = new Size(1097, 613);
            dgvDatos.TabIndex = 1;
            dgvDatos.DataSourceChanged += DgvDatos_DataSourceChanged;
            dgvDatos.CellClick += DgvDatos_CellClick;
            // 
            // tabConfiguraciones
            // 
            tabConfiguraciones.BackColor = Color.White;
            tabConfiguraciones.Controls.Add(panelFormulario);
            tabConfiguraciones.Location = new Point(4, 29);
            tabConfiguraciones.Margin = new Padding(3, 4, 3, 4);
            tabConfiguraciones.Name = "tabConfiguraciones";
            tabConfiguraciones.Padding = new Padding(3, 4, 3, 4);
            tabConfiguraciones.Size = new Size(1135, 767);
            tabConfiguraciones.TabIndex = 1;
            tabConfiguraciones.Text = "Configuraciones";
            // 
            // panelFormulario
            // 
            panelFormulario.Controls.Add(panelSuperior);
            panelFormulario.Controls.Add(panelBotones);
            panelFormulario.Dock = DockStyle.Fill;
            panelFormulario.Location = new Point(3, 4);
            panelFormulario.Margin = new Padding(3, 4, 3, 4);
            panelFormulario.Name = "panelFormulario";
            panelFormulario.Size = new Size(1129, 759);
            panelFormulario.TabIndex = 0;
            // 
            // panelSuperior
            // 
            panelSuperior.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            panelSuperior.BackColor = Color.FromArgb(240, 240, 240);
            panelSuperior.BorderStyle = BorderStyle.FixedSingle;
            panelSuperior.Controls.Add(lblModo);
            panelSuperior.Controls.Add(cmbModo);
            panelSuperior.Controls.Add(lblId);
            panelSuperior.Controls.Add(txtId);
            panelSuperior.Location = new Point(11, 13);
            panelSuperior.Margin = new Padding(3, 4, 3, 4);
            panelSuperior.Name = "panelSuperior";
            panelSuperior.Size = new Size(1099, 66);
            panelSuperior.TabIndex = 0;
            // 
            // lblModo
            // 
            lblModo.AutoSize = true;
            lblModo.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblModo.ForeColor = Color.Black;
            lblModo.Location = new Point(11, 20);
            lblModo.Name = "lblModo";
            lblModo.Size = new Size(54, 20);
            lblModo.TabIndex = 0;
            lblModo.Text = "Modo:";
            // 
            // cmbModo
            // 
            cmbModo.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbModo.FormattingEnabled = true;
            cmbModo.Items.AddRange(new object[] { "Nuevo", "Edición" });
            cmbModo.Location = new Point(80, 16);
            cmbModo.Margin = new Padding(3, 4, 3, 4);
            cmbModo.Name = "cmbModo";
            cmbModo.Size = new Size(137, 28);
            cmbModo.TabIndex = 1;
            cmbModo.SelectedIndexChanged += CmbModo_SelectedIndexChanged;
            // 
            // lblId
            // 
            lblId.AutoSize = true;
            lblId.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblId.ForeColor = Color.Black;
            lblId.Location = new Point(240, 20);
            lblId.Name = "lblId";
            lblId.Size = new Size(29, 20);
            lblId.TabIndex = 2;
            lblId.Text = "ID:";
            // 
            // txtId
            // 
            txtId.BackColor = SystemColors.Control;
            txtId.Enabled = false;
            txtId.Location = new Point(286, 16);
            txtId.Margin = new Padding(3, 4, 3, 4);
            txtId.Name = "txtId";
            txtId.Size = new Size(91, 27);
            txtId.TabIndex = 3;
            // 
            // panelBotones
            // 
            panelBotones.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panelBotones.BackColor = Color.FromArgb(240, 240, 240);
            panelBotones.BorderStyle = BorderStyle.FixedSingle;
            panelBotones.Controls.Add(btnGuardar);
            panelBotones.Controls.Add(btnCancelar);
            panelBotones.Controls.Add(btnEliminar);
            panelBotones.Location = new Point(11, 671);
            panelBotones.Margin = new Padding(3, 4, 3, 4);
            panelBotones.Name = "panelBotones";
            panelBotones.Size = new Size(1099, 66);
            panelBotones.TabIndex = 1;
            // 
            // btnGuardar
            // 
            btnGuardar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnGuardar.BackColor = Color.FromArgb(40, 167, 69);
            btnGuardar.FlatStyle = FlatStyle.Flat;
            btnGuardar.ForeColor = Color.White;
            btnGuardar.Location = new Point(836, 13);
            btnGuardar.Margin = new Padding(3, 4, 3, 4);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new Size(114, 40);
            btnGuardar.TabIndex = 0;
            btnGuardar.Text = "Guardar";
            btnGuardar.UseVisualStyleBackColor = false;
            btnGuardar.Click += BtnGuardar_Click;
            // 
            // btnCancelar
            // 
            btnCancelar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnCancelar.BackColor = Color.FromArgb(108, 117, 125);
            btnCancelar.FlatStyle = FlatStyle.Flat;
            btnCancelar.ForeColor = Color.White;
            btnCancelar.Location = new Point(962, 13);
            btnCancelar.Margin = new Padding(3, 4, 3, 4);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(114, 40);
            btnCancelar.TabIndex = 1;
            btnCancelar.Text = "Cancelar";
            btnCancelar.UseVisualStyleBackColor = false;
            btnCancelar.Click += BtnCancelar_Click;
            // 
            // btnEliminar
            // 
            btnEliminar.BackColor = Color.FromArgb(220, 53, 69);
            btnEliminar.FlatStyle = FlatStyle.Flat;
            btnEliminar.ForeColor = Color.White;
            btnEliminar.Location = new Point(23, 13);
            btnEliminar.Margin = new Padding(3, 4, 3, 4);
            btnEliminar.Name = "btnEliminar";
            btnEliminar.Size = new Size(114, 40);
            btnEliminar.TabIndex = 2;
            btnEliminar.Text = "Eliminar";
            btnEliminar.UseVisualStyleBackColor = false;
            btnEliminar.Visible = false;
            btnEliminar.Click += BtnEliminar_Click;
            // 
            // BaseModulos
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(tabControlPrincipal);
            Margin = new Padding(3, 4, 3, 4);
            Name = "BaseModulos";
            Size = new Size(1143, 800);
            Load += SearchBase_Load;
            tabControlPrincipal.ResumeLayout(false);
            tabInicio.ResumeLayout(false);
            panelBusqueda.ResumeLayout(false);
            panelBusqueda.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvDatos).EndInit();
            tabConfiguraciones.ResumeLayout(false);
            panelFormulario.ResumeLayout(false);
            panelSuperior.ResumeLayout(false);
            panelSuperior.PerformLayout();
            panelBotones.ResumeLayout(false);
            ResumeLayout(false);

        }

        #endregion

        protected System.Windows.Forms.TabControl tabControlPrincipal;
        protected System.Windows.Forms.TabPage tabInicio;
        protected System.Windows.Forms.TabPage tabConfiguraciones;
        protected System.Windows.Forms.Panel panelBusqueda;
        protected System.Windows.Forms.TextBox txtBuscar;
        protected System.Windows.Forms.Button btnBuscar;
        protected System.Windows.Forms.Button btnNuevo;
        protected System.Windows.Forms.CheckBox chkMostrarTodo;
        protected System.Windows.Forms.DataGridView dgvDatos;
        protected System.Windows.Forms.Panel panelFormulario;
        protected System.Windows.Forms.Panel panelSuperior;
        protected System.Windows.Forms.Label lblModo;
        protected System.Windows.Forms.ComboBox cmbModo;
        protected System.Windows.Forms.Label lblId;
        protected System.Windows.Forms.TextBox txtId;
        protected System.Windows.Forms.Panel panelBotones;
        protected System.Windows.Forms.Button btnGuardar;
        protected System.Windows.Forms.Button btnCancelar;
        protected System.Windows.Forms.Button btnEliminar;
    }
}
