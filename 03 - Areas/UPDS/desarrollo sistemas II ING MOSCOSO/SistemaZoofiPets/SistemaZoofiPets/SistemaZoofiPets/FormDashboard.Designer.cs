namespace SistemaZoofiPets
{
    partial class FormDashboard
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.lblTotalAnimales = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.lblCitasHoy = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.lblVentasHoy = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.lblInventarioBajo = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = ColoresPastelGlobales.Dashboard.PanelAnimales;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.lblTotalAnimales);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(50, 100);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(150, 120);
            this.panel1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(10, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(130, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "🐾 Total Animales";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTotalAnimales
            // 
            this.lblTotalAnimales.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalAnimales.ForeColor = ColoresPastelGlobales.Dashboard.TextoPrincipal;
            this.lblTotalAnimales.Location = new System.Drawing.Point(10, 45);
            this.lblTotalAnimales.Name = "lblTotalAnimales";
            this.lblTotalAnimales.Size = new System.Drawing.Size(130, 60);
            this.lblTotalAnimales.TabIndex = 1;
            this.lblTotalAnimales.Text = "0";
            this.lblTotalAnimales.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel2
            // 
            this.panel2.BackColor = ColoresPastelGlobales.Dashboard.PanelCitas;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.lblCitasHoy);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Location = new System.Drawing.Point(250, 100);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(150, 120);
            this.panel2.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(10, 10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(130, 25);
            this.label2.TabIndex = 0;
            this.label2.Text = "📅 Citas Hoy";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblCitasHoy
            // 
            this.lblCitasHoy.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCitasHoy.ForeColor = ColoresPastelGlobales.Dashboard.TextoPrincipal;
            this.lblCitasHoy.Location = new System.Drawing.Point(10, 45);
            this.lblCitasHoy.Name = "lblCitasHoy";
            this.lblCitasHoy.Size = new System.Drawing.Size(130, 60);
            this.lblCitasHoy.TabIndex = 1;
            this.lblCitasHoy.Text = "0";
            this.lblCitasHoy.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel3
            // 
            this.panel3.BackColor = ColoresPastelGlobales.Dashboard.PanelVentas;
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.lblVentasHoy);
            this.panel3.Controls.Add(this.label3);
            this.panel3.Location = new System.Drawing.Point(450, 100);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(150, 120);
            this.panel3.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(10, 10);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(130, 25);
            this.label3.TabIndex = 0;
            this.label3.Text = "💰 Ventas Hoy";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblVentasHoy
            // 
            this.lblVentasHoy.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVentasHoy.ForeColor = ColoresPastelGlobales.VerdeSuave;
            this.lblVentasHoy.Location = new System.Drawing.Point(10, 45);
            this.lblVentasHoy.Name = "lblVentasHoy";
            this.lblVentasHoy.Size = new System.Drawing.Size(130, 60);
            this.lblVentasHoy.TabIndex = 1;
            this.lblVentasHoy.Text = "$0.00";
            this.lblVentasHoy.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel4
            // 
            this.panel4.BackColor = ColoresPastelGlobales.Dashboard.PanelInventario;
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel4.Controls.Add(this.lblInventarioBajo);
            this.panel4.Controls.Add(this.label4);
            this.panel4.Location = new System.Drawing.Point(650, 100);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(150, 120);
            this.panel4.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(10, 10);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(130, 25);
            this.label4.TabIndex = 0;
            this.label4.Text = "📦 Stock Bajo";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblInventarioBajo
            // 
            this.lblInventarioBajo.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInventarioBajo.ForeColor = ColoresPastelGlobales.NaranjaSuave;
            this.lblInventarioBajo.Location = new System.Drawing.Point(10, 45);
            this.lblInventarioBajo.Name = "lblInventarioBajo";
            this.lblInventarioBajo.Size = new System.Drawing.Size(130, 60);
            this.lblInventarioBajo.TabIndex = 1;
            this.lblInventarioBajo.Text = "0";
            this.lblInventarioBajo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = ColoresPastelGlobales.VioletaProfundo;
            this.label5.Location = new System.Drawing.Point(50, 30);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(500, 40);
            this.label5.TabIndex = 4;
            this.label5.Text = "🏥 Dashboard - Clínica Veterinaria ZoofiPets";
            // 
            // FormDashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = ColoresPastelGlobales.Dashboard.FondoPrincipal;
            this.ClientSize = new System.Drawing.Size(850, 300);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormDashboard";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Dashboard - Clínica Veterinaria";
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label lblTotalAnimales;
        private System.Windows.Forms.Label lblCitasHoy;
        private System.Windows.Forms.Label lblVentasHoy;
        private System.Windows.Forms.Label lblInventarioBajo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
    }
}