using System.Windows.Forms;
using System.Drawing;
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
            tabControlPrincipal.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            tabControlPrincipal.ItemSize = new Size(200, 35);
            tabControlPrincipal.Location = new Point(0, 0);
            tabControlPrincipal.Name = "tabControlPrincipal";
            tabControlPrincipal.SelectedIndex = 0;
            tabControlPrincipal.Size = new Size(1000, 600);
            tabControlPrincipal.SizeMode = TabSizeMode.Fixed;
            tabControlPrincipal.TabIndex = 0;
            // 
            // tabInicio
            // 
            tabInicio.BackColor = Color.FromArgb(248, 249, 250);
            tabInicio.Controls.Add(panelBusqueda);
            tabInicio.Controls.Add(dgvDatos);
            tabInicio.Location = new Point(4, 25);
            tabInicio.Name = "tabInicio";
            tabInicio.Padding = new Padding(10);
            tabInicio.Size = new Size(992, 572);
            tabInicio.TabIndex = 0;
            tabInicio.Text = "Inicio";
            // 
            // panelBusqueda
            // 
            panelBusqueda.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            panelBusqueda.BackColor = Color.White;
            panelBusqueda.BorderStyle = BorderStyle.FixedSingle;
            panelBusqueda.Controls.Add(txtBuscar);
            panelBusqueda.Controls.Add(btnBuscar);
            panelBusqueda.Controls.Add(btnNuevo);
            panelBusqueda.Controls.Add(chkMostrarTodo);
            panelBusqueda.Location = new Point(10, 15);
            panelBusqueda.Name = "panelBusqueda";
            panelBusqueda.Padding = new Padding(15);
            panelBusqueda.Size = new Size(972, 120);
            panelBusqueda.TabIndex = 0;
            // 
            // txtBuscar
            // 
            txtBuscar.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtBuscar.BorderStyle = BorderStyle.FixedSingle;
            txtBuscar.Font = new Font("Segoe UI", 11F);
            txtBuscar.ForeColor = Color.FromArgb(44, 62, 80);
            txtBuscar.Location = new Point(20, 25);
            txtBuscar.Name = "txtBuscar";
            txtBuscar.Size = new Size(450, 32);
            txtBuscar.TabIndex = 0;
            txtBuscar.KeyPress += TxtBuscar_KeyPress;
            // 
            // btnBuscar
            // 
            btnBuscar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnBuscar.BackColor = Color.FromArgb(52, 152, 219);
            btnBuscar.FlatStyle = FlatStyle.Flat;
            btnBuscar.FlatAppearance.BorderSize = 0;
            btnBuscar.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnBuscar.ForeColor = Color.White;
            btnBuscar.Location = new Point(485, 23);
            btnBuscar.Name = "btnBuscar";
            btnBuscar.Size = new Size(120, 36);
            btnBuscar.TabIndex = 1;
            btnBuscar.Text = "🔍 Buscar";
            btnBuscar.UseVisualStyleBackColor = false;
            btnBuscar.Click += BtnBuscar_Click;
            // 
            // btnNuevo
            // 
            btnNuevo.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnNuevo.BackColor = Color.FromArgb(46, 204, 113);
            btnNuevo.FlatStyle = FlatStyle.Flat;
            btnNuevo.FlatAppearance.BorderSize = 0;
            btnNuevo.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnNuevo.ForeColor = Color.White;
            btnNuevo.Location = new Point(825, 23);
            btnNuevo.Name = "btnNuevo";
            btnNuevo.Size = new Size(120, 36);
            btnNuevo.TabIndex = 2;
            btnNuevo.Text = "➕ Nueva Mascota";
            btnNuevo.UseVisualStyleBackColor = false;
            btnNuevo.Click += BtnNuevo_Click;
            // 
            // chkMostrarTodo
            // 
            chkMostrarTodo.AutoSize = true;
            chkMostrarTodo.BackColor = Color.White;
            chkMostrarTodo.Checked = true;
            chkMostrarTodo.CheckState = CheckState.Checked;
            chkMostrarTodo.Font = new Font("Segoe UI", 10F);
            chkMostrarTodo.ForeColor = Color.FromArgb(44, 62, 80);
            chkMostrarTodo.Location = new Point(20, 75);
            chkMostrarTodo.Name = "chkMostrarTodo";
            chkMostrarTodo.Size = new Size(223, 27);
            chkMostrarTodo.TabIndex = 3;
            chkMostrarTodo.Text = "📊 Mostrar todas las columnas";
            chkMostrarTodo.UseVisualStyleBackColor = false;
            chkMostrarTodo.CheckedChanged += ChkMostrarTodo_CheckedChanged;
            // 
            // dgvDatos
            // 
            dgvDatos.AllowUserToAddRows = false;
            dgvDatos.AllowUserToDeleteRows = false;
            dgvDatos.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvDatos.BackgroundColor = Color.White;
            dgvDatos.BorderStyle = BorderStyle.FixedSingle;
            dgvDatos.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvDatos.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgvDatos.ColumnHeadersHeight = 45;
            dgvDatos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvDatos.EnableHeadersVisualStyles = false;
            dgvDatos.GridColor = Color.FromArgb(236, 240, 241);
            dgvDatos.Location = new Point(10, 150);
            dgvDatos.MultiSelect = false;
            dgvDatos.Name = "dgvDatos";
            dgvDatos.ReadOnly = true;
            dgvDatos.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgvDatos.RowHeadersVisible = false;
            dgvDatos.RowHeadersWidth = 25;
            dgvDatos.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dgvDatos.RowTemplate.Height = 35;
            dgvDatos.ScrollBars = ScrollBars.Both;
            dgvDatos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvDatos.Size = new Size(972, 400);
            dgvDatos.TabIndex = 1;
            dgvDatos.DataSourceChanged += DgvDatos_DataSourceChanged;
            dgvDatos.CellClick += DgvDatos_CellClick;

            // Estilos para las celdas
            dgvDatos.DefaultCellStyle.BackColor = Color.White;
            dgvDatos.DefaultCellStyle.Font = new Font("Segoe UI", 10F);
            dgvDatos.DefaultCellStyle.ForeColor = Color.FromArgb(44, 62, 80);
            dgvDatos.DefaultCellStyle.SelectionBackColor = Color.FromArgb(52, 152, 219);
            dgvDatos.DefaultCellStyle.SelectionForeColor = Color.White;
            dgvDatos.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvDatos.DefaultCellStyle.Padding = new Padding(8, 5, 8, 5);
            dgvDatos.DefaultCellStyle.WrapMode = DataGridViewTriState.False;

            // Estilos para los encabezados de columna
            dgvDatos.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(52, 73, 94);
            dgvDatos.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            dgvDatos.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvDatos.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvDatos.ColumnHeadersDefaultCellStyle.Padding = new Padding(10, 8, 10, 8);

            // Estilos alternados para filas
            dgvDatos.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 249, 250);
            dgvDatos.AlternatingRowsDefaultCellStyle.ForeColor = Color.FromArgb(44, 62, 80);
            dgvDatos.AlternatingRowsDefaultCellStyle.SelectionBackColor = Color.FromArgb(52, 152, 219);
            dgvDatos.AlternatingRowsDefaultCellStyle.SelectionForeColor = Color.White;
            // 
            // tabConfiguraciones
            // 
            tabConfiguraciones.BackColor = Color.FromArgb(248, 249, 250);
            tabConfiguraciones.Controls.Add(panelFormulario);
            tabConfiguraciones.Location = new Point(4, 39);
            tabConfiguraciones.Name = "tabConfiguraciones";
            tabConfiguraciones.Padding = new Padding(10);
            tabConfiguraciones.Size = new Size(992, 557);
            tabConfiguraciones.TabIndex = 1;
            tabConfiguraciones.Text = "Configuraciones";
            // 
            // panelFormulario
            // 
            panelFormulario.BackColor = Color.FromArgb(248, 249, 250);
            panelFormulario.Controls.Add(panelSuperior);
            panelFormulario.Controls.Add(panelBotones);
            panelFormulario.Dock = DockStyle.Fill;
            panelFormulario.Location = new Point(10, 10);
            panelFormulario.Name = "panelFormulario";
            panelFormulario.Padding = new Padding(10);
            panelFormulario.Size = new Size(972, 537);
            panelFormulario.TabIndex = 0;
            // 
            // panelSuperior
            // 
            panelSuperior.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            panelSuperior.BackColor = Color.White;
            panelSuperior.BorderStyle = BorderStyle.FixedSingle;
            panelSuperior.Controls.Add(lblModo);
            panelSuperior.Controls.Add(cmbModo);
            panelSuperior.Controls.Add(lblId);
            panelSuperior.Controls.Add(txtId);
            panelSuperior.Location = new Point(15, 15);
            panelSuperior.Name = "panelSuperior";
            panelSuperior.Padding = new Padding(10);
            panelSuperior.Size = new Size(942, 60);
            panelSuperior.TabIndex = 0;
            // 
            // lblModo
            // 
            lblModo.AutoSize = true;
            lblModo.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblModo.ForeColor = Color.FromArgb(52, 73, 94);
            lblModo.Location = new Point(15, 20);
            lblModo.Name = "lblModo";
            lblModo.Size = new Size(63, 25);
            lblModo.TabIndex = 0;
            lblModo.Text = "⚙️ Modo:";
            // 
            // cmbModo
            // 
            cmbModo.BackColor = Color.White;
            cmbModo.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbModo.FlatStyle = FlatStyle.Flat;
            cmbModo.Font = new Font("Segoe UI", 10F);
            cmbModo.ForeColor = Color.FromArgb(44, 62, 80);
            cmbModo.FormattingEnabled = true;
            cmbModo.Items.AddRange(new object[] { "Nuevo", "Edición" });
            cmbModo.Location = new Point(100, 18);
            cmbModo.Name = "cmbModo";
            cmbModo.Size = new Size(140, 31);
            cmbModo.TabIndex = 1;
            cmbModo.SelectedIndexChanged += CmbModo_SelectedIndexChanged;
            // 
            // lblId
            // 
            lblId.AutoSize = true;
            lblId.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblId.ForeColor = Color.FromArgb(52, 73, 94);
            lblId.Location = new Point(270, 20);
            lblId.Name = "lblId";
            lblId.Size = new Size(35, 25);
            lblId.TabIndex = 2;
            lblId.Text = "ID:";
            // 
            // txtId
            // 
            txtId.BackColor = Color.FromArgb(236, 240, 241);
            txtId.BorderStyle = BorderStyle.FixedSingle;
            txtId.Enabled = false;
            txtId.Font = new Font("Segoe UI", 10F);
            txtId.ForeColor = Color.FromArgb(44, 62, 80);
            txtId.Location = new Point(320, 18);
            txtId.Name = "txtId";
            txtId.Size = new Size(100, 30);
            txtId.TabIndex = 3;
            // 
            // panelBotones
            // 
            panelBotones.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panelBotones.BackColor = Color.White;
            panelBotones.BorderStyle = BorderStyle.FixedSingle;
            panelBotones.Controls.Add(btnGuardar);
            panelBotones.Controls.Add(btnCancelar);
            panelBotones.Controls.Add(btnEliminar);
            panelBotones.Location = new Point(15, 462);
            panelBotones.Name = "panelBotones";
            panelBotones.Padding = new Padding(10);
            panelBotones.Size = new Size(942, 60);
            panelBotones.TabIndex = 5;
            // 
            // btnGuardar
            // 
            btnGuardar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnGuardar.BackColor = Color.FromArgb(46, 204, 113);
            btnGuardar.FlatStyle = FlatStyle.Flat;
            btnGuardar.FlatAppearance.BorderSize = 0;
            btnGuardar.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnGuardar.ForeColor = Color.White;
            btnGuardar.Location = new Point(802, 15);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new Size(120, 35);
            btnGuardar.TabIndex = 0;
            btnGuardar.Text = "✔️ Guardar";
            btnGuardar.UseVisualStyleBackColor = false;
            btnGuardar.Click += BtnGuardar_Click;
            // 
            // btnCancelar
            // 
            btnCancelar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnCancelar.BackColor = Color.FromArgb(149, 165, 166);
            btnCancelar.FlatStyle = FlatStyle.Flat;
            btnCancelar.FlatAppearance.BorderSize = 0;
            btnCancelar.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnCancelar.ForeColor = Color.White;
            btnCancelar.Location = new Point(667, 15);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(120, 35);
            btnCancelar.TabIndex = 1;
            btnCancelar.Text = "❌ Cancelar";
            btnCancelar.UseVisualStyleBackColor = false;
            btnCancelar.Click += BtnCancelar_Click;
            // 
            // btnEliminar
            // 
            btnEliminar.BackColor = Color.FromArgb(231, 76, 60);
            btnEliminar.FlatStyle = FlatStyle.Flat;
            btnEliminar.FlatAppearance.BorderSize = 0;
            btnEliminar.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnEliminar.ForeColor = Color.White;
            btnEliminar.Location = new Point(15, 15);
            btnEliminar.Name = "btnEliminar";
            btnEliminar.Size = new Size(120, 35);
            btnEliminar.TabIndex = 2;
            btnEliminar.Text = "🗑️ Eliminar";
            btnEliminar.UseVisualStyleBackColor = false;
            btnEliminar.Visible = false;
            btnEliminar.Click += BtnEliminar_Click;
            // 
            // BaseModulos
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(tabControlPrincipal);
            Name = "BaseModulos";
            Size = new Size(1000, 600);
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
        public System.Windows.Forms.DataGridView dgvDatos;
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
