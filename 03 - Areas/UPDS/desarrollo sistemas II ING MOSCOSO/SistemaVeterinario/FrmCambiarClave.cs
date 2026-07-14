using CapaNegocio;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace VeterinariaApp
{
    public partial class FrmCambiarClave : Form
    {
        public int IdUsuario { get; set; }
        public string NombreUsuario { get; set; }

        public FrmCambiarClave()
        {
            InitializeComponent();
            ConfigurarFormulario();
        }

        private void ConfigurarFormulario()
        {
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.ShowInTaskbar = false;
            
            this.AcceptButton = btnGuardar;
            this.CancelButton = btnCancelar;
        }

        private void FrmCambiarClave_Load(object sender, EventArgs e)
        {
            lblUsuario.Text = $"Usuario: {NombreUsuario}";
            txtClaveAnterior.Focus();
            lblError.Visible = false;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                lblError.Visible = false;

                // Validar campos
                if (string.IsNullOrWhiteSpace(txtClaveAnterior.Text))
                {
                    MostrarError("Debe ingresar la contraseña anterior");
                    txtClaveAnterior.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtClaveNueva.Text))
                {
                    MostrarError("Debe ingresar la nueva contraseña");
                    txtClaveNueva.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtConfirmarClave.Text))
                {
                    MostrarError("Debe confirmar la nueva contraseña");
                    txtConfirmarClave.Focus();
                    return;
                }

                if (txtClaveNueva.Text.Length < 6)
                {
                    MostrarError("La nueva contraseña debe tener al menos 6 caracteres");
                    txtClaveNueva.Focus();
                    return;
                }

                if (txtClaveNueva.Text != txtConfirmarClave.Text)
                {
                    MostrarError("Las contraseñas nuevas no coinciden");
                    txtConfirmarClave.Focus();
                    return;
                }

                if (txtClaveAnterior.Text == txtClaveNueva.Text)
                {
                    MostrarError("La nueva contraseña debe ser diferente a la anterior");
                    txtClaveNueva.Focus();
                    return;
                }

                // Intentar cambiar contraseña
                string resultado = NUsuario.CambiarClave(IdUsuario, txtClaveAnterior.Text, txtClaveNueva.Text);

                if (resultado.Equals("OK"))
                {
                    MessageBox.Show("Contraseña cambiada exitosamente", "Sistema Veterinario", 
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MostrarError(resultado);
                }
            }
            catch (Exception ex)
            {
                MostrarError("Error al cambiar contraseña: " + ex.Message);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void MostrarError(string mensaje)
        {
            lblError.Text = mensaje;
            lblError.ForeColor = Color.Red;
            lblError.Visible = true;
        }

        private void txtClaveAnterior_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                txtClaveNueva.Focus();
            }
        }

        private void txtClaveNueva_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                txtConfirmarClave.Focus();
            }
        }

        private void txtConfirmarClave_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                btnGuardar_Click(sender, e);
            }
        }

        private void btnMostrarClave_Click(object sender, EventArgs e)
        {
            bool mostrar = !txtClaveAnterior.UseSystemPasswordChar;
            txtClaveAnterior.UseSystemPasswordChar = mostrar;
            txtClaveNueva.UseSystemPasswordChar = mostrar;
            txtConfirmarClave.UseSystemPasswordChar = mostrar;
            btnMostrarClave.Text = mostrar ? "👁️" : "👁️‍🗨️";
        }
    }
}