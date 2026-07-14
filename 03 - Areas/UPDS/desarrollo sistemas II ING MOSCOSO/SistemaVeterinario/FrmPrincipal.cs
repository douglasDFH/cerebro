using FontAwesome.Sharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VeterinariaApp
{
    public partial class FrmPrincipal : Form
    {
        //Declaramos campos
        private IconButton currentBtn;
        private Panel leftBorderBtn;
        private Form currentChildForm;

        // Información del usuario actual
        public int IdUsuario;
        public string NombreUsuario;
        public string NombreCompleto;
        public string Email;
        public string Rol;
        public System.Data.DataRow UsuarioActual;
        //Constructor
        public FrmPrincipal()
        {
            InitializeComponent();
            leftBorderBtn = new Panel();
            leftBorderBtn.Size = new Size(7, 60);
            panelMenu.Controls.Add(leftBorderBtn);
            //Codigo para quitar la barra de titulo del formulario
            this.Text = string.Empty;
            this.ControlBox = false;
            //Para reducir el parpadeo
            this.DoubleBuffered = true;
            //Para evitar perder la barra de windows al maximizar
            //codificamos hasta el area de trabajo
            this.MaximizedBounds = Screen.FromHandle(this.Handle).WorkingArea;
        }

        // Constructor que recibe información del usuario
        public FrmPrincipal(System.Data.DataRow usuarioActual) : this()
        {
            CargarInformacionUsuario(usuarioActual);
        }

        private void CargarInformacionUsuario(System.Data.DataRow usuarioData)
        {
            try
            {
                UsuarioActual = usuarioData;
                IdUsuario = Convert.ToInt32(usuarioData["IdUsuario"]);
                NombreUsuario = usuarioData["NombreUsuario"].ToString();
                NombreCompleto = usuarioData["NombreCompleto"].ToString();
                Email = usuarioData["Email"].ToString();
                Rol = usuarioData["Rol"].ToString();

                // Actualizar interfaz con información del usuario
                ActualizarInterfazUsuario();
                ConfigurarAccesoSegunRol();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar información del usuario: " + ex.Message, 
                    "Sistema Veterinario", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ActualizarInterfazUsuario()
        {
            // Actualizar etiquetas en la interfaz
            if (lblUsuarioActual != null)
                lblUsuarioActual.Text = $"Usuario: {NombreCompleto}";
            
            if (lblRolActual != null)
                lblRolActual.Text = $"Rol: {Rol}";

            // Actualizar título de la ventana
            this.Text = $"Sistema Veterinario - {NombreCompleto} ({Rol})";
        }

        private void ConfigurarAccesoSegunRol()
        {
            try
            {
                // Configurar visibilidad de botones según el rol
                switch (Rol.ToUpper())
                {
                    case "ADMIN":
                        // Administrador: acceso total
                        btnDashboard.Visible = true;
                        btnCategorias.Visible = true;
                        btnClientes.Visible = true;
                        btnAnimales.Visible = true;
                        btnPedidos.Visible = true; // Citas
                        btnProductos.Visible = true;
                        btnUsuarios.Visible = true;
                        break;

                    case "VETERINARIO":
                        // Veterinario: acceso a funciones médicas
                        btnDashboard.Visible = true;
                        btnCategorias.Visible = false;
                        btnClientes.Visible = true;
                        btnAnimales.Visible = true;
                        btnPedidos.Visible = true; // Citas médicas
                        btnProductos.Visible = false;
                        btnUsuarios.Visible = false;
                        break;

                    case "AUXILIAR":
                        // Auxiliar: acceso limitado
                        btnDashboard.Visible = true;
                        btnCategorias.Visible = false;
                        btnClientes.Visible = true;
                        btnAnimales.Visible = true;
                        btnPedidos.Visible = false;
                        btnProductos.Visible = false;
                        btnUsuarios.Visible = false;
                        break;

                    case "RECEPCIONISTA":
                        // Recepcionista: gestión de clientes y citas
                        btnDashboard.Visible = true;
                        btnCategorias.Visible = false;
                        btnClientes.Visible = true;
                        btnAnimales.Visible = true;
                        btnPedidos.Visible = true; // Gestión de citas
                        btnProductos.Visible = false;
                        btnUsuarios.Visible = false;
                        break;

                    default:
                        // Rol desconocido: acceso mínimo
                        btnDashboard.Visible = true;
                        btnCategorias.Visible = false;
                        btnClientes.Visible = false;
                        btnAnimales.Visible = false;
                        btnPedidos.Visible = false;
                        btnProductos.Visible = false;
                        btnUsuarios.Visible = false;
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al configurar acceso: " + ex.Message, 
                    "Sistema Veterinario", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //Estructura para almacenar colores
        private struct RGBColors
        {
            public static Color Color1 = Color.FromArgb(172, 126, 241);
            public static Color Color2 = Color.FromArgb(249, 118, 176);
            public static Color Color3 = Color.FromArgb(253, 138, 114);
            public static Color Color4 = Color.FromArgb(95, 77, 221);
            public static Color Color5 = Color.FromArgb(249, 88, 155);
            public static Color Color6 = Color.FromArgb(24, 161, 251);
            public static Color Color7 = Color.FromArgb(29, 200, 220);
        }
        //Metodos
        private void ActivateButton(object senderBtn, Color color)
        {
            if (senderBtn != null)
            {
                DisableButton();
                //Boton
                currentBtn = (IconButton)senderBtn;
                currentBtn.BackColor = Color.FromArgb(37, 36, 81);
                currentBtn.ForeColor = color;
                currentBtn.TextAlign = ContentAlignment.MiddleCenter;
                currentBtn.IconColor = color;
                currentBtn.TextImageRelation = TextImageRelation.TextBeforeImage;
                currentBtn.ImageAlign = ContentAlignment.MiddleRight;
                //Borde izq del boton
                leftBorderBtn.BackColor = color;
                leftBorderBtn.Location = new Point(0, currentBtn.Location.Y);
                leftBorderBtn.Visible = true;
                leftBorderBtn.BringToFront();
                //Icono actual del formulario hijo
                iconCurrentChildForm.IconChar = currentBtn.IconChar;
                iconCurrentChildForm.IconColor = color;
            }
        }
        private void DisableButton()
        {
            if (currentBtn != null)
            {
                currentBtn.BackColor = Color.FromArgb(31,30,68);
                currentBtn.ForeColor = Color.Gainsboro;
                currentBtn.TextAlign = ContentAlignment.MiddleLeft;
                currentBtn.IconColor = Color.Gainsboro;
                currentBtn.TextImageRelation = TextImageRelation.ImageBeforeText;
                currentBtn.ImageAlign = ContentAlignment.MiddleLeft;
            }
        }
        //Creamos un formulario hijo y mostramos el titulo en la barra de titulo
        private void OpenChildForm(Form childForm)
        {
            if (currentChildForm != null)
            {
                //Abrimos solo un formulario
                currentChildForm.Close();
            }
            currentChildForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            panelDesktop.Controls.Add(childForm);
            panelDesktop.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
            lblTitleChildForm.Text = childForm.Text;
        }
        //Eventos
        private void btnDashboard_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColors.Color1);
            //OpenChildForm(new DashBoard());
        }
        private void btnCategorias_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColors.Color2);
           // OpenChildForm(new FrmCategories());
        }
        private void btnClientes_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColors.Color3);
            OpenChildForm(new FormPersona());
        }

        private void btnAnimales_Click(object sender, EventArgs e)
        {
            try
            {
                ActivateButton(sender, RGBColors.Color7);
                OpenChildForm(new FrmAnimales());
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al abrir el formulario de animales: " + ex.Message, 
                    "Error del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnPedidos_Click(object sender, EventArgs e)
        {
            try
            {
                ActivateButton(sender, RGBColors.Color4);
                OpenChildForm(new FrmCitas());
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al abrir gestión de citas: " + ex.Message, 
                    "Sistema Veterinario", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnProductos_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColors.Color5);
           // OpenChildForm(FrmProducts.GetInstancia());
        }
        private void btnUsuarios_Click(object sender, EventArgs e)
        {
            try
            {
                ActivateButton(sender, RGBColors.Color6);
                OpenChildForm(new FrmUsers());
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al abrir el formulario de usuarios: " + ex.Message + "\n\n" + 
                    ex.StackTrace + "\n\nInner Exception: " + (ex.InnerException?.Message ?? "None"), 
                    "Error del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            DialogResult resultado = MessageBox.Show("¿Está seguro que desea cerrar la sesión?", 
                "Sistema Veterinario", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (resultado == DialogResult.Yes)
            {
                this.Hide();
                FrmLogin loginForm = new FrmLogin();
                if (loginForm.ShowDialog() == DialogResult.OK)
                {
                    // Actualizar información del usuario
                    CargarInformacionUsuario(loginForm.UsuarioActual);
                    this.Show();
                }
                else
                {
                    // Si cancela el login, cerrar la aplicación
                    Application.Exit();
                }
            }
        }

        private void btnCambiarClave_Click(object sender, EventArgs e)
        {
            try
            {
                FrmCambiarClave frmCambiar = new FrmCambiarClave();
                frmCambiar.IdUsuario = IdUsuario;
                frmCambiar.NombreUsuario = NombreUsuario;
                
                if (frmCambiar.ShowDialog() == DialogResult.OK)
                {
                    MessageBox.Show("Contraseña cambiada exitosamente", "Sistema Veterinario", 
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cambiar contraseña: " + ex.Message, "Sistema Veterinario", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnHome_Click(object sender, EventArgs e)
        {
            currentChildForm.Close();
            Reset();
        }
        private void Reset()
        {
            DisableButton();
            leftBorderBtn.Visible = false;
            iconCurrentChildForm.IconChar = IconChar.Home;
            iconCurrentChildForm.IconColor = Color.MediumPurple;
            lblTitleChildForm.Text = "Inicio";
        }
        //Codigo para arrastrar el formulario e importar libreria System.Runtime.InteropServices
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private static extern void ReleaseCapture();
        [DllImport("user32.DLL",EntryPoint = "SendMessage")]
        private static extern void SendMessage(System.IntPtr hWnd,
            int wMsg, int wParam, int lParam);
        private void panelTitleBar_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void btnMaximize_Click(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Normal)
            {
                WindowState = FormWindowState.Maximized;
            }
            else
            {
                WindowState = FormWindowState.Normal;
            }
        }
        private void btnMinimize_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }
    }
}
