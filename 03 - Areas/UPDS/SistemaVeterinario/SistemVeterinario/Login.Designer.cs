using System.Drawing;
using System.Windows.Forms;
using System;
namespace SistemVeterinario
{
    partial class Login
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Login));
            panelMain = new Panel();
            panelLogin = new Panel();
            groupBox2 = new GroupBox();
            pictureBox4 = new PictureBox();
            txtContrasena = new TextBox();
            groupBox1 = new GroupBox();
            pictureBox3 = new PictureBox();
            txtUsuario = new TextBox();
            pictureBox2 = new PictureBox();
            lblTitulo = new Label();
            lblSubtitulo = new Label();
            btnLogin = new Button();
            btnCancelar = new Button();
            lblOlvideContrasena = new Label();
            lblCrearCuenta = new Label();
            panelLogo = new Panel();
            label1 = new Label();
            pictureBox1 = new PictureBox();
            lblLogo = new Label();
            // 
            // panelMain
            // 
            panelMain.BackColor = Color.FromArgb(236, 240, 241);
            panelMain.Controls.Add(panelLogin);
            panelMain.Controls.Add(panelLogo);
            panelMain.Dock = DockStyle.Fill;
            panelMain.Location = new Point(0, 0);
            panelMain.Margin = new Padding(3, 4, 3, 4);
            panelMain.Name = "panelMain";
            panelMain.Size = new Size(914, 667);
            panelMain.TabIndex = 0;
            // 
            // panelLogin
            // 
            panelLogin.BackColor = Color.White;
            panelLogin.Controls.Add(groupBox2);
            panelLogin.Controls.Add(groupBox1);
            panelLogin.Controls.Add(pictureBox2);
            panelLogin.Controls.Add(lblTitulo);
            panelLogin.Controls.Add(lblSubtitulo);
            panelLogin.Controls.Add(btnLogin);
            panelLogin.Controls.Add(btnCancelar);
            panelLogin.Controls.Add(lblOlvideContrasena);
            panelLogin.Controls.Add(lblCrearCuenta);
            panelLogin.Dock = DockStyle.Fill;
            panelLogin.Location = new Point(457, 0);
            panelLogin.Margin = new Padding(3, 4, 3, 4);
            panelLogin.Name = "panelLogin";
            panelLogin.Padding = new Padding(46, 53, 46, 53);
            panelLogin.Size = new Size(457, 667);
            panelLogin.TabIndex = 1;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(pictureBox4);
            groupBox2.Controls.Add(txtContrasena);
            groupBox2.Location = new Point(17, 376);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(414, 78);
            groupBox2.TabIndex = 11;
            groupBox2.TabStop = false;
            // 
            // pictureBox4
            // 
            pictureBox4.Image = Properties.Resources.contrasena;
            pictureBox4.Location = new Point(11, 15);
            pictureBox4.Name = "pictureBox4";
            pictureBox4.Size = new Size(58, 57);
            pictureBox4.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox4.TabIndex = 10;
            pictureBox4.TabStop = false;
            // 
            // txtContrasena
            // 
            txtContrasena.BorderStyle = BorderStyle.FixedSingle;
            txtContrasena.Font = new Font("Segoe UI", 12F);
            txtContrasena.Location = new Point(75, 27);
            txtContrasena.Margin = new Padding(3, 4, 3, 4);
            txtContrasena.Name = "txtContrasena";
            txtContrasena.PasswordChar = '*';
            txtContrasena.Size = new Size(333, 34);
            txtContrasena.TabIndex = 3;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(pictureBox3);
            groupBox1.Controls.Add(txtUsuario);
            groupBox1.Location = new Point(17, 275);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(414, 81);
            groupBox1.TabIndex = 10;
            groupBox1.TabStop = false;
            // 
            // pictureBox3
            // 
            pictureBox3.Image = (Image)resources.GetObject("pictureBox3.Image");
            pictureBox3.Location = new Point(12, 18);
            pictureBox3.Name = "pictureBox3";
            pictureBox3.Size = new Size(58, 57);
            pictureBox3.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox3.TabIndex = 9;
            pictureBox3.TabStop = false;
            // 
            // txtUsuario
            // 
            txtUsuario.BorderStyle = BorderStyle.FixedSingle;
            txtUsuario.Font = new Font("Segoe UI", 12F);
            txtUsuario.ForeColor = Color.Gray;
            txtUsuario.Location = new Point(76, 27);
            txtUsuario.Margin = new Padding(3, 4, 3, 4);
            txtUsuario.Name = "txtUsuario";
            txtUsuario.Size = new Size(332, 34);
            txtUsuario.TabIndex = 2;
            txtUsuario.Text = "";
            // 
            // pictureBox2
            // 
            pictureBox2.Image = Properties.Resources.loginZoofiPetss;
            pictureBox2.Location = new Point(128, 13);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(193, 186);
            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox2.TabIndex = 8;
            pictureBox2.TabStop = false;
            // 
            // lblTitulo
            // 
            lblTitulo.AutoSize = true;
            lblTitulo.Font = new Font("Segoe UI", 24F, FontStyle.Bold);
            lblTitulo.ForeColor = Color.FromArgb(52, 73, 94);
            lblTitulo.Location = new Point(92, 220);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(268, 54);
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "BIENVENIDO";
            // 
            // lblSubtitulo
            // 
            lblSubtitulo.AutoSize = true;
            lblSubtitulo.Font = new Font("Segoe UI", 12F);
            lblSubtitulo.ForeColor = Color.DimGray;
            lblSubtitulo.Location = new Point(116, 270);
            lblSubtitulo.Name = "lblSubtitulo";
            lblSubtitulo.Size = new Size(218, 28);
            lblSubtitulo.TabIndex = 1;
            lblSubtitulo.Text = "Ingrese sus credenciales";
            // 
            // btnLogin
            // 
            btnLogin.BackColor = Color.MediumOrchid;
            btnLogin.FlatAppearance.BorderSize = 0;
            btnLogin.FlatStyle = FlatStyle.Flat;
            btnLogin.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            btnLogin.ForeColor = Color.White;
            btnLogin.Location = new Point(49, 475);
            btnLogin.Margin = new Padding(3, 4, 3, 4);
            btnLogin.Name = "btnLogin";
            btnLogin.Size = new Size(160, 53);
            btnLogin.TabIndex = 4;
            btnLogin.Text = "Ingresar";
            btnLogin.UseVisualStyleBackColor = false;
            // 
            // btnCancelar
            // 
            btnCancelar.BackColor = Color.DodgerBlue;
            btnCancelar.FlatAppearance.BorderSize = 0;
            btnCancelar.FlatStyle = FlatStyle.Flat;
            btnCancelar.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            btnCancelar.ForeColor = Color.White;
            btnCancelar.Location = new Point(240, 475);
            btnCancelar.Margin = new Padding(3, 4, 3, 4);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(160, 53);
            btnCancelar.TabIndex = 5;
            btnCancelar.Text = "Cancelar";
            btnCancelar.UseVisualStyleBackColor = false;
            // 
            // lblOlvideContrasena
            // 
            lblOlvideContrasena.AutoSize = true;
            lblOlvideContrasena.Cursor = Cursors.Hand;
            lblOlvideContrasena.Font = new Font("Segoe UI", 10F, FontStyle.Underline);
            lblOlvideContrasena.ForeColor = Color.FromArgb(41, 128, 185);
            lblOlvideContrasena.Location = new Point(141, 554);
            lblOlvideContrasena.Name = "lblOlvideContrasena";
            lblOlvideContrasena.Size = new Size(171, 23);
            lblOlvideContrasena.TabIndex = 6;
            lblOlvideContrasena.Text = "Olvidé mi contraseña";
            // 
            // lblCrearCuenta
            // 
            lblCrearCuenta.AutoSize = true;
            lblCrearCuenta.Cursor = Cursors.Hand;
            lblCrearCuenta.Font = new Font("Segoe UI", 10F, FontStyle.Underline);
            lblCrearCuenta.ForeColor = Color.FromArgb(41, 128, 185);
            lblCrearCuenta.Location = new Point(167, 588);
            lblCrearCuenta.Name = "lblCrearCuenta";
            lblCrearCuenta.Size = new Size(108, 23);
            lblCrearCuenta.TabIndex = 7;
            lblCrearCuenta.Text = "Crear cuenta";
            // 
            // panelLogo
            // 
            panelLogo.BackColor = Color.MediumOrchid;
            panelLogo.Controls.Add(label1);
            panelLogo.Controls.Add(pictureBox1);
            panelLogo.Controls.Add(lblLogo);
            panelLogo.Dock = DockStyle.Left;
            panelLogo.Location = new Point(0, 0);
            panelLogo.Margin = new Padding(3, 4, 3, 4);
            panelLogo.Name = "panelLogo";
            panelLogo.Size = new Size(457, 667);
            panelLogo.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 24F, FontStyle.Bold);
            label1.ForeColor = SystemColors.ButtonHighlight;
            label1.Location = new Point(103, 160);
            label1.Name = "label1";
            label1.Size = new Size(254, 54);
            label1.TabIndex = 2;
            label1.Text = "ZOOFIPETSS";
            // 
            // pictureBox1
            // 
            pictureBox1.Image = Properties.Resources.LOGO;
            pictureBox1.Location = new Point(58, 217);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(329, 311);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 1;
            pictureBox1.TabStop = false;
            // 
            // lblLogo
            // 
            lblLogo.Anchor = AnchorStyles.None;
            lblLogo.AutoSize = true;
            lblLogo.Font = new Font("Segoe UI", 24F, FontStyle.Bold);
            lblLogo.ForeColor = Color.White;
            lblLogo.Location = new Point(37, 119);
            lblLogo.Name = "lblLogo";
            lblLogo.Size = new Size(393, 54);
            lblLogo.TabIndex = 0;
            lblLogo.Text = "🐾VETERINARIA🐾";
            lblLogo.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // Login
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(914, 667);
            Controls.Add(panelMain);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Margin = new Padding(3, 4, 3, 4);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "Login";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Inicio de Seccion";
        }

        #endregion

        private Panel panelMain;
        private Panel panelLogo;
        private Label lblLogo;
        private Panel panelLogin;
        private Label lblTitulo;
        private Label lblSubtitulo;
        private TextBox txtUsuario;
        private TextBox txtContrasena;
        private Button btnLogin;
        private Button btnCancelar;
        private Label lblOlvideContrasena;
        private Label lblCrearCuenta;
        private PictureBox pictureBox1;
        private Label label1;
        private PictureBox pictureBox2;
        private GroupBox groupBox2;
        private PictureBox pictureBox4;
        private GroupBox groupBox1;
        private PictureBox pictureBox3;
    }
}
