namespace VeterinariaApp
{
    partial class FrmCambiarClave
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
            this.lblUsuario = new System.Windows.Forms.Label();
            this.lblClaveAnterior = new System.Windows.Forms.Label();
            this.txtClaveAnterior = new System.Windows.Forms.TextBox();
            this.lblClaveNueva = new System.Windows.Forms.Label();
            this.txtClaveNueva = new System.Windows.Forms.TextBox();
            this.lblConfirmarClave = new System.Windows.Forms.Label();
            this.txtConfirmarClave = new System.Windows.Forms.TextBox();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.lblError = new System.Windows.Forms.Label();
            this.btnMostrarClave = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblUsuario
            // 
            this.lblUsuario.AutoSize = true;
            this.lblUsuario.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblUsuario.Location = new System.Drawing.Point(20, 20);
            this.lblUsuario.Name = "lblUsuario";
            this.lblUsuario.Size = new System.Drawing.Size(60, 19);
            this.lblUsuario.TabIndex = 0;
            this.lblUsuario.Text = "Usuario:";
            // 
            // lblClaveAnterior
            // 
            this.lblClaveAnterior.AutoSize = true;
            this.lblClaveAnterior.Location = new System.Drawing.Point(20, 30);
            this.lblClaveAnterior.Name = "lblClaveAnterior";
            this.lblClaveAnterior.Size = new System.Drawing.Size(101, 15);
            this.lblClaveAnterior.TabIndex = 0;
            this.lblClaveAnterior.Text = "Contraseña actual:";
            // 
            // txtClaveAnterior
            // 
            this.txtClaveAnterior.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtClaveAnterior.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtClaveAnterior.Location = new System.Drawing.Point(20, 50);
            this.txtClaveAnterior.Name = "txtClaveAnterior";
            this.txtClaveAnterior.Size = new System.Drawing.Size(250, 25);
            this.txtClaveAnterior.TabIndex = 1;
            this.txtClaveAnterior.UseSystemPasswordChar = true;
            this.txtClaveAnterior.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtClaveAnterior_KeyPress);
            // 
            // lblClaveNueva
            // 
            this.lblClaveNueva.AutoSize = true;
            this.lblClaveNueva.Location = new System.Drawing.Point(20, 90);
            this.lblClaveNueva.Name = "lblClaveNueva";
            this.lblClaveNueva.Size = new System.Drawing.Size(97, 15);
            this.lblClaveNueva.TabIndex = 0;
            this.lblClaveNueva.Text = "Contraseña nueva:";
            // 
            // txtClaveNueva
            // 
            this.txtClaveNueva.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtClaveNueva.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtClaveNueva.Location = new System.Drawing.Point(20, 110);
            this.txtClaveNueva.Name = "txtClaveNueva";
            this.txtClaveNueva.Size = new System.Drawing.Size(250, 25);
            this.txtClaveNueva.TabIndex = 2;
            this.txtClaveNueva.UseSystemPasswordChar = true;
            this.txtClaveNueva.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtClaveNueva_KeyPress);
            // 
            // lblConfirmarClave
            // 
            this.lblConfirmarClave.AutoSize = true;
            this.lblConfirmarClave.Location = new System.Drawing.Point(20, 150);
            this.lblConfirmarClave.Name = "lblConfirmarClave";
            this.lblConfirmarClave.Size = new System.Drawing.Size(112, 15);
            this.lblConfirmarClave.TabIndex = 0;
            this.lblConfirmarClave.Text = "Confirmar contraseña:";
            // 
            // txtConfirmarClave
            // 
            this.txtConfirmarClave.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtConfirmarClave.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtConfirmarClave.Location = new System.Drawing.Point(20, 170);
            this.txtConfirmarClave.Name = "txtConfirmarClave";
            this.txtConfirmarClave.Size = new System.Drawing.Size(250, 25);
            this.txtConfirmarClave.TabIndex = 3;
            this.txtConfirmarClave.UseSystemPasswordChar = true;
            this.txtConfirmarClave.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtConfirmarClave_KeyPress);
            // 
            // btnGuardar
            // 
            this.btnGuardar.BackColor = System.Drawing.Color.SteelBlue;
            this.btnGuardar.FlatAppearance.BorderSize = 0;
            this.btnGuardar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGuardar.ForeColor = System.Drawing.Color.White;
            this.btnGuardar.Location = new System.Drawing.Point(60, 300);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(100, 35);
            this.btnGuardar.TabIndex = 4;
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.UseVisualStyleBackColor = false;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.BackColor = System.Drawing.Color.Gray;
            this.btnCancelar.FlatAppearance.BorderSize = 0;
            this.btnCancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelar.ForeColor = System.Drawing.Color.White;
            this.btnCancelar.Location = new System.Drawing.Point(180, 300);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(100, 35);
            this.btnCancelar.TabIndex = 5;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = false;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // lblError
            // 
            this.lblError.AutoSize = true;
            this.lblError.ForeColor = System.Drawing.Color.Red;
            this.lblError.Location = new System.Drawing.Point(20, 220);
            this.lblError.Name = "lblError";
            this.lblError.Size = new System.Drawing.Size(0, 15);
            this.lblError.TabIndex = 6;
            this.lblError.Visible = false;
            // 
            // btnMostrarClave
            // 
            this.btnMostrarClave.BackColor = System.Drawing.Color.Transparent;
            this.btnMostrarClave.FlatAppearance.BorderSize = 0;
            this.btnMostrarClave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMostrarClave.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.btnMostrarClave.Location = new System.Drawing.Point(280, 110);
            this.btnMostrarClave.Name = "btnMostrarClave";
            this.btnMostrarClave.Size = new System.Drawing.Size(30, 25);
            this.btnMostrarClave.TabIndex = 7;
            this.btnMostrarClave.Text = "👁️";
            this.btnMostrarClave.UseVisualStyleBackColor = false;
            this.btnMostrarClave.Click += new System.EventHandler(this.btnMostrarClave_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblClaveAnterior);
            this.groupBox1.Controls.Add(this.txtClaveAnterior);
            this.groupBox1.Controls.Add(this.lblClaveNueva);
            this.groupBox1.Controls.Add(this.txtClaveNueva);
            this.groupBox1.Controls.Add(this.lblConfirmarClave);
            this.groupBox1.Controls.Add(this.txtConfirmarClave);
            this.groupBox1.Controls.Add(this.btnMostrarClave);
            this.groupBox1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.groupBox1.Location = new System.Drawing.Point(20, 50);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(320, 220);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Cambio de Contraseña";
            // 
            // FrmCambiarClave
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(360, 360);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.lblError);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnGuardar);
            this.Controls.Add(this.lblUsuario);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmCambiarClave";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Cambiar Contraseña";
            this.Load += new System.EventHandler(this.FrmCambiarClave_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblUsuario;
        private System.Windows.Forms.Label lblClaveAnterior;
        private System.Windows.Forms.TextBox txtClaveAnterior;
        private System.Windows.Forms.Label lblClaveNueva;
        private System.Windows.Forms.TextBox txtClaveNueva;
        private System.Windows.Forms.Label lblConfirmarClave;
        private System.Windows.Forms.TextBox txtConfirmarClave;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Label lblError;
        private System.Windows.Forms.Button btnMostrarClave;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}