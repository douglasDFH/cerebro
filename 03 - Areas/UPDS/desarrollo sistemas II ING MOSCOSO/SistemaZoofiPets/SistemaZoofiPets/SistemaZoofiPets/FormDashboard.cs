using System;
using System.Drawing;
using System.Windows.Forms;

namespace SistemaZoofiPets
{
    public partial class FormDashboard : Form
    {
        public FormDashboard()
        {
            InitializeComponent();
            LoadDashboardData();
        }

        private void LoadDashboardData()
        {
            // Simular datos del dashboard
            lblTotalAnimales.Text = "142";
            lblCitasHoy.Text = "8";
            lblVentasHoy.Text = "$2,450.00";
            lblInventarioBajo.Text = "3";
        }
    }
}