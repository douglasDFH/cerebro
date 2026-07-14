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

namespace VeterinariaApp
{
    public partial class FrmUsers : Form
    {
        private bool EsNuevo = false;
        private bool EsEdita = false;
        public FrmUsers()
        {
            try
            {
                InitializeComponent();
                
                // Verificar si los controles existen, si no, crearlos temporalmente
                if (cmbRol == null)
                {
                    cmbRol = new System.Windows.Forms.ComboBox();
                    tabPage2.Controls.Add(cmbRol);
                }
                
                if (cmbPersonal == null)
                {
                    cmbPersonal = new System.Windows.Forms.ComboBox();
                    tabPage2.Controls.Add(cmbPersonal);
                }
                
                if (chkActivo == null)
                {
                    chkActivo = new System.Windows.Forms.CheckBox();
                    tabPage2.Controls.Add(chkActivo);
                }
                
                if (chkBloqueado == null)
                {
                    chkBloqueado = new System.Windows.Forms.CheckBox();
                    tabPage2.Controls.Add(chkBloqueado);
                }
                
                ttMensaje.SetToolTip(txtNombre, "Ingrese el nombre de la categoría");
                txtIdusuario.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error en constructor FrmUsers: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }
        //Mostrar Mensaje de Confirmacion
        private void MensajeOK(string mensaje)
        {
            MessageBox.Show(mensaje, "Sistema Veterinario", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
        }
        //Mostrar Mensaje de Error
        private void MensajeError(string mensaje)
        {
            MessageBox.Show(mensaje, "Sistema Veterinario", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
        }
        //Habilitar los controles del formulario
        private void Habilitar(bool valor)
        {
            txtIdusuario.ReadOnly = !valor;
            txtNombre.ReadOnly = !valor;
            txtClave.ReadOnly = !valor;
            txtEmail.ReadOnly = !valor;
            cmbRol.Enabled = valor;
            cmbPersonal.Enabled = valor;
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
            dataListado.DataSource = NUsuario.Mostrar();
            OcultarColumnas();
            lblTotal.Text = "Registros encontrados: " + Convert.ToString(dataListado.Rows.Count);
            tabControl1.SelectedIndex = 0;
        }
        private void BuscarUsuario()
        {
            dataListado.DataSource = NUsuario.BuscarPorNombre(txtBuscar.Text);
            OcultarColumnas();
            lblTotal.Text = "Registros encontrados: " + Convert.ToString(dataListado.Rows.Count);
        }
        private void Limpiar()
        {
            txtIdusuario.Text = string.Empty;
            txtNombre.Text = string.Empty;
            txtClave.Text = string.Empty;
            txtEmail.Text = string.Empty;
            cmbRol.SelectedIndex = 2; // AUXILIAR por defecto
            cmbPersonal.SelectedIndex = -1;
            chkActivo.Checked = true;
            chkBloqueado.Checked = false;
        }
        private void FrmUsers_Load(object sender, EventArgs e)
        {
            try
            {
                CargarRoles();
                CargarPersonal();
                Mostrar();
                Habilitar(false);
                Botones();
            }
            catch (Exception ex)
            {
                MensajeError("Error al cargar formulario de usuarios: " + ex.Message + "\n\nStackTrace: " + ex.StackTrace);
            }
        }

        private void CargarRoles()
        {
            cmbRol.Items.Clear();
            cmbRol.Items.AddRange(new string[] { "ADMIN", "VETERINARIO", "AUXILIAR", "RECEPCIONISTA" });
            cmbRol.SelectedIndex = 2; // AUXILIAR por defecto
        }

        private void CargarPersonal()
        {
            try
            {
                cmbPersonal.Items.Clear();
                cmbPersonal.Items.Add(new { Text = "-- Sin asignar --", Value = (int?)null });
                // Aquí se cargaría el personal desde la base de datos
                // Por ahora solo agregamos la opción sin asignar
                cmbPersonal.DisplayMember = "Text";
                cmbPersonal.ValueMember = "Value";
                cmbPersonal.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MensajeError("Error al cargar personal: " + ex.Message);
            }
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
                
                // Limpiar errores previos
                errorIcono.Clear();
                
                // Validar campos obligatorios
                if (string.IsNullOrWhiteSpace(txtNombre.Text) || 
                    string.IsNullOrWhiteSpace(txtClave.Text) || 
                    string.IsNullOrWhiteSpace(txtEmail.Text))
                {
                    MensajeError("Todos los campos marcados con (*) son obligatorios");
                    if (string.IsNullOrWhiteSpace(txtNombre.Text))
                        errorIcono.SetError(txtNombre, "Ingrese un nombre de usuario");
                    if (string.IsNullOrWhiteSpace(txtClave.Text))
                        errorIcono.SetError(txtClave, "Ingrese una contraseña");
                    if (string.IsNullOrWhiteSpace(txtEmail.Text))
                        errorIcono.SetError(txtEmail, "Ingrese un correo electrónico");
                    return;
                }

                // Validar formato de email
                if (!IsValidEmail(txtEmail.Text))
                {
                    MensajeError("El formato del email no es válido");
                    errorIcono.SetError(txtEmail, "Formato de email inválido");
                    return;
                }

                // Validar longitud de contraseña
                if (txtClave.Text.Length < 6)
                {
                    MensajeError("La contraseña debe tener al menos 6 caracteres");
                    errorIcono.SetError(txtClave, "Mínimo 6 caracteres");
                    return;
                }

                string rol = cmbRol.SelectedItem?.ToString() ?? "AUXILIAR";
                int? idPersonal = cmbPersonal.SelectedValue as int?;

                if (EsNuevo)
                {
                    rpta = NUsuario.Insertar(txtNombre.Text.Trim(), txtClave.Text.Trim(),
                        txtEmail.Text.Trim(), rol, idPersonal);
                }
                else
                {
                    rpta = NUsuario.Editar(Convert.ToInt32(txtIdusuario.Text),
                        txtNombre.Text.Trim(), txtClave.Text.Trim(), 
                        txtEmail.Text.Trim(), rol, idPersonal);
                }

                if (rpta.Equals("OK"))
                {
                    if (EsNuevo) 
                        MensajeOK("Usuario creado exitosamente");
                    else 
                        MensajeOK("Usuario actualizado exitosamente");
                    
                    EsNuevo = false;
                    EsEdita = false;
                    Botones();
                    Limpiar();
                    Mostrar();
                }
                else 
                {
                    MensajeError(rpta);
                }
            }
            catch (Exception ex)
            {
                MensajeError("Error al guardar: " + ex.Message);
            }
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
        private void dataListado_DoubleClick(object sender, EventArgs e)
        {
            if (dataListado.CurrentRow != null)
            {
                txtIdusuario.Text = Convert.ToString(dataListado.CurrentRow.Cells["IdUsuario"].Value);
                txtNombre.Text = Convert.ToString(dataListado.CurrentRow.Cells["NombreUsuario"].Value);
                txtClave.Text = ""; // No mostrar contraseña por seguridad
                txtEmail.Text = Convert.ToString(dataListado.CurrentRow.Cells["Email"].Value);
                
                string rol = Convert.ToString(dataListado.CurrentRow.Cells["Rol"].Value);
                cmbRol.SelectedItem = rol;
                
                bool estado = Convert.ToBoolean(dataListado.CurrentRow.Cells["Estado"].Value);
                chkActivo.Checked = estado;
                
                bool bloqueado = Convert.ToBoolean(dataListado.CurrentRow.Cells["Bloqueado"].Value);
                chkBloqueado.Checked = bloqueado;
                
                tabControl1.SelectedIndex = 1;
            }
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
            /*try
            {
                DialogResult opcion;
                opcion = MessageBox.Show("Realmente desea elliminar los registros",
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
                            rpta = Ncategories.Eliminar(Convert.ToInt32(codigo));
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
            }*/
        }
        private void btnImprimir_Click(object sender, EventArgs e)
        {
            // Implementar funcionalidad de impresión si es necesario
        }

        private void btnDesbloquear_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtIdusuario.Text))
                {
                    MensajeError("Debe seleccionar un usuario");
                    return;
                }

                DialogResult opcion = MessageBox.Show("¿Está seguro de desbloquear este usuario?",
                    "Sistema Veterinario", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (opcion == DialogResult.Yes)
                {
                    string rpta = NUsuario.DesbloquearUsuario(Convert.ToInt32(txtIdusuario.Text));
                    if (rpta.Equals("OK"))
                    {
                        MensajeOK("Usuario desbloqueado exitosamente");
                        Mostrar();
                    }
                    else
                    {
                        MensajeError(rpta);
                    }
                }
            }
            catch (Exception ex)
            {
                MensajeError("Error al desbloquear usuario: " + ex.Message);
            }
        }

        private void btnCambiarClave_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtIdusuario.Text))
                {
                    MensajeError("Debe seleccionar un usuario");
                    return;
                }

                FrmCambiarClave frmCambiar = new FrmCambiarClave();
                frmCambiar.IdUsuario = Convert.ToInt32(txtIdusuario.Text);
                frmCambiar.NombreUsuario = txtNombre.Text;
                
                if (frmCambiar.ShowDialog() == DialogResult.OK)
                {
                    MensajeOK("Contraseña cambiada exitosamente");
                }
            }
            catch (Exception ex)
            {
                MensajeError("Error al cambiar contraseña: " + ex.Message);
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
