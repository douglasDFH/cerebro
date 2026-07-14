using System;
using System.Data;
using System.Windows.Forms;
using CapaNegocios.Servicios;
using CapaDatos.Entidades;

namespace SistemaZoofiPets
{
    public partial class FormClientes : Form
    {
        private readonly ClienteServicio clienteServicio;
        private int idClienteSeleccionado = 0;

        public FormClientes()
        {
            InitializeComponent();
            clienteServicio = new ClienteServicio();
            CargarClientes();
            ConfigurarDataGridView();
        }

        private void ConfigurarDataGridView()
        {
            if (dgv_clientes.Columns.Count > 0)
            {
                dgv_clientes.Columns["IdPersona"].HeaderText = "ID";
                dgv_clientes.Columns["IdPersona"].Width = 50;
                dgv_clientes.Columns["DNI"].HeaderText = "DNI";
                dgv_clientes.Columns["DNI"].Width = 100;
                dgv_clientes.Columns["Nombre"].HeaderText = "Nombre";
                dgv_clientes.Columns["Nombre"].Width = 120;
                dgv_clientes.Columns["Apellido"].HeaderText = "Apellido";
                dgv_clientes.Columns["Apellido"].Width = 120;
                dgv_clientes.Columns["Email"].HeaderText = "Email";
                dgv_clientes.Columns["Email"].Width = 200;
                dgv_clientes.Columns["Telefono"].HeaderText = "Teléfono";
                dgv_clientes.Columns["Telefono"].Width = 100;
                dgv_clientes.Columns["TotalMascotas"].HeaderText = "Mascotas";
                dgv_clientes.Columns["TotalMascotas"].Width = 80;

                dgv_clientes.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgv_clientes.ReadOnly = true;
                dgv_clientes.AllowUserToAddRows = false;
                dgv_clientes.AllowUserToDeleteRows = false;
            }
        }

