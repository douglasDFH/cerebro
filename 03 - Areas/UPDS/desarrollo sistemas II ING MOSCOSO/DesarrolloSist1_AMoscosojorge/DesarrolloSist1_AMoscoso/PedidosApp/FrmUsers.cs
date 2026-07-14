using CapaNegocio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PedidosApp
{
    public partial class FrmUsers : Form
    {
        private bool EsNuevo = false;
        private bool EsEdita = false;
        public FrmUsers()
        {
            InitializeComponent();
            ttMensaje.SetToolTip(txtNombre, "Ingrese el nombre de la categoría");
            txtIdusuario.Enabled = false;
        }
        //Mostrar Mensaje de Confirmacion
        private void MensajeOK(string mensaje)
        {
            MessageBox.Show(mensaje, "Pedidos App", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
        }
        //Mostrar Mensaje de Error
        private void MensajeError(string mensaje)
        {
            MessageBox.Show(mensaje, "Pedidos App", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
        }
        //Habilitar los controles del formulario
        private void Habilitar(bool valor)
        {
            txtIdusuario.ReadOnly = !valor;
            txtNombre.ReadOnly = !valor;
            txtClave.ReadOnly = !valor;
            txtEmail.ReadOnly = !valor;
        }
        //Habilitar los botones
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
        //Metodo para ocultar columnas
        private void OcultarColumnas()
        {
            if (dataListado.RowCount > 0)
            {
                dataListado.Columns[0].Visible = false;
                dataListado.Columns[1].Visible = false;
            }
        }
        //Metodo Mostrar o Poblar de datos
        private void Mostrar()
        {
            dataListado.DataSource = Nusers.Mostrar();
            OcultarColumnas();
            lblTotal.Text = "Registros encontrados: " + Convert.ToString(dataListado.Rows.Count);
            tabControl1.SelectedIndex = 0;
        }
        private void BuscarUsuario()
        {
            dataListado.DataSource = Nusers.BuscarNombre(txtBuscar.Text);
            OcultarColumnas();
            lblTotal.Text = "Registros encontrados: " + Convert.ToString(dataListado.Rows.Count);
        }
        private void Limpiar()
        {
            txtIdusuario.Text = string.Empty;
            txtNombre.Text = string.Empty;
            txtClave.Text = string.Empty;
            txtEmail.Text = string.Empty;
        }
        private void FrmUsers_Load(object sender, EventArgs e)
        {
            Mostrar();
            Habilitar(false);
            Botones();
        }
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            BuscarUsuario();
        }
        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            BuscarUsuario();
        }
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            EsNuevo = true;
            EsEdita = false;
            Botones();
            Limpiar();
            Habilitar(true);
            txtNombre.Focus();
        }
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                string rpta = string.Empty;
                if (txtNombre.Text == string.Empty)
                {
                    MensajeError("Faltó ingresar algunos datos, serán marcados");
                    errorIcono.SetError(txtNombre, "Ingrese un nombre de usuario");
                    errorIcono.SetError(txtClave, "Ingrese una contraseña del usuario");
                    errorIcono.SetError(txtEmail, "Ingrese el correo elect. del usuario");
                }
                else
                {
                    if (EsNuevo)
                    {
                        rpta = Nusers.Insertar(txtNombre.Text.Trim(),txtClave.Text.Trim(),
                            txtEmail.Text.Trim());
                    }
                    else
                    {
                        rpta = Nusers.Editar(Convert.ToInt32(txtIdusuario.Text),
                            txtNombre.Text.Trim(), txtClave.Text.Trim(), txtEmail.Text.Trim());
                    }
                }
                if (rpta.Equals("OK"))
                {
                    if (EsNuevo) { MensajeOK("Se insertó correctamente el registro"); }
                    else { MensajeOK("Se actualizó correctamente el registro"); }
                }
                else { MensajeError(rpta); }
                EsNuevo = false;
                EsEdita = false;
                Botones();
                Limpiar();
                Mostrar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }
        private void dataListado_DoubleClick(object sender, EventArgs e)
        {
            txtIdusuario.Text = Convert.ToString(dataListado.CurrentRow.Cells["usuario_id"].Value);
            txtNombre.Text = Convert.ToString(dataListado.CurrentRow.Cells["nombre"].Value);
            txtClave.Text = Convert.ToString(dataListado.CurrentRow.Cells["clave"].Value);
            txtEmail.Text = Convert.ToString(dataListado.CurrentRow.Cells["email"].Value);
            tabControl1.SelectedIndex = 1;
        }
        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (!txtIdusuario.Text.Equals(""))
            {
                EsEdita = true;
                Botones();
                Habilitar(true);
                txtNombre.Focus();
            }
            else
            {
                MensajeError("Debe seleccionar primero una fila de la lista");
            }
        }
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            EsNuevo = false;
            EsEdita = false;
            Botones();
            Limpiar();
            Habilitar(false);
        }
        private void chkEliminar_CheckedChanged(object sender, EventArgs e)
        {
            if (chkEliminar.Checked) { dataListado.Columns[0].Visible = true; }
            else { dataListado.Columns[0].Visible = false; }
        }
        private void dataListado_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dataListado.Columns["Eliminar"].Index)
            {
                DataGridViewCheckBoxCell chkEliminar =
                    (DataGridViewCheckBoxCell)dataListado.Rows[e.RowIndex].Cells["Eliminar"];
                chkEliminar.Value = !Convert.ToBoolean(chkEliminar.Value);
            }
        }
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult opcion;
                opcion = MessageBox.Show("Realmente desea eliminar los registros",
                    "Pedidos App", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (opcion == DialogResult.OK)
                {
                    string codigo;
                    string rpta = string.Empty;
                    foreach (DataGridViewRow row in dataListado.Rows)
                    {
                        if (Convert.ToBoolean(row.Cells[0].Value))
                        {
                            codigo = Convert.ToString(row.Cells[1].Value);
                            rpta = Nusers.Eliminar(Convert.ToInt32(codigo));
                            if (rpta.Equals("OK")) { MensajeOK("Se elimino el registro"); }
                            else MensajeError(rpta);
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
        private void btnImprimir_Click(object sender, EventArgs e)
        {

        }
    }
}
