using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class DMascotas
    {
        private int _id;
        private string _nombre = string.Empty;
        private string _especie = string.Empty;
        private string _raza = string.Empty;
        private DateTime? _fechaNacimiento;
        private decimal? _peso;
        private string _color = string.Empty;
        private string _genero = string.Empty;
        private bool _esterilizado;
        private string _microchip = string.Empty;
        private int _personaId;
        private bool _activo;
        private string _textoBuscar = string.Empty;

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


        // Método para verificar si el procedimiento almacenado existe
        private bool VerificarProcedimientoExiste()
        {
            SqlConnection connection = DbConnection.Instance.GetConnection();
            try
            {
                string query = "SELECT COUNT(*) FROM sys.procedures WHERE name = 'SP05_CreateOrUpdateAnimal'";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    int count = Convert.ToInt32(command.ExecuteScalar());
                    return count > 0;
                }
            }
            catch
            {
                return false;
            }
        }

        // Método para crear el procedimiento almacenado si no existe
        private bool CrearProcedimientoAlmacenado()
        {
            SqlConnection connection = DbConnection.Instance.GetConnection();
            try
            {
                string createSP = @"
                CREATE PROCEDURE SP05_CreateOrUpdateAnimal
                    @id INT = NULL,
                    @nombre VARCHAR(100),
                    @especie VARCHAR(50),
                    @persona_id INT,
                    @raza VARCHAR(100) = NULL,
                    @fecha_nacimiento DATE = NULL,
                    @peso DECIMAL(5,2) = NULL,
                    @color VARCHAR(50) = NULL,
                    @genero CHAR(1) = NULL,
                    @esterilizado BIT = 0,
                    @microchip VARCHAR(50) = NULL
                AS
                BEGIN
                    SET NOCOUNT ON;
                    DECLARE @mensaje VARCHAR(255);
                    DECLARE @nuevo_id INT = 0;

                    BEGIN TRY
                        IF @id IS NULL OR @id = 0
                        BEGIN
                            -- Insertar nuevo animal
                            INSERT INTO animal (nombre, especie, persona_id, raza, fecha_nacimiento, peso, color, genero, esterilizado, microchip, activo)
                            VALUES (@nombre, @especie, @persona_id, @raza, @fecha_nacimiento, @peso, @color, @genero, @esterilizado, @microchip, 1);
                            
                            SET @nuevo_id = SCOPE_IDENTITY();
                            SET @mensaje = 'Animal insertado exitosamente';
                        END
                        ELSE
                        BEGIN
                            -- Actualizar animal existente
                            UPDATE animal 
                            SET nombre = @nombre,
                                especie = @especie,
                                persona_id = @persona_id,
                                raza = @raza,
                                fecha_nacimiento = @fecha_nacimiento,
                                peso = @peso,
                                color = @color,
                                genero = @genero,
                                esterilizado = @esterilizado,
                                microchip = @microchip
                            WHERE id = @id;
                            
                            SET @nuevo_id = @id;
                            SET @mensaje = 'Animal actualizado exitosamente';
                        END

                        SELECT @nuevo_id as id, @mensaje as mensaje;
                    END TRY
                    BEGIN CATCH
                        SELECT 0 as id, ERROR_MESSAGE() as mensaje;
                    END CATCH
                END";

                using (SqlCommand command = new SqlCommand(createSP, connection))
                {
                    command.ExecuteNonQuery();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        public string Insertar(DMascotas mascota)
        {
            string rpta = string.Empty;
            SqlConnection connection = DbConnection.Instance.GetConnection();
            try
            {
                // Verificar si el procedimiento almacenado existe, si no, crearlo
                if (!VerificarProcedimientoExiste())
                {
                    if (!CrearProcedimientoAlmacenado())
                    {
                        return "Error: No se pudo crear el procedimiento almacenado SP05_CreateOrUpdateAnimal";
                    }
                }

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
                            // Debug: Verificar qué columnas devuelve el procedimiento
                            System.Diagnostics.Debug.WriteLine($"Columnas devueltas por SP05_CreateOrUpdateAnimal:");
                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                System.Diagnostics.Debug.WriteLine($"  {reader.GetName(i)}: {reader.GetValue(i)}");
                            }

                            int id = Convert.ToInt32(reader["id"]);
                            string mensaje = reader["mensaje"]?.ToString() ?? "";

                            System.Diagnostics.Debug.WriteLine($"SP05 devolvió - ID: {id}, Mensaje: {mensaje}");

                            if (id > 0)
                            {
                                mascota.Id = id;
                                rpta = "OK";
                                System.Diagnostics.Debug.WriteLine($"Mascota insertada exitosamente with ID: {id}");
                            }
                            else
                            {
                                rpta = mensaje;
                                System.Diagnostics.Debug.WriteLine($"Error del procedimiento: {mensaje}");
                            }
                        }
                        else
                        {
                            rpta = "El procedimiento no devolvió ningún resultado";
                            System.Diagnostics.Debug.WriteLine("SP05_CreateOrUpdateAnimal no devolvió ningún resultado");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                rpta = $"Error al insertar mascota: {ex.Message}";
            }
            return rpta;
        }

        public string Editar(DMascotas mascota)
        {
            string rpta = string.Empty;
            SqlConnection connection = DbConnection.Instance.GetConnection();
            try
            {
                // Verificar si el procedimiento almacenado existe, si no, crearlo
                if (!VerificarProcedimientoExiste())
                {
                    if (!CrearProcedimientoAlmacenado())
                    {
                        return "Error: No se pudo crear el procedimiento almacenado SP05_CreateOrUpdateAnimal";
                    }
                }

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
                            rpta = reader["mensaje"]?.ToString() ?? "No se recibió mensaje del procedimiento";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                rpta = ex.Message;
            }
            return rpta;
        }

        public string Eliminar(DMascotas mascota)
        {
            string rpta = string.Empty;
            SqlConnection connection = DbConnection.Instance.GetConnection();
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
            return rpta;
        }

        public DataTable Mostrar()
        {
            DataTable dtResultado = new DataTable("Mascotas");
            SqlConnection connection = DbConnection.Instance.GetConnection();
            try
            {
                // Usar la vista VW_AnimalesConPropietario para simplificar el query
                string query = @"SELECT id, animal_nombre as nombre, especie, raza, fecha_nacimiento, 
                                        peso, color, genero, esterilizado, microchip, activo,
                                        propietario, telefono_propietario
                                 FROM VW_AnimalesConPropietario 
                                 WHERE activo = 1
                                 ORDER BY animal_nombre";

                using (SqlCommand command = new SqlCommand(query, connection))
                using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                {
                    adapter.Fill(dtResultado);
                }
            }
            catch (Exception ex)
            {
                dtResultado = new DataTable();
                System.Diagnostics.Debug.WriteLine($"Error en Mostrar: {ex.Message}");
            }
            return dtResultado;
        }

        public DataTable BuscarPorNombre(DMascotas mascota)
        {
            DataTable dtResultado = new DataTable("MascotasBusqueda");
            SqlConnection connection = DbConnection.Instance.GetConnection();
            try
            {
                // Usar la vista VW_AnimalesConPropietario para simplificar la búsqueda
                string query = @"SELECT id, animal_nombre as nombre, especie, raza, fecha_nacimiento, 
                                        peso, color, genero, esterilizado, microchip, activo,
                                        propietario, telefono_propietario
                                 FROM VW_AnimalesConPropietario 
                                 WHERE activo = 1 
                                   AND (animal_nombre LIKE @textoBuscar 
                                        OR especie LIKE @textoBuscar
                                        OR raza LIKE @textoBuscar
                                        OR propietario LIKE @textoBuscar
                                        OR microchip LIKE @textoBuscar)
                                 ORDER BY animal_nombre";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@textoBuscar", "%" + (mascota.TextoBuscar ?? "") + "%");
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(dtResultado);
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error en BuscarPorNombre: {ex.Message}");
                dtResultado = new DataTable();
            }
                return dtResultado;
        }

        public DataTable BuscarPorPropietario(int propietarioId)
        {
            DataTable dtResultado = new DataTable("MascotasPorPropietario");
            SqlConnection connection = DbConnection.Instance.GetConnection();
            try
            {
                // Podemos usar la vista o la consulta directa, ambas funcionan bien
                // Para este caso específico, mantenemos la consulta directa ya que es eficiente
                string query = @"SELECT id, animal_nombre as nombre, especie, raza, 
                                        fecha_nacimiento, peso, color, genero, 
                                        esterilizado, microchip, activo
                                 FROM VW_AnimalesConPropietario 
                                 WHERE tipo_propietario IS NOT NULL 
                                   AND activo = 1
                                   AND id IN (SELECT id FROM animal WHERE persona_id = @propietarioId)
                                 ORDER BY animal_nombre";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@propietarioId", propietarioId);
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(dtResultado);
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error en BuscarPorPropietario: {ex.Message}");
                dtResultado = new DataTable();
            }
            return dtResultado;
        }

        public DataTable ObtenerPorId(DMascotas mascota)
        {
            DataTable dtResultado = new DataTable("MascotaPorId");
            SqlConnection connection = DbConnection.Instance.GetConnection();
            try
            {
                // Verificar y recrear el procedimiento almacenado para usar la vista
                if (!VerificarProcedimientoObtenerPorId())
                {
                    if (!CrearProcedimientoObtenerPorId())
                    {
                        System.Diagnostics.Debug.WriteLine("No se pudo crear el procedimiento SP_ObtenerAnimalPorId");
                        return new DataTable();
                    }
                }
                else
                {
                    // Recrear el procedimiento para asegurar que use la vista
                    RecrearProcedimientoSiExiste();
                }

                // Usar el procedimiento almacenado
                using (SqlCommand command = new SqlCommand("SP_ObtenerAnimalPorId", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@id", mascota.Id);

                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(dtResultado);
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error en DMascotas.ObtenerPorId - ID: {mascota.Id}, Error: {ex.Message}");
                dtResultado = new DataTable();
            }
            return dtResultado;
        }

        private bool VerificarProcedimientoObtenerPorId()
        {
            SqlConnection connection = DbConnection.Instance.GetConnection();
            try
            {
                string query = "SELECT COUNT(*) FROM sys.procedures WHERE name = 'SP_ObtenerAnimalPorId'";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    int count = (int)command.ExecuteScalar();
                    return count > 0;
                }
            }
            catch
            {
                return false;
            }
        }

        private void RecrearProcedimientoSiExiste()
        {
            SqlConnection connection = DbConnection.Instance.GetConnection();
            try
            {
                // Eliminar el procedimiento si existe
                string dropSP = "IF EXISTS (SELECT 1 FROM sys.procedures WHERE name = 'SP_ObtenerAnimalPorId') DROP PROCEDURE SP_ObtenerAnimalPorId";
                using (SqlCommand command = new SqlCommand(dropSP, connection))
                {
                    command.ExecuteNonQuery();
                }
                
                // Recrear el procedimiento
                CrearProcedimientoObtenerPorId();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error recreando procedimiento: {ex.Message}");
            }
        }

        private bool CrearProcedimientoObtenerPorId()
        {
            SqlConnection connection = DbConnection.Instance.GetConnection();
            try
            {
                string createSP = @"
                CREATE PROCEDURE SP_ObtenerAnimalPorId
                    @id INT
                AS
                BEGIN
                    SET NOCOUNT ON;
                    
                    SELECT 
                        id, 
                        animal_nombre as nombre, 
                        especie, 
                        raza, 
                        fecha_nacimiento, 
                        peso, 
                        color, 
                        genero, 
                        esterilizado, 
                        microchip, 
                        activo, 
                        tipo_propietario,
                        propietario,
                        telefono_propietario,
                        -- Agregar persona_id para compatibilidad
                        (SELECT persona_id FROM animal WHERE id = @id) as persona_id
                    FROM VW_AnimalesConPropietario
                    WHERE id = @id
                END";

                using (SqlCommand command = new SqlCommand(createSP, connection))
                {
                    command.ExecuteNonQuery();
                    return true;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error creando procedimiento SP_ObtenerAnimalPorId: {ex.Message}");
                return false;
            }
        }

        public DataTable ObtenerEspecies()
        {
            DataTable dtResultado = new DataTable("Especies");
            SqlConnection connection = DbConnection.Instance.GetConnection();
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
                dtResultado = new DataTable();
            }
            return dtResultado;
        }

        public DataTable ObtenerRazasPorEspecie(string especie)
        {
            DataTable dtResultado = new DataTable("Razas");
            SqlConnection connection = DbConnection.Instance.GetConnection();
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
                dtResultado = new DataTable();
            }
            return dtResultado;
        }

        public DataTable ObtenerHistorialClinico(int animalId)
        {
            DataTable dtResultado = new DataTable("HistorialClinico");
            SqlConnection connection = DbConnection.Instance.GetConnection();
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
                dtResultado = new DataTable();
            }
            return dtResultado;
        }

        #endregion
    }
}