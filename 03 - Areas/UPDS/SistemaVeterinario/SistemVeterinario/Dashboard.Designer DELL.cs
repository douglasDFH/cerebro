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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Dashboard));
            panelSidebar = new Panel();
            pictureBox2 = new PictureBox();
            BtnConfiguracion = new FontAwesome.Sharp.IconButton();
            BtnDashboard = new FontAwesome.Sharp.IconButton();
            BtnReportes = new FontAwesome.Sharp.IconButton();
            BtnConsultas = new FontAwesome.Sharp.IconButton();
            BtnHistorial = new FontAwesome.Sharp.IconButton();
            BtnMascotas = new FontAwesome.Sharp.IconButton();
            BtnProductos = new FontAwesome.Sharp.IconButton();
            BtnVentas = new FontAwesome.Sharp.IconButton();
            BtnInventario = new FontAwesome.Sharp.IconButton();
            BtnPersonal = new FontAwesome.Sharp.IconButton();
            BtnClientes = new FontAwesome.Sharp.IconButton();
            pictureBox1 = new PictureBox();
            panelContent = new Panel();
            panel1 = new Panel();
            lblHora = new Label();
            lblEmail = new Label();
            iconButton2 = new FontAwesome.Sharp.IconButton();
            iconButton1 = new FontAwesome.Sharp.IconButton();
            lblUsuario = new Label();
            tituloSuperior = new Label();
            iconoSuperior = new FontAwesome.Sharp.IconButton();
            panel2 = new Panel();
            pictureBox12 = new PictureBox();
            pictureBox3 = new PictureBox();
            pictureBox11 = new PictureBox();
            pictureBox8 = new PictureBox();
            lblTitulo = new Label();
            pictureBox6 = new PictureBox();
            pictureBox10 = new PictureBox();
            pictureBox9 = new PictureBox();
            lblSubtitulo = new Label();
            timer1 = new System.Windows.Forms.Timer(components);
            panelSidebar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            panelContent.SuspendLayout();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox12).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox11).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox8).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox6).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox10).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox9).BeginInit();
            SuspendLayout();
            // 
            // panelSidebar
            // 
            panelSidebar.BackColor = Color.MediumOrchid;
            panelSidebar.Controls.Add(pictureBox2);
            panelSidebar.Controls.Add(BtnConfiguracion);
            panelSidebar.Controls.Add(BtnDashboard);
            panelSidebar.Controls.Add(BtnReportes);
            panelSidebar.Controls.Add(BtnConsultas);
            panelSidebar.Controls.Add(BtnHistorial);
            panelSidebar.Controls.Add(BtnMascotas);
            panelSidebar.Controls.Add(BtnProductos);
            panelSidebar.Controls.Add(BtnVentas);
            panelSidebar.Controls.Add(BtnInventario);
            panelSidebar.Controls.Add(BtnPersonal);
            panelSidebar.Controls.Add(BtnClientes);
            panelSidebar.Controls.Add(pictureBox1);
            panelSidebar.Dock = DockStyle.Left;
            panelSidebar.Location = new Point(0, 0);
            panelSidebar.Margin = new Padding(3, 4, 3, 4);
            panelSidebar.Name = "panelSidebar";
            panelSidebar.Size = new Size(238, 933);
            panelSidebar.TabIndex = 1;
            // 
            // pictureBox2
            // 
            pictureBox2.Image = Properties.Resources.UPDS;
            pictureBox2.Location = new Point(12, 860);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(220, 61);
            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox2.TabIndex = 12;
            pictureBox2.TabStop = false;
            // 
            // BtnConfiguracion
            // 
            BtnConfiguracion.Dock = DockStyle.Top;
            BtnConfiguracion.FlatAppearance.BorderSize = 0;
            BtnConfiguracion.FlatStyle = FlatStyle.Flat;
            BtnConfiguracion.Font = new Font("Arial Narrow", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            BtnConfiguracion.ForeColor = SystemColors.ControlLightLight;
            BtnConfiguracion.IconChar = FontAwesome.Sharp.IconChar.Toolbox;
            BtnConfiguracion.IconColor = Color.White;
            BtnConfiguracion.IconFont = FontAwesome.Sharp.IconFont.Auto;
            BtnConfiguracion.IconSize = 50;
            BtnConfiguracion.ImageAlign = ContentAlignment.MiddleLeft;
            BtnConfiguracion.Location = new Point(0, 781);
            BtnConfiguracion.Name = "BtnConfiguracion";
            BtnConfiguracion.Size = new Size(238, 63);
            BtnConfiguracion.TabIndex = 11;
            BtnConfiguracion.Text = "Configuracion";
            BtnConfiguracion.TextAlign = ContentAlignment.MiddleLeft;
            BtnConfiguracion.TextImageRelation = TextImageRelation.ImageBeforeText;
            BtnConfiguracion.UseVisualStyleBackColor = true;
            BtnConfiguracion.Click += BtnConfiguracion_Click;
            // 
            // BtnDashboard
            // 
            BtnDashboard.Dock = DockStyle.Top;
            BtnDashboard.FlatAppearance.BorderSize = 0;
            BtnDashboard.FlatStyle = FlatStyle.Flat;
            BtnDashboard.Font = new Font("Arial Narrow", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            BtnDashboard.ForeColor = SystemColors.ControlLightLight;
            BtnDashboard.IconChar = FontAwesome.Sharp.IconChar.TachometerFast;
            BtnDashboard.IconColor = Color.White;
            BtnDashboard.IconFont = FontAwesome.Sharp.IconFont.Auto;
            BtnDashboard.IconSize = 50;
            BtnDashboard.ImageAlign = ContentAlignment.MiddleLeft;
            BtnDashboard.Location = new Point(0, 718);
            BtnDashboard.Name = "BtnDashboard";
            BtnDashboard.Size = new Size(238, 63);
            BtnDashboard.TabIndex = 10;
            BtnDashboard.Text = "Dashboard";
            BtnDashboard.TextAlign = ContentAlignment.MiddleLeft;
            BtnDashboard.TextImageRelation = TextImageRelation.ImageBeforeText;
            BtnDashboard.UseVisualStyleBackColor = true;
            BtnDashboard.Click += BtnDashboard_Click;
            // 
            // BtnReportes
            // 
            BtnReportes.Dock = DockStyle.Top;
            BtnReportes.FlatAppearance.BorderSize = 0;
            BtnReportes.FlatStyle = FlatStyle.Flat;
            BtnReportes.Font = new Font("Arial Narrow", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            BtnReportes.ForeColor = SystemColors.ControlLightLight;
            BtnReportes.IconChar = FontAwesome.Sharp.IconChar.ChartColumn;
            BtnReportes.IconColor = Color.White;
            BtnReportes.IconFont = FontAwesome.Sharp.IconFont.Auto;
            BtnReportes.IconSize = 50;
            BtnReportes.ImageAlign = ContentAlignment.MiddleLeft;
            BtnReportes.Location = new Point(0, 655);
            BtnReportes.Name = "BtnReportes";
            BtnReportes.Size = new Size(238, 63);
            BtnReportes.TabIndex = 9;
            BtnReportes.Text = "Reportes";
            BtnReportes.TextAlign = ContentAlignment.MiddleLeft;
            BtnReportes.TextImageRelation = TextImageRelation.ImageBeforeText;
            BtnReportes.UseVisualStyleBackColor = true;
            BtnReportes.Click += BtnReportes_Click;
            // 
            // BtnConsultas
            // 
            BtnConsultas.Dock = DockStyle.Top;
            BtnConsultas.FlatAppearance.BorderSize = 0;
            BtnConsultas.FlatStyle = FlatStyle.Flat;
            BtnConsultas.Font = new Font("Arial Narrow", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            BtnConsultas.ForeColor = SystemColors.ControlLightLight;
            BtnConsultas.IconChar = FontAwesome.Sharp.IconChar.UserNurse;
            BtnConsultas.IconColor = Color.White;
            BtnConsultas.IconFont = FontAwesome.Sharp.IconFont.Auto;
            BtnConsultas.IconSize = 50;
            BtnConsultas.ImageAlign = ContentAlignment.MiddleLeft;
            BtnConsultas.Location = new Point(0, 592);
            BtnConsultas.Name = "BtnConsultas";
            BtnConsultas.Size = new Size(238, 63);
            BtnConsultas.TabIndex = 8;
            BtnConsultas.Text = "Consultas";
            BtnConsultas.TextAlign = ContentAlignment.MiddleLeft;
            BtnConsultas.TextImageRelation = TextImageRelation.ImageBeforeText;
            BtnConsultas.UseVisualStyleBackColor = true;
            BtnConsultas.Click += BtnConsultas_Click;
            // 
            // BtnHistorial
            // 
            BtnHistorial.Dock = DockStyle.Top;
            BtnHistorial.FlatAppearance.BorderSize = 0;
            BtnHistorial.FlatStyle = FlatStyle.Flat;
            BtnHistorial.Font = new Font("Arial Narrow", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            BtnHistorial.ForeColor = SystemColors.ControlLightLight;
            BtnHistorial.IconChar = FontAwesome.Sharp.IconChar.NotesMedical;
            BtnHistorial.IconColor = Color.White;
            BtnHistorial.IconFont = FontAwesome.Sharp.IconFont.Auto;
            BtnHistorial.IconSize = 50;
            BtnHistorial.ImageAlign = ContentAlignment.MiddleLeft;
            BtnHistorial.Location = new Point(0, 529);
            BtnHistorial.Name = "BtnHistorial";
            BtnHistorial.Size = new Size(238, 63);
            BtnHistorial.TabIndex = 7;
            BtnHistorial.Text = "Historial Medico";
            BtnHistorial.TextAlign = ContentAlignment.MiddleLeft;
            BtnHistorial.TextImageRelation = TextImageRelation.ImageBeforeText;
            BtnHistorial.UseVisualStyleBackColor = true;
            BtnHistorial.Click += BtnHistorial_Click;
            // 
            // BtnMascotas
            // 
            BtnMascotas.Dock = DockStyle.Top;
            BtnMascotas.FlatAppearance.BorderSize = 0;
            BtnMascotas.FlatStyle = FlatStyle.Flat;
            BtnMascotas.Font = new Font("Arial Narrow", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            BtnMascotas.ForeColor = SystemColors.ControlLightLight;
            BtnMascotas.IconChar = FontAwesome.Sharp.IconChar.Paw;
            BtnMascotas.IconColor = Color.White;
            BtnMascotas.IconFont = FontAwesome.Sharp.IconFont.Auto;
            BtnMascotas.IconSize = 50;
            BtnMascotas.ImageAlign = ContentAlignment.MiddleLeft;
            BtnMascotas.Location = new Point(0, 466);
            BtnMascotas.Name = "BtnMascotas";
            BtnMascotas.Size = new Size(238, 63);
            BtnMascotas.TabIndex = 6;
            BtnMascotas.Text = "Mascotas";
            BtnMascotas.TextAlign = ContentAlignment.MiddleLeft;
            BtnMascotas.TextImageRelation = TextImageRelation.ImageBeforeText;
            BtnMascotas.UseVisualStyleBackColor = true;
            BtnMascotas.Click += BtnMascotas_Click;
            // 
            // BtnProductos
            // 
            BtnProductos.Dock = DockStyle.Top;
            BtnProductos.FlatAppearance.BorderSize = 0;
            BtnProductos.FlatStyle = FlatStyle.Flat;
            BtnProductos.Font = new Font("Arial Narrow", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            BtnProductos.ForeColor = SystemColors.ControlLightLight;
            BtnProductos.IconChar = FontAwesome.Sharp.IconChar.BoxOpen;
            BtnProductos.IconColor = Color.White;
            BtnProductos.IconFont = FontAwesome.Sharp.IconFont.Auto;
            BtnProductos.IconSize = 50;
            BtnProductos.ImageAlign = ContentAlignment.MiddleLeft;
            BtnProductos.Location = new Point(0, 403);
            BtnProductos.Name = "BtnProductos";
            BtnProductos.Size = new Size(238, 63);
            BtnProductos.TabIndex = 5;
            BtnProductos.Text = "Productos";
            BtnProductos.TextAlign = ContentAlignment.MiddleLeft;
            BtnProductos.TextImageRelation = TextImageRelation.ImageBeforeText;
            BtnProductos.UseVisualStyleBackColor = true;
            BtnProductos.Click += BtnProductos_Click;
            // 
            // BtnVentas
            // 
            BtnVentas.Dock = DockStyle.Top;
            BtnVentas.FlatAppearance.BorderSize = 0;
            BtnVentas.FlatStyle = FlatStyle.Flat;
            BtnVentas.Font = new Font("Arial Narrow", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            BtnVentas.ForeColor = SystemColors.ControlLightLight;
            BtnVentas.IconChar = FontAwesome.Sharp.IconChar.Shopify;
            BtnVentas.IconColor = Color.White;
            BtnVentas.IconFont = FontAwesome.Sharp.IconFont.Auto;
            BtnVentas.IconSize = 50;
            BtnVentas.ImageAlign = ContentAlignment.MiddleLeft;
            BtnVentas.Location = new Point(0, 340);
            BtnVentas.Name = "BtnVentas";
            BtnVentas.Size = new Size(238, 63);
            BtnVentas.TabIndex = 4;
            BtnVentas.Text = "Ventas";
            BtnVentas.TextAlign = ContentAlignment.MiddleLeft;
            BtnVentas.TextImageRelation = TextImageRelation.ImageBeforeText;
            BtnVentas.UseVisualStyleBackColor = true;
            BtnVentas.Click += BtnVentas_Click;
            // 
            // BtnInventario
            // 
            BtnInventario.Dock = DockStyle.Top;
            BtnInventario.FlatAppearance.BorderSize = 0;
            BtnInventario.FlatStyle = FlatStyle.Flat;
            BtnInventario.Font = new Font("Arial Narrow", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            BtnInventario.ForeColor = SystemColors.ControlLightLight;
            BtnInventario.IconChar = FontAwesome.Sharp.IconChar.BoxesStacked;
            BtnInventario.IconColor = Color.White;
            BtnInventario.IconFont = FontAwesome.Sharp.IconFont.Auto;
            BtnInventario.IconSize = 50;
            BtnInventario.ImageAlign = ContentAlignment.MiddleLeft;
            BtnInventario.Location = new Point(0, 277);
            BtnInventario.Name = "BtnInventario";
            BtnInventario.Size = new Size(238, 63);
            BtnInventario.TabIndex = 3;
            BtnInventario.Text = "Inventario";
            BtnInventario.TextAlign = ContentAlignment.MiddleLeft;
            BtnInventario.TextImageRelation = TextImageRelation.ImageBeforeText;
            BtnInventario.UseVisualStyleBackColor = true;
            BtnInventario.Click += BtnInventario_Click;
            // 
            // BtnPersonal
            // 
            BtnPersonal.Dock = DockStyle.Top;
            BtnPersonal.FlatAppearance.BorderSize = 0;
            BtnPersonal.FlatStyle = FlatStyle.Flat;
            BtnPersonal.Font = new Font("Arial Narrow", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            BtnPersonal.ForeColor = SystemColors.ControlLightLight;
            BtnPersonal.IconChar = FontAwesome.Sharp.IconChar.UserGear;
            BtnPersonal.IconColor = Color.White;
            BtnPersonal.IconFont = FontAwesome.Sharp.IconFont.Auto;
            BtnPersonal.IconSize = 50;
            BtnPersonal.ImageAlign = ContentAlignment.MiddleLeft;
            BtnPersonal.Location = new Point(0, 214);
            BtnPersonal.Name = "BtnPersonal";
            BtnPersonal.Size = new Size(238, 63);
            BtnPersonal.TabIndex = 2;
            BtnPersonal.Text = "Personal";
            BtnPersonal.TextAlign = ContentAlignment.MiddleLeft;
            BtnPersonal.TextImageRelation = TextImageRelation.ImageBeforeText;
            BtnPersonal.UseVisualStyleBackColor = true;
            BtnPersonal.Click += BtnPersonal_Click;
            // 
            // BtnClientes
            // 
            BtnClientes.Dock = DockStyle.Top;
            BtnClientes.FlatAppearance.BorderSize = 0;
            BtnClientes.FlatStyle = FlatStyle.Flat;
            BtnClientes.Font = new Font("Arial Narrow", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            BtnClientes.ForeColor = SystemColors.ControlLightLight;
            BtnClientes.IconChar = FontAwesome.Sharp.IconChar.Users;
            BtnClientes.IconColor = Color.White;
            BtnClientes.IconFont = FontAwesome.Sharp.IconFont.Auto;
            BtnClientes.IconSize = 50;
            BtnClientes.ImageAlign = ContentAlignment.MiddleLeft;
            BtnClientes.Location = new Point(0, 141);
            BtnClientes.Name = "BtnClientes";
            BtnClientes.Size = new Size(238, 73);
            BtnClientes.TabIndex = 1;
            BtnClientes.Text = "Clientes";
            BtnClientes.TextAlign = ContentAlignment.MiddleLeft;
            BtnClientes.TextImageRelation = TextImageRelation.ImageBeforeText;
            BtnClientes.UseVisualStyleBackColor = true;
            BtnClientes.Click += BtnClientes_Click;
            // 
            // pictureBox1
            // 
            pictureBox1.Dock = DockStyle.Top;
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(0, 0);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(238, 141);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // panelContent
            // 
            panelContent.BackColor = Color.FromArgb(236, 240, 241);
            panelContent.Controls.Add(panel1);
            panelContent.Controls.Add(panel2);
            panelContent.Dock = DockStyle.Fill;
            panelContent.ForeColor = Color.Cornsilk;
            panelContent.Location = new Point(238, 0);
            panelContent.Margin = new Padding(3, 4, 3, 4);
            panelContent.Name = "panelContent";
            panelContent.Size = new Size(1133, 933);
            panelContent.TabIndex = 2;
            // 
            // panel1
            // 
            panel1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panel1.BackColor = Color.MediumOrchid;
            panel1.Controls.Add(lblHora);
            panel1.Controls.Add(lblEmail);
            panel1.Controls.Add(iconButton2);
            panel1.Controls.Add(iconButton1);
            panel1.Controls.Add(lblUsuario);
            panel1.Controls.Add(tituloSuperior);
            panel1.Controls.Add(iconoSuperior);
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(1133, 100);
            panel1.TabIndex = 12;
            // 
            // lblHora
            // 
            lblHora.Anchor = AnchorStyles.Left;
            lblHora.AutoSize = true;
            lblHora.Font = new Font("Arial Narrow", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblHora.Location = new Point(217, 39);
            lblHora.Name = "lblHora";
            lblHora.Size = new Size(58, 27);
            lblHora.TabIndex = 8;
            lblHora.Text = "00:00";
            // 
            // lblEmail
            // 
            lblEmail.Anchor = AnchorStyles.Right;
            lblEmail.AutoSize = true;
            lblEmail.Font = new Font("Arial Narrow", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblEmail.Location = new Point(726, 49);
            lblEmail.Name = "lblEmail";
            lblEmail.Size = new Size(160, 24);
            lblEmail.TabIndex = 7;
            lblEmail.Text = "Correo Electronico:";
            // 
            // iconButton2
            // 
            iconButton2.Anchor = AnchorStyles.Right;
            iconButton2.BackColor = Color.MediumOrchid;
            iconButton2.IconChar = FontAwesome.Sharp.IconChar.Bell;
            iconButton2.IconColor = Color.White;
            iconButton2.IconFont = FontAwesome.Sharp.IconFont.Auto;
            iconButton2.Location = new Point(960, 12);
            iconButton2.Name = "iconButton2";
            iconButton2.Size = new Size(73, 73);
            iconButton2.TabIndex = 6;
            iconButton2.UseVisualStyleBackColor = false;
            // 
            // iconButton1
            // 
            iconButton1.Anchor = AnchorStyles.Right;
            iconButton1.BackColor = Color.MediumOrchid;
            iconButton1.IconChar = FontAwesome.Sharp.IconChar.UserDoctor;
            iconButton1.IconColor = Color.White;
            iconButton1.IconFont = FontAwesome.Sharp.IconFont.Auto;
            iconButton1.Location = new Point(1048, 12);
            iconButton1.Name = "iconButton1";
            iconButton1.Size = new Size(73, 73);
            iconButton1.TabIndex = 5;
            iconButton1.UseVisualStyleBackColor = false;
            // 
            // lblUsuario
            // 
            lblUsuario.Anchor = AnchorStyles.Right;
            lblUsuario.AutoSize = true;
            lblUsuario.Font = new Font("Arial Narrow", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblUsuario.Location = new Point(798, 22);
            lblUsuario.Name = "lblUsuario";
            lblUsuario.Size = new Size(88, 27);
            lblUsuario.TabIndex = 4;
            lblUsuario.Text = "Usuario:";
            // 
            // tituloSuperior
            // 
            tituloSuperior.Anchor = AnchorStyles.Left;
            tituloSuperior.AutoSize = true;
            tituloSuperior.Font = new Font("Arial Narrow", 16.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            tituloSuperior.Location = new Point(95, 35);
            tituloSuperior.Name = "tituloSuperior";
            tituloSuperior.Size = new Size(79, 33);
            tituloSuperior.TabIndex = 1;
            tituloSuperior.Text = "Home";
            // 
            // iconoSuperior
            // 
            iconoSuperior.Anchor = AnchorStyles.Left;
            iconoSuperior.BackColor = Color.MediumOrchid;
            iconoSuperior.IconChar = FontAwesome.Sharp.IconChar.HomeLg;
            iconoSuperior.IconColor = Color.White;
            iconoSuperior.IconFont = FontAwesome.Sharp.IconFont.Auto;
            iconoSuperior.Location = new Point(16, 12);
            iconoSuperior.Name = "iconoSuperior";
            iconoSuperior.Size = new Size(73, 73);
            iconoSuperior.TabIndex = 0;
            iconoSuperior.UseVisualStyleBackColor = false;
            // 
            // panel2
            // 
            panel2.BackColor = Color.Thistle;
            panel2.Controls.Add(pictureBox12);
            panel2.Controls.Add(pictureBox3);
            panel2.Controls.Add(pictureBox11);
            panel2.Controls.Add(pictureBox8);
            panel2.Controls.Add(lblTitulo);
            panel2.Controls.Add(pictureBox6);
            panel2.Controls.Add(pictureBox10);
            panel2.Controls.Add(pictureBox9);
            panel2.Controls.Add(lblSubtitulo);
            panel2.Dock = DockStyle.Fill;
            panel2.Location = new Point(0, 0);
            panel2.Name = "panel2";
            panel2.Size = new Size(1133, 933);
            panel2.TabIndex = 13;
            // 
            // pictureBox12
            // 
            pictureBox12.Anchor = AnchorStyles.None;
            pictureBox12.Image = (Image)resources.GetObject("pictureBox12.Image");
            pictureBox12.Location = new Point(935, 607);
            pictureBox12.Name = "pictureBox12";
            pictureBox12.Size = new Size(164, 160);
            pictureBox12.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox12.TabIndex = 11;
            pictureBox12.TabStop = false;
            // 
            // pictureBox3
            // 
            pictureBox3.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            pictureBox3.Image = (Image)resources.GetObject("pictureBox3.Image");
            pictureBox3.Location = new Point(418, 195);
            pictureBox3.Name = "pictureBox3";
            pictureBox3.Size = new Size(268, 241);
            pictureBox3.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox3.TabIndex = 0;
            pictureBox3.TabStop = false;
            // 
            // pictureBox11
            // 
            pictureBox11.Anchor = AnchorStyles.None;
            pictureBox11.Image = (Image)resources.GetObject("pictureBox11.Image");
            pictureBox11.Location = new Point(752, 607);
            pictureBox11.Name = "pictureBox11";
            pictureBox11.Size = new Size(164, 160);
            pictureBox11.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox11.TabIndex = 10;
            pictureBox11.TabStop = false;
            // 
            // pictureBox8
            // 
            pictureBox8.Anchor = AnchorStyles.None;
            pictureBox8.Image = (Image)resources.GetObject("pictureBox8.Image");
            pictureBox8.Location = new Point(210, 607);
            pictureBox8.Name = "pictureBox8";
            pictureBox8.Size = new Size(164, 160);
            pictureBox8.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox8.TabIndex = 7;
            pictureBox8.TabStop = false;
            // 
            // lblTitulo
            // 
            lblTitulo.Anchor = AnchorStyles.None;
            lblTitulo.AutoSize = true;
            lblTitulo.Font = new Font("Britannic Bold", 22.2F);
            lblTitulo.ForeColor = Color.DarkSlateBlue;
            lblTitulo.Location = new Point(338, 482);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(454, 41);
            lblTitulo.TabIndex = 1;
            lblTitulo.Text = "VETERINARIA -ZOOFIPETSS-";
            lblTitulo.Click += lblTitulo_Click;
            // 
            // pictureBox6
            // 
            pictureBox6.Anchor = AnchorStyles.None;
            pictureBox6.Image = (Image)resources.GetObject("pictureBox6.Image");
            pictureBox6.Location = new Point(24, 607);
            pictureBox6.Name = "pictureBox6";
            pictureBox6.Size = new Size(164, 160);
            pictureBox6.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox6.TabIndex = 6;
            pictureBox6.TabStop = false;
            // 
            // pictureBox10
            // 
            pictureBox10.Anchor = AnchorStyles.None;
            pictureBox10.Image = (Image)resources.GetObject("pictureBox10.Image");
            pictureBox10.Location = new Point(572, 607);
            pictureBox10.Name = "pictureBox10";
            pictureBox10.Size = new Size(164, 160);
            pictureBox10.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox10.TabIndex = 9;
            pictureBox10.TabStop = false;
            // 
            // pictureBox9
            // 
            pictureBox9.Anchor = AnchorStyles.None;
            pictureBox9.Image = (Image)resources.GetObject("pictureBox9.Image");
            pictureBox9.Location = new Point(393, 607);
            pictureBox9.Name = "pictureBox9";
            pictureBox9.Size = new Size(164, 160);
            pictureBox9.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox9.TabIndex = 8;
            pictureBox9.TabStop = false;
            // 
            // lblSubtitulo
            // 
            lblSubtitulo.Anchor = AnchorStyles.None;
            lblSubtitulo.AutoSize = true;
            lblSubtitulo.Font = new Font("Century", 13.8F, FontStyle.Bold);
            lblSubtitulo.ForeColor = Color.DimGray;
            lblSubtitulo.Location = new Point(406, 545);
            lblSubtitulo.Name = "lblSubtitulo";
            lblSubtitulo.Size = new Size(303, 28);
            lblSubtitulo.TabIndex = 2;
            lblSubtitulo.Text = "Desarrollado en la UPDS";
            // 
            // timer1
            // 
            timer1.Interval = 1000;
            timer1.Tick += timer1_Tick;
            // 
            // Dashboard
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1371, 933);
            Controls.Add(panelContent);
            Controls.Add(panelSidebar);
            Margin = new Padding(3, 4, 3, 4);
            Name = "Dashboard";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Veterinaria - Dashboard";
            WindowState = FormWindowState.Maximized;
            Load += Dashboard_Load;
            panelSidebar.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            panelContent.ResumeLayout(false);
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox12).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox11).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox8).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox6).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox10).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox9).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private Panel panelSidebar;
        private Panel panelContent;
        private Panel panel1;
        private FontAwesome.Sharp.IconButton BtnClientes;
        private PictureBox pictureBox1;
        private FontAwesome.Sharp.IconButton BtnReportes;
        private FontAwesome.Sharp.IconButton BtnConsultas;
        private FontAwesome.Sharp.IconButton BtnHistorial;
        private FontAwesome.Sharp.IconButton BtnMascotas;
        private FontAwesome.Sharp.IconButton BtnProductos;
        private FontAwesome.Sharp.IconButton BtnVentas;
        private FontAwesome.Sharp.IconButton BtnInventario;
        private FontAwesome.Sharp.IconButton BtnPersonal;
        private FontAwesome.Sharp.IconButton BtnConfiguracion;
        private FontAwesome.Sharp.IconButton BtnDashboard;
        private FontAwesome.Sharp.IconButton iconoSuperior;
        private Label tituloSuperior;
        private Label lblUsuario;
        private PictureBox pictureBox2;
        private FontAwesome.Sharp.IconButton iconButton2;
        private FontAwesome.Sharp.IconButton iconButton1;
        private Label lblEmail;
        private Label lblHora;
        private System.Windows.Forms.Timer timer1;
        private Panel panel2;
        private PictureBox pictureBox12;
        private PictureBox pictureBox3;
        private PictureBox pictureBox11;
        private PictureBox pictureBox8;
        private Label lblTitulo;
        private PictureBox pictureBox6;
        private PictureBox pictureBox10;
        private PictureBox pictureBox9;
        private Label lblSubtitulo;
    }
}
