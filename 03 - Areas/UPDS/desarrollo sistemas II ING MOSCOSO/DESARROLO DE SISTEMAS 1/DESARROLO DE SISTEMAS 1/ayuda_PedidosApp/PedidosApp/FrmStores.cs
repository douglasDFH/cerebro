using CapaNegocio;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace PedidosApp
{
    public partial class FrmStores : Form
    {
        private bool EsNuevo = false;
        private bool EsEdita = false;
        private Nstores nStores = new Nstores();

        public FrmStores()
        {
            InitializeComponent();
            this.Text = "Gestión de Tiendas";
            ttMensaje.SetToolTip(txtStoreName, "Ingrese el nombre de la tienda");
            ttMensaje.SetToolTip(txtPhone, "Ingrese el teléfono de la tienda");
            ttMensaje.SetToolTip(txtEmail, "Ingrese el email de la tienda");
            txtStoreId.Enabled = false;
        }

        private void MensajeOK(string mensaje)
        {
            MessageBox.Show(mensaje, "Gestión de Tiendas", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void MensajeError(string mensaje)
        {
            MessageBox.Show(mensaje, "Gestión de Tiendas", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void Habilitar(bool valor)
        {
            txtStoreName.ReadOnly = !valor;
            txtPhone.ReadOnly = !valor;
            txtEmail.ReadOnly = !valor;
            txtStreet.ReadOnly = !valor;
            txtCity.ReadOnly = !valor;
            txtState.ReadOnly = !valor;
            txtZipCode.ReadOnly = !valor;
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
                dataListado.DataSource = nStores.Mostrar();
                OcultarColumnas();
                lblTotal.Text = "Registros encontrados: " + Convert.ToString(dataListado.Rows.Count);
                tabControl1.SelectedIndex = 0;
                
                // Personalizar headers del DataGridView
                if (dataListado.Columns.Count > 2)
                {
                    dataListado.Columns["store_id"].HeaderText = "ID";
                    dataListado.Columns["store_name"].HeaderText = "Nombre Tienda";
                    dataListado.Columns["phone"].HeaderText = "Teléfono";
                    dataListado.Columns["email"].HeaderText = "Email";
                    dataListado.Columns["street"].HeaderText = "Dirección";
                    dataListado.Columns["city"].HeaderText = "Ciudad";
                    dataListado.Columns["state"].HeaderText = "Estado";
                    dataListado.Columns["zip_code"].HeaderText = "Código Postal";
                }
            }
            catch (Exception ex)
            {
                MensajeError("Error al mostrar datos: " + ex.Message);
            }
        }

        private void BuscarStore()
        {
            try
            {
                if (cmbBuscar.Text == "" || txtBuscar.Text.Trim() == "")
                {
                    Mostrar();
                    return;
                }

                dataListado.DataSource = nStores.Buscar(cmbBuscar.Text, txtBuscar.Text.Trim());
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
            txtStoreId.Text = string.Empty;
            txtStoreName.Text = string.Empty;
            txtPhone.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtStreet.Text = string.Empty;
            txtCity.Text = string.Empty;
            txtState.Text = string.Empty;
            txtZipCode.Text = string.Empty;
        }

        private void CargarComboBoxes()
        {
            try
            {
                // Configurar combo de búsqueda
                cmbBuscar.Items.Clear();
                cmbBuscar.Items.AddRange(new object[] { "store_id", "store_name", "phone", "email", "city", "state" });
                cmbBuscar.Text = "store_name";
            }
            catch (Exception ex)
            {
                MensajeError("Error al cargar datos de configuración: " + ex.Message);
            }
        }

        private void FrmStores_Load(object sender, EventArgs e)
        {
            CargarComboBoxes();
            Mostrar();
            Habilitar(false);
            Botones();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            BuscarStore();
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            BuscarStore();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            EsNuevo = true;
            EsEdita = false;
            Botones();
            Limpiar();
            Habilitar(true);
            txtStoreName.Focus();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                string rpta = string.Empty;

                // Validaciones básicas
                if (string.IsNullOrWhiteSpace(txtStoreName.Text))
                {
                    MensajeError("El nombre de la tienda es requerido");
                    errorIcono.SetError(txtStoreName, "Ingrese el nombre de la tienda");
                    return;
                }

                // Limpiar errores previos
                errorIcono.Clear();

                // Obtener valores
                string storeName = txtStoreName.Text.Trim();
                string phone = string.IsNullOrWhiteSpace(txtPhone.Text) ? null : txtPhone.Text.Trim();
                string email = string.IsNullOrWhiteSpace(txtEmail.Text) ? null : txtEmail.Text.Trim();
                string street = string.IsNullOrWhiteSpace(txtStreet.Text) ? null : txtStreet.Text.Trim();
                string city = string.IsNullOrWhiteSpace(txtCity.Text) ? null : txtCity.Text.Trim();
                string state = string.IsNullOrWhiteSpace(txtState.Text) ? null : txtState.Text.Trim();
                string zipCode = string.IsNullOrWhiteSpace(txtZipCode.Text) ? null : txtZipCode.Text.Trim();

                if (EsNuevo)
                {
                    rpta = nStores.Insertar(storeName, phone, email, street, city, state, zipCode);
                }
                else
                {
                    int storeId = Convert.ToInt32(txtStoreId.Text);
                    rpta = nStores.Editar(storeId, storeName, phone, email, street, city, state, zipCode);
                }

                if (rpta.Equals("OK"))
                {
                    if (EsNuevo) MensajeOK("Tienda registrada correctamente");
                    else MensajeOK("Tienda actualizada correctamente");
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

                txtStoreId.Text = Convert.ToString(dataListado.CurrentRow.Cells["store_id"].Value);
                txtStoreName.Text = Convert.ToString(dataListado.CurrentRow.Cells["store_name"].Value);
                txtPhone.Text = Convert.ToString(dataListado.CurrentRow.Cells["phone"].Value);
                txtEmail.Text = Convert.ToString(dataListado.CurrentRow.Cells["email"].Value);
                txtStreet.Text = Convert.ToString(dataListado.CurrentRow.Cells["street"].Value);
                txtCity.Text = Convert.ToString(dataListado.CurrentRow.Cells["city"].Value);
                txtState.Text = Convert.ToString(dataListado.CurrentRow.Cells["state"].Value);
                txtZipCode.Text = Convert.ToString(dataListado.CurrentRow.Cells["zip_code"].Value);

                tabControl1.SelectedIndex = 1;
            }
            catch (Exception ex)
            {
                MensajeError("Error al cargar datos de la tienda: " + ex.Message);
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtStoreId.Text))
            {
                EsEdita = true;
                Botones();
                Habilitar(true);
                txtStoreName.Focus();
            }
            else
            {
                MensajeError("Debe seleccionar una tienda de la lista");
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
                DialogResult opcion = MessageBox.Show("¿Realmente desea eliminar las tiendas seleccionadas?",
                    "Gestión de Tiendas", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

                if (opcion == DialogResult.OK)
                {
                    string rpta = string.Empty;
                    foreach (DataGridViewRow row in dataListado.Rows)
                    {
                        if (Convert.ToBoolean(row.Cells[0].Value))
                        {
                            int storeId = Convert.ToInt32(row.Cells[1].Value);
                            rpta = nStores.Eliminar(storeId);

                            if (rpta.Equals("OK"))
                            {
                                MensajeOK("Tienda eliminada correctamente");
                            }
                            else
                            {
                                MensajeError(rpta);
                            }
                        }
                    }
                    Mostrar();
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
            BuscarStore();
        }

        private void btnEstadisticas_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable stats = nStores.GetStatistics();
                if (stats.Rows.Count > 0)
                {
                    DataRow row = stats.Rows[0];
                    string mensaje = $"Estadísticas de Tiendas:\n\n" +
                                   $"Total de Tiendas: {row["TotalTiendas"]}\n" +
                                   $"Tiendas con Teléfono: {row["ConTelefono"]}\n" +
                                   $"Tiendas con Email: {row["ConEmail"]}";

                    MessageBox.Show(mensaje, "Estadísticas", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MensajeError("Error al obtener estadísticas: " + ex.Message);
            }
        }

        private void btnStaffInfo_Click(object sender, EventArgs e)
        {
            try
            {
                // Mostrar información de tiendas con staff
                DataTable storesWithStaff = nStores.GetStoresWithStaffInfo();
                dataListado.DataSource = storesWithStaff;
                
                if (dataListado.Columns.Count > 0)
                {
                    dataListado.Columns["store_id"].HeaderText = "ID";
                    dataListado.Columns["store_name"].HeaderText = "Tienda";
                    dataListado.Columns["city"].HeaderText = "Ciudad";
                    dataListado.Columns["state"].HeaderText = "Estado";
                    dataListado.Columns["TotalStaff"].HeaderText = "Total Staff";
                    dataListado.Columns["StaffActivo"].HeaderText = "Staff Activo";
                }

                lblTotal.Text = "Tiendas encontradas: " + Convert.ToString(dataListado.Rows.Count);
                
                MessageBox.Show("Vista actualizada para mostrar información de personal por tienda", 
                              "Vista Actualizada", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MensajeError("Error al mostrar información de staff: " + ex.Message);
            }
        }
    }
}