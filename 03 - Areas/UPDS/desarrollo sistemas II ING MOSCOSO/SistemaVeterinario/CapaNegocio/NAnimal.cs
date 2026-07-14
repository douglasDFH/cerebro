using CapaDatos;
using System;
using System.Data;
using System.Linq;

namespace CapaNegocio
{
    public class NAnimal
    {
        public static string Insertar(int idPropietario, string nombre, string tipo, string raza = null,
            string color = null, char? sexo = null, DateTime? fechaNacimiento = null, decimal? peso = null,
            string microchip = null, string observaciones = null)
        {
            if (idPropietario <= 0)
                return "Debe seleccionar un propietario válido";

            if (string.IsNullOrWhiteSpace(nombre) || string.IsNullOrWhiteSpace(tipo))
                return "Nombre y tipo de animal son obligatorios";

            if (sexo.HasValue && !"MHI".Contains(sexo.Value))
                return "El sexo debe ser M (Macho), H (Hembra) o I (Indefinido)";

            if (peso.HasValue && peso.Value <= 0)
                return "El peso debe ser mayor a 0";

            if (fechaNacimiento.HasValue && fechaNacimiento.Value > DateTime.Now)
                return "La fecha de nacimiento no puede ser futura";

            DAnimal obj = new DAnimal();
            obj.IdPropietario = idPropietario;
            obj.Nombre = nombre;
            obj.Tipo = tipo;
            obj.Raza = raza;
            obj.Color = color;
            obj.Sexo = sexo;
            obj.FechaNacimiento = fechaNacimiento;
            obj.Peso = peso;
            obj.Microchip = microchip;
            obj.Observaciones = observaciones;
            obj.Estado = true;

            return obj.Insertar(obj);
        }

        public static string Editar(int idAnimal, string nombre, string tipo, string raza = null,
            string color = null, char? sexo = null, DateTime? fechaNacimiento = null, decimal? peso = null,
            decimal? altura = null, string microchip = null, string numPedigree = null,
            bool esterilizado = false, bool vacunado = false, string observaciones = null)
        {
            if (idAnimal <= 0)
                return "ID de animal no válido";

            if (string.IsNullOrWhiteSpace(nombre) || string.IsNullOrWhiteSpace(tipo))
                return "Nombre y tipo de animal son obligatorios";

            if (sexo.HasValue && !"MHI".Contains(sexo.Value))
                return "El sexo debe ser M (Macho), H (Hembra) o I (Indefinido)";

            if (peso.HasValue && peso.Value <= 0)
                return "El peso debe ser mayor a 0";

            if (altura.HasValue && altura.Value <= 0)
                return "La altura debe ser mayor a 0";

            if (fechaNacimiento.HasValue && fechaNacimiento.Value > DateTime.Now)
                return "La fecha de nacimiento no puede ser futura";

            DAnimal obj = new DAnimal();
            obj.IdAnimal = idAnimal;
            obj.Nombre = nombre;
            obj.Tipo = tipo;
            obj.Raza = raza;
            obj.Color = color;
            obj.Sexo = sexo;
            obj.FechaNacimiento = fechaNacimiento;
            obj.Peso = peso;
            obj.Altura = altura;
            obj.Microchip = microchip;
            obj.NumPedigree = numPedigree;
            obj.Esterilizado = esterilizado;
            obj.Vacunado = vacunado;
            obj.Observaciones = observaciones;

            return obj.Editar(obj);
        }

        public static string Eliminar(int idAnimal)
        {
            if (idAnimal <= 0)
                return "ID de animal no válido";

            DAnimal obj = new DAnimal();
            obj.IdAnimal = idAnimal;
            return obj.Eliminar(obj);
        }

        public static DataTable Mostrar()
        {
            return new DAnimal().Mostrar();
        }

        public static DataTable BuscarPorPropietario(int idPropietario)
        {
            if (idPropietario <= 0)
                return null;

            return new DAnimal().BuscarPorPropietario(idPropietario);
        }

        public static DataTable BuscarTexto(string textoBuscar)
        {
            DAnimal obj = new DAnimal();
            obj.TextoBuscar = textoBuscar;
            return obj.BuscarTexto(obj);
        }

        public static DataTable BuscarPorTipo(string tipo)
        {
            if (string.IsNullOrWhiteSpace(tipo))
                return null;

            return new DAnimal().BuscarPorTipo(tipo);
        }

        public static int CalcularEdad(DateTime fechaNacimiento)
        {
            DateTime hoy = DateTime.Today;
            int edad = hoy.Year - fechaNacimiento.Year;
            
            if (fechaNacimiento.Date > hoy.AddYears(-edad))
                edad--;
                
            return edad;
        }

        public static string[] GetTiposAnimales()
        {
            return new string[] 
            {
                "Perro", "Gato", "Conejo", "Hámster", "Loro", "Canario",
                "Pez", "Tortuga", "Iguana", "Hurón", "Chinchilla", "Cobaya",
                "Reptil", "Ave", "Otro"
            };
        }

        public static string[] GetRazasPerro()
        {
            return new string[]
            {
                "Pastor Alemán", "Labrador", "Golden Retriever", "Bulldog",
                "Chihuahua", "Yorkshire", "Poodle", "Boxer", "Husky",
                "Rottweiler", "Mestizo", "Otra"
            };
        }

        public static string[] GetRazasGato()
        {
            return new string[]
            {
                "Persa", "Siamés", "Maine Coon", "Ragdoll", "Bengalí",
                "Azul Ruso", "Británico", "Angora", "Mestizo", "Otra"
            };
        }

        public static bool ValidarMicrochip(string microchip)
        {
            if (string.IsNullOrWhiteSpace(microchip))
                return true; // Es opcional

            // Microchip típicamente tiene 15 dígitos
            return microchip.Length == 15 && microchip.All(char.IsDigit);
        }

        public static string GetDescripcionSexo(char? sexo)
        {
            if (!sexo.HasValue) return "No especificado";

            switch (sexo.Value)
            {
                case 'M': return "Macho";
                case 'H': return "Hembra";
                case 'I': return "Indefinido";
                default: return "No especificado";
            }
        }
    }
}