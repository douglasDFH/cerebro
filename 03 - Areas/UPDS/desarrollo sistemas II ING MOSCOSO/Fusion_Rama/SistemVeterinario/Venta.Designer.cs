using System.Drawing;

namespace SistemVeterinario
{
    partial class Venta
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
        private DataGridView dgvVentas;
        private Panel panelBusqueda;
        private Label lblBuscarPersona;
        private TextBox txtBuscarPersonaId;
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

        private GroupBox grpDatosFactura;
        private Label lblNumeroFactura;
        private TextBox txtNumeroFactura;
        private Label lblPersonaId;
        private TextBox txtPersonaId;
        private Label lblFechaEmision;
        private DateTimePicker dtpFechaEmision;
        private Label lblFechaVencimiento;
        private DateTimePicker dtpFechaVencimiento;
        private CheckBox chkTieneFechaVencimiento;
        private Label lblEstado;
        private ComboBox cmbEstado;

        private GroupBox grpMontos;
        private Label lblImpuestos;
        private NumericUpDown nudImpuestos;
        private Label lblDescuentos;
        private NumericUpDown nudDescuentos;
        private CheckBox chkFinalizar;

        private GroupBox grpDetalles;
        private Label lblNotas;
        private TextBox txtNotas;
        private Label lblProductos;
        private TextBox txtProductosJson;
        private Label lblServicios;
        private TextBox txtServiciosJson;

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
            this.Text = "Gestión de Ventas";
            this.Size = new Size(1200, 700);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.MinimumSize = new Size(1000, 600);

            // TabControl principal
            tabControl = new TabControl();
            tabControl.Dock = DockStyle.Fill;

            // Pestaña de Listado
            tabListado = new TabPage("Listado de Ventas");
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

            lblBuscarPersona = new Label();
            lblBuscarPersona.Text = "ID Persona:";
            lblBuscarPersona.Location = new Point(10, 20);
            lblBuscarPersona.Size = new Size(80, 20);

            txtBuscarPersonaId = new TextBox();
            txtBuscarPersonaId.Location = new Point(100, 18);
            txtBuscarPersonaId.Size = new Size(100, 23);

            btnBuscar = new Button();
            btnBuscar.Text = "Buscar";
            btnBuscar.Location = new Point(210, 17);
            btnBuscar.Size = new Size(75, 25);
            btnBuscar.BackColor = Color.LightBlue;

            btnRefrescar = new Button();
            btnRefrescar.Text = "Refrescar";
            btnRefrescar.Location = new Point(295, 17);
            btnRefrescar.Size = new Size(75, 25);
            btnRefrescar.BackColor = Color.LightGreen;

            lblTotalRegistros = new Label();
            lblTotalRegistros.Text = "Total registros: 0";
            lblTotalRegistros.Location = new Point(400, 20);
            lblTotalRegistros.Size = new Size(150, 20);
            lblTotalRegistros.ForeColor = Color.DarkBlue;

            panelBusqueda.Controls.AddRange(new Control[] {
                lblBuscarPersona, txtBuscarPersonaId, btnBuscar, btnRefrescar, lblTotalRegistros
            });

            // DataGridView
            dgvVentas = new DataGridView();
            dgvVentas.Dock = DockStyle.Fill;
            dgvVentas.AllowUserToAddRows = false;
            dgvVentas.AllowUserToDeleteRows = false;
            dgvVentas.ReadOnly = true;
            dgvVentas.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvVentas.MultiSelect = false;
            dgvVentas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgvVentas.BackgroundColor = Color.White;
            dgvVentas.AlternatingRowsDefaultCellStyle.BackColor = Color.AliceBlue;
            dgvVentas.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;

            tabListado.Controls.Add(dgvVentas);
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

            // Panel principal de contenido
            Panel panelContenido = new Panel();
            panelContenido.Dock = DockStyle.Fill;
            panelContenido.Padding = new Padding(10);
            panelContenido.AutoScroll = true;

            // GroupBox - Datos de la Factura
            grpDatosFactura = new GroupBox();
            grpDatosFactura.Text = "Datos de la Factura";
            grpDatosFactura.Location = new Point(10, 10);
            grpDatosFactura.Size = new Size(560, 180);

            lblNumeroFactura = new Label();
            lblNumeroFactura.Text = "Número de Factura *:";
            lblNumeroFactura.Location = new Point(15, 30);
            lblNumeroFactura.Size = new Size(120, 20);

            txtNumeroFactura = new TextBox();
            txtNumeroFactura.Location = new Point(145, 28);
            txtNumeroFactura.Size = new Size(150, 23);

            lblPersonaId = new Label();
            lblPersonaId.Text = "ID Persona *:";
            lblPersonaId.Location = new Point(315, 30);
            lblPersonaId.Size = new Size(80, 20);

            txtPersonaId = new TextBox();
            txtPersonaId.Location = new Point(405, 28);
            txtPersonaId.Size = new Size(100, 23);

            lblFechaEmision = new Label();
            lblFechaEmision.Text = "Fecha Emisión:";
            lblFechaEmision.Location = new Point(15, 65);
            lblFechaEmision.Size = new Size(100, 20);

