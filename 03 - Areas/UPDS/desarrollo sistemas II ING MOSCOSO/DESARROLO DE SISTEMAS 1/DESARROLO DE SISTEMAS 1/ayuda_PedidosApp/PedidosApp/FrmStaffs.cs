using CapaNegocio;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace PedidosApp
{
    public partial class FrmStaffs : Form
    {
        private bool EsNuevo = false;
        private bool EsEdita = false;
        private Nstaffs nStaffs = new Nstaffs();

        public FrmStaffs()
        {
            InitializeComponent();
            this.Text = "Gestión de Personal";
            ttMensaje.SetToolTip(txtFirstName, "Ingrese el nombre del empleado");
            ttMensaje.SetToolTip(txtLastName, "Ingrese el apellido del empleado");
            ttMensaje.SetToolTip(txtEmail, "Ingrese el email del empleado");
            ttMensaje.SetToolTip(txtPhone, "Ingrese el teléfono (opcional)");
            txtStaffId.Enabled = false;
        }

        private void MensajeOK(string mensaje)
        {
            MessageBox.Show(mensaje, "Gestión de Personal", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void MensajeError(string mensaje)
        {
            MessageBox.Show(mensaje, "Gestión de Personal", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void Habilitar(bool valor)
        {
            txtFirstName.ReadOnly = !valor;
            txtLastName.ReadOnly = !valor;
            txtEmail.ReadOnly = !valor;
            txtPhone.ReadOnly = !valor;
            chkActive.Enabled = valor;
            cmbStore.Enabled = valor;
            cmbManager.Enabled = valor;
        }

        private void Botones()
        {
            if (EsNuevo || EsEdita)
            {
                Habilitar(true);
                btnNuevo.Enabled = false;
                btnGuardar.Enabled = true;
                btnEditar.Enabled = false;
                btnCancelar.Enabled = true;
            }
            else
            {
                Habilitar(false);
                btnNuevo.Enabled = true;
                btnGuardar.Enabled = false;
                btnEditar.Enabled = true;
                btnCancelar.Enabled = false;
            }
        }

        private void OcultarColumnas()
        {
            if (dataListado.RowCount > 0)
            {
                dataListado.Columns[0].Visible = false;
                dataListado.Columns[1].Visible = false;
            }
        }

        private void Mostrar()
        {
            try
            {
                dataListado.DataSource = nStaffs.Mostrar();
                OcultarColumnas();
                lblTotal.Text = "Registros encontrados: " + Convert.ToString(dataListado.Rows.Count);
                tabControl1.SelectedIndex = 0;
                
                // Personalizar headers del DataGridView
                if (dataListado.Columns.Count > 2)
                {
                    dataListado.Columns["staff_id"].HeaderText = "ID";
                    dataListado.Columns["first_name"].HeaderText = "Nombre";
                    dataListado.Columns["last_name"].HeaderText = "Apellido";
                    dataListado.Columns["email"].HeaderText = "Email";
                    dataListado.Columns["phone"].HeaderText = "Teléfono";
                    dataListado.Columns["active"].HeaderText = "Activo";
                    dataListado.Columns["store_id"].HeaderText = "Tienda ID";
                    dataListado.Columns["manager_id"].HeaderText = "Manager ID";
                }
            }
            catch (Exception ex)
            {
                MensajeError("Error al mostrar datos: " + ex.Message);
            }
        }

        private void BuscarStaff()
        {
            try
            {
                if (cmbBuscar.Text == "" || txtBuscar.Text.Trim() == "")
                {
                    Mostrar();
                    return;
                }

                dataListado.DataSource = nStaffs.Buscar(cmbBuscar.Text, txtBuscar.Text.Trim());
                OcultarColumnas();
                lblTotal.Text = "Registros encontrados: " + Convert.ToString(dataListado.Rows.Count);
            }
            catch (Exception ex)
            {
                MensajeError("Error en la búsqueda: " + ex.Message);
            }
        }

        private void Limpiar()
        {
            txtStaffId.Text = string.Empty;
            txtFirstName.Text = string.Empty;
            txtLastName.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtPhone.Text = string.Empty;
            chkActive.Checked = true;
            cmbStore.SelectedIndex = -1;
            cmbManager.SelectedIndex = -1;
        }

        private void CargarComboBoxes()
        {
            try
            {
                // Cargar tiendas
                DataTable tiendas = new CapaDatos.Dstaffs().ObtenerTiendas();
                cmbStore.DisplayMember = "store_name";
                cmbStore.ValueMember = "store_id";
                cmbStore.DataSource = tiendas;

                // Cargar managers
                DataTable managers = nStaffs.GetManagers();
                cmbManager.DisplayMember = "full_name";
                cmbManager.ValueMember = "staff_id";
                cmbManager.DataSource = managers;

                // Configurar combo de búsqueda
                cmbBuscar.Items.Clear();
                cmbBuscar.Items.AddRange(new object[] { "staff_id", "first_name", "last_name", "email", "phone" });
                cmbBuscar.Text = "first_name";
            }
            catch (Exception ex)
            {
                MensajeError("Error al cargar datos de configuración: " + ex.Message);
            }
        }

        private void FrmStaffs_Load(object sender, EventArgs e)
        {
            CargarComboBoxes();
            Mostrar();
            Habilitar(false);
            Botones();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            BuscarStaff();
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            BuscarStaff();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            EsNuevo = true;
            EsEdita = false;
            Botones();
            Limpiar();
            Habilitar(true);
            txtFirstName.Focus();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                string rpta = string.Empty;

                // Validaciones básicas
                if (string.IsNullOrWhiteSpace(txtFirstName.Text) || 
                    string.IsNullOrWhiteSpace(txtLastName.Text) || 
                    string.IsNullOrWhiteSpace(txtEmail.Text) ||
                    cmbStore.SelectedIndex == -1)
                {
                    MensajeError("Faltan datos requeridos: Nombre, Apellido, Email y Tienda son obligatorios");
                    if (string.IsNullOrWhiteSpace(txtFirstName.Text)) errorIcono.SetError(txtFirstName, "Ingrese el nombre");
                    if (string.IsNullOrWhiteSpace(txtLastName.Text)) errorIcono.SetError(txtLastName, "Ingrese el apellido");
                    if (string.IsNullOrWhiteSpace(txtEmail.Text)) errorIcono.SetError(txtEmail, "Ingrese el email");
                    if (cmbStore.SelectedIndex == -1) errorIcono.SetError(cmbStore, "Seleccione una tienda");
                    return;
                }

                // Limpiar errores previos
                errorIcono.Clear();

                // Obtener valores
                string firstName = txtFirstName.Text.Trim();
                string lastName = txtLastName.Text.Trim();
                string email = txtEmail.Text.Trim();
                string phone = string.IsNullOrWhiteSpace(txtPhone.Text) ? null : txtPhone.Text.Trim();
                bool active = chkActive.Checked;
                int storeId = Convert.ToInt32(cmbStore.SelectedValue);
                int? managerId = (cmbManager.SelectedValue == DBNull.Value || cmbManager.SelectedIndex <= 0) 
                                 ? (int?)null : Convert.ToInt32(cmbManager.SelectedValue);

                if (EsNuevo)
                {
                    rpta = nStaffs.Insertar(firstName, lastName, email, phone, active, storeId, managerId);
                }
                else
                {
                    int staffId = Convert.ToInt32(txtStaffId.Text);
                    rpta = nStaffs.Editar(staffId, firstName, lastName, email, phone, active, storeId, managerId);
                }

                if (rpta.Equals("OK"))
                {
                    if (EsNuevo) MensajeOK("Empleado registrado correctamente");
                    else MensajeOK("Empleado actualizado correctamente");
                }
                else
                {
                    MensajeError(rpta);
                    return;
                }

                EsNuevo = false;
                EsEdita = false;
                Botones();
                Limpiar();
                Mostrar();
                CargarComboBoxes(); // Recargar managers por si agregamos uno nuevo
            }
            catch (Exception ex)
            {
                MensajeError("Error al guardar: " + ex.Message);
            }
        }

        private void dataListado_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (dataListado.CurrentRow == null) return;

                txtStaffId.Text = Convert.ToString(dataListado.CurrentRow.Cells["staff_id"].Value);
                txtFirstName.Text = Convert.ToString(dataListado.CurrentRow.Cells["first_name"].Value);
                txtLastName.Text = Convert.ToString(dataListado.CurrentRow.Cells["last_name"].Value);
                txtEmail.Text = Convert.ToString(dataListado.CurrentRow.Cells["email"].Value);
                txtPhone.Text = Convert.ToString(dataListado.CurrentRow.Cells["phone"].Value);
                chkActive.Checked = Convert.ToBoolean(dataListado.CurrentRow.Cells["active"].Value);

                // Seleccionar tienda
                int storeId = Convert.ToInt32(dataListado.CurrentRow.Cells["store_id"].Value);
                cmbStore.SelectedValue = storeId;

                // Seleccionar manager
                var managerId = dataListado.CurrentRow.Cells["manager_id"].Value;
                if (managerId != DBNull.Value)
                {
                    cmbManager.SelectedValue = Convert.ToInt32(managerId);
                }
                else
                {
                    cmbManager.SelectedIndex = 0; // "Sin Manager"
                }

                tabControl1.SelectedIndex = 1;
            }
            catch (Exception ex)
            {
                MensajeError("Error al cargar datos del empleado: " + ex.Message);
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtStaffId.Text))
            {
                EsEdita = true;
                Botones();
                Habilitar(true);
                txtFirstName.Focus();
            }
            else
            {
                MensajeError("Debe seleccionar un empleado de la lista");
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            EsNuevo = false;
            EsEdita = false;
            Botones();
            Limpiar();
            Habilitar(false);
            errorIcono.Clear();
        }

        private void chkEliminar_CheckedChanged(object sender, EventArgs e)
        {
            if (chkEliminar.Checked)
            {
                dataListado.Columns[0].Visible = true;
            }
            else
            {
                dataListado.Columns[0].Visible = false;
            }
        }

        private void dataListado_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dataListado.Columns["Eliminar"].Index)
            {
                DataGridViewCheckBoxCell chkEliminar = (DataGridViewCheckBoxCell)dataListado.Rows[e.RowIndex].Cells["Eliminar"];
                chkEliminar.Value = !Convert.ToBoolean(chkEliminar.Value);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult opcion = MessageBox.Show("¿Realmente desea eliminar los empleados seleccionados?",
                    "Gestión de Personal", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

                if (opcion == DialogResult.OK)
                {
                    string rpta = string.Empty;
                    foreach (DataGridViewRow row in dataListado.Rows)
                    {
                        if (Convert.ToBoolean(row.Cells[0].Value))
                        {
                            int staffId = Convert.ToInt32(row.Cells[1].Value);
                            rpta = nStaffs.Eliminar(staffId);

                            if (rpta.Equals("OK"))
                            {
                                MensajeOK("Empleado eliminado correctamente");
                            }
                            else
                            {
                                MensajeError(rpta);
                            }
                        }
                    }
                    Mostrar();
                    CargarComboBoxes(); // Recargar managers por si eliminamos alguno
                }
            }
            catch (Exception ex)
            {
                MensajeError("Error al eliminar: " + ex.Message);
            }
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            // Funcionalidad de impresión pendiente
            MensajeError("Funcionalidad de impresión no implementada");
        }

        private void cmbBuscar_SelectedIndexChanged(object sender, EventArgs e)
        {
            BuscarStaff();
        }
    }
}