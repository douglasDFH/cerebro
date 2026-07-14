using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VeterinariaApp
{
    internal static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            
            try
            {
                // Mostrar formulario de login
                FrmLogin loginForm = new FrmLogin();
                
                if (loginForm.ShowDialog() == DialogResult.OK)
                {
                    // Si el login es exitoso, abrir formulario principal con datos del usuario
                    FrmPrincipal formPrincipal = new FrmPrincipal(loginForm.UsuarioActual);
                    Application.Run(formPrincipal);
                }
                else
                {
                    // Si cancela el login, no abrir la aplicación
                    Application.Exit();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al iniciar la aplicación: {ex.Message}", 
                    "Sistema Veterinario", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
