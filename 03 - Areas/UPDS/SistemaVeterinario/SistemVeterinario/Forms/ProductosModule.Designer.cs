namespace SistemVeterinario.Forms
{
    partial class ProductosModule
    {
        /// <summary> 
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        // Controles específicos de productos
        private System.Windows.Forms.Label lblContador;
        private System.Windows.Forms.ComboBox cmbCategoriaFiltro;
        private System.Windows.Forms.Label lblCategoriaFiltro;
        private System.Windows.Forms.GroupBox grpDatosProducto;
        private System.Windows.Forms.TableLayoutPanel tableLayoutProducto;
        private System.Windows.Forms.Label lblCodigo;
        private System.Windows.Forms.TextBox txtCodigo;
        private System.Windows.Forms.Button btnGenerarCodigo;
        private System.Windows.Forms.Label lblNombre;
        private System.Windows.Forms.TextBox txtNombre;
        private System.Windows.Forms.Label lblCategoria;
        private System.Windows.Forms.ComboBox cmbCategoria;
        private System.Windows.Forms.Button btnNuevaCategoria;
        private System.Windows.Forms.Label lblPrecio;
        private System.Windows.Forms.NumericUpDown nudPrecio;
        private System.Windows.Forms.Label lblStockMinimo;
        private System.Windows.Forms.NumericUpDown nudStockMinimo;
        private System.Windows.Forms.Label lblStockActual;
        private System.Windows.Forms.NumericUpDown nudStockActual;
        private System.Windows.Forms.CheckBox chkRequiereReceta;
        private System.Windows.Forms.Label lblDescripcion;
        private System.Windows.Forms.TextBox txtDescripcion;
        private System.Windows.Forms.Button btnStockBajo;

        // Controles para gestión de categorías
        private System.Windows.Forms.GroupBox grpGestionCategorias;
        private System.Windows.Forms.DataGridView dgvCategorias;
        private System.Windows.Forms.GroupBox grpFormCategoria;
        private System.Windows.Forms.Label lblNombreCategoria;
        private System.Windows.Forms.TextBox txtNombreCategoria;
        private System.Windows.Forms.Label lblDescripcionCategoria;
        private System.Windows.Forms.TextBox txtDescripcionCategoria;
        private System.Windows.Forms.Button btnNuevaConfigCat;
        private System.Windows.Forms.Button btnGuardarCategoria;
        private System.Windows.Forms.Button btnEditarCategoria;
        private System.Windows.Forms.Button btnEliminarCategoria;
        private System.Windows.Forms.Button btnCancelarCategoria;
        private System.Windows.Forms.Button btnInicializarCategorias;

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
            this.grpDatosProducto = new System.Windows.Forms.GroupBox();
            this.tableLayoutProducto = new System.Windows.Forms.TableLayoutPanel();
            this.lblCodigo = new System.Windows.Forms.Label();
            this.txtCodigo = new System.Windows.Forms.TextBox();
            this.btnGenerarCodigo = new System.Windows.Forms.Button();
            this.lblNombre = new System.Windows.Forms.Label();
            this.txtNombre = new System.Windows.Forms.TextBox();
            this.lblCategoria = new System.Windows.Forms.Label();
            this.cmbCategoria = new System.Windows.Forms.ComboBox();
            this.btnNuevaCategoria = new System.Windows.Forms.Button();
            this.lblPrecio = new System.Windows.Forms.Label();
            this.nudPrecio = new System.Windows.Forms.NumericUpDown();
            this.lblStockMinimo = new System.Windows.Forms.Label();
            this.nudStockMinimo = new System.Windows.Forms.NumericUpDown();
            this.lblStockActual = new System.Windows.Forms.Label();
            this.nudStockActual = new System.Windows.Forms.NumericUpDown();
            this.chkRequiereReceta = new System.Windows.Forms.CheckBox();
            this.lblDescripcion = new System.Windows.Forms.Label();
            this.txtDescripcion = new System.Windows.Forms.TextBox();
            this.btnStockBajo = new System.Windows.Forms.Button();
            this.grpGestionCategorias = new System.Windows.Forms.GroupBox();
            this.dgvCategorias = new System.Windows.Forms.DataGridView();
            this.grpFormCategoria = new System.Windows.Forms.GroupBox();
            this.lblNombreCategoria = new System.Windows.Forms.Label();
            this.txtNombreCategoria = new System.Windows.Forms.TextBox();
            this.lblDescripcionCategoria = new System.Windows.Forms.Label();
            this.txtDescripcionCategoria = new System.Windows.Forms.TextBox();
            this.btnGuardarCategoria = new System.Windows.Forms.Button();
            this.btnCancelarCategoria = new System.Windows.Forms.Button();
            this.btnNuevaConfigCat = new System.Windows.Forms.Button();
            this.btnEditarCategoria = new System.Windows.Forms.Button();
            this.btnEliminarCategoria = new System.Windows.Forms.Button();
            this.btnInicializarCategorias = new System.Windows.Forms.Button();
            this.tabControlPrincipal.SuspendLayout();
            this.tabInicio.SuspendLayout();
            this.tabConfiguraciones.SuspendLayout();
            this.panelBusqueda.SuspendLayout();
            this.panelFormulario.SuspendLayout();
            this.panelSuperior.SuspendLayout();
            this.panelBotones.SuspendLayout();
            this.grpDatosProducto.SuspendLayout();
            this.tableLayoutProducto.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudPrecio)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudStockMinimo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudStockActual)).BeginInit();
            this.grpGestionCategorias.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCategorias)).BeginInit();
            this.grpFormCategoria.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControlPrincipal
            // 
            this.tabControlPrincipal.Margin = new System.Windows.Forms.Padding(2);
            // 
            // tabInicio
            // 
            this.tabInicio.Location = new System.Drawing.Point(4, 39);
            this.tabInicio.Margin = new System.Windows.Forms.Padding(2);
            this.tabInicio.Padding = new System.Windows.Forms.Padding(7);
            this.tabInicio.Size = new System.Drawing.Size(1135, 597);
            this.tabInicio.Text = "Gestión de Productos";
            // 
            // tabConfiguraciones
            // 
            this.tabConfiguraciones.Location = new System.Drawing.Point(4, 39);
            this.tabConfiguraciones.Margin = new System.Windows.Forms.Padding(2);
            this.tabConfiguraciones.Padding = new System.Windows.Forms.Padding(7);
            this.tabConfiguraciones.Size = new System.Drawing.Size(1135, 597);
            this.tabConfiguraciones.Text = "Configuración de Producto";
            // 
            // panelBusqueda
            // 
            this.panelBusqueda.Controls.Add(this.btnStockBajo);
            this.panelBusqueda.Controls.Add(this.cmbCategoriaFiltro);
            this.panelBusqueda.Controls.Add(this.lblCategoriaFiltro);
            this.panelBusqueda.Controls.Add(this.lblContador);
            this.panelBusqueda.Margin = new System.Windows.Forms.Padding(2);
            this.panelBusqueda.Padding = new System.Windows.Forms.Padding(11, 12, 11, 12);
            this.panelBusqueda.Size = new System.Drawing.Size(1121, 98);
            this.panelBusqueda.Controls.SetChildIndex(this.lblContador, 0);
            this.panelBusqueda.Controls.SetChildIndex(this.lblCategoriaFiltro, 0);
            this.panelBusqueda.Controls.SetChildIndex(this.cmbCategoriaFiltro, 0);
            this.panelBusqueda.Controls.SetChildIndex(this.btnStockBajo, 0);
            this.panelBusqueda.Controls.SetChildIndex(this.chkMostrarTodo, 0);
            this.panelBusqueda.Controls.SetChildIndex(this.btnNuevo, 0);
            this.panelBusqueda.Controls.SetChildIndex(this.btnBuscar, 0);
            this.panelBusqueda.Controls.SetChildIndex(this.txtBuscar, 0);
            // 
            // txtBuscar
            // 
            this.txtBuscar.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtBuscar.Location = new System.Drawing.Point(27, 18);
            this.txtBuscar.Margin = new System.Windows.Forms.Padding(2);
            this.txtBuscar.Size = new System.Drawing.Size(338, 30);
            // 
            // btnBuscar
            // 
            this.btnBuscar.FlatAppearance.BorderSize = 0;
            this.btnBuscar.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnBuscar.Location = new System.Drawing.Point(384, 13);
            this.btnBuscar.Margin = new System.Windows.Forms.Padding(2);
            this.btnBuscar.Size = new System.Drawing.Size(104, 38);
            // 
            // btnNuevo
            // 
            this.btnNuevo.FlatAppearance.BorderSize = 0;
            this.btnNuevo.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnNuevo.Location = new System.Drawing.Point(951, 11);
            this.btnNuevo.Margin = new System.Windows.Forms.Padding(2);
            this.btnNuevo.Size = new System.Drawing.Size(122, 38);
            // 
            // chkMostrarTodo
            // 
            this.chkMostrarTodo.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.chkMostrarTodo.Location = new System.Drawing.Point(384, 59);
            this.chkMostrarTodo.Margin = new System.Windows.Forms.Padding(2);
            this.chkMostrarTodo.Size = new System.Drawing.Size(237, 24);
            // 
            // panelFormulario
            // 
            this.panelFormulario.Controls.Add(this.grpDatosProducto);
            this.panelFormulario.Location = new System.Drawing.Point(7, 7);
            this.panelFormulario.Margin = new System.Windows.Forms.Padding(2);
            this.panelFormulario.Padding = new System.Windows.Forms.Padding(8);
            this.panelFormulario.Size = new System.Drawing.Size(1121, 583);
            this.panelFormulario.Controls.SetChildIndex(this.grpDatosProducto, 0);
            this.panelFormulario.Controls.SetChildIndex(this.panelSuperior, 0);
            this.panelFormulario.Controls.SetChildIndex(this.panelBotones, 0);
            // 
            // panelSuperior
            // 
            this.panelSuperior.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(250)))));
            this.panelSuperior.Margin = new System.Windows.Forms.Padding(2);
            this.panelSuperior.Padding = new System.Windows.Forms.Padding(7);
            this.panelSuperior.Size = new System.Drawing.Size(1105, 49);
            // 
            // lblModo
            // 
            this.lblModo.Location = new System.Drawing.Point(42, 12);
            this.lblModo.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            // 
            // cmbModo
            // 
            this.cmbModo.Location = new System.Drawing.Point(138, 11);
            this.cmbModo.Margin = new System.Windows.Forms.Padding(2);
            // 
            // lblId
            // 
            this.lblId.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblId.Location = new System.Drawing.Point(336, 15);
            this.lblId.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblId.Size = new System.Drawing.Size(33, 23);
            // 
            // txtId
            // 
            this.txtId.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(240)))), ((int)(((byte)(241)))));
            this.txtId.Location = new System.Drawing.Point(406, 11);
            this.txtId.Margin = new System.Windows.Forms.Padding(2);
            this.txtId.Size = new System.Drawing.Size(76, 30);
            // 
            // panelBotones
            // 
            this.panelBotones.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(250)))));
            this.panelBotones.Location = new System.Drawing.Point(8, 525);
            this.panelBotones.Margin = new System.Windows.Forms.Padding(2);
            this.panelBotones.Padding = new System.Windows.Forms.Padding(7);
            this.panelBotones.Size = new System.Drawing.Size(1105, 50);
            this.panelBotones.TabIndex = 0;
            this.panelBotones.Tag = "EditableButtonPanel";
            // 
            // btnGuardar
            // 
            this.btnGuardar.FlatAppearance.BorderSize = 0;
            this.btnGuardar.Location = new System.Drawing.Point(993, 13);
            this.btnGuardar.Margin = new System.Windows.Forms.Padding(2);
            // 
            // btnCancelar
            // 
            this.btnCancelar.FlatAppearance.BorderSize = 0;
            this.btnCancelar.Location = new System.Drawing.Point(877, 13);
            this.btnCancelar.Margin = new System.Windows.Forms.Padding(2);
            // 
            // btnEliminar
            // 
            this.btnEliminar.FlatAppearance.BorderSize = 0;
            // 
            // lblContador
            // 
            this.lblContador.AutoSize = true;
            this.lblContador.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblContador.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.lblContador.Location = new System.Drawing.Point(27, 55);
            this.lblContador.Name = "lblContador";
            this.lblContador.Size = new System.Drawing.Size(169, 23);
            this.lblContador.TabIndex = 0;
            this.lblContador.Text = "Total de registros: 0";
            // 
            // cmbCategoriaFiltro
            // 
            this.cmbCategoriaFiltro.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCategoriaFiltro.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbCategoriaFiltro.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbCategoriaFiltro.FormattingEnabled = true;
            this.cmbCategoriaFiltro.Location = new System.Drawing.Point(686, 47);
            this.cmbCategoriaFiltro.Name = "cmbCategoriaFiltro";
            this.cmbCategoriaFiltro.Size = new System.Drawing.Size(180, 31);
            this.cmbCategoriaFiltro.TabIndex = 2;
            // 
            // lblCategoriaFiltro
            // 
            this.lblCategoriaFiltro.AutoSize = true;
            this.lblCategoriaFiltro.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblCategoriaFiltro.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.lblCategoriaFiltro.Location = new System.Drawing.Point(739, 18);
            this.lblCategoriaFiltro.Name = "lblCategoriaFiltro";
            this.lblCategoriaFiltro.Size = new System.Drawing.Size(127, 23);
            this.lblCategoriaFiltro.TabIndex = 1;
            this.lblCategoriaFiltro.Text = "📂 Filtrar por:";
            // 
            // grpDatosProducto
            // 
            this.grpDatosProducto.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpDatosProducto.Controls.Add(this.tableLayoutProducto);
            this.grpDatosProducto.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.grpDatosProducto.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.grpDatosProducto.Location = new System.Drawing.Point(8, 65);
            this.grpDatosProducto.Name = "grpDatosProducto";
            this.grpDatosProducto.Padding = new System.Windows.Forms.Padding(3, 8, 3, 3);
            this.grpDatosProducto.Size = new System.Drawing.Size(1105, 452);
            this.grpDatosProducto.TabIndex = 1;
            this.grpDatosProducto.TabStop = false;
            this.grpDatosProducto.Text = "📦 Información del Producto";
            // 
            // tableLayoutProducto
            // 
            this.tableLayoutProducto.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutProducto.BackColor = System.Drawing.Color.White;
            this.tableLayoutProducto.ColumnCount = 4;
            this.tableLayoutProducto.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tableLayoutProducto.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutProducto.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tableLayoutProducto.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutProducto.Controls.Add(this.lblCodigo, 0, 0);
            this.tableLayoutProducto.Controls.Add(this.txtCodigo, 1, 0);
            this.tableLayoutProducto.Controls.Add(this.btnGenerarCodigo, 2, 0);
            this.tableLayoutProducto.Controls.Add(this.lblNombre, 0, 1);
            this.tableLayoutProducto.Controls.Add(this.txtNombre, 1, 1);
            this.tableLayoutProducto.Controls.Add(this.lblCategoria, 0, 2);
            this.tableLayoutProducto.Controls.Add(this.cmbCategoria, 1, 2);
            this.tableLayoutProducto.Controls.Add(this.btnNuevaCategoria, 2, 2);
            this.tableLayoutProducto.Controls.Add(this.lblPrecio, 0, 3);
            this.tableLayoutProducto.Controls.Add(this.nudPrecio, 1, 3);
            this.tableLayoutProducto.Controls.Add(this.lblStockMinimo, 2, 3);
            this.tableLayoutProducto.Controls.Add(this.nudStockMinimo, 3, 3);
            this.tableLayoutProducto.Controls.Add(this.lblStockActual, 0, 4);
            this.tableLayoutProducto.Controls.Add(this.nudStockActual, 1, 4);
            this.tableLayoutProducto.Controls.Add(this.chkRequiereReceta, 2, 4);
            this.tableLayoutProducto.Controls.Add(this.lblDescripcion, 0, 5);
            this.tableLayoutProducto.Controls.Add(this.txtDescripcion, 1, 5);
            this.tableLayoutProducto.Location = new System.Drawing.Point(15, 35);
            this.tableLayoutProducto.Name = "tableLayoutProducto";
            this.tableLayoutProducto.Padding = new System.Windows.Forms.Padding(20);
            this.tableLayoutProducto.RowCount = 6;
            this.tableLayoutProducto.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 45F));
            this.tableLayoutProducto.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 45F));
            this.tableLayoutProducto.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 45F));
            this.tableLayoutProducto.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 45F));
            this.tableLayoutProducto.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 45F));
            this.tableLayoutProducto.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutProducto.Size = new System.Drawing.Size(1075, 409);
            this.tableLayoutProducto.TabIndex = 0;
            // 
            // lblCodigo
            // 
            this.lblCodigo.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblCodigo.AutoSize = true;
            this.lblCodigo.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblCodigo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(76)))), ((int)(((byte)(60)))));
            this.lblCodigo.Location = new System.Drawing.Point(23, 31);
            this.lblCodigo.Name = "lblCodigo";
            this.lblCodigo.Size = new System.Drawing.Size(97, 23);
            this.lblCodigo.TabIndex = 0;
            this.lblCodigo.Text = "🔖 Código";
            // 
            // txtCodigo
            // 
            this.txtCodigo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCodigo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCodigo.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtCodigo.Location = new System.Drawing.Point(143, 27);
            this.txtCodigo.MaxLength = 50;
            this.txtCodigo.Name = "txtCodigo";
            this.txtCodigo.Size = new System.Drawing.Size(391, 30);
            this.txtCodigo.TabIndex = 1;
            // 
            // btnGenerarCodigo
            // 
            this.btnGenerarCodigo.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnGenerarCodigo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.btnGenerarCodigo.FlatAppearance.BorderSize = 0;
            this.btnGenerarCodigo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGenerarCodigo.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnGenerarCodigo.ForeColor = System.Drawing.Color.White;
            this.btnGenerarCodigo.Location = new System.Drawing.Point(540, 27);
            this.btnGenerarCodigo.Name = "btnGenerarCodigo";
            this.btnGenerarCodigo.Size = new System.Drawing.Size(85, 30);
            this.btnGenerarCodigo.TabIndex = 2;
            this.btnGenerarCodigo.Text = "📋 Generar";
            this.btnGenerarCodigo.UseVisualStyleBackColor = false;
            // 
            // lblNombre
            // 
            this.lblNombre.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblNombre.AutoSize = true;
            this.lblNombre.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblNombre.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(76)))), ((int)(((byte)(60)))));
            this.lblNombre.Location = new System.Drawing.Point(23, 76);
            this.lblNombre.Name = "lblNombre";
            this.lblNombre.Size = new System.Drawing.Size(105, 23);
            this.lblNombre.TabIndex = 3;
            this.lblNombre.Text = "📝 Nombre";
            // 
            // txtNombre
            // 
            this.txtNombre.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtNombre.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tableLayoutProducto.SetColumnSpan(this.txtNombre, 3);
            this.txtNombre.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtNombre.Location = new System.Drawing.Point(143, 72);
            this.txtNombre.MaxLength = 255;
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.Size = new System.Drawing.Size(909, 30);
            this.txtNombre.TabIndex = 4;
            // 
            // lblCategoria
            // 
            this.lblCategoria.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblCategoria.AutoSize = true;
            this.lblCategoria.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblCategoria.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(76)))), ((int)(((byte)(60)))));
            this.lblCategoria.Location = new System.Drawing.Point(23, 110);
            this.lblCategoria.Name = "lblCategoria";
            this.lblCategoria.Size = new System.Drawing.Size(88, 45);
            this.lblCategoria.TabIndex = 5;
            this.lblCategoria.Text = "🗂️ Categoría";
            // 
            // cmbCategoria
            // 
            this.cmbCategoria.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbCategoria.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCategoria.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbCategoria.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbCategoria.FormattingEnabled = true;
            this.cmbCategoria.Location = new System.Drawing.Point(143, 117);
            this.cmbCategoria.Name = "cmbCategoria";
            this.cmbCategoria.Size = new System.Drawing.Size(391, 31);
            this.cmbCategoria.TabIndex = 6;
            // 
            // btnNuevaCategoria
            // 
            this.btnNuevaCategoria.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnNuevaCategoria.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.btnNuevaCategoria.FlatAppearance.BorderSize = 0;
            this.btnNuevaCategoria.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNuevaCategoria.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnNuevaCategoria.ForeColor = System.Drawing.Color.White;
            this.btnNuevaCategoria.Location = new System.Drawing.Point(540, 117);
            this.btnNuevaCategoria.Name = "btnNuevaCategoria";
            this.btnNuevaCategoria.Size = new System.Drawing.Size(85, 30);
            this.btnNuevaCategoria.TabIndex = 7;
            this.btnNuevaCategoria.Text = "➕ Nueva";
            this.btnNuevaCategoria.UseVisualStyleBackColor = false;
            // 
            // lblPrecio
            // 
            this.lblPrecio.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblPrecio.AutoSize = true;
            this.lblPrecio.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblPrecio.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(76)))), ((int)(((byte)(60)))));
            this.lblPrecio.Location = new System.Drawing.Point(23, 166);
            this.lblPrecio.Name = "lblPrecio";
            this.lblPrecio.Size = new System.Drawing.Size(88, 23);
            this.lblPrecio.TabIndex = 8;
            this.lblPrecio.Text = "💰 Precio";
            // 
            // nudPrecio
            // 
            this.nudPrecio.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.nudPrecio.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.nudPrecio.DecimalPlaces = 2;
            this.nudPrecio.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.nudPrecio.Location = new System.Drawing.Point(143, 162);
            this.nudPrecio.Maximum = new decimal(new int[] {
            999999,
            0,
            0,
            131072});
            this.nudPrecio.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.nudPrecio.Name = "nudPrecio";
            this.nudPrecio.Size = new System.Drawing.Size(391, 30);
            this.nudPrecio.TabIndex = 9;
            this.nudPrecio.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.nudPrecio.ThousandsSeparator = true;
            this.nudPrecio.Value = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            // 
            // lblStockMinimo
            // 
            this.lblStockMinimo.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblStockMinimo.AutoSize = true;
            this.lblStockMinimo.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblStockMinimo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(76)))), ((int)(((byte)(60)))));
            this.lblStockMinimo.Location = new System.Drawing.Point(540, 155);
            this.lblStockMinimo.Name = "lblStockMinimo";
            this.lblStockMinimo.Size = new System.Drawing.Size(89, 45);
            this.lblStockMinimo.TabIndex = 10;
            this.lblStockMinimo.Text = "⚠️ Stock Mínimo";
            // 
            // nudStockMinimo
            // 
            this.nudStockMinimo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.nudStockMinimo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.nudStockMinimo.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.nudStockMinimo.Location = new System.Drawing.Point(660, 162);
            this.nudStockMinimo.Maximum = new decimal(new int[] {
            99999,
            0,
            0,
            0});
            this.nudStockMinimo.Name = "nudStockMinimo";
            this.nudStockMinimo.Size = new System.Drawing.Size(392, 30);
            this.nudStockMinimo.TabIndex = 11;
            this.nudStockMinimo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.nudStockMinimo.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // lblStockActual
            // 
            this.lblStockActual.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblStockActual.AutoSize = true;
            this.lblStockActual.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblStockActual.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(76)))), ((int)(((byte)(60)))));
            this.lblStockActual.Location = new System.Drawing.Point(23, 200);
            this.lblStockActual.Name = "lblStockActual";
            this.lblStockActual.Size = new System.Drawing.Size(89, 45);
            this.lblStockActual.TabIndex = 12;
            this.lblStockActual.Text = "📦 Stock Actual";
            // 
            // nudStockActual
            // 
            this.nudStockActual.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.nudStockActual.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.nudStockActual.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.nudStockActual.Location = new System.Drawing.Point(143, 207);
            this.nudStockActual.Maximum = new decimal(new int[] {
            99999,
            0,
            0,
            0});
            this.nudStockActual.Name = "nudStockActual";
            this.nudStockActual.Size = new System.Drawing.Size(391, 30);
            this.nudStockActual.TabIndex = 13;
            this.nudStockActual.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // chkRequiereReceta
            // 
            this.chkRequiereReceta.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.chkRequiereReceta.AutoSize = true;
            this.chkRequiereReceta.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.tableLayoutProducto.SetColumnSpan(this.chkRequiereReceta, 2);
            this.chkRequiereReceta.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.chkRequiereReceta.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.chkRequiereReceta.Location = new System.Drawing.Point(540, 209);
            this.chkRequiereReceta.Name = "chkRequiereReceta";
            this.chkRequiereReceta.Size = new System.Drawing.Size(190, 27);
            this.chkRequiereReceta.TabIndex = 14;
            this.chkRequiereReceta.Text = "🏥 Requiere Receta";
            this.chkRequiereReceta.UseVisualStyleBackColor = true;
            // 
            // lblDescripcion
            // 
            this.lblDescripcion.AutoSize = true;
            this.lblDescripcion.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblDescripcion.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(76)))), ((int)(((byte)(60)))));
            this.lblDescripcion.Location = new System.Drawing.Point(23, 245);
            this.lblDescripcion.Name = "lblDescripcion";
            this.lblDescripcion.Size = new System.Drawing.Size(103, 46);
            this.lblDescripcion.TabIndex = 15;
            this.lblDescripcion.Text = "📄 Descripción";
            // 
            // txtDescripcion
            // 
            this.txtDescripcion.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDescripcion.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tableLayoutProducto.SetColumnSpan(this.txtDescripcion, 3);
            this.txtDescripcion.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtDescripcion.Location = new System.Drawing.Point(143, 248);
            this.txtDescripcion.Multiline = true;
            this.txtDescripcion.Name = "txtDescripcion";
            this.txtDescripcion.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtDescripcion.Size = new System.Drawing.Size(909, 138);
            this.txtDescripcion.TabIndex = 16;
            // 
            // btnStockBajo
            // 
            this.btnStockBajo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(126)))), ((int)(((byte)(34)))));
            this.btnStockBajo.FlatAppearance.BorderSize = 0;
            this.btnStockBajo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStockBajo.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnStockBajo.ForeColor = System.Drawing.Color.White;
            this.btnStockBajo.Location = new System.Drawing.Point(908, 51);
            this.btnStockBajo.Name = "btnStockBajo";
            this.btnStockBajo.Size = new System.Drawing.Size(165, 32);
            this.btnStockBajo.TabIndex = 3;
            this.btnStockBajo.Text = "⚠️ Stock Bajo";
            this.btnStockBajo.UseVisualStyleBackColor = false;
            // 
            // grpGestionCategorias
            // 
            this.grpGestionCategorias.Controls.Add(this.dgvCategorias);
            this.grpGestionCategorias.Controls.Add(this.grpFormCategoria);
            this.grpGestionCategorias.Controls.Add(this.btnNuevaConfigCat);
            this.grpGestionCategorias.Controls.Add(this.btnEditarCategoria);
            this.grpGestionCategorias.Controls.Add(this.btnEliminarCategoria);
            this.grpGestionCategorias.Controls.Add(this.btnInicializarCategorias);
            this.grpGestionCategorias.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpGestionCategorias.Location = new System.Drawing.Point(3, 3);
            this.grpGestionCategorias.Name = "grpGestionCategorias";
            this.grpGestionCategorias.Size = new System.Drawing.Size(800, 400);
            this.grpGestionCategorias.TabIndex = 0;
            this.grpGestionCategorias.TabStop = false;
            this.grpGestionCategorias.Text = "Gestión de Categorías";
            // 
            // dgvCategorias
            // 
            this.dgvCategorias.AllowUserToAddRows = false;
            this.dgvCategorias.AllowUserToDeleteRows = false;
            this.dgvCategorias.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvCategorias.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCategorias.Location = new System.Drawing.Point(10, 25);
            this.dgvCategorias.Name = "dgvCategorias";
            this.dgvCategorias.ReadOnly = true;
            this.dgvCategorias.RowHeadersWidth = 51;
            this.dgvCategorias.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvCategorias.Size = new System.Drawing.Size(450, 200);
            this.dgvCategorias.TabIndex = 0;
            // 
            // grpFormCategoria
            // 
            this.grpFormCategoria.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpFormCategoria.Controls.Add(this.lblNombreCategoria);
            this.grpFormCategoria.Controls.Add(this.txtNombreCategoria);
            this.grpFormCategoria.Controls.Add(this.lblDescripcionCategoria);
            this.grpFormCategoria.Controls.Add(this.txtDescripcionCategoria);
            this.grpFormCategoria.Controls.Add(this.btnGuardarCategoria);
            this.grpFormCategoria.Controls.Add(this.btnCancelarCategoria);
            this.grpFormCategoria.Location = new System.Drawing.Point(470, 25);
            this.grpFormCategoria.Name = "grpFormCategoria";
            this.grpFormCategoria.Size = new System.Drawing.Size(320, 320);
            this.grpFormCategoria.TabIndex = 1;
            this.grpFormCategoria.TabStop = false;
            this.grpFormCategoria.Text = "Datos de Categoría";
            // 
            // lblNombreCategoria
            // 
            this.lblNombreCategoria.AutoSize = true;
            this.lblNombreCategoria.Location = new System.Drawing.Point(15, 30);
            this.lblNombreCategoria.Name = "lblNombreCategoria";
            this.lblNombreCategoria.Size = new System.Drawing.Size(59, 16);
            this.lblNombreCategoria.TabIndex = 0;
            this.lblNombreCategoria.Text = "Nombre:";
            // 
            // txtNombreCategoria
            // 
            this.txtNombreCategoria.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtNombreCategoria.Location = new System.Drawing.Point(15, 50);
            this.txtNombreCategoria.Name = "txtNombreCategoria";
            this.txtNombreCategoria.Size = new System.Drawing.Size(290, 22);
            this.txtNombreCategoria.TabIndex = 1;
            // 
            // lblDescripcionCategoria
            // 
            this.lblDescripcionCategoria.AutoSize = true;
            this.lblDescripcionCategoria.Location = new System.Drawing.Point(15, 85);
            this.lblDescripcionCategoria.Name = "lblDescripcionCategoria";
            this.lblDescripcionCategoria.Size = new System.Drawing.Size(82, 16);
            this.lblDescripcionCategoria.TabIndex = 2;
            this.lblDescripcionCategoria.Text = "Descripción:";
            // 
            // txtDescripcionCategoria
            // 
            this.txtDescripcionCategoria.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDescripcionCategoria.Location = new System.Drawing.Point(15, 105);
            this.txtDescripcionCategoria.Multiline = true;
            this.txtDescripcionCategoria.Name = "txtDescripcionCategoria";
            this.txtDescripcionCategoria.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtDescripcionCategoria.Size = new System.Drawing.Size(290, 150);
            this.txtDescripcionCategoria.TabIndex = 3;
            // 
            // btnGuardarCategoria
            // 
            this.btnGuardarCategoria.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGuardarCategoria.Location = new System.Drawing.Point(135, 280);
            this.btnGuardarCategoria.Name = "btnGuardarCategoria";
            this.btnGuardarCategoria.Size = new System.Drawing.Size(80, 30);
            this.btnGuardarCategoria.TabIndex = 4;
            this.btnGuardarCategoria.Text = "Guardar";
            this.btnGuardarCategoria.UseVisualStyleBackColor = true;
            // 
            // btnCancelarCategoria
            // 
            this.btnCancelarCategoria.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancelarCategoria.Location = new System.Drawing.Point(225, 280);
            this.btnCancelarCategoria.Name = "btnCancelarCategoria";
            this.btnCancelarCategoria.Size = new System.Drawing.Size(80, 30);
            this.btnCancelarCategoria.TabIndex = 5;
            this.btnCancelarCategoria.Text = "Cancelar";
            this.btnCancelarCategoria.UseVisualStyleBackColor = true;
            // 
            // btnNuevaConfigCat
            // 
            this.btnNuevaConfigCat.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnNuevaConfigCat.Location = new System.Drawing.Point(10, 355);
            this.btnNuevaConfigCat.Name = "btnNuevaConfigCat";
            this.btnNuevaConfigCat.Size = new System.Drawing.Size(80, 30);
            this.btnNuevaConfigCat.TabIndex = 2;
            this.btnNuevaConfigCat.Text = "Nueva";
            this.btnNuevaConfigCat.UseVisualStyleBackColor = true;
            // 
            // btnEditarCategoria
            // 
            this.btnEditarCategoria.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnEditarCategoria.Location = new System.Drawing.Point(100, 355);
            this.btnEditarCategoria.Name = "btnEditarCategoria";
            this.btnEditarCategoria.Size = new System.Drawing.Size(80, 30);
            this.btnEditarCategoria.TabIndex = 3;
            this.btnEditarCategoria.Text = "Editar";
            this.btnEditarCategoria.UseVisualStyleBackColor = true;
            // 
            // btnEliminarCategoria
            // 
            this.btnEliminarCategoria.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnEliminarCategoria.Location = new System.Drawing.Point(190, 355);
            this.btnEliminarCategoria.Name = "btnEliminarCategoria";
            this.btnEliminarCategoria.Size = new System.Drawing.Size(80, 30);
            this.btnEliminarCategoria.TabIndex = 4;
            this.btnEliminarCategoria.Text = "Eliminar";
            this.btnEliminarCategoria.UseVisualStyleBackColor = true;
            // 
            // btnInicializarCategorias
            // 
            this.btnInicializarCategorias.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnInicializarCategorias.Location = new System.Drawing.Point(650, 355);
            this.btnInicializarCategorias.Name = "btnInicializarCategorias";
            this.btnInicializarCategorias.Size = new System.Drawing.Size(140, 30);
            this.btnInicializarCategorias.TabIndex = 5;
            this.btnInicializarCategorias.Text = "Inicializar Categorías";
            this.btnInicializarCategorias.UseVisualStyleBackColor = true;
            // 
            // ProductosModule
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.Name = "ProductosModule";
            this.tabControlPrincipal.ResumeLayout(false);
            this.tabInicio.ResumeLayout(false);
            this.tabConfiguraciones.ResumeLayout(false);
            this.panelBusqueda.ResumeLayout(false);
            this.panelBusqueda.PerformLayout();
            this.panelFormulario.ResumeLayout(false);
            this.panelSuperior.ResumeLayout(false);
            this.panelSuperior.PerformLayout();
            this.panelBotones.ResumeLayout(false);
            this.grpDatosProducto.ResumeLayout(false);
            this.tableLayoutProducto.ResumeLayout(false);
            this.tableLayoutProducto.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudPrecio)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudStockMinimo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudStockActual)).EndInit();
            this.grpGestionCategorias.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCategorias)).EndInit();
            this.grpFormCategoria.ResumeLayout(false);
            this.grpFormCategoria.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
    }
}