            dtpFechaEmision = new DateTimePicker();
            dtpFechaEmision.Location = new Point(145, 63);
            dtpFechaEmision.Size = new Size(150, 23);
            dtpFechaEmision.Format = DateTimePickerFormat.Short;

            chkTieneFechaVencimiento = new CheckBox();
            chkTieneFechaVencimiento.Text = "Fecha Vencimiento:";
            chkTieneFechaVencimiento.Location = new Point(315, 65);
            chkTieneFechaVencimiento.Size = new Size(130, 20);
            chkTieneFechaVencimiento.Checked = true;

            dtpFechaVencimiento = new DateTimePicker();
            dtpFechaVencimiento.Location = new Point(315, 88);
            dtpFechaVencimiento.Size = new Size(150, 23);
            dtpFechaVencimiento.Format = DateTimePickerFormat.Short;

            lblEstado = new Label();
            lblEstado.Text = "Estado:";
            lblEstado.Location = new Point(15, 100);
            lblEstado.Size = new Size(50, 20);

            cmbEstado = new ComboBox();
            cmbEstado.Location = new Point(145, 98);
            cmbEstado.Size = new Size(150, 23);
            cmbEstado.DropDownStyle = ComboBoxStyle.DropDownList;

            grpDatosFactura.Controls.AddRange(new Control[] {
                lblNumeroFactura, txtNumeroFactura, lblPersonaId, txtPersonaId,
                lblFechaEmision, dtpFechaEmision, chkTieneFechaVencimiento, dtpFechaVencimiento,
                lblEstado, cmbEstado
            });

            // GroupBox - Montos
            grpMontos = new GroupBox();
            grpMontos.Text = "Montos";
            grpMontos.Location = new Point(580, 10);
            grpMontos.Size = new Size(280, 120);

            lblImpuestos = new Label();
            lblImpuestos.Text = "Impuestos:";
            lblImpuestos.Location = new Point(15, 30);
            lblImpuestos.Size = new Size(70, 20);

            nudImpuestos = new NumericUpDown();
            nudImpuestos.Location = new Point(95, 28);
            nudImpuestos.Size = new Size(120, 23);
            nudImpuestos.DecimalPlaces = 2;
            nudImpuestos.Maximum = 999999;

            lblDescuentos = new Label();
            lblDescuentos.Text = "Descuentos:";
            lblDescuentos.Location = new Point(15, 65);
            lblDescuentos.Size = new Size(70, 20);

            nudDescuentos = new NumericUpDown();
            nudDescuentos.Location = new Point(95, 63);
            nudDescuentos.Size = new Size(120, 23);
            nudDescuentos.DecimalPlaces = 2;
            nudDescuentos.Maximum = 999999;

            chkFinalizar = new CheckBox();
            chkFinalizar.Text = "Finalizar Factura";
            chkFinalizar.Location = new Point(15, 90);
            chkFinalizar.Size = new Size(120, 20);

            grpMontos.Controls.AddRange(new Control[] {
                lblImpuestos, nudImpuestos, lblDescuentos, nudDescuentos, chkFinalizar
            });

            // GroupBox - Detalles
            grpDetalles = new GroupBox();
            grpDetalles.Text = "Detalles";
            grpDetalles.Location = new Point(10, 200);
            grpDetalles.Size = new Size(850, 250);

            lblNotas = new Label();
            lblNotas.Text = "Notas:";
            lblNotas.Location = new Point(15, 30);
            lblNotas.Size = new Size(50, 20);

            txtNotas = new TextBox();
            txtNotas.Location = new Point(15, 53);
            txtNotas.Size = new Size(820, 60);
            txtNotas.Multiline = true;
            txtNotas.ScrollBars = ScrollBars.Vertical;

            lblProductos = new Label();
            lblProductos.Text = "Productos (JSON):";
            lblProductos.Location = new Point(15, 125);
            lblProductos.Size = new Size(120, 20);

            txtProductosJson = new TextBox();
            txtProductosJson.Location = new Point(15, 148);
            txtProductosJson.Size = new Size(400, 80);
            txtProductosJson.Multiline = true;
            txtProductosJson.ScrollBars = ScrollBars.Vertical;
            txtProductosJson.Font = new Font("Consolas", 9);

            lblServicios = new Label();
            lblServicios.Text = "Servicios (JSON):";
            lblServicios.Location = new Point(435, 125);
            lblServicios.Size = new Size(120, 20);

            txtServiciosJson = new TextBox();
            txtServiciosJson.Location = new Point(435, 148);
            txtServiciosJson.Size = new Size(400, 80);
            txtServiciosJson.Multiline = true;
            txtServiciosJson.ScrollBars = ScrollBars.Vertical;
            txtServiciosJson.Font = new Font("Consolas", 9);

            grpDetalles.Controls.AddRange(new Control[] {
                lblNotas, txtNotas, lblProductos, txtProductosJson, lblServicios, txtServiciosJson
            });

            panelContenido.Controls.AddRange(new Control[] {
                grpDatosFactura, grpMontos, grpDetalles
            });

            tabMantenimiento.Controls.Add(panelContenido);
            tabMantenimiento.Controls.Add(panelBotones);
        }

        #endregion
    }
}