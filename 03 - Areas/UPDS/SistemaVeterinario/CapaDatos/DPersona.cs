using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Microsoft.Data.SqlClient;

namespace CapaDatos
{
    public class DPersona
    {
        // Propiedades comunes de persona
        private int _id;
        private string _tipo = string.Empty;
        private string _email = string.Empty;
        private string _direccion = string.Empty;
        private string _telefono = string.Empty;
        private bool _activo;

        // Propiedades específicas de persona física
        private string _ci = string.Empty;
        private string _nombre = string.Empty;
        private string _apellido = string.Empty;
        private DateTime? _fechaNacimiento;
        private char? _genero;

        // Propiedades específicas de persona jurídica
        private string _razonSocial = string.Empty;
        private string _nit = string.Empty;
        private string _encargadoNombre = string.Empty;
        private string _encargadoCargo = string.Empty;

        // Propiedades auxiliares
        private string _textoBuscar = string.Empty;

        // Propiedades públicas
        public int Id { get => _id; set => _id = value; }
        public string Tipo { get => _tipo; set => _tipo = value; }
        public string Email { get => _email; set => _email = value; }
        public string Direccion { get => _direccion; set => _direccion = value; }
        public string Telefono { get => _telefono; set => _telefono = value; }
        public bool Activo { get => _activo; set => _activo = value; }

        // Persona Física
        public string Ci { get => _ci; set => _ci = value; }
        public string Nombre { get => _nombre; set => _nombre = value; }
        public string Apellido { get => _apellido; set => _apellido = value; }
        public DateTime? FechaNacimiento { get => _fechaNacimiento; set => _fechaNacimiento = value; }
        public char? Genero { get => _genero; set => _genero = value; }

        // Persona Jurídica
        public string RazonSocial { get => _razonSocial; set => _razonSocial = value; }
        public string Nit { get => _nit; set => _nit = value; }
        public string EncargadoNombre { get => _encargadoNombre; set => _encargadoNombre = value; }
        public string EncargadoCargo { get => _encargadoCargo; set => _encargadoCargo = value; }

        // Auxiliares
        public string TextoBuscar { get => _textoBuscar; set => _textoBuscar = value; }

        // Constructores
        public DPersona() 
        { 
            _activo = true;
        }

        public DPersona(int id, string tipo)
        {
            Id = id;
            Tipo = tipo;
            Activo = true;
        }

