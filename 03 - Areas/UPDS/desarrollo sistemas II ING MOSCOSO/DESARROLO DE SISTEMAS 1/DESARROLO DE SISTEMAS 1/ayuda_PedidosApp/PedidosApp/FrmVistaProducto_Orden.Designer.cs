namespace PedidosApp
{
    partial class FrmVistaProducto_Orden
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
            this.components = new System.ComponentModel.Container();
            this.dataListado = new System.Windows.Forms.DataGridView();
            this.btnBuscar = new FontAwesome.Sharp.IconButton();
            this.txtBuscar = new System.Windows.Forms.TextBox();
            this.lblTotal = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.ttMensaje = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dataListado)).BeginInit();
            this.SuspendLayout();
            // 
            // dataListado
            // 
            this.dataListado.AllowUserToAddRows = false;
            this.dataListado.AllowUserToDeleteRows = false;
            this.dataListado.AllowUserToOrderColumns = true;
            this.dataListado.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataListado.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.dataListado.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataListado.Location = new System.Drawing.Point(12, 78);
            this.dataListado.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dataListado.Name = "dataListado";
            this.dataListado.ReadOnly = true;
            this.dataListado.RowHeadersWidth = 62;
            this.dataListado.RowTemplate.Height = 28;
            this.dataListado.Size = new System.Drawing.Size(556, 213);
            this.dataListado.TabIndex = 25;
            this.dataListado.DoubleClick += new System.EventHandler(this.dataListado_DoubleClick);
            // 
            // btnBuscar
            // 
            this.btnBuscar.IconChar = FontAwesome.Sharp.IconChar.MagnifyingGlass;
            this.btnBuscar.IconColor = System.Drawing.Color.Black;
            this.btnBuscar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnBuscar.IconSize = 25;
            this.btnBuscar.Location = new System.Drawing.Point(424, 12);
            this.btnBuscar.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(127, 34);
            this.btnBuscar.TabIndex = 24;
            this.btnBuscar.Text = "Buscar";
            this.btnBuscar.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnBuscar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnBuscar.UseVisualStyleBackColor = true;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // txtBuscar
            // 
            this.txtBuscar.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBuscar.Location = new System.Drawing.Point(160, 16);
            this.txtBuscar.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtBuscar.Name = "txtBuscar";
            this.txtBuscar.Size = new System.Drawing.Size(224, 26);
            this.txtBuscar.TabIndex = 22;
            // 
            // lblTotal
            // 
            this.lblTotal.AutoSize = true;
            this.lblTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotal.Location = new System.Drawing.Point(17, 53);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(163, 18);
            this.lblTotal.TabIndex = 23;
            this.lblTotal.Text = "Registros encontrados:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(17, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(123, 18);
            this.label1.TabIndex = 21;
            this.label1.Text = "Ingrese producto:";
            // 
            // FrmVistaProducto_Orden
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(580, 303);
            this.Controls.Add(this.dataListado);
            this.Controls.Add(this.btnBuscar);
            this.Controls.Add(this.txtBuscar);
            this.Controls.Add(this.lblTotal);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmVistaProducto_Orden";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Productos en Orden Pedido";
            this.Load += new System.EventHandler(this.FrmVistaProducto_Orden_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataListado)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataListado;
        private FontAwesome.Sharp.IconButton btnBuscar;
        private System.Windows.Forms.TextBox txtBuscar;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolTip ttMensaje;
    }
}