        private void CargarClientes()
        {
            try
            {
                DataTable dt = clienteServicio.ConsultarClientes();
                dgv_clientes.DataSource = dt;
                
                if (dt.Rows.Count == 0)
                {
                    lbl_total_registros.Text = "No hay clientes registrados";
                }
                else
                {
                    lbl_total_registros.Text = $"Total de clientes: {dt.Rows.Count}";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar clientes: {ex.Message}", "Error", 
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_agregar_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidarCampos())
                    return;

                int idCliente = clienteServicio.RegistrarCliente(
                    txt_dni.Text,
                    txt_nombre.Text,
                    txt_apellido.Text,
                    txt_email.Text,
                    txt_telefono.Text,
                    txt_direccion.Text,
                    dtp_fecha_nacimiento.Checked ? dtp_fecha_nacimiento.Value : (DateTime?)null
                );

                if (idCliente > 0)
                {
                    MessageBox.Show("Cliente registrado exitosamente", "Éxito", 
                                  MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LimpiarCampos();
                    CargarClientes();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al registrar cliente: {ex.Message}", "Error", 
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_modificar_Click(object sender, EventArgs e)
        {
            try
            {
                if (idClienteSeleccionado <= 0)
                {
                    MessageBox.Show("Seleccione un cliente para modificar", "Advertencia", 
                                  MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!ValidarCampos())
                    return;

                bool resultado = clienteServicio.ModificarCliente(
                    idClienteSeleccionado,
                    txt_dni.Text,
                    txt_nombre.Text,
                    txt_apellido.Text,
                    txt_email.Text,
                    txt_telefono.Text,
                    txt_direccion.Text,
                    dtp_fecha_nacimiento.Checked ? dtp_fecha_nacimiento.Value : (DateTime?)null
                );

                if (resultado)
                {
                    MessageBox.Show("Cliente modificado exitosamente", "Éxito", 
                                  MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LimpiarCampos();
                    CargarClientes();
                    idClienteSeleccionado = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al modificar cliente: {ex.Message}", "Error", 
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_eliminar_Click(object sender, EventArgs e)
        {
            try
            {
                if (idClienteSeleccionado <= 0)
                {
                    MessageBox.Show("Seleccione un cliente para eliminar", "Advertencia", 
                                  MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DialogResult confirmacion = MessageBox.Show(
                    "¿Está seguro de que desea eliminar este cliente?\nEsta acción no se puede deshacer.", 
                    "Confirmar Eliminación", 
                    MessageBoxButtons.YesNo, 
                    MessageBoxIcon.Question
                );

                if (confirmacion == DialogResult.Yes)
                {
                    bool resultado = clienteServicio.EliminarCliente(idClienteSeleccionado);

                    if (resultado)
                    {
                        MessageBox.Show("Cliente eliminado exitosamente", "Éxito", 
                                      MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LimpiarCampos();
                        CargarClientes();
                        idClienteSeleccionado = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al eliminar cliente: {ex.Message}", "Error", 
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_buscar_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txt_buscar.Text))
                {
                    CargarClientes();
                    return;
                }

                DataTable dt = clienteServicio.BuscarClientes(txt_buscar.Text);
                dgv_clientes.DataSource = dt;
                
                lbl_total_registros.Text = $"Resultados encontrados: {dt.Rows.Count}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al buscar clientes: {ex.Message}", "Error", 
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_limpiar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
            idClienteSeleccionado = 0;
        }

        private void dgv_clientes_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (dgv_clientes.SelectedRows.Count > 0)
                {
                    DataGridViewRow row = dgv_clientes.SelectedRows[0];
                    
                    idClienteSeleccionado = Convert.ToInt32(row.Cells["IdPersona"].Value);
                    txt_dni.Text = row.Cells["DNI"].Value?.ToString() ?? "";
                    txt_nombre.Text = row.Cells["Nombre"].Value?.ToString() ?? "";
                    txt_apellido.Text = row.Cells["Apellido"].Value?.ToString() ?? "";
                    txt_email.Text = row.Cells["Email"].Value?.ToString() ?? "";
                    txt_telefono.Text = row.Cells["Telefono"].Value?.ToString() ?? "";
                    txt_direccion.Text = row.Cells["Direccion"].Value?.ToString() ?? "";

                    // Cargar datos completos del cliente
                    PersonaFisica cliente = clienteServicio.ObtenerCliente(idClienteSeleccionado);
                    if (cliente?.FechaNacimiento.HasValue == true)
                    {
                        dtp_fecha_nacimiento.Value = cliente.FechaNacimiento.Value;
                        dtp_fecha_nacimiento.Checked = true;
                    }
                    else
                    {
                        dtp_fecha_nacimiento.Checked = false;
                    }

                    btn_modificar.Enabled = true;
                    btn_eliminar.Enabled = true;
                }
                else
                {
                    btn_modificar.Enabled = false;
                    btn_eliminar.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al seleccionar cliente: {ex.Message}", "Error", 
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ValidarCampos()
        {
            if (string.IsNullOrWhiteSpace(txt_dni.Text))
            {
                MessageBox.Show("El DNI es requerido", "Campo Requerido", 
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txt_dni.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txt_nombre.Text))
            {
                MessageBox.Show("El nombre es requerido", "Campo Requerido", 
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txt_nombre.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txt_apellido.Text))
            {
                MessageBox.Show("El apellido es requerido", "Campo Requerido", 
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txt_apellido.Focus();
                return false;
            }

            return true;
        }

        private void LimpiarCampos()
        {
            txt_dni.Clear();
            txt_nombre.Clear();
            txt_apellido.Clear();
            txt_email.Clear();
            txt_telefono.Clear();
            txt_direccion.Clear();
            dtp_fecha_nacimiento.Checked = false;
            txt_buscar.Clear();
        }

        private void FormClientes_Load(object sender, EventArgs e)
        {
            btn_modificar.Enabled = false;
            btn_eliminar.Enabled = false;
        }
    }
}