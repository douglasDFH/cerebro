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
            txtCustomer_id.Text = string.Empty;
            txtFirstName.Text = string.Empty;
            txtLastName.Text = string.Empty;
            txtPhone.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtStreet.Text = string.Empty;
            txtCity.Text = string.Empty;
            txtState.Text = string.Empty;
        }

        private void Habilitar(bool valor)
        {
            txtCustomer_id.ReadOnly = !valor;
            txtFirstName.ReadOnly = !valor;
            txtLastName.ReadOnly = !valor;
            txtPhone.ReadOnly = !valor;
            txtEmail.ReadOnly = !valor;
            txtStreet.ReadOnly = !valor;
            txtCity.ReadOnly = !valor;
            txtState.ReadOnly = !valor;
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
            if (dataListado.RowCount > 0)
            {
                dataListado.Columns[0].Visible = false;
            }
        }

        //Metodo Mostrar
        private void Mostrar()
        {
            dataListado.DataSource = Ncustomer.Mostrar();
            OcultarColumnas();
            lblTotal.Text = "Registros encontrados: " + Convert.ToString(dataListado.Rows.Count);
        }

        private void BuscarNombre()
        {
            dataListado.DataSource = Ncustomer.BuscarNombre(txtBuscar.Text);
            OcultarColumnas();
            lblTotal.Text = "Registros encontrados: " + Convert.ToString(dataListado.Rows.Count);
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
                        txtStreet.Text.Trim(),
                        txtCity.Text.Trim(),
                        txtState.Text.Trim()
                    );
                }
                else
                {
                    rpta = Ncustomer.Editar(
                        Convert.ToInt32(txtCustomer_id.Text),
                        txtFirstName.Text.Trim(),
                        txtLastName.Text.Trim(),
                        txtPhone.Text.Trim(),
                        txtEmail.Text.Trim(),
                        txtStreet.Text.Trim(),
                        txtCity.Text.Trim(),
                        txtState.Text.Trim()
                    );
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
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (dataListado.CurrentRow != null)
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

                    foreach (DataGridViewRow row in dataListado.Rows)
                    {
                        if (Convert.ToBoolean(row.Cells[0].Value))
                        {
                            codigo = Convert.ToInt32(row.Cells[1].Value);
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
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }

        private void dataListado_DoubleClick(object sender, EventArgs e)
        {
            if (dataListado.CurrentRow != null)
            {
                txtCustomer_id.Text = Convert.ToString(dataListado.CurrentRow.Cells["customer_id"].Value);
                txtFirstName.Text = Convert.ToString(dataListado.CurrentRow.Cells["first_name"].Value);
                txtLastName.Text = Convert.ToString(dataListado.CurrentRow.Cells["last_name"].Value);
                txtPhone.Text = Convert.ToString(dataListado.CurrentRow.Cells["phone"].Value);
                txtEmail.Text = Convert.ToString(dataListado.CurrentRow.Cells["email"].Value);
                txtStreet.Text = Convert.ToString(dataListado.CurrentRow.Cells["street"].Value);
                txtCity.Text = Convert.ToString(dataListado.CurrentRow.Cells["city"].Value);
                txtState.Text = Convert.ToString(dataListado.CurrentRow.Cells["state"].Value);
            }
        }

        private void dataListado_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dataListado.Columns["Eliminar"].Index)
            {
                DataGridViewCheckBoxCell ChkEliminar = (DataGridViewCheckBoxCell)dataListado.Rows[e.RowIndex].Cells["Eliminar"];
                ChkEliminar.Value = !Convert.ToBoolean(ChkEliminar.Value);
            }
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
    }
}