        // Método para insertar persona física
        public string InsertarPersonaFisica(DPersona persona)
        {
            string rpta = string.Empty;
            SqlConnection connection = DbConnection.Instance.GetConnection();

            try
            {
                using (SqlCommand command = new SqlCommand("SP01_CreateOrUpdatePFisica", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Parámetros para persona física
                    command.Parameters.AddWithValue("@id", DBNull.Value);
                    command.Parameters.AddWithValue("@ci", persona.Ci ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@nombre", persona.Nombre);
                    command.Parameters.AddWithValue("@apellido", persona.Apellido);
                    command.Parameters.AddWithValue("@email", persona.Email ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@direccion", persona.Direccion ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@telefono", persona.Telefono ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@fecha_nacimiento", persona.FechaNacimiento ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@genero", persona.Genero ?? (object)DBNull.Value);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            int id = Convert.ToInt32(reader["id"]);
                            if (id > 0)
                            {
                                persona.Id = id;
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

        // Método para insertar persona jurídica
        public string InsertarPersonaJuridica(DPersona persona)
        {
            string rpta = string.Empty;
            SqlConnection connection = DbConnection.Instance.GetConnection();

            try
            {
                using (SqlCommand command = new SqlCommand("SP02_CreateOrUpdatePJuridica", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Parámetros para persona jurídica
                    command.Parameters.AddWithValue("@id", DBNull.Value);
                    command.Parameters.AddWithValue("@razon_social", persona.RazonSocial);
                    command.Parameters.AddWithValue("@nit", persona.Nit ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@encargado_nombre", persona.EncargadoNombre ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@encargado_cargo", persona.EncargadoCargo ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@email", persona.Email ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@direccion", persona.Direccion ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@telefono", persona.Telefono ?? (object)DBNull.Value);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            int id = Convert.ToInt32(reader["id"]);
                            if (id > 0)
                            {
                                persona.Id = id;
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

        // Método para editar persona (física o jurídica)
        public string Editar(DPersona persona)
        {
            string rpta = string.Empty;

            if (persona.Tipo == "Física")
            {
                rpta = EditarPersonaFisica(persona);
            }
            else if (persona.Tipo == "Jurídica")
            {
                rpta = EditarPersonaJuridica(persona);
            }
            else
            {
                rpta = "Tipo de persona no válido";
            }

            return rpta;
        }

        private string EditarPersonaFisica(DPersona persona)
        {
            string rpta = string.Empty;
            SqlConnection connection = DbConnection.Instance.GetConnection();

            try
            {
                using (SqlCommand command = new SqlCommand("SP01_CreateOrUpdatePFisica", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@id", persona.Id);
                    command.Parameters.AddWithValue("@ci", persona.Ci ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@nombre", persona.Nombre);
                    command.Parameters.AddWithValue("@apellido", persona.Apellido);
                    command.Parameters.AddWithValue("@email", persona.Email ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@direccion", persona.Direccion ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@telefono", persona.Telefono ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@fecha_nacimiento", persona.FechaNacimiento ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@genero", persona.Genero ?? (object)DBNull.Value);

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

        private string EditarPersonaJuridica(DPersona persona)
        {
            string rpta = string.Empty;
            SqlConnection connection = DbConnection.Instance.GetConnection();

            try
            {
                using (SqlCommand command = new SqlCommand("SP02_CreateOrUpdatePJuridica", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@id", persona.Id);
                    command.Parameters.AddWithValue("@razon_social", persona.RazonSocial);
                    command.Parameters.AddWithValue("@nit", persona.Nit ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@encargado_nombre", persona.EncargadoNombre ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@encargado_cargo", persona.EncargadoCargo ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@email", persona.Email ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@direccion", persona.Direccion ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@telefono", persona.Telefono ?? (object)DBNull.Value);

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

        // Método para eliminar persona (cambiar estado a inactivo)
        public string Eliminar(DPersona persona)
        {
            string rpta = string.Empty;
            SqlConnection connection = DbConnection.Instance.GetConnection();

            try
            {
                using (SqlCommand command = new SqlCommand("SP_EliminarPersona", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@id", persona.Id);
                    
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

        // Método para cambiar tipo de persona
        public string CambiarTipoPersona(DPersona persona)
        {
            string rpta = string.Empty;
            SqlConnection connection = DbConnection.Instance.GetConnection();

            try
            {
                using (SqlTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        // Primero eliminar de la tabla específica actual
                        string deleteQuery = persona.Tipo == "Física" ? 
                            "DELETE FROM persona_fisica WHERE id = @Id" :
                            "DELETE FROM persona_juridica WHERE id = @Id";

                        using (SqlCommand deleteCommand = new SqlCommand(deleteQuery, connection, transaction))
                        {
                            deleteCommand.Parameters.AddWithValue("@Id", persona.Id);
                            deleteCommand.ExecuteNonQuery();
                        }

                        // Actualizar el tipo en la tabla persona
                        string updateQuery = "UPDATE persona SET tipo = @NuevoTipo WHERE id = @Id";
                        using (SqlCommand updateCommand = new SqlCommand(updateQuery, connection, transaction))
                        {
                            updateCommand.Parameters.AddWithValue("@NuevoTipo", persona.Tipo);
                            updateCommand.Parameters.AddWithValue("@Id", persona.Id);
                            updateCommand.ExecuteNonQuery();
                        }

                        // Insertar en la nueva tabla específica
                        if (persona.Tipo == "Física")
                        {
                            string insertQuery = @"INSERT INTO persona_fisica (id, ci, nombre, apellido, fecha_nacimiento, genero) 
                                                 VALUES (@Id, @Ci, @Nombre, @Apellido, @FechaNacimiento, @Genero)";
                            using (SqlCommand insertCommand = new SqlCommand(insertQuery, connection, transaction))
                            {
                                insertCommand.Parameters.AddWithValue("@Id", persona.Id);
                                insertCommand.Parameters.AddWithValue("@Ci", persona.Ci ?? (object)DBNull.Value);
                                insertCommand.Parameters.AddWithValue("@Nombre", persona.Nombre);
                                insertCommand.Parameters.AddWithValue("@Apellido", persona.Apellido);
                                insertCommand.Parameters.AddWithValue("@FechaNacimiento", persona.FechaNacimiento ?? (object)DBNull.Value);
                                insertCommand.Parameters.AddWithValue("@Genero", persona.Genero ?? (object)DBNull.Value);
                                insertCommand.ExecuteNonQuery();
                            }
                        }
                        else if (persona.Tipo == "Jurídica")
                        {
                            string insertQuery = @"INSERT INTO persona_juridica (id, razon_social, nit, encargado_nombre, encargado_cargo) 
                                                 VALUES (@Id, @RazonSocial, @Nit, @EncargadoNombre, @EncargadoCargo)";
                            using (SqlCommand insertCommand = new SqlCommand(insertQuery, connection, transaction))
                            {
                                insertCommand.Parameters.AddWithValue("@Id", persona.Id);
                                insertCommand.Parameters.AddWithValue("@RazonSocial", persona.RazonSocial);
                                insertCommand.Parameters.AddWithValue("@Nit", persona.Nit ?? (object)DBNull.Value);
                                insertCommand.Parameters.AddWithValue("@EncargadoNombre", persona.EncargadoNombre ?? (object)DBNull.Value);
                                insertCommand.Parameters.AddWithValue("@EncargadoCargo", persona.EncargadoCargo ?? (object)DBNull.Value);
                                insertCommand.ExecuteNonQuery();
                            }
                        }

                        transaction.Commit();
                        rpta = "OK";
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
            catch (Exception ex)
            {
                rpta = ex.Message;
            }

            return rpta;
        }

        // Método para mostrar todas las personas
        public DataTable Mostrar()
        {
            DataTable dtResultado = new DataTable("Persona");
            SqlConnection connection = DbConnection.Instance.GetConnection();

            try
            {
                using (SqlCommand command = new SqlCommand("SP_MostrarPersonas", connection))
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
                dtResultado = new DataTable("Persona");
            }

            return dtResultado;
        }

        // Método para buscar texto en personas
        public DataTable BuscarTexto(DPersona persona)
        {
            DataTable dtResultado = new DataTable("Persona");
            SqlConnection connection = DbConnection.Instance.GetConnection();

            try
            {
                using (SqlCommand command = new SqlCommand("SP_BuscarPersonaPorTexto", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@textoBuscar", persona.TextoBuscar ?? "");
                    
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
                dtResultado = new DataTable("Persona");
            }

            return dtResultado;
        }

        // Método para buscar por tipo de persona
        public DataTable BuscarPorTipo(string tipoPersona)
        {
            DataTable dtResultado = new DataTable("Persona");
            SqlConnection connection = DbConnection.Instance.GetConnection();

            try
            {
                using (SqlCommand command = new SqlCommand("SP_BuscarPersonaPorTipo", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@tipoPersona", tipoPersona);
                    
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
                dtResultado = new DataTable("Persona");
            }

            return dtResultado;
        }

        // Método para obtener persona por ID
        public DataTable ObtenerPorId(int id)
        {
            DataTable dtResultado = new DataTable("Persona");
            SqlConnection connection = DbConnection.Instance.GetConnection();

            try
            {
                using (SqlCommand command = new SqlCommand("SP_ObtenerPersonaPorId", connection))
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
                dtResultado = new DataTable("Persona");
            }

            return dtResultado;
        }

        // Método para validar CI único
        public bool ValidarCIUnico(string ci, int? idPersona = null)
        {
            if (string.IsNullOrEmpty(ci))
                return true; // CI es opcional

            SqlConnection connection = DbConnection.Instance.GetConnection();

            try
            {
                using (SqlCommand command = new SqlCommand("SP_ValidarCIUnico", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@ci", ci);
                    command.Parameters.AddWithValue("@idPersona", idPersona ?? (object)DBNull.Value);
                    
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            bool existe = Convert.ToBoolean(reader["existe"]);
                            return !existe; // Retorna true si NO existe (es único)
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

        // Método para validar NIT único
        public bool ValidarNITUnico(string nit, int? idPersona = null)
        {
            if (string.IsNullOrEmpty(nit))
                return true; // NIT es opcional

            SqlConnection connection = DbConnection.Instance.GetConnection();

            try
            {
                using (SqlCommand command = new SqlCommand("SP_ValidarNITUnico", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@nit", nit);
                    command.Parameters.AddWithValue("@idPersona", idPersona ?? (object)DBNull.Value);
                    
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            bool existe = Convert.ToBoolean(reader["existe"]);
                            return !existe; // Retorna true si NO existe (es único)
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

        // Método para validar Email único
        public bool ValidarEmailUnico(string email, int? idPersona = null)
        {
            if (string.IsNullOrEmpty(email))
                return true; // Email es opcional

            SqlConnection connection = DbConnection.Instance.GetConnection();

            try
            {
                using (SqlCommand command = new SqlCommand("SP_ValidarEmailUnico", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@email", email);
                    command.Parameters.AddWithValue("@idPersona", idPersona ?? (object)DBNull.Value);
                    
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            bool existe = Convert.ToBoolean(reader["existe"]);
                            return !existe; // Retorna true si NO existe (es único)
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

        // Método para buscar personas por nombre completo
        public DataTable BuscarPorNombre(string nombre)
        {
            DataTable dtResultado = new DataTable("Persona");
            SqlConnection connection = DbConnection.Instance.GetConnection();

            try
            {
                using (SqlCommand command = new SqlCommand("SP_BuscarPersonaPorTexto", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@textoBuscar", nombre ?? "");
                    
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
                dtResultado = new DataTable("Persona");
            }

            return dtResultado;
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
                string tipo = row["tipo"]?.ToString() ?? "";
                
                if (tipo == "Física")
                {
                    string nombre = row["nombre"]?.ToString() ?? "";
                    string apellido = row["apellido"]?.ToString() ?? "";
                    row["nombre_completo"] = $"{nombre} {apellido}".Trim();
                }
                else if (tipo == "Jurídica")
                {
                    string razonSocial = row["razon_social"]?.ToString() ?? "";
                    row["nombre_completo"] = razonSocial;
                }
                else
                {
                    row["nombre_completo"] = "Sin nombre";
                }
            }
        }
    }
}
