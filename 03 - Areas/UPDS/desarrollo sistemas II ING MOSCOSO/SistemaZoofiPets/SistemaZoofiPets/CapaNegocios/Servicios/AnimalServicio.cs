using System;
using System.Data;
using System.Text.RegularExpressions;
using CapaDatos.Repositorios;
using CapaDatos.Entidades;

namespace CapaNegocios.Servicios
{
    public class AnimalServicio
    {
        private readonly AnimalRepositorio animalRepo;
        private readonly ClienteRepositorio clienteRepo;

        public AnimalServicio()
        {
            animalRepo = new AnimalRepositorio();
            clienteRepo = new ClienteRepositorio();
        }

        public DataTable ConsultarAnimales()
        {
            try
            {
                return animalRepo.ObtenerAnimales();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al consultar animales: {ex.Message}");
            }
        }

        public DataTable ConsultarAnimalesPorPropietario(int idPropietario)
        {
            try
            {
                if (idPropietario <= 0)
                    throw new ArgumentException("ID de propietario no válido");

                return animalRepo.ObtenerAnimalesPorPropietario(idPropietario);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al consultar animales del propietario: {ex.Message}");
            }
        }

        public Animal ObtenerAnimal(int idAnimal)
        {
            try
            {
                if (idAnimal <= 0)
                    throw new ArgumentException("ID de animal no válido");

                return animalRepo.ObtenerAnimalPorId(idAnimal);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener animal: {ex.Message}");
            }
        }

        public int RegistrarAnimal(int idPropietario, string tipo, string nombre, int? edad, 
            string raza, decimal? peso, string color, string observaciones)
        {
            try
            {
                ValidarDatosAnimal(idPropietario, tipo, nombre, edad, peso);

                var animal = new Animal
                {
                    IdPropietario = idPropietario,
                    Tipo = tipo.Trim(),
                    Nombre = nombre.Trim(),
                    Edad = edad,
                    Raza = string.IsNullOrWhiteSpace(raza) ? null : raza.Trim(),
                    Peso = peso,
                    Color = string.IsNullOrWhiteSpace(color) ? null : color.Trim(),
                    Observaciones = string.IsNullOrWhiteSpace(observaciones) ? null : observaciones.Trim(),
                    FechaRegistro = DateTime.Now,
                    Activo = true
                };

                return animalRepo.CrearAnimal(animal);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al registrar animal: {ex.Message}");
            }
        }

        public bool ModificarAnimal(int idAnimal, string tipo, string nombre, int? edad, 
            string raza, decimal? peso, string color, string observaciones)
        {
            try
            {
                if (idAnimal <= 0)
                    throw new ArgumentException("ID de animal no válido");

                ValidarDatosAnimal(0, tipo, nombre, edad, peso); // idPropietario = 0 para modificación

                var animal = new Animal
                {
                    IdAnimal = idAnimal,
                    Tipo = tipo.Trim(),
                    Nombre = nombre.Trim(),
                    Edad = edad,
                    Raza = string.IsNullOrWhiteSpace(raza) ? null : raza.Trim(),
                    Peso = peso,
                    Color = string.IsNullOrWhiteSpace(color) ? null : color.Trim(),
                    Observaciones = string.IsNullOrWhiteSpace(observaciones) ? null : observaciones.Trim()
                };

                return animalRepo.ActualizarAnimal(animal);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al modificar animal: {ex.Message}");
            }
        }

        public bool EliminarAnimal(int idAnimal)
        {
            try
            {
                if (idAnimal <= 0)
                    throw new ArgumentException("ID de animal no válido");

                return animalRepo.EliminarAnimal(idAnimal);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al eliminar animal: {ex.Message}");
            }
        }

        public DataTable BuscarAnimales(string criterio)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(criterio))
                    return ConsultarAnimales();

                return animalRepo.BuscarAnimales(criterio.Trim());
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al buscar animales: {ex.Message}");
            }
        }

        public DataTable ObtenerTiposAnimales()
        {
            try
            {
                return animalRepo.ObtenerTiposAnimales();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener tipos de animales: {ex.Message}");
            }
        }

        private void ValidarDatosAnimal(int idPropietario, string tipo, string nombre, int? edad, decimal? peso)
        {
            // Validar propietario solo si es un registro nuevo
            if (idPropietario > 0)
            {
                var cliente = clienteRepo.ObtenerClientePorId(idPropietario);
                if (cliente == null)
                    throw new ArgumentException("El propietario especificado no existe");
            }

            if (string.IsNullOrWhiteSpace(tipo))
                throw new ArgumentException("El tipo de animal es requerido");

            if (string.IsNullOrWhiteSpace(nombre))
                throw new ArgumentException("El nombre del animal es requerido");

            // Validar tipo de animal
            string[] tiposValidos = { "Perro", "Gato", "Ave", "Roedor", "Reptil", "Pez", "Otro" };
            if (Array.IndexOf(tiposValidos, tipo.Trim()) == -1)
                throw new ArgumentException("Tipo de animal no válido");

            // Validar nombre (letras, números y espacios)
            if (!Regex.IsMatch(nombre.Trim(), @"^[a-zA-ZáéíóúÁÉÍÓÚñÑ0-9\s]+$"))
                throw new ArgumentException("El nombre del animal solo puede contener letras, números y espacios");

            // Validar edad
            if (edad.HasValue && (edad.Value < 0 || edad.Value > 50))
                throw new ArgumentException("La edad debe estar entre 0 y 50 años");

            // Validar peso
            if (peso.HasValue && (peso.Value <= 0 || peso.Value > 200))
                throw new ArgumentException("El peso debe estar entre 0.1 y 200 kg");
        }

        public string[] ObtenerTiposAnimalesArray()
        {
            return new string[] { "Perro", "Gato", "Ave", "Roedor", "Reptil", "Pez", "Otro" };
        }

        public bool ValidarPropietario(int idPropietario)
        {
            try
            {
                if (idPropietario <= 0)
                    return false;

                var cliente = clienteRepo.ObtenerClientePorId(idPropietario);
                return cliente != null;
            }
            catch
            {
                return false;
            }
        }
    }
}