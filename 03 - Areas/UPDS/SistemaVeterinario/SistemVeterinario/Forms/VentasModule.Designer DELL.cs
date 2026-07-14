using System.Drawing;
using System.Windows.Forms;

namespace SistemVeterinario.Forms
{
    partial class VentasModule
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        // Controles específicos de ventas (los controles base como tabInicio, tabConfiguraciones, etc. se heredan de BaseModulos)
        private Label lblBuscarPersona;
        private TextBox txtBuscarPersonaId;
        private Button btnRefrescar;
        private Label lblTotalRegistros;

        // Controles específicos para el formulario de ventas
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

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private new void InitializeComponent()
        {
            components = new System.ComponentModel.Container();

            // Inicializar controles específicos de ventas
            InitializarControlesVentas();

            // Configurar las pestañas heredadas de BaseModulos
            ConfigurarPestanas();

            // Configurar panel de búsqueda
            ConfigurarPanelBusqueda();

            // Configurar formulario de ventas
            ConfigurarFormularioVentas();
        }

        private void ConfigurarPestanas()
        {
            // Configurar textos de las pestañas heredadas del BaseModulos
            tabInicio.Text = "Gestión de Ventas";
            tabConfiguraciones.Text = "Configuración de Ventas";
        }

        private void InitializarControlesVentas()
        {
            // Controles específicos de ventas para el panel de búsqueda
            lblBuscarPersona = new Label();
            txtBuscarPersonaId = new TextBox();
            btnRefrescar = new Button();
            lblTotalRegistros = new Label();

            // Inicializar controles del formulario de ventas
            InitializarControlesFormulario();
        }

        private void InitializarControlesFormulario()
        {
            // GroupBox - Datos de la Factura
            grpDatosFactura = new GroupBox();
            lblNumeroFactura = new Label();
            txtNumeroFactura = new TextBox();
            lblPersonaId = new Label();
            txtPersonaId = new TextBox();
            lblFechaEmision = new Label();
            dtpFechaEmision = new DateTimePicker();
            lblFechaVencimiento = new Label();
            dtpFechaVencimiento = new DateTimePicker();
            chkTieneFechaVencimiento = new CheckBox();
            lblEstado = new Label();
            cmbEstado = new ComboBox();

            // GroupBox - Montos
            grpMontos = new GroupBox();
            lblImpuestos = new Label();
            nudImpuestos = new NumericUpDown();
            lblDescuentos = new Label();
            nudDescuentos = new NumericUpDown();
            chkFinalizar = new CheckBox();

            // GroupBox - Detalles
            grpDetalles = new GroupBox();
            lblNotas = new Label();
            txtNotas = new TextBox();
            lblProductos = new Label();
            txtProductosJson = new TextBox();
            lblServicios = new Label();
            txtServiciosJson = new TextBox();
        }

        private void ConfigurarPanelBusqueda()
        {
            // Configurar controles adicionales en el panel de búsqueda heredado
            // El panelBusqueda ya existe en BaseModulos

            lblBuscarPersona.Text = "ID Persona:";
            lblBuscarPersona.Location = new Point(180, 25);
            lblBuscarPersona.Size = new Size(80, 20);
            lblBuscarPersona.AutoSize = true;

            txtBuscarPersonaId.Location = new Point(270, 22);
            txtBuscarPersonaId.Size = new Size(100, 23);
            txtBuscarPersonaId.PlaceholderText = "ID de persona";

            btnRefrescar.Text = "Refrescar";
            btnRefrescar.Location = new Point(380, 21);
            btnRefrescar.Size = new Size(75, 25);
            btnRefrescar.BackColor = Color.LightGreen;

            lblTotalRegistros.Text = "Total registros: 0";
            lblTotalRegistros.Location = new Point(470, 25);
            lblTotalRegistros.Size = new Size(150, 20);
            lblTotalRegistros.ForeColor = Color.DarkBlue;
            lblTotalRegistros.AutoSize = true;

            // Agregar controles al panel de búsqueda heredado
            panelBusqueda.Controls.AddRange(new Control[] {
                lblBuscarPersona, txtBuscarPersonaId, btnRefrescar, lblTotalRegistros
            });

            // Configurar el txtBuscar heredado
            txtBuscar.PlaceholderText = "Buscar por número de factura, cliente, estado...";
        }

