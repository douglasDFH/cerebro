namespace SistemaZoofiPets
{
    partial class FormUsers
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.txt_nombre = new System.Windows.Forms.TextBox();
            this.txt_apellido = new System.Windows.Forms.TextBox();
            this.txt_email = new System.Windows.Forms.TextBox();
            this.txt_telefono = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txt_contrasena = new System.Windows.Forms.TextBox();
            this.txt_usuario = new System.Windows.Forms.TextBox();
            this.Btn_nuevousuario = new System.Windows.Forms.Button();
            this.Btn_modusuario = new System.Windows.Forms.Button();
            this.Btn_eliminarusuario = new System.Windows.Forms.Button();
            this.btn_cerrar = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txt_direccion = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(10, 10);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(872, 355);
            this.dataGridView1.TabIndex = 0;
            // 
            // txt_nombre
            // 
            this.txt_nombre.BackColor = ColoresPastelGlobales.Formularios.FondoInputs;
            this.txt_nombre.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_nombre.Location = new System.Drawing.Point(20, 441);
            this.txt_nombre.Name = "txt_nombre";
            this.txt_nombre.Size = new System.Drawing.Size(135, 22);
            this.txt_nombre.TabIndex = 1;
            // 
            // txt_apellido
            // 
            this.txt_apellido.BackColor = ColoresPastelGlobales.Formularios.FondoInputs;
            this.txt_apellido.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_apellido.Location = new System.Drawing.Point(183, 441);
            this.txt_apellido.Name = "txt_apellido";
            this.txt_apellido.Size = new System.Drawing.Size(135, 22);
            this.txt_apellido.TabIndex = 2;
            // 
            // txt_email
            // 
            this.txt_email.BackColor = ColoresPastelGlobales.Formularios.FondoInputs;
            this.txt_email.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_email.Location = new System.Drawing.Point(348, 441);
            this.txt_email.Name = "txt_email";
            this.txt_email.Size = new System.Drawing.Size(137, 22);
            this.txt_email.TabIndex = 3;
            // 
            // txt_telefono
            // 
            this.txt_telefono.BackColor = ColoresPastelGlobales.Formularios.FondoInputs;
            this.txt_telefono.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_telefono.Location = new System.Drawing.Point(505, 441);
            this.txt_telefono.Name = "txt_telefono";
            this.txt_telefono.Size = new System.Drawing.Size(137, 22);
            this.txt_telefono.TabIndex = 4;
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = ColoresPastelGlobales.Formularios.PanelContenido;
            this.groupBox1.ForeColor = ColoresPastelGlobales.Formularios.TextoNormal;
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.txt_contrasena);
            this.groupBox1.Controls.Add(this.txt_usuario);
            this.groupBox1.Location = new System.Drawing.Point(663, 397);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(321, 78);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Información de Acceso";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(175, 25);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(76, 16);
            this.label6.TabIndex = 15;
            this.label6.Text = "Contraseña";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 25);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(54, 16);
            this.label5.TabIndex = 14;
            this.label5.Text = "Usuario";
            // 
            // txt_contrasena
            // 
            this.txt_contrasena.BackColor = ColoresPastelGlobales.Formularios.FondoInputs;
            this.txt_contrasena.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_contrasena.Location = new System.Drawing.Point(176, 44);
            this.txt_contrasena.Name = "txt_contrasena";
            this.txt_contrasena.PasswordChar = '*';
            this.txt_contrasena.Size = new System.Drawing.Size(139, 22);
            this.txt_contrasena.TabIndex = 7;
            // 
            // txt_usuario
            // 
            this.txt_usuario.BackColor = ColoresPastelGlobales.Formularios.FondoInputs;
            this.txt_usuario.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_usuario.Location = new System.Drawing.Point(6, 44);
            this.txt_usuario.Name = "txt_usuario";
            this.txt_usuario.Size = new System.Drawing.Size(139, 22);
            this.txt_usuario.TabIndex = 6;
            // 
            // Btn_nuevousuario
            // 
            this.Btn_nuevousuario.BackColor = ColoresPastelGlobales.VerdeSuave;
            this.Btn_nuevousuario.ForeColor = ColoresPastelGlobales.NegroCarbon;
            this.Btn_nuevousuario.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Btn_nuevousuario.FlatAppearance.BorderColor = ColoresPastelGlobales.NegroSuave;
            this.Btn_nuevousuario.FlatAppearance.BorderSize = 1;
            this.Btn_nuevousuario.Location = new System.Drawing.Point(18, 525);
            this.Btn_nuevousuario.Name = "Btn_nuevousuario";
            this.Btn_nuevousuario.Size = new System.Drawing.Size(137, 48);
            this.Btn_nuevousuario.TabIndex = 6;
            this.Btn_nuevousuario.Text = "Nuevo Usuario";
            this.Btn_nuevousuario.UseVisualStyleBackColor = false;
            this.Btn_nuevousuario.Click += new System.EventHandler(this.Btn_nuevousuario_Click);
            // 
            // Btn_modusuario
            // 
            this.Btn_modusuario.BackColor = ColoresPastelGlobales.NaranjaSuave;
            this.Btn_modusuario.ForeColor = ColoresPastelGlobales.NegroCarbon;
            this.Btn_modusuario.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Btn_modusuario.FlatAppearance.BorderColor = ColoresPastelGlobales.NegroSuave;
            this.Btn_modusuario.FlatAppearance.BorderSize = 1;
            this.Btn_modusuario.Location = new System.Drawing.Point(181, 525);
            this.Btn_modusuario.Name = "Btn_modusuario";
            this.Btn_modusuario.Size = new System.Drawing.Size(137, 48);
            this.Btn_modusuario.TabIndex = 7;
            this.Btn_modusuario.Text = "Modificar Usuario";
            this.Btn_modusuario.UseVisualStyleBackColor = false;
            this.Btn_modusuario.Click += new System.EventHandler(this.Btn_modusuario_Click);
            // 
            // Btn_eliminarusuario
            // 
            this.Btn_eliminarusuario.BackColor = ColoresPastelGlobales.RojoSuave;
            this.Btn_eliminarusuario.ForeColor = ColoresPastelGlobales.NegroCarbon;
            this.Btn_eliminarusuario.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Btn_eliminarusuario.FlatAppearance.BorderColor = ColoresPastelGlobales.NegroSuave;
            this.Btn_eliminarusuario.FlatAppearance.BorderSize = 1;
            this.Btn_eliminarusuario.Location = new System.Drawing.Point(348, 525);
            this.Btn_eliminarusuario.Name = "Btn_eliminarusuario";
            this.Btn_eliminarusuario.Size = new System.Drawing.Size(137, 48);
            this.Btn_eliminarusuario.TabIndex = 8;
            this.Btn_eliminarusuario.Text = "Eliminar Usuario";
            this.Btn_eliminarusuario.UseVisualStyleBackColor = false;
            this.Btn_eliminarusuario.Click += new System.EventHandler(this.Btn_eliminarusuario_Click);
            // 
            // btn_cerrar
            // 
            this.btn_cerrar.BackColor = ColoresPastelGlobales.GrisVioletaClaro;
            this.btn_cerrar.ForeColor = ColoresPastelGlobales.NegroCarbon;
            this.btn_cerrar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_cerrar.FlatAppearance.BorderColor = ColoresPastelGlobales.NegroSuave;
            this.btn_cerrar.FlatAppearance.BorderSize = 1;
            this.btn_cerrar.Location = new System.Drawing.Point(841, 503);
            this.btn_cerrar.Name = "btn_cerrar";
            this.btn_cerrar.Size = new System.Drawing.Size(137, 48);
            this.btn_cerrar.TabIndex = 9;
            this.btn_cerrar.Text = "Cerrar";
            this.btn_cerrar.UseVisualStyleBackColor = false;
            this.btn_cerrar.Click += new System.EventHandler(this.btn_cerrar_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 422);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 16);
            this.label1.TabIndex = 10;
            this.label1.Text = "Nombre";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(180, 422);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 16);
            this.label2.TabIndex = 11;
            this.label2.Text = "Apellido";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(345, 422);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(42, 16);
            this.label3.TabIndex = 12;
            this.label3.Text = "Gmail";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(502, 422);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(61, 16);
            this.label4.TabIndex = 13;
            this.label4.Text = "Telefono";
            // 
            // txt_direccion
            // 
            this.txt_direccion.BackColor = ColoresPastelGlobales.Formularios.FondoInputs;
            this.txt_direccion.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_direccion.Location = new System.Drawing.Point(208, 483);
            this.txt_direccion.Name = "txt_direccion";
            this.txt_direccion.Size = new System.Drawing.Size(391, 22);
            this.txt_direccion.TabIndex = 14;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(128, 486);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(64, 16);
            this.label7.TabIndex = 15;
            this.label7.Text = "Direccion";
            // 
            // pictureBox1
            // 
            // this.pictureBox1.Image = global::SistemaZoofiPets.Properties.Resources.Usuarios;
            this.pictureBox1.Location = new System.Drawing.Point(888, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(105, 163);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 16;
            this.pictureBox1.TabStop = false;
            // 
            // FormUsers
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = ColoresPastelGlobales.Formularios.FondoPrincipal;
            this.ClientSize = new System.Drawing.Size(996, 584);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txt_direccion);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btn_cerrar);
            this.Controls.Add(this.Btn_eliminarusuario);
            this.Controls.Add(this.Btn_modusuario);
            this.Controls.Add(this.Btn_nuevousuario);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.txt_telefono);
            this.Controls.Add(this.txt_email);
            this.Controls.Add(this.txt_apellido);
            this.Controls.Add(this.txt_nombre);
            this.Controls.Add(this.dataGridView1);
            this.Name = "FormUsers";
            this.Text = "Gestión de Usuarios - Sistema Veterinario ZoofiPets";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.TextBox txt_nombre;
        private System.Windows.Forms.TextBox txt_apellido;
        private System.Windows.Forms.TextBox txt_email;
        private System.Windows.Forms.TextBox txt_telefono;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txt_contrasena;
        private System.Windows.Forms.TextBox txt_usuario;
        private System.Windows.Forms.Button Btn_nuevousuario;
        private System.Windows.Forms.Button Btn_modusuario;
        private System.Windows.Forms.Button Btn_eliminarusuario;
        private System.Windows.Forms.Button btn_cerrar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txt_direccion;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}