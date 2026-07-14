namespace SistemaZoofiPets
{
    partial class FormClientes
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.DataGridView dgv_clientes;
        private System.Windows.Forms.TextBox txt_dni;
        private System.Windows.Forms.TextBox txt_nombre;
        private System.Windows.Forms.TextBox txt_apellido;
        private System.Windows.Forms.TextBox txt_email;
        private System.Windows.Forms.TextBox txt_telefono;
        private System.Windows.Forms.TextBox txt_direccion;
        private System.Windows.Forms.DateTimePicker dtp_fecha_nacimiento;
        private System.Windows.Forms.TextBox txt_buscar;
        private System.Windows.Forms.Button btn_agregar;
        private System.Windows.Forms.Button btn_modificar;
        private System.Windows.Forms.Button btn_eliminar;
        private System.Windows.Forms.Button btn_buscar;
        private System.Windows.Forms.Button btn_limpiar;
        private System.Windows.Forms.Label lbl_total_registros;
        private System.Windows.Forms.Label lbl_dni;
        private System.Windows.Forms.Label lbl_nombre;
        private System.Windows.Forms.Label lbl_apellido;
        private System.Windows.Forms.Label lbl_email;
        private System.Windows.Forms.Label lbl_telefono;
        private System.Windows.Forms.Label lbl_direccion;
        private System.Windows.Forms.Label lbl_fecha_nacimiento;
        private System.Windows.Forms.GroupBox gb_datos_cliente;
        private System.Windows.Forms.GroupBox gb_busqueda;
        private System.Windows.Forms.GroupBox gb_lista_clientes;

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
            this.txt_dni = new System.Windows.Forms.TextBox();
            this.txt_nombre = new System.Windows.Forms.TextBox();
            this.txt_apellido = new System.Windows.Forms.TextBox();
            this.txt_email = new System.Windows.Forms.TextBox();
            this.txt_telefono = new System.Windows.Forms.TextBox();
            this.txt_direccion = new System.Windows.Forms.TextBox();
            this.dtp_fecha_nacimiento = new System.Windows.Forms.DateTimePicker();
            this.txt_buscar = new System.Windows.Forms.TextBox();
            this.btn_agregar = new System.Windows.Forms.Button();
            this.btn_modificar = new System.Windows.Forms.Button();
            this.btn_eliminar = new System.Windows.Forms.Button();
            this.btn_buscar = new System.Windows.Forms.Button();
            this.btn_limpiar = new System.Windows.Forms.Button();
            this.lbl_total_registros = new System.Windows.Forms.Label();
            this.lbl_dni = new System.Windows.Forms.Label();
            this.lbl_nombre = new System.Windows.Forms.Label();
            this.lbl_apellido = new System.Windows.Forms.Label();
            this.lbl_email = new System.Windows.Forms.Label();
            this.lbl_telefono = new System.Windows.Forms.Label();
            this.lbl_direccion = new System.Windows.Forms.Label();
            this.lbl_fecha_nacimiento = new System.Windows.Forms.Label();
            this.gb_datos_cliente = new System.Windows.Forms.GroupBox();
            this.gb_busqueda = new System.Windows.Forms.GroupBox();
            this.gb_lista_clientes = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_clientes)).BeginInit();
            this.gb_datos_cliente.SuspendLayout();
            this.gb_busqueda.SuspendLayout();
            this.gb_lista_clientes.SuspendLayout();
            this.SuspendLayout();
            
            // dgv_clientes
            this.dgv_clientes.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgv_clientes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_clientes.Location = new System.Drawing.Point(6, 19);
            this.dgv_clientes.Name = "dgv_clientes";
            this.dgv_clientes.Size = new System.Drawing.Size(762, 300);
            this.dgv_clientes.TabIndex = 0;
            this.dgv_clientes.SelectionChanged += new System.EventHandler(this.dgv_clientes_SelectionChanged);
            
            // txt_dni
            this.txt_dni.BackColor = ColoresPastelGlobales.Formularios.FondoInputs;
            this.txt_dni.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_dni.Location = new System.Drawing.Point(100, 25);
            this.txt_dni.MaxLength = 9;
            this.txt_dni.Name = "txt_dni";
            this.txt_dni.Size = new System.Drawing.Size(120, 20);
            this.txt_dni.TabIndex = 1;
            
            // txt_nombre
            this.txt_nombre.BackColor = ColoresPastelGlobales.Formularios.FondoInputs;
            this.txt_nombre.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_nombre.Location = new System.Drawing.Point(100, 55);
            this.txt_nombre.MaxLength = 100;
            this.txt_nombre.Name = "txt_nombre";
            this.txt_nombre.Size = new System.Drawing.Size(200, 20);
            this.txt_nombre.TabIndex = 2;
            
            // txt_apellido
            this.txt_apellido.BackColor = ColoresPastelGlobales.Formularios.FondoInputs;
            this.txt_apellido.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_apellido.Location = new System.Drawing.Point(100, 85);
            this.txt_apellido.MaxLength = 100;
            this.txt_apellido.Name = "txt_apellido";
            this.txt_apellido.Size = new System.Drawing.Size(200, 20);
            this.txt_apellido.TabIndex = 3;
            
            // txt_email
            this.txt_email.BackColor = ColoresPastelGlobales.Formularios.FondoInputs;
            this.txt_email.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_email.Location = new System.Drawing.Point(100, 115);
            this.txt_email.MaxLength = 150;
            this.txt_email.Name = "txt_email";
            this.txt_email.Size = new System.Drawing.Size(250, 20);
            this.txt_email.TabIndex = 4;
            
            // txt_telefono
            this.txt_telefono.BackColor = ColoresPastelGlobales.Formularios.FondoInputs;
            this.txt_telefono.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_telefono.Location = new System.Drawing.Point(100, 145);
            this.txt_telefono.MaxLength = 20;
            this.txt_telefono.Name = "txt_telefono";
            this.txt_telefono.Size = new System.Drawing.Size(150, 20);
            this.txt_telefono.TabIndex = 5;
            
            // txt_direccion
            this.txt_direccion.BackColor = ColoresPastelGlobales.Formularios.FondoInputs;
            this.txt_direccion.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_direccion.Location = new System.Drawing.Point(100, 175);
            this.txt_direccion.MaxLength = 255;
            this.txt_direccion.Multiline = true;
            this.txt_direccion.Name = "txt_direccion";
            this.txt_direccion.Size = new System.Drawing.Size(300, 40);
            this.txt_direccion.TabIndex = 6;
            
            // dtp_fecha_nacimiento
            this.dtp_fecha_nacimiento.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtp_fecha_nacimiento.Location = new System.Drawing.Point(140, 225);
            this.dtp_fecha_nacimiento.Name = "dtp_fecha_nacimiento";
            this.dtp_fecha_nacimiento.ShowCheckBox = true;
            this.dtp_fecha_nacimiento.Size = new System.Drawing.Size(120, 20);
            this.dtp_fecha_nacimiento.TabIndex = 7;
            
            // txt_buscar
            this.txt_buscar.BackColor = ColoresPastelGlobales.Formularios.FondoInputs;
            this.txt_buscar.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_buscar.Location = new System.Drawing.Point(10, 25);
            this.txt_buscar.Name = "txt_buscar";
            this.txt_buscar.Size = new System.Drawing.Size(200, 20);
            this.txt_buscar.TabIndex = 8;
            
            // btn_agregar
            this.btn_agregar.BackColor = ColoresPastelGlobales.VerdeSuave;
            this.btn_agregar.ForeColor = ColoresPastelGlobales.NegroCarbon;
            this.btn_agregar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_agregar.FlatAppearance.BorderColor = ColoresPastelGlobales.NegroSuave;
            this.btn_agregar.FlatAppearance.BorderSize = 1;
            this.btn_agregar.Location = new System.Drawing.Point(420, 25);
            this.btn_agregar.Name = "btn_agregar";
            this.btn_agregar.Size = new System.Drawing.Size(75, 30);
            this.btn_agregar.TabIndex = 9;
            this.btn_agregar.Text = "Agregar";
            this.btn_agregar.UseVisualStyleBackColor = false;
            this.btn_agregar.Click += new System.EventHandler(this.btn_agregar_Click);
            
            // btn_modificar
            this.btn_modificar.BackColor = ColoresPastelGlobales.NaranjaSuave;
            this.btn_modificar.ForeColor = ColoresPastelGlobales.NegroCarbon;
            this.btn_modificar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_modificar.FlatAppearance.BorderColor = ColoresPastelGlobales.NegroSuave;
            this.btn_modificar.FlatAppearance.BorderSize = 1;
            this.btn_modificar.Location = new System.Drawing.Point(420, 65);
            this.btn_modificar.Name = "btn_modificar";
            this.btn_modificar.Size = new System.Drawing.Size(75, 30);
            this.btn_modificar.TabIndex = 10;
            this.btn_modificar.Text = "Modificar";
            this.btn_modificar.UseVisualStyleBackColor = false;
            this.btn_modificar.Click += new System.EventHandler(this.btn_modificar_Click);
            
            // btn_eliminar
            this.btn_eliminar.BackColor = ColoresPastelGlobales.RojoSuave;
            this.btn_eliminar.ForeColor = ColoresPastelGlobales.NegroCarbon;
            this.btn_eliminar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_eliminar.FlatAppearance.BorderColor = ColoresPastelGlobales.NegroSuave;
            this.btn_eliminar.FlatAppearance.BorderSize = 1;
            this.btn_eliminar.Location = new System.Drawing.Point(420, 105);
            this.btn_eliminar.Name = "btn_eliminar";
            this.btn_eliminar.Size = new System.Drawing.Size(75, 30);
            this.btn_eliminar.TabIndex = 11;
            this.btn_eliminar.Text = "Eliminar";
            this.btn_eliminar.UseVisualStyleBackColor = false;
            this.btn_eliminar.Click += new System.EventHandler(this.btn_eliminar_Click);
            
            // btn_buscar
            this.btn_buscar.BackColor = ColoresPastelGlobales.AzulSuave;
            this.btn_buscar.ForeColor = ColoresPastelGlobales.NegroCarbon;
            this.btn_buscar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_buscar.FlatAppearance.BorderColor = ColoresPastelGlobales.NegroSuave;
            this.btn_buscar.FlatAppearance.BorderSize = 1;
            this.btn_buscar.Location = new System.Drawing.Point(220, 23);
            this.btn_buscar.Name = "btn_buscar";
            this.btn_buscar.Size = new System.Drawing.Size(75, 23);
            this.btn_buscar.TabIndex = 12;
            this.btn_buscar.Text = "Buscar";
            this.btn_buscar.UseVisualStyleBackColor = false;
            this.btn_buscar.Click += new System.EventHandler(this.btn_buscar_Click);
            
            // btn_limpiar
            this.btn_limpiar.BackColor = ColoresPastelGlobales.GrisVioletaClaro;
            this.btn_limpiar.ForeColor = ColoresPastelGlobales.NegroCarbon;
            this.btn_limpiar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_limpiar.FlatAppearance.BorderColor = ColoresPastelGlobales.NegroSuave;
            this.btn_limpiar.FlatAppearance.BorderSize = 1;
            this.btn_limpiar.Location = new System.Drawing.Point(420, 145);
            this.btn_limpiar.Name = "btn_limpiar";
            this.btn_limpiar.Size = new System.Drawing.Size(75, 30);
            this.btn_limpiar.TabIndex = 13;
            this.btn_limpiar.Text = "Limpiar";
            this.btn_limpiar.UseVisualStyleBackColor = false;
            this.btn_limpiar.Click += new System.EventHandler(this.btn_limpiar_Click);
            
            // lbl_total_registros
            this.lbl_total_registros.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lbl_total_registros.AutoSize = true;
            this.lbl_total_registros.Location = new System.Drawing.Point(6, 330);
            this.lbl_total_registros.Name = "lbl_total_registros";
            this.lbl_total_registros.Size = new System.Drawing.Size(100, 13);
            this.lbl_total_registros.TabIndex = 14;
            this.lbl_total_registros.Text = "Total de registros: 0";
            
            // Labels
            this.lbl_dni.AutoSize = true;
            this.lbl_dni.Location = new System.Drawing.Point(15, 28);
            this.lbl_dni.Name = "lbl_dni";
            this.lbl_dni.Size = new System.Drawing.Size(26, 13);
            this.lbl_dni.TabIndex = 15;
            this.lbl_dni.Text = "DNI:";
            
            this.lbl_nombre.AutoSize = true;
            this.lbl_nombre.Location = new System.Drawing.Point(15, 58);
            this.lbl_nombre.Name = "lbl_nombre";
            this.lbl_nombre.Size = new System.Drawing.Size(47, 13);
            this.lbl_nombre.TabIndex = 16;
            this.lbl_nombre.Text = "Nombre:";
            
            this.lbl_apellido.AutoSize = true;
            this.lbl_apellido.Location = new System.Drawing.Point(15, 88);
            this.lbl_apellido.Name = "lbl_apellido";
            this.lbl_apellido.Size = new System.Drawing.Size(47, 13);
            this.lbl_apellido.TabIndex = 17;
            this.lbl_apellido.Text = "Apellido:";
            
            this.lbl_email.AutoSize = true;
            this.lbl_email.Location = new System.Drawing.Point(15, 118);
            this.lbl_email.Name = "lbl_email";
            this.lbl_email.Size = new System.Drawing.Size(35, 13);
            this.lbl_email.TabIndex = 18;
            this.lbl_email.Text = "Email:";
            
            this.lbl_telefono.AutoSize = true;
            this.lbl_telefono.Location = new System.Drawing.Point(15, 148);
            this.lbl_telefono.Name = "lbl_telefono";
            this.lbl_telefono.Size = new System.Drawing.Size(52, 13);
            this.lbl_telefono.TabIndex = 19;
            this.lbl_telefono.Text = "Teléfono:";
            
            this.lbl_direccion.AutoSize = true;
            this.lbl_direccion.Location = new System.Drawing.Point(15, 178);
            this.lbl_direccion.Name = "lbl_direccion";
            this.lbl_direccion.Size = new System.Drawing.Size(55, 13);
            this.lbl_direccion.TabIndex = 20;
            this.lbl_direccion.Text = "Dirección:";
            
            this.lbl_fecha_nacimiento.AutoSize = true;
            this.lbl_fecha_nacimiento.Location = new System.Drawing.Point(15, 228);
            this.lbl_fecha_nacimiento.Name = "lbl_fecha_nacimiento";
            this.lbl_fecha_nacimiento.Size = new System.Drawing.Size(111, 13);
            this.lbl_fecha_nacimiento.TabIndex = 21;
            this.lbl_fecha_nacimiento.Text = "Fecha de Nacimiento:";
            
            // GroupBoxes
            this.gb_datos_cliente.BackColor = ColoresPastelGlobales.Formularios.PanelContenido;
            this.gb_datos_cliente.ForeColor = ColoresPastelGlobales.Formularios.TextoNormal;
            this.gb_datos_cliente.Controls.Add(this.lbl_dni);
            this.gb_datos_cliente.Controls.Add(this.txt_dni);
            this.gb_datos_cliente.Controls.Add(this.lbl_nombre);
            this.gb_datos_cliente.Controls.Add(this.txt_nombre);
            this.gb_datos_cliente.Controls.Add(this.lbl_apellido);
            this.gb_datos_cliente.Controls.Add(this.txt_apellido);
            this.gb_datos_cliente.Controls.Add(this.lbl_email);
            this.gb_datos_cliente.Controls.Add(this.txt_email);
            this.gb_datos_cliente.Controls.Add(this.lbl_telefono);
            this.gb_datos_cliente.Controls.Add(this.txt_telefono);
            this.gb_datos_cliente.Controls.Add(this.lbl_direccion);
            this.gb_datos_cliente.Controls.Add(this.txt_direccion);
            this.gb_datos_cliente.Controls.Add(this.lbl_fecha_nacimiento);
            this.gb_datos_cliente.Controls.Add(this.dtp_fecha_nacimiento);
            this.gb_datos_cliente.Controls.Add(this.btn_agregar);
            this.gb_datos_cliente.Controls.Add(this.btn_modificar);
            this.gb_datos_cliente.Controls.Add(this.btn_eliminar);
            this.gb_datos_cliente.Controls.Add(this.btn_limpiar);
            this.gb_datos_cliente.Location = new System.Drawing.Point(12, 12);
            this.gb_datos_cliente.Name = "gb_datos_cliente";
            this.gb_datos_cliente.Size = new System.Drawing.Size(520, 260);
            this.gb_datos_cliente.TabIndex = 22;
            this.gb_datos_cliente.TabStop = false;
            this.gb_datos_cliente.Text = "Datos del Cliente";
            
            this.gb_busqueda.BackColor = ColoresPastelGlobales.Formularios.PanelContenido;
            this.gb_busqueda.ForeColor = ColoresPastelGlobales.Formularios.TextoNormal;
            this.gb_busqueda.Controls.Add(this.txt_buscar);
            this.gb_busqueda.Controls.Add(this.btn_buscar);
            this.gb_busqueda.Location = new System.Drawing.Point(550, 12);
            this.gb_busqueda.Name = "gb_busqueda";
            this.gb_busqueda.Size = new System.Drawing.Size(310, 60);
            this.gb_busqueda.TabIndex = 23;
            this.gb_busqueda.TabStop = false;
            this.gb_busqueda.Text = "Búsqueda";
            
            this.gb_lista_clientes.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gb_lista_clientes.BackColor = ColoresPastelGlobales.Formularios.PanelContenido;
            this.gb_lista_clientes.ForeColor = ColoresPastelGlobales.Formularios.TextoNormal;
            this.gb_lista_clientes.Controls.Add(this.dgv_clientes);
            this.gb_lista_clientes.Controls.Add(this.lbl_total_registros);
            this.gb_lista_clientes.Location = new System.Drawing.Point(12, 280);
            this.gb_lista_clientes.Name = "gb_lista_clientes";
            this.gb_lista_clientes.Size = new System.Drawing.Size(780, 350);
            this.gb_lista_clientes.TabIndex = 24;
            this.gb_lista_clientes.TabStop = false;
            this.gb_lista_clientes.Text = "Lista de Clientes";
            
            // FormClientes
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = ColoresPastelGlobales.Formularios.FondoPrincipal;
            this.ClientSize = new System.Drawing.Size(884, 641);
            this.Controls.Add(this.gb_datos_cliente);
            this.Controls.Add(this.gb_busqueda);
            this.Controls.Add(this.gb_lista_clientes);
            this.MinimumSize = new System.Drawing.Size(900, 680);
            this.Name = "FormClientes";
            this.Text = "Gestión de Clientes - Sistema Veterinario ZoofiPets";
            this.Load += new System.EventHandler(this.FormClientes_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_clientes)).EndInit();
            this.gb_datos_cliente.ResumeLayout(false);
            this.gb_datos_cliente.PerformLayout();
            this.gb_busqueda.ResumeLayout(false);
            this.gb_busqueda.PerformLayout();
            this.gb_lista_clientes.ResumeLayout(false);
            this.gb_lista_clientes.PerformLayout();
            this.ResumeLayout(false);
        }
    }
}