using System;
using System.Data;
using Microsoft.Data.SqlClient;

namespace CapaDatos
{
    public class DPersonal
    {
        // Propiedades comunes del personal
        private int _id;
        private string _nombre = string.Empty;
        private string _apellido = string.Empty;
        private string _email = string.Empty;
        private string _usuario = string.Empty;
        private string _contrasena = string.Empty;
        private string _telefono = string.Empty;
        private string _direccion = string.Empty;
        private DateTime _fechaContratacion;
        private decimal? _salario;
        private string _rol = "Usuario";
        private bool _activo;
        private DateTime? _fechaUltimoAcceso;
        private string _creadoPor = "Sistema";
        private string _modificadoPor = string.Empty;
        private DateTime? _fechaModificacion;

        // Propiedades específicas de veterinario
        private string _numLicencia = string.Empty;
        private string _especialidad = string.Empty;
        private string _universidad = string.Empty;
        private int? _aniosExperiencia;

        // Propiedades específicas de auxiliar
        private string _area;
        private string _turno = "Mañana";
        private string _nivel = "Básico";

        // Propiedades auxiliares
        private string _textoBuscar = string.Empty;
        private string _tipoPersonal = "Auxiliar";

        // Propiedades públicas
        public int Id { get => _id; set => _id = value; }
        public string Nombre { get => _nombre; set => _nombre = value; }
        public string Apellido { get => _apellido; set => _apellido = value; }
        public string Email { get => _email; set => _email = value; }
        public string Usuario { get => _usuario; set => _usuario = value; }
        public string Contrasena { get => _contrasena; set => _contrasena = value; }
        public string Telefono { get => _telefono; set => _telefono = value; }
        public string Direccion { get => _direccion; set => _direccion = value; }
        public DateTime FechaContratacion { get => _fechaContratacion; set => _fechaContratacion = value; }
        public decimal? Salario { get => _salario; set => _salario = value; }
        public string Rol { get => _rol; set => _rol = value; }
        public bool Activo { get => _activo; set => _activo = value; }
        public DateTime? FechaUltimoAcceso { get => _fechaUltimoAcceso; set => _fechaUltimoAcceso = value; }
        public string CreadoPor { get => _creadoPor; set => _creadoPor = value; }
        public string ModificadoPor { get => _modificadoPor; set => _modificadoPor = value; }
        public DateTime? FechaModificacion { get => _fechaModificacion; set => _fechaModificacion = value; }

        // Veterinario
        public string NumLicencia { get => _numLicencia; set => _numLicencia = value; }
        public string Especialidad { get => _especialidad; set => _especialidad = value; }
        public string Universidad { get => _universidad; set => _universidad = value; }
        public int? AniosExperiencia { get => _aniosExperiencia; set => _aniosExperiencia = value; }

        // Auxiliar
        public string Area { get => _area; set => _area = value; }
        public string Turno { get => _turno; set => _turno = value; }
        public string Nivel { get => _nivel; set => _nivel = value; }

        // Auxiliares
        public string TextoBuscar { get => _textoBuscar; set => _textoBuscar = value; }
        public string TipoPersonal { get => _tipoPersonal; set => _tipoPersonal = value; }

        // Constructor
        public DPersonal()
        {
            _fechaContratacion = DateTime.Now;
            _activo = true;
        }

