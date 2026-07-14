using System.Drawing;

namespace SistemVeterinario
{
    partial class Cliente
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        // Controles principales
        private TabControl tabControl;
        private TabPage tabListado;
        private TabPage tabMantenimiento;

        // Controles del listado
        private DataGridView dgvClientes;
        private Panel panelBusqueda;
        private Label lblBuscar;
        private TextBox txtBuscar;
        private Button btnBuscar;
        private Button btnRefrescar;
        private Label lblTotalRegistros;

        // Controles del mantenimiento
        private Panel panelBotones;
        private Button btnNuevo;
        private Button btnGuardar;
        private Button btnEditar;
        private Button btnEliminar;
        private Button btnCancelar;

        // Controles generales
        private GroupBox grpTipoPersona;
        private Label lblTipoPersona;
        private ComboBox cmbTipoPersona;
        
        private GroupBox grpDatosGenerales;
        private Label lblEmail;
        private TextBox txtEmail;
        private Label lblDireccion;
        private TextBox txtDireccion;
        private Label lblTelefono;
        private TextBox txtTelefono;

        // Controles persona física
        private GroupBox grpPersonaFisica;
        private Label lblCi;
        private TextBox txtCi;
        private Label lblNombre;
        private TextBox txtNombre;
        private Label lblApellido;
        private TextBox txtApellido;
        private Label lblFechaNacimiento;
        private DateTimePicker dtpFechaNacimiento;
        private Label lblGenero;
        private ComboBox cmbGenero;

        // Controles persona jurídica
        private GroupBox grpPersonaJuridica;
        private Label lblRazonSocial;
        private TextBox txtRazonSocial;
        private Label lblNit;
        private TextBox txtNit;
        private Label lblEncargadoNombre;
        private TextBox txtEncargadoNombre;
        private Label lblEncargadoCargo;
        private TextBox txtEncargadoCargo;

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
            components = new System.ComponentModel.Container();
            
            // Configuración del formulario
            this.Text = "Gestión de Clientes";
            this.Size = new Size(1200, 700);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.MinimumSize = new Size(1000, 600);

            // TabControl principal
            tabControl = new TabControl();
            tabControl.Dock = DockStyle.Fill;

            // Pestaña de Listado
            tabListado = new TabPage("Listado de Clientes");
            InitializeTabListado();
            tabControl.TabPages.Add(tabListado);

            // Pestaña de Mantenimiento
            tabMantenimiento = new TabPage("Mantenimiento");
            InitializeTabMantenimiento();
            tabControl.TabPages.Add(tabMantenimiento);

