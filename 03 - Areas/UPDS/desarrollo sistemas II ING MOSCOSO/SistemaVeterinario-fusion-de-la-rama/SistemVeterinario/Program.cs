using CapaDatos;

namespace SistemVeterinario
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
                // To customize application configuration such as set high DPI settings or default font,
                // see https://aka.ms/applicationconfiguration.
                ApplicationConfiguration.Initialize();

                // Inicializar la conexión singleton a la base de datos
                var dbInstance = DbConnection.Instance;

                // Ejecutar la aplicación
                Application.Run(new Login());
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error fatal en la aplicación: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                // Cerrar la conexión de base de datos al finalizar la aplicación
                DbConnection.CloseInstance();
            }
        }
    }
}