using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class DPersonas : DbConnection
    {
        private int _id;
        private string _tipo;
        private string _email;
        private string _direccion;
        private string _telefono;
        private bool _activo;
        
        // Campos específicos de Persona Física
        private string _ci;
        private string _nombre;
        private string _apellido;
        private DateTime? _fechaNacimiento;
        private string _genero;
        
        // Campos específicos de Persona Jurídica
        private string _razonSocial;
        private string _nit;
        private string _encargadoNombre;
        private string _encargadoCargo;
        
        // Campo para búsquedas
        private string _textoBuscar;

        #region Propiedades
        public int Id { get => _id; set => _id = value; }
        public string Tipo { get => _tipo; set => _tipo = value; }
        public string Email { get => _email; set => _email = value; }
        public string Direccion { get => _direccion; set => _direccion = value; }
        public string Telefono { get => _telefono; set => _telefono = value; }
        public bool Activo { get => _activo; set => _activo = value; }
        
        // Propiedades Persona Física
        public string Ci { get => _ci; set => _ci = value; }
        public string Nombre { get => _nombre; set => _nombre = value; }
        public string Apellido { get => _apellido; set => _apellido = value; }
        public DateTime? FechaNacimiento { get => _fechaNacimiento; set => _fechaNacimiento = value; }
        public string Genero { get => _genero; set => _genero = value; }
        
        // Propiedades Persona Jurídica
        public string RazonSocial { get => _razonSocial; set => _razonSocial = value; }
        public string Nit { get => _nit; set => _nit = value; }
        public string EncargadoNombre { get => _encargadoNombre; set => _encargadoNombre = value; }
        public string EncargadoCargo { get => _encargadoCargo; set => _encargadoCargo = value; }
        
        public string TextoBuscar { get => _textoBuscar; set => _textoBuscar = value; }
        #endregion

        #region Constructores
        public DPersonas() 
        {
            _activo = true;
            _tipo = "Física";
        }

        public DPersonas(int id, string tipo, string email, string direccion, string telefono)
        {
            Id = id;
            Tipo = tipo;
            Email = email;
            Direccion = direccion;
            Telefono = telefono;
            Activo = true;
        }

        // Constructor para Persona Física
        public DPersonas(string ci, string nombre, string apellido, string email, 
            string direccion = "", string telefono = "", DateTime? fechaNacimiento = null, string genero = "")
        {
            Tipo = "Física";
            Ci = ci;
            Nombre = nombre;
            Apellido = apellido;
            Email = email;
            Direccion = direccion;
            Telefono = telefono;
            FechaNacimiento = fechaNacimiento;
            Genero = genero;
            Activo = true;
        }

        // Constructor para Persona Jurídica
        public DPersonas(string razonSocial, string nit, string email, string direccion = "", 
            string telefono = "", string encargadoNombre = "", string encargadoCargo = "")
        {
            Tipo = "Jurídica";
            RazonSocial = razonSocial;
            Nit = nit;
            Email = email;
            Direccion = direccion;
            Telefono = telefono;
            EncargadoNombre = encargadoNombre;
            EncargadoCargo = encargadoCargo;
            Activo = true;
        }
        #endregion

        #region Métodos CRUD

        public string InsertarPersonaFisica(DPersonas persona)
        {
            string rpta = string.Empty;
            using (SqlConnection connection = GetConnection())
            {
                connection.Open();
                try
                {
                    using (SqlCommand command = new SqlCommand("SP01_CreateOrUpdatePFisica", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@id", DBNull.Value);
                        command.Parameters.AddWithValue("@ci", persona.Ci ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@nombre", persona.Nombre ?? "");
                        command.Parameters.AddWithValue("@apellido", persona.Apellido ?? "");
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

        public string InsertarPersonaJuridica(DPersonas persona)
        {
            string rpta = string.Empty;
            using (SqlConnection connection = GetConnection())
            {
                connection.Open();
                try
                {
                    using (SqlCommand command = new SqlCommand("SP02_CreateOrUpdatePJuridica", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@id", DBNull.Value);
                        command.Parameters.AddWithValue("@razon_social", persona.RazonSocial ?? "");
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

        public string Insertar(DPersonas persona)
        {
            if (persona.Tipo == "Física")
            {
                return InsertarPersonaFisica(persona);
            }
            else if (persona.Tipo == "Jurídica")
            {
                return InsertarPersonaJuridica(persona);
            }
            else
            {
                return "Error: Tipo de persona no válido";
            }
        }

        public string EditarPersonaFisica(DPersonas persona)
        {
            string rpta = string.Empty;
            using (SqlConnection connection = GetConnection())
            {
                connection.Open();
                try
                {
                    using (SqlCommand command = new SqlCommand("SP01_CreateOrUpdatePFisica", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@id", persona.Id);
                        command.Parameters.AddWithValue("@ci", persona.Ci ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@nombre", persona.Nombre ?? "");
                        command.Parameters.AddWithValue("@apellido", persona.Apellido ?? "");
                        command.Parameters.AddWithValue("@email", persona.Email ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@direccion", persona.Direccion ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@telefono", persona.Telefono ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@fecha_nacimiento", persona.FechaNacimiento ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@genero", persona.Genero ?? (object)DBNull.Value);

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

        public string EditarPersonaJuridica(DPersonas persona)
        {
            string rpta = string.Empty;
            using (SqlConnection connection = GetConnection())
            {
                connection.Open();
                try
                {
                    using (SqlCommand command = new SqlCommand("SP02_CreateOrUpdatePJuridica", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@id", persona.Id);
                        command.Parameters.AddWithValue("@razon_social", persona.RazonSocial ?? "");
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

        public string Editar(DPersonas persona)
        {
            if (persona.Tipo == "Física")
            {
                return EditarPersonaFisica(persona);
            }
            else if (persona.Tipo == "Jurídica")
            {
                return EditarPersonaJuridica(persona);
            }
            else
            {
                return "Error: Tipo de persona no válido";
            }
        }

        public string Eliminar(DPersonas persona)
        {
            string rpta = string.Empty;
            using (SqlConnection connection = GetConnection())
            {
                connection.Open();
                try
                {
                    // Eliminación lógica - cambiar activo a false
                    string query = "UPDATE persona SET activo = 0 WHERE id = @id";
                    
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@id", persona.Id);
                        
                        int filasAfectadas = command.ExecuteNonQuery();
                        rpta = filasAfectadas > 0 ? "OK" : "No se encontró la persona";
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
            DataTable dtResultado = new DataTable("Personas");
            using (SqlConnection connection = GetConnection())
            {
                connection.Open();
                try
                {
                    string query = @"SELECT * FROM VW_PersonasCompletas WHERE activo = 1 ORDER BY nombre_mostrar";
                    
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

        public DataTable BuscarPorNombre(DPersonas persona)
        {
            DataTable dtResultado = new DataTable("PersonasBusqueda");
            using (SqlConnection connection = GetConnection())
            {
                connection.Open();
                try
                {
                    string query = @"SELECT * FROM VW_PersonasCompletas 
                                   WHERE activo = 1 
                                   AND (nombre_mostrar LIKE @textoBuscar 
                                        OR email LIKE @textoBuscar 
                                        OR ci LIKE @textoBuscar 
                                        OR nit LIKE @textoBuscar)
                                   ORDER BY nombre_mostrar";
                    
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@textoBuscar", "%" + (persona.TextoBuscar ?? "") + "%");
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

        public DataTable ObtenerPorId(DPersonas persona)
        {
            DataTable dtResultado = new DataTable("PersonaPorId");
            using (SqlConnection connection = GetConnection())
            {
                connection.Open();
                try
                {
                    string query = @"SELECT * FROM VW_PersonasCompletas WHERE id = @id";
                    
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@id", persona.Id);
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

        public DataTable ObtenerTipos()
        {
            DataTable dtResultado = new DataTable("TiposPersona");
            dtResultado.Columns.Add("Tipo", typeof(string));
            dtResultado.Rows.Add("Física");
            dtResultado.Rows.Add("Jurídica");
            return dtResultado;
        }

        #endregion
    }
}