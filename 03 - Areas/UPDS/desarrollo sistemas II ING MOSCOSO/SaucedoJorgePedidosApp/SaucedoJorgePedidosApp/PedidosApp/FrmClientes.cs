using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CapaDatos;

namespace PedidosApp
{
    public partial class FrmClientes : Form
    {
        private Dstaffs dstaffs;
        private bool isEditMode = false;
        private int currentStaffId = 0;

        public FrmClientes()
        {
            InitializeComponent();
            dstaffs = new Dstaffs();
            InitializeForm();
        }

        private void InitializeForm()
        {
            // Configurar ComboBox de búsqueda
            cbBuscar.Items.Add("Código");
            cbBuscar.Items.Add("Nombre");
            cbBuscar.Items.Add("Apellido");
            cbBuscar.Items.Add("Email");
            cbBuscar.Items.Add("Teléfono");
            cbBuscar.SelectedIndex = 0;

            // Configurar DataGridView
            ConfigureDataGridView();

            // Cargar datos iniciales
            LoadStaffs();

            // Configurar estado inicial de controles
            SetControlsState(false);
        }

        private void ConfigureDataGridView()
        {
            dataGridViewStaffs.AutoGenerateColumns = false;
            dataGridViewStaffs.AllowUserToAddRows = false;
            dataGridViewStaffs.AllowUserToDeleteRows = false;
            dataGridViewStaffs.ReadOnly = true;
            dataGridViewStaffs.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewStaffs.MultiSelect = false;

            // Agregar columnas
            dataGridViewStaffs.Columns.Add("staff_id", "Código");
            dataGridViewStaffs.Columns.Add("first_name", "Nombre");
            dataGridViewStaffs.Columns.Add("last_name", "Apellido");
            dataGridViewStaffs.Columns.Add("email", "Email");
            dataGridViewStaffs.Columns.Add("phone", "Teléfono");
            dataGridViewStaffs.Columns.Add("active", "Activo");

            // Configurar ancho de columnas
            dataGridViewStaffs.Columns["staff_id"].Width = 80;
            dataGridViewStaffs.Columns["first_name"].Width = 120;
            dataGridViewStaffs.Columns["last_name"].Width = 120;
            dataGridViewStaffs.Columns["email"].Width = 200;
            dataGridViewStaffs.Columns["phone"].Width = 120;
            dataGridViewStaffs.Columns["active"].Width = 80;
        }

