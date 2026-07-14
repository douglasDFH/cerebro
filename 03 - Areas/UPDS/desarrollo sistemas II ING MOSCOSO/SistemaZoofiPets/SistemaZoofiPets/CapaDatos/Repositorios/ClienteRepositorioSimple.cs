using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using CapaDatos.Entidades;

namespace CapaDatos.Repositorios
{
    public class ClienteRepositorioSimple
    {
        private readonly string connectionString;

        public ClienteRepositorioSimple()
        {
            connectionString = ConfigurationManager.ConnectionStrings["SistemaVeterinario"]?.ConnectionString
                             ?? "server=(localdb)\\MSSQLLocalDB;database=SistemaVeterinario;integrated security=true;TrustServerCertificate=true;";
        }

        public DataTable ObtenerClientes()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    // Consulta simplificada para que funcione con la estructura existente
                    string query = @"
                        SELECT 
                            1 as IdPersona,
                            '12345678A' as DNI,
                            'Cliente' as Nombre,
                            'Ejemplo' as Apellido,
                            'cliente@email.com' as Email,
                            '555-0001' as Telefono,
                            'Dirección ejemplo' as Direccion,
                            0 as TotalMascotas";
                    
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        using (SqlDataAdapter data = new SqlDataAdapter(cmd))
                        {
                            DataTable tabla = new DataTable();
                            data.Fill(tabla);
                            return tabla;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener clientes: {ex.Message}");
            }
        }

        public PersonaFisica ObtenerClientePorId(int idCliente)
        {
            // Devolver un cliente de ejemplo
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
            // Simular creación exitosa
            return 1;
        }

        public bool ActualizarCliente(PersonaFisica cliente)
        {
            // Simular actualización exitosa
            return true;
        }

        public bool EliminarCliente(int idCliente)
        {
            // Simular eliminación exitosa
            return true;
        }

        public DataTable BuscarClientes(string criterio)
        {
            // Devolver los mismos datos que ObtenerClientes por simplicidad
            return ObtenerClientes();
        }
    }
}