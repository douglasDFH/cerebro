using System;
using System.Data;
using System.Data.SqlClient;

namespace CapaDatos
{
    public class DPersona : DbConnection
    {
        private int _persona_id;
        private string _nombre;
        private string _apellidos;
        private char _tipo_persona;
        private string _email;
        private string _direccion;
        private string _telefono;
        private byte[] _foto;
        private string _textoBuscar;

        // Propiedades para persona física
        private string _dni;
        private DateTime? _fecha_nacimiento;
        private char _genero;

        // Propiedades para persona jurídica
        private string _cif;
        private string _razon_social;
        private DateTime? _fecha_constitucion;
        private string _actividad_principal;

        public int Persona_id { get => _persona_id; set => _persona_id = value; }
        public string Nombre { get => _nombre; set => _nombre = value; }
        public string Apellidos { get => _apellidos; set => _apellidos = value; }
        public char Tipo_persona { get => _tipo_persona; set => _tipo_persona = value; }
        public string Email { get => _email; set => _email = value; }
        public string Direccion { get => _direccion; set => _direccion = value; }
        public string Telefono { get => _telefono; set => _telefono = value; }
        public byte[] Foto { get => _foto; set => _foto = value; }
        public string TextoBuscar { get => _textoBuscar; set => _textoBuscar = value; }

        // Propiedades persona física
        public string Dni { get => _dni; set => _dni = value; }
        public DateTime? Fecha_nacimiento { get => _fecha_nacimiento; set => _fecha_nacimiento = value; }
        public char Genero { get => _genero; set => _genero = value; }

        // Propiedades persona jurídica
        public string Cif { get => _cif; set => _cif = value; }
        public string Razon_social { get => _razon_social; set => _razon_social = value; }
        public DateTime? Fecha_constitucion { get => _fecha_constitucion; set => _fecha_constitucion = value; }
        public string Actividad_principal { get => _actividad_principal; set => _actividad_principal = value; }

        public DPersona() { }

        public string Insertar(DPersona Persona)
        {
            string rpta = "";
            using (SqlConnection connection = GetConnection())
            {
                connection.Open();
                SqlTransaction transaction = connection.BeginTransaction();
                try
                {
                    // Insertar en tabla Persona
                    string spName = Persona.Tipo_persona == 'F' ? "spInsertar_Fisica" : "spInsertar_Juridica";

                    using (SqlCommand command = new SqlCommand(spName, connection, transaction))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        SqlParameter ParPersonaId = new SqlParameter("@persona_id", SqlDbType.Int);
                        ParPersonaId.Direction = ParameterDirection.Output;
                        command.Parameters.Add(ParPersonaId);

                        command.Parameters.AddWithValue("@nombre", Persona.Nombre);
                        command.Parameters.AddWithValue("@apellidos", Persona.Apellidos);
                        command.Parameters.AddWithValue("@email", Persona.Email);
                        command.Parameters.AddWithValue("@direccion", Persona.Direccion);
                        command.Parameters.AddWithValue("@telefono", Persona.Telefono);
                        command.Parameters.AddWithValue("@foto", Persona.Foto ?? (object)DBNull.Value);

                        if (Persona.Tipo_persona == 'F')
                        {
                            command.Parameters.AddWithValue("@dni", Persona.Dni);
                            command.Parameters.AddWithValue("@fecha_nacimiento",
                                Persona.Fecha_nacimiento.HasValue ? (object)Persona.Fecha_nacimiento.Value : DBNull.Value);
                            command.Parameters.AddWithValue("@genero", Persona.Genero);
                        }
                        else
                        {
                            command.Parameters.AddWithValue("@cif", Persona.Cif);
                            command.Parameters.AddWithValue("@razon_social", Persona.Razon_social);
                            command.Parameters.AddWithValue("@fecha_constitucion",
                                Persona.Fecha_constitucion.HasValue ? (object)Persona.Fecha_constitucion.Value : DBNull.Value);
                            command.Parameters.AddWithValue("@actividad_principal",
                                string.IsNullOrEmpty(Persona.Actividad_principal) ? (object)DBNull.Value : Persona.Actividad_principal);
                        }

                        rpta = command.ExecuteNonQuery() >= 1 ? "OK" : "No se insertó el registro";

                        if (rpta.Equals("OK"))
                        {
                            Persona.Persona_id = Convert.ToInt32(ParPersonaId.Value);
                        }
                    }

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    rpta = ex.Message;
                }
            }
            return rpta;
        }