        // Método para insertar personal auxiliar
        public string InsertarPersonalAuxiliar(DPersonal personal)
        {
            string rpta = string.Empty;
            SqlConnection connection = DbConnection.Instance.GetConnection();

            try
            {
                using (SqlCommand command = new SqlCommand("SP_InsertarPersonalAuxiliar", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@nombre", personal.Nombre);
                    command.Parameters.AddWithValue("@apellido", personal.Apellido);
                    command.Parameters.AddWithValue("@email", personal.Email);
                    command.Parameters.AddWithValue("@usuario", personal.Usuario);
                    command.Parameters.AddWithValue("@contrasena", personal.Contrasena);
                    command.Parameters.AddWithValue("@telefono", personal.Telefono ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@direccion", personal.Direccion ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@fecha_contratacion", personal.FechaContratacion);
                    command.Parameters.AddWithValue("@salario", personal.Salario ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@rol", personal.Rol);
                    command.Parameters.AddWithValue("@area", personal.Area ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@turno", personal.Turno);
                    command.Parameters.AddWithValue("@nivel", personal.Nivel);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            int id = Convert.ToInt32(reader["id"]);
                            if (id > 0)
                            {
                                personal.Id = id;
                                rpta = "OK";
                            }
                            else
                            {
                                rpta = reader["mensaje"]?.ToString() ?? "Error desconocido";
                            }
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

        // Método para insertar personal veterinario
        public string InsertarPersonalVeterinario(DPersonal personal)
        {
            string rpta = string.Empty;
            SqlConnection connection = DbConnection.Instance.GetConnection();

            try
            {
                using (SqlCommand command = new SqlCommand("SP_InsertarPersonalVeterinario", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@nombre", personal.Nombre);
                    command.Parameters.AddWithValue("@apellido", personal.Apellido);
                    command.Parameters.AddWithValue("@email", personal.Email);
                    command.Parameters.AddWithValue("@usuario", personal.Usuario);
                    command.Parameters.AddWithValue("@contrasena", personal.Contrasena);
                    command.Parameters.AddWithValue("@telefono", personal.Telefono ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@direccion", personal.Direccion ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@fecha_contratacion", personal.FechaContratacion);
                    command.Parameters.AddWithValue("@salario", personal.Salario ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@rol", personal.Rol);
                    command.Parameters.AddWithValue("@num_licencia", personal.NumLicencia ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@especialidad", personal.Especialidad ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@universidad", personal.Universidad ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@anios_experiencia", personal.AniosExperiencia ?? (object)DBNull.Value);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            int id = Convert.ToInt32(reader["id"]);
                            if (id > 0)
                            {
                                personal.Id = id;
                                rpta = "OK";
                            }
                            else
                            {
                                rpta = reader["mensaje"]?.ToString() ?? "Error desconocido";
                            }
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

        // Método para editar personal
        public string Editar(DPersonal personal)
        {
            string rpta = string.Empty;

            if (personal.TipoPersonal == "Veterinario")
            {
                rpta = EditarPersonalVeterinario(personal);
            }
            else if (personal.TipoPersonal == "Auxiliar")
            {
                rpta = EditarPersonalAuxiliar(personal);
            }
            else
            {
                rpta = "Tipo de personal no válido";
            }

            return rpta;
        }

        private string EditarPersonalVeterinario(DPersonal personal)
        {
            string rpta = string.Empty;
            SqlConnection connection = DbConnection.Instance.GetConnection();

            try
            {
                using (SqlCommand command = new SqlCommand("SP_EditarPersonalVeterinario", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@id", personal.Id);
                    command.Parameters.AddWithValue("@nombre", personal.Nombre);
                    command.Parameters.AddWithValue("@apellido", personal.Apellido);
                    command.Parameters.AddWithValue("@email", personal.Email);
                    command.Parameters.AddWithValue("@usuario", personal.Usuario);
                    command.Parameters.AddWithValue("@telefono", personal.Telefono ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@direccion", personal.Direccion ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@salario", personal.Salario ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@rol", personal.Rol);
                    command.Parameters.AddWithValue("@num_licencia", personal.NumLicencia ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@especialidad", personal.Especialidad ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@universidad", personal.Universidad ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@anios_experiencia", personal.AniosExperiencia ?? (object)DBNull.Value);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            rpta = reader["mensaje"]?.ToString() ?? "Actualizado correctamente";
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

        private string EditarPersonalAuxiliar(DPersonal personal)
        {
            string rpta = string.Empty;
            SqlConnection connection = DbConnection.Instance.GetConnection();

            try
            {
                using (SqlCommand command = new SqlCommand("SP_EditarPersonalAuxiliar", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@id", personal.Id);
                    command.Parameters.AddWithValue("@nombre", personal.Nombre);
                    command.Parameters.AddWithValue("@apellido", personal.Apellido);
                    command.Parameters.AddWithValue("@email", personal.Email);
                    command.Parameters.AddWithValue("@usuario", personal.Usuario);
                    command.Parameters.AddWithValue("@telefono", personal.Telefono ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@direccion", personal.Direccion ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@salario", personal.Salario ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@rol", personal.Rol);
                    command.Parameters.AddWithValue("@area", personal.Area ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@turno", personal.Turno);
                    command.Parameters.AddWithValue("@nivel", personal.Nivel);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            rpta = reader["mensaje"]?.ToString() ?? "Actualizado correctamente";
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

        // Método para eliminar personal (cambiar estado a inactivo)
        public string Eliminar(DPersonal personal)
        {
            string rpta = string.Empty;
            SqlConnection connection = DbConnection.Instance.GetConnection();

            try
            {
                using (SqlCommand command = new SqlCommand("SP_EliminarPersonal", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@id", personal.Id);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            int resultado = Convert.ToInt32(reader["resultado"]);
                            rpta = resultado == 1 ? "OK" : reader["mensaje"]?.ToString() ?? "Error desconocido";
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

        // Método para mostrar todo el personal
        public DataTable Mostrar()
        {
            DataTable dtResultado = new DataTable("Personal");
            SqlConnection connection = DbConnection.Instance.GetConnection();

            try
            {
                using (SqlCommand command = new SqlCommand("SP_MostrarPersonal", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(dtResultado);
                    }
                }

                // Agregar columna nombre_completo
                AgregarColumnaNombreCompleto(dtResultado);
            }
            catch
            {
                dtResultado = new DataTable("Personal");
            }

            return dtResultado;
        }

        // Método para buscar texto en personal
        public DataTable BuscarTexto(DPersonal personal)
        {
            DataTable dtResultado = new DataTable("Personal");
            SqlConnection connection = DbConnection.Instance.GetConnection();

            try
            {
                using (SqlCommand command = new SqlCommand("SP_BuscarPersonalPorTexto", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@textoBuscar", personal.TextoBuscar ?? "");

                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(dtResultado);
                    }
                }

                // Agregar columna nombre_completo
                AgregarColumnaNombreCompleto(dtResultado);
            }
            catch
            {
                dtResultado = new DataTable("Personal");
            }

            return dtResultado;
        }

        // Método para buscar por tipo de personal
        public DataTable BuscarPorTipo(string tipoPersonal)
        {
            DataTable dtResultado = new DataTable("Personal");
            SqlConnection connection = DbConnection.Instance.GetConnection();

            try
            {
                using (SqlCommand command = new SqlCommand("SP_BuscarPersonalPorTipo", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@tipoPersonal", tipoPersonal);

                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(dtResultado);
                    }
                }

                // Agregar columna nombre_completo
                AgregarColumnaNombreCompleto(dtResultado);
            }
            catch
            {
                dtResultado = new DataTable("Personal");
            }

            return dtResultado;
        }

        // Método para obtener personal por ID
        public DataTable ObtenerPorId(int id)
        {
            DataTable dtResultado = new DataTable("Personal");
            SqlConnection connection = DbConnection.Instance.GetConnection();

            try
            {
                using (SqlCommand command = new SqlCommand("SP_ObtenerPersonalPorId", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@id", id);

                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(dtResultado);
                    }
                }

                // Agregar columna nombre_completo
                AgregarColumnaNombreCompleto(dtResultado);
            }
            catch
            {
                dtResultado = new DataTable("Personal");
            }

            return dtResultado;
        }

        // Validación de usuario único
        public bool ValidarUsuarioUnico(string usuario, int? idPersonal = null)
        {
            if (string.IsNullOrEmpty(usuario))
                return false;

            SqlConnection connection = DbConnection.Instance.GetConnection();

            try
            {
                using (SqlCommand command = new SqlCommand("SP_ValidarUsuarioUnico", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@usuario", usuario);
                    command.Parameters.AddWithValue("@idPersonal", idPersonal ?? (object)DBNull.Value);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            bool existe = Convert.ToBoolean(reader["existe"]);
                            return !existe;
                        }
                    }
                }
            }
            catch
            {
                return false;
            }

            return false;
        }

        // Validación de email único
        public bool ValidarEmailUnico(string email, int? idPersonal = null)
        {
            if (string.IsNullOrEmpty(email))
                return false;

            SqlConnection connection = DbConnection.Instance.GetConnection();

            try
            {
                using (SqlCommand command = new SqlCommand("SP_ValidarEmailPersonalUnico", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@email", email);
                    command.Parameters.AddWithValue("@idPersonal", idPersonal ?? (object)DBNull.Value);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            bool existe = Convert.ToBoolean(reader["existe"]);
                            return !existe;
                        }
                    }
                }
            }
            catch
            {
                return false;
            }

            return false;
        }

        // Método auxiliar para agregar columna nombre_completo
        private void AgregarColumnaNombreCompleto(DataTable dt)
        {
            if (dt == null || dt.Rows.Count == 0) return;

            // Agregar la columna si no existe
            if (!dt.Columns.Contains("nombre_completo"))
            {
                dt.Columns.Add("nombre_completo", typeof(string));
            }

            // Llenar la columna para cada fila
            foreach (DataRow row in dt.Rows)
            {
                string nombre = row["nombre"]?.ToString() ?? "";
                string apellido = row["apellido"]?.ToString() ?? "";
                row["nombre_completo"] = $"{nombre} {apellido}".Trim();
            }
        }
    }
}