using System;
using System.Data;
using System.Data.SqlClient;

namespace CapaDatos
{
    public class DPersona : DbConnection
    {
        private int _idPersona;
        private char _tipoPersona;
        private char _genero;
        private string _dni;
        private string _cif;
        private string _nombre;
        private string _apellidos;
        private string _razonSocial;
        private string _email;
        private string _direccion;
        private string _telefono;
        private string _telefonoAlternativo;
        private string _observaciones;
        private bool _estado;
        private string _textoBuscar;

        public int IdPersona { get => _idPersona; set => _idPersona = value; }
        public char TipoPersona { get => _tipoPersona; set => _tipoPersona = value; }
        public char Genero { get => _genero; set => _genero = value; }
        public string DNI { get => _dni; set => _dni = value; }
        public string CIF { get => _cif; set => _cif = value; }
        public string Nombre { get => _nombre; set => _nombre = value; }
        public string Apellidos { get => _apellidos; set => _apellidos = value; }
        public string RazonSocial { get => _razonSocial; set => _razonSocial = value; }
        public string Email { get => _email; set => _email = value; }
        public string Direccion { get => _direccion; set => _direccion = value; }
        public string Telefono { get => _telefono; set => _telefono = value; }
        public string TelefonoAlternativo { get => _telefonoAlternativo; set => _telefonoAlternativo = value; }
        public string Observaciones { get => _observaciones; set => _observaciones = value; }
        public bool Estado { get => _estado; set => _estado = value; }
        public string TextoBuscar { get => _textoBuscar; set => _textoBuscar = value; }

        public DPersona() { }

