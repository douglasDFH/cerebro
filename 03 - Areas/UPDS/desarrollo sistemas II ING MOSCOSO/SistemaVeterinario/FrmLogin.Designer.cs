namespace VeterinariaApp
{
    partial class FrmLogin
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

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.panelLogin = new System.Windows.Forms.Panel();
            this.linkOlvideClave = new System.Windows.Forms.LinkLabel();
            this.btnMostrarClave = new System.Windows.Forms.Button();
            this.lblError = new System.Windows.Forms.Label();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnLogin = new System.Windows.Forms.Button();
            this.txtClave = new System.Windows.Forms.TextBox();
            this.txtUsuario = new System.Windows.Forms.TextBox();
            this.lblClave = new System.Windows.Forms.Label();
            this.lblUsuario = new System.Windows.Forms.Label();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.panelHeader = new System.Windows.Forms.Panel();
            this.btnMinimizar = new System.Windows.Forms.Button();
            this.btnCerrar = new System.Windows.Forms.Button();
            this.lblSistema = new System.Windows.Forms.Label();
            this.pictureBoxLogo = new System.Windows.Forms.PictureBox();
            this.panelLogin.SuspendLayout();
            this.panelHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLogo)).BeginInit();
            this.SuspendLayout();
            // 
            // panelLogin
            // 
            this.panelLogin.BackColor = System.Drawing.Color.White;
            this.panelLogin.Controls.Add(this.linkOlvideClave);
            this.panelLogin.Controls.Add(this.btnMostrarClave);
            this.panelLogin.Controls.Add(this.lblError);
            this.panelLogin.Controls.Add(this.btnCancelar);
            this.panelLogin.Controls.Add(this.btnLogin);
            this.panelLogin.Controls.Add(this.txtClave);
            this.panelLogin.Controls.Add(this.txtUsuario);
            this.panelLogin.Controls.Add(this.lblClave);
            this.panelLogin.Controls.Add(this.lblUsuario);
            this.panelLogin.Controls.Add(this.lblTitulo);
            this.panelLogin.Location = new System.Drawing.Point(1, 70);
            this.panelLogin.Name = "panelLogin";
            this.panelLogin.Size = new System.Drawing.Size(400, 330);
            this.panelLogin.TabIndex = 0;
            // 
            // linkOlvideClave
            // 
            this.linkOlvideClave.AutoSize = true;
            this.linkOlvideClave.LinkColor = System.Drawing.Color.SteelBlue;
            this.linkOlvideClave.Location = new System.Drawing.Point(140, 250);
            this.linkOlvideClave.Name = "linkOlvideClave";
            this.linkOlvideClave.Size = new System.Drawing.Size(120, 13);
            this.linkOlvideClave.TabIndex = 5;
            this.linkOlvideClave.TabStop = true;
            this.linkOlvideClave.Text = "¿Olvidó su contraseña?";
            this.linkOlvideClave.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkOlvideClave_LinkClicked);
            // 
            // btnMostrarClave
            // 
            this.btnMostrarClave.BackColor = System.Drawing.Color.Transparent;
            this.btnMostrarClave.FlatAppearance.BorderSize = 0;
            this.btnMostrarClave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMostrarClave.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.btnMostrarClave.Location = new System.Drawing.Point(320, 130);
            this.btnMostrarClave.Name = "btnMostrarClave";
            this.btnMostrarClave.Size = new System.Drawing.Size(30, 25);
            this.btnMostrarClave.TabIndex = 6;
            this.btnMostrarClave.Text = "👁️";
            this.btnMostrarClave.UseVisualStyleBackColor = false;
            this.btnMostrarClave.Click += new System.EventHandler(this.btnMostrarClave_Click);
            // 
            // lblError
            // 
            this.lblError.AutoSize = true;
            this.lblError.ForeColor = System.Drawing.Color.Red;
            this.lblError.Location = new System.Drawing.Point(50, 170);
            this.lblError.Name = "lblError";
            this.lblError.Size = new System.Drawing.Size(0, 13);
            this.lblError.TabIndex = 7;
            this.lblError.Visible = false;
            // 
            // btnCancelar
            // 
            this.btnCancelar.BackColor = System.Drawing.Color.Gray;
            this.btnCancelar.FlatAppearance.BorderSize = 0;
            this.btnCancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelar.ForeColor = System.Drawing.Color.White;
            this.btnCancelar.Location = new System.Drawing.Point(220, 200);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(120, 35);
            this.btnCancelar.TabIndex = 4;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = false;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // btnLogin
            // 
            this.btnLogin.BackColor = System.Drawing.Color.SteelBlue;
            this.btnLogin.FlatAppearance.BorderSize = 0;
            this.btnLogin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLogin.ForeColor = System.Drawing.Color.White;
            this.btnLogin.Location = new System.Drawing.Point(60, 200);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(120, 35);
            this.btnLogin.TabIndex = 3;
            this.btnLogin.Text = "Iniciar Sesión";
            this.btnLogin.UseVisualStyleBackColor = false;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // txtClave
            // 
            this.txtClave.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtClave.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtClave.Location = new System.Drawing.Point(60, 130);
            this.txtClave.Name = "txtClave";
            this.txtClave.Size = new System.Drawing.Size(280, 25);
            this.txtClave.TabIndex = 2;
            this.txtClave.UseSystemPasswordChar = true;
            this.txtClave.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtClave_KeyPress);
            // 
            // txtUsuario
            // 
            this.txtUsuario.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtUsuario.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtUsuario.Location = new System.Drawing.Point(60, 80);
            this.txtUsuario.Name = "txtUsuario";
            this.txtUsuario.Size = new System.Drawing.Size(280, 25);
            this.txtUsuario.TabIndex = 1;
            this.txtUsuario.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtUsuario_KeyPress);
            // 
            // lblClave
            // 
            this.lblClave.AutoSize = true;
            this.lblClave.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblClave.Location = new System.Drawing.Point(57, 115);
            this.lblClave.Name = "lblClave";
            this.lblClave.Size = new System.Drawing.Size(70, 15);
            this.lblClave.TabIndex = 0;
            this.lblClave.Text = "Contraseña:";
            // 
            // lblUsuario
            // 
            this.lblUsuario.AutoSize = true;
            this.lblUsuario.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblUsuario.Location = new System.Drawing.Point(57, 65);
            this.lblUsuario.Name = "lblUsuario";
            this.lblUsuario.Size = new System.Drawing.Size(50, 15);
            this.lblUsuario.TabIndex = 0;
            this.lblUsuario.Text = "Usuario:";
            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitulo.ForeColor = System.Drawing.Color.SteelBlue;
            this.lblTitulo.Location = new System.Drawing.Point(125, 20);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(150, 30);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "Iniciar Sesión";
            // 
            // panelHeader
            // 
            this.panelHeader.BackColor = System.Drawing.Color.SteelBlue;
            this.panelHeader.Controls.Add(this.btnMinimizar);
            this.panelHeader.Controls.Add(this.btnCerrar);
            this.panelHeader.Controls.Add(this.lblSistema);
            this.panelHeader.Controls.Add(this.pictureBoxLogo);
            this.panelHeader.Location = new System.Drawing.Point(1, 1);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(400, 70);
            this.panelHeader.TabIndex = 1;
            this.panelHeader.MouseDown += new System.Windows.Forms.MouseEventHandler(this.FrmLogin_MouseDown);
            this.panelHeader.MouseMove += new System.Windows.Forms.MouseEventHandler(this.FrmLogin_MouseMove);
            this.panelHeader.MouseUp += new System.Windows.Forms.MouseEventHandler(this.FrmLogin_MouseUp);
            // 
            // btnMinimizar
            // 
            this.btnMinimizar.BackColor = System.Drawing.Color.Transparent;
            this.btnMinimizar.FlatAppearance.BorderSize = 0;
            this.btnMinimizar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMinimizar.ForeColor = System.Drawing.Color.White;
            this.btnMinimizar.Location = new System.Drawing.Point(340, 5);
            this.btnMinimizar.Name = "btnMinimizar";
            this.btnMinimizar.Size = new System.Drawing.Size(25, 25);
            this.btnMinimizar.TabIndex = 8;
            this.btnMinimizar.Text = "─";
            this.btnMinimizar.UseVisualStyleBackColor = false;
            this.btnMinimizar.Click += new System.EventHandler(this.btnMinimizar_Click);
            // 
            // btnCerrar
            // 
            this.btnCerrar.BackColor = System.Drawing.Color.Red;
            this.btnCerrar.FlatAppearance.BorderSize = 0;
            this.btnCerrar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCerrar.ForeColor = System.Drawing.Color.White;
            this.btnCerrar.Location = new System.Drawing.Point(370, 5);
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Size = new System.Drawing.Size(25, 25);
            this.btnCerrar.TabIndex = 9;
            this.btnCerrar.Text = "✕";
            this.btnCerrar.UseVisualStyleBackColor = false;
            this.btnCerrar.Click += new System.EventHandler(this.btnCerrar_Click);
            // 
            // lblSistema
            // 
            this.lblSistema.AutoSize = true;
            this.lblSistema.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblSistema.ForeColor = System.Drawing.Color.White;
            this.lblSistema.Location = new System.Drawing.Point(80, 25);
            this.lblSistema.Name = "lblSistema";
            this.lblSistema.Size = new System.Drawing.Size(180, 25);
            this.lblSistema.TabIndex = 0;
            this.lblSistema.Text = "Sistema Veterinario";
            // 
            // pictureBoxLogo
            // 
            this.pictureBoxLogo.BackColor = System.Drawing.Color.White;
            this.pictureBoxLogo.Location = new System.Drawing.Point(15, 15);
            this.pictureBoxLogo.Name = "pictureBoxLogo";
            this.pictureBoxLogo.Size = new System.Drawing.Size(50, 40);
            this.pictureBoxLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxLogo.TabIndex = 0;
            this.pictureBoxLogo.TabStop = false;
            // 
            // FrmLogin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightGray;
            this.ClientSize = new System.Drawing.Size(402, 401);
            this.Controls.Add(this.panelHeader);
            this.Controls.Add(this.panelLogin);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmLogin";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Sistema Veterinario - Login";
            this.Load += new System.EventHandler(this.FrmLogin_Load);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.FrmLogin_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.FrmLogin_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.FrmLogin_MouseUp);
            this.panelLogin.ResumeLayout(false);
            this.panelLogin.PerformLayout();
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLogo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelLogin;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Label lblUsuario;
        private System.Windows.Forms.Label lblClave;
        private System.Windows.Forms.TextBox txtUsuario;
        private System.Windows.Forms.TextBox txtClave;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Label lblError;
        private System.Windows.Forms.Button btnMostrarClave;
        private System.Windows.Forms.LinkLabel linkOlvideClave;
        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Label lblSistema;
        private System.Windows.Forms.PictureBox pictureBoxLogo;
        private System.Windows.Forms.Button btnCerrar;
        private System.Windows.Forms.Button btnMinimizar;
    }
}