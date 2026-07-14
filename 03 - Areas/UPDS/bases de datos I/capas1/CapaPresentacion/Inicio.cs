
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using CapaEntidad;
using FontAwesome.Sharp;
using CapaNegocio;

namespace CapaPresentacion
{
    public partial class Inicio : Form
    {
        private static Usuario usuarioActual;
        private static IconMenuItem MenuActivo = null;
        private static Form FormularioActivo = null;

        public Inicio(Usuario objusuario)
        {
            usuarioActual = objusuario;

            InitializeComponent();

        }

        private void Inicio_Load(object sender, EventArgs e)
        {
            List<Permiso> listaPermisos = new CN_Permiso().Listar(usuarioActual.IdUsuario);

            foreach (IconMenuItem iconmenu in menu.Items)
            {
                bool encontrado = listaPermisos.Any(m => m.NombreMenu == iconmenu.Name);
                //iconmenu.Visible = true; // Make all items visible initially
                if(encontrado == false)
               // if (!listaPermisos.Any(m => m.NombreMenu == iconmenu.Name))
                {
                    iconmenu.Visible = false; // Hide only if no permission found
                }
            }

            lblusuario.Text = usuarioActual.NombreCompleto; 

        }

       
        private void AbrirFormulario(IconMenuItem menu, Form formulario)
        {
            if (MenuActivo != null)
            {
                MenuActivo.BackColor = Color.DarkRed;
            }
            menu.BackColor = Color.Red;
            MenuActivo = menu;

            if (FormularioActivo != null)
            {
                FormularioActivo.Close();
            }
            FormularioActivo = formulario;
            formulario.TopLevel = false;
            formulario.FormBorderStyle = FormBorderStyle.None;
            formulario.Dock = DockStyle.Fill;
            formulario.BackColor = Color.OrangeRed;

            Contenedor.Controls.Add(formulario);
            formulario.Show();

        }

       

        
       

        private void submenucategoria_Click(object sender, EventArgs e)
        {
            AbrirFormulario(MenuMantenedor, new FrmCategoria());
        }

        private void submenuproducto_Click(object sender, EventArgs e)
        {
            AbrirFormulario(MenuMantenedor, new FrmProducto());
        }

        private void submenuregistrar_Click(object sender, EventArgs e)
        {
            AbrirFormulario(MenuVentas, new FrmVenrtas());
        }

        private void submenuverdetalleventa_Click(object sender, EventArgs e)
        {
            AbrirFormulario(MenuVentas, new FrmDetalleVenta());
        }

        private void submenuverdetallecompra_Click(object sender, EventArgs e)
        {
            AbrirFormulario(MenuCompras, new FrmDetalleCompra());
        }

        private void submenuregistrarcompra_Click(object sender, EventArgs e)
        {

            AbrirFormulario(MenuCompras, new frmCompras());
        }

        private void MenuClientes_Click(object sender, EventArgs e)
        {
            AbrirFormulario((IconMenuItem)sender, new frmClientes());
        }

        private void MenuProveedores_Click(object sender, EventArgs e)
        {
            AbrirFormulario((IconMenuItem)sender, new FrmProveedores());
        }

        private void MenuReportes_Click(object sender, EventArgs e)
        {
            AbrirFormulario((IconMenuItem)sender, new FrmReporte());
        }

        private void MenuUsuario_Click(object sender, EventArgs e)
        {
            AbrirFormulario((IconMenuItem)sender, new FrmUsuario());
        }

       
    }
}
