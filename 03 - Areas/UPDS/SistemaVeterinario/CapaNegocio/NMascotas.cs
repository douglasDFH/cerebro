using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using CapaDatos;

namespace CapaNegocio
{
    public class NMascotas
    {
        #region Métodos CRUD

        public static string Insertar(string nombre, string especie, int personaId,
            string raza = "", DateTime? fechaNacimiento = null, decimal? peso = null,
            string color = "", string genero = "", bool esterilizado = false, string microchip = "")
        {
            DMascotas objMascota = new DMascotas()
            {
                Nombre = nombre,
                Especie = especie,
                PersonaId = personaId,
                Raza = raza,
                FechaNacimiento = fechaNacimiento,
                Peso = peso,
                Color = color,
                Genero = genero,
                Esterilizado = esterilizado,
                Microchip = microchip
            };
            return objMascota.Insertar(objMascota);
        }

        public static string Editar(int id, string nombre, string especie, int personaId,
            string raza = "", DateTime? fechaNacimiento = null, decimal? peso = null,
            string color = "", string genero = "", bool esterilizado = false, string microchip = "")
        {
            DMascotas objMascota = new DMascotas()
            {
                Id = id,
                Nombre = nombre,
                Especie = especie,
                PersonaId = personaId,
                Raza = raza,
                FechaNacimiento = fechaNacimiento,
                Peso = peso,
                Color = color,
                Genero = genero,
                Esterilizado = esterilizado,
                Microchip = microchip
            };
            return objMascota.Editar(objMascota);
        }

        public static string Eliminar(int id)
        {
            DMascotas objMascota = new DMascotas()
            {
                Id = id
            };
            return objMascota.Eliminar(objMascota);
        }

        public static DataTable Mostrar()
        {
            return new DMascotas().Mostrar();
        }

        public static DataTable BuscarPorNombre(string textoBuscar)
        {
            DMascotas objMascota = new DMascotas()
            {
                TextoBuscar = textoBuscar
            };
            return objMascota.BuscarPorNombre(objMascota);
        }

        public static DataTable BuscarPorPropietario(int propietarioId)
        {
            return new DMascotas().BuscarPorPropietario(propietarioId);
        }

        public static DataTable ObtenerPorId(int id)
        {
            DMascotas objMascota = new DMascotas()
            {
                Id = id
            };
            return objMascota.ObtenerPorId(objMascota);
        }

        public static DataTable ObtenerEspecies()
        {
            return new DMascotas().ObtenerEspecies();
        }

        public static DataTable ObtenerRazasPorEspecie(string especie)
        {
            return new DMascotas().ObtenerRazasPorEspecie(especie);
        }

        public static DataTable ObtenerHistorialClinico(int animalId)
        {
            return new DMascotas().ObtenerHistorialClinico(animalId);
        }

        #endregion

        #region Validaciones de Negocio

        public static bool ValidarMascota(string nombre, string especie, int personaId)
        {
            if (string.IsNullOrWhiteSpace(nombre))
                return false;

            if (string.IsNullOrWhiteSpace(especie))
                return false;

            if (personaId <= 0)
                return false;

            return true;
        }

        public static bool ValidarPeso(decimal? peso)
        {
            if (peso.HasValue && peso.Value <= 0)
                return false;

            return true;
        }

        public static bool ValidarGenero(string genero)
        {
            if (string.IsNullOrWhiteSpace(genero))
                return true; // Género es opcional

            return genero == "M" || genero == "F";
        }

        public static bool ValidarMicrochip(string microchip)
        {
            if (string.IsNullOrWhiteSpace(microchip))
                return true; // Microchip es opcional

            // Validar longitud típica de microchip (15 dígitos)
            if (microchip.Length > 50)
                return false;

            return true;
        }

        public static string ValidarDatosMascota(string nombre, string especie, int personaId,
            decimal? peso, string genero, string microchip, DateTime? fechaNacimiento)
        {
            var errores = new List<string>();

            if (string.IsNullOrWhiteSpace(nombre))
                errores.Add("El nombre de la mascota es requerido");

            if (string.IsNullOrWhiteSpace(especie))
                errores.Add("La especie es requerida");

            if (personaId <= 0)
                errores.Add("Debe seleccionar un propietario válido");

            if (!ValidarPeso(peso))
                errores.Add("El peso debe ser mayor a 0");

            if (!ValidarGenero(genero))
                errores.Add("El género debe ser 'M' o 'F'");

            if (!ValidarMicrochip(microchip))
                errores.Add("El microchip no tiene un formato válido");

            if (fechaNacimiento.HasValue && fechaNacimiento.Value > DateTime.Now)
                errores.Add("La fecha de nacimiento no puede ser futura");

            if (!string.IsNullOrWhiteSpace(nombre) && nombre.Length > 100)
                errores.Add("El nombre no puede tener más de 100 caracteres");

            if (!string.IsNullOrWhiteSpace(especie) && especie.Length > 50)
                errores.Add("La especie no puede tener más de 50 caracteres");

            return errores.Any() ? string.Join(", ", errores) : "";
        }

