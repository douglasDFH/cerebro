using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CapaNegocios;

namespace SistemaZoofiPets
{
    public partial class CapaPresentacion : Form
    {
        private readonly ConexionSQLN cn;

        public CapaPresentacion()
        {
            InitializeComponent();
            cn = new ConexionSQLN();
        }

        private void CargarImagenFondo()
        {
            try
            {
                // Intentar cargar desde el directorio de ejecución
                string imagePath = System.IO.Path.Combine(Application.StartupPath, "Resources", "login.png");
                if (System.IO.File.Exists(imagePath))
                {
                    this.pictureBoxFondo.Image = System.Drawing.Image.FromFile(imagePath);
                    return;
                }

                // Intentar cargar desde el directorio del proyecto (modo desarrollo)
                string projectPath = @"C:\Users\59168\Desktop\SistemaZoofiPets\SistemaZoofiPets\SistemaZoofiPets\Resources\login.png";
                if (System.IO.File.Exists(projectPath))
                {
                    this.pictureBoxFondo.Image = System.Drawing.Image.FromFile(projectPath);
                    return;
                }

                // Intentar cargar desde directorio relativo
                string relativePath = System.IO.Path.Combine(Environment.CurrentDirectory, "Resources", "login.png");
                if (System.IO.File.Exists(relativePath))
                {
                    this.pictureBoxFondo.Image = System.Drawing.Image.FromFile(relativePath);
                    return;
                }

                // Si no se encuentra la imagen, usar color de fondo
                this.pictureBoxFondo.BackColor = ColoresPastelGlobales.AzulSuave;
            }
            catch (Exception ex)
            {
                // En caso de error, usar color de fondo
                this.pictureBoxFondo.BackColor = ColoresPastelGlobales.AzulSuave;
                System.Diagnostics.Debug.WriteLine($"Error cargando imagen: {ex.Message}");
            }
        }

        private void btn_salir_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btn_ingresar_Click(object sender, EventArgs e)
        {
            try
            {
                // Validaciones básicas en UI
                if (string.IsNullOrWhiteSpace(txt_usuario.Text))
                {
                    MessageBox.Show("Por favor ingrese el usuario", "Campo Requerido", 
                                  MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txt_usuario.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(txt_contrasena.Text))
                {
                    MessageBox.Show("Por favor ingrese la contraseña", "Campo Requerido", 
                                  MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txt_contrasena.Focus();
                    return;
                }

                // Deshabilitar botón durante la validación
                btn_ingresar.Enabled = false;
                btn_ingresar.Text = "Validando...";

                int resultado = cn.ValidarLogin(txt_usuario.Text, txt_contrasena.Text);
                
                if (resultado == 1)
                {
                    MessageBox.Show("Bienvenido al Sistema Clínica Veterinaria", "Acceso Concedido", 
                                  MessageBoxButtons.OK, MessageBoxIcon.Information);
                
                    this.Hide();
                    
                    VentanaPrincipalModerna v1 = new VentanaPrincipalModerna();
                    v1.Nombre = txt_usuario.Text;
                    v1.Iduser = "1";
                    v1.Email = "admin@veterinaria.com";
                    v1.Show();
                }
                else
                {
                    MessageBox.Show("Usuario o contraseña incorrectos.\nVerifique sus credenciales e intente nuevamente.", 
                                  "Error de Autenticación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    
                    // Limpiar contraseña por seguridad
                    txt_contrasena.Clear();
                    txt_usuario.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al iniciar sesión:\n{ex.Message}", "Error", 
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
                
                // Limpiar campos en caso de error
                txt_contrasena.Clear();
                txt_usuario.Focus();
            }
            finally
            {
                // Restaurar botón
                btn_ingresar.Enabled = true;
                btn_ingresar.Text = "Ingresar";
            }
        }
    }
}
