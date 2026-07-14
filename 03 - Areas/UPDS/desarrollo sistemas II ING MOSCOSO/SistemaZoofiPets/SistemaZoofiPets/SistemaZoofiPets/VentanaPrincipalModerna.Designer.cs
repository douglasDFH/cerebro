namespace SistemaZoofiPets
{
    partial class VentanaPrincipalModerna
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
            this.panelMenu = new System.Windows.Forms.Panel();
            this.panelTitleBar = new System.Windows.Forms.Panel();
            this.lblTitleChildForm = new System.Windows.Forms.Label();
            this.iconCurrentUser = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.iconCurrentChildForm = new System.Windows.Forms.Button();
            this.panelDesktop = new System.Windows.Forms.Panel();
            this.panelTitleBar.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelMenu
            // 
            this.panelMenu.BackColor = ColoresPastelGlobales.Navegacion.MenuLateral;
            this.panelMenu.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelMenu.Location = new System.Drawing.Point(0, 0);
            this.panelMenu.Name = "panelMenu";
            this.panelMenu.Size = new System.Drawing.Size(250, 700);
            this.panelMenu.TabIndex = 0;
            // 
            // panelTitleBar
            // 
            this.panelTitleBar.BackColor = ColoresPastelGlobales.Navegacion.BarraTitulo;
            this.panelTitleBar.Controls.Add(this.iconCurrentChildForm);
            this.panelTitleBar.Controls.Add(this.btnExit);
            this.panelTitleBar.Controls.Add(this.iconCurrentUser);
            this.panelTitleBar.Controls.Add(this.lblTitleChildForm);
            this.panelTitleBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTitleBar.Location = new System.Drawing.Point(250, 0);
            this.panelTitleBar.Name = "panelTitleBar";
            this.panelTitleBar.Size = new System.Drawing.Size(950, 65);
            this.panelTitleBar.TabIndex = 1;
            this.panelTitleBar.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panelTitleBar_MouseDown);
            // 
            // lblTitleChildForm
            // 
            this.lblTitleChildForm.AutoSize = true;
            this.lblTitleChildForm.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitleChildForm.ForeColor = ColoresPastelGlobales.Navegacion.TextoTitulo;
            this.lblTitleChildForm.Location = new System.Drawing.Point(100, 20);
            this.lblTitleChildForm.Name = "lblTitleChildForm";
            this.lblTitleChildForm.Size = new System.Drawing.Size(52, 21);
            this.lblTitleChildForm.TabIndex = 0;
            this.lblTitleChildForm.Text = "Inicio";
            // 
            // iconCurrentUser
            // 
            this.iconCurrentUser.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.iconCurrentUser.BackColor = ColoresPastelGlobales.Navegacion.BarraTitulo;
            this.iconCurrentUser.FlatAppearance.BorderSize = 0;
            this.iconCurrentUser.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.iconCurrentUser.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.iconCurrentUser.ForeColor = ColoresPastelGlobales.Navegacion.IconosActivos;
            this.iconCurrentUser.Location = new System.Drawing.Point(710, 0);
            this.iconCurrentUser.Name = "iconCurrentUser";
            this.iconCurrentUser.Size = new System.Drawing.Size(190, 65);
            this.iconCurrentUser.TabIndex = 1;
            this.iconCurrentUser.Text = "👤 Usuario";
            this.iconCurrentUser.UseVisualStyleBackColor = false;
            // 
            // btnExit
            // 
            this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExit.BackColor = ColoresPastelGlobales.Navegacion.BarraTitulo;
            this.btnExit.FlatAppearance.BorderSize = 0;
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExit.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExit.ForeColor = ColoresPastelGlobales.Navegacion.TextoBoton;
            this.btnExit.Location = new System.Drawing.Point(900, 0);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(45, 30);
            this.btnExit.TabIndex = 2;
            this.btnExit.Text = "✕";
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // iconCurrentChildForm
            // 
            this.iconCurrentChildForm.BackColor = ColoresPastelGlobales.Navegacion.BarraTitulo;
            this.iconCurrentChildForm.FlatAppearance.BorderSize = 0;
            this.iconCurrentChildForm.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.iconCurrentChildForm.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.iconCurrentChildForm.ForeColor = ColoresPastelGlobales.Navegacion.IconosActivos;
            this.iconCurrentChildForm.Location = new System.Drawing.Point(20, 10);
            this.iconCurrentChildForm.Name = "iconCurrentChildForm";
            this.iconCurrentChildForm.Size = new System.Drawing.Size(60, 45);
            this.iconCurrentChildForm.TabIndex = 3;
            this.iconCurrentChildForm.Text = "🏠";
            this.iconCurrentChildForm.UseVisualStyleBackColor = false;
            // 
            // panelDesktop
            // 
            this.panelDesktop.BackColor = ColoresPastelGlobales.Navegacion.AreaPrincipal;
            this.panelDesktop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelDesktop.Location = new System.Drawing.Point(250, 65);
            this.panelDesktop.Name = "panelDesktop";
            this.panelDesktop.Size = new System.Drawing.Size(950, 635);
            this.panelDesktop.TabIndex = 2;
            // 
            // VentanaPrincipalModerna
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = ColoresPastelGlobales.VioletaMuyClaro;
            this.ClientSize = new System.Drawing.Size(1200, 700);
            this.Controls.Add(this.panelDesktop);
            this.Controls.Add(this.panelTitleBar);
            this.Controls.Add(this.panelMenu);
            this.Name = "VentanaPrincipalModerna";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Sistema Clínica Veterinaria ZoofiPets";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.VentanaPrincipalModerna_Load);
            this.panelTitleBar.ResumeLayout(false);
            this.panelTitleBar.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelMenu;
        private System.Windows.Forms.Panel panelTitleBar;
        private System.Windows.Forms.Panel panelDesktop;
        private System.Windows.Forms.Label lblTitleChildForm;
        private System.Windows.Forms.Button iconCurrentUser;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button iconCurrentChildForm;
    }
}