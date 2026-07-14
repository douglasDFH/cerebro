using System.Windows.Forms;
using System.Drawing;
namespace SistemVeterinario.Reportes
{
    partial class ReportesAvanzados
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
            panelTop = new Panel();
            lblTitulo = new Label();
            panelControles = new Panel();
            groupBox1 = new GroupBox();
            cmbRangoFechas = new ComboBox();
            label1 = new Label();
            dtpFechaFin = new DateTimePicker();
            dtpFechaInicio = new DateTimePicker();
            label3 = new Label();
            label2 = new Label();
            groupBox2 = new GroupBox();
            cmbTipoReporte = new ComboBox();
            label5 = new Label();
            cmbPeriodo = new ComboBox();
            label4 = new Label();
            btnGenerar = new Button();
            panelStatus = new Panel();
            lblResultados = new Label();
            panelReporte = new Panel();
            panelTop.SuspendLayout();
            panelControles.SuspendLayout();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            panelStatus.SuspendLayout();
            panelReporte.SuspendLayout();
            SuspendLayout();
            // 
            // panelTop
            // 
            panelTop.BackColor = Color.FromArgb(31, 30, 68);
            panelTop.Controls.Add(lblTitulo);
            panelTop.Dock = DockStyle.Top;
            panelTop.Location = new Point(0, 0);
            panelTop.Margin = new Padding(4);
            panelTop.Name = "panelTop";
            panelTop.Size = new Size(1400, 69);
            panelTop.TabIndex = 0;
            // 
            // lblTitulo
            // 
            lblTitulo.Anchor = AnchorStyles.None;
            lblTitulo.AutoSize = true;
            lblTitulo.Font = new Font("Microsoft Sans Serif", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTitulo.ForeColor = Color.White;
            lblTitulo.Location = new Point(525, 17);
            lblTitulo.Margin = new Padding(4, 0, 4, 0);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(314, 29);
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "REPORTES AVANZADOS";
            lblTitulo.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // panelControles
            // 
            panelControles.BackColor = Color.WhiteSmoke;
            panelControles.Controls.Add(groupBox1);
            panelControles.Controls.Add(groupBox2);
            panelControles.Controls.Add(btnGenerar);
            panelControles.Dock = DockStyle.Top;
            panelControles.Location = new Point(0, 69);
            panelControles.Margin = new Padding(4);
            panelControles.Name = "panelControles";
            panelControles.Size = new Size(1400, 112);
            panelControles.TabIndex = 1;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(cmbRangoFechas);
            groupBox1.Controls.Add(label1);
            groupBox1.Controls.Add(dtpFechaFin);
            groupBox1.Controls.Add(dtpFechaInicio);
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(label2);
            groupBox1.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            groupBox1.Location = new Point(14, 7);
            groupBox1.Margin = new Padding(4);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new Padding(4);
            groupBox1.Size = new Size(409, 102);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "Rango de Fechas";
            // 
            // cmbRangoFechas
            // 
            cmbRangoFechas.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbRangoFechas.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            cmbRangoFechas.FormattingEnabled = true;
            cmbRangoFechas.Location = new Point(116, 26);
            cmbRangoFechas.Margin = new Padding(4);
            cmbRangoFechas.Name = "cmbRangoFechas";
            cmbRangoFechas.Size = new Size(274, 21);
            cmbRangoFechas.TabIndex = 5;
            cmbRangoFechas.SelectedIndexChanged += cmbRangoFechas_SelectedIndexChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.Black;
            label1.Location = new Point(7, 28);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(48, 13);
            label1.TabIndex = 4;
            label1.Text = "Período:";
            // 
            // dtpFechaFin
            // 
            dtpFechaFin.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dtpFechaFin.Format = DateTimePickerFormat.Short;
            dtpFechaFin.Location = new Point(274, 69);
            dtpFechaFin.Margin = new Padding(4);
            dtpFechaFin.Name = "dtpFechaFin";
            dtpFechaFin.Size = new Size(116, 20);
            dtpFechaFin.TabIndex = 3;
            // 
            // dtpFechaInicio
            // 
            dtpFechaInicio.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dtpFechaInicio.Format = DateTimePickerFormat.Short;
            dtpFechaInicio.Location = new Point(116, 69);
            dtpFechaInicio.Margin = new Padding(4);
            dtpFechaInicio.Name = "dtpFechaInicio";
            dtpFechaInicio.Size = new Size(116, 20);
            dtpFechaInicio.TabIndex = 2;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label3.ForeColor = Color.Black;
            label3.Location = new Point(270, 51);
            label3.Margin = new Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.Size = new Size(57, 13);
            label3.TabIndex = 1;
            label3.Text = "Fecha Fin:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label2.ForeColor = Color.Black;
            label2.Location = new Point(113, 51);
            label2.Margin = new Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new Size(68, 13);
            label2.TabIndex = 0;
            label2.Text = "Fecha Inicio:";
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(cmbTipoReporte);
            groupBox2.Controls.Add(label5);
            groupBox2.Controls.Add(cmbPeriodo);
            groupBox2.Controls.Add(label4);
            groupBox2.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            groupBox2.Location = new Point(438, 7);
            groupBox2.Margin = new Padding(4);
            groupBox2.Name = "groupBox2";
            groupBox2.Padding = new Padding(4);
            groupBox2.Size = new Size(700, 102);
            groupBox2.TabIndex = 1;
            groupBox2.TabStop = false;
            groupBox2.Text = "Configuración del Reporte";
            // 
            // cmbTipoReporte
            // 
            cmbTipoReporte.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbTipoReporte.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            cmbTipoReporte.FormattingEnabled = true;
            cmbTipoReporte.Location = new Point(315, 44);
            cmbTipoReporte.Margin = new Padding(4);
            cmbTipoReporte.Name = "cmbTipoReporte";
            cmbTipoReporte.Size = new Size(274, 21);
            cmbTipoReporte.TabIndex = 3;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label5.ForeColor = Color.Black;
            label5.Location = new Point(315, 22);
            label5.Margin = new Padding(4, 0, 4, 0);
            label5.Name = "label5";
            label5.Size = new Size(72, 13);
            label5.TabIndex = 2;
            label5.Text = "Tipo Reporte:";
            // 
            // cmbPeriodo
            // 
            cmbPeriodo.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbPeriodo.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            cmbPeriodo.FormattingEnabled = true;
            cmbPeriodo.Location = new Point(8, 44);
            cmbPeriodo.Margin = new Padding(4);
            cmbPeriodo.Name = "cmbPeriodo";
            cmbPeriodo.Size = new Size(274, 21);
            cmbPeriodo.TabIndex = 1;
            cmbPeriodo.Items.AddRange(new string[] {
                "Diario",
                "Semanal",
                "Mensual",
                "Anual"
            });
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label4.ForeColor = Color.Black;
            label4.Location = new Point(8, 21);
            label4.Margin = new Padding(4, 0, 4, 0);
            label4.Name = "label4";
            label4.Size = new Size(66, 13);
            label4.TabIndex = 0;
            label4.Text = "Agrupar Por:";
            // 
            // btnGenerar
            // 
            btnGenerar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnGenerar.BackColor = Color.FromArgb(31, 30, 68);
            btnGenerar.FlatStyle = FlatStyle.Flat;
            btnGenerar.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnGenerar.ForeColor = Color.White;
            btnGenerar.Location = new Point(1224, 23);
            btnGenerar.Margin = new Padding(4);
            btnGenerar.Name = "btnGenerar";
            btnGenerar.Size = new Size(151, 81);
            btnGenerar.TabIndex = 3;
            btnGenerar.Text = "GENERAR REPORTE";
            btnGenerar.UseVisualStyleBackColor = false;
            btnGenerar.Click += btnGenerar_Click;
            // 
            // panelStatus
            // 
            panelStatus.BackColor = Color.FromArgb(31, 30, 68);
            panelStatus.Controls.Add(lblResultados);
            panelStatus.Dock = DockStyle.Bottom;
            panelStatus.Location = new Point(0, 750);
            panelStatus.Margin = new Padding(4);
            panelStatus.Name = "panelStatus";
            panelStatus.Size = new Size(1400, 34);
            panelStatus.TabIndex = 4;
            // 
            // lblResultados
            // 
            lblResultados.Anchor = AnchorStyles.Left;
            lblResultados.AutoSize = true;
            lblResultados.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblResultados.ForeColor = Color.White;
            lblResultados.Location = new Point(14, 9);
            lblResultados.Margin = new Padding(4, 0, 4, 0);
            lblResultados.Name = "lblResultados";
            lblResultados.Size = new Size(254, 15);
            lblResultados.TabIndex = 0;
            lblResultados.Text = "Listo para generar reportes avanzados";
            // 
            // ReportesAvanzados
            // 
            // 
            // panelReporte
            // 
            panelReporte.BackColor = Color.White;
            panelReporte.Dock = DockStyle.Fill;
            panelReporte.Location = new Point(0, 181);
            panelReporte.Margin = new Padding(4);
            panelReporte.Name = "panelReporte";
            panelReporte.Size = new Size(1400, 569);
            panelReporte.TabIndex = 5;
            // 
            // ReportesAvanzados
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(panelReporte);
            Controls.Add(panelStatus);
            Controls.Add(panelControles);
            Controls.Add(panelTop);
            Margin = new Padding(4);
            MinimumSize = new Size(1166, 692);
            Name = "ReportesAvanzados";
            Size = new Size(1400, 784);
            Load += FrmReportesAvanzados_Load;
            panelTop.ResumeLayout(false);
            panelTop.PerformLayout();
            panelControles.ResumeLayout(false);
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            panelStatus.ResumeLayout(false);
            panelStatus.PerformLayout();
            panelReporte.ResumeLayout(false);
            ResumeLayout(false);

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
        private System.Windows.Forms.Button btnGenerar;
        private System.Windows.Forms.Panel panelStatus;
        private System.Windows.Forms.Label lblResultados;
        private Panel panelReporte;
        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
    }
}