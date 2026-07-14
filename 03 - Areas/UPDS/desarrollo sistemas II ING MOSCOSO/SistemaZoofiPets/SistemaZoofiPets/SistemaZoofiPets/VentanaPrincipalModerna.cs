using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace SistemaZoofiPets
{
    public partial class VentanaPrincipalModerna : Form
    {
        // Declarar campos
        private Button currentBtn;
        private Panel leftBorderBtn;
        private Form currentChildForm;

        // Nota: Los controles principales están declarados en VentanaPrincipalModerna.Designer.cs

        public string Iduser;
        public string Nombre;
        public string Email;
        public string Acceso;

        public VentanaPrincipalModerna()
        {
            InitializeComponent();
            PostInitializeSetup();
        }

        private void PostInitializeSetup()
        {
            // Crear los botones del menú y controles de la barra de título
            CreateMenuButtons();
            
            // Configurar borde lateral para botones activos
            leftBorderBtn = new Panel();
            leftBorderBtn.Size = new Size(7, 60);
            if (panelMenu != null)
                panelMenu.Controls.Add(leftBorderBtn);
            
            // Configuración de ventana sin borde
            this.Text = string.Empty;
            this.ControlBox = false;
            this.DoubleBuffered = true;
            this.MaximizedBounds = Screen.FromHandle(this.Handle).WorkingArea;
        }

        // Colores para diferentes módulos usando la paleta violeta
        private struct ColoresModulos
        {
            public static Color Dashboard = ColoresPastelGlobales.VioletaClaro;
            public static Color Animales = ColoresPastelGlobales.VerdeSuave;
            public static Color Clientes = ColoresPastelGlobales.AzulSuave;
            public static Color Citas = ColoresPastelGlobales.VioletaMedio;
            public static Color Inventario = ColoresPastelGlobales.NaranjaSuave;
            public static Color Ventas = ColoresPastelGlobales.VerdeSuave;
            public static Color Usuarios = ColoresPastelGlobales.RojoSuave;
            public static Color Reportes = ColoresPastelGlobales.GrisVioletaClaro;
        }


        private void CreateMenuButtons()
        {
            // Definir elementos del menú
            string[] menuItems = { 
                "🏠  Dashboard", 
                "🐾  Animales", 
                "👥  Clientes", 
                "📅  Citas", 
                "📦  Inventario", 
                "💰  Ventas", 
                "👤  Usuarios", 
                "📊  Reportes" 
            };
            
            EventHandler[] handlers = { 
                btnDashboard_Click, 
                btnAnimales_Click, 
                btnClientes_Click, 
                btnCitas_Click, 
                btnInventario_Click, 
                btnVentas_Click, 
                btnUsuarios_Click, 
                btnReportes_Click 
            };
            
            int yPosition = 20;
            for (int i = 0; i < menuItems.Length; i++)
            {
                Button btn = new Button();
                btn.Text = menuItems[i];
                btn.Size = new Size(250, 60);
                btn.Location = new Point(0, yPosition);
                btn.FlatStyle = FlatStyle.Flat;
                btn.FlatAppearance.BorderSize = 1;
                btn.FlatAppearance.BorderColor = ColoresPastelGlobales.NegroSuave;
                btn.BackColor = ColoresPastelGlobales.Navegacion.BotonInactivo;
                btn.ForeColor = ColoresPastelGlobales.Navegacion.TextoBoton;
                btn.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
                btn.TextAlign = ContentAlignment.MiddleLeft;
                btn.Padding = new Padding(12, 0, 0, 0);
                btn.Click += handlers[i];
                panelMenu.Controls.Add(btn);
                yPosition += 60;
            }
        }


        // Métodos para manejar botones activos
        private void ActivateButton(object senderBtn, Color color)
        {
            if (senderBtn != null)
            {
                DisableButton();
                currentBtn = (Button)senderBtn;
                currentBtn.BackColor = ColoresPastelGlobales.Navegacion.BotonActivo;
                currentBtn.ForeColor = ColoresPastelGlobales.NegroCarbon;
                currentBtn.TextAlign = ContentAlignment.MiddleCenter;
                currentBtn.FlatAppearance.BorderColor = ColoresPastelGlobales.BlancoPuro;
                
                // Configurar borde lateral
                leftBorderBtn.BackColor = color;
                leftBorderBtn.Location = new Point(0, currentBtn.Location.Y);
                leftBorderBtn.Visible = true;
                leftBorderBtn.BringToFront();
                
                // Actualizar icono
                if (iconCurrentChildForm != null)
                    iconCurrentChildForm.ForeColor = color;
            }
        }

        private void DisableButton()
        {
            if (currentBtn != null)
            {
                currentBtn.BackColor = ColoresPastelGlobales.Navegacion.BotonInactivo;
                currentBtn.ForeColor = ColoresPastelGlobales.Navegacion.TextoBoton;
                currentBtn.TextAlign = ContentAlignment.MiddleLeft;
                currentBtn.FlatAppearance.BorderColor = ColoresPastelGlobales.NegroSuave;
            }
        }

        private void OpenChildForm(Form childForm)
        {
            if (currentChildForm != null)
            {
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

        // Event handlers para los botones
        private void btnDashboard_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, ColoresModulos.Dashboard);
            iconCurrentChildForm.Text = "🏠";
            lblTitleChildForm.Text = "Dashboard - Inicio";
            OpenChildForm(new FormDashboard());
        }

        private void btnAnimales_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, ColoresModulos.Animales);
            iconCurrentChildForm.Text = "🐾";
            MessageBox.Show("Módulo de Animales en desarrollo", "Información", 
                          MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnClientes_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, ColoresModulos.Clientes);
            iconCurrentChildForm.Text = "👥";
            OpenChildForm(new FormClientesSimple());
        }

        private void btnCitas_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, ColoresModulos.Citas);
            iconCurrentChildForm.Text = "📅";
            MessageBox.Show("Módulo de Citas en desarrollo", "Información", 
                          MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnInventario_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, ColoresModulos.Inventario);
            iconCurrentChildForm.Text = "📦";
            OpenChildForm(new FormInventario());
        }

        private void btnVentas_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, ColoresModulos.Ventas);
            iconCurrentChildForm.Text = "💰";
            OpenChildForm(new FormVentas());
        }

        private void btnUsuarios_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, ColoresModulos.Usuarios);
            iconCurrentChildForm.Text = "👤";
            OpenChildForm(new FormUsers());
        }

        private void btnReportes_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, ColoresModulos.Reportes);
            iconCurrentChildForm.Text = "📊";
            OpenChildForm(new FormReportes());
        }

        private void Reset()
        {
            DisableButton();
            leftBorderBtn.Visible = false;
            iconCurrentChildForm.Text = "🏠";
            iconCurrentChildForm.ForeColor = ColoresPastelGlobales.VioletaProfundo;
            lblTitleChildForm.Text = "Inicio";
        }

        // Funcionalidad para arrastrar ventana
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private static extern void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private static extern void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);

        private void panelTitleBar_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void VentanaPrincipalModerna_Load(object sender, EventArgs e)
        {
            if (iconCurrentUser != null)
                iconCurrentUser.Text = $"👤 {Nombre}";
        }

    }
}