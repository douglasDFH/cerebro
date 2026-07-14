using System;
using System.Windows.Forms;
using CapaDatos;

namespace SistemVeterinario
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        static void Main()
        {
            try
            {
                // Inicializar la conexión singleton a la base de datos
                var dbInstance = DbConnection.Instance;

                // Ejecutar la aplicación
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Login());
            }
            catch (Exception ex)
            {
                DbConnection.CloseInstance();
                throw ex;
            }
            finally
            {
                DbConnection.CloseInstance();
                // Cerrar la conexión de base de datos al finalizar la aplicación
            }
        }
    }
}