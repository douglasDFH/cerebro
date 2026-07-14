using System.Drawing;

namespace SistemVeterinario
{
    partial class Mascota
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
        private DataGridView dgvMascotas;
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
        private Button btnHistorial;

        // Controles datos mascota
        private GroupBox grpDatosMascota;
        private Label lblNombre;
        private TextBox txtNombre;
        private Label lblEspecie;
        private ComboBox cmbEspecie;
        private Label lblRaza;
        private ComboBox cmbRaza;
        private TextBox txtRaza;
        private Label lblPropietario;
        private TextBox txtPropietario;
        private Button btnBuscarPropietario;
        private Label lblGenero;
        private ComboBox cmbGenero;
        private Label lblPeso;
        private NumericUpDown nudPeso;
        private Label lblColor;
        private TextBox txtColor;
        private Label lblMicrochip;
        private TextBox txtMicrochip;
        private Label lblFechaNacimiento;
        private DateTimePicker dtpFechaNacimiento;
        private CheckBox chkTieneFechaNacimiento;
        private CheckBox chkEsterilizado;

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
            this.Text = "Gestión de Mascotas";
            this.Size = new Size(1200, 750);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.MinimumSize = new Size(1000, 600);

            // TabControl principal
            tabControl = new TabControl();
            tabControl.Dock = DockStyle.Fill;

            // Pestaña de Listado
            tabListado = new TabPage("Listado de Mascotas");
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
            lblBuscar.Text = "Buscar Mascota:";
            lblBuscar.Location = new Point(10, 20);
            lblBuscar.Size = new Size(100, 20);

            txtBuscar = new TextBox();
            txtBuscar.Location = new Point(120, 18);
            txtBuscar.Size = new Size(200, 23);
            txtBuscar.PlaceholderText = "Nombre, especie, raza, propietario...";

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
            dgvMascotas = new DataGridView();
            dgvMascotas.Dock = DockStyle.Fill;
            dgvMascotas.AllowUserToAddRows = false;
            dgvMascotas.AllowUserToDeleteRows = false;
            dgvMascotas.ReadOnly = true;
            dgvMascotas.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvMascotas.MultiSelect = false;
            dgvMascotas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            dgvMascotas.AutoGenerateColumns = true;
            dgvMascotas.BackgroundColor = Color.White;
            dgvMascotas.AlternatingRowsDefaultCellStyle.BackColor = Color.AliceBlue;
            dgvMascotas.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvMascotas.RowHeadersVisible = false;
            dgvMascotas.BorderStyle = BorderStyle.FixedSingle;

            tabListado.Controls.Add(dgvMascotas);
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

            btnHistorial = new Button();
            btnHistorial.Text = "Historial";
            btnHistorial.Location = new Point(460, 15);
            btnHistorial.Size = new Size(80, 30);
            btnHistorial.BackColor = Color.PaleGoldenrod;

            panelBotones.Controls.AddRange(new Control[] {
                btnNuevo, btnGuardar, btnEditar, btnEliminar, btnCancelar, btnHistorial
            });

            // Panel principal de contenido con scroll
            Panel panelContenido = new Panel();
            panelContenido.Dock = DockStyle.Fill;
            panelContenido.Padding = new Padding(10);
            panelContenido.AutoScroll = true;

            // GroupBox - Datos de la Mascota
            grpDatosMascota = new GroupBox();
            grpDatosMascota.Text = "Datos de la Mascota";
            grpDatosMascota.Location = new Point(10, 10);
            grpDatosMascota.Size = new Size(850, 350);

            // Fila 1
            lblNombre = new Label();
            lblNombre.Text = "Nombre *:";
            lblNombre.Location = new Point(15, 30);
            lblNombre.Size = new Size(70, 20);

            txtNombre = new TextBox();
            txtNombre.Location = new Point(90, 28);
            txtNombre.Size = new Size(150, 23);

            lblEspecie = new Label();
            lblEspecie.Text = "Especie *:";
            lblEspecie.Location = new Point(260, 30);
            lblEspecie.Size = new Size(70, 20);

            cmbEspecie = new ComboBox();
            cmbEspecie.Location = new Point(335, 28);
            cmbEspecie.Size = new Size(120, 23);
            cmbEspecie.DropDownStyle = ComboBoxStyle.DropDownList;

