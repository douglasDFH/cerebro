using System.Drawing;

namespace SistemVeterinario
{
    partial class Producto
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
        private DataGridView dgvProductos;
        private Panel panelBusqueda;
        private Label lblBuscar;
        private TextBox txtBuscar;
        private Button btnBuscar;
        private Button btnRefrescar;
        private Button btnStockBajo;
        private Label lblTotalRegistros;

        // Controles del mantenimiento
        private Panel panelBotones;
        private Button btnNuevo;
        private Button btnGuardar;
        private Button btnEditar;
        private Button btnEliminar;
        private Button btnCancelar;

        // Controles datos producto
        private GroupBox grpDatosProducto;
        private Label lblCodigo;
        private TextBox txtCodigo;
        private Button btnGenerarCodigo;
        private Label lblNombre;
        private TextBox txtNombre;
        private Label lblCategoria;
        private ComboBox cmbCategoria;
        private Button btnNuevaCategoria;
        private Label lblPrecio;
        private NumericUpDown nudPrecio;
        private Label lblStockMinimo;
        private NumericUpDown nudStockMinimo;
        private Label lblStockActual;
        private NumericUpDown nudStockActual;
        private CheckBox chkRequiereReceta;
        private Label lblDescripcion;
        private TextBox txtDescripcion;

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
            this.Text = "Gestión de Productos";
            this.Size = new Size(1200, 750);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.MinimumSize = new Size(1000, 600);

            // TabControl principal
            tabControl = new TabControl();
            tabControl.Dock = DockStyle.Fill;

            // Pestaña de Listado
            tabListado = new TabPage("Listado de Productos");
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
            lblBuscar.Text = "Buscar Producto:";
            lblBuscar.Location = new Point(10, 20);
            lblBuscar.Size = new Size(100, 20);

            txtBuscar = new TextBox();
            txtBuscar.Location = new Point(120, 18);
            txtBuscar.Size = new Size(200, 23);
            txtBuscar.PlaceholderText = "Código, nombre, categoría...";

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

            btnStockBajo = new Button();
            btnStockBajo.Text = "Stock Bajo";
            btnStockBajo.Location = new Point(500, 17);
            btnStockBajo.Size = new Size(85, 25);
            btnStockBajo.BackColor = Color.LightSalmon;

            lblTotalRegistros = new Label();
            lblTotalRegistros.Text = "Total registros: 0";
            lblTotalRegistros.Location = new Point(620, 20);
            lblTotalRegistros.Size = new Size(150, 20);
            lblTotalRegistros.ForeColor = Color.DarkBlue;

            panelBusqueda.Controls.AddRange(new Control[] {
                lblBuscar, txtBuscar, btnBuscar, btnRefrescar, btnStockBajo, lblTotalRegistros
            });

            // DataGridView
            dgvProductos = new DataGridView();
            dgvProductos.Dock = DockStyle.Fill;
            dgvProductos.AllowUserToAddRows = false;
            dgvProductos.AllowUserToDeleteRows = false;
            dgvProductos.ReadOnly = true;
            dgvProductos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvProductos.MultiSelect = false;
            dgvProductos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            dgvProductos.AutoGenerateColumns = true;
            dgvProductos.BackgroundColor = Color.White;
            dgvProductos.AlternatingRowsDefaultCellStyle.BackColor = Color.AliceBlue;
            dgvProductos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvProductos.RowHeadersVisible = false;
            dgvProductos.BorderStyle = BorderStyle.FixedSingle;

            tabListado.Controls.Add(dgvProductos);
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

            // GroupBox - Datos del Producto
            grpDatosProducto = new GroupBox();
            grpDatosProducto.Text = "Datos del Producto";
            grpDatosProducto.Location = new Point(10, 10);
            grpDatosProducto.Size = new Size(900, 400);

            // Fila 1: Código y botón generar
            lblCodigo = new Label();
            lblCodigo.Text = "Código:";
            lblCodigo.Location = new Point(15, 30);
            lblCodigo.Size = new Size(60, 20);

            txtCodigo = new TextBox();
            txtCodigo.Location = new Point(80, 28);
            txtCodigo.Size = new Size(120, 23);

