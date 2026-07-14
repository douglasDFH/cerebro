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
    public partial class FrmLogin : Form
    {
        public FrmLogin()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            DataTable datos = Nusers.Login(txtUsuario.Text.Trim(), txtPassword.Text.Trim());
            try
            {
                if (datos.Rows.Count == 0)
                {
                    MessageBox.Show("Usuario o contraseña invalidos", "Pedidos App", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    FrmPrincipal frm = new FrmPrincipal();
                    frm.Iduser = datos.Rows[0]["usuario_id"].ToString();
                    frm.Nombre = datos.Rows[0]["nombre"].ToString();
                    frm.Email = datos.Rows[0]["email"].ToString();

                    frm.Show();
                    Hide();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
