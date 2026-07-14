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
    public partial class FrmVistaCategoria_Producto : Form
    {
        public FrmVistaCategoria_Producto()
        {
            InitializeComponent();
        }
        //Metodo Mostrar
        private void Mostrar()
        {
            dataListado.DataSource = Ncategories.Mostrar();
            lblTotal.Text = "Registros encontrados: " + Convert.ToString(dataListado.Rows.Count);
        }
        private void BuscarNombre()
        {
            dataListado.DataSource = Ncategories.BuscarNombre(txtBuscar.Text.Trim());
            lblTotal.Text = "Registros encontrados: " + Convert.ToString(dataListado.Rows.Count);
        }
        private void FrmVistaCategoria_Producto_Load(object sender, EventArgs e)
        {
            Mostrar();
        }
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            BuscarNombre();
        }
        private void dataListado_DoubleClick(object sender, EventArgs e)
        {
            FrmProducts form = FrmProducts.GetInstancia();
            string par1, par2;
            par1 = Convert.ToString(dataListado.CurrentRow.Cells["category_id"].Value);
            par2 = Convert.ToString(dataListado.CurrentRow.Cells["category_name"].Value);
            form.setCategoria(par1, par2);
            Hide();
        }
    }
}