            btnGenerarCodigo = new Button();
            btnGenerarCodigo.Text = "Generar";
            btnGenerarCodigo.Location = new Point(210, 27);
            btnGenerarCodigo.Size = new Size(70, 25);
            btnGenerarCodigo.BackColor = Color.LightCyan;

            lblNombre = new Label();
            lblNombre.Text = "Nombre *:";
            lblNombre.Location = new Point(300, 30);
            lblNombre.Size = new Size(70, 20);

            txtNombre = new TextBox();
            txtNombre.Location = new Point(375, 28);
            txtNombre.Size = new Size(250, 23);

            // Fila 2: Categoría y precio
            lblCategoria = new Label();
            lblCategoria.Text = "Categoría *:";
            lblCategoria.Location = new Point(15, 65);
            lblCategoria.Size = new Size(80, 20);

            cmbCategoria = new ComboBox();
            cmbCategoria.Location = new Point(100, 63);
            cmbCategoria.Size = new Size(150, 23);
            cmbCategoria.DropDownStyle = ComboBoxStyle.DropDownList;

            btnNuevaCategoria = new Button();
            btnNuevaCategoria.Text = "Nueva";
            btnNuevaCategoria.Location = new Point(260, 62);
            btnNuevaCategoria.Size = new Size(60, 25);
            btnNuevaCategoria.BackColor = Color.LightGreen;

            lblPrecio = new Label();
            lblPrecio.Text = "Precio *:";
            lblPrecio.Location = new Point(340, 65);
            lblPrecio.Size = new Size(60, 20);

            nudPrecio = new NumericUpDown();
            nudPrecio.Location = new Point(405, 63);
            nudPrecio.Size = new Size(100, 23);
            nudPrecio.DecimalPlaces = 2;
            nudPrecio.Maximum = 999999.99m;
            nudPrecio.Minimum = 0.01m;
            nudPrecio.Value = 0.01m;

            chkRequiereReceta = new CheckBox();
            chkRequiereReceta.Text = "Requiere Receta";
            chkRequiereReceta.Location = new Point(520, 65);
            chkRequiereReceta.Size = new Size(120, 20);

            // Fila 3: Stock
            lblStockMinimo = new Label();
            lblStockMinimo.Text = "Stock Mínimo:";
            lblStockMinimo.Location = new Point(15, 100);
            lblStockMinimo.Size = new Size(85, 20);

            nudStockMinimo = new NumericUpDown();
            nudStockMinimo.Location = new Point(105, 98);
            nudStockMinimo.Size = new Size(80, 23);
            nudStockMinimo.Maximum = 99999;
            nudStockMinimo.Minimum = 0;
            nudStockMinimo.Value = 5;

            lblStockActual = new Label();
            lblStockActual.Text = "Stock Actual:";
            lblStockActual.Location = new Point(200, 100);
            lblStockActual.Size = new Size(80, 20);

            nudStockActual = new NumericUpDown();
            nudStockActual.Location = new Point(285, 98);
            nudStockActual.Size = new Size(80, 23);
            nudStockActual.Maximum = 99999;
            nudStockActual.Minimum = 0;
            nudStockActual.Value = 0;

            // Fila 4: Descripción
            lblDescripcion = new Label();
            lblDescripcion.Text = "Descripción:";
            lblDescripcion.Location = new Point(15, 135);
            lblDescripcion.Size = new Size(80, 20);

            txtDescripcion = new TextBox();
            txtDescripcion.Location = new Point(15, 160);
            txtDescripcion.Size = new Size(850, 80);
            txtDescripcion.Multiline = true;
            txtDescripcion.ScrollBars = ScrollBars.Vertical;

            grpDatosProducto.Controls.AddRange(new Control[] {
                lblCodigo, txtCodigo, btnGenerarCodigo, lblNombre, txtNombre,
                lblCategoria, cmbCategoria, btnNuevaCategoria, lblPrecio, nudPrecio, chkRequiereReceta,
                lblStockMinimo, nudStockMinimo, lblStockActual, nudStockActual,
                lblDescripcion, txtDescripcion
            });

            panelContenido.Controls.Add(grpDatosProducto);

            tabMantenimiento.Controls.Add(panelContenido);
            tabMantenimiento.Controls.Add(panelBotones);
        }

        #endregion
    }
}