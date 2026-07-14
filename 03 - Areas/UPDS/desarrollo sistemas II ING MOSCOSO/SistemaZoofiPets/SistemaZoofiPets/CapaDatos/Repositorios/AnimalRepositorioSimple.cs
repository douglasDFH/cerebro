using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using CapaDatos.Entidades;

namespace CapaDatos.Repositorios
{
    public class AnimalRepositorioSimple
    {
        private readonly string connectionString;

        public AnimalRepositorioSimple()
        {
            connectionString = ConfigurationManager.ConnectionStrings["SistemaVeterinario"]?.ConnectionString
                             ?? "server=(localdb)\\MSSQLLocalDB;database=SistemaVeterinario;integrated security=true;TrustServerCertificate=true;";
        }

        public DataTable ObtenerAnimales()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string query = @"
                        SELECT 
                            1 as IdAnimal,
                            'Max' as NombreMascota,
                            'Perro' as Tipo,
                            'Labrador' as Raza,
                            3 as Edad,
                            25.5 as Peso,
                            'Dorado' as Color,
                            'Cliente Ejemplo' as NombrePropietario,
                            '555-0001' as Telefono,
                            'cliente@email.com' as Email,
                            GETDATE() as FechaRegistro";
                    
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
                throw new Exception($"Error al obtener animales: {ex.Message}");
            }
        }

        public DataTable ObtenerAnimalesPorPropietario(int idPropietario)
        {
            return ObtenerAnimales(); // Simplificado
        }

        public Animal ObtenerAnimalPorId(int idAnimal)
        {
            return new Animal
            {
                IdAnimal = idAnimal,
                IdPropietario = 1,
                Tipo = "Perro",
                Nombre = "Max",
                Edad = 3,
                Raza = "Labrador",
                Peso = 25.5m,
                Color = "Dorado",
                Observaciones = "Mascota ejemplo",
                FechaRegistro = DateTime.Now,
                NombrePropietario = "Cliente Ejemplo",
                TelefonoPropietario = "555-0001",
                EmailPropietario = "cliente@email.com",
                Activo = true
            };
        }

        public int CrearAnimal(Animal animal)
        {
            return 1; // Simular creación exitosa
        }

        public bool ActualizarAnimal(Animal animal)
        {
            return true; // Simular actualización exitosa
        }

        public bool EliminarAnimal(int idAnimal)
        {
            return true; // Simular eliminación exitosa
        }

        public DataTable BuscarAnimales(string criterio)
        {
            return ObtenerAnimales(); // Simplificado
        }

        public DataTable ObtenerTiposAnimales()
        {
            try
            {
                DataTable tabla = new DataTable();
                tabla.Columns.Add("Tipo", typeof(string));
                tabla.Rows.Add("Perro");
                tabla.Rows.Add("Gato");
                tabla.Rows.Add("Ave");
                tabla.Rows.Add("Roedor");
                return tabla;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener tipos de animales: {ex.Message}");
            }
        }
    }
}