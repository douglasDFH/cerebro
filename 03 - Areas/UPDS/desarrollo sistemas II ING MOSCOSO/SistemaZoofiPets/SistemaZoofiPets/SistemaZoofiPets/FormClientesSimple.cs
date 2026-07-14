using System;
using System.Data;
using System.Windows.Forms;

namespace SistemaZoofiPets
{
    public partial class FormClientesSimple : Form
    {
        public FormClientesSimple()
        {
            InitializeComponent();
            CargarDatosEjemplo();
        }

        private void CargarDatosEjemplo()
        {
            try
            {
                DataTable tabla = new DataTable();
                tabla.Columns.Add("ID", typeof(int));
                tabla.Columns.Add("Nombre", typeof(string));
                tabla.Columns.Add("Apellido", typeof(string));
                tabla.Columns.Add("Telefono", typeof(string));
                tabla.Columns.Add("Email", typeof(string));

                // Datos de ejemplo
                tabla.Rows.Add(1, "Ana", "Martínez", "555-1001", "ana@email.com");
                tabla.Rows.Add(2, "Carlos", "López", "555-1002", "carlos@email.com");
                tabla.Rows.Add(3, "Sofía", "Rodríguez", "555-1003", "sofia@email.com");

                if (dgv_clientes != null)
                {
                    dgv_clientes.DataSource = tabla;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar datos: {ex.Message}", "Error", 
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_agregar_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Funcionalidad de agregar cliente", "Info", 
                          MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btn_modificar_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Funcionalidad de modificar cliente", "Info", 
                          MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btn_eliminar_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Funcionalidad de eliminar cliente", "Info", 
                          MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void FormClientesSimple_Load(object sender, EventArgs e)
        {
            // Inicialización adicional si es necesaria
        }
    }
}