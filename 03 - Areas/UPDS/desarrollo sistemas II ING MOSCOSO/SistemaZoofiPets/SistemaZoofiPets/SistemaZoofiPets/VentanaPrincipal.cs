using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SistemaZoofiPets
{
    public partial class VentanaPrincipal : Form
    {
        public VentanaPrincipal()
        {
            InitializeComponent();
        }

        private void btn_cerrar_Click(object sender, EventArgs e)
        {
            Application.Exit();

        }

        private void Btn_menu_Usuarios_Click(object sender, EventArgs e)
        {
            FormUsers v1 = new FormUsers();
            this.Hide();
            v1.ShowDialog();
            this.Show();    
        }

        private void Btn_menu_Inventario_Click(object sender, EventArgs e)
        {
            FormInventario v1 = new FormInventario();
            this.Hide();
            v1.ShowDialog();
            this.Show();
        }

        private void Btn_menu_Ventas_Click(object sender, EventArgs e)
        {
            FormVentas v1 = new FormVentas();
            this.Hide();
            v1.ShowDialog();
            this.Show();
        }

        private void Btn_menu_Reportes_Click(object sender, EventArgs e)
        {
            FormReportes v1 = new FormReportes();
            this.Hide();
            v1.ShowDialog();
            this.Show();
        }

    }
}
    
