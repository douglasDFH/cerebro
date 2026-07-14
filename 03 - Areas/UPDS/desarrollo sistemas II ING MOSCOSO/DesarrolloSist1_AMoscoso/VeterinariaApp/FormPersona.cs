using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using CapaNegocio;

namespace VeterinariaApp
{
    public partial class FormPersona : Form
    {
        private bool IsNuevo = false;
        private bool IsEditar = false;

        public FormPersona()
        {
            InitializeComponent();
            this.ttMensaje.SetToolTip(this.txtEmail, "Ingrese el Email de la Persona");
            this.ttMensaje.SetToolTip(this.txtDireccion, "Ingrese la Dirección de la Persona");
            this.ttMensaje.SetToolTip(this.txtTelefono, "Ingrese el Teléfono de la Persona");
        }

        private void FormPersona_Load(object sender, EventArgs e)
        {
            this.Mostrar();
            this.Habilitar(false);
            this.Botones();
        }

        private void Mostrar()
        {
            this.dataListado.DataSource = NPersona.Mostrar();
            this.lblTotal.Text = "Total Registros: " + Convert.ToString(dataListado.Rows.Count);
        }

        private void Buscar()
        {
            this.dataListado.DataSource = NPersona.BuscarEmail(this.txtBuscar.Text);
            this.lblTotal.Text = "Total Registros: " + Convert.ToString(dataListado.Rows.Count);
        }

        private void Habilitar(bool valor)
        {
            this.txtEmail.ReadOnly = !valor;
            this.txtDireccion.ReadOnly = !valor;
            this.txtTelefono.ReadOnly = !valor;
        }

        private void Limpiar()
        {
            this.txtEmail.Text = string.Empty;
            this.txtDireccion.Text = string.Empty;
            this.txtTelefono.Text = string.Empty;
        }

        private void Botones()
        {
            if (this.IsNuevo || this.IsEditar)
            {
                this.Habilitar(true);
                this.btnNuevo.Enabled = false;
                this.btnGuardar.Enabled = true;
                this.btnEditar.Enabled = false;
                this.btnCancelar.Enabled = true;
            }
            else
            {
                this.Habilitar(false);
                this.btnNuevo.Enabled = true;
                this.btnGuardar.Enabled = false;
                this.btnEditar.Enabled = true;
                this.btnCancelar.Enabled = false;
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            this.Buscar();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult Opcion;
                Opcion = MessageBox.Show("¿Desea eliminar los registros seleccionados?", "Sistema Veterinaria", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

                if (Opcion == DialogResult.OK)
                {
                    string Codigo;
                    string Respuesta = "";

                    foreach (DataGridViewRow row in dataListado.Rows)
                    {
                        if (Convert.ToBoolean(row.Cells[0].Value))
                        {
                            Codigo = Convert.ToString(row.Cells[1].Value);
                            Respuesta = NPersona.Eliminar(Convert.ToInt32(Codigo));

                            if (Respuesta.Equals("OK"))
                            {
                                this.Mostrar();
                            }
                            else
                            {
                                MessageBox.Show(Respuesta);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void chkEliminar_CheckedChanged(object sender, EventArgs e)
        {
            if (chkEliminar.Checked)
            {
                this.dataListado.Columns[0].Visible = true;
            }
            else
            {
                this.dataListado.Columns[0].Visible = false;
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

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            this.IsNuevo = true;
            this.IsEditar = false;
            this.Botones();
            this.Limpiar();
            this.Habilitar(true);
            this.txtEmail.Focus();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                string respuesta = "";
                if (this.txtEmail.Text == string.Empty || this.txtTelefono.Text == string.Empty)
                {
                    MessageBox.Show("Los campos con (*) son obligatorios", "Sistema Veterinaria", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    if (this.IsNuevo)
                    {
                        respuesta = NPersona.Insertar(this.txtEmail.Text.Trim(),
                                                     this.txtDireccion.Text.Trim(),
                                                     this.txtTelefono.Text.Trim());
                    }
                    else
                    {
                        respuesta = NPersona.Editar(Convert.ToInt32(this.txtIdCliente.Text),
                                                  this.txtEmail.Text.Trim(),
                                                  this.txtDireccion.Text.Trim(),
                                                  this.txtTelefono.Text.Trim());
                    }

                    if (respuesta.Equals("OK"))
                    {
                        if (this.IsNuevo)
                        {
                            MessageBox.Show("Registro guardado correctamente", "Sistema Veterinaria", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Registro actualizado correctamente", "Sistema Veterinaria", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    else
                    {
                        MessageBox.Show(respuesta, "Sistema Veterinaria", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    this.IsNuevo = false;
                    this.IsEditar = false;
                    this.Botones();
                    this.Limpiar();
                    this.Mostrar();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (!this.txtIdCliente.Text.Equals(""))
            {
                this.IsEditar = true;
                this.Botones();
                this.Habilitar(true);
            }
            else
            {
                MessageBox.Show("Debe seleccionar un registro para editar", "Sistema Veterinaria", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.IsNuevo = false;
            this.IsEditar = false;
            this.Botones();
            this.Habilitar(false);
            this.Limpiar();
        }

        private void dataListado_DoubleClick(object sender, EventArgs e)
        {
            this.txtIdCliente.Text = Convert.ToString(this.dataListado.CurrentRow.Cells["persona_id"].Value);
            this.txtEmail.Text = Convert.ToString(this.dataListado.CurrentRow.Cells["email"].Value);
            this.txtDireccion.Text = Convert.ToString(this.dataListado.CurrentRow.Cells["direccion"].Value);
            this.txtTelefono.Text = Convert.ToString(this.dataListado.CurrentRow.Cells["telefono"].Value);

            this.tabControl1.SelectedIndex = 1;
        }

        private void lblTotal_Click(object sender, EventArgs e)
        {

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void chkEliminar_CheckedChanged_1(object sender, EventArgs e)
        {

        }

        private void btnEliminar_Click_1(object sender, EventArgs e)
        {

        }

        private void btnBuscar_Click_1(object sender, EventArgs e)
        {

        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataListado_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void txtTelefono_TextChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void txtDireccion_TextChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void txtEmail_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void txtIdCliente_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void btnCancelar_Click_1(object sender, EventArgs e)
        {

        }

        private void btnEditar_Click_1(object sender, EventArgs e)
        {

        }

        private void btnGuardar_Click_1(object sender, EventArgs e)
        {

        }

        private void btnNuevo_Click_1(object sender, EventArgs e)
        {

        }

        private void ttMensaje_Popup(object sender, PopupEventArgs e)
        {

        }
    }
}