        public static bool ExisteMascota(string nombre, int propietarioId)
        {
            try
            {
                DataTable mascotas = BuscarPorPropietario(propietarioId);
                if (mascotas != null)
                {
                    foreach (DataRow row in mascotas.Rows)
                    {
                        if ((row["animal_nombre"]?.ToString() ?? "").Equals(nombre, StringComparison.OrdinalIgnoreCase))
                            return true;
                    }
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        public static int CalcularEdad(DateTime? fechaNacimiento)
        {
            if (!fechaNacimiento.HasValue)
                return 0;

            DateTime fechaActual = DateTime.Now;
            int edad = fechaActual.Year - fechaNacimiento.Value.Year;

            if (fechaActual.Month < fechaNacimiento.Value.Month ||
                (fechaActual.Month == fechaNacimiento.Value.Month && fechaActual.Day < fechaNacimiento.Value.Day))
            {
                edad--;
            }

            return edad;
        }

        public static string ObtenerEdadTexto(DateTime? fechaNacimiento)
        {
            if (!fechaNacimiento.HasValue)
                return "Edad no especificada";

            int edadAnios = CalcularEdad(fechaNacimiento);

            if (edadAnios < 1)
            {
                int edadMeses = (DateTime.Now.Year - fechaNacimiento.Value.Year) * 12 +
                               DateTime.Now.Month - fechaNacimiento.Value.Month;

                if (edadMeses < 1)
                {
                    int edadDias = (DateTime.Now - fechaNacimiento.Value).Days;
                    return $"{edadDias} días";
                }

                return edadMeses == 1 ? "1 mes" : $"{edadMeses} meses";
            }

            return edadAnios == 1 ? "1 año" : $"{edadAnios} años";
        }

        public static List<string> ObtenerEspeciesComunes()
        {
            return new List<string>
            {
                "Perro",
                "Gato",
                "Conejo",
                "Hamster",
                "Cobayo",
                "Hurón",
                "Ave",
                "Pez",
                "Reptil",
                "Otro"
            };
        }

        public static List<string> ObtenerRazasComunes(string especie)
        {
            switch (especie?.ToLower())
            {
                case "perro":
                    return new List<string>
                    {
                        "Mestizo", "Labrador", "Golden Retriever", "Pastor Alemán", "Bulldog",
                        "Chihuahua", "Poodle", "Beagle", "Rottweiler", "Yorkshire Terrier",
                        "Boxer", "Dálmata", "Husky Siberiano", "Cocker Spaniel", "Shih Tzu"
                    };

                case "gato":
                    return new List<string>
                    {
                        "Mestizo", "Persa", "Siamés", "Angora", "Maine Coon",
                        "Ragdoll", "British Shorthair", "Bengalí", "Abisinio", "Ruso Azul"
                    };

                case "conejo":
                    return new List<string>
                    {
                        "Holandés", "Angora", "Cabeza de León", "Mini Lop", "Holland Lop"
                    };

                case "ave":
                    return new List<string>
                    {
                        "Canario", "Periquito", "Cacatúa", "Loro", "Ninfa", "Agapornis"
                    };

                default:
                    return new List<string>();
            }
        }

        #endregion

        #region Estadísticas y Reportes

        public static DataTable ObtenerEstadisticasPorEspecie()
        {
            DataTable dtResultado = new DataTable("EstadisticasPorEspecie");
            try
            {
                DataTable mascotas = Mostrar();
                if (mascotas != null)
                {
                    var estadisticas = mascotas.AsEnumerable()
                        .GroupBy(row => row.Field<string>("especie"))
                        .Select(g => new
                        {
                            Especie = g.Key ?? "Sin especificar",
                            Cantidad = g.Count(),
                            Machos = g.Count(r => r.Field<string>("genero") == "M"),
                            Hembras = g.Count(r => r.Field<string>("genero") == "F"),
                            Esterilizados = g.Count(r => r.Field<bool?>("esterilizado") == true)
                        })
                        .OrderByDescending(x => x.Cantidad);

                    dtResultado.Columns.Add("Especie", typeof(string));
                    dtResultado.Columns.Add("Cantidad", typeof(int));
                    dtResultado.Columns.Add("Machos", typeof(int));
                    dtResultado.Columns.Add("Hembras", typeof(int));
                    dtResultado.Columns.Add("Esterilizados", typeof(int));

                    foreach (var item in estadisticas)
                    {
                        dtResultado.Rows.Add(item.Especie, item.Cantidad, item.Machos, item.Hembras, item.Esterilizados);
                    }
                }
            }
            catch (Exception ex)
            {
                dtResultado = new DataTable("ObtenerEstadisticasPorEspecie");
                System.Diagnostics.Debug.WriteLine($"Error: {ex.Message}");
            }
            return dtResultado;
        }

        public static int ContarMascotasActivas()
        {
            try
            {
                var connection = DbConnection.Instance.GetConnection();
                using (var command = new SqlCommand("SP_ContarMascotasActivas", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    var result = command.ExecuteScalar();
                    return Convert.ToInt32(result);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error contando mascotas activas: {ex.Message}");
                return 0;
            }
        }

        public static int ContarMascotasPorPropietario(int propietarioId)
        {
            try
            {
                DataTable mascotas = BuscarPorPropietario(propietarioId);
                return mascotas?.Rows.Count ?? 0;
            }
            catch
            {
                return 0;
            }
        }

        #endregion
    }
}