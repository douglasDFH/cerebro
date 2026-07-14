namespace VeterinariaApp
{
    partial class FormPersona
    {
        // ... (código existente se mantiene igual hasta la declaración de controles)

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.DataGridView dataListado;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Eliminar;
        private System.Windows.Forms.CheckBox chkEliminar;
        private FontAwesome.Sharp.IconButton btnEliminar;
        private FontAwesome.Sharp.IconButton btnBuscar;
        private System.Windows.Forms.TextBox txtBuscar;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TextBox txtTelefono;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtDireccion;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtIdCliente;
        private System.Windows.Forms.Label label2;
        private FontAwesome.Sharp.IconButton btnCancelar;
        private FontAwesome.Sharp.IconButton btnEditar;
        private FontAwesome.Sharp.IconButton btnGuardar;
        private FontAwesome.Sharp.IconButton btnNuevo;
        private System.Windows.Forms.ToolTip ttMensaje;

        // Nuevos controles agregados
        private System.Windows.Forms.TextBox txtNombre;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtApellidos;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.RadioButton rbFisica;
        private System.Windows.Forms.RadioButton rbJuridica;
        private System.Windows.Forms.GroupBox gbFisica;
        private System.Windows.Forms.TextBox txtDNI;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.DateTimePicker dtFechaNacimiento;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox cbGenero;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.GroupBox gbJuridica;
        private System.Windows.Forms.TextBox txtCIF;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtRazonSocial;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.DateTimePicker dtFechaConstitucion;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txtActividadPrincipal;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.PictureBox pbFoto;
        private System.Windows.Forms.Button btnCargarFoto;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // FormPersona
            // 
            this.ClientSize = new System.Drawing.Size(988, 413);
            this.Name = "FormPersona";
            this.ResumeLayout(false);

        }

#endregion

        // ... (declaraciones existentes se mantienen)

        // Nuevas declaraciones
        private System.Windows.Forms.TextBox txtNombre;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtApellidos;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.RadioButton rbFisica;
        private System.Windows.Forms.RadioButton rbJuridica;
        private System.Windows.Forms.GroupBox gbFisica;
        private System.Windows.Forms.TextBox txtDNI;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.DateTimePicker dtFechaNacimiento;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox cbGenero;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.GroupBox gbJuridica;
        private System.Windows.Forms.TextBox txtCIF;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtRazonSocial;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.DateTimePicker dtFechaConstitucion;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txtActividadPrincipal;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.PictureBox pbFoto;
        private System.Windows.Forms.Button btnCargarFoto;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
    }
}