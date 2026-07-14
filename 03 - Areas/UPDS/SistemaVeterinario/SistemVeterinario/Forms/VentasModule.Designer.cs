using System.Drawing;
using System.Windows.Forms;

namespace SistemVeterinario.Forms
{
    partial class VentasModule
    {
        /// <summary> 
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        // Controles específicos de ventas
        private System.Windows.Forms.Label lblContador;
        private System.Windows.Forms.ComboBox cmbEstadoFiltro;
        private System.Windows.Forms.Label lblEstadoFiltro;
        private System.Windows.Forms.GroupBox grpDatosFactura;
        private System.Windows.Forms.TableLayoutPanel tableLayoutFactura;
        private System.Windows.Forms.Label lblNumeroFactura;
        private System.Windows.Forms.TextBox txtNumeroFactura;
        private System.Windows.Forms.Button btnGenerarNumero;
        private System.Windows.Forms.Label lblCliente;
        private System.Windows.Forms.TextBox txtClienteSeleccionado;
        private System.Windows.Forms.Button btnSeleccionarCliente;
        private System.Windows.Forms.Label lblFechaEmision;
        private System.Windows.Forms.DateTimePicker dtpFechaEmision;
        private System.Windows.Forms.Label lblFechaVencimiento;
        private System.Windows.Forms.DateTimePicker dtpFechaVencimiento;
        private System.Windows.Forms.CheckBox chkTieneFechaVencimiento;
        private System.Windows.Forms.CheckBox chkFinalizar;
        private System.Windows.Forms.Label lblEstado;
        private System.Windows.Forms.ComboBox cmbEstado;
        private System.Windows.Forms.Label lblSubtotal;
        private System.Windows.Forms.NumericUpDown nudSubtotal;
        private System.Windows.Forms.Label lblImpuestos;
        private System.Windows.Forms.NumericUpDown nudImpuestos;
        private System.Windows.Forms.Label lblDescuentos;
        private System.Windows.Forms.NumericUpDown nudDescuentos;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.NumericUpDown nudTotal;
        private System.Windows.Forms.Label lblNotas;
        private System.Windows.Forms.TextBox txtNotas;
        private System.Windows.Forms.GroupBox grpItems;
        private System.Windows.Forms.DataGridView dgvItems;
        private System.Windows.Forms.Button btnAgregarProductos;
        private System.Windows.Forms.Button btnAgregarServicios;
        private System.Windows.Forms.Button btnEliminarItem;
        private System.Windows.Forms.Button btnRefrescar;
        private System.Windows.Forms.Button btnLimpiarFiltros;
        private System.Windows.Forms.Label lblTotalRegistros;
        private System.Windows.Forms.TextBox txtBuscarCliente;
        private System.Windows.Forms.TextBox txtBuscarPersonaId;
        
