using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using CapaDatos.Entidades;

namespace CapaDatos.Repositorios
{
    public class AnimalRepositorio
    {
        private readonly string connectionString;

        public AnimalRepositorio()
        {
            connectionString = ConfigurationManager.ConnectionStrings["SistemaVeterinario"]?.ConnectionString
                             ?? "server=(localdb)\\MSSQLLocalDB;database=SistemaVeterinario;integrated security=true;TrustServerCertificate=true;";
        }

        public DataTable ObtenerAnimales()
        {
            try
            {
                DataTable tabla = new DataTable();
                tabla.Columns.Add("IdAnimal", typeof(int));
                tabla.Columns.Add("NombreMascota", typeof(string));
                tabla.Columns.Add("Tipo", typeof(string));
                tabla.Columns.Add("Raza", typeof(string));
                tabla.Columns.Add("Edad", typeof(int));
                tabla.Columns.Add("Peso", typeof(decimal));
                tabla.Columns.Add("Color", typeof(string));
                tabla.Columns.Add("NombrePropietario", typeof(string));
                tabla.Columns.Add("Telefono", typeof(string));
                tabla.Columns.Add("Email", typeof(string));
                tabla.Columns.Add("FechaRegistro", typeof(DateTime));

                // Datos de ejemplo
                tabla.Rows.Add(1, "Max", "Perro", "Labrador", 3, 25.5, "Dorado", "Cliente Ejemplo", "555-0001", "cliente@email.com", DateTime.Now);
                tabla.Rows.Add(2, "Luna", "Gato", "Persa", 2, 4.2, "Blanco", "Cliente Ejemplo", "555-0001", "cliente@email.com", DateTime.Now);

                return tabla;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener animales: {ex.Message}");
            }
        }

        public DataTable ObtenerAnimalesPorPropietario(int idPropietario)
        {
            return ObtenerAnimales();
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
            return 1;
        }

        public bool ActualizarAnimal(Animal animal)
        {
            return true;
        }

        public bool EliminarAnimal(int idAnimal)
        {
            return true;
        }

        public DataTable BuscarAnimales(string criterio)
        {
            return ObtenerAnimales();
        }

        public DataTable ObtenerTiposAnimales()
        {
            DataTable tabla = new DataTable();
            tabla.Columns.Add("Tipo", typeof(string));
            tabla.Rows.Add("Perro");
            tabla.Rows.Add("Gato");
            tabla.Rows.Add("Ave");
            tabla.Rows.Add("Roedor");
            return tabla;
        }
    }
}