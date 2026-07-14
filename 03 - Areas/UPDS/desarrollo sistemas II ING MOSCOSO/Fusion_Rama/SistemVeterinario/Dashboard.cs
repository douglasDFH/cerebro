

namespace SistemVeterinario
{
    /// <summary>
    /// Dashboard principal del sistema - Pantalla principal después del login
    /// </summary>
    public partial class Dashboard : Form
    {
        private NavigationManager? _navigationManager;

        public Dashboard()
        {
            InitializeComponent();
            ConfigurarDashboard();
            InicializarNavegacion();
        }

        private void ConfigurarDashboard()
        {
            // Configurar información del usuario


            lblUsuario.Text = $"Bienvenido, Falta obtener su nombre";
            lblEmail.Text = "Falta obtener su email";

            // Configurar eventos de botones
            btnClientes.Click += BtnClientes_Click;
            btnMascotas.Click += BtnMascotas_Click;
            btnVentas.Click += BtnVentas_Click;
            btnProductos.Click += BtnProductos_Click;
            btnReportes.Click += BtnReportes_Click;
            btnPersonal.Click += BtnConfiguracion_Click;
            btnLogout.Click += BtnLogout_Click;
        }

        /// <summary>
        /// Inicializa el sistema de navegación modular
        /// </summary>
        private void InicializarNavegacion()
        {
            try
            {
                // NO limpiar el panel - mantener el diseño original del Visual Studio Designer

                // Crear el navegador
                _navigationManager = new NavigationManager(panelContent);
                _navigationManager.NavigationRequested += OnNavigationRequested;

                // La pantalla de bienvenida ya está diseñada en el Visual Studio Designer
                // Solo asegurarse de que esté visible
                MostrarPantallaInicial();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error inicializando navegación: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Muestra la pantalla inicial (diseñada en Visual Studio Designer)
        /// </summary>
        private void MostrarPantallaInicial()
        {
            // Remover cualquier UserControl que pueda estar activo
            var userControls = panelContent.Controls.OfType<UserControl>().ToList();
            foreach (var control in userControls)
            {
                panelContent.Controls.Remove(control);
                control.Dispose();
            }

            // Hacer visibles todos los elementos del diseñador
            foreach (Control control in panelContent.Controls)
            {
                control.Visible = true;
            }

            // Refrescar el panel para asegurar que se vea todo correctamente
            panelContent.Refresh();
        }

        /// <summary>
        /// Maneja las solicitudes de navegación entre vistas
        /// </summary>
        private void OnNavigationRequested(string module, ViewType viewType, object? data)
        {
            try
            {
                switch (module.ToLower())
                {
                    case "cliente":
                        try
                        {
                            using (Cliente frmCliente = new Cliente())
                            {
                                frmCliente.ShowDialog(this);
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Error al abrir el módulo de clientes: {ex.Message}", "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        break;

                    case "mascota":
                        try
                        {
                            using (Mascota frmMascota = new Mascota())
                            {
                                frmMascota.ShowDialog(this);
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Error al abrir el módulo de mascotas: {ex.Message}", "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        break;

                    case "producto":
                        try
                        {
                            using (Producto frmProducto = new Producto())
                            {
                                frmProducto.ShowDialog(this);
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Error al abrir el módulo de productos: {ex.Message}", "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        break;

                    case "venta":
                        // TODO: Implementar navegación de ventas
                        MessageBox.Show("Módulo de Ventas - Próximamente", "Información",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        break;

                    default:
                        MessageBox.Show($"Módulo '{module}' no implementado", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error en navegación: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Oculta los elementos del diseñador cuando se navega a un módulo
        /// </summary>
        private void OcultarElementosDisenador()
        {
            foreach (Control control in panelContent.Controls)
            {
                if (!(control is UserControl))
                {
                    control.Visible = false;
                }
            }
        }

        /// <summary>
        /// Maneja la navegación específica del módulo de Clientes
        /// </summary>
        private void ManejarNavegacionCliente(ViewType viewType, object? data)
        {
            UserControl? vista = null;

            // Ocultar elementos del diseñador
            OcultarElementosDisenador();

            switch (viewType)
            {
                case ViewType.Index:

                    break;

                case ViewType.Create:

                    break;

                case ViewType.Edit:

                    break;
            }

            if (vista != null && _navigationManager != null)
            {
                _navigationManager.NavigateTo("Cliente", viewType, vista, data);
            }
        }


        private void BtnClientes_Click(object? sender, EventArgs e)
        {
            try
            {
                using (Cliente frmCliente = new Cliente())
                {
                    frmCliente.ShowDialog(this);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al abrir el módulo de clientes: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnMascotas_Click(object? sender, EventArgs e)
        {
            try
            {
                using (Mascota frmMascota = new Mascota())
                {
                    frmMascota.ShowDialog(this);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al abrir el módulo de mascotas: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnVentas_Click(object? sender, EventArgs e)
        {
            try
            {
                using (Venta frmVenta = new Venta())
                {
                    frmVenta.ShowDialog(this);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al abrir el módulo de ventas: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnProductos_Click(object? sender, EventArgs e)
        {
            try
            {
                using (Producto frmProducto = new Producto())
                {
                    frmProducto.ShowDialog(this);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al abrir el módulo de productos: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnReportes_Click(object? sender, EventArgs e)
        {
            try
            {
                using (Reportes frmReportes = new Reportes())
                {
                    frmReportes.ShowDialog(this);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al abrir el módulo de reportes: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnConfiguracion_Click(object? sender, EventArgs e)
        {
            // TODO: Implementar formulario de configuración
            MessageBox.Show("Módulo de Configuración - Próximamente", "Información",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BtnLogout_Click(object? sender, EventArgs e)
        {
            var result = MessageBox.Show("¿Está seguro que desea cerrar sesión?",
                "Confirmar Logout", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                // Cerrar el dashboard - esto hará que se regrese al login automáticamente
                this.Close();
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            // Solo preguntar si el usuario está cerrando manualmente (no cuando se hace logout)
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

            // Limpiar recursos de navegación
            _navigationManager?.Dispose();

            base.OnFormClosing(e);
        }

        /// <summary>
        /// Maneja la navegación específica del módulo de Mascotas
        /// </summary>
        private void ManejarNavegacionMascota(ViewType viewType, object? data)
        {
            UserControl? vista = null;

            switch (viewType)
            {
                case ViewType.Index:

                    break;

                case ViewType.Create:

                    break;

                case ViewType.Edit:

                    break;
            }

            if (vista != null && _navigationManager != null)
            {
                _navigationManager.NavigateTo("Mascota", viewType, vista, data);
            }
        }






    }
}