        private void LoadStaffs()
        {
            try
            {
                DataTable dt = dstaffs.Mostrar();
                dataGridViewStaffs.Rows.Clear();

                foreach (DataRow row in dt.Rows)
                {
                    int rowIndex = dataGridViewStaffs.Rows.Add();
                    dataGridViewStaffs.Rows[rowIndex].Cells["staff_id"].Value = row["staff_id"];
                    dataGridViewStaffs.Rows[rowIndex].Cells["first_name"].Value = row["first_name"];
                    dataGridViewStaffs.Rows[rowIndex].Cells["last_name"].Value = row["last_name"];
                    dataGridViewStaffs.Rows[rowIndex].Cells["email"].Value = row["email"];
                    dataGridViewStaffs.Rows[rowIndex].Cells["phone"].Value = row["phone"];
                    dataGridViewStaffs.Rows[rowIndex].Cells["active"].Value = Convert.ToBoolean(row["active"]) ? "Sí" : "No";
                }

                lblTotal.Text = $"Registros encontrados: {dt.Rows.Count}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar datos: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SetControlsState(bool editing)
        {
            // Controles de datos
            txtStaffId.Enabled = false; // ID siempre deshabilitado
            txtFirstName.Enabled = editing;
            txtLastName.Enabled = editing;
            txtEmail.Enabled = editing;
            txtPhone.Enabled = editing;
            chkActive.Enabled = editing;

            // Botones
            btnNuevo.Enabled = !editing;
            btnGuardar.Enabled = editing;
            btnEditar.Enabled = !editing && dataGridViewStaffs.SelectedRows.Count > 0;
            btnCancelar.Enabled = editing;
            btnEliminar.Enabled = !editing && dataGridViewStaffs.SelectedRows.Count > 0;

            // Controles de búsqueda
            cbBuscar.Enabled = !editing;
            txtBuscar.Enabled = !editing;
            btnBuscar.Enabled = !editing;
            chkEliminar.Enabled = !editing;
        }

        private void ClearControls()
        {
            txtStaffId.Clear();
            txtFirstName.Clear();
            txtLastName.Clear();
            txtEmail.Clear();
            txtPhone.Clear();
            chkActive.Checked = true;
            currentStaffId = 0;
        }

        private void LoadSelectedStaff()
        {
            if (dataGridViewStaffs.SelectedRows.Count > 0)
            {
                DataGridViewRow row = dataGridViewStaffs.SelectedRows[0];

                txtStaffId.Text = row.Cells["staff_id"].Value.ToString();
                txtFirstName.Text = row.Cells["first_name"].Value.ToString();
                txtLastName.Text = row.Cells["last_name"].Value.ToString();
                txtEmail.Text = row.Cells["email"].Value.ToString();
                txtPhone.Text = row.Cells["phone"].Value.ToString();
                chkActive.Checked = row.Cells["active"].Value.ToString() == "Sí";

                currentStaffId = Convert.ToInt32(row.Cells["staff_id"].Value);
            }
        }

        private bool ValidateData()
        {
            if (string.IsNullOrWhiteSpace(txtFirstName.Text))
            {
                MessageBox.Show("El nombre es obligatorio.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtFirstName.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtLastName.Text))
            {
                MessageBox.Show("El apellido es obligatorio.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtLastName.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                MessageBox.Show("El email es obligatorio.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEmail.Focus();
                return false;
            }

            // Validar formato de email
            try
            {
                var addr = new System.Net.Mail.MailAddress(txtEmail.Text);
                if (addr.Address != txtEmail.Text)
                    throw new Exception();
            }
            catch
            {
                MessageBox.Show("El formato del email no es válido.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEmail.Focus();
                return false;
            }

            return true;
        }

        // Eventos de controles
        private void cbBuscar_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtBuscar.Focus();
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtBuscar.Text))
            {
                LoadStaffs();
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                string searchValue = txtBuscar.Text.Trim();
                if (string.IsNullOrEmpty(searchValue))
                {
                    LoadStaffs();
                    return;
                }

                string searchField = "";
                switch (cbBuscar.SelectedItem.ToString())
                {
                    case "Código":
                        searchField = "staff_id";
                        break;
                    case "Nombre":
                        searchField = "first_name";
                        break;
                    case "Apellido":
                        searchField = "last_name";
                        break;
                    case "Email":
                        searchField = "email";
                        break;
                    case "Teléfono":
                        searchField = "phone";
                        break;
                }

                DataTable dt = dstaffs.Buscar(searchField, searchValue);
                dataGridViewStaffs.Rows.Clear();

                foreach (DataRow row in dt.Rows)
                {
                    int rowIndex = dataGridViewStaffs.Rows.Add();
                    dataGridViewStaffs.Rows[rowIndex].Cells["staff_id"].Value = row["staff_id"];
                    dataGridViewStaffs.Rows[rowIndex].Cells["first_name"].Value = row["first_name"];
                    dataGridViewStaffs.Rows[rowIndex].Cells["last_name"].Value = row["last_name"];
                    dataGridViewStaffs.Rows[rowIndex].Cells["email"].Value = row["email"];
                    dataGridViewStaffs.Rows[rowIndex].Cells["phone"].Value = row["phone"];
                    dataGridViewStaffs.Rows[rowIndex].Cells["active"].Value = Convert.ToBoolean(row["active"]) ? "Sí" : "No";
                }

                lblTotal.Text = $"Registros encontrados: {dt.Rows.Count}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error en la búsqueda: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dataGridViewStaffs.SelectedRows.Count == 0)
            {
                MessageBox.Show("Seleccione un registro para eliminar.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (MessageBox.Show("¿Está seguro de eliminar este registro?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    int staffId = Convert.ToInt32(dataGridViewStaffs.SelectedRows[0].Cells["staff_id"].Value);
                    string result = dstaffs.Eliminar(staffId);

                    if (result == "OK")
                    {
                        MessageBox.Show("Registro eliminado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadStaffs();
                        ClearControls();
                    }
                    else
                    {
                        MessageBox.Show($"Error al eliminar: {result}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al eliminar: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            // Implementar funcionalidad de impresión según necesidades
            MessageBox.Show("Funcionalidad de impresión pendiente de implementar.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void chkEliminar_CheckedChanged(object sender, EventArgs e)
        {
            // Habilitar/deshabilitar botón eliminar según checkbox
            btnEliminar.Enabled = chkEliminar.Checked && dataGridViewStaffs.SelectedRows.Count > 0 && !isEditMode;
        }

        private void dataGridViewStaffs_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && !isEditMode)
            {
                LoadSelectedStaff();
                SetControlsState(false);
            }
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            ClearControls();
            isEditMode = true;
            SetControlsState(true);
            txtFirstName.Focus();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (ValidateData())
            {
                try
                {
                    string result;

                    if (currentStaffId == 0) // Nuevo registro
                    {
                        result = dstaffs.Insertar(
                            txtFirstName.Text.Trim(),
                            txtLastName.Text.Trim(),
                            txtEmail.Text.Trim(),
                            txtPhone.Text.Trim(),
                            chkActive.Checked,
                            1, // store_id por defecto
                            null // manager_id
                        );
                    }
                    else // Actualizar registro existente
                    {
                        result = dstaffs.Editar(
                            currentStaffId,
                            txtFirstName.Text.Trim(),
                            txtLastName.Text.Trim(),
                            txtEmail.Text.Trim(),
                            txtPhone.Text.Trim(),
                            chkActive.Checked,
                            1, // store_id
                            null // manager_id
                        );
                    }

                    if (result == "OK")
                    {
                        MessageBox.Show("Registro guardado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        isEditMode = false;
                        SetControlsState(false);
                        LoadStaffs();
                        ClearControls();
                    }
                    else
                    {
                        MessageBox.Show($"Error al guardar: {result}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al guardar: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (dataGridViewStaffs.SelectedRows.Count == 0)
            {
                MessageBox.Show("Seleccione un registro para editar.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            LoadSelectedStaff();
            isEditMode = true;
            SetControlsState(true);
            txtFirstName.Focus();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Está seguro de cancelar? Se perderán los cambios no guardados.", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                isEditMode = false;
                SetControlsState(false);
                ClearControls();
            }
        }

        // Eventos vacíos mantenidos para compatibilidad
        private void txtStaffId_TextChanged(object sender, EventArgs e) { }
        private void lblCodigo_Click(object sender, EventArgs e) { }
        private void lblNombre_Click(object sender, EventArgs e) { }
        private void txtFirstName_TextChanged(object sender, EventArgs e) { }
        private void groupBoxUsuarios_Enter(object sender, EventArgs e) { }
        private void lblApellidos_Click(object sender, EventArgs e) { }
        private void txtLastName_TextChanged(object sender, EventArgs e) { }
        private void label5_Click(object sender, EventArgs e) { }
        private void txtEmail_TextChanged(object sender, EventArgs e) { }
        private void lblGenero_Click(object sender, EventArgs e) { }
        private void txtPhone_TextChanged(object sender, EventArgs e) { }
        private void chkActive_CheckedChanged(object sender, EventArgs e) { }
        private void lblTotal_Click(object sender, EventArgs e) { }
    }
}