        public string Editar(DPersona Persona)
        {
            string rpta = "";
            using (SqlConnection connection = GetConnection())
            {
                connection.Open();
                SqlTransaction transaction = connection.BeginTransaction();
                try
                {
                    // Primero actualizar la tabla Persona
                    using (SqlCommand command = new SqlCommand("spEditar_Persona", connection, transaction))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@persona_id", Persona.Persona_id);
                        command.Parameters.AddWithValue("@nombre", Persona.Nombre);
                        command.Parameters.AddWithValue("@apellidos", Persona.Apellidos);
                        command.Parameters.AddWithValue("@email", Persona.Email);
                        command.Parameters.AddWithValue("@direccion", Persona.Direccion);
                        command.Parameters.AddWithValue("@telefono", Persona.Telefono);
                        command.Parameters.AddWithValue("@foto", Persona.Foto ?? (object)DBNull.Value);

                        rpta = command.ExecuteNonQuery() >= 1 ? "OK" : "No se actualizó el registro";
                    }

                    if (rpta.Equals("OK"))
                    {
                        // Luego actualizar la tabla específica (Fisica o Juridica)
                        string spName = Persona.Tipo_persona == 'F' ? "spEditar_Fisica" : "spEditar_Juridica";

                        using (SqlCommand command = new SqlCommand(spName, connection, transaction))
                        {
                            command.CommandType = CommandType.StoredProcedure;
                            command.Parameters.AddWithValue("@persona_id", Persona.Persona_id);

                            if (Persona.Tipo_persona == 'F')
                            {
                                command.Parameters.AddWithValue("@dni", Persona.Dni);
                                command.Parameters.AddWithValue("@fecha_nacimiento",
                                    Persona.Fecha_nacimiento.HasValue ? (object)Persona.Fecha_nacimiento.Value : DBNull.Value);
                                command.Parameters.AddWithValue("@genero", Persona.Genero);
                            }
                            else
                            {
                                command.Parameters.AddWithValue("@cif", Persona.Cif);
                                command.Parameters.AddWithValue("@razon_social", Persona.Razon_social);
                                command.Parameters.AddWithValue("@fecha_constitucion",
                                    Persona.Fecha_constitucion.HasValue ? (object)Persona.Fecha_constitucion.Value : DBNull.Value);
                                command.Parameters.AddWithValue("@actividad_principal",
                                    string.IsNullOrEmpty(Persona.Actividad_principal) ? (object)DBNull.Value : Persona.Actividad_principal);
                            }

                            rpta = command.ExecuteNonQuery() >= 1 ? "OK" : "No se actualizó el registro";
                        }
                    }

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    rpta = ex.Message;
                }
            }
            return rpta;
        }

        public string Eliminar(DPersona Persona)
        {
            string rpta = "";
            using (SqlConnection connection = GetConnection())
            {
                connection.Open();
                SqlTransaction transaction = connection.BeginTransaction();
                try
                {
                    // Primero eliminar de Fisica/Juridica
                    string spName = Persona.Tipo_persona == 'F' ? "spEliminar_Fisica" : "spEliminar_Juridica";

                    using (SqlCommand command = new SqlCommand(spName, connection, transaction))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@persona_id", Persona.Persona_id);
                        rpta = command.ExecuteNonQuery() >= 1 ? "OK" : "No se eliminó el registro";
                    }

                    if (rpta.Equals("OK"))
                    {
                        // Luego eliminar de Persona
                        using (SqlCommand command = new SqlCommand("spEliminar_Persona", connection, transaction))
                        {
                            command.CommandType = CommandType.StoredProcedure;
                            command.Parameters.AddWithValue("@persona_id", Persona.Persona_id);
                            rpta = command.ExecuteNonQuery() >= 1 ? "OK" : "No se eliminó el registro";
                        }
                    }

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    rpta = ex.Message;
                }
            }
            return rpta;
        }

        public DataTable Mostrar()
        {
            DataTable dtResultado = new DataTable("Persona");
            using (SqlConnection connection = GetConnection())
            {
                connection.Open();
                try
                {
                    using (SqlCommand command = new SqlCommand("spMostrar_Personas", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

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

        public DataTable BuscarPorNombre(string textoBuscar)
        {
            DataTable dtResultado = new DataTable("Persona");
            using (SqlConnection connection = GetConnection())
            {
                connection.Open();
                try
                {
                    using (SqlCommand command = new SqlCommand("spBuscar_Persona_Nombre", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@textobuscar", textoBuscar);

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

        public DataTable BuscarPorDocumento(string textoBuscar, char tipoPersona)
        {
            DataTable dtResultado = new DataTable("Persona");
            using (SqlConnection connection = GetConnection())
            {
                connection.Open();
                try
                {
                    string spName = tipoPersona == 'F' ? "spBuscar_Persona_DNI" : "spBuscar_Persona_CIF";

                    using (SqlCommand command = new SqlCommand(spName, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@textobuscar", textoBuscar);

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
    }
}