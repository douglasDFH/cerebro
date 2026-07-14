namespace PedidosApp.Reportes
{
    partial class FrmReportesAvanzados
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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
            this.panelTop = new System.Windows.Forms.Panel();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.panelControles = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cmbRangoFechas = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dtpFechaFin = new System.Windows.Forms.DateTimePicker();
            this.dtpFechaInicio = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cmbTipoReporte = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cmbPeriodo = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnLimpiar = new System.Windows.Forms.Button();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.txtBuscar = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.btnGenerar = new System.Windows.Forms.Button();
            this.panelBotones = new System.Windows.Forms.Panel();
            this.btnVistaPrevia = new System.Windows.Forms.Button();
            this.btnImprimir = new System.Windows.Forms.Button();
            this.btnExportarWord = new System.Windows.Forms.Button();
            this.btnExportarExcel = new System.Windows.Forms.Button();
            this.btnExportarPDF = new System.Windows.Forms.Button();
            this.panelReporte = new System.Windows.Forms.Panel();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.panelStatus = new System.Windows.Forms.Panel();
            this.lblResultados = new System.Windows.Forms.Label();
            this.panelTop.SuspendLayout();
            this.panelControles.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.panelBotones.SuspendLayout();
            this.panelReporte.SuspendLayout();
            this.panelStatus.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelTop
            // 
            this.panelTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(30)))), ((int)(((byte)(68)))));
            this.panelTop.Controls.Add(this.lblTitulo);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(1200, 60);
            this.panelTop.TabIndex = 0;
            // 
            // lblTitulo
            // 
            this.lblTitulo.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitulo.ForeColor = System.Drawing.Color.White;
            this.lblTitulo.Location = new System.Drawing.Point(450, 15);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(300, 29);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "REPORTES AVANZADOS";
            this.lblTitulo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panelControles
            // 
            this.panelControles.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panelControles.Controls.Add(this.groupBox1);
            this.panelControles.Controls.Add(this.groupBox2);
            this.panelControles.Controls.Add(this.groupBox3);
            this.panelControles.Controls.Add(this.btnGenerar);
            this.panelControles.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControles.Location = new System.Drawing.Point(0, 60);
            this.panelControles.Name = "panelControles";
            this.panelControles.Size = new System.Drawing.Size(1200, 120);
            this.panelControles.TabIndex = 1;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cmbRangoFechas);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.dtpFechaFin);
            this.groupBox1.Controls.Add(this.dtpFechaInicio);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(12, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(350, 108);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Rango de Fechas";
            // 
            // cmbRangoFechas
            // 
            this.cmbRangoFechas.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRangoFechas.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbRangoFechas.FormattingEnabled = true;
            this.cmbRangoFechas.Location = new System.Drawing.Point(100, 22);
            this.cmbRangoFechas.Name = "cmbRangoFechas";
            this.cmbRangoFechas.Size = new System.Drawing.Size(235, 21);
            this.cmbRangoFechas.TabIndex = 5;
            this.cmbRangoFechas.SelectedIndexChanged += new System.EventHandler(this.cmbRangoFechas_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(6, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Período:";
            // 
            // dtpFechaFin
            // 
            this.dtpFechaFin.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpFechaFin.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFechaFin.Location = new System.Drawing.Point(235, 75);
            this.dtpFechaFin.Name = "dtpFechaFin";
            this.dtpFechaFin.Size = new System.Drawing.Size(100, 20);
            this.dtpFechaFin.TabIndex = 3;
            // 
            // dtpFechaInicio
            // 
            this.dtpFechaInicio.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpFechaInicio.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFechaInicio.Location = new System.Drawing.Point(100, 75);
            this.dtpFechaInicio.Name = "dtpFechaInicio";
            this.dtpFechaInicio.Size = new System.Drawing.Size(100, 20);
            this.dtpFechaInicio.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(232, 59);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(57, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "Fecha Fin:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(97, 59);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(68, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Fecha Inicio:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cmbTipoReporte);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.cmbPeriodo);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(375, 6);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(350, 108);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Configuración del Reporte";
            // 
            // cmbTipoReporte
            // 
            this.cmbTipoReporte.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTipoReporte.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbTipoReporte.FormattingEnabled = true;
            this.cmbTipoReporte.Location = new System.Drawing.Point(100, 75);
            this.cmbTipoReporte.Name = "cmbTipoReporte";
            this.cmbTipoReporte.Size = new System.Drawing.Size(235, 21);
            this.cmbTipoReporte.TabIndex = 3;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(6, 78);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(72, 13);
            this.label5.TabIndex = 2;
            this.label5.Text = "Tipo Reporte:";
            // 
            // cmbPeriodo
            // 
            this.cmbPeriodo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPeriodo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbPeriodo.FormattingEnabled = true;
            this.cmbPeriodo.Location = new System.Drawing.Point(100, 35);
            this.cmbPeriodo.Name = "cmbPeriodo";
            this.cmbPeriodo.Size = new System.Drawing.Size(235, 21);
            this.cmbPeriodo.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(6, 38);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(88, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Agrupar Por:";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btnLimpiar);
            this.groupBox3.Controls.Add(this.btnBuscar);
            this.groupBox3.Controls.Add(this.txtBuscar);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.Location = new System.Drawing.Point(738, 6);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(300, 108);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Búsqueda y Filtros";
            // 
            // btnLimpiar
            // 
            this.btnLimpiar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.btnLimpiar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLimpiar.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLimpiar.ForeColor = System.Drawing.Color.White;
            this.btnLimpiar.Location = new System.Drawing.Point(155, 68);
            this.btnLimpiar.Name = "btnLimpiar";
            this.btnLimpiar.Size = new System.Drawing.Size(70, 25);
            this.btnLimpiar.TabIndex = 3;
            this.btnLimpiar.Text = "Limpiar";
            this.btnLimpiar.UseVisualStyleBackColor = false;
            this.btnLimpiar.Click += new System.EventHandler(this.btnLimpiar_Click);
            // 
            // btnBuscar
            // 
            this.btnBuscar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.btnBuscar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBuscar.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBuscar.ForeColor = System.Drawing.Color.Black;
            this.btnBuscar.Location = new System.Drawing.Point(75, 68);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(70, 25);
            this.btnBuscar.TabIndex = 2;
            this.btnBuscar.Text = "Buscar";
            this.btnBuscar.UseVisualStyleBackColor = false;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // txtBuscar
            // 
            this.txtBuscar.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBuscar.Location = new System.Drawing.Point(75, 35);
            this.txtBuscar.Name = "txtBuscar";
            this.txtBuscar.Size = new System.Drawing.Size(200, 20);
            this.txtBuscar.TabIndex = 1;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(6, 38);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(43, 13);
            this.label6.TabIndex = 0;
            this.label6.Text = "Buscar:";
            // 
            // btnGenerar
            // 
            this.btnGenerar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGenerar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(30)))), ((int)(((byte)(68)))));
            this.btnGenerar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGenerar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGenerar.ForeColor = System.Drawing.Color.White;
            this.btnGenerar.Location = new System.Drawing.Point(1050, 25);
            this.btnGenerar.Name = "btnGenerar";
            this.btnGenerar.Size = new System.Drawing.Size(130, 70);
            this.btnGenerar.TabIndex = 3;
            this.btnGenerar.Text = "GENERAR REPORTE";
            this.btnGenerar.UseVisualStyleBackColor = false;
            this.btnGenerar.Click += new System.EventHandler(this.btnGenerar_Click);
            // 
            // panelBotones
            // 
            this.panelBotones.BackColor = System.Drawing.Color.LightGray;
            this.panelBotones.Controls.Add(this.btnVistaPrevia);
            this.panelBotones.Controls.Add(this.btnImprimir);
            this.panelBotones.Controls.Add(this.btnExportarWord);
            this.panelBotones.Controls.Add(this.btnExportarExcel);
            this.panelBotones.Controls.Add(this.btnExportarPDF);
            this.panelBotones.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelBotones.Location = new System.Drawing.Point(0, 180);
            this.panelBotones.Name = "panelBotones";
            this.panelBotones.Size = new System.Drawing.Size(1200, 50);
            this.panelBotones.TabIndex = 2;
            // 
            // btnVistaPrevia
            // 
            this.btnVistaPrevia.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnVistaPrevia.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.btnVistaPrevia.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnVistaPrevia.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnVistaPrevia.Location = new System.Drawing.Point(650, 12);
            this.btnVistaPrevia.Name = "btnVistaPrevia";
            this.btnVistaPrevia.Size = new System.Drawing.Size(110, 26);
            this.btnVistaPrevia.TabIndex = 4;
            this.btnVistaPrevia.Text = "Vista Completa";
            this.btnVistaPrevia.UseVisualStyleBackColor = false;
            this.btnVistaPrevia.Click += new System.EventHandler(this.btnVistaPrevia_Click);
            // 
            // btnImprimir
            // 
            this.btnImprimir.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnImprimir.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.btnImprimir.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnImprimir.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnImprimir.ForeColor = System.Drawing.Color.White;
            this.btnImprimir.Location = new System.Drawing.Point(520, 12);
            this.btnImprimir.Name = "btnImprimir";
            this.btnImprimir.Size = new System.Drawing.Size(110, 26);
            this.btnImprimir.TabIndex = 3;
            this.btnImprimir.Text = "Imprimir";
            this.btnImprimir.UseVisualStyleBackColor = false;
            this.btnImprimir.Click += new System.EventHandler(this.btnImprimir_Click);
            // 
            // btnExportarWord
            // 
            this.btnExportarWord.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnExportarWord.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.btnExportarWord.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExportarWord.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExportarWord.ForeColor = System.Drawing.Color.White;
            this.btnExportarWord.Location = new System.Drawing.Point(390, 12);
            this.btnExportarWord.Name = "btnExportarWord";
            this.btnExportarWord.Size = new System.Drawing.Size(110, 26);
            this.btnExportarWord.TabIndex = 2;
            this.btnExportarWord.Text = "Exportar Word";
            this.btnExportarWord.UseVisualStyleBackColor = false;
            this.btnExportarWord.Click += new System.EventHandler(this.btnExportarWord_Click);
            // 
            // btnExportarExcel
            // 
            this.btnExportarExcel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnExportarExcel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.btnExportarExcel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExportarExcel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExportarExcel.ForeColor = System.Drawing.Color.Black;
            this.btnExportarExcel.Location = new System.Drawing.Point(260, 12);
            this.btnExportarExcel.Name = "btnExportarExcel";
            this.btnExportarExcel.Size = new System.Drawing.Size(110, 26);
            this.btnExportarExcel.TabIndex = 1;
            this.btnExportarExcel.Text = "Exportar Excel";
            this.btnExportarExcel.UseVisualStyleBackColor = false;
            this.btnExportarExcel.Click += new System.EventHandler(this.btnExportarExcel_Click);
            // 
            // btnExportarPDF
            // 
            this.btnExportarPDF.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnExportarPDF.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.btnExportarPDF.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExportarPDF.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExportarPDF.ForeColor = System.Drawing.Color.White;
            this.btnExportarPDF.Location = new System.Drawing.Point(130, 12);
            this.btnExportarPDF.Name = "btnExportarPDF";
            this.btnExportarPDF.Size = new System.Drawing.Size(110, 26);
            this.btnExportarPDF.TabIndex = 0;
            this.btnExportarPDF.Text = "Exportar PDF";
            this.btnExportarPDF.UseVisualStyleBackColor = false;
            this.btnExportarPDF.Click += new System.EventHandler(this.btnExportarPDF_Click);
            // 
            // panelReporte
            // 
            this.panelReporte.Controls.Add(this.reportViewer1);
            this.panelReporte.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelReporte.Location = new System.Drawing.Point(0, 230);
            this.panelReporte.Name = "panelReporte";
            this.panelReporte.Size = new System.Drawing.Size(1200, 420);
            this.panelReporte.TabIndex = 3;
            // 
            // reportViewer1
            // 
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.reportViewer1.Location = new System.Drawing.Point(0, 0);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.ServerReport.BearerToken = null;
            this.reportViewer1.Size = new System.Drawing.Size(1200, 420);
            this.reportViewer1.TabIndex = 0;
            // 
            // panelStatus
            // 
            this.panelStatus.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(30)))), ((int)(((byte)(68)))));
            this.panelStatus.Controls.Add(this.lblResultados);
            this.panelStatus.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelStatus.Location = new System.Drawing.Point(0, 650);
            this.panelStatus.Name = "panelStatus";
            this.panelStatus.Size = new System.Drawing.Size(1200, 30);
            this.panelStatus.TabIndex = 4;
            // 
            // lblResultados
            // 
            this.lblResultados.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblResultados.AutoSize = true;
            this.lblResultados.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblResultados.ForeColor = System.Drawing.Color.White;
            this.lblResultados.Location = new System.Drawing.Point(12, 8);
            this.lblResultados.Name = "lblResultados";
            this.lblResultados.Size = new System.Drawing.Size(280, 15);
            this.lblResultados.TabIndex = 0;
            this.lblResultados.Text = "Listo para generar reportes avanzados";
            // 
            // FrmReportesAvanzados
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1200, 680);
            this.Controls.Add(this.panelReporte);
            this.Controls.Add(this.panelStatus);
            this.Controls.Add(this.panelBotones);
            this.Controls.Add(this.panelControles);
            this.Controls.Add(this.panelTop);
            this.MinimumSize = new System.Drawing.Size(1000, 600);
            this.Name = "FrmReportesAvanzados";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Reportes Avanzados - Sistema de Pedidos";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FrmReportesAvanzados_Load);
            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
            this.panelControles.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.panelBotones.ResumeLayout(false);
            this.panelReporte.ResumeLayout(false);
            this.panelStatus.ResumeLayout(false);
            this.panelStatus.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Panel panelControles;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cmbRangoFechas;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpFechaFin;
        private System.Windows.Forms.DateTimePicker dtpFechaInicio;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox cmbTipoReporte;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cmbPeriodo;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnLimpiar;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.TextBox txtBuscar;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnGenerar;
        private System.Windows.Forms.Panel panelBotones;
        private System.Windows.Forms.Button btnVistaPrevia;
        private System.Windows.Forms.Button btnImprimir;
        private System.Windows.Forms.Button btnExportarWord;
        private System.Windows.Forms.Button btnExportarExcel;
        private System.Windows.Forms.Button btnExportarPDF;
        private System.Windows.Forms.Panel panelReporte;
        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.Panel panelStatus;
        private System.Windows.Forms.Label lblResultados;
    }
}