            lblGenero = new Label();
            lblGenero.Text = "Sexo:";
            lblGenero.Location = new Point(480, 30);
            lblGenero.Size = new Size(40, 20);

            cmbGenero = new ComboBox();
            cmbGenero.Location = new Point(525, 28);
            cmbGenero.Size = new Size(60, 23);
            cmbGenero.DropDownStyle = ComboBoxStyle.DropDownList;

            lblPeso = new Label();
            lblPeso.Text = "Peso (kg):";
            lblPeso.Location = new Point(610, 30);
            lblPeso.Size = new Size(60, 20);

            nudPeso = new NumericUpDown();
            nudPeso.Location = new Point(675, 28);
            nudPeso.Size = new Size(80, 23);
            nudPeso.DecimalPlaces = 2;
            nudPeso.Maximum = 999;
            nudPeso.Minimum = 0;

            // Fila 2
            lblRaza = new Label();
            lblRaza.Text = "Raza:";
            lblRaza.Location = new Point(15, 65);
            lblRaza.Size = new Size(50, 20);

            cmbRaza = new ComboBox();
            cmbRaza.Location = new Point(70, 63);
            cmbRaza.Size = new Size(120, 23);
            cmbRaza.DropDownStyle = ComboBoxStyle.DropDownList;

            txtRaza = new TextBox();
            txtRaza.Location = new Point(200, 63);
            txtRaza.Size = new Size(120, 23);
            txtRaza.PlaceholderText = "o escriba otra raza";

            lblColor = new Label();
            lblColor.Text = "Color:";
            lblColor.Location = new Point(340, 65);
            lblColor.Size = new Size(50, 20);

            txtColor = new TextBox();
            txtColor.Location = new Point(395, 63);
            txtColor.Size = new Size(120, 23);

            chkEsterilizado = new CheckBox();
            chkEsterilizado.Text = "Esterilizado";
            chkEsterilizado.Location = new Point(540, 65);
            chkEsterilizado.Size = new Size(100, 20);

            // Fila 3
            lblPropietario = new Label();
            lblPropietario.Text = "Propietario *:";
            lblPropietario.Location = new Point(15, 100);
            lblPropietario.Size = new Size(80, 20);

            txtPropietario = new TextBox();
            txtPropietario.Location = new Point(100, 98);
            txtPropietario.Size = new Size(250, 23);
            txtPropietario.ReadOnly = true;
            txtPropietario.BackColor = Color.LightGray;

            btnBuscarPropietario = new Button();
            btnBuscarPropietario.Text = "Buscar";
            btnBuscarPropietario.Location = new Point(360, 97);
            btnBuscarPropietario.Size = new Size(70, 25);
            btnBuscarPropietario.BackColor = Color.LightBlue;

            lblMicrochip = new Label();
            lblMicrochip.Text = "Microchip:";
            lblMicrochip.Location = new Point(450, 100);
            lblMicrochip.Size = new Size(70, 20);

            txtMicrochip = new TextBox();
            txtMicrochip.Location = new Point(525, 98);
            txtMicrochip.Size = new Size(150, 23);

            // Fila 4
            chkTieneFechaNacimiento = new CheckBox();
            chkTieneFechaNacimiento.Text = "Fecha de Nacimiento:";
            chkTieneFechaNacimiento.Location = new Point(15, 135);
            chkTieneFechaNacimiento.Size = new Size(140, 20);

            dtpFechaNacimiento = new DateTimePicker();
            dtpFechaNacimiento.Location = new Point(165, 133);
            dtpFechaNacimiento.Size = new Size(150, 23);
            dtpFechaNacimiento.Format = DateTimePickerFormat.Short;
            dtpFechaNacimiento.Enabled = false;

            grpDatosMascota.Controls.AddRange(new Control[] {
                lblNombre, txtNombre, lblEspecie, cmbEspecie, lblGenero, cmbGenero, lblPeso, nudPeso,
                lblRaza, cmbRaza, txtRaza, lblColor, txtColor, chkEsterilizado,
                lblPropietario, txtPropietario, btnBuscarPropietario, lblMicrochip, txtMicrochip,
                chkTieneFechaNacimiento, dtpFechaNacimiento
            });

            panelContenido.Controls.Add(grpDatosMascota);

            tabMantenimiento.Controls.Add(panelContenido);
            tabMantenimiento.Controls.Add(panelBotones);
        }

        #endregion
    }
}