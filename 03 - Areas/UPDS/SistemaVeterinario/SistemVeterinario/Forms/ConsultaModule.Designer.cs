using System.Windows.Forms;
using System.Drawing;

namespace SistemVeterinario.Forms
{
    partial class ConsultaModule
    {
        /// <summary> 
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        // Controles específicos de consultas/diagnósticos
        private Label lblContador;
        private ComboBox cmbCategoriaFiltro;
        private Label lblCategoriaFiltro;
        private GroupBox grpDatosDiagnostico;
        private TableLayoutPanel tableLayoutDiagnostico;
        private Label lblCodigo;
        private TextBox txtCodigo;
        private Button btnGenerarCodigo;
        private Label lblNombre;
        private TextBox txtNombre;
        private Label lblCategoria;
        private ComboBox cmbCategoria;
        private Button btnNuevaCategoria;
        private Label lblPrecioBase;
        private NumericUpDown nudPrecioBase;
        private CheckBox chkRequiereEquipamiento;
        private CheckBox chkActivo;
        private Label lblDescripcion;
        private TextBox txtDescripcion;
        private Button btnVerPorCategoria;

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
            this.cmbCategoriaFiltro = new System.Windows.Forms.ComboBox();
            this.lblCategoriaFiltro = new System.Windows.Forms.Label();
            this.grpDatosDiagnostico = new System.Windows.Forms.GroupBox();
            this.tableLayoutDiagnostico = new System.Windows.Forms.TableLayoutPanel();
            this.lblCodigo = new System.Windows.Forms.Label();
            this.txtCodigo = new System.Windows.Forms.TextBox();
            this.btnGenerarCodigo = new System.Windows.Forms.Button();
            this.lblNombre = new System.Windows.Forms.Label();
            this.txtNombre = new System.Windows.Forms.TextBox();
            this.chkRequiereEquipamiento = new System.Windows.Forms.CheckBox();
            this.lblCategoria = new System.Windows.Forms.Label();
            this.cmbCategoria = new System.Windows.Forms.ComboBox();
            this.btnNuevaCategoria = new System.Windows.Forms.Button();
            this.lblPrecioBase = new System.Windows.Forms.Label();
            this.nudPrecioBase = new System.Windows.Forms.NumericUpDown();
            this.chkActivo = new System.Windows.Forms.CheckBox();
            this.lblDescripcion = new System.Windows.Forms.Label();
            this.txtDescripcion = new System.Windows.Forms.TextBox();
            this.btnVerPorCategoria = new System.Windows.Forms.Button();
            this.tabControlPrincipal.SuspendLayout();
            this.tabInicio.SuspendLayout();
            this.tabConfiguraciones.SuspendLayout();
            this.panelBusqueda.SuspendLayout();
            this.panelFormulario.SuspendLayout();
            this.panelSuperior.SuspendLayout();
            this.panelBotones.SuspendLayout();
            this.grpDatosDiagnostico.SuspendLayout();
            this.tableLayoutDiagnostico.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudPrecioBase)).BeginInit();
            this.SuspendLayout();
            // 
            // tabInicio
            // 
            this.tabInicio.Location = new System.Drawing.Point(4, 39);
            this.tabInicio.Padding = new System.Windows.Forms.Padding(3);
            this.tabInicio.Size = new System.Drawing.Size(1135, 597);
            this.tabInicio.Text = "Gestión de Consultas";
            // 
            // tabConfiguraciones
            // 
            this.tabConfiguraciones.Location = new System.Drawing.Point(4, 39);
            this.tabConfiguraciones.Padding = new System.Windows.Forms.Padding(3);
            this.tabConfiguraciones.Size = new System.Drawing.Size(1135, 597);
            this.tabConfiguraciones.Text = "Configuración de Diagnósticos";
            // 
            // panelBusqueda
            // 
            this.panelBusqueda.Controls.Add(this.lblContador);
            this.panelBusqueda.Controls.Add(this.lblCategoriaFiltro);
            this.panelBusqueda.Controls.Add(this.cmbCategoriaFiltro);
            this.panelBusqueda.Controls.Add(this.btnVerPorCategoria);
            this.panelBusqueda.Location = new System.Drawing.Point(18, 11);
            this.panelBusqueda.Margin = new System.Windows.Forms.Padding(10, 8, 10, 8);
            this.panelBusqueda.Padding = new System.Windows.Forms.Padding(17, 13, 17, 13);
            this.panelBusqueda.Size = new System.Drawing.Size(1121, 120);
            this.panelBusqueda.Controls.SetChildIndex(this.btnVerPorCategoria, 0);
            this.panelBusqueda.Controls.SetChildIndex(this.cmbCategoriaFiltro, 0);
            this.panelBusqueda.Controls.SetChildIndex(this.lblCategoriaFiltro, 0);
            this.panelBusqueda.Controls.SetChildIndex(this.lblContador, 0);
            this.panelBusqueda.Controls.SetChildIndex(this.chkMostrarTodo, 0);
            this.panelBusqueda.Controls.SetChildIndex(this.btnNuevo, 0);
            this.panelBusqueda.Controls.SetChildIndex(this.btnBuscar, 0);
            this.panelBusqueda.Controls.SetChildIndex(this.txtBuscar, 0);
            // 
            // txtBuscar
            // 
            this.txtBuscar.Location = new System.Drawing.Point(22, 9);
            this.txtBuscar.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.txtBuscar.Size = new System.Drawing.Size(308, 32);
            // 
            // btnBuscar
            // 
            this.btnBuscar.FlatAppearance.BorderSize = 0;
            this.btnBuscar.Location = new System.Drawing.Point(352, 4);
            this.btnBuscar.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.btnBuscar.Size = new System.Drawing.Size(120, 41);
            // 
            // btnNuevo
            // 
            this.btnNuevo.FlatAppearance.BorderSize = 0;
            this.btnNuevo.Location = new System.Drawing.Point(975, 4);
            this.btnNuevo.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.btnNuevo.Size = new System.Drawing.Size(140, 40);
            this.btnNuevo.Text = "➕ Nuevo Diagnóstico";
            // 
            // chkMostrarTodo
            // 
            this.chkMostrarTodo.Location = new System.Drawing.Point(49, 49);
            this.chkMostrarTodo.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            // 
            // panelFormulario
            // 
            this.panelFormulario.Controls.Add(this.grpDatosDiagnostico);
            this.panelFormulario.Location = new System.Drawing.Point(3, 3);
            this.panelFormulario.Padding = new System.Windows.Forms.Padding(11, 9, 11, 9);
            this.panelFormulario.Size = new System.Drawing.Size(1129, 591);
            this.panelFormulario.Controls.SetChildIndex(this.grpDatosDiagnostico, 0);
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
            this.lblModo.Location = new System.Drawing.Point(49, 23);
            // 
            // cmbModo
            // 
            this.cmbModo.Location = new System.Drawing.Point(173, 17);
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
            this.btnGuardar.Location = new System.Drawing.Point(771, 12);
            this.btnGuardar.Size = new System.Drawing.Size(140, 38);
            this.btnGuardar.Text = "💾 Guardar Diagnóstico";
            // 
            // btnCancelar
            // 
            this.btnCancelar.FlatAppearance.BorderSize = 0;
            this.btnCancelar.Location = new System.Drawing.Point(616, 12);
            this.btnCancelar.Size = new System.Drawing.Size(137, 41);
            // 
            // btnEliminar
            // 
            this.btnEliminar.FlatAppearance.BorderSize = 0;
            this.btnEliminar.Location = new System.Drawing.Point(17, 12);
            this.btnEliminar.Size = new System.Drawing.Size(140, 38);
            this.btnEliminar.Text = "🗑️ Eliminar Diagnóstico";
            // 
            // lblContador
            // 
            this.lblContador.AutoSize = true;
            this.lblContador.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblContador.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.lblContador.Location = new System.Drawing.Point(550, 9);
            this.lblContador.Name = "lblContador";
            this.lblContador.Size = new System.Drawing.Size(153, 20);
            this.lblContador.TabIndex = 6;
            this.lblContador.Text = "📊 Total: 0 registros";
            // 
            // cmbCategoriaFiltro
            // 
            this.cmbCategoriaFiltro.BackColor = System.Drawing.Color.White;
            this.cmbCategoriaFiltro.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCategoriaFiltro.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbCategoriaFiltro.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.cmbCategoriaFiltro.FormattingEnabled = true;
            this.cmbCategoriaFiltro.Location = new System.Drawing.Point(509, 38);
            this.cmbCategoriaFiltro.Name = "cmbCategoriaFiltro";
            this.cmbCategoriaFiltro.Size = new System.Drawing.Size(220, 31);
            this.cmbCategoriaFiltro.TabIndex = 8;
            // 
            // lblCategoriaFiltro
            // 
            this.lblCategoriaFiltro.AutoSize = true;
            this.lblCategoriaFiltro.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblCategoriaFiltro.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.lblCategoriaFiltro.Location = new System.Drawing.Point(751, 49);
            this.lblCategoriaFiltro.Name = "lblCategoriaFiltro";
            this.lblCategoriaFiltro.Size = new System.Drawing.Size(209, 23);
            this.lblCategoriaFiltro.TabIndex = 7;
            this.lblCategoriaFiltro.Text = "🏷️ Filtrar por Categoría:";
            // 
            // grpDatosDiagnostico
            // 
            this.grpDatosDiagnostico.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpDatosDiagnostico.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(250)))));
            this.grpDatosDiagnostico.Controls.Add(this.tableLayoutDiagnostico);
            this.grpDatosDiagnostico.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.grpDatosDiagnostico.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.grpDatosDiagnostico.Location = new System.Drawing.Point(20, 75);
            this.grpDatosDiagnostico.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.grpDatosDiagnostico.Name = "grpDatosDiagnostico";
            this.grpDatosDiagnostico.Padding = new System.Windows.Forms.Padding(15);
            this.grpDatosDiagnostico.Size = new System.Drawing.Size(1089, 349);
            this.grpDatosDiagnostico.TabIndex = 0;
            this.grpDatosDiagnostico.TabStop = false;
            this.grpDatosDiagnostico.Text = "🩺 Información del Diagnóstico";
            // 
            // tableLayoutDiagnostico
            // 
            this.tableLayoutDiagnostico.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutDiagnostico.BackColor = System.Drawing.Color.White;
            this.tableLayoutDiagnostico.ColumnCount = 4;
            this.tableLayoutDiagnostico.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 174F));
            this.tableLayoutDiagnostico.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 232F));
            this.tableLayoutDiagnostico.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 116F));
            this.tableLayoutDiagnostico.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutDiagnostico.Controls.Add(this.lblCodigo, 0, 0);
            this.tableLayoutDiagnostico.Controls.Add(this.txtCodigo, 1, 0);
            this.tableLayoutDiagnostico.Controls.Add(this.btnGenerarCodigo, 2, 0);
            this.tableLayoutDiagnostico.Controls.Add(this.lblNombre, 3, 0);
            this.tableLayoutDiagnostico.Controls.Add(this.txtNombre, 0, 1);
            this.tableLayoutDiagnostico.Controls.Add(this.chkRequiereEquipamiento, 1, 1);
            this.tableLayoutDiagnostico.Controls.Add(this.lblCategoria, 2, 1);
            this.tableLayoutDiagnostico.Controls.Add(this.cmbCategoria, 3, 1);
            this.tableLayoutDiagnostico.Controls.Add(this.btnNuevaCategoria, 0, 2);
            this.tableLayoutDiagnostico.Controls.Add(this.lblPrecioBase, 1, 2);
            this.tableLayoutDiagnostico.Controls.Add(this.nudPrecioBase, 2, 2);
            this.tableLayoutDiagnostico.Controls.Add(this.chkActivo, 3, 2);
            this.tableLayoutDiagnostico.Controls.Add(this.lblDescripcion, 0, 3);
            this.tableLayoutDiagnostico.Controls.Add(this.txtDescripcion, 0, 4);
            this.tableLayoutDiagnostico.Location = new System.Drawing.Point(20, 43);
            this.tableLayoutDiagnostico.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tableLayoutDiagnostico.Name = "tableLayoutDiagnostico";
            this.tableLayoutDiagnostico.RowCount = 5;
            this.tableLayoutDiagnostico.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 48F));
            this.tableLayoutDiagnostico.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 48F));
            this.tableLayoutDiagnostico.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 48F));
            this.tableLayoutDiagnostico.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tableLayoutDiagnostico.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutDiagnostico.Size = new System.Drawing.Size(1013, 278);
            this.tableLayoutDiagnostico.TabIndex = 0;
            // 
            // lblCodigo
            // 
            this.lblCodigo.AutoSize = true;
            this.lblCodigo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCodigo.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblCodigo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.lblCodigo.Location = new System.Drawing.Point(3, 0);
            this.lblCodigo.Name = "lblCodigo";
            this.lblCodigo.Size = new System.Drawing.Size(168, 48);
            this.lblCodigo.TabIndex = 0;
            this.lblCodigo.Text = "🏷️ Código:";
            this.lblCodigo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtCodigo
            // 
            this.txtCodigo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCodigo.BackColor = System.Drawing.Color.White;
            this.txtCodigo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCodigo.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtCodigo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.txtCodigo.Location = new System.Drawing.Point(177, 8);
            this.txtCodigo.Margin = new System.Windows.Forms.Padding(3, 2, 10, 2);
            this.txtCodigo.MaxLength = 20;
            this.txtCodigo.Name = "txtCodigo";
            this.txtCodigo.Size = new System.Drawing.Size(219, 32);
            this.txtCodigo.TabIndex = 1;
            // 
            // btnGenerarCodigo
            // 
            this.btnGenerarCodigo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGenerarCodigo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.btnGenerarCodigo.FlatAppearance.BorderSize = 0;
            this.btnGenerarCodigo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGenerarCodigo.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnGenerarCodigo.ForeColor = System.Drawing.Color.White;
            this.btnGenerarCodigo.Location = new System.Drawing.Point(409, 6);
            this.btnGenerarCodigo.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnGenerarCodigo.Name = "btnGenerarCodigo";
            this.btnGenerarCodigo.Size = new System.Drawing.Size(110, 36);
            this.btnGenerarCodigo.TabIndex = 2;
            this.btnGenerarCodigo.Text = "🎲 Generar";
            this.btnGenerarCodigo.UseVisualStyleBackColor = false;
            // 
            // lblNombre
            // 
            this.lblNombre.AutoSize = true;
            this.lblNombre.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblNombre.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblNombre.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(76)))), ((int)(((byte)(60)))));
            this.lblNombre.Location = new System.Drawing.Point(525, 0);
            this.lblNombre.Name = "lblNombre";
            this.lblNombre.Size = new System.Drawing.Size(485, 48);
            this.lblNombre.TabIndex = 3;
            this.lblNombre.Text = "📝 Nombre del Diagnóstico *:";
            this.lblNombre.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtNombre
            // 
            this.txtNombre.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtNombre.BackColor = System.Drawing.Color.White;
            this.txtNombre.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tableLayoutDiagnostico.SetColumnSpan(this.txtNombre, 4);
            this.txtNombre.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtNombre.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.txtNombre.Location = new System.Drawing.Point(3, 56);
            this.txtNombre.Margin = new System.Windows.Forms.Padding(3, 2, 10, 2);
            this.txtNombre.MaxLength = 200;
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.Size = new System.Drawing.Size(1000, 32);
            this.txtNombre.TabIndex = 4;
            // 
            // chkRequiereEquipamiento
            // 
            this.chkRequiereEquipamiento.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.chkRequiereEquipamiento.AutoSize = true;
            this.chkRequiereEquipamiento.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.chkRequiereEquipamiento.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(89)))), ((int)(((byte)(182)))));
            this.chkRequiereEquipamiento.Location = new System.Drawing.Point(3, 106);
            this.chkRequiereEquipamiento.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.chkRequiereEquipamiento.Name = "chkRequiereEquipamiento";
            this.chkRequiereEquipamiento.Size = new System.Drawing.Size(168, 27);
            this.chkRequiereEquipamiento.TabIndex = 5;
            this.chkRequiereEquipamiento.Text = "🔧 Requiere Equipamiento";
            this.chkRequiereEquipamiento.UseVisualStyleBackColor = true;
            // 
            // lblCategoria
            // 
            this.lblCategoria.AutoSize = true;
            this.lblCategoria.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCategoria.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblCategoria.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.lblCategoria.Location = new System.Drawing.Point(177, 96);
            this.lblCategoria.Name = "lblCategoria";
            this.lblCategoria.Size = new System.Drawing.Size(226, 48);
            this.lblCategoria.TabIndex = 6;
            this.lblCategoria.Text = "📋 Categoría:";
            this.lblCategoria.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cmbCategoria
            // 
            this.cmbCategoria.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbCategoria.BackColor = System.Drawing.Color.White;
            this.cmbCategoria.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.cmbCategoria.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.cmbCategoria.FormattingEnabled = true;
            this.cmbCategoria.Location = new System.Drawing.Point(409, 103);
            this.cmbCategoria.Margin = new System.Windows.Forms.Padding(3, 2, 10, 2);
            this.cmbCategoria.Name = "cmbCategoria";
            this.cmbCategoria.Size = new System.Drawing.Size(103, 33);
            this.cmbCategoria.TabIndex = 7;
            // 
            // btnNuevaCategoria
            // 
            this.btnNuevaCategoria.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNuevaCategoria.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.btnNuevaCategoria.FlatAppearance.BorderSize = 0;
            this.btnNuevaCategoria.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNuevaCategoria.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnNuevaCategoria.ForeColor = System.Drawing.Color.White;
            this.btnNuevaCategoria.Location = new System.Drawing.Point(525, 102);
            this.btnNuevaCategoria.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnNuevaCategoria.Name = "btnNuevaCategoria";
            this.btnNuevaCategoria.Size = new System.Drawing.Size(485, 36);
            this.btnNuevaCategoria.TabIndex = 8;
            this.btnNuevaCategoria.Text = "➕ Nueva Categoría";
            this.btnNuevaCategoria.UseVisualStyleBackColor = false;
            // 
            // lblPrecioBase
            // 
            this.lblPrecioBase.AutoSize = true;
            this.lblPrecioBase.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblPrecioBase.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblPrecioBase.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(126)))), ((int)(((byte)(34)))));
            this.lblPrecioBase.Location = new System.Drawing.Point(3, 144);
            this.lblPrecioBase.Name = "lblPrecioBase";
            this.lblPrecioBase.Size = new System.Drawing.Size(168, 32);
            this.lblPrecioBase.TabIndex = 9;
            this.lblPrecioBase.Text = "💰 Precio Base *:";
            this.lblPrecioBase.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // nudPrecioBase
            // 
            this.nudPrecioBase.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.nudPrecioBase.BackColor = System.Drawing.Color.White;
            this.nudPrecioBase.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.nudPrecioBase.DecimalPlaces = 2;
            this.nudPrecioBase.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.nudPrecioBase.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.nudPrecioBase.Location = new System.Drawing.Point(177, 146);
            this.nudPrecioBase.Margin = new System.Windows.Forms.Padding(3, 2, 10, 2);
            this.nudPrecioBase.Maximum = new decimal(new int[] {
            999999,
            0,
            0,
            131072});
            this.nudPrecioBase.Name = "nudPrecioBase";
            this.nudPrecioBase.Size = new System.Drawing.Size(219, 32);
            this.nudPrecioBase.TabIndex = 10;
            this.nudPrecioBase.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.nudPrecioBase.ThousandsSeparator = true;
            // 
            // chkActivo
            // 
            this.chkActivo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.chkActivo.AutoSize = true;
            this.chkActivo.Checked = true;
            this.chkActivo.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkActivo.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.chkActivo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.chkActivo.Location = new System.Drawing.Point(409, 146);
            this.chkActivo.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.chkActivo.Name = "chkActivo";
            this.chkActivo.Size = new System.Drawing.Size(110, 27);
            this.chkActivo.TabIndex = 11;
            this.chkActivo.Text = "✅ Diagnóstico Activo";
            this.chkActivo.UseVisualStyleBackColor = true;
            // 
            // lblDescripcion
            // 
            this.lblDescripcion.AutoSize = true;
            this.lblDescripcion.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblDescripcion.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblDescripcion.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.lblDescripcion.Location = new System.Drawing.Point(525, 144);
            this.lblDescripcion.Name = "lblDescripcion";
            this.lblDescripcion.Size = new System.Drawing.Size(485, 32);
            this.lblDescripcion.TabIndex = 12;
            this.lblDescripcion.Text = "📄 Descripción:";
            this.lblDescripcion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtDescripcion
            // 
            this.txtDescripcion.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDescripcion.BackColor = System.Drawing.Color.White;
            this.txtDescripcion.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tableLayoutDiagnostico.SetColumnSpan(this.txtDescripcion, 4);
            this.txtDescripcion.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtDescripcion.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.txtDescripcion.Location = new System.Drawing.Point(3, 178);
            this.txtDescripcion.Margin = new System.Windows.Forms.Padding(3, 2, 10, 2);
            this.txtDescripcion.Multiline = true;
            this.txtDescripcion.Name = "txtDescripcion";
            this.txtDescripcion.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtDescripcion.Size = new System.Drawing.Size(1000, 98);
            this.txtDescripcion.TabIndex = 13;
            // 
            // btnVerPorCategoria
            // 
            this.btnVerPorCategoria.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(89)))), ((int)(((byte)(182)))));
            this.btnVerPorCategoria.FlatAppearance.BorderSize = 0;
            this.btnVerPorCategoria.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnVerPorCategoria.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnVerPorCategoria.ForeColor = System.Drawing.Color.White;
            this.btnVerPorCategoria.Location = new System.Drawing.Point(778, 4);
            this.btnVerPorCategoria.Name = "btnVerPorCategoria";
            this.btnVerPorCategoria.Size = new System.Drawing.Size(140, 31);
            this.btnVerPorCategoria.TabIndex = 9;
            this.btnVerPorCategoria.Text = "📋 Ver por Categoría";
            this.btnVerPorCategoria.UseVisualStyleBackColor = false;
            // 
            // ConsultaModule
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.Name = "ConsultaModule";
            this.tabControlPrincipal.ResumeLayout(false);
            this.tabInicio.ResumeLayout(false);
            this.tabConfiguraciones.ResumeLayout(false);
            this.panelBusqueda.ResumeLayout(false);
            this.panelBusqueda.PerformLayout();
            this.panelFormulario.ResumeLayout(false);
            this.panelSuperior.ResumeLayout(false);
            this.panelSuperior.PerformLayout();
            this.panelBotones.ResumeLayout(false);
            this.grpDatosDiagnostico.ResumeLayout(false);
            this.tableLayoutDiagnostico.ResumeLayout(false);
            this.tableLayoutDiagnostico.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudPrecioBase)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
    }
}
