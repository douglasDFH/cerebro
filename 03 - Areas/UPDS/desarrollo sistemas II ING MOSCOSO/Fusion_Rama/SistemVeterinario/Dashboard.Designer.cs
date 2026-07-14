using System.Drawing;

namespace SistemVeterinario
{
    partial class Dashboard
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Dashboard));
            panelTop = new Panel();
            pictureBox7 = new PictureBox();
            pictureBox5 = new PictureBox();
            pictureBox4 = new PictureBox();
            pictureBox1 = new PictureBox();
            lblUsuario = new Label();
            lblEmail = new Label();
            btnLogout = new Button();
            panelSidebar = new Panel();
            button2 = new Button();
            button1 = new Button();
            pictureBox2 = new PictureBox();
            btnPersonal = new Button();
            btnClientes = new Button();
            btnMascotas = new Button();
            btnVentas = new Button();
            btnProductos = new Button();
            btnReportes = new Button();
            panelContent = new Panel();
            pictureBox12 = new PictureBox();
            pictureBox11 = new PictureBox();
            pictureBox10 = new PictureBox();
            pictureBox9 = new PictureBox();
            pictureBox8 = new PictureBox();
            pictureBox6 = new PictureBox();
            lblSubtitulo = new Label();
            lblTitulo = new Label();
            pictureBox3 = new PictureBox();
            panelTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox7).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox5).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            panelSidebar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            panelContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox12).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox11).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox10).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox9).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox8).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox6).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).BeginInit();
            SuspendLayout();
            // 
            // panelTop
            // 
            panelTop.BackColor = Color.DodgerBlue;
            panelTop.Controls.Add(pictureBox7);
            panelTop.Controls.Add(pictureBox5);
            panelTop.Controls.Add(pictureBox4);
            panelTop.Controls.Add(pictureBox1);
            panelTop.Controls.Add(lblUsuario);
            panelTop.Controls.Add(lblEmail);
            panelTop.Controls.Add(btnLogout);
            panelTop.Dock = DockStyle.Top;
            panelTop.Location = new Point(0, 0);
            panelTop.Margin = new Padding(3, 4, 3, 4);
            panelTop.Name = "panelTop";
            panelTop.Size = new Size(1371, 99);
            panelTop.TabIndex = 0;
            // 
            // pictureBox7
            // 
            pictureBox7.Image = Properties.Resources.Lupa;
            pictureBox7.Location = new Point(372, 14);
            pictureBox7.Name = "pictureBox7";
            pictureBox7.Size = new Size(69, 59);
            pictureBox7.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox7.TabIndex = 6;
            pictureBox7.TabStop = false;
            // 
            // pictureBox5
            // 
            pictureBox5.Image = (Image)resources.GetObject("pictureBox5.Image");
            pictureBox5.Location = new Point(1010, 13);
            pictureBox5.Name = "pictureBox5";
            pictureBox5.Size = new Size(69, 59);
            pictureBox5.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox5.TabIndex = 5;
            pictureBox5.TabStop = false;
            // 
            // pictureBox4
            // 
            pictureBox4.Image = Properties.Resources.cerrar;
            pictureBox4.Location = new Point(1094, 12);
            pictureBox4.Name = "pictureBox4";
            pictureBox4.Size = new Size(69, 59);
            pictureBox4.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox4.TabIndex = 4;
            pictureBox4.TabStop = false;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = Properties.Resources.usuario;
            pictureBox1.Location = new Point(23, 12);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(81, 80);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 3;
            pictureBox1.TabStop = false;
            // 
            // lblUsuario
            // 
            lblUsuario.AutoSize = true;
            lblUsuario.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblUsuario.ForeColor = Color.White;
            lblUsuario.Location = new Point(110, 24);
            lblUsuario.Name = "lblUsuario";
            lblUsuario.Size = new Size(201, 28);
            lblUsuario.TabIndex = 0;
            lblUsuario.Text = "Bienvenido, Usuario";
            // 
            // lblEmail
            // 
            lblEmail.AutoSize = true;
            lblEmail.Font = new Font("Segoe UI", 9F);
            lblEmail.ForeColor = Color.LightGray;
            lblEmail.Location = new Point(132, 52);
            lblEmail.Name = "lblEmail";
            lblEmail.Size = new Size(140, 20);
            lblEmail.TabIndex = 1;
            lblEmail.Text = "usuario@email.com";
            // 
            // btnLogout
            // 
            btnLogout.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnLogout.BackColor = Color.DodgerBlue;
            btnLogout.FlatAppearance.BorderSize = 0;
            btnLogout.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            btnLogout.ForeColor = Color.White;
            btnLogout.Location = new Point(1181, 11);
            btnLogout.Margin = new Padding(3, 4, 3, 4);
            btnLogout.Name = "btnLogout";
            btnLogout.Size = new Size(167, 60);
            btnLogout.TabIndex = 2;
            btnLogout.Text = "Cerrar Sesión";
            btnLogout.UseVisualStyleBackColor = false;
            // 
            // panelSidebar
            // 
            panelSidebar.BackColor = Color.MediumOrchid;
            panelSidebar.Controls.Add(button2);
            panelSidebar.Controls.Add(button1);
            panelSidebar.Controls.Add(pictureBox2);
            panelSidebar.Controls.Add(btnPersonal);
            panelSidebar.Controls.Add(btnClientes);
            panelSidebar.Controls.Add(btnMascotas);
            panelSidebar.Controls.Add(btnVentas);
            panelSidebar.Controls.Add(btnProductos);
            panelSidebar.Controls.Add(btnReportes);
            panelSidebar.Dock = DockStyle.Left;
            panelSidebar.Location = new Point(0, 99);
            panelSidebar.Margin = new Padding(3, 4, 3, 4);
            panelSidebar.Name = "panelSidebar";
            panelSidebar.Size = new Size(229, 834);
            panelSidebar.TabIndex = 1;
            // 
            // button2
            // 
            button2.BackColor = Color.MediumOrchid;
            button2.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            button2.ForeColor = SystemColors.Control;
            button2.Location = new Point(12, 723);
            button2.Name = "button2";
            button2.Size = new Size(195, 57);
            button2.TabIndex = 1;
            button2.Text = "⚙️Configuracion";
            button2.UseVisualStyleBackColor = false;
            // 
            // button1
            // 
            button1.BackColor = Color.MediumOrchid;
            button1.FlatAppearance.BorderSize = 0;
            button1.FlatStyle = FlatStyle.Flat;
            button1.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            button1.ForeColor = Color.White;
            button1.Location = new Point(0, 384);
            button1.Margin = new Padding(3, 4, 3, 4);
            button1.Name = "button1";
            button1.Size = new Size(229, 67);
            button1.TabIndex = 6;
            button1.Text = "📊 Reportes";
            button1.TextAlign = ContentAlignment.MiddleLeft;
            button1.UseVisualStyleBackColor = false;
            // 
            // pictureBox2
            // 
            pictureBox2.Image = Properties.Resources.LOGO;
            pictureBox2.Location = new Point(12, 492);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(195, 201);
            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox2.TabIndex = 1;
            pictureBox2.TabStop = false;
            // 
            // btnPersonal
            // 
            btnPersonal.BackColor = Color.MediumOrchid;
            btnPersonal.FlatAppearance.BorderSize = 0;
            btnPersonal.FlatStyle = FlatStyle.Flat;
            btnPersonal.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            btnPersonal.ForeColor = Color.White;
            btnPersonal.Location = new Point(0, 100);
            btnPersonal.Margin = new Padding(3, 4, 3, 4);
            btnPersonal.Name = "btnPersonal";
            btnPersonal.Size = new Size(229, 67);
            btnPersonal.TabIndex = 5;
            btnPersonal.Text = "👤Personal";
            btnPersonal.TextAlign = ContentAlignment.MiddleLeft;
            btnPersonal.UseVisualStyleBackColor = false;
            // 
            // btnClientes
            // 
            btnClientes.BackColor = Color.MediumOrchid;
            btnClientes.FlatAppearance.BorderSize = 0;
            btnClientes.FlatStyle = FlatStyle.Flat;
            btnClientes.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            btnClientes.ForeColor = Color.White;
            btnClientes.Location = new Point(0, 37);
            btnClientes.Margin = new Padding(3, 4, 3, 4);
            btnClientes.Name = "btnClientes";
            btnClientes.Size = new Size(229, 67);
            btnClientes.TabIndex = 0;
            btnClientes.Text = "\U0001f9d1‍\U0001f91d‍\U0001f9d1 Clientes";
            btnClientes.TextAlign = ContentAlignment.MiddleLeft;
            btnClientes.UseVisualStyleBackColor = false;
            // 
            // btnMascotas
            // 
            btnMascotas.BackColor = Color.MediumOrchid;
            btnMascotas.FlatAppearance.BorderSize = 0;
            btnMascotas.FlatStyle = FlatStyle.Flat;
            btnMascotas.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            btnMascotas.ForeColor = Color.White;
            btnMascotas.Location = new Point(0, 175);
            btnMascotas.Margin = new Padding(3, 4, 3, 4);
            btnMascotas.Name = "btnMascotas";
            btnMascotas.Size = new Size(229, 67);
            btnMascotas.TabIndex = 1;
            btnMascotas.Text = "🐾 Mascotas";
            btnMascotas.TextAlign = ContentAlignment.MiddleLeft;
            btnMascotas.UseVisualStyleBackColor = false;
            // 
            // btnVentas
            // 
            btnVentas.BackColor = Color.MediumOrchid;
            btnVentas.FlatAppearance.BorderSize = 0;
            btnVentas.FlatStyle = FlatStyle.Flat;
            btnVentas.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            btnVentas.ForeColor = Color.White;
            btnVentas.Location = new Point(0, 250);
            btnVentas.Margin = new Padding(3, 4, 3, 4);
            btnVentas.Name = "btnVentas";
            btnVentas.Size = new Size(229, 67);
            btnVentas.TabIndex = 2;
            btnVentas.Text = "\U0001f6d2 Ventas";
            btnVentas.TextAlign = ContentAlignment.MiddleLeft;
            btnVentas.UseVisualStyleBackColor = false;
            // 
            // btnProductos
            // 
            btnProductos.BackColor = Color.MediumOrchid;
            btnProductos.FlatAppearance.BorderSize = 0;
            btnProductos.FlatStyle = FlatStyle.Flat;
            btnProductos.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            btnProductos.ForeColor = Color.White;
            btnProductos.Location = new Point(0, 316);
            btnProductos.Margin = new Padding(3, 4, 3, 4);
            btnProductos.Name = "btnProductos";
            btnProductos.Size = new Size(229, 67);
            btnProductos.TabIndex = 3;
            btnProductos.Text = "📦 Productos";
            btnProductos.TextAlign = ContentAlignment.MiddleLeft;
            btnProductos.UseVisualStyleBackColor = false;
            // 
            // btnReportes
            // 
            btnReportes.BackColor = Color.MediumOrchid;
            btnReportes.FlatAppearance.BorderSize = 0;
            btnReportes.FlatStyle = FlatStyle.Flat;
            btnReportes.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            btnReportes.ForeColor = Color.White;
            btnReportes.Location = new Point(0, 391);
            btnReportes.Margin = new Padding(3, 4, 3, 4);
            btnReportes.Name = "btnReportes";
            btnReportes.Size = new Size(229, 67);
            btnReportes.TabIndex = 4;
            btnReportes.Text = "📊 Reportes";
            btnReportes.TextAlign = ContentAlignment.MiddleLeft;
            btnReportes.UseVisualStyleBackColor = false;
            // 
            // panelContent
            // 
            panelContent.BackColor = Color.FromArgb(236, 240, 241);
            panelContent.Controls.Add(pictureBox12);
            panelContent.Controls.Add(pictureBox11);
            panelContent.Controls.Add(pictureBox10);
            panelContent.Controls.Add(pictureBox9);
            panelContent.Controls.Add(pictureBox8);
            panelContent.Controls.Add(pictureBox6);
            panelContent.Controls.Add(lblSubtitulo);
            panelContent.Controls.Add(lblTitulo);
            panelContent.Controls.Add(pictureBox3);
            panelContent.Dock = DockStyle.Fill;
            panelContent.ForeColor = Color.Cornsilk;
            panelContent.Location = new Point(229, 99);
            panelContent.Margin = new Padding(3, 4, 3, 4);
            panelContent.Name = "panelContent";
            panelContent.Size = new Size(1142, 834);
            panelContent.TabIndex = 2;
            // 
            // pictureBox12
            // 
            pictureBox12.Image = (Image)resources.GetObject("pictureBox12.Image");
            pictureBox12.Location = new Point(930, 493);
            pictureBox12.Name = "pictureBox12";
            pictureBox12.Size = new Size(164, 160);
            pictureBox12.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox12.TabIndex = 11;
            pictureBox12.TabStop = false;
            // 
            // pictureBox11
            // 
            pictureBox11.Image = (Image)resources.GetObject("pictureBox11.Image");
            pictureBox11.Location = new Point(751, 493);
            pictureBox11.Name = "pictureBox11";
            pictureBox11.Size = new Size(164, 160);
            pictureBox11.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox11.TabIndex = 10;
            pictureBox11.TabStop = false;
            // 
            // pictureBox10
            // 
            pictureBox10.Image = (Image)resources.GetObject("pictureBox10.Image");
            pictureBox10.Location = new Point(571, 493);
            pictureBox10.Name = "pictureBox10";
            pictureBox10.Size = new Size(164, 160);
            pictureBox10.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox10.TabIndex = 9;
            pictureBox10.TabStop = false;
            // 
            // pictureBox9
            // 
            pictureBox9.Image = (Image)resources.GetObject("pictureBox9.Image");
            pictureBox9.Location = new Point(392, 493);
            pictureBox9.Name = "pictureBox9";
            pictureBox9.Size = new Size(164, 160);
            pictureBox9.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox9.TabIndex = 8;
            pictureBox9.TabStop = false;
            // 
            // pictureBox8
            // 
            pictureBox8.Image = (Image)resources.GetObject("pictureBox8.Image");
            pictureBox8.Location = new Point(213, 493);
            pictureBox8.Name = "pictureBox8";
            pictureBox8.Size = new Size(164, 160);
            pictureBox8.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox8.TabIndex = 7;
            pictureBox8.TabStop = false;
            // 
            // pictureBox6
            // 
            pictureBox6.Image = Properties.Resources.InicioWeb;
            pictureBox6.Location = new Point(34, 493);
            pictureBox6.Name = "pictureBox6";
            pictureBox6.Size = new Size(164, 160);
            pictureBox6.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox6.TabIndex = 6;
            pictureBox6.TabStop = false;
            // 
            // lblSubtitulo
            // 
            lblSubtitulo.AutoSize = true;
            lblSubtitulo.Font = new Font("Segoe UI", 12F);
            lblSubtitulo.ForeColor = Color.DimGray;
            lblSubtitulo.Location = new Point(453, 447);
            lblSubtitulo.Name = "lblSubtitulo";
            lblSubtitulo.Size = new Size(225, 28);
            lblSubtitulo.TabIndex = 2;
            lblSubtitulo.Text = "Desarrollado en la UPDS";
            // 
            // lblTitulo
            // 
            lblTitulo.AutoSize = true;
            lblTitulo.Font = new Font("Segoe UI", 24F, FontStyle.Bold);
            lblTitulo.ForeColor = Color.FromArgb(52, 73, 94);
            lblTitulo.Location = new Point(282, 395);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(555, 54);
            lblTitulo.TabIndex = 1;
            lblTitulo.Text = "VETERINARIA -ZOOFIPETSS-";
            // 
            // pictureBox3
            // 
            pictureBox3.Image = Properties.Resources.LOGO;
            pictureBox3.Location = new Point(435, 107);
            pictureBox3.Name = "pictureBox3";
            pictureBox3.Size = new Size(280, 276);
            pictureBox3.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox3.TabIndex = 0;
            pictureBox3.TabStop = false;
            // 
            // Dashboard
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1371, 933);
            Controls.Add(panelContent);
            Controls.Add(panelSidebar);
            Controls.Add(panelTop);
            Margin = new Padding(3, 4, 3, 4);
            Name = "Dashboard";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Veterinaria - Dashboard";
            WindowState = FormWindowState.Maximized;
            panelTop.ResumeLayout(false);
            panelTop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox7).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox5).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            panelSidebar.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            panelContent.ResumeLayout(false);
            panelContent.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox12).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox11).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox10).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox9).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox8).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox6).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel panelTop;
        private Label lblUsuario;
        private Label lblEmail;
        private Button btnLogout;
        private Panel panelSidebar;
        private Button btnClientes;
        private Button btnMascotas;
        private Button btnVentas;
        private Button btnProductos;
        private Button btnReportes;
        private Button btnPersonal;
        private Panel panelContent;
        private PictureBox pictureBox1;
        private PictureBox pictureBox2;
        private Button button1;
        private Button button2;
        private PictureBox pictureBox3;
        private Label lblTitulo;
        private Label lblSubtitulo;
        private PictureBox pictureBox4;
        private PictureBox pictureBox6;
        private PictureBox pictureBox7;
        private PictureBox pictureBox5;
        private PictureBox pictureBox12;
        private PictureBox pictureBox11;
        private PictureBox pictureBox10;
        private PictureBox pictureBox9;
        private PictureBox pictureBox8;
    }
}