        private void ConfigurarFormularioVentas()
        {
            // Configurar GroupBox - Datos de la Factura
            grpDatosFactura.Text = "Datos de la Factura";
            grpDatosFactura.Location = new Point(15, 100);
            grpDatosFactura.Size = new Size(560, 180);
            grpDatosFactura.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            grpDatosFactura.ForeColor = Color.DarkBlue;

            lblNumeroFactura.Text = "Número de Factura *:";
            lblNumeroFactura.Location = new Point(15, 30);
            lblNumeroFactura.Size = new Size(130, 20);
            lblNumeroFactura.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            lblNumeroFactura.ForeColor = Color.DarkRed;

            txtNumeroFactura.Location = new Point(150, 28);
            txtNumeroFactura.Size = new Size(150, 23);
            txtNumeroFactura.MaxLength = 50;

            lblPersonaId.Text = "ID Persona *:";
            lblPersonaId.Location = new Point(315, 30);
            lblPersonaId.Size = new Size(80, 20);
            lblPersonaId.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            lblPersonaId.ForeColor = Color.DarkRed;

            txtPersonaId.Location = new Point(405, 28);
            txtPersonaId.Size = new Size(100, 23);

            lblFechaEmision.Text = "Fecha Emisión:";
            lblFechaEmision.Location = new Point(15, 65);
            lblFechaEmision.Size = new Size(100, 20);
            lblFechaEmision.AutoSize = true;

            dtpFechaEmision.Location = new Point(150, 63);
            dtpFechaEmision.Size = new Size(150, 23);
            dtpFechaEmision.Format = DateTimePickerFormat.Short;

            chkTieneFechaVencimiento.Text = "Fecha Vencimiento:";
            chkTieneFechaVencimiento.Location = new Point(315, 65);
            chkTieneFechaVencimiento.Size = new Size(130, 20);
            chkTieneFechaVencimiento.Checked = true;

            dtpFechaVencimiento.Location = new Point(315, 88);
            dtpFechaVencimiento.Size = new Size(150, 23);
            dtpFechaVencimiento.Format = DateTimePickerFormat.Short;

            lblEstado.Text = "Estado:";
            lblEstado.Location = new Point(15, 100);
            lblEstado.Size = new Size(50, 20);
            lblEstado.AutoSize = true;

            cmbEstado.Location = new Point(150, 98);
            cmbEstado.Size = new Size(150, 23);
            cmbEstado.DropDownStyle = ComboBoxStyle.DropDownList;

            grpDatosFactura.Controls.AddRange(new Control[] {
                lblNumeroFactura, txtNumeroFactura, lblPersonaId, txtPersonaId,
                lblFechaEmision, dtpFechaEmision, chkTieneFechaVencimiento, dtpFechaVencimiento,
                lblEstado, cmbEstado
            });

            // Configurar GroupBox - Montos
            grpMontos.Text = "Montos";
            grpMontos.Location = new Point(580, 100);
            grpMontos.Size = new Size(280, 120);
            grpMontos.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            grpMontos.ForeColor = Color.DarkGreen;

            lblImpuestos.Text = "Impuestos:";
            lblImpuestos.Location = new Point(15, 30);
            lblImpuestos.Size = new Size(70, 20);
            lblImpuestos.AutoSize = true;

            nudImpuestos.Location = new Point(95, 28);
            nudImpuestos.Size = new Size(120, 23);
            nudImpuestos.DecimalPlaces = 2;
            nudImpuestos.Maximum = 999999;

            lblDescuentos.Text = "Descuentos:";
            lblDescuentos.Location = new Point(15, 65);
            lblDescuentos.Size = new Size(70, 20);
            lblDescuentos.AutoSize = true;

            nudDescuentos.Location = new Point(95, 63);
            nudDescuentos.Size = new Size(120, 23);
            nudDescuentos.DecimalPlaces = 2;
            nudDescuentos.Maximum = 999999;

            chkFinalizar.Text = "Finalizar Factura";
            chkFinalizar.Location = new Point(15, 90);
            chkFinalizar.Size = new Size(120, 20);

            grpMontos.Controls.AddRange(new Control[] {
                lblImpuestos, nudImpuestos, lblDescuentos, nudDescuentos, chkFinalizar
            });

            // Configurar GroupBox - Detalles
            grpDetalles.Text = "Detalles";
            grpDetalles.Location = new Point(15, 290);
            grpDetalles.Size = new Size(845, 180);
            grpDetalles.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            grpDetalles.ForeColor = Color.DarkSlateGray;

            lblNotas.Text = "Notas:";
            lblNotas.Location = new Point(15, 30);
            lblNotas.Size = new Size(50, 20);
            lblNotas.AutoSize = true;

            txtNotas.Location = new Point(15, 53);
            txtNotas.Size = new Size(820, 40);
            txtNotas.Multiline = true;
            txtNotas.ScrollBars = ScrollBars.Vertical;

            lblProductos.Text = "Productos (JSON):";
            lblProductos.Location = new Point(15, 105);
            lblProductos.Size = new Size(120, 20);
            lblProductos.AutoSize = true;

            txtProductosJson.Location = new Point(15, 128);
            txtProductosJson.Size = new Size(400, 40);
            txtProductosJson.Multiline = true;
            txtProductosJson.ScrollBars = ScrollBars.Vertical;
            txtProductosJson.Font = new Font("Consolas", 9);

            lblServicios.Text = "Servicios (JSON):";
            lblServicios.Location = new Point(435, 105);
            lblServicios.Size = new Size(120, 20);
            lblServicios.AutoSize = true;

            txtServiciosJson.Location = new Point(435, 128);
            txtServiciosJson.Size = new Size(400, 40);
            txtServiciosJson.Multiline = true;
            txtServiciosJson.ScrollBars = ScrollBars.Vertical;
            txtServiciosJson.Font = new Font("Consolas", 9);

            grpDetalles.Controls.AddRange(new Control[] {
                lblNotas, txtNotas, lblProductos, txtProductosJson, lblServicios, txtServiciosJson
            });

            // Agregar los grupos al panel de formulario heredado de BaseModulos
            panelFormulario.Controls.AddRange(new Control[] {
                grpDatosFactura, grpMontos, grpDetalles
            });
        }

        #endregion
    }
}