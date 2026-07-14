namespace SistemaZoofiPets
{
    partial class CapaPresentacion
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

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.pictureBoxFondo = new System.Windows.Forms.PictureBox();
            this.panelLogin = new System.Windows.Forms.Panel();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.lblSubtitulo = new System.Windows.Forms.Label();
            this.panelUsuario = new System.Windows.Forms.Panel();
            this.txt_usuario = new System.Windows.Forms.TextBox();
            this.lblUsuario = new System.Windows.Forms.Label();
            this.panelPassword = new System.Windows.Forms.Panel();
            this.txt_contrasena = new System.Windows.Forms.TextBox();
            this.lblPassword = new System.Windows.Forms.Label();
            this.btn_ingresar = new System.Windows.Forms.Button();
            this.btn_salir = new System.Windows.Forms.Button();
            this.lblFooter = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxFondo)).BeginInit();
            this.panelLogin.SuspendLayout();
            this.panelUsuario.SuspendLayout();
            this.panelPassword.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBoxFondo
            // 
            this.pictureBoxFondo.Dock = System.Windows.Forms.DockStyle.Fill;
            // Cargar imagen de fondo
            this.CargarImagenFondo();
            this.pictureBoxFondo.Location = new System.Drawing.Point(0, 0);
            this.pictureBoxFondo.Name = "pictureBoxFondo";
            this.pictureBoxFondo.Size = new System.Drawing.Size(1200, 700);
            this.pictureBoxFondo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxFondo.TabIndex = 0;
            this.pictureBoxFondo.TabStop = false;
            // 
            // panelLogin
            // 
            this.panelLogin.BackColor = ColoresPastelGlobales.Login.PanelLogin;
            this.panelLogin.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelLogin.Controls.Add(this.lblFooter);
            this.panelLogin.Controls.Add(this.btn_salir);
            this.panelLogin.Controls.Add(this.btn_ingresar);
            this.panelLogin.Controls.Add(this.panelPassword);
            this.panelLogin.Controls.Add(this.panelUsuario);
            this.panelLogin.Controls.Add(this.lblSubtitulo);
            this.panelLogin.Controls.Add(this.lblTitulo);
            this.panelLogin.Location = new System.Drawing.Point(750, 120);
            this.panelLogin.Name = "panelLogin";
            this.panelLogin.Size = new System.Drawing.Size(400, 460);
            this.panelLogin.TabIndex = 1;
            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitulo.ForeColor = ColoresPastelGlobales.Login.TextoTitulo;
            this.lblTitulo.Location = new System.Drawing.Point(90, 30);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(220, 45);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "Bienvenido";
            this.lblTitulo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblSubtitulo
            // 
            this.lblSubtitulo.AutoSize = true;
            this.lblSubtitulo.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSubtitulo.ForeColor = ColoresPastelGlobales.Login.TextoSubtitulo;
            this.lblSubtitulo.Location = new System.Drawing.Point(94, 75);
            this.lblSubtitulo.Name = "lblSubtitulo";
            this.lblSubtitulo.Size = new System.Drawing.Size(212, 21);
            this.lblSubtitulo.TabIndex = 1;
            this.lblSubtitulo.Text = "Clínica Veterinaria ZoofiPets";
            this.lblSubtitulo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panelUsuario
            // 
            this.panelUsuario.BackColor = ColoresPastelGlobales.Login.CamposInput;
            this.panelUsuario.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelUsuario.Controls.Add(this.txt_usuario);
            this.panelUsuario.Controls.Add(this.lblUsuario);
            this.panelUsuario.Location = new System.Drawing.Point(40, 130);
            this.panelUsuario.Name = "panelUsuario";
            this.panelUsuario.Size = new System.Drawing.Size(320, 70);
            this.panelUsuario.TabIndex = 2;
            // 
            // lblUsuario
            // 
            this.lblUsuario.AutoSize = true;
            this.lblUsuario.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUsuario.ForeColor = ColoresPastelGlobales.Login.EtiquetasCampos;
            this.lblUsuario.Location = new System.Drawing.Point(15, 8);
            this.lblUsuario.Name = "lblUsuario";
            this.lblUsuario.Size = new System.Drawing.Size(138, 19);
            this.lblUsuario.TabIndex = 0;
            this.lblUsuario.Text = "👤 Nombre Usuario";
            // 
            // txt_usuario
            // 
            this.txt_usuario.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txt_usuario.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_usuario.ForeColor = ColoresPastelGlobales.NegroCarbon;
            this.txt_usuario.Location = new System.Drawing.Point(19, 35);
            this.txt_usuario.Name = "txt_usuario";
            this.txt_usuario.Size = new System.Drawing.Size(280, 22);
            this.txt_usuario.TabIndex = 1;
            this.txt_usuario.Text = "admin";
            // 
            // panelPassword
            // 
            this.panelPassword.BackColor = ColoresPastelGlobales.Login.CamposInput;
            this.panelPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelPassword.Controls.Add(this.txt_contrasena);
            this.panelPassword.Controls.Add(this.lblPassword);
            this.panelPassword.Location = new System.Drawing.Point(40, 220);
            this.panelPassword.Name = "panelPassword";
            this.panelPassword.Size = new System.Drawing.Size(320, 70);
            this.panelPassword.TabIndex = 3;
            // 
            // lblPassword
            // 
            this.lblPassword.AutoSize = true;
            this.lblPassword.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPassword.ForeColor = ColoresPastelGlobales.Login.EtiquetasCampos;
            this.lblPassword.Location = new System.Drawing.Point(15, 8);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(113, 19);
            this.lblPassword.TabIndex = 0;
            this.lblPassword.Text = "🔒 Contraseña";
            // 
            // txt_contrasena
            // 
            this.txt_contrasena.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txt_contrasena.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_contrasena.ForeColor = ColoresPastelGlobales.NegroCarbon;
            this.txt_contrasena.Location = new System.Drawing.Point(19, 35);
            this.txt_contrasena.Name = "txt_contrasena";
            this.txt_contrasena.Size = new System.Drawing.Size(280, 22);
            this.txt_contrasena.TabIndex = 1;
            this.txt_contrasena.Text = "123";
            this.txt_contrasena.UseSystemPasswordChar = true;
            // 
            // btn_ingresar
            // 
            this.btn_ingresar.BackColor = ColoresPastelGlobales.Login.BotonIngresar;
            this.btn_ingresar.FlatAppearance.BorderSize = 0;
            this.btn_ingresar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_ingresar.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_ingresar.ForeColor = ColoresPastelGlobales.Login.TextoBotones;
            this.btn_ingresar.Location = new System.Drawing.Point(40, 320);
            this.btn_ingresar.Name = "btn_ingresar";
            this.btn_ingresar.Size = new System.Drawing.Size(150, 45);
            this.btn_ingresar.TabIndex = 4;
            this.btn_ingresar.Text = "🚀 Ingresar";
            this.btn_ingresar.UseVisualStyleBackColor = false;
            this.btn_ingresar.Click += new System.EventHandler(this.btn_ingresar_Click);
            // 
            // btn_salir
            // 
            this.btn_salir.BackColor = ColoresPastelGlobales.Login.BotonSalir;
            this.btn_salir.FlatAppearance.BorderSize = 0;
            this.btn_salir.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_salir.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_salir.ForeColor = ColoresPastelGlobales.Login.TextoBotones;
            this.btn_salir.Location = new System.Drawing.Point(210, 320);
            this.btn_salir.Name = "btn_salir";
            this.btn_salir.Size = new System.Drawing.Size(150, 45);
            this.btn_salir.TabIndex = 5;
            this.btn_salir.Text = "❌ Salir";
            this.btn_salir.UseVisualStyleBackColor = false;
            this.btn_salir.Click += new System.EventHandler(this.btn_salir_Click);
            // 
            // lblFooter
            // 
            this.lblFooter.AutoSize = true;
            this.lblFooter.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFooter.ForeColor = ColoresPastelGlobales.GrisVioletaMedio;
            this.lblFooter.Location = new System.Drawing.Point(120, 385);
            this.lblFooter.Name = "lblFooter";
            this.lblFooter.Size = new System.Drawing.Size(160, 26);
            this.lblFooter.TabIndex = 6;
            this.lblFooter.Text = "© 2025 ZoofiPets v1.0\r\nTodos los derechos reservados";
            this.lblFooter.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // CapaPresentacion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1200, 700);
            this.Controls.Add(this.panelLogin);
            this.Controls.Add(this.pictureBoxFondo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "CapaPresentacion";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ZoofiPets - Login";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxFondo)).EndInit();
            this.panelLogin.ResumeLayout(false);
            this.panelLogin.PerformLayout();
            this.panelUsuario.ResumeLayout(false);
            this.panelUsuario.PerformLayout();
            this.panelPassword.ResumeLayout(false);
            this.panelPassword.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBoxFondo;
        private System.Windows.Forms.Panel panelLogin;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Label lblSubtitulo;
        private System.Windows.Forms.Panel panelUsuario;
        private System.Windows.Forms.TextBox txt_usuario;
        private System.Windows.Forms.Label lblUsuario;
        private System.Windows.Forms.Panel panelPassword;
        private System.Windows.Forms.TextBox txt_contrasena;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.Button btn_ingresar;
        private System.Windows.Forms.Button btn_salir;
        private System.Windows.Forms.Label lblFooter;
    }
}

