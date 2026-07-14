using System.Drawing;
using System.Windows.Forms;
using CapaNegocio;
using CapaDatos;

namespace SistemVeterinario
{
    /// <summary>
    /// Formulario de Login - Autenticación de usuarios
    /// </summary>
    public partial class Login : Form
    {
        private Dashboard? _dashboardInstance;

        public Login()
        {
            InitializeComponent();
            ConfigurarLogin();
        }

        private void ConfigurarLogin()
        {
            // Configurar eventos
            btnLogin.Click += BtnLogin_Click;
            btnCancelar.Click += BtnCancelar_Click;

            // Configurar tecla Enter para login
            this.KeyPreview = true;
            this.KeyDown += Login_KeyDown;

            // Configurar focus inicial
            txtUsuario.Focus();

            // Configurar placeholder text (opcional)
            if (string.IsNullOrEmpty(txtUsuario.Text))
                txtUsuario.PlaceholderText = "Usuario o Email";

            txtUsuario.Enter += TxtUsuario_Enter;
            txtUsuario.Leave += TxtUsuario_Leave;
        }

        private void Login_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                BtnLogin_Click(sender, e);
            }
            else if (e.KeyCode == Keys.Escape)
            {
                BtnCancelar_Click(sender, e);
            }
        }

        private void TxtUsuario_Enter(object? sender, EventArgs e)
        {
            if (txtUsuario.PlaceholderText == "Usuario o Email")
            {
                txtUsuario.PlaceholderText = "";
                txtUsuario.ForeColor = Color.Black;
            }
        }

        private void TxtUsuario_Leave(object? sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUsuario.Text))
            {
                txtUsuario.PlaceholderText = "Usuario o Email";
                txtUsuario.ForeColor = Color.Gray;
            }
        }

        private async void BtnLogin_Click(object? sender, EventArgs e)
        {
            await RealizarLogin();
        }

        private async Task RealizarLogin()
        {
            try
            {
                // Deshabilitar controles durante el login
                btnLogin.Enabled = false;
                btnLogin.Text = "Ingresando...";

                // Obtener credenciales
                var usuario = txtUsuario.Text == "Usuario o Email" ? "" : txtUsuario.Text.Trim();
                var contrasena = txtContrasena.Text;

                // Validar campos
                if (string.IsNullOrWhiteSpace(usuario))
                {
                    MessageBox.Show("Por favor ingrese su usuario o email.", "Campo Requerido",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtUsuario.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(contrasena))
                {
                    MessageBox.Show("Por favor ingrese su contraseña.", "Campo Requerido",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtContrasena.Focus();
                    return;
                }

                
    // Realizar autenticación
                bool loginExitoso = NUsuario.ValidarLogin(usuario, contrasena);

                if (loginExitoso)
                {
                    // Login exitoso - abrir dashboard de forma modal
                    AbrirDashboard();
                }
                else
                {
                    // Mostrar error
                    MessageBox.Show("Usuario o contraseña incorrectos", "Error de Autenticación",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);

                    // Limpiar contraseña y enfocar usuario
                    txtContrasena.Text = "";
                    txtUsuario.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error inesperado: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                // Rehabilitar controles
                btnLogin.Enabled = true;
                btnLogin.Text = "Ingresar";
            }
        }

        /// <summary>
        /// Abre el Dashboard y maneja su ciclo de vida correctamente
        /// </summary>
        private void AbrirDashboard()
        {
            try
            {
                // Ocultar el formulario de login temporalmente
                this.Hide();

                // Crear nueva instancia del dashboard
                _dashboardInstance = new Dashboard();

                // Configurar eventos del dashboard para manejar su cierre
                _dashboardInstance.FormClosed += Dashboard_FormClosed;

                // Mostrar dashboard como formulario modal
                _dashboardInstance.ShowDialog(this);

                // Cuando el dashboard se cierra, se ejecutará Dashboard_FormClosed
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al abrir dashboard: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Show(); // Mostrar login nuevamente en caso de error
            }
        }

        /// <summary>
        /// Se ejecuta cuando el Dashboard se cierra
        /// </summary>
        private void Dashboard_FormClosed(object? sender, FormClosedEventArgs e)
        {
            try
            {
                // Limpiar la referencia del dashboard
                if (_dashboardInstance != null)
                {
                    _dashboardInstance.FormClosed -= Dashboard_FormClosed;
                    _dashboardInstance = null;
                }


                // Limpiar campos de login para nueva sesión
                txtContrasena.Text = "";
                if (txtUsuario.Text != "Usuario o Email")
                {
                    txtUsuario.Focus();
                }
                else
                {
                    txtUsuario.PlaceholderText = "Usuario o Email";
                    txtUsuario.ForeColor = Color.Gray;
                }

                // Mostrar el formulario de login nuevamente
                this.Show();
                this.WindowState = FormWindowState.Normal;
                this.BringToFront();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al procesar cierre del dashboard: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);

                // En caso de error, mostrar el login de todos modos
                this.Show();
            }
        }

        private void BtnCancelar_Click(object? sender, EventArgs e)
        {
            var result = MessageBox.Show("¿Está seguro que desea salir de la aplicación?",
                "Confirmar Salida", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void LblCrearCuenta_Click(object? sender, EventArgs e)
        {
            // TODO: Implementar formulario de registro
            MessageBox.Show("Funcionalidad de registro - Próximamente", "Información",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void LblOlvideContrasena_Click(object? sender, EventArgs e)
        {
            // TODO: Implementar recuperación de contraseña
            MessageBox.Show("Recuperación de contraseña - Próximamente", "Información",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            // Si hay una instancia del dashboard activa, cerrarla primero
            if (_dashboardInstance != null && !_dashboardInstance.IsDisposed)
            {
                _dashboardInstance.FormClosed -= Dashboard_FormClosed;
                _dashboardInstance.Close();
                _dashboardInstance = null;
            }

            if (e.CloseReason == CloseReason.UserClosing)
            {
                var result = MessageBox.Show("¿Está seguro que desea salir de la aplicación?",
                    "Confirmar Salida", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.No)
                {
                    e.Cancel = true;
                    return;
                }
            }

            // Asegurar logout antes de cerrar
            base.OnFormClosing(e);
        }
    }
}
