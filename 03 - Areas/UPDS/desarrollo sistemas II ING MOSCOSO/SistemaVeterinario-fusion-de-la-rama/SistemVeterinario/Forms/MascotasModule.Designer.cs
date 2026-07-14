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
            lblContador = new Label();
            grpDatosMascota = new GroupBox();
            lblNombre = new Label();
            txtNombre = new TextBox();
            lblEspecie = new Label();
            cmbEspecie = new ComboBox();
            lblRaza = new Label();
            cmbRaza = new ComboBox();
            txtRaza = new TextBox();
            lblGenero = new Label();
            cmbGenero = new ComboBox();
            lblPeso = new Label();
            nudPeso = new NumericUpDown();
            lblColor = new Label();
            txtColor = new TextBox();
            chkEsterilizado = new CheckBox();
            lblPropietario = new Label();
            txtPropietario = new TextBox();
            btnBuscarPropietario = new Button();
            lblMicrochip = new Label();
            txtMicrochip = new TextBox();
            chkTieneFechaNacimiento = new CheckBox();
            dtpFechaNacimiento = new DateTimePicker();
            tabInicio.SuspendLayout();
            panelBusqueda.SuspendLayout();
            tabConfiguraciones.SuspendLayout();
            panelFormulario.SuspendLayout();
            panelSuperior.SuspendLayout();
            panelBotones.SuspendLayout();
            grpDatosMascota.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudPeso).BeginInit();
            SuspendLayout();
            // 
            // tabInicio
            // 
            tabInicio.Text = "Gestión de Mascotas";
            // 
            // tabConfiguraciones
            // 
            tabConfiguraciones.Text = "Configuración de Mascota";
            // 
            // panelBusqueda
            // 
            panelBusqueda.Controls.Add(lblContador);
            panelBusqueda.Size = new Size(960, 80);
            // 
            // txtBuscar
            // 
            txtBuscar.PlaceholderText = "Buscar por nombre, especie, raza, propietario...";
            txtBuscar.Size = new Size(350, 23);
            // 
            // btnBuscar
            // 
            btnBuscar.Location = new Point(360, 17);
            // 
            // btnNuevo
            // 
            btnNuevo.Location = new Point(860, 17);
            // 
            // chkMostrarTodo
            // 
            chkMostrarTodo.Location = new Point(470, 20);
            // 
            // panelFormulario
            // 
            panelFormulario.Controls.Add(grpDatosMascota);
            // 
            // panelSuperior
            // 
            panelSuperior.Size = new Size(992, 50);
            // 
            // lblContador
            // 
            lblContador.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblContador.AutoSize = true;
            lblContador.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            lblContador.Location = new Point(750, 50);
            lblContador.Name = "lblContador";
            lblContador.Size = new Size(121, 15);
            lblContador.TabIndex = 6;
            lblContador.Text = "Total de registros: 0";
            // 
            // grpDatosMascota
            // 
            grpDatosMascota.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            grpDatosMascota.Controls.Add(dtpFechaNacimiento);
            grpDatosMascota.Controls.Add(chkTieneFechaNacimiento);
            grpDatosMascota.Controls.Add(txtMicrochip);
            grpDatosMascota.Controls.Add(lblMicrochip);
            grpDatosMascota.Controls.Add(btnBuscarPropietario);
            grpDatosMascota.Controls.Add(txtPropietario);
            grpDatosMascota.Controls.Add(lblPropietario);
            grpDatosMascota.Controls.Add(chkEsterilizado);
            grpDatosMascota.Controls.Add(txtColor);
            grpDatosMascota.Controls.Add(lblColor);
            grpDatosMascota.Controls.Add(nudPeso);
            grpDatosMascota.Controls.Add(lblPeso);
            grpDatosMascota.Controls.Add(cmbGenero);
            grpDatosMascota.Controls.Add(lblGenero);
            grpDatosMascota.Controls.Add(txtRaza);
            grpDatosMascota.Controls.Add(cmbRaza);
            grpDatosMascota.Controls.Add(lblRaza);
            grpDatosMascota.Controls.Add(cmbEspecie);
            grpDatosMascota.Controls.Add(lblEspecie);
            grpDatosMascota.Controls.Add(txtNombre);
            grpDatosMascota.Controls.Add(lblNombre);
            grpDatosMascota.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            grpDatosMascota.ForeColor = Color.DarkBlue;
            grpDatosMascota.Location = new Point(15, 65);
            grpDatosMascota.Name = "grpDatosMascota";
            grpDatosMascota.Size = new Size(960, 320);
            grpDatosMascota.TabIndex = 1;
            grpDatosMascota.TabStop = false;
            grpDatosMascota.Text = "Datos de la Mascota";
            // 
            // lblNombre
            // 
            lblNombre.AutoSize = true;
            lblNombre.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            lblNombre.ForeColor = Color.DarkRed;
            lblNombre.Location = new Point(15, 30);
            lblNombre.Name = "lblNombre";
            lblNombre.Size = new Size(59, 15);
            lblNombre.TabIndex = 0;
            lblNombre.Text = "Nombre *";
            // 
            // txtNombre
            // 
            txtNombre.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            txtNombre.ForeColor = Color.Black;
            txtNombre.BackColor = Color.White;
            txtNombre.Location = new Point(15, 50);
            txtNombre.MaxLength = 100;
            txtNombre.Name = "txtNombre";
            txtNombre.Size = new Size(200, 23);
            txtNombre.TabIndex = 1;
            // 
            // lblEspecie
            // 
            lblEspecie.AutoSize = true;
            lblEspecie.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            lblEspecie.ForeColor = Color.DarkRed;
            lblEspecie.Location = new Point(230, 30);
            lblEspecie.Name = "lblEspecie";
            lblEspecie.Size = new Size(60, 15);
            lblEspecie.TabIndex = 2;
            lblEspecie.Text = "Especie *";
            // 
            // cmbEspecie
            // 
            cmbEspecie.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbEspecie.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            cmbEspecie.ForeColor = Color.Black;
            cmbEspecie.BackColor = Color.White;
            cmbEspecie.Location = new Point(230, 50);
            cmbEspecie.Name = "cmbEspecie";
            cmbEspecie.Size = new Size(150, 23);
            cmbEspecie.TabIndex = 3;
            // 
            // lblRaza
            // 
            lblRaza.AutoSize = true;
            lblRaza.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            lblRaza.ForeColor = Color.Black;
            lblRaza.Location = new Point(15, 85);
            lblRaza.Name = "lblRaza";
            lblRaza.Size = new Size(34, 15);
            lblRaza.TabIndex = 4;
            lblRaza.Text = "Raza:";
            // 
            // cmbRaza
            // 
            cmbRaza.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbRaza.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            cmbRaza.ForeColor = Color.Black;
            cmbRaza.BackColor = Color.White;
            cmbRaza.Location = new Point(15, 105);
            cmbRaza.Name = "cmbRaza";
            cmbRaza.Size = new Size(150, 23);
            cmbRaza.TabIndex = 5;
            // 
            // txtRaza
            // 
            txtRaza.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            txtRaza.ForeColor = Color.Black;
            txtRaza.BackColor = Color.White;
            txtRaza.Location = new Point(180, 105);
            txtRaza.MaxLength = 100;
            txtRaza.Name = "txtRaza";
            txtRaza.PlaceholderText = "o escriba otra raza";
            txtRaza.Size = new Size(150, 23);
            txtRaza.TabIndex = 6;
            // 
            // lblGenero
            // 
            lblGenero.AutoSize = true;
            lblGenero.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            lblGenero.ForeColor = Color.Black;
            lblGenero.Location = new Point(400, 30);
            lblGenero.Name = "lblGenero";
            lblGenero.Size = new Size(38, 15);
            lblGenero.TabIndex = 7;
            lblGenero.Text = "Sexo:";
            // 
            // cmbGenero
            // 
            cmbGenero.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbGenero.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            cmbGenero.ForeColor = Color.Black;
            cmbGenero.BackColor = Color.White;
            cmbGenero.Location = new Point(400, 50);
            cmbGenero.Name = "cmbGenero";
            cmbGenero.Size = new Size(80, 23);
            cmbGenero.TabIndex = 8;
            // 
            // lblPeso
            // 
            lblPeso.AutoSize = true;
            lblPeso.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            lblPeso.ForeColor = Color.Black;
            lblPeso.Location = new Point(500, 30);
            lblPeso.Name = "lblPeso";
            lblPeso.Size = new Size(60, 15);
            lblPeso.TabIndex = 9;
            lblPeso.Text = "Peso (kg):";
            // 
            // nudPeso
            // 
            nudPeso.DecimalPlaces = 2;
            nudPeso.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            nudPeso.Location = new Point(500, 50);
            nudPeso.Maximum = new decimal(new int[] { 999, 0, 0, 0 });
            nudPeso.Name = "nudPeso";
            nudPeso.Size = new Size(100, 23);
            nudPeso.TabIndex = 10;
            // 
            // lblColor
            // 
            lblColor.AutoSize = true;
            lblColor.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            lblColor.ForeColor = Color.Black;
            lblColor.Location = new Point(350, 85);
            lblColor.Name = "lblColor";
            lblColor.Size = new Size(39, 15);
            lblColor.TabIndex = 11;
            lblColor.Text = "Color:";
            // 
            // txtColor
            // 
            txtColor.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            txtColor.ForeColor = Color.Black;
            txtColor.BackColor = Color.White;
            txtColor.Location = new Point(350, 105);
            txtColor.MaxLength = 100;
            txtColor.Name = "txtColor";
            txtColor.Size = new Size(150, 23);
            txtColor.TabIndex = 12;
            // 
            // chkEsterilizado
            // 
            chkEsterilizado.AutoSize = true;
            chkEsterilizado.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            chkEsterilizado.ForeColor = Color.Black;
            chkEsterilizado.Location = new Point(620, 30);
            chkEsterilizado.Name = "chkEsterilizado";
            chkEsterilizado.Size = new Size(85, 19);
            chkEsterilizado.TabIndex = 13;
            chkEsterilizado.Text = "Esterilizado";
            chkEsterilizado.UseVisualStyleBackColor = true;
            // 
            // lblPropietario
            // 
            lblPropietario.AutoSize = true;
            lblPropietario.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            lblPropietario.ForeColor = Color.DarkRed;
            lblPropietario.Location = new Point(15, 140);
            lblPropietario.Name = "lblPropietario";
            lblPropietario.Size = new Size(81, 15);
            lblPropietario.TabIndex = 14;
            lblPropietario.Text = "Propietario *";
            // 
            // txtPropietario
            // 
            txtPropietario.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            txtPropietario.ForeColor = Color.Black;
            txtPropietario.BackColor = Color.LightGray;
            txtPropietario.Location = new Point(15, 160);
            txtPropietario.Name = "txtPropietario";
            txtPropietario.ReadOnly = true;
            txtPropietario.Size = new Size(300, 23);
            txtPropietario.TabIndex = 15;
            // 
            // btnBuscarPropietario
            // 
            btnBuscarPropietario.BackColor = Color.LightBlue;
            btnBuscarPropietario.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            btnBuscarPropietario.Location = new Point(330, 160);
            btnBuscarPropietario.Name = "btnBuscarPropietario";
            btnBuscarPropietario.Size = new Size(80, 23);
            btnBuscarPropietario.TabIndex = 16;
            btnBuscarPropietario.Text = "Buscar";
            btnBuscarPropietario.UseVisualStyleBackColor = false;
            btnBuscarPropietario.Click += new EventHandler(this.BtnBuscarPropietario_Click);
            // 
            // lblMicrochip
            // 
            lblMicrochip.AutoSize = true;
            lblMicrochip.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            lblMicrochip.ForeColor = Color.Black;
            lblMicrochip.Location = new Point(430, 140);
            lblMicrochip.Name = "lblMicrochip";
            lblMicrochip.Size = new Size(66, 15);
            lblMicrochip.TabIndex = 17;
            lblMicrochip.Text = "Microchip:";
            // 
            // txtMicrochip
            // 
            txtMicrochip.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            txtMicrochip.ForeColor = Color.Black;
            txtMicrochip.BackColor = Color.White;
            txtMicrochip.Location = new Point(430, 160);
            txtMicrochip.MaxLength = 50;
            txtMicrochip.Name = "txtMicrochip";
            txtMicrochip.Size = new Size(180, 23);
            txtMicrochip.TabIndex = 18;
            // 
            // chkTieneFechaNacimiento
            // 
            chkTieneFechaNacimiento.AutoSize = true;
            chkTieneFechaNacimiento.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            chkTieneFechaNacimiento.ForeColor = Color.Black;
            chkTieneFechaNacimiento.Location = new Point(15, 200);
            chkTieneFechaNacimiento.Name = "chkTieneFechaNacimiento";
            chkTieneFechaNacimiento.Size = new Size(148, 19);
            chkTieneFechaNacimiento.TabIndex = 19;
            chkTieneFechaNacimiento.Text = "Fecha de Nacimiento:";
            chkTieneFechaNacimiento.UseVisualStyleBackColor = true;
            // 
            // dtpFechaNacimiento
            // 
            dtpFechaNacimiento.Enabled = false;
            dtpFechaNacimiento.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            dtpFechaNacimiento.Format = DateTimePickerFormat.Short;
            dtpFechaNacimiento.Location = new Point(180, 198);
            dtpFechaNacimiento.Name = "dtpFechaNacimiento";
            dtpFechaNacimiento.Size = new Size(120, 23);
            dtpFechaNacimiento.TabIndex = 20;
            // 
            // panelBotones
            // 
            panelBotones.Location = new Point(0, 490);
            // 
            // MascotasModule
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Name = "MascotasModule";
            tabInicio.ResumeLayout(false);
            panelBusqueda.ResumeLayout(false);
            panelBusqueda.PerformLayout();
            tabConfiguraciones.ResumeLayout(false);
            panelFormulario.ResumeLayout(false);
            panelSuperior.ResumeLayout(false);
            panelSuperior.PerformLayout();
            panelBotones.ResumeLayout(false);
            grpDatosMascota.ResumeLayout(false);
            grpDatosMascota.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nudPeso).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Label lblContador;
        private GroupBox grpDatosMascota;
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