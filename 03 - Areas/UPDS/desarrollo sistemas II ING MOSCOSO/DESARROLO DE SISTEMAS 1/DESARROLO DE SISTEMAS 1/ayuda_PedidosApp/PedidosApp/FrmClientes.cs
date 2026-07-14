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
    public partial class FrmClientes : Form
    {
        private bool EsNuevo = false;
        private static FrmClientes _instancia;

        public static FrmClientes GetInstancia()
        {
            if (_instancia == null)
            {
                _instancia = new FrmClientes();
            }
            return _instancia;
        }

        public FrmClientes()
        {
            InitializeComponent();
        }

        //Mostrar Mensaje de Confirmacion
        private void MensajeOK(string mensaje)
        {
            MessageBox.Show(mensaje, "Pedidos App", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        //Mostrar Mensaje de Error
        private void MensajeError(string mensaje)
        {
            MessageBox.Show(mensaje, "Pedidos App", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void Limpiar()
        {
            txtStaffId.Text = string.Empty;
            txtFirstName.Text = string.Empty;
            txtLastName.Text = string.Empty;
            txtPhone.Text = string.Empty;
            txtEmail.Text = string.Empty;
            chkActive.Checked = true;
        }

        private void Habilitar(bool valor)
        {
            txtFirstName.ReadOnly = !valor;
            txtLastName.ReadOnly = !valor;
            txtPhone.ReadOnly = !valor;
            txtEmail.ReadOnly = !valor;
        }

        //Habilitar los botones
        private void Botones()
        {
            if (EsNuevo)
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

        //Metodo para ocultar columnas
        private void OcultarColumnas()
        {
            if (dataGridViewStaffs.RowCount > 0)
            {
                dataGridViewStaffs.Columns[0].Visible = false;
            }
        }

        //Metodo Mostrar
        private void Mostrar()
        {
            try
            {
                dataGridViewStaffs.DataSource = Ncustomer.Mostrar();
                OcultarColumnas();
                lblTotal.Text = "Registros encontrados: " + Convert.ToString(dataGridViewStaffs.Rows.Count);
            }
            catch (Exception ex)
            {
                MensajeError("Error al mostrar datos: " + ex.Message);
            }
        }

        private void BuscarNombre()
        {
            try
            {
                dataGridViewStaffs.DataSource = Ncustomer.BuscarNombre(txtBuscar.Text);
                OcultarColumnas();
                lblTotal.Text = "Registros encontrados: " + Convert.ToString(dataGridViewStaffs.Rows.Count);
            }
            catch (Exception ex)
            {
                MensajeError("Error al buscar: " + ex.Message);
            }
        }

        private void FrmClientes_Load(object sender, EventArgs e)
        {
            Mostrar();
            Habilitar(false);
            Botones();
        }

        private void FrmClientes_FormClosing(object sender, FormClosingEventArgs e)
        {
            _instancia = null;
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            BuscarNombre();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            EsNuevo = true;
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
                if (string.IsNullOrWhiteSpace(txtFirstName.Text))
                {
                    MensajeError("Debe ingresar el nombre del cliente");
                    txtFirstName.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtLastName.Text))
                {
                    MensajeError("Debe ingresar el apellido del cliente");
                    txtLastName.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtEmail.Text))
                {
                    MensajeError("Debe ingresar el email del cliente");
                    txtEmail.Focus();
                    return;
                }

                // Validar email usando BusinessValidator
                var emailValidation = BusinessValidator.CustomerValidator.ValidateEmail(txtEmail.Text);
                if (!emailValidation.IsValid)
                {
                    MensajeError(emailValidation.ErrorMessage);
                    txtEmail.Focus();
                    return;
                }

                // Validar teléfono si se ingresó
                if (!string.IsNullOrWhiteSpace(txtPhone.Text))
                {
                    var phoneValidation = BusinessValidator.CustomerValidator.ValidatePhone(txtPhone.Text);
                    if (!phoneValidation.IsValid)
                    {
                        MensajeError(phoneValidation.ErrorMessage);
                        txtPhone.Focus();
                        return;
                    }
                }

                if (EsNuevo)
                {
                    rpta = Ncustomer.Insertar(
                        txtFirstName.Text.Trim(),
                        txtLastName.Text.Trim(),
                        txtPhone.Text.Trim(),
                        txtEmail.Text.Trim(),
                        "", // street - vacío por ahora
                        "", // city - vacío por ahora  
                        ""  // state - vacío por ahora
                    );
                }
                else
                {
                    if (!string.IsNullOrEmpty(txtStaffId.Text))
                    {
                        rpta = Ncustomer.Editar(
                            Convert.ToInt32(txtStaffId.Text),
                            txtFirstName.Text.Trim(),
                            txtLastName.Text.Trim(),
                            txtPhone.Text.Trim(),
                            txtEmail.Text.Trim(),
                            "", // street
                            "", // city
                            ""  // state
                        );
                    }
                }

                if (rpta.Equals("OK"))
                {
                    if (EsNuevo) 
                    {
                        MensajeOK("Cliente registrado correctamente");
                    }
                    else 
                    {
                        MensajeOK("Cliente actualizado correctamente");
                    }
                }
                else 
                {
                    MensajeError(rpta);
                }

                EsNuevo = false;
                Botones();
                Limpiar();
                Mostrar();
            }
            catch (Exception ex)
            {
                MensajeError("Error: " + ex.Message);
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (dataGridViewStaffs.CurrentRow != null)
            {
                EsNuevo = false;
                Botones();
                Habilitar(true);
                txtFirstName.Focus();
            }
            else
            {
                MensajeError("Debe seleccionar un registro para editar");
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            EsNuevo = false;
            Botones();
            Limpiar();
            Habilitar(false);
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult opcion = MessageBox.Show(
                    "¿Realmente desea eliminar los registros seleccionados?", 
                    "Pedidos App",
                    MessageBoxButtons.OKCancel, 
                    MessageBoxIcon.Question
                );

                if (opcion == DialogResult.OK)
                {
                    int codigo;
                    string rpta = string.Empty;

                    foreach (DataGridViewRow row in dataGridViewStaffs.Rows)
                    {
                        if (row.Selected && row.Cells["customer_id"].Value != null)
                        {
                            codigo = Convert.ToInt32(row.Cells["customer_id"].Value);
                            rpta = Ncustomer.Eliminar(codigo);

                            if (rpta.Equals("OK"))
                            {
                                MensajeOK("Cliente eliminado correctamente");
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
                MensajeError("Error: " + ex.Message);
            }
        }

        private void dataGridViewStaffs_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dataGridViewStaffs.CurrentRow != null)
            {
                try
                {
                    txtStaffId.Text = Convert.ToString(dataGridViewStaffs.CurrentRow.Cells["customer_id"].Value);
                    txtFirstName.Text = Convert.ToString(dataGridViewStaffs.CurrentRow.Cells["first_name"].Value);
                    txtLastName.Text = Convert.ToString(dataGridViewStaffs.CurrentRow.Cells["last_name"].Value);
                    
                    if (dataGridViewStaffs.CurrentRow.Cells["phone"].Value != null)
                        txtPhone.Text = Convert.ToString(dataGridViewStaffs.CurrentRow.Cells["phone"].Value);
                    
                    if (dataGridViewStaffs.CurrentRow.Cells["email"].Value != null)
                        txtEmail.Text = Convert.ToString(dataGridViewStaffs.CurrentRow.Cells["email"].Value);
                }
                catch (Exception ex)
                {
                    MensajeError("Error al cargar datos: " + ex.Message);
                }
            }
        }

        private void chkEliminar_CheckedChanged(object sender, EventArgs e)
        {
            // No hay columna de eliminación en este diseño, solo selección de fila
        }

        // Eventos adicionales requeridos por el Designer
        private void cbBuscar_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void lblTotal_Click(object sender, EventArgs e)
        {
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            MensajeOK("Función de impresión no implementada");
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
        }

        private void groupBoxUsuarios_Enter(object sender, EventArgs e)
        {
        }

        private void chkActive_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void txtPhone_TextChanged(object sender, EventArgs e)
        {
        }

        private void txtEmail_TextChanged(object sender, EventArgs e)
        {
        }

        private void txtLastName_TextChanged(object sender, EventArgs e)
        {
        }

        private void txtFirstName_TextChanged(object sender, EventArgs e)
        {
        }

        private void txtStaffId_TextChanged(object sender, EventArgs e)
        {
        }

        private void label5_Click(object sender, EventArgs e)
        {
        }

        private void lblGenero_Click(object sender, EventArgs e)
        {
        }

        private void lblApellidos_Click(object sender, EventArgs e)
        {
        }

        private void lblNombre_Click(object sender, EventArgs e)
        {
        }

        private void lblCodigo_Click(object sender, EventArgs e)
        {
        }
    }
}