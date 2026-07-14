using System;
using System.Data;
using System.Windows.Forms;
using CapaNegocio;

namespace SistemVeterinario.Forms
{
    /// <summary>
    /// Diálogo para búsqueda y selección de clientes
    /// </summary>
    public partial class BuscarClienteDialog : Form
    {
        public int ClienteSeleccionadoId { get; private set; }
        public string ClienteSeleccionadoNombre { get; private set; } = "";

        public BuscarClienteDialog()
        {
            InitializeComponent();
        }

        private void BuscarClienteDialog_Load(object sender, EventArgs e)
        {
            CargarClientes();
        }

        private void CargarClientes()
        {
            try
            {
                DataTable dt = NPersona.Mostrar();
                if (dt != null)
                {
                    dgvClientes.DataSource = dt;
                    foreach (DataGridViewColumn column in dgvClientes.Columns)
                    {
                        column.Visible = false;
                    }
                    dgvClientes.Columns["id"].Visible = true;
                    dgvClientes.Columns["nombre_completo"].Visible = true;
                    dgvClientes.Columns["tipo"].Visible = true;
                    dgvClientes.Columns["telefono"].Visible = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error cargando clientes: {ex.Message}");
            }
        }

        private void BtnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                string texto = txtBuscar.Text.Trim();
                DataTable dt = string.IsNullOrEmpty(texto) ? 
                    NPersona.Mostrar() : 
                    NPersona.BuscarTexto(texto);
                
                dgvClientes.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error en búsqueda: {ex.Message}");
            }
        }

        private void BtnSeleccionar_Click(object sender, EventArgs e)
        {
            SeleccionarCliente();
        }

        private void DgvClientes_DoubleClick(object sender, EventArgs e)
        {
            SeleccionarCliente();
        }

        private void BtnCancelar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void SeleccionarCliente()
        {
            if (dgvClientes.SelectedRows.Count > 0)
            {
                ClienteSeleccionadoId = Convert.ToInt32(dgvClientes.SelectedRows[0].Cells["id"].Value);
                ClienteSeleccionadoNombre = dgvClientes.SelectedRows[0].Cells["nombre_completo"].Value?.ToString() ?? "";
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                MessageBox.Show("Seleccione un cliente");
            }
        }
    }
}