        // Campos ocultos para JSON
        private System.Windows.Forms.TextBox txtPersonaId;
        private System.Windows.Forms.TextBox txtProductosJson;
        private System.Windows.Forms.TextBox txtServiciosJson;

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
            this.cmbEstadoFiltro = new System.Windows.Forms.ComboBox();
            this.lblEstadoFiltro = new System.Windows.Forms.Label();
            this.grpDatosFactura = new System.Windows.Forms.GroupBox();
            this.tableLayoutFactura = new System.Windows.Forms.TableLayoutPanel();
            this.lblNumeroFactura = new System.Windows.Forms.Label();
            this.txtNumeroFactura = new System.Windows.Forms.TextBox();
            this.btnGenerarNumero = new System.Windows.Forms.Button();
            this.lblCliente = new System.Windows.Forms.Label();
            this.txtClienteSeleccionado = new System.Windows.Forms.TextBox();
            this.btnSeleccionarCliente = new System.Windows.Forms.Button();
            this.lblFechaEmision = new System.Windows.Forms.Label();
            this.dtpFechaEmision = new System.Windows.Forms.DateTimePicker();
            this.lblFechaVencimiento = new System.Windows.Forms.Label();
            this.dtpFechaVencimiento = new System.Windows.Forms.DateTimePicker();
            this.chkTieneFechaVencimiento = new System.Windows.Forms.CheckBox();
            this.chkFinalizar = new System.Windows.Forms.CheckBox();
            this.lblEstado = new System.Windows.Forms.Label();
            this.cmbEstado = new System.Windows.Forms.ComboBox();
            this.lblSubtotal = new System.Windows.Forms.Label();
            this.nudSubtotal = new System.Windows.Forms.NumericUpDown();
            this.lblImpuestos = new System.Windows.Forms.Label();
            this.nudImpuestos = new System.Windows.Forms.NumericUpDown();
            this.lblDescuentos = new System.Windows.Forms.Label();
            this.nudDescuentos = new System.Windows.Forms.NumericUpDown();
            this.lblTotal = new System.Windows.Forms.Label();
            this.nudTotal = new System.Windows.Forms.NumericUpDown();
            this.lblNotas = new System.Windows.Forms.Label();
            this.txtNotas = new System.Windows.Forms.TextBox();
            this.grpItems = new System.Windows.Forms.GroupBox();
            this.dgvItems = new System.Windows.Forms.DataGridView();
            this.btnAgregarProductos = new System.Windows.Forms.Button();
            this.btnAgregarServicios = new System.Windows.Forms.Button();
            this.btnEliminarItem = new System.Windows.Forms.Button();
            this.btnRefrescar = new System.Windows.Forms.Button();
            this.btnLimpiarFiltros = new System.Windows.Forms.Button();
            this.lblTotalRegistros = new System.Windows.Forms.Label();
            this.txtBuscarCliente = new System.Windows.Forms.TextBox();
            this.txtBuscarPersonaId = new System.Windows.Forms.TextBox();
            this.txtPersonaId = new System.Windows.Forms.TextBox();
            this.txtProductosJson = new System.Windows.Forms.TextBox();
            this.txtServiciosJson = new System.Windows.Forms.TextBox();
            this.tabControlPrincipal.SuspendLayout();
            this.tabInicio.SuspendLayout();
            this.tabConfiguraciones.SuspendLayout();
            this.panelBusqueda.SuspendLayout();
            this.panelFormulario.SuspendLayout();
            this.panelSuperior.SuspendLayout();
            this.panelBotones.SuspendLayout();
            this.grpDatosFactura.SuspendLayout();
            this.tableLayoutFactura.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudSubtotal)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudImpuestos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudDescuentos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudTotal)).BeginInit();
            this.grpItems.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvItems)).BeginInit();
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
            this.tabInicio.Text = "Gestión de Ventas";
            // 
            // tabConfiguraciones
            // 
            this.tabConfiguraciones.Location = new System.Drawing.Point(4, 39);
            this.tabConfiguraciones.Margin = new System.Windows.Forms.Padding(2);
            this.tabConfiguraciones.Padding = new System.Windows.Forms.Padding(7);
            this.tabConfiguraciones.Size = new System.Drawing.Size(1135, 597);
            this.tabConfiguraciones.Text = "Configuración de Factura";
            // 
            // panelBusqueda
            // 
            this.panelBusqueda.Controls.Add(this.btnLimpiarFiltros);
            this.panelBusqueda.Controls.Add(this.btnRefrescar);
            this.panelBusqueda.Controls.Add(this.cmbEstadoFiltro);
            this.panelBusqueda.Controls.Add(this.lblEstadoFiltro);
            this.panelBusqueda.Controls.Add(this.lblContador);
            this.panelBusqueda.Controls.Add(this.lblTotalRegistros);
            this.panelBusqueda.Margin = new System.Windows.Forms.Padding(2);
            this.panelBusqueda.Padding = new System.Windows.Forms.Padding(11, 12, 11, 12);
            this.panelBusqueda.Size = new System.Drawing.Size(1121, 98);
            this.panelBusqueda.Controls.SetChildIndex(this.lblTotalRegistros, 0);
            this.panelBusqueda.Controls.SetChildIndex(this.lblContador, 0);
            this.panelBusqueda.Controls.SetChildIndex(this.lblEstadoFiltro, 0);
            this.panelBusqueda.Controls.SetChildIndex(this.cmbEstadoFiltro, 0);
            this.panelBusqueda.Controls.SetChildIndex(this.btnRefrescar, 0);
            this.panelBusqueda.Controls.SetChildIndex(this.chkMostrarTodo, 0);
            this.panelBusqueda.Controls.SetChildIndex(this.btnNuevo, 0);
            this.panelBusqueda.Controls.SetChildIndex(this.btnLimpiarFiltros, 0);
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
            this.btnBuscar.Location = new System.Drawing.Point(393, 13);
            this.btnBuscar.Margin = new System.Windows.Forms.Padding(2);
            this.btnBuscar.Size = new System.Drawing.Size(104, 38);
            // 
            // btnNuevo
            // 
            this.btnNuevo.FlatAppearance.BorderSize = 0;
            this.btnNuevo.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnNuevo.Location = new System.Drawing.Point(989, 11);
            this.btnNuevo.Margin = new System.Windows.Forms.Padding(2);
            this.btnNuevo.Size = new System.Drawing.Size(122, 31);
            // 
            // chkMostrarTodo
            // 
            this.chkMostrarTodo.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.chkMostrarTodo.Location = new System.Drawing.Point(68, 54);
            this.chkMostrarTodo.Margin = new System.Windows.Forms.Padding(2);
            this.chkMostrarTodo.Size = new System.Drawing.Size(237, 24);
            // 
            // panelFormulario
            // 
            this.panelFormulario.Controls.Add(this.grpItems);
            this.panelFormulario.Controls.Add(this.grpDatosFactura);
            this.panelFormulario.Location = new System.Drawing.Point(7, 7);
            this.panelFormulario.Margin = new System.Windows.Forms.Padding(2);
            this.panelFormulario.Padding = new System.Windows.Forms.Padding(8);
            this.panelFormulario.Size = new System.Drawing.Size(1121, 583);
            this.panelFormulario.Controls.SetChildIndex(this.grpDatosFactura, 0);
            this.panelFormulario.Controls.SetChildIndex(this.grpItems, 0);
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
            this.lblModo.Location = new System.Drawing.Point(60, 12);
            this.lblModo.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            // 
            // cmbModo
            // 
            this.cmbModo.Location = new System.Drawing.Point(168, 11);
            this.cmbModo.Margin = new System.Windows.Forms.Padding(2);
            // 
            // lblId
            // 
            this.lblId.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblId.Location = new System.Drawing.Point(379, 14);
            this.lblId.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblId.Size = new System.Drawing.Size(33, 23);
            // 
            // txtId
            // 
            this.txtId.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(240)))), ((int)(((byte)(241)))));
            this.txtId.Location = new System.Drawing.Point(434, 9);
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
            this.btnGuardar.Location = new System.Drawing.Point(952, 9);
            this.btnGuardar.Margin = new System.Windows.Forms.Padding(2);
            // 
            // btnCancelar
            // 
            this.btnCancelar.FlatAppearance.BorderSize = 0;
            this.btnCancelar.Location = new System.Drawing.Point(787, 9);
            this.btnCancelar.Margin = new System.Windows.Forms.Padding(2);
            // 
            // btnEliminar
            // 
            this.btnEliminar.FlatAppearance.BorderSize = 0;
            this.btnEliminar.Location = new System.Drawing.Point(56, 9);
            // 
            // lblContador
            // 
            this.lblContador.AutoSize = true;
            this.lblContador.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblContador.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.lblContador.Location = new System.Drawing.Point(358, 58);
            this.lblContador.Name = "lblContador";
            this.lblContador.Size = new System.Drawing.Size(169, 23);
            this.lblContador.TabIndex = 0;
            this.lblContador.Text = "Total de registros: 0";
            // 
            // cmbEstadoFiltro
            // 
            this.cmbEstadoFiltro.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbEstadoFiltro.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbEstadoFiltro.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbEstadoFiltro.FormattingEnabled = true;
            this.cmbEstadoFiltro.Location = new System.Drawing.Point(577, 44);
            this.cmbEstadoFiltro.Name = "cmbEstadoFiltro";
            this.cmbEstadoFiltro.Size = new System.Drawing.Size(202, 31);
            this.cmbEstadoFiltro.TabIndex = 2;
            // 
            // lblEstadoFiltro
            // 
            this.lblEstadoFiltro.AutoSize = true;
            this.lblEstadoFiltro.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblEstadoFiltro.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.lblEstadoFiltro.Location = new System.Drawing.Point(617, 18);
            this.lblEstadoFiltro.Name = "lblEstadoFiltro";
            this.lblEstadoFiltro.Size = new System.Drawing.Size(127, 23);
            this.lblEstadoFiltro.TabIndex = 1;
            this.lblEstadoFiltro.Text = "📊 Filtrar por:";
            // 
            // grpDatosFactura
            // 
            this.grpDatosFactura.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpDatosFactura.Controls.Add(this.tableLayoutFactura);
            this.grpDatosFactura.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.grpDatosFactura.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.grpDatosFactura.Location = new System.Drawing.Point(8, 65);
            this.grpDatosFactura.Name = "grpDatosFactura";
            this.grpDatosFactura.Padding = new System.Windows.Forms.Padding(3, 8, 3, 3);
            this.grpDatosFactura.Size = new System.Drawing.Size(1105, 280);
            this.grpDatosFactura.TabIndex = 1;
            this.grpDatosFactura.TabStop = false;
            this.grpDatosFactura.Text = "🧾 Información de la Factura";
            // 
            // tableLayoutFactura
            // 
            this.tableLayoutFactura.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutFactura.BackColor = System.Drawing.Color.White;
            this.tableLayoutFactura.ColumnCount = 4;
            this.tableLayoutFactura.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 140F));
            this.tableLayoutFactura.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tableLayoutFactura.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 140F));
            this.tableLayoutFactura.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.tableLayoutFactura.Controls.Add(this.lblNumeroFactura, 0, 0);
            this.tableLayoutFactura.Controls.Add(this.txtNumeroFactura, 1, 0);
            this.tableLayoutFactura.Controls.Add(this.btnGenerarNumero, 2, 0);
            this.tableLayoutFactura.Controls.Add(this.lblCliente, 0, 1);
            this.tableLayoutFactura.Controls.Add(this.txtClienteSeleccionado, 1, 1);
            this.tableLayoutFactura.Controls.Add(this.btnSeleccionarCliente, 2, 1);
            this.tableLayoutFactura.Controls.Add(this.lblFechaEmision, 0, 2);
            this.tableLayoutFactura.Controls.Add(this.dtpFechaEmision, 1, 2);
            this.tableLayoutFactura.Controls.Add(this.lblFechaVencimiento, 2, 2);
            this.tableLayoutFactura.Controls.Add(this.dtpFechaVencimiento, 3, 2);
            this.tableLayoutFactura.Controls.Add(this.chkTieneFechaVencimiento, 1, 3);
            this.tableLayoutFactura.Controls.Add(this.chkFinalizar, 3, 3);
            this.tableLayoutFactura.Controls.Add(this.lblEstado, 0, 4);
            this.tableLayoutFactura.Controls.Add(this.cmbEstado, 1, 4);
            this.tableLayoutFactura.Controls.Add(this.lblSubtotal, 0, 5);
            this.tableLayoutFactura.Controls.Add(this.nudSubtotal, 1, 5);
            this.tableLayoutFactura.Controls.Add(this.lblImpuestos, 2, 5);
            this.tableLayoutFactura.Controls.Add(this.nudImpuestos, 3, 5);
            this.tableLayoutFactura.Controls.Add(this.lblDescuentos, 0, 6);
            this.tableLayoutFactura.Controls.Add(this.nudDescuentos, 1, 6);
            this.tableLayoutFactura.Controls.Add(this.lblTotal, 2, 6);
            this.tableLayoutFactura.Controls.Add(this.nudTotal, 3, 6);
            this.tableLayoutFactura.Location = new System.Drawing.Point(15, 35);
            this.tableLayoutFactura.Name = "tableLayoutFactura";
            this.tableLayoutFactura.Padding = new System.Windows.Forms.Padding(20);
            this.tableLayoutFactura.RowCount = 7;
            this.tableLayoutFactura.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutFactura.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutFactura.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutFactura.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutFactura.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutFactura.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutFactura.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutFactura.Size = new System.Drawing.Size(1075, 237);
            this.tableLayoutFactura.TabIndex = 0;
            // 
            // lblNumeroFactura
            // 
            this.lblNumeroFactura.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblNumeroFactura.AutoSize = true;
            this.lblNumeroFactura.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblNumeroFactura.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(76)))), ((int)(((byte)(60)))));
            this.lblNumeroFactura.Location = new System.Drawing.Point(23, 26);
            this.lblNumeroFactura.Name = "lblNumeroFactura";
            this.lblNumeroFactura.Size = new System.Drawing.Size(121, 23);
            this.lblNumeroFactura.TabIndex = 0;
            this.lblNumeroFactura.Text = "🧾 N° Factura";
            // 
            // txtNumeroFactura
            // 
            this.txtNumeroFactura.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtNumeroFactura.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNumeroFactura.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtNumeroFactura.Location = new System.Drawing.Point(163, 23);
            this.txtNumeroFactura.MaxLength = 50;
            this.txtNumeroFactura.Name = "txtNumeroFactura";
            this.txtNumeroFactura.Size = new System.Drawing.Size(296, 30);
            this.txtNumeroFactura.TabIndex = 1;
            // 
            // btnGenerarNumero
            // 
            this.btnGenerarNumero.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnGenerarNumero.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.btnGenerarNumero.FlatAppearance.BorderSize = 0;
            this.btnGenerarNumero.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGenerarNumero.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnGenerarNumero.ForeColor = System.Drawing.Color.White;
            this.btnGenerarNumero.Location = new System.Drawing.Point(465, 23);
            this.btnGenerarNumero.Name = "btnGenerarNumero";
            this.btnGenerarNumero.Size = new System.Drawing.Size(100, 29);
            this.btnGenerarNumero.TabIndex = 2;
            this.btnGenerarNumero.Text = "🔢 Generar";
            this.btnGenerarNumero.UseVisualStyleBackColor = false;
            // 
            // lblCliente
            // 
            this.lblCliente.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblCliente.AutoSize = true;
            this.lblCliente.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblCliente.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(76)))), ((int)(((byte)(60)))));
            this.lblCliente.Location = new System.Drawing.Point(23, 61);
            this.lblCliente.Name = "lblCliente";
            this.lblCliente.Size = new System.Drawing.Size(95, 23);
            this.lblCliente.TabIndex = 3;
            this.lblCliente.Text = "👤 Cliente";
            // 
            // txtClienteSeleccionado
            // 
            this.txtClienteSeleccionado.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtClienteSeleccionado.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtClienteSeleccionado.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtClienteSeleccionado.Location = new System.Drawing.Point(163, 58);
            this.txtClienteSeleccionado.Name = "txtClienteSeleccionado";
            this.txtClienteSeleccionado.ReadOnly = true;
            this.txtClienteSeleccionado.Size = new System.Drawing.Size(296, 30);
            this.txtClienteSeleccionado.TabIndex = 4;
            // 
            // btnSeleccionarCliente
            // 
            this.btnSeleccionarCliente.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnSeleccionarCliente.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.btnSeleccionarCliente.FlatAppearance.BorderSize = 0;
            this.btnSeleccionarCliente.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSeleccionarCliente.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnSeleccionarCliente.ForeColor = System.Drawing.Color.White;
            this.btnSeleccionarCliente.Location = new System.Drawing.Point(465, 58);
            this.btnSeleccionarCliente.Name = "btnSeleccionarCliente";
            this.btnSeleccionarCliente.Size = new System.Drawing.Size(100, 29);
            this.btnSeleccionarCliente.TabIndex = 5;
            this.btnSeleccionarCliente.Text = "🔍 Buscar";
            this.btnSeleccionarCliente.UseVisualStyleBackColor = false;
            // 
            // lblFechaEmision
            // 
            this.lblFechaEmision.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblFechaEmision.AutoSize = true;
            this.lblFechaEmision.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblFechaEmision.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(76)))), ((int)(((byte)(60)))));
            this.lblFechaEmision.Location = new System.Drawing.Point(23, 96);
            this.lblFechaEmision.Name = "lblFechaEmision";
            this.lblFechaEmision.Size = new System.Drawing.Size(119, 23);
            this.lblFechaEmision.TabIndex = 6;
            this.lblFechaEmision.Text = "📅 F. Emisión";
            // 
            // dtpFechaEmision
            // 
            this.dtpFechaEmision.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.dtpFechaEmision.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.dtpFechaEmision.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFechaEmision.Location = new System.Drawing.Point(163, 93);
            this.dtpFechaEmision.Name = "dtpFechaEmision";
            this.dtpFechaEmision.Size = new System.Drawing.Size(296, 30);
            this.dtpFechaEmision.TabIndex = 7;
            // 
            // lblFechaVencimiento
            // 
            this.lblFechaVencimiento.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblFechaVencimiento.AutoSize = true;
            this.lblFechaVencimiento.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblFechaVencimiento.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(76)))), ((int)(((byte)(60)))));
            this.lblFechaVencimiento.Location = new System.Drawing.Point(465, 90);
            this.lblFechaVencimiento.Name = "lblFechaVencimiento";
            this.lblFechaVencimiento.Size = new System.Drawing.Size(109, 35);
            this.lblFechaVencimiento.TabIndex = 8;
            this.lblFechaVencimiento.Text = "⏰ F. Vencimiento";
            // 
            // dtpFechaVencimiento
            // 
            this.dtpFechaVencimiento.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.dtpFechaVencimiento.Enabled = false;
            this.dtpFechaVencimiento.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.dtpFechaVencimiento.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFechaVencimiento.Location = new System.Drawing.Point(605, 93);
            this.dtpFechaVencimiento.Name = "dtpFechaVencimiento";
            this.dtpFechaVencimiento.Size = new System.Drawing.Size(447, 30);
            this.dtpFechaVencimiento.TabIndex = 9;
            // 
            // chkTieneFechaVencimiento
            // 
            this.chkTieneFechaVencimiento.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.chkTieneFechaVencimiento.AutoSize = true;
            this.chkTieneFechaVencimiento.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.chkTieneFechaVencimiento.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.chkTieneFechaVencimiento.Location = new System.Drawing.Point(163, 129);
            this.chkTieneFechaVencimiento.Name = "chkTieneFechaVencimiento";
            this.chkTieneFechaVencimiento.Size = new System.Drawing.Size(258, 27);
            this.chkTieneFechaVencimiento.TabIndex = 10;
            this.chkTieneFechaVencimiento.Text = "⏳ Tiene Fecha Vencimiento";
            this.chkTieneFechaVencimiento.UseVisualStyleBackColor = true;
            // 
            // chkFinalizar
            // 
            this.chkFinalizar.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.chkFinalizar.AutoSize = true;
            this.chkFinalizar.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.chkFinalizar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.chkFinalizar.Location = new System.Drawing.Point(605, 129);
            this.chkFinalizar.Name = "chkFinalizar";
            this.chkFinalizar.Size = new System.Drawing.Size(178, 27);
            this.chkFinalizar.TabIndex = 11;
            this.chkFinalizar.Text = "✅ Finalizar Venta";
            this.chkFinalizar.UseVisualStyleBackColor = true;
            // 
            // lblEstado
            // 
            this.lblEstado.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblEstado.AutoSize = true;
            this.lblEstado.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblEstado.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(76)))), ((int)(((byte)(60)))));
            this.lblEstado.Location = new System.Drawing.Point(23, 166);
            this.lblEstado.Name = "lblEstado";
            this.lblEstado.Size = new System.Drawing.Size(92, 23);
            this.lblEstado.TabIndex = 11;
            this.lblEstado.Text = "📋 Estado";
            // 
            // cmbEstado
            // 
            this.cmbEstado.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbEstado.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbEstado.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbEstado.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbEstado.FormattingEnabled = true;
            this.cmbEstado.Location = new System.Drawing.Point(163, 163);
            this.cmbEstado.Name = "cmbEstado";
            this.cmbEstado.Size = new System.Drawing.Size(296, 31);
            this.cmbEstado.TabIndex = 12;
            // 
            // lblSubtotal
            // 
            this.lblSubtotal.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblSubtotal.AutoSize = true;
            this.lblSubtotal.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblSubtotal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(76)))), ((int)(((byte)(60)))));
            this.lblSubtotal.Location = new System.Drawing.Point(23, 201);
            this.lblSubtotal.Name = "lblSubtotal";
            this.lblSubtotal.Size = new System.Drawing.Size(108, 23);
            this.lblSubtotal.TabIndex = 13;
            this.lblSubtotal.Text = "💰 Subtotal";
            // 
            // nudSubtotal
            // 
            this.nudSubtotal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.nudSubtotal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.nudSubtotal.DecimalPlaces = 2;
            this.nudSubtotal.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.nudSubtotal.Location = new System.Drawing.Point(163, 198);
            this.nudSubtotal.Maximum = new decimal(new int[] {
            999999,
            0,
            0,
            131072});
            this.nudSubtotal.Name = "nudSubtotal";
            this.nudSubtotal.ReadOnly = true;
            this.nudSubtotal.Size = new System.Drawing.Size(296, 30);
            this.nudSubtotal.TabIndex = 14;
            this.nudSubtotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.nudSubtotal.ThousandsSeparator = true;
            // 
            // lblImpuestos
            // 
            this.lblImpuestos.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblImpuestos.AutoSize = true;
            this.lblImpuestos.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblImpuestos.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(76)))), ((int)(((byte)(60)))));
            this.lblImpuestos.Location = new System.Drawing.Point(465, 201);
            this.lblImpuestos.Name = "lblImpuestos";
            this.lblImpuestos.Size = new System.Drawing.Size(121, 23);
            this.lblImpuestos.TabIndex = 15;
            this.lblImpuestos.Text = "🏛️ Impuestos";
            // 
            // nudImpuestos
            // 
            this.nudImpuestos.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.nudImpuestos.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.nudImpuestos.DecimalPlaces = 2;
            this.nudImpuestos.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.nudImpuestos.Location = new System.Drawing.Point(605, 198);
            this.nudImpuestos.Maximum = new decimal(new int[] {
            99999,
            0,
            0,
            131072});
            this.nudImpuestos.Name = "nudImpuestos";
            this.nudImpuestos.Size = new System.Drawing.Size(447, 30);
            this.nudImpuestos.TabIndex = 16;
            this.nudImpuestos.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.nudImpuestos.ThousandsSeparator = true;
            // 
            // lblDescuentos
            // 
            this.lblDescuentos.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblDescuentos.AutoSize = true;
            this.lblDescuentos.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblDescuentos.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(76)))), ((int)(((byte)(60)))));
            this.lblDescuentos.Location = new System.Drawing.Point(23, 236);
            this.lblDescuentos.Name = "lblDescuentos";
            this.lblDescuentos.Size = new System.Drawing.Size(129, 23);
            this.lblDescuentos.TabIndex = 17;
            this.lblDescuentos.Text = "🏷️ Descuentos";
            // 
            // nudDescuentos
            // 
            this.nudDescuentos.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.nudDescuentos.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.nudDescuentos.DecimalPlaces = 2;
            this.nudDescuentos.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.nudDescuentos.Location = new System.Drawing.Point(163, 233);
            this.nudDescuentos.Maximum = new decimal(new int[] {
            99999,
            0,
            0,
            131072});
            this.nudDescuentos.Name = "nudDescuentos";
            this.nudDescuentos.Size = new System.Drawing.Size(296, 30);
            this.nudDescuentos.TabIndex = 18;
            this.nudDescuentos.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.nudDescuentos.ThousandsSeparator = true;
            // 
            // lblTotal
            // 
            this.lblTotal.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblTotal.AutoSize = true;
            this.lblTotal.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblTotal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.lblTotal.Location = new System.Drawing.Point(465, 233);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(106, 28);
            this.lblTotal.TabIndex = 19;
            this.lblTotal.Text = "💵 TOTAL";
            // 
            // nudTotal
            // 
            this.nudTotal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.nudTotal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.nudTotal.DecimalPlaces = 2;
            this.nudTotal.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.nudTotal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.nudTotal.Location = new System.Drawing.Point(605, 233);
            this.nudTotal.Maximum = new decimal(new int[] {
            999999,
            0,
            0,
            131072});
            this.nudTotal.Name = "nudTotal";
            this.nudTotal.ReadOnly = true;
            this.nudTotal.Size = new System.Drawing.Size(447, 34);
            this.nudTotal.TabIndex = 20;
            this.nudTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.nudTotal.ThousandsSeparator = true;
            // 
            // lblNotas
            // 
            this.lblNotas.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblNotas.AutoSize = true;
            this.lblNotas.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblNotas.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(76)))), ((int)(((byte)(60)))));
            this.lblNotas.Location = new System.Drawing.Point(15, 133);
            this.lblNotas.Name = "lblNotas";
            this.lblNotas.Size = new System.Drawing.Size(85, 23);
            this.lblNotas.TabIndex = 4;
            this.lblNotas.Text = "📝 Notas";
            // 
            // txtNotas
            // 
            this.txtNotas.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtNotas.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNotas.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtNotas.Location = new System.Drawing.Point(81, 130);
            this.txtNotas.Multiline = true;
            this.txtNotas.Name = "txtNotas";
            this.txtNotas.Size = new System.Drawing.Size(1009, 25);
            this.txtNotas.TabIndex = 5;
            // 
            // grpItems
            // 
            this.grpItems.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpItems.Controls.Add(this.dgvItems);
            this.grpItems.Controls.Add(this.btnAgregarProductos);
            this.grpItems.Controls.Add(this.btnAgregarServicios);
            this.grpItems.Controls.Add(this.btnEliminarItem);
            this.grpItems.Controls.Add(this.lblNotas);
            this.grpItems.Controls.Add(this.txtNotas);
            this.grpItems.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.grpItems.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.grpItems.Location = new System.Drawing.Point(8, 351);
            this.grpItems.Name = "grpItems";
            this.grpItems.Padding = new System.Windows.Forms.Padding(3, 8, 3, 3);
            this.grpItems.Size = new System.Drawing.Size(1105, 166);
            this.grpItems.TabIndex = 2;
            this.grpItems.TabStop = false;
            this.grpItems.Text = "🛒 Items de la Factura";
            // 
            // dgvItems
            // 
            this.dgvItems.AllowUserToAddRows = false;
            this.dgvItems.AllowUserToDeleteRows = false;
            this.dgvItems.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvItems.BackgroundColor = System.Drawing.Color.White;
            this.dgvItems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvItems.Location = new System.Drawing.Point(15, 30);
            this.dgvItems.Name = "dgvItems";
            this.dgvItems.ReadOnly = true;
            this.dgvItems.RowHeadersWidth = 51;
            this.dgvItems.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvItems.Size = new System.Drawing.Size(700, 90);
            this.dgvItems.TabIndex = 0;
            // 
            // btnAgregarProductos
            // 
            this.btnAgregarProductos.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAgregarProductos.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.btnAgregarProductos.FlatAppearance.BorderSize = 0;
            this.btnAgregarProductos.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAgregarProductos.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnAgregarProductos.ForeColor = System.Drawing.Color.White;
            this.btnAgregarProductos.Location = new System.Drawing.Point(725, 30);
            this.btnAgregarProductos.Name = "btnAgregarProductos";
            this.btnAgregarProductos.Size = new System.Drawing.Size(139, 35);
            this.btnAgregarProductos.TabIndex = 1;
            this.btnAgregarProductos.Text = "📦 + Productos";
            this.btnAgregarProductos.UseVisualStyleBackColor = false;
            // 
            // btnAgregarServicios
            // 
            this.btnAgregarServicios.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAgregarServicios.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(89)))), ((int)(((byte)(182)))));
            this.btnAgregarServicios.FlatAppearance.BorderSize = 0;
            this.btnAgregarServicios.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAgregarServicios.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnAgregarServicios.ForeColor = System.Drawing.Color.White;
            this.btnAgregarServicios.Location = new System.Drawing.Point(870, 30);
            this.btnAgregarServicios.Name = "btnAgregarServicios";
            this.btnAgregarServicios.Size = new System.Drawing.Size(120, 35);
            this.btnAgregarServicios.TabIndex = 2;
            this.btnAgregarServicios.Text = "🏥 + Servicios";
            this.btnAgregarServicios.UseVisualStyleBackColor = false;
            // 
            // btnEliminarItem
            // 
            this.btnEliminarItem.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEliminarItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(76)))), ((int)(((byte)(60)))));
            this.btnEliminarItem.FlatAppearance.BorderSize = 0;
            this.btnEliminarItem.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEliminarItem.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnEliminarItem.ForeColor = System.Drawing.Color.White;
            this.btnEliminarItem.Location = new System.Drawing.Point(996, 30);
            this.btnEliminarItem.Name = "btnEliminarItem";
            this.btnEliminarItem.Size = new System.Drawing.Size(94, 35);
            this.btnEliminarItem.TabIndex = 3;
            this.btnEliminarItem.Text = "🗑️ Quitar";
            this.btnEliminarItem.UseVisualStyleBackColor = false;
            // 
            // btnRefrescar
            // 
            this.btnRefrescar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.btnRefrescar.FlatAppearance.BorderSize = 0;
            this.btnRefrescar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefrescar.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnRefrescar.ForeColor = System.Drawing.Color.White;
            this.btnRefrescar.Location = new System.Drawing.Point(836, 17);
            this.btnRefrescar.Name = "btnRefrescar";
            this.btnRefrescar.Size = new System.Drawing.Size(100, 32);
            this.btnRefrescar.TabIndex = 3;
            this.btnRefrescar.Text = "🔄 Refrescar";
            this.btnRefrescar.UseVisualStyleBackColor = false;
            // 
            // btnLimpiarFiltros
            // 
            this.btnLimpiarFiltros.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(126)))), ((int)(((byte)(34)))));
            this.btnLimpiarFiltros.FlatAppearance.BorderSize = 0;
            this.btnLimpiarFiltros.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLimpiarFiltros.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnLimpiarFiltros.ForeColor = System.Drawing.Color.White;
            this.btnLimpiarFiltros.Location = new System.Drawing.Point(989, 49);
            this.btnLimpiarFiltros.Name = "btnLimpiarFiltros";
            this.btnLimpiarFiltros.Size = new System.Drawing.Size(122, 32);
            this.btnLimpiarFiltros.TabIndex = 4;
            this.btnLimpiarFiltros.Text = "🧹 Limpiar Filtros";
            this.btnLimpiarFiltros.UseVisualStyleBackColor = false;
            // 
            // lblTotalRegistros
            // 
            this.lblTotalRegistros.AutoSize = true;
            this.lblTotalRegistros.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblTotalRegistros.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.lblTotalRegistros.Location = new System.Drawing.Point(819, 55);
            this.lblTotalRegistros.Name = "lblTotalRegistros";
            this.lblTotalRegistros.Size = new System.Drawing.Size(127, 20);
            this.lblTotalRegistros.TabIndex = 5;
            this.lblTotalRegistros.Text = "Total registros: 0";
            // 
            // txtBuscarCliente
            // 
            this.txtBuscarCliente.Location = new System.Drawing.Point(-1000, -1000);
            this.txtBuscarCliente.Name = "txtBuscarCliente";
            this.txtBuscarCliente.Size = new System.Drawing.Size(1, 22);
            this.txtBuscarCliente.TabIndex = 0;
            this.txtBuscarCliente.Visible = false;
            // 
            // txtBuscarPersonaId
            // 
            this.txtBuscarPersonaId.Location = new System.Drawing.Point(-1000, -1000);
            this.txtBuscarPersonaId.Name = "txtBuscarPersonaId";
            this.txtBuscarPersonaId.Size = new System.Drawing.Size(1, 22);
            this.txtBuscarPersonaId.TabIndex = 0;
            this.txtBuscarPersonaId.Visible = false;
            // 
            // txtPersonaId
            // 
            this.txtPersonaId.Location = new System.Drawing.Point(-1000, -1000);
            this.txtPersonaId.Name = "txtPersonaId";
            this.txtPersonaId.Size = new System.Drawing.Size(1, 22);
            this.txtPersonaId.TabIndex = 0;
            this.txtPersonaId.Visible = false;
            // 
            // txtProductosJson
            // 
            this.txtProductosJson.Location = new System.Drawing.Point(-1000, -1000);
            this.txtProductosJson.Name = "txtProductosJson";
            this.txtProductosJson.Size = new System.Drawing.Size(1, 22);
            this.txtProductosJson.TabIndex = 0;
            this.txtProductosJson.Visible = false;
            // 
            // txtServiciosJson
            // 
            this.txtServiciosJson.Location = new System.Drawing.Point(-1000, -1000);
            this.txtServiciosJson.Name = "txtServiciosJson";
            this.txtServiciosJson.Size = new System.Drawing.Size(1, 22);
            this.txtServiciosJson.TabIndex = 0;
            this.txtServiciosJson.Visible = false;
            // 
            // VentasModule
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.Name = "VentasModule";
            this.tabControlPrincipal.ResumeLayout(false);
            this.tabInicio.ResumeLayout(false);
            this.tabConfiguraciones.ResumeLayout(false);
            this.panelBusqueda.ResumeLayout(false);
            this.panelBusqueda.PerformLayout();
            this.panelFormulario.ResumeLayout(false);
            this.panelSuperior.ResumeLayout(false);
            this.panelSuperior.PerformLayout();
            this.panelBotones.ResumeLayout(false);
            this.grpDatosFactura.ResumeLayout(false);
            this.tableLayoutFactura.ResumeLayout(false);
            this.tableLayoutFactura.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudSubtotal)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudImpuestos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudDescuentos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudTotal)).EndInit();
            this.grpItems.ResumeLayout(false);
            this.grpItems.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvItems)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
    }
}
