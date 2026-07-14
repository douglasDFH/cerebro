using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using CapaDatos.Entidades;

namespace CapaDatos.Repositorios
{
    public class ClienteRepositorio
    {
        private readonly string connectionString;

        public ClienteRepositorio()
        {
            connectionString = ConfigurationManager.ConnectionStrings["SistemaVeterinario"]?.ConnectionString
                             ?? "server=(localdb)\\MSSQLLocalDB;database=SistemaVeterinario;integrated security=true;TrustServerCertificate=true;";
        }

        public DataTable ObtenerClientes()
        {
            try
            {
                DataTable tabla = new DataTable();
                tabla.Columns.Add("IdPersona", typeof(int));
                tabla.Columns.Add("DNI", typeof(string));
                tabla.Columns.Add("Nombre", typeof(string));
                tabla.Columns.Add("Apellido", typeof(string));
                tabla.Columns.Add("Email", typeof(string));
                tabla.Columns.Add("Telefono", typeof(string));
                tabla.Columns.Add("Direccion", typeof(string));
                tabla.Columns.Add("TotalMascotas", typeof(int));

                // Datos de ejemplo
                tabla.Rows.Add(1, "12345678A", "Ana", "Martínez", "ana@email.com", "555-1001", "Calle 10 #123", 2);
                tabla.Rows.Add(2, "23456789B", "Carlos", "López", "carlos@email.com", "555-1002", "Avenida 20 #456", 1);
                tabla.Rows.Add(3, "34567890C", "Sofía", "Rodríguez", "sofia@email.com", "555-1003", "Carrera 30 #789", 1);

                return tabla;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener clientes: {ex.Message}");
            }
        }

        public PersonaFisica ObtenerClientePorId(int idCliente)
        {
            return new PersonaFisica
            {
                IdPersona = idCliente,
                DNI = "12345678A",
                Nombre = "Cliente",
                Apellido = "Ejemplo",
                Email = "cliente@email.com",
                Telefono = "555-0001",
                Direccion = "Dirección ejemplo",
                TipoPersona = "Fisica",
                Activo = true
            };
        }

        public int CrearCliente(PersonaFisica cliente)
        {
            return 1; // Simular creación exitosa
        }

        public bool ActualizarCliente(PersonaFisica cliente)
        {
            return true; // Simular actualización exitosa
        }

        public bool EliminarCliente(int idCliente)
        {
            return true; // Simular eliminación exitosa
        }

        public DataTable BuscarClientes(string criterio)
        {
            return ObtenerClientes(); // Simplificado
        }
    }
}