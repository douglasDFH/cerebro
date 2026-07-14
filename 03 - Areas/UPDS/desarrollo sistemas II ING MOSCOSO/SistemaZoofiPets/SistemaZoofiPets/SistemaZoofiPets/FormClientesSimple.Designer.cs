namespace SistemaZoofiPets
{
    partial class FormClientesSimple
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.DataGridView dgv_clientes;
        private System.Windows.Forms.Button btn_agregar;
        private System.Windows.Forms.Button btn_modificar;
        private System.Windows.Forms.Button btn_eliminar;
        private System.Windows.Forms.GroupBox gb_clientes;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.dgv_clientes = new System.Windows.Forms.DataGridView();
            this.btn_agregar = new System.Windows.Forms.Button();
            this.btn_modificar = new System.Windows.Forms.Button();
            this.btn_eliminar = new System.Windows.Forms.Button();
            this.gb_clientes = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_clientes)).BeginInit();
            this.gb_clientes.SuspendLayout();
            this.SuspendLayout();
            
            // dgv_clientes
            this.dgv_clientes.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgv_clientes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_clientes.Location = new System.Drawing.Point(6, 19);
            this.dgv_clientes.Name = "dgv_clientes";
            this.dgv_clientes.ReadOnly = true;
            this.dgv_clientes.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_clientes.Size = new System.Drawing.Size(600, 300);
            this.dgv_clientes.TabIndex = 0;
            
            // btn_agregar
            this.btn_agregar.BackColor = ColoresPastelGlobales.VerdeSuave;
            this.btn_agregar.ForeColor = ColoresPastelGlobales.NegroCarbon;
            this.btn_agregar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_agregar.FlatAppearance.BorderColor = ColoresPastelGlobales.NegroSuave;
            this.btn_agregar.FlatAppearance.BorderSize = 1;
            this.btn_agregar.Location = new System.Drawing.Point(12, 12);
            this.btn_agregar.Name = "btn_agregar";
            this.btn_agregar.Size = new System.Drawing.Size(100, 35);
            this.btn_agregar.TabIndex = 1;
            this.btn_agregar.Text = "Agregar";
            this.btn_agregar.UseVisualStyleBackColor = false;
            this.btn_agregar.Click += new System.EventHandler(this.btn_agregar_Click);
            
            // btn_modificar
            this.btn_modificar.BackColor = ColoresPastelGlobales.NaranjaSuave;
            this.btn_modificar.ForeColor = ColoresPastelGlobales.NegroCarbon;
            this.btn_modificar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_modificar.FlatAppearance.BorderColor = ColoresPastelGlobales.NegroSuave;
            this.btn_modificar.FlatAppearance.BorderSize = 1;
            this.btn_modificar.Location = new System.Drawing.Point(130, 12);
            this.btn_modificar.Name = "btn_modificar";
            this.btn_modificar.Size = new System.Drawing.Size(100, 35);
            this.btn_modificar.TabIndex = 2;
            this.btn_modificar.Text = "Modificar";
            this.btn_modificar.UseVisualStyleBackColor = false;
            this.btn_modificar.Click += new System.EventHandler(this.btn_modificar_Click);
            
            // btn_eliminar
            this.btn_eliminar.BackColor = ColoresPastelGlobales.RojoSuave;
            this.btn_eliminar.ForeColor = ColoresPastelGlobales.NegroCarbon;
            this.btn_eliminar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_eliminar.FlatAppearance.BorderColor = ColoresPastelGlobales.NegroSuave;
            this.btn_eliminar.FlatAppearance.BorderSize = 1;
            this.btn_eliminar.Location = new System.Drawing.Point(248, 12);
            this.btn_eliminar.Name = "btn_eliminar";
            this.btn_eliminar.Size = new System.Drawing.Size(100, 35);
            this.btn_eliminar.TabIndex = 3;
            this.btn_eliminar.Text = "Eliminar";
            this.btn_eliminar.UseVisualStyleBackColor = false;
            this.btn_eliminar.Click += new System.EventHandler(this.btn_eliminar_Click);
            
            // gb_clientes
            this.gb_clientes.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gb_clientes.BackColor = ColoresPastelGlobales.Formularios.PanelContenido;
            this.gb_clientes.ForeColor = ColoresPastelGlobales.Formularios.TextoNormal;
            this.gb_clientes.Controls.Add(this.dgv_clientes);
            this.gb_clientes.Location = new System.Drawing.Point(12, 60);
            this.gb_clientes.Name = "gb_clientes";
            this.gb_clientes.Size = new System.Drawing.Size(620, 330);
            this.gb_clientes.TabIndex = 4;
            this.gb_clientes.TabStop = false;
            this.gb_clientes.Text = "Lista de Clientes";
            
            // FormClientesSimple
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = ColoresPastelGlobales.Formularios.FondoPrincipal;
            this.ClientSize = new System.Drawing.Size(650, 400);
            this.Controls.Add(this.btn_agregar);
            this.Controls.Add(this.btn_modificar);
            this.Controls.Add(this.btn_eliminar);
            this.Controls.Add(this.gb_clientes);
            this.MinimumSize = new System.Drawing.Size(650, 400);
            this.Name = "FormClientesSimple";
            this.Text = "Gestión de Clientes - Sistema Veterinario ZoofiPets";
            this.Load += new System.EventHandler(this.FormClientesSimple_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_clientes)).EndInit();
            this.gb_clientes.ResumeLayout(false);
            this.ResumeLayout(false);
        }
    }
}