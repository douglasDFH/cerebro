using System.Windows.Forms;
using System.Drawing;

namespace SistemVeterinario.Forms
{
    partial class HistorialModule
    {
        /// <summary> 
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        // Controles específicos de historial médico
        private Label lblContador;
        private ComboBox cmbAnimalFiltro;
        private Label lblAnimalFiltro;
        private GroupBox grpDatosHistorial;
        private TableLayoutPanel tableLayoutHistorial;
        private Label lblAnimal;
        private ComboBox cmbAnimal;
        private Button btnBuscarAnimal;
        private Label lblNotasGenerales;
        private TextBox txtNotasGenerales;
        private Label lblAlergias;
        private TextBox txtAlergias;
        private Label lblCondicionesMedicas;
        private TextBox txtCondicionesMedicas;
        private Label lblFechaCreacion;
        private DateTimePicker dtpFechaCreacion;
        private Label lblFechaActualizacion;
        private DateTimePicker dtpFechaActualizacion;
        private Button btnVerDetalles;

        // Controles para detalles de historial
        private GroupBox grpDetallesHistorial;
        private DataGridView dgvDetalles;
        private Panel panelBotonesDetalle;
        private Button btnNuevoDetalle;
        private Button btnEliminarDetalle;
        private GroupBox grpDatosDetalle;
        private TableLayoutPanel tableLayoutDetalle;
        private Label lblTipoRegistro;
        private ComboBox cmbTipoRegistro;
        private Label lblDescripcionDetalle;
        private TextBox txtDescripcionDetalle;
        private Label lblFechaRegistro;
        private DateTimePicker dtpFechaRegistro;
        private Label lblVeterinario;
        private ComboBox cmbVeterinario;
        private Label lblTratamiento;
        private TextBox txtTratamiento;
        private Label lblMedicamentos;
        private TextBox txtMedicamentos;
        private Label lblObservaciones;
        private TextBox txtObservaciones;
        private Panel panelBotonesDetalleForm;
        private Button btnGuardarDetalle;
        private Button btnCancelarDetalle;

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
            this.lblContador = new System.Windows.Forms.Label();
            this.cmbAnimalFiltro = new System.Windows.Forms.ComboBox();
            this.lblAnimalFiltro = new System.Windows.Forms.Label();
            this.grpDatosHistorial = new System.Windows.Forms.GroupBox();
            this.tableLayoutHistorial = new System.Windows.Forms.TableLayoutPanel();
            this.lblAnimal = new System.Windows.Forms.Label();
            this.cmbAnimal = new System.Windows.Forms.ComboBox();
            this.btnBuscarAnimal = new System.Windows.Forms.Button();
            this.lblFechaCreacion = new System.Windows.Forms.Label();
            this.dtpFechaCreacion = new System.Windows.Forms.DateTimePicker();
            this.lblFechaActualizacion = new System.Windows.Forms.Label();
            this.dtpFechaActualizacion = new System.Windows.Forms.DateTimePicker();
            this.lblNotasGenerales = new System.Windows.Forms.Label();
            this.txtNotasGenerales = new System.Windows.Forms.TextBox();
            this.lblAlergias = new System.Windows.Forms.Label();
            this.txtAlergias = new System.Windows.Forms.TextBox();
            this.lblCondicionesMedicas = new System.Windows.Forms.Label();
            this.txtCondicionesMedicas = new System.Windows.Forms.TextBox();
            this.btnVerDetalles = new System.Windows.Forms.Button();
            this.grpDetallesHistorial = new System.Windows.Forms.GroupBox();
            this.dgvDetalles = new System.Windows.Forms.DataGridView();
            this.panelBotonesDetalle = new System.Windows.Forms.Panel();
            this.btnNuevoDetalle = new System.Windows.Forms.Button();
            this.btnEliminarDetalle = new System.Windows.Forms.Button();
            this.grpDatosDetalle = new System.Windows.Forms.GroupBox();
            this.tableLayoutDetalle = new System.Windows.Forms.TableLayoutPanel();
            this.lblTipoRegistro = new System.Windows.Forms.Label();
            this.cmbTipoRegistro = new System.Windows.Forms.ComboBox();
            this.lblFechaRegistro = new System.Windows.Forms.Label();
            this.dtpFechaRegistro = new System.Windows.Forms.DateTimePicker();
            this.lblVeterinario = new System.Windows.Forms.Label();
            this.cmbVeterinario = new System.Windows.Forms.ComboBox();
            this.lblDescripcionDetalle = new System.Windows.Forms.Label();
            this.txtDescripcionDetalle = new System.Windows.Forms.TextBox();
            this.lblTratamiento = new System.Windows.Forms.Label();
            this.txtTratamiento = new System.Windows.Forms.TextBox();
            this.lblMedicamentos = new System.Windows.Forms.Label();
            this.txtMedicamentos = new System.Windows.Forms.TextBox();
            this.lblObservaciones = new System.Windows.Forms.Label();
            this.txtObservaciones = new System.Windows.Forms.TextBox();
            this.panelBotonesDetalleForm = new System.Windows.Forms.Panel();
            this.btnGuardarDetalle = new System.Windows.Forms.Button();
            this.btnCancelarDetalle = new System.Windows.Forms.Button();
            this.tabControlPrincipal.SuspendLayout();
            this.tabInicio.SuspendLayout();
            this.tabConfiguraciones.SuspendLayout();
            this.panelBusqueda.SuspendLayout();
            this.panelFormulario.SuspendLayout();
            this.panelSuperior.SuspendLayout();
            this.panelBotones.SuspendLayout();
            this.grpDatosHistorial.SuspendLayout();
            this.tableLayoutHistorial.SuspendLayout();
            this.grpDetallesHistorial.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetalles)).BeginInit();
            this.panelBotonesDetalle.SuspendLayout();
            this.grpDatosDetalle.SuspendLayout();
            this.tableLayoutDetalle.SuspendLayout();
            this.panelBotonesDetalleForm.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabInicio
            // 
            this.tabInicio.Location = new System.Drawing.Point(4, 39);
            this.tabInicio.Padding = new System.Windows.Forms.Padding(3);
            this.tabInicio.Size = new System.Drawing.Size(1135, 597);
            this.tabInicio.Text = "Gestión de Historiales";
            // 
            // tabConfiguraciones
            // 
            this.tabConfiguraciones.Controls.Add(this.grpDetallesHistorial);
            this.tabConfiguraciones.Controls.Add(this.grpDatosDetalle);
            this.tabConfiguraciones.Location = new System.Drawing.Point(4, 39);
            this.tabConfiguraciones.Padding = new System.Windows.Forms.Padding(3);
            this.tabConfiguraciones.Size = new System.Drawing.Size(1135, 597);
            this.tabConfiguraciones.Text = "Detalles del Historial";
            this.tabConfiguraciones.Controls.SetChildIndex(this.grpDatosDetalle, 0);
            this.tabConfiguraciones.Controls.SetChildIndex(this.grpDetallesHistorial, 0);
            this.tabConfiguraciones.Controls.SetChildIndex(this.panelFormulario, 0);
            // 
            // panelBusqueda
            // 
            this.panelBusqueda.Controls.Add(this.lblContador);
            this.panelBusqueda.Controls.Add(this.lblAnimalFiltro);
            this.panelBusqueda.Controls.Add(this.cmbAnimalFiltro);
            this.panelBusqueda.Controls.Add(this.btnVerDetalles);
            this.panelBusqueda.Location = new System.Drawing.Point(10, 15);
            this.panelBusqueda.Margin = new System.Windows.Forms.Padding(0);
            this.panelBusqueda.Padding = new System.Windows.Forms.Padding(15);
            this.panelBusqueda.Size = new System.Drawing.Size(1121, 120);
            this.panelBusqueda.Controls.SetChildIndex(this.btnVerDetalles, 0);
            this.panelBusqueda.Controls.SetChildIndex(this.cmbAnimalFiltro, 0);
            this.panelBusqueda.Controls.SetChildIndex(this.lblAnimalFiltro, 0);
            this.panelBusqueda.Controls.SetChildIndex(this.lblContador, 0);
            this.panelBusqueda.Controls.SetChildIndex(this.chkMostrarTodo, 0);
            this.panelBusqueda.Controls.SetChildIndex(this.btnNuevo, 0);
            this.panelBusqueda.Controls.SetChildIndex(this.btnBuscar, 0);
            this.panelBusqueda.Controls.SetChildIndex(this.txtBuscar, 0);
            // 
            // txtBuscar
            // 
            this.txtBuscar.Location = new System.Drawing.Point(38, 9);
            this.txtBuscar.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.txtBuscar.Size = new System.Drawing.Size(308, 32);
            // 
            // btnBuscar
            // 
            this.btnBuscar.FlatAppearance.BorderSize = 0;
            this.btnBuscar.Location = new System.Drawing.Point(372, 8);
            this.btnBuscar.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.btnBuscar.Size = new System.Drawing.Size(120, 41);
            // 
            // btnNuevo
            // 
            this.btnNuevo.FlatAppearance.BorderSize = 0;
            this.btnNuevo.Location = new System.Drawing.Point(950, 8);
            this.btnNuevo.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.btnNuevo.Size = new System.Drawing.Size(140, 40);
            this.btnNuevo.Text = "➕ Nuevo Historial";
            // 
            // chkMostrarTodo
            // 
            this.chkMostrarTodo.Location = new System.Drawing.Point(61, 49);
            this.chkMostrarTodo.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            // 
            // panelFormulario
            // 
            this.panelFormulario.Controls.Add(this.grpDatosHistorial);
            this.panelFormulario.Location = new System.Drawing.Point(3, 3);
            this.panelFormulario.Padding = new System.Windows.Forms.Padding(11, 9, 11, 9);
            this.panelFormulario.Size = new System.Drawing.Size(1129, 591);
            this.panelFormulario.Controls.SetChildIndex(this.grpDatosHistorial, 0);
            this.panelFormulario.Controls.SetChildIndex(this.panelBotones, 0);
            this.panelFormulario.Controls.SetChildIndex(this.panelSuperior, 0);
            // 
            // panelSuperior
            // 
            this.panelSuperior.Location = new System.Drawing.Point(13, 11);
            this.panelSuperior.Padding = new System.Windows.Forms.Padding(11, 9, 11, 9);
            this.panelSuperior.Size = new System.Drawing.Size(2745, 64);
            // 
            // lblModo
            // 
            this.lblModo.Location = new System.Drawing.Point(56, 15);
            // 
            // cmbModo
            // 
            this.cmbModo.Location = new System.Drawing.Point(177, 14);
            this.cmbModo.Size = new System.Drawing.Size(156, 31);
            // 
            // lblId
            // 
            this.lblId.Location = new System.Drawing.Point(392, 17);
            // 
            // txtId
            // 
            this.txtId.Location = new System.Drawing.Point(452, 14);
            this.txtId.Size = new System.Drawing.Size(129, 30);
            // 
            // panelBotones
            // 
            this.panelBotones.Location = new System.Drawing.Point(100, 525);
            this.panelBotones.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panelBotones.Padding = new System.Windows.Forms.Padding(11, 9, 11, 9);
            this.panelBotones.Size = new System.Drawing.Size(2051, 52);
            // 
            // btnGuardar
            // 
            this.btnGuardar.FlatAppearance.BorderSize = 0;
            this.btnGuardar.Location = new System.Drawing.Point(771, 16);
            this.btnGuardar.Size = new System.Drawing.Size(140, 34);
            this.btnGuardar.Text = "💾 Guardar Historial";
            // 
            // btnCancelar
            // 
            this.btnCancelar.FlatAppearance.BorderSize = 0;
            this.btnCancelar.Location = new System.Drawing.Point(616, 16);
            // 
            // btnEliminar
            // 
            this.btnEliminar.FlatAppearance.BorderSize = 0;
            this.btnEliminar.Size = new System.Drawing.Size(140, 34);
            this.btnEliminar.Text = "🗑️ Eliminar Historial";
            // 
            // lblContador
            // 
            this.lblContador.AutoSize = true;
            this.lblContador.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblContador.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.lblContador.Location = new System.Drawing.Point(751, 47);
            this.lblContador.Name = "lblContador";
            this.lblContador.Size = new System.Drawing.Size(153, 20);
            this.lblContador.TabIndex = 6;
            this.lblContador.Text = "📊 Total: 0 registros";
            // 
            // cmbAnimalFiltro
            // 
            this.cmbAnimalFiltro.BackColor = System.Drawing.Color.White;
            this.cmbAnimalFiltro.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbAnimalFiltro.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbAnimalFiltro.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.cmbAnimalFiltro.FormattingEnabled = true;
            this.cmbAnimalFiltro.Location = new System.Drawing.Point(512, 35);
            this.cmbAnimalFiltro.Name = "cmbAnimalFiltro";
            this.cmbAnimalFiltro.Size = new System.Drawing.Size(220, 31);
            this.cmbAnimalFiltro.TabIndex = 8;
            // 
            // lblAnimalFiltro
            // 
            this.lblAnimalFiltro.AutoSize = true;
            this.lblAnimalFiltro.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblAnimalFiltro.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.lblAnimalFiltro.Location = new System.Drawing.Point(532, 9);
            this.lblAnimalFiltro.Name = "lblAnimalFiltro";
            this.lblAnimalFiltro.Size = new System.Drawing.Size(188, 23);
            this.lblAnimalFiltro.TabIndex = 7;
            this.lblAnimalFiltro.Text = "🐕 Filtrar por Animal:";
            // 
            // grpDatosHistorial
            // 
            this.grpDatosHistorial.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpDatosHistorial.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(250)))));
            this.grpDatosHistorial.Controls.Add(this.tableLayoutHistorial);
            this.grpDatosHistorial.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.grpDatosHistorial.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.grpDatosHistorial.Location = new System.Drawing.Point(20, 75);
            this.grpDatosHistorial.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.grpDatosHistorial.Name = "grpDatosHistorial";
            this.grpDatosHistorial.Padding = new System.Windows.Forms.Padding(15);
            this.grpDatosHistorial.Size = new System.Drawing.Size(1089, 349);
            this.grpDatosHistorial.TabIndex = 0;
            this.grpDatosHistorial.TabStop = false;
            this.grpDatosHistorial.Text = "📋 Información del Historial Médico";
            // 
            // tableLayoutHistorial
            // 
            this.tableLayoutHistorial.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutHistorial.BackColor = System.Drawing.Color.White;
            this.tableLayoutHistorial.ColumnCount = 4;
            this.tableLayoutHistorial.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 174F));
            this.tableLayoutHistorial.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutHistorial.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tableLayoutHistorial.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutHistorial.Controls.Add(this.lblAnimal, 0, 0);
            this.tableLayoutHistorial.Controls.Add(this.cmbAnimal, 1, 0);
            this.tableLayoutHistorial.Controls.Add(this.btnBuscarAnimal, 2, 0);
            this.tableLayoutHistorial.Controls.Add(this.lblFechaCreacion, 3, 0);
            this.tableLayoutHistorial.Controls.Add(this.dtpFechaCreacion, 0, 1);
            this.tableLayoutHistorial.Controls.Add(this.lblFechaActualizacion, 1, 1);
            this.tableLayoutHistorial.Controls.Add(this.dtpFechaActualizacion, 2, 1);
            this.tableLayoutHistorial.Controls.Add(this.lblNotasGenerales, 0, 2);
            this.tableLayoutHistorial.Controls.Add(this.txtNotasGenerales, 0, 3);
            this.tableLayoutHistorial.Controls.Add(this.lblAlergias, 0, 4);
            this.tableLayoutHistorial.Controls.Add(this.txtAlergias, 0, 5);
            this.tableLayoutHistorial.Controls.Add(this.lblCondicionesMedicas, 2, 4);
            this.tableLayoutHistorial.Controls.Add(this.txtCondicionesMedicas, 2, 5);
            this.tableLayoutHistorial.Location = new System.Drawing.Point(15, 35);
            this.tableLayoutHistorial.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tableLayoutHistorial.Name = "tableLayoutHistorial";
            this.tableLayoutHistorial.RowCount = 6;
            this.tableLayoutHistorial.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 48F));
            this.tableLayoutHistorial.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 48F));
            this.tableLayoutHistorial.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tableLayoutHistorial.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.tableLayoutHistorial.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tableLayoutHistorial.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tableLayoutHistorial.Size = new System.Drawing.Size(1059, 294);
            this.tableLayoutHistorial.TabIndex = 0;
            // 
            // lblAnimal
            // 
            this.lblAnimal.AutoSize = true;
            this.lblAnimal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblAnimal.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblAnimal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(76)))), ((int)(((byte)(60)))));
            this.lblAnimal.Location = new System.Drawing.Point(3, 0);
            this.lblAnimal.Name = "lblAnimal";
            this.lblAnimal.Size = new System.Drawing.Size(168, 48);
            this.lblAnimal.TabIndex = 0;
            this.lblAnimal.Text = "🐕 Animal *:";
            this.lblAnimal.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cmbAnimal
            // 
            this.cmbAnimal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbAnimal.BackColor = System.Drawing.Color.White;
            this.cmbAnimal.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbAnimal.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.cmbAnimal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.cmbAnimal.FormattingEnabled = true;
            this.cmbAnimal.Location = new System.Drawing.Point(177, 7);
            this.cmbAnimal.Margin = new System.Windows.Forms.Padding(3, 2, 10, 2);
            this.cmbAnimal.Name = "cmbAnimal";
            this.cmbAnimal.Size = new System.Drawing.Size(369, 33);
            this.cmbAnimal.TabIndex = 1;
            // 
            // btnBuscarAnimal
            // 
            this.btnBuscarAnimal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBuscarAnimal.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.btnBuscarAnimal.FlatAppearance.BorderSize = 0;
            this.btnBuscarAnimal.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBuscarAnimal.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnBuscarAnimal.ForeColor = System.Drawing.Color.White;
            this.btnBuscarAnimal.Location = new System.Drawing.Point(559, 6);
            this.btnBuscarAnimal.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnBuscarAnimal.Name = "btnBuscarAnimal";
            this.btnBuscarAnimal.Size = new System.Drawing.Size(114, 36);
            this.btnBuscarAnimal.TabIndex = 2;
            this.btnBuscarAnimal.Text = "🔍 Buscar";
            this.btnBuscarAnimal.UseVisualStyleBackColor = false;
            // 
            // lblFechaCreacion
            // 
            this.lblFechaCreacion.AutoSize = true;
            this.lblFechaCreacion.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblFechaCreacion.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblFechaCreacion.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.lblFechaCreacion.Location = new System.Drawing.Point(679, 0);
            this.lblFechaCreacion.Name = "lblFechaCreacion";
            this.lblFechaCreacion.Size = new System.Drawing.Size(377, 48);
            this.lblFechaCreacion.TabIndex = 3;
            this.lblFechaCreacion.Text = "📅 Fecha Creación:";
            this.lblFechaCreacion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // dtpFechaCreacion
            // 
            this.dtpFechaCreacion.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.dtpFechaCreacion.Enabled = false;
            this.dtpFechaCreacion.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.dtpFechaCreacion.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFechaCreacion.Location = new System.Drawing.Point(3, 56);
            this.dtpFechaCreacion.Margin = new System.Windows.Forms.Padding(3, 2, 10, 2);
            this.dtpFechaCreacion.Name = "dtpFechaCreacion";
            this.dtpFechaCreacion.Size = new System.Drawing.Size(161, 32);
            this.dtpFechaCreacion.TabIndex = 4;
            // 
            // lblFechaActualizacion
            // 
            this.lblFechaActualizacion.AutoSize = true;
            this.lblFechaActualizacion.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblFechaActualizacion.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblFechaActualizacion.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.lblFechaActualizacion.Location = new System.Drawing.Point(177, 48);
            this.lblFechaActualizacion.Name = "lblFechaActualizacion";
            this.lblFechaActualizacion.Size = new System.Drawing.Size(376, 48);
            this.lblFechaActualizacion.TabIndex = 5;
            this.lblFechaActualizacion.Text = "🔄 Última Actualización:";
            this.lblFechaActualizacion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // dtpFechaActualizacion
            // 
            this.dtpFechaActualizacion.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.dtpFechaActualizacion.Enabled = false;
            this.dtpFechaActualizacion.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.dtpFechaActualizacion.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFechaActualizacion.Location = new System.Drawing.Point(559, 56);
            this.dtpFechaActualizacion.Margin = new System.Windows.Forms.Padding(3, 2, 10, 2);
            this.dtpFechaActualizacion.Name = "dtpFechaActualizacion";
            this.dtpFechaActualizacion.Size = new System.Drawing.Size(107, 32);
            this.dtpFechaActualizacion.TabIndex = 6;
            // 
            // lblNotasGenerales
            // 
            this.lblNotasGenerales.AutoSize = true;
            this.tableLayoutHistorial.SetColumnSpan(this.lblNotasGenerales, 4);
            this.lblNotasGenerales.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblNotasGenerales.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblNotasGenerales.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(76)))), ((int)(((byte)(60)))));
            this.lblNotasGenerales.Location = new System.Drawing.Point(3, 96);
            this.lblNotasGenerales.Name = "lblNotasGenerales";
            this.lblNotasGenerales.Size = new System.Drawing.Size(1053, 32);
            this.lblNotasGenerales.TabIndex = 7;
            this.lblNotasGenerales.Text = "📝 Notas Generales *:";
            this.lblNotasGenerales.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // txtNotasGenerales
            // 
            this.txtNotasGenerales.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtNotasGenerales.BackColor = System.Drawing.Color.White;
            this.txtNotasGenerales.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tableLayoutHistorial.SetColumnSpan(this.txtNotasGenerales, 4);
            this.txtNotasGenerales.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtNotasGenerales.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.txtNotasGenerales.Location = new System.Drawing.Point(3, 130);
            this.txtNotasGenerales.Margin = new System.Windows.Forms.Padding(3, 2, 10, 2);
            this.txtNotasGenerales.MaxLength = 2000;
            this.txtNotasGenerales.Multiline = true;
            this.txtNotasGenerales.Name = "txtNotasGenerales";
            this.txtNotasGenerales.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtNotasGenerales.Size = new System.Drawing.Size(1046, 76);
            this.txtNotasGenerales.TabIndex = 8;
            // 
            // lblAlergias
            // 
            this.lblAlergias.AutoSize = true;
            this.tableLayoutHistorial.SetColumnSpan(this.lblAlergias, 2);
            this.lblAlergias.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblAlergias.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblAlergias.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.lblAlergias.Location = new System.Drawing.Point(3, 208);
            this.lblAlergias.Name = "lblAlergias";
            this.lblAlergias.Size = new System.Drawing.Size(550, 32);
            this.lblAlergias.TabIndex = 9;
            this.lblAlergias.Text = "⚠️ Alergias:";
            this.lblAlergias.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // txtAlergias
            // 
            this.txtAlergias.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtAlergias.BackColor = System.Drawing.Color.White;
            this.txtAlergias.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tableLayoutHistorial.SetColumnSpan(this.txtAlergias, 2);
            this.txtAlergias.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtAlergias.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.txtAlergias.Location = new System.Drawing.Point(3, 242);
            this.txtAlergias.Margin = new System.Windows.Forms.Padding(3, 2, 10, 2);
            this.txtAlergias.MaxLength = 500;
            this.txtAlergias.Multiline = true;
            this.txtAlergias.Name = "txtAlergias";
            this.txtAlergias.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtAlergias.Size = new System.Drawing.Size(543, 50);
            this.txtAlergias.TabIndex = 10;
            // 
            // lblCondicionesMedicas
            // 
            this.lblCondicionesMedicas.AutoSize = true;
            this.tableLayoutHistorial.SetColumnSpan(this.lblCondicionesMedicas, 2);
            this.lblCondicionesMedicas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCondicionesMedicas.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblCondicionesMedicas.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(89)))), ((int)(((byte)(182)))));
            this.lblCondicionesMedicas.Location = new System.Drawing.Point(559, 208);
            this.lblCondicionesMedicas.Name = "lblCondicionesMedicas";
            this.lblCondicionesMedicas.Size = new System.Drawing.Size(497, 32);
            this.lblCondicionesMedicas.TabIndex = 11;
            this.lblCondicionesMedicas.Text = "🩺 Condiciones Médicas:";
            this.lblCondicionesMedicas.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // txtCondicionesMedicas
            // 
            this.txtCondicionesMedicas.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCondicionesMedicas.BackColor = System.Drawing.Color.White;
            this.txtCondicionesMedicas.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tableLayoutHistorial.SetColumnSpan(this.txtCondicionesMedicas, 2);
            this.txtCondicionesMedicas.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtCondicionesMedicas.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.txtCondicionesMedicas.Location = new System.Drawing.Point(559, 242);
            this.txtCondicionesMedicas.Margin = new System.Windows.Forms.Padding(3, 2, 10, 2);
            this.txtCondicionesMedicas.MaxLength = 500;
            this.txtCondicionesMedicas.Multiline = true;
            this.txtCondicionesMedicas.Name = "txtCondicionesMedicas";
            this.txtCondicionesMedicas.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtCondicionesMedicas.Size = new System.Drawing.Size(490, 50);
            this.txtCondicionesMedicas.TabIndex = 12;
            // 
            // btnVerDetalles
            // 
            this.btnVerDetalles.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.btnVerDetalles.FlatAppearance.BorderSize = 0;
            this.btnVerDetalles.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnVerDetalles.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnVerDetalles.ForeColor = System.Drawing.Color.White;
            this.btnVerDetalles.Location = new System.Drawing.Point(765, 8);
            this.btnVerDetalles.Name = "btnVerDetalles";
            this.btnVerDetalles.Size = new System.Drawing.Size(130, 31);
            this.btnVerDetalles.TabIndex = 9;
            this.btnVerDetalles.Text = "📋 Ver Detalles";
            this.btnVerDetalles.UseVisualStyleBackColor = false;
            // 
            // grpDetallesHistorial
            // 
            this.grpDetallesHistorial.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpDetallesHistorial.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(250)))));
            this.grpDetallesHistorial.Controls.Add(this.dgvDetalles);
            this.grpDetallesHistorial.Controls.Add(this.panelBotonesDetalle);
            this.grpDetallesHistorial.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.grpDetallesHistorial.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.grpDetallesHistorial.Location = new System.Drawing.Point(20, 20);
            this.grpDetallesHistorial.Name = "grpDetallesHistorial";
            this.grpDetallesHistorial.Padding = new System.Windows.Forms.Padding(10);
            this.grpDetallesHistorial.Size = new System.Drawing.Size(1095, 280);
            this.grpDetallesHistorial.TabIndex = 0;
            this.grpDetallesHistorial.TabStop = false;
            this.grpDetallesHistorial.Text = "📋 Detalles del Historial Médico";
            // 
            // dgvDetalles
            // 
            this.dgvDetalles.AllowUserToAddRows = false;
            this.dgvDetalles.AllowUserToDeleteRows = false;
            this.dgvDetalles.BackgroundColor = System.Drawing.Color.White;
            this.dgvDetalles.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDetalles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvDetalles.Location = new System.Drawing.Point(10, 37);
            this.dgvDetalles.Name = "dgvDetalles";
            this.dgvDetalles.ReadOnly = true;
            this.dgvDetalles.RowHeadersWidth = 51;
            this.dgvDetalles.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDetalles.Size = new System.Drawing.Size(1075, 193);
            this.dgvDetalles.TabIndex = 0;
            // 
            // panelBotonesDetalle
            // 
            this.panelBotonesDetalle.Controls.Add(this.btnNuevoDetalle);
            this.panelBotonesDetalle.Controls.Add(this.btnEliminarDetalle);
            this.panelBotonesDetalle.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelBotonesDetalle.Location = new System.Drawing.Point(10, 230);
            this.panelBotonesDetalle.Name = "panelBotonesDetalle";
            this.panelBotonesDetalle.Size = new System.Drawing.Size(1075, 40);
            this.panelBotonesDetalle.TabIndex = 1;
            // 
            // btnNuevoDetalle
            // 
            this.btnNuevoDetalle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.btnNuevoDetalle.FlatAppearance.BorderSize = 0;
            this.btnNuevoDetalle.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNuevoDetalle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnNuevoDetalle.ForeColor = System.Drawing.Color.White;
            this.btnNuevoDetalle.Location = new System.Drawing.Point(10, 5);
            this.btnNuevoDetalle.Name = "btnNuevoDetalle";
            this.btnNuevoDetalle.Size = new System.Drawing.Size(120, 30);
            this.btnNuevoDetalle.TabIndex = 0;
            this.btnNuevoDetalle.Text = "➕ Nuevo";
            this.btnNuevoDetalle.UseVisualStyleBackColor = false;
            // 
            // btnEliminarDetalle
            // 
            this.btnEliminarDetalle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(76)))), ((int)(((byte)(60)))));
            this.btnEliminarDetalle.FlatAppearance.BorderSize = 0;
            this.btnEliminarDetalle.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEliminarDetalle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnEliminarDetalle.ForeColor = System.Drawing.Color.White;
            this.btnEliminarDetalle.Location = new System.Drawing.Point(140, 5);
            this.btnEliminarDetalle.Name = "btnEliminarDetalle";
            this.btnEliminarDetalle.Size = new System.Drawing.Size(120, 30);
            this.btnEliminarDetalle.TabIndex = 1;
            this.btnEliminarDetalle.Text = "🗑️ Eliminar";
            this.btnEliminarDetalle.UseVisualStyleBackColor = false;
            // 
            // grpDatosDetalle
            // 
            this.grpDatosDetalle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpDatosDetalle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(250)))));
            this.grpDatosDetalle.Controls.Add(this.tableLayoutDetalle);
            this.grpDatosDetalle.Controls.Add(this.panelBotonesDetalleForm);
            this.grpDatosDetalle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.grpDatosDetalle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.grpDatosDetalle.Location = new System.Drawing.Point(20, 320);
            this.grpDatosDetalle.Name = "grpDatosDetalle";
            this.grpDatosDetalle.Padding = new System.Windows.Forms.Padding(10);
            this.grpDatosDetalle.Size = new System.Drawing.Size(1095, 250);
            this.grpDatosDetalle.TabIndex = 1;
            this.grpDatosDetalle.TabStop = false;
            this.grpDatosDetalle.Text = "📝 Formulario de Detalle";
            this.grpDatosDetalle.Visible = false;
            // 
            // tableLayoutDetalle
            // 
            this.tableLayoutDetalle.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutDetalle.BackColor = System.Drawing.Color.White;
            this.tableLayoutDetalle.ColumnCount = 4;
            this.tableLayoutDetalle.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutDetalle.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutDetalle.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutDetalle.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutDetalle.Controls.Add(this.lblTipoRegistro, 0, 0);
            this.tableLayoutDetalle.Controls.Add(this.cmbTipoRegistro, 0, 1);
            this.tableLayoutDetalle.Controls.Add(this.lblFechaRegistro, 1, 0);
            this.tableLayoutDetalle.Controls.Add(this.dtpFechaRegistro, 1, 1);
            this.tableLayoutDetalle.Controls.Add(this.lblVeterinario, 2, 0);
            this.tableLayoutDetalle.Controls.Add(this.cmbVeterinario, 2, 1);
            this.tableLayoutDetalle.Controls.Add(this.lblDescripcionDetalle, 0, 2);
            this.tableLayoutDetalle.Controls.Add(this.txtDescripcionDetalle, 0, 3);
            this.tableLayoutDetalle.Controls.Add(this.lblTratamiento, 0, 4);
            this.tableLayoutDetalle.Controls.Add(this.txtTratamiento, 0, 5);
            this.tableLayoutDetalle.Controls.Add(this.lblMedicamentos, 2, 4);
            this.tableLayoutDetalle.Controls.Add(this.txtMedicamentos, 2, 5);
            this.tableLayoutDetalle.Controls.Add(this.lblObservaciones, 0, 6);
            this.tableLayoutDetalle.Controls.Add(this.txtObservaciones, 0, 7);
            this.tableLayoutDetalle.Location = new System.Drawing.Point(13, 30);
            this.tableLayoutDetalle.Name = "tableLayoutDetalle";
            this.tableLayoutDetalle.RowCount = 8;
            this.tableLayoutDetalle.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutDetalle.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutDetalle.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutDetalle.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutDetalle.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutDetalle.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutDetalle.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutDetalle.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutDetalle.Size = new System.Drawing.Size(1069, 175);
            this.tableLayoutDetalle.TabIndex = 0;
            // 
            // lblTipoRegistro
            // 
            this.lblTipoRegistro.Location = new System.Drawing.Point(3, 0);
            this.lblTipoRegistro.Name = "lblTipoRegistro";
            this.lblTipoRegistro.Size = new System.Drawing.Size(100, 23);
            this.lblTipoRegistro.TabIndex = 0;
            // 
            // cmbTipoRegistro
            // 
            this.cmbTipoRegistro.Location = new System.Drawing.Point(3, 28);
            this.cmbTipoRegistro.Name = "cmbTipoRegistro";
            this.cmbTipoRegistro.Size = new System.Drawing.Size(121, 36);
            this.cmbTipoRegistro.TabIndex = 1;
            // 
            // lblFechaRegistro
            // 
            this.lblFechaRegistro.Location = new System.Drawing.Point(270, 0);
            this.lblFechaRegistro.Name = "lblFechaRegistro";
            this.lblFechaRegistro.Size = new System.Drawing.Size(100, 23);
            this.lblFechaRegistro.TabIndex = 2;
            // 
            // dtpFechaRegistro
            // 
            this.dtpFechaRegistro.Location = new System.Drawing.Point(270, 28);
            this.dtpFechaRegistro.Name = "dtpFechaRegistro";
            this.dtpFechaRegistro.Size = new System.Drawing.Size(200, 34);
            this.dtpFechaRegistro.TabIndex = 3;
            // 
            // lblVeterinario
            // 
            this.lblVeterinario.Location = new System.Drawing.Point(537, 0);
            this.lblVeterinario.Name = "lblVeterinario";
            this.lblVeterinario.Size = new System.Drawing.Size(100, 23);
            this.lblVeterinario.TabIndex = 4;
            // 
            // cmbVeterinario
            // 
            this.cmbVeterinario.Location = new System.Drawing.Point(537, 28);
            this.cmbVeterinario.Name = "cmbVeterinario";
            this.cmbVeterinario.Size = new System.Drawing.Size(121, 36);
            this.cmbVeterinario.TabIndex = 5;
            // 
            // lblDescripcionDetalle
            // 
            this.lblDescripcionDetalle.Location = new System.Drawing.Point(3, 60);
            this.lblDescripcionDetalle.Name = "lblDescripcionDetalle";
            this.lblDescripcionDetalle.Size = new System.Drawing.Size(100, 23);
            this.lblDescripcionDetalle.TabIndex = 6;
            // 
            // txtDescripcionDetalle
            // 
            this.txtDescripcionDetalle.Location = new System.Drawing.Point(3, 88);
            this.txtDescripcionDetalle.Name = "txtDescripcionDetalle";
            this.txtDescripcionDetalle.Size = new System.Drawing.Size(100, 34);
            this.txtDescripcionDetalle.TabIndex = 7;
            // 
            // lblTratamiento
            // 
            this.lblTratamiento.Location = new System.Drawing.Point(3, 120);
            this.lblTratamiento.Name = "lblTratamiento";
            this.lblTratamiento.Size = new System.Drawing.Size(100, 23);
            this.lblTratamiento.TabIndex = 8;
            // 
            // txtTratamiento
            // 
            this.txtTratamiento.Location = new System.Drawing.Point(3, 148);
            this.txtTratamiento.Name = "txtTratamiento";
            this.txtTratamiento.Size = new System.Drawing.Size(100, 34);
            this.txtTratamiento.TabIndex = 9;
            // 
            // lblMedicamentos
            // 
            this.lblMedicamentos.Location = new System.Drawing.Point(537, 120);
            this.lblMedicamentos.Name = "lblMedicamentos";
            this.lblMedicamentos.Size = new System.Drawing.Size(100, 23);
            this.lblMedicamentos.TabIndex = 10;
            // 
            // txtMedicamentos
            // 
            this.txtMedicamentos.Location = new System.Drawing.Point(537, 148);
            this.txtMedicamentos.Name = "txtMedicamentos";
            this.txtMedicamentos.Size = new System.Drawing.Size(100, 34);
            this.txtMedicamentos.TabIndex = 11;
            // 
            // lblObservaciones
            // 
            this.lblObservaciones.Location = new System.Drawing.Point(3, 180);
            this.lblObservaciones.Name = "lblObservaciones";
            this.lblObservaciones.Size = new System.Drawing.Size(100, 23);
            this.lblObservaciones.TabIndex = 12;
            // 
            // txtObservaciones
            // 
            this.txtObservaciones.Location = new System.Drawing.Point(3, 208);
            this.txtObservaciones.Name = "txtObservaciones";
            this.txtObservaciones.Size = new System.Drawing.Size(100, 34);
            this.txtObservaciones.TabIndex = 13;
            // 
            // panelBotonesDetalleForm
            // 
            this.panelBotonesDetalleForm.Controls.Add(this.btnGuardarDetalle);
            this.panelBotonesDetalleForm.Controls.Add(this.btnCancelarDetalle);
            this.panelBotonesDetalleForm.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelBotonesDetalleForm.Location = new System.Drawing.Point(10, 210);
            this.panelBotonesDetalleForm.Name = "panelBotonesDetalleForm";
            this.panelBotonesDetalleForm.Size = new System.Drawing.Size(1075, 30);
            this.panelBotonesDetalleForm.TabIndex = 1;
            // 
            // btnGuardarDetalle
            // 
            this.btnGuardarDetalle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.btnGuardarDetalle.FlatAppearance.BorderSize = 0;
            this.btnGuardarDetalle.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGuardarDetalle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnGuardarDetalle.ForeColor = System.Drawing.Color.White;
            this.btnGuardarDetalle.Location = new System.Drawing.Point(10, 0);
            this.btnGuardarDetalle.Name = "btnGuardarDetalle";
            this.btnGuardarDetalle.Size = new System.Drawing.Size(120, 30);
            this.btnGuardarDetalle.TabIndex = 0;
            this.btnGuardarDetalle.Text = "💾 Guardar";
            this.btnGuardarDetalle.UseVisualStyleBackColor = false;
            // 
            // btnCancelarDetalle
            // 
            this.btnCancelarDetalle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(149)))), ((int)(((byte)(165)))), ((int)(((byte)(166)))));
            this.btnCancelarDetalle.FlatAppearance.BorderSize = 0;
            this.btnCancelarDetalle.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelarDetalle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnCancelarDetalle.ForeColor = System.Drawing.Color.White;
            this.btnCancelarDetalle.Location = new System.Drawing.Point(140, 0);
            this.btnCancelarDetalle.Name = "btnCancelarDetalle";
            this.btnCancelarDetalle.Size = new System.Drawing.Size(120, 30);
            this.btnCancelarDetalle.TabIndex = 1;
            this.btnCancelarDetalle.Text = "❌ Cancelar";
            this.btnCancelarDetalle.UseVisualStyleBackColor = false;
            // 
            // HistorialModule
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.Name = "HistorialModule";
            this.tabControlPrincipal.ResumeLayout(false);
            this.tabInicio.ResumeLayout(false);
            this.tabConfiguraciones.ResumeLayout(false);
            this.panelBusqueda.ResumeLayout(false);
            this.panelBusqueda.PerformLayout();
            this.panelFormulario.ResumeLayout(false);
            this.panelSuperior.ResumeLayout(false);
            this.panelSuperior.PerformLayout();
            this.panelBotones.ResumeLayout(false);
            this.grpDatosHistorial.ResumeLayout(false);
            this.tableLayoutHistorial.ResumeLayout(false);
            this.tableLayoutHistorial.PerformLayout();
            this.grpDetallesHistorial.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetalles)).EndInit();
            this.panelBotonesDetalle.ResumeLayout(false);
            this.grpDatosDetalle.ResumeLayout(false);
            this.tableLayoutDetalle.ResumeLayout(false);
            this.tableLayoutDetalle.PerformLayout();
            this.panelBotonesDetalleForm.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
    }
}
