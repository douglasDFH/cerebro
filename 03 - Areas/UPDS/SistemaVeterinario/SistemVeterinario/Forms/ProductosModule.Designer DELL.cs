namespace SistemVeterinario.Forms
{
    partial class ProductosModule
    {
        /// <summary> 
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        // Controles específicos de productos
        private Label lblContador;
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
        private Button btnStockBajo;

        /// <summary> 
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de componentes

        /// <summary> 
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private new void InitializeComponent()
        {
            // Controles específicos de productos
            lblContador = new Label();
            grpDatosProducto = new GroupBox();
            lblCodigo = new Label();
            txtCodigo = new TextBox();
            btnGenerarCodigo = new Button();
            lblNombre = new Label();
            txtNombre = new TextBox();
            lblCategoria = new Label();
            cmbCategoria = new ComboBox();
            btnNuevaCategoria = new Button();
            lblPrecio = new Label();
            nudPrecio = new NumericUpDown();
            lblStockMinimo = new Label();
            nudStockMinimo = new NumericUpDown();
            lblStockActual = new Label();
            nudStockActual = new NumericUpDown();
            chkRequiereReceta = new CheckBox();
            lblDescripcion = new Label();
            txtDescripcion = new TextBox();
            btnStockBajo = new Button();

            // Suspender layout
            tabInicio.SuspendLayout();
            panelBusqueda.SuspendLayout();
            tabConfiguraciones.SuspendLayout();
            panelFormulario.SuspendLayout();
            panelSuperior.SuspendLayout();
            panelBotones.SuspendLayout();
            grpDatosProducto.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudPrecio).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudStockMinimo).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudStockActual).BeginInit();
            SuspendLayout();
            
            // 
            // tabInicio
            // 
            tabInicio.Text = "Gestión de Productos";
            
            // 
            // tabConfiguraciones
            // 
            tabConfiguraciones.Text = "Configuración de Producto";
            
            // 
            // panelBusqueda
            // 
            panelBusqueda.Controls.Add(lblContador);
            panelBusqueda.Controls.Add(btnStockBajo);
            panelBusqueda.Size = new Size(960, 80);
            
            // 
            // txtBuscar
            // 
            txtBuscar.PlaceholderText = "Buscar por código, nombre, categoría...";
            txtBuscar.Size = new Size(350, 23);
            
            // 
            // btnBuscar
            // 
            btnBuscar.Location = new Point(360, 17);
            
            // 
            // btnNuevo
            // 
            btnNuevo.Location = new Point(860, 17);
            
            // 
            // chkMostrarTodo
            // 
            chkMostrarTodo.Location = new Point(470, 20);
            
            // 
            // lblContador
            // 
            lblContador.AutoSize = true;
            lblContador.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            lblContador.ForeColor = Color.DarkBlue;
            lblContador.Location = new Point(620, 20);
            lblContador.Name = "lblContador";
            lblContador.Size = new Size(120, 15);
            lblContador.TabIndex = 0;
            lblContador.Text = "Total de registros: 0";
            
            // 
            // btnStockBajo
            // 
            btnStockBajo.BackColor = Color.LightSalmon;
            btnStockBajo.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            btnStockBajo.Location = new Point(500, 17);
            btnStockBajo.Name = "btnStockBajo";
            btnStockBajo.Size = new Size(85, 25);
            btnStockBajo.TabIndex = 1;
            btnStockBajo.Text = "Stock Bajo";
            btnStockBajo.UseVisualStyleBackColor = false;
            
            // 
            // panelFormulario
            // 
            panelFormulario.Controls.Add(grpDatosProducto);
            
            // 
            // panelSuperior
            // 
            panelSuperior.Size = new Size(992, 50);
            
            // 
            // grpDatosProducto
            // 
            grpDatosProducto.Controls.Add(lblCodigo);
            grpDatosProducto.Controls.Add(txtCodigo);
            grpDatosProducto.Controls.Add(btnGenerarCodigo);
            grpDatosProducto.Controls.Add(lblNombre);
            grpDatosProducto.Controls.Add(txtNombre);
            grpDatosProducto.Controls.Add(lblCategoria);
            grpDatosProducto.Controls.Add(cmbCategoria);
            grpDatosProducto.Controls.Add(btnNuevaCategoria);
            grpDatosProducto.Controls.Add(lblPrecio);
            grpDatosProducto.Controls.Add(nudPrecio);
            grpDatosProducto.Controls.Add(lblStockMinimo);
            grpDatosProducto.Controls.Add(nudStockMinimo);
            grpDatosProducto.Controls.Add(lblStockActual);
            grpDatosProducto.Controls.Add(nudStockActual);
            grpDatosProducto.Controls.Add(chkRequiereReceta);
            grpDatosProducto.Controls.Add(lblDescripcion);
            grpDatosProducto.Controls.Add(txtDescripcion);
            grpDatosProducto.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            grpDatosProducto.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            grpDatosProducto.Location = new Point(50, 20);
            grpDatosProducto.Name = "grpDatosProducto";
            grpDatosProducto.Size = new Size(900, 450);
            grpDatosProducto.TabIndex = 0;
            grpDatosProducto.TabStop = false;
            grpDatosProducto.Text = "📦 Datos del Producto";
            
            // 
            // lblCodigo
            // 
            lblCodigo.AutoSize = true;
            lblCodigo.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            lblCodigo.Location = new Point(30, 40);
            lblCodigo.Name = "lblCodigo";
            lblCodigo.Size = new Size(49, 15);
            lblCodigo.TabIndex = 0;
            lblCodigo.Text = "Código:";
            
            // 
            // txtCodigo
            // 
            txtCodigo.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            txtCodigo.Location = new Point(100, 38);
            txtCodigo.Name = "txtCodigo";
            txtCodigo.Size = new Size(140, 23);
            txtCodigo.TabIndex = 1;
            
            // 
            // btnGenerarCodigo
            // 
            btnGenerarCodigo.BackColor = Color.LightCyan;
            btnGenerarCodigo.Font = new Font("Segoe UI", 8F, FontStyle.Regular, GraphicsUnit.Point);
            btnGenerarCodigo.Location = new Point(250, 37);
            btnGenerarCodigo.Name = "btnGenerarCodigo";
            btnGenerarCodigo.Size = new Size(80, 25);
            btnGenerarCodigo.TabIndex = 2;
            btnGenerarCodigo.Text = "🎲 Generar";
            btnGenerarCodigo.UseVisualStyleBackColor = false;
            
            // 
            // lblNombre
            // 
            lblNombre.AutoSize = true;
            lblNombre.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            lblNombre.Location = new Point(360, 40);
            lblNombre.Name = "lblNombre";
            lblNombre.Size = new Size(60, 15);
            lblNombre.TabIndex = 3;
            lblNombre.Text = "Nombre *:";
            
            // 
            // txtNombre
            // 
            txtNombre.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            txtNombre.Location = new Point(430, 38);
            txtNombre.Name = "txtNombre";
            txtNombre.Size = new Size(300, 23);
            txtNombre.TabIndex = 4;
            
            // 
            // lblCategoria
            // 
            lblCategoria.AutoSize = true;
            lblCategoria.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            lblCategoria.Location = new Point(30, 80);
            lblCategoria.Name = "lblCategoria";
            lblCategoria.Size = new Size(69, 15);
            lblCategoria.TabIndex = 5;
            lblCategoria.Text = "Categoría *:";
            
            // 
            // cmbCategoria
            // 
            cmbCategoria.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbCategoria.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            cmbCategoria.FormattingEnabled = true;
            cmbCategoria.Location = new Point(110, 78);
            cmbCategoria.Name = "cmbCategoria";
            cmbCategoria.Size = new Size(180, 23);
            cmbCategoria.TabIndex = 6;
            
            // 
            // btnNuevaCategoria
            // 
            btnNuevaCategoria.BackColor = Color.LightGreen;
            btnNuevaCategoria.Font = new Font("Segoe UI", 8F, FontStyle.Regular, GraphicsUnit.Point);
            btnNuevaCategoria.Location = new Point(300, 77);
            btnNuevaCategoria.Name = "btnNuevaCategoria";
            btnNuevaCategoria.Size = new Size(80, 25);
            btnNuevaCategoria.TabIndex = 7;
            btnNuevaCategoria.Text = "➕ Nueva";
            btnNuevaCategoria.UseVisualStyleBackColor = false;
            
            // 
            // lblPrecio
            // 
            lblPrecio.AutoSize = true;
            lblPrecio.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            lblPrecio.Location = new Point(420, 80);
            lblPrecio.Name = "lblPrecio";
            lblPrecio.Size = new Size(49, 15);
            lblPrecio.TabIndex = 8;
            lblPrecio.Text = "Precio *:";
            
            // 
            // nudPrecio
            // 
            nudPrecio.DecimalPlaces = 2;
            nudPrecio.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            nudPrecio.Location = new Point(480, 78);
            nudPrecio.Maximum = new decimal(new int[] { 999999, 0, 0, 131072 });
            nudPrecio.Minimum = new decimal(new int[] { 1, 0, 0, 131072 });
            nudPrecio.Name = "nudPrecio";
            nudPrecio.Size = new Size(120, 23);
            nudPrecio.TabIndex = 9;
            nudPrecio.Value = new decimal(new int[] { 1, 0, 0, 131072 });
            
            // 
            // chkRequiereReceta
            // 
            chkRequiereReceta.AutoSize = true;
            chkRequiereReceta.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            chkRequiereReceta.Location = new Point(620, 80);
            chkRequiereReceta.Name = "chkRequiereReceta";
            chkRequiereReceta.Size = new Size(111, 19);
            chkRequiereReceta.TabIndex = 10;
            chkRequiereReceta.Text = "💊 Requiere Receta";
            chkRequiereReceta.UseVisualStyleBackColor = true;
            
            // 
            // lblStockMinimo
            // 
            lblStockMinimo.AutoSize = true;
            lblStockMinimo.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            lblStockMinimo.Location = new Point(30, 120);
            lblStockMinimo.Name = "lblStockMinimo";
            lblStockMinimo.Size = new Size(83, 15);
            lblStockMinimo.TabIndex = 11;
            lblStockMinimo.Text = "Stock Mínimo:";
            
            // 
            // nudStockMinimo
            // 
            nudStockMinimo.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            nudStockMinimo.Location = new Point(130, 118);
            nudStockMinimo.Maximum = new decimal(new int[] { 99999, 0, 0, 0 });
            nudStockMinimo.Name = "nudStockMinimo";
            nudStockMinimo.Size = new Size(100, 23);
            nudStockMinimo.TabIndex = 12;
            nudStockMinimo.Value = new decimal(new int[] { 5, 0, 0, 0 });
            
            // 
            // lblStockActual
            // 
            lblStockActual.AutoSize = true;
            lblStockActual.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            lblStockActual.Location = new Point(260, 120);
            lblStockActual.Name = "lblStockActual";
            lblStockActual.Size = new Size(76, 15);
            lblStockActual.TabIndex = 13;
            lblStockActual.Text = "Stock Actual:";
            
            // 
            // nudStockActual
            // 
            nudStockActual.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            nudStockActual.Location = new Point(350, 118);
            nudStockActual.Maximum = new decimal(new int[] { 99999, 0, 0, 0 });
            nudStockActual.Name = "nudStockActual";
            nudStockActual.Size = new Size(100, 23);
            nudStockActual.TabIndex = 14;
            
            // 
            // lblDescripcion
            // 
            lblDescripcion.AutoSize = true;
            lblDescripcion.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            lblDescripcion.Location = new Point(30, 160);
            lblDescripcion.Name = "lblDescripcion";
            lblDescripcion.Size = new Size(72, 15);
            lblDescripcion.TabIndex = 15;
            lblDescripcion.Text = "📝 Descripción:";
            
            // 
            // txtDescripcion
            // 
            txtDescripcion.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            txtDescripcion.Location = new Point(30, 185);
            txtDescripcion.Multiline = true;
            txtDescripcion.Name = "txtDescripcion";
            txtDescripcion.ScrollBars = ScrollBars.Vertical;
            txtDescripcion.Size = new Size(820, 120);
            txtDescripcion.TabIndex = 16;
            txtDescripcion.PlaceholderText = "Ingrese una descripción detallada del producto...";

            // Reanudar layout
            tabInicio.ResumeLayout(false);
            panelBusqueda.ResumeLayout(false);
            panelBusqueda.PerformLayout();
            tabConfiguraciones.ResumeLayout(false);
            panelFormulario.ResumeLayout(false);
            panelSuperior.ResumeLayout(false);
            panelBotones.ResumeLayout(false);
            grpDatosProducto.ResumeLayout(false);
            grpDatosProducto.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nudPrecio).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudStockMinimo).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudStockActual).EndInit();
            ResumeLayout(false);
        }

        #endregion
    }
}