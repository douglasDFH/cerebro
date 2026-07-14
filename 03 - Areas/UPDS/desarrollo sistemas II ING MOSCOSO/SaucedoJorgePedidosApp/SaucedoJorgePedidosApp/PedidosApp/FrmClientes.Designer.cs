namespace PedidosApp
{
    partial class FrmClientes
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
            this.tabControlClientes = new System.Windows.Forms.TabControl();
            this.tabListado = new System.Windows.Forms.TabPage();
            this.cbBuscar = new System.Windows.Forms.ComboBox();
            this.dataGridViewStaffs = new System.Windows.Forms.DataGridView();
            this.lblTotal = new System.Windows.Forms.Label();
            this.chkEliminar = new System.Windows.Forms.CheckBox();
            this.btnImprimir = new FontAwesome.Sharp.IconButton();
            this.btnEliminar = new FontAwesome.Sharp.IconButton();
            this.iconButton1 = new FontAwesome.Sharp.IconButton();
            this.btnBuscar = new FontAwesome.Sharp.IconButton();
            this.txtBuscar = new System.Windows.Forms.TextBox();
            this.lblBuscar = new System.Windows.Forms.Label();
            this.tabMantenimiento = new System.Windows.Forms.TabPage();
            this.groupBoxUsuarios = new System.Windows.Forms.GroupBox();
            this.chkActive = new System.Windows.Forms.CheckBox();
            this.txtPhone = new System.Windows.Forms.TextBox();
            this.btnCancelar = new FontAwesome.Sharp.IconButton();
            this.btnEditar = new FontAwesome.Sharp.IconButton();
            this.btnGuardar = new FontAwesome.Sharp.IconButton();
            this.btnNuevo = new FontAwesome.Sharp.IconButton();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.txtLastName = new System.Windows.Forms.TextBox();
            this.txtFirstName = new System.Windows.Forms.TextBox();
            this.txtStaffId = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.lblGenero = new System.Windows.Forms.Label();
            this.lblApellidos = new System.Windows.Forms.Label();
            this.lblNombre = new System.Windows.Forms.Label();
            this.lblCodigo = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.errorIcono = new System.Windows.Forms.ErrorProvider(this.components);
            this.ttMensaje = new System.Windows.Forms.ToolTip(this.components);
            this.tabControlClientes.SuspendLayout();
            this.tabListado.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewStaffs)).BeginInit();
            this.tabMantenimiento.SuspendLayout();
            this.groupBoxUsuarios.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorIcono)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControlClientes
            // 
            this.tabControlClientes.Controls.Add(this.tabListado);
            this.tabControlClientes.Controls.Add(this.tabMantenimiento);
            this.tabControlClientes.Location = new System.Drawing.Point(0, -1);
            this.tabControlClientes.Name = "tabControlClientes";
            this.tabControlClientes.SelectedIndex = 0;
            this.tabControlClientes.Size = new System.Drawing.Size(994, 482);
            this.tabControlClientes.TabIndex = 0;
            // 
            // tabListado
            // 
            this.tabListado.Controls.Add(this.cbBuscar);
            this.tabListado.Controls.Add(this.dataGridViewStaffs);
            this.tabListado.Controls.Add(this.lblTotal);
            this.tabListado.Controls.Add(this.chkEliminar);
            this.tabListado.Controls.Add(this.btnImprimir);
            this.tabListado.Controls.Add(this.btnEliminar);
            this.tabListado.Controls.Add(this.iconButton1);
            this.tabListado.Controls.Add(this.btnBuscar);
            this.tabListado.Controls.Add(this.txtBuscar);
            this.tabListado.Controls.Add(this.lblBuscar);
            this.tabListado.Location = new System.Drawing.Point(4, 25);
            this.tabListado.Name = "tabListado";
            this.tabListado.Padding = new System.Windows.Forms.Padding(3);
            this.tabListado.Size = new System.Drawing.Size(986, 453);
            this.tabListado.TabIndex = 0;
            this.tabListado.Text = "Listado";
            this.tabListado.UseVisualStyleBackColor = true;
            // 
            // cbBuscar
            // 
            this.cbBuscar.FormattingEnabled = true;
            this.cbBuscar.Items.AddRange(new object[] {
            "Documento"});
            this.cbBuscar.Location = new System.Drawing.Point(22, 35);
            this.cbBuscar.Name = "cbBuscar";
            this.cbBuscar.Size = new System.Drawing.Size(121, 24);
            this.cbBuscar.TabIndex = 9;
            this.cbBuscar.SelectedIndexChanged += new System.EventHandler(this.cbBuscar_SelectedIndexChanged);
            // 
            // dataGridViewStaffs
            // 
            this.dataGridViewStaffs.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewStaffs.Location = new System.Drawing.Point(8, 118);
            this.dataGridViewStaffs.Name = "dataGridViewStaffs";
            this.dataGridViewStaffs.RowHeadersWidth = 51;
            this.dataGridViewStaffs.RowTemplate.Height = 24;
            this.dataGridViewStaffs.Size = new System.Drawing.Size(966, 327);
            this.dataGridViewStaffs.TabIndex = 8;
            this.dataGridViewStaffs.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewStaffs_CellContentClick);
            // 
            // lblTotal
            // 
            this.lblTotal.AutoSize = true;
            this.lblTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotal.Location = new System.Drawing.Point(609, 86);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(199, 18);
            this.lblTotal.TabIndex = 7;
            this.lblTotal.Text = "Registros encontrados: 0";
            this.lblTotal.Click += new System.EventHandler(this.lblTotal_Click);
            // 
            // chkEliminar
            // 
            this.chkEliminar.AutoSize = true;
            this.chkEliminar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkEliminar.Location = new System.Drawing.Point(22, 80);
            this.chkEliminar.Name = "chkEliminar";
            this.chkEliminar.Size = new System.Drawing.Size(91, 22);
            this.chkEliminar.TabIndex = 6;
            this.chkEliminar.Text = "Eliminar";
            this.chkEliminar.UseVisualStyleBackColor = true;
            this.chkEliminar.CheckedChanged += new System.EventHandler(this.chkEliminar_CheckedChanged);
            // 
            // btnImprimir
            // 
            this.btnImprimir.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnImprimir.IconChar = FontAwesome.Sharp.IconChar.None;
            this.btnImprimir.IconColor = System.Drawing.Color.Black;
            this.btnImprimir.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnImprimir.Location = new System.Drawing.Point(845, 32);
            this.btnImprimir.Name = "btnImprimir";
            this.btnImprimir.Size = new System.Drawing.Size(106, 31);
            this.btnImprimir.TabIndex = 5;
            this.btnImprimir.Text = "Imprimir";
            this.btnImprimir.UseVisualStyleBackColor = true;
            this.btnImprimir.Click += new System.EventHandler(this.btnImprimir_Click);
            // 
            // btnEliminar
            // 
            this.btnEliminar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEliminar.IconChar = FontAwesome.Sharp.IconChar.None;
            this.btnEliminar.IconColor = System.Drawing.Color.Black;
            this.btnEliminar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnEliminar.Location = new System.Drawing.Point(702, 31);
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Size = new System.Drawing.Size(106, 31);
            this.btnEliminar.TabIndex = 4;
            this.btnEliminar.Text = "Eliminar";
            this.btnEliminar.UseVisualStyleBackColor = true;
            this.btnEliminar.Click += new System.EventHandler(this.btnEliminar_Click);
            // 
            // iconButton1
            // 
            this.iconButton1.IconChar = FontAwesome.Sharp.IconChar.None;
            this.iconButton1.IconColor = System.Drawing.Color.Black;
            this.iconButton1.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.iconButton1.Location = new System.Drawing.Point(733, 38);
            this.iconButton1.Name = "iconButton1";
            this.iconButton1.Size = new System.Drawing.Size(8, 8);
            this.iconButton1.TabIndex = 3;
            this.iconButton1.Text = "iconButton1";
            this.iconButton1.UseVisualStyleBackColor = true;
            // 
            // btnBuscar
            // 
            this.btnBuscar.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBuscar.IconChar = FontAwesome.Sharp.IconChar.None;
            this.btnBuscar.IconColor = System.Drawing.Color.Black;
            this.btnBuscar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnBuscar.Location = new System.Drawing.Point(572, 30);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(106, 31);
            this.btnBuscar.TabIndex = 2;
            this.btnBuscar.Text = "Buscar";
            this.btnBuscar.UseVisualStyleBackColor = true;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // txtBuscar
            // 
            this.txtBuscar.Location = new System.Drawing.Point(328, 35);
            this.txtBuscar.Name = "txtBuscar";
            this.txtBuscar.Size = new System.Drawing.Size(224, 22);
            this.txtBuscar.TabIndex = 1;
            this.txtBuscar.TextChanged += new System.EventHandler(this.txtBuscar_TextChanged);
            // 
            // lblBuscar
            // 
            this.lblBuscar.AutoSize = true;
            this.lblBuscar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBuscar.Location = new System.Drawing.Point(196, 38);
            this.lblBuscar.Name = "lblBuscar";
            this.lblBuscar.Size = new System.Drawing.Size(126, 18);
            this.lblBuscar.TabIndex = 0;
            this.lblBuscar.Text = "Ingrese criterio:";
            // 
            // tabMantenimiento
            // 
            this.tabMantenimiento.Controls.Add(this.groupBoxUsuarios);
            this.tabMantenimiento.Controls.Add(this.label1);
            this.tabMantenimiento.Location = new System.Drawing.Point(4, 25);
            this.tabMantenimiento.Name = "tabMantenimiento";
            this.tabMantenimiento.Padding = new System.Windows.Forms.Padding(3);
            this.tabMantenimiento.Size = new System.Drawing.Size(986, 453);
            this.tabMantenimiento.TabIndex = 1;
            this.tabMantenimiento.Text = "Mantenimiento\n\n";
            this.tabMantenimiento.UseVisualStyleBackColor = true;
            // 
            // groupBoxUsuarios
            // 
            this.groupBoxUsuarios.Controls.Add(this.chkActive);
            this.groupBoxUsuarios.Controls.Add(this.txtPhone);
            this.groupBoxUsuarios.Controls.Add(this.btnCancelar);
            this.groupBoxUsuarios.Controls.Add(this.btnEditar);
            this.groupBoxUsuarios.Controls.Add(this.btnGuardar);
            this.groupBoxUsuarios.Controls.Add(this.btnNuevo);
            this.groupBoxUsuarios.Controls.Add(this.txtEmail);
            this.groupBoxUsuarios.Controls.Add(this.txtLastName);
            this.groupBoxUsuarios.Controls.Add(this.txtFirstName);
            this.groupBoxUsuarios.Controls.Add(this.txtStaffId);
            this.groupBoxUsuarios.Controls.Add(this.label5);
            this.groupBoxUsuarios.Controls.Add(this.lblGenero);
            this.groupBoxUsuarios.Controls.Add(this.lblApellidos);
            this.groupBoxUsuarios.Controls.Add(this.lblNombre);
            this.groupBoxUsuarios.Controls.Add(this.lblCodigo);
            this.groupBoxUsuarios.Location = new System.Drawing.Point(8, 66);
            this.groupBoxUsuarios.Name = "groupBoxUsuarios";
            this.groupBoxUsuarios.Size = new System.Drawing.Size(966, 379);
            this.groupBoxUsuarios.TabIndex = 66;
            this.groupBoxUsuarios.TabStop = false;
            this.groupBoxUsuarios.Text = "Usuario";
            this.groupBoxUsuarios.Enter += new System.EventHandler(this.groupBoxUsuarios_Enter);
            // 
            // chkActive
            // 
            this.chkActive.AutoSize = true;
            this.chkActive.Location = new System.Drawing.Point(170, 191);
            this.chkActive.Name = "chkActive";
            this.chkActive.Size = new System.Drawing.Size(69, 20);
            this.chkActive.TabIndex = 90;
            this.chkActive.Text = "Activo:";
            this.chkActive.UseVisualStyleBackColor = true;
            this.chkActive.CheckedChanged += new System.EventHandler(this.chkActive_CheckedChanged);
            // 
            // txtPhone
            // 
            this.txtPhone.Location = new System.Drawing.Point(518, 140);
            this.txtPhone.Name = "txtPhone";
            this.txtPhone.Size = new System.Drawing.Size(199, 22);
            this.txtPhone.TabIndex = 89;
            this.txtPhone.TextChanged += new System.EventHandler(this.txtPhone_TextChanged);
            // 
            // btnCancelar
            // 
            this.btnCancelar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancelar.IconChar = FontAwesome.Sharp.IconChar.X;
            this.btnCancelar.IconColor = System.Drawing.Color.Black;
            this.btnCancelar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnCancelar.IconSize = 30;
            this.btnCancelar.Location = new System.Drawing.Point(690, 279);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(133, 44);
            this.btnCancelar.TabIndex = 85;
            this.btnCancelar.Text = "&Cancelar";
            this.btnCancelar.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancelar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // btnEditar
            // 
            this.btnEditar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEditar.IconChar = FontAwesome.Sharp.IconChar.Pencil;
            this.btnEditar.IconColor = System.Drawing.Color.Black;
            this.btnEditar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnEditar.IconSize = 30;
            this.btnEditar.Location = new System.Drawing.Point(495, 279);
            this.btnEditar.Name = "btnEditar";
            this.btnEditar.Size = new System.Drawing.Size(133, 44);
            this.btnEditar.TabIndex = 84;
            this.btnEditar.Text = "&Editar";
            this.btnEditar.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnEditar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnEditar.UseVisualStyleBackColor = true;
            this.btnEditar.Click += new System.EventHandler(this.btnEditar_Click);
            // 
            // btnGuardar
            // 
            this.btnGuardar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGuardar.IconChar = FontAwesome.Sharp.IconChar.FloppyDisk;
            this.btnGuardar.IconColor = System.Drawing.Color.Black;
            this.btnGuardar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnGuardar.IconSize = 30;
            this.btnGuardar.Location = new System.Drawing.Point(286, 279);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(133, 44);
            this.btnGuardar.TabIndex = 83;
            this.btnGuardar.Text = "&Guardar";
            this.btnGuardar.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnGuardar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnGuardar.UseVisualStyleBackColor = true;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // btnNuevo
            // 
            this.btnNuevo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNuevo.IconChar = FontAwesome.Sharp.IconChar.PlusCircle;
            this.btnNuevo.IconColor = System.Drawing.Color.Black;
            this.btnNuevo.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnNuevo.IconSize = 30;
            this.btnNuevo.Location = new System.Drawing.Point(85, 279);
            this.btnNuevo.Name = "btnNuevo";
            this.btnNuevo.Size = new System.Drawing.Size(133, 44);
            this.btnNuevo.TabIndex = 82;
            this.btnNuevo.Text = "&Nuevo";
            this.btnNuevo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNuevo.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnNuevo.UseVisualStyleBackColor = true;
            this.btnNuevo.Click += new System.EventHandler(this.btnNuevo_Click);
            // 
            // txtEmail
            // 
            this.txtEmail.Location = new System.Drawing.Point(173, 140);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(269, 22);
            this.txtEmail.TabIndex = 77;
            this.txtEmail.TextChanged += new System.EventHandler(this.txtEmail_TextChanged);
            // 
            // txtLastName
            // 
            this.txtLastName.Location = new System.Drawing.Point(518, 86);
            this.txtLastName.Name = "txtLastName";
            this.txtLastName.Size = new System.Drawing.Size(269, 22);
            this.txtLastName.TabIndex = 76;
            this.txtLastName.TextChanged += new System.EventHandler(this.txtLastName_TextChanged);
            // 
            // txtFirstName
            // 
            this.txtFirstName.Location = new System.Drawing.Point(170, 86);
            this.txtFirstName.Name = "txtFirstName";
            this.txtFirstName.Size = new System.Drawing.Size(241, 22);
            this.txtFirstName.TabIndex = 75;
            this.txtFirstName.TextChanged += new System.EventHandler(this.txtFirstName_TextChanged);
            // 
            // txtStaffId
            // 
            this.txtStaffId.Location = new System.Drawing.Point(170, 49);
            this.txtStaffId.Name = "txtStaffId";
            this.txtStaffId.ReadOnly = true;
            this.txtStaffId.Size = new System.Drawing.Size(77, 22);
            this.txtStaffId.TabIndex = 74;
            this.txtStaffId.TextChanged += new System.EventHandler(this.txtStaffId_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(103, 140);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(48, 16);
            this.label5.TabIndex = 70;
            this.label5.Text = "E-mail:";
            this.label5.Click += new System.EventHandler(this.label5_Click);
            // 
            // lblGenero
            // 
            this.lblGenero.AutoSize = true;
            this.lblGenero.Location = new System.Drawing.Point(448, 145);
            this.lblGenero.Name = "lblGenero";
            this.lblGenero.Size = new System.Drawing.Size(64, 16);
            this.lblGenero.TabIndex = 69;
            this.lblGenero.Text = "Telefono:";
            this.lblGenero.Click += new System.EventHandler(this.lblGenero_Click);
            // 
            // lblApellidos
            // 
            this.lblApellidos.AutoSize = true;
            this.lblApellidos.Location = new System.Drawing.Point(452, 92);
            this.lblApellidos.Name = "lblApellidos";
            this.lblApellidos.Size = new System.Drawing.Size(60, 16);
            this.lblApellidos.TabIndex = 68;
            this.lblApellidos.Text = "Apellido:";
            this.lblApellidos.Click += new System.EventHandler(this.lblApellidos_Click);
            // 
            // lblNombre
            // 
            this.lblNombre.AutoSize = true;
            this.lblNombre.Location = new System.Drawing.Point(105, 86);
            this.lblNombre.Name = "lblNombre";
            this.lblNombre.Size = new System.Drawing.Size(59, 16);
            this.lblNombre.TabIndex = 67;
            this.lblNombre.Text = "Nombre:";
            this.lblNombre.Click += new System.EventHandler(this.lblNombre_Click);
            // 
            // lblCodigo
            // 
            this.lblCodigo.AutoSize = true;
            this.lblCodigo.Location = new System.Drawing.Point(105, 55);
            this.lblCodigo.Name = "lblCodigo";
            this.lblCodigo.Size = new System.Drawing.Size(54, 16);
            this.lblCodigo.TabIndex = 66;
            this.lblCodigo.Text = "Codigo:";
            this.lblCodigo.Click += new System.EventHandler(this.lblCodigo_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label1.Location = new System.Drawing.Point(304, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(393, 36);
            this.label1.TabIndex = 63;
            this.label1.Text = "REGISTRO DE CLIENTES";
            // 
            // errorIcono
            // 
            this.errorIcono.ContainerControl = this;
            // 
            // ttMensaje
            // 
            this.ttMensaje.IsBalloon = true;
            // 
            // FrmClientes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(990, 481);
            this.Controls.Add(this.tabControlClientes);
            this.Name = "FrmClientes";
            this.Text = "Clientes";
            this.tabControlClientes.ResumeLayout(false);
            this.tabListado.ResumeLayout(false);
            this.tabListado.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewStaffs)).EndInit();
            this.tabMantenimiento.ResumeLayout(false);
            this.tabMantenimiento.PerformLayout();
            this.groupBoxUsuarios.ResumeLayout(false);
            this.groupBoxUsuarios.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorIcono)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControlClientes;
        private System.Windows.Forms.TabPage tabListado;
        private System.Windows.Forms.Label lblBuscar;
        private FontAwesome.Sharp.IconButton btnBuscar;
        private System.Windows.Forms.TextBox txtBuscar;
        private System.Windows.Forms.CheckBox chkEliminar;
        private FontAwesome.Sharp.IconButton btnImprimir;
        private FontAwesome.Sharp.IconButton btnEliminar;
        private FontAwesome.Sharp.IconButton iconButton1;
        private System.Windows.Forms.DataGridView dataGridViewStaffs;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.TabPage tabMantenimiento;
        private System.Windows.Forms.ComboBox cbBuscar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBoxUsuarios;
        private System.Windows.Forms.CheckBox chkActive;
        private System.Windows.Forms.TextBox txtPhone;
        private FontAwesome.Sharp.IconButton btnCancelar;
        private FontAwesome.Sharp.IconButton btnEditar;
        private FontAwesome.Sharp.IconButton btnGuardar;
        private FontAwesome.Sharp.IconButton btnNuevo;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.TextBox txtLastName;
        private System.Windows.Forms.TextBox txtFirstName;
        private System.Windows.Forms.TextBox txtStaffId;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblGenero;
        private System.Windows.Forms.Label lblApellidos;
        private System.Windows.Forms.Label lblNombre;
        private System.Windows.Forms.Label lblCodigo;
        private System.Windows.Forms.ErrorProvider errorIcono;
        private System.Windows.Forms.ToolTip ttMensaje;
    }
}