        public string InsertarPersonaFisica(DPersona Persona)
        {
            string rpta = "";
            using (SqlConnection connection = GetConnection())
            {
                connection.Open();
                try
                {
                    using (SqlCommand command = new SqlCommand("SP_InsertarPersonaFisica", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@DNI", Persona.DNI);
                        command.Parameters.AddWithValue("@Nombre", Persona.Nombre);
                        command.Parameters.AddWithValue("@Apellidos", Persona.Apellidos);
                        command.Parameters.AddWithValue("@Genero", Persona.Genero);
                        command.Parameters.AddWithValue("@Email", Persona.Email ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@Direccion", Persona.Direccion ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@Telefono", Persona.Telefono ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@Observaciones", Persona.Observaciones ?? (object)DBNull.Value);

                        var result = command.ExecuteScalar();
                        if (result != null && int.TryParse(result.ToString(), out int idPersona))
                        {
                            Persona.IdPersona = idPersona;
                            rpta = "OK";
                        }
                        else
                        {
                            rpta = "No se insertó el registro";
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

        public string InsertarPersonaJuridica(DPersona Persona)
        {
            string rpta = "";
            using (SqlConnection connection = GetConnection())
            {
                connection.Open();
                try
                {
                    using (SqlCommand command = new SqlCommand("SP_InsertarPersonaJuridica", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@CIF", Persona.CIF);
                        command.Parameters.AddWithValue("@RazonSocial", Persona.RazonSocial);
                        command.Parameters.AddWithValue("@Email", Persona.Email ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@Direccion", Persona.Direccion ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@Telefono", Persona.Telefono ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@Observaciones", Persona.Observaciones ?? (object)DBNull.Value);

                        var result = command.ExecuteScalar();
                        if (result != null && int.TryParse(result.ToString(), out int idPersona))
                        {
                            Persona.IdPersona = idPersona;
                            rpta = "OK";
                        }
                        else
                        {
                            rpta = "No se insertó el registro";
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

        public string Editar(DPersona Persona)
        {
            string rpta = "";
            using (SqlConnection connection = GetConnection())
            {
                connection.Open();
                try
                {
                    string sql = @"
                        UPDATE Persona 
                        SET Email = @Email,
                            Direccion = @Direccion,
                            Telefono = @Telefono,
                            TelefonoAlternativo = @TelefonoAlternativo,
                            Observaciones = @Observaciones,
                            FechaModificacion = GETDATE()
                        WHERE IdPersona = @IdPersona";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@IdPersona", Persona.IdPersona);
                        command.Parameters.AddWithValue("@Email", Persona.Email ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@Direccion", Persona.Direccion ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@Telefono", Persona.Telefono ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@TelefonoAlternativo", Persona.TelefonoAlternativo ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@Observaciones", Persona.Observaciones ?? (object)DBNull.Value);

                        rpta = command.ExecuteNonQuery() == 1 ? "OK" : "No se actualizó el registro";
                    }
                }
                catch (Exception ex)
                {
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
                try
                {
                    string sql = @"
                        UPDATE Persona 
                        SET Estado = 0,
                            FechaModificacion = GETDATE()
                        WHERE IdPersona = @IdPersona";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@IdPersona", Persona.IdPersona);

                        rpta = command.ExecuteNonQuery() == 1 ? "OK" : "No se eliminó el registro";
                    }
                }
                catch (Exception ex)
                {
                    rpta = ex.Message;
                }
            }
            return rpta;
        }

        public string CambiarTipoPersona(DPersona Persona)
        {
            string rpta = "";
            using (SqlConnection connection = GetConnection())
            {
                connection.Open();
                try
                {
                    string sql = @"
                        UPDATE Persona 
                        SET TipoPersona = @TipoPersona,
                            DNI = @DNI,
                            CIF = @CIF,
                            Nombre = @Nombre,
                            Apellidos = @Apellidos,
                            RazonSocial = @RazonSocial,
                            Genero = @Genero,
                            Email = @Email,
                            Direccion = @Direccion,
                            Telefono = @Telefono,
                            TelefonoAlternativo = @TelefonoAlternativo,
                            Observaciones = @Observaciones,
                            FechaModificacion = GETDATE()
                        WHERE IdPersona = @IdPersona";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@IdPersona", Persona.IdPersona);
                        command.Parameters.AddWithValue("@TipoPersona", Persona.TipoPersona);
                        command.Parameters.AddWithValue("@DNI", Persona.DNI ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@CIF", Persona.CIF ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@Nombre", Persona.Nombre ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@Apellidos", Persona.Apellidos ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@RazonSocial", Persona.RazonSocial ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@Genero", Persona.Genero);
                        command.Parameters.AddWithValue("@Email", Persona.Email ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@Direccion", Persona.Direccion ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@Telefono", Persona.Telefono ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@TelefonoAlternativo", Persona.TelefonoAlternativo ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@Observaciones", Persona.Observaciones ?? (object)DBNull.Value);

                        rpta = command.ExecuteNonQuery() == 1 ? "OK" : "No se actualizó el registro";
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
            DataTable dtResultado = new DataTable("Persona");
            using (SqlConnection connection = GetConnection())
            {
                connection.Open();
                try
                {
                    string sql = @"
                        SELECT 
                            IdPersona,
                            TipoPersona,
                            DNI,
                            CIF,
                            Nombre,
                            Apellidos,
                            RazonSocial,
                            Genero,
                            Email,
                            Direccion,
                            Telefono,
                            TelefonoAlternativo,
                            Estado,
                            FechaRegistro,
                            Observaciones,
                            CASE 
                                WHEN TipoPersona = 'F' THEN Nombre + ' ' + ISNULL(Apellidos, '')
                                ELSE RazonSocial
                            END AS NombreCompleto
                        FROM Persona 
                        WHERE Estado = 1 
                        ORDER BY FechaRegistro DESC";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
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

        public DataTable BuscarTexto(DPersona Persona)
        {
            DataTable dtResultado = new DataTable("Persona");
            using (SqlConnection connection = GetConnection())
            {
                connection.Open();
                try
                {
                    string sql = @"
                        SELECT 
                            IdPersona,
                            TipoPersona,
                            DNI,
                            CIF,
                            Nombre,
                            Apellidos,
                            RazonSocial,
                            Genero,
                            Email,
                            Direccion,
                            Telefono,
                            TelefonoAlternativo,
                            Estado,
                            FechaRegistro,
                            Observaciones,
                            CASE 
                                WHEN TipoPersona = 'F' THEN Nombre + ' ' + ISNULL(Apellidos, '')
                                ELSE RazonSocial
                            END AS NombreCompleto
                        FROM Persona 
                        WHERE Estado = 1 
                          AND (
                            Email LIKE '%' + @TextoBuscar + '%' OR
                            Nombre LIKE '%' + @TextoBuscar + '%' OR
                            Apellidos LIKE '%' + @TextoBuscar + '%' OR
                            RazonSocial LIKE '%' + @TextoBuscar + '%' OR
                            DNI LIKE '%' + @TextoBuscar + '%' OR
                            CIF LIKE '%' + @TextoBuscar + '%' OR
                            Telefono LIKE '%' + @TextoBuscar + '%'
                          )
                        ORDER BY FechaRegistro DESC";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@TextoBuscar", Persona.TextoBuscar ?? "");

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

        public DataTable BuscarPorTipo(char tipoPersona)
        {
            DataTable dtResultado = new DataTable("Persona");
            using (SqlConnection connection = GetConnection())
            {

                connection.Open();
                try
                {
                    string sql = @"
                        SELECT 
                            IdPersona,
                            TipoPersona,
                            DNI,
                            CIF,
                            Nombre,
                            Apellidos,
                            RazonSocial,
                            Genero,
                            Email,
                            Direccion,
                            Telefono,
                            TelefonoAlternativo,
                            Estado,
                            FechaRegistro,
                            Observaciones,
                            CASE 
                                WHEN TipoPersona = 'F' THEN Nombre + ' ' + ISNULL(Apellidos, '')
                                ELSE RazonSocial
                            END AS NombreCompleto
                        FROM Persona 
                        WHERE Estado = 1 AND TipoPersona = @TipoPersona
                        ORDER BY 
                            CASE 
                                WHEN TipoPersona = 'F' THEN Apellidos + ', ' + Nombre
                                ELSE RazonSocial
                            END";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@TipoPersona", tipoPersona);

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