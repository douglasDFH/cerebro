namespace SistemaZoofiPets
{
    partial class VentanaPrincipal
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
            this.btn_cerrar = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.Btn_menu_Usuarios = new System.Windows.Forms.ToolStripMenuItem();
            this.Btn_menu_Inventario = new System.Windows.Forms.ToolStripMenuItem();
            this.Btn_menu_Ventas = new System.Windows.Forms.ToolStripMenuItem();
            this.Btn_menu_Reportes = new System.Windows.Forms.ToolStripMenuItem();
            this.historialDeFacturasToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_cerrar
            // 
            this.btn_cerrar.Location = new System.Drawing.Point(830, 445);
            this.btn_cerrar.Name = "btn_cerrar";
            this.btn_cerrar.Size = new System.Drawing.Size(138, 52);
            this.btn_cerrar.TabIndex = 0;
            this.btn_cerrar.Text = "Cerrar";
            this.btn_cerrar.UseVisualStyleBackColor = true;
            this.btn_cerrar.Click += new System.EventHandler(this.btn_cerrar_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.Btn_menu_Usuarios,
            this.Btn_menu_Inventario,
            this.Btn_menu_Ventas,
            this.Btn_menu_Reportes});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(980, 28);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem2});
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(73, 24);
            this.toolStripMenuItem1.Text = "Archivo";
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(176, 26);
            this.toolStripMenuItem2.Text = "Gestión Clínica";
            // 
            // Btn_menu_Usuarios
            // 
            this.Btn_menu_Usuarios.Name = "Btn_menu_Usuarios";
            this.Btn_menu_Usuarios.Size = new System.Drawing.Size(79, 24);
            this.Btn_menu_Usuarios.Text = "Usuarios";
            this.Btn_menu_Usuarios.Click += new System.EventHandler(this.Btn_menu_Usuarios_Click);
            // 
            // Btn_menu_Inventario
            // 
            this.Btn_menu_Inventario.Name = "Btn_menu_Inventario";
            this.Btn_menu_Inventario.Size = new System.Drawing.Size(89, 24);
            this.Btn_menu_Inventario.Text = "Inventario";
            this.Btn_menu_Inventario.Click += new System.EventHandler(this.Btn_menu_Inventario_Click);
            // 
            // Btn_menu_Ventas
            // 
            this.Btn_menu_Ventas.Name = "Btn_menu_Ventas";
            this.Btn_menu_Ventas.Size = new System.Drawing.Size(66, 24);
            this.Btn_menu_Ventas.Text = "Ventas";
            this.Btn_menu_Ventas.Click += new System.EventHandler(this.Btn_menu_Ventas_Click);
            // 
            // Btn_menu_Reportes
            // 
            this.Btn_menu_Reportes.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.historialDeFacturasToolStripMenuItem});
            this.Btn_menu_Reportes.Name = "Btn_menu_Reportes";
            this.Btn_menu_Reportes.Size = new System.Drawing.Size(82, 24);
            this.Btn_menu_Reportes.Text = "Reportes";
            this.Btn_menu_Reportes.Click += new System.EventHandler(this.Btn_menu_Reportes_Click);
            // 
            // historialDeFacturasToolStripMenuItem
            // 
            this.historialDeFacturasToolStripMenuItem.Name = "historialDeFacturasToolStripMenuItem";
            this.historialDeFacturasToolStripMenuItem.Size = new System.Drawing.Size(226, 26);
            this.historialDeFacturasToolStripMenuItem.Text = "Historial de Facturas";
            // 
            // VentanaPrincipal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(980, 584);
            this.Controls.Add(this.btn_cerrar);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "VentanaPrincipal";
            this.Text = "VentanaPrincipal";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_cerrar;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem Btn_menu_Usuarios;
        private System.Windows.Forms.ToolStripMenuItem Btn_menu_Inventario;
        private System.Windows.Forms.ToolStripMenuItem Btn_menu_Ventas;
        private System.Windows.Forms.ToolStripMenuItem Btn_menu_Reportes;
        private System.Windows.Forms.ToolStripMenuItem historialDeFacturasToolStripMenuItem;
    }
}