            // Agregar el TabControl al formulario
            this.Controls.Add(tabControl);
        }

        private void InitializeTabListado()
        {
            // Panel de búsqueda
            panelBusqueda = new Panel();
            panelBusqueda.Height = 60;
            panelBusqueda.Dock = DockStyle.Top;
            panelBusqueda.BackColor = Color.LightGray;
            panelBusqueda.Padding = new Padding(10);

            lblBuscar = new Label();
            lblBuscar.Text = "Buscar Cliente:";
            lblBuscar.Location = new Point(10, 20);
            lblBuscar.Size = new Size(100, 20);

            txtBuscar = new TextBox();
            txtBuscar.Location = new Point(120, 18);
            txtBuscar.Size = new Size(200, 23);
            txtBuscar.PlaceholderText = "Nombre, CI, NIT, Email...";

            btnBuscar = new Button();
            btnBuscar.Text = "Buscar";
            btnBuscar.Location = new Point(330, 17);
            btnBuscar.Size = new Size(75, 25);
            btnBuscar.BackColor = Color.LightBlue;

            btnRefrescar = new Button();
            btnRefrescar.Text = "Refrescar";
            btnRefrescar.Location = new Point(415, 17);
            btnRefrescar.Size = new Size(75, 25);
            btnRefrescar.BackColor = Color.LightGreen;

            lblTotalRegistros = new Label();
            lblTotalRegistros.Text = "Total registros: 0";
            lblTotalRegistros.Location = new Point(520, 20);
            lblTotalRegistros.Size = new Size(150, 20);
            lblTotalRegistros.ForeColor = Color.DarkBlue;

            panelBusqueda.Controls.AddRange(new Control[] {
                lblBuscar, txtBuscar, btnBuscar, btnRefrescar, lblTotalRegistros
            });

            // DataGridView
            dgvClientes = new DataGridView();
            dgvClientes.Dock = DockStyle.Fill;
            dgvClientes.AllowUserToAddRows = false;
            dgvClientes.AllowUserToDeleteRows = false;
            dgvClientes.ReadOnly = true;
            dgvClientes.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvClientes.MultiSelect = false;
            dgvClientes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgvClientes.BackgroundColor = Color.White;
            dgvClientes.AlternatingRowsDefaultCellStyle.BackColor = Color.AliceBlue;
            dgvClientes.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;

            tabListado.Controls.Add(dgvClientes);
            tabListado.Controls.Add(panelBusqueda);
        }

        private void InitializeTabMantenimiento()
        {
            // Panel de botones
            panelBotones = new Panel();
            panelBotones.Height = 60;
            panelBotones.Dock = DockStyle.Top;
            panelBotones.BackColor = Color.LightGray;
            panelBotones.Padding = new Padding(10);

            btnNuevo = new Button();
            btnNuevo.Text = "Nuevo";
            btnNuevo.Location = new Point(10, 15);
            btnNuevo.Size = new Size(80, 30);
            btnNuevo.BackColor = Color.LightGreen;

            btnGuardar = new Button();
            btnGuardar.Text = "Guardar";
            btnGuardar.Location = new Point(100, 15);
            btnGuardar.Size = new Size(80, 30);
            btnGuardar.BackColor = Color.LightBlue;

            btnEditar = new Button();
            btnEditar.Text = "Editar";
            btnEditar.Location = new Point(190, 15);
            btnEditar.Size = new Size(80, 30);
            btnEditar.BackColor = Color.LightYellow;

            btnEliminar = new Button();
            btnEliminar.Text = "Eliminar";
            btnEliminar.Location = new Point(280, 15);
            btnEliminar.Size = new Size(80, 30);
            btnEliminar.BackColor = Color.LightCoral;

            btnCancelar = new Button();
            btnCancelar.Text = "Cancelar";
            btnCancelar.Location = new Point(370, 15);
            btnCancelar.Size = new Size(80, 30);
            btnCancelar.BackColor = Color.LightGray;

            panelBotones.Controls.AddRange(new Control[] {
                btnNuevo, btnGuardar, btnEditar, btnEliminar, btnCancelar
            });

            // Panel principal de contenido con scroll
            Panel panelContenido = new Panel();
            panelContenido.Dock = DockStyle.Fill;
            panelContenido.Padding = new Padding(10);
            panelContenido.AutoScroll = true;

            // GroupBox - Tipo de Persona
            grpTipoPersona = new GroupBox();
            grpTipoPersona.Text = "Tipo de Persona";
            grpTipoPersona.Location = new Point(10, 10);
            grpTipoPersona.Size = new Size(300, 60);

            lblTipoPersona = new Label();
            lblTipoPersona.Text = "Tipo *:";
            lblTipoPersona.Location = new Point(15, 25);
            lblTipoPersona.Size = new Size(50, 20);

            cmbTipoPersona = new ComboBox();
            cmbTipoPersona.Location = new Point(75, 23);
            cmbTipoPersona.Size = new Size(150, 23);
            cmbTipoPersona.DropDownStyle = ComboBoxStyle.DropDownList;

            grpTipoPersona.Controls.AddRange(new Control[] {
                lblTipoPersona, cmbTipoPersona
            });

            // GroupBox - Datos Generales
            grpDatosGenerales = new GroupBox();
            grpDatosGenerales.Text = "Datos Generales";
            grpDatosGenerales.Location = new Point(320, 10);
            grpDatosGenerales.Size = new Size(520, 100);

            lblEmail = new Label();
            lblEmail.Text = "Email:";
            lblEmail.Location = new Point(15, 25);
            lblEmail.Size = new Size(50, 20);

            txtEmail = new TextBox();
            txtEmail.Location = new Point(80, 23);
            txtEmail.Size = new Size(200, 23);

            lblTelefono = new Label();
            lblTelefono.Text = "Teléfono:";
            lblTelefono.Location = new Point(290, 25);
            lblTelefono.Size = new Size(60, 20);

            txtTelefono = new TextBox();
            txtTelefono.Location = new Point(360, 23);
            txtTelefono.Size = new Size(150, 23);

            lblDireccion = new Label();
            lblDireccion.Text = "Dirección:";
            lblDireccion.Location = new Point(15, 55);
            lblDireccion.Size = new Size(65, 20);

            txtDireccion = new TextBox();
            txtDireccion.Location = new Point(80, 53);
            txtDireccion.Size = new Size(430, 23);

            grpDatosGenerales.Controls.AddRange(new Control[] {
                lblEmail, txtEmail, lblTelefono, txtTelefono, lblDireccion, txtDireccion
            });

            // GroupBox - Persona Física
            grpPersonaFisica = new GroupBox();
            grpPersonaFisica.Text = "Datos de Persona Física";
            grpPersonaFisica.Location = new Point(10, 120);
            grpPersonaFisica.Size = new Size(830, 140);

            lblCi = new Label();
            lblCi.Text = "Cédula:";
            lblCi.Location = new Point(15, 25);
            lblCi.Size = new Size(60, 20);

            txtCi = new TextBox();
            txtCi.Location = new Point(80, 23);
            txtCi.Size = new Size(120, 23);

            lblNombre = new Label();
            lblNombre.Text = "Nombre *:";
            lblNombre.Location = new Point(220, 25);
            lblNombre.Size = new Size(70, 20);

            txtNombre = new TextBox();
            txtNombre.Location = new Point(295, 23);
            txtNombre.Size = new Size(150, 23);

            lblApellido = new Label();
            lblApellido.Text = "Apellido *:";
            lblApellido.Location = new Point(465, 25);
            lblApellido.Size = new Size(70, 20);

            txtApellido = new TextBox();
            txtApellido.Location = new Point(540, 23);
            txtApellido.Size = new Size(150, 23);

            lblFechaNacimiento = new Label();
            lblFechaNacimiento.Text = "Fecha Nac.:";
            lblFechaNacimiento.Location = new Point(15, 60);
            lblFechaNacimiento.Size = new Size(80, 20);

            dtpFechaNacimiento = new DateTimePicker();
            dtpFechaNacimiento.Location = new Point(100, 58);
            dtpFechaNacimiento.Size = new Size(150, 23);
            dtpFechaNacimiento.Format = DateTimePickerFormat.Short;

            lblGenero = new Label();
            lblGenero.Text = "Género:";
            lblGenero.Location = new Point(270, 60);
            lblGenero.Size = new Size(60, 20);

            cmbGenero = new ComboBox();
            cmbGenero.Location = new Point(335, 58);
            cmbGenero.Size = new Size(80, 23);
            cmbGenero.DropDownStyle = ComboBoxStyle.DropDownList;

            grpPersonaFisica.Controls.AddRange(new Control[] {
                lblCi, txtCi, lblNombre, txtNombre, lblApellido, txtApellido,
                lblFechaNacimiento, dtpFechaNacimiento, lblGenero, cmbGenero
            });

            // GroupBox - Persona Jurídica
            grpPersonaJuridica = new GroupBox();
            grpPersonaJuridica.Text = "Datos de Persona Jurídica";
            grpPersonaJuridica.Location = new Point(10, 120);
            grpPersonaJuridica.Size = new Size(830, 140);
            grpPersonaJuridica.Visible = false; // Inicialmente oculto

            lblRazonSocial = new Label();
            lblRazonSocial.Text = "Razón Social *:";
            lblRazonSocial.Location = new Point(15, 25);
            lblRazonSocial.Size = new Size(90, 20);

            txtRazonSocial = new TextBox();
            txtRazonSocial.Location = new Point(110, 23);
            txtRazonSocial.Size = new Size(300, 23);

            lblNit = new Label();
            lblNit.Text = "NIT:";
            lblNit.Location = new Point(430, 25);
            lblNit.Size = new Size(40, 20);

            txtNit = new TextBox();
            txtNit.Location = new Point(480, 23);
            txtNit.Size = new Size(150, 23);

            lblEncargadoNombre = new Label();
            lblEncargadoNombre.Text = "Encargado:";
            lblEncargadoNombre.Location = new Point(15, 60);
            lblEncargadoNombre.Size = new Size(80, 20);

            txtEncargadoNombre = new TextBox();
            txtEncargadoNombre.Location = new Point(100, 58);
            txtEncargadoNombre.Size = new Size(200, 23);

            lblEncargadoCargo = new Label();
            lblEncargadoCargo.Text = "Cargo:";
            lblEncargadoCargo.Location = new Point(320, 60);
            lblEncargadoCargo.Size = new Size(50, 20);

            txtEncargadoCargo = new TextBox();
            txtEncargadoCargo.Location = new Point(375, 58);
            txtEncargadoCargo.Size = new Size(150, 23);

            grpPersonaJuridica.Controls.AddRange(new Control[] {
                lblRazonSocial, txtRazonSocial, lblNit, txtNit,
                lblEncargadoNombre, txtEncargadoNombre, lblEncargadoCargo, txtEncargadoCargo
            });

            panelContenido.Controls.AddRange(new Control[] {
                grpTipoPersona, grpDatosGenerales, grpPersonaFisica, grpPersonaJuridica
            });

            tabMantenimiento.Controls.Add(panelContenido);
            tabMantenimiento.Controls.Add(panelBotones);
        }

        #endregion
    }
}