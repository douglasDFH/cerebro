using System.Drawing;
using System.Windows.Forms;
namespace SistemVeterinario.Forms
{
    partial class MascotasModule
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
            this.lblContador = new System.Windows.Forms.Label();
            this.grpDatosMascota = new System.Windows.Forms.GroupBox();
            this.tableLayoutMain = new System.Windows.Forms.TableLayoutPanel();
            this.lblNombre = new System.Windows.Forms.Label();
            this.txtNombre = new System.Windows.Forms.TextBox();
            this.lblEspecie = new System.Windows.Forms.Label();
            this.cmbEspecie = new System.Windows.Forms.ComboBox();
            this.lblGenero = new System.Windows.Forms.Label();
            this.cmbGenero = new System.Windows.Forms.ComboBox();
            this.lblPeso = new System.Windows.Forms.Label();
            this.nudPeso = new System.Windows.Forms.NumericUpDown();
            this.lblRaza = new System.Windows.Forms.Label();
            this.cmbRaza = new System.Windows.Forms.ComboBox();
            this.txtRaza = new System.Windows.Forms.TextBox();
            this.lblColor = new System.Windows.Forms.Label();
            this.txtColor = new System.Windows.Forms.TextBox();
            this.chkEsterilizado = new System.Windows.Forms.CheckBox();
            this.lblMicrochip = new System.Windows.Forms.Label();
            this.txtMicrochip = new System.Windows.Forms.TextBox();
            this.lblPropietario = new System.Windows.Forms.Label();
            this.txtPropietario = new System.Windows.Forms.TextBox();
            this.btnBuscarPropietario = new System.Windows.Forms.Button();
            this.chkTieneFechaNacimiento = new System.Windows.Forms.CheckBox();
            this.dtpFechaNacimiento = new System.Windows.Forms.DateTimePicker();
            this.tabControlPrincipal.SuspendLayout();
            this.tabInicio.SuspendLayout();
            this.tabConfiguraciones.SuspendLayout();
            this.panelBusqueda.SuspendLayout();
            this.panelFormulario.SuspendLayout();
            this.panelSuperior.SuspendLayout();
            this.panelBotones.SuspendLayout();
            this.grpDatosMascota.SuspendLayout();
            this.tableLayoutMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudPeso)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControlPrincipal
            // 
            this.tabControlPrincipal.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tabControlPrincipal.Size = new System.Drawing.Size(857, 574);
            // 
            // tabInicio
            // 
            this.tabInicio.Location = new System.Drawing.Point(4, 39);
            this.tabInicio.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tabInicio.Padding = new System.Windows.Forms.Padding(7, 7, 7, 7);
            this.tabInicio.Size = new System.Drawing.Size(635, 379);
            this.tabInicio.Text = "Gestión de Mascotas";
            // 
            // tabConfiguraciones
            // 
            this.tabConfiguraciones.Location = new System.Drawing.Point(4, 39);
            this.tabConfiguraciones.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tabConfiguraciones.Padding = new System.Windows.Forms.Padding(7, 7, 7, 7);
            this.tabConfiguraciones.Size = new System.Drawing.Size(849, 531);
            this.tabConfiguraciones.Text = "Configuración de Mascota";
            // 
            // panelBusqueda
            // 
            this.panelBusqueda.Controls.Add(this.lblContador);
            this.panelBusqueda.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.panelBusqueda.Padding = new System.Windows.Forms.Padding(11, 12, 11, 12);
            this.panelBusqueda.Size = new System.Drawing.Size(841, 98);
            this.panelBusqueda.Controls.SetChildIndex(this.lblContador, 0);
            this.panelBusqueda.Controls.SetChildIndex(this.chkMostrarTodo, 0);
            this.panelBusqueda.Controls.SetChildIndex(this.btnNuevo, 0);
            this.panelBusqueda.Controls.SetChildIndex(this.btnBuscar, 0);
            this.panelBusqueda.Controls.SetChildIndex(this.txtBuscar, 0);
            // 
            // txtBuscar
            // 
            this.txtBuscar.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtBuscar.Location = new System.Drawing.Point(27, 18);
            this.txtBuscar.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtBuscar.Size = new System.Drawing.Size(338, 25);
            // 
            // btnBuscar
            // 
            this.btnBuscar.FlatAppearance.BorderSize = 0;
            this.btnBuscar.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnBuscar.Location = new System.Drawing.Point(415, 11);
            this.btnBuscar.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnBuscar.Size = new System.Drawing.Size(104, 38);
            // 
            // btnNuevo
            // 
            this.btnNuevo.FlatAppearance.BorderSize = 0;
            this.btnNuevo.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnNuevo.Location = new System.Drawing.Point(631, 11);
            this.btnNuevo.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnNuevo.Size = new System.Drawing.Size(122, 38);
            // 
            // chkMostrarTodo
            // 
            this.chkMostrarTodo.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.chkMostrarTodo.Location = new System.Drawing.Point(118, 47);
            this.chkMostrarTodo.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.chkMostrarTodo.Size = new System.Drawing.Size(186, 19);
            // 
            // panelFormulario
            // 
            this.panelFormulario.Controls.Add(this.grpDatosMascota);
            this.panelFormulario.Location = new System.Drawing.Point(7, 7);
            this.panelFormulario.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.panelFormulario.Padding = new System.Windows.Forms.Padding(8, 8, 8, 8);
            this.panelFormulario.Size = new System.Drawing.Size(835, 517);
            this.panelFormulario.Controls.SetChildIndex(this.grpDatosMascota, 0);
            this.panelFormulario.Controls.SetChildIndex(this.panelSuperior, 0);
            this.panelFormulario.Controls.SetChildIndex(this.panelBotones, 0);
            // 
            // panelSuperior
            // 
            this.panelSuperior.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(250)))));
            this.panelSuperior.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.panelSuperior.Padding = new System.Windows.Forms.Padding(7, 7, 7, 7);
            this.panelSuperior.Size = new System.Drawing.Size(1020, 49);
            // 
            // lblModo
            // 
            this.lblModo.Location = new System.Drawing.Point(42, 12);
            this.lblModo.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            // 
            // cmbModo
            // 
            this.cmbModo.Location = new System.Drawing.Point(138, 11);
            this.cmbModo.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            // 
            // lblId
            // 
            this.lblId.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblId.Location = new System.Drawing.Point(336, 15);
            this.lblId.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblId.Size = new System.Drawing.Size(27, 19);
            // 
            // txtId
            // 
            this.txtId.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(240)))), ((int)(((byte)(241)))));
            this.txtId.Location = new System.Drawing.Point(406, 11);
            this.txtId.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtId.Size = new System.Drawing.Size(76, 25);
            // 
            // panelBotones
            // 
            this.panelBotones.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(250)))));
            this.panelBotones.Location = new System.Drawing.Point(13, 677);
            this.panelBotones.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.panelBotones.Padding = new System.Windows.Forms.Padding(7, 7, 7, 7);
            this.panelBotones.Size = new System.Drawing.Size(1017, 65);
            this.panelBotones.TabIndex = 0;
            this.panelBotones.Tag = "EditableButtonPanel";
            // 
            // btnGuardar
            // 
            this.btnGuardar.FlatAppearance.BorderSize = 0;
            this.btnGuardar.Location = new System.Drawing.Point(900, 13);
            this.btnGuardar.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            // 
            // btnCancelar
            // 
            this.btnCancelar.FlatAppearance.BorderSize = 0;
            this.btnCancelar.Location = new System.Drawing.Point(784, 13);
            this.btnCancelar.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            // 
            // btnEliminar
            // 
            this.btnEliminar.FlatAppearance.BorderSize = 0;
            // 
            // lblContador
            // 
            this.lblContador.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblContador.AutoSize = true;
            this.lblContador.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblContador.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.lblContador.Location = new System.Drawing.Point(610, 54);
            this.lblContador.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblContador.Name = "lblContador";
            this.lblContador.Size = new System.Drawing.Size(142, 19);
            this.lblContador.TabIndex = 6;
            this.lblContador.Text = "Total de registros: 0";
            // 
            // grpDatosMascota
            // 
            this.grpDatosMascota.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpDatosMascota.Controls.Add(this.tableLayoutMain);
            this.grpDatosMascota.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.grpDatosMascota.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.grpDatosMascota.Location = new System.Drawing.Point(15, 61);
            this.grpDatosMascota.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.grpDatosMascota.Name = "grpDatosMascota";
            this.grpDatosMascota.Padding = new System.Windows.Forms.Padding(11, 12, 11, 12);
            this.grpDatosMascota.Size = new System.Drawing.Size(805, 320);
            this.grpDatosMascota.TabIndex = 1;
            this.grpDatosMascota.TabStop = false;
            this.grpDatosMascota.Text = "📋 Información de la Mascota";
            // 
            // tableLayoutMain
            // 
            this.tableLayoutMain.ColumnCount = 6;
            this.tableLayoutMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.67F));
            this.tableLayoutMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.67F));
            this.tableLayoutMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.67F));
            this.tableLayoutMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.67F));
            this.tableLayoutMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.67F));
            this.tableLayoutMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.65F));
            this.tableLayoutMain.Controls.Add(this.lblNombre, 0, 0);
            this.tableLayoutMain.Controls.Add(this.txtNombre, 0, 1);
            this.tableLayoutMain.Controls.Add(this.lblEspecie, 2, 0);
            this.tableLayoutMain.Controls.Add(this.cmbEspecie, 2, 1);
            this.tableLayoutMain.Controls.Add(this.lblGenero, 4, 0);
            this.tableLayoutMain.Controls.Add(this.cmbGenero, 4, 1);
            this.tableLayoutMain.Controls.Add(this.lblPeso, 5, 0);
            this.tableLayoutMain.Controls.Add(this.nudPeso, 5, 1);
            this.tableLayoutMain.Controls.Add(this.lblRaza, 0, 2);
            this.tableLayoutMain.Controls.Add(this.cmbRaza, 0, 3);
            this.tableLayoutMain.Controls.Add(this.txtRaza, 1, 3);
            this.tableLayoutMain.Controls.Add(this.lblColor, 2, 2);
            this.tableLayoutMain.Controls.Add(this.txtColor, 2, 3);
            this.tableLayoutMain.Controls.Add(this.chkEsterilizado, 3, 3);
            this.tableLayoutMain.Controls.Add(this.lblMicrochip, 4, 2);
            this.tableLayoutMain.Controls.Add(this.txtMicrochip, 4, 3);
            this.tableLayoutMain.Controls.Add(this.lblPropietario, 0, 4);
            this.tableLayoutMain.Controls.Add(this.txtPropietario, 0, 5);
            this.tableLayoutMain.Controls.Add(this.btnBuscarPropietario, 3, 5);
            this.tableLayoutMain.Controls.Add(this.chkTieneFechaNacimiento, 0, 6);
            this.tableLayoutMain.Controls.Add(this.dtpFechaNacimiento, 2, 6);
            this.tableLayoutMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutMain.Location = new System.Drawing.Point(11, 32);
            this.tableLayoutMain.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tableLayoutMain.Name = "tableLayoutMain";
            this.tableLayoutMain.Padding = new System.Windows.Forms.Padding(8, 8, 8, 8);
            this.tableLayoutMain.RowCount = 7;
            this.tableLayoutMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tableLayoutMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 37F));
            this.tableLayoutMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tableLayoutMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 37F));
            this.tableLayoutMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tableLayoutMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 37F));
            this.tableLayoutMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 37F));
            this.tableLayoutMain.Size = new System.Drawing.Size(783, 276);
            this.tableLayoutMain.TabIndex = 0;
            // 
            // lblNombre
            // 
            this.lblNombre.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblNombre.AutoSize = true;
            this.lblNombre.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblNombre.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(76)))), ((int)(((byte)(60)))));
            this.lblNombre.Location = new System.Drawing.Point(12, 12);
            this.lblNombre.Margin = new System.Windows.Forms.Padding(4, 4, 4, 0);
            this.lblNombre.Name = "lblNombre";
            this.lblNombre.Size = new System.Drawing.Size(119, 19);
            this.lblNombre.TabIndex = 0;
            this.lblNombre.Text = "📝 Nombre *";
            // 
            // txtNombre
            // 
            this.txtNombre.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtNombre.BackColor = System.Drawing.Color.White;
            this.txtNombre.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNombre.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtNombre.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.txtNombre.Location = new System.Drawing.Point(12, 40);
            this.txtNombre.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtNombre.MaxLength = 100;
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.Size = new System.Drawing.Size(119, 25);
            this.txtNombre.TabIndex = 1;
            // 
            // lblEspecie
            // 
            this.lblEspecie.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblEspecie.AutoSize = true;
            this.lblEspecie.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblEspecie.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(76)))), ((int)(((byte)(60)))));
            this.lblEspecie.Location = new System.Drawing.Point(266, 12);
            this.lblEspecie.Margin = new System.Windows.Forms.Padding(4, 4, 4, 0);
            this.lblEspecie.Name = "lblEspecie";
            this.lblEspecie.Size = new System.Drawing.Size(119, 19);
            this.lblEspecie.TabIndex = 2;
            this.lblEspecie.Text = "🐾 Especie *";
            // 
            // cmbEspecie
            // 
            this.cmbEspecie.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbEspecie.BackColor = System.Drawing.Color.White;
            this.cmbEspecie.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbEspecie.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbEspecie.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbEspecie.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.cmbEspecie.Location = new System.Drawing.Point(266, 40);
            this.cmbEspecie.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cmbEspecie.Name = "cmbEspecie";
            this.cmbEspecie.Size = new System.Drawing.Size(119, 25);
            this.cmbEspecie.TabIndex = 3;
            // 
            // lblGenero
            // 
            this.lblGenero.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblGenero.AutoSize = true;
            this.lblGenero.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblGenero.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.lblGenero.Location = new System.Drawing.Point(520, 12);
            this.lblGenero.Margin = new System.Windows.Forms.Padding(4, 4, 4, 0);
            this.lblGenero.Name = "lblGenero";
            this.lblGenero.Size = new System.Drawing.Size(119, 19);
            this.lblGenero.TabIndex = 7;
            this.lblGenero.Text = "🐾 Sexo";
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
            this.cmbGenero.Location = new System.Drawing.Point(520, 40);
            this.cmbGenero.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cmbGenero.Name = "cmbGenero";
            this.cmbGenero.Size = new System.Drawing.Size(119, 25);
            this.cmbGenero.TabIndex = 8;
            // 
            // lblPeso
            // 
            this.lblPeso.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPeso.AutoSize = true;
            this.lblPeso.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblPeso.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.lblPeso.Location = new System.Drawing.Point(647, 12);
            this.lblPeso.Margin = new System.Windows.Forms.Padding(4, 4, 4, 0);
            this.lblPeso.Name = "lblPeso";
            this.lblPeso.Size = new System.Drawing.Size(124, 19);
            this.lblPeso.TabIndex = 9;
            this.lblPeso.Text = "⚖️ Peso (kg)";
            // 
            // nudPeso
            // 
            this.nudPeso.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.nudPeso.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.nudPeso.DecimalPlaces = 2;
            this.nudPeso.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.nudPeso.Location = new System.Drawing.Point(647, 40);
            this.nudPeso.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.nudPeso.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.nudPeso.Name = "nudPeso";
            this.nudPeso.Size = new System.Drawing.Size(124, 25);
            this.nudPeso.TabIndex = 10;
            // 
            // lblRaza
            // 
            this.lblRaza.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblRaza.AutoSize = true;
            this.lblRaza.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblRaza.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.lblRaza.Location = new System.Drawing.Point(12, 77);
            this.lblRaza.Margin = new System.Windows.Forms.Padding(4, 4, 4, 0);
            this.lblRaza.Name = "lblRaza";
            this.lblRaza.Size = new System.Drawing.Size(119, 19);
            this.lblRaza.TabIndex = 4;
            this.lblRaza.Text = "🐕 Raza";
            // 
            // cmbRaza
            // 
            this.cmbRaza.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbRaza.BackColor = System.Drawing.Color.White;
            this.cmbRaza.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRaza.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbRaza.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbRaza.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.cmbRaza.Location = new System.Drawing.Point(12, 105);
            this.cmbRaza.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cmbRaza.Name = "cmbRaza";
            this.cmbRaza.Size = new System.Drawing.Size(119, 25);
            this.cmbRaza.TabIndex = 5;
            // 
            // txtRaza
            // 
            this.txtRaza.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtRaza.BackColor = System.Drawing.Color.White;
            this.txtRaza.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtRaza.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtRaza.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.txtRaza.Location = new System.Drawing.Point(139, 105);
            this.txtRaza.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtRaza.MaxLength = 100;
            this.txtRaza.Name = "txtRaza";
            this.txtRaza.Size = new System.Drawing.Size(119, 25);
            this.txtRaza.TabIndex = 6;
            // 
            // lblColor
            // 
            this.lblColor.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblColor.AutoSize = true;
            this.lblColor.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblColor.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.lblColor.Location = new System.Drawing.Point(266, 77);
            this.lblColor.Margin = new System.Windows.Forms.Padding(4, 4, 4, 0);
            this.lblColor.Name = "lblColor";
            this.lblColor.Size = new System.Drawing.Size(119, 19);
            this.lblColor.TabIndex = 11;
            this.lblColor.Text = "🎨 Color";
            // 
            // txtColor
            // 
            this.txtColor.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtColor.BackColor = System.Drawing.Color.White;
            this.txtColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtColor.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtColor.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.txtColor.Location = new System.Drawing.Point(266, 105);
            this.txtColor.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtColor.MaxLength = 100;
            this.txtColor.Name = "txtColor";
            this.txtColor.Size = new System.Drawing.Size(119, 25);
            this.txtColor.TabIndex = 12;
            // 
            // chkEsterilizado
            // 
            this.chkEsterilizado.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chkEsterilizado.AutoSize = true;
            this.chkEsterilizado.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.chkEsterilizado.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.chkEsterilizado.Location = new System.Drawing.Point(393, 105);
            this.chkEsterilizado.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.chkEsterilizado.Name = "chkEsterilizado";
            this.chkEsterilizado.Size = new System.Drawing.Size(119, 23);
            this.chkEsterilizado.TabIndex = 13;
            this.chkEsterilizado.Text = "⚕️ Esterilizado";
            this.chkEsterilizado.UseVisualStyleBackColor = false;
            // 
            // lblMicrochip
            // 
            this.lblMicrochip.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblMicrochip.AutoSize = true;
            this.lblMicrochip.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblMicrochip.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.lblMicrochip.Location = new System.Drawing.Point(520, 77);
            this.lblMicrochip.Margin = new System.Windows.Forms.Padding(4, 4, 4, 0);
            this.lblMicrochip.Name = "lblMicrochip";
            this.lblMicrochip.Size = new System.Drawing.Size(119, 19);
            this.lblMicrochip.TabIndex = 17;
            this.lblMicrochip.Text = "🔍 Microchip";
            // 
            // txtMicrochip
            // 
            this.txtMicrochip.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMicrochip.BackColor = System.Drawing.Color.White;
            this.txtMicrochip.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtMicrochip.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtMicrochip.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.txtMicrochip.Location = new System.Drawing.Point(520, 105);
            this.txtMicrochip.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtMicrochip.MaxLength = 50;
            this.txtMicrochip.Name = "txtMicrochip";
            this.txtMicrochip.Size = new System.Drawing.Size(119, 25);
            this.txtMicrochip.TabIndex = 18;
            // 
            // lblPropietario
            // 
            this.lblPropietario.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPropietario.AutoSize = true;
            this.lblPropietario.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblPropietario.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(76)))), ((int)(((byte)(60)))));
            this.lblPropietario.Location = new System.Drawing.Point(12, 142);
            this.lblPropietario.Margin = new System.Windows.Forms.Padding(4, 4, 4, 0);
            this.lblPropietario.Name = "lblPropietario";
            this.lblPropietario.Size = new System.Drawing.Size(119, 19);
            this.lblPropietario.TabIndex = 14;
            this.lblPropietario.Text = "👤 Propietario *";
            // 
            // txtPropietario
            // 
            this.txtPropietario.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPropietario.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(240)))), ((int)(((byte)(241)))));
            this.txtPropietario.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPropietario.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtPropietario.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.txtPropietario.Location = new System.Drawing.Point(12, 170);
            this.txtPropietario.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtPropietario.Name = "txtPropietario";
            this.txtPropietario.ReadOnly = true;
            this.txtPropietario.Size = new System.Drawing.Size(119, 25);
            this.txtPropietario.TabIndex = 15;
            // 
            // btnBuscarPropietario
            // 
            this.btnBuscarPropietario.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.btnBuscarPropietario.FlatAppearance.BorderSize = 0;
            this.btnBuscarPropietario.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBuscarPropietario.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnBuscarPropietario.ForeColor = System.Drawing.Color.White;
            this.btnBuscarPropietario.Location = new System.Drawing.Point(393, 170);
            this.btnBuscarPropietario.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnBuscarPropietario.Name = "btnBuscarPropietario";
            this.btnBuscarPropietario.Size = new System.Drawing.Size(85, 28);
            this.btnBuscarPropietario.TabIndex = 16;
            this.btnBuscarPropietario.Text = "🔍 Buscar";
            this.btnBuscarPropietario.UseVisualStyleBackColor = false;
            this.btnBuscarPropietario.Click += new System.EventHandler(this.BtnBuscarPropietario_Click);
            // 
            // chkTieneFechaNacimiento
            // 
            this.chkTieneFechaNacimiento.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chkTieneFechaNacimiento.AutoSize = true;
            this.chkTieneFechaNacimiento.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.chkTieneFechaNacimiento.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.chkTieneFechaNacimiento.Location = new System.Drawing.Point(12, 207);
            this.chkTieneFechaNacimiento.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.chkTieneFechaNacimiento.Name = "chkTieneFechaNacimiento";
            this.chkTieneFechaNacimiento.Size = new System.Drawing.Size(119, 23);
            this.chkTieneFechaNacimiento.TabIndex = 19;
            this.chkTieneFechaNacimiento.Text = "📅 Fecha de Nacimiento";
            this.chkTieneFechaNacimiento.UseVisualStyleBackColor = false;
            // 
            // dtpFechaNacimiento
            // 
            this.dtpFechaNacimiento.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dtpFechaNacimiento.Enabled = false;
            this.dtpFechaNacimiento.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.dtpFechaNacimiento.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFechaNacimiento.Location = new System.Drawing.Point(266, 207);
            this.dtpFechaNacimiento.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dtpFechaNacimiento.Name = "dtpFechaNacimiento";
            this.dtpFechaNacimiento.Size = new System.Drawing.Size(119, 25);
            this.dtpFechaNacimiento.TabIndex = 20;
            // 
            // MascotasModule
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "MascotasModule";
            this.Size = new System.Drawing.Size(857, 574);
            this.tabControlPrincipal.ResumeLayout(false);
            this.tabInicio.ResumeLayout(false);
            this.tabConfiguraciones.ResumeLayout(false);
            this.panelBusqueda.ResumeLayout(false);
            this.panelBusqueda.PerformLayout();
            this.panelFormulario.ResumeLayout(false);
            this.panelSuperior.ResumeLayout(false);
            this.panelSuperior.PerformLayout();
            this.panelBotones.ResumeLayout(false);
            this.grpDatosMascota.ResumeLayout(false);
            this.tableLayoutMain.ResumeLayout(false);
            this.tableLayoutMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudPeso)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Label lblContador;
        private GroupBox grpDatosMascota;
        private TableLayoutPanel tableLayoutMain;
        private Label lblNombre;
        private TextBox txtNombre;
        private Label lblEspecie;
        private ComboBox cmbEspecie;
        private Label lblRaza;
        private ComboBox cmbRaza;
        private TextBox txtRaza;
        private Label lblGenero;
        private ComboBox cmbGenero;
        private Label lblPeso;
        private NumericUpDown nudPeso;
        private Label lblColor;
        private TextBox txtColor;
        private CheckBox chkEsterilizado;
        private Label lblPropietario;
        private TextBox txtPropietario;
        private Button btnBuscarPropietario;
        private Label lblMicrochip;
        private TextBox txtMicrochip;
        private CheckBox chkTieneFechaNacimiento;
        private DateTimePicker dtpFechaNacimiento;
    }
}