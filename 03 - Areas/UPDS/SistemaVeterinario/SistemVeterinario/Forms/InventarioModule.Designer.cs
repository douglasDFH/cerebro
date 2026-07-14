namespace SistemVeterinario.Forms
{
    partial class InventarioModule
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

        private new void InitializeComponent()
        {
            // Llamar primero al InitializeComponent de BaseModulos
            base.InitializeComponent();
            
            this.groupMovimientos = new System.Windows.Forms.GroupBox();
            this.btnRegistrarMovimiento = new System.Windows.Forms.Button();
            this.cmbUbicacion = new System.Windows.Forms.ComboBox();
            this.lblUbicacion = new System.Windows.Forms.Label();
            this.txtMotivoMovimiento = new System.Windows.Forms.TextBox();
            this.lblMotivoMovimiento = new System.Windows.Forms.Label();
            this.txtCantidadMovimiento = new System.Windows.Forms.TextBox();
            this.lblCantidadMovimiento = new System.Windows.Forms.Label();
            this.cmbTipoMovimiento = new System.Windows.Forms.ComboBox();
            this.lblTipoMovimiento = new System.Windows.Forms.Label();
            this.txtStockMaximo = new System.Windows.Forms.TextBox();
            this.lblStockMaximo = new System.Windows.Forms.Label();
            this.txtStockMinimo = new System.Windows.Forms.TextBox();
            this.lblStockMinimo = new System.Windows.Forms.Label();
            this.txtStockActual = new System.Windows.Forms.TextBox();
            this.lblStockActual = new System.Windows.Forms.Label();
            this.txtPrecio = new System.Windows.Forms.TextBox();
            this.lblPrecio = new System.Windows.Forms.Label();
            this.txtDescripcion = new System.Windows.Forms.TextBox();
            this.lblDescripcion = new System.Windows.Forms.Label();
            this.txtNombreProducto = new System.Windows.Forms.TextBox();
            this.lblNombreProducto = new System.Windows.Forms.Label();
            this.tabMovimientos = new System.Windows.Forms.TabPage();
            this.groupFiltroMovimientos = new System.Windows.Forms.GroupBox();
            this.btnVerMovimientos = new System.Windows.Forms.Button();
            this.dtpFechaFin = new System.Windows.Forms.DateTimePicker();
            this.lblFechaFin = new System.Windows.Forms.Label();
            this.dtpFechaInicio = new System.Windows.Forms.DateTimePicker();
            this.lblFechaInicio = new System.Windows.Forms.Label();
            this.dgvMovimientos = new System.Windows.Forms.DataGridView();
            this.tabAlertas = new System.Windows.Forms.TabPage();
            this.groupAlertas = new System.Windows.Forms.GroupBox();
            this.btnStockBajo = new System.Windows.Forms.Button();
            this.dgvStockBajo = new System.Windows.Forms.DataGridView();
            this.groupMovimientos.SuspendLayout();
            this.tabMovimientos.SuspendLayout();
            this.groupFiltroMovimientos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMovimientos)).BeginInit();
            this.tabAlertas.SuspendLayout();
            this.groupAlertas.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvStockBajo)).BeginInit();
            this.tabControlPrincipal.SuspendLayout();
            this.tabConfiguraciones.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControlPrincipal
            // 
            this.tabControlPrincipal.Controls.Add(this.tabMovimientos);
            this.tabControlPrincipal.Controls.Add(this.tabAlertas);
            this.tabControlPrincipal.Controls.SetChildIndex(this.tabInicio, 0);
            this.tabControlPrincipal.Controls.SetChildIndex(this.tabConfiguraciones, 0);
            this.tabControlPrincipal.Controls.SetChildIndex(this.tabMovimientos, 0);
            this.tabControlPrincipal.Controls.SetChildIndex(this.tabAlertas, 0);
            // 
            // tabConfiguraciones
            // 
            this.tabConfiguraciones.Controls.Add(this.groupMovimientos);
            this.tabConfiguraciones.Controls.Add(this.txtStockMaximo);
            this.tabConfiguraciones.Controls.Add(this.lblStockMaximo);
            this.tabConfiguraciones.Controls.Add(this.txtStockMinimo);
            this.tabConfiguraciones.Controls.Add(this.lblStockMinimo);
            this.tabConfiguraciones.Controls.Add(this.txtStockActual);
            this.tabConfiguraciones.Controls.Add(this.lblStockActual);
            this.tabConfiguraciones.Controls.Add(this.txtPrecio);
            this.tabConfiguraciones.Controls.Add(this.lblPrecio);
            this.tabConfiguraciones.Controls.Add(this.txtDescripcion);
            this.tabConfiguraciones.Controls.Add(this.lblDescripcion);
            this.tabConfiguraciones.Controls.Add(this.txtNombreProducto);
            this.tabConfiguraciones.Controls.Add(this.lblNombreProducto);
            this.tabConfiguraciones.Controls.SetChildIndex(this.lblNombreProducto, 0);
            this.tabConfiguraciones.Controls.SetChildIndex(this.txtNombreProducto, 0);
            this.tabConfiguraciones.Controls.SetChildIndex(this.lblDescripcion, 0);
            this.tabConfiguraciones.Controls.SetChildIndex(this.txtDescripcion, 0);
            this.tabConfiguraciones.Controls.SetChildIndex(this.lblPrecio, 0);
            this.tabConfiguraciones.Controls.SetChildIndex(this.txtPrecio, 0);
            this.tabConfiguraciones.Controls.SetChildIndex(this.lblStockActual, 0);
            this.tabConfiguraciones.Controls.SetChildIndex(this.txtStockActual, 0);
            this.tabConfiguraciones.Controls.SetChildIndex(this.lblStockMinimo, 0);
            this.tabConfiguraciones.Controls.SetChildIndex(this.txtStockMinimo, 0);
            this.tabConfiguraciones.Controls.SetChildIndex(this.lblStockMaximo, 0);
            this.tabConfiguraciones.Controls.SetChildIndex(this.txtStockMaximo, 0);
            this.tabConfiguraciones.Controls.SetChildIndex(this.groupMovimientos, 0);
            this.tabConfiguraciones.Controls.SetChildIndex(this.txtId, 0);
            this.tabConfiguraciones.Controls.SetChildIndex(this.lblId, 0);
            this.tabConfiguraciones.Controls.SetChildIndex(this.cmbModo, 0);
            this.tabConfiguraciones.Controls.SetChildIndex(this.lblModo, 0);
            this.tabConfiguraciones.Controls.SetChildIndex(this.btnGuardar, 0);
            this.tabConfiguraciones.Controls.SetChildIndex(this.btnCancelar, 0);
            this.tabConfiguraciones.Controls.SetChildIndex(this.btnEliminar, 0);
            // 
            // groupMovimientos
            // 
            this.groupMovimientos.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupMovimientos.Controls.Add(this.btnRegistrarMovimiento);
            this.groupMovimientos.Controls.Add(this.cmbUbicacion);
            this.groupMovimientos.Controls.Add(this.lblUbicacion);
            this.groupMovimientos.Controls.Add(this.txtMotivoMovimiento);
            this.groupMovimientos.Controls.Add(this.lblMotivoMovimiento);
            this.groupMovimientos.Controls.Add(this.txtCantidadMovimiento);
            this.groupMovimientos.Controls.Add(this.lblCantidadMovimiento);
            this.groupMovimientos.Controls.Add(this.cmbTipoMovimiento);
            this.groupMovimientos.Controls.Add(this.lblTipoMovimiento);
            this.groupMovimientos.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupMovimientos.Location = new System.Drawing.Point(450, 120);
            this.groupMovimientos.Name = "groupMovimientos";
            this.groupMovimientos.Size = new System.Drawing.Size(350, 280);
            this.groupMovimientos.TabIndex = 100;
            this.groupMovimientos.TabStop = false;
            this.groupMovimientos.Text = "Registro de Movimientos";
            // 
            // btnRegistrarMovimiento
            // 
            this.btnRegistrarMovimiento.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69)))));
            this.btnRegistrarMovimiento.FlatAppearance.BorderSize = 0;
            this.btnRegistrarMovimiento.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRegistrarMovimiento.ForeColor = System.Drawing.Color.White;
            this.btnRegistrarMovimiento.Location = new System.Drawing.Point(25, 230);
            this.btnRegistrarMovimiento.Name = "btnRegistrarMovimiento";
            this.btnRegistrarMovimiento.Size = new System.Drawing.Size(300, 35);
            this.btnRegistrarMovimiento.TabIndex = 8;
            this.btnRegistrarMovimiento.Text = "Registrar Movimiento";
            this.btnRegistrarMovimiento.UseVisualStyleBackColor = false;
            this.btnRegistrarMovimiento.Click += new System.EventHandler(this.BtnRegistrarMovimiento_Click);
            // 
            // cmbUbicacion
            // 
            this.cmbUbicacion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbUbicacion.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbUbicacion.FormattingEnabled = true;
            this.cmbUbicacion.Location = new System.Drawing.Point(115, 190);
            this.cmbUbicacion.Name = "cmbUbicacion";
            this.cmbUbicacion.Size = new System.Drawing.Size(210, 23);
            this.cmbUbicacion.TabIndex = 7;
            // 
            // lblUbicacion
            // 
            this.lblUbicacion.AutoSize = true;
            this.lblUbicacion.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUbicacion.Location = new System.Drawing.Point(25, 193);
            this.lblUbicacion.Name = "lblUbicacion";
            this.lblUbicacion.Size = new System.Drawing.Size(64, 15);
            this.lblUbicacion.TabIndex = 6;
            this.lblUbicacion.Text = "Ubicación:";
            // 
            // txtMotivoMovimiento
            // 
            this.txtMotivoMovimiento.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMotivoMovimiento.Location = new System.Drawing.Point(115, 120);
            this.txtMotivoMovimiento.Multiline = true;
            this.txtMotivoMovimiento.Name = "txtMotivoMovimiento";
            this.txtMotivoMovimiento.Size = new System.Drawing.Size(210, 60);
            this.txtMotivoMovimiento.TabIndex = 5;
            // 
            // lblMotivoMovimiento
            // 
            this.lblMotivoMovimiento.AutoSize = true;
            this.lblMotivoMovimiento.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMotivoMovimiento.Location = new System.Drawing.Point(25, 123);
            this.lblMotivoMovimiento.Name = "lblMotivoMovimiento";
            this.lblMotivoMovimiento.Size = new System.Drawing.Size(46, 15);
            this.lblMotivoMovimiento.TabIndex = 4;
            this.lblMotivoMovimiento.Text = "Motivo:";
            // 
            // txtCantidadMovimiento
            // 
            this.txtCantidadMovimiento.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCantidadMovimiento.Location = new System.Drawing.Point(115, 85);
            this.txtCantidadMovimiento.Name = "txtCantidadMovimiento";
            this.txtCantidadMovimiento.Size = new System.Drawing.Size(210, 21);
            this.txtCantidadMovimiento.TabIndex = 3;
            // 
            // lblCantidadMovimiento
            // 
            this.lblCantidadMovimiento.AutoSize = true;
            this.lblCantidadMovimiento.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCantidadMovimiento.Location = new System.Drawing.Point(25, 88);
            this.lblCantidadMovimiento.Name = "lblCantidadMovimiento";
            this.lblCantidadMovimiento.Size = new System.Drawing.Size(61, 15);
            this.lblCantidadMovimiento.TabIndex = 2;
            this.lblCantidadMovimiento.Text = "Cantidad:";
            // 
            // cmbTipoMovimiento
            // 
            this.cmbTipoMovimiento.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTipoMovimiento.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbTipoMovimiento.FormattingEnabled = true;
            this.cmbTipoMovimiento.Location = new System.Drawing.Point(115, 50);
            this.cmbTipoMovimiento.Name = "cmbTipoMovimiento";
            this.cmbTipoMovimiento.Size = new System.Drawing.Size(210, 23);
            this.cmbTipoMovimiento.TabIndex = 1;
            // 
            // lblTipoMovimiento
            // 
            this.lblTipoMovimiento.AutoSize = true;
            this.lblTipoMovimiento.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTipoMovimiento.Location = new System.Drawing.Point(25, 53);
            this.lblTipoMovimiento.Name = "lblTipoMovimiento";
            this.lblTipoMovimiento.Size = new System.Drawing.Size(35, 15);
            this.lblTipoMovimiento.TabIndex = 0;
            this.lblTipoMovimiento.Text = "Tipo:";
            // 
            // txtStockMaximo
            // 
            this.txtStockMaximo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtStockMaximo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtStockMaximo.Location = new System.Drawing.Point(140, 350);
            this.txtStockMaximo.Name = "txtStockMaximo";
            this.txtStockMaximo.Size = new System.Drawing.Size(250, 21);
            this.txtStockMaximo.TabIndex = 11;
            // 
            // lblStockMaximo
            // 
            this.lblStockMaximo.AutoSize = true;
            this.lblStockMaximo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStockMaximo.Location = new System.Drawing.Point(30, 353);
            this.lblStockMaximo.Name = "lblStockMaximo";
            this.lblStockMaximo.Size = new System.Drawing.Size(88, 15);
            this.lblStockMaximo.TabIndex = 10;
            this.lblStockMaximo.Text = "Stock Máximo:";
            // 
            // txtStockMinimo
            // 
            this.txtStockMinimo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtStockMinimo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtStockMinimo.Location = new System.Drawing.Point(140, 315);
            this.txtStockMinimo.Name = "txtStockMinimo";
            this.txtStockMinimo.Size = new System.Drawing.Size(250, 21);
            this.txtStockMinimo.TabIndex = 9;
            // 
            // lblStockMinimo
            // 
            this.lblStockMinimo.AutoSize = true;
            this.lblStockMinimo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStockMinimo.Location = new System.Drawing.Point(30, 318);
            this.lblStockMinimo.Name = "lblStockMinimo";
            this.lblStockMinimo.Size = new System.Drawing.Size(84, 15);
            this.lblStockMinimo.TabIndex = 8;
            this.lblStockMinimo.Text = "Stock Mínimo:";
            // 
            // txtStockActual
            // 
            this.txtStockActual.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtStockActual.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtStockActual.Location = new System.Drawing.Point(140, 280);
            this.txtStockActual.Name = "txtStockActual";
            this.txtStockActual.Size = new System.Drawing.Size(250, 21);
            this.txtStockActual.TabIndex = 7;
            // 
            // lblStockActual
            // 
            this.lblStockActual.AutoSize = true;
            this.lblStockActual.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStockActual.Location = new System.Drawing.Point(30, 283);
            this.lblStockActual.Name = "lblStockActual";
            this.lblStockActual.Size = new System.Drawing.Size(78, 15);
            this.lblStockActual.TabIndex = 6;
            this.lblStockActual.Text = "Stock Actual:";
            // 
            // txtPrecio
            // 
            this.txtPrecio.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPrecio.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPrecio.Location = new System.Drawing.Point(140, 245);
            this.txtPrecio.Name = "txtPrecio";
            this.txtPrecio.Size = new System.Drawing.Size(250, 21);
            this.txtPrecio.TabIndex = 5;
            // 
            // lblPrecio
            // 
            this.lblPrecio.AutoSize = true;
            this.lblPrecio.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPrecio.Location = new System.Drawing.Point(30, 248);
            this.lblPrecio.Name = "lblPrecio";
            this.lblPrecio.Size = new System.Drawing.Size(45, 15);
            this.lblPrecio.TabIndex = 4;
            this.lblPrecio.Text = "Precio:";
            // 
            // txtDescripcion
            // 
            this.txtDescripcion.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDescripcion.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDescripcion.Location = new System.Drawing.Point(140, 165);
            this.txtDescripcion.Multiline = true;
            this.txtDescripcion.Name = "txtDescripcion";
            this.txtDescripcion.Size = new System.Drawing.Size(250, 70);
            this.txtDescripcion.TabIndex = 3;
            // 
            // lblDescripcion
            // 
            this.lblDescripcion.AutoSize = true;
            this.lblDescripcion.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDescripcion.Location = new System.Drawing.Point(30, 168);
            this.lblDescripcion.Name = "lblDescripcion";
            this.lblDescripcion.Size = new System.Drawing.Size(75, 15);
            this.lblDescripcion.TabIndex = 2;
            this.lblDescripcion.Text = "Descripción:";
            // 
            // txtNombreProducto
            // 
            this.txtNombreProducto.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtNombreProducto.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNombreProducto.Location = new System.Drawing.Point(140, 130);
            this.txtNombreProducto.Name = "txtNombreProducto";
            this.txtNombreProducto.Size = new System.Drawing.Size(250, 21);
            this.txtNombreProducto.TabIndex = 1;
            // 
            // lblNombreProducto
            // 
            this.lblNombreProducto.AutoSize = true;
            this.lblNombreProducto.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNombreProducto.Location = new System.Drawing.Point(30, 133);
            this.lblNombreProducto.Name = "lblNombreProducto";
            this.lblNombreProducto.Size = new System.Drawing.Size(107, 15);
            this.lblNombreProducto.TabIndex = 0;
            this.lblNombreProducto.Text = "Nombre Producto:";
            // 
            // tabMovimientos
            // 
            this.tabMovimientos.Controls.Add(this.groupFiltroMovimientos);
            this.tabMovimientos.Controls.Add(this.dgvMovimientos);
            this.tabMovimientos.Location = new System.Drawing.Point(4, 22);
            this.tabMovimientos.Name = "tabMovimientos";
            this.tabMovimientos.Padding = new System.Windows.Forms.Padding(3);
            this.tabMovimientos.Size = new System.Drawing.Size(842, 474);
            this.tabMovimientos.TabIndex = 2;
            this.tabMovimientos.Text = "Movimientos";
            this.tabMovimientos.UseVisualStyleBackColor = true;
            // 
            // groupFiltroMovimientos
            // 
            this.groupFiltroMovimientos.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupFiltroMovimientos.Controls.Add(this.btnVerMovimientos);
            this.groupFiltroMovimientos.Controls.Add(this.dtpFechaFin);
            this.groupFiltroMovimientos.Controls.Add(this.lblFechaFin);
            this.groupFiltroMovimientos.Controls.Add(this.dtpFechaInicio);
            this.groupFiltroMovimientos.Controls.Add(this.lblFechaInicio);
            this.groupFiltroMovimientos.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupFiltroMovimientos.Location = new System.Drawing.Point(15, 15);
            this.groupFiltroMovimientos.Name = "groupFiltroMovimientos";
            this.groupFiltroMovimientos.Size = new System.Drawing.Size(810, 80);
            this.groupFiltroMovimientos.TabIndex = 1;
            this.groupFiltroMovimientos.TabStop = false;
            this.groupFiltroMovimientos.Text = "Filtro de Fechas";
            // 
            // btnVerMovimientos
            // 
            this.btnVerMovimientos.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(215)))));
            this.btnVerMovimientos.FlatAppearance.BorderSize = 0;
            this.btnVerMovimientos.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnVerMovimientos.ForeColor = System.Drawing.Color.White;
            this.btnVerMovimientos.Location = new System.Drawing.Point(620, 25);
            this.btnVerMovimientos.Name = "btnVerMovimientos";
            this.btnVerMovimientos.Size = new System.Drawing.Size(150, 35);
            this.btnVerMovimientos.TabIndex = 4;
            this.btnVerMovimientos.Text = "Ver Movimientos";
            this.btnVerMovimientos.UseVisualStyleBackColor = false;
            this.btnVerMovimientos.Click += new System.EventHandler(this.BtnVerMovimientos_Click);
            // 
            // dtpFechaFin
            // 
            this.dtpFechaFin.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpFechaFin.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFechaFin.Location = new System.Drawing.Point(400, 35);
            this.dtpFechaFin.Name = "dtpFechaFin";
            this.dtpFechaFin.Size = new System.Drawing.Size(150, 21);
            this.dtpFechaFin.TabIndex = 3;
            // 
            // lblFechaFin
            // 
            this.lblFechaFin.AutoSize = true;
            this.lblFechaFin.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFechaFin.Location = new System.Drawing.Point(320, 38);
            this.lblFechaFin.Name = "lblFechaFin";
            this.lblFechaFin.Size = new System.Drawing.Size(68, 15);
            this.lblFechaFin.TabIndex = 2;
            this.lblFechaFin.Text = "Fecha Fin:";
            // 
            // dtpFechaInicio
            // 
            this.dtpFechaInicio.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpFechaInicio.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFechaInicio.Location = new System.Drawing.Point(120, 35);
            this.dtpFechaInicio.Name = "dtpFechaInicio";
            this.dtpFechaInicio.Size = new System.Drawing.Size(150, 21);
            this.dtpFechaInicio.TabIndex = 1;
            // 
            // lblFechaInicio
            // 
            this.lblFechaInicio.AutoSize = true;
            this.lblFechaInicio.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFechaInicio.Location = new System.Drawing.Point(30, 38);
            this.lblFechaInicio.Name = "lblFechaInicio";
            this.lblFechaInicio.Size = new System.Drawing.Size(80, 15);
            this.lblFechaInicio.TabIndex = 0;
            this.lblFechaInicio.Text = "Fecha Inicio:";
            // 
            // dgvMovimientos
            // 
            this.dgvMovimientos.AllowUserToAddRows = false;
            this.dgvMovimientos.AllowUserToDeleteRows = false;
            this.dgvMovimientos.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvMovimientos.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvMovimientos.BackgroundColor = System.Drawing.Color.White;
            this.dgvMovimientos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMovimientos.Location = new System.Drawing.Point(15, 110);
            this.dgvMovimientos.Name = "dgvMovimientos";
            this.dgvMovimientos.ReadOnly = true;
            this.dgvMovimientos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvMovimientos.Size = new System.Drawing.Size(810, 350);
            this.dgvMovimientos.TabIndex = 0;
            // 
            // tabAlertas
            // 
            this.tabAlertas.Controls.Add(this.groupAlertas);
            this.tabAlertas.Controls.Add(this.dgvStockBajo);
            this.tabAlertas.Location = new System.Drawing.Point(4, 22);
            this.tabAlertas.Name = "tabAlertas";
            this.tabAlertas.Padding = new System.Windows.Forms.Padding(3);
            this.tabAlertas.Size = new System.Drawing.Size(842, 474);
            this.tabAlertas.TabIndex = 3;
            this.tabAlertas.Text = "Alertas de Stock";
            this.tabAlertas.UseVisualStyleBackColor = true;
            // 
            // groupAlertas
            // 
            this.groupAlertas.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupAlertas.Controls.Add(this.btnStockBajo);
            this.groupAlertas.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupAlertas.Location = new System.Drawing.Point(15, 15);
            this.groupAlertas.Name = "groupAlertas";
            this.groupAlertas.Size = new System.Drawing.Size(810, 70);
            this.groupAlertas.TabIndex = 1;
            this.groupAlertas.TabStop = false;
            this.groupAlertas.Text = "Alertas";
            // 
            // btnStockBajo
            // 
            this.btnStockBajo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(193)))), ((int)(((byte)(7)))));
            this.btnStockBajo.FlatAppearance.BorderSize = 0;
            this.btnStockBajo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStockBajo.ForeColor = System.Drawing.Color.Black;
            this.btnStockBajo.Location = new System.Drawing.Point(30, 25);
            this.btnStockBajo.Name = "btnStockBajo";
            this.btnStockBajo.Size = new System.Drawing.Size(200, 35);
            this.btnStockBajo.TabIndex = 0;
            this.btnStockBajo.Text = "⚠️ Ver Productos Stock Bajo";
            this.btnStockBajo.UseVisualStyleBackColor = false;
            this.btnStockBajo.Click += new System.EventHandler(this.BtnStockBajo_Click);
            // 
            // dgvStockBajo
            // 
            this.dgvStockBajo.AllowUserToAddRows = false;
            this.dgvStockBajo.AllowUserToDeleteRows = false;
            this.dgvStockBajo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvStockBajo.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvStockBajo.BackgroundColor = System.Drawing.Color.White;
            this.dgvStockBajo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvStockBajo.Location = new System.Drawing.Point(15, 100);
            this.dgvStockBajo.Name = "dgvStockBajo";
            this.dgvStockBajo.ReadOnly = true;
            this.dgvStockBajo.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvStockBajo.Size = new System.Drawing.Size(810, 360);
            this.dgvStockBajo.TabIndex = 0;
            // 
            // InventarioModule
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "InventarioModule";
            this.groupMovimientos.ResumeLayout(false);
            this.groupMovimientos.PerformLayout();
            this.tabMovimientos.ResumeLayout(false);
            this.groupFiltroMovimientos.ResumeLayout(false);
            this.groupFiltroMovimientos.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMovimientos)).EndInit();
            this.tabAlertas.ResumeLayout(false);
            this.groupAlertas.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvStockBajo)).EndInit();
            this.tabControlPrincipal.ResumeLayout(false);
            this.tabConfiguraciones.ResumeLayout(false);
            this.tabConfiguraciones.PerformLayout();
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.GroupBox groupMovimientos;
        private System.Windows.Forms.Button btnRegistrarMovimiento;
        private System.Windows.Forms.ComboBox cmbUbicacion;
        private System.Windows.Forms.Label lblUbicacion;
        private System.Windows.Forms.TextBox txtMotivoMovimiento;
        private System.Windows.Forms.Label lblMotivoMovimiento;
        private System.Windows.Forms.TextBox txtCantidadMovimiento;
        private System.Windows.Forms.Label lblCantidadMovimiento;
        private System.Windows.Forms.ComboBox cmbTipoMovimiento;
        private System.Windows.Forms.Label lblTipoMovimiento;
        private System.Windows.Forms.TextBox txtStockMaximo;
        private System.Windows.Forms.Label lblStockMaximo;
        private System.Windows.Forms.TextBox txtStockMinimo;
        private System.Windows.Forms.Label lblStockMinimo;
        private System.Windows.Forms.TextBox txtStockActual;
        private System.Windows.Forms.Label lblStockActual;
        private System.Windows.Forms.TextBox txtPrecio;
        private System.Windows.Forms.Label lblPrecio;
        private System.Windows.Forms.TextBox txtDescripcion;
        private System.Windows.Forms.Label lblDescripcion;
        private System.Windows.Forms.TextBox txtNombreProducto;
        private System.Windows.Forms.Label lblNombreProducto;
        private System.Windows.Forms.TabPage tabMovimientos;
        private System.Windows.Forms.GroupBox groupFiltroMovimientos;
        private System.Windows.Forms.Button btnVerMovimientos;
        private System.Windows.Forms.DateTimePicker dtpFechaFin;
        private System.Windows.Forms.Label lblFechaFin;
        private System.Windows.Forms.DateTimePicker dtpFechaInicio;
        private System.Windows.Forms.Label lblFechaInicio;
        private System.Windows.Forms.DataGridView dgvMovimientos;
        private System.Windows.Forms.TabPage tabAlertas;
        private System.Windows.Forms.GroupBox groupAlertas;
        private System.Windows.Forms.Button btnStockBajo;
        private System.Windows.Forms.DataGridView dgvStockBajo;
    }
}