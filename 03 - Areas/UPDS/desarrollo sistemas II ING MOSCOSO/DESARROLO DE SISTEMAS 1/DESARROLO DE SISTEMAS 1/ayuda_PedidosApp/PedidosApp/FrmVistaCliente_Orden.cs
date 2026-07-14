using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CapaNegocio;

namespace PedidosApp
{
    public partial class FrmVistaCliente_Orden : Form
    {
        public FrmVistaCliente_Orden()
        {
            InitializeComponent();
            ttMensaje.SetToolTip(txtBuscar, "Ej. Clientes que inicia con letra A");
            ttMensaje.SetToolTip(dataListado, "Para seleccionar un Cliente con Doble Click");
        }
        //Metodo Mostrar
        private void Mostrar()
        {
            dataListado.DataSource = Ncustomer.Mostrar();
            OcultarColumnas();
            lblTotal.Text = "Registros encontrados: " + Convert.ToString(dataListado.Rows.Count);
        }
        private void BuscarNombre()
        {
            dataListado.DataSource = Ncustomer.BuscarNombre(txtBuscar.Text.Trim());
            lblTotal.Text = "Registros encontrados: " + Convert.ToString(dataListado.Rows.Count);
        }
        //Metodo para ocultar columnas
        private void OcultarColumnas()
        {
            if (dataListado.RowCount > 0 && dataListado.ColumnCount > 0)
            {
                // Ocultar customer_id (índice 0)
                if (dataListado.ColumnCount > 0) dataListado.Columns[0].Visible = false;
                // Ocultar phone (índice 3)
                if (dataListado.ColumnCount > 3) dataListado.Columns[3].Visible = false;
                // Ocultar email (índice 4) 
                if (dataListado.ColumnCount > 4) dataListado.Columns[4].Visible = false;
                // Ocultar street (índice 5)
                if (dataListado.ColumnCount > 5) dataListado.Columns[5].Visible = false;
                // Ocultar city (índice 6)
                if (dataListado.ColumnCount > 6) dataListado.Columns[6].Visible = false;
                // Ocultar state (índice 7)
                if (dataListado.ColumnCount > 7) dataListado.Columns[7].Visible = false;
                
                // Configurar encabezados de columnas visibles
                if (dataListado.ColumnCount > 1) dataListado.Columns[1].HeaderText = "Nombre";
                if (dataListado.ColumnCount > 2) dataListado.Columns[2].HeaderText = "Apellido";
            }
        }
        private void FrmVistaCliente_Orden_Load(object sender, EventArgs e)
        {
            Mostrar();
        }
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            BuscarNombre();
        }
        private void dataListado_DoubleClick(object sender, EventArgs e)
        {
            FrmOrders form = FrmOrders.GetInstancia();
            string par1, par2;
            par1 = Convert.ToString(dataListado.CurrentRow.Cells["customer_id"].Value);
            par2 = Convert.ToString(dataListado.CurrentRow.Cells["first_name"].Value) + " " +
                Convert.ToString(dataListado.CurrentRow.Cells["last_name"].Value);
            form.setCliente(par1, par2);
            Hide();
        }
    }
}
