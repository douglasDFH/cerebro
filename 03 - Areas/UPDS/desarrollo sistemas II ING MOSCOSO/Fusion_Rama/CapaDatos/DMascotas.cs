using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class DMascotas : DbConnection
    {
        private int _id;
        private string _nombre;
        private string _especie;
        private string _raza;
        private DateTime? _fechaNacimiento;
        private decimal? _peso;
        private string _color;
        private string _genero;
        private bool _esterilizado;
        private string _microchip;
        private int _personaId;
        private bool _activo;
        private string _textoBuscar;

        #region Propiedades
        public int Id { get => _id; set => _id = value; }
        public string Nombre { get => _nombre; set => _nombre = value; }
        public string Especie { get => _especie; set => _especie = value; }
        public string Raza { get => _raza; set => _raza = value; }
        public DateTime? FechaNacimiento { get => _fechaNacimiento; set => _fechaNacimiento = value; }
        public decimal? Peso { get => _peso; set => _peso = value; }
        public string Color { get => _color; set => _color = value; }
        public string Genero { get => _genero; set => _genero = value; }
        public bool Esterilizado { get => _esterilizado; set => _esterilizado = value; }
        public string Microchip { get => _microchip; set => _microchip = value; }
        public int PersonaId { get => _personaId; set => _personaId = value; }
        public bool Activo { get => _activo; set => _activo = value; }
        public string TextoBuscar { get => _textoBuscar; set => _textoBuscar = value; }
        #endregion

        #region Constructores
        public DMascotas()
        {
            _activo = true;
            _esterilizado = false;
        }

        public DMascotas(string nombre, string especie, int personaId)
        {
            Nombre = nombre;
            Especie = especie;
            PersonaId = personaId;
            Activo = true;
            Esterilizado = false;
        }

        public DMascotas(string nombre, string especie, int personaId, string raza = "", 
            DateTime? fechaNacimiento = null, decimal? peso = null, string color = "", 
            string genero = "", bool esterilizado = false, string microchip = "")
        {
            Nombre = nombre;
            Especie = especie;
            PersonaId = personaId;
            Raza = raza;
            FechaNacimiento = fechaNacimiento;
            Peso = peso;
            Color = color;
            Genero = genero;
            Esterilizado = esterilizado;
            Microchip = microchip;
            Activo = true;
        }
        #endregion

        #region Métodos CRUD

        public string Insertar(DMascotas mascota)
        {
            string rpta = string.Empty;
            using (SqlConnection connection = GetConnection())
            {
                connection.Open();
                try
                {
                    using (SqlCommand command = new SqlCommand("SP05_CreateOrUpdateAnimal", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@id", DBNull.Value);
                        command.Parameters.AddWithValue("@nombre", mascota.Nombre ?? "");
                        command.Parameters.AddWithValue("@especie", mascota.Especie ?? "");
                        command.Parameters.AddWithValue("@persona_id", mascota.PersonaId);
                        command.Parameters.AddWithValue("@raza", mascota.Raza ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@fecha_nacimiento", mascota.FechaNacimiento ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@peso", mascota.Peso ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@color", mascota.Color ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@genero", mascota.Genero ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@esterilizado", mascota.Esterilizado);
                        command.Parameters.AddWithValue("@microchip", mascota.Microchip ?? (object)DBNull.Value);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                int id = Convert.ToInt32(reader["id"]);
                                if (id > 0)
                                {
                                    mascota.Id = id;
                                    rpta = "OK";
                                }
                                else
                                {
                                    rpta = reader["mensaje"].ToString();
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    rpta = ex.Message;
                }
            }
            return rpta;
        }

        public string Editar(DMascotas mascota)
        {
            string rpta = string.Empty;
            using (SqlConnection connection = GetConnection())
            {
                connection.Open();
                try
                {
                    using (SqlCommand command = new SqlCommand("SP05_CreateOrUpdateAnimal", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@id", mascota.Id);
                        command.Parameters.AddWithValue("@nombre", mascota.Nombre ?? "");
                        command.Parameters.AddWithValue("@especie", mascota.Especie ?? "");
                        command.Parameters.AddWithValue("@persona_id", mascota.PersonaId);
                        command.Parameters.AddWithValue("@raza", mascota.Raza ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@fecha_nacimiento", mascota.FechaNacimiento ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@peso", mascota.Peso ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@color", mascota.Color ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@genero", mascota.Genero ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@esterilizado", mascota.Esterilizado);
                        command.Parameters.AddWithValue("@microchip", mascota.Microchip ?? (object)DBNull.Value);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                rpta = reader["mensaje"].ToString();
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    rpta = ex.Message;
                }
            }
            return rpta;
        }

        public string Eliminar(DMascotas mascota)
        {
            string rpta = string.Empty;
            using (SqlConnection connection = GetConnection())
            {
                connection.Open();
                try
                {
                    string query = "UPDATE animal SET activo = 0 WHERE id = @id";
                    
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@id", mascota.Id);
                        
                        int filasAfectadas = command.ExecuteNonQuery();
                        rpta = filasAfectadas > 0 ? "OK" : "No se encontró la mascota";
                    }
                }
                catch (Exception ex)
                {
                    rpta = ex.Message;
                }
            }
            return rpta;
        }

        public DataTable Mostrar()
        {
            DataTable dtResultado = new DataTable("Mascotas");
            using (SqlConnection connection = GetConnection())
            {
                connection.Open();
                try
                {
                    string query = @"SELECT a.id, a.nombre, a.especie, a.raza, a.fecha_nacimiento, 
                                          a.peso, a.color, a.genero, a.esterilizado, a.microchip, a.activo,
                                          CASE 
                                            WHEN p.nombre IS NOT NULL THEN CONCAT(p.nombre, ' ', ISNULL(p.apellido, ''))
                                            ELSE p.razon_social
                                          END as propietario,
                                          p.telefono as telefono_propietario
                                   FROM animal a
                                   INNER JOIN persona p ON a.persona_id = p.id
                                   WHERE a.activo = 1
                                   ORDER BY a.nombre";
                    
                    using (SqlCommand command = new SqlCommand(query, connection))
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(dtResultado);
                    }
                }
                catch
                {
                    dtResultado = null;
                }
            }
            return dtResultado;
        }

        public DataTable BuscarPorNombre(DMascotas mascota)
        {
            DataTable dtResultado = new DataTable("MascotasBusqueda");
            using (SqlConnection connection = GetConnection())
            {
                connection.Open();
                try
                {
                    string query = @"SELECT a.id, a.nombre, a.especie, a.raza, a.fecha_nacimiento, 
                                          a.peso, a.color, a.genero, a.esterilizado, a.microchip, a.activo,
                                          CASE 
                                            WHEN p.nombre IS NOT NULL THEN CONCAT(p.nombre, ' ', ISNULL(p.apellido, ''))
                                            ELSE p.razon_social
                                          END as propietario,
                                          p.telefono as telefono_propietario
                                   FROM animal a
                                   INNER JOIN persona p ON a.persona_id = p.id
                                   WHERE a.activo = 1 
                                     AND (a.nombre LIKE @textoBuscar 
                                          OR a.especie LIKE @textoBuscar
                                          OR a.raza LIKE @textoBuscar
                                          OR p.nombre LIKE @textoBuscar
                                          OR p.razon_social LIKE @textoBuscar
                                          OR a.microchip LIKE @textoBuscar)
                                   ORDER BY a.nombre";
                    
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@textoBuscar", "%" + (mascota.TextoBuscar ?? "") + "%");
                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            adapter.Fill(dtResultado);
                        }
                    }
                }
                catch
                {
                    dtResultado = null;
                }
            }
            return dtResultado;
        }

        public DataTable BuscarPorPropietario(int propietarioId)
        {
            DataTable dtResultado = new DataTable("MascotasPorPropietario");
            using (SqlConnection connection = GetConnection())
            {
                connection.Open();
                try
                {
                    string query = @"SELECT a.id, a.nombre as animal_nombre, a.especie, a.raza, 
                                          a.fecha_nacimiento, a.peso, a.color, a.genero, 
                                          a.esterilizado, a.microchip, a.activo
                                   FROM animal a 
                                   WHERE a.persona_id = @propietarioId AND a.activo = 1
                                   ORDER BY a.nombre";
                    
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@propietarioId", propietarioId);
                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            adapter.Fill(dtResultado);
                        }
                    }
                }
                catch
                {
                    dtResultado = null;
                }
            }
            return dtResultado;
        }

        public DataTable ObtenerPorId(DMascotas mascota)
        {
            DataTable dtResultado = new DataTable("MascotaPorId");
            using (SqlConnection connection = GetConnection())
            {
                connection.Open();
                try
                {
                    string query = @"SELECT a.id, a.nombre, a.especie, a.raza, a.fecha_nacimiento, 
                                          a.peso, a.color, a.genero, a.esterilizado, a.microchip, a.activo,
                                          CASE 
                                            WHEN p.nombre IS NOT NULL THEN CONCAT(p.nombre, ' ', ISNULL(p.apellido, ''))
                                            ELSE p.razon_social
                                          END as propietario,
                                          p.telefono as telefono_propietario, p.id as persona_id
                                   FROM animal a
                                   INNER JOIN persona p ON a.persona_id = p.id
                                   WHERE a.id = @id";
                    
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@id", mascota.Id);
                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            adapter.Fill(dtResultado);
                        }
                    }
                }
                catch
                {
                    dtResultado = null;
                }
            }
            return dtResultado;
        }

        public DataTable ObtenerEspecies()
        {
            DataTable dtResultado = new DataTable("Especies");
            using (SqlConnection connection = GetConnection())
            {
                connection.Open();
                try
                {
                    string query = @"SELECT DISTINCT especie FROM animal WHERE activo = 1 AND especie IS NOT NULL 
                                   ORDER BY especie";
                    
                    using (SqlCommand command = new SqlCommand(query, connection))
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(dtResultado);
                    }
                }
                catch
                {
                    dtResultado = null;
                }
            }
            return dtResultado;
        }

        public DataTable ObtenerRazasPorEspecie(string especie)
        {
            DataTable dtResultado = new DataTable("Razas");
            using (SqlConnection connection = GetConnection())
            {
                connection.Open();
                try
                {
                    string query = @"SELECT DISTINCT raza FROM animal 
                                   WHERE activo = 1 AND especie = @especie AND raza IS NOT NULL 
                                   ORDER BY raza";
                    
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@especie", especie);
                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            adapter.Fill(dtResultado);
                        }
                    }
                }
                catch
                {
                    dtResultado = null;
                }
            }
            return dtResultado;
        }

        public DataTable ObtenerHistorialClinico(int animalId)
        {
            DataTable dtResultado = new DataTable("HistorialClinico");
            using (SqlConnection connection = GetConnection())
            {
                connection.Open();
                try
                {
                    string query = @"SELECT dh.id, dh.tipo_evento, dh.fecha_evento, 
                                          dh.observaciones, dh.tratamiento, dh.medicamentos, 
                                          dh.dosis, dh.peso_animal, dh.temperatura, dh.costo,
                                          CONCAT(p.nombre, ' ', p.apellido) as veterinario
                                   FROM detalle_historico dh
                                   INNER JOIN historico h ON dh.historico_id = h.id
                                   LEFT JOIN personal_veterinario pv ON dh.veterinario_id = pv.id
                                   LEFT JOIN personal p ON pv.id = p.id
                                   WHERE h.animal_id = @animalId
                                   ORDER BY dh.fecha_evento DESC";
                    
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@animalId", animalId);
                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            adapter.Fill(dtResultado);
                        }
                    }
                }
                catch
                {
                    dtResultado = null;
                }
            }
            return dtResultado;
        }

        #endregion
    }
}