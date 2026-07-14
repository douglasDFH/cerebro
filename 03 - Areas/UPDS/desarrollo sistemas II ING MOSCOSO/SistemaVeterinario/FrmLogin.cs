using CapaNegocio;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace VeterinariaApp
{
    public partial class FrmLogin : Form
    {
        public bool LoginExitoso { get; private set; } = false;
        public DataRow UsuarioActual { get; private set; }

        public FrmLogin()
        {
            InitializeComponent();
            ConfigurarFormulario();
        }

        private void ConfigurarFormulario()
        {
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.ShowInTaskbar = false;
            
            // Establecer foco en el campo usuario
            txtUsuario.Focus();
            
            // Configurar enter para avanzar entre campos
            this.AcceptButton = btnLogin;
            this.CancelButton = btnCancelar;

            // Tooltips
            ToolTip tt = new ToolTip();
            tt.SetToolTip(txtUsuario, "Ingrese su nombre de usuario o email");
            tt.SetToolTip(txtClave, "Ingrese su contraseña");
            tt.SetToolTip(btnLogin, "Iniciar sesión");
            tt.SetToolTip(btnCancelar, "Cancelar y cerrar aplicación");
        }

        private void FrmLogin_Load(object sender, EventArgs e)
        {
            lblError.Visible = false;
            txtUsuario.Focus();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                lblError.Visible = false;

                // Validar campos
                if (string.IsNullOrWhiteSpace(txtUsuario.Text))
                {
                    MostrarError("Debe ingresar un usuario");
                    txtUsuario.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtClave.Text))
                {
                    MostrarError("Debe ingresar una contraseña");
                    txtClave.Focus();
                    return;
                }

                // Deshabilitar botón mientras procesa
                btnLogin.Enabled = false;
                btnLogin.Text = "Verificando...";
                this.Cursor = Cursors.WaitCursor;

                // Intentar login
                DataTable resultado = NUsuario.Login(txtUsuario.Text.Trim(), txtClave.Text);

                if (resultado != null && resultado.Rows.Count > 0)
                {
                    DataRow row = resultado.Rows[0];
                    int resultadoLogin = Convert.ToInt32(row["Resultado"]);

                    if (resultadoLogin == 1)
                    {
                        // Login exitoso
                        UsuarioActual = row;
                        LoginExitoso = true;
                        
                        MostrarMensaje($"Bienvenido, {row["NombreCompleto"]}", false);
                        
                        // Pequeña pausa para mostrar mensaje
                        System.Threading.Thread.Sleep(1000);
                        
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                    else
                    {
                        // Login fallido
                        string mensaje = row["Mensaje"].ToString();
                        MostrarError(mensaje);
                        LimpiarCampos();
                    }
                }
                else
                {
                    MostrarError("Error en el servidor. Intente nuevamente.");
                }
            }
            catch (Exception ex)
            {
                MostrarError("Error al conectar: " + ex.Message);
            }
            finally
            {
                btnLogin.Enabled = true;
                btnLogin.Text = "Iniciar Sesión";
                this.Cursor = Cursors.Default;
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            Application.Exit();
        }

        private void MostrarError(string mensaje)
        {
            lblError.Text = mensaje;
            lblError.ForeColor = Color.Red;
            lblError.Visible = true;
        }

        private void MostrarMensaje(string mensaje, bool esError)
        {
            lblError.Text = mensaje;
            lblError.ForeColor = esError ? Color.Red : Color.Green;
            lblError.Visible = true;
        }

        private void LimpiarCampos()
        {
            txtClave.Clear();
            txtUsuario.Focus();
        }

        private void txtUsuario_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                txtClave.Focus();
            }
        }

        private void txtClave_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                btnLogin_Click(sender, e);
            }
        }

        private void btnMostrarClave_Click(object sender, EventArgs e)
        {
            if (txtClave.UseSystemPasswordChar)
            {
                txtClave.UseSystemPasswordChar = false;
                btnMostrarClave.Text = "👁️‍🗨️"; // Ocultar
            }
            else
            {
                txtClave.UseSystemPasswordChar = true;
                btnMostrarClave.Text = "👁️"; // Mostrar
            }
        }

        private void linkOlvideClave_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show("Para recuperar su contraseña, contacte al administrador del sistema.", 
                "Sistema Veterinario", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // Permitir arrastrar el formulario
        private bool dragging = false;
        private Point dragCursorPoint;
        private Point dragFormPoint;

        private void FrmLogin_MouseDown(object sender, MouseEventArgs e)
        {
            dragging = true;
            dragCursorPoint = Cursor.Position;
            dragFormPoint = this.Location;
        }

        private void FrmLogin_MouseMove(object sender, MouseEventArgs e)
        {
            if (dragging)
            {
                Point dif = Point.Subtract(Cursor.Position, new Size(dragCursorPoint));
                this.Location = Point.Add(dragFormPoint, new Size(dif));
            }
        }

        private void FrmLogin_MouseUp(object sender, MouseEventArgs e)
        {
            dragging = false;
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnMinimizar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            // Agregar borde al formulario
            ControlPaint.DrawBorder(e.Graphics, this.ClientRectangle, Color.LightGray, ButtonBorderStyle.Solid);
        }
    }
}