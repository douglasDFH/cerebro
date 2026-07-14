using FontAwesome.Sharp;
using SistemVeterinario.Forms;
using System.Windows.Forms;
using System.Drawing;
using System;
using System.Linq;

namespace SistemVeterinario
{
    public partial class Dashboard : Form
    {
        private Control ModuleActive { get; set; }

        public Dashboard()
        {
            InitializeComponent();
            ConfigurarDashboard();
            InicializarNavegacion();
            timer1.Start();
            ModuleActive = null;
        }

        private void ConfigurarDashboard()
        {
            lblUsuario.Text = "Bienvenido, Falta obtener su nombre";
            lblEmail.Text = "Falta obtener su email";
            // Configurar el orden z de los paneles
            
            // Configurar pictureBox UPDS
            try
            {
                pictureBoxUPDS.Image = Properties.Resources.UPDS;
                pictureBoxUPDS.Visible = true;
            }
            catch (Exception)
            {
                // Si no se puede cargar la imagen, mostrar texto alternativo
                pictureBoxUPDS.BackColor = Color.White;
            }
        }

        private void InicializarNavegacion()
        {
            try
            {
                MostrarPantallaInicial();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error inicializando navegación: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void MostrarPantallaInicial()
        {
            // Limpiar módulos activos
            if (ModuleActive != null)
            {
                panelModulo.Controls.Remove(ModuleActive);
                ModuleActive.Dispose();
                ModuleActive = null;
            }

            // Mostrar pantalla de bienvenida
            panelModulo.Visible = false;
            panel2.Visible = true;
            panel2.BringToFront();
            
            // Asegurar que todos los pictureBox del panel2 sean visibles
            foreach (Control control in panel2.Controls)
            {
                if (control is PictureBox)
                {
                    control.Visible = true;
                    control.BringToFront();
                }
            }
            
            // Asegurar que los labels también estén visibles
            if (lblTitulo != null)
            {
                lblTitulo.Visible = true;
                lblTitulo.BringToFront();
            }
            if (lblSubtitulo != null)
            {
                lblSubtitulo.Visible = true;
                lblSubtitulo.BringToFront();
            }

            CambiarIconoSuperior(IconChar.Home, "Inicio");
        }

        private void OcultarElementosDisenador()
        {
            panel2.Visible = false;
            panelModulo.Visible = true;
            panelModulo.BringToFront();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                var result = MessageBox.Show("¿Está seguro que desea salir del sistema?",
                    "Confirmar Salida", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.No)
                {
                    e.Cancel = true;
                    return;
                }
            }

            base.OnFormClosing(e);
        }

        private void CambiarIconoSuperior(IconChar nuevoIcono, string nuevoTitulo)
        {
            if (iconoSuperior != null)
            {
                iconoSuperior.IconChar = nuevoIcono;
            }
            if (tituloSuperior != null)
            {
                tituloSuperior.Text = nuevoTitulo;
            }
        }

        // Método unificado para cambiar entre módulos
        private void SwitchPanel(UserControl modulo)
        {
            try
            {
                OcultarElementosDisenador();
                // Limpiar módulo activo anterior
                if (ModuleActive != null)
                {
                    panelModulo.Controls.Remove(ModuleActive);
                    ModuleActive.Dispose();
                }

                // Configurar y agregar nuevo módulo
                modulo.Dock = DockStyle.Fill;
                panelModulo.Controls.Add(modulo);
                modulo.BringToFront();
                ModuleActive = modulo;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar el módulo: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Manejadores de eventos para los botones
        private void BtnClientes_Click(object sender, EventArgs e)
        {
            CambiarIconoSuperior(IconChar.Users, "Clientes");
            SwitchPanel(new ClientesModule());
        }

        private void BtnMascotas_Click(object sender, EventArgs e)
        {
            CambiarIconoSuperior(IconChar.Paw, "Mascotas");
            SwitchPanel(new MascotasModule());
        }

        private void BtnPersonal_Click(object sender, EventArgs e)
        {
            CambiarIconoSuperior(IconChar.UserTie, "Personal");
            SwitchPanel(new PersonalModule());
        }

        private void BtnHistorial_Click(object sender, EventArgs e)
        {
            try
            {
                CambiarIconoSuperior(IconChar.FileMedical, "Historial Médico");
                SwitchPanel(new HistorialModule());
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar el módulo de Historial Médico: {ex.Message}\n\nDetalles: {ex.ToString()}", 
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnConsultas_Click(object sender, EventArgs e)
        {
            try
            {
                CambiarIconoSuperior(IconChar.UserMd, "Consultas y Diagnósticos");
                SwitchPanel(new ConsultaModule());
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar el módulo de Consultas: {ex.Message}\n\nDetalles: {ex.ToString()}", 
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnDashboard_Click(object sender, EventArgs e)
        {
            MostrarPantallaInicial();
        }

        private void BtnVentas_Click(object sender, EventArgs e)
        {
            CambiarIconoSuperior(IconChar.ShoppingBag, "Ventas");
            SwitchPanel(new VentasModule());
        }

        private void BtnReportes_Click(object sender, EventArgs e)
        {
            CambiarIconoSuperior(IconChar.ChartLine, "Reportes");
            SwitchPanel(new Reportes.ReportesAvanzados());
        }

        private void BtnConfiguracion_Click(object sender, EventArgs e)
        {
            CambiarIconoSuperior(IconChar.Toolbox, "Configuración");
            MessageBox.Show("Módulo de Configuración - Próximamente", "Información",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BtnLogout_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("¿Está seguro que desea cerrar sesión?",
                "Confirmar Logout", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void BtnProductos_Click(object sender, EventArgs e)
        {
            CambiarIconoSuperior(IconChar.BoxOpen, "Productos");
            SwitchPanel(new ProductosModule());
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblHora.Text = DateTime.Now.ToString("HH:mm:ss - dd/MM/yyyy");
        }

        private void Dashboard_Load(object sender, EventArgs e)
        {
            // Asegurar el orden correcto de los paneles al cargar
            panel1.BringToFront();
            panel2.BringToFront();
            panelModulo.SendToBack();
            
            
            CambiarIconoSuperior(IconChar.Home, "Home");